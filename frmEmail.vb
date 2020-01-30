Imports System.IO
Imports System.Reflection
Imports System.Xml.Serialization
Imports System.Xml
Imports System.Security.Cryptography
Public Class frmEmail
    Inherits System.Windows.Forms.Form
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 NK-INC.COM All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>
#Region "REG KEYS"
#End Region
#Region " Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub
    Public Sub New(ByRef frmMain2 As frmMain)
        MyBase.New()
        InitializeComponent()
        frmMain1 = frmMain2
    End Sub
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
    Public Function String_IsNullOrEmpty(ByVal str As String) As Boolean
        If str = Nothing Then
            Return True
        ElseIf CStr(str & "") = "" Then
            Return True
        ElseIf CStr(str & "") = String.Empty Then
            Return True
        End If
        Return False
    End Function
    Private components As System.ComponentModel.IContainer
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents txtFrom_Name As System.Windows.Forms.TextBox
    Friend WithEvents txtFrom_Email As System.Windows.Forms.TextBox
    Friend WithEvents SaveScript As System.Windows.Forms.SaveFileDialog
    Friend WithEvents mnuMain As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents optRecipients As System.Windows.Forms.RadioButton
    Friend WithEvents optRecipients2 As System.Windows.Forms.RadioButton
    Friend WithEvents cmbSettings As System.Windows.Forms.ComboBox
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents optFormat_PDF As System.Windows.Forms.RadioButton
    Friend WithEvents optFormat_FDF As System.Windows.Forms.RadioButton
    Friend WithEvents optFormat_xFDF As System.Windows.Forms.RadioButton
    Friend WithEvents optFormat_XML As System.Windows.Forms.RadioButton
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtUser_Name As System.Windows.Forms.TextBox
    Friend WithEvents txtServer_Port As System.Windows.Forms.TextBox
    Friend WithEvents txtServer_Name As System.Windows.Forms.TextBox
    Friend WithEvents chkExchange As System.Windows.Forms.CheckBox
    Friend WithEvents chkFDFTKSetup As System.Windows.Forms.CheckBox
    Friend WithEvents lblSMTP2 As System.Windows.Forms.Label
    Friend WithEvents lblSmtp1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtMessage_Subject As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtMessage_Body As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents optFormat_XDP As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents optSMTP_Auth_Credentials As System.Windows.Forms.RadioButton
    Friend WithEvents optSMTP_Auth_Basic As System.Windows.Forms.RadioButton
    Friend WithEvents optSMTP_Auth_None As System.Windows.Forms.RadioButton
    Friend WithEvents txtTo_Email As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtPassword_Credential As System.Windows.Forms.TextBox
    Friend WithEvents txtUser_Credential As System.Windows.Forms.TextBox
    Friend WithEvents lblCred1 As System.Windows.Forms.Label
    Friend WithEvents lblCred2 As System.Windows.Forms.Label
    Friend WithEvents chkSSL As System.Windows.Forms.CheckBox
    Friend WithEvents txtBCC_Email As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtCC_Email As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtAttachmentFName As System.Windows.Forms.TextBox
    Friend WithEvents lblAttachment_FName As System.Windows.Forms.Label
    Friend WithEvents txtDomain_Credential As System.Windows.Forms.TextBox
    Friend WithEvents lblDomain_Credentials As System.Windows.Forms.Label
    Friend WithEvents optFormat_XPDF As System.Windows.Forms.RadioButton
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtMSG_TO_FIELD As System.Windows.Forms.TextBox
    Friend WithEvents btnCopy As System.Windows.Forms.Button
    Friend WithEvents txtPDF_OpenPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtPDF_ModifyPassword As System.Windows.Forms.TextBox
    Friend WithEvents chkFlatten_PDF As System.Windows.Forms.CheckBox
    Friend WithEvents lblPDFOpenPW As System.Windows.Forms.Label
    Friend WithEvents lblPDFModifyPW As System.Windows.Forms.Label
    Friend WithEvents optFormat_RAW As System.Windows.Forms.RadioButton
    Friend WithEvents btnTestSMTP As System.Windows.Forms.Button
    Friend WithEvents FileSystemWatcher1 As System.IO.FileSystemWatcher
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents SMTP_Delivery_Method_Specify As System.Windows.Forms.RadioButton
    Friend WithEvents SMTP_Delivery_Method_IIS As System.Windows.Forms.RadioButton
    Friend WithEvents SMTP_Delivery_Method_Network As System.Windows.Forms.RadioButton
    Friend WithEvents SMTP_DELIVERY_METHOD_SPECIFY_TEXT As System.Windows.Forms.TextBox
    Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
    Friend WithEvents btnSupportImport As System.Windows.Forms.Button
    Friend WithEvents btnSupportExport As System.Windows.Forms.Button
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents txtSettingName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents lblPDFURL As System.Windows.Forms.LinkLabel
    Friend WithEvents txtPDFURL As System.Windows.Forms.TextBox
    Friend WithEvents TabPage7 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents txtSendResults As System.Windows.Forms.TextBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage8 As System.Windows.Forms.TabPage
    Friend WithEvents optFormat_HTML_Attachment As RadioButton
    Friend WithEvents optFormat_HTML_Inline As RadioButton
    Friend WithEvents chkEmailMessage_HTMLFormat As CheckBox
    Friend WithEvents optFormat_None As RadioButton
    Friend WithEvents btnMailServerGmailSettings As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEmail))
        Me.txtFrom_Name = New System.Windows.Forms.TextBox()
        Me.txtFrom_Email = New System.Windows.Forms.TextBox()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.SaveScript = New System.Windows.Forms.SaveFileDialog()
        Me.mnuMain = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.optRecipients = New System.Windows.Forms.RadioButton()
        Me.optRecipients2 = New System.Windows.Forms.RadioButton()
        Me.cmbSettings = New System.Windows.Forms.ComboBox()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.optFormat_HTML_Attachment = New System.Windows.Forms.RadioButton()
        Me.optFormat_HTML_Inline = New System.Windows.Forms.RadioButton()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.lblPDFURL = New System.Windows.Forms.LinkLabel()
        Me.txtPDFURL = New System.Windows.Forms.TextBox()
        Me.optFormat_RAW = New System.Windows.Forms.RadioButton()
        Me.txtPDF_OpenPassword = New System.Windows.Forms.TextBox()
        Me.txtPDF_ModifyPassword = New System.Windows.Forms.TextBox()
        Me.chkFlatten_PDF = New System.Windows.Forms.CheckBox()
        Me.lblPDFOpenPW = New System.Windows.Forms.Label()
        Me.lblPDFModifyPW = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.optFormat_XPDF = New System.Windows.Forms.RadioButton()
        Me.txtAttachmentFName = New System.Windows.Forms.TextBox()
        Me.optFormat_XDP = New System.Windows.Forms.RadioButton()
        Me.optFormat_XML = New System.Windows.Forms.RadioButton()
        Me.optFormat_xFDF = New System.Windows.Forms.RadioButton()
        Me.optFormat_FDF = New System.Windows.Forms.RadioButton()
        Me.optFormat_PDF = New System.Windows.Forms.RadioButton()
        Me.lblAttachment_FName = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.btnSupportImport = New System.Windows.Forms.Button()
        Me.btnSupportExport = New System.Windows.Forms.Button()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.txtSettingName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.btnMailServerGmailSettings = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.SMTP_DELIVERY_METHOD_SPECIFY_TEXT = New System.Windows.Forms.TextBox()
        Me.SMTP_Delivery_Method_Specify = New System.Windows.Forms.RadioButton()
        Me.SMTP_Delivery_Method_IIS = New System.Windows.Forms.RadioButton()
        Me.SMTP_Delivery_Method_Network = New System.Windows.Forms.RadioButton()
        Me.btnTestSMTP = New System.Windows.Forms.Button()
        Me.chkSSL = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtDomain_Credential = New System.Windows.Forms.TextBox()
        Me.lblDomain_Credentials = New System.Windows.Forms.Label()
        Me.txtPassword_Credential = New System.Windows.Forms.TextBox()
        Me.txtUser_Credential = New System.Windows.Forms.TextBox()
        Me.lblCred1 = New System.Windows.Forms.Label()
        Me.lblCred2 = New System.Windows.Forms.Label()
        Me.optSMTP_Auth_Credentials = New System.Windows.Forms.RadioButton()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.optSMTP_Auth_Basic = New System.Windows.Forms.RadioButton()
        Me.txtUser_Name = New System.Windows.Forms.TextBox()
        Me.optSMTP_Auth_None = New System.Windows.Forms.RadioButton()
        Me.lblSmtp1 = New System.Windows.Forms.Label()
        Me.lblSMTP2 = New System.Windows.Forms.Label()
        Me.txtServer_Port = New System.Windows.Forms.TextBox()
        Me.txtServer_Name = New System.Windows.Forms.TextBox()
        Me.chkExchange = New System.Windows.Forms.CheckBox()
        Me.chkFDFTKSetup = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.chkEmailMessage_HTMLFormat = New System.Windows.Forms.CheckBox()
        Me.txtBCC_Email = New System.Windows.Forms.TextBox()
        Me.txtCC_Email = New System.Windows.Forms.TextBox()
        Me.txtTo_Email = New System.Windows.Forms.TextBox()
        Me.txtMessage_Subject = New System.Windows.Forms.TextBox()
        Me.txtMessage_Body = New System.Windows.Forms.TextBox()
        Me.txtMSG_TO_FIELD = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.TabPage8 = New System.Windows.Forms.TabPage()
        Me.TabPage7 = New System.Windows.Forms.TabPage()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txtSendResults = New System.Windows.Forms.TextBox()
        Me.btnCopy = New System.Windows.Forms.Button()
        Me.FileSystemWatcher1 = New System.IO.FileSystemWatcher()
        Me.optFormat_None = New System.Windows.Forms.RadioButton()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage6.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage7.SuspendLayout()
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        Me.txtFrom_Name.BackColor = System.Drawing.Color.White
        Me.txtFrom_Name.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFrom_Name.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtFrom_Name.Location = New System.Drawing.Point(16, 24)
        Me.txtFrom_Name.Name = "txtFrom_Name"
        Me.txtFrom_Name.Size = New System.Drawing.Size(232, 24)
        Me.txtFrom_Name.TabIndex = 50
        Me.txtFrom_Name.Text = "Joe User"
        Me.txtFrom_Email.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtFrom_Email.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFrom_Email.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtFrom_Email.Location = New System.Drawing.Point(16, 80)
        Me.txtFrom_Email.Name = "txtFrom_Email"
        Me.txtFrom_Email.Size = New System.Drawing.Size(232, 24)
        Me.txtFrom_Email.TabIndex = 51
        Me.txtFrom_Email.Text = "joe.user@domain.com"
        Me.lblStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.ForeColor = System.Drawing.Color.DarkBlue
        Me.lblStatus.Location = New System.Drawing.Point(17, 544)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(552, 69)
        Me.lblStatus.TabIndex = 16
        Me.lblStatus.Text = "Status: Ready"
        Me.mnuMain.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1})
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem3})
        Me.MenuItem1.Text = "File"
        Me.MenuItem3.Index = 0
        Me.MenuItem3.Text = "Exit"
        Me.optRecipients.BackColor = System.Drawing.Color.Transparent
        Me.optRecipients.Checked = True
        Me.optRecipients.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.optRecipients.Location = New System.Drawing.Point(200, 272)
        Me.optRecipients.Name = "optRecipients"
        Me.optRecipients.Size = New System.Drawing.Size(89, 20)
        Me.optRecipients.TabIndex = 54
        Me.optRecipients.TabStop = True
        Me.optRecipients.Text = "Recipient List"
        Me.optRecipients.UseVisualStyleBackColor = False
        Me.optRecipients.Visible = False
        Me.optRecipients2.BackColor = System.Drawing.Color.Transparent
        Me.optRecipients2.Enabled = False
        Me.optRecipients2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.optRecipients2.Location = New System.Drawing.Point(48, 272)
        Me.optRecipients2.Name = "optRecipients2"
        Me.optRecipients2.Size = New System.Drawing.Size(142, 20)
        Me.optRecipients2.TabIndex = 53
        Me.optRecipients2.Text = "Dynamic Recipients"
        Me.optRecipients2.UseVisualStyleBackColor = False
        Me.optRecipients2.Visible = False
        Me.cmbSettings.BackColor = System.Drawing.Color.White
        Me.cmbSettings.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSettings.ForeColor = System.Drawing.Color.Navy
        Me.cmbSettings.Location = New System.Drawing.Point(16, 18)
        Me.cmbSettings.Name = "cmbSettings"
        Me.cmbSettings.Size = New System.Drawing.Size(256, 32)
        Me.cmbSettings.TabIndex = 1
        Me.cmbSettings.Text = "Select a Setting"
        Me.btnAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnAdd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnAdd.Location = New System.Drawing.Point(280, 16)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(56, 32)
        Me.btnAdd.TabIndex = 2
        Me.btnAdd.Text = "New"
        Me.btnAdd.UseVisualStyleBackColor = False
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnDelete.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDelete.Location = New System.Drawing.Point(480, 16)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(50, 32)
        Me.btnDelete.TabIndex = 5
        Me.btnDelete.TabStop = False
        Me.btnDelete.Text = "Del"
        Me.btnDelete.UseVisualStyleBackColor = False
        Me.btnUpdate.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnUpdate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnUpdate.Location = New System.Drawing.Point(344, 16)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(72, 32)
        Me.btnUpdate.TabIndex = 3
        Me.btnUpdate.Text = "Update"
        Me.btnUpdate.UseVisualStyleBackColor = False
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnCancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCancel.Location = New System.Drawing.Point(536, 16)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(48, 32)
        Me.btnCancel.TabIndex = 6
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        Me.Panel1.Controls.Add(Me.optFormat_None)
        Me.Panel1.Controls.Add(Me.optFormat_HTML_Attachment)
        Me.Panel1.Controls.Add(Me.optFormat_HTML_Inline)
        Me.Panel1.Controls.Add(Me.Button2)
        Me.Panel1.Controls.Add(Me.lblPDFURL)
        Me.Panel1.Controls.Add(Me.txtPDFURL)
        Me.Panel1.Controls.Add(Me.optFormat_RAW)
        Me.Panel1.Controls.Add(Me.txtPDF_OpenPassword)
        Me.Panel1.Controls.Add(Me.txtPDF_ModifyPassword)
        Me.Panel1.Controls.Add(Me.chkFlatten_PDF)
        Me.Panel1.Controls.Add(Me.lblPDFOpenPW)
        Me.Panel1.Controls.Add(Me.lblPDFModifyPW)
        Me.Panel1.Controls.Add(Me.Label18)
        Me.Panel1.Controls.Add(Me.optFormat_XPDF)
        Me.Panel1.Controls.Add(Me.txtAttachmentFName)
        Me.Panel1.Controls.Add(Me.optFormat_XDP)
        Me.Panel1.Controls.Add(Me.optFormat_XML)
        Me.Panel1.Controls.Add(Me.optFormat_xFDF)
        Me.Panel1.Controls.Add(Me.optFormat_FDF)
        Me.Panel1.Controls.Add(Me.optFormat_PDF)
        Me.Panel1.Controls.Add(Me.lblAttachment_FName)
        Me.Panel1.Location = New System.Drawing.Point(23, 36)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(515, 402)
        Me.Panel1.TabIndex = 21
        Me.optFormat_HTML_Attachment.BackColor = System.Drawing.Color.Transparent
        Me.optFormat_HTML_Attachment.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optFormat_HTML_Attachment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.optFormat_HTML_Attachment.Location = New System.Drawing.Point(232, 166)
        Me.optFormat_HTML_Attachment.Name = "optFormat_HTML_Attachment"
        Me.optFormat_HTML_Attachment.Size = New System.Drawing.Size(171, 24)
        Me.optFormat_HTML_Attachment.TabIndex = 59
        Me.optFormat_HTML_Attachment.Text = "HTML ""Attachment"""
        Me.optFormat_HTML_Attachment.UseVisualStyleBackColor = False
        Me.optFormat_HTML_Inline.BackColor = System.Drawing.Color.Transparent
        Me.optFormat_HTML_Inline.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optFormat_HTML_Inline.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.optFormat_HTML_Inline.Location = New System.Drawing.Point(232, 196)
        Me.optFormat_HTML_Inline.Name = "optFormat_HTML_Inline"
        Me.optFormat_HTML_Inline.Size = New System.Drawing.Size(153, 24)
        Me.optFormat_HTML_Inline.TabIndex = 60
        Me.optFormat_HTML_Inline.Text = "HTML Embeded Inline"
        Me.optFormat_HTML_Inline.UseVisualStyleBackColor = False
        Me.Button2.Location = New System.Drawing.Point(432, 369)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 65
        Me.Button2.Text = "From file"
        Me.Button2.UseVisualStyleBackColor = True
        Me.lblPDFURL.AutoSize = True
        Me.lblPDFURL.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPDFURL.Location = New System.Drawing.Point(24, 345)
        Me.lblPDFURL.Name = "lblPDFURL"
        Me.lblPDFURL.Size = New System.Drawing.Size(270, 18)
        Me.lblPDFURL.TabIndex = 66
        Me.lblPDFURL.TabStop = True
        Me.lblPDFURL.Text = "URL to Blank PDF Document (required)"
        Me.txtPDFURL.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPDFURL.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPDFURL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPDFURL.Location = New System.Drawing.Point(24, 369)
        Me.txtPDFURL.Name = "txtPDFURL"
        Me.txtPDFURL.Size = New System.Drawing.Size(402, 24)
        Me.txtPDFURL.TabIndex = 64
        Me.optFormat_RAW.BackColor = System.Drawing.Color.Transparent
        Me.optFormat_RAW.Checked = True
        Me.optFormat_RAW.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optFormat_RAW.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.optFormat_RAW.Location = New System.Drawing.Point(32, 40)
        Me.optFormat_RAW.Name = "optFormat_RAW"
        Me.optFormat_RAW.Size = New System.Drawing.Size(184, 24)
        Me.optFormat_RAW.TabIndex = 51
        Me.optFormat_RAW.TabStop = True
        Me.optFormat_RAW.Text = "RAW submission data"
        Me.optFormat_RAW.UseVisualStyleBackColor = False
        Me.txtPDF_OpenPassword.BackColor = System.Drawing.Color.White
        Me.txtPDF_OpenPassword.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPDF_OpenPassword.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPDF_OpenPassword.Location = New System.Drawing.Point(24, 256)
        Me.txtPDF_OpenPassword.Name = "txtPDF_OpenPassword"
        Me.txtPDF_OpenPassword.Size = New System.Drawing.Size(200, 24)
        Me.txtPDF_OpenPassword.TabIndex = 61
        Me.txtPDF_ModifyPassword.BackColor = System.Drawing.Color.White
        Me.txtPDF_ModifyPassword.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPDF_ModifyPassword.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPDF_ModifyPassword.Location = New System.Drawing.Point(240, 256)
        Me.txtPDF_ModifyPassword.Name = "txtPDF_ModifyPassword"
        Me.txtPDF_ModifyPassword.Size = New System.Drawing.Size(200, 24)
        Me.txtPDF_ModifyPassword.TabIndex = 62
        Me.chkFlatten_PDF.BackColor = System.Drawing.Color.Transparent
        Me.chkFlatten_PDF.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFlatten_PDF.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.chkFlatten_PDF.Location = New System.Drawing.Point(32, 136)
        Me.chkFlatten_PDF.Name = "chkFlatten_PDF"
        Me.chkFlatten_PDF.Size = New System.Drawing.Size(176, 24)
        Me.chkFlatten_PDF.TabIndex = 54
        Me.chkFlatten_PDF.Text = "Flatten PDF Attachment"
        Me.chkFlatten_PDF.UseVisualStyleBackColor = False
        Me.lblPDFOpenPW.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPDFOpenPW.ForeColor = System.Drawing.Color.Black
        Me.lblPDFOpenPW.Location = New System.Drawing.Point(24, 232)
        Me.lblPDFOpenPW.Name = "lblPDFOpenPW"
        Me.lblPDFOpenPW.Size = New System.Drawing.Size(184, 32)
        Me.lblPDFOpenPW.TabIndex = 45
        Me.lblPDFOpenPW.Text = "PDF Open Password"
        Me.lblPDFModifyPW.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPDFModifyPW.ForeColor = System.Drawing.Color.Black
        Me.lblPDFModifyPW.Location = New System.Drawing.Point(240, 232)
        Me.lblPDFModifyPW.Name = "lblPDFModifyPW"
        Me.lblPDFModifyPW.Size = New System.Drawing.Size(219, 27)
        Me.lblPDFModifyPW.TabIndex = 47
        Me.lblPDFModifyPW.Text = "PDF Modify/Owner Password"
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Black
        Me.Label18.Location = New System.Drawing.Point(16, 8)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(187, 16)
        Me.Label18.TabIndex = 42
        Me.Label18.Text = "ATTACHMENT FORMAT"
        Me.optFormat_XPDF.BackColor = System.Drawing.Color.Transparent
        Me.optFormat_XPDF.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optFormat_XPDF.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.optFormat_XPDF.Location = New System.Drawing.Point(32, 104)
        Me.optFormat_XPDF.Name = "optFormat_XPDF"
        Me.optFormat_XPDF.Size = New System.Drawing.Size(184, 24)
        Me.optFormat_XPDF.TabIndex = 53
        Me.optFormat_XPDF.Text = "Merged Static LiveCycle XFA"
        Me.optFormat_XPDF.UseVisualStyleBackColor = False
        Me.txtAttachmentFName.BackColor = System.Drawing.Color.White
        Me.txtAttachmentFName.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAttachmentFName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtAttachmentFName.Location = New System.Drawing.Point(24, 303)
        Me.txtAttachmentFName.Name = "txtAttachmentFName"
        Me.txtAttachmentFName.Size = New System.Drawing.Size(482, 24)
        Me.txtAttachmentFName.TabIndex = 63
        Me.optFormat_XDP.BackColor = System.Drawing.Color.Transparent
        Me.optFormat_XDP.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optFormat_XDP.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.optFormat_XDP.Location = New System.Drawing.Point(232, 104)
        Me.optFormat_XDP.Name = "optFormat_XDP"
        Me.optFormat_XDP.Size = New System.Drawing.Size(280, 24)
        Me.optFormat_XDP.TabIndex = 57
        Me.optFormat_XDP.Text = "XDP Data  (XML based + Filename + Images)"
        Me.optFormat_XDP.UseVisualStyleBackColor = False
        Me.optFormat_XML.BackColor = System.Drawing.Color.Transparent
        Me.optFormat_XML.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optFormat_XML.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.optFormat_XML.Location = New System.Drawing.Point(232, 136)
        Me.optFormat_XML.Name = "optFormat_XML"
        Me.optFormat_XML.Size = New System.Drawing.Size(250, 24)
        Me.optFormat_XML.TabIndex = 58
        Me.optFormat_XML.Text = "XML Data (No Filename)"
        Me.optFormat_XML.UseVisualStyleBackColor = False
        Me.optFormat_xFDF.BackColor = System.Drawing.Color.Transparent
        Me.optFormat_xFDF.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optFormat_xFDF.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.optFormat_xFDF.Location = New System.Drawing.Point(232, 72)
        Me.optFormat_xFDF.Name = "optFormat_xFDF"
        Me.optFormat_xFDF.Size = New System.Drawing.Size(272, 24)
        Me.optFormat_xFDF.TabIndex = 56
        Me.optFormat_xFDF.Text = "xFDF (XML based + Filename) Data"
        Me.optFormat_xFDF.UseVisualStyleBackColor = False
        Me.optFormat_FDF.BackColor = System.Drawing.Color.Transparent
        Me.optFormat_FDF.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optFormat_FDF.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.optFormat_FDF.Location = New System.Drawing.Point(232, 40)
        Me.optFormat_FDF.Name = "optFormat_FDF"
        Me.optFormat_FDF.Size = New System.Drawing.Size(202, 24)
        Me.optFormat_FDF.TabIndex = 55
        Me.optFormat_FDF.Text = "FDF Data (Includes Filename)"
        Me.optFormat_FDF.UseVisualStyleBackColor = False
        Me.optFormat_PDF.BackColor = System.Drawing.Color.Transparent
        Me.optFormat_PDF.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optFormat_PDF.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.optFormat_PDF.Location = New System.Drawing.Point(32, 72)
        Me.optFormat_PDF.Name = "optFormat_PDF"
        Me.optFormat_PDF.Size = New System.Drawing.Size(184, 24)
        Me.optFormat_PDF.TabIndex = 52
        Me.optFormat_PDF.Text = "Merged Acrobat PDF"
        Me.optFormat_PDF.UseVisualStyleBackColor = False
        Me.lblAttachment_FName.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAttachment_FName.ForeColor = System.Drawing.Color.Black
        Me.lblAttachment_FName.Location = New System.Drawing.Point(24, 281)
        Me.lblAttachment_FName.Name = "lblAttachment_FName"
        Me.lblAttachment_FName.Size = New System.Drawing.Size(482, 30)
        Me.lblAttachment_FName.TabIndex = 40
        Me.lblAttachment_FName.Text = "Attachment File name"
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(24, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(144, 23)
        Me.Label6.TabIndex = 27
        Me.Label6.Text = "Attachment Options"
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(8, 8)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(240, 33)
        Me.Label9.TabIndex = 31
        Me.Label9.Text = "From Name"
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(8, 59)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(232, 28)
        Me.Label10.TabIndex = 32
        Me.Label10.Text = "From Email (required)"
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(269, 240)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(275, 23)
        Me.Label11.TabIndex = 33
        Me.Label11.Text = "*Optional Append PDF Field Name - TO:"
        Me.Label11.Visible = False
        Me.TabControl1.Controls.Add(Me.TabPage6)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Controls.Add(Me.TabPage8)
        Me.TabControl1.Controls.Add(Me.TabPage7)
        Me.TabControl1.Location = New System.Drawing.Point(16, 64)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(570, 477)
        Me.TabControl1.TabIndex = 7
        Me.TabPage6.Controls.Add(Me.btnSupportImport)
        Me.TabPage6.Controls.Add(Me.btnSupportExport)
        Me.TabPage6.Controls.Add(Me.Label25)
        Me.TabPage6.Controls.Add(Me.Button4)
        Me.TabPage6.Controls.Add(Me.Button5)
        Me.TabPage6.Controls.Add(Me.txtSettingName)
        Me.TabPage6.Controls.Add(Me.Label2)
        Me.TabPage6.Controls.Add(Me.Label29)
        Me.TabPage6.Location = New System.Drawing.Point(4, 22)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Size = New System.Drawing.Size(562, 451)
        Me.TabPage6.TabIndex = 5
        Me.TabPage6.Text = "Settings"
        Me.TabPage6.UseVisualStyleBackColor = True
        Me.btnSupportImport.Location = New System.Drawing.Point(32, 264)
        Me.btnSupportImport.Name = "btnSupportImport"
        Me.btnSupportImport.Size = New System.Drawing.Size(224, 40)
        Me.btnSupportImport.TabIndex = 96
        Me.btnSupportImport.Text = "Import Setting"
        Me.btnSupportImport.UseVisualStyleBackColor = True
        Me.btnSupportExport.Location = New System.Drawing.Point(32, 320)
        Me.btnSupportExport.Name = "btnSupportExport"
        Me.btnSupportExport.Size = New System.Drawing.Size(224, 40)
        Me.btnSupportExport.TabIndex = 97
        Me.btnSupportExport.Text = "Export Current Setting"
        Me.btnSupportExport.UseVisualStyleBackColor = True
        Me.Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.Color.Black
        Me.Label25.Location = New System.Drawing.Point(32, 232)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(232, 23)
        Me.Label25.TabIndex = 98
        Me.Label25.Text = "Import && Export Current Setting"
        Me.Button4.Location = New System.Drawing.Point(304, 264)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(224, 40)
        Me.Button4.TabIndex = 93
        Me.Button4.Text = "Import All Settings"
        Me.Button4.UseVisualStyleBackColor = True
        Me.Button5.Location = New System.Drawing.Point(304, 320)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(224, 40)
        Me.Button5.TabIndex = 94
        Me.Button5.Text = "Export All Settings"
        Me.Button5.UseVisualStyleBackColor = True
        Me.txtSettingName.BackColor = System.Drawing.Color.White
        Me.txtSettingName.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSettingName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSettingName.Location = New System.Drawing.Point(40, 48)
        Me.txtSettingName.Name = "txtSettingName"
        Me.txtSettingName.Size = New System.Drawing.Size(496, 30)
        Me.txtSettingName.TabIndex = 80
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(32, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(128, 23)
        Me.Label2.TabIndex = 48
        Me.Label2.Text = "Settings Name"
        Me.Label29.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.Color.Black
        Me.Label29.Location = New System.Drawing.Point(304, 232)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(216, 23)
        Me.Label29.TabIndex = 95
        Me.Label29.Text = "Import && Export All Settings"
        Me.TabPage1.Controls.Add(Me.btnMailServerGmailSettings)
        Me.TabPage1.Controls.Add(Me.GroupBox2)
        Me.TabPage1.Controls.Add(Me.btnTestSMTP)
        Me.TabPage1.Controls.Add(Me.chkSSL)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.txtServer_Port)
        Me.TabPage1.Controls.Add(Me.txtServer_Name)
        Me.TabPage1.Controls.Add(Me.chkExchange)
        Me.TabPage1.Controls.Add(Me.chkFDFTKSetup)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.Label12)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(562, 451)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Mail Server"
        Me.TabPage1.UseVisualStyleBackColor = True
        Me.btnMailServerGmailSettings.Location = New System.Drawing.Point(376, 72)
        Me.btnMailServerGmailSettings.Name = "btnMailServerGmailSettings"
        Me.btnMailServerGmailSettings.Size = New System.Drawing.Size(172, 32)
        Me.btnMailServerGmailSettings.TabIndex = 52
        Me.btnMailServerGmailSettings.Text = "Load GMAIL defaults"
        Me.btnMailServerGmailSettings.UseVisualStyleBackColor = True
        Me.GroupBox2.Controls.Add(Me.SMTP_DELIVERY_METHOD_SPECIFY_TEXT)
        Me.GroupBox2.Controls.Add(Me.SMTP_Delivery_Method_Specify)
        Me.GroupBox2.Controls.Add(Me.SMTP_Delivery_Method_IIS)
        Me.GroupBox2.Controls.Add(Me.SMTP_Delivery_Method_Network)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(248, 280)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(302, 128)
        Me.GroupBox2.TabIndex = 51
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "SMTP Delivery Method"
        Me.SMTP_DELIVERY_METHOD_SPECIFY_TEXT.Location = New System.Drawing.Point(42, 88)
        Me.SMTP_DELIVERY_METHOD_SPECIFY_TEXT.Name = "SMTP_DELIVERY_METHOD_SPECIFY_TEXT"
        Me.SMTP_DELIVERY_METHOD_SPECIFY_TEXT.Size = New System.Drawing.Size(237, 23)
        Me.SMTP_DELIVERY_METHOD_SPECIFY_TEXT.TabIndex = 44
        Me.SMTP_Delivery_Method_Specify.AutoSize = True
        Me.SMTP_Delivery_Method_Specify.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SMTP_Delivery_Method_Specify.Location = New System.Drawing.Point(22, 69)
        Me.SMTP_Delivery_Method_Specify.Name = "SMTP_Delivery_Method_Specify"
        Me.SMTP_Delivery_Method_Specify.Size = New System.Drawing.Size(199, 21)
        Me.SMTP_Delivery_Method_Specify.TabIndex = 43
        Me.SMTP_Delivery_Method_Specify.Text = "Specify Directory for pickup"
        Me.SMTP_Delivery_Method_Specify.UseVisualStyleBackColor = True
        Me.SMTP_Delivery_Method_IIS.AutoSize = True
        Me.SMTP_Delivery_Method_IIS.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SMTP_Delivery_Method_IIS.Location = New System.Drawing.Point(22, 46)
        Me.SMTP_Delivery_Method_IIS.Name = "SMTP_Delivery_Method_IIS"
        Me.SMTP_Delivery_Method_IIS.Size = New System.Drawing.Size(178, 21)
        Me.SMTP_Delivery_Method_IIS.TabIndex = 42
        Me.SMTP_Delivery_Method_IIS.Text = "Pickup directory from IIS"
        Me.SMTP_Delivery_Method_IIS.UseVisualStyleBackColor = True
        Me.SMTP_Delivery_Method_Network.AutoSize = True
        Me.SMTP_Delivery_Method_Network.Checked = True
        Me.SMTP_Delivery_Method_Network.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SMTP_Delivery_Method_Network.Location = New System.Drawing.Point(22, 23)
        Me.SMTP_Delivery_Method_Network.Name = "SMTP_Delivery_Method_Network"
        Me.SMTP_Delivery_Method_Network.Size = New System.Drawing.Size(136, 21)
        Me.SMTP_Delivery_Method_Network.TabIndex = 41
        Me.SMTP_Delivery_Method_Network.TabStop = True
        Me.SMTP_Delivery_Method_Network.Text = "Network (Default)"
        Me.SMTP_Delivery_Method_Network.UseVisualStyleBackColor = True
        Me.btnTestSMTP.ForeColor = System.Drawing.Color.Black
        Me.btnTestSMTP.Location = New System.Drawing.Point(376, 32)
        Me.btnTestSMTP.Name = "btnTestSMTP"
        Me.btnTestSMTP.Size = New System.Drawing.Size(172, 31)
        Me.btnTestSMTP.TabIndex = 50
        Me.btnTestSMTP.Text = "Test SMTP Connection"
        Me.btnTestSMTP.UseVisualStyleBackColor = True
        Me.chkSSL.BackColor = System.Drawing.Color.Transparent
        Me.chkSSL.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSSL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.chkSSL.Location = New System.Drawing.Point(35, 301)
        Me.chkSSL.Name = "chkSSL"
        Me.chkSSL.Size = New System.Drawing.Size(144, 24)
        Me.chkSSL.TabIndex = 48
        Me.chkSSL.Text = "Enable SSL"
        Me.chkSSL.UseVisualStyleBackColor = False
        Me.GroupBox1.Controls.Add(Me.txtDomain_Credential)
        Me.GroupBox1.Controls.Add(Me.lblDomain_Credentials)
        Me.GroupBox1.Controls.Add(Me.txtPassword_Credential)
        Me.GroupBox1.Controls.Add(Me.txtUser_Credential)
        Me.GroupBox1.Controls.Add(Me.lblCred1)
        Me.GroupBox1.Controls.Add(Me.lblCred2)
        Me.GroupBox1.Controls.Add(Me.optSMTP_Auth_Credentials)
        Me.GroupBox1.Controls.Add(Me.txtPassword)
        Me.GroupBox1.Controls.Add(Me.optSMTP_Auth_Basic)
        Me.GroupBox1.Controls.Add(Me.txtUser_Name)
        Me.GroupBox1.Controls.Add(Me.optSMTP_Auth_None)
        Me.GroupBox1.Controls.Add(Me.lblSmtp1)
        Me.GroupBox1.Controls.Add(Me.lblSMTP2)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(8, 112)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(544, 161)
        Me.GroupBox1.TabIndex = 37
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "SMTP Authentication"
        Me.txtDomain_Credential.BackColor = System.Drawing.Color.White
        Me.txtDomain_Credential.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDomain_Credential.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDomain_Credential.Location = New System.Drawing.Point(384, 122)
        Me.txtDomain_Credential.Name = "txtDomain_Credential"
        Me.txtDomain_Credential.Size = New System.Drawing.Size(154, 24)
        Me.txtDomain_Credential.TabIndex = 45
        Me.lblDomain_Credentials.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDomain_Credentials.ForeColor = System.Drawing.Color.Black
        Me.lblDomain_Credentials.Location = New System.Drawing.Point(384, 106)
        Me.lblDomain_Credentials.Name = "lblDomain_Credentials"
        Me.lblDomain_Credentials.Size = New System.Drawing.Size(163, 23)
        Me.lblDomain_Credentials.TabIndex = 50
        Me.lblDomain_Credentials.Text = "Credential Domain (optional)"
        Me.txtPassword_Credential.BackColor = System.Drawing.Color.White
        Me.txtPassword_Credential.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassword_Credential.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPassword_Credential.Location = New System.Drawing.Point(384, 80)
        Me.txtPassword_Credential.Name = "txtPassword_Credential"
        Me.txtPassword_Credential.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword_Credential.Size = New System.Drawing.Size(154, 24)
        Me.txtPassword_Credential.TabIndex = 44
        Me.txtUser_Credential.BackColor = System.Drawing.Color.White
        Me.txtUser_Credential.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUser_Credential.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtUser_Credential.Location = New System.Drawing.Point(384, 40)
        Me.txtUser_Credential.Name = "txtUser_Credential"
        Me.txtUser_Credential.Size = New System.Drawing.Size(154, 24)
        Me.txtUser_Credential.TabIndex = 43
        Me.txtUser_Credential.Text = " "
        Me.lblCred1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCred1.ForeColor = System.Drawing.Color.Black
        Me.lblCred1.Location = New System.Drawing.Point(384, 24)
        Me.lblCred1.Name = "lblCred1"
        Me.lblCred1.Size = New System.Drawing.Size(163, 23)
        Me.lblCred1.TabIndex = 48
        Me.lblCred1.Text = "Credential User name"
        Me.lblCred2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCred2.ForeColor = System.Drawing.Color.Black
        Me.lblCred2.Location = New System.Drawing.Point(384, 64)
        Me.lblCred2.Name = "lblCred2"
        Me.lblCred2.Size = New System.Drawing.Size(163, 23)
        Me.lblCred2.TabIndex = 47
        Me.lblCred2.Text = "Credential Password"
        Me.optSMTP_Auth_Credentials.AutoSize = True
        Me.optSMTP_Auth_Credentials.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optSMTP_Auth_Credentials.Location = New System.Drawing.Point(24, 96)
        Me.optSMTP_Auth_Credentials.Name = "optSMTP_Auth_Credentials"
        Me.optSMTP_Auth_Credentials.Size = New System.Drawing.Size(184, 21)
        Me.optSMTP_Auth_Credentials.TabIndex = 40
        Me.optSMTP_Auth_Credentials.Text = "Credential Authentication"
        Me.optSMTP_Auth_Credentials.UseVisualStyleBackColor = True
        Me.txtPassword.BackColor = System.Drawing.Color.White
        Me.txtPassword.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassword.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPassword.Location = New System.Drawing.Point(216, 80)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(154, 24)
        Me.txtPassword.TabIndex = 42
        Me.optSMTP_Auth_Basic.AutoSize = True
        Me.optSMTP_Auth_Basic.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optSMTP_Auth_Basic.Location = New System.Drawing.Point(24, 64)
        Me.optSMTP_Auth_Basic.Name = "optSMTP_Auth_Basic"
        Me.optSMTP_Auth_Basic.Size = New System.Drawing.Size(154, 21)
        Me.optSMTP_Auth_Basic.TabIndex = 39
        Me.optSMTP_Auth_Basic.Text = "Basic Authentication"
        Me.optSMTP_Auth_Basic.UseVisualStyleBackColor = True
        Me.txtUser_Name.BackColor = System.Drawing.Color.White
        Me.txtUser_Name.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUser_Name.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtUser_Name.Location = New System.Drawing.Point(216, 40)
        Me.txtUser_Name.Name = "txtUser_Name"
        Me.txtUser_Name.Size = New System.Drawing.Size(154, 24)
        Me.txtUser_Name.TabIndex = 41
        Me.txtUser_Name.Text = " "
        Me.optSMTP_Auth_None.AutoSize = True
        Me.optSMTP_Auth_None.Checked = True
        Me.optSMTP_Auth_None.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optSMTP_Auth_None.Location = New System.Drawing.Point(24, 32)
        Me.optSMTP_Auth_None.Name = "optSMTP_Auth_None"
        Me.optSMTP_Auth_None.Size = New System.Drawing.Size(138, 21)
        Me.optSMTP_Auth_None.TabIndex = 38
        Me.optSMTP_Auth_None.TabStop = True
        Me.optSMTP_Auth_None.Text = "No Authentication"
        Me.optSMTP_Auth_None.UseVisualStyleBackColor = True
        Me.lblSmtp1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSmtp1.ForeColor = System.Drawing.Color.Black
        Me.lblSmtp1.Location = New System.Drawing.Point(216, 24)
        Me.lblSmtp1.Name = "lblSmtp1"
        Me.lblSmtp1.Size = New System.Drawing.Size(165, 23)
        Me.lblSmtp1.TabIndex = 44
        Me.lblSmtp1.Text = "SMTP User name Authentication"
        Me.lblSMTP2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSMTP2.ForeColor = System.Drawing.Color.Black
        Me.lblSMTP2.Location = New System.Drawing.Point(216, 64)
        Me.lblSMTP2.Name = "lblSMTP2"
        Me.lblSMTP2.Size = New System.Drawing.Size(165, 23)
        Me.lblSMTP2.TabIndex = 43
        Me.lblSMTP2.Text = "SMTP Password Authentication"
        Me.txtServer_Port.BackColor = System.Drawing.Color.White
        Me.txtServer_Port.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServer_Port.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtServer_Port.Location = New System.Drawing.Point(328, 35)
        Me.txtServer_Port.Name = "txtServer_Port"
        Me.txtServer_Port.Size = New System.Drawing.Size(38, 26)
        Me.txtServer_Port.TabIndex = 36
        Me.txtServer_Port.Text = "25"
        Me.txtServer_Name.BackColor = System.Drawing.Color.White
        Me.txtServer_Name.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServer_Name.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtServer_Name.Location = New System.Drawing.Point(8, 35)
        Me.txtServer_Name.Name = "txtServer_Name"
        Me.txtServer_Name.Size = New System.Drawing.Size(312, 26)
        Me.txtServer_Name.TabIndex = 35
        Me.txtServer_Name.Text = "domain.com"
        Me.chkExchange.BackColor = System.Drawing.Color.Transparent
        Me.chkExchange.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkExchange.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.chkExchange.Location = New System.Drawing.Point(35, 333)
        Me.chkExchange.Name = "chkExchange"
        Me.chkExchange.Size = New System.Drawing.Size(152, 24)
        Me.chkExchange.TabIndex = 47
        Me.chkExchange.Text = "Exchange Server"
        Me.chkExchange.UseVisualStyleBackColor = False
        Me.chkFDFTKSetup.BackColor = System.Drawing.Color.Transparent
        Me.chkFDFTKSetup.Checked = True
        Me.chkFDFTKSetup.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkFDFTKSetup.Enabled = False
        Me.chkFDFTKSetup.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFDFTKSetup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.chkFDFTKSetup.Location = New System.Drawing.Point(35, 365)
        Me.chkFDFTKSetup.Name = "chkFDFTKSetup"
        Me.chkFDFTKSetup.Size = New System.Drawing.Size(192, 24)
        Me.chkFDFTKSetup.TabIndex = 46
        Me.chkFDFTKSetup.Text = "FDFToolkit.net is Installed"
        Me.chkFDFTKSetup.UseVisualStyleBackColor = False
        Me.chkFDFTKSetup.Visible = False
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(328, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(40, 23)
        Me.Label4.TabIndex = 45
        Me.Label4.Text = "Port"
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(8, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(312, 23)
        Me.Label5.TabIndex = 46
        Me.Label5.Text = "SMTP Server Domain, Host name, or IP address"
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(24, 280)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(168, 23)
        Me.Label12.TabIndex = 49
        Me.Label12.Text = "Server Settings"
        Me.TabPage2.Controls.Add(Me.chkEmailMessage_HTMLFormat)
        Me.TabPage2.Controls.Add(Me.txtBCC_Email)
        Me.TabPage2.Controls.Add(Me.txtCC_Email)
        Me.TabPage2.Controls.Add(Me.txtTo_Email)
        Me.TabPage2.Controls.Add(Me.txtMessage_Subject)
        Me.TabPage2.Controls.Add(Me.txtFrom_Email)
        Me.TabPage2.Controls.Add(Me.txtFrom_Name)
        Me.TabPage2.Controls.Add(Me.txtMessage_Body)
        Me.TabPage2.Controls.Add(Me.txtMSG_TO_FIELD)
        Me.TabPage2.Controls.Add(Me.Label11)
        Me.TabPage2.Controls.Add(Me.optRecipients)
        Me.TabPage2.Controls.Add(Me.optRecipients2)
        Me.TabPage2.Controls.Add(Me.Label7)
        Me.TabPage2.Controls.Add(Me.Label17)
        Me.TabPage2.Controls.Add(Me.Label3)
        Me.TabPage2.Controls.Add(Me.Label8)
        Me.TabPage2.Controls.Add(Me.Label10)
        Me.TabPage2.Controls.Add(Me.Label9)
        Me.TabPage2.Controls.Add(Me.Label16)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(562, 451)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Email Message"
        Me.TabPage2.UseVisualStyleBackColor = True
        Me.chkEmailMessage_HTMLFormat.BackColor = System.Drawing.Color.Transparent
        Me.chkEmailMessage_HTMLFormat.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEmailMessage_HTMLFormat.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.chkEmailMessage_HTMLFormat.Location = New System.Drawing.Point(372, 190)
        Me.chkEmailMessage_HTMLFormat.Name = "chkEmailMessage_HTMLFormat"
        Me.chkEmailMessage_HTMLFormat.Size = New System.Drawing.Size(172, 24)
        Me.chkEmailMessage_HTMLFormat.TabIndex = 60
        Me.chkEmailMessage_HTMLFormat.Text = "Body is HTML format"
        Me.chkEmailMessage_HTMLFormat.UseVisualStyleBackColor = False
        Me.txtBCC_Email.BackColor = System.Drawing.Color.White
        Me.txtBCC_Email.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBCC_Email.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtBCC_Email.HideSelection = False
        Me.txtBCC_Email.Location = New System.Drawing.Point(280, 120)
        Me.txtBCC_Email.Name = "txtBCC_Email"
        Me.txtBCC_Email.Size = New System.Drawing.Size(272, 24)
        Me.txtBCC_Email.TabIndex = 59
        Me.txtBCC_Email.WordWrap = False
        Me.txtCC_Email.BackColor = System.Drawing.Color.White
        Me.txtCC_Email.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCC_Email.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCC_Email.HideSelection = False
        Me.txtCC_Email.Location = New System.Drawing.Point(280, 72)
        Me.txtCC_Email.Name = "txtCC_Email"
        Me.txtCC_Email.Size = New System.Drawing.Size(276, 24)
        Me.txtCC_Email.TabIndex = 58
        Me.txtCC_Email.WordWrap = False
        Me.txtTo_Email.BackColor = System.Drawing.Color.White
        Me.txtTo_Email.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTo_Email.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTo_Email.HideSelection = False
        Me.txtTo_Email.Location = New System.Drawing.Point(280, 24)
        Me.txtTo_Email.Name = "txtTo_Email"
        Me.txtTo_Email.Size = New System.Drawing.Size(276, 24)
        Me.txtTo_Email.TabIndex = 57
        Me.txtTo_Email.WordWrap = False
        Me.txtMessage_Subject.BackColor = System.Drawing.Color.White
        Me.txtMessage_Subject.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMessage_Subject.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMessage_Subject.Location = New System.Drawing.Point(8, 160)
        Me.txtMessage_Subject.Name = "txtMessage_Subject"
        Me.txtMessage_Subject.Size = New System.Drawing.Size(544, 24)
        Me.txtMessage_Subject.TabIndex = 52
        Me.txtMessage_Body.AcceptsReturn = True
        Me.txtMessage_Body.BackColor = System.Drawing.Color.White
        Me.txtMessage_Body.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMessage_Body.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMessage_Body.HideSelection = False
        Me.txtMessage_Body.Location = New System.Drawing.Point(8, 216)
        Me.txtMessage_Body.Multiline = True
        Me.txtMessage_Body.Name = "txtMessage_Body"
        Me.txtMessage_Body.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtMessage_Body.Size = New System.Drawing.Size(544, 200)
        Me.txtMessage_Body.TabIndex = 55
        Me.txtMessage_Body.Text = "PDF Form Data - See Attachment"
        Me.txtMessage_Body.WordWrap = False
        Me.txtMSG_TO_FIELD.BackColor = System.Drawing.Color.White
        Me.txtMSG_TO_FIELD.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMSG_TO_FIELD.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMSG_TO_FIELD.HideSelection = False
        Me.txtMSG_TO_FIELD.Location = New System.Drawing.Point(272, 256)
        Me.txtMSG_TO_FIELD.Name = "txtMSG_TO_FIELD"
        Me.txtMSG_TO_FIELD.Size = New System.Drawing.Size(268, 24)
        Me.txtMSG_TO_FIELD.TabIndex = 56
        Me.txtMSG_TO_FIELD.Visible = False
        Me.txtMSG_TO_FIELD.WordWrap = False
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(8, 192)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(192, 31)
        Me.Label7.TabIndex = 51
        Me.Label7.Text = "Message Body (required)"
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(272, 104)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(272, 24)
        Me.Label17.TabIndex = 58
        Me.Label17.Text = "Recipient(s) BCC: Semi-colon Separated"
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(272, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(272, 23)
        Me.Label3.TabIndex = 56
        Me.Label3.Text = "Recipient(s) CC: Semi-colon Separated"
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(8, 136)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(256, 25)
        Me.Label8.TabIndex = 52
        Me.Label8.Text = "Message Subject (required)"
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(272, 8)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(272, 23)
        Me.Label16.TabIndex = 54
        Me.Label16.Text = "Recipient(s) TO: Semi-colon Separated"
        Me.TabPage3.Controls.Add(Me.Label6)
        Me.TabPage3.Controls.Add(Me.Panel1)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(562, 451)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Attachment"
        Me.TabPage3.UseVisualStyleBackColor = True
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(562, 451)
        Me.TabPage4.TabIndex = 8
        Me.TabPage4.Text = "Import Data"
        Me.TabPage4.UseVisualStyleBackColor = True
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Size = New System.Drawing.Size(562, 451)
        Me.TabPage5.TabIndex = 9
        Me.TabPage5.Text = "Filter Data"
        Me.TabPage5.UseVisualStyleBackColor = True
        Me.TabPage8.Location = New System.Drawing.Point(4, 22)
        Me.TabPage8.Name = "TabPage8"
        Me.TabPage8.Size = New System.Drawing.Size(562, 451)
        Me.TabPage8.TabIndex = 10
        Me.TabPage8.Text = "Field Mappings"
        Me.TabPage8.UseVisualStyleBackColor = True
        Me.TabPage7.Controls.Add(Me.Button1)
        Me.TabPage7.Controls.Add(Me.txtSendResults)
        Me.TabPage7.Location = New System.Drawing.Point(4, 22)
        Me.TabPage7.Name = "TabPage7"
        Me.TabPage7.Size = New System.Drawing.Size(562, 451)
        Me.TabPage7.TabIndex = 6
        Me.TabPage7.Text = "Send Messages"
        Me.TabPage7.UseVisualStyleBackColor = True
        Me.Button1.Location = New System.Drawing.Point(8, 8)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(544, 56)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Send Messages"
        Me.Button1.UseVisualStyleBackColor = True
        Me.txtSendResults.AcceptsReturn = True
        Me.txtSendResults.AcceptsTab = True
        Me.txtSendResults.BackColor = System.Drawing.Color.White
        Me.txtSendResults.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSendResults.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSendResults.HideSelection = False
        Me.txtSendResults.Location = New System.Drawing.Point(9, 72)
        Me.txtSendResults.Multiline = True
        Me.txtSendResults.Name = "txtSendResults"
        Me.txtSendResults.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtSendResults.Size = New System.Drawing.Size(544, 335)
        Me.txtSendResults.TabIndex = 57
        Me.txtSendResults.Text = "Send Results:"
        Me.txtSendResults.WordWrap = False
        Me.btnCopy.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnCopy.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnCopy.Location = New System.Drawing.Point(424, 16)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(52, 32)
        Me.btnCopy.TabIndex = 4
        Me.btnCopy.Text = "Copy"
        Me.btnCopy.UseVisualStyleBackColor = False
        Me.FileSystemWatcher1.EnableRaisingEvents = True
        Me.FileSystemWatcher1.SynchronizingObject = Me
        Me.optFormat_None.BackColor = System.Drawing.Color.Transparent
        Me.optFormat_None.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optFormat_None.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.optFormat_None.Location = New System.Drawing.Point(32, 166)
        Me.optFormat_None.Name = "optFormat_None"
        Me.optFormat_None.Size = New System.Drawing.Size(153, 24)
        Me.optFormat_None.TabIndex = 67
        Me.optFormat_None.Text = "no attachment"
        Me.optFormat_None.UseVisualStyleBackColor = False
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(598, 622)
        Me.Controls.Add(Me.btnCopy)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.cmbSettings)
        Me.Controls.Add(Me.lblStatus)
        Me.ForeColor = System.Drawing.Color.Black
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Menu = Me.mnuMain
        Me.Name = "frmEmail"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PDForms: Email Form"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage6.ResumeLayout(False)
        Me.TabPage6.PerformLayout()
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage7.ResumeLayout(False)
        Me.TabPage7.PerformLayout()
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
    End Sub
#End Region
    Public Class EncDec
        Public Shared Function Encrypt(ByVal clearData As Byte(), ByVal Key As Byte(), ByVal IV As Byte()) As Byte()
            Dim ms As New MemoryStream()
            Dim alg As Rijndael = Rijndael.Create()
            alg.Key = Key
            alg.IV = IV
            Dim cs As New CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write)
            cs.Write(clearData, 0, clearData.Length)
            cs.Close()
            Dim encryptedData As Byte() = ms.ToArray()
            Return encryptedData
        End Function
        Public Shared Function Encrypt(ByVal clearText As String, ByVal Password As String) As String
            Dim clearBytes As Byte() = System.Text.Encoding.Unicode.GetBytes(clearText)
            Dim pdb As New Rfc2898DeriveBytes(Password, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
             &H65, &H64, &H76, &H65, &H64, &H65,
             &H76})
            Dim encryptedData As Byte() = Encrypt(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16))
            Return Convert.ToBase64String(encryptedData)
        End Function
        Public Shared Function Encrypt(ByVal clearData As Byte(), ByVal Password As String) As Byte()
            Dim pdb As New Rfc2898DeriveBytes(Password, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
             &H65, &H64, &H76, &H65, &H64, &H65,
             &H76})
            Return Encrypt(clearData, pdb.GetBytes(32), pdb.GetBytes(16))
        End Function
        Public Shared Sub Encrypt(ByVal fileIn As String, ByVal fileOut As String, ByVal Password As String)
            Dim fsIn As New FileStream(fileIn, FileMode.Open, FileAccess.Read)
            Dim fsOut As New FileStream(fileOut, FileMode.OpenOrCreate, FileAccess.Write)
            Dim pdb As New Rfc2898DeriveBytes(Password, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
             &H65, &H64, &H76, &H65, &H64, &H65,
             &H76})
            Dim alg As Rijndael = Rijndael.Create()
            alg.Key = pdb.GetBytes(32)
            alg.IV = pdb.GetBytes(16)
            Dim cs As New CryptoStream(fsOut, alg.CreateEncryptor(), CryptoStreamMode.Write)
            Dim bufferLen As Integer = 4096
            Dim buffer As Byte() = New Byte(bufferLen - 1) {}
            Dim bytesRead As Integer
            Do
                bytesRead = fsIn.Read(buffer, 0, bufferLen)
                cs.Write(buffer, 0, bytesRead)
            Loop While bytesRead <> 0
            cs.Close()
            fsIn.Close()
        End Sub
        Public Shared Function Decrypt(ByVal cipherData As Byte(), ByVal Key As Byte(), ByVal IV As Byte()) As Byte()
            Dim ms As New MemoryStream()
            Dim alg As Rijndael = Rijndael.Create()
            alg.Key = Key
            alg.IV = IV
            Dim cs As New CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write)
            cs.Write(cipherData, 0, cipherData.Length)
            cs.Close()
            Dim decryptedData As Byte() = ms.ToArray()
            Return decryptedData
        End Function
        Public Shared Function Decrypt(ByVal cipherText As String, ByVal Password As String) As String
            Dim cipherBytes As Byte() = Convert.FromBase64String(cipherText)
            Dim pdb As New Rfc2898DeriveBytes(Password, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
             &H65, &H64, &H76, &H65, &H64, &H65,
             &H76})
            Dim decryptedData As Byte() = Decrypt(cipherBytes, pdb.GetBytes(32), pdb.GetBytes(16))
            Return System.Text.Encoding.Unicode.GetString(decryptedData)
        End Function
        Public Shared Function Decrypt(ByVal cipherData As Byte(), ByVal Password As String) As Byte()
            Dim pdb As New Rfc2898DeriveBytes(Password, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
             &H65, &H64, &H76, &H65, &H64, &H65,
             &H76})
            Return Decrypt(cipherData, pdb.GetBytes(32), pdb.GetBytes(16))
        End Function
        Public Shared Sub Decrypt(ByVal fileIn As String, ByVal fileOut As String, ByVal Password As String)
            Dim fsIn As New FileStream(fileIn, FileMode.Open, FileAccess.Read)
            Dim fsOut As New FileStream(fileOut, FileMode.OpenOrCreate, FileAccess.Write)
            Dim pdb As New Rfc2898DeriveBytes(Password, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
             &H65, &H64, &H76, &H65, &H64, &H65,
             &H76})
            Dim alg As Rijndael = Rijndael.Create()
            alg.Key = pdb.GetBytes(32)
            alg.IV = pdb.GetBytes(16)
            Dim cs As New CryptoStream(fsOut, alg.CreateDecryptor(), CryptoStreamMode.Write)
            Dim bufferLen As Integer = 4096
            Dim buffer As Byte() = New Byte(bufferLen - 1) {}
            Dim bytesRead As Integer
            Do
                bytesRead = fsIn.Read(buffer, 0, bufferLen)
                cs.Write(buffer, 0, bytesRead)
            Loop While bytesRead <> 0
            cs.Close()
            fsIn.Close()
        End Sub
        Public Sub New()
        End Sub
    End Class
    Private CopyCode1 As Boolean = False
    Public SessionBytes() As Byte = Nothing
    Public pdfOwnerPassword As String = ""
    Public fieldNames As New List(Of String)
    Public Function getAllFieldNames() As String()
        Try
            fieldNames = New List(Of String)
            If Not SessionBytes Is Nothing Then
                If SessionBytes.Length > 0 Then
                    Dim MySource As New List(Of String)
                    Dim cfdf As New FDFApp.FDFDoc_Class
                    Dim cpdf As New FDFApp.FDFApp_Class
                    cfdf = cpdf.PDFOpenFromBuf(SessionBytes, True, True, pdfOwnerPassword)
                    For Each fld As FDFApp.FDFDoc_Class.FDFField In cfdf.XDPGetAllFields()
                        If Not fld Is Nothing Then
                            If Not String.IsNullOrEmpty(fld.FieldName.ToString.Trim() & "") Then
                                MySource.Add("" & fld.FieldName & "")
                                fieldNames.Add("" & fld.FieldName & "")
                            End If
                        End If
                    Next
                    Return MySource.ToArray
                End If
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
        Return Nothing
    End Function
    Private lv As New List(Of clsAutocomplete)
    Public cfdf As New FDFApp.FDFDoc_Class
    Public cpdf As New FDFApp.FDFApp_Class
    Public Function InjectFieldNameValues(ByVal strInput As String, Optional msg As System.Net.Mail.MailMessage = Nothing, Optional linkedResourcesList As System.Collections.Generic.List(Of System.Net.Mail.LinkedResource) = Nothing) As String
        Dim strTmp As String = strInput & ""
        If String.IsNullOrEmpty(strInput & "") Then
            Return strInput & ""
        End If
        For Each fld As String In fieldNames
            Try
                strTmp = strTmp.Replace("{" & fld.ToString & "}", cfdf.FDFGetValue(fld.ToString.ToString.Replace("{", "").Replace("}", "").ToString(), False) & "")
            Catch ex As Exception
                Err.Clear()
            End Try
        Next
        Try
            If Not msg Is Nothing Then
                Dim r As iTextSharp.text.pdf.PdfReader
                If frmMain1.pdfOwnerPassword = "" Then
                    r = New iTextSharp.text.pdf.PdfReader(SessionBytes)
                Else
                    r = New iTextSharp.text.pdf.PdfReader(SessionBytes, frmMain1.getBytes(pdfOwnerPassword))
                End If
                For p As Integer = 1 To r.NumberOfPages
                    Dim imgBytes() As Byte = frmMain1.A0_LoadImageGhostScript(frmMain1.getPDFBytes(r), pdfOwnerPassword, p, r.GetPageSizeWithRotation(p).Width, r.GetPageSizeWithRotation(p).Height, False)
                    Dim img As System.Drawing.Image = System.Drawing.Image.FromStream(New MemoryStream(imgBytes))
                    Dim imgStream As New MemoryStream
                    img.Save(imgStream, System.Drawing.Imaging.ImageFormat.Png)
                    If imgStream.CanSeek Then
                        imgStream.Seek(0, SeekOrigin.Begin)
                    End If
                    Dim picture As New System.Net.Mail.LinkedResource(imgStream, "image/png")
                    picture.ContentId = "pageImage_" & p.ToString & ""
                    strTmp = strTmp.Replace("""{pageImage_" & p.ToString & "}""", "cid:" & picture.ContentId)
                    linkedResourcesList.Add(picture)
                Next
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
        Return strTmp.ToString & ""
    End Function
    Public Sub addAutoCompleteFields(ByVal sessionBytesTemp() As Byte, ByVal pdfownerPasswordTemp As String, ByRef frmMain2 As frmMain)
        Try
            SessionBytes = sessionBytesTemp
            pdfOwnerPassword = pdfownerPasswordTemp
            cfdf = cpdf.PDFOpenFromBuf(SessionBytes, True, True, pdfOwnerPassword)
            Dim MySource As New AutoCompleteStringCollection()
            For Each fld As String In getAllFieldNames()
                If Not String.IsNullOrEmpty(fld.ToString.Trim() & "") Then
                    MySource.Add("{" & fld.ToString.Trim() & "}")
                End If
            Next
            lv.Add(New clsAutocomplete(txtMessage_Body, fieldNames.ToArray(), True, True, True, frmMain2, "{", "}"))
            lv.Add(New clsAutocomplete(txtFrom_Name, fieldNames.ToArray(), False))
            lv.Add(New clsAutocomplete(txtFrom_Email, fieldNames.ToArray(), False))
            lv.Add(New clsAutocomplete(txtTo_Email, fieldNames.ToArray(), False))
            lv.Add(New clsAutocomplete(txtCC_Email, fieldNames.ToArray(), False))
            lv.Add(New clsAutocomplete(txtBCC_Email, fieldNames.ToArray(), False))
            lv.Add(New clsAutocomplete(txtFrom_Email, fieldNames.ToArray(), False))
            lv.Add(New clsAutocomplete(txtMessage_Subject, fieldNames.ToArray(), False))
            lv.Add(New clsAutocomplete(txtFrom_Email, fieldNames.ToArray(), False))
            lv.Add(New clsAutocomplete(txtAttachmentFName, fieldNames.ToArray(), False))
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public Sub frm_Close()
        For l As Integer = 0 To lv.Count - 1
            Try
                lv(l).closeDispose()
                Me.Controls.Remove(lv(l))
                lv(l) = Nothing
            Catch ex As Exception
                Err.Clear()
            End Try
        Next
        Try
            If Not Me.Owner Is Nothing Then
                If TypeOf (Me.Owner) Is frmMain Then
                    Me.Owner.Show()
                    Me.Owner.BringToFront()
                End If
            End If
        Catch ex As Exception
            Err.Clear()
        Finally
        End Try
    End Sub
    Private Sub frmEmail_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        frm_Close()
    End Sub
    Public pageRange As String = ""
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Load_Settings()
            lblStatus.Text = "Status: Ready"
            lblStatus.ForeColor = Color.DarkBlue
        Catch ex As Exception
            lblStatus.Text = "ERROR: Contact Support: " & ex.Message()
            lblStatus.ForeColor = Color.DarkRed
        End Try
    End Sub
    Private Function Check_Settings(ByVal path As String) As String
        If Not File.Exists(path) Then
            Dim dsTemp As New DataSet
            dsTemp.ReadXml(Application.StartupPath & "\settings_template.xml", XmlReadMode.ReadSchema)
            dsTemp.WriteXml(path, XmlWriteMode.WriteSchema)
            btnAdd.Enabled = False
            cmbSettings.SelectedIndex = -1
            cmbSettings.Enabled = False
            Me.txtFrom_Email.Text = ""
            Me.txtFrom_Name.Text = ""
            Me.txtMessage_Body.Text = ""
            Me.txtPassword.Text = ""
            Me.txtServer_Name.Text = "localhost"
            Me.txtServer_Port.Text = "25"
            Me.txtUser_Name.Text = ""
            Me.optFormat_FDF.Checked = True
            Me.txtPDF_ModifyPassword.Text = ""
            Me.txtPDF_OpenPassword.Text = ""
            Me.chkFlatten_PDF.Checked = False
            Me.chkExchange.Checked = False
            Me.chkFDFTKSetup.Checked = True
            Me.txtUser_Credential.Text = ""
            Me.txtPassword_Credential.Text = ""
            Me.txtTo_Email.Text = ""
            Me.optSMTP_Auth_None.Checked = True
            Me.txtPDFURL.Text = ""
            Me.chkSSL.Checked = False
            Me.txtAttachmentFName.Text = "PDFAttachment.pdf"
            Me.txtCC_Email.Text = ""
            Me.txtBCC_Email.Text = ""
            Me.txtDomain_Credential.Text = ""
            Me.txtMSG_TO_FIELD.Text = ""
            Me.txtSettingName.Text = "Default Setting"
            Me.lblStatus.Text = "Status: Click ""UPDATE"" When Done"
            btnAdd.Enabled = False
            Return path
        Else
            Dim dsCheck As New DataSet, dt As New DataTable
            Dim dsTemp As New DataSet
            Dim s As String = Application.StartupPath
            dsTemp.ReadXml(Application.StartupPath & "\settings_template.xml", XmlReadMode.ReadSchema)
            dsCheck.ReadXml(path, XmlReadMode.ReadSchema)
            dsCheck.WriteXml(path, XmlWriteMode.WriteSchema)
            For Each colTemp As DataColumn In dsTemp.Tables(0).Columns
                For Each col As DataColumn In dsCheck.Tables(0).Columns
                    If Not Check_Col(dsCheck, colTemp.ColumnName) Then
                        Dim newCol As New DataColumn
                        newCol.ColumnName = colTemp.ColumnName
                        newCol.DataType = colTemp.DataType
                        newCol.DefaultValue = colTemp.DefaultValue
                        newCol.Unique = colTemp.Unique
                        newCol.AllowDBNull = colTemp.AllowDBNull
                        dsCheck.Tables(0).Columns.Add(newCol)
                        For Each drX As DataRow In dsCheck.Tables(0).Rows
                            drX(newCol.ColumnName) = vbNull
                        Next
                        Exit For
                    End If
                Next
            Next
            dsCheck.WriteXml(path, XmlWriteMode.WriteSchema)
            Return path
        End If
    End Function
    Private Function Check_Key(ByVal pks As DataColumn(), ByVal columnname As String, ByVal newpks As DataColumn()) As Boolean
        For Each dc As DataColumn In pks
            If dc.ColumnName.ToLower = columnname.ToLower Then
                Return True
            End If
        Next
        Return False
    End Function
    Private Function Check_Col(ByVal ds As DataSet, ByVal colname As String) As Boolean
        For Each col As DataColumn In ds.Tables(0).Columns
            If col.ColumnName.ToLower = colname.ToLower Then
                Return True
            End If
        Next
        Return False
    End Function
    Private Function ContainsProjectName(ByVal ds As DataSet, ByVal strProjectName As String) As Boolean
        Dim dv As DataView = ds.Tables(0).DefaultView
        dv.RowFilter = "Setting_Name = '" & strProjectName & "'"
        If dv.Count <= 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Private Shared Sub CopyDirectory(ByVal sourcePath As String, ByVal destPath As String)
        If Not Directory.Exists(destPath) Then
            Directory.CreateDirectory(destPath)
        End If
        For Each file__1 As String In Directory.GetFiles(sourcePath)
            Dim dest As String = Path.Combine(destPath, Path.GetFileName(file__1))
            File.Copy(file__1, dest)
        Next
        For Each folder As String In Directory.GetDirectories(sourcePath)
            Dim dest As String = Path.Combine(destPath, Path.GetFileName(folder))
            CopyDirectory(folder, dest)
        Next
    End Sub
    Public ReadOnly Property ApplicationDataFolder(Optional ByVal TrimEnd As Boolean = False) As String
        Get
            Return DirectCast(Me.Owner, frmMain).ApplicationDataFolder(False, "").ToString()
        End Get
    End Property
    Public Sub AddEveryoneToPathACL(ByVal path As String)
        Dim sec As System.Security.AccessControl.DirectorySecurity = Directory.GetAccessControl(path)
        Dim everyone As System.Security.Principal.SecurityIdentifier = New System.Security.Principal.SecurityIdentifier(Security.Principal.WellKnownSidType.WorldSid, Nothing)
        sec.AddAccessRule(New System.Security.AccessControl.FileSystemAccessRule(everyone, Security.AccessControl.FileSystemRights.FullControl, Security.AccessControl.InheritanceFlags.ContainerInherit + Security.AccessControl.InheritanceFlags.ObjectInherit, Security.AccessControl.PropagationFlags.InheritOnly, Security.AccessControl.AccessControlType.Allow))
        Directory.SetAccessControl(path, sec)
    End Sub
    Private Sub Load_Settings(Optional ByVal lstIndex As Integer = -1)
        Dim ds As New System.Data.DataSet
        btnAdd.Enabled = True
        ds.ReadXml(Check_Settings(ApplicationDataFolder & "settings.xml"), XmlReadMode.ReadSchema)
        cmbSettings.DisplayMember = "Setting_Name"
        cmbSettings.ValueMember = "Setting_Name"
        cmbSettings.DataSource = ds.Tables(0).DefaultView
        cmbSettings.Enabled = True
        If Not ds.Tables(0).Columns.Contains("SMTP_Delivery") Then
            ds.Tables(0).Columns.Add(New System.Data.DataColumn("SMTP_Delivery", GetType(Integer)))
            ds.Tables(0).Columns("SMTP_Delivery").DefaultValue = 0
            ds.Tables(0).Columns.Add(New System.Data.DataColumn("SMTP_Delivery_Specify", GetType(String)))
            ds.Tables(0).Columns("SMTP_Delivery_Specify").DefaultValue = ""
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                ds.Tables(0).Rows(i)("SMTP_Delivery") = 0
                ds.Tables(0).Rows(i)("SMTP_Delivery_Specify") = ""
            Next
            ds.WriteXml(Check_Settings(ApplicationDataFolder & "settings.xml"), XmlWriteMode.WriteSchema)
        End If
        If lstIndex >= 0 Then
            Me.cmbSettings.SelectedIndex = lstIndex
        End If
        Try
            If Me.cmbSettings.Items.Count <= 0 Then
                Me.btnAdd_Click(Me, New EventArgs())
                lblStatus.Text = "Status: To add a new settings, Click the update button when done."
                MsgBox("To add a new settings, Click the update button when done.", MsgBoxStyle.OkOnly + MsgBoxStyle.ApplicationModal, "ADD NEW SETTING")
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub Reset_Form()
        Try
            lblStatus.Text = "Status: Ready" & vbNewLine & vbNewLine & "You need to run your own Microsoft IIS Web server in order to use this script." & vbNewLine & vbNewLine & "Please download the Setup Guide for instructions"
        Catch ex As Exception
            lblStatus.Text = "Status: " & ex.Message
        End Try
    End Sub
    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Reset_Form()
    End Sub
    Private Sub MenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem3.Click
        Try
            Me.Close()
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub optRecipients2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optRecipients2.CheckedChanged
        If optRecipients2.Checked Then
            txtMessage_Body.Enabled = False
        Else
            txtMessage_Body.Enabled = True
        End If
    End Sub
    Private Sub cmbSettings_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSettings.SelectedIndexChanged
        If cmbSettings.Enabled = False Then
            Me.txtSettingName.Text = ""
            Me.txtFrom_Email.Text = ""
            Me.txtFrom_Name.Text = ""
            Me.txtMessage_Body.Text = ""
            Me.txtPassword.Text = ""
            Me.txtServer_Name.Text = ""
            Me.txtServer_Port.Text = ""
            Me.txtUser_Name.Text = ""
            Me.txtMessage_Subject.Text = ""
            Me.optFormat_FDF.Checked = True
            Me.optFormat_PDF.Checked = False
            Me.optFormat_xFDF.Checked = False
            Me.optFormat_XML.Checked = False
            Me.optFormat_RAW.Checked = False
            Me.optFormat_HTML_Inline.Checked = False
            Me.optFormat_HTML_Attachment.Checked = False
            Me.chkExchange.Checked = False
            Me.chkFDFTKSetup.Checked = True
            Me.chkFlatten_PDF.Checked = False
            Me.txtPDF_ModifyPassword.Text = ""
            Me.txtPDF_OpenPassword.Text = ""
            Me.txtUser_Credential.Text = ""
            Me.txtPassword_Credential.Text = ""
            Me.txtTo_Email.Text = ""
            Me.optSMTP_Auth_None.Checked = True
            Me.txtPDFURL.Text = ""
            Me.chkSSL.Checked = False
            Me.txtSettingName.Text = ""
            Me.txtAttachmentFName.Text = ""
            Me.txtTo_Email.Text = ""
            Me.txtBCC_Email.Text = ""
            Me.txtCC_Email.Text = ""
            Me.txtDomain_Credential.Text = ""
            Me.txtMSG_TO_FIELD.Text = ""
            lblStatus.Text = "Status: Error - No settings loaded!"
        End If
        Try
            Dim ds As New System.Data.DataSet
            Dim drow As System.Data.DataRow
            ds.ReadXml(ApplicationDataFolder & "settings.xml", XmlReadMode.ReadSchema)
            drow = ds.Tables(0).Rows(cmbSettings.SelectedIndex)
            Me.txtSettingName.Text = drow("Setting_Name") & ""
            Me.txtFrom_Email.Text = drow("From_Email") & ""
            Me.txtFrom_Name.Text = drow("From_Name") & ""
            Me.txtMessage_Body.Text = drow("Message_Body") & ""
            Me.txtServer_Name.Text = drow("Server_Name") & ""
            Me.txtServer_Port.Text = drow("Server_Port") & ""
            Me.txtUser_Name.Text = drow("User_Name") & ""
            Me.txtPassword.Text = drow("Password") & ""
            Me.txtMessage_Subject.Text = drow("Message_Subject") & ""
            Select Case CStr(drow("Attachment_Format") & "").ToLower
                Case "fdf"
                    Me.optFormat_FDF.Checked = True
                Case "pdf"
                    Me.optFormat_PDF.Checked = True
                Case "xfdf"
                    Me.optFormat_xFDF.Checked = True
                Case "xml"
                    Me.optFormat_XML.Checked = True
                Case "xdp"
                    Me.optFormat_XDP.Checked = True
                Case "xpdf"
                    Me.optFormat_XPDF.Checked = True
                Case "raw"
                    Me.optFormat_RAW.Checked = True
                Case "html"
                    Me.optFormat_HTML_Attachment.Checked = True
                Case "html-inline"
                    Me.optFormat_HTML_Inline.Checked = True
                Case Else
                    Me.optFormat_FDF.Checked = True
            End Select
            Me.txtPDF_ModifyPassword.Text = drow("Modify_PDF") & ""
            Me.txtPDF_OpenPassword.Text = drow("Open_PDF") & ""
            Me.chkFlatten_PDF.Checked = drow("Flatten_PDF")
            Me.chkExchange.Checked = CBool(drow("Exchange_Server"))
            Me.chkFDFTKSetup.Checked = CBool(drow("FDFToolkit_Installed"))
            Me.txtUser_Credential.Text = drow("Credential_User_Name") & ""
            Me.txtPassword_Credential.Text = drow("Credential_Password") & ""
            Me.txtDomain_Credential.Text = drow("Credential_Domain") & ""
            Me.txtTo_Email.Text = drow("To_Email") & ""
            Me.txtBCC_Email.Text = drow("BCC_Email") & ""
            Me.txtCC_Email.Text = drow("CC_Email") & ""
            Me.txtMSG_TO_FIELD.Text = drow("MSG_TO_FIELD") & ""
            If drow("SMTP_Authentication") = 0 Then
                Me.optSMTP_Auth_None.Checked = True
            ElseIf drow("SMTP_Authentication") = 1 Then
                Me.optSMTP_Auth_Basic.Checked = True
            ElseIf drow("SMTP_Authentication") = 2 Then
                Me.optSMTP_Auth_Credentials.Checked = True
            End If
            Try
                If drow("SMTP_Delivery") = 2 Then
                    Me.SMTP_Delivery_Method_IIS.Checked = True
                ElseIf drow("SMTP_Delivery") = 1 Then
                    Me.SMTP_Delivery_Method_Specify.Checked = True
                ElseIf drow("SMTP_Delivery") = 0 Then
                    Me.SMTP_Delivery_Method_Network.Checked = True
                End If
                Me.SMTP_DELIVERY_METHOD_SPECIFY_TEXT.Text = drow("SMTP_Delivery_Specify") & ""
            Catch ex As Exception
                Err.Clear()
            End Try
            Me.txtPDFURL.Text = drow("PDF_URL") & ""
            Me.chkSSL.Checked = CBool(drow("SMTP_SSL"))
            Me.txtSettingName.Text = drow("Setting_Name") & ""
            Me.txtAttachmentFName.Text = drow("Attachment_Filename") & ""
            lblStatus.Text = "Status: Loaded Settings"
        Catch ex As Exception
            lblStatus.Text = "Status: Error Loading Settings" & vbNewLine & vbNewLine & ex.Message
        End Try
    End Sub
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If cmbSettings.Enabled = True Then
            btnAdd.Enabled = False
            cmbSettings.Enabled = False
            Me.txtFrom_Email.Text = ""
            Me.txtFrom_Name.Text = ""
            Me.txtMessage_Body.Text = ""
            Me.txtPassword.Text = ""
            Me.txtServer_Name.Text = "localhost"
            Me.txtServer_Port.Text = "25"
            Me.txtUser_Name.Text = ""
            Me.optFormat_FDF.Checked = True
            Me.txtPDF_ModifyPassword.Text = ""
            Me.txtPDF_OpenPassword.Text = ""
            Me.chkFlatten_PDF.Checked = False
            Me.chkExchange.Checked = False
            Me.chkFDFTKSetup.Checked = True
            Me.txtUser_Credential.Text = ""
            Me.txtPassword_Credential.Text = ""
            Me.txtTo_Email.Text = ""
            Me.optSMTP_Auth_None.Checked = True
            Me.txtPDFURL.Text = ""
            Me.chkSSL.Checked = False
            Me.txtAttachmentFName.Text = "PDFAttachment.pdf"
            Me.txtTo_Email.Text = ""
            Me.txtCC_Email.Text = ""
            Me.txtBCC_Email.Text = ""
            Me.txtDomain_Credential.Text = ""
            Me.txtMSG_TO_FIELD.Text = ""
            Try
                Me.SMTP_Delivery_Method_Network.Checked = True
                Me.SMTP_Delivery_Method_IIS.Checked = False
                Me.SMTP_Delivery_Method_Specify.Checked = False
                Me.SMTP_DELIVERY_METHOD_SPECIFY_TEXT.Text = ""
            Catch ex As Exception
                Err.Clear()
            End Try
            Me.txtSettingName.Text = "Default Setting"
            Me.lblStatus.Text = "Status: Click ""UPDATE"" When Done"
            Exit Sub
        Else
            btnUpdate_Click(sender, e)
            btnAdd.Enabled = True
        End If
    End Sub
    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Dim ds As New System.Data.DataSet
