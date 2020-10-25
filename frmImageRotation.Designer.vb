<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmImageRotation
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ImageRotation_ImageRotation = New System.Windows.Forms.ComboBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.CropImageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PasteImageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImageRotation_btnResizeImage = New System.Windows.Forms.Button()
        Me.CopyImageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoadImageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImageRotation_txtResizeHeight = New System.Windows.Forms.TextBox()
        Me.ImageRotation_txtResizeWidth = New System.Windows.Forms.TextBox()
        Me.SaveImageToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveImageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ImageRotation_btnResizeRevert = New System.Windows.Forms.Button()
        Me.ImageRotation_ImageFlip = New System.Windows.Forms.ComboBox()
        Me.ImageRotation_Dimensions_Resized = New System.Windows.Forms.Label()
        Me.ImageRotation_chkResizeProportional = New System.Windows.Forms.CheckBox()
        Me.lblResizeHeight = New System.Windows.Forms.Label()
        Me.lblResizeWidth = New System.Windows.Forms.Label()
        Me.ImageRotation_Dimensions_Original = New System.Windows.Forms.Label()
        Me.ImageRotation_btnOK = New System.Windows.Forms.Button()
        Me.ImageRotation_btnCancel = New System.Windows.Forms.Button()
        Me.ImageRotation_CheckboxResize = New System.Windows.Forms.CheckBox()
        Me.ImageRotation_PictureBox = New System.Windows.Forms.PictureBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.ImageRotation_PictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 13)
        Me.Label2.TabIndex = 33
        Me.Label2.Text = "Image Flip:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 13)
        Me.Label1.TabIndex = 32
        Me.Label1.Text = "Image Rotation:"
        '
        'ImageRotation_ImageRotation
        '
        Me.ImageRotation_ImageRotation.FormattingEnabled = True
        Me.ImageRotation_ImageRotation.Items.AddRange(New Object() {"None", "90 degrees clockwise", "180 degrees clockwise", "270 degrees clockwise", "360 degrees clockwise"})
        Me.ImageRotation_ImageRotation.Location = New System.Drawing.Point(10, 17)
        Me.ImageRotation_ImageRotation.Name = "ImageRotation_ImageRotation"
        Me.ImageRotation_ImageRotation.Size = New System.Drawing.Size(172, 21)
        Me.ImageRotation_ImageRotation.TabIndex = 30
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(7, 273)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(237, 31)
        Me.Button2.TabIndex = 29
        Me.Button2.Text = "DELETE IMAGE"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(7, 230)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(237, 37)
        Me.Button1.TabIndex = 28
        Me.Button1.Text = "Optimize Image"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'CropImageToolStripMenuItem
        '
        Me.CropImageToolStripMenuItem.Name = "CropImageToolStripMenuItem"
        Me.CropImageToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.CropImageToolStripMenuItem.Text = "Crop Image"
        '
        'PasteImageToolStripMenuItem
        '
        Me.PasteImageToolStripMenuItem.Name = "PasteImageToolStripMenuItem"
        Me.PasteImageToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.PasteImageToolStripMenuItem.Text = "Paste Image"
        '
        'ImageRotation_btnResizeImage
        '
        Me.ImageRotation_btnResizeImage.Location = New System.Drawing.Point(8, 64)
        Me.ImageRotation_btnResizeImage.Name = "ImageRotation_btnResizeImage"
        Me.ImageRotation_btnResizeImage.Size = New System.Drawing.Size(144, 39)
        Me.ImageRotation_btnResizeImage.TabIndex = 19
        Me.ImageRotation_btnResizeImage.Text = "Resize Image"
        Me.ImageRotation_btnResizeImage.UseVisualStyleBackColor = True
        '
        'CopyImageToolStripMenuItem
        '
        Me.CopyImageToolStripMenuItem.Name = "CopyImageToolStripMenuItem"
        Me.CopyImageToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.CopyImageToolStripMenuItem.Text = "Copy Image"
        '
        'LoadImageToolStripMenuItem
        '
        Me.LoadImageToolStripMenuItem.Name = "LoadImageToolStripMenuItem"
        Me.LoadImageToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.LoadImageToolStripMenuItem.Text = "Load Image"
        '
        'ImageRotation_txtResizeHeight
        '
        Me.ImageRotation_txtResizeHeight.Location = New System.Drawing.Point(149, 41)
        Me.ImageRotation_txtResizeHeight.Name = "ImageRotation_txtResizeHeight"
        Me.ImageRotation_txtResizeHeight.Size = New System.Drawing.Size(40, 20)
        Me.ImageRotation_txtResizeHeight.TabIndex = 23
        '
        'ImageRotation_txtResizeWidth
        '
        Me.ImageRotation_txtResizeWidth.Location = New System.Drawing.Point(44, 40)
        Me.ImageRotation_txtResizeWidth.Name = "ImageRotation_txtResizeWidth"
        Me.ImageRotation_txtResizeWidth.Size = New System.Drawing.Size(38, 20)
        Me.ImageRotation_txtResizeWidth.TabIndex = 22
        '
        'SaveImageToolStripMenuItem1
        '
        Me.SaveImageToolStripMenuItem1.Name = "SaveImageToolStripMenuItem1"
        Me.SaveImageToolStripMenuItem1.Size = New System.Drawing.Size(180, 22)
        Me.SaveImageToolStripMenuItem1.Text = "Save Image"
        '
        'SaveImageToolStripMenuItem
        '
        Me.SaveImageToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveImageToolStripMenuItem1, Me.LoadImageToolStripMenuItem, Me.CopyImageToolStripMenuItem, Me.PasteImageToolStripMenuItem, Me.CropImageToolStripMenuItem})
        Me.SaveImageToolStripMenuItem.Name = "SaveImageToolStripMenuItem"
        Me.SaveImageToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.SaveImageToolStripMenuItem.Text = "File"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveImageToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(695, 24)
        Me.MenuStrip1.TabIndex = 36
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ImageRotation_btnResizeRevert
        '
        Me.ImageRotation_btnResizeRevert.Location = New System.Drawing.Point(152, 64)
        Me.ImageRotation_btnResizeRevert.Name = "ImageRotation_btnResizeRevert"
        Me.ImageRotation_btnResizeRevert.Size = New System.Drawing.Size(64, 39)
        Me.ImageRotation_btnResizeRevert.TabIndex = 25
        Me.ImageRotation_btnResizeRevert.Text = "Revert"
        Me.ImageRotation_btnResizeRevert.UseVisualStyleBackColor = True
        '
        'ImageRotation_ImageFlip
        '
        Me.ImageRotation_ImageFlip.FormattingEnabled = True
        Me.ImageRotation_ImageFlip.Items.AddRange(New Object() {"None", "Vertical", "Horizontal", "Both"})
        Me.ImageRotation_ImageFlip.Location = New System.Drawing.Point(10, 57)
        Me.ImageRotation_ImageFlip.Name = "ImageRotation_ImageFlip"
        Me.ImageRotation_ImageFlip.Size = New System.Drawing.Size(172, 21)
        Me.ImageRotation_ImageFlip.TabIndex = 31
        '
        'ImageRotation_Dimensions_Resized
        '
        Me.ImageRotation_Dimensions_Resized.AutoSize = True
        Me.ImageRotation_Dimensions_Resized.Location = New System.Drawing.Point(11, 108)
        Me.ImageRotation_Dimensions_Resized.Name = "ImageRotation_Dimensions_Resized"
        Me.ImageRotation_Dimensions_Resized.Size = New System.Drawing.Size(173, 13)
        Me.ImageRotation_Dimensions_Resized.TabIndex = 18
        Me.ImageRotation_Dimensions_Resized.Text = "Resized Dimensions: w: {0} x h: {1}"
        Me.ImageRotation_Dimensions_Resized.Visible = False
        '
        'ImageRotation_chkResizeProportional
        '
        Me.ImageRotation_chkResizeProportional.AutoSize = True
        Me.ImageRotation_chkResizeProportional.Checked = True
        Me.ImageRotation_chkResizeProportional.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ImageRotation_chkResizeProportional.Location = New System.Drawing.Point(18, 20)
        Me.ImageRotation_chkResizeProportional.Name = "ImageRotation_chkResizeProportional"
        Me.ImageRotation_chkResizeProportional.Size = New System.Drawing.Size(122, 17)
        Me.ImageRotation_chkResizeProportional.TabIndex = 24
        Me.ImageRotation_chkResizeProportional.Text = "Maintain Proportions"
        Me.ImageRotation_chkResizeProportional.UseVisualStyleBackColor = True
        '
        'lblResizeHeight
        '
        Me.lblResizeHeight.AutoSize = True
        Me.lblResizeHeight.Location = New System.Drawing.Point(88, 44)
        Me.lblResizeHeight.Name = "lblResizeHeight"
        Me.lblResizeHeight.Size = New System.Drawing.Size(55, 13)
        Me.lblResizeHeight.TabIndex = 21
        Me.lblResizeHeight.Text = "x   Height:"
        '
        'lblResizeWidth
        '
        Me.lblResizeWidth.AutoSize = True
        Me.lblResizeWidth.Location = New System.Drawing.Point(8, 44)
        Me.lblResizeWidth.Name = "lblResizeWidth"
        Me.lblResizeWidth.Size = New System.Drawing.Size(38, 13)
        Me.lblResizeWidth.TabIndex = 20
        Me.lblResizeWidth.Text = "Width:"
        '
        'ImageRotation_Dimensions_Original
        '
        Me.ImageRotation_Dimensions_Original.AutoSize = True
        Me.ImageRotation_Dimensions_Original.Location = New System.Drawing.Point(7, 81)
        Me.ImageRotation_Dimensions_Original.Name = "ImageRotation_Dimensions_Original"
        Me.ImageRotation_Dimensions_Original.Size = New System.Drawing.Size(170, 13)
        Me.ImageRotation_Dimensions_Original.TabIndex = 17
        Me.ImageRotation_Dimensions_Original.Text = "Original Dimensions: w: {0} x h: {1}"
        '
        'ImageRotation_btnOK
        '
        Me.ImageRotation_btnOK.Location = New System.Drawing.Point(7, 345)
        Me.ImageRotation_btnOK.Name = "ImageRotation_btnOK"
        Me.ImageRotation_btnOK.Size = New System.Drawing.Size(237, 31)
        Me.ImageRotation_btnOK.TabIndex = 5
        Me.ImageRotation_btnOK.Text = "OK"
        Me.ImageRotation_btnOK.UseVisualStyleBackColor = True
        '
        'ImageRotation_btnCancel
        '
        Me.ImageRotation_btnCancel.Location = New System.Drawing.Point(7, 310)
        Me.ImageRotation_btnCancel.Name = "ImageRotation_btnCancel"
        Me.ImageRotation_btnCancel.Size = New System.Drawing.Size(237, 32)
        Me.ImageRotation_btnCancel.TabIndex = 6
        Me.ImageRotation_btnCancel.Text = "CANCEL"
        Me.ImageRotation_btnCancel.UseVisualStyleBackColor = True
        '
        'ImageRotation_CheckboxResize
        '
        Me.ImageRotation_CheckboxResize.AutoSize = True
        Me.ImageRotation_CheckboxResize.Location = New System.Drawing.Point(133, 319)
        Me.ImageRotation_CheckboxResize.Name = "ImageRotation_CheckboxResize"
        Me.ImageRotation_CheckboxResize.Size = New System.Drawing.Size(90, 17)
        Me.ImageRotation_CheckboxResize.TabIndex = 16
        Me.ImageRotation_CheckboxResize.Text = "Resize Image"
        Me.ImageRotation_CheckboxResize.UseVisualStyleBackColor = True
        Me.ImageRotation_CheckboxResize.Visible = False
        '
        'ImageRotation_PictureBox
        '
        Me.ImageRotation_PictureBox.BackColor = System.Drawing.Color.Transparent
        Me.ImageRotation_PictureBox.Location = New System.Drawing.Point(0, 20)
        Me.ImageRotation_PictureBox.Name = "ImageRotation_PictureBox"
        Me.ImageRotation_PictureBox.Size = New System.Drawing.Size(440, 403)
        Me.ImageRotation_PictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ImageRotation_PictureBox.TabIndex = 35
        Me.ImageRotation_PictureBox.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ImageRotation_txtResizeHeight)
        Me.GroupBox1.Controls.Add(Me.ImageRotation_txtResizeWidth)
        Me.GroupBox1.Controls.Add(Me.ImageRotation_btnResizeImage)
        Me.GroupBox1.Controls.Add(Me.ImageRotation_btnResizeRevert)
        Me.GroupBox1.Controls.Add(Me.ImageRotation_Dimensions_Resized)
        Me.GroupBox1.Controls.Add(Me.ImageRotation_chkResizeProportional)
        Me.GroupBox1.Controls.Add(Me.lblResizeHeight)
        Me.GroupBox1.Controls.Add(Me.lblResizeWidth)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 97)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(237, 127)
        Me.GroupBox1.TabIndex = 26
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Resize Image"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.ImageRotation_ImageFlip)
        Me.Panel1.Controls.Add(Me.ImageRotation_ImageRotation)
        Me.Panel1.Controls.Add(Me.Button2)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.ImageRotation_Dimensions_Original)
        Me.Panel1.Controls.Add(Me.ImageRotation_btnOK)
        Me.Panel1.Controls.Add(Me.ImageRotation_btnCancel)
        Me.Panel1.Controls.Add(Me.ImageRotation_CheckboxResize)
        Me.Panel1.Location = New System.Drawing.Point(439, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(255, 403)
        Me.Panel1.TabIndex = 37
        '
        'frmImageRotation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(695, 416)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.ImageRotation_PictureBox)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmImageRotation"
        Me.Text = "Select Image Rotation:"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.ImageRotation_PictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Public WithEvents ImageRotation_ImageRotation As ComboBox
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents CropImageToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PasteImageToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ImageRotation_btnResizeImage As Button
    Friend WithEvents CopyImageToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LoadImageToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ImageRotation_txtResizeHeight As TextBox
    Friend WithEvents ImageRotation_txtResizeWidth As TextBox
    Friend WithEvents SaveImageToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents SaveImageToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ImageRotation_btnResizeRevert As Button
    Public WithEvents ImageRotation_ImageFlip As ComboBox
    Friend WithEvents ImageRotation_Dimensions_Resized As Label
    Friend WithEvents ImageRotation_chkResizeProportional As CheckBox
    Friend WithEvents lblResizeHeight As Label
    Friend WithEvents lblResizeWidth As Label
    Friend WithEvents ImageRotation_Dimensions_Original As Label
    Friend WithEvents ImageRotation_btnOK As Button
    Friend WithEvents ImageRotation_btnCancel As Button
    Public WithEvents ImageRotation_CheckboxResize As CheckBox
    Public WithEvents ImageRotation_PictureBox As PictureBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Panel1 As Panel
End Class
