<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAttachments
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
        Me.btnAttachmentsAdd = New System.Windows.Forms.Button()
        Me.btnAttachmentsRemove = New System.Windows.Forms.Button()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.btnOpenFile = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.btnEditFileWait = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnAttachmentsAdd
        '
        Me.btnAttachmentsAdd.Location = New System.Drawing.Point(12, 24)
        Me.btnAttachmentsAdd.Name = "btnAttachmentsAdd"
        Me.btnAttachmentsAdd.Size = New System.Drawing.Size(31, 23)
        Me.btnAttachmentsAdd.TabIndex = 3
        Me.btnAttachmentsAdd.Text = "+"
        Me.btnAttachmentsAdd.UseVisualStyleBackColor = True
        '
        'btnAttachmentsRemove
        '
        Me.btnAttachmentsRemove.Location = New System.Drawing.Point(49, 24)
        Me.btnAttachmentsRemove.Name = "btnAttachmentsRemove"
        Me.btnAttachmentsRemove.Size = New System.Drawing.Size(31, 23)
        Me.btnAttachmentsRemove.TabIndex = 4
        Me.btnAttachmentsRemove.Text = "-"
        Me.btnAttachmentsRemove.UseVisualStyleBackColor = True
        '
        'btnOk
        '
        Me.btnOk.Location = New System.Drawing.Point(384, 264)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(75, 23)
        Me.btnOk.TabIndex = 5
        Me.btnOk.Text = "Close"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'btnOpenFile
        '
        Me.btnOpenFile.Location = New System.Drawing.Point(311, 22)
        Me.btnOpenFile.Name = "btnOpenFile"
        Me.btnOpenFile.Size = New System.Drawing.Size(71, 23)
        Me.btnOpenFile.TabIndex = 6
        Me.btnOpenFile.Text = "Open With"
        Me.btnOpenFile.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DataGridView1.Location = New System.Drawing.Point(12, 51)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(447, 207)
        Me.DataGridView1.TabIndex = 7
        '
        'btnEditFileWait
        '
        Me.btnEditFileWait.Location = New System.Drawing.Point(388, 22)
        Me.btnEditFileWait.Name = "btnEditFileWait"
        Me.btnEditFileWait.Size = New System.Drawing.Size(71, 23)
        Me.btnEditFileWait.TabIndex = 8
        Me.btnEditFileWait.Text = "Edit"
        Me.btnEditFileWait.UseVisualStyleBackColor = True
        '
        'frmAttachments
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(471, 288)
        Me.Controls.Add(Me.btnEditFileWait)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.btnOpenFile)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.btnAttachmentsRemove)
        Me.Controls.Add(Me.btnAttachmentsAdd)
        Me.Name = "frmAttachments"
        Me.Text = "Attachments"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents btnAttachmentsAdd As Button
    Public WithEvents btnAttachmentsRemove As Button
    Public WithEvents btnOk As Button
    Friend WithEvents btnOpenFile As Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents btnEditFileWait As Button
End Class