RETURN_ADDFIELDS:
        btnAdd.Enabled = True
        ds.ReadXml(ApplicationDataFolder & "settings.xml", XmlReadMode.ReadSchema)
        Dim drow As System.Data.DataRow
        Try
            If cmbSettings.Enabled = False Then
                lblStatus.Text = "Status: Added new Setting"
                drow = ds.Tables(0).NewRow
            Else
                lblStatus.Text = "Status: Updated Settings"
                drow = ds.Tables(0).Rows(cmbSettings.SelectedIndex)
            End If
            drow("From_Email") = Me.txtFrom_Email.Text & ""
            drow("From_Name") = Me.txtFrom_Name.Text & ""
            drow("Message_Body") = Me.txtMessage_Body.Text & ""
            drow("Server_Name") = Me.txtServer_Name.Text & ""
            drow("Server_Port") = Me.txtServer_Port.Text & ""
            drow("User_Name") = Me.txtUser_Name.Text & ""
            drow("Password") = Me.txtPassword.Text & ""
            drow("Message_Subject") = Me.txtMessage_Subject.Text & ""
            If Me.optFormat_FDF.Checked Then
                drow("Attachment_Format") = "fdf"
            ElseIf Me.optFormat_PDF.Checked Then
                drow("Attachment_Format") = "pdf"
            ElseIf Me.optFormat_xFDF.Checked Then
                drow("Attachment_Format") = "xfdf"
            ElseIf Me.optFormat_XML.Checked Then
                drow("Attachment_Format") = "xml"
            ElseIf Me.optFormat_XDP.Checked Then
                drow("Attachment_Format") = "xdp"
            ElseIf Me.optFormat_XPDF.Checked Then
                drow("Attachment_Format") = "xpdf"
            ElseIf Me.optFormat_RAW.Checked Then
                drow("Attachment_Format") = "raw"
            ElseIf Me.optFormat_HTML_Attachment.Checked Then
                drow("Attachment_Format") = "html"
            ElseIf Me.optFormat_HTML_Inline.Checked Then
                drow("Attachment_Format") = "html-inline"
            Else
                drow("Attachment_Format") = "pdf"
            End If
            drow("Exchange_Server") = Me.chkExchange.Checked
            drow("FDFToolkit_Installed") = Me.chkFDFTKSetup.Checked
            drow("Modify_PDF") = Me.txtPDF_ModifyPassword.Text & ""
            drow("Open_PDF") = Me.txtPDF_OpenPassword.Text & ""
            drow("Flatten_PDF") = Me.chkFlatten_PDF.Checked
            drow("Credential_User_Name") = Me.txtUser_Credential.Text & ""
            drow("Credential_Password") = Me.txtPassword_Credential.Text & ""
            drow("Credential_Domain") = Me.txtDomain_Credential.Text & ""
            drow("To_Email") = Me.txtTo_Email.Text & ""
            If Me.optSMTP_Auth_None.Checked Then
                drow("SMTP_Authentication") = 0
            ElseIf Me.optSMTP_Auth_Basic.Checked Then
                drow("SMTP_Authentication") = 1
            ElseIf Me.optSMTP_Auth_Credentials.Checked Then
                drow("SMTP_Authentication") = 2
            End If
            Try
                If Me.SMTP_Delivery_Method_Network.Checked Then
                    drow("SMTP_Delivery") = 0
                ElseIf Me.SMTP_Delivery_Method_Specify.Checked Then
                    drow("SMTP_Delivery") = 1
                ElseIf Me.SMTP_Delivery_Method_IIS.Checked Then
                    drow("SMTP_Delivery") = 2
                End If
                drow("SMTP_Delivery_Specify") = Me.SMTP_DELIVERY_METHOD_SPECIFY_TEXT.Text & ""
            Catch ex As Exception
                Err.Clear()
            End Try
            drow("Sent_URL") = ""
            drow("Error_URL") = ""
            drow("Sent_Msg") = ""
            drow("Error_Msg") = ""
            drow("PDF_URL") = Me.txtPDFURL.Text & ""
            drow("SMTP_SSL") = Me.chkSSL.Checked
            drow("Setting_Name") = Me.txtSettingName.Text & ""
            drow("Attachment_Filename") = Me.txtAttachmentFName.Text & ""
            drow("TO_Email") = Me.txtTo_Email.Text & ""
            drow("BCC_Email") = Me.txtBCC_Email.Text & ""
            drow("CC_Email") = Me.txtCC_Email.Text & ""
            drow("MSG_TO_FIELD") = Me.txtMSG_TO_FIELD.Text & ""
            drow("Target_Frame") = ""
            If cmbSettings.Enabled = False Then
                ds.Tables(0).Rows.Add(drow)
                ds.WriteXml(ApplicationDataFolder & "settings.xml", XmlWriteMode.WriteSchema)
                cmbSettings.Enabled = True
                Load_Settings()
                cmbSettings.Enabled = True
                Me.cmbSettings.SelectedIndex = Me.cmbSettings.Items.Count - 1
                lblStatus.Text = "Status: Created new setting."
            Else
                ds.WriteXml(ApplicationDataFolder & "settings.xml", XmlWriteMode.WriteSchema)
                Load_Settings(Me.cmbSettings.SelectedIndex)
                cmbSettings.Enabled = True
                lblStatus.Text = "Status: Updated setting."
            End If
        Catch ex As Exception
            cmbSettings.Enabled = True
            Load_Settings()
            lblStatus.Text = "Status: Error Updating Settings" & vbNewLine & vbNewLine & ex.Message
        End Try
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        btnAdd.Enabled = True
        Load_Settings()
        cmbSettings.Enabled = True
        lblStatus.Text = "Status: Ready" & vbNewLine & vbNewLine & "You need a Microsoft .Net 1.1 enabled web server to use this script." & vbNewLine & vbNewLine & "For more information, please view the Setup Guide for instructions."
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim _resp As DialogResult = MsgBox("Are you sure you want to delete this record?", MsgBoxStyle.YesNo, "Confirm Deletion")
        If _resp <> Windows.Forms.DialogResult.Yes And _resp <> Windows.Forms.DialogResult.OK Then
            lblStatus.Text = "Status: Error Deletion Cancelled"
            Return
        End If
        Dim ds As New System.Data.DataSet
        ds.ReadXml(ApplicationDataFolder & "settings.xml", XmlReadMode.ReadSchema)
        btnAdd.Enabled = True
        Dim drow As System.Data.DataRow
        Try
            If cmbSettings.Enabled = False Then
                Exit Sub
                Load_Settings()
            Else
                drow = ds.Tables(0).Rows(cmbSettings.SelectedIndex)
                drow.Delete()
                ds.WriteXml(ApplicationDataFolder & "settings.xml", XmlWriteMode.WriteSchema)
                cmbSettings.Enabled = True
                Load_Settings()
                lblStatus.Text = "Status: Deleted Setting"
            End If
        Catch ex As Exception
            cmbSettings.Enabled = True
            Load_Settings()
            lblStatus.Text = "Status: Error Deleting Setting" & vbNewLine & ex.Message
        End Try
    End Sub
    Private Sub frmMain_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
    End Sub
    Private Sub Open_File(ByVal strPath As String)
        If strPath <> "" Then
            Process.Start(strPath)
        End If
    End Sub
    Private Sub MenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Open_File(Application.StartupPath & "\HELP\Server_Setup.pdf")
    End Sub
    Private Sub MenuItem6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Open_File("http://www.nk-inc.com/support/")
    End Sub
    Private Sub MenuItem8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Open_File("http://www.nk-inc.com/software/pdfemail.net2/")
    End Sub
    Private Sub MenuItem9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Hide()
    End Sub
    Private Sub lnkHelp_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        Try
        Catch ex As Exception
        End Try
    End Sub
    Private Sub optRecipients_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optRecipients.CheckedChanged
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Show()
    End Sub
    Private Sub txtServer_Name_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub optFormat_PDF_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optFormat_PDF.CheckedChanged
        Update_OptFormat()
    End Sub
    Private Sub Update_OptFormat()
        Me.lblPDFModifyPW.Visible = optFormat_PDF.Checked
        Me.lblPDFOpenPW.Visible = optFormat_PDF.Checked
        Me.txtPDF_ModifyPassword.Visible = optFormat_PDF.Checked
        Me.txtPDF_OpenPassword.Visible = optFormat_PDF.Checked
        Me.chkFlatten_PDF.Visible = optFormat_PDF.Checked
        Me.lblPDFURL.Visible = True
        Me.txtPDFURL.Visible = True
        If String.IsNullOrEmpty(txtAttachmentFName.Text & "") Then
            txtAttachmentFName.Text = "PDFAttachment.pdf"
        End If
        If optFormat_FDF.Checked Then
            If txtAttachmentFName.Text.ToString.Contains("."c) Then
                txtAttachmentFName.Text = CStr(txtAttachmentFName.Text).Substring(0, txtAttachmentFName.Text.LastIndexOf("."c)) & ".fdf"
            Else
                txtAttachmentFName.Text = txtAttachmentFName.Text & ".fdf"
            End If
        ElseIf optFormat_PDF.Checked Then
            If txtAttachmentFName.Text.ToString.Contains("."c) Then
                txtAttachmentFName.Text = CStr(txtAttachmentFName.Text).Substring(0, txtAttachmentFName.Text.LastIndexOf("."c)) & ".pdf"
            Else
                txtAttachmentFName.Text = txtAttachmentFName.Text & ".pdf"
            End If
        ElseIf optFormat_RAW.Checked Then
            If txtAttachmentFName.Text.ToString.Contains("."c) Then
            Else
            End If
        ElseIf optFormat_XDP.Checked Then
            If txtAttachmentFName.Text.ToString.Contains("."c) Then
                txtAttachmentFName.Text = CStr(txtAttachmentFName.Text).Substring(0, txtAttachmentFName.Text.LastIndexOf("."c)) & ".xdp"
            Else
                txtAttachmentFName.Text = txtAttachmentFName.Text & ".xdp"
            End If
        ElseIf optFormat_xFDF.Checked Then
            If txtAttachmentFName.Text.ToString.Contains("."c) Then
                txtAttachmentFName.Text = CStr(txtAttachmentFName.Text).Substring(0, txtAttachmentFName.Text.LastIndexOf("."c)) & ".xfdf"
            Else
                txtAttachmentFName.Text = txtAttachmentFName.Text & ".xfdf"
            End If
        ElseIf optFormat_XML.Checked Then
            If txtAttachmentFName.Text.ToString.Contains("."c) Then
                txtAttachmentFName.Text = CStr(txtAttachmentFName.Text).Substring(0, txtAttachmentFName.Text.LastIndexOf("."c)) & ".xml"
            Else
                txtAttachmentFName.Text = txtAttachmentFName.Text & ".xml"
            End If
        ElseIf optFormat_HTML_Attachment.Checked Then
            If txtAttachmentFName.Text.ToString.Contains("."c) Then
                txtAttachmentFName.Text = CStr(txtAttachmentFName.Text).Substring(0, txtAttachmentFName.Text.LastIndexOf("."c)) & ".html"
            Else
                txtAttachmentFName.Text = txtAttachmentFName.Text & ".html"
            End If
        ElseIf optFormat_HTML_Inline.Checked Then
            If txtAttachmentFName.Text.ToString.Contains("."c) Then
                txtAttachmentFName.Text = CStr(txtAttachmentFName.Text).Substring(0, txtAttachmentFName.Text.LastIndexOf("."c)) & ".html"
            Else
                txtAttachmentFName.Text = txtAttachmentFName.Text & ".html"
            End If
        ElseIf optFormat_XPDF.Checked Then
            If txtAttachmentFName.Text.ToString.Contains("."c) Then
                txtAttachmentFName.Text = CStr(txtAttachmentFName.Text).Substring(0, txtAttachmentFName.Text.LastIndexOf("."c)) & ".pdf"
            Else
                txtAttachmentFName.Text = txtAttachmentFName.Text & ".pdf"
            End If
        Else
            If txtAttachmentFName.Text.ToString.Contains("."c) Then
            Else
            End If
        End If
        Try
            Me.lblPDFModifyPW.Visible = IIf(Me.optFormat_PDF.Checked Or Me.optFormat_XPDF.Checked Or Me.optFormat_RAW.Checked, True, False)
            Me.lblPDFOpenPW.Visible = IIf(Me.optFormat_PDF.Checked Or Me.optFormat_XPDF.Checked Or Me.optFormat_RAW.Checked, True, False)
            Me.txtPDF_ModifyPassword.Visible = IIf(Me.optFormat_PDF.Checked Or Me.optFormat_XPDF.Checked Or Me.optFormat_RAW.Checked, True, False)
            Me.txtPDF_OpenPassword.Visible = IIf(Me.optFormat_PDF.Checked Or Me.optFormat_XPDF.Checked Or Me.optFormat_RAW.Checked, True, False)
            Me.chkFlatten_PDF.Visible = IIf(Me.optFormat_PDF.Checked Or Me.optFormat_XPDF.Checked Or Me.optFormat_RAW.Checked, True, False)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub optFormat_FDF_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optFormat_FDF.CheckedChanged
        Update_OptFormat()
    End Sub
    Private Sub optFormat_xFDF_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optFormat_xFDF.CheckedChanged
        Update_OptFormat()
    End Sub
    Private Sub optFormat_XML_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optFormat_XML.CheckedChanged
        Update_OptFormat()
    End Sub
    Public Function getLicenseEncrypted(ByVal strLicense As String) As String
        Try
            Return System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes((System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(strLicense & "")).Replace(" ", "-")))).Replace(" ", "-").Replace("=", "+") & "b642"
        Catch ex As Exception
            Return ""
        End Try
        Return ""
    End Function
    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optFormat_XDP.CheckedChanged
        Update_OptFormat()
    End Sub
    Private Sub optSMTP_Auth_None_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optSMTP_Auth_None.CheckedChanged
        Update_SMTPOptions()
    End Sub
    Private Sub optSMTP_Auth_Basic_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optSMTP_Auth_Basic.CheckedChanged
        Update_SMTPOptions()
    End Sub
    Private Sub optSMTP_Auth_Credentials_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optSMTP_Auth_Credentials.CheckedChanged
        Update_SMTPOptions()
    End Sub
    Private Sub Update_SMTPOptions()
        Me.lblCred1.Enabled = Me.optSMTP_Auth_Credentials.Checked
        Me.lblCred2.Enabled = Me.optSMTP_Auth_Credentials.Checked
        Me.lblDomain_Credentials.Enabled = Me.optSMTP_Auth_Credentials.Checked
        Me.txtUser_Credential.Enabled = Me.optSMTP_Auth_Credentials.Checked
        Me.txtPassword_Credential.Enabled = Me.optSMTP_Auth_Credentials.Checked
        Me.txtDomain_Credential.Enabled = Me.optSMTP_Auth_Credentials.Checked
        Me.lblSmtp1.Enabled = Not Me.optSMTP_Auth_None.Checked
        Me.lblSMTP2.Enabled = Not Me.optSMTP_Auth_None.Checked
        Me.txtUser_Name.Enabled = Not Me.optSMTP_Auth_None.Checked
        Me.txtPassword.Enabled = Not Me.optSMTP_Auth_None.Checked
    End Sub
    Private Sub optFormat_XPDF_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optFormat_XPDF.CheckedChanged
        Update_OptFormat()
    End Sub
    Public Function FixSettingsNameAlreadyExists(ByRef strName As String) As String
        Dim n As String = strName
        Try
