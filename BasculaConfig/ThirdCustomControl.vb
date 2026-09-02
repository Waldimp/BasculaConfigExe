Imports System.IO
Imports System.IO.Ports

Public Class ThirdCustomControl
    Dim StrBufferOut As String
    Dim StrBufferIn As String

    Dim peso As String

    Dim obj As Object
    Dim Archivo As Object
    Dim ruta, rutaArch As String

    Private Sub btnDeterminar_Click(sender As Object, e As EventArgs) Handles btnDeterminar.Click

        listaPuertos.Items.Clear()
        cboPuertos.Items.Clear()

        ' Se listan todos los puertos que reporta Windows, sin descartar ninguno.
        '
        ' Antes se intentaba abrir cada puerto para "comprobar" si servía, y el que
        ' fallara se quitaba de la lista. Eso descartaba puertos perfectamente
        ' utilizables por tres motivos: un adaptador USB recién conectado suele
        ' rechazar la primera apertura mientras el driver se acomoda; si el cierre de
        ' un puerto anterior fallaba, el objeto quedaba abierto y entonces fallaban
        ' todos los siguientes en cascada; y abrir y cerrar mueve las líneas DTR y RTS,
        ' lo que en algunas básculas provoca un reinicio.
        '
        ' Windows no ofrece forma de saber si un puerto está libre sin abrirlo, así que
        ' se muestran todos y, si al conectar el puerto está ocupado, se explica ahí.
        For Each puerto As String In Module1.PuertosDelSistema()
            listaPuertos.Items.Add(puerto)
            cboPuertos.Items.Add(puerto)
        Next

        If cboPuertos.Items.Count > 0 Then

            ' Si ya había un puerto elegido y sigue presente, se respeta.
            Dim guardado As String = My.Settings.Demanda_PuertoCOM
            If Not String.IsNullOrEmpty(guardado) AndAlso cboPuertos.Items.Contains(guardado) Then
                cboPuertos.Text = guardado
            Else
                cboPuertos.Text = cboPuertos.Items(0).ToString()
            End If

            btnConectar.Enabled = True

            panelCaracteres.Visible = True
            PanelRuta.Visible = True

        Else
            cboPuertos.Text = ""
            MessageBox.Show("Windows no reporta ningún puerto serial en esta computadora." & vbCrLf & vbCrLf &
                            "Revisa que el cable o el adaptador USB estén conectados.",
                            "Sin puertos", MessageBoxButtons.OK, MessageBoxIcon.Information)
            btnConectar.Enabled = False

            panelCaracteres.Visible = False
            PanelRuta.Visible = False

            lblPuerto.Text = ""

        End If

    End Sub


    Private Sub btnConnectar_Click(sender As Object, e As EventArgs) Handles btnConectar.Click

        Try

            If btnConectar.Text = "Conectar" Then
                spPuertos.PortName = cboPuertos.Text
                btnConectar.Text = "Desconectar"

                Dim BaudRate As String
                BaudRate = My.Computer.FileSystem.ReadAllText("C:\config\BaudRate.txt")
                BaudRate = Replace(BaudRate, vbCrLf, "")

                Dim DataBits As String
                DataBits = My.Computer.FileSystem.ReadAllText("C:\config\DataBits.txt")
                DataBits = Replace(DataBits, vbCrLf, "")

                Dim Parity As String
                Parity = My.Computer.FileSystem.ReadAllText("C:\config\Parity.txt")
                Parity = Replace(Parity, vbCrLf, "")

                Dim Handshake As String
                Handshake = My.Computer.FileSystem.ReadAllText("C:\config\Handshake.txt")
                Handshake = Replace(Handshake, vbCrLf, "")

                spPuertos.BaudRate = BaudRate
                spPuertos.DataBits = DataBits
                spPuertos.Parity = Parity
                spPuertos.Handshake = Handshake

                Module1.Activo = True
                lblRemember.Visible = True
                Label4.Text = "Desconecta para contar"

                spPuertos.Open()

                tmrTimer.Interval = Convert.ToInt32(txtTimer.Text)

                tmrTimer.Enabled = True

            ElseIf btnConectar.Text = "Desconectar" Then

                tmrTimer.Enabled = False

                btnConectar.Text = "Conectar"

                Module1.Activo = False
                lblRemember.Visible = False


                Label4.Text = "Cuenta los caracteres"

                spPuertos.Close()


            End If
        Catch ex As Exception
            ' La conexión no prosperó: se deja el botón como estaba para que el usuario
            ' pueda reintentar, en lugar de quedar diciendo "Desconectar" sin estarlo.
            btnConectar.Text = "Conectar"
            Module1.Activo = False
            tmrTimer.Enabled = False
            MessageBox.Show(Module1.MensajeDePuerto(ex, cboPuertos.Text), "No se pudo conectar",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try


    End Sub

    Private Sub cboPuertos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPuertos.SelectedIndexChanged

        Try
            lblPuerto.Text = cboPuertos.Text
            Module1.Demanda_Nombre_Puerto = cboPuertos.Text
            My.Settings.Demanda_PuertoCOM = cboPuertos.Text
            My.Settings.Save()


            obj = CreateObject("Scripting.FileSystemObject")
            Archivo = obj.CreateTextFile("C:\config\Demanda_Puerto.txt")
            Archivo.WriteLine(cboPuertos.Text)
            Archivo.Close()
        Catch ex As Exception
            MessageBox.Show("Error con el puerto: " + ex.ToString())
        End Try


    End Sub

    Private Sub nudInicio_ValueChanged(sender As Object, e As EventArgs) Handles nudInicio.ValueChanged

        Try
            Module1.Demanda_Inicio = nudInicio.Value
            My.Settings.Demanda_StringInicio = nudInicio.Value
            My.Settings.Save()

            obj = CreateObject("Scripting.FileSystemObject")
            Archivo = obj.CreateTextFile("C:\config\Demanda_Inicio.txt")
            Archivo.WriteLine(nudInicio.Value)
            Archivo.Close()
        Catch ex As Exception
            MessageBox.Show("Error con el puerto: " + ex.ToString())
        End Try


    End Sub

    Private Sub nudOcupar_ValueChanged(sender As Object, e As EventArgs) Handles nudOcupar.ValueChanged

        Try
            Module1.Demanda_Recorrer = nudOcupar.Value
            My.Settings.Demanda_StringRecorrer = nudOcupar.Value
            My.Settings.Save()

            obj = CreateObject("Scripting.FileSystemObject")
            Archivo = obj.CreateTextFile("C:\config\Demanda_Ocupar.txt")
            Archivo.WriteLine(nudOcupar.Value)
            Archivo.Close()
        Catch ex As Exception
            MessageBox.Show("Error con el puerto: " + ex.ToString())
        End Try


    End Sub

    Private Sub txtRuta_TextChanged(sender As Object, e As EventArgs) Handles txtRuta.TextChanged

        Try
            Module1.Demanda_rutaUser = txtRuta.Text
            My.Settings.Demanda_Ruta = txtRuta.Text
            My.Settings.Save()

            obj = CreateObject("Scripting.FileSystemObject")
            Archivo = obj.CreateTextFile("C:\config\Demanda_Ruta.txt")
            Archivo.WriteLine(txtRuta.Text)
            Archivo.Close()
        Catch ex As Exception
            MessageBox.Show("Error con el puerto: " + ex.ToString())
        End Try


    End Sub

    Private Sub btnExaminar_Click(sender As Object, e As EventArgs) Handles btnExaminar.Click

        Try
            Folder.ShowDialog()
            txtRuta.Text = Folder.SelectedPath
        Catch ex As Exception
            MessageBox.Show("Error con el puerto: " + ex.ToString())
        End Try


    End Sub

    Private Sub ThirdCustomControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            CheckForIllegalCrossThreadCalls = False

            ruta = "C:\config"

            If Directory.Exists(ruta) Then
            Else
                MkDir(ruta)
            End If

            StrBufferIn = ""

            btnConectar.Enabled = True
            tmrTimer.Enabled = False

            If My.Settings.Demanda_PuertoCOM <> "" And My.Settings.Demanda_StringInicio <> "" And My.Settings.Demanda_StringRecorrer <> "" And My.Settings.Demanda_Ruta <> "" And My.Settings.Demanda_Caracter <> "" Then

                listaPuertos.Items.Add(My.Settings.Demanda_PuertoCOM)
                cboPuertos.Items.Add(My.Settings.Demanda_PuertoCOM)
                cboPuertos.Text = My.Settings.Demanda_PuertoCOM
                btnConectar.Enabled = True

                panelCaracteres.Visible = True
                PanelRuta.Visible = True
                nudInicio.Value = My.Settings.Demanda_StringInicio
                nudOcupar.Value = My.Settings.Demanda_StringRecorrer
                txtRuta.Text = My.Settings.Demanda_Ruta
                txtEnviar.Text = My.Settings.Demanda_Caracter



            End If

        Catch ex As Exception
            MessageBox.Show("Error con el puerto: " + ex.ToString())
        End Try



    End Sub

    Private Sub btnEnviar_Click(sender As Object, e As EventArgs) Handles btnEnviar.Click

        Try
            If spPuertos.IsOpen Then
                ''Do
                ''spPuertos.WriteLine(Demanda_caracter)
                ''Loop While (again() = False)

                spPuertos.WriteLine(Demanda_caracter)

            Else
                MsgBox("NO ESTAS CONECTADO", MsgBoxStyle.Exclamation)
            End If
        Catch ex As Exception
            MessageBox.Show("Error con el puerto: " + ex.ToString())
        End Try


    End Sub

    Private Sub txtEnviar_TextChanged(sender As Object, e As EventArgs) Handles txtEnviar.TextChanged

        Try
            cboHex.Checked = False

            Module1.Demanda_caracter = txtEnviar.Text
            My.Settings.Demanda_Caracter = txtEnviar.Text
            My.Settings.Save()

            obj = CreateObject("Scripting.FileSystemObject")
            Archivo = obj.CreateTextFile("C:\config\Demanda_Caracter.txt")
            Archivo.WriteLine(txtEnviar.Text)
            Archivo.Close()

            lblCaracteerxd.Text = txtEnviar.Text
        Catch ex As Exception
            MessageBox.Show("Error con el texto a enviar: " + ex.ToString())
        End Try


    End Sub

    Private Sub cboHex_CheckedChanged(sender As Object, e As EventArgs) Handles cboHex.CheckedChanged

        Try
            If cboHex.Checked = True Then

                lblCaracteerxd.Text = ""

                Try
                    Dim TEXTO As String = txtEnviar.Text
                    TEXTO = TEXTO.Replace("-", "")
                    Dim MIARRAY As New ArrayList(TEXTO.ToCharArray)
                    For I = 0 To MIARRAY.Count - 2 Step 2
                        Dim ENTERO As Integer = MIARRAY(I).ToString * 16
                        Dim COMPLEMENTO As String = MIARRAY(I + 1)
                        Select Case COMPLEMENTO.ToUpper
                            Case "A"
                                ENTERO += 10
                                lblCaracteerxd.Text += Convert.ToChar(ENTERO)
                            Case "B"
                                ENTERO += 11
                                lblCaracteerxd.Text += Convert.ToChar(ENTERO)
                            Case "C"
                                ENTERO += 12
                                lblCaracteerxd.Text += Convert.ToChar(ENTERO)
                            Case "D"
                                ENTERO += 13
                                lblCaracteerxd.Text += Convert.ToChar(ENTERO)
                            Case "E"
                                ENTERO += 14
                                lblCaracteerxd.Text += Convert.ToChar(ENTERO)
                            Case "F"
                                ENTERO += 15
                                lblCaracteerxd.Text += Convert.ToChar(ENTERO)
                            Case Else
                                ENTERO += CInt(COMPLEMENTO)
                                lblCaracteerxd.Text += Convert.ToChar(ENTERO)
                        End Select
                    Next
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

                Module1.Demanda_caracter = lblCaracteerxd.Text
                My.Settings.Demanda_Caracter = lblCaracteerxd.Text
                My.Settings.Save()

                obj = CreateObject("Scripting.FileSystemObject")
                Archivo = obj.CreateTextFile("C:\config\Demanda_Caracter.txt")
                Archivo.WriteLine(lblCaracteerxd.Text)
                Archivo.Close()

            End If
        Catch ex As Exception
            MessageBox.Show("Error con con la conversión: " + ex.ToString())
        End Try


    End Sub

    Private Sub tmrTimer_Tick(sender As Object, e As EventArgs) Handles tmrTimer.Tick
        If (spPuertos.IsOpen = True) Then
            spPuertos.WriteLine(lblCaracteerxd.Text)
        End If
    End Sub

    Private Sub spPuertos_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles spPuertos.DataReceived

        Try
            Dim buffer As String
            buffer = spPuertos.ReadExisting
            txtBufferIn.Text = buffer
        Catch ex As Exception
            MessageBox.Show("Error con el puerto: " + ex.ToString())
        End Try

    End Sub

    Private Sub txtTimer_TextChanged(sender As Object, e As EventArgs) Handles txtTimer.TextChanged
        Try
            If (Convert.ToInt32(txtTimer.Text) > 10) Then
                tmrTimer.Interval = Convert.ToInt32(txtTimer.Text)
            End If
        Catch ex As Exception
            Console.WriteLine(ex)
        End Try
    End Sub

    Function again()
        Dim Buffer As String

        Buffer = spPuertos.ReadExisting
        'MsgBox(Buffer)
        If Buffer <> "" And Buffer.Length > 5 Then
            Return True

        Else
            Return False
        End If

    End Function
End Class
