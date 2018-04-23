<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dialogListFields
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.GroupBoxFile1 = New System.Windows.Forms.GroupBox
        Me.FieldValue_Checkbox = New System.Windows.Forms.CheckBox
        Me.lblFieldName = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblFieldName1 = New System.Windows.Forms.Label
        Me.btnFileGetPdfFromEditor = New System.Windows.Forms.Button
        Me.FieldValue_ComboBoxDisplayNames = New System.Windows.Forms.ComboBox
        Me.FieldValue_ComboBoxValues = New System.Windows.Forms.ComboBox
        Me.FieldValue_ListBoxDisplayNames = New System.Windows.Forms.ListBox
        Me.FieldValue_ListBoxExportValues = New System.Windows.Forms.ListBox
        Me.FieldValue_TextBox = New System.Windows.Forms.TextBox
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBoxFile1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(565, 456)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(144, 80)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(66, 74)
        Me.OK_Button.TabIndex = 101
        Me.OK_Button.TabStop = False
        Me.OK_Button.Text = "SAVE"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(75, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(66, 74)
        Me.Cancel_Button.TabIndex = 105
        Me.Cancel_Button.TabStop = False
        Me.Cancel_Button.Text = "Cancel"
        '
        'GroupBoxFile1
        '
        Me.GroupBoxFile1.Controls.Add(Me.FieldValue_Checkbox)
        Me.GroupBoxFile1.Controls.Add(Me.lblFieldName)
        Me.GroupBoxFile1.Controls.Add(Me.Label1)
        Me.GroupBoxFile1.Controls.Add(Me.lblFieldName1)
        Me.GroupBoxFile1.Controls.Add(Me.btnFileGetPdfFromEditor)
        Me.GroupBoxFile1.Controls.Add(Me.FieldValue_ComboBoxDisplayNames)
        Me.GroupBoxFile1.Controls.Add(Me.FieldValue_ComboBoxValues)
        Me.GroupBoxFile1.Controls.Add(Me.FieldValue_TextBox)
        Me.GroupBoxFile1.Controls.Add(Me.FieldValue_ListBoxDisplayNames)
        Me.GroupBoxFile1.Controls.Add(Me.FieldValue_ListBoxExportValues)
        Me.GroupBoxFile1.Location = New System.Drawing.Point(2, 304)
        Me.GroupBoxFile1.Name = "GroupBoxFile1"
        Me.GroupBoxFile1.Size = New System.Drawing.Size(702, 147)
        Me.GroupBoxFile1.TabIndex = 39
        Me.GroupBoxFile1.TabStop = False
        Me.GroupBoxFile1.Text = "File Options:"
        '
        'FieldValue_Checkbox
        '
        Me.FieldValue_Checkbox.AutoSize = True
        Me.FieldValue_Checkbox.Location = New System.Drawing.Point(104, 48)
        Me.FieldValue_Checkbox.Name = "FieldValue_Checkbox"
        Me.FieldValue_Checkbox.Size = New System.Drawing.Size(81, 17)
        Me.FieldValue_Checkbox.TabIndex = 44
        Me.FieldValue_Checkbox.Text = "CheckBox1"
        Me.FieldValue_Checkbox.UseVisualStyleBackColor = True
        '
        'lblFieldName
        '
        Me.lblFieldName.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFieldName.Location = New System.Drawing.Point(104, 24)
        Me.lblFieldName.Name = "lblFieldName"
        Me.lblFieldName.Size = New System.Drawing.Size(320, 24)
        Me.lblFieldName.TabIndex = 42
        Me.lblFieldName.Text = "lblFieldName"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 24)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "Field Value:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblFieldName1
        '
        Me.lblFieldName1.Location = New System.Drawing.Point(8, 24)
        Me.lblFieldName1.Name = "lblFieldName1"
        Me.lblFieldName1.Size = New System.Drawing.Size(96, 24)
        Me.lblFieldName1.TabIndex = 15
        Me.lblFieldName1.Text = "Field Name:"
        Me.lblFieldName1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'btnFileGetPdfFromEditor
        '
        Me.btnFileGetPdfFromEditor.Location = New System.Drawing.Point(512, 48)
        Me.btnFileGetPdfFromEditor.Name = "btnFileGetPdfFromEditor"
        Me.btnFileGetPdfFromEditor.Size = New System.Drawing.Size(128, 48)
        Me.btnFileGetPdfFromEditor.TabIndex = 100
        Me.btnFileGetPdfFromEditor.TabStop = False
        Me.btnFileGetPdfFromEditor.Text = "Reload List"
        Me.btnFileGetPdfFromEditor.UseVisualStyleBackColor = True
        '
        'FieldValue_ComboBoxDisplayNames
        '
        Me.FieldValue_ComboBoxDisplayNames.FormattingEnabled = True
        Me.FieldValue_ComboBoxDisplayNames.Location = New System.Drawing.Point(104, 48)
        Me.FieldValue_ComboBoxDisplayNames.Name = "FieldValue_ComboBoxDisplayNames"
        Me.FieldValue_ComboBoxDisplayNames.Size = New System.Drawing.Size(376, 21)
        Me.FieldValue_ComboBoxDisplayNames.TabIndex = 45
        '
        'FieldValue_ComboBoxValues
        '
        Me.FieldValue_ComboBoxValues.FormattingEnabled = True
        Me.FieldValue_ComboBoxValues.Location = New System.Drawing.Point(104, 48)
        Me.FieldValue_ComboBoxValues.Name = "FieldValue_ComboBoxValues"
        Me.FieldValue_ComboBoxValues.Size = New System.Drawing.Size(376, 21)
        Me.FieldValue_ComboBoxValues.TabIndex = 27
        '
        'FieldValue_ListBoxDisplayNames
        '
        Me.FieldValue_ListBoxDisplayNames.FormattingEnabled = True
        Me.FieldValue_ListBoxDisplayNames.Location = New System.Drawing.Point(104, 48)
        Me.FieldValue_ListBoxDisplayNames.Name = "FieldValue_ListBoxDisplayNames"
        Me.FieldValue_ListBoxDisplayNames.Size = New System.Drawing.Size(376, 95)
        Me.FieldValue_ListBoxDisplayNames.TabIndex = 43
        '
        'FieldValue_ListBoxExportValues
        '
        Me.FieldValue_ListBoxExportValues.FormattingEnabled = True
        Me.FieldValue_ListBoxExportValues.Location = New System.Drawing.Point(104, 48)
        Me.FieldValue_ListBoxExportValues.Name = "FieldValue_ListBoxExportValues"
        Me.FieldValue_ListBoxExportValues.Size = New System.Drawing.Size(376, 95)
        Me.FieldValue_ListBoxExportValues.TabIndex = 28
        '
        'FieldValue_TextBox
        '
        Me.FieldValue_TextBox.AcceptsTab = True
        Me.FieldValue_TextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FieldValue_TextBox.Location = New System.Drawing.Point(104, 48)
        Me.FieldValue_TextBox.Multiline = True
        Me.FieldValue_TextBox.Name = "FieldValue_TextBox"
        Me.FieldValue_TextBox.Size = New System.Drawing.Size(376, 96)
        Me.FieldValue_TextBox.TabIndex = 25
        '
        'ProgressBar1
        '
        Me.ProgressBar1.BackColor = System.Drawing.SystemColors.Control
        Me.ProgressBar1.Location = New System.Drawing.Point(2, 507)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(544, 23)
        Me.ProgressBar1.TabIndex = 43
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DataGridView1.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(704, 304)
        Me.DataGridView1.TabIndex = 75
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(0, 456)
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox2.Size = New System.Drawing.Size(544, 48)
        Me.TextBox2.TabIndex = 0
        Me.TextBox2.TabStop = False
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(2, 83)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox1.Size = New System.Drawing.Size(704, 120)
        Me.TextBox1.TabIndex = 27
        '
        'dialogListFields
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(708, 534)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.GroupBoxFile1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dialogListFields"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "PDF Fields:"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GroupBoxFile1.ResumeLayout(False)
        Me.GroupBoxFile1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents GroupBoxFile1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblFieldName1 As System.Windows.Forms.Label
    Friend WithEvents btnFileGetPdfFromEditor As System.Windows.Forms.Button
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Public WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Public WithEvents FieldValue_TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents FieldValue_ComboBoxValues As System.Windows.Forms.ComboBox
    Friend WithEvents FieldValue_ListBoxExportValues As System.Windows.Forms.ListBox
    Friend WithEvents lblFieldName As System.Windows.Forms.Label
    Friend WithEvents FieldValue_ListBoxDisplayNames As System.Windows.Forms.ListBox
    Friend WithEvents FieldValue_Checkbox As System.Windows.Forms.CheckBox
    Friend WithEvents FieldValue_ComboBoxDisplayNames As System.Windows.Forms.ComboBox

End Class
