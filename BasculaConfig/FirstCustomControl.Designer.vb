<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FirstCustomControl
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
        Me.rdbDemanda = New System.Windows.Forms.RadioButton()
        Me.rdbContinuo = New System.Windows.Forms.RadioButton()
        Me.btnFinalizar = New System.Windows.Forms.Button()
        Me.lblConfigxd = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnSiguiente = New System.Windows.Forms.Button()
        Me.label6 = New System.Windows.Forms.Label()
        Me.spPuertos = New System.IO.Ports.SerialPort(Me.components)
        Me.tmrTimer = New System.Windows.Forms.Timer(Me.components)
        Me.tmrButton = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'rdbDemanda
        '
        Me.rdbDemanda.AutoSize = True
        Me.rdbDemanda.Font = New System.Drawing.Font("Century Gothic", 11.25!)
        Me.rdbDemanda.Location = New System.Drawing.Point(422, 182)
        Me.rdbDemanda.Name = "rdbDemanda"
        Me.rdbDemanda.Size = New System.Drawing.Size(136, 24)
        Me.rdbDemanda.TabIndex = 32
        Me.rdbDemanda.TabStop = True
        Me.rdbDemanda.Text = "Bajo Demanda"
        Me.rdbDemanda.UseVisualStyleBackColor = True
        '
        'rdbContinuo
        '
        Me.rdbContinuo.AutoSize = True
        Me.rdbContinuo.Font = New System.Drawing.Font("Century Gothic", 11.25!)
        Me.rdbContinuo.Location = New System.Drawing.Point(272, 182)
        Me.rdbContinuo.Name = "rdbContinuo"
        Me.rdbContinuo.Size = New System.Drawing.Size(94, 24)
        Me.rdbContinuo.TabIndex = 31
        Me.rdbContinuo.TabStop = True
        Me.rdbContinuo.Text = "Continuo"
        Me.rdbContinuo.UseVisualStyleBackColor = True
        '
        'btnFinalizar
        '
        Me.btnFinalizar.BackColor = System.Drawing.Color.FromArgb(CType(CType(182, Byte), Integer), CType(CType(172, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnFinalizar.FlatAppearance.BorderSize = 0
        Me.btnFinalizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFinalizar.Font = New System.Drawing.Font("Century Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFinalizar.ForeColor = System.Drawing.Color.White
        Me.btnFinalizar.Location = New System.Drawing.Point(476, 259)
        Me.btnFinalizar.Name = "btnFinalizar"
        Me.btnFinalizar.Size = New System.Drawing.Size(148, 44)
        Me.btnFinalizar.TabIndex = 30
        Me.btnFinalizar.Text = "Guardar"
        Me.btnFinalizar.UseVisualStyleBackColor = False
        '
        'lblConfigxd
        '
        Me.lblConfigxd.AutoSize = True
        Me.lblConfigxd.Font = New System.Drawing.Font("Century Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConfigxd.Location = New System.Drawing.Point(79, 59)
        Me.lblConfigxd.Name = "lblConfigxd"
        Me.lblConfigxd.Size = New System.Drawing.Size(386, 20)
        Me.lblConfigxd.TabIndex = 29
        Me.lblConfigxd.Text = "Realiza las configuraciones para poder conectarte."
        Me.lblConfigxd.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Century Gothic", 20.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(280, 110)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(245, 32)
        Me.Label1.TabIndex = 28
        Me.Label1.Text = "Modo de trabajo:"
        '
        'btnSiguiente
        '
        Me.btnSiguiente.BackColor = System.Drawing.Color.FromArgb(CType(CType(182, Byte), Integer), CType(CType(172, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSiguiente.FlatAppearance.BorderSize = 0
        Me.btnSiguiente.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSiguiente.Font = New System.Drawing.Font("Century Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSiguiente.ForeColor = System.Drawing.Color.White
        Me.btnSiguiente.Location = New System.Drawing.Point(187, 259)
        Me.btnSiguiente.Name = "btnSiguiente"
        Me.btnSiguiente.Size = New System.Drawing.Size(148, 43)
        Me.btnSiguiente.TabIndex = 27
        Me.btnSiguiente.Text = "Siguiente"
        Me.btnSiguiente.UseVisualStyleBackColor = False
        '
        'label6
        '
        Me.label6.AutoSize = True
        Me.label6.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label6.Location = New System.Drawing.Point(80, 123)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(0, 17)
        Me.label6.TabIndex = 26
        '
        'tmrTimer
        '
        Me.tmrTimer.Enabled = True
        Me.tmrTimer.Interval = 500
        '
        'tmrButton
        '
        Me.tmrButton.Enabled = True
        Me.tmrButton.Interval = 500
        '
        'FirstCustomControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.rdbDemanda)
        Me.Controls.Add(Me.rdbContinuo)
        Me.Controls.Add(Me.btnFinalizar)
        Me.Controls.Add(Me.lblConfigxd)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnSiguiente)
        Me.Controls.Add(Me.label6)
        Me.Name = "FirstCustomControl"
        Me.Size = New System.Drawing.Size(817, 423)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents rdbDemanda As RadioButton
    Friend WithEvents rdbContinuo As RadioButton
    Private WithEvents btnFinalizar As Button
    Private WithEvents lblConfigxd As Label
    Private WithEvents Label1 As Label
    Private WithEvents btnSiguiente As Button
    Private WithEvents label6 As Label
    Friend WithEvents spPuertos As IO.Ports.SerialPort
    Friend WithEvents tmrTimer As Timer
    Friend WithEvents tmrButton As Timer
End Class
