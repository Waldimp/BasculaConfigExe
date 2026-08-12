Imports System.IO

''' <summary>
''' Registro de eventos en archivo de texto. Sirve para diagnosticar en sitio del cliente
''' qué pasó cuando el lector no entregó un peso, algo que hasta ahora era imposible
''' porque la aplicación corría oculta y sin dejar rastro.
'''
''' Los archivos se guardan en %LocalAppData%\BasculaNET\registro\ con un archivo por día,
''' y se conservan los últimos <see cref="DiasQueSeConservan"/> días.
''' </summary>
Module Registro

    ''' <summary>Días de historial que se conservan antes de borrar los registros viejos.</summary>
    Public Const DiasQueSeConservan As Integer = 15

    Private ReadOnly _bloqueo As New Object()
    Private _yaSeLimpio As Boolean = False

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
    ''' </summary>
    Public Sub Trama(crudo As String, pesoExtraido As String)
        Escribir("TRAMA", "recibido=[" & Visible(crudo) & "] extraido=[" & Visible(pesoExtraido) & "]")
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

                If Not _yaSeLimpio Then
                    _yaSeLimpio = True
                    BorrarRegistrosViejos()
                End If

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

    Private Sub BorrarRegistrosViejos()
        Try
            Dim limite As DateTime = DateTime.Now.AddDays(-DiasQueSeConservan)
            For Each archivo As String In Directory.GetFiles(Carpeta, "lector-*.log")
                If File.GetLastWriteTime(archivo) < limite Then
                    Try
                        File.Delete(archivo)
                    Catch
                    End Try
                End If
            Next
        Catch
        End Try
    End Sub

End Module
