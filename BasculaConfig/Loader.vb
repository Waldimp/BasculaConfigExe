Imports System.IO

Public Class Loader

    Private Const CarpetaConfiguracion As String = "C:\config"

    ''' <summary>
    ''' Siembra los parámetros del puerto la primera vez que se abre el programa.
    '''
    ''' Antes esto escribía directo en C:\config sin asegurarse de que la carpeta
    ''' existiera y sin capturar errores. En una computadora nueva —donde esa carpeta
    ''' todavía no está— el arranque fallaba con un error de .NET que el usuario veía
    ''' antes que cualquier otra cosa del programa.
    ''' </summary>
    Private Sub Loader_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            Directory.CreateDirectory(CarpetaConfiguracion)

            If String.IsNullOrEmpty(My.Settings.BaudRate) Then
                My.Settings.BaudRate = "9600"
                GuardarParametro("BaudRate.txt", My.Settings.BaudRate)
            End If

            If String.IsNullOrEmpty(My.Settings.DataBits) Then
                My.Settings.DataBits = "8"
                GuardarParametro("DataBits.txt", My.Settings.DataBits)
            End If

            If String.IsNullOrEmpty(My.Settings.HandShake) Then
                My.Settings.HandShake = "0"
                GuardarParametro("HandShake.txt", My.Settings.HandShake)
            End If

            If String.IsNullOrEmpty(My.Settings.Parity) Then
                My.Settings.Parity = "0"
                GuardarParametro("Parity.txt", My.Settings.Parity)
            End If

            My.Settings.Save()

        Catch ex As Exception
            ' El arranque no debe caerse por no poder sembrar la configuración. Las
            ' pantallas de configuración vuelven a crear lo que falte cuando se abren.
        End Try

    End Sub

    Private Sub GuardarParametro(nombreArchivo As String, contenido As String)
        File.WriteAllText(Path.Combine(CarpetaConfiguracion, nombreArchivo),
                          contenido & vbCrLf, System.Text.Encoding.ASCII)
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
