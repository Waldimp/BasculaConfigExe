Imports System.Drawing
Imports System.IO
Imports System.Reflection
Imports System.Runtime.InteropServices

''' <summary>
''' Ventana del lector de báscula.
'''
''' A diferencia de la versión anterior, que se abría oculta, tomaba un peso y se cerraba,
''' esta se queda encendida repitiendo la lectura cada cierto tiempo. Muestra en todo
''' momento qué está pasando y permite detener o reanudar sin cerrar el programa.
'''
''' Según lo que se haya elegido en el programa de configuración, empieza a leer sola al
''' encender la computadora o espera a que el operativo lo indique.
''' </summary>
Public Class Form1

    Private Const MargenDePantalla As Integer = 16
    Private Const NombreDelIcono As String = "basculas.ico"

    ''' <summary>Posición del botón cuando hay un aviso visible debajo del detalle.</summary>
    Private Const AltoBotonConAviso As Integer = 284

    ''' <summary>Posición del botón cuando no hay aviso y la ventana se compacta.</summary>
    Private Const AltoBotonSinAviso As Integer = 216

    Private _config As ConfiguracionBascula
    Private _lector As LectorBascula
    Private _leyendo As Boolean = False
    Private _proximaLectura As DateTime = DateTime.MinValue
    Private _avisoDeAnomalia As String = ""
    Private _yaSeAvisoDeLaBandeja As Boolean = False

