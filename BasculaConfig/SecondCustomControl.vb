Imports System.IO
Public Class SecondCustomControl
    Dim StrBufferOut As String
    Dim StrBufferIn As String

    Dim peso As String

    Dim obj As Object
    Dim Archivo As Object
    Dim ruta, rutaArch As String


    Private Sub SecondCustomControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            ruta = "C:\config"

            If Directory.Exists(ruta) Then
            Else
                MkDir(ruta)
            End If

            StrBufferIn = ""

            btnConectar.Enabled = True
            tmrTimer.Enabled = False

            If My.Settings.Continuo_PuertoCOM <> "" And My.Settings.Continuo_StringInicio <> "" And My.Settings.Continuo_StringRecorrer <> "" And My.Settings.Continuo_Ruta <> "" Then

                listaPuertos.Items.Add(My.Settings.Continuo_PuertoCOM)
                cboPuertos.Items.Add(My.Settings.Continuo_PuertoCOM)
                cboPuertos.Text = My.Settings.Continuo_PuertoCOM
                btnConectar.Enabled = True

                panelCaracteres.Visible = True
                PanelRuta.Visible = True
                nudInicio.Value = My.Settings.Continuo_StringInicio
                nudOcupar.Value = My.Settings.Continuo_StringRecorrer
                txtRuta.Text = My.Settings.Continuo_Ruta

            End If
        Catch ex As Exception
            MessageBox.Show("Error con el puerto: " + ex.ToString())
        End Try


    End Sub



    Private Sub tmrTimer_Tick(sender As Object, e As EventArgs) Handles tmrTimer.Tick

        Try
            StrBufferIn = spPuertos.ReadExisting

            If StrBufferIn <> "" Then

                peso = StrBufferIn
                txtBufferIn.Text = peso


                StrBufferIn = ""
                spPuertos.DiscardInBuffer()

            End If
        Catch ex As Exception
            tmrTimer.Enabled = False
            MessageBox.Show("Error con el puerto, desconecta y revisa la conexión : " + ex.ToString())
        End Try

    End Sub

    Private Sub btnDeterminar_Click(sender As Object, e As EventArgs) Handles btnDeterminar.Click


        listaPuertos.Items.Clear()
        cboPuertos.Items.Clear()


        For Each PuertoDisponible As String In My.Computer.Ports.SerialPortNames

            listaPuertos.Items.Add(PuertoDisponible)

            cboPuertos.Items.Add(PuertoDisponible)

            spPuertosPrueba.PortName = PuertoDisponible

            Try
                spPuertosPrueba.Open()
            Catch ex As Exception


                cboPuertos.Items.Remove(PuertoDisponible)
            End Try

            If spPuertosPrueba.IsOpen = True Then
                spPuertosPrueba.Close()
            End If

        Next


        If cboPuertos.Items.Count > 0 Then
            cboPuertos.Text = cboPuertos.Items(0)

            MessageBox.Show("SELECCIONE EL PUERTO A UTILIZAR")
            btnConectar.Enabled = True

            panelCaracteres.Visible = True
            PanelRuta.Visible = True

        Else
            cboPuertos.Text = ""
            MessageBox.Show("NO SE ENCONTRARON PUERTOS APTOS DISPONIBLES")
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

                tmrTimer.Enabled = True

                Module1.Activo = True
                lblRemember.Visible = True
                Label4.Text = "Desconecta para contar"


                spPuertos.Open()

            ElseIf btnConectar.Text = "Desconectar" Then

                btnConectar.Text = "Conectar"

                Module1.Activo = False
                lblRemember.Visible = False
                tmrTimer.Enabled = False

                Label4.Text = "Cuenta los caracteres"

                spPuertos.Close()

            End If
        Catch ex As Exception
            MessageBox.Show("Error al encender el puerto: " + ex.ToString())
        End Try

    End Sub

    Private Sub cboPuertos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPuertos.SelectedIndexChanged

        Try
            lblPuerto.Text = cboPuertos.Text
            Module1.Continuo_Nombre_Puerto = cboPuertos.Text
            My.Settings.Continuo_PuertoCOM = cboPuertos.Text
            My.Settings.Save()


            obj = CreateObject("Scripting.FileSystemObject")
            Archivo = obj.CreateTextFile("C:\config\Continuo_Puerto.txt")
            Archivo.WriteLine(cboPuertos.Text)
            Archivo.Close()
        Catch ex As Exception
            MessageBox.Show("Error con el puerto: " + ex.ToString())
        End Try

    End Sub

    Private Sub nudInicio_ValueChanged(sender As Object, e As EventArgs) Handles nudInicio.ValueChanged

        Try
            Module1.Continuo_Inicio = nudInicio.Value
            My.Settings.Continuo_StringInicio = nudInicio.Value
            My.Settings.Save()

            obj = CreateObject("Scripting.FileSystemObject")
            Archivo = obj.CreateTextFile("C:\config\Continuo_Inicio.txt")
            Archivo.WriteLine(nudInicio.Value)
            Archivo.Close()
        Catch ex As Exception
            MessageBox.Show("Error con el puerto: " + ex.ToString())
        End Try

    End Sub

    Private Sub nudOcupar_ValueChanged(sender As Object, e As EventArgs) Handles nudOcupar.ValueChanged

        Try
            Module1.Continuo_Recorrer = nudOcupar.Value
            My.Settings.Continuo_StringRecorrer = nudOcupar.Value
            My.Settings.Save()

            obj = CreateObject("Scripting.FileSystemObject")
            Archivo = obj.CreateTextFile("C:\config\Continuo_Ocupar.txt")
            Archivo.WriteLine(nudOcupar.Value)
            Archivo.Close()
        Catch ex As Exception
            MessageBox.Show("Error con el puerto: " + ex.ToString())
        End Try


    End Sub

    Private Sub txtRuta_TextChanged(sender As Object, e As EventArgs) Handles txtRuta.TextChanged

        Try
            Module1.Continuo_rutaUser = txtRuta.Text
            My.Settings.Continuo_Ruta = txtRuta.Text
            My.Settings.Save()

            obj = CreateObject("Scripting.FileSystemObject")
            Archivo = obj.CreateTextFile("C:\config\Continuo_Ruta.txt")
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
End Class
