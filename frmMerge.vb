Public Class frmMerge
    Inherits System.Windows.Forms.Form
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 Nicholas Kowalewicz All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>
#Region " Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
    Private components As System.ComponentModel.IContainer
    Friend WithEvents cmnDlg As System.Windows.Forms.OpenFileDialog
    Friend WithEvents pnlDataSource As System.Windows.Forms.Panel
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents btnSelectSource As System.Windows.Forms.Button
    Friend WithEvents txtDataFields_1 As System.Windows.Forms.TextBox
    Friend WithEvents txtDataFields_2 As System.Windows.Forms.TextBox
    Friend WithEvents txtDataFields_0 As System.Windows.Forms.TextBox
    Friend WithEvents txtDataFields_3 As System.Windows.Forms.TextBox
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblConnected As System.Windows.Forms.Label
    Friend WithEvents pnlFields As System.Windows.Forms.Panel
    Friend WithEvents btnFilterMergePanel As System.Windows.Forms.Button
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents lstDBFields As System.Windows.Forms.CheckedListBox
    Friend WithEvents lstMappedFields As System.Windows.Forms.ListBox
    Friend WithEvents lnkLoadPDF As System.Windows.Forms.LinkLabel
    Friend WithEvents btnLoadPDF As System.Windows.Forms.Button
    Friend WithEvents lnkRemove As System.Windows.Forms.LinkLabel
    Friend WithEvents lnkAddNew As System.Windows.Forms.LinkLabel
    Public WithEvents cmbDBTables As System.Windows.Forms.ComboBox
    Public WithEvents cmbPDFFields As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents pnlFilterPrint As System.Windows.Forms.Panel
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents btnSavePDFs As System.Windows.Forms.Button
    Public WithEvents cmbComparison As System.Windows.Forms.ComboBox
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Public WithEvents cmbDBFieldFilter As System.Windows.Forms.ComboBox
    Friend WithEvents btnRecords_Affected As System.Windows.Forms.Button
    Friend WithEvents txtFieldValue As System.Windows.Forms.TextBox
    Friend WithEvents lnkRemoveFilter As System.Windows.Forms.LinkLabel
    Friend WithEvents lnkAddFilter As System.Windows.Forms.LinkLabel
    Friend WithEvents lstFilter As System.Windows.Forms.ListBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents cmbDBTablesFilter As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents pnlOutputPDFs As System.Windows.Forms.Panel
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents lnkSelect_Folder As System.Windows.Forms.LinkLabel
    Friend WithEvents txtOutputFolder As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents txtMappingRaw As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents OpenFileSettings As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SaveFileSettings As System.Windows.Forms.SaveFileDialog
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents btnOutputPDFs As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents lblMessage As System.Windows.Forms.Label
    Friend WithEvents chkOutpdfPDFFlattened As System.Windows.Forms.CheckBox
    Friend WithEvents TabPage7 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents SMTP_DeliveryDirectoryLocation As System.Windows.Forms.TextBox
    Friend WithEvents SMTP_DeliveryDirectory As System.Windows.Forms.RadioButton
    Friend WithEvents SMTP_DeliveryIIS As System.Windows.Forms.RadioButton
    Friend WithEvents SMTP_DeliveryNetwork As System.Windows.Forms.RadioButton
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents SMTP_SSL As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents SMTP_CredDomain As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents SMTP_CredPassword As System.Windows.Forms.TextBox
    Friend WithEvents SMTP_CredUsername As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents SMTP_CredentialAuthentication As System.Windows.Forms.RadioButton
    Friend WithEvents SMTP_AuthPassword As System.Windows.Forms.TextBox
    Friend WithEvents SMTP_BasicAuthentication As System.Windows.Forms.RadioButton
    Friend WithEvents SMTP_AuthUsername As System.Windows.Forms.TextBox
    Friend WithEvents SMTP_NoAuthentication As System.Windows.Forms.RadioButton
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents SMTP_Port As System.Windows.Forms.TextBox
    Friend WithEvents SMTP_Hostname As System.Windows.Forms.TextBox
    Friend WithEvents SMTP_Exchange As System.Windows.Forms.CheckBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents txtServer_Port As System.Windows.Forms.TextBox
    Friend WithEvents txtServer_Name As System.Windows.Forms.TextBox
    Friend WithEvents chkExchange As System.Windows.Forms.CheckBox
    Friend WithEvents chkFDFTKSetup As System.Windows.Forms.CheckBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents SMTP_Delivery_Method_Specify As System.Windows.Forms.RadioButton
    Friend WithEvents btnTestSMTP As System.Windows.Forms.Button
    Friend WithEvents chkSSL As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtDomain_Credential As System.Windows.Forms.TextBox
    Friend WithEvents lblDomain_Credentials As System.Windows.Forms.Label
    Friend WithEvents txtPassword_Credential As System.Windows.Forms.TextBox
    Friend WithEvents txtUser_Credential As System.Windows.Forms.TextBox
    Friend WithEvents lblCred1 As System.Windows.Forms.Label
    Friend WithEvents lblCred2 As System.Windows.Forms.Label
    Friend WithEvents optSMTP_Auth_Credentials As System.Windows.Forms.RadioButton
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents optSMTP_Auth_Basic As System.Windows.Forms.RadioButton
    Friend WithEvents txtUser_Name As System.Windows.Forms.TextBox
    Friend WithEvents optSMTP_Auth_None As System.Windows.Forms.RadioButton
    Friend WithEvents lblSmtp1 As System.Windows.Forms.Label
    Friend WithEvents lblSMTP2 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Button10 As System.Windows.Forms.Button
    Friend WithEvents TabControl2 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage8 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage9 As System.Windows.Forms.TabPage
    Friend WithEvents MSG_BCC As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents MSG_CC As System.Windows.Forms.TextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents MSG_TO As System.Windows.Forms.TextBox
    Friend WithEvents MSG_SUBJECT As System.Windows.Forms.TextBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents MSG_BODY As System.Windows.Forms.TextBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents MSG_FROM_EMAIL As System.Windows.Forms.TextBox
    Friend WithEvents MSG_FROM_NAME As System.Windows.Forms.TextBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents MSG_ATTACHMENT_FILENAME As System.Windows.Forms.TextBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents MSG_ATTACHMENT_FDF As System.Windows.Forms.RadioButton
    Friend WithEvents MSG_ATTACHMENT_PDF As System.Windows.Forms.RadioButton
    Friend WithEvents MSG_ATTACHMENT_XML As System.Windows.Forms.RadioButton
    Friend WithEvents MSG_ATTACHMENT_XDP As System.Windows.Forms.RadioButton
    Friend WithEvents MSG_ATTACHMENT_XFDF As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtOutputFilename As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents OUTPUT_FORMAT_PDF As System.Windows.Forms.RadioButton
    Friend WithEvents OUTPUT_FORMAT_XML As System.Windows.Forms.RadioButton
    Friend WithEvents OUTPUT_FORMAT_FDF As System.Windows.Forms.RadioButton
    Friend WithEvents OUTPUT_FORMAT_XDP As System.Windows.Forms.RadioButton
    Friend WithEvents OUTPUT_FORMAT_XFDF As System.Windows.Forms.RadioButton
    Friend WithEvents MSG_ATTACHMENT_PDF_FLATTENED As System.Windows.Forms.CheckBox
    Friend WithEvents MSG_ATTACHMENT_NONE As RadioButton
    Friend WithEvents chkBodyIsHtml As CheckBox
    Friend WithEvents MSG_ATTACHMENT_JSON As RadioButton
    Friend WithEvents MSG_ATTACHMENT_HTML As RadioButton
    Friend WithEvents fldOutput As System.Windows.Forms.FolderBrowserDialog
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMerge))
        Me.cmnDlg = New System.Windows.Forms.OpenFileDialog()
        Me.fldOutput = New System.Windows.Forms.FolderBrowserDialog()
        Me.pnlDataSource = New System.Windows.Forms.Panel()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.btnSelectSource = New System.Windows.Forms.Button()
        Me.txtDataFields_1 = New System.Windows.Forms.TextBox()
        Me.txtDataFields_2 = New System.Windows.Forms.TextBox()
        Me.txtDataFields_0 = New System.Windows.Forms.TextBox()
        Me.txtDataFields_3 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblConnected = New System.Windows.Forms.Label()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.pnlFields = New System.Windows.Forms.Panel()
        Me.txtMappingRaw = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.lstDBFields = New System.Windows.Forms.CheckedListBox()
        Me.lstMappedFields = New System.Windows.Forms.ListBox()
        Me.lnkLoadPDF = New System.Windows.Forms.LinkLabel()
        Me.btnLoadPDF = New System.Windows.Forms.Button()
        Me.lnkAddNew = New System.Windows.Forms.LinkLabel()
        Me.cmbDBTables = New System.Windows.Forms.ComboBox()
        Me.cmbPDFFields = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lnkRemove = New System.Windows.Forms.LinkLabel()
        Me.btnFilterMergePanel = New System.Windows.Forms.Button()
        Me.pnlFilterPrint = New System.Windows.Forms.Panel()
        Me.btnSavePDFs = New System.Windows.Forms.Button()
        Me.cmbComparison = New System.Windows.Forms.ComboBox()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.cmbDBFieldFilter = New System.Windows.Forms.ComboBox()
        Me.btnRecords_Affected = New System.Windows.Forms.Button()
        Me.txtFieldValue = New System.Windows.Forms.TextBox()
        Me.lnkRemoveFilter = New System.Windows.Forms.LinkLabel()
        Me.lnkAddFilter = New System.Windows.Forms.LinkLabel()
        Me.lstFilter = New System.Windows.Forms.ListBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmbDBTablesFilter = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.pnlOutputPDFs = New System.Windows.Forms.Panel()
        Me.btnOutputPDFs = New System.Windows.Forms.Button()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.OUTPUT_FORMAT_PDF = New System.Windows.Forms.RadioButton()
        Me.OUTPUT_FORMAT_XML = New System.Windows.Forms.RadioButton()
        Me.OUTPUT_FORMAT_FDF = New System.Windows.Forms.RadioButton()
        Me.chkOutpdfPDFFlattened = New System.Windows.Forms.CheckBox()
        Me.OUTPUT_FORMAT_XDP = New System.Windows.Forms.RadioButton()
        Me.OUTPUT_FORMAT_XFDF = New System.Windows.Forms.RadioButton()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtOutputFilename = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.lnkSelect_Folder = New System.Windows.Forms.LinkLabel()
        Me.txtOutputFolder = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.OpenFileSettings = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileSettings = New System.Windows.Forms.SaveFileDialog()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.TabPage7 = New System.Windows.Forms.TabPage()
        Me.TabControl2 = New System.Windows.Forms.TabControl()
        Me.TabPage8 = New System.Windows.Forms.TabPage()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.SMTP_DeliveryDirectoryLocation = New System.Windows.Forms.TextBox()
        Me.SMTP_DeliveryDirectory = New System.Windows.Forms.RadioButton()
        Me.SMTP_DeliveryIIS = New System.Windows.Forms.RadioButton()
        Me.SMTP_DeliveryNetwork = New System.Windows.Forms.RadioButton()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.SMTP_SSL = New System.Windows.Forms.CheckBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.SMTP_CredDomain = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.SMTP_CredPassword = New System.Windows.Forms.TextBox()
        Me.SMTP_CredUsername = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.SMTP_CredentialAuthentication = New System.Windows.Forms.RadioButton()
        Me.SMTP_AuthPassword = New System.Windows.Forms.TextBox()
        Me.SMTP_BasicAuthentication = New System.Windows.Forms.RadioButton()
        Me.SMTP_AuthUsername = New System.Windows.Forms.TextBox()
        Me.SMTP_NoAuthentication = New System.Windows.Forms.RadioButton()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.SMTP_Exchange = New System.Windows.Forms.CheckBox()
        Me.SMTP_Port = New System.Windows.Forms.TextBox()
        Me.SMTP_Hostname = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.TabPage9 = New System.Windows.Forms.TabPage()
        Me.chkBodyIsHtml = New System.Windows.Forms.CheckBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.MSG_ATTACHMENT_HTML = New System.Windows.Forms.RadioButton()
        Me.MSG_ATTACHMENT_JSON = New System.Windows.Forms.RadioButton()
        Me.MSG_ATTACHMENT_NONE = New System.Windows.Forms.RadioButton()
        Me.MSG_ATTACHMENT_PDF_FLATTENED = New System.Windows.Forms.CheckBox()
        Me.MSG_ATTACHMENT_PDF = New System.Windows.Forms.RadioButton()
        Me.MSG_ATTACHMENT_XML = New System.Windows.Forms.RadioButton()
        Me.MSG_ATTACHMENT_FDF = New System.Windows.Forms.RadioButton()
        Me.MSG_ATTACHMENT_XDP = New System.Windows.Forms.RadioButton()
        Me.MSG_ATTACHMENT_XFDF = New System.Windows.Forms.RadioButton()
        Me.MSG_ATTACHMENT_FILENAME = New System.Windows.Forms.TextBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Button10 = New System.Windows.Forms.Button()
        Me.MSG_BCC = New System.Windows.Forms.TextBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.MSG_CC = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.MSG_TO = New System.Windows.Forms.TextBox()
        Me.MSG_SUBJECT = New System.Windows.Forms.TextBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.MSG_BODY = New System.Windows.Forms.TextBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.MSG_FROM_EMAIL = New System.Windows.Forms.TextBox()
        Me.MSG_FROM_NAME = New System.Windows.Forms.TextBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.txtServer_Port = New System.Windows.Forms.TextBox()
        Me.txtServer_Name = New System.Windows.Forms.TextBox()
        Me.chkExchange = New System.Windows.Forms.CheckBox()
        Me.chkFDFTKSetup = New System.Windows.Forms.CheckBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.pnlDataSource.SuspendLayout()
        Me.pnlFields.SuspendLayout()
        Me.pnlFilterPrint.SuspendLayout()
        Me.pnlOutputPDFs.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        Me.TabPage7.SuspendLayout()
        Me.TabControl2.SuspendLayout()
        Me.TabPage8.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.TabPage9.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlDataSource
        '
        Me.pnlDataSource.BackColor = System.Drawing.Color.Transparent
        Me.pnlDataSource.Controls.Add(Me.lblStatus)
        Me.pnlDataSource.Controls.Add(Me.btnSelectSource)
        Me.pnlDataSource.Controls.Add(Me.txtDataFields_1)
        Me.pnlDataSource.Controls.Add(Me.txtDataFields_2)
        Me.pnlDataSource.Controls.Add(Me.txtDataFields_0)
        Me.pnlDataSource.Controls.Add(Me.txtDataFields_3)
        Me.pnlDataSource.Controls.Add(Me.Label1)
        Me.pnlDataSource.Controls.Add(Me.lblConnected)
        Me.pnlDataSource.Location = New System.Drawing.Point(8, 13)
        Me.pnlDataSource.Name = "pnlDataSource"
        Me.pnlDataSource.Size = New System.Drawing.Size(720, 307)
        Me.pnlDataSource.TabIndex = 93
        '
        'lblStatus
        '
        Me.lblStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblStatus.ForeColor = System.Drawing.Color.Lime
        Me.lblStatus.Location = New System.Drawing.Point(10, 242)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(702, 62)
        Me.lblStatus.TabIndex = 79
        '
        'btnSelectSource
        '
        Me.btnSelectSource.BackColor = System.Drawing.Color.Silver
        Me.btnSelectSource.ForeColor = System.Drawing.Color.White
        Me.btnSelectSource.Location = New System.Drawing.Point(16, 52)
        Me.btnSelectSource.Name = "btnSelectSource"
        Me.btnSelectSource.Size = New System.Drawing.Size(120, 30)
        Me.btnSelectSource.TabIndex = 80
        Me.btnSelectSource.Text = "Select Source"
        Me.btnSelectSource.UseVisualStyleBackColor = False
        '
        'txtDataFields_1
        '
        Me.txtDataFields_1.BackColor = System.Drawing.Color.Black
        Me.txtDataFields_1.ForeColor = System.Drawing.Color.White
        Me.txtDataFields_1.Location = New System.Drawing.Point(16, 111)
        Me.txtDataFields_1.Name = "txtDataFields_1"
        Me.txtDataFields_1.Size = New System.Drawing.Size(458, 20)
        Me.txtDataFields_1.TabIndex = 81
        Me.txtDataFields_1.Text = "TextBox1"
        '
        'txtDataFields_2
        '
        Me.txtDataFields_2.BackColor = System.Drawing.Color.Black
        Me.txtDataFields_2.ForeColor = System.Drawing.Color.White
        Me.txtDataFields_2.Location = New System.Drawing.Point(16, 134)
        Me.txtDataFields_2.Name = "txtDataFields_2"
        Me.txtDataFields_2.Size = New System.Drawing.Size(458, 20)
        Me.txtDataFields_2.TabIndex = 83
        Me.txtDataFields_2.Text = "TextBox2"
        '
        'txtDataFields_0
        '
        Me.txtDataFields_0.BackColor = System.Drawing.Color.Black
        Me.txtDataFields_0.ForeColor = System.Drawing.Color.White
        Me.txtDataFields_0.Location = New System.Drawing.Point(16, 86)
        Me.txtDataFields_0.Name = "txtDataFields_0"
        Me.txtDataFields_0.Size = New System.Drawing.Size(458, 20)
        Me.txtDataFields_0.TabIndex = 82
        Me.txtDataFields_0.Text = "TextBox1"
        '
        'txtDataFields_3
        '
        Me.txtDataFields_3.BackColor = System.Drawing.Color.Black
        Me.txtDataFields_3.ForeColor = System.Drawing.Color.White
        Me.txtDataFields_3.Location = New System.Drawing.Point(16, 158)
        Me.txtDataFields_3.Name = "txtDataFields_3"
        Me.txtDataFields_3.Size = New System.Drawing.Size(458, 20)
        Me.txtDataFields_3.TabIndex = 84
        Me.txtDataFields_3.Text = "TextBox3"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(337, 25)
        Me.Label1.TabIndex = 89
        Me.Label1.Text = "LOAD DATA SOURCE"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblConnected
        '
        Me.lblConnected.Location = New System.Drawing.Point(13, 205)
        Me.lblConnected.Name = "lblConnected"
        Me.lblConnected.Size = New System.Drawing.Size(471, 37)
        Me.lblConnected.TabIndex = 90
        Me.lblConnected.Text = "Test: Not Connected"
        '
        'btnNext
        '
        Me.btnNext.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnNext.BackColor = System.Drawing.Color.Transparent
        Me.btnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnNext.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNext.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.btnNext.FlatAppearance.BorderSize = 0
        Me.btnNext.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNext.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!)
        Me.btnNext.ForeColor = System.Drawing.Color.White
        Me.btnNext.Location = New System.Drawing.Point(544, 48)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(202, 64)
        Me.btnNext.TabIndex = 91
        Me.btnNext.UseVisualStyleBackColor = False
        Me.btnNext.Visible = False
        '
        'pnlFields
        '
        Me.pnlFields.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlFields.BackColor = System.Drawing.Color.Transparent
        Me.pnlFields.Controls.Add(Me.txtMappingRaw)
        Me.pnlFields.Controls.Add(Me.Label20)
        Me.pnlFields.Controls.Add(Me.Label17)
        Me.pnlFields.Controls.Add(Me.lstDBFields)
        Me.pnlFields.Controls.Add(Me.lstMappedFields)
        Me.pnlFields.Controls.Add(Me.lnkLoadPDF)
        Me.pnlFields.Controls.Add(Me.btnLoadPDF)
        Me.pnlFields.Controls.Add(Me.lnkAddNew)
        Me.pnlFields.Controls.Add(Me.cmbDBTables)
        Me.pnlFields.Controls.Add(Me.cmbPDFFields)
        Me.pnlFields.Controls.Add(Me.Label2)
        Me.pnlFields.Controls.Add(Me.Label3)
        Me.pnlFields.Controls.Add(Me.Label4)
        Me.pnlFields.Controls.Add(Me.Label5)
        Me.pnlFields.Controls.Add(Me.lnkRemove)
        Me.pnlFields.Location = New System.Drawing.Point(8, 8)
        Me.pnlFields.Name = "pnlFields"
        Me.pnlFields.Size = New System.Drawing.Size(720, 320)
        Me.pnlFields.TabIndex = 94
        '
        'txtMappingRaw
        '
        Me.txtMappingRaw.Location = New System.Drawing.Point(264, 206)
        Me.txtMappingRaw.MaxLength = 999
        Me.txtMappingRaw.Multiline = True
        Me.txtMappingRaw.Name = "txtMappingRaw"
        Me.txtMappingRaw.Size = New System.Drawing.Size(264, 106)
        Me.txtMappingRaw.TabIndex = 107
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Location = New System.Drawing.Point(261, 190)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(101, 20)
        Me.Label20.TabIndex = 106
        Me.Label20.Text = "Mapping Raw"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(4, 1)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(350, 31)
        Me.Label17.TabIndex = 102
        Me.Label17.Text = "FIELD MAP SETTINGS"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lstDBFields
        '
        Me.lstDBFields.BackColor = System.Drawing.Color.Black
        Me.lstDBFields.ForeColor = System.Drawing.Color.White
        Me.lstDBFields.Location = New System.Drawing.Point(8, 99)
        Me.lstDBFields.Name = "lstDBFields"
        Me.lstDBFields.Size = New System.Drawing.Size(250, 79)
        Me.lstDBFields.TabIndex = 100
        '
        'lstMappedFields
        '
        Me.lstMappedFields.BackColor = System.Drawing.Color.Black
        Me.lstMappedFields.ForeColor = System.Drawing.Color.White
        Me.lstMappedFields.Location = New System.Drawing.Point(8, 206)
        Me.lstMappedFields.Name = "lstMappedFields"
        Me.lstMappedFields.Size = New System.Drawing.Size(250, 108)
        Me.lstMappedFields.TabIndex = 90
        '
        'lnkLoadPDF
        '
        Me.lnkLoadPDF.ActiveLinkColor = System.Drawing.Color.Black
        Me.lnkLoadPDF.DisabledLinkColor = System.Drawing.Color.Gray
        Me.lnkLoadPDF.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkLoadPDF.ForeColor = System.Drawing.Color.Black
        Me.lnkLoadPDF.LinkColor = System.Drawing.Color.Black
        Me.lnkLoadPDF.Location = New System.Drawing.Point(316, 46)
        Me.lnkLoadPDF.Name = "lnkLoadPDF"
        Me.lnkLoadPDF.Size = New System.Drawing.Size(172, 35)
        Me.lnkLoadPDF.TabIndex = 99
        Me.lnkLoadPDF.TabStop = True
        Me.lnkLoadPDF.Text = "Reload PDF"
        Me.lnkLoadPDF.VisitedLinkColor = System.Drawing.Color.Black
        '
        'btnLoadPDF
        '
        Me.btnLoadPDF.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnLoadPDF.BackColor = System.Drawing.Color.Transparent
        Me.btnLoadPDF.BackgroundImage = CType(resources.GetObject("btnLoadPDF.BackgroundImage"), System.Drawing.Image)
        Me.btnLoadPDF.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnLoadPDF.Location = New System.Drawing.Point(264, 32)
        Me.btnLoadPDF.Name = "btnLoadPDF"
        Me.btnLoadPDF.Size = New System.Drawing.Size(51, 50)
        Me.btnLoadPDF.TabIndex = 98
        Me.btnLoadPDF.UseVisualStyleBackColor = False
        '
        'lnkAddNew
        '
        Me.lnkAddNew.ActiveLinkColor = System.Drawing.Color.Black
        Me.lnkAddNew.DisabledLinkColor = System.Drawing.Color.Gray
        Me.lnkAddNew.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkAddNew.ForeColor = System.Drawing.Color.Black
        Me.lnkAddNew.LinkColor = System.Drawing.Color.Black
        Me.lnkAddNew.Location = New System.Drawing.Point(264, 123)
        Me.lnkAddNew.Name = "lnkAddNew"
        Me.lnkAddNew.Size = New System.Drawing.Size(93, 23)
        Me.lnkAddNew.TabIndex = 91
        Me.lnkAddNew.TabStop = True
        Me.lnkAddNew.Text = "Add New Map"
        Me.lnkAddNew.VisitedLinkColor = System.Drawing.Color.Black
        '
        'cmbDBTables
        '
        Me.cmbDBTables.BackColor = System.Drawing.Color.Black
        Me.cmbDBTables.ForeColor = System.Drawing.Color.White
        Me.cmbDBTables.Location = New System.Drawing.Point(10, 56)
        Me.cmbDBTables.Name = "cmbDBTables"
        Me.cmbDBTables.Size = New System.Drawing.Size(248, 21)
        Me.cmbDBTables.TabIndex = 89
        Me.cmbDBTables.Text = "Select a Database Table"
        '
        'cmbPDFFields
        '
        Me.cmbPDFFields.BackColor = System.Drawing.Color.Black
        Me.cmbPDFFields.Enabled = False
        Me.cmbPDFFields.ForeColor = System.Drawing.Color.White
        Me.cmbPDFFields.Location = New System.Drawing.Point(264, 99)
        Me.cmbPDFFields.Name = "cmbPDFFields"
        Me.cmbPDFFields.Size = New System.Drawing.Size(223, 21)
        Me.cmbPDFFields.TabIndex = 87
        Me.cmbPDFFields.Text = "Select a PDF Field"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(8, 187)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(104, 23)
        Me.Label2.TabIndex = 94
        Me.Label2.Text = "PDF Field Mappings"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(10, 40)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(196, 23)
        Me.Label3.TabIndex = 95
        Me.Label3.Text = "Select Database Table"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(8, 84)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(100, 23)
        Me.Label4.TabIndex = 96
        Me.Label4.Text = "Database Fields"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(261, 83)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(101, 20)
        Me.Label5.TabIndex = 97
        Me.Label5.Text = "PDF Fields"
        '
        'lnkRemove
        '
        Me.lnkRemove.ActiveLinkColor = System.Drawing.Color.Black
        Me.lnkRemove.DisabledLinkColor = System.Drawing.Color.Gray
        Me.lnkRemove.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkRemove.ForeColor = System.Drawing.Color.Black
        Me.lnkRemove.LinkColor = System.Drawing.Color.Black
        Me.lnkRemove.Location = New System.Drawing.Point(128, 184)
        Me.lnkRemove.Name = "lnkRemove"
        Me.lnkRemove.Size = New System.Drawing.Size(136, 23)
        Me.lnkRemove.TabIndex = 93
        Me.lnkRemove.TabStop = True
        Me.lnkRemove.Text = "Remove Selected Map"
        Me.lnkRemove.VisitedLinkColor = System.Drawing.Color.Black
        '
        'btnFilterMergePanel
        '
        Me.btnFilterMergePanel.BackColor = System.Drawing.Color.Transparent
        Me.btnFilterMergePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnFilterMergePanel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnFilterMergePanel.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.btnFilterMergePanel.FlatAppearance.BorderSize = 0
        Me.btnFilterMergePanel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnFilterMergePanel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnFilterMergePanel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFilterMergePanel.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!)
        Me.btnFilterMergePanel.ForeColor = System.Drawing.Color.White
        Me.btnFilterMergePanel.Location = New System.Drawing.Point(528, 56)
        Me.btnFilterMergePanel.Name = "btnFilterMergePanel"
        Me.btnFilterMergePanel.Size = New System.Drawing.Size(221, 64)
        Me.btnFilterMergePanel.TabIndex = 103
        Me.btnFilterMergePanel.UseVisualStyleBackColor = False
        Me.btnFilterMergePanel.Visible = False
        '
        'pnlFilterPrint
        '
        Me.pnlFilterPrint.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlFilterPrint.BackColor = System.Drawing.Color.Transparent
        Me.pnlFilterPrint.Controls.Add(Me.btnSavePDFs)
        Me.pnlFilterPrint.Controls.Add(Me.cmbComparison)
        Me.pnlFilterPrint.Controls.Add(Me.btnPrint)
        Me.pnlFilterPrint.Controls.Add(Me.cmbDBFieldFilter)
        Me.pnlFilterPrint.Controls.Add(Me.btnRecords_Affected)
        Me.pnlFilterPrint.Controls.Add(Me.txtFieldValue)
        Me.pnlFilterPrint.Controls.Add(Me.lnkRemoveFilter)
        Me.pnlFilterPrint.Controls.Add(Me.lnkAddFilter)
        Me.pnlFilterPrint.Controls.Add(Me.lstFilter)
        Me.pnlFilterPrint.Controls.Add(Me.Label8)
        Me.pnlFilterPrint.Controls.Add(Me.cmbDBTablesFilter)
        Me.pnlFilterPrint.Controls.Add(Me.Label6)
        Me.pnlFilterPrint.Controls.Add(Me.Label7)
        Me.pnlFilterPrint.Controls.Add(Me.Label9)
        Me.pnlFilterPrint.Controls.Add(Me.Label10)
        Me.pnlFilterPrint.Controls.Add(Me.Label19)
        Me.pnlFilterPrint.Location = New System.Drawing.Point(10, 10)
        Me.pnlFilterPrint.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlFilterPrint.Name = "pnlFilterPrint"
        Me.pnlFilterPrint.Size = New System.Drawing.Size(718, 318)
        Me.pnlFilterPrint.TabIndex = 95
        '
        'btnSavePDFs
        '
        Me.btnSavePDFs.Location = New System.Drawing.Point(352, 138)
        Me.btnSavePDFs.Name = "btnSavePDFs"
        Me.btnSavePDFs.Size = New System.Drawing.Size(116, 29)
        Me.btnSavePDFs.TabIndex = 119
        Me.btnSavePDFs.Text = "Output PDFs"
        '
        'cmbComparison
        '
        Me.cmbComparison.BackColor = System.Drawing.Color.Black
        Me.cmbComparison.ForeColor = System.Drawing.Color.White
        Me.cmbComparison.Location = New System.Drawing.Point(246, 106)
        Me.cmbComparison.Name = "cmbComparison"
        Me.cmbComparison.Size = New System.Drawing.Size(80, 21)
        Me.cmbComparison.TabIndex = 117
        Me.cmbComparison.Text = "="
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(242, 138)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(104, 29)
        Me.btnPrint.TabIndex = 115
        Me.btnPrint.Text = "Print"
        '
        'cmbDBFieldFilter
        '
        Me.cmbDBFieldFilter.BackColor = System.Drawing.Color.Black
        Me.cmbDBFieldFilter.ForeColor = System.Drawing.Color.White
        Me.cmbDBFieldFilter.Location = New System.Drawing.Point(15, 106)
        Me.cmbDBFieldFilter.Name = "cmbDBFieldFilter"
        Me.cmbDBFieldFilter.Size = New System.Drawing.Size(225, 21)
        Me.cmbDBFieldFilter.TabIndex = 114
        Me.cmbDBFieldFilter.Text = "Select a Database Field"
        '
        'btnRecords_Affected
        '
        Me.btnRecords_Affected.Location = New System.Drawing.Point(14, 138)
        Me.btnRecords_Affected.Name = "btnRecords_Affected"
        Me.btnRecords_Affected.Size = New System.Drawing.Size(226, 29)
        Me.btnRecords_Affected.TabIndex = 113
        Me.btnRecords_Affected.Text = "Records Affected = 0"
        '
        'txtFieldValue
        '
        Me.txtFieldValue.BackColor = System.Drawing.Color.Black
        Me.txtFieldValue.ForeColor = System.Drawing.Color.White
        Me.txtFieldValue.Location = New System.Drawing.Point(326, 106)
        Me.txtFieldValue.Name = "txtFieldValue"
        Me.txtFieldValue.Size = New System.Drawing.Size(136, 20)
        Me.txtFieldValue.TabIndex = 111
        '
        'lnkRemoveFilter
        '
        Me.lnkRemoveFilter.ActiveLinkColor = System.Drawing.Color.Black
        Me.lnkRemoveFilter.DisabledLinkColor = System.Drawing.Color.Gray
        Me.lnkRemoveFilter.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkRemoveFilter.ForeColor = System.Drawing.Color.Black
        Me.lnkRemoveFilter.LinkColor = System.Drawing.Color.Black
        Me.lnkRemoveFilter.Location = New System.Drawing.Point(504, 176)
        Me.lnkRemoveFilter.Name = "lnkRemoveFilter"
        Me.lnkRemoveFilter.Size = New System.Drawing.Size(154, 23)
        Me.lnkRemoveFilter.TabIndex = 109
        Me.lnkRemoveFilter.TabStop = True
        Me.lnkRemoveFilter.Text = "Remove Selected Filter"
        Me.lnkRemoveFilter.VisitedLinkColor = System.Drawing.Color.Black
        '
        'lnkAddFilter
        '
        Me.lnkAddFilter.ActiveLinkColor = System.Drawing.Color.Black
        Me.lnkAddFilter.DisabledLinkColor = System.Drawing.Color.Gray
        Me.lnkAddFilter.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkAddFilter.ForeColor = System.Drawing.Color.Black
        Me.lnkAddFilter.LinkColor = System.Drawing.Color.Black
        Me.lnkAddFilter.Location = New System.Drawing.Point(470, 106)
        Me.lnkAddFilter.Name = "lnkAddFilter"
        Me.lnkAddFilter.Size = New System.Drawing.Size(128, 23)
        Me.lnkAddFilter.TabIndex = 108
        Me.lnkAddFilter.TabStop = True
        Me.lnkAddFilter.Text = "Add Filter"
        Me.lnkAddFilter.VisitedLinkColor = System.Drawing.Color.Black
        '
        'lstFilter
        '
        Me.lstFilter.BackColor = System.Drawing.Color.Black
        Me.lstFilter.ForeColor = System.Drawing.Color.White
        Me.lstFilter.Location = New System.Drawing.Point(12, 196)
        Me.lstFilter.Name = "lstFilter"
        Me.lstFilter.Size = New System.Drawing.Size(646, 121)
        Me.lstFilter.TabIndex = 107
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(12, 177)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(184, 23)
        Me.Label8.TabIndex = 110
        Me.Label8.Text = "Filter"
        '
        'cmbDBTablesFilter
        '
        Me.cmbDBTablesFilter.BackColor = System.Drawing.Color.Black
        Me.cmbDBTablesFilter.ForeColor = System.Drawing.Color.White
        Me.cmbDBTablesFilter.Location = New System.Drawing.Point(15, 66)
        Me.cmbDBTablesFilter.Name = "cmbDBTablesFilter"
        Me.cmbDBTablesFilter.Size = New System.Drawing.Size(225, 21)
        Me.cmbDBTablesFilter.TabIndex = 102
        Me.cmbDBTablesFilter.Text = "Select a Database Table"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(15, 50)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(100, 23)
        Me.Label6.TabIndex = 103
        Me.Label6.Text = "Database Tables"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(15, 90)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(96, 23)
        Me.Label7.TabIndex = 104
        Me.Label7.Text = "Database Fields"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Location = New System.Drawing.Point(326, 90)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(100, 23)
        Me.Label9.TabIndex = 112
        Me.Label9.Text = "Field Value"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Location = New System.Drawing.Point(246, 90)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(100, 23)
        Me.Label10.TabIndex = 118
        Me.Label10.Text = "Comparison"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.Black
        Me.Label19.Location = New System.Drawing.Point(8, 8)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(479, 28)
        Me.Label19.TabIndex = 120
        Me.Label19.Text = "DATA FILTERS"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlOutputPDFs
        '
        Me.pnlOutputPDFs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlOutputPDFs.BackColor = System.Drawing.Color.Transparent
        Me.pnlOutputPDFs.Controls.Add(Me.btnOutputPDFs)
        Me.pnlOutputPDFs.Controls.Add(Me.GroupBox6)
        Me.pnlOutputPDFs.Controls.Add(Me.Label11)
        Me.pnlOutputPDFs.Controls.Add(Me.txtOutputFilename)
        Me.pnlOutputPDFs.Controls.Add(Me.Label18)
        Me.pnlOutputPDFs.Controls.Add(Me.lnkSelect_Folder)
        Me.pnlOutputPDFs.Controls.Add(Me.txtOutputFolder)
        Me.pnlOutputPDFs.Controls.Add(Me.Label16)
        Me.pnlOutputPDFs.Location = New System.Drawing.Point(8, 8)
        Me.pnlOutputPDFs.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlOutputPDFs.Name = "pnlOutputPDFs"
        Me.pnlOutputPDFs.Size = New System.Drawing.Size(718, 320)
        Me.pnlOutputPDFs.TabIndex = 121
        '
        'btnOutputPDFs
        '
        Me.btnOutputPDFs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnOutputPDFs.BackColor = System.Drawing.Color.Transparent
        Me.btnOutputPDFs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnOutputPDFs.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnOutputPDFs.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnOutputPDFs.FlatAppearance.BorderSize = 0
        Me.btnOutputPDFs.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnOutputPDFs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnOutputPDFs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOutputPDFs.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!)
        Me.btnOutputPDFs.ForeColor = System.Drawing.Color.Red
        Me.btnOutputPDFs.Location = New System.Drawing.Point(16, 240)
        Me.btnOutputPDFs.Name = "btnOutputPDFs"
        Me.btnOutputPDFs.Size = New System.Drawing.Size(336, 51)
        Me.btnOutputPDFs.TabIndex = 133
        Me.btnOutputPDFs.Text = "CLICK TO OUTPUT MERGED PDFs"
        Me.btnOutputPDFs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnOutputPDFs.UseVisualStyleBackColor = False
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.OUTPUT_FORMAT_PDF)
        Me.GroupBox6.Controls.Add(Me.OUTPUT_FORMAT_XML)
        Me.GroupBox6.Controls.Add(Me.OUTPUT_FORMAT_FDF)
        Me.GroupBox6.Controls.Add(Me.chkOutpdfPDFFlattened)
        Me.GroupBox6.Controls.Add(Me.OUTPUT_FORMAT_XDP)
        Me.GroupBox6.Controls.Add(Me.OUTPUT_FORMAT_XFDF)
        Me.GroupBox6.Location = New System.Drawing.Point(16, 128)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(192, 96)
        Me.GroupBox6.TabIndex = 135
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Output Format"
        '
        'OUTPUT_FORMAT_PDF
        '
        Me.OUTPUT_FORMAT_PDF.AutoSize = True
        Me.OUTPUT_FORMAT_PDF.Checked = True
        Me.OUTPUT_FORMAT_PDF.Location = New System.Drawing.Point(16, 24)
        Me.OUTPUT_FORMAT_PDF.Name = "OUTPUT_FORMAT_PDF"
        Me.OUTPUT_FORMAT_PDF.Size = New System.Drawing.Size(46, 17)
        Me.OUTPUT_FORMAT_PDF.TabIndex = 79
        Me.OUTPUT_FORMAT_PDF.TabStop = True
        Me.OUTPUT_FORMAT_PDF.Text = "PDF"
        Me.OUTPUT_FORMAT_PDF.UseVisualStyleBackColor = True
        '
        'OUTPUT_FORMAT_XML
        '
        Me.OUTPUT_FORMAT_XML.AutoSize = True
        Me.OUTPUT_FORMAT_XML.Location = New System.Drawing.Point(80, 72)
        Me.OUTPUT_FORMAT_XML.Name = "OUTPUT_FORMAT_XML"
        Me.OUTPUT_FORMAT_XML.Size = New System.Drawing.Size(47, 17)
        Me.OUTPUT_FORMAT_XML.TabIndex = 83
        Me.OUTPUT_FORMAT_XML.Text = "XML"
        Me.OUTPUT_FORMAT_XML.UseVisualStyleBackColor = True
        '
        'OUTPUT_FORMAT_FDF
        '
        Me.OUTPUT_FORMAT_FDF.AutoSize = True
        Me.OUTPUT_FORMAT_FDF.Location = New System.Drawing.Point(16, 48)
        Me.OUTPUT_FORMAT_FDF.Name = "OUTPUT_FORMAT_FDF"
        Me.OUTPUT_FORMAT_FDF.Size = New System.Drawing.Size(45, 17)
        Me.OUTPUT_FORMAT_FDF.TabIndex = 80
        Me.OUTPUT_FORMAT_FDF.Text = "FDF"
        Me.OUTPUT_FORMAT_FDF.UseVisualStyleBackColor = True
        '
        'chkOutpdfPDFFlattened
        '
        Me.chkOutpdfPDFFlattened.AutoSize = True
        Me.chkOutpdfPDFFlattened.Location = New System.Drawing.Point(80, 24)
        Me.chkOutpdfPDFFlattened.Name = "chkOutpdfPDFFlattened"
        Me.chkOutpdfPDFFlattened.Size = New System.Drawing.Size(98, 17)
        Me.chkOutpdfPDFFlattened.TabIndex = 132
        Me.chkOutpdfPDFFlattened.Text = "FLATTEN PDF"
        Me.chkOutpdfPDFFlattened.UseVisualStyleBackColor = True
        '
        'OUTPUT_FORMAT_XDP
        '
        Me.OUTPUT_FORMAT_XDP.AutoSize = True
        Me.OUTPUT_FORMAT_XDP.Location = New System.Drawing.Point(80, 48)
        Me.OUTPUT_FORMAT_XDP.Name = "OUTPUT_FORMAT_XDP"
        Me.OUTPUT_FORMAT_XDP.Size = New System.Drawing.Size(47, 17)
        Me.OUTPUT_FORMAT_XDP.TabIndex = 82
        Me.OUTPUT_FORMAT_XDP.Text = "XDP"
        Me.OUTPUT_FORMAT_XDP.UseVisualStyleBackColor = True
        '
        'OUTPUT_FORMAT_XFDF
        '
        Me.OUTPUT_FORMAT_XFDF.AutoSize = True
        Me.OUTPUT_FORMAT_XFDF.Location = New System.Drawing.Point(16, 72)
        Me.OUTPUT_FORMAT_XFDF.Name = "OUTPUT_FORMAT_XFDF"
        Me.OUTPUT_FORMAT_XFDF.Size = New System.Drawing.Size(52, 17)
        Me.OUTPUT_FORMAT_XFDF.TabIndex = 81
        Me.OUTPUT_FORMAT_XFDF.Text = "XFDF"
        Me.OUTPUT_FORMAT_XFDF.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Location = New System.Drawing.Point(16, 88)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(128, 16)
        Me.Label11.TabIndex = 134
        Me.Label11.Text = "Output File Name"
        '
        'txtOutputFilename
        '
        Me.txtOutputFilename.Location = New System.Drawing.Point(16, 104)
        Me.txtOutputFilename.Name = "txtOutputFilename"
        Me.txtOutputFilename.Size = New System.Drawing.Size(659, 20)
        Me.txtOutputFilename.TabIndex = 133
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Black
        Me.Label18.Location = New System.Drawing.Point(8, 8)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(487, 28)
        Me.Label18.TabIndex = 131
        Me.Label18.Text = "OUTPUT FILE NAMING CONVENTIONS"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lnkSelect_Folder
        '
        Me.lnkSelect_Folder.ActiveLinkColor = System.Drawing.Color.Black
        Me.lnkSelect_Folder.DisabledLinkColor = System.Drawing.Color.Gray
        Me.lnkSelect_Folder.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkSelect_Folder.ForeColor = System.Drawing.Color.Black
        Me.lnkSelect_Folder.LinkColor = System.Drawing.Color.Black
        Me.lnkSelect_Folder.Location = New System.Drawing.Point(118, 45)
        Me.lnkSelect_Folder.Name = "lnkSelect_Folder"
        Me.lnkSelect_Folder.Size = New System.Drawing.Size(141, 14)
        Me.lnkSelect_Folder.TabIndex = 128
        Me.lnkSelect_Folder.TabStop = True
        Me.lnkSelect_Folder.Text = "Select Output Folder"
        Me.lnkSelect_Folder.VisitedLinkColor = System.Drawing.Color.Black
        '
        'txtOutputFolder
        '
        Me.txtOutputFolder.Location = New System.Drawing.Point(16, 64)
        Me.txtOutputFolder.Name = "txtOutputFolder"
        Me.txtOutputFolder.Size = New System.Drawing.Size(659, 20)
        Me.txtOutputFolder.TabIndex = 126
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Location = New System.Drawing.Point(16, 45)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(128, 16)
        Me.Label16.TabIndex = 127
        Me.Label16.Text = "Output Folder"
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.Transparent
        Me.btnExit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnExit.FlatAppearance.BorderSize = 0
        Me.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.Location = New System.Drawing.Point(705, 0)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(56, 53)
        Me.btnExit.TabIndex = 122
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button1.BackColor = System.Drawing.Color.Transparent
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Pixel, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.Gray
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button1.Location = New System.Drawing.Point(34, 302)
        Me.Button1.Margin = New System.Windows.Forms.Padding(0)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(179, 46)
        Me.Button1.TabIndex = 94
        Me.Button1.Text = "Data Sources"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage
        Me.Button1.UseCompatibleTextRendering = True
        Me.Button1.UseMnemonic = False
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button4.BackColor = System.Drawing.Color.Transparent
        Me.Button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Button4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button4.FlatAppearance.BorderSize = 0
        Me.Button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Pixel, CType(0, Byte))
        Me.Button4.ForeColor = System.Drawing.Color.Gray
        Me.Button4.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button4.Location = New System.Drawing.Point(45, 58)
        Me.Button4.Margin = New System.Windows.Forms.Padding(0)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(172, 46)
        Me.Button4.TabIndex = 134
        Me.Button4.Text = "Save Settings"
        Me.Button4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button4.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage
        Me.Button4.UseCompatibleTextRendering = True
        Me.Button4.UseMnemonic = False
        Me.Button4.UseVisualStyleBackColor = True
        '
        'OpenFileSettings
        '
        Me.OpenFileSettings.DefaultExt = "xml"
        Me.OpenFileSettings.FileName = "settings-merge.xml"
        Me.OpenFileSettings.Filter = "XML Settings | *.xml"
        Me.OpenFileSettings.Title = "Load Settings"
        '
        'SaveFileSettings
        '
        Me.SaveFileSettings.DefaultExt = "xml"
        Me.SaveFileSettings.FileName = "settings-merge.xml"
        Me.SaveFileSettings.Filter = "XML Settings | *.xml"
        Me.SaveFileSettings.Title = "Save Settings"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Controls.Add(Me.TabPage7)
        Me.TabControl1.Location = New System.Drawing.Point(8, 56)
        Me.TabControl1.Multiline = True
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(744, 356)
        Me.TabControl1.TabIndex = 136
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Button5)
        Me.TabPage1.Controls.Add(Me.Button4)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(736, 330)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Settings"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button5.BackColor = System.Drawing.Color.Transparent
        Me.Button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Button5.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button5.FlatAppearance.BorderSize = 0
        Me.Button5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Button5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button5.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Pixel, CType(0, Byte))
        Me.Button5.ForeColor = System.Drawing.Color.Gray
        Me.Button5.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button5.Location = New System.Drawing.Point(38, 12)
        Me.Button5.Margin = New System.Windows.Forms.Padding(0)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(179, 46)
        Me.Button5.TabIndex = 136
        Me.Button5.Text = "Load Settings"
        Me.Button5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button5.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage
        Me.Button5.UseCompatibleTextRendering = True
        Me.Button5.UseMnemonic = False
        Me.Button5.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.pnlDataSource)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(736, 330)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "1) DataSources"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.pnlFields)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(736, 330)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "2) Field Mappings"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.pnlFilterPrint)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(736, 330)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "3) Data Filters"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.pnlOutputPDFs)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Size = New System.Drawing.Size(736, 330)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "4) Output Merged PDFs"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'TabPage7
        '
        Me.TabPage7.Controls.Add(Me.TabControl2)
        Me.TabPage7.Location = New System.Drawing.Point(4, 22)
        Me.TabPage7.Name = "TabPage7"
        Me.TabPage7.Size = New System.Drawing.Size(736, 330)
        Me.TabPage7.TabIndex = 6
        Me.TabPage7.Text = "6) Email Merged PDFs"
        Me.TabPage7.UseVisualStyleBackColor = True
        '
        'TabControl2
        '
        Me.TabControl2.Controls.Add(Me.TabPage8)
        Me.TabControl2.Controls.Add(Me.TabPage9)
        Me.TabControl2.Location = New System.Drawing.Point(0, 0)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(736, 328)
        Me.TabControl2.TabIndex = 63
        '
        'TabPage8
        '
        Me.TabPage8.Controls.Add(Me.GroupBox3)
        Me.TabPage8.Controls.Add(Me.Button6)
        Me.TabPage8.Controls.Add(Me.Label31)
        Me.TabPage8.Controls.Add(Me.SMTP_SSL)
        Me.TabPage8.Controls.Add(Me.GroupBox4)
        Me.TabPage8.Controls.Add(Me.SMTP_Exchange)
        Me.TabPage8.Controls.Add(Me.SMTP_Port)
        Me.TabPage8.Controls.Add(Me.SMTP_Hostname)
        Me.TabPage8.Controls.Add(Me.Label30)
        Me.TabPage8.Controls.Add(Me.Label29)
        Me.TabPage8.Location = New System.Drawing.Point(4, 22)
        Me.TabPage8.Name = "TabPage8"
        Me.TabPage8.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage8.Size = New System.Drawing.Size(728, 302)
        Me.TabPage8.TabIndex = 0
        Me.TabPage8.Text = "SMTP Settings"
        Me.TabPage8.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.SMTP_DeliveryDirectoryLocation)
        Me.GroupBox3.Controls.Add(Me.SMTP_DeliveryDirectory)
        Me.GroupBox3.Controls.Add(Me.SMTP_DeliveryIIS)
        Me.GroupBox3.Controls.Add(Me.SMTP_DeliveryNetwork)
        Me.GroupBox3.Location = New System.Drawing.Point(137, 179)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(422, 116)
        Me.GroupBox3.TabIndex = 62
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "SMTP Delivery Method"
        '
        'SMTP_DeliveryDirectoryLocation
        '
        Me.SMTP_DeliveryDirectoryLocation.Location = New System.Drawing.Point(42, 88)
        Me.SMTP_DeliveryDirectoryLocation.Name = "SMTP_DeliveryDirectoryLocation"
        Me.SMTP_DeliveryDirectoryLocation.Size = New System.Drawing.Size(355, 20)
        Me.SMTP_DeliveryDirectoryLocation.TabIndex = 44
        '
        'SMTP_DeliveryDirectory
        '
        Me.SMTP_DeliveryDirectory.AutoSize = True
        Me.SMTP_DeliveryDirectory.Location = New System.Drawing.Point(22, 69)
        Me.SMTP_DeliveryDirectory.Name = "SMTP_DeliveryDirectory"
        Me.SMTP_DeliveryDirectory.Size = New System.Drawing.Size(155, 17)
        Me.SMTP_DeliveryDirectory.TabIndex = 43
        Me.SMTP_DeliveryDirectory.Text = "Specify Directory for pickup"
        Me.SMTP_DeliveryDirectory.UseVisualStyleBackColor = True
        '
        'SMTP_DeliveryIIS
        '
        Me.SMTP_DeliveryIIS.AutoSize = True
        Me.SMTP_DeliveryIIS.Location = New System.Drawing.Point(22, 46)
        Me.SMTP_DeliveryIIS.Name = "SMTP_DeliveryIIS"
        Me.SMTP_DeliveryIIS.Size = New System.Drawing.Size(140, 17)
        Me.SMTP_DeliveryIIS.TabIndex = 42
        Me.SMTP_DeliveryIIS.Text = "Pickup directory from IIS"
        Me.SMTP_DeliveryIIS.UseVisualStyleBackColor = True
        '
        'SMTP_DeliveryNetwork
        '
        Me.SMTP_DeliveryNetwork.AutoSize = True
        Me.SMTP_DeliveryNetwork.Checked = True
        Me.SMTP_DeliveryNetwork.Location = New System.Drawing.Point(22, 23)
        Me.SMTP_DeliveryNetwork.Name = "SMTP_DeliveryNetwork"
        Me.SMTP_DeliveryNetwork.Size = New System.Drawing.Size(108, 17)
        Me.SMTP_DeliveryNetwork.TabIndex = 41
        Me.SMTP_DeliveryNetwork.TabStop = True
        Me.SMTP_DeliveryNetwork.Text = "Network (Default)"
        Me.SMTP_DeliveryNetwork.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.ForeColor = System.Drawing.Color.Black
        Me.Button6.Location = New System.Drawing.Point(343, 17)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(139, 23)
        Me.Button6.TabIndex = 61
        Me.Button6.Text = "Test SMTP Connection"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Label31
        '
        Me.Label31.ForeColor = System.Drawing.Color.Black
        Me.Label31.Location = New System.Drawing.Point(4, 179)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(100, 23)
        Me.Label31.TabIndex = 60
        Me.Label31.Text = "Server Settings"
        '
        'SMTP_SSL
        '
        Me.SMTP_SSL.BackColor = System.Drawing.Color.Transparent
        Me.SMTP_SSL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.SMTP_SSL.Location = New System.Drawing.Point(19, 230)
        Me.SMTP_SSL.Name = "SMTP_SSL"
        Me.SMTP_SSL.Size = New System.Drawing.Size(112, 24)
        Me.SMTP_SSL.TabIndex = 59
        Me.SMTP_SSL.Text = "Enable SSL"
        Me.SMTP_SSL.UseVisualStyleBackColor = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.SMTP_CredDomain)
        Me.GroupBox4.Controls.Add(Me.Label24)
        Me.GroupBox4.Controls.Add(Me.SMTP_CredPassword)
        Me.GroupBox4.Controls.Add(Me.SMTP_CredUsername)
        Me.GroupBox4.Controls.Add(Me.Label25)
        Me.GroupBox4.Controls.Add(Me.Label26)
        Me.GroupBox4.Controls.Add(Me.SMTP_CredentialAuthentication)
        Me.GroupBox4.Controls.Add(Me.SMTP_AuthPassword)
        Me.GroupBox4.Controls.Add(Me.SMTP_BasicAuthentication)
        Me.GroupBox4.Controls.Add(Me.SMTP_AuthUsername)
        Me.GroupBox4.Controls.Add(Me.SMTP_NoAuthentication)
        Me.GroupBox4.Controls.Add(Me.Label27)
        Me.GroupBox4.Controls.Add(Me.Label28)
        Me.GroupBox4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.GroupBox4.Location = New System.Drawing.Point(7, 46)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(552, 133)
        Me.GroupBox4.TabIndex = 54
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "SMTP Authentication"
        '
        'SMTP_CredDomain
        '
        Me.SMTP_CredDomain.BackColor = System.Drawing.Color.White
        Me.SMTP_CredDomain.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SMTP_CredDomain.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.SMTP_CredDomain.Location = New System.Drawing.Point(357, 110)
        Me.SMTP_CredDomain.Name = "SMTP_CredDomain"
        Me.SMTP_CredDomain.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.SMTP_CredDomain.Size = New System.Drawing.Size(170, 21)
        Me.SMTP_CredDomain.TabIndex = 45
        '
        'Label24
        '
        Me.Label24.ForeColor = System.Drawing.Color.Black
        Me.Label24.Location = New System.Drawing.Point(357, 94)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(154, 23)
        Me.Label24.TabIndex = 50
        Me.Label24.Text = "Credential Domain (optional)"
        '
        'SMTP_CredPassword
        '
        Me.SMTP_CredPassword.BackColor = System.Drawing.Color.White
        Me.SMTP_CredPassword.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SMTP_CredPassword.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.SMTP_CredPassword.Location = New System.Drawing.Point(357, 68)
        Me.SMTP_CredPassword.Name = "SMTP_CredPassword"
        Me.SMTP_CredPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.SMTP_CredPassword.Size = New System.Drawing.Size(170, 21)
        Me.SMTP_CredPassword.TabIndex = 44
        '
        'SMTP_CredUsername
        '
        Me.SMTP_CredUsername.BackColor = System.Drawing.Color.White
        Me.SMTP_CredUsername.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SMTP_CredUsername.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.SMTP_CredUsername.Location = New System.Drawing.Point(357, 28)
        Me.SMTP_CredUsername.Name = "SMTP_CredUsername"
        Me.SMTP_CredUsername.Size = New System.Drawing.Size(170, 21)
        Me.SMTP_CredUsername.TabIndex = 43
        Me.SMTP_CredUsername.Text = ""
        '
        'Label25
        '
        Me.Label25.ForeColor = System.Drawing.Color.Black
        Me.Label25.Location = New System.Drawing.Point(357, 12)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(154, 23)
        Me.Label25.TabIndex = 48
        Me.Label25.Text = "Credential User name"
        '
        'Label26
        '
        Me.Label26.ForeColor = System.Drawing.Color.Black
        Me.Label26.Location = New System.Drawing.Point(357, 52)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(154, 23)
        Me.Label26.TabIndex = 47
        Me.Label26.Text = "Credential Password"
        '
        'SMTP_CredentialAuthentication
        '
        Me.SMTP_CredentialAuthentication.AutoSize = True
        Me.SMTP_CredentialAuthentication.Checked = True
        Me.SMTP_CredentialAuthentication.Location = New System.Drawing.Point(22, 76)
        Me.SMTP_CredentialAuthentication.Name = "SMTP_CredentialAuthentication"
        Me.SMTP_CredentialAuthentication.Size = New System.Drawing.Size(77, 17)
        Me.SMTP_CredentialAuthentication.TabIndex = 40
        Me.SMTP_CredentialAuthentication.TabStop = True
        Me.SMTP_CredentialAuthentication.Text = "Credentials"
        Me.SMTP_CredentialAuthentication.UseVisualStyleBackColor = True
        '
        'SMTP_AuthPassword
        '
        Me.SMTP_AuthPassword.BackColor = System.Drawing.Color.White
        Me.SMTP_AuthPassword.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SMTP_AuthPassword.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.SMTP_AuthPassword.Location = New System.Drawing.Point(147, 68)
        Me.SMTP_AuthPassword.Name = "SMTP_AuthPassword"
        Me.SMTP_AuthPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.SMTP_AuthPassword.Size = New System.Drawing.Size(178, 21)
        Me.SMTP_AuthPassword.TabIndex = 42
        '
        'SMTP_BasicAuthentication
        '
        Me.SMTP_BasicAuthentication.AutoSize = True
        Me.SMTP_BasicAuthentication.Location = New System.Drawing.Point(22, 53)
        Me.SMTP_BasicAuthentication.Name = "SMTP_BasicAuthentication"
        Me.SMTP_BasicAuthentication.Size = New System.Drawing.Size(51, 17)
        Me.SMTP_BasicAuthentication.TabIndex = 39
        Me.SMTP_BasicAuthentication.Text = "Basic"
        Me.SMTP_BasicAuthentication.UseVisualStyleBackColor = True
        '
        'SMTP_AuthUsername
        '
        Me.SMTP_AuthUsername.BackColor = System.Drawing.Color.White
        Me.SMTP_AuthUsername.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SMTP_AuthUsername.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.SMTP_AuthUsername.Location = New System.Drawing.Point(147, 28)
        Me.SMTP_AuthUsername.Name = "SMTP_AuthUsername"
        Me.SMTP_AuthUsername.Size = New System.Drawing.Size(178, 21)
        Me.SMTP_AuthUsername.TabIndex = 41
        Me.SMTP_AuthUsername.Text = " "
        '
        'SMTP_NoAuthentication
        '
        Me.SMTP_NoAuthentication.AutoSize = True
        Me.SMTP_NoAuthentication.Location = New System.Drawing.Point(22, 30)
        Me.SMTP_NoAuthentication.Name = "SMTP_NoAuthentication"
        Me.SMTP_NoAuthentication.Size = New System.Drawing.Size(110, 17)
        Me.SMTP_NoAuthentication.TabIndex = 38
        Me.SMTP_NoAuthentication.Text = "No Authentication"
        Me.SMTP_NoAuthentication.UseVisualStyleBackColor = True
        '
        'Label27
        '
        Me.Label27.ForeColor = System.Drawing.Color.Black
        Me.Label27.Location = New System.Drawing.Point(147, 12)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(193, 23)
        Me.Label27.TabIndex = 44
        Me.Label27.Text = "SMTP User name Authentication"
        '
        'Label28
        '
        Me.Label28.ForeColor = System.Drawing.Color.Black
        Me.Label28.Location = New System.Drawing.Point(147, 52)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(193, 23)
        Me.Label28.TabIndex = 43
        Me.Label28.Text = "SMTP Password Authentication"
        '
        'SMTP_Exchange
        '
        Me.SMTP_Exchange.BackColor = System.Drawing.Color.Transparent
        Me.SMTP_Exchange.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.SMTP_Exchange.Location = New System.Drawing.Point(19, 200)
        Me.SMTP_Exchange.Name = "SMTP_Exchange"
        Me.SMTP_Exchange.Size = New System.Drawing.Size(112, 24)
        Me.SMTP_Exchange.TabIndex = 58
        Me.SMTP_Exchange.Text = "Exchange Server"
        Me.SMTP_Exchange.UseVisualStyleBackColor = False
        '
        'SMTP_Port
        '
        Me.SMTP_Port.BackColor = System.Drawing.Color.White
        Me.SMTP_Port.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SMTP_Port.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.SMTP_Port.Location = New System.Drawing.Point(276, 19)
        Me.SMTP_Port.Name = "SMTP_Port"
        Me.SMTP_Port.Size = New System.Drawing.Size(56, 21)
        Me.SMTP_Port.TabIndex = 53
        Me.SMTP_Port.Text = "587"
        '
        'SMTP_Hostname
        '
        Me.SMTP_Hostname.BackColor = System.Drawing.Color.White
        Me.SMTP_Hostname.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SMTP_Hostname.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.SMTP_Hostname.Location = New System.Drawing.Point(6, 19)
        Me.SMTP_Hostname.Name = "SMTP_Hostname"
        Me.SMTP_Hostname.Size = New System.Drawing.Size(264, 21)
        Me.SMTP_Hostname.TabIndex = 52
        Me.SMTP_Hostname.Text = ""
        '
        'Label30
        '
        Me.Label30.ForeColor = System.Drawing.Color.Black
        Me.Label30.Location = New System.Drawing.Point(6, 3)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(264, 23)
        Me.Label30.TabIndex = 57
        Me.Label30.Text = "SMTP Server Domain, Host name, or IP address"
        '
        'Label29
        '
        Me.Label29.ForeColor = System.Drawing.Color.Black
        Me.Label29.Location = New System.Drawing.Point(276, 3)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(100, 23)
        Me.Label29.TabIndex = 55
        Me.Label29.Text = "Port"
        '
        'TabPage9
        '
        Me.TabPage9.Controls.Add(Me.chkBodyIsHtml)
        Me.TabPage9.Controls.Add(Me.GroupBox5)
        Me.TabPage9.Controls.Add(Me.MSG_ATTACHMENT_FILENAME)
        Me.TabPage9.Controls.Add(Me.Label38)
        Me.TabPage9.Controls.Add(Me.Button10)
        Me.TabPage9.Controls.Add(Me.MSG_BCC)
        Me.TabPage9.Controls.Add(Me.Label32)
        Me.TabPage9.Controls.Add(Me.MSG_CC)
        Me.TabPage9.Controls.Add(Me.Label33)
        Me.TabPage9.Controls.Add(Me.MSG_TO)
        Me.TabPage9.Controls.Add(Me.MSG_SUBJECT)
        Me.TabPage9.Controls.Add(Me.Label34)
        Me.TabPage9.Controls.Add(Me.MSG_BODY)
        Me.TabPage9.Controls.Add(Me.Label35)
        Me.TabPage9.Controls.Add(Me.MSG_FROM_EMAIL)
        Me.TabPage9.Controls.Add(Me.MSG_FROM_NAME)
        Me.TabPage9.Controls.Add(Me.Label36)
        Me.TabPage9.Controls.Add(Me.Label37)
        Me.TabPage9.Controls.Add(Me.Label39)
        Me.TabPage9.Location = New System.Drawing.Point(4, 22)
        Me.TabPage9.Name = "TabPage9"
        Me.TabPage9.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage9.Size = New System.Drawing.Size(728, 302)
        Me.TabPage9.TabIndex = 1
        Me.TabPage9.Text = "Message Settings"
        Me.TabPage9.UseVisualStyleBackColor = True
        '
        'chkBodyIsHtml
        '
        Me.chkBodyIsHtml.AutoSize = True
        Me.chkBodyIsHtml.Location = New System.Drawing.Point(353, 135)
        Me.chkBodyIsHtml.Name = "chkBodyIsHtml"
        Me.chkBodyIsHtml.Size = New System.Drawing.Size(155, 17)
        Me.chkBodyIsHtml.TabIndex = 87
        Me.chkBodyIsHtml.Text = "Message Body Html Format"
        Me.chkBodyIsHtml.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.MSG_ATTACHMENT_HTML)
        Me.GroupBox5.Controls.Add(Me.MSG_ATTACHMENT_JSON)
        Me.GroupBox5.Controls.Add(Me.MSG_ATTACHMENT_NONE)
        Me.GroupBox5.Controls.Add(Me.MSG_ATTACHMENT_PDF_FLATTENED)
        Me.GroupBox5.Controls.Add(Me.MSG_ATTACHMENT_PDF)
        Me.GroupBox5.Controls.Add(Me.MSG_ATTACHMENT_XML)
        Me.GroupBox5.Controls.Add(Me.MSG_ATTACHMENT_FDF)
        Me.GroupBox5.Controls.Add(Me.MSG_ATTACHMENT_XDP)
        Me.GroupBox5.Controls.Add(Me.MSG_ATTACHMENT_XFDF)
        Me.GroupBox5.Location = New System.Drawing.Point(528, 56)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(197, 96)
        Me.GroupBox5.TabIndex = 86
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Attachment Format"
        '
        'MSG_ATTACHMENT_HTML
        '
        Me.MSG_ATTACHMENT_HTML.AutoSize = True
        Me.MSG_ATTACHMENT_HTML.Location = New System.Drawing.Point(131, 49)
        Me.MSG_ATTACHMENT_HTML.Name = "MSG_ATTACHMENT_HTML"
        Me.MSG_ATTACHMENT_HTML.Size = New System.Drawing.Size(55, 17)
        Me.MSG_ATTACHMENT_HTML.TabIndex = 87
        Me.MSG_ATTACHMENT_HTML.Text = "HTML"
        Me.MSG_ATTACHMENT_HTML.UseVisualStyleBackColor = True
        '
        'MSG_ATTACHMENT_JSON
        '
        Me.MSG_ATTACHMENT_JSON.AutoSize = True
        Me.MSG_ATTACHMENT_JSON.Location = New System.Drawing.Point(72, 73)
        Me.MSG_ATTACHMENT_JSON.Name = "MSG_ATTACHMENT_JSON"
        Me.MSG_ATTACHMENT_JSON.Size = New System.Drawing.Size(53, 17)
        Me.MSG_ATTACHMENT_JSON.TabIndex = 84
        Me.MSG_ATTACHMENT_JSON.Text = "JSON"
        Me.MSG_ATTACHMENT_JSON.UseVisualStyleBackColor = True
        '
        'MSG_ATTACHMENT_NONE
        '
        Me.MSG_ATTACHMENT_NONE.AutoSize = True
        Me.MSG_ATTACHMENT_NONE.Location = New System.Drawing.Point(131, 73)
        Me.MSG_ATTACHMENT_NONE.Name = "MSG_ATTACHMENT_NONE"
        Me.MSG_ATTACHMENT_NONE.Size = New System.Drawing.Size(56, 17)
        Me.MSG_ATTACHMENT_NONE.TabIndex = 85
        Me.MSG_ATTACHMENT_NONE.Text = "NONE"
        Me.MSG_ATTACHMENT_NONE.UseVisualStyleBackColor = True
        '
        'MSG_ATTACHMENT_PDF_FLATTENED
        '
        Me.MSG_ATTACHMENT_PDF_FLATTENED.AutoSize = True
        Me.MSG_ATTACHMENT_PDF_FLATTENED.Location = New System.Drawing.Point(120, 24)
        Me.MSG_ATTACHMENT_PDF_FLATTENED.Name = "MSG_ATTACHMENT_PDF_FLATTENED"
        Me.MSG_ATTACHMENT_PDF_FLATTENED.Size = New System.Drawing.Size(74, 17)
        Me.MSG_ATTACHMENT_PDF_FLATTENED.TabIndex = 86
        Me.MSG_ATTACHMENT_PDF_FLATTENED.Text = "FLATTEN"
        Me.MSG_ATTACHMENT_PDF_FLATTENED.UseVisualStyleBackColor = True
        '
        'MSG_ATTACHMENT_PDF
        '
        Me.MSG_ATTACHMENT_PDF.AutoSize = True
        Me.MSG_ATTACHMENT_PDF.Checked = True
        Me.MSG_ATTACHMENT_PDF.Location = New System.Drawing.Point(16, 24)
        Me.MSG_ATTACHMENT_PDF.Name = "MSG_ATTACHMENT_PDF"
        Me.MSG_ATTACHMENT_PDF.Size = New System.Drawing.Size(46, 17)
        Me.MSG_ATTACHMENT_PDF.TabIndex = 79
        Me.MSG_ATTACHMENT_PDF.TabStop = True
        Me.MSG_ATTACHMENT_PDF.Text = "PDF"
        Me.MSG_ATTACHMENT_PDF.UseVisualStyleBackColor = True
        '
        'MSG_ATTACHMENT_XML
        '
        Me.MSG_ATTACHMENT_XML.AutoSize = True
        Me.MSG_ATTACHMENT_XML.Location = New System.Drawing.Point(15, 49)
        Me.MSG_ATTACHMENT_XML.Name = "MSG_ATTACHMENT_XML"
        Me.MSG_ATTACHMENT_XML.Size = New System.Drawing.Size(47, 17)
        Me.MSG_ATTACHMENT_XML.TabIndex = 81
        Me.MSG_ATTACHMENT_XML.Text = "XML"
        Me.MSG_ATTACHMENT_XML.UseVisualStyleBackColor = True
        '
        'MSG_ATTACHMENT_FDF
        '
        Me.MSG_ATTACHMENT_FDF.AutoSize = True
        Me.MSG_ATTACHMENT_FDF.Location = New System.Drawing.Point(72, 25)
        Me.MSG_ATTACHMENT_FDF.Name = "MSG_ATTACHMENT_FDF"
        Me.MSG_ATTACHMENT_FDF.Size = New System.Drawing.Size(45, 17)
        Me.MSG_ATTACHMENT_FDF.TabIndex = 80
        Me.MSG_ATTACHMENT_FDF.Text = "FDF"
        Me.MSG_ATTACHMENT_FDF.UseVisualStyleBackColor = True
        '
        'MSG_ATTACHMENT_XDP
        '
        Me.MSG_ATTACHMENT_XDP.AutoSize = True
        Me.MSG_ATTACHMENT_XDP.Location = New System.Drawing.Point(72, 49)
        Me.MSG_ATTACHMENT_XDP.Name = "MSG_ATTACHMENT_XDP"
        Me.MSG_ATTACHMENT_XDP.Size = New System.Drawing.Size(47, 17)
        Me.MSG_ATTACHMENT_XDP.TabIndex = 82
        Me.MSG_ATTACHMENT_XDP.Text = "XDP"
        Me.MSG_ATTACHMENT_XDP.UseVisualStyleBackColor = True
        '
        'MSG_ATTACHMENT_XFDF
        '
        Me.MSG_ATTACHMENT_XFDF.AutoSize = True
        Me.MSG_ATTACHMENT_XFDF.Location = New System.Drawing.Point(16, 72)
        Me.MSG_ATTACHMENT_XFDF.Name = "MSG_ATTACHMENT_XFDF"
        Me.MSG_ATTACHMENT_XFDF.Size = New System.Drawing.Size(52, 17)
        Me.MSG_ATTACHMENT_XFDF.TabIndex = 83
        Me.MSG_ATTACHMENT_XFDF.Text = "XFDF"
        Me.MSG_ATTACHMENT_XFDF.UseVisualStyleBackColor = True
        '
        'MSG_ATTACHMENT_FILENAME
        '
        Me.MSG_ATTACHMENT_FILENAME.BackColor = System.Drawing.Color.White
        Me.MSG_ATTACHMENT_FILENAME.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MSG_ATTACHMENT_FILENAME.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.MSG_ATTACHMENT_FILENAME.HideSelection = False
        Me.MSG_ATTACHMENT_FILENAME.Location = New System.Drawing.Point(528, 24)
        Me.MSG_ATTACHMENT_FILENAME.Name = "MSG_ATTACHMENT_FILENAME"
        Me.MSG_ATTACHMENT_FILENAME.Size = New System.Drawing.Size(192, 21)
        Me.MSG_ATTACHMENT_FILENAME.TabIndex = 78
        Me.MSG_ATTACHMENT_FILENAME.Text = "attachment.pdf"
        Me.MSG_ATTACHMENT_FILENAME.WordWrap = False
        '
        'Label38
        '
        Me.Label38.ForeColor = System.Drawing.Color.Black
        Me.Label38.Location = New System.Drawing.Point(525, 8)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(195, 23)
        Me.Label38.TabIndex = 78
        Me.Label38.Text = "Attachment Filename"
        '
        'Button10
        '
        Me.Button10.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Button10.ForeColor = System.Drawing.Color.Black
        Me.Button10.Location = New System.Drawing.Point(528, 160)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(192, 128)
        Me.Button10.TabIndex = 85
        Me.Button10.Text = "EMAIL MERGED PDFS"
        Me.Button10.UseVisualStyleBackColor = False
        '
        'MSG_BCC
        '
        Me.MSG_BCC.BackColor = System.Drawing.Color.White
        Me.MSG_BCC.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MSG_BCC.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.MSG_BCC.HideSelection = False
        Me.MSG_BCC.Location = New System.Drawing.Point(272, 112)
        Me.MSG_BCC.Name = "MSG_BCC"
        Me.MSG_BCC.Size = New System.Drawing.Size(247, 21)
        Me.MSG_BCC.TabIndex = 67
        Me.MSG_BCC.WordWrap = False
        '
        'Label32
        '
        Me.Label32.ForeColor = System.Drawing.Color.Black
        Me.Label32.Location = New System.Drawing.Point(269, 96)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(260, 23)
        Me.Label32.TabIndex = 76
        Me.Label32.Text = "Blind Carbon Copy List (BCC) (Semi-colon Separated)"
        '
        'MSG_CC
        '
        Me.MSG_CC.BackColor = System.Drawing.Color.White
        Me.MSG_CC.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MSG_CC.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.MSG_CC.HideSelection = False
        Me.MSG_CC.Location = New System.Drawing.Point(272, 69)
        Me.MSG_CC.Name = "MSG_CC"
        Me.MSG_CC.Size = New System.Drawing.Size(247, 21)
        Me.MSG_CC.TabIndex = 66
        Me.MSG_CC.WordWrap = False
        '
        'Label33
        '
        Me.Label33.ForeColor = System.Drawing.Color.Black
        Me.Label33.Location = New System.Drawing.Point(269, 53)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(250, 23)
        Me.Label33.TabIndex = 73
        Me.Label33.Text = "Carbon Copy List (CC) (Semi-colon Separated)"
        '
        'MSG_TO
        '
        Me.MSG_TO.BackColor = System.Drawing.Color.White
        Me.MSG_TO.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MSG_TO.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.MSG_TO.HideSelection = False
        Me.MSG_TO.Location = New System.Drawing.Point(272, 24)
        Me.MSG_TO.Name = "MSG_TO"
        Me.MSG_TO.Size = New System.Drawing.Size(247, 21)
        Me.MSG_TO.TabIndex = 65
        Me.MSG_TO.WordWrap = False
        '
        'MSG_SUBJECT
        '
        Me.MSG_SUBJECT.BackColor = System.Drawing.Color.White
        Me.MSG_SUBJECT.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MSG_SUBJECT.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.MSG_SUBJECT.Location = New System.Drawing.Point(16, 112)
        Me.MSG_SUBJECT.Name = "MSG_SUBJECT"
        Me.MSG_SUBJECT.Size = New System.Drawing.Size(247, 21)
        Me.MSG_SUBJECT.TabIndex = 68
        Me.MSG_SUBJECT.Text = "TEST"
        '
        'Label34
        '
        Me.Label34.ForeColor = System.Drawing.Color.Black
        Me.Label34.Location = New System.Drawing.Point(16, 96)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(168, 23)
        Me.Label34.TabIndex = 66
        Me.Label34.Text = "Message Subject"
        '
        'MSG_BODY
        '
        Me.MSG_BODY.AcceptsReturn = True
        Me.MSG_BODY.BackColor = System.Drawing.Color.White
        Me.MSG_BODY.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MSG_BODY.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.MSG_BODY.HideSelection = False
        Me.MSG_BODY.Location = New System.Drawing.Point(16, 155)
        Me.MSG_BODY.Multiline = True
        Me.MSG_BODY.Name = "MSG_BODY"
        Me.MSG_BODY.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.MSG_BODY.Size = New System.Drawing.Size(503, 141)
        Me.MSG_BODY.TabIndex = 84
        Me.MSG_BODY.Text = "PDF Form Data - See Attachment"
        Me.MSG_BODY.WordWrap = False
        '
        'Label35
        '
        Me.Label35.ForeColor = System.Drawing.Color.Black
        Me.Label35.Location = New System.Drawing.Point(16, 141)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(99, 56)
        Me.Label35.TabIndex = 65
        Me.Label35.Text = "Message Body"
        '
        'MSG_FROM_EMAIL
        '
        Me.MSG_FROM_EMAIL.BackColor = System.Drawing.Color.White
        Me.MSG_FROM_EMAIL.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MSG_FROM_EMAIL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.MSG_FROM_EMAIL.Location = New System.Drawing.Point(16, 67)
        Me.MSG_FROM_EMAIL.Name = "MSG_FROM_EMAIL"
        Me.MSG_FROM_EMAIL.Size = New System.Drawing.Size(247, 21)
        Me.MSG_FROM_EMAIL.TabIndex = 64
        Me.MSG_FROM_EMAIL.Text = ""
        '
        'MSG_FROM_NAME
        '
        Me.MSG_FROM_NAME.BackColor = System.Drawing.Color.White
        Me.MSG_FROM_NAME.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MSG_FROM_NAME.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.MSG_FROM_NAME.Location = New System.Drawing.Point(16, 24)
        Me.MSG_FROM_NAME.Name = "MSG_FROM_NAME"
        Me.MSG_FROM_NAME.Size = New System.Drawing.Size(247, 21)
        Me.MSG_FROM_NAME.TabIndex = 63
        Me.MSG_FROM_NAME.Text = ""
        '
        'Label36
        '
        Me.Label36.ForeColor = System.Drawing.Color.Black
        Me.Label36.Location = New System.Drawing.Point(16, 51)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(168, 23)
        Me.Label36.TabIndex = 61
        Me.Label36.Text = "From Email (required)"
        '
        'Label37
        '
        Me.Label37.ForeColor = System.Drawing.Color.Black
        Me.Label37.Location = New System.Drawing.Point(16, 8)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(99, 23)
        Me.Label37.TabIndex = 60
        Me.Label37.Text = "From Name"
        '
        'Label39
        '
        Me.Label39.ForeColor = System.Drawing.Color.Black
        Me.Label39.Location = New System.Drawing.Point(269, 8)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(250, 23)
        Me.Label39.TabIndex = 69
        Me.Label39.Text = "Recipient To List (Semi-colon Separated)"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(8, 416)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(744, 23)
        Me.ProgressBar1.TabIndex = 134
        '
        'lblMessage
        '
        Me.lblMessage.BackColor = System.Drawing.Color.Transparent
        Me.lblMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessage.Location = New System.Drawing.Point(8, 0)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(696, 56)
        Me.lblMessage.TabIndex = 137
        Me.lblMessage.Text = "Status: ready..."
        '
        'txtServer_Port
        '
        Me.txtServer_Port.BackColor = System.Drawing.Color.White
        Me.txtServer_Port.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServer_Port.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtServer_Port.Location = New System.Drawing.Point(310, 35)
        Me.txtServer_Port.Name = "txtServer_Port"
        Me.txtServer_Port.Size = New System.Drawing.Size(56, 21)
        Me.txtServer_Port.TabIndex = 36
        Me.txtServer_Port.Text = "25"
        '
        'txtServer_Name
        '
        Me.txtServer_Name.BackColor = System.Drawing.Color.White
        Me.txtServer_Name.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServer_Name.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtServer_Name.Location = New System.Drawing.Point(40, 35)
        Me.txtServer_Name.Name = "txtServer_Name"
        Me.txtServer_Name.Size = New System.Drawing.Size(264, 21)
        Me.txtServer_Name.TabIndex = 35
        Me.txtServer_Name.Text = "domain.com"
        '
        'chkExchange
        '
        Me.chkExchange.BackColor = System.Drawing.Color.Transparent
        Me.chkExchange.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.chkExchange.Location = New System.Drawing.Point(52, 290)
        Me.chkExchange.Name = "chkExchange"
        Me.chkExchange.Size = New System.Drawing.Size(112, 24)
        Me.chkExchange.TabIndex = 47
        Me.chkExchange.Text = "Exchange Server"
        Me.chkExchange.UseVisualStyleBackColor = False
        '
        'chkFDFTKSetup
        '
        Me.chkFDFTKSetup.BackColor = System.Drawing.Color.Transparent
        Me.chkFDFTKSetup.Checked = True
        Me.chkFDFTKSetup.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkFDFTKSetup.Enabled = False
        Me.chkFDFTKSetup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.chkFDFTKSetup.Location = New System.Drawing.Point(52, 260)
        Me.chkFDFTKSetup.Name = "chkFDFTKSetup"
        Me.chkFDFTKSetup.Size = New System.Drawing.Size(184, 24)
        Me.chkFDFTKSetup.TabIndex = 46
        Me.chkFDFTKSetup.Text = "FDFToolkit.net is Installed"
        Me.chkFDFTKSetup.UseVisualStyleBackColor = False
        '
        'Label21
        '
        Me.Label21.ForeColor = System.Drawing.Color.Black
        Me.Label21.Location = New System.Drawing.Point(310, 19)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(100, 23)
        Me.Label21.TabIndex = 45
        Me.Label21.Text = "Port"
        '
        'Label22
        '
        Me.Label22.ForeColor = System.Drawing.Color.Black
        Me.Label22.Location = New System.Drawing.Point(40, 19)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(264, 23)
        Me.Label22.TabIndex = 46
        Me.Label22.Text = "SMTP Server Domain, Host name, or IP address"
        '
        'Label23
        '
        Me.Label23.ForeColor = System.Drawing.Color.Black
        Me.Label23.Location = New System.Drawing.Point(37, 243)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(100, 23)
        Me.Label23.TabIndex = 49
        Me.Label23.Text = "Server Settings"
        '
        'frmMerge
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(760, 451)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btnFilterMergePanel)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.lblMessage)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMerge"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Mass Merge PDFs"
        Me.pnlDataSource.ResumeLayout(False)
        Me.pnlDataSource.PerformLayout()
        Me.pnlFields.ResumeLayout(False)
        Me.pnlFields.PerformLayout()
        Me.pnlFilterPrint.ResumeLayout(False)
        Me.pnlFilterPrint.PerformLayout()
        Me.pnlOutputPDFs.ResumeLayout(False)
        Me.pnlOutputPDFs.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage7.ResumeLayout(False)
        Me.TabControl2.ResumeLayout(False)
        Me.TabPage8.ResumeLayout(False)
        Me.TabPage8.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.TabPage9.ResumeLayout(False)
        Me.TabPage9.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.ResumeLayout(False)
    End Sub
