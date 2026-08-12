Imports System.IO

Public Class Loader
    Dim obj As Object
    Dim Archivo As Object
    Private Sub Loader_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'MessageBox.Show("Baud :" + My.Settings.BaudRate)

        If My.Settings.BaudRate.Equals("") Then
            My.Settings.BaudRate = "9600"

            obj = CreateObject("Scripting.FileSystemObject")
            Archivo = obj.CreateTextFile("C:\config\BaudRate.txt")
            Archivo.WriteLine(My.Settings.BaudRate)
            Archivo.Close()
        End If

        If My.Settings.DataBits.Equals("") Then
            My.Settings.DataBits = "8"
            obj = CreateObject("Scripting.FileSystemObject")
            Archivo = obj.CreateTextFile("C:\config\DataBits.txt")
            Archivo.WriteLine(My.Settings.DataBits)
            Archivo.Close()
        End If

        If My.Settings.HandShake.Equals("") Then
            My.Settings.HandShake = 0
            obj = CreateObject("Scripting.FileSystemObject")
            Archivo = obj.CreateTextFile("C:\config\HandShake.txt")
            Archivo.WriteLine(My.Settings.HandShake)
            Archivo.Close()
        End If

        If My.Settings.Parity.Equals("") Then
            My.Settings.Parity = 0
            obj = CreateObject("Scripting.FileSystemObject")
            Archivo = obj.CreateTextFile("C:\config\Parity.txt")
            Archivo.WriteLine(My.Settings.Parity)
            Archivo.Close()
        End If


    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            Guna2ProgressBar1.Value += 1


            If (Guna2ProgressBar1.Value = 100) Then

                Timer1.Stop()



                Form1.Show()



                Me.Hide()

            End If
        Catch ex As Exception

        End Try
    End Sub
End Class