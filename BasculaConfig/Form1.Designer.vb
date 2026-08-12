<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.btnAutomatizacion = New System.Windows.Forms.Button()
        Me.btnConfigPuerto = New System.Windows.Forms.Button()
        Me.btnConfigBajoDemanda = New System.Windows.Forms.Button()
        Me.SidePanel = New System.Windows.Forms.Panel()
        Me.btnConfig = New System.Windows.Forms.Button()
        Me.btnInicio = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnMini = New System.Windows.Forms.Button()
        Me.lblHora = New System.Windows.Forms.Label()
        Me.lblFecha = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.tmrTimer = New System.Windows.Forms.Timer(Me.components)
        Me.spPuertos = New System.IO.Ports.SerialPort(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.tmrHora = New System.Windows.Forms.Timer(Me.components)
        Me.FileSystemWatcher1 = New System.IO.FileSystemWatcher()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.btnCerrar = New System.Windows.Forms.Button()
        Me.AutomatizacionControl1 = New BasculaConfig.AutomatizacionControl()
        Me.ConfigurationPort1 = New BasculaConfig.ConfigurationPort()
        Me.ThirdCustomControl1 = New BasculaConfig.ThirdCustomControl()
        Me.SecondCustomControl1 = New BasculaConfig.SecondCustomControl()
        Me.FirstCustomControl1 = New BasculaConfig.FirstCustomControl()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(41, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.Panel4.Controls.Add(Me.btnAutomatizacion)
        Me.Panel4.Controls.Add(Me.btnConfigPuerto)
        Me.Panel4.Controls.Add(Me.btnConfigBajoDemanda)
        Me.Panel4.Controls.Add(Me.SidePanel)
        Me.Panel4.Controls.Add(Me.btnConfig)
        Me.Panel4.Controls.Add(Me.btnInicio)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(208, 571)
        Me.Panel4.TabIndex = 4
        '
        'btnAutomatizacion
        '
        Me.btnAutomatizacion.FlatAppearance.BorderSize = 0
        Me.btnAutomatizacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAutomatizacion.Font = New System.Drawing.Font("Century Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAutomatizacion.ForeColor = System.Drawing.Color.White
        Me.btnAutomatizacion.Image = CType(resources.GetObject("btnAutomatizacion.Image"), System.Drawing.Image)
        Me.btnAutomatizacion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAutomatizacion.Location = New System.Drawing.Point(11, 347)
        Me.btnAutomatizacion.Name = "btnAutomatizacion"
        Me.btnAutomatizacion.Size = New System.Drawing.Size(197, 54)
        Me.btnAutomatizacion.TabIndex = 38
        Me.btnAutomatizacion.Text = "       Automatización"
        Me.btnAutomatizacion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnAutomatizacion.UseVisualStyleBackColor = True
        '
        'btnConfigPuerto
        '
        Me.btnConfigPuerto.FlatAppearance.BorderSize = 0
        Me.btnConfigPuerto.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnConfigPuerto.Font = New System.Drawing.Font("Century Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnConfigPuerto.ForeColor = System.Drawing.Color.White
        Me.btnConfigPuerto.Image = CType(resources.GetObject("btnConfigPuerto.Image"), System.Drawing.Image)
        Me.btnConfigPuerto.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnConfigPuerto.Location = New System.Drawing.Point(11, 287)
        Me.btnConfigPuerto.Name = "btnConfigPuerto"
        Me.btnConfigPuerto.Size = New System.Drawing.Size(197, 54)
        Me.btnConfigPuerto.TabIndex = 37
        Me.btnConfigPuerto.Text = "       Configuración         Puerto"
        Me.btnConfigPuerto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnConfigPuerto.UseVisualStyleBackColor = True
        '
        'btnConfigBajoDemanda
        '
        Me.btnConfigBajoDemanda.FlatAppearance.BorderSize = 0
        Me.btnConfigBajoDemanda.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnConfigBajoDemanda.Font = New System.Drawing.Font("Century Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnConfigBajoDemanda.ForeColor = System.Drawing.Color.White
        Me.btnConfigBajoDemanda.Image = CType(resources.GetObject("btnConfigBajoDemanda.Image"), System.Drawing.Image)
        Me.btnConfigBajoDemanda.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnConfigBajoDemanda.Location = New System.Drawing.Point(11, 227)
        Me.btnConfigBajoDemanda.Name = "btnConfigBajoDemanda"
        Me.btnConfigBajoDemanda.Size = New System.Drawing.Size(197, 54)
        Me.btnConfigBajoDemanda.TabIndex = 9
        Me.btnConfigBajoDemanda.Text = "       Configuración         Bajo Demanda"
        Me.btnConfigBajoDemanda.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnConfigBajoDemanda.UseVisualStyleBackColor = True
        '
        'SidePanel
        '
        Me.SidePanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(178, Byte), Integer), CType(CType(8, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.SidePanel.Location = New System.Drawing.Point(0, 109)
        Me.SidePanel.Name = "SidePanel"
        Me.SidePanel.Size = New System.Drawing.Size(10, 54)
        Me.SidePanel.TabIndex = 5
        '
        'btnConfig
        '
        Me.btnConfig.FlatAppearance.BorderSize = 0
        Me.btnConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnConfig.Font = New System.Drawing.Font("Century Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnConfig.ForeColor = System.Drawing.Color.White
        Me.btnConfig.Image = CType(resources.GetObject("btnConfig.Image"), System.Drawing.Image)
        Me.btnConfig.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnConfig.Location = New System.Drawing.Point(11, 167)
        Me.btnConfig.Name = "btnConfig"
        Me.btnConfig.Size = New System.Drawing.Size(197, 54)
        Me.btnConfig.TabIndex = 6
        Me.btnConfig.Text = "       Configuración    Continuo"
        Me.btnConfig.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnConfig.UseVisualStyleBackColor = True
        '
        'btnInicio
        '
        Me.btnInicio.FlatAppearance.BorderSize = 0
        Me.btnInicio.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInicio.Font = New System.Drawing.Font("Century Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnInicio.ForeColor = System.Drawing.Color.White
        Me.btnInicio.Image = CType(resources.GetObject("btnInicio.Image"), System.Drawing.Image)
        Me.btnInicio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnInicio.Location = New System.Drawing.Point(11, 107)
        Me.btnInicio.Name = "btnInicio"
        Me.btnInicio.Size = New System.Drawing.Size(197, 54)
        Me.btnInicio.TabIndex = 8
        Me.btnInicio.Text = "       Inicio"
        Me.btnInicio.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnInicio.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(178, Byte), Integer), CType(CType(8, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(208, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(818, 10)
        Me.Panel2.TabIndex = 5
        '
        'btnMini
        '
        Me.btnMini.FlatAppearance.BorderSize = 0
        Me.btnMini.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMini.Font = New System.Drawing.Font("Century Gothic", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMini.ForeColor = System.Drawing.Color.Gray
        Me.btnMini.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMini.Location = New System.Drawing.Point(945, 22)
        Me.btnMini.Name = "btnMini"
        Me.btnMini.Size = New System.Drawing.Size(32, 35)
        Me.btnMini.TabIndex = 33
        Me.btnMini.Text = "-"
        Me.btnMini.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnMini.UseVisualStyleBackColor = True
        '
        'lblHora
        '
        Me.lblHora.AutoSize = True
        Me.lblHora.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHora.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblHora.Location = New System.Drawing.Point(602, 28)
        Me.lblHora.Name = "lblHora"
        Me.lblHora.Size = New System.Drawing.Size(14, 21)
        Me.lblHora.TabIndex = 32
        Me.lblHora.Text = "."
        '
        'lblFecha
        '
        Me.lblFecha.AutoSize = True
        Me.lblFecha.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFecha.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFecha.Location = New System.Drawing.Point(772, 28)
        Me.lblFecha.Name = "lblFecha"
        Me.lblFecha.Size = New System.Drawing.Size(14, 21)
        Me.lblFecha.TabIndex = 31
        Me.lblFecha.Text = "."
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(353, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(179, 21)
        Me.Label3.TabIndex = 29
        Me.Label3.Text = "Aplicación Versión 3.0"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(178, Byte), Integer), CType(CType(8, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.PictureBox1)
        Me.Panel3.Location = New System.Drawing.Point(245, 8)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(107, 143)
        Me.Panel3.TabIndex = 28
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(3, 106)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 17)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "App"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(3, 87)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 19)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Balanzas"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.BasculaConfig.My.Resources.Resources.basculas1
        Me.PictureBox1.Location = New System.Drawing.Point(6, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(96, 84)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 4
        Me.PictureBox1.TabStop = False
        '
        'tmrTimer
        '
        Me.tmrTimer.Interval = 500
        '
        'tmrHora
        '
        Me.tmrHora.Interval = 1000
        '
        'FileSystemWatcher1
        '
        Me.FileSystemWatcher1.EnableRaisingEvents = True
        Me.FileSystemWatcher1.SynchronizingObject = Me
        '
        'btnCerrar
        '
        Me.btnCerrar.FlatAppearance.BorderSize = 0
        Me.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCerrar.Font = New System.Drawing.Font("Century Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCerrar.ForeColor = System.Drawing.Color.White
        Me.btnCerrar.Image = CType(resources.GetObject("btnCerrar.Image"), System.Drawing.Image)
        Me.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCerrar.Location = New System.Drawing.Point(974, 22)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(32, 35)
        Me.btnCerrar.TabIndex = 30
        Me.btnCerrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnCerrar.UseVisualStyleBackColor = True
        '
        'AutomatizacionControl1
        '
        Me.AutomatizacionControl1.BackColor = System.Drawing.Color.White
        Me.AutomatizacionControl1.Location = New System.Drawing.Point(208, 148)
        Me.AutomatizacionControl1.Name = "AutomatizacionControl1"
        Me.AutomatizacionControl1.Size = New System.Drawing.Size(817, 423)
        Me.AutomatizacionControl1.TabIndex = 39
        '
        'ConfigurationPort1
        '
        Me.ConfigurationPort1.Location = New System.Drawing.Point(208, 148)
        Me.ConfigurationPort1.Name = "ConfigurationPort1"
        Me.ConfigurationPort1.Size = New System.Drawing.Size(817, 423)
        Me.ConfigurationPort1.TabIndex = 37
        '
        'ThirdCustomControl1
        '
        Me.ThirdCustomControl1.Location = New System.Drawing.Point(208, 148)
        Me.ThirdCustomControl1.Name = "ThirdCustomControl1"
        Me.ThirdCustomControl1.Size = New System.Drawing.Size(817, 423)
        Me.ThirdCustomControl1.TabIndex = 36
        '
        'SecondCustomControl1
        '
        Me.SecondCustomControl1.Location = New System.Drawing.Point(208, 148)
        Me.SecondCustomControl1.Name = "SecondCustomControl1"
        Me.SecondCustomControl1.Size = New System.Drawing.Size(817, 423)
        Me.SecondCustomControl1.TabIndex = 35
        '
        'FirstCustomControl1
        '
        Me.FirstCustomControl1.Location = New System.Drawing.Point(208, 148)
        Me.FirstCustomControl1.Name = "FirstCustomControl1"
        Me.FirstCustomControl1.Size = New System.Drawing.Size(817, 423)
        Me.FirstCustomControl1.TabIndex = 34
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1026, 571)
        Me.Controls.Add(Me.AutomatizacionControl1)
        Me.Controls.Add(Me.ConfigurationPort1)
        Me.Controls.Add(Me.ThirdCustomControl1)
        Me.Controls.Add(Me.SecondCustomControl1)
        Me.Controls.Add(Me.FirstCustomControl1)
        Me.Controls.Add(Me.btnMini)
        Me.Controls.Add(Me.lblHora)
        Me.Controls.Add(Me.lblFecha)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel4)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        Me.Panel4.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel4 As Panel
    Private WithEvents btnConfigBajoDemanda As Button
    Private WithEvents SidePanel As Panel
    Private WithEvents btnConfig As Button
    Private WithEvents btnInicio As Button
    Friend WithEvents Panel2 As Panel
    Private WithEvents btnMini As Button
    Friend WithEvents lblHora As Label
    Friend WithEvents lblFecha As Label
    Private WithEvents btnCerrar As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents tmrTimer As Timer
    Friend WithEvents spPuertos As IO.Ports.SerialPort
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents tmrHora As Timer
    Friend WithEvents FileSystemWatcher1 As IO.FileSystemWatcher
    Friend WithEvents FirstCustomControl1 As FirstCustomControl
    Friend WithEvents ThirdCustomControl1 As ThirdCustomControl
    Friend WithEvents SecondCustomControl1 As SecondCustomControl
    Private WithEvents btnConfigPuerto As Button
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents ConfigurationPort1 As ConfigurationPort
    Friend WithEvents btnAutomatizacion As Button
    Friend WithEvents AutomatizacionControl1 As AutomatizacionControl
End Class
