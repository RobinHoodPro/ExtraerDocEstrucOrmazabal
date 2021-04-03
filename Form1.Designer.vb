<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmExtDocEstOmzb
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.TlpTitulo = New System.Windows.Forms.TableLayoutPanel()
        Me.LbTitulo = New System.Windows.Forms.Label()
        Me.TbArchEstruct = New System.Windows.Forms.TextBox()
        Me.BtArchEstruct = New System.Windows.Forms.Button()
        Me.TbArchModeling = New System.Windows.Forms.TextBox()
        Me.BtArchModeling = New System.Windows.Forms.Button()
        Me.TlpPrincipal = New System.Windows.Forms.TableLayoutPanel()
        Me.TlpEstructura = New System.Windows.Forms.TableLayoutPanel()
        Me.TlpTitleEstruct = New System.Windows.Forms.TableLayoutPanel()
        Me.LbTitleEstruct = New System.Windows.Forms.Label()
        Me.PbBarra = New System.Windows.Forms.PictureBox()
        Me.TlpTitulo.SuspendLayout()
        Me.TlpPrincipal.SuspendLayout()
        Me.TlpEstructura.SuspendLayout()
        Me.TlpTitleEstruct.SuspendLayout()
        CType(Me.PbBarra, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TlpTitulo
        '
        Me.TlpTitulo.AutoSize = True
        Me.TlpTitulo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.TlpTitulo.BackColor = System.Drawing.Color.Gold
        Me.TlpTitulo.ColumnCount = 1
        Me.TlpTitulo.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TlpTitulo.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TlpTitulo.Controls.Add(Me.LbTitulo, 0, 0)
        Me.TlpTitulo.Dock = System.Windows.Forms.DockStyle.Top
        Me.TlpTitulo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TlpTitulo.Location = New System.Drawing.Point(0, 0)
        Me.TlpTitulo.Margin = New System.Windows.Forms.Padding(0)
        Me.TlpTitulo.Name = "TlpTitulo"
        Me.TlpTitulo.RowCount = 1
        Me.TlpTitulo.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TlpTitulo.Size = New System.Drawing.Size(605, 38)
        Me.TlpTitulo.TabIndex = 23
        Me.TlpTitulo.TabStop = True
        '
        'LbTitulo
        '
        Me.LbTitulo.BackColor = System.Drawing.Color.Transparent
        Me.LbTitulo.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LbTitulo.Location = New System.Drawing.Point(0, 0)
        Me.LbTitulo.Margin = New System.Windows.Forms.Padding(0)
        Me.LbTitulo.Name = "LbTitulo"
        Me.LbTitulo.Padding = New System.Windows.Forms.Padding(8, 6, 0, 6)
        Me.LbTitulo.Size = New System.Drawing.Size(585, 38)
        Me.LbTitulo.TabIndex = 0
        Me.LbTitulo.Text = "Extraer Documentacion De Estructura De Ormazabal"
        '
        'TbArchEstruct
        '
        Me.TbArchEstruct.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TbArchEstruct.Enabled = False
        Me.TbArchEstruct.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TbArchEstruct.Location = New System.Drawing.Point(70, 12)
        Me.TbArchEstruct.Margin = New System.Windows.Forms.Padding(10)
        Me.TbArchEstruct.Name = "TbArchEstruct"
        Me.TbArchEstruct.ReadOnly = True
        Me.TbArchEstruct.Size = New System.Drawing.Size(296, 30)
        Me.TbArchEstruct.TabIndex = 3
        Me.TbArchEstruct.TabStop = False
        Me.TbArchEstruct.Text = "Seleccione Archivo Estructura Ormazabal"
        '
        'BtArchEstruct
        '
        Me.BtArchEstruct.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.BtArchEstruct.AutoSize = True
        Me.BtArchEstruct.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BtArchEstruct.BackColor = System.Drawing.Color.YellowGreen
        Me.BtArchEstruct.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtArchEstruct.Location = New System.Drawing.Point(10, 10)
        Me.BtArchEstruct.Margin = New System.Windows.Forms.Padding(10)
        Me.BtArchEstruct.Name = "BtArchEstruct"
        Me.BtArchEstruct.Size = New System.Drawing.Size(40, 35)
        Me.BtArchEstruct.TabIndex = 2
        Me.BtArchEstruct.Text = "..."
        Me.BtArchEstruct.UseVisualStyleBackColor = False
        '
        'TbArchModeling
        '
        Me.TbArchModeling.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TbArchModeling.Enabled = False
        Me.TbArchModeling.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TbArchModeling.Location = New System.Drawing.Point(70, 67)
        Me.TbArchModeling.Margin = New System.Windows.Forms.Padding(10)
        Me.TbArchModeling.Name = "TbArchModeling"
        Me.TbArchModeling.ReadOnly = True
        Me.TbArchModeling.Size = New System.Drawing.Size(286, 30)
        Me.TbArchModeling.TabIndex = 29
        Me.TbArchModeling.TabStop = False
        Me.TbArchModeling.Text = "Seleccione Archivo Estructura Modeling"
        '
        'BtArchModeling
        '
        Me.BtArchModeling.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.BtArchModeling.AutoSize = True
        Me.BtArchModeling.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BtArchModeling.BackColor = System.Drawing.Color.YellowGreen
        Me.BtArchModeling.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtArchModeling.Location = New System.Drawing.Point(10, 65)
        Me.BtArchModeling.Margin = New System.Windows.Forms.Padding(10)
        Me.BtArchModeling.Name = "BtArchModeling"
        Me.BtArchModeling.Size = New System.Drawing.Size(40, 35)
        Me.BtArchModeling.TabIndex = 29
        Me.BtArchModeling.Text = "..."
        Me.BtArchModeling.UseVisualStyleBackColor = False
        '
        'TlpPrincipal
        '
        Me.TlpPrincipal.AutoSize = True
        Me.TlpPrincipal.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.TlpPrincipal.BackColor = System.Drawing.Color.DarkOrange
        Me.TlpPrincipal.ColumnCount = 2
        Me.TlpPrincipal.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TlpPrincipal.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TlpPrincipal.Controls.Add(Me.TlpEstructura, 0, 2)
        Me.TlpPrincipal.Controls.Add(Me.BtArchModeling, 0, 1)
        Me.TlpPrincipal.Controls.Add(Me.TbArchModeling, 1, 1)
        Me.TlpPrincipal.Controls.Add(Me.BtArchEstruct, 0, 0)
        Me.TlpPrincipal.Controls.Add(Me.TbArchEstruct, 1, 0)
        Me.TlpPrincipal.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TlpPrincipal.Location = New System.Drawing.Point(20, 58)
        Me.TlpPrincipal.Margin = New System.Windows.Forms.Padding(20)
        Me.TlpPrincipal.Name = "TlpPrincipal"
        Me.TlpPrincipal.RowCount = 3
        Me.TlpPrincipal.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TlpPrincipal.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TlpPrincipal.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TlpPrincipal.Size = New System.Drawing.Size(565, 609)
        Me.TlpPrincipal.TabIndex = 28
        Me.TlpPrincipal.TabStop = True
        '
        'TlpEstructura
        '
        Me.TlpEstructura.AutoSize = True
        Me.TlpEstructura.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.TlpEstructura.BackColor = System.Drawing.Color.DarkOrange
        Me.TlpEstructura.ColumnCount = 1
        Me.TlpPrincipal.SetColumnSpan(Me.TlpEstructura, 2)
        Me.TlpEstructura.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TlpEstructura.Controls.Add(Me.TlpTitleEstruct, 0, 0)
        Me.TlpEstructura.Controls.Add(Me.PbBarra, 0, 1)
        Me.TlpEstructura.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TlpEstructura.Location = New System.Drawing.Point(10, 120)
        Me.TlpEstructura.Margin = New System.Windows.Forms.Padding(10)
        Me.TlpEstructura.Name = "TlpEstructura"
        Me.TlpEstructura.RowCount = 2
        Me.TlpEstructura.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TlpEstructura.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TlpEstructura.Size = New System.Drawing.Size(545, 479)
        Me.TlpEstructura.TabIndex = 29
        Me.TlpEstructura.TabStop = True
        '
        'TlpTitleEstruct
        '
        Me.TlpTitleEstruct.AutoSize = True
        Me.TlpTitleEstruct.BackColor = System.Drawing.Color.Gold
        Me.TlpTitleEstruct.ColumnCount = 1
        Me.TlpTitleEstruct.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TlpTitleEstruct.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TlpTitleEstruct.Controls.Add(Me.LbTitleEstruct, 0, 0)
        Me.TlpTitleEstruct.Dock = System.Windows.Forms.DockStyle.Top
        Me.TlpTitleEstruct.Location = New System.Drawing.Point(5, 5)
        Me.TlpTitleEstruct.Margin = New System.Windows.Forms.Padding(5, 5, 5, 3)
        Me.TlpTitleEstruct.Name = "TlpTitleEstruct"
        Me.TlpTitleEstruct.RowCount = 1
        Me.TlpTitleEstruct.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TlpTitleEstruct.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41.0!))
        Me.TlpTitleEstruct.Size = New System.Drawing.Size(535, 41)
        Me.TlpTitleEstruct.TabIndex = 30
        '
        'LbTitleEstruct
        '
        Me.LbTitleEstruct.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.LbTitleEstruct.AutoSize = True
        Me.LbTitleEstruct.BackColor = System.Drawing.Color.Transparent
        Me.LbTitleEstruct.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LbTitleEstruct.Location = New System.Drawing.Point(117, 0)
        Me.LbTitleEstruct.Margin = New System.Windows.Forms.Padding(0)
        Me.LbTitleEstruct.Name = "LbTitleEstruct"
        Me.LbTitleEstruct.Padding = New System.Windows.Forms.Padding(8, 6, 0, 6)
        Me.LbTitleEstruct.Size = New System.Drawing.Size(301, 41)
        Me.LbTitleEstruct.TabIndex = 1
        Me.LbTitleEstruct.Text = "Estructura: ________-__"
        '
        'PbBarra
        '
        Me.PbBarra.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.PbBarra.BackColor = System.Drawing.Color.Transparent
        Me.PbBarra.Location = New System.Drawing.Point(10, 59)
        Me.PbBarra.Margin = New System.Windows.Forms.Padding(10)
        Me.PbBarra.Name = "PbBarra"
        Me.PbBarra.Size = New System.Drawing.Size(525, 410)
        Me.PbBarra.TabIndex = 30
        Me.PbBarra.TabStop = False
        '
        'FrmExtDocEstOmzb
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(13.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.LightGreen
        Me.ClientSize = New System.Drawing.Size(605, 692)
        Me.Controls.Add(Me.TlpTitulo)
        Me.Controls.Add(Me.TlpPrincipal)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "FrmExtDocEstOmzb"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        Me.TlpTitulo.ResumeLayout(False)
        Me.TlpPrincipal.ResumeLayout(False)
        Me.TlpPrincipal.PerformLayout()
        Me.TlpEstructura.ResumeLayout(False)
        Me.TlpEstructura.PerformLayout()
        Me.TlpTitleEstruct.ResumeLayout(False)
        Me.TlpTitleEstruct.PerformLayout()
        CType(Me.PbBarra, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TlpTitulo As TableLayoutPanel
    Friend WithEvents LbTitulo As Label
    Friend WithEvents TbArchEstruct As TextBox
    Friend WithEvents BtArchEstruct As Button
    Friend WithEvents TbArchModeling As TextBox
    Friend WithEvents BtArchModeling As Button
    Friend WithEvents TlpPrincipal As TableLayoutPanel
    Friend WithEvents TlpEstructura As TableLayoutPanel
    Friend WithEvents TlpTitleEstruct As TableLayoutPanel
    Friend WithEvents LbTitleEstruct As Label
    Friend WithEvents PbBarra As PictureBox
End Class
