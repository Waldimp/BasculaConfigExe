Imports System.IO
Imports System.IO.Ports
Imports System.Text

''' <summary>Situación en la que se encuentra el lector, para pintarla en la ventana.</summary>
Public Enum EstadoLector
    Detenido
    Leyendo
    SinConexion
    ConfiguracionIncompleta
End Enum

''' <summary>
''' Se encarga del puerto serial y de obtener el peso.
'''
''' La lógica de lectura y de extracción del peso es la misma que venía funcionando:
''' se toma lo que haya en el buffer con ReadExisting y se recorta con Mid usando la
''' posición de inicio y la cantidad de caracteres configuradas. Eso no se modificó.
'''
''' Lo que se agregó alrededor: límites de tiempo para que una operación no se quede
''' colgada para siempre, reconexión automática, y registro de lo que ocurre.
''' </summary>
Public Class LectorBascula

    ''' <summary>Se dispara cuando se obtuvo un peso. Puede llegar desde un hilo secundario.</summary>
    Public Event PesoRecibido(peso As String, tramaCruda As String, anomalia As String)

    ''' <summary>Se dispara cuando cambia la situación del lector. Puede llegar desde un hilo secundario.</summary>
    Public Event EstadoCambiado(estado As EstadoLector, mensaje As String)

    Private _puerto As SerialPort
    Private ReadOnly _config As ConfiguracionBascula
    Private ReadOnly _bloqueo As New Object()

    ''' <summary>Evita que dos ciclos se solapen si uno tarda más que el intervalo.</summary>
    Private _cicloEnCurso As Boolean = False

    Private _estadoActual As EstadoLector = EstadoLector.Detenido
    Private _mensajeActual As String = ""
    Private _debeReconectar As Boolean = False

    Public Sub New(configuracion As ConfiguracionBascula)
        _config = configuracion
    End Sub

    Public ReadOnly Property Estado As EstadoLector
        Get
            Return _estadoActual
        End Get
    End Property

    Public ReadOnly Property Mensaje As String
        Get
            Return _mensajeActual
        End Get
    End Property

    ''' <summary>
    ''' Ejecuta un ciclo de lectura. Lo llama el temporizador de la ventana.
    ''' Si el ciclo anterior sigue trabajando, este se salta sin hacer nada.
    ''' </summary>
    Public Sub EjecutarCiclo()
        SyncLock _bloqueo
            If _cicloEnCurso Then
                Registro.Advertencia("Se omitió un ciclo porque el anterior seguía en curso")
                Return
            End If
            _cicloEnCurso = True
        End SyncLock

        Try
            Dim motivo As String = ""
            If Not AsegurarPuertoAbierto(motivo) Then
                CambiarEstado(EstadoLector.SinConexion, motivo)
                Return
            End If

            If _config.EsModoDemanda Then
                ' Modo bajo demanda: se le pide el peso a la báscula y la respuesta llega
                ' por el evento DataReceived, igual que en la versión anterior.
                _puerto.WriteLine(_config.CaracterDeSolicitud)
            Else
                ' Modo continuo: la báscula transmite sola, se lee lo que haya llegado.
                ProcesarTrama(_puerto.ReadExisting())
            End If

            CambiarEstado(EstadoLector.Leyendo, "")

        Catch ex As Exception
            MarcarFalla(ex)
        Finally
            SyncLock _bloqueo
                _cicloEnCurso = False
            End SyncLock
        End Try
    End Sub

    ''' <summary>Cierra el puerto y deja el lector detenido.</summary>
    Public Sub Detener()
        CerrarPuerto()
        CambiarEstado(EstadoLector.Detenido, "")
    End Sub

#Region "Puerto"

    Private Function AsegurarPuertoAbierto(ByRef motivo As String) As Boolean
        motivo = ""

        If _debeReconectar Then
            CerrarPuerto()
            _debeReconectar = False
        End If

        If _puerto IsNot Nothing AndAlso _puerto.IsOpen Then Return True

        ' Se revisa primero que el puerto exista: así, si desconectaron el cable, el
        ' mensaje es claro en lugar de una excepción genérica.
        If Not PuertoDisponible(_config.Puerto) Then
            motivo = String.Format("No se encuentra el puerto {0}. Revisa que el cable esté conectado.", _config.Puerto)
            If _estadoActual <> EstadoLector.SinConexion Then
                Registro.Advertencia("El puerto " & _config.Puerto & " no aparece entre los disponibles")
            End If
            Return False
        End If

        Try
            CrearPuerto()
            _puerto.Open()
            Registro.Informacion("Puerto " & _config.Puerto & " abierto")
            Return True

        Catch ex As UnauthorizedAccessException
            motivo = String.Format("El puerto {0} está siendo usado por otro programa.", _config.Puerto)
            Registro.Error_("Puerto ocupado", ex)
            CerrarPuerto()
            Return False

        Catch ex As Exception
            motivo = String.Format("No se pudo abrir el puerto {0}. Revisa la conexión con la báscula.", _config.Puerto)
            Registro.Error_("Fallo al abrir el puerto", ex)
            CerrarPuerto()
            Return False
        End Try
    End Function

    Private Shared Function PuertoDisponible(nombre As String) As Boolean
        If String.IsNullOrEmpty(nombre) Then Return False
        Try
            For Each disponible As String In SerialPort.GetPortNames()
                If String.Equals(disponible, nombre, StringComparison.OrdinalIgnoreCase) Then Return True
            Next
        Catch
            ' Si no se puede consultar la lista, se deja que el intento de apertura decida.
            Return True
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Crea un objeto SerialPort nuevo en cada conexión.
    '''
    ''' No se reutiliza el anterior a propósito: cuando se desconecta físicamente un
    ''' adaptador USB a serial, el objeto queda en un estado del que no se recupera y
    ''' hasta cerrarlo puede quedarse bloqueado. Desecharlo y crear uno limpio es la
    ''' forma confiable de reconectar.
    ''' </summary>
    Private Sub CrearPuerto()
        Dim puerto As New SerialPort()
        puerto.PortName = _config.Puerto
        puerto.BaudRate = _config.BaudRate
        puerto.DataBits = _config.DataBits
        puerto.Parity = _config.Paridad
        puerto.Handshake = _config.ControlDeFlujo

        ' Sin estos límites, una escritura sobre un puerto cuyo dispositivo no responde
        ' se queda esperando para siempre y cuelga la aplicación. Antes no se notaba
        ' porque el programa se cerraba tras una sola lectura.
        Dim limite As Integer = LimiteDeEsperaEnMilisegundos()
        puerto.ReadTimeout = limite
        puerto.WriteTimeout = limite

        AddHandler puerto.DataReceived, AddressOf AlRecibirDatos

        _puerto = puerto
    End Sub

    Private Function LimiteDeEsperaEnMilisegundos() As Integer
        ' Nunca más de dos segundos, y siempre por debajo del intervalo configurado
        ' para que un ciclo no invada al siguiente.
        Dim mitadDelIntervalo As Integer = (_config.IntervaloSegundos * 1000) \ 2
        Return Math.Max(500, Math.Min(2000, mitadDelIntervalo))
    End Function

    Private Sub CerrarPuerto()
        Dim puerto As SerialPort = _puerto
        _puerto = Nothing
        If puerto Is Nothing Then Return

        Try
            RemoveHandler puerto.DataReceived, AddressOf AlRecibirDatos
        Catch
        End Try

        Try
            If puerto.IsOpen Then puerto.Close()
        Catch ex As Exception
            Registro.Advertencia("El puerto no se cerró limpiamente: " & ex.Message)
        End Try

        Try
            puerto.Dispose()
        Catch
        End Try
    End Sub

    Private Sub AlRecibirDatos(remitente As Object, argumentos As SerialDataReceivedEventArgs)
        ' Este método corre en un hilo secundario del sistema, no en el de la ventana.
        Try
            Dim puerto As SerialPort = _puerto
            If puerto Is Nothing OrElse Not puerto.IsOpen Then Return
            ProcesarTrama(puerto.ReadExisting())
        Catch ex As Exception
            MarcarFalla(ex)
        End Try
    End Sub

    Private Sub MarcarFalla(ex As Exception)
        _debeReconectar = True

        Dim mensaje As String
        If TypeOf ex Is TimeoutException Then
            mensaje = "La báscula no respondió. Verifica que esté encendida."
        ElseIf TypeOf ex Is UnauthorizedAccessException Then
            mensaje = String.Format("El puerto {0} está siendo usado por otro programa.", _config.Puerto)
        ElseIf TypeOf ex Is IOException Then
            mensaje = String.Format("Se perdió la comunicación con el puerto {0}. Revisa el cable.", _config.Puerto)
        ElseIf TypeOf ex Is InvalidOperationException Then
            mensaje = String.Format("El puerto {0} se cerró inesperadamente. Reintentando.", _config.Puerto)
        Else
            mensaje = "Ocurrió un problema al leer la báscula. Reintentando."
        End If

        Registro.Error_("Fallo durante la lectura", ex)
        CambiarEstado(EstadoLector.SinConexion, mensaje)
    End Sub

#End Region

#Region "Lectura del peso"

    ''' <summary>
    ''' Extrae el peso de la trama recibida y lo guarda.
    '''
    ''' El recorte con Mid y la condición de longitud son los mismos que traía la versión
    ''' anterior. Lo nuevo es que la trama cruda queda registrada y que, si algo se ve
    ''' raro, se avisa en pantalla; pero el peso se escribe igual, sin bloquear nada.
    ''' </summary>
    Private Sub ProcesarTrama(tramaCruda As String)
        If tramaCruda Is Nothing Then Return

        If tramaCruda <> "" AndAlso tramaCruda.Length > _config.PosicionInicio Then

            Dim peso As String = Mid(tramaCruda, _config.PosicionInicio, _config.CantidadCaracteres)

            Dim anomalia As String = RevisarPeso(tramaCruda, peso)
            Registro.Trama(tramaCruda, peso)

            Dim problemaAlGuardar As String = GuardarPeso(peso)
            If problemaAlGuardar <> "" Then
                CambiarEstado(EstadoLector.SinConexion, problemaAlGuardar)
                Return
            End If

            RaiseEvent PesoRecibido(peso, tramaCruda, anomalia)

            Try
                If _puerto IsNot Nothing AndAlso _puerto.IsOpen Then _puerto.DiscardInBuffer()
            Catch
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Revisa si el peso extraído se ve sospechoso. Solo informa: nunca impide que se
    ''' guarde, porque hoy el sistema del cliente funciona con lo que llegue y bloquear
    ''' la escritura sería un cambio de comportamiento que no se puede probar sin báscula.
    ''' </summary>
    Private Function RevisarPeso(tramaCruda As String, peso As String) As String
        Dim caracteresNecesarios As Integer = _config.PosicionInicio + _config.CantidadCaracteres - 1

        If tramaCruda.Length < caracteresNecesarios Then
            Return "La báscula envió menos datos de los esperados; el peso podría estar incompleto."
        End If

        If peso Is Nothing OrElse peso.Trim().Length = 0 Then
            Return "La báscula envió datos, pero en la posición configurada no había ningún peso."
        End If

        Dim numero As Double
        Dim limpio As String = peso.Trim().Replace(",", ".")
        If Not Double.TryParse(limpio, Globalization.NumberStyles.Any,
                               Globalization.CultureInfo.InvariantCulture, numero) Then
            Return "El peso leído no parece un número. Revisa la posición y la cantidad de caracteres."
        End If

        Return ""
    End Function

    ''' <summary>
    ''' Escribe el peso en pesas.txt. Devuelve cadena vacía si todo salió bien, o el
    ''' motivo en lenguaje natural si falló.
    '''
    ''' El archivo conserva exactamente el mismo formato de siempre: únicamente el peso
    ''' seguido de un salto de línea, en texto plano. El sistema del cliente lo lee así
    ''' y se reescribe en cada lectura para que la fecha de modificación que ven los
    ''' operativos en el explorador de Windows siga actualizándose.
    ''' </summary>
    Private Function GuardarPeso(peso As String) As String
        Dim carpeta As String = _config.CarpetaDestino
        Dim archivo As String = _config.ArchivoDePeso

        Try
            If Not Directory.Exists(carpeta) Then Directory.CreateDirectory(carpeta)
            File.WriteAllText(archivo, peso & vbCrLf, Encoding.ASCII)
            Return ""

        Catch ex As UnauthorizedAccessException
            Registro.Error_("Sin permisos para escribir el peso", ex)
            Return String.Format("No se puede guardar en {0}. Revisa los permisos de la carpeta.", carpeta)

        Catch ex As DirectoryNotFoundException
            Registro.Error_("Carpeta de destino inexistente", ex)
            Return String.Format("No se encuentra la carpeta {0}. Revísala en el programa de configuración.", carpeta)

        Catch ex As IOException
            Registro.Error_("Fallo de escritura del peso", ex)
            Return String.Format("No se pudo guardar el peso en {0}. Puede que el archivo esté abierto.", carpeta)

        Catch ex As Exception
            Registro.Error_("Fallo inesperado al guardar el peso", ex)
            Return "No se pudo guardar el peso. Revisa la carpeta de destino."
        End Try
    End Function

#End Region

    Private Sub CambiarEstado(estado As EstadoLector, mensaje As String)
        If _estadoActual = estado AndAlso _mensajeActual = mensaje Then Return

        _estadoActual = estado
        _mensajeActual = mensaje
        RaiseEvent EstadoCambiado(estado, mensaje)
    End Sub

End Class
