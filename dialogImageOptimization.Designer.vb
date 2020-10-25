<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dialogImageOptimization
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmb_InterpolationMode = New System.Windows.Forms.ComboBox()
        Me.cmb_SmoothingMode = New System.Windows.Forms.ComboBox()
        Me.cmb_ImageResizeSizePercent = New System.Windows.Forms.ComboBox()
        Me.cmb_CompositingQuality = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmb_PageResizeSizePercent = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.chkAllowTransparent = New System.Windows.Forms.CheckBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.chkAutoResize = New System.Windows.Forms.CheckBox()
        Me.chkOptimizePngMasks = New System.Windows.Forms.CheckBox()
        Me.lblResults = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 83.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(10, 365)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(326, 36)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(250, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 30)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "ACCEPT"
        Me.OK_Button.Visible = False
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(173, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(66, 30)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "CANCEL"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Enabled = False
        Me.ProgressBar1.Location = New System.Drawing.Point(10, 368)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(166, 30)
        Me.ProgressBar1.Step = 1
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.ProgressBar1.TabIndex = 8
        Me.ProgressBar1.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(40, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Interpolation Mode:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(48, 81)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(90, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Smoothing Mode:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(119, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Image Re-Size Percent:"
        '
        'cmb_InterpolationMode
        '
        Me.cmb_InterpolationMode.FormattingEnabled = True
        Me.cmb_InterpolationMode.Items.AddRange(New Object() {"Default", "Low", "High", "Bilinear", "BiCubic", "Nearest Neighbor", "High Quality Bilinear", "High Quality Bicubic"})
        Me.cmb_InterpolationMode.Location = New System.Drawing.Point(144, 48)
        Me.cmb_InterpolationMode.Name = "cmb_InterpolationMode"
        Me.cmb_InterpolationMode.Size = New System.Drawing.Size(192, 21)
        Me.cmb_InterpolationMode.TabIndex = 2
        '
        'cmb_SmoothingMode
        '
        Me.cmb_SmoothingMode.FormattingEnabled = True
        Me.cmb_SmoothingMode.Items.AddRange(New Object() {"Default", "High Speed", "High Quality", "None", "Anti-Alias"})
        Me.cmb_SmoothingMode.Location = New System.Drawing.Point(144, 81)
        Me.cmb_SmoothingMode.Name = "cmb_SmoothingMode"
        Me.cmb_SmoothingMode.Size = New System.Drawing.Size(192, 21)
        Me.cmb_SmoothingMode.TabIndex = 3
        '
        'cmb_ImageResizeSizePercent
        '
        Me.cmb_ImageResizeSizePercent.FormattingEnabled = True
        Me.cmb_ImageResizeSizePercent.Location = New System.Drawing.Point(144, 16)
        Me.cmb_ImageResizeSizePercent.Name = "cmb_ImageResizeSizePercent"
        Me.cmb_ImageResizeSizePercent.Size = New System.Drawing.Size(192, 21)
        Me.cmb_ImageResizeSizePercent.TabIndex = 1
        '
        'cmb_CompositingQuality
        '
        Me.cmb_CompositingQuality.FormattingEnabled = True
        Me.cmb_CompositingQuality.Items.AddRange(New Object() {"Default", "High Speed", "High Quality", "Gamma Corrected", "Assumed Linear"})
        Me.cmb_CompositingQuality.Location = New System.Drawing.Point(144, 115)
        Me.cmb_CompositingQuality.Name = "cmb_CompositingQuality"
        Me.cmb_CompositingQuality.Size = New System.Drawing.Size(192, 21)
        Me.cmb_CompositingQuality.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(32, 115)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(102, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Compositing Quality:"
        '
        'cmb_PageResizeSizePercent
        '
        Me.cmb_PageResizeSizePercent.FormattingEnabled = True
        Me.cmb_PageResizeSizePercent.Location = New System.Drawing.Point(144, 147)
        Me.cmb_PageResizeSizePercent.Name = "cmb_PageResizeSizePercent"
        Me.cmb_PageResizeSizePercent.Size = New System.Drawing.Size(192, 21)
        Me.cmb_PageResizeSizePercent.TabIndex = 9
        Me.cmb_PageResizeSizePercent.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(64, 147)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Page Scale:"
        Me.Label5.Visible = False
        '
        'chkAllowTransparent
        '
        Me.chkAllowTransparent.AutoSize = True
        Me.chkAllowTransparent.Location = New System.Drawing.Point(12, 184)
        Me.chkAllowTransparent.Name = "chkAllowTransparent"
        Me.chkAllowTransparent.Size = New System.Drawing.Size(111, 17)
        Me.chkAllowTransparent.TabIndex = 11
        Me.chkAllowTransparent.Text = "Allow Transparent"
        Me.chkAllowTransparent.UseVisualStyleBackColor = True
        Me.chkAllowTransparent.Visible = False
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.Blue
        Me.Button1.Location = New System.Drawing.Point(10, 239)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(329, 54)
        Me.Button1.TabIndex = 12
        Me.Button1.Text = "RUN IMAGE ANALYSIS"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'chkAutoResize
        '
        Me.chkAutoResize.AutoSize = True
        Me.chkAutoResize.Location = New System.Drawing.Point(144, 184)
        Me.chkAutoResize.Name = "chkAutoResize"
        Me.chkAutoResize.Size = New System.Drawing.Size(180, 17)
        Me.chkAutoResize.TabIndex = 13
        Me.chkAutoResize.Text = "Resize images from on-page size"
        Me.chkAutoResize.UseVisualStyleBackColor = True
        '
        'chkOptimizePngMasks
        '
        Me.chkOptimizePngMasks.AutoSize = True
        Me.chkOptimizePngMasks.Location = New System.Drawing.Point(144, 216)
        Me.chkOptimizePngMasks.Name = "chkOptimizePngMasks"
        Me.chkOptimizePngMasks.Size = New System.Drawing.Size(153, 17)
        Me.chkOptimizePngMasks.TabIndex = 14
        Me.chkOptimizePngMasks.Text = "Optmize transparent masks"
        Me.chkOptimizePngMasks.UseVisualStyleBackColor = True
        '
        'lblResults
        '
        Me.lblResults.AutoSize = True
        Me.lblResults.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblResults.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblResults.Location = New System.Drawing.Point(14, 297)
        Me.lblResults.Name = "lblResults"
        Me.lblResults.Size = New System.Drawing.Size(32, 17)
        Me.lblResults.TabIndex = 15
        Me.lblResults.Text = "      "
        '
        'dialogImageOptimization
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(347, 413)
        Me.Controls.Add(Me.lblResults)
        Me.Controls.Add(Me.chkOptimizePngMasks)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.chkAutoResize)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.chkAllowTransparent)
        Me.Controls.Add(Me.cmb_PageResizeSizePercent)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cmb_CompositingQuality)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmb_ImageResizeSizePercent)
        Me.Controls.Add(Me.cmb_SmoothingMode)
        Me.Controls.Add(Me.cmb_InterpolationMode)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dialogImageOptimization"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "PDF Image Optimization"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmb_InterpolationMode As System.Windows.Forms.ComboBox
    Friend WithEvents cmb_SmoothingMode As System.Windows.Forms.ComboBox
    Friend WithEvents cmb_ImageResizeSizePercent As System.Windows.Forms.ComboBox
    Friend WithEvents cmb_CompositingQuality As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents cmb_PageResizeSizePercent As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents chkAllowTransparent As System.Windows.Forms.CheckBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents chkAutoResize As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptimizePngMasks As System.Windows.Forms.CheckBox
    Friend WithEvents lblResults As System.Windows.Forms.Label

End Class
