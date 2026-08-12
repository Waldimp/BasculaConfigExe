Imports System.IO

''' <summary>
''' Registro de eventos en archivo de texto. Sirve para diagnosticar en sitio del cliente
''' qué pasó cuando el lector no entregó un peso, algo que hasta ahora era imposible
''' porque la aplicación corría oculta y sin dejar rastro.
'''
''' Los archivos se guardan en %LocalAppData%\BasculaNET\registro\, uno por día.
'''
''' Sobre el tamaño: leyendo cada cinco segundos las veinticuatro horas saldrían más de
''' diecisiete mil líneas diarias. Para que eso no crezca sin control hay tres medidas:
''' las tramas repetidas no se escriben una por una sino que se resumen con un contador,
''' se borran los archivos de más de <see cref="DiasQueSeConservan"/> días, y existe un
''' tope de espacio total por si algo se sale de lo previsto.
''' </summary>
Module Registro

    ''' <summary>Días de historial que se conservan antes de borrar los registros viejos.</summary>
    Public Const DiasQueSeConservan As Integer = 30

    ''' <summary>Tope de espacio para toda la carpeta de registros.</summary>
    Public Const EspacioMaximoEnMegas As Integer = 20

    ''' <summary>Cada cuánto se vuelve a escribir una trama repetida, para dejar constancia de que sigue viva.</summary>
    Private Const MinutosEntreRepeticiones As Integer = 5

    ''' <summary>Cada cuánto se revisa si hay archivos viejos que borrar.</summary>
    Private Const HorasEntreLimpiezas As Integer = 6

    Private ReadOnly _bloqueo As New Object()
    Private _ultimaLimpieza As DateTime = DateTime.MinValue

    Private _tramaAnterior As String = Nothing
    Private _repeticiones As Integer = 0
    Private _horaDeLaUltimaTrama As DateTime = DateTime.MinValue

    ''' <summary>Carpeta donde se guardan los archivos de registro.</summary>
    Public ReadOnly Property Carpeta As String
        Get
            Return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "BasculaNET", "registro")
        End Get
    End Property

    ''' <summary>
    ''' Archivo del día actual. Se calcula en cada llamada para que una sesión que lleve
    ''' días encendida cambie de archivo sola al pasar la medianoche.
    ''' </summary>
    Public ReadOnly Property ArchivoDeHoy As String
        Get
            Return Path.Combine(Carpeta, "lector-" & DateTime.Now.ToString("yyyy-MM-dd") & ".log")
        End Get
    End Property

    Public Sub Informacion(mensaje As String)
        Escribir("INFO", mensaje)
    End Sub

    Public Sub Advertencia(mensaje As String)
        Escribir("AVISO", mensaje)
    End Sub

    Public Sub Error_(mensaje As String, Optional detalle As Exception = Nothing)
        If detalle Is Nothing Then
            Escribir("ERROR", mensaje)
        Else
            Escribir("ERROR", mensaje & " | " & detalle.GetType().Name & ": " & detalle.Message)
        End If
    End Sub

    ''' <summary>
    ''' Registra la trama cruda recibida del puerto. Es la información más valiosa del
    ''' registro: permite analizar después qué envía realmente cada modelo de báscula
    ''' sin necesidad de tener el equipo enfrente.
    '''
    ''' Mientras la báscula repita exactamente la misma trama, no se escribe una línea
    ''' por lectura: se cuentan las repeticiones y se dejan anotadas cuando el valor
    ''' cambia. En una báscula en reposo eso reduce el registro a casi nada, sin perder
    ''' ninguna lectura distinta.
    ''' </summary>
    Public Sub Trama(crudo As String, pesoExtraido As String)
        SyncLock _bloqueo
            Dim ahora As DateTime = DateTime.Now
            Dim esLaMisma As Boolean = (crudo = _tramaAnterior)
            Dim pasoMuchoRato As Boolean =
                (ahora - _horaDeLaUltimaTrama).TotalMinutes >= MinutosEntreRepeticiones

            If esLaMisma AndAlso Not pasoMuchoRato Then
                _repeticiones += 1
                Return
            End If

            Dim resumen As String = ""
            If _repeticiones > 0 Then
                resumen = " (la anterior se repitió " & _repeticiones & " veces)"
            End If

            Escribir("TRAMA", "recibido=[" & Visible(crudo) & "] extraido=[" & Visible(pesoExtraido) & "]" & resumen)

            _tramaAnterior = crudo
            _repeticiones = 0
            _horaDeLaUltimaTrama = ahora
        End SyncLock
    End Sub

    ''' <summary>
    ''' Convierte los caracteres no imprimibles en algo legible, para que la trama se pueda
    ''' leer en el registro. Sin esto, los caracteres de control se pierden en el archivo.
    ''' </summary>
    Public Function Visible(texto As String) As String
        If String.IsNullOrEmpty(texto) Then Return ""

        Dim salida As New Text.StringBuilder(texto.Length + 16)
        For Each caracter As Char In texto
            Select Case AscW(caracter)
                Case 13 : salida.Append("<CR>")
                Case 10 : salida.Append("<LF>")
                Case 9 : salida.Append("<TAB>")
                Case 2 : salida.Append("<STX>")
                Case 3 : salida.Append("<ETX>")
                Case 0 To 31, 127
                    salida.Append("<" & AscW(caracter).ToString("X2") & ">")
                Case Else
                    salida.Append(caracter)
            End Select
        Next
        Return salida.ToString()
    End Function

    Private Sub Escribir(nivel As String, mensaje As String)
        SyncLock _bloqueo
            Try
                Directory.CreateDirectory(Carpeta)
                LimpiarSiToca()

                File.AppendAllText(
                    ArchivoDeHoy,
                    DateTime.Now.ToString("HH:mm:ss") & "  " & nivel.PadRight(5) & "  " & mensaje & Environment.NewLine,
                    Text.Encoding.UTF8)
            Catch
                ' El registro nunca debe tumbar la aplicación: si no se puede escribir
                ' (disco lleno, permisos), se pierde la línea y la lectura continúa.
            End Try
        End SyncLock
    End Sub

    ''' <summary>
    ''' Revisa cada cierto tiempo si hay que borrar registros viejos. No basta con
    ''' hacerlo al arrancar: este programa puede quedarse encendido semanas seguidas.
    ''' </summary>
    Private Sub LimpiarSiToca()
        If (DateTime.Now - _ultimaLimpieza).TotalHours < HorasEntreLimpiezas Then Return
        _ultimaLimpieza = DateTime.Now

        Try
            Dim archivos As New List(Of FileInfo)()
            For Each ruta As String In Directory.GetFiles(Carpeta, "lector-*.log")
                archivos.Add(New FileInfo(ruta))
            Next

            ' Primero por antigüedad.
            Dim limite As DateTime = DateTime.Now.AddDays(-DiasQueSeConservan)
            For Each archivo As FileInfo In archivos.ToArray()
                If archivo.LastWriteTime < limite Then
                    If Borrar(archivo) Then archivos.Remove(archivo)
                End If
            Next

            ' Después por espacio, quitando los más viejos hasta entrar en el tope.
            Dim tope As Long = CLng(EspacioMaximoEnMegas) * 1024L * 1024L
            Dim ocupado As Long = 0
            For Each archivo As FileInfo In archivos
                ocupado += archivo.Length
            Next

            If ocupado <= tope Then Return

            archivos.Sort(Function(uno, otro) uno.LastWriteTime.CompareTo(otro.LastWriteTime))
            For Each archivo As FileInfo In archivos
                If ocupado <= tope Then Exit For
                Dim tamano As Long = archivo.Length
                If Borrar(archivo) Then ocupado -= tamano
            Next

        Catch
            ' Si la limpieza falla, se reintenta en la siguiente revisión.
        End Try
    End Sub

    Private Function Borrar(archivo As FileInfo) As Boolean
        Try
            ' Nunca se borra el archivo del día, que es el que se está escribiendo.
            If String.Equals(archivo.FullName, ArchivoDeHoy, StringComparison.OrdinalIgnoreCase) Then Return False
            archivo.Delete()
            Return True
        Catch
            Return False
        End Try
    End Function

End Module
