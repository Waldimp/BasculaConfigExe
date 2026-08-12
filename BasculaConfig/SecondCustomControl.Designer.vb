<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SecondCustomControl
    Inherits System.Windows.Forms.UserControl

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.spPuertosPrueba = New System.IO.Ports.SerialPort(Me.components)
        Me.spPuertos = New System.IO.Ports.SerialPort(Me.components)
        Me.tmrTimer = New System.Windows.Forms.Timer(Me.components)
        Me.Folder = New System.Windows.Forms.FolderBrowserDialog()
        Me.lblRemember = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.PanelRuta = New System.Windows.Forms.Panel()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtRuta = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.btnExaminar = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.panelCaracteres = New System.Windows.Forms.Panel()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.nudOcupar = New System.Windows.Forms.NumericUpDown()
        Me.nudInicio = New System.Windows.Forms.NumericUpDown()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtBufferIn = New System.Windows.Forms.TextBox()
        Me.btnConectar = New System.Windows.Forms.Button()
        Me.label5 = New System.Windows.Forms.Label()
        Me.label6 = New System.Windows.Forms.Label()
        Me.label8 = New System.Windows.Forms.Label()
        Me.panel1 = New System.Windows.Forms.Panel()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.listaPuertos = New System.Windows.Forms.ListBox()
        Me.btnDeterminar = New System.Windows.Forms.Button()
        Me.label3 = New System.Windows.Forms.Label()
        Me.cboPuertos = New System.Windows.Forms.ComboBox()
        Me.label2 = New System.Windows.Forms.Label()
        Me.lblPuerto = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.PanelRuta.SuspendLayout()
        Me.panelCaracteres.SuspendLayout()
        CType(Me.nudOcupar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudInicio, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'spPuertos
        '
        Me.spPuertos.DataBits = 7
        Me.spPuertos.Parity = System.IO.Ports.Parity.Even
        '
        'tmrTimer
        '
        Me.tmrTimer.Interval = 500
        '
        'lblRemember
        '
        Me.lblRemember.AutoSize = True
        Me.lblRemember.Font = New System.Drawing.Font("Century Gothic", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemember.ForeColor = System.Drawing.Color.Red
        Me.lblRemember.Location = New System.Drawing.Point(178, 384)
        Me.lblRemember.Name = "lblRemember"
        Me.lblRemember.Size = New System.Drawing.Size(457, 18)
        Me.lblRemember.TabIndex = 18
        Me.lblRemember.Text = "*Recuerda desconectar la conexión antes de volver al inicio"
        Me.lblRemember.Visible = False
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Century Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(314, 20)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(169, 25)
        Me.Label13.TabIndex = 17
        Me.Label13.Text = "Modo Continuo"
        '
        'PanelRuta
        '
        Me.PanelRuta.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.PanelRuta.Controls.Add(Me.Label24)
        Me.PanelRuta.Controls.Add(Me.Label16)
        Me.PanelRuta.Controls.Add(Me.txtRuta)
        Me.PanelRuta.Controls.Add(Me.Label15)
        Me.PanelRuta.Controls.Add(Me.btnExaminar)
        Me.PanelRuta.Controls.Add(Me.Label11)
        Me.PanelRuta.Controls.Add(Me.Label12)
        Me.PanelRuta.Controls.Add(Me.Label14)
        Me.PanelRuta.Location = New System.Drawing.Point(579, 69)
        Me.PanelRuta.Name = "PanelRuta"
        Me.PanelRuta.Size = New System.Drawing.Size(212, 303)
        Me.PanelRuta.TabIndex = 16
        Me.PanelRuta.Visible = False
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Century Gothic", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.Red
        Me.Label24.Location = New System.Drawing.Point(113, 111)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(95, 13)
        Me.Label24.TabIndex = 15
        Me.Label24.Text = "*Campo obligatorio"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(5, 276)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(0, 17)
        Me.Label16.TabIndex = 12
        '
        'txtRuta
        '
        Me.txtRuta.Font = New System.Drawing.Font("Century Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRuta.Location = New System.Drawing.Point(12, 235)
        Me.txtRuta.Name = "txtRuta"
        Me.txtRuta.ReadOnly = True
        Me.txtRuta.Size = New System.Drawing.Size(180, 23)
        Me.txtRuta.TabIndex = 8
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(9, 158)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(163, 17)
        Me.Label15.TabIndex = 7
        Me.Label15.Text = "O deje la predeterminada"
        '
        'btnExaminar
        '
        Me.btnExaminar.BackColor = System.Drawing.Color.FromArgb(CType(CType(182, Byte), Integer), CType(CType(172, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnExaminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExaminar.Font = New System.Drawing.Font("Century Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExaminar.ForeColor = System.Drawing.Color.White
        Me.btnExaminar.Location = New System.Drawing.Point(41, 194)
        Me.btnExaminar.Name = "btnExaminar"
        Me.btnExaminar.Size = New System.Drawing.Size(107, 26)
        Me.btnExaminar.TabIndex = 1
        Me.btnExaminar.Text = "Examinar"
        Me.btnExaminar.UseVisualStyleBackColor = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(11, 141)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(64, 17)
        Me.Label11.TabIndex = 1
        Me.Label11.Text = "los pesos."
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(9, 124)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(193, 17)
        Me.Label12.TabIndex = 1
        Me.Label12.Text = "Determine la ruta para guardar"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Century Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(7, 81)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(166, 25)
        Me.Label14.TabIndex = 1
        Me.Label14.Text = "Ruta a guardar"
        '
        'panelCaracteres
        '
        Me.panelCaracteres.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.panelCaracteres.Controls.Add(Me.Label23)
        Me.panelCaracteres.Controls.Add(Me.nudOcupar)
        Me.panelCaracteres.Controls.Add(Me.nudInicio)
        Me.panelCaracteres.Controls.Add(Me.Label10)
        Me.panelCaracteres.Controls.Add(Me.Label9)
        Me.panelCaracteres.Controls.Add(Me.Label7)
        Me.panelCaracteres.Controls.Add(Me.Label4)
        Me.panelCaracteres.Controls.Add(Me.txtBufferIn)
        Me.panelCaracteres.Controls.Add(Me.btnConectar)
        Me.panelCaracteres.Controls.Add(Me.label5)
        Me.panelCaracteres.Controls.Add(Me.label6)
        Me.panelCaracteres.Controls.Add(Me.label8)
        Me.panelCaracteres.Location = New System.Drawing.Point(251, 69)
        Me.panelCaracteres.Name = "panelCaracteres"
        Me.panelCaracteres.Size = New System.Drawing.Size(300, 303)
        Me.panelCaracteres.TabIndex = 14
        Me.panelCaracteres.Visible = False
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Century Gothic", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.Red
        Me.Label23.Location = New System.Drawing.Point(202, 40)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(95, 13)
        Me.Label23.TabIndex = 10
        Me.Label23.Text = "*Campo obligatorio"
        '
        'nudOcupar
        '
        Me.nudOcupar.Font = New System.Drawing.Font("Century Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nudOcupar.Location = New System.Drawing.Point(186, 232)
        Me.nudOcupar.Name = "nudOcupar"
        Me.nudOcupar.Size = New System.Drawing.Size(46, 26)
        Me.nudOcupar.TabIndex = 8
        '
        'nudInicio
        '
        Me.nudInicio.Font = New System.Drawing.Font("Century Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nudInicio.Location = New System.Drawing.Point(186, 187)
        Me.nudInicio.Name = "nudInicio"
        Me.nudInicio.Size = New System.Drawing.Size(46, 26)
        Me.nudInicio.TabIndex = 7
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(29, 241)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(127, 17)
        Me.Label10.TabIndex = 6
        Me.Label10.Text = "a partir del de inicio"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(29, 224)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(132, 17)
        Me.Label9.TabIndex = 5
        Me.Label9.Text = "Caracteres a ocupar"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(29, 192)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(115, 17)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "Caracter a iniciar:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(126, 113)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(11, 17)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "."
        '
        'txtBufferIn
        '
        Me.txtBufferIn.Font = New System.Drawing.Font("Century Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBufferIn.Location = New System.Drawing.Point(32, 151)
        Me.txtBufferIn.Name = "txtBufferIn"
        Me.txtBufferIn.Size = New System.Drawing.Size(227, 23)
        Me.txtBufferIn.TabIndex = 2
        '
        'btnConectar
        '
        Me.btnConectar.BackColor = System.Drawing.Color.FromArgb(CType(CType(182, Byte), Integer), CType(CType(172, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnConectar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnConectar.Font = New System.Drawing.Font("Century Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnConectar.ForeColor = System.Drawing.Color.White
        Me.btnConectar.Location = New System.Drawing.Point(13, 108)
        Me.btnConectar.Name = "btnConectar"
        Me.btnConectar.Size = New System.Drawing.Size(107, 26)
        Me.btnConectar.TabIndex = 1
        Me.btnConectar.Text = "Conectar"
        Me.btnConectar.UseVisualStyleBackColor = False
        '
        'label5
        '
        Me.label5.AutoSize = True
        Me.label5.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label5.Location = New System.Drawing.Point(10, 75)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(249, 17)
        Me.label5.TabIndex = 1
        Me.label5.Text = "y elige cuantos caracteres quiere recibir"
        '
        'label6
        '
        Me.label6.AutoSize = True
        Me.label6.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label6.Location = New System.Drawing.Point(10, 58)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(262, 17)
        Me.label6.TabIndex = 1
        Me.label6.Text = "Conecta con el puerto COM seleccionado"
        '
        'label8
        '
        Me.label8.AutoSize = True
        Me.label8.Font = New System.Drawing.Font("Century Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label8.Location = New System.Drawing.Point(8, 15)
        Me.label8.Name = "label8"
        Me.label8.Size = New System.Drawing.Size(124, 25)
        Me.label8.TabIndex = 1
        Me.label8.Text = "Caracteres"
        '
        'panel1
        '
        Me.panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.panel1.Controls.Add(Me.Label22)
        Me.panel1.Controls.Add(Me.Label21)
        Me.panel1.Controls.Add(Me.listaPuertos)
        Me.panel1.Controls.Add(Me.btnDeterminar)
        Me.panel1.Controls.Add(Me.label3)
        Me.panel1.Controls.Add(Me.cboPuertos)
        Me.panel1.Controls.Add(Me.label2)
        Me.panel1.Controls.Add(Me.lblPuerto)
        Me.panel1.Controls.Add(Me.label1)
        Me.panel1.Location = New System.Drawing.Point(26, 69)
        Me.panel1.Name = "panel1"
        Me.panel1.Size = New System.Drawing.Size(197, 303)
        Me.panel1.TabIndex = 15
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Century Gothic", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.Red
        Me.Label22.Location = New System.Drawing.Point(99, 40)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(95, 13)
        Me.Label22.TabIndex = 9
        Me.Label22.Text = "*Campo obligatorio"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(15, 232)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(121, 17)
        Me.Label21.TabIndex = 8
        Me.Label21.Text = "Puertos disponibles"
        '
        'listaPuertos
        '
        Me.listaPuertos.Enabled = False
        Me.listaPuertos.FormattingEnabled = True
        Me.listaPuertos.Location = New System.Drawing.Point(18, 151)
        Me.listaPuertos.Name = "listaPuertos"
        Me.listaPuertos.Size = New System.Drawing.Size(153, 69)
        Me.listaPuertos.TabIndex = 7
        '
        'btnDeterminar
        '
        Me.btnDeterminar.BackColor = System.Drawing.Color.FromArgb(CType(CType(182, Byte), Integer), CType(CType(172, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDeterminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDeterminar.Font = New System.Drawing.Font("Century Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDeterminar.ForeColor = System.Drawing.Color.White
        Me.btnDeterminar.Location = New System.Drawing.Point(32, 108)
        Me.btnDeterminar.Name = "btnDeterminar"
        Me.btnDeterminar.Size = New System.Drawing.Size(107, 26)
        Me.btnDeterminar.TabIndex = 1
        Me.btnDeterminar.Text = "Determinar"
        Me.btnDeterminar.UseVisualStyleBackColor = False
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label3.Location = New System.Drawing.Point(12, 75)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(43, 17)
        Me.label3.TabIndex = 1
        Me.label3.Text = "a usar"
        '
        'cboPuertos
        '
        Me.cboPuertos.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPuertos.FormattingEnabled = True
        Me.cboPuertos.Location = New System.Drawing.Point(15, 261)
        Me.cboPuertos.Name = "cboPuertos"
        Me.cboPuertos.Size = New System.Drawing.Size(153, 32)
        Me.cboPuertos.TabIndex = 6
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label2.Location = New System.Drawing.Point(10, 58)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(161, 17)
        Me.label2.TabIndex = 1
        Me.label2.Text = "Determine el puerto COM"
        '
        'lblPuerto
        '
        Me.lblPuerto.AutoSize = True
        Me.lblPuerto.Font = New System.Drawing.Font("Century Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPuerto.ForeColor = System.Drawing.Color.Blue
        Me.lblPuerto.Location = New System.Drawing.Point(86, 308)
        Me.lblPuerto.Name = "lblPuerto"
        Me.lblPuerto.Size = New System.Drawing.Size(0, 25)
        Me.lblPuerto.TabIndex = 1
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Font = New System.Drawing.Font("Century Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.Location = New System.Drawing.Point(8, 15)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(110, 25)
        Me.label1.TabIndex = 1
        Me.label1.Text = "Conexión"
        '
        'SecondCustomControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lblRemember)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.PanelRuta)
        Me.Controls.Add(Me.panelCaracteres)
        Me.Controls.Add(Me.panel1)
        Me.Name = "SecondCustomControl"
        Me.Size = New System.Drawing.Size(817, 423)
        Me.PanelRuta.ResumeLayout(False)
        Me.PanelRuta.PerformLayout()
        Me.panelCaracteres.ResumeLayout(False)
        Me.panelCaracteres.PerformLayout()
        CType(Me.nudOcupar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudInicio, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panel1.ResumeLayout(False)
        Me.panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents spPuertosPrueba As IO.Ports.SerialPort
    Friend WithEvents tmrTimer As Timer
    Friend WithEvents Folder As FolderBrowserDialog
    Private WithEvents lblRemember As Label
    Private WithEvents Label13 As Label
    Private WithEvents PanelRuta As Panel
    Private WithEvents Label24 As Label
    Private WithEvents Label16 As Label
    Friend WithEvents txtRuta As TextBox
    Private WithEvents Label15 As Label
    Private WithEvents btnExaminar As Button
    Private WithEvents Label11 As Label
    Private WithEvents Label12 As Label
    Private WithEvents Label14 As Label
    Private WithEvents panelCaracteres As Panel
    Private WithEvents Label23 As Label
    Friend WithEvents nudOcupar As NumericUpDown
    Friend WithEvents nudInicio As NumericUpDown
    Private WithEvents Label10 As Label
    Private WithEvents Label9 As Label
    Private WithEvents Label7 As Label
    Private WithEvents Label4 As Label
    Friend WithEvents txtBufferIn As TextBox
    Private WithEvents btnConectar As Button
    Private WithEvents label5 As Label
    Private WithEvents label6 As Label
    Private WithEvents label8 As Label
    Private WithEvents panel1 As Panel
    Private WithEvents Label22 As Label
    Private WithEvents Label21 As Label
    Friend WithEvents listaPuertos As ListBox
    Private WithEvents btnDeterminar As Button
    Private WithEvents label3 As Label
    Friend WithEvents cboPuertos As ComboBox
    Private WithEvents label2 As Label
    Private WithEvents lblPuerto As Label
    Private WithEvents label1 As Label
    Private WithEvents spPuertos As IO.Ports.SerialPort
End Class
