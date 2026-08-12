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

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.pnlEncabezado = New System.Windows.Forms.Panel()
        Me.lblTitulo = New System.Windows.Forms.Label()
        Me.btnMinimizar = New System.Windows.Forms.Button()
        Me.btnCerrar = New System.Windows.Forms.Button()
        Me.lblIndicador = New System.Windows.Forms.Label()
        Me.lblEstado = New System.Windows.Forms.Label()
        Me.lblPeso = New System.Windows.Forms.Label()
        Me.lblUltimaLectura = New System.Windows.Forms.Label()
        Me.pnlBarraFondo = New System.Windows.Forms.Panel()
        Me.pnlBarraAvance = New System.Windows.Forms.Panel()
        Me.lblDetalle = New System.Windows.Forms.Label()
        Me.pnlAviso = New System.Windows.Forms.Panel()
        Me.lblAviso = New System.Windows.Forms.Label()
        Me.btnPrincipal = New System.Windows.Forms.Button()
        Me.lnkRegistro = New System.Windows.Forms.LinkLabel()
        Me.tmrLectura = New System.Windows.Forms.Timer(Me.components)
        Me.tmrInterfaz = New System.Windows.Forms.Timer(Me.components)
        Me.iconoBandeja = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.menuBandeja = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.menuMostrar = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuAlternar = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuSeparador = New System.Windows.Forms.ToolStripSeparator()
        Me.menuSalir = New System.Windows.Forms.ToolStripMenuItem()
        Me.pnlEncabezado.SuspendLayout()
        Me.pnlBarraFondo.SuspendLayout()
        Me.pnlAviso.SuspendLayout()
        Me.menuBandeja.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlEncabezado
        '
        Me.pnlEncabezado.BackColor = System.Drawing.Color.FromArgb(CType(CType(178, Byte), Integer), CType(CType(8, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.pnlEncabezado.Controls.Add(Me.lblTitulo)
        Me.pnlEncabezado.Controls.Add(Me.btnMinimizar)
        Me.pnlEncabezado.Controls.Add(Me.btnCerrar)
        Me.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlEncabezado.Location = New System.Drawing.Point(0, 0)
        Me.pnlEncabezado.Name = "pnlEncabezado"
        Me.pnlEncabezado.Size = New System.Drawing.Size(400, 46)
        Me.pnlEncabezado.TabIndex = 0
        '
        'lblTitulo
        '
        Me.lblTitulo.AutoSize = True
        Me.lblTitulo.BackColor = System.Drawing.Color.Transparent
        Me.lblTitulo.Font = New System.Drawing.Font("Century Gothic", 11.25!)
        Me.lblTitulo.ForeColor = System.Drawing.Color.White
        Me.lblTitulo.Location = New System.Drawing.Point(18, 13)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(129, 18)
        Me.lblTitulo.TabIndex = 0
        Me.lblTitulo.Text = "Lector de báscula"
        '
        'btnMinimizar
        '
        Me.btnMinimizar.FlatAppearance.BorderSize = 0
        Me.btnMinimizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMinimizar.Font = New System.Drawing.Font("Century Gothic", 11.25!)
        Me.btnMinimizar.ForeColor = System.Drawing.Color.White
        Me.btnMinimizar.Location = New System.Drawing.Point(312, 0)
        Me.btnMinimizar.Name = "btnMinimizar"
        Me.btnMinimizar.Size = New System.Drawing.Size(44, 46)
        Me.btnMinimizar.TabIndex = 1
        Me.btnMinimizar.TabStop = False
        Me.btnMinimizar.Text = "—"
        Me.btnMinimizar.UseVisualStyleBackColor = True
        '
        'btnCerrar
        '
        Me.btnCerrar.FlatAppearance.BorderSize = 0
        Me.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCerrar.Font = New System.Drawing.Font("Century Gothic", 11.25!)
        Me.btnCerrar.ForeColor = System.Drawing.Color.White
        Me.btnCerrar.Location = New System.Drawing.Point(356, 0)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(44, 46)
        Me.btnCerrar.TabIndex = 2
        Me.btnCerrar.TabStop = False
        Me.btnCerrar.Text = "✕"
        Me.btnCerrar.UseVisualStyleBackColor = True
        '
        'lblIndicador
        '
        Me.lblIndicador.AutoSize = True
        Me.lblIndicador.Font = New System.Drawing.Font("Century Gothic", 12.0!)
        Me.lblIndicador.ForeColor = System.Drawing.Color.Silver
        Me.lblIndicador.Location = New System.Drawing.Point(22, 60)
        Me.lblIndicador.Name = "lblIndicador"
        Me.lblIndicador.Size = New System.Drawing.Size(18, 19)
        Me.lblIndicador.TabIndex = 1
        Me.lblIndicador.Text = "●"
        '
        'lblEstado
        '
        Me.lblEstado.Font = New System.Drawing.Font("Century Gothic", 11.25!)
        Me.lblEstado.ForeColor = System.Drawing.Color.FromArgb(CType(CType(41, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.lblEstado.Location = New System.Drawing.Point(44, 61)
        Me.lblEstado.Name = "lblEstado"
        Me.lblEstado.Size = New System.Drawing.Size(334, 20)
        Me.lblEstado.TabIndex = 2
        Me.lblEstado.Text = "Detenido"
        '
        'lblPeso
        '
        Me.lblPeso.Font = New System.Drawing.Font("Century Gothic", 34.0!)
        Me.lblPeso.ForeColor = System.Drawing.Color.FromArgb(CType(CType(41, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.lblPeso.Location = New System.Drawing.Point(0, 88)
        Me.lblPeso.Name = "lblPeso"
        Me.lblPeso.Size = New System.Drawing.Size(400, 58)
        Me.lblPeso.TabIndex = 3
        Me.lblPeso.Text = "--"
        Me.lblPeso.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblUltimaLectura
        '
        Me.lblUltimaLectura.Font = New System.Drawing.Font("Century Gothic", 8.25!)
        Me.lblUltimaLectura.ForeColor = System.Drawing.Color.Gray
        Me.lblUltimaLectura.Location = New System.Drawing.Point(0, 148)
        Me.lblUltimaLectura.Name = "lblUltimaLectura"
        Me.lblUltimaLectura.Size = New System.Drawing.Size(400, 18)
        Me.lblUltimaLectura.TabIndex = 4
        Me.lblUltimaLectura.Text = "Sin lecturas todavía"
        Me.lblUltimaLectura.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlBarraFondo
        '
        Me.pnlBarraFondo.BackColor = System.Drawing.Color.FromArgb(CType(CType(228, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(228, Byte), Integer))
        Me.pnlBarraFondo.Controls.Add(Me.pnlBarraAvance)
        Me.pnlBarraFondo.Location = New System.Drawing.Point(24, 178)
        Me.pnlBarraFondo.Name = "pnlBarraFondo"
        Me.pnlBarraFondo.Size = New System.Drawing.Size(352, 4)
        Me.pnlBarraFondo.TabIndex = 5
        '
        'pnlBarraAvance
        '
        Me.pnlBarraAvance.BackColor = System.Drawing.Color.FromArgb(CType(CType(182, Byte), Integer), CType(CType(172, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlBarraAvance.Location = New System.Drawing.Point(0, 0)
        Me.pnlBarraAvance.Name = "pnlBarraAvance"
        Me.pnlBarraAvance.Size = New System.Drawing.Size(0, 4)
        Me.pnlBarraAvance.TabIndex = 0
        '
        'lblDetalle
        '
        Me.lblDetalle.Font = New System.Drawing.Font("Century Gothic", 8.25!)
        Me.lblDetalle.ForeColor = System.Drawing.Color.Gray
        Me.lblDetalle.Location = New System.Drawing.Point(0, 190)
        Me.lblDetalle.Name = "lblDetalle"
        Me.lblDetalle.Size = New System.Drawing.Size(400, 18)
        Me.lblDetalle.TabIndex = 6
        Me.lblDetalle.Text = "Sin configurar"
        Me.lblDetalle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlAviso
        '
        Me.pnlAviso.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.pnlAviso.Controls.Add(Me.lblAviso)
        Me.pnlAviso.Location = New System.Drawing.Point(24, 216)
        Me.pnlAviso.Name = "pnlAviso"
        Me.pnlAviso.Padding = New System.Windows.Forms.Padding(10, 6, 10, 6)
        Me.pnlAviso.Size = New System.Drawing.Size(352, 56)
        Me.pnlAviso.TabIndex = 7
        Me.pnlAviso.Visible = False
        '
        'lblAviso
        '
        Me.lblAviso.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblAviso.Font = New System.Drawing.Font("Century Gothic", 8.25!)
        Me.lblAviso.ForeColor = System.Drawing.Color.FromArgb(CType(CType(150, Byte), Integer), CType(CType(95, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblAviso.Location = New System.Drawing.Point(10, 6)
        Me.lblAviso.Name = "lblAviso"
        Me.lblAviso.Size = New System.Drawing.Size(332, 44)
        Me.lblAviso.TabIndex = 0
        Me.lblAviso.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnPrincipal
        '
        Me.btnPrincipal.BackColor = System.Drawing.Color.FromArgb(CType(CType(182, Byte), Integer), CType(CType(172, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnPrincipal.FlatAppearance.BorderSize = 0
        Me.btnPrincipal.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrincipal.Font = New System.Drawing.Font("Century Gothic", 11.25!)
        Me.btnPrincipal.ForeColor = System.Drawing.Color.White
        Me.btnPrincipal.Location = New System.Drawing.Point(24, 284)
        Me.btnPrincipal.Name = "btnPrincipal"
        Me.btnPrincipal.Size = New System.Drawing.Size(180, 40)
        Me.btnPrincipal.TabIndex = 8
        Me.btnPrincipal.Text = "Iniciar"
        Me.btnPrincipal.UseVisualStyleBackColor = False
        '
        'lnkRegistro
        '
        Me.lnkRegistro.ActiveLinkColor = System.Drawing.Color.FromArgb(CType(CType(178, Byte), Integer), CType(CType(8, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.lnkRegistro.Font = New System.Drawing.Font("Century Gothic", 8.25!)
        Me.lnkRegistro.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.lnkRegistro.LinkColor = System.Drawing.Color.Gray
        Me.lnkRegistro.Location = New System.Drawing.Point(248, 296)
        Me.lnkRegistro.Name = "lnkRegistro"
        Me.lnkRegistro.Size = New System.Drawing.Size(128, 18)
        Me.lnkRegistro.TabIndex = 9
        Me.lnkRegistro.TabStop = True
        Me.lnkRegistro.Text = "Ver registro"
        Me.lnkRegistro.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tmrLectura
        '
        Me.tmrLectura.Interval = 5000
        '
        'tmrInterfaz
        '
        Me.tmrInterfaz.Interval = 200
        '
        'iconoBandeja
        '
        Me.iconoBandeja.ContextMenuStrip = Me.menuBandeja
        Me.iconoBandeja.Text = "Lector de báscula"
        Me.iconoBandeja.Visible = True
        '
        'menuBandeja
        '
        Me.menuBandeja.Font = New System.Drawing.Font("Century Gothic", 9.0!)
        Me.menuBandeja.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuMostrar, Me.menuAlternar, Me.menuSeparador, Me.menuSalir})
        Me.menuBandeja.Name = "menuBandeja"
        Me.menuBandeja.Size = New System.Drawing.Size(181, 76)
        '
        'menuMostrar
        '
        Me.menuMostrar.Name = "menuMostrar"
        Me.menuMostrar.Size = New System.Drawing.Size(180, 22)
        Me.menuMostrar.Text = "Mostrar ventana"
        '
        'menuAlternar
        '
        Me.menuAlternar.Name = "menuAlternar"
        Me.menuAlternar.Size = New System.Drawing.Size(180, 22)
        Me.menuAlternar.Text = "Iniciar lectura"
        '
        'menuSeparador
        '
        Me.menuSeparador.Name = "menuSeparador"
        Me.menuSeparador.Size = New System.Drawing.Size(177, 6)
        '
        'menuSalir
        '
        Me.menuSalir.Name = "menuSalir"
        Me.menuSalir.Size = New System.Drawing.Size(180, 22)
        Me.menuSalir.Text = "Salir"
        '
        'Form1
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(400, 344)
        Me.Controls.Add(Me.lnkRegistro)
        Me.Controls.Add(Me.btnPrincipal)
        Me.Controls.Add(Me.pnlAviso)
        Me.Controls.Add(Me.lblDetalle)
        Me.Controls.Add(Me.pnlBarraFondo)
        Me.Controls.Add(Me.lblUltimaLectura)
        Me.Controls.Add(Me.lblPeso)
        Me.Controls.Add(Me.lblEstado)
        Me.Controls.Add(Me.lblIndicador)
        Me.Controls.Add(Me.pnlEncabezado)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = True
        Me.Name = "Form1"
        Me.ShowInTaskbar = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Lector de báscula"
        Me.pnlEncabezado.ResumeLayout(False)
        Me.pnlEncabezado.PerformLayout()
        Me.pnlBarraFondo.ResumeLayout(False)
        Me.pnlAviso.ResumeLayout(False)
        Me.menuBandeja.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlEncabezado As Panel
    Friend WithEvents lblTitulo As Label
    Friend WithEvents btnMinimizar As Button
    Friend WithEvents btnCerrar As Button
    Friend WithEvents lblIndicador As Label
    Friend WithEvents lblEstado As Label
    Friend WithEvents lblPeso As Label
    Friend WithEvents lblUltimaLectura As Label
    Friend WithEvents pnlBarraFondo As Panel
    Friend WithEvents pnlBarraAvance As Panel
    Friend WithEvents lblDetalle As Label
    Friend WithEvents pnlAviso As Panel
    Friend WithEvents lblAviso As Label
    Friend WithEvents btnPrincipal As Button
    Friend WithEvents lnkRegistro As LinkLabel
    Friend WithEvents tmrLectura As Timer
    Friend WithEvents tmrInterfaz As Timer
    Friend WithEvents iconoBandeja As NotifyIcon
    Friend WithEvents menuBandeja As ContextMenuStrip
    Friend WithEvents menuMostrar As ToolStripMenuItem
    Friend WithEvents menuAlternar As ToolStripMenuItem
    Friend WithEvents menuSeparador As ToolStripSeparator
    Friend WithEvents menuSalir As ToolStripMenuItem

End Class
