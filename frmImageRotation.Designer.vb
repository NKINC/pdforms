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
        Me.ImageRotation_PictureBox = New System.Windows.Forms.PictureBox()
        Me.ImageRotation_btnOK = New System.Windows.Forms.Button()
        Me.ImageRotation_btnCancel = New System.Windows.Forms.Button()
        Me.ImageRotation_Dimensions_Original = New System.Windows.Forms.Label()
        Me.ImageRotation_CheckboxResize = New System.Windows.Forms.CheckBox()
        Me.ImageRotation_Dimensions_Resized = New System.Windows.Forms.Label()
        Me.ImageRotation_btnResizeImage = New System.Windows.Forms.Button()
        Me.lblResizeWidth = New System.Windows.Forms.Label()
        Me.lblResizeHeight = New System.Windows.Forms.Label()
        Me.ImageRotation_txtResizeWidth = New System.Windows.Forms.TextBox()
        Me.ImageRotation_txtResizeHeight = New System.Windows.Forms.TextBox()
        Me.ImageRotation_chkResizeProportional = New System.Windows.Forms.CheckBox()
        Me.ImageRotation_btnResizeRevert = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.SaveImageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveImageToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoadImageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyImageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ImageRotation_ImageFlip = New System.Windows.Forms.ComboBox()
        Me.ImageRotation_ImageRotation = New System.Windows.Forms.ComboBox()
        CType(Me.ImageRotation_PictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ImageRotation_PictureBox
        '
        Me.ImageRotation_PictureBox.BackColor = System.Drawing.Color.Transparent
        Me.ImageRotation_PictureBox.Location = New System.Drawing.Point(8, 27)
        Me.ImageRotation_PictureBox.Name = "ImageRotation_PictureBox"
        Me.ImageRotation_PictureBox.Size = New System.Drawing.Size(425, 380)
        Me.ImageRotation_PictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ImageRotation_PictureBox.TabIndex = 0
        Me.ImageRotation_PictureBox.TabStop = False
        '
        'ImageRotation_btnOK
        '
        Me.ImageRotation_btnOK.Location = New System.Drawing.Point(446, 376)
        Me.ImageRotation_btnOK.Name = "ImageRotation_btnOK"
        Me.ImageRotation_btnOK.Size = New System.Drawing.Size(237, 31)
        Me.ImageRotation_btnOK.TabIndex = 5
        Me.ImageRotation_btnOK.Text = "OK"
        Me.ImageRotation_btnOK.UseVisualStyleBackColor = True
        '
        'ImageRotation_btnCancel
        '
        Me.ImageRotation_btnCancel.Location = New System.Drawing.Point(446, 341)
        Me.ImageRotation_btnCancel.Name = "ImageRotation_btnCancel"
        Me.ImageRotation_btnCancel.Size = New System.Drawing.Size(237, 32)
        Me.ImageRotation_btnCancel.TabIndex = 6
        Me.ImageRotation_btnCancel.Text = "CANCEL"
        Me.ImageRotation_btnCancel.UseVisualStyleBackColor = True
        '
        'ImageRotation_Dimensions_Original
        '
        Me.ImageRotation_Dimensions_Original.AutoSize = True
        Me.ImageRotation_Dimensions_Original.Location = New System.Drawing.Point(446, 112)
        Me.ImageRotation_Dimensions_Original.Name = "ImageRotation_Dimensions_Original"
        Me.ImageRotation_Dimensions_Original.Size = New System.Drawing.Size(170, 13)
        Me.ImageRotation_Dimensions_Original.TabIndex = 17
        Me.ImageRotation_Dimensions_Original.Text = "Original Dimensions: w: {0} x h: {1}"
        '
        'ImageRotation_CheckboxResize
        '
        Me.ImageRotation_CheckboxResize.AutoSize = True
        Me.ImageRotation_CheckboxResize.Location = New System.Drawing.Point(572, 350)
        Me.ImageRotation_CheckboxResize.Name = "ImageRotation_CheckboxResize"
        Me.ImageRotation_CheckboxResize.Size = New System.Drawing.Size(90, 17)
        Me.ImageRotation_CheckboxResize.TabIndex = 16
        Me.ImageRotation_CheckboxResize.Text = "Resize Image"
        Me.ImageRotation_CheckboxResize.UseVisualStyleBackColor = True
        Me.ImageRotation_CheckboxResize.Visible = False
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
        'ImageRotation_btnResizeImage
        '
        Me.ImageRotation_btnResizeImage.Location = New System.Drawing.Point(8, 64)
        Me.ImageRotation_btnResizeImage.Name = "ImageRotation_btnResizeImage"
        Me.ImageRotation_btnResizeImage.Size = New System.Drawing.Size(144, 39)
        Me.ImageRotation_btnResizeImage.TabIndex = 19
        Me.ImageRotation_btnResizeImage.Text = "Resize Image"
        Me.ImageRotation_btnResizeImage.UseVisualStyleBackColor = True
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
        'lblResizeHeight
        '
        Me.lblResizeHeight.AutoSize = True
        Me.lblResizeHeight.Location = New System.Drawing.Point(88, 44)
        Me.lblResizeHeight.Name = "lblResizeHeight"
        Me.lblResizeHeight.Size = New System.Drawing.Size(55, 13)
        Me.lblResizeHeight.TabIndex = 21
        Me.lblResizeHeight.Text = "x   Height:"
        '
        'ImageRotation_txtResizeWidth
        '
        Me.ImageRotation_txtResizeWidth.Location = New System.Drawing.Point(44, 40)
        Me.ImageRotation_txtResizeWidth.Name = "ImageRotation_txtResizeWidth"
        Me.ImageRotation_txtResizeWidth.Size = New System.Drawing.Size(38, 20)
        Me.ImageRotation_txtResizeWidth.TabIndex = 22
        '
        'ImageRotation_txtResizeHeight
        '
        Me.ImageRotation_txtResizeHeight.Location = New System.Drawing.Point(149, 41)
        Me.ImageRotation_txtResizeHeight.Name = "ImageRotation_txtResizeHeight"
        Me.ImageRotation_txtResizeHeight.Size = New System.Drawing.Size(40, 20)
        Me.ImageRotation_txtResizeHeight.TabIndex = 23
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
        'ImageRotation_btnResizeRevert
        '
        Me.ImageRotation_btnResizeRevert.Location = New System.Drawing.Point(152, 64)
        Me.ImageRotation_btnResizeRevert.Name = "ImageRotation_btnResizeRevert"
        Me.ImageRotation_btnResizeRevert.Size = New System.Drawing.Size(64, 39)
        Me.ImageRotation_btnResizeRevert.TabIndex = 25
        Me.ImageRotation_btnResizeRevert.Text = "Revert"
        Me.ImageRotation_btnResizeRevert.UseVisualStyleBackColor = True
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
        Me.GroupBox1.Location = New System.Drawing.Point(446, 128)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(237, 127)
        Me.GroupBox1.TabIndex = 26
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Resize Image"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveImageToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(695, 24)
        Me.MenuStrip1.TabIndex = 27
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'SaveImageToolStripMenuItem
        '
        Me.SaveImageToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveImageToolStripMenuItem1, Me.LoadImageToolStripMenuItem, Me.CopyImageToolStripMenuItem})
        Me.SaveImageToolStripMenuItem.Name = "SaveImageToolStripMenuItem"
        Me.SaveImageToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.SaveImageToolStripMenuItem.Text = "File"
        '
        'SaveImageToolStripMenuItem1
        '
        Me.SaveImageToolStripMenuItem1.Name = "SaveImageToolStripMenuItem1"
        Me.SaveImageToolStripMenuItem1.Size = New System.Drawing.Size(138, 22)
        Me.SaveImageToolStripMenuItem1.Text = "Save Image"
        '
        'LoadImageToolStripMenuItem
        '
        Me.LoadImageToolStripMenuItem.Name = "LoadImageToolStripMenuItem"
        Me.LoadImageToolStripMenuItem.Size = New System.Drawing.Size(138, 22)
        Me.LoadImageToolStripMenuItem.Text = "Load Image"
        '
        'CopyImageToolStripMenuItem
        '
        Me.CopyImageToolStripMenuItem.Name = "CopyImageToolStripMenuItem"
        Me.CopyImageToolStripMenuItem.Size = New System.Drawing.Size(138, 22)
        Me.CopyImageToolStripMenuItem.Text = "Copy Image"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(446, 261)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(237, 37)
        Me.Button1.TabIndex = 28
        Me.Button1.Text = "Optimize Image"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(446, 304)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(237, 31)
        Me.Button2.TabIndex = 29
        Me.Button2.Text = "DELETE IMAGE"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(446, 72)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 13)
        Me.Label2.TabIndex = 33
        Me.Label2.Text = "Image Flip:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(446, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 13)
        Me.Label1.TabIndex = 32
        Me.Label1.Text = "Image Rotation:"
        '
        'ImageRotation_ImageFlip
        '
        Me.ImageRotation_ImageFlip.FormattingEnabled = True
        Me.ImageRotation_ImageFlip.Items.AddRange(New Object() {"None", "Vertical", "Horizontal", "Both"})
        Me.ImageRotation_ImageFlip.Location = New System.Drawing.Point(449, 88)
        Me.ImageRotation_ImageFlip.Name = "ImageRotation_ImageFlip"
        Me.ImageRotation_ImageFlip.Size = New System.Drawing.Size(172, 21)
        Me.ImageRotation_ImageFlip.TabIndex = 31
        '
        'ImageRotation_ImageRotation
        '
        Me.ImageRotation_ImageRotation.FormattingEnabled = True
        Me.ImageRotation_ImageRotation.Items.AddRange(New Object() {"None", "90 degrees clockwise", "180 degrees clockwise", "270 degrees clockwise", "360 degrees clockwise"})
        Me.ImageRotation_ImageRotation.Location = New System.Drawing.Point(449, 48)
        Me.ImageRotation_ImageRotation.Name = "ImageRotation_ImageRotation"
        Me.ImageRotation_ImageRotation.Size = New System.Drawing.Size(172, 21)
        Me.ImageRotation_ImageRotation.TabIndex = 30
        '
        'frmImageRotation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(695, 416)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ImageRotation_ImageFlip)
        Me.Controls.Add(Me.ImageRotation_ImageRotation)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ImageRotation_Dimensions_Original)
        Me.Controls.Add(Me.ImageRotation_btnOK)
        Me.Controls.Add(Me.ImageRotation_btnCancel)
        Me.Controls.Add(Me.ImageRotation_CheckboxResize)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.ImageRotation_PictureBox)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "frmImageRotation"
        Me.Text = "Select Image Rotation:"
        CType(Me.ImageRotation_PictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ImageRotation_btnOK As System.Windows.Forms.Button
    Friend WithEvents ImageRotation_btnCancel As System.Windows.Forms.Button
    Public WithEvents ImageRotation_PictureBox As System.Windows.Forms.PictureBox
    Friend WithEvents ImageRotation_Dimensions_Original As System.Windows.Forms.Label
    Friend WithEvents ImageRotation_Dimensions_Resized As System.Windows.Forms.Label
    Public WithEvents ImageRotation_CheckboxResize As System.Windows.Forms.CheckBox
    Friend WithEvents ImageRotation_btnResizeImage As System.Windows.Forms.Button
    Friend WithEvents lblResizeWidth As System.Windows.Forms.Label
    Friend WithEvents lblResizeHeight As System.Windows.Forms.Label
    Friend WithEvents ImageRotation_txtResizeWidth As System.Windows.Forms.TextBox
    Friend WithEvents ImageRotation_txtResizeHeight As System.Windows.Forms.TextBox
    Friend WithEvents ImageRotation_chkResizeProportional As System.Windows.Forms.CheckBox
    Friend WithEvents ImageRotation_btnResizeRevert As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents SaveImageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveImageToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LoadImageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopyImageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Public WithEvents ImageRotation_ImageFlip As ComboBox
    Public WithEvents ImageRotation_ImageRotation As ComboBox
End Class
