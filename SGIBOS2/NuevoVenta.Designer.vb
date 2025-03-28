<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NuevoVenta
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnAñadirVen = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.dgvDetalleVenta = New System.Windows.Forms.DataGridView()
        Me.lblSubtotal = New System.Windows.Forms.Label()
        Me.txtPrecioUni = New System.Windows.Forms.TextBox()
        Me.txtCantidad = New System.Windows.Forms.TextBox()
        Me.txtIDdetalle = New System.Windows.Forms.TextBox()
        Me.cmbProducto = New System.Windows.Forms.ComboBox()
        Me.txtNumVenta = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtNumVenta2 = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblFechaActual = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblTotalVenta = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cmbMetodoPago = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtCliente = New System.Windows.Forms.TextBox()
        Me.btnGuardarSalir = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvDetalleVenta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnAñadirVen
        '
        Me.btnAñadirVen.Location = New System.Drawing.Point(63, 227)
        Me.btnAñadirVen.Margin = New System.Windows.Forms.Padding(2)
        Me.btnAñadirVen.Name = "btnAñadirVen"
        Me.btnAñadirVen.Size = New System.Drawing.Size(130, 40)
        Me.btnAñadirVen.TabIndex = 0
        Me.btnAñadirVen.Text = "Añadir "
        Me.btnAñadirVen.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 30)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Num. de venta:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 62)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Producto:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 90)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(85, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "ID detalle venta:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dgvDetalleVenta)
        Me.GroupBox1.Controls.Add(Me.lblSubtotal)
        Me.GroupBox1.Controls.Add(Me.txtPrecioUni)
        Me.GroupBox1.Controls.Add(Me.txtCantidad)
        Me.GroupBox1.Controls.Add(Me.txtIDdetalle)
        Me.GroupBox1.Controls.Add(Me.cmbProducto)
        Me.GroupBox1.Controls.Add(Me.txtNumVenta)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.btnAñadirVen)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(15, 14)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Size = New System.Drawing.Size(644, 291)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Detalle de venta"
        '
        'dgvDetalleVenta
        '
        Me.dgvDetalleVenta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDetalleVenta.Location = New System.Drawing.Point(289, 30)
        Me.dgvDetalleVenta.Margin = New System.Windows.Forms.Padding(2)
        Me.dgvDetalleVenta.Name = "dgvDetalleVenta"
        Me.dgvDetalleVenta.RowHeadersWidth = 62
        Me.dgvDetalleVenta.RowTemplate.Height = 28
        Me.dgvDetalleVenta.Size = New System.Drawing.Size(322, 190)
        Me.dgvDetalleVenta.TabIndex = 13
        '
        'lblSubtotal
        '
        Me.lblSubtotal.AutoSize = True
        Me.lblSubtotal.Location = New System.Drawing.Point(115, 194)
        Me.lblSubtotal.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblSubtotal.Name = "lblSubtotal"
        Me.lblSubtotal.Size = New System.Drawing.Size(28, 13)
        Me.lblSubtotal.TabIndex = 12
        Me.lblSubtotal.Text = "0.00"
        '
        'txtPrecioUni
        '
        Me.txtPrecioUni.Location = New System.Drawing.Point(115, 150)
        Me.txtPrecioUni.Margin = New System.Windows.Forms.Padding(2)
        Me.txtPrecioUni.Name = "txtPrecioUni"
        Me.txtPrecioUni.Size = New System.Drawing.Size(128, 20)
        Me.txtPrecioUni.TabIndex = 11
        '
        'txtCantidad
        '
        Me.txtCantidad.Location = New System.Drawing.Point(115, 119)
        Me.txtCantidad.Margin = New System.Windows.Forms.Padding(2)
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Size = New System.Drawing.Size(128, 20)
        Me.txtCantidad.TabIndex = 10
        '
        'txtIDdetalle
        '
        Me.txtIDdetalle.Location = New System.Drawing.Point(115, 90)
        Me.txtIDdetalle.Margin = New System.Windows.Forms.Padding(2)
        Me.txtIDdetalle.Name = "txtIDdetalle"
        Me.txtIDdetalle.Size = New System.Drawing.Size(128, 20)
        Me.txtIDdetalle.TabIndex = 9
        '
        'cmbProducto
        '
        Me.cmbProducto.FormattingEnabled = True
        Me.cmbProducto.Location = New System.Drawing.Point(115, 56)
        Me.cmbProducto.Margin = New System.Windows.Forms.Padding(2)
        Me.cmbProducto.Name = "cmbProducto"
        Me.cmbProducto.Size = New System.Drawing.Size(128, 21)
        Me.cmbProducto.TabIndex = 8
        '
        'txtNumVenta
        '
        Me.txtNumVenta.Location = New System.Drawing.Point(115, 30)
        Me.txtNumVenta.Margin = New System.Windows.Forms.Padding(2)
        Me.txtNumVenta.Name = "txtNumVenta"
        Me.txtNumVenta.Size = New System.Drawing.Size(128, 20)
        Me.txtNumVenta.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(13, 194)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(49, 13)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Subtotal:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(10, 154)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 13)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Precio unitario:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(10, 119)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Cantidad:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(97, 339)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(80, 13)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "Num. de venta:"
        '
        'txtNumVenta2
        '
        Me.txtNumVenta2.Location = New System.Drawing.Point(190, 337)
        Me.txtNumVenta2.Margin = New System.Windows.Forms.Padding(2)
        Me.txtNumVenta2.Name = "txtNumVenta2"
        Me.txtNumVenta2.Size = New System.Drawing.Size(128, 20)
        Me.txtNumVenta2.TabIndex = 13
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(94, 378)
        Me.Label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(85, 13)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "Fecha de venta:"
        '
        'lblFechaActual
        '
        Me.lblFechaActual.AutoSize = True
        Me.lblFechaActual.Location = New System.Drawing.Point(198, 378)
        Me.lblFechaActual.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblFechaActual.Name = "lblFechaActual"
        Me.lblFechaActual.Size = New System.Drawing.Size(91, 13)
        Me.lblFechaActual.TabIndex = 15
        Me.lblFechaActual.Text = "00-00-0000 00.00"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(385, 341)
        Me.Label9.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(79, 13)
        Me.Label9.TabIndex = 16
        Me.Label9.Text = "Total de venta:"
        '
        'lblTotalVenta
        '
        Me.lblTotalVenta.AutoSize = True
        Me.lblTotalVenta.Location = New System.Drawing.Point(489, 341)
        Me.lblTotalVenta.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblTotalVenta.Name = "lblTotalVenta"
        Me.lblTotalVenta.Size = New System.Drawing.Size(28, 13)
        Me.lblTotalVenta.TabIndex = 17
        Me.lblTotalVenta.Text = "0.00"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(385, 377)
        Me.Label11.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(88, 13)
        Me.Label11.TabIndex = 18
        Me.Label11.Text = "Metodo de pago:"
        '
        'cmbMetodoPago
        '
        Me.cmbMetodoPago.FormattingEnabled = True
        Me.cmbMetodoPago.Location = New System.Drawing.Point(489, 372)
        Me.cmbMetodoPago.Margin = New System.Windows.Forms.Padding(2)
        Me.cmbMetodoPago.Name = "cmbMetodoPago"
        Me.cmbMetodoPago.Size = New System.Drawing.Size(128, 21)
        Me.cmbMetodoPago.TabIndex = 13
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(104, 418)
        Me.Label12.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(42, 13)
        Me.Label12.TabIndex = 19
        Me.Label12.Text = "Cliente:"
        '
        'txtCliente
        '
        Me.txtCliente.Location = New System.Drawing.Point(198, 414)
        Me.txtCliente.Margin = New System.Windows.Forms.Padding(2)
        Me.txtCliente.Name = "txtCliente"
        Me.txtCliente.Size = New System.Drawing.Size(128, 20)
        Me.txtCliente.TabIndex = 20
        '
        'btnGuardarSalir
        '
        Me.btnGuardarSalir.Location = New System.Drawing.Point(287, 464)
        Me.btnGuardarSalir.Margin = New System.Windows.Forms.Padding(2)
        Me.btnGuardarSalir.Name = "btnGuardarSalir"
        Me.btnGuardarSalir.Size = New System.Drawing.Size(130, 40)
        Me.btnGuardarSalir.TabIndex = 13
        Me.btnGuardarSalir.Text = "Guardar y salir"
        Me.btnGuardarSalir.UseVisualStyleBackColor = True
        '
        'NuevoVenta
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(687, 487)
        Me.Controls.Add(Me.btnGuardarSalir)
        Me.Controls.Add(Me.txtCliente)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.cmbMetodoPago)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.lblTotalVenta)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.lblFechaActual)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtNumVenta2)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.GroupBox1)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "NuevoVenta"
        Me.Text = "Venta"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgvDetalleVenta, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnAñadirVen As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents lblSubtotal As Label
    Friend WithEvents txtPrecioUni As TextBox
    Friend WithEvents txtCantidad As TextBox
    Friend WithEvents txtIDdetalle As TextBox
    Friend WithEvents cmbProducto As ComboBox
    Friend WithEvents txtNumVenta As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents txtNumVenta2 As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents lblFechaActual As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents lblTotalVenta As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents txtCliente As TextBox
    Friend WithEvents btnGuardarSalir As Button
    Friend WithEvents cmbMetodoPago As ComboBox
    Friend WithEvents dgvDetalleVenta As DataGridView
End Class