#End Region
    Dim Database_Format(5) As String
    Dim Data_Fields(4, 4) As String
    Dim Database_Filter As String
    Dim XML_RAW_NAME As String = ""
    Public SessionBytes() As Byte = Nothing
    Public pdfOwnerPassword As String = ""
    Private strNameConventions(0) As String
    Public clsFileXML1 As New clsFileXML(Me)
    Public blnCancelProcess As Boolean = False
    Public Sub LoadPDF(ByVal session() As Byte, ByVal pdfownerPw As String)
        SessionBytes = session
        pdfOwnerPassword = pdfownerPw
    End Sub
    Private Sub btnSelectSource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectSource.Click
        SelectSource()
    End Sub
    Public Sub SelectSource(Optional ByVal fn As String = "")
        On Error GoTo errHandler
        frmMerge1 = Me
        lblStatus.Text = ""
        Dim strFilnm As String
        If Not (String.IsNullOrEmpty(fn & "")) Then
            If System.IO.File.Exists(fn & "") Then
                cmnDlg.InitialDirectory = System.IO.Path.GetDirectoryName(fn) & ""
                cmnDlg.FileName = fn
            Else
                cmnDlg.InitialDirectory = Application.StartupPath.ToString() & ""
                cmnDlg.Filter = Database_Filter
                If Not String.IsNullOrEmpty(txtDataFields_0.Text & "") Then
                    If Not CStr(txtDataFields_0.Text & "").ToLower = "datasource".ToLower() Then
                        cmnDlg.FileName = txtDataFields_0.Text & ""
                    Else
                        cmnDlg.FileName = ""
                    End If
                Else
                    cmnDlg.FileName = ""
                End If
                cmnDlg.ShowDialog()
            End If
        Else
            cmnDlg.InitialDirectory = Application.StartupPath.ToString() & ""
            cmnDlg.Filter = Database_Filter
            If Not String.IsNullOrEmpty(txtDataFields_0.Text & "") Then
                If Not CStr(txtDataFields_0.Text & "").ToLower = "datasource".ToLower() Then
                    cmnDlg.FileName = txtDataFields_0.Text & ""
                Else
                    cmnDlg.FileName = ""
                End If
            Else
                cmnDlg.FileName = ""
            End If
            cmnDlg.ShowDialog()
        End If
        strFilnm = cmnDlg.FileName
        txtDataFields_0.Text = cmnDlg.FileName
        Select Case New System.IO.FileInfo(cmnDlg.FileName).Extension
            Case ".xml"
                modSources.Database_Connection = cmnDlg.FileName
                txtDataFields_0.Visible = True
                txtDataFields_1.Visible = False
                txtDataFields_2.Visible = False
                txtDataFields_3.Visible = False
                clsFileXML1.Data_SourcePath = cmnDlg.FileName & ""
                clsFileXML1.Data_Username = txtDataFields_1.Text & ""
                clsFileXML1.Data_Password = txtDataFields_2.Text & ""
                clsFileXML1.Data_DatabasePassword = txtDataFields_3.Text
                clsFileXML1.Database_Connection = cmnDlg.FileName
            Case ".mdb"
                If txtDataFields_3.Text = "Database Password" Or txtDataFields_3.Text = "" Then
                    modADOConn.OLEDB_MSJet(strFilnm, CStr(IIf(txtDataFields_1.Text = "Username", "", txtDataFields_1.Text)), CStr(IIf(txtDataFields_2.Text = "Password", "", txtDataFields_2.Text)))
                Else
                    modADOConn.OLEDB_MSJetDBPassword(strFilnm, txtDataFields_3.Text, CStr(IIf(txtDataFields_1.Text = "Username", "", txtDataFields_1.Text)), CStr(IIf(txtDataFields_2.Text = "Password", "", txtDataFields_2.Text)))
                End If
                modSources.Database_Connection = modADOConn.Connection_String_VB
                clsFileXML1.Data_SourcePath = cmnDlg.FileName & ""
                clsFileXML1.Data_Username = txtDataFields_1.Text & ""
                clsFileXML1.Data_Password = txtDataFields_2.Text & ""
                clsFileXML1.Data_DatabasePassword = txtDataFields_3.Text
                clsFileXML1.Database_Connection = modSources.Database_Connection & ""
                txtDataFields_0.Visible = True
                txtDataFields_1.Visible = True
                txtDataFields_2.Visible = True
                txtDataFields_3.Visible = True
            Case ".xls"
                modADOConn.OLEDB_MSJetExcell(cmnDlg.FileName, True)
                modSources.Database_Connection = modADOConn.Connection_String_Net
                clsFileXML1.Data_SourcePath = cmnDlg.FileName & ""
                clsFileXML1.Data_Username = txtDataFields_1.Text & ""
                clsFileXML1.Data_Password = txtDataFields_2.Text & ""
                clsFileXML1.Data_DatabasePassword = txtDataFields_3.Text
                clsFileXML1.Database_Connection = modSources.Database_Connection & ""
                txtDataFields_0.Visible = True
                txtDataFields_1.Visible = True
                txtDataFields_2.Visible = True
                txtDataFields_3.Visible = True
        End Select
        lblStatus.Text = modSources.Database_Connection
        If Test_Connection() Then
            lblConnected.Text = "Test: Connected"
            btnNext.Visible = True
        Else
            lblConnected.Text = "Test: Not Connected"
            btnNext.Visible = False
        End If
        If PopulateTables() Then
            Populate_TableCombo(cmbDBTables, Me)
            Populate_TableCombo(cmbDBTablesFilter, Me)
        End If
        Exit Sub
