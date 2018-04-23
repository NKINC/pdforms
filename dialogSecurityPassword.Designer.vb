<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dialogSecurityPassword
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
        Me.pnlPDFEncryption_BtnClear = New System.Windows.Forms.Button
        Me.pnlPDFEncryption = New System.Windows.Forms.Panel
        Me.pnlPDFEncryption_CompatibilityCmb = New System.Windows.Forms.ComboBox
        Me.pnlPDFEncryption_PermissionsChkRestrictDocument = New System.Windows.Forms.CheckBox
        Me.Label80 = New System.Windows.Forms.Label
        Me.pnlPDFEncryption_OpenPasswordChkRequired = New System.Windows.Forms.CheckBox
        Me.groupBoxEncryption = New System.Windows.Forms.GroupBox
        Me.pnlPDFEncryption_EncryptionRadFileAttachment = New System.Windows.Forms.RadioButton
        Me.pnlPDFEncryption_EncryptionRadAllExceptMeta = New System.Windows.Forms.RadioButton
        Me.pnlPDFEncryption_EncryptionRadAll = New System.Windows.Forms.RadioButton
        Me.groupBoxPermissions = New System.Windows.Forms.GroupBox
        Me.pnlPDFEncryption_PermissionsChk_FillIn = New System.Windows.Forms.CheckBox
        Me.pnlPDFEncryption_PermissionsChk_Annotations = New System.Windows.Forms.CheckBox
        Me.pnlPDFEncryption_PermissionsChk_Assembly = New System.Windows.Forms.CheckBox
        Me.pnlPDFEncryption_PermissionsChk_Contents = New System.Windows.Forms.CheckBox
        Me.pnlPDFEncryption_PermissionsTxtOwnerPassword = New System.Windows.Forms.TextBox
        Me.Label75 = New System.Windows.Forms.Label
        Me.pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared = New System.Windows.Forms.CheckBox
        Me.pnlPDFEncryption_PermissionsChkRestrictions_Copy = New System.Windows.Forms.CheckBox
        Me.pnlPDFEncryption_PermissionsCmbChangeRestrictions = New System.Windows.Forms.ComboBox
        Me.Label78 = New System.Windows.Forms.Label
        Me.pnlPDFEncryption_PermissionsCmbPrintingRestrictions = New System.Windows.Forms.ComboBox
        Me.Label79 = New System.Windows.Forms.Label
        Me.pnlPDFEncryption_EncryptionCmbStrength = New System.Windows.Forms.ComboBox
        Me.Label77 = New System.Windows.Forms.Label
        Me.pnlPDFEncryption_OpenPasswordTxtUserPassword = New System.Windows.Forms.TextBox
        Me.Label76 = New System.Windows.Forms.Label
        Me.TableLayoutPanel1.SuspendLayout()
        Me.pnlPDFEncryption.SuspendLayout()
        Me.groupBoxEncryption.SuspendLayout()
        Me.groupBoxPermissions.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.AutoSize = True
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.pnlPDFEncryption_BtnClear, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(5, 560)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(468, 50)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.OK_Button.Location = New System.Drawing.Point(159, 5)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(150, 40)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(315, 5)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(150, 40)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'pnlPDFEncryption_BtnClear
        '
        Me.pnlPDFEncryption_BtnClear.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.pnlPDFEncryption_BtnClear.Location = New System.Drawing.Point(3, 5)
        Me.pnlPDFEncryption_BtnClear.Name = "pnlPDFEncryption_BtnClear"
        Me.pnlPDFEncryption_BtnClear.Size = New System.Drawing.Size(150, 40)
        Me.pnlPDFEncryption_BtnClear.TabIndex = 960
        Me.pnlPDFEncryption_BtnClear.Text = "Unlock Document"
        Me.pnlPDFEncryption_BtnClear.UseVisualStyleBackColor = True
        '
        'pnlPDFEncryption
        '
        Me.pnlPDFEncryption.BackColor = System.Drawing.Color.Transparent
        Me.pnlPDFEncryption.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlPDFEncryption.Controls.Add(Me.pnlPDFEncryption_CompatibilityCmb)
        Me.pnlPDFEncryption.Controls.Add(Me.pnlPDFEncryption_PermissionsChkRestrictDocument)
        Me.pnlPDFEncryption.Controls.Add(Me.Label80)
        Me.pnlPDFEncryption.Controls.Add(Me.pnlPDFEncryption_OpenPasswordChkRequired)
        Me.pnlPDFEncryption.Controls.Add(Me.groupBoxEncryption)
        Me.pnlPDFEncryption.Controls.Add(Me.groupBoxPermissions)
        Me.pnlPDFEncryption.Controls.Add(Me.pnlPDFEncryption_EncryptionCmbStrength)
        Me.pnlPDFEncryption.Controls.Add(Me.Label77)
        Me.pnlPDFEncryption.Controls.Add(Me.pnlPDFEncryption_OpenPasswordTxtUserPassword)
        Me.pnlPDFEncryption.Controls.Add(Me.Label76)
        Me.pnlPDFEncryption.Location = New System.Drawing.Point(0, 0)
        Me.pnlPDFEncryption.Name = "pnlPDFEncryption"
        Me.pnlPDFEncryption.Size = New System.Drawing.Size(480, 560)
        Me.pnlPDFEncryption.TabIndex = 963
        '
        'pnlPDFEncryption_CompatibilityCmb
        '
        Me.pnlPDFEncryption_CompatibilityCmb.Items.AddRange(New Object() {"1.3", "1.4", "1.5", "1.6", "1.7", "Custom"})
        Me.pnlPDFEncryption_CompatibilityCmb.Location = New System.Drawing.Point(168, 8)
        Me.pnlPDFEncryption_CompatibilityCmb.Name = "pnlPDFEncryption_CompatibilityCmb"
        Me.pnlPDFEncryption_CompatibilityCmb.Size = New System.Drawing.Size(200, 21)
        Me.pnlPDFEncryption_CompatibilityCmb.TabIndex = 955
        '
        'pnlPDFEncryption_PermissionsChkRestrictDocument
        '
        Me.pnlPDFEncryption_PermissionsChkRestrictDocument.AutoSize = True
        Me.pnlPDFEncryption_PermissionsChkRestrictDocument.Location = New System.Drawing.Point(16, 272)
        Me.pnlPDFEncryption_PermissionsChkRestrictDocument.Name = "pnlPDFEncryption_PermissionsChkRestrictDocument"
        Me.pnlPDFEncryption_PermissionsChkRestrictDocument.Size = New System.Drawing.Size(234, 17)
        Me.pnlPDFEncryption_PermissionsChkRestrictDocument.TabIndex = 951
        Me.pnlPDFEncryption_PermissionsChkRestrictDocument.Text = "Restrict editing and printing of the document"
        Me.pnlPDFEncryption_PermissionsChkRestrictDocument.UseVisualStyleBackColor = True
        '
        'Label80
        '
        Me.Label80.BackColor = System.Drawing.Color.Transparent
        Me.Label80.Location = New System.Drawing.Point(40, 8)
        Me.Label80.Name = "Label80"
        Me.Label80.Size = New System.Drawing.Size(120, 23)
        Me.Label80.TabIndex = 956
        Me.Label80.Text = "Compatibility:"
        Me.Label80.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlPDFEncryption_OpenPasswordChkRequired
        '
        Me.pnlPDFEncryption_OpenPasswordChkRequired.AutoSize = True
        Me.pnlPDFEncryption_OpenPasswordChkRequired.Location = New System.Drawing.Point(16, 200)
        Me.pnlPDFEncryption_OpenPasswordChkRequired.Name = "pnlPDFEncryption_OpenPasswordChkRequired"
        Me.pnlPDFEncryption_OpenPasswordChkRequired.Size = New System.Drawing.Size(227, 17)
        Me.pnlPDFEncryption_OpenPasswordChkRequired.TabIndex = 954
        Me.pnlPDFEncryption_OpenPasswordChkRequired.Text = "Require a password to open the document"
        Me.pnlPDFEncryption_OpenPasswordChkRequired.UseVisualStyleBackColor = True
        '
        'groupBoxEncryption
        '
        Me.groupBoxEncryption.Controls.Add(Me.pnlPDFEncryption_EncryptionRadFileAttachment)
        Me.groupBoxEncryption.Controls.Add(Me.pnlPDFEncryption_EncryptionRadAllExceptMeta)
        Me.groupBoxEncryption.Controls.Add(Me.pnlPDFEncryption_EncryptionRadAll)
        Me.groupBoxEncryption.Location = New System.Drawing.Point(8, 72)
        Me.groupBoxEncryption.Name = "groupBoxEncryption"
        Me.groupBoxEncryption.Size = New System.Drawing.Size(464, 120)
        Me.groupBoxEncryption.TabIndex = 953
        Me.groupBoxEncryption.TabStop = False
        Me.groupBoxEncryption.Text = "Select Document Components to Encrypt"
        '
        'pnlPDFEncryption_EncryptionRadFileAttachment
        '
        Me.pnlPDFEncryption_EncryptionRadFileAttachment.AutoSize = True
        Me.pnlPDFEncryption_EncryptionRadFileAttachment.Location = New System.Drawing.Point(40, 88)
        Me.pnlPDFEncryption_EncryptionRadFileAttachment.Name = "pnlPDFEncryption_EncryptionRadFileAttachment"
        Me.pnlPDFEncryption_EncryptionRadFileAttachment.Size = New System.Drawing.Size(221, 17)
        Me.pnlPDFEncryption_EncryptionRadFileAttachment.TabIndex = 949
        Me.pnlPDFEncryption_EncryptionRadFileAttachment.Text = "Encrypt only file attachments - Acrobat 7+"
        Me.pnlPDFEncryption_EncryptionRadFileAttachment.UseVisualStyleBackColor = True
        '
        'pnlPDFEncryption_EncryptionRadAllExceptMeta
        '
        Me.pnlPDFEncryption_EncryptionRadAllExceptMeta.AutoSize = True
        Me.pnlPDFEncryption_EncryptionRadAllExceptMeta.Location = New System.Drawing.Point(40, 56)
        Me.pnlPDFEncryption_EncryptionRadAllExceptMeta.Name = "pnlPDFEncryption_EncryptionRadAllExceptMeta"
        Me.pnlPDFEncryption_EncryptionRadAllExceptMeta.Size = New System.Drawing.Size(319, 17)
        Me.pnlPDFEncryption_EncryptionRadAllExceptMeta.TabIndex = 948
        Me.pnlPDFEncryption_EncryptionRadAllExceptMeta.Text = "Encrypt all document contents (Except Metadata) - Acrobat 6+"
        Me.pnlPDFEncryption_EncryptionRadAllExceptMeta.UseVisualStyleBackColor = True
        '
        'pnlPDFEncryption_EncryptionRadAll
        '
        Me.pnlPDFEncryption_EncryptionRadAll.AutoSize = True
        Me.pnlPDFEncryption_EncryptionRadAll.Checked = True
        Me.pnlPDFEncryption_EncryptionRadAll.Location = New System.Drawing.Point(40, 24)
        Me.pnlPDFEncryption_EncryptionRadAll.Name = "pnlPDFEncryption_EncryptionRadAll"
        Me.pnlPDFEncryption_EncryptionRadAll.Size = New System.Drawing.Size(168, 17)
        Me.pnlPDFEncryption_EncryptionRadAll.TabIndex = 947
        Me.pnlPDFEncryption_EncryptionRadAll.TabStop = True
        Me.pnlPDFEncryption_EncryptionRadAll.Text = "Encrypt all document contents"
        Me.pnlPDFEncryption_EncryptionRadAll.UseVisualStyleBackColor = True
        '
        'groupBoxPermissions
        '
        Me.groupBoxPermissions.Controls.Add(Me.pnlPDFEncryption_PermissionsChk_FillIn)
        Me.groupBoxPermissions.Controls.Add(Me.pnlPDFEncryption_PermissionsChk_Annotations)
        Me.groupBoxPermissions.Controls.Add(Me.pnlPDFEncryption_PermissionsChk_Assembly)
        Me.groupBoxPermissions.Controls.Add(Me.pnlPDFEncryption_PermissionsChk_Contents)
        Me.groupBoxPermissions.Controls.Add(Me.pnlPDFEncryption_PermissionsTxtOwnerPassword)
        Me.groupBoxPermissions.Controls.Add(Me.Label75)
        Me.groupBoxPermissions.Controls.Add(Me.pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared)
        Me.groupBoxPermissions.Controls.Add(Me.pnlPDFEncryption_PermissionsChkRestrictions_Copy)
        Me.groupBoxPermissions.Controls.Add(Me.pnlPDFEncryption_PermissionsCmbChangeRestrictions)
        Me.groupBoxPermissions.Controls.Add(Me.Label78)
        Me.groupBoxPermissions.Controls.Add(Me.pnlPDFEncryption_PermissionsCmbPrintingRestrictions)
        Me.groupBoxPermissions.Controls.Add(Me.Label79)
        Me.groupBoxPermissions.Location = New System.Drawing.Point(8, 296)
        Me.groupBoxPermissions.Name = "groupBoxPermissions"
        Me.groupBoxPermissions.Size = New System.Drawing.Size(464, 256)
        Me.groupBoxPermissions.TabIndex = 952
        Me.groupBoxPermissions.TabStop = False
        Me.groupBoxPermissions.Text = "Permissions"
        '
        'pnlPDFEncryption_PermissionsChk_FillIn
        '
        Me.pnlPDFEncryption_PermissionsChk_FillIn.AutoSize = True
        Me.pnlPDFEncryption_PermissionsChk_FillIn.Enabled = False
        Me.pnlPDFEncryption_PermissionsChk_FillIn.Location = New System.Drawing.Point(112, 144)
        Me.pnlPDFEncryption_PermissionsChk_FillIn.Name = "pnlPDFEncryption_PermissionsChk_FillIn"
        Me.pnlPDFEncryption_PermissionsChk_FillIn.Size = New System.Drawing.Size(102, 17)
        Me.pnlPDFEncryption_PermissionsChk_FillIn.TabIndex = 959
        Me.pnlPDFEncryption_PermissionsChk_FillIn.Text = "FILL IN (128-bit)"
        Me.pnlPDFEncryption_PermissionsChk_FillIn.UseVisualStyleBackColor = True
        '
        'pnlPDFEncryption_PermissionsChk_Annotations
        '
        Me.pnlPDFEncryption_PermissionsChk_Annotations.AutoSize = True
        Me.pnlPDFEncryption_PermissionsChk_Annotations.Enabled = False
        Me.pnlPDFEncryption_PermissionsChk_Annotations.Location = New System.Drawing.Point(112, 120)
        Me.pnlPDFEncryption_PermissionsChk_Annotations.Name = "pnlPDFEncryption_PermissionsChk_Annotations"
        Me.pnlPDFEncryption_PermissionsChk_Annotations.Size = New System.Drawing.Size(148, 17)
        Me.pnlPDFEncryption_PermissionsChk_Annotations.TabIndex = 958
        Me.pnlPDFEncryption_PermissionsChk_Annotations.Text = "MODIFY ANNOTATIONS"
        Me.pnlPDFEncryption_PermissionsChk_Annotations.UseVisualStyleBackColor = True
        '
        'pnlPDFEncryption_PermissionsChk_Assembly
        '
        Me.pnlPDFEncryption_PermissionsChk_Assembly.AutoSize = True
        Me.pnlPDFEncryption_PermissionsChk_Assembly.Enabled = False
        Me.pnlPDFEncryption_PermissionsChk_Assembly.Location = New System.Drawing.Point(272, 144)
        Me.pnlPDFEncryption_PermissionsChk_Assembly.Name = "pnlPDFEncryption_PermissionsChk_Assembly"
        Me.pnlPDFEncryption_PermissionsChk_Assembly.Size = New System.Drawing.Size(124, 17)
        Me.pnlPDFEncryption_PermissionsChk_Assembly.TabIndex = 957
        Me.pnlPDFEncryption_PermissionsChk_Assembly.Text = "ASSEMBLY (128-bit)"
        Me.pnlPDFEncryption_PermissionsChk_Assembly.UseVisualStyleBackColor = True
        '
        'pnlPDFEncryption_PermissionsChk_Contents
        '
        Me.pnlPDFEncryption_PermissionsChk_Contents.AutoSize = True
        Me.pnlPDFEncryption_PermissionsChk_Contents.Enabled = False
        Me.pnlPDFEncryption_PermissionsChk_Contents.Location = New System.Drawing.Point(272, 120)
        Me.pnlPDFEncryption_PermissionsChk_Contents.Name = "pnlPDFEncryption_PermissionsChk_Contents"
        Me.pnlPDFEncryption_PermissionsChk_Contents.Size = New System.Drawing.Size(129, 17)
        Me.pnlPDFEncryption_PermissionsChk_Contents.TabIndex = 956
        Me.pnlPDFEncryption_PermissionsChk_Contents.Text = "MODIFY CONTENTS"
        Me.pnlPDFEncryption_PermissionsChk_Contents.UseVisualStyleBackColor = True
        '
        'pnlPDFEncryption_PermissionsTxtOwnerPassword
        '
        Me.pnlPDFEncryption_PermissionsTxtOwnerPassword.Location = New System.Drawing.Point(112, 24)
        Me.pnlPDFEncryption_PermissionsTxtOwnerPassword.Multiline = True
        Me.pnlPDFEncryption_PermissionsTxtOwnerPassword.Name = "pnlPDFEncryption_PermissionsTxtOwnerPassword"
        Me.pnlPDFEncryption_PermissionsTxtOwnerPassword.Size = New System.Drawing.Size(224, 24)
        Me.pnlPDFEncryption_PermissionsTxtOwnerPassword.TabIndex = 954
        '
        'Label75
        '
        Me.Label75.BackColor = System.Drawing.Color.Transparent
        Me.Label75.Location = New System.Drawing.Point(8, 24)
        Me.Label75.Name = "Label75"
        Me.Label75.Size = New System.Drawing.Size(88, 23)
        Me.Label75.TabIndex = 955
        Me.Label75.Text = "Owner Password"
        Me.Label75.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared
        '
        Me.pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared.AutoSize = True
        Me.pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared.Location = New System.Drawing.Point(56, 216)
        Me.pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared.Name = "pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared"
        Me.pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared.Size = New System.Drawing.Size(349, 17)
        Me.pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared.TabIndex = 953
        Me.pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared.Text = "Enable text access for screen reader devices for the visually impared"
        Me.pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared.UseVisualStyleBackColor = True
        '
        'pnlPDFEncryption_PermissionsChkRestrictions_Copy
        '
        Me.pnlPDFEncryption_PermissionsChkRestrictions_Copy.AutoSize = True
        Me.pnlPDFEncryption_PermissionsChkRestrictions_Copy.Location = New System.Drawing.Point(56, 184)
        Me.pnlPDFEncryption_PermissionsChkRestrictions_Copy.Name = "pnlPDFEncryption_PermissionsChkRestrictions_Copy"
        Me.pnlPDFEncryption_PermissionsChkRestrictions_Copy.Size = New System.Drawing.Size(260, 17)
        Me.pnlPDFEncryption_PermissionsChkRestrictions_Copy.TabIndex = 952
        Me.pnlPDFEncryption_PermissionsChkRestrictions_Copy.Text = "Enable copying of text, images, and other content"
        Me.pnlPDFEncryption_PermissionsChkRestrictions_Copy.UseVisualStyleBackColor = True
        '
        'pnlPDFEncryption_PermissionsCmbChangeRestrictions
        '
        Me.pnlPDFEncryption_PermissionsCmbChangeRestrictions.Items.AddRange(New Object() {"None", "Inserting, deleting, and rotating pages", "Filling in form fields and signing existing signature fields", "Commenting, filling in form fields, and signing existing signature fields", "Any except extracting pages", "Custom"})
        Me.pnlPDFEncryption_PermissionsCmbChangeRestrictions.Location = New System.Drawing.Point(112, 88)
        Me.pnlPDFEncryption_PermissionsCmbChangeRestrictions.Name = "pnlPDFEncryption_PermissionsCmbChangeRestrictions"
        Me.pnlPDFEncryption_PermissionsCmbChangeRestrictions.Size = New System.Drawing.Size(344, 21)
        Me.pnlPDFEncryption_PermissionsCmbChangeRestrictions.TabIndex = 950
        '
        'Label78
        '
        Me.Label78.BackColor = System.Drawing.Color.Transparent
        Me.Label78.Location = New System.Drawing.Point(16, 56)
        Me.Label78.Name = "Label78"
        Me.Label78.Size = New System.Drawing.Size(80, 23)
        Me.Label78.TabIndex = 947
        Me.Label78.Text = "Printing Allowed"
        Me.Label78.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlPDFEncryption_PermissionsCmbPrintingRestrictions
        '
        Me.pnlPDFEncryption_PermissionsCmbPrintingRestrictions.Items.AddRange(New Object() {"None", "Low Resolution (150 dpi)", "High Resolution (128-bit only)"})
        Me.pnlPDFEncryption_PermissionsCmbPrintingRestrictions.Location = New System.Drawing.Point(112, 56)
        Me.pnlPDFEncryption_PermissionsCmbPrintingRestrictions.Name = "pnlPDFEncryption_PermissionsCmbPrintingRestrictions"
        Me.pnlPDFEncryption_PermissionsCmbPrintingRestrictions.Size = New System.Drawing.Size(224, 21)
        Me.pnlPDFEncryption_PermissionsCmbPrintingRestrictions.TabIndex = 949
        '
        'Label79
        '
        Me.Label79.BackColor = System.Drawing.Color.Transparent
        Me.Label79.Location = New System.Drawing.Point(16, 88)
        Me.Label79.Name = "Label79"
        Me.Label79.Size = New System.Drawing.Size(80, 23)
        Me.Label79.TabIndex = 948
        Me.Label79.Text = "Changes Allowed"
        Me.Label79.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlPDFEncryption_EncryptionCmbStrength
        '
        Me.pnlPDFEncryption_EncryptionCmbStrength.Items.AddRange(New Object() {"None", "40-bit RC4", "128-bit RC4", "128-bit AES", "256-bit AES"})
        Me.pnlPDFEncryption_EncryptionCmbStrength.Location = New System.Drawing.Point(168, 40)
        Me.pnlPDFEncryption_EncryptionCmbStrength.Name = "pnlPDFEncryption_EncryptionCmbStrength"
        Me.pnlPDFEncryption_EncryptionCmbStrength.Size = New System.Drawing.Size(200, 21)
        Me.pnlPDFEncryption_EncryptionCmbStrength.TabIndex = 942
        '
        'Label77
        '
        Me.Label77.BackColor = System.Drawing.Color.Transparent
        Me.Label77.Location = New System.Drawing.Point(40, 40)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(120, 23)
        Me.Label77.TabIndex = 943
        Me.Label77.Text = "Encryption Strength"
        Me.Label77.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlPDFEncryption_OpenPasswordTxtUserPassword
        '
        Me.pnlPDFEncryption_OpenPasswordTxtUserPassword.Location = New System.Drawing.Point(168, 224)
        Me.pnlPDFEncryption_OpenPasswordTxtUserPassword.Multiline = True
        Me.pnlPDFEncryption_OpenPasswordTxtUserPassword.Name = "pnlPDFEncryption_OpenPasswordTxtUserPassword"
        Me.pnlPDFEncryption_OpenPasswordTxtUserPassword.Size = New System.Drawing.Size(296, 24)
        Me.pnlPDFEncryption_OpenPasswordTxtUserPassword.TabIndex = 939
        '
        'Label76
        '
        Me.Label76.BackColor = System.Drawing.Color.Transparent
        Me.Label76.Location = New System.Drawing.Point(16, 224)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(144, 23)
        Me.Label76.TabIndex = 940
        Me.Label76.Text = "Document Open Password"
        Me.Label76.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dialogSecurityPassword
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(481, 622)
        Me.Controls.Add(Me.pnlPDFEncryption)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dialogSecurityPassword"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Password Security"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.pnlPDFEncryption.ResumeLayout(False)
        Me.pnlPDFEncryption.PerformLayout()
        Me.groupBoxEncryption.ResumeLayout(False)
        Me.groupBoxEncryption.PerformLayout()
        Me.groupBoxPermissions.ResumeLayout(False)
        Me.groupBoxPermissions.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents pnlPDFEncryption_CompatibilityCmb As System.Windows.Forms.ComboBox
    Public WithEvents pnlPDFEncryption_PermissionsTxtOwnerPassword As System.Windows.Forms.TextBox
    Public WithEvents pnlPDFEncryption_PermissionsCmbChangeRestrictions As System.Windows.Forms.ComboBox
    Public WithEvents pnlPDFEncryption_PermissionsCmbPrintingRestrictions As System.Windows.Forms.ComboBox
    Public WithEvents pnlPDFEncryption_EncryptionCmbStrength As System.Windows.Forms.ComboBox
    Public WithEvents pnlPDFEncryption_OpenPasswordTxtUserPassword As System.Windows.Forms.TextBox
    Public WithEvents pnlPDFEncryption_BtnClear As System.Windows.Forms.Button
    Public WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Public WithEvents OK_Button As System.Windows.Forms.Button
    Public WithEvents Cancel_Button As System.Windows.Forms.Button
    Public WithEvents pnlPDFEncryption As System.Windows.Forms.Panel
    Public WithEvents pnlPDFEncryption_PermissionsChkRestrictDocument As System.Windows.Forms.CheckBox
    Public WithEvents Label80 As System.Windows.Forms.Label
    Public WithEvents pnlPDFEncryption_OpenPasswordChkRequired As System.Windows.Forms.CheckBox
    Public WithEvents groupBoxEncryption As System.Windows.Forms.GroupBox
    Public WithEvents pnlPDFEncryption_EncryptionRadFileAttachment As System.Windows.Forms.RadioButton
    Public WithEvents pnlPDFEncryption_EncryptionRadAllExceptMeta As System.Windows.Forms.RadioButton
    Public WithEvents pnlPDFEncryption_EncryptionRadAll As System.Windows.Forms.RadioButton
    Public WithEvents groupBoxPermissions As System.Windows.Forms.GroupBox
    Public WithEvents pnlPDFEncryption_PermissionsChk_FillIn As System.Windows.Forms.CheckBox
    Public WithEvents pnlPDFEncryption_PermissionsChk_Annotations As System.Windows.Forms.CheckBox
    Public WithEvents pnlPDFEncryption_PermissionsChk_Assembly As System.Windows.Forms.CheckBox
    Public WithEvents pnlPDFEncryption_PermissionsChk_Contents As System.Windows.Forms.CheckBox
    Public WithEvents Label75 As System.Windows.Forms.Label
    Public WithEvents pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared As System.Windows.Forms.CheckBox
    Public WithEvents pnlPDFEncryption_PermissionsChkRestrictions_Copy As System.Windows.Forms.CheckBox
    Public WithEvents Label78 As System.Windows.Forms.Label
    Public WithEvents Label79 As System.Windows.Forms.Label
    Public WithEvents Label77 As System.Windows.Forms.Label
    Public WithEvents Label76 As System.Windows.Forms.Label

End Class
