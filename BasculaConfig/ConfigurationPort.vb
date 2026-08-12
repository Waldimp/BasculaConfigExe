Public Class ConfigurationPort

    Dim obj As Object
    Dim Archivo As Object

    Private Sub ConfigurationPort_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        cboParity.Items.Add("None")
        cboParity.Items.Add("Odd")
        cboParity.Items.Add("Even")
        cboParity.Items.Add("Mark")
        cboParity.Items.Add("Space")

        cboHandshake.Items.Add("None")
        cboHandshake.Items.Add("XOnXOff")
        cboHandshake.Items.Add("RequestToSend")
        cboHandshake.Items.Add("RequestToSendXOnXOff")

        If My.Settings.Parity <> "" Then
            cboParity.SelectedIndex = My.Settings.Parity
        Else
            cboParity.SelectedIndex = 2
        End If

        If My.Settings.HandShake <> "" Then
            cboHandshake.SelectedIndex = My.Settings.HandShake
        Else
            cboHandshake.SelectedIndex = 1
        End If

        If My.Settings.BaudRate <> "" Then
            txtBoudRate.Text = My.Settings.BaudRate
        End If

        If My.Settings.DataBits <> "" Then
            txtDataBit.Text = My.Settings.DataBits
        End If

    End Sub

    Private Sub cboParity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboParity.SelectedIndexChanged

        My.Settings.Parity = cboParity.SelectedIndex
        obj = CreateObject("Scripting.FileSystemObject")
        Archivo = obj.CreateTextFile("C:\config\Parity.txt")
        Archivo.WriteLine(My.Settings.Parity)
        Archivo.Close()

    End Sub

    Private Sub panel1_Paint(sender As Object, e As PaintEventArgs) Handles panel1.Paint

    End Sub

    Private Sub cboHandshake_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboHandshake.SelectedIndexChanged
        My.Settings.HandShake = cboHandshake.SelectedIndex
        obj = CreateObject("Scripting.FileSystemObject")
        Archivo = obj.CreateTextFile("C:\config\HandShake.txt")
        Archivo.WriteLine(My.Settings.HandShake)
        Archivo.Close()
    End Sub

    Private Sub txtBoudRate_TextChanged(sender As Object, e As EventArgs) Handles txtBoudRate.TextChanged
        If txtBoudRate.Text.Length > 0 Then
            My.Settings.BaudRate = txtBoudRate.Text

            obj = CreateObject("Scripting.FileSystemObject")
            Archivo = obj.CreateTextFile("C:\config\BaudRate.txt")
            Archivo.WriteLine(My.Settings.BaudRate)
            Archivo.Close()
        End If
    End Sub

    Private Sub txtDataBit_TextChanged(sender As Object, e As EventArgs) Handles txtDataBit.TextChanged
        If txtDataBit.Text.Length > 0 Then
            My.Settings.DataBits = txtDataBit.Text
            obj = CreateObject("Scripting.FileSystemObject")
            Archivo = obj.CreateTextFile("C:\config\DataBits.txt")
            Archivo.WriteLine(My.Settings.DataBits)
            Archivo.Close()
        End If
    End Sub
End Class
