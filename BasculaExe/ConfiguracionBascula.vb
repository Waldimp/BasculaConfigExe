Imports System.IO
Imports System.IO.Ports

''' <summary>
''' Lee la configuración que dejó el programa configurador en C:\config\ y la valida.
'''
''' Antes, cada valor se leía con ReadAllText directo en el arranque: si faltaba un solo
''' archivo la aplicación moría con una excepción invisible. Ahora se recogen todos los
''' problemas y se explican en lenguaje natural para que el operativo sepa qué hacer.
''' </summary>
Public Class ConfiguracionBascula

    ''' <summary>Carpeta donde el configurador deja los archivos de configuración.</summary>
    Public Const CarpetaConfiguracion As String = "C:\config"

    ''' <summary>Segundos entre lecturas cuando no hay un intervalo configurado.</summary>
    Public Const IntervaloPorDefecto As Integer = 5

    Public Const IntervaloMinimo As Integer = 1
    Public Const IntervaloMaximo As Integer = 3600

    Public Property Modo As String = ""
    Public Property Puerto As String = ""
    Public Property BaudRate As Integer = 9600
    Public Property DataBits As Integer = 8
    Public Property Paridad As Parity = Parity.None
    Public Property ControlDeFlujo As Handshake = Handshake.None
    Public Property PosicionInicio As Integer = 0
    Public Property CantidadCaracteres As Integer = 0
    Public Property CarpetaSalida As String = ""
    Public Property CaracterDeSolicitud As String = ""
    Public Property ArranqueAutomatico As Boolean = False
    Public Property IntervaloSegundos As Integer = IntervaloPorDefecto

    Private ReadOnly _problemas As New List(Of String)()

    ''' <summary>Lista de problemas encontrados, redactados para que los lea un operativo.</summary>
    Public ReadOnly Property Problemas As List(Of String)
        Get
            Return _problemas
        End Get
    End Property

    Public ReadOnly Property EsValida As Boolean
        Get
            Return _problemas.Count = 0
        End Get
    End Property

    Public ReadOnly Property EsModoDemanda As Boolean
        Get
            Return String.Equals(Modo, "Demanda", StringComparison.OrdinalIgnoreCase)
        End Get
    End Property

    ''' <summary>Resumen corto para mostrar en la ventana, por ejemplo "COM3 · Modo continuo · cada 5 s".</summary>
    Public ReadOnly Property Resumen As String
        Get
            Dim nombreModo As String = If(EsModoDemanda, "Modo bajo demanda", "Modo continuo")
            Return String.Format("{0} · {1} · cada {2} s",
                                 If(String.IsNullOrEmpty(Puerto), "Sin puerto", Puerto),
                                 nombreModo,
                                 IntervaloSegundos)
        End Get
    End Property

    ''' <summary>Carpeta final donde se escribe pesas.txt.</summary>
    Public ReadOnly Property CarpetaDestino As String
        Get
            If String.IsNullOrEmpty(CarpetaSalida) Then Return ""
            Return Path.Combine(CarpetaSalida, "BasculaNET")
        End Get
    End Property

    ''' <summary>Ruta completa del archivo de peso.</summary>
    Public ReadOnly Property ArchivoDePeso As String
        Get
            Dim destino As String = CarpetaDestino
            If String.IsNullOrEmpty(destino) Then Return ""
            Return Path.Combine(destino, "pesas.txt")
        End Get
    End Property

    ''' <summary>Primer problema encontrado, o cadena vacía si la configuración está completa.</summary>
    Public ReadOnly Property PrimerProblema As String
        Get
            If _problemas.Count = 0 Then Return ""
            Return _problemas(0)
        End Get
    End Property

    ''' <summary>
    ''' Lee la configuración completa desde disco. Nunca lanza excepciones: todo lo que
    ''' falte o venga mal queda anotado en <see cref="Problemas"/>.
    ''' </summary>
    Public Shared Function Cargar() As ConfiguracionBascula
        Dim config As New ConfiguracionBascula()
        config.LeerTodo()
        Return config
    End Function

    Private Sub LeerTodo()
        If Not Directory.Exists(CarpetaConfiguracion) Then
            _problemas.Add("No se encuentra la configuración. Abre el programa de configuración y complétala.")
            Return
        End If

        Modo = LeerTexto("modo.txt")
        If String.IsNullOrEmpty(Modo) Then
            _problemas.Add("Falta elegir el modo de lectura. Ábrelo en el programa de configuración.")
        ElseIf Not EsModoDemanda AndAlso Not String.Equals(Modo, "Continuo", StringComparison.OrdinalIgnoreCase) Then
            _problemas.Add("El modo de lectura guardado no es válido. Vuelve a elegirlo en el programa de configuración.")
        End If

        LeerParametrosDelPuerto()

        Dim prefijo As String = If(EsModoDemanda, "Demanda_", "Continuo_")

        Puerto = LeerTexto(prefijo & "Puerto.txt")
        If String.IsNullOrEmpty(Puerto) Then
            _problemas.Add("Falta elegir el puerto de la báscula. Ábrelo en el programa de configuración.")
        End If

        PosicionInicio = LeerEntero(prefijo & "Inicio.txt", 0)
        If PosicionInicio <= 0 Then
            _problemas.Add("Falta indicar en qué carácter empieza el peso. Ábrelo en el programa de configuración.")
        End If

        CantidadCaracteres = LeerEntero(prefijo & "Ocupar.txt", 0)
        If CantidadCaracteres <= 0 Then
            _problemas.Add("Falta indicar cuántos caracteres ocupa el peso. Ábrelo en el programa de configuración.")
        End If

        CarpetaSalida = LeerTexto(prefijo & "Ruta.txt")
        If String.IsNullOrEmpty(CarpetaSalida) Then
            _problemas.Add("Falta elegir la carpeta donde se guardará el peso. Ábrelo en el programa de configuración.")
        End If

        If EsModoDemanda Then
            ' El carácter de solicitud se lee sin recortar espacios: un espacio puede ser
            ' parte de lo que la báscula espera recibir. Solo se quita el salto de línea
            ' que agrega el configurador al guardar el archivo.
            CaracterDeSolicitud = LeerTextoSinRecortar("Demanda_Caracter.txt")
            If String.IsNullOrEmpty(CaracterDeSolicitud) Then
                _problemas.Add("Falta indicar qué carácter se le envía a la báscula para pedirle el peso.")
            End If
        End If

        LeerOpcionesDeArranque()
    End Sub

    Private Sub LeerParametrosDelPuerto()
        BaudRate = LeerEntero("BaudRate.txt", 9600)
        If BaudRate <= 0 Then
            BaudRate = 9600
            _problemas.Add("La velocidad del puerto guardada no es válida. Revísala en el programa de configuración.")
        End If

        DataBits = LeerEntero("DataBits.txt", 8)
        If DataBits < 5 OrElse DataBits > 8 Then
            DataBits = 8
            _problemas.Add("Los bits de datos guardados no son válidos. Revísalos en el programa de configuración.")
        End If

        ' El configurador guarda el índice de la lista desplegable, no el nombre.
        ' Los índices coinciden con los valores de System.IO.Ports.
        Paridad = CType(LimitarEntero(LeerEntero("Parity.txt", 0), 0, 4), Parity)
        ControlDeFlujo = CType(LimitarEntero(LeerEntero("Handshake.txt", 0), 0, 3), Handshake)
    End Sub

    Private Sub LeerOpcionesDeArranque()
        ' Estas dos opciones son nuevas. Una instalación configurada con la versión
        ' anterior no las tiene, así que se asumen valores seguros: no arrancar sola
        ' y leer cada cinco segundos.
        Dim arranque As String = LeerTexto("Arranque.txt")
        ArranqueAutomatico = String.Equals(arranque, "Automatico", StringComparison.OrdinalIgnoreCase)

        Dim intervalo As Integer = LeerEntero("Intervalo.txt", IntervaloPorDefecto)
        If intervalo < IntervaloMinimo OrElse intervalo > IntervaloMaximo Then
            intervalo = IntervaloPorDefecto
        End If
        IntervaloSegundos = intervalo
    End Sub

    Private Function LeerTexto(nombreArchivo As String) As String
        Return LeerTextoSinRecortar(nombreArchivo).Trim()
    End Function

    Private Function LeerTextoSinRecortar(nombreArchivo As String) As String
        Dim ruta As String = Path.Combine(CarpetaConfiguracion, nombreArchivo)
        Try
            If Not File.Exists(ruta) Then Return ""
            ' Mismo criterio que la versión anterior: se elimina el salto de línea que
            ' agrega WriteLine al guardar, sin tocar el resto del contenido.
            Return File.ReadAllText(ruta).Replace(vbCrLf, "").Replace(vbLf, "")
        Catch ex As Exception
            Registro.Error_("No se pudo leer " & nombreArchivo, ex)
            Return ""
        End Try
    End Function

    Private Function LeerEntero(nombreArchivo As String, valorPorDefecto As Integer) As Integer
        Dim texto As String = LeerTexto(nombreArchivo)
        If String.IsNullOrEmpty(texto) Then Return valorPorDefecto

        Dim numero As Integer
        If Integer.TryParse(texto, numero) Then Return numero
        Return valorPorDefecto
    End Function

    Private Shared Function LimitarEntero(valor As Integer, minimo As Integer, maximo As Integer) As Integer
        If valor < minimo Then Return minimo
        If valor > maximo Then Return maximo
        Return valor
    End Function

End Class
