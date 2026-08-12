Imports System.IO

Public Class FirstCustomControl
    Dim obj As Object
    Dim Archivo As Object

    Dim ruta, rutaArch As String
    Private Sub FirstCustomControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        btnSiguiente.Enabled = False
        'btnFinalizar.Enabled = False

        If My.Settings.Modo = "Continuo" Then
            rdbContinuo.Checked = True
            rdbDemanda.Checked = False
        ElseIf My.Settings.Modo = "Demanda" Then
            rdbContinuo.Checked = False
            rdbDemanda.Checked = True
        End If
    End Sub

    Private Sub rdbContinuo_CheckedChanged(sender As Object, e As EventArgs) Handles rdbContinuo.CheckedChanged
        btnSiguiente.Enabled = True
    End Sub

    Private Sub rdbDemanda_CheckedChanged(sender As Object, e As EventArgs) Handles rdbDemanda.CheckedChanged
        btnSiguiente.Enabled = True
    End Sub

    Private Sub tmrTimer_Tick(sender As Object, e As EventArgs) Handles tmrTimer.Tick
        tmrTimer.Enabled = False
        If My.Settings.Demanda_PuertoCOM <> "" And My.Settings.Demanda_StringInicio <> "" And My.Settings.Demanda_StringRecorrer <> "" And My.Settings.Demanda_Ruta <> "" And My.Settings.Demanda_Caracter <> "" Then
            btnFinalizar.Enabled = True

        ElseIf My.Settings.Continuo_PuertoCOM <> "" And My.Settings.Continuo_StringInicio <> "" And My.Settings.Continuo_StringRecorrer <> "" And My.Settings.Continuo_Ruta <> "" Then
            btnFinalizar.Enabled = True
        End If



    End Sub

    Private Sub btnFinalizar_Click(sender As Object, e As EventArgs) Handles btnFinalizar.Click
        Dim Texto As String
        Dim Estilo As MsgBoxStyle
        Dim Respuesta As MsgBoxResult
        Texto = “¿Desea guardar su configuración?”
        Estilo = MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Question Or MsgBoxStyle.YesNo
        Respuesta = MsgBox(Texto, Estilo)
        If Respuesta = MsgBoxResult.Yes Then
            Form1.Close()
            Loader.Close()
        End If
    End Sub

    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click

        Try
            ruta = "C:\config"

            If Directory.Exists(ruta) Then
            Else
                MkDir(ruta)
            End If


            If rdbContinuo.Checked = True Then

                MessageBox.Show("Configura en modo continuo desde la configuración.")
                Module1.Modo = "Continuo"

                My.Settings.Modo = "Continuo"
                My.Settings.Save()


                obj = CreateObject("Scripting.FileSystemObject")

                Archivo = obj.CreateTextFile("C:\config\modo.txt")

                Archivo.WriteLine("Continuo")
                Archivo.Close()


            Else
                MessageBox.Show("Configura en modo bajo demanda desde la configuración.")
                Module1.Modo = "Demanda"

                obj = CreateObject("Scripting.FileSystemObject")

                Archivo = obj.CreateTextFile("C:\config\modo.txt")

                Archivo.WriteLine("Demanda")
                Archivo.Close()

                My.Settings.Modo = "Demanda"
                My.Settings.Save()

            End If
        Catch ex As Exception
            MessageBox.Show("Error, debes seleccionar bien. " + ex.ToString())
        End Try


    End Sub
End Class
