<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmFlashObject
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
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.SaveImageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveImageToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoadImageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyImageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        CType(Me.ImageRotation_PictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ImageRotation_PictureBox
        '
        Me.ImageRotation_PictureBox.Location = New System.Drawing.Point(8, 27)
        Me.ImageRotation_PictureBox.Name = "ImageRotation_PictureBox"
        Me.ImageRotation_PictureBox.Size = New System.Drawing.Size(425, 380)
        Me.ImageRotation_PictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ImageRotation_PictureBox.TabIndex = 0
        Me.ImageRotation_PictureBox.TabStop = False
        '
        'ImageRotation_btnOK
        '
        Me.ImageRotation_btnOK.Location = New System.Drawing.Point(752, 372)
        Me.ImageRotation_btnOK.Name = "ImageRotation_btnOK"
        Me.ImageRotation_btnOK.Size = New System.Drawing.Size(237, 31)
        Me.ImageRotation_btnOK.TabIndex = 5
        Me.ImageRotation_btnOK.Text = "OK"
        Me.ImageRotation_btnOK.UseVisualStyleBackColor = True
        '
        'ImageRotation_btnCancel
        '
        Me.ImageRotation_btnCancel.Location = New System.Drawing.Point(752, 337)
        Me.ImageRotation_btnCancel.Name = "ImageRotation_btnCancel"
        Me.ImageRotation_btnCancel.Size = New System.Drawing.Size(237, 32)
        Me.ImageRotation_btnCancel.TabIndex = 6
        Me.ImageRotation_btnCancel.Text = "CANCEL"
        Me.ImageRotation_btnCancel.UseVisualStyleBackColor = True
        '
        'ImageRotation_Dimensions_Original
        '
        Me.ImageRotation_Dimensions_Original.AutoSize = True
        Me.ImageRotation_Dimensions_Original.Location = New System.Drawing.Point(752, 28)
        Me.ImageRotation_Dimensions_Original.Name = "ImageRotation_Dimensions_Original"
        Me.ImageRotation_Dimensions_Original.Size = New System.Drawing.Size(170, 13)
        Me.ImageRotation_Dimensions_Original.TabIndex = 17
        Me.ImageRotation_Dimensions_Original.Text = "Original Dimensions: w: {0} x h: {1}"
        '
        'ImageRotation_CheckboxResize
        '
        Me.ImageRotation_CheckboxResize.AutoSize = True
        Me.ImageRotation_CheckboxResize.Location = New System.Drawing.Point(878, 346)
        Me.ImageRotation_CheckboxResize.Name = "ImageRotation_CheckboxResize"
        Me.ImageRotation_CheckboxResize.Size = New System.Drawing.Size(90, 17)
        Me.ImageRotation_CheckboxResize.TabIndex = 16
        Me.ImageRotation_CheckboxResize.Text = "Resize Image"
        Me.ImageRotation_CheckboxResize.UseVisualStyleBackColor = True
        Me.ImageRotation_CheckboxResize.Visible = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveImageToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1004, 24)
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
        Me.SaveImageToolStripMenuItem1.Size = New System.Drawing.Size(170, 22)
        Me.SaveImageToolStripMenuItem1.Text = "Save Flash (SWF)"
        '
        'LoadImageToolStripMenuItem
        '
        Me.LoadImageToolStripMenuItem.Name = "LoadImageToolStripMenuItem"
        Me.LoadImageToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.LoadImageToolStripMenuItem.Text = "Load Flash (SWF)"
        '
        'CopyImageToolStripMenuItem
        '
        Me.CopyImageToolStripMenuItem.Name = "CopyImageToolStripMenuItem"
        Me.CopyImageToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.CopyImageToolStripMenuItem.Text = "Copy Flash Object"
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Location = New System.Drawing.Point(8, 27)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.Size = New System.Drawing.Size(738, 380)
        Me.WebBrowser1.TabIndex = 30
        '
        'frmFlashObject
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1004, 416)
        Me.Controls.Add(Me.WebBrowser1)
        Me.Controls.Add(Me.ImageRotation_Dimensions_Original)
        Me.Controls.Add(Me.ImageRotation_btnOK)
        Me.Controls.Add(Me.ImageRotation_btnCancel)
        Me.Controls.Add(Me.ImageRotation_CheckboxResize)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.ImageRotation_PictureBox)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "frmFlashObject"
        Me.Text = "Select Image Rotation:"
        CType(Me.ImageRotation_PictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ImageRotation_btnOK As System.Windows.Forms.Button
    Friend WithEvents ImageRotation_btnCancel As System.Windows.Forms.Button
    Public WithEvents ImageRotation_PictureBox As System.Windows.Forms.PictureBox
    Friend WithEvents ImageRotation_Dimensions_Original As System.Windows.Forms.Label
    Public WithEvents ImageRotation_CheckboxResize As System.Windows.Forms.CheckBox
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents SaveImageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveImageToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LoadImageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopyImageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents WebBrowser1 As WebBrowser
End Class