#Region "Arranque"

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Registro.Informacion("--- Lector de báscula iniciado ---")

        AplicarIconos()
        ColocarEnEsquinaInferiorDerecha()

        _config = ConfiguracionBascula.Cargar()
        _lector = New LectorBascula(_config)
        AddHandler _lector.PesoRecibido, AddressOf AlRecibirPeso
        AddHandler _lector.EstadoCambiado, AddressOf AlCambiarEstado

        tmrLectura.Interval = _config.IntervaloSegundos * 1000
        lblDetalle.Text = _config.Resumen
        tmrInterfaz.Start()

        If Not _config.EsValida Then
            Registro.Advertencia("Configuración incompleta: " & String.Join(" / ", _config.Problemas))
            MostrarConfiguracionIncompleta()
            Return
        End If

        If _config.ArranqueAutomatico Then
            Registro.Informacion("Arranque automático activado, comenzando a leer")
            IniciarLectura()
        Else
            Registro.Informacion("Arranque manual, esperando al operativo")
            PintarEstado(EstadoLector.Detenido, "")
        End If
    End Sub

    Private Sub AplicarIconos()
        Try
            Dim iconoGrande As Icon = CargarIcono(32)
            Dim iconoChico As Icon = CargarIcono(16)

            If iconoGrande Is Nothing OrElse iconoChico Is Nothing Then
                Registro.Advertencia("No se encontró el icono incrustado; se usará el que trae Windows por defecto")
                Return
            End If

            Me.Icon = iconoGrande
            iconoBandeja.Icon = iconoChico
        Catch ex As Exception
            Registro.Advertencia("No se pudo cargar el icono: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Carga el icono incrustado en el ejecutable, en el tamaño pedido.
    '''
    ''' El nombre del recurso se busca en lugar de escribirlo fijo porque en los
    ''' proyectos de VB el nombre lógico no incluye la carpeta: un archivo que está en
    ''' Resources\ queda como "BasculaExe.basculas.ico" y no como se esperaría.
    ''' </summary>
    Private Shared Function CargarIcono(tamano As Integer) As Icon
        Dim ensamblado As Assembly = Assembly.GetExecutingAssembly()
        Dim nombre As String = Nothing

        For Each candidato As String In ensamblado.GetManifestResourceNames()
            If candidato.EndsWith(NombreDelIcono, StringComparison.OrdinalIgnoreCase) Then
                nombre = candidato
                Exit For
            End If
        Next

        If nombre Is Nothing Then Return Nothing

        Using flujo As Stream = ensamblado.GetManifestResourceStream(nombre)
            If flujo Is Nothing Then Return Nothing
            Return New Icon(flujo, New Size(tamano, tamano))
        End Using
    End Function

    ''' <summary>Coloca la ventana en la esquina inferior derecha, sin tapar la barra de tareas.</summary>
    Private Sub ColocarEnEsquinaInferiorDerecha()
        Dim area As Rectangle = Screen.PrimaryScreen.WorkingArea
        Me.Location = New Point(area.Right - Me.Width - MargenDePantalla,
                                area.Bottom - Me.Height - MargenDePantalla)
    End Sub

    Private Sub MostrarConfiguracionIncompleta()
        btnPrincipal.Enabled = False
        menuAlternar.Enabled = False
        PintarEstado(EstadoLector.ConfiguracionIncompleta, _config.PrimerProblema)
    End Sub

#End Region

#Region "Encender y apagar la lectura"

    Private Sub IniciarLectura()
        If _leyendo Then Return

        _leyendo = True
        btnPrincipal.Text = "Detener"
        menuAlternar.Text = "Detener lectura"
        ProgramarProximaLectura()
        tmrLectura.Start()

        Registro.Informacion("Lectura iniciada")

        ' Primer ciclo de una vez, para no hacer esperar un intervalo completo.
        EjecutarUnCiclo()
    End Sub

    Private Sub DetenerLectura()
        If Not _leyendo Then Return

        _leyendo = False
        tmrLectura.Stop()
        btnPrincipal.Text = "Iniciar"
        menuAlternar.Text = "Iniciar lectura"
        pnlBarraAvance.Width = 0
        _avisoDeAnomalia = ""

        _lector.Detener()
        Registro.Informacion("Lectura detenida por el operativo")
    End Sub

    Private Sub EjecutarUnCiclo()
        ProgramarProximaLectura()
        _lector.EjecutarCiclo()
    End Sub

    Private Sub ProgramarProximaLectura()
        _proximaLectura = DateTime.Now.AddSeconds(_config.IntervaloSegundos)
    End Sub

    Private Sub tmrLectura_Tick(sender As Object, e As EventArgs) Handles tmrLectura.Tick
        EjecutarUnCiclo()
    End Sub

#End Region

#Region "Avisos del lector"

    ''' <summary>
    ''' El lector puede avisar desde un hilo secundario, porque los datos del puerto
    ''' llegan por un evento del sistema. Todo lo que toque la ventana se reenvía al
    ''' hilo de la interfaz en lugar de desactivar la comprobación, como se hacía antes.
    ''' </summary>
    Private Sub EnLaVentana(accion As MethodInvoker)
        If Me.IsDisposed OrElse Not Me.IsHandleCreated Then Return
        Try
            If Me.InvokeRequired Then
                Me.BeginInvoke(accion)
            Else
                accion()
            End If
        Catch ex As Exception
            Registro.Advertencia("No se pudo actualizar la ventana: " & ex.Message)
        End Try
    End Sub

    Private Sub AlRecibirPeso(peso As String, tramaCruda As String, anomalia As String)
        EnLaVentana(Sub()
                        lblPeso.Text = If(String.IsNullOrWhiteSpace(peso), "--", peso.Trim())
                        lblUltimaLectura.Text = "Última lectura a las " & DateTime.Now.ToString("HH:mm:ss")
                        _avisoDeAnomalia = anomalia
                        If _lector.Estado = EstadoLector.Leyendo Then PintarEstado(EstadoLector.Leyendo, "")
                    End Sub)
    End Sub

    Private Sub AlCambiarEstado(estado As EstadoLector, mensaje As String)
        EnLaVentana(Sub() PintarEstado(estado, mensaje))
    End Sub

    Private Sub PintarEstado(estado As EstadoLector, mensaje As String)
        Select Case estado
            Case EstadoLector.Leyendo
                lblIndicador.ForeColor = Color.FromArgb(46, 160, 67)
                lblEstado.Text = "Leyendo la báscula"

            Case EstadoLector.SinConexion
                lblIndicador.ForeColor = Color.FromArgb(219, 154, 4)
                lblEstado.Text = "Sin comunicación"

            Case EstadoLector.ConfiguracionIncompleta
                lblIndicador.ForeColor = Color.FromArgb(178, 8, 55)
                lblEstado.Text = "Falta configurar"

            Case Else
                lblIndicador.ForeColor = Color.Silver
                lblEstado.Text = "Detenido"
        End Select

        ' Un problema de comunicación tiene prioridad sobre un aviso de lectura rara.
        Dim textoDelAviso As String = If(String.IsNullOrEmpty(mensaje), _avisoDeAnomalia, mensaje)
        MostrarAviso(textoDelAviso)

        iconoBandeja.Text = Recortar("Lector de báscula" & vbCrLf & lblEstado.Text, 63)
    End Sub

    Private Sub MostrarAviso(texto As String)
        Dim hayAviso As Boolean = Not String.IsNullOrEmpty(texto)

        lblAviso.Text = If(hayAviso, texto, "")
        pnlAviso.Visible = hayAviso
        AjustarAltoDeLaVentana(hayAviso)
    End Sub

    ''' <summary>
    ''' La ventana se encoge cuando no hay ningún aviso que mostrar, para que no quede
    ''' un hueco vacío. Crece hacia arriba conservando el borde inferior, así no salta
    ''' de lugar si el operativo la movió a otra parte de la pantalla.
    ''' </summary>
    Private Sub AjustarAltoDeLaVentana(conAviso As Boolean)
        Dim posicionDelBoton As Integer = If(conAviso, AltoBotonConAviso, AltoBotonSinAviso)
        If btnPrincipal.Top = posicionDelBoton Then Return

        Dim bordeInferior As Integer = Me.Bottom

        btnPrincipal.Top = posicionDelBoton
        lnkRegistro.Top = posicionDelBoton + 12
        Me.ClientSize = New Size(Me.ClientSize.Width, posicionDelBoton + btnPrincipal.Height + 20)
        Me.Top = bordeInferior - Me.Height
    End Sub

    ''' <summary>El texto emergente del icono de la bandeja no admite más de 63 caracteres.</summary>
    Private Shared Function Recortar(texto As String, largoMaximo As Integer) As String
        If String.IsNullOrEmpty(texto) OrElse texto.Length <= largoMaximo Then Return texto
        Return texto.Substring(0, largoMaximo)
    End Function

#End Region

#Region "Cuenta regresiva"

    Private Sub tmrInterfaz_Tick(sender As Object, e As EventArgs) Handles tmrInterfaz.Tick
        If Not _leyendo Then
            If pnlBarraAvance.Width <> 0 Then pnlBarraAvance.Width = 0
            Return
        End If

        Dim total As Double = _config.IntervaloSegundos
        Dim restante As Double = (_proximaLectura - DateTime.Now).TotalSeconds
        If restante < 0 Then restante = 0
        If total <= 0 Then total = 1

        Dim avance As Double = 1.0R - (restante / total)
        If avance < 0 Then avance = 0
        If avance > 1 Then avance = 1

        pnlBarraAvance.Width = CInt(pnlBarraFondo.Width * avance)
    End Sub

#End Region

#Region "Botones y ventana"

    Private Sub btnPrincipal_Click(sender As Object, e As EventArgs) Handles btnPrincipal.Click
        AlternarLectura()
    End Sub

    Private Sub menuAlternar_Click(sender As Object, e As EventArgs) Handles menuAlternar.Click
        AlternarLectura()
    End Sub

    Private Sub AlternarLectura()
        If _leyendo Then
            DetenerLectura()
        Else
            ' La configuración pudo cambiar mientras la ventana estaba abierta.
            RecargarConfiguracion()
            If Not _config.EsValida Then
                MostrarConfiguracionIncompleta()
                Return
            End If
            btnPrincipal.Enabled = True
            menuAlternar.Enabled = True
            IniciarLectura()
        End If
    End Sub

    ''' <summary>
    ''' Vuelve a leer C:\config antes de arrancar. Así, si el técnico acaba de cambiar
    ''' algo en el programa de configuración, no hace falta cerrar y abrir el lector.
    ''' </summary>
    Private Sub RecargarConfiguracion()
        _config = ConfiguracionBascula.Cargar()

        RemoveHandler _lector.PesoRecibido, AddressOf AlRecibirPeso
        RemoveHandler _lector.EstadoCambiado, AddressOf AlCambiarEstado
        _lector.Detener()

        _lector = New LectorBascula(_config)
        AddHandler _lector.PesoRecibido, AddressOf AlRecibirPeso
        AddHandler _lector.EstadoCambiado, AddressOf AlCambiarEstado

        tmrLectura.Interval = _config.IntervaloSegundos * 1000
        lblDetalle.Text = _config.Resumen
    End Sub

    Private Sub btnMinimizar_Click(sender As Object, e As EventArgs) Handles btnMinimizar.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    ''' <summary>
    ''' La equis esconde la ventana en lugar de cerrar el programa: si cerrara, se
    ''' detendría la lectura sin que el operativo se diera cuenta. Para salir de verdad
    ''' está la opción Salir del menú del icono junto al reloj.
    ''' </summary>
    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        OcultarEnLaBandeja()
    End Sub

    Private Sub OcultarEnLaBandeja()
        Me.Hide()

        If Not _yaSeAvisoDeLaBandeja Then
            _yaSeAvisoDeLaBandeja = True
            Try
                iconoBandeja.BalloonTipTitle = "El lector sigue trabajando"
                iconoBandeja.BalloonTipText = "Quedó junto al reloj. Haz doble clic para volver a mostrarlo."
                iconoBandeja.ShowBalloonTip(4000)
            Catch
            End Try
        End If
    End Sub

    Private Sub MostrarVentana()
        Me.Show()
        Me.WindowState = FormWindowState.Normal
        ColocarEnEsquinaInferiorDerecha()
        Me.Activate()
    End Sub

    Private Sub menuMostrar_Click(sender As Object, e As EventArgs) Handles menuMostrar.Click
        MostrarVentana()
    End Sub

    Private Sub iconoBandeja_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles iconoBandeja.MouseDoubleClick
        If Me.Visible AndAlso Me.WindowState = FormWindowState.Normal Then
            OcultarEnLaBandeja()
        Else
            MostrarVentana()
        End If
    End Sub

    Private Sub menuSalir_Click(sender As Object, e As EventArgs) Handles menuSalir.Click
        Registro.Informacion("El operativo cerró el lector")
        CerrarTodo()
        Application.Exit()
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If e.CloseReason = CloseReason.UserClosing Then
            ' Alt+F4 se comporta como la equis: esconder, no salir.
            e.Cancel = True
            OcultarEnLaBandeja()
            Return
        End If
        CerrarTodo()
    End Sub

    Private Sub CerrarTodo()
        Try
            tmrLectura.Stop()
            tmrInterfaz.Stop()
            If _lector IsNot Nothing Then _lector.Detener()
            iconoBandeja.Visible = False
        Catch ex As Exception
            Registro.Advertencia("Problema al cerrar: " & ex.Message)
        End Try
    End Sub

    Private Sub lnkRegistro_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkRegistro.LinkClicked
        Try
            If File.Exists(Registro.ArchivoDeHoy) Then
                Process.Start(Registro.ArchivoDeHoy)
            ElseIf Directory.Exists(Registro.Carpeta) Then
                Process.Start(Registro.Carpeta)
            Else
                MostrarAviso("Todavía no hay registro que mostrar.")
            End If
        Catch ex As Exception
            Registro.Advertencia("No se pudo abrir el registro: " & ex.Message)
            MostrarAviso("No se pudo abrir el registro.")
        End Try
    End Sub

#End Region

#Region "Arrastrar la ventana"

    ' La ventana no tiene barra de título propia, así que se arrastra desde el encabezado.

    Private Const WM_NCLBUTTONDOWN As Integer = &HA1
    Private Const HTCAPTION As Integer = 2

    <DllImport("user32.dll")>
    Private Shared Function SendMessage(ventana As IntPtr, mensaje As Integer, parametro As Integer, texto As Integer) As Integer
    End Function

    <DllImport("user32.dll")>
    Private Shared Function ReleaseCapture() As Boolean
    End Function

    Private Sub Encabezado_MouseDown(sender As Object, e As MouseEventArgs) _
        Handles pnlEncabezado.MouseDown, lblTitulo.MouseDown

        If e.Button <> MouseButtons.Left Then Return
        ReleaseCapture()
        SendMessage(Me.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0)
    End Sub

#End Region

End Class
