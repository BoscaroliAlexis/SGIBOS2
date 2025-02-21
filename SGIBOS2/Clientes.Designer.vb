<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Clientes
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
        Me.components = New System.ComponentModel.Container()
        Me.txtBuscarCliente = New System.Windows.Forms.TextBox()
        Me.btnBuscarCliente = New System.Windows.Forms.Button()
        Me.dgvClientes = New System.Windows.Forms.DataGridView()
        Me.btnGenerarReporteCli = New System.Windows.Forms.Button()
        Me.btnAñadirCli = New System.Windows.Forms.Button()
        Me.cmsActElim = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ActualizarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EliminarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.dgvClientes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsActElim.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtBuscarCliente
        '
        Me.txtBuscarCliente.Location = New System.Drawing.Point(36, 46)
        Me.txtBuscarCliente.Multiline = True
        Me.txtBuscarCliente.Name = "txtBuscarCliente"
        Me.txtBuscarCliente.Size = New System.Drawing.Size(487, 48)
        Me.txtBuscarCliente.TabIndex = 0
        '
        'btnBuscarCliente
        '
        Me.btnBuscarCliente.Location = New System.Drawing.Point(549, 46)
        Me.btnBuscarCliente.Name = "btnBuscarCliente"
        Me.btnBuscarCliente.Size = New System.Drawing.Size(92, 48)
        Me.btnBuscarCliente.TabIndex = 1
        Me.btnBuscarCliente.Text = "Buscar"
        Me.btnBuscarCliente.UseVisualStyleBackColor = True
        '
        'dgvClientes
        '
        Me.dgvClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvClientes.Location = New System.Drawing.Point(36, 134)
        Me.dgvClientes.Name = "dgvClientes"
        Me.dgvClientes.RowHeadersWidth = 62
        Me.dgvClientes.RowTemplate.Height = 28
        Me.dgvClientes.Size = New System.Drawing.Size(718, 296)
        Me.dgvClientes.TabIndex = 2
        '
        'btnGenerarReporteCli
        '
        Me.btnGenerarReporteCli.Location = New System.Drawing.Point(280, 448)
        Me.btnGenerarReporteCli.Name = "btnGenerarReporteCli"
        Me.btnGenerarReporteCli.Size = New System.Drawing.Size(207, 46)
        Me.btnGenerarReporteCli.TabIndex = 3
        Me.btnGenerarReporteCli.Text = "Generar reporte"
        Me.btnGenerarReporteCli.UseVisualStyleBackColor = True
        '
        'btnAñadirCli
        '
        Me.btnAñadirCli.Location = New System.Drawing.Point(662, 46)
        Me.btnAñadirCli.Name = "btnAñadirCli"
        Me.btnAñadirCli.Size = New System.Drawing.Size(92, 48)
        Me.btnAñadirCli.TabIndex = 4
        Me.btnAñadirCli.Text = "Añadir"
        Me.btnAñadirCli.UseVisualStyleBackColor = True
        '
        'cmsActElim
        '
        Me.cmsActElim.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.cmsActElim.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ActualizarToolStripMenuItem, Me.EliminarToolStripMenuItem})
        Me.cmsActElim.Name = "cmsActElim"
        Me.cmsActElim.Size = New System.Drawing.Size(241, 101)
        '
        'ActualizarToolStripMenuItem
        '
        Me.ActualizarToolStripMenuItem.Name = "ActualizarToolStripMenuItem"
        Me.ActualizarToolStripMenuItem.Size = New System.Drawing.Size(240, 32)
        Me.ActualizarToolStripMenuItem.Text = "Actualizar"
        '
        'EliminarToolStripMenuItem
        '
        Me.EliminarToolStripMenuItem.Name = "EliminarToolStripMenuItem"
        Me.EliminarToolStripMenuItem.Size = New System.Drawing.Size(240, 32)
        Me.EliminarToolStripMenuItem.Text = "Eliminar"
        '
        'Clientes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 516)
        Me.Controls.Add(Me.btnAñadirCli)
        Me.Controls.Add(Me.btnGenerarReporteCli)
        Me.Controls.Add(Me.dgvClientes)
        Me.Controls.Add(Me.btnBuscarCliente)
        Me.Controls.Add(Me.txtBuscarCliente)
        Me.Name = "Clientes"
        Me.Text = "Clientes"
        CType(Me.dgvClientes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsActElim.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtBuscarCliente As TextBox
    Friend WithEvents btnBuscarCliente As Button
    Friend WithEvents dgvClientes As DataGridView
    Friend WithEvents btnGenerarReporteCli As Button
    Friend WithEvents btnAñadirCli As Button
    Friend WithEvents cmsActElim As ContextMenuStrip
    Friend WithEvents ActualizarToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EliminarToolStripMenuItem As ToolStripMenuItem
End Class