RETURN_ADDFIELDS:
            Dim ds As New System.Data.DataSet
            ds.ReadXml(ApplicationDataFolder & "settings.xml", XmlReadMode.ReadSchema)
            Dim dv As New DataView
            dv = ds.Tables(0).DefaultView
            dv.RowFilter = "Setting_Name = '" & strName & "'"
            Dim dt As DataTable = dv.ToTable()
            If dt.Rows.Count > 0 Then
                Dim s As String = Guid.NewGuid.ToString
                strName = strName & "-" & s.Substring(0, 3)
                ds.Dispose()
                ds = Nothing
                GoTo RETURN_ADDFIELDS
            Else
                ds.Dispose()
                ds = Nothing
                Return strName & ""
            End If
        Catch ex As Exception
            Return n
        End Try
    End Function
    Private Sub btnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        Dim ds As New System.Data.DataSet
RETURN_ADDFIELDS:
        btnAdd.Enabled = True
        ds.ReadXml(ApplicationDataFolder & "settings.xml", XmlReadMode.ReadSchema)
        Dim drow As System.Data.DataRow
        Try
            If cmbSettings.Enabled = False Then
                lblStatus.Text = "Status: Please select a setting"
                Exit Sub
            Else
                lblStatus.Text = "Status: Copied Settings"
                drow = ds.Tables(0).NewRow
            End If
            drow("From_Email") = Me.txtFrom_Email.Text & ""
            drow("From_Name") = Me.txtFrom_Name.Text & ""
            drow("Message_Body") = Me.txtMessage_Body.Text & ""
            drow("Server_Name") = Me.txtServer_Name.Text & ""
            drow("Server_Port") = Me.txtServer_Port.Text & ""
            drow("User_Name") = Me.txtUser_Name.Text & ""
            drow("Password") = Me.txtPassword.Text & ""
            drow("Message_Subject") = Me.txtMessage_Subject.Text & ""
            If Me.optFormat_FDF.Checked Then
                drow("Attachment_Format") = "fdf"
            ElseIf Me.optFormat_PDF.Checked Then
                drow("Attachment_Format") = "pdf"
            ElseIf Me.optFormat_xFDF.Checked Then
                drow("Attachment_Format") = "xfdf"
            ElseIf Me.optFormat_XML.Checked Then
                drow("Attachment_Format") = "xml"
            ElseIf Me.optFormat_XDP.Checked Then
                drow("Attachment_Format") = "xdp"
            ElseIf Me.optFormat_XPDF.Checked Then
                drow("Attachment_Format") = "xpdf"
            ElseIf Me.optFormat_RAW.Checked Then
                drow("Attachment_Format") = "raw"
            Else
                drow("Attachment_Format") = "pdf"
            End If
            drow("Exchange_Server") = Me.chkExchange.Checked
            drow("FDFToolkit_Installed") = Me.chkFDFTKSetup.Checked
            drow("Modify_PDF") = Me.txtPDF_ModifyPassword.Text & ""
            drow("Open_PDF") = Me.txtPDF_OpenPassword.Text & ""
            drow("Flatten_PDF") = Me.chkFlatten_PDF.Checked
            drow("Credential_User_Name") = Me.txtUser_Credential.Text & ""
            drow("Credential_Password") = Me.txtPassword_Credential.Text & ""
            drow("Credential_Domain") = Me.txtDomain_Credential.Text & ""
            drow("To_Email") = Me.txtTo_Email.Text & ""
            If Me.optSMTP_Auth_None.Checked Then
                drow("SMTP_Authentication") = 0
            ElseIf Me.optSMTP_Auth_Basic.Checked Then
                drow("SMTP_Authentication") = 1
            ElseIf Me.optSMTP_Auth_Credentials.Checked Then
                drow("SMTP_Authentication") = 2
            End If
            drow("PDF_URL") = Me.txtPDFURL.Text & ""
            drow("SMTP_SSL") = Me.chkSSL.Checked
            drow("Attachment_Filename") = Me.txtAttachmentFName.Text & ""
            drow("BCC_Email") = Me.txtBCC_Email.Text & ""
            drow("CC_Email") = Me.txtCC_Email.Text & ""
            drow("MSG_TO_FIELD") = Me.txtMSG_TO_FIELD.Text & ""
            drow("Setting_Name") = "Copy - " & Me.txtSettingName.Text & ""
            drow("Setting_Name") = FixSettingsNameAlreadyExists(drow("Setting_Name") & "")
            Try
                If Me.SMTP_Delivery_Method_Network.Checked Then
                    drow("SMTP_Delivery") = 0
                ElseIf Me.SMTP_Delivery_Method_Specify.Checked Then
                    drow("SMTP_Delivery") = 1
                ElseIf Me.SMTP_Delivery_Method_IIS.Checked Then
                    drow("SMTP_Delivery") = 2
                End If
                drow("SMTP_Delivery_Specify") = Me.SMTP_DELIVERY_METHOD_SPECIFY_TEXT.Text & ""
            Catch ex As Exception
                Err.Clear()
            End Try
            ds.Tables(0).Rows.Add(drow)
            ds.WriteXml(ApplicationDataFolder & "settings.xml", XmlWriteMode.WriteSchema)
            cmbSettings.Enabled = True
            Load_Settings()
            cmbSettings.SelectedIndex = cmbSettings.Items.Count - 1
            lblStatus.Text = "Status: Copied setting."
        Catch ex As Exception
            cmbSettings.Enabled = True
            Load_Settings()
            lblStatus.Text = "Status: Error Updating Settings" & vbNewLine & vbNewLine & ex.Message
        End Try
    End Sub
    Private Sub txtMSG_TO_FIELD_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMSG_TO_FIELD.LostFocus
        If Not String.IsNullOrEmpty(txtMSG_TO_FIELD.Text) Then
            If txtMSG_TO_FIELD.Text.Contains("@") = True Then
                Dim msgResponse As DialogResult = MessageBox.Show("This field is reserved for a field in the PDF or HTML form, do not enter actual e-mail addresses here." & Environment.NewLine & Environment.NewLine & "Do you want to place the recipient in the correct ""To"" location?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly, False)
                If msgResponse = Windows.Forms.DialogResult.Yes Then
                    txtTo_Email.Text = txtMSG_TO_FIELD.Text & ""
                    txtMSG_TO_FIELD.Text = ""
                End If
            End If
        End If
    End Sub
    Private Sub optFormat_RAW_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optFormat_RAW.CheckedChanged
        Update_OptFormat()
    End Sub
    Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem1.Click
    End Sub
    Private Sub MenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub MenuItem5_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Process.Start(ApplicationDataFolder & "MY SCRIPTS\")
        Catch ex As Exception
        End Try
    End Sub
    <Serializable()>
    Public Class Settings
        Public From_Email As String = ""
        Public From_Name As String = ""
        Public Message_Body As String = ""
        Public Server_Name As String = ""
        Public Server_Port As String = ""
        Public User_Name As String = ""
        Public Password As String = ""
        Public Message_Subject As String = ""
        Public Attachment_Format As String = ""
        Public Modify_PDF As String = ""
        Public Open_PDF As String = ""
        Public Flatten_PDF As String = ""
        Public Exchange_Server As String = ""
        Public FDFToolkit_Installed As String = ""
        Public Credential_User_Name As String = ""
        Public Credential_Password As String = ""
        Public Credential_Domain As String = ""
        Public To_Email As String = ""
        Public SMTP_Authentication As String = ""
        Public Sent_URL As String = ""
        Public Error_URL As String = ""
        Public Sent_Msg As String = ""
        Public Error_Msg As String = ""
        Public PDF_URL As String = ""
        Public SMTP_SSL As String = ""
        Public Setting_Name As String = ""
        Public Attachment_Filename As String = ""
        Public BCC_Email As String = ""
        Public CC_Email As String = ""
        Public MSG_TO_FIELD As String = ""
        Public Target_Frame As String = ""
        Public Settings As New System.Collections.Generic.List(Of String)
        Public SMTP_Delivery As Integer = 2
        Public SMTP_Delivery_Specify As String = ""
        Public Sub SaveSettings(Optional ByVal SelectedIndex As Integer = -1)
        End Sub
    End Class
    Private Sub radioFormatPDF_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub btnTestSMTP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestSMTP.Click
        Try
            Dim smtp As New System.Net.Mail.SmtpClient(Me.txtServer_Name.Text & "", CInt(Me.txtServer_Port.Text))
            Dim msg As New System.Net.Mail.MailMessage(Me.txtFrom_Email.Text, Me.txtTo_Email.Text, Me.txtMessage_Subject.Text, Me.txtMessage_Body.Text)
            Try
                If optSMTP_Auth_None.Checked Then
                ElseIf optSMTP_Auth_Basic.Checked Then
                ElseIf optSMTP_Auth_Credentials.Checked Then
                    smtp.UseDefaultCredentials = False
                    Dim cred As New System.Net.NetworkCredential(Me.txtUser_Credential.Text, Me.txtPassword_Credential.Text, Me.txtDomain_Credential.Text)
                    smtp.Credentials = cred
                End If
                If chkSSL.Checked Then
                    smtp.EnableSsl = True
                End If
                If SMTP_Delivery_Method_Network.Checked Then
                    smtp.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.Network
                ElseIf SMTP_Delivery_Method_Specify.Checked Then
                    smtp.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.SpecifiedPickupDirectory
                    smtp.PickupDirectoryLocation = SMTP_DELIVERY_METHOD_SPECIFY_TEXT.Text & ""
                ElseIf SMTP_Delivery_Method_IIS.Checked Then
                    smtp.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis
                End If
                smtp.Timeout = 20000
                Me.lblStatus.Text = "Status: sending test message, please wait..."
                smtp.Send(msg)
                Me.lblStatus.Text = "Status: test message sent successfully!"
                Me.btnTestSMTP.ForeColor = Color.Black
            Catch ex As Exception
                Me.lblStatus.Text = "Status: Error - Test message not sent. " & DateTime.Now.ToLocalTime.ToString & Environment.NewLine & "Error: " & ex.Message.ToString & ""
                Me.lblStatus.Text &= ". " & "Note: Firewalls and routers may prevent this software from reaching the SMTP server."
                Me.btnTestSMTP.ForeColor = Color.Red
            End Try
        Catch ex2 As Exception
            Me.lblStatus.Text = "Status: Error - Test message not sent. " & DateTime.Now.ToLocalTime.ToString & Environment.NewLine & "Error: " & ex2.Message.ToString & ""
            Me.lblStatus.Text &= ". " & "Note: Firewalls and routers may prevent this software from reaching the SMTP server."
            Me.btnTestSMTP.ForeColor = Color.Red
        End Try
    End Sub
    Private Sub btnSupportImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSupportImport.Click
        Dim ds As New System.Data.DataSet
        Try
            Dim openFile1 As New OpenFileDialog()
            openFile1.Filter = "PDFMail|*.pdfmail|XML File|*.xml"
            openFile1.DefaultExt = ".pdfmail"
            openFile1.Title = "Export Settings"
            If Not Directory.Exists(ApplicationDataFolder.ToString.TrimEnd("\".ToCharArray()) & "\backup\") Then
                Directory.CreateDirectory(ApplicationDataFolder.ToString.TrimEnd("\".ToCharArray()) & "\backup\")
            End If
            openFile1.InitialDirectory = ApplicationDataFolder.ToString.TrimEnd("\".ToCharArray()) & "\backup\"
            Dim rest As DialogResult = openFile1.ShowDialog()
            If rest = Windows.Forms.DialogResult.OK Or rest = Windows.Forms.DialogResult.Yes Then
                If Not String.IsNullOrEmpty(openFile1.FileName) Then
                    ds.ReadXml(openFile1.FileName, XmlReadMode.Auto)
                    If Not ds Is Nothing Then
                        Dim dsCurrent As New System.Data.DataSet
                        dsCurrent.ReadXml(ApplicationDataFolder & "settings.xml", XmlReadMode.ReadSchema)
                        For Each dr As DataRow In ds.Tables(0).Rows
                            If ContainsProjectName(dsCurrent, dr("Setting_Name")) Then
                                dr("Setting_Name") = dr("Setting_Name") & " -" & DateTime.Now.ToLocalTime.ToString.Replace("/", "-").Replace("\", "-").Replace(":", "")
                            Else
                                dr("Setting_Name") = dr("Setting_Name") & ""
                            End If
                            dsCurrent.Tables(0).ImportRow(dr)
                        Next
                        dsCurrent.WriteXml(ApplicationDataFolder & "settings.xml", XmlWriteMode.WriteSchema)
                        Load_Settings(dsCurrent.Tables(0).Rows.Count - 1)
                        Me.lblStatus.Text = "Status: Imported settings"
                    Else
                        Me.lblStatus.Text = "Status: Error Importing settings"
                    End If
                End If
            End If
        Catch ex As Exception
            Me.lblStatus.Text = "Status: Error Importing settings"
        End Try
    End Sub
    Public Sub LoadSetting(ByVal pathFile As String)
        Try
            If Not String.IsNullOrEmpty(pathFile) Then
                If System.IO.File.Exists(pathFile) Then
                    Dim ds As New System.Data.DataSet
                    ds.ReadXml(pathFile, XmlReadMode.Auto)
                    If Not ds Is Nothing Then
                        Dim dsCurrent As New System.Data.DataSet
                        Try
                            dsCurrent.ReadXml(ApplicationDataFolder & "settings.xml", XmlReadMode.ReadSchema)
                            For Each dr As DataRow In ds.Tables(0).Rows
                                dr("Setting_Name") = dr("Setting_Name") & " - " & DateTime.Now.ToFileTime.ToString.Replace(":", "").Replace("-", "").Replace(" ", "")
                                dsCurrent.Tables(0).ImportRow(dr)
                            Next
                            dsCurrent.WriteXml(ApplicationDataFolder & "settings.xml", XmlWriteMode.WriteSchema)
                            Load_Settings(dsCurrent.Tables(0).Rows.Count - 1)
                            Me.lblStatus.Text = "Status: Imported settings"
                        Catch ex As Exception
                            Load_Settings(-1)
                            Me.lblStatus.Text = "Status: Error Importing settings"
                        End Try
                    Else
                        Me.lblStatus.Text = "Status: Error Importing settings"
                    End If
                End If
            End If
        Catch ex1 As Exception
            Me.lblStatus.Text = "Status: Error Importing settings"
        End Try
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub txtSupportEmailBody_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Public Function SerializeSetting(ByVal index As Integer) As Byte()
        Dim ds As New System.Data.DataSet
        Dim drow As System.Data.DataRow
        ds.ReadXml(ApplicationDataFolder & "settings.xml", XmlReadMode.ReadSchema)
        Dim xml As New System.Xml.Serialization.XmlSerializer(GetType(System.Data.DataRow))
        Dim memStream As New System.IO.MemoryStream
        drow = ds.Tables(0).Rows(index)
        xml.Serialize(memStream, drow)
        Return memStream.GetBuffer
    End Function
    Public Function DeSerializeSetting(ByVal xmlData As Byte()) As DataRow
        Dim ds As New System.Data.DataSet
        Dim drow As System.Data.DataRow
        Dim xml As New System.Xml.Serialization.XmlSerializer(GetType(System.Data.DataRow))
        Dim memStream As New System.IO.MemoryStream(xmlData)
        drow = xml.Deserialize(memStream)
        Return drow
    End Function
    Public Function SerializeSettings(ByVal index As Integer) As Byte()
        Dim ds As New System.Data.DataSet
        ds.ReadXml(ApplicationDataFolder & "settings.xml", XmlReadMode.ReadSchema)
        Dim xml As New System.Xml.Serialization.XmlSerializer(GetType(System.Data.DataSet))
        Dim memStream As New System.IO.MemoryStream
        xml.Serialize(memStream, ds.Tables(0))
        Return memStream.GetBuffer
    End Function
    Public Function DeSerializeSettings(ByVal xmlData As Byte()) As DataSet
        Dim ds As New System.Data.DataSet
        Dim xml As New System.Xml.Serialization.XmlSerializer(GetType(System.Data.DataSet))
        Dim memStream As New System.IO.MemoryStream(xmlData)
        ds = xml.Deserialize(memStream)
        Return ds
    End Function
    Public Function ReadFile(ByVal file_path As String) As Byte()
        If File.Exists(file_path) Then
            Dim binRead As New BinaryReader(New FileStream(file_path, FileMode.Open, FileAccess.Read))
            Dim bytes(binRead.BaseStream.Length) As Byte
            binRead.BaseStream.Read(bytes, 0, bytes.Length)
            binRead.BaseStream.Close()
            binRead.BaseStream.Dispose()
            Return bytes
        End If
        Return Nothing
    End Function
    Public Function GetFile(ByVal fn As String) As Byte()
        Dim bytes() As Byte
        Try
            If File.Exists(fn) Then
                Dim fs As New System.IO.FileStream(fn, FileMode.Open, FileAccess.Read)
                ReDim bytes(CInt(fs.Length))
                fs.Read(bytes, 0, bytes.Length)
                fs.Close()
                fs.Dispose()
                Return bytes
            Else
                Dim www As New System.Net.WebClient
                Dim bytes2 As Byte() = www.DownloadData(fn)
                Return bytes2
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function GetCurrentSettingXMLByte() As Byte()
        Dim dsCurrent As New System.Data.DataSet
        Dim dsAttach As New System.Data.DataSet
        Try
            dsCurrent.ReadXml(ApplicationDataFolder & "settings.xml", XmlReadMode.ReadSchema)
            dsAttach.ReadXmlSchema(ApplicationDataFolder & "settings.xml")
            Dim dr As DataRow = dsCurrent.Tables(0).Rows(Me.cmbSettings.SelectedIndex)
            dsAttach.Tables(0).ImportRow(dr)
            Dim msAttach As New MemoryStream()
            dsAttach.WriteXml(msAttach, XmlWriteMode.WriteSchema)
            If msAttach.CanSeek Then
                msAttach.Position = 0
            End If
            Return msAttach.GetBuffer
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Private Function CurrentScriptVersion() As String
        Dim strVer As String = ""
        Dim strFile As String = ""
        Try
            If Not System.IO.File.Exists(Application.StartupPath.ToString.TrimEnd("\".ToCharArray) & "\pdfmail.txt") Then
                Return Application.ProductVersion.ToString
            End If
            Dim fs As New System.IO.FileStream(Application.StartupPath.ToString.TrimEnd("\".ToCharArray) & "\pdfmail.txt", FileMode.Open, FileAccess.ReadWrite)
            If Not fs Is Nothing Then
                Dim versionReader As New System.IO.StreamReader(fs)
                strVer = versionReader.ReadLine() & ""
                fs.Close()
                fs.Dispose()
                If Not String.IsNullOrEmpty(strVer) Then
                    Return strVer
                End If
            End If
        Catch ex As Exception
        End Try
        Return Application.ProductVersion.ToString
    End Function
    Public Function VersionCompare(ByVal ver1 As String, ByVal ver2 As String) As Boolean
        If ver1.Split(".".ToCharArray).Length = ver2.Split(".".ToCharArray).Length Then
            Dim v As Integer = ver1.Split(".".ToCharArray).Length
            If ver1 > ver2 Then
                Return True
            End If
            Return False
        End If
        Return False
    End Function
    Private Function NeedsLibraries() As Boolean
        If Not File.Exists(Application.StartupPath.ToString.TrimEnd("\".ToCharArray()) & "\DLLs\2.0\itextsharp.dll") Then
            Return True
        ElseIf Not File.Exists(Application.StartupPath.ToString.TrimEnd("\".ToCharArray()) & "\DLLs\1.1\itextsharp.dll") Then
            Return True
        End If
        Return False
    End Function
    Private Function DownloadDLLsOpenSource() As Boolean
        Dim fs As System.IO.FileStream = Nothing
        Try
            Dim cWEb As New System.Net.WebClient()
            Dim itextSharp() As Byte = cWEb.DownloadData("http://www.nk-inc.com/downloads/itextsharp.dll")
            If Not itextSharp Is Nothing And itextSharp.Length > 0 Then
                Application.DoEvents()
                fs = New System.IO.FileStream(Application.StartupPath.ToString.TrimEnd("\".ToCharArray()) & "\DLLs\2.0\itextsharp.dll", FileMode.Create, FileAccess.ReadWrite)
                fs.Write(itextSharp, 0, itextSharp.Length)
                fs.Close()
                fs.Dispose()
                Application.DoEvents()
                fs = New System.IO.FileStream(Application.StartupPath.ToString.TrimEnd("\".ToCharArray()) & "\DLLs\1.1\itextsharp.dll", FileMode.Create, FileAccess.ReadWrite)
                fs.Write(itextSharp, 0, itextSharp.Length)
                fs.Close()
                fs.Dispose()
                Return True
            End If
        Catch ex As Exception
            fs.Close()
            fs.Dispose()
            Return False
        Finally
            fs = Nothing
        End Try
        Return False
    End Function
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim ds As New System.Data.DataSet
        Try
            Dim openFile1 As New OpenFileDialog()
            openFile1.Filter = "PDFMail|*.pdfmail|XML File|*.xml"
            openFile1.DefaultExt = ".pdfmail"
            openFile1.Title = "Export Settings"
            If Not Directory.Exists(ApplicationDataFolder.ToString.TrimEnd("\".ToCharArray()) & "\backup\") Then
                Directory.CreateDirectory(ApplicationDataFolder.ToString.TrimEnd("\".ToCharArray()) & "\backup\")
            End If
            openFile1.InitialDirectory = ApplicationDataFolder.ToString.TrimEnd("\".ToCharArray()) & "\backup\"
            Dim rest As DialogResult = openFile1.ShowDialog()
            If rest = Windows.Forms.DialogResult.OK Or rest = Windows.Forms.DialogResult.Yes Then
                If Not String.IsNullOrEmpty(openFile1.FileName) Then
                    ds.ReadXml(openFile1.FileName, XmlReadMode.Auto)
                    If Not ds Is Nothing Then
                        Dim dsCurrent As New System.Data.DataSet
                        dsCurrent.ReadXml(ApplicationDataFolder & "settings.xml", XmlReadMode.ReadSchema)
                        For Each dr As DataRow In ds.Tables(0).Rows
                            dr("Setting_Name") = dr("Setting_Name") & " " & DateTime.Now.ToLocalTime.ToString.Replace("/", "-").Replace("\", "-").Replace(":", "")
                            dsCurrent.Tables(0).ImportRow(dr)
                        Next
                        dsCurrent.WriteXml(ApplicationDataFolder & "settings.xml", XmlWriteMode.WriteSchema)
                        Load_Settings(-1)
                        Me.lblStatus.Text = "Status: Imported settings"
                    Else
                        Me.lblStatus.Text = "Status: Error Importing settings"
                    End If
                End If
            End If
        Catch ex As Exception
            Me.lblStatus.Text = "Status: Error Importing settings"
        End Try
    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim ds As New System.Data.DataSet
        Try
            ds.ReadXml(ApplicationDataFolder & "settings.xml", XmlReadMode.ReadSchema)
            Dim saveFile1 As New SaveFileDialog()
            saveFile1.Filter = "PDFMail|*.pdfmail|XML File|*.xml"
            saveFile1.DefaultExt = ".pdfmail"
            saveFile1.Title = "Export Settings"
            If Not Directory.Exists(ApplicationDataFolder.ToString.TrimEnd("\".ToCharArray()) & "\backup\") Then
                Directory.CreateDirectory(ApplicationDataFolder.ToString.TrimEnd("\".ToCharArray()) & "\backup\")
            End If
            saveFile1.InitialDirectory = ApplicationDataFolder.ToString.TrimEnd("\".ToCharArray()) & "\backup\"
            Dim rest As DialogResult = saveFile1.ShowDialog()
            If rest = Windows.Forms.DialogResult.OK Or rest = Windows.Forms.DialogResult.Yes Then
                If Not String.IsNullOrEmpty(saveFile1.FileName) Then
                    If Not saveFile1.FileName = ApplicationDataFolder & "settings.xml" Then
                        ds.WriteXml(saveFile1.FileName, XmlWriteMode.WriteSchema)
                        Me.lblStatus.Text = "Status: Exported setting"
                    Else
                        Me.lblStatus.Text = "Status: Error Exporting setting"
                        Return
                    End If
                End If
            End If
        Catch ex As Exception
            Me.lblStatus.Text = "Status: Error Exporting setting"
        End Try
    End Sub
    Private Sub TabPage7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub btnSupportExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSupportExport.Click
        Dim ds As New System.Data.DataSet
        Try
            ds.ReadXml(ApplicationDataFolder & "settings.xml", XmlReadMode.ReadSchema)
            Dim saveFile1 As New SaveFileDialog()
            saveFile1.Filter = "PDFMail|*.pdfmail|XML File|*.xml"
            saveFile1.DefaultExt = ".pdfmail"
            saveFile1.Title = "Export Settings"
            If Not Directory.Exists(ApplicationDataFolder.ToString.TrimEnd("\".ToCharArray()) & "\backup\") Then
                Directory.CreateDirectory(ApplicationDataFolder.ToString.TrimEnd("\".ToCharArray()) & "\backup\")
            End If
            saveFile1.InitialDirectory = ApplicationDataFolder.ToString.TrimEnd("\".ToCharArray()) & "\backup\"
            Dim rest As DialogResult = saveFile1.ShowDialog()
            If rest = Windows.Forms.DialogResult.OK Or rest = Windows.Forms.DialogResult.Yes Then
                If Not String.IsNullOrEmpty(saveFile1.FileName) Then
                    If Not saveFile1.FileName = ApplicationDataFolder & "settings.xml" Then
                        If Me.cmbSettings.SelectedIndex >= 0 And Me.cmbSettings.Items.Count >= 1 Then
                            Dim dsCurrent As New DataSet
                            dsCurrent.ReadXmlSchema(ApplicationDataFolder & "settings.xml")
                            dsCurrent.Tables(0).ImportRow(ds.Tables(0).Rows(Me.cmbSettings.SelectedIndex))
                            dsCurrent.WriteXml(saveFile1.FileName, XmlWriteMode.WriteSchema)
                        End If
                        Me.lblStatus.Text = "Status: Exported setting"
                    Else
                        Me.lblStatus.Text = "Status: Error Exporting setting"
                        Return
                    End If
                End If
            End If
        Catch ex As Exception
            Me.lblStatus.Text = "Status: Error Exporting setting"
        End Try
    End Sub
    Private Sub btnSupportScripts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub TabPage7_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub MenuItem8_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            lblStatus.Text = IIf(DownloadDLLsOpenSource(), "Status: Downloaded DLLs", "Status: Error - Downloading DLLs")
        Catch ex As Exception
            lblStatus.Text = ("Status: Error - Downloading DLLs")
        End Try
    End Sub
    Private Sub MenuItem10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Process.Start("http://fdftoolkit.codeplex.com")
        Catch ex As Exception
        End Try
    End Sub
    Private Sub lblPDFModifyPW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblPDFModifyPW.Click
    End Sub
    Private Sub txtPDF_ModifyPassword_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPDF_ModifyPassword.TextChanged
    End Sub
    Private Sub lblPDFURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub UPDATE_SMTPDELIVERYMETHOD()
        Me.SMTP_DELIVERY_METHOD_SPECIFY_TEXT.Enabled = Me.SMTP_Delivery_Method_Specify.Checked
    End Sub
    Private Sub SMTP_Delivery_Method_Specify_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SMTP_Delivery_Method_Specify.CheckedChanged
        UPDATE_SMTPDELIVERYMETHOD()
    End Sub
    Private Sub SMTP_Delivery_Method_Network_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SMTP_Delivery_Method_Network.CheckedChanged
        UPDATE_SMTPDELIVERYMETHOD()
    End Sub
    Private Sub SMTP_Delivery_Method_IIS_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SMTP_Delivery_Method_IIS.CheckedChanged
        UPDATE_SMTPDELIVERYMETHOD()
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SMTP_DELIVERY_METHOD_SPECIFY_TEXT.TextChanged
    End Sub
    Private Sub TabPage2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage2.Click
    End Sub
    Private Sub Label16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label16.Click
    End Sub
    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint
    End Sub
    Private Sub Label15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub TabPage8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub txtPDF_OpenPassword_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPDF_OpenPassword.TextChanged
    End Sub
    Private Sub Label21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub TabPage4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub txtSettingName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSettingName.TextChanged
    End Sub
    Private Sub TabPage6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage6.Click
    End Sub
    Private Sub lnkLblPDFURL_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblPDFURL.LinkClicked
        Try
            Process.Start(Me.txtPDFURL.Text.ToString())
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub txtPDFURL_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub txtPDFURL_TextChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPDFURL.TextChanged
    End Sub
    Private Sub TabPage3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage3.Click
    End Sub
    Private Sub MenuItem11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim f As String = ""
        Try
            f = Application.StartupPath.ToString.TrimEnd("\".ToCharArray()) & "\scripts\1.1\pdf_email.txt"
            If File.Exists(f) Then
                File.Delete(f)
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
        Try
            f = Application.StartupPath.ToString.TrimEnd("\".ToCharArray()) & "\scripts\2.0\pdf_email.txt"
            If File.Exists(f) Then
                File.Delete(f)
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
        Me.lblStatus.Text = "Status: " & "reset scripts complete"
    End Sub
    Private Sub MenuItem13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Process.Start(Application.StartupPath.TrimEnd("\"c) & "\")
        Catch ex As Exception
        End Try
    End Sub
    Private Sub MenuItem14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Process.Start(ApplicationDataFolder(True) & "\scripts\")
        Catch ex As Exception
        End Try
    End Sub
    Private Sub MenuItem17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Process.Start(ApplicationDataFolder(True) & "\")
    End Sub
    Private Sub txtSupportOrderEmailFrom_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub txtSupportEmailOrderNumber_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub btnMailServerGmailSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMailServerGmailSettings.Click
        Try
            optSMTP_Auth_Basic.Checked = False
            optSMTP_Auth_None.Checked = False
            optSMTP_Auth_Credentials.Checked = True
            chkSSL.Checked = True
            chkExchange.Checked = False
            txtServer_Port.Text = "587"
            txtServer_Name.Text = "smtp.gmail.com"
            Select Case MsgBox("Enabling the less secure login feature in Gmail may be needed.." & Environment.NewLine & "Do you wish to open Gmail and enable this feature?", MsgBoxStyle.Information + MsgBoxStyle.Critical + MsgBoxStyle.YesNo, "Open Gmail: SMTP Login")
                Case MsgBoxResult.Yes, MsgBoxResult.Ok
                    Process.Start("https://www.google.com/settings/security/lesssecureapps")
                Case Else
                    Return
            End Select
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub txtSupportEmailFrom_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub chkSupportEmailPDF_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub Label17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label17.Click
    End Sub
    Private Sub txtPassword_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPassword.GotFocus
        txtPassword.PasswordChar = ""
    End Sub
    Private Sub txtPassword_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPassword.LostFocus
        txtPassword.PasswordChar = "*"
    End Sub
    Private Sub txtPassword_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPassword.TextChanged
    End Sub
    Private Sub txtPassword_Credential_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPassword_Credential.GotFocus
        txtPassword_Credential.PasswordChar = ""
    End Sub
    Private Sub txtPassword_Credential_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPassword_Credential.LostFocus
        txtPassword_Credential.PasswordChar = "*"
    End Sub
    Private Sub txtPassword_Credential_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPassword_Credential.TextChanged
    End Sub
    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        txtSendResults.Text = "SENDING STARTED @ " & DateTime.Now.ToLocalTime.ToLongTimeString()
        lblStatus.Text = txtSendResults.Text.ToString() & ""
        Application.DoEvents()
        Dim tmStart As DateTime = DateTime.Now.ToLocalTime
        SendCurrentMessage()
        Dim difDt As TimeSpan = DateTime.Now.ToLocalTime.Subtract(tmStart)
        Dim durationStr As String = ""
        txtSendResults.Text &= Environment.NewLine & "SENDING COMPLETED @ " & DateTime.Now.ToLocalTime.ToLongTimeString()
        durationStr = "TOTAL DURATION:" & IIf(difDt.Days > 0, " " & difDt.Days.ToString() & "d", "") & IIf(difDt.Hours > 0, " " & difDt.Hours.ToString() & "h", "") & IIf(difDt.Minutes > 0, " " & difDt.Minutes.ToString() & "m", "") & IIf(difDt.Seconds > 0, " " & difDt.Seconds.ToString() & "s", "") & IIf(difDt.Milliseconds > 0, " " & difDt.Milliseconds.ToString() & "ms", "")
        txtSendResults.Text &= Environment.NewLine & "" & durationStr & ""
        lblStatus.Text = "SENDING COMPLETED @ " & DateTime.Now.ToLocalTime.ToLongTimeString() & " - " & durationStr & ""
    End Sub
    Public Sub SendCurrentMessage(Optional ByVal tmStart As DateTime = Nothing)
        Try
            If tmStart = Nothing Then
                tmStart = DateTime.Now.ToLocalTime
            End If
            Dim msg As New System.Net.Mail.MailMessage()
            msg.From = New System.Net.Mail.MailAddress(InjectFieldNameValues(txtFrom_Email.Text & "") & "", InjectFieldNameValues(txtFrom_Name.Text & "") & "")
            If txtTo_Email.Text.Contains(";") Or txtTo_Email.Text.Contains(",") Then
                For Each a As String In InjectFieldNameValues(txtTo_Email.Text & "").ToString.Replace(",", ";").Split(CStr(";")).ToArray()
                    If Not String_IsNullOrEmpty(a & "") Then
                        msg.To.Add(a & "")
                    End If
                Next
            ElseIf Not txtTo_Email.Text = "" Then
                msg.To.Add(txtTo_Email.Text)
            End If
            msg.Subject = InjectFieldNameValues(txtMessage_Subject.Text & "") & ""
            Dim alternate As System.Net.Mail.AlternateView = Nothing
            msg.IsBodyHtml = chkEmailMessage_HTMLFormat.Checked
            If msg.IsBodyHtml Then
                Dim linkedResources As New System.Collections.Generic.List(Of System.Net.Mail.LinkedResource)
                Dim strBody = InjectFieldNameValues(txtMessage_Body.Text & "", msg, linkedResources) & ""
                alternate = System.Net.Mail.AlternateView.CreateAlternateViewFromString(strBody, New System.Net.Mime.ContentType("text/html"))
                For Each lnkedRes As System.Net.Mail.LinkedResource In linkedResources
                    alternate.LinkedResources.Add(lnkedRes)
                Next
                msg.AlternateViews.Add(alternate)
                msg.Body = strBody
                msg.BodyEncoding = System.Text.Encoding.UTF8
            Else
                msg.Body = InjectFieldNameValues(txtMessage_Body.Text & "", msg) & ""
            End If
            msg.ReplyTo = msg.From
            msg.Sender = msg.From
            Dim att As System.Net.Mail.Attachment
            If optFormat_RAW.Checked Then
                Dim f As New FDFApp.FDFDoc_Class, p As New FDFApp.FDFApp_Class
                f = p.PDFOpenFromBuf(DirectCast(Me.Owner, frmMain).Session(), True, True, DirectCast(Me.Owner, frmMain).pdfOwnerPassword & "")
                Dim b() As Byte = DirectCast(Me.Owner, frmMain).Session()
                If Not pageRange = "" Then
                    Dim r As New iTextSharp.text.pdf.PdfReader(b, DirectCast(Me.Owner, frmMain).getBytes(DirectCast(Me.Owner, frmMain).pdfOwnerPassword & ""))
                    r.SelectPages(pageRange)
                    b = DirectCast(Me.Owner, frmMain).getPDFBytes(r, True)
                End If
                If chkFlatten_PDF.Checked Then
                    b = f.PDFFlatten2Buf(b, True, DirectCast(Me.Owner, frmMain).pdfOwnerPassword & "")
                End If
                att = New System.Net.Mail.Attachment(New MemoryStream(b), InjectFieldNameValues(System.IO.Path.GetFileNameWithoutExtension(txtAttachmentFName.Text & "") & ".pdf") & "")
                msg.Attachments.Add(att)
            ElseIf optFormat_PDF.Checked Then
                Dim f As New FDFApp.FDFDoc_Class, p As New FDFApp.FDFApp_Class
                f = p.PDFOpenFromBuf(DirectCast(Me.Owner, frmMain).Session(), True, True, DirectCast(Me.Owner, frmMain).pdfOwnerPassword & "")
                Dim b() As Byte = DirectCast(Me.Owner, frmMain).Session()
                If Not pageRange = "" Then
                    Dim r As New iTextSharp.text.pdf.PdfReader(b, DirectCast(Me.Owner, frmMain).getBytes(DirectCast(Me.Owner, frmMain).pdfOwnerPassword & ""))
                    r.SelectPages(pageRange)
                    b = DirectCast(Me.Owner, frmMain).getPDFBytes(r, True)
                End If
                If chkFlatten_PDF.Checked Then
                    b = f.PDFFlatten2Buf(DirectCast(Me.Owner, frmMain).Session(), True, DirectCast(Me.Owner, frmMain).pdfOwnerPassword & "")
                End If
                att = New System.Net.Mail.Attachment(New MemoryStream(b), InjectFieldNameValues(System.IO.Path.GetFileNameWithoutExtension(txtAttachmentFName.Text & "") & ".pdf") & "")
                msg.Attachments.Add(att)
            ElseIf optFormat_FDF.Checked Then
                Dim f As New FDFApp.FDFDoc_Class, p As New FDFApp.FDFApp_Class
                f = p.PDFOpenFromBuf(DirectCast(Me.Owner, frmMain).Session(), True, True, DirectCast(Me.Owner, frmMain).pdfOwnerPassword & "")
                f.FDFSetFile(txtPDFURL.Text & "")
                att = New System.Net.Mail.Attachment(New MemoryStream(f.FDFSavetoBuf(FDFApp.FDFDoc_Class.FDFType.FDF, True)), InjectFieldNameValues(System.IO.Path.GetFileNameWithoutExtension(txtAttachmentFName.Text & "") & ".fdf") & "")
                msg.Attachments.Add(att)
            ElseIf optFormat_xFDF.Checked Then
                Dim f As New FDFApp.FDFDoc_Class, p As New FDFApp.FDFApp_Class
                f = p.PDFOpenFromBuf(DirectCast(Me.Owner, frmMain).Session(), True, True, DirectCast(Me.Owner, frmMain).pdfOwnerPassword & "")
                f.FDFSetFile(txtPDFURL.Text & "")
                att = New System.Net.Mail.Attachment(New MemoryStream(f.FDFSavetoBuf(FDFApp.FDFDoc_Class.FDFType.xFDF, True)), InjectFieldNameValues(System.IO.Path.GetFileNameWithoutExtension(txtAttachmentFName.Text & "") & ".xfdf") & "")
                msg.Attachments.Add(att)
            ElseIf optFormat_XDP.Checked Then
                Dim f As New FDFApp.FDFDoc_Class, p As New FDFApp.FDFApp_Class
                f = p.PDFOpenFromBuf(DirectCast(Me.Owner, frmMain).Session(), True, True, DirectCast(Me.Owner, frmMain).pdfOwnerPassword & "")
                f.FDFSetFile(txtPDFURL.Text & "")
                att = New System.Net.Mail.Attachment(New MemoryStream(f.FDFSavetoBuf(FDFApp.FDFDoc_Class.FDFType.XDP, True)), InjectFieldNameValues(System.IO.Path.GetFileNameWithoutExtension(txtAttachmentFName.Text & "") & ".xdp") & "")
                msg.Attachments.Add(att)
            ElseIf optFormat_XML.Checked Then
                Dim f As New FDFApp.FDFDoc_Class, p As New FDFApp.FDFApp_Class
                f = p.PDFOpenFromBuf(DirectCast(Me.Owner, frmMain).Session(), True, True, DirectCast(Me.Owner, frmMain).pdfOwnerPassword & "")
                f.FDFSetFile(txtPDFURL.Text & "")
                att = New System.Net.Mail.Attachment(New MemoryStream(f.FDFSavetoBuf(FDFApp.FDFDoc_Class.FDFType.XML, True)), InjectFieldNameValues(System.IO.Path.GetFileNameWithoutExtension(txtAttachmentFName.Text & "") & ".xml") & "")
                msg.Attachments.Add(att)
            ElseIf optFormat_HTML_Attachment.Checked Then
                att = New System.Net.Mail.Attachment(New MemoryStream(System.Text.Encoding.UTF8.GetBytes(DirectCast(Me.Owner, frmMain).createHTMLFile(pageRange, True, True, InjectFieldNameValues(System.IO.Path.GetFileNameWithoutExtension(txtAttachmentFName.Text & "") & ".html"), Me))), InjectFieldNameValues(System.IO.Path.GetFileNameWithoutExtension(txtAttachmentFName.Text & "") & ".html") & "")
                msg.Attachments.Add(att)
            ElseIf optFormat_HTML_Inline.Checked Then
                Dim strHTMLBody As String = DirectCast(Me.Owner, frmMain).createHTMLFile(pageRange, False, False, InjectFieldNameValues(System.IO.Path.GetFileNameWithoutExtension(txtAttachmentFName.Text & "") & ".html"), Me, txtMessage_Body.Text.Replace(Environment.NewLine, "<br/>"), False, "", True, -1)
                Dim dir As String = DirectCast(Me.Owner, frmMain).fileDirectory() & "html\" & Path.GetFileNameWithoutExtension(DirectCast(Me.Owner, frmMain).fpath) & "\" & Path.GetFileNameWithoutExtension(DirectCast(Me.Owner, frmMain).fpath & "") & ".htm" & ""
                dir = dir.Replace("//", "/"c).Replace("\\", "\"c).Replace("/"c, "\"c)
                msg.IsBodyHtml = True
                msg.Body = strHTMLBody
                msg.BodyEncoding = System.Text.Encoding.UTF8
                Dim cEmail As New clsEmail(msg, strHTMLBody, System.IO.Path.GetDirectoryName(dir), True)
            End If
            If txtCC_Email.Text.Contains(",") Or txtCC_Email.Text.Contains(";") Then
                For Each a As String In InjectFieldNameValues(txtCC_Email.Text & "").ToString.Replace(",", ";").Split(CStr(";")).ToArray()
                    If Not String_IsNullOrEmpty(a & "") Then
                        msg.CC.Add(a)
                    End If
                Next
            ElseIf Not txtCC_Email.Text = "" Then
                msg.CC.Add(txtCC_Email.Text)
            End If
            If txtCC_Email.Text.Contains(",") Or txtCC_Email.Text.Contains(";") Then
                For Each a As String In InjectFieldNameValues(txtBCC_Email.Text & "").ToString.Replace(",", ";").Split(CStr(";")).ToArray()
                    If Not String_IsNullOrEmpty(a & "") Then
                        msg.Bcc.Add(a)
                    End If
                Next
            ElseIf Not txtBCC_Email.Text = "" Then
                msg.Bcc.Add(txtBCC_Email.Text)
            End If
            Dim s As New System.Net.Mail.SmtpClient(txtServer_Name.Text & "", CInt(txtServer_Port.Text) + 0)
            If optSMTP_Auth_Credentials.Checked Then
                Dim c As System.Net.NetworkCredential
                If String.IsNullOrEmpty(txtDomain_Credential.Text & "") Then
                    c = New System.Net.NetworkCredential(txtUser_Credential.Text & "", txtPassword_Credential.Text & "")
                Else
                    c = New System.Net.NetworkCredential(txtUser_Credential.Text & "", txtPassword_Credential.Text & "", txtDomain_Credential.Text & "")
                End If
                s.Credentials = c
            End If
            Dim strResult As String = ""
            Try
                Threading.Thread.Sleep(900)
                s.Send(msg)
                Dim difDt As TimeSpan = DateTime.Now.ToLocalTime.Subtract(tmStart)
                strResult &= "SENT " & IIf(msg.To.Count > 0, "TO: " & msg.To.ToString(), "") & IIf(msg.CC.Count > 0, " - CC: " & msg.CC.ToString(), "") & IIf(msg.Bcc.Count > 0, " - BCC: " & msg.Bcc.ToString(), "") & " - DURATION:" & IIf(difDt.Days > 0, " " & difDt.Days.ToString() & "d", "") & IIf(difDt.Hours > 0, " " & difDt.Hours.ToString() & "h", "") & IIf(difDt.Minutes > 0, " " & difDt.Minutes.ToString() & "m", "") & IIf(difDt.Seconds > 0, " " & difDt.Seconds.ToString() & "s", "") & IIf(difDt.Milliseconds > 0, " " & difDt.Milliseconds.ToString() & "ms", "")
                txtSendResults.Text &= Environment.NewLine & strResult & ""
            Catch exSMTP As Exception
                strResult &= "ERROR @ " & DateTime.Now.ToLocalTime.ToLongTimeString() & " - " & IIf(msg.To.Count > 0, " - TO: " & msg.To(0).Address.ToString(), "Unknown E-mail") & Environment.NewLine & " - Error Message: " & exSMTP.Message.ToString()
                Err.Clear()
            End Try
        Catch ex As Exception
            MsgBox("error:" & ex.Message)
        Finally
            Application.DoEvents()
        End Try
    End Sub
    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            txtPDFURL.Text = DirectCast(Me.Owner, frmMain).fpath & ""
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub optFormat_HTML_Attachment_CheckedChanged(sender As Object, e As EventArgs) Handles optFormat_HTML_Attachment.CheckedChanged
        Update_OptFormat()
    End Sub
    Private Sub optFormat_HTML_Inline_CheckedChanged(sender As Object, e As EventArgs) Handles optFormat_HTML_Inline.CheckedChanged
        Update_OptFormat()
    End Sub
    Public frmMain1 As frmMain = Nothing
    Public Overloads Function ShowDialog(ByRef p As Form, pageRange1 As String)
        pageRange = pageRange1
        If Not p Is Nothing Then
            If p.GetType = GetType(frmMain) Then
                frmMain1 = DirectCast(p, frmMain)
            End If
            Return Me.ShowDialog(p)
        Else
            Return Me.ShowDialog()
        End If
    End Function
End Class