errHandler:
        lblStatus.Text = "Datastructure Not Loaded"
    End Sub
    Private Sub frmMassPopulateAndEmail_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        frm_Close()
    End Sub
    Private Sub frmDataSource_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ShowPanel(pnlDataSource)
        Data_Fields(0, 0) = "Datasource"
        Data_Fields(1, 0) = "admin"
        Data_Fields(2, 0) = "Password"
        Data_Fields(3, 0) = "Database Password"
        txtDataFields_0.Text = Data_Fields(0, 0)
        txtDataFields_1.Text = Data_Fields(1, 0)
        txtDataFields_2.Text = Data_Fields(2, 0)
        txtDataFields_3.Text = Data_Fields(3, 0)
        If txtDataFields_0.Text = "" Then
            txtDataFields_0.Visible = False
        Else
            txtDataFields_0.Visible = True
        End If
        If txtDataFields_1.Text = "" Then
            txtDataFields_1.Visible = False
        Else
            txtDataFields_1.Visible = True
        End If
        If txtDataFields_2.Text = "" Then
            txtDataFields_2.Visible = False
        Else
            txtDataFields_2.Visible = True
        End If
        If txtDataFields_3.Text = "" Then
            txtDataFields_3.Visible = False
        Else
            txtDataFields_3.Visible = True
        End If
        Database_Filter = "MDB|*.MDB|XLS|*.XLS|XML|*.XML"
    End Sub
    Private Sub btnLoadPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadPDF.Click
        If Load_PDF(SessionBytes, pdfOwnerPassword) Then
            lnkAddNew.Enabled = True
            lnkRemove.Enabled = True
            cmbPDFFields.Enabled = True
            cmbPDFFields.Items.Clear()
            Populate_PDFFields_Combo(cmbPDFFields, lstMappedFields, Me)
            UpdateMessage("Loaded PDF form success!")
        Else
            lnkAddNew.Enabled = False
            lnkRemove.Enabled = False
            cmbPDFFields.Enabled = False
            cmbPDFFields.Items.Clear()
            UpdateMessage("Error Loading PDF form.")
        End If
    End Sub
    Public fieldNames As New List(Of String)
    Public Function getAllFieldNames(Optional ByVal force As Boolean = False) As String()
        Try
            If force Or fieldNames.Count <= 0 Then
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
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
        Return Nothing
    End Function
    Private lv As New List(Of clsAutocomplete)
    Public cfdf As New FDFApp.FDFDoc_Class
    Public cpdf As New FDFApp.FDFApp_Class
    Public Function InjectFieldNameValues(ByVal strInput As String, ByVal cfdfDoc As FDFApp.FDFDoc_Class) As String
        Dim strTmp As String = strInput & ""
        If String.IsNullOrEmpty(strInput & "") Then
            Return strInput & ""
        End If
        For Each fld As String In fieldNames
            Try
                If strTmp.Contains("{" & fld.ToString & "}") Then
                    strTmp = strTmp.Replace("{" & fld.ToString & "}", cfdfDoc.FDFGetValue(fld.ToString.ToString.Replace("{", "").Replace("}", "").ToString(), False) & "")
                    Exit For
                End If
            Catch ex As Exception
                Err.Clear()
            End Try
        Next
        Return strTmp.ToString & ""
    End Function
    Public Function InjectFieldNameValues(ByVal strInput As String, ByVal dr As DataRow) As String
        Dim strTmp As String = strInput & ""
        If String.IsNullOrEmpty(strInput & "") Then
            Return strInput & ""
        End If
        For Each fld As DataColumn In dr.Table.Columns
            Try
                If strTmp.Contains("{" & fld.ColumnName.ToString & "}") Then
                    strTmp = strTmp.Replace("{" & fld.ColumnName.ToString & "}", CStr(dr(fld.ColumnName.ToString).ToString()) & "")
                    Exit For
                End If
            Catch ex As Exception
                Err.Clear()
            End Try
        Next
        Return strTmp.ToString & ""
    End Function
    Public Sub addAutoCompleteFields(ByVal sessionBytesTemp() As Byte, ByVal pdfownerPasswordTemp As String)
        Try
            SessionBytes = sessionBytesTemp
            pdfOwnerPassword = pdfownerPasswordTemp
            cfdf = cpdf.PDFOpenFromBuf(SessionBytes, True, True, pdfOwnerPassword)
            Dim MySource As New AutoCompleteStringCollection()
            For Each fld As String In getAllFieldNames(True)
                If Not String.IsNullOrEmpty(fld.ToString.Trim() & "") Then
                    MySource.Add("{" & fld.ToString.Trim() & "}")
                End If
            Next
            lv.Add(New clsAutocomplete(txtMappingRaw, fieldNames.ToArray(), False))
            lv.Add(New clsAutocomplete(MSG_FROM_NAME, fieldNames.ToArray(), False))
            lv.Add(New clsAutocomplete(MSG_FROM_EMAIL, fieldNames.ToArray(), False))
            lv.Add(New clsAutocomplete(MSG_SUBJECT, fieldNames.ToArray(), False))
            lv.Add(New clsAutocomplete(MSG_TO, fieldNames.ToArray(), False))
            lv.Add(New clsAutocomplete(MSG_CC, fieldNames.ToArray(), False))
            lv.Add(New clsAutocomplete(MSG_BCC, fieldNames.ToArray(), False))
            lv.Add(New clsAutocomplete(MSG_ATTACHMENT_FILENAME, fieldNames.ToArray(), False))
            lv.Add(New clsAutocomplete(txtOutputFilename, fieldNames.ToArray(), False))
            lv.Add(New clsAutocomplete(txtOutputFolder, fieldNames.ToArray(), False))
            lv.Add(New clsAutocomplete(MSG_BODY, fieldNames.ToArray(), True, True, Me.Owner))
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub lnkLoadPDF_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkLoadPDF.LinkClicked
        If Load_PDF(SessionBytes, pdfOwnerPassword) Then
            lnkAddNew.Enabled = True
            lnkRemove.Enabled = True
            cmbPDFFields.Enabled = True
            cmbPDFFields.Items.Clear()
            For Each itm As String In lstMappedFields.Items
                Try
                    RemoveMap(itm.ToString & "")
                Catch ex As Exception
                    Err.Clear()
                End Try
            Next
            lstMappedFields.Items.Clear()
            frmMerge1 = Me
            Populate_PDFFields_Combo(cmbPDFFields, lstMappedFields, Me)
            addAutoCompleteFields(SessionBytes, pdfOwnerPassword)
        Else
            frmMerge1 = Me
            lnkAddNew.Enabled = False
            lnkRemove.Enabled = False
            For Each itm As String In lstMappedFields.Items
                Try
                    RemoveMap(itm.ToString & "")
                Catch ex As Exception
                    Err.Clear()
                End Try
            Next
            lstMappedFields.Items.Clear()
            cmbPDFFields.Enabled = False
            cmbPDFFields.Items.Clear()
        End If
    End Sub
    Private Sub cmbDBTables_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDBTables.SelectedIndexChanged
        If cmbDBTables.SelectedIndex >= 0 Then
            Populate_DBFieldsList(lstDBFields, cmbDBTables.Text)
        End If
    End Sub
    Private Sub lnkAddNew_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkAddNew.LinkClicked
        Dim strX As String = ""
        Dim xItems As CheckedListBox.CheckedItemCollection, intSelCnt As Integer
        xItems = lstDBFields.CheckedItems
        For intSelCnt = 0 To xItems.Count - 1
            strX = strX & CStr(IIf(strX = "", "", " ")) & "{" & CStr(xItems(intSelCnt)) & "}"
        Next
        If Not String.IsNullOrEmpty(Me.txtMappingRaw.Text) Then
            Me.txtMappingRaw.Text &= "{" & strX & "}"
            AddMap(cmbPDFFields.Text, strX, cmbDBTables.Text, Me.txtMappingRaw.Text)
            lstMappedFields.Items.Add(Me.txtMappingRaw.Text)
        Else
            Me.txtMappingRaw.Text = cmbPDFFields.Text & "=" & "{" & strX & "}"
            AddMap(cmbPDFFields.Text, strX, cmbDBTables.Text, Me.txtMappingRaw.Text)
            lstMappedFields.Items.Add("{" & strX & "}")
        End If
        lstDBFields.ClearSelected()
    End Sub
    Private Sub lnkRemove_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkRemove.LinkClicked
        If lstMappedFields.SelectedIndex > -1 Then
            If Not lstMappedFields.SelectedItem Is Nothing Then
                modSources.RemoveMap(lstMappedFields.SelectedItem.ToString.Split("="c)(0))
                lstMappedFields.Items.Remove(lstMappedFields.SelectedItem)
                UpdateMappFieldsList()
                Return
            End If
        End If
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim dsx As New DataSet
        If Database_Connection.ToString.ToLower.EndsWith(".xml") Then
            dsx = MergeXML()
        ElseIf Database_Connection.ToString.ToLower.EndsWith(".json") Then
            dsx = MergeJSON()
        Else
            dsx = Merge()
        End If
    End Sub
    Private Sub txtDataFields_0_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)
    End Sub
    Private Sub lnkAddFilter_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkAddFilter.LinkClicked
        Add_FilterField(cmbDBFieldFilter.Text, cDBFieldType(cmbDBFieldFilter.SelectedIndex + 1), cmbDBTables.Text, txtFieldValue.Text & "", cmbComparison.Text & "")
        lstFilter.Items.Add(cmbDBFieldFilter.Text & " " & cmbComparison.Text & " " & txtFieldValue.Text & "")
        Dim dsx As New DataSet
        If Database_Connection.ToString.ToLower.EndsWith(".xml") Then
            dsx = MergeXML()
        ElseIf Database_Connection.ToString.ToLower.EndsWith(".json") Then
            dsx = MergeJSON()
        Else
            dsx = Merge()
        End If
        btnRecords_Affected.Text = "Records Affected = " & dsx.Tables(0).Rows.Count
    End Sub
    Private Sub cmbDBFieldFilter_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDBFieldFilter.SelectedIndexChanged
        If cmbDBFieldFilter.SelectedIndex < 0 Then Exit Sub
        txtFieldValue.Text = ""
        cmbComparison.Items.Clear()
        Select Case cDBFieldType(cmbDBFieldFilter.SelectedIndex + 1)
            Case ADODB.DataTypeEnum.adDate, ADODB.DataTypeEnum.adDBDate, ADODB.DataTypeEnum.adDBTime, ADODB.DataTypeEnum.adDBTimeStamp
                txtFieldValue.Text = Today.Date.ToString
                cmbComparison.Items.Add("="c)
                cmbComparison.Items.Add(">")
                cmbComparison.Items.Add("<")
                cmbComparison.Items.Add(">=")
                cmbComparison.Items.Add("<=")
                cmbComparison.Items.Add("NOT =")
            Case ADODB.DataTypeEnum.adBigInt, ADODB.DataTypeEnum.adCurrency, ADODB.DataTypeEnum.adDecimal, ADODB.DataTypeEnum.adDouble, ADODB.DataTypeEnum.adInteger, ADODB.DataTypeEnum.adNumeric, ADODB.DataTypeEnum.adSingle, ADODB.DataTypeEnum.adSmallInt, ADODB.DataTypeEnum.adTinyInt, ADODB.DataTypeEnum.adVarNumeric
                txtFieldValue.Text = "0"
                cmbComparison.Items.Add("="c)
                cmbComparison.Items.Add(">")
                cmbComparison.Items.Add("<")
                cmbComparison.Items.Add(">=")
                cmbComparison.Items.Add("<=")
                cmbComparison.Items.Add("NOT =")
            Case ADODB.DataTypeEnum.adBSTR, ADODB.DataTypeEnum.adChar, ADODB.DataTypeEnum.adLongVarChar, ADODB.DataTypeEnum.adLongVarWChar, ADODB.DataTypeEnum.adWChar, ADODB.DataTypeEnum.adVarWChar, ADODB.DataTypeEnum.adVarChar
                txtFieldValue.Text = ""
                cmbComparison.Items.Add("="c)
                cmbComparison.Items.Add("LIKE")
                cmbComparison.Items.Add("<>")
            Case ADODB.DataTypeEnum.adBoolean
                txtFieldValue.Text = "False"
                cmbComparison.Items.Add("="c)
            Case Else
                cmbComparison.Items.Add("="c)
                txtFieldValue.Text = ""
        End Select
    End Sub
    Private Sub cmbDBTablesFilter_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDBTablesFilter.SelectedIndexChanged
        If cmbDBTables.SelectedIndex < 0 Then Exit Sub
        Populate_DBFieldsCombo(cmbDBFieldFilter, cmbDBTablesFilter.Text & "")
    End Sub
    Private Sub btnRecords_Affected_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRecords_Affected.Click
        Dim dsx As New DataSet
        If Database_Connection.ToString.ToLower.EndsWith(".xml") Then
            dsx = MergeXML()
        ElseIf Database_Connection.ToString.ToLower.EndsWith(".json") Then
            dsx = MergeJSON()
        Else
            dsx = Merge()
        End If
        btnRecords_Affected.Text = "Records Affected = " & dsx.Tables(0).Rows.Count
    End Sub
    Private Sub btnOutputPDFs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOutputPDFs.Click
        Dim strNames As String = ""
        Dim dsx As New DataSet
        Dim btnOutputPDFsText As String = "CLICK TO OUTPUT MERGED PDFs"
        Dim frmMain1 As frmMain = DirectCast(Me.Owner, frmMain)
        frmMain1.Session("saved-output-merged") = frmMain1.Session
        Try
            blnCancelProcess = False
            If btnOutputPDFs.Text = "CANCEL PROCESS" Then
                blnCancelProcess = True
                btnOutputPDFs.Text = btnOutputPDFsText
                Exit Sub
            Else
                btnOutputPDFs.Text = "CANCEL PROCESS"
                Application.DoEvents()
            End If
            If Database_Connection.ToString.ToLower.EndsWith(".xml") Then
                dsx = MergeXML()
            ElseIf Database_Connection.ToString.ToLower.EndsWith(".json") Then
                dsx = MergeJSON()
            Else
                dsx = Merge()
            End If
            Dim outputFormat As String = "pdf"
            If Me.OUTPUT_FORMAT_PDF.Checked Then
                outputFormat = "pdf"
            ElseIf Me.OUTPUT_FORMAT_FDF.Checked Then
                outputFormat = "fdf"
            ElseIf Me.OUTPUT_FORMAT_XFDF.Checked Then
                outputFormat = "xfdf"
            ElseIf Me.OUTPUT_FORMAT_XDP.Checked Then
                outputFormat = "xdp"
            ElseIf Me.OUTPUT_FORMAT_XML.Checked Then
                outputFormat = "xml"
            Else
                outputFormat = "pdf"
            End If
            Dim drCntr As Integer = 0
            Dim tmStart As DateTime = DateTime.Now
            Dim clsInput As New clsPromptDialog
            Dim htmlSubmitAction As String = ""
            If Me.chkBodyIsHtml.Checked Then
                htmlSubmitAction = clsInput.ShowDialog("HTML submit action URL?", "HTML Submit Action URL?", Me, "", "OK")
            End If
            For Each dr As DataRow In dsx.Tables(0).Rows
                frmMain1.Session = frmMain1.Session("saved-output-merged")
                tmStart = DateTime.Now
                drCntr += 1
                ProgressBar1.Value = CInt((drCntr / dsx.Tables(0).Rows.Count) * 100)
                cfdf = cpdf.PDFOpenFromBuf(frmMain1.Session("saved-output-merged"), True, True, frmMain1.pdfOwnerPassword)
                Try
                    Select Case cfdf.Determine_Type(frmMain1.Session("saved-output-merged"))
                        Case FDFApp.FDFDoc_Class.FDFType.PDF
                            cfdf.XDPSetValuesFromDataRow(dr)
                            frmMain1.Session = cfdf.PDFMergeFDF2Buf(frmMain1.Session("saved-output-merged"), False, frmMain1.pdfOwnerPassword)
                        Case FDFApp.FDFDoc_Class.FDFType.XPDF
                            cfdf.XDPSetValuesFromDataRow(dr)
                            frmMain1.Session = cfdf.PDFMergeXDP2Buf(frmMain1.Session("saved-output-merged"), False, frmMain1.pdfOwnerPassword)
                        Case FDFApp.FDFDoc_Class.FDFType.XFA
                            cfdf.XDPSetValuesFromDataRow(dr)
                            frmMain1.Session = cfdf.PDFMergeXDP2Buf(frmMain1.Session("saved-output-merged"), False, frmMain1.pdfOwnerPassword)
                    End Select
                    If outputFormat = "pdf" Then
                        If chkOutpdfPDFFlattened.Checked Then
                            System.IO.File.WriteAllBytes(txtOutputFolder.Text.ToString().TrimEnd("\"c) & "\"c & InjectFieldNameValues(txtOutputFilename.Text.ToString(), dr), cfdf.PDFFlatten2Buf(frmMain1.Session, True, ""))
                        Else
                            System.IO.File.WriteAllBytes(txtOutputFolder.Text.ToString().TrimEnd("\"c) & "\"c & InjectFieldNameValues(txtOutputFilename.Text.ToString(), dr), frmMain1.Session)
                        End If
                    ElseIf outputFormat = "fdf" Then
                        System.IO.File.WriteAllBytes(txtOutputFolder.Text.ToString().TrimEnd("\"c) & "\"c & InjectFieldNameValues(txtOutputFilename.Text.ToString(), dr), cfdf.FDFSavetoBuf(FDFApp.FDFDoc_Class.FDFType.FDF, True))
                    ElseIf outputFormat = "xfdf" Then
                        System.IO.File.WriteAllBytes(txtOutputFolder.Text.ToString().TrimEnd("\"c) & "\"c & InjectFieldNameValues(txtOutputFilename.Text.ToString(), dr), cfdf.FDFSavetoBuf(FDFApp.FDFDoc_Class.FDFType.xFDF, True))
                    ElseIf outputFormat = "xdp" Then
                        System.IO.File.WriteAllBytes(txtOutputFolder.Text.ToString().TrimEnd("\"c) & "\"c & InjectFieldNameValues(txtOutputFilename.Text.ToString(), dr), cfdf.FDFSavetoBuf(FDFApp.FDFDoc_Class.FDFType.XDP, True))
                    ElseIf outputFormat = "xml" Then
                        System.IO.File.WriteAllBytes(txtOutputFolder.Text.ToString().TrimEnd("\"c) & "\"c & InjectFieldNameValues(txtOutputFilename.Text.ToString(), dr), cfdf.FDFSavetoBuf(FDFApp.FDFDoc_Class.FDFType.XML, True))
                    ElseIf outputFormat = "json" Then
                        System.IO.File.WriteAllBytes(txtOutputFolder.Text.ToString().TrimEnd("\"c) & "\"c & InjectFieldNameValues(txtOutputFilename.Text.ToString(), dr), cfdf.FDFSavetoBuf(FDFApp.FDFDoc_Class.FDFType.Json, True))
                    End If
                Catch ex2 As Exception
                    UpdateMessage("Error - Message not sent - " & DateTime.Now.ToLocalTime.ToString & Environment.NewLine & "Error: " & ex2.Message.ToString & "")
                    Err.Clear()
                End Try
            Next
        Catch ex As Exception
            Err.Clear()
        Finally
            Dim dlg As New DialogResult
            frmMain1.Session = frmMain1.Session("saved-output-merged")
            If MsgBox("Do you want to open the output folder?", MsgBoxStyle.YesNo Or MsgBoxStyle.ApplicationModal, "OPEN OUTPUT FOLDER") = MsgBoxResult.Yes Then
                Process.Start(Me.txtOutputFolder.Text.ToString.TrimEnd("\"c) & "\")
            End If
            btnOutputPDFs.Text = btnOutputPDFsText
        End Try
    End Sub
    Public Sub UpdateMappFieldsList()
        lstMappedFields.Items.Clear()
        For Each FieldMapX As FieldMap In modSources.FieldsMaps
            lstMappedFields.Items.Add(FieldMapX.DBMapRaw)
        Next
    End Sub
    Private Sub lnkSelect_Folder_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkSelect_Folder.LinkClicked
        Try
            Dim fs As New FolderSelect.FolderSelectDialog
            Dim frmMain1 As frmMain = DirectCast(Me.Owner, frmMain)
            fs.InitialDirectory = System.IO.Path.GetDirectoryName(frmMain1.fpath)
            fs.Title = "Select Output Folder:"
            Select Case fs.ShowDialog(Me.Handle)
                Case True
                    If System.IO.Directory.Exists(CStr(fs.FileName.ToString.TrimEnd("\"c) & "\"c)) Then
                        Me.txtOutputFolder.Text = CStr(fs.FileName.ToString.TrimEnd("\"c) & "\"c)
                        fs = Nothing
                    End If
                Case False
                    Return
            End Select
        Catch ex As Exception
            Err.Clear()
            fldOutput.ShowDialog()
            If Not fldOutput.SelectedPath = "" Then
                Me.txtOutputFolder.Text = fldOutput.SelectedPath
            End If
        End Try
    End Sub
    Private Sub pnlOutputPDFs_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)
    End Sub
    Private Sub pnlDataSource_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)
    End Sub
    Private Sub btnFilterMergePanel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterMergePanel.Click
        ShowPanel(pnlFilterPrint)
        Populate_TableCombo(Me.cmbDBTablesFilter, Me)
    End Sub
    Private Sub btnSavePDFs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSavePDFs.Click
        ShowPanel(pnlOutputPDFs)
    End Sub
    Private Sub btnStartOver_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ShowPanel(pnlDataSource)
    End Sub
    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        cmbDBTables.Items.Clear()
        If Populate_TableCombo(cmbDBTables, Me) Then
            ShowPanel(pnlFields)
        End If
    End Sub
    Private Sub lnkRemoveFilter_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkRemoveFilter.LinkClicked
        If Me.lstFilter.SelectedIndex >= 0 And Me.lstFilter.Items.Count > 0 Then
            RemoveFilter(Me.lstFilter.SelectedIndex)
            Me.lstFilter.Items.RemoveAt(Me.lstFilter.SelectedIndex)
        End If
        Dim dsx As New DataSet
        If Database_Connection.ToString.ToLower.EndsWith(".xml") Then
            dsx = MergeXML()
        ElseIf Database_Connection.ToString.ToLower.EndsWith(".json") Then
            dsx = MergeJSON()
        Else
            dsx = Merge()
        End If
        btnRecords_Affected.Text = "Records Affected = " & dsx.Tables(0).Rows.Count
    End Sub
    Public Sub frm_Close()
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
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub TabPage2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ShowPanel(pnlDataSource)
    End Sub
    Public Sub ShowPanel(ByVal pnl As Panel)
        Me.pnlDataSource.Visible = True
        Me.pnlFields.Visible = True
        Me.pnlFilterPrint.Visible = True
        Me.pnlOutputPDFs.Visible = True
        pnl.Show()
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub lstNameConventions_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub lstMappedFields_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstMappedFields.SelectedIndexChanged
        If lstMappedFields.SelectedIndex >= 0 Then
            Me.txtMappingRaw.Text = modSources.FieldsMaps(Me.lstMappedFields.SelectedIndex).DBMapRaw.ToString
        Else
            Me.txtMappingRaw.Text = ""
        End If
    End Sub
    Private Sub txtMappingRaw_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMappingRaw.TextChanged
        If lstMappedFields.SelectedIndex >= 0 Then
            DBRawFieldMapping(Me.lstMappedFields.SelectedIndex) = Me.txtMappingRaw.Text & ""
            Dim xSelect As Integer = Me.lstMappedFields.SelectedIndex
            UpdateMappFieldsList()
            Me.lstMappedFields.SelectedIndex = xSelect + 0
        End If
    End Sub
    Private Sub Button4_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim _r As DialogResult
        With SaveFileSettings
            _r = .ShowDialog()
            Select Case _r
                Case Windows.Forms.DialogResult.OK
                    clsFileXML1.Data_SourcePath = cmnDlg.FileName & ""
                    clsFileXML1.Data_Username = txtDataFields_1.Text & ""
                    clsFileXML1.Data_Password = txtDataFields_2.Text & ""
                    clsFileXML1.Data_DatabasePassword = txtDataFields_3.Text
                    clsFileXML1.Database_Connection = modSources.Database_Connection & ""
                    clsFileXML1.Output_Folder = txtOutputFolder.Text & ""
                    clsFileXML1.Output_Filename = txtOutputFilename.Text & ""
                    clsFileXML1.NameConventionsClear()
                    Try
                    Catch ex As Exception
                    End Try
                    clsFileXML1.SavePDFEmail(.FileName)
                    UpdateMessage("File saved success!")
                Case Else
                    UpdateMessage("Error - File not saved!")
                    Return
            End Select
        End With
    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim _r As DialogResult
        clsFileXML1 = New clsFileXML(Me)
        With OpenFileSettings
            _r = .ShowDialog()
            Select Case _r
                Case Windows.Forms.DialogResult.OK
                    Try
                        clsFileXML1.LoadSerialized(.FileName, Me)
                    Catch ex As Exception
                        UpdateMessage("Error loading settings file")
                        Err.Clear()
                        Return
                    End Try
                    Try
                        .FileName = clsFileXML1.Data_SourcePath & ""
                        txtDataFields_1.Text = clsFileXML1.Data_Username & ""
                        txtDataFields_2.Text = clsFileXML1.Data_Password & ""
                        txtDataFields_3.Text = clsFileXML1.Data_DatabasePassword & ""
                        modSources.Database_Connection = clsFileXML1.Database_Connection & ""
                    Catch ex As Exception
                        Err.Clear()
                    End Try
                    Try
                        UpdateMappFieldsList()
                        For Each fld As FilterField In modSources.FilterFields
                            lstFilter.Items.Add(fld.DBSql)
                        Next
                    Catch ex As Exception
                        Err.Clear()
                    End Try
                    Me.txtDataFields_0.Text = clsFileXML1.Data_SourcePath & ""
                    Try
                        Select Case New System.IO.FileInfo(.FileName).Extension
                            Case ".xml"
                                modSources.Database_Connection = .FileName
                                txtDataFields_0.Visible = True
                                txtDataFields_1.Visible = False
                                txtDataFields_2.Visible = False
                                txtDataFields_3.Visible = False
                                clsFileXML1.Data_SourcePath = .FileName & ""
                                clsFileXML1.Data_Username = txtDataFields_1.Text & ""
                                clsFileXML1.Data_Password = txtDataFields_2.Text & ""
                                clsFileXML1.Data_DatabasePassword = txtDataFields_3.Text
                                clsFileXML1.Database_Connection = .FileName
                                SelectSource(.FileName)
                            Case ".mdb"
                                If txtDataFields_3.Text = "Database Password" Or txtDataFields_3.Text = "" Then
                                    modADOConn.OLEDB_MSJet(.FileName, CStr(IIf(txtDataFields_1.Text = "Username", "", txtDataFields_1.Text)), CStr(IIf(txtDataFields_2.Text = "Password", "", txtDataFields_2.Text)))
                                Else
                                    modADOConn.OLEDB_MSJetDBPassword(.FileName, txtDataFields_3.Text, CStr(IIf(txtDataFields_1.Text = "Username", "", txtDataFields_1.Text)), CStr(IIf(txtDataFields_2.Text = "Password", "", txtDataFields_2.Text)))
                                End If
                                modSources.Database_Connection = modADOConn.Connection_String_VB
                                clsFileXML1.Data_SourcePath = .FileName & ""
                                clsFileXML1.Data_Username = txtDataFields_1.Text & ""
                                clsFileXML1.Data_Password = txtDataFields_2.Text & ""
                                clsFileXML1.Data_DatabasePassword = txtDataFields_3.Text
                                clsFileXML1.Database_Connection = modSources.Database_Connection & ""
                                txtDataFields_0.Visible = True
                                txtDataFields_1.Visible = True
                                txtDataFields_2.Visible = True
                                txtDataFields_3.Visible = True
                                SelectSource(.FileName)
                            Case ".xls"
                                modADOConn.OLEDB_MSJetExcell(.FileName, True)
                                modSources.Database_Connection = modADOConn.Connection_String_Net
                                clsFileXML1.Data_SourcePath = .FileName & ""
                                clsFileXML1.Data_Username = txtDataFields_1.Text & ""
                                clsFileXML1.Data_Password = txtDataFields_2.Text & ""
                                clsFileXML1.Data_DatabasePassword = txtDataFields_3.Text
                                clsFileXML1.Database_Connection = modSources.Database_Connection & ""
                                txtDataFields_0.Visible = True
                                txtDataFields_1.Visible = True
                                txtDataFields_2.Visible = True
                                txtDataFields_3.Visible = True
                                SelectSource(.FileName)
                        End Select
                    Catch ex As Exception
                        Throw ex
                        Err.Clear()
                    End Try
                    Try
                        lblStatus.Text = modSources.Database_Connection
                        If Test_Connection() Then
                            lblConnected.Text = "Test: Connected"
                            btnNext.Visible = True
                        Else
                            lblConnected.Text = "Test: Not Connected"
                            btnNext.Visible = False
                        End If
                    Catch ex As Exception
                        Err.Clear()
                    End Try
                    Try
                        txtOutputFolder.Text = clsFileXML1.Output_Folder & ""
                        txtOutputFilename.Text = clsFileXML1.Output_Filename & ""
                    Catch ex As Exception
                        Err.Clear()
                    End Try
                    Try
                        If Not String.IsNullOrEmpty(clsFileXML1.PDF_Source & "") Then
                            If Load_PDF(SessionBytes, pdfOwnerPassword) Then
                                lnkAddNew.Enabled = True
                                lnkRemove.Enabled = True
                                cmbPDFFields.Enabled = True
                                If cmbPDFFields.Items.Count <= 0 Then
                                    cmbPDFFields.Items.Clear()
                                    Populate_PDFFields_Combo(cmbPDFFields, lstMappedFields, Me)
                                End If
                            Else
                                lnkAddNew.Enabled = False
                                lnkRemove.Enabled = False
                                cmbPDFFields.Enabled = False
                                cmbPDFFields.Items.Clear()
                            End If
                        End If
                    Catch ex As Exception
                        Err.Clear()
                    End Try
                    Try
                        btnRecords_Affected_Click(sender, e)
                    Catch ex As Exception
                        Err.Clear()
                    End Try
                    Try
                        If PopulateTables() Then
                            cmbDBTables.Items.Clear()
                            cmbDBTablesFilter.Items.Clear()
                            Populate_TableCombo(cmbDBTables, Me)
                            Populate_TableCombo(cmbDBTablesFilter, Me)
                            If cmbDBTables.Items.Count = 1 Then cmbDBTables.SelectedIndex = 0
                            lnkLoadPDF_LinkClicked(Me, New System.Windows.Forms.LinkLabelLinkClickedEventArgs(lnkLoadPDF.Links(0)))
                        End If
                    Catch ex As Exception
                        Err.Clear()
                    End Try
                    Try
                        clsFileXML1.SetForm(Me)
                    Catch ex As Exception
                        Throw ex
                        Err.Clear()
                    End Try
                    UpdateMessage("Settings file loaded successfully!")
                Case Else
                    UpdateMessage("Error - File not loaded.")
                    Return
            End Select
        End With
    End Sub
    Public Sub UpdateMessage(ByVal strMessage As String, Optional ByVal prefix As String = "Status: ")
        lblMessage.Text = prefix & strMessage
        Try
            DirectCast(Me.Owner, frmMain).TimeStampAdd(prefix & strMessage)
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub SMTP_NoAuthentication_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SMTP_NoAuthentication.CheckedChanged
        Update_SMTPOptions()
    End Sub
    Private Sub SMTP_BasicAuthentication_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SMTP_BasicAuthentication.CheckedChanged
        Update_SMTPOptions()
    End Sub
    Private Sub SMTP_CredentialAuthentication_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SMTP_CredentialAuthentication.CheckedChanged
        Update_SMTPOptions()
    End Sub
    Private Sub Update_SMTPOptions()
        Me.Label25.Enabled = Me.SMTP_CredentialAuthentication.Checked
        Me.Label26.Enabled = Me.SMTP_CredentialAuthentication.Checked
        Me.Label24.Enabled = Me.SMTP_CredentialAuthentication.Checked
        Me.SMTP_CredUsername.Enabled = Me.SMTP_CredentialAuthentication.Checked
        Me.SMTP_CredPassword.Enabled = Me.SMTP_CredentialAuthentication.Checked
        Me.SMTP_CredDomain.Enabled = Me.SMTP_CredentialAuthentication.Checked
        Me.Label27.Enabled = Not Me.SMTP_NoAuthentication.Checked
        Me.SMTP_AuthUsername.Enabled = Not Me.SMTP_NoAuthentication.Checked
        Me.SMTP_AuthPassword.Enabled = Not Me.SMTP_NoAuthentication.Checked
        Me.Label28.Enabled = Not Me.SMTP_NoAuthentication.Checked
    End Sub
    Private Sub SMTP_DeliveryNetwork_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SMTP_DeliveryNetwork.CheckedChanged
        UPDATE_SMTPDELIVERYMETHOD()
    End Sub
    Private Sub SMTP_DeliveryIIS_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SMTP_DeliveryIIS.CheckedChanged
        UPDATE_SMTPDELIVERYMETHOD()
    End Sub
    Private Sub SMTP_DeliveryDirectory_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SMTP_DeliveryDirectory.CheckedChanged
        UPDATE_SMTPDELIVERYMETHOD()
    End Sub
    Private Sub UPDATE_SMTPDELIVERYMETHOD()
        Me.SMTP_DeliveryDirectoryLocation.Enabled = Me.SMTP_DeliveryDirectory.Checked
    End Sub
    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Dim strNames As String = ""
        Dim button10Text As String = "EMAIL MERGED PDFS"
        ProgressBar1.Value = 0
        ProgressBar1.Maximum = 100
        Dim frmMain1 As frmMain = DirectCast(Me.Owner, frmMain)
        frmMain1.Session("saved-output-merged") = frmMain1.Session
        Try
            blnCancelProcess = False
            If Button10.Text = "CANCEL PROCESS" Then
                blnCancelProcess = True
                Button10.Text = button10Text
                Exit Sub
            Else
                Button10.Text = "CANCEL PROCESS"
                Application.DoEvents()
            End If
            Dim dsx As New DataSet
            If Database_Connection.ToString.ToLower.EndsWith(".xml") Then
                dsx = MergeXML()
            ElseIf Database_Connection.ToString.ToLower.EndsWith(".json") Then
                dsx = MergeJSON()
            Else
                dsx = Merge()
            End If
            Dim drCntr As Integer = 0
            Dim tmStart As DateTime = DateTime.Now
            Dim clsInput As New clsPromptDialog
            Dim htmlSubmitAction As String = ""
            If Me.chkBodyIsHtml.Checked Then
                htmlSubmitAction = clsInput.ShowDialog("HTML submit action URL?", "HTML Submit Action URL?", Me, "", "OK")
            End If
            For Each dr As DataRow In dsx.Tables(0).Rows
                frmMain1.Session = frmMain1.Session("saved-output-merged")
                tmStart = DateTime.Now
                drCntr += 1
                ProgressBar1.Value = CInt((drCntr / dsx.Tables(0).Rows.Count) * 100)
                cfdf = cpdf.PDFOpenFromBuf(frmMain1.Session("saved-output-merged"), True, True, frmMain1.pdfOwnerPassword)
                Select Case cfdf.Determine_Type(frmMain1.Session("saved-output-merged"))
                    Case FDFApp.FDFDoc_Class.FDFType.PDF
                        cfdf.XDPSetValuesFromDataRow(dr)
                        frmMain1.Session = cfdf.PDFMergeFDF2Buf(frmMain1.Session("saved-output-merged"), False, frmMain1.pdfOwnerPassword)
                    Case FDFApp.FDFDoc_Class.FDFType.XPDF
                        cfdf.XDPSetValuesFromDataRow(dr)
                        frmMain1.Session = cfdf.PDFMergeXDP2Buf(frmMain1.Session("saved-output-merged"), False, frmMain1.pdfOwnerPassword)
                    Case FDFApp.FDFDoc_Class.FDFType.XFA
                        cfdf.XDPSetValuesFromDataRow(dr)
                        frmMain1.Session = cfdf.PDFMergeXDP2Buf(frmMain1.Session("saved-output-merged"), False, frmMain1.pdfOwnerPassword)
                End Select
                Try
                    Dim m As New System.Net.Mail.MailMessage
                    If Not String.IsNullOrEmpty(Me.MSG_FROM_EMAIL.Text.Trim()) Then
                        If Not String.IsNullOrEmpty(Me.MSG_FROM_NAME.Text) Then
                            m.ReplyToList.Add(New System.Net.Mail.MailAddress(MSG_FROM_EMAIL.Text))
                        Else
                            m.ReplyToList.Add(New System.Net.Mail.MailAddress(MSG_FROM_EMAIL.Text, MSG_FROM_NAME.Text))
                        End If
                    End If
                    If Not String.IsNullOrEmpty(Me.SMTP_CredUsername.Text.Trim()) Then
                        If CStr(Me.SMTP_CredUsername.Text).Contains("@") Then
                            If CStr(Me.SMTP_CredUsername.Text).Split("@")(1).Contains(".") Then
                                m.From = New System.Net.Mail.MailAddress(Me.SMTP_CredUsername.Text)
                            Else
                                m.From = New System.Net.Mail.MailAddress(MSG_FROM_EMAIL.Text, MSG_FROM_NAME.Text)
                            End If
                        Else
                            m.From = New System.Net.Mail.MailAddress(MSG_FROM_EMAIL.Text, MSG_FROM_NAME.Text)
                        End If
                    Else
                        m.From = New System.Net.Mail.MailAddress(MSG_FROM_EMAIL.Text, MSG_FROM_NAME.Text)
                    End If
                    If Not String.IsNullOrEmpty(Me.MSG_TO.Text.Trim()) Then
                        For Each r As String In Me.MSG_TO.Text.Replace(",", ";").Split(";")
                            If Not String.IsNullOrEmpty(r.Trim()) Then
                                m.To.Add(InjectFieldNameValues(r.Trim(), dr))
                            End If
                        Next
                    End If
                    If Not String.IsNullOrEmpty(Me.MSG_CC.Text.Trim()) Then
                        For Each r As String In Me.MSG_CC.Text.Replace(",", ";").Split(";")
                            If Not String.IsNullOrEmpty(r.Trim()) Then
                                m.CC.Add(InjectFieldNameValues(r.Trim(), dr))
                            End If
                        Next
                    End If
                    If Not String.IsNullOrEmpty(Me.MSG_BCC.Text.Trim()) Then
                        For Each r As String In Me.MSG_BCC.Text.Replace(",", ";").Split(";")
                            If Not String.IsNullOrEmpty(r.Trim()) Then
                                m.Bcc.Add(InjectFieldNameValues(r.Trim(), dr))
                            End If
                        Next
                    End If
                    m.Subject = InjectFieldNameValues(MSG_SUBJECT.Text, dr)
                    m.Body = InjectFieldNameValues(MSG_BODY.Text, dr)
                    m.IsBodyHtml = chkBodyIsHtml.Checked
                    If m.IsBodyHtml Then
                        If Not frmMain1 Is Nothing Then
                            Dim tmpProgressbarVal As Integer = ProgressBar1.Value
                            For p As Integer = 1 To frmMain1.pdfReaderDoc.NumberOfPages
                                ProgressBar1.Value = CInt((p / frmMain1.pdfReaderDoc.NumberOfPages) * 100)
                                If Me.MSG_BODY.Text.ToString.ToLower.Contains(CStr("name=""page_" & p & """ id=""page_" & p & """").ToLower()) Then
                                    Dim contentID As String = "pageImage_" & p.ToString()
                                    If m.Body.ToString.Contains("{" & contentID & "}") Then
                                        Dim att As New Net.Mail.Attachment(New System.IO.MemoryStream(frmMain1.A0_LoadImage(frmMain1.Session, p, frmMain.pdfReaderDoc.GetPageSizeWithRotation(p).Width * frmMain.getPercent(), frmMain.pdfReaderDoc.GetPageSizeWithRotation(p).Height * frmMain.getPercent(), False)), contentID, "image/png")
                                        att.ContentId = contentID
                                        m.Attachments.Add(att)
                                        m.Body = m.Body.ToString.Replace("{" & contentID & "}", "cid:" & contentID)
                                    End If
                                Else
                                    Dim contentID As String = "pageImage_" & p.ToString()
                                    If m.Body.ToString.Contains("{" & contentID & "}") Then
                                        Dim att As New Net.Mail.Attachment(New System.IO.MemoryStream(frmMain1.A0_LoadImage(frmMain1.Session, p, frmMain.pdfReaderDoc.GetPageSizeWithRotation(p).Width * frmMain.getPercent(), frmMain.pdfReaderDoc.GetPageSizeWithRotation(p).Height * frmMain.getPercent(), False)), contentID, "image/png")
                                        att.ContentId = contentID
                                        m.Attachments.Add(att)
                                        m.Body = m.Body.ToString.Replace("{" & contentID & "}", "cid:" & contentID)
                                    End If
                                End If
                            Next
                            ProgressBar1.Value = tmpProgressbarVal
                        End If
                    End If
                    Dim cSMTP As New System.Net.Mail.SmtpClient(SMTP_Hostname.Text, CInt(SMTP_Port.Text) + 0)
                    cSMTP.Timeout = 1000000
                    cSMTP.EnableSsl = SMTP_SSL.Checked
                    If SMTP_CredentialAuthentication.Checked Then
                        cSMTP.UseDefaultCredentials = False
                        cSMTP.Credentials = New System.Net.NetworkCredential(SMTP_CredUsername.Text, SMTP_CredPassword.Text, SMTP_CredDomain.Text)
                    ElseIf SMTP_BasicAuthentication.Checked Then
                        cSMTP.UseDefaultCredentials = True
                    Else
                        cSMTP.UseDefaultCredentials = True
                    End If
                    Dim attFormat As String = "pdf"
                    Dim attBytes() As Byte = Nothing
                    If MSG_ATTACHMENT_PDF.Checked Then
                        attFormat = "pdf"
                        attBytes = frmMain1.Session()
                        Dim att As New Net.Mail.Attachment(New System.IO.MemoryStream(attBytes), InjectFieldNameValues(MSG_ATTACHMENT_FILENAME.Text.ToString(), dr), cpdf.MimePDF)
                        m.Attachments.Add(att)
                    ElseIf MSG_ATTACHMENT_FDF.Checked Then
                        attFormat = "fdf"
                        attBytes = cfdf.FDFSavetoBuf(FDFApp.FDFDoc_Class.FDFType.FDF, True)
                        Dim att As New Net.Mail.Attachment(New System.IO.MemoryStream(attBytes), InjectFieldNameValues(MSG_ATTACHMENT_FILENAME.Text.ToString(), dr), cpdf.MimeFDF)
                        m.Attachments.Add(att)
                    ElseIf MSG_ATTACHMENT_XFDF.Checked Then
                        attFormat = "xfdf"
                        attBytes = cfdf.FDFSavetoBuf(FDFApp.FDFDoc_Class.FDFType.xFDF, True)
                        Dim att As New Net.Mail.Attachment(New System.IO.MemoryStream(attBytes), InjectFieldNameValues(MSG_ATTACHMENT_FILENAME.Text.ToString(), dr), cpdf.MimeXFDF)
                        m.Attachments.Add(att)
                    ElseIf MSG_ATTACHMENT_XDP.Checked Then
                        attFormat = "xdp"
                        attBytes = cfdf.FDFSavetoBuf(FDFApp.FDFDoc_Class.FDFType.XDP, True)
                        Dim att As New Net.Mail.Attachment(New System.IO.MemoryStream(attBytes), InjectFieldNameValues(MSG_ATTACHMENT_FILENAME.Text.ToString(), dr), cpdf.MimeXDP)
                        m.Attachments.Add(att)
                    ElseIf MSG_ATTACHMENT_XML.Checked Then
                        attFormat = "xml"
                        attBytes = cfdf.FDFSavetoBuf(FDFApp.FDFDoc_Class.FDFType.XML, True)
                        Dim att As New Net.Mail.Attachment(New System.IO.MemoryStream(attBytes), InjectFieldNameValues(MSG_ATTACHMENT_FILENAME.Text.ToString(), dr), cpdf.MimeXML)
                        m.Attachments.Add(att)
                    ElseIf MSG_ATTACHMENT_JSON.Checked Then
                        attFormat = "json"
                        attBytes = cfdf.FDFSavetoBuf(FDFApp.FDFDoc_Class.FDFType.Json, True)
                        Dim att As New Net.Mail.Attachment(New System.IO.MemoryStream(attBytes), InjectFieldNameValues(MSG_ATTACHMENT_FILENAME.Text.ToString(), dr), cpdf.MimeJSON)
                        m.Attachments.Add(att)
                    ElseIf MSG_ATTACHMENT_HTML.Checked Then
                        attFormat = "html"
                        attBytes = System.Text.Encoding.UTF8.GetBytes(frmMain1.createHTMLFile("", True, True, InjectFieldNameValues(htmlSubmitAction, dr), Me, "", InjectFieldNameValues(htmlSubmitAction, dr), True))
                        Dim att As New Net.Mail.Attachment(New System.IO.MemoryStream(attBytes), InjectFieldNameValues(System.IO.Path.GetFileNameWithoutExtension(MSG_ATTACHMENT_FILENAME.Text.ToString()).ToString & ".html", dr), cpdf.MimeHTML)
                        m.Attachments.Add(att)
                    ElseIf MSG_ATTACHMENT_NONE.Checked Then
                        attBytes = Nothing
                        attFormat = ""
                    End If
                    Try
                        cSMTP.Send(m)
                        UpdateMessage("Message sent (" & m.To.ToString() & ") - " & DateTime.Now.ToLocalTime.ToString)
                    Catch ex As Exception
                        Throw ex
                    End Try
                    If tmStart.AddMilliseconds(1000) > DateTime.Now Then
                        If frmMain1.DoEvents_Wait(1000) Then
                            Exit Try
                        End If
                    End If
                Catch ex2 As Exception
                    UpdateMessage("Error - Message not sent - " & DateTime.Now.ToLocalTime.ToString & Environment.NewLine & "Error: " & ex2.Message.ToString & "")
                    Err.Clear()
                End Try
            Next
        Catch ex As Exception
            Err.Clear()
        Finally
            frmMain1.Session = frmMain1.Session("saved-output-merged")
            Button10.Text = button10Text
            ProgressBar1.Value = 100
            UpdateMessage("Sending Messages complete - " & DateTime.Now.ToLocalTime.ToString)
        End Try
    End Sub
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Try
            Dim smtp As New System.Net.Mail.SmtpClient(Me.SMTP_Hostname.Text & "", CInt(Me.SMTP_Port.Text))
            Dim msg As New System.Net.Mail.MailMessage(Me.MSG_FROM_EMAIL.Text, Me.MSG_TO.Text, Me.MSG_SUBJECT.Text, Me.MSG_BODY.Text)
            msg.Bcc.Add(New System.Net.Mail.MailAddress("", ""))
            Try
                If SMTP_NoAuthentication.Checked Then
                ElseIf SMTP_BasicAuthentication.Checked Then
                ElseIf SMTP_CredentialAuthentication.Checked Then
                    smtp.UseDefaultCredentials = False
                    Dim cred As New System.Net.NetworkCredential(Me.SMTP_CredUsername.Text, Me.SMTP_CredPassword.Text, Me.SMTP_CredDomain.Text)
                    smtp.Credentials = cred
                End If
                If SMTP_SSL.Checked Then
                    smtp.EnableSsl = True
                End If
                If SMTP_DeliveryNetwork.Checked Then
                    smtp.DeliveryMethod = 0
                ElseIf SMTP_DeliveryDirectory.Checked Then
                    smtp.DeliveryMethod = DirectCast(1, System.Net.Mail.SmtpDeliveryMethod)
                    smtp.PickupDirectoryLocation = SMTP_DeliveryDirectoryLocation.Text & ""
                ElseIf SMTP_DeliveryIIS.Checked Then
                    smtp.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis
                End If
                smtp.Timeout = 20000
                smtp.Send(msg)
                UpdateMessage("test message sent successfully!")
            Catch ex As Exception
                UpdateMessage("Error - Test message not sent. " & DateTime.Now.ToLocalTime.ToString & Environment.NewLine & "Error: " & ex.Message.ToString & "")
                Err.Clear()
            End Try
        Catch ex2 As Exception
            UpdateMessage("Error - Test message not sent. " & DateTime.Now.ToLocalTime.ToString & Environment.NewLine & "Error: " & ex2.Message.ToString & "")
            Err.Clear()
        End Try
    End Sub
    Private Sub TabPage8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage8.Click
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub TabPage9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage9.Click
    End Sub
    Private Sub OUTPUT_FORMAT_XML_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OUTPUT_FORMAT_XML.CheckedChanged
    End Sub
    Private Sub OUTPUT_FORMAT_XDP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OUTPUT_FORMAT_XDP.CheckedChanged
    End Sub
    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub MSG_BODY_TextChanged(sender As Object, e As EventArgs) Handles MSG_BODY.TextChanged
    End Sub
    Private Sub MSG_ATTACHMENT_XML_CheckedChanged(sender As Object, e As EventArgs) Handles MSG_ATTACHMENT_XML.CheckedChanged
    End Sub
    Private Sub chkOutpdfPDFFlattened_CheckedChanged(sender As Object, e As EventArgs) Handles chkOutpdfPDFFlattened.CheckedChanged
    End Sub
End Class
