<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AutomatizacionControl
    Inherits System.Windows.Forms.UserControl

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

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lblIntro = New System.Windows.Forms.Label()
        Me.lblTituloArranque = New System.Windows.Forms.Label()
        Me.chkInicioWindows = New System.Windows.Forms.CheckBox()
        Me.lblNotaArranque = New System.Windows.Forms.Label()
        Me.lblTituloLectura = New System.Windows.Forms.Label()
        Me.rdbEmpezarSolo = New System.Windows.Forms.RadioButton()
        Me.rdbEsperarOperativo = New System.Windows.Forms.RadioButton()
        Me.lblTituloIntervalo = New System.Windows.Forms.Label()
        Me.nudIntervalo = New System.Windows.Forms.NumericUpDown()
        Me.lblSegundos = New System.Windows.Forms.Label()
        Me.lblResultado = New System.Windows.Forms.Label()
        CType(Me.nudIntervalo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblIntro
        '
        Me.lblIntro.AutoSize = True
        Me.lblIntro.Font = New System.Drawing.Font("Century Gothic", 11.25!)
        Me.lblIntro.Location = New System.Drawing.Point(79, 26)
        Me.lblIntro.Name = "lblIntro"
        Me.lblIntro.Size = New System.Drawing.Size(517, 20)
        Me.lblIntro.TabIndex = 0
        Me.lblIntro.Text = "Define cómo trabajará el lector de peso en esta computadora."
        '
        'lblTituloArranque
        '
        Me.lblTituloArranque.AutoSize = True
        Me.lblTituloArranque.Font = New System.Drawing.Font("Century Gothic", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTituloArranque.Location = New System.Drawing.Point(79, 66)
        Me.lblTituloArranque.Name = "lblTituloArranque"
        Me.lblTituloArranque.Size = New System.Drawing.Size(214, 26)
        Me.lblTituloArranque.TabIndex = 1
        Me.lblTituloArranque.Text = "Puesta en marcha"
        '
        'chkInicioWindows
        '
        Me.chkInicioWindows.AutoSize = True
        Me.chkInicioWindows.Checked = True
        Me.chkInicioWindows.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkInicioWindows.Font = New System.Drawing.Font("Century Gothic", 11.25!)
        Me.chkInicioWindows.Location = New System.Drawing.Point(100, 108)
        Me.chkInicioWindows.Name = "chkInicioWindows"
        Me.chkInicioWindows.Size = New System.Drawing.Size(383, 24)
        Me.chkInicioWindows.TabIndex = 2
        Me.chkInicioWindows.Text = "Abrir el lector al encender la computadora"
        Me.chkInicioWindows.UseVisualStyleBackColor = True
        '
        'lblNotaArranque
        '
        Me.lblNotaArranque.AutoSize = True
        Me.lblNotaArranque.Font = New System.Drawing.Font("Century Gothic", 9.0!)
        Me.lblNotaArranque.ForeColor = System.Drawing.Color.Gray
        Me.lblNotaArranque.Location = New System.Drawing.Point(122, 136)
        Me.lblNotaArranque.Name = "lblNotaArranque"
        Me.lblNotaArranque.Size = New System.Drawing.Size(388, 17)
        Me.lblNotaArranque.TabIndex = 3
        Me.lblNotaArranque.Text = "Queda junto al reloj, listo para usarse en cualquier momento."
        '
        'lblTituloLectura
        '
        Me.lblTituloLectura.AutoSize = True
        Me.lblTituloLectura.Font = New System.Drawing.Font("Century Gothic", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTituloLectura.Location = New System.Drawing.Point(79, 180)
        Me.lblTituloLectura.Name = "lblTituloLectura"
        Me.lblTituloLectura.Size = New System.Drawing.Size(285, 26)
        Me.lblTituloLectura.TabIndex = 4
        Me.lblTituloLectura.Text = "¿Cuándo empieza a leer?"
        '
        'rdbEmpezarSolo
        '
        Me.rdbEmpezarSolo.AutoSize = True
        Me.rdbEmpezarSolo.Checked = True
        Me.rdbEmpezarSolo.Font = New System.Drawing.Font("Century Gothic", 11.25!)
        Me.rdbEmpezarSolo.Location = New System.Drawing.Point(100, 222)
        Me.rdbEmpezarSolo.Name = "rdbEmpezarSolo"
        Me.rdbEmpezarSolo.Size = New System.Drawing.Size(253, 24)
        Me.rdbEmpezarSolo.TabIndex = 5
        Me.rdbEmpezarSolo.Text = "Apenas se abra el lector"
        Me.rdbEmpezarSolo.UseVisualStyleBackColor = True
        '
        'rdbEsperarOperativo
        '
        Me.rdbEsperarOperativo.AutoSize = True
        Me.rdbEsperarOperativo.Font = New System.Drawing.Font("Century Gothic", 11.25!)
        Me.rdbEsperarOperativo.Location = New System.Drawing.Point(100, 252)
        Me.rdbEsperarOperativo.Name = "rdbEsperarOperativo"
        Me.rdbEsperarOperativo.Size = New System.Drawing.Size(376, 24)
        Me.rdbEsperarOperativo.TabIndex = 6
        Me.rdbEsperarOperativo.Text = "Cuando el operativo presione Iniciar"
        Me.rdbEsperarOperativo.UseVisualStyleBackColor = True
        '
        'lblTituloIntervalo
        '
        Me.lblTituloIntervalo.AutoSize = True
        Me.lblTituloIntervalo.Font = New System.Drawing.Font("Century Gothic", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTituloIntervalo.Location = New System.Drawing.Point(79, 298)
        Me.lblTituloIntervalo.Name = "lblTituloIntervalo"
        Me.lblTituloIntervalo.Size = New System.Drawing.Size(219, 26)
        Me.lblTituloIntervalo.TabIndex = 7
        Me.lblTituloIntervalo.Text = "¿Cada cuánto lee?"
        '
        'nudIntervalo
        '
        Me.nudIntervalo.Font = New System.Drawing.Font("Century Gothic", 11.25!)
        Me.nudIntervalo.Location = New System.Drawing.Point(100, 340)
        Me.nudIntervalo.Maximum = New Decimal(New Integer() {3600, 0, 0, 0})
        Me.nudIntervalo.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudIntervalo.Name = "nudIntervalo"
        Me.nudIntervalo.Size = New System.Drawing.Size(72, 27)
        Me.nudIntervalo.TabIndex = 8
        Me.nudIntervalo.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'lblSegundos
        '
        Me.lblSegundos.AutoSize = True
        Me.lblSegundos.Font = New System.Drawing.Font("Century Gothic", 11.25!)
        Me.lblSegundos.Location = New System.Drawing.Point(182, 343)
        Me.lblSegundos.Name = "lblSegundos"
        Me.lblSegundos.Size = New System.Drawing.Size(388, 20)
        Me.lblSegundos.TabIndex = 9
        Me.lblSegundos.Text = "segundos entre una lectura y la siguiente"
        '
        'lblResultado
        '
        Me.lblResultado.Font = New System.Drawing.Font("Century Gothic", 9.75!)
        Me.lblResultado.ForeColor = System.Drawing.Color.Gray
        Me.lblResultado.Location = New System.Drawing.Point(79, 386)
        Me.lblResultado.Name = "lblResultado"
        Me.lblResultado.Size = New System.Drawing.Size(660, 22)
        Me.lblResultado.TabIndex = 10
        '
        'AutomatizacionControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.lblResultado)
        Me.Controls.Add(Me.lblSegundos)
        Me.Controls.Add(Me.nudIntervalo)
        Me.Controls.Add(Me.lblTituloIntervalo)
        Me.Controls.Add(Me.rdbEsperarOperativo)
        Me.Controls.Add(Me.rdbEmpezarSolo)
        Me.Controls.Add(Me.lblTituloLectura)
        Me.Controls.Add(Me.lblNotaArranque)
        Me.Controls.Add(Me.chkInicioWindows)
        Me.Controls.Add(Me.lblTituloArranque)
        Me.Controls.Add(Me.lblIntro)
        Me.Name = "AutomatizacionControl"
        Me.Size = New System.Drawing.Size(817, 423)
        CType(Me.nudIntervalo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblIntro As Label
    Friend WithEvents lblTituloArranque As Label
    Friend WithEvents chkInicioWindows As CheckBox
    Friend WithEvents lblNotaArranque As Label
    Friend WithEvents lblTituloLectura As Label
    Friend WithEvents rdbEmpezarSolo As RadioButton
    Friend WithEvents rdbEsperarOperativo As RadioButton
    Friend WithEvents lblTituloIntervalo As Label
    Friend WithEvents nudIntervalo As NumericUpDown
    Friend WithEvents lblSegundos As Label
    Friend WithEvents lblResultado As Label

End Class
