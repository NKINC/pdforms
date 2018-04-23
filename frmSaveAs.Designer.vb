<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSaveAs
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
        Me.frmSaveAs_PDFVersion = New System.Windows.Forms.ComboBox()
        Me.frmSaveAs_PDFVersion_Label = New System.Windows.Forms.Label()
        Me.frmSaveAs_FileSelection = New System.Windows.Forms.Button()
        Me.frmSaveAs_TextFilePath = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.frmSaveAs_ButtonSaveAs = New System.Windows.Forms.Button()
        Me.frmSaveAs_ButtonCancel = New System.Windows.Forms.Button()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.frmSaveAs_TextFileName = New System.Windows.Forms.TextBox()
        Me.chkRemoveUnusedObjects = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'frmSaveAs_PDFVersion
        '
        Me.frmSaveAs_PDFVersion.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frmSaveAs_PDFVersion.FormattingEnabled = True
        Me.frmSaveAs_PDFVersion.Items.AddRange(New Object() {"1.2", "1.3", "1.4", "1.5", "1.6", "1.7"})
        Me.frmSaveAs_PDFVersion.Location = New System.Drawing.Point(101, 110)
        Me.frmSaveAs_PDFVersion.Name = "frmSaveAs_PDFVersion"
        Me.frmSaveAs_PDFVersion.Size = New System.Drawing.Size(104, 24)
        Me.frmSaveAs_PDFVersion.TabIndex = 2
        Me.frmSaveAs_PDFVersion.Visible = False
        '
        'frmSaveAs_PDFVersion_Label
        '
        Me.frmSaveAs_PDFVersion_Label.Location = New System.Drawing.Point(13, 110)
        Me.frmSaveAs_PDFVersion_Label.Name = "frmSaveAs_PDFVersion_Label"
        Me.frmSaveAs_PDFVersion_Label.Size = New System.Drawing.Size(88, 23)
        Me.frmSaveAs_PDFVersion_Label.TabIndex = 1
        Me.frmSaveAs_PDFVersion_Label.Text = "PDF Version:"
        Me.frmSaveAs_PDFVersion_Label.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.frmSaveAs_PDFVersion_Label.Visible = False
        '
        'frmSaveAs_FileSelection
        '
        Me.frmSaveAs_FileSelection.Location = New System.Drawing.Point(418, 78)
        Me.frmSaveAs_FileSelection.Name = "frmSaveAs_FileSelection"
        Me.frmSaveAs_FileSelection.Size = New System.Drawing.Size(48, 23)
        Me.frmSaveAs_FileSelection.TabIndex = 1
        Me.frmSaveAs_FileSelection.Text = "select"
        Me.frmSaveAs_FileSelection.UseVisualStyleBackColor = True
        '
        'frmSaveAs_TextFilePath
        '
        Me.frmSaveAs_TextFilePath.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frmSaveAs_TextFilePath.Location = New System.Drawing.Point(101, 78)
        Me.frmSaveAs_TextFilePath.Name = "frmSaveAs_TextFilePath"
        Me.frmSaveAs_TextFilePath.Size = New System.Drawing.Size(312, 23)
        Me.frmSaveAs_TextFilePath.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(13, 78)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 23)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "File path:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmSaveAs_ButtonSaveAs
        '
        Me.frmSaveAs_ButtonSaveAs.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frmSaveAs_ButtonSaveAs.Location = New System.Drawing.Point(101, 151)
        Me.frmSaveAs_ButtonSaveAs.Name = "frmSaveAs_ButtonSaveAs"
        Me.frmSaveAs_ButtonSaveAs.Size = New System.Drawing.Size(271, 40)
        Me.frmSaveAs_ButtonSaveAs.TabIndex = 3
        Me.frmSaveAs_ButtonSaveAs.Text = "SAVE"
        Me.frmSaveAs_ButtonSaveAs.UseVisualStyleBackColor = True
        '
        'frmSaveAs_ButtonCancel
        '
        Me.frmSaveAs_ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.frmSaveAs_ButtonCancel.Location = New System.Drawing.Point(378, 151)
        Me.frmSaveAs_ButtonCancel.Name = "frmSaveAs_ButtonCancel"
        Me.frmSaveAs_ButtonCancel.Size = New System.Drawing.Size(88, 40)
        Me.frmSaveAs_ButtonCancel.TabIndex = 4
        Me.frmSaveAs_ButtonCancel.Text = "CANCEL"
        Me.frmSaveAs_ButtonCancel.UseVisualStyleBackColor = True
        '
        'lblStatus
        '
        Me.lblStatus.ForeColor = System.Drawing.Color.Blue
        Me.lblStatus.Location = New System.Drawing.Point(101, 9)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(368, 40)
        Me.lblStatus.TabIndex = 7
        Me.lblStatus.Text = "Status: Select a File Path"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(13, 49)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 23)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "File name:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmSaveAs_TextFileName
        '
        Me.frmSaveAs_TextFileName.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frmSaveAs_TextFileName.Location = New System.Drawing.Point(101, 49)
        Me.frmSaveAs_TextFileName.Name = "frmSaveAs_TextFileName"
        Me.frmSaveAs_TextFileName.ReadOnly = True
        Me.frmSaveAs_TextFileName.Size = New System.Drawing.Size(365, 23)
        Me.frmSaveAs_TextFileName.TabIndex = 8
        '
        'chkRemoveUnusedObjects
        '
        Me.chkRemoveUnusedObjects.AutoSize = True
        Me.chkRemoveUnusedObjects.Checked = True
        Me.chkRemoveUnusedObjects.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkRemoveUnusedObjects.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRemoveUnusedObjects.Location = New System.Drawing.Point(273, 111)
        Me.chkRemoveUnusedObjects.Name = "chkRemoveUnusedObjects"
        Me.chkRemoveUnusedObjects.Size = New System.Drawing.Size(193, 22)
        Me.chkRemoveUnusedObjects.TabIndex = 10
        Me.chkRemoveUnusedObjects.Text = "Remove Unused Objects"
        Me.chkRemoveUnusedObjects.UseVisualStyleBackColor = True
        '
        'frmSaveAs
        '
        Me.AcceptButton = Me.frmSaveAs_ButtonSaveAs
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.frmSaveAs_ButtonCancel
        Me.ClientSize = New System.Drawing.Size(481, 203)
        Me.Controls.Add(Me.chkRemoveUnusedObjects)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.frmSaveAs_TextFileName)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.frmSaveAs_ButtonCancel)
        Me.Controls.Add(Me.frmSaveAs_ButtonSaveAs)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.frmSaveAs_TextFilePath)
        Me.Controls.Add(Me.frmSaveAs_FileSelection)
        Me.Controls.Add(Me.frmSaveAs_PDFVersion_Label)
        Me.Controls.Add(Me.frmSaveAs_PDFVersion)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSaveAs"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Save Document As:"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents frmSaveAs_PDFVersion As System.Windows.Forms.ComboBox
    Public WithEvents frmSaveAs_PDFVersion_Label As System.Windows.Forms.Label
    Public WithEvents frmSaveAs_FileSelection As System.Windows.Forms.Button
    Public WithEvents frmSaveAs_TextFilePath As System.Windows.Forms.TextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents frmSaveAs_ButtonSaveAs As System.Windows.Forms.Button
    Public WithEvents frmSaveAs_ButtonCancel As System.Windows.Forms.Button
    Public WithEvents lblStatus As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents frmSaveAs_TextFileName As System.Windows.Forms.TextBox
    Friend WithEvents chkRemoveUnusedObjects As CheckBox
End Class
