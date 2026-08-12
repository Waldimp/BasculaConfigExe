Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SidePanel.Height = btnInicio.Height
        FirstCustomControl1.BringToFront()

        tmrHora.Enabled = True
        tmrTimer.Enabled = False

        lblFecha.Text = DateTime.Now.ToShortDateString()

    End Sub

    Private Sub tmrTimer_Tick(sender As Object, e As EventArgs) Handles tmrTimer.Tick
        tmrHora.Enabled = False
        lblHora.Text = DateTime.Now.ToLongTimeString()
    End Sub

    Private Sub btnInicio_Click(sender As Object, e As EventArgs) Handles btnInicio.Click
        If Module1.Activo = True Then
            MessageBox.Show("POR FAVOR, DESCONECTA LA CONEXIÓN HECHA DESDE CONFIGURACIÓN")
            SecondCustomControl1.BringToFront()
        Else
            SidePanel.Height = btnInicio.Height
            SidePanel.Top = btnInicio.Top
            FirstCustomControl1.BringToFront()
        End If
    End Sub

    Private Sub btnConfig_Click(sender As Object, e As EventArgs) Handles btnConfig.Click
        If My.Settings.Modo = "" Then

            MsgBox("PRIMERO SELECCIONE UN MODO Y HAGA CLICK EN SIGUIENTE", MsgBoxStyle.Information)

        ElseIf My.Settings.Modo = "Continuo" Then

            SidePanel.Height = btnConfig.Height
            SidePanel.Top = btnConfig.Top
            SecondCustomControl1.BringToFront()

        Else

            MsgBox("NO HA SELECCIONADO EL MODO CONTINUO PARA CONFIGURARLO", MsgBoxStyle.Information)

        End If
    End Sub

    Private Sub btnConfigBajoDemanda_Click(sender As Object, e As EventArgs) Handles btnConfigBajoDemanda.Click
        If My.Settings.Modo = "" Then

            MsgBox("PRIMERO SELECCIONE UN MODO Y HAGA CLICK EN SIGUIENTE", MsgBoxStyle.Information)

        ElseIf My.Settings.Modo = "Demanda" Then

            SidePanel.Height = btnConfigBajoDemanda.Height
            SidePanel.Top = btnConfigBajoDemanda.Top
            ThirdCustomControl1.BringToFront()

        Else

            MsgBox("NO HA SELECCIONADO EL MODO BAJO DEMANDA PARA CONFIGURARLO", MsgBoxStyle.Information)

        End If
    End Sub

    Private Sub btnConfigPuerto_Click(sender As Object, e As EventArgs) Handles btnConfigPuerto.Click
        If Module1.Activo = True Then
            MessageBox.Show("POR FAVOR, DESCONECTA LA CONEXIÓN HECHA DESDE CONFIGURACIÓN")
            'SecondCustomControl1.BringToFront()
        Else
            SidePanel.Height = btnConfigPuerto.Height
            SidePanel.Top = btnConfigPuerto.Top

            ConfigurationPort1.BringToFront()
        End If
    End Sub

    Private Sub btnAutomatizacion_Click(sender As Object, e As EventArgs) Handles btnAutomatizacion.Click
        If Module1.Activo = True Then
            MessageBox.Show("POR FAVOR, DESCONECTA LA CONEXIÓN HECHA DESDE CONFIGURACIÓN")
        Else
            SidePanel.Height = btnAutomatizacion.Height
            SidePanel.Top = btnAutomatizacion.Top
            AutomatizacionControl1.BringToFront()
        End If
    End Sub

    Private Sub FirstCustomControl1_Load(sender As Object, e As EventArgs) Handles FirstCustomControl1.Load

    End Sub

    Private Sub btnMini_Click(sender As Object, e As EventArgs) Handles btnMini.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Dim Texto As String
        Dim Estilo As MsgBoxStyle
        Dim Respuesta As MsgBoxResult
        Texto = “¿Está usted seguro?”
        Estilo = MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Question Or MsgBoxStyle.YesNo
        Respuesta = MsgBox(Texto, Estilo)
        If Respuesta = MsgBoxResult.Yes Then
            Me.Close()
            Loader.Close()
        End If
    End Sub

    Private Sub tmrHora_Tick(sender As Object, e As EventArgs) Handles tmrHora.Tick
        'tmrHora.Enabled = False
        lblHora.Text = DateTime.Now.ToLongTimeString()
    End Sub


End Class
