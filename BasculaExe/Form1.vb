Imports System.IO.Ports
Imports System.IO
Public Class Form1

    Dim obj As Object
    Dim Archivo As Object

    Dim ruta, StrBufferIn, peso As String

    Dim Continuo_Inicio As String
    Dim Continuo_Ocupar As String
    Dim Continuo_Ruta As String

    Private Sub tmrTimer_Tick(sender As Object, e As EventArgs) Handles tmrTimer.Tick
        StrBufferIn = spPuertos.ReadExisting

        If StrBufferIn <> "" And StrBufferIn.Length > Continuo_Inicio Then

            peso = Mid(StrBufferIn, Continuo_Inicio, Continuo_Ocupar)

            If Directory.Exists(Continuo_Ruta) Then
            Else
                MkDir(Continuo_Ruta)
            End If

            ruta = Continuo_Ruta + "\pesas.txt"

            obj = CreateObject("Scripting.FileSystemObject")
            Archivo = obj.CreateTextFile(ruta)
            Archivo.WriteLine(peso)
            Archivo.Close()

            StrBufferIn = ""
            spPuertos.DiscardInBuffer()

            spPuertos.Close()

            Me.Close()

        End If
    End Sub

    Private Sub spPuertos_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles spPuertos.DataReceived

        If Modo = "Demanda" Then




            Dim Demanda_Inicio As String
            Demanda_Inicio = My.Computer.FileSystem.ReadAllText("C:\config\Demanda_Inicio.txt")
            Demanda_Inicio = Replace(Demanda_Inicio, vbCrLf, "")

            Label4.Text = Demanda_Inicio

            Dim Demanda_Ocupar As String
            Demanda_Ocupar = My.Computer.FileSystem.ReadAllText("C:\config\Demanda_Ocupar.txt")
            Demanda_Ocupar = Replace(Demanda_Ocupar, vbCrLf, "")

            Label5.Text = Demanda_Ocupar

            Dim Demanda_Ruta As String
            Demanda_Ruta = My.Computer.FileSystem.ReadAllText("C:\config\Demanda_Ruta.txt")
            Demanda_Ruta = Replace(Demanda_Ruta, vbCrLf, "")

            ruta = ruta + "\pesas.txt"

            Dim buffer, peso As String
            buffer = spPuertos.ReadExisting

            If buffer <> "" And buffer.Length > Demanda_Inicio Then

                peso = Mid(buffer, Demanda_Inicio, Demanda_Ocupar)

                Label6.Text = peso

                obj = CreateObject("Scripting.FileSystemObject")
                Archivo = obj.CreateTextFile(ruta)
                Archivo.WriteLine(peso)
                Archivo.Close()

                spPuertos.Close()

                Me.Close()

            End If




        End If
    End Sub

    Dim Modo As String

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Hide()

        If (My.Settings.abrir <> 1) Then
            Dim rutaActual As String = My.Computer.FileSystem.CurrentDirectory
            Process.Start("explorer.exe", rutaActual)

            Try
                Dim selecciona As SaveFileDialog = New SaveFileDialog()

                selecciona.Title = "Selecciona la ubicación de guardado"
                selecciona.Filter = "Archivo exe (*.exe)|*.exe"
                selecciona.FileName = "BasculaExe"

                If (selecciona.ShowDialog() = DialogResult.OK) Then

                    Dim FileToCopy = rutaActual + "\BasculaExe.exe"
                    Label2.Text = selecciona.FileName

                    System.IO.File.Copy(FileToCopy, selecciona.FileName)

                End If



            Catch ex As Exception

            End Try

            Label1.Text = rutaActual

            My.Settings.abrir = 1
            My.Settings.Save()
        End If

        CheckForIllegalCrossThreadCalls = False


        Modo = My.Computer.FileSystem.ReadAllText("C:\config\modo.txt")
        Modo = Replace(Modo, vbCrLf, "")

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

        If Modo = "Demanda" Then

            Dim Demanda_Puerto As String
            Demanda_Puerto = My.Computer.FileSystem.ReadAllText("C:\config\Demanda_Puerto.txt")
            Demanda_Puerto = Replace(Demanda_Puerto, vbCrLf, "")

            Label1.Text = Demanda_Puerto

            spPuertos.PortName = Demanda_Puerto

            If (spPuertos.IsOpen) Then
                spPuertos.Close()
            End If

            Dim Demanda_Caracter As String
            Demanda_Caracter = My.Computer.FileSystem.ReadAllText("C:\config\Demanda_Caracter.txt")
            Demanda_Caracter = Replace(Demanda_Caracter, vbCrLf, "")

            Label2.Text = Demanda_Caracter

            Dim Demanda_Ruta As String
            Demanda_Ruta = My.Computer.FileSystem.ReadAllText("C:\config\Demanda_Ruta.txt")
            Demanda_Ruta = Replace(Demanda_Ruta, vbCrLf, "")

            Label3.Text = Demanda_Ruta

            ruta = Demanda_Ruta + "\BasculaNET"

            If Directory.Exists(ruta) Then
            Else
                MkDir(ruta)
            End If

            spPuertos.PortName = Demanda_Puerto
            spPuertos.Open()

            ''Do
            spPuertos.WriteLine(Demanda_Caracter)
            ''Loop While (again() = False)


        ElseIf Modo = "Continuo" Then

            Dim Continuo_Puerto As String
            Continuo_Puerto = My.Computer.FileSystem.ReadAllText("C:\config\Continuo_Puerto.txt")
            Continuo_Puerto = Replace(Continuo_Puerto, vbCrLf, "")

            spPuertos.PortName = Continuo_Puerto

            If (spPuertos.IsOpen) Then
                spPuertos.Close()
            End If


            Continuo_Inicio = My.Computer.FileSystem.ReadAllText("C:\config\Continuo_Inicio.txt")
            Continuo_Inicio = Replace(Continuo_Inicio, vbCrLf, "")


            Continuo_Ocupar = My.Computer.FileSystem.ReadAllText("C:\config\Continuo_Ocupar.txt")
            Continuo_Ocupar = Replace(Continuo_Ocupar, vbCrLf, "")


            Continuo_Ruta = My.Computer.FileSystem.ReadAllText("C:\config\Continuo_Ruta.txt")
            Continuo_Ruta = Replace(Continuo_Ruta, vbCrLf, "")
            Continuo_Ruta = Continuo_Ruta + "\BasculaNET"

            spPuertos.PortName = Continuo_Puerto

            spPuertos.Open()

            If spPuertos.IsOpen Then
                tmrTimer.Enabled = True
            End If




        End If
    End Sub

    Function again()
        Dim Buffer As String

        Buffer = spPuertos.ReadExisting
        ''MsgBox(Buffer)
        If Buffer <> "" And Buffer.Length > 5 Then
            Return True

        Else
            Return False
        End If

    End Function

End Class
