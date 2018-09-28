Imports System.IO
Imports System.Reflection
Imports System.Xml.Serialization
Imports System.Xml
Imports System.Security.Cryptography
Public Class frmEmailScriptsClient
    Inherits System.Windows.Forms.Form
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 Nicholas Kowalewicz All Rights reserved.
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
    Public submitFormAction As Boolean = False
    Public submitFormUrl As String = ""
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
    Friend WithEvents SaveScript As System.Windows.Forms.SaveFileDialog
    Friend WithEvents mnuMain As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents txtMessage_Subject As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtMessage_Body As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtTo_Email As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtBCC_Email As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtCC_Email As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnCopy As System.Windows.Forms.Button
    Friend WithEvents FileSystemWatcher1 As System.IO.FileSystemWatcher
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtSent_Message As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtError_Message As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtTarget As System.Windows.Forms.TextBox
    Friend WithEvents txtError_URL As System.Windows.Forms.TextBox
    Friend WithEvents txtSent_URL As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Public WithEvents txtScript As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents mailFormRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents mailDocRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents submitFormRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents mailMsgRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButton5 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton4 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton3 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton6 As System.Windows.Forms.RadioButton
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnCreateScript20 As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEmailScriptsClient))
        Me.lblStatus = New System.Windows.Forms.Label
        Me.SaveScript = New System.Windows.Forms.SaveFileDialog
        Me.mnuMain = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.MenuItem3 = New System.Windows.Forms.MenuItem
        Me.btnCancel = New System.Windows.Forms.Button
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.txtBCC_Email = New System.Windows.Forms.TextBox
        Me.txtCC_Email = New System.Windows.Forms.TextBox
        Me.txtTo_Email = New System.Windows.Forms.TextBox
        Me.txtMessage_Subject = New System.Windows.Forms.TextBox
        Me.txtMessage_Body = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.TabPage5 = New System.Windows.Forms.TabPage
        Me.Label21 = New System.Windows.Forms.Label
        Me.txtSent_Message = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.txtError_Message = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtTarget = New System.Windows.Forms.TextBox
        Me.txtError_URL = New System.Windows.Forms.TextBox
        Me.txtSent_URL = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.Button1 = New System.Windows.Forms.Button
        Me.btnCopy = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.RadioButton6 = New System.Windows.Forms.RadioButton
        Me.RadioButton5 = New System.Windows.Forms.RadioButton
        Me.RadioButton4 = New System.Windows.Forms.RadioButton
        Me.RadioButton3 = New System.Windows.Forms.RadioButton
        Me.RadioButton2 = New System.Windows.Forms.RadioButton
        Me.RadioButton1 = New System.Windows.Forms.RadioButton
        Me.mailMsgRadioButton = New System.Windows.Forms.RadioButton
        Me.mailFormRadioButton = New System.Windows.Forms.RadioButton
        Me.mailDocRadioButton = New System.Windows.Forms.RadioButton
        Me.submitFormRadioButton = New System.Windows.Forms.RadioButton
        Me.txtScript = New System.Windows.Forms.TextBox
        Me.btnCreateScript20 = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.FileSystemWatcher1 = New System.IO.FileSystemWatcher
        Me.TabControl1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblStatus
        '
        Me.lblStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.ForeColor = System.Drawing.Color.DarkBlue
        Me.lblStatus.Location = New System.Drawing.Point(24, 515)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(552, 69)
        Me.lblStatus.TabIndex = 16
        Me.lblStatus.Text = "Status: Ready"
        '
        'mnuMain
        '
        Me.mnuMain.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem3})
        Me.MenuItem1.Text = "File"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 0
        Me.MenuItem3.Text = "Exit"
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnCancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCancel.Location = New System.Drawing.Point(532, 6)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(48, 32)
        Me.btnCancel.TabIndex = 6
        Me.btnCancel.Text = "Close"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Location = New System.Drawing.Point(16, 32)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(570, 480)
        Me.TabControl1.TabIndex = 7
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.txtBCC_Email)
        Me.TabPage2.Controls.Add(Me.txtCC_Email)
        Me.TabPage2.Controls.Add(Me.txtTo_Email)
        Me.TabPage2.Controls.Add(Me.txtMessage_Subject)
        Me.TabPage2.Controls.Add(Me.txtMessage_Body)
        Me.TabPage2.Controls.Add(Me.Label7)
        Me.TabPage2.Controls.Add(Me.Label17)
        Me.TabPage2.Controls.Add(Me.Label3)
        Me.TabPage2.Controls.Add(Me.Label8)
        Me.TabPage2.Controls.Add(Me.Label16)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(562, 454)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Email Message"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'txtBCC_Email
        '
        Me.txtBCC_Email.BackColor = System.Drawing.Color.White
        Me.txtBCC_Email.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBCC_Email.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtBCC_Email.HideSelection = False
        Me.txtBCC_Email.Location = New System.Drawing.Point(8, 112)
        Me.txtBCC_Email.Name = "txtBCC_Email"
        Me.txtBCC_Email.Size = New System.Drawing.Size(544, 24)
        Me.txtBCC_Email.TabIndex = 59
        Me.txtBCC_Email.WordWrap = False
        '
        'txtCC_Email
        '
        Me.txtCC_Email.BackColor = System.Drawing.Color.White
        Me.txtCC_Email.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCC_Email.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCC_Email.HideSelection = False
        Me.txtCC_Email.Location = New System.Drawing.Point(8, 64)
        Me.txtCC_Email.Name = "txtCC_Email"
        Me.txtCC_Email.Size = New System.Drawing.Size(544, 24)
        Me.txtCC_Email.TabIndex = 58
        Me.txtCC_Email.WordWrap = False
        '
        'txtTo_Email
        '
        Me.txtTo_Email.BackColor = System.Drawing.Color.White
        Me.txtTo_Email.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTo_Email.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTo_Email.HideSelection = False
        Me.txtTo_Email.Location = New System.Drawing.Point(8, 16)
        Me.txtTo_Email.Name = "txtTo_Email"
        Me.txtTo_Email.Size = New System.Drawing.Size(544, 24)
        Me.txtTo_Email.TabIndex = 57
        Me.txtTo_Email.WordWrap = False
        '
        'txtMessage_Subject
        '
        Me.txtMessage_Subject.BackColor = System.Drawing.Color.White
        Me.txtMessage_Subject.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMessage_Subject.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMessage_Subject.Location = New System.Drawing.Point(8, 168)
        Me.txtMessage_Subject.Name = "txtMessage_Subject"
        Me.txtMessage_Subject.Size = New System.Drawing.Size(544, 24)
        Me.txtMessage_Subject.TabIndex = 60
        '
        'txtMessage_Body
        '
        Me.txtMessage_Body.AcceptsReturn = True
        Me.txtMessage_Body.BackColor = System.Drawing.Color.White
        Me.txtMessage_Body.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMessage_Body.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMessage_Body.HideSelection = False
        Me.txtMessage_Body.Location = New System.Drawing.Point(8, 232)
        Me.txtMessage_Body.Multiline = True
        Me.txtMessage_Body.Name = "txtMessage_Body"
        Me.txtMessage_Body.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtMessage_Body.Size = New System.Drawing.Size(544, 200)
        Me.txtMessage_Body.TabIndex = 61
        Me.txtMessage_Body.WordWrap = False
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(8, 208)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(192, 31)
        Me.Label7.TabIndex = 51
        Me.Label7.Text = "Message Body (required)"
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(0, 96)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(272, 24)
        Me.Label17.TabIndex = 58
        Me.Label17.Text = "Recipient(s) BCC: Semi-colon Separated"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(0, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(272, 23)
        Me.Label3.TabIndex = 56
        Me.Label3.Text = "Recipient(s) CC: Semi-colon Separated"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(8, 144)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(256, 25)
        Me.Label8.TabIndex = 52
        Me.Label8.Text = "Message Subject (required)"
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(0, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(272, 23)
        Me.Label16.TabIndex = 54
        Me.Label16.Text = "Recipient(s) TO: Semi-colon Separated"
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.Label21)
        Me.TabPage5.Controls.Add(Me.txtSent_Message)
        Me.TabPage5.Controls.Add(Me.Label15)
        Me.TabPage5.Controls.Add(Me.txtError_Message)
        Me.TabPage5.Controls.Add(Me.Label14)
        Me.TabPage5.Controls.Add(Me.txtTarget)
        Me.TabPage5.Controls.Add(Me.txtError_URL)
        Me.TabPage5.Controls.Add(Me.txtSent_URL)
        Me.TabPage5.Controls.Add(Me.Label13)
        Me.TabPage5.Controls.Add(Me.Label1)
        Me.TabPage5.Controls.Add(Me.Label20)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Size = New System.Drawing.Size(562, 454)
        Me.TabPage5.TabIndex = 7
        Me.TabPage5.Text = "User Response"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Black
        Me.Label21.Location = New System.Drawing.Point(54, 148)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(466, 23)
        Me.Label21.TabIndex = 80
        Me.Label21.Text = "OR RESPOND WITH STATUS MESSAGE"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSent_Message
        '
        Me.txtSent_Message.BackColor = System.Drawing.Color.White
        Me.txtSent_Message.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSent_Message.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSent_Message.Location = New System.Drawing.Point(59, 211)
        Me.txtSent_Message.Multiline = True
        Me.txtSent_Message.Name = "txtSent_Message"
        Me.txtSent_Message.Size = New System.Drawing.Size(436, 50)
        Me.txtSent_Message.TabIndex = 77
        '
        'Label15
        '
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(56, 193)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(254, 23)
        Me.Label15.TabIndex = 73
        Me.Label15.Text = "Message Sent - Response Status Popup Message"
        '
        'txtError_Message
        '
        Me.txtError_Message.BackColor = System.Drawing.Color.White
        Me.txtError_Message.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtError_Message.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtError_Message.Location = New System.Drawing.Point(59, 281)
        Me.txtError_Message.Multiline = True
        Me.txtError_Message.Name = "txtError_Message"
        Me.txtError_Message.Size = New System.Drawing.Size(436, 48)
        Me.txtError_Message.TabIndex = 78
        '
        'Label14
        '
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(56, 264)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(254, 23)
        Me.Label14.TabIndex = 72
        Me.Label14.Text = "Message Error - Response Status Popup Message"
        '
        'txtTarget
        '
        Me.txtTarget.BackColor = System.Drawing.Color.White
        Me.txtTarget.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTarget.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTarget.Location = New System.Drawing.Point(59, 323)
        Me.txtTarget.Name = "txtTarget"
        Me.txtTarget.Size = New System.Drawing.Size(264, 21)
        Me.txtTarget.TabIndex = 79
        Me.txtTarget.Visible = False
        '
        'txtError_URL
        '
        Me.txtError_URL.BackColor = System.Drawing.Color.White
        Me.txtError_URL.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtError_URL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtError_URL.Location = New System.Drawing.Point(59, 107)
        Me.txtError_URL.Name = "txtError_URL"
        Me.txtError_URL.Size = New System.Drawing.Size(436, 21)
        Me.txtError_URL.TabIndex = 76
        '
        'txtSent_URL
        '
        Me.txtSent_URL.BackColor = System.Drawing.Color.White
        Me.txtSent_URL.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSent_URL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSent_URL.Location = New System.Drawing.Point(59, 57)
        Me.txtSent_URL.Name = "txtSent_URL"
        Me.txtSent_URL.Size = New System.Drawing.Size(436, 21)
        Me.txtSent_URL.TabIndex = 75
        '
        'Label13
        '
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(56, 90)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(437, 23)
        Me.Label13.TabIndex = 71
        Me.Label13.Text = "Message Error Redirect URL (Overrides Response Status Message)"
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(56, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(439, 23)
        Me.Label1.TabIndex = 70
        Me.Label1.Text = "Message Sent Redirect URL (Overrides Response Status Message)"
        '
        'Label20
        '
        Me.Label20.ForeColor = System.Drawing.Color.Black
        Me.Label20.Location = New System.Drawing.Point(56, 306)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(278, 23)
        Me.Label20.TabIndex = 74
        Me.Label20.Text = "PDF Open Target Frame [_self, _blank, _top, ...]"
        Me.Label20.Visible = False
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.Button1)
        Me.TabPage4.Controls.Add(Me.btnCopy)
        Me.TabPage4.Controls.Add(Me.GroupBox1)
        Me.TabPage4.Controls.Add(Me.txtScript)
        Me.TabPage4.Controls.Add(Me.btnCreateScript20)
        Me.TabPage4.Controls.Add(Me.Label2)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(562, 454)
        Me.TabPage4.TabIndex = 6
        Me.TabPage4.Text = "Generate Scripts"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Button1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Button1.Location = New System.Drawing.Point(416, 136)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(136, 56)
        Me.Button1.TabIndex = 78
        Me.Button1.Text = "Copy to Field Action"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'btnCopy
        '
        Me.btnCopy.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnCopy.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnCopy.Location = New System.Drawing.Point(312, 136)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(104, 56)
        Me.btnCopy.TabIndex = 4
        Me.btnCopy.Text = "Copy to Clipboard"
        Me.btnCopy.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.mailMsgRadioButton)
        Me.GroupBox1.Controls.Add(Me.mailFormRadioButton)
        Me.GroupBox1.Controls.Add(Me.mailDocRadioButton)
        Me.GroupBox1.Controls.Add(Me.submitFormRadioButton)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(544, 128)
        Me.GroupBox1.TabIndex = 77
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "JavaScript Methods:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Blue
        Me.Label4.Location = New System.Drawing.Point(16, 104)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(459, 15)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Information: submitForm() method submits the whole PDF format in Adobe Reader."
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.RadioButton6)
        Me.GroupBox2.Controls.Add(Me.RadioButton5)
        Me.GroupBox2.Controls.Add(Me.RadioButton4)
        Me.GroupBox2.Controls.Add(Me.RadioButton3)
        Me.GroupBox2.Controls.Add(Me.RadioButton2)
        Me.GroupBox2.Controls.Add(Me.RadioButton1)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 40)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(525, 56)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Submit Format As:"
        '
        'RadioButton6
        '
        Me.RadioButton6.AutoSize = True
        Me.RadioButton6.Location = New System.Drawing.Point(304, 24)
        Me.RadioButton6.Name = "RadioButton6"
        Me.RadioButton6.Size = New System.Drawing.Size(55, 17)
        Me.RadioButton6.TabIndex = 5
        Me.RadioButton6.Text = "HTML"
        Me.RadioButton6.UseVisualStyleBackColor = True
        '
        'RadioButton5
        '
        Me.RadioButton5.AutoSize = True
        Me.RadioButton5.Location = New System.Drawing.Point(248, 24)
        Me.RadioButton5.Name = "RadioButton5"
        Me.RadioButton5.Size = New System.Drawing.Size(47, 17)
        Me.RadioButton5.TabIndex = 4
        Me.RadioButton5.Text = "XDP"
        Me.RadioButton5.UseVisualStyleBackColor = True
        '
        'RadioButton4
        '
        Me.RadioButton4.AutoSize = True
        Me.RadioButton4.Location = New System.Drawing.Point(192, 24)
        Me.RadioButton4.Name = "RadioButton4"
        Me.RadioButton4.Size = New System.Drawing.Size(47, 17)
        Me.RadioButton4.TabIndex = 3
        Me.RadioButton4.Text = "XML"
        Me.RadioButton4.UseVisualStyleBackColor = True
        '
        'RadioButton3
        '
        Me.RadioButton3.AutoSize = True
        Me.RadioButton3.Location = New System.Drawing.Point(128, 24)
        Me.RadioButton3.Name = "RadioButton3"
        Me.RadioButton3.Size = New System.Drawing.Size(50, 17)
        Me.RadioButton3.TabIndex = 2
        Me.RadioButton3.Text = "xFDF"
        Me.RadioButton3.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(72, 24)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(45, 17)
        Me.RadioButton2.TabIndex = 1
        Me.RadioButton2.Text = "FDF"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Checked = True
        Me.RadioButton1.Location = New System.Drawing.Point(16, 24)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(46, 17)
        Me.RadioButton1.TabIndex = 0
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "PDF"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'mailMsgRadioButton
        '
        Me.mailMsgRadioButton.AutoSize = True
        Me.mailMsgRadioButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mailMsgRadioButton.Location = New System.Drawing.Point(416, 16)
        Me.mailMsgRadioButton.Name = "mailMsgRadioButton"
        Me.mailMsgRadioButton.Size = New System.Drawing.Size(127, 24)
        Me.mailMsgRadioButton.TabIndex = 4
        Me.mailMsgRadioButton.Text = "mailMsg(FDF)"
        Me.mailMsgRadioButton.UseVisualStyleBackColor = True
        '
        'mailFormRadioButton
        '
        Me.mailFormRadioButton.AutoSize = True
        Me.mailFormRadioButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mailFormRadioButton.Location = New System.Drawing.Point(280, 16)
        Me.mailFormRadioButton.Name = "mailFormRadioButton"
        Me.mailFormRadioButton.Size = New System.Drawing.Size(134, 24)
        Me.mailFormRadioButton.TabIndex = 2
        Me.mailFormRadioButton.Text = "mailForm(FDF)"
        Me.mailFormRadioButton.UseVisualStyleBackColor = True
        '
        'mailDocRadioButton
        '
        Me.mailDocRadioButton.AutoSize = True
        Me.mailDocRadioButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mailDocRadioButton.Location = New System.Drawing.Point(152, 16)
        Me.mailDocRadioButton.Name = "mailDocRadioButton"
        Me.mailDocRadioButton.Size = New System.Drawing.Size(126, 24)
        Me.mailDocRadioButton.TabIndex = 1
        Me.mailDocRadioButton.Text = "mailDoc(PDF)"
        Me.mailDocRadioButton.UseVisualStyleBackColor = True
        '
        'submitFormRadioButton
        '
        Me.submitFormRadioButton.AutoSize = True
        Me.submitFormRadioButton.Checked = True
        Me.submitFormRadioButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.submitFormRadioButton.Location = New System.Drawing.Point(24, 16)
        Me.submitFormRadioButton.Name = "submitFormRadioButton"
        Me.submitFormRadioButton.Size = New System.Drawing.Size(127, 24)
        Me.submitFormRadioButton.TabIndex = 0
        Me.submitFormRadioButton.TabStop = True
        Me.submitFormRadioButton.Text = "submitForm(*)"
        Me.submitFormRadioButton.UseVisualStyleBackColor = True
        '
        'txtScript
        '
        Me.txtScript.AcceptsReturn = True
        Me.txtScript.BackColor = System.Drawing.Color.White
        Me.txtScript.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtScript.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtScript.HideSelection = False
        Me.txtScript.Location = New System.Drawing.Point(16, 216)
        Me.txtScript.Multiline = True
        Me.txtScript.Name = "txtScript"
        Me.txtScript.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtScript.Size = New System.Drawing.Size(528, 232)
        Me.txtScript.TabIndex = 76
        Me.txtScript.WordWrap = False
        '
        'btnCreateScript20
        '
        Me.btnCreateScript20.BackColor = System.Drawing.Color.Gray
        Me.btnCreateScript20.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCreateScript20.ForeColor = System.Drawing.Color.Yellow
        Me.btnCreateScript20.Location = New System.Drawing.Point(8, 136)
        Me.btnCreateScript20.Name = "btnCreateScript20"
        Me.btnCreateScript20.Size = New System.Drawing.Size(304, 54)
        Me.btnCreateScript20.TabIndex = 74
        Me.btnCreateScript20.Text = "Generate Client-side Email JavaScript"
        Me.btnCreateScript20.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(8, 192)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(192, 32)
        Me.Label2.TabIndex = 75
        Me.Label2.Text = "JavaScript:"
        '
        'FileSystemWatcher1
        '
        Me.FileSystemWatcher1.EnableRaisingEvents = True
        Me.FileSystemWatcher1.SynchronizingObject = Me
        '
        'frmEmailScriptsClient
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(598, 591)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.TabControl1)
        Me.ForeColor = System.Drawing.Color.Black
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Menu = Me.mnuMain
        Me.Name = "frmEmailScriptsClient"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PDForms: Client-side Email Script Generator"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage5.PerformLayout()
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
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
    Public Function InjectFieldNameValues(ByVal strInput As String) As String
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
        Return strTmp.ToString & ""
    End Function
    Public Sub addAutoCompleteFields(ByVal sessionBytesTemp() As Byte, ByVal pdfownerPasswordTemp As String)
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
            lv.Add(New clsAutocomplete(txtMessage_Body, fieldNames.ToArray(), True))
            lv.Add(New clsAutocomplete(txtTo_Email, fieldNames.ToArray(), False))
            lv.Add(New clsAutocomplete(txtCC_Email, fieldNames.ToArray(), False))
            lv.Add(New clsAutocomplete(txtBCC_Email, fieldNames.ToArray(), False))
            lv.Add(New clsAutocomplete(txtMessage_Subject, fieldNames.ToArray(), False))
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public Sub frm_Close()
        Try
            Me.DialogResult = Windows.Forms.DialogResult.OK
            If Not Me.Owner Is Nothing Then
                If TypeOf (Me.Owner) Is frmMain Then
                    Me.Owner.Show()
                    Me.Owner.BringToFront()
                End If
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub frmEmail_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        frm_Close()
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            SessionBytes = DirectCast(Me.Owner, frmMain).Session
            If submitFormAction = False Then
                addAutoCompleteFields(SessionBytes, DirectCast(Me.Owner, frmMain).pdfOwnerPassword)
            End If
            TabControl1.TabPages.Remove(TabPage5)
            lblStatus.Text = "Status: Ready"
            lblStatus.ForeColor = Color.DarkBlue
        Catch ex As Exception
            lblStatus.Text = "ERROR: Contact Support: " & ex.Message()
            lblStatus.ForeColor = Color.DarkRed
        End Try
    End Sub
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
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
        lblStatus.Text = "Status: Ready" & vbNewLine & vbNewLine & "You need a Microsoft .Net 1.1 enabled web server to use this script." & vbNewLine & vbNewLine & "For more information, please view the Setup Guide for instructions."
    End Sub
    Private Sub frmMain_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
    End Sub
    Private Sub Open_File(ByVal strPath As String)
        If strPath <> "" Then
            Process.Start(strPath)
        End If
    End Sub
    Private Sub MenuItem9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Hide()
    End Sub
    Private Sub lnkHelp_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        Try
        Catch ex As Exception
        End Try
    End Sub
    Private Sub optRecipients_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Show()
    End Sub
    Private Sub txtServer_Name_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub optFormat_PDF_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Update_OptFormat()
    End Sub
    Private Sub Update_OptFormat()
    End Sub
    Private Sub optFormat_FDF_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Update_OptFormat()
    End Sub
    Private Sub optFormat_xFDF_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Update_OptFormat()
    End Sub
    Private Sub optFormat_XML_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
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
    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Update_OptFormat()
    End Sub
    Private Sub optSMTP_Auth_None_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Update_SMTPOptions()
    End Sub
    Private Sub optSMTP_Auth_Basic_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Update_SMTPOptions()
    End Sub
    Private Sub optSMTP_Auth_Credentials_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Update_SMTPOptions()
    End Sub
    Private Sub Update_SMTPOptions()
    End Sub
    Private Sub optFormat_XPDF_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
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
        If txtScript.Text = "" Then
            If submitFormAction Then
                txtScript.Text = GenerateScript_ASPnet_20(True) & ""
            Else
                txtScript.Text = GenerateScript_ASPnet_20(False) & ""
            End If
        End If
        Try
            Clipboard.SetText(txtScript.Text & "", TextDataFormat.Text)
            lblStatus.Text = "Status: Copied to clipboard"
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub optFormat_RAW_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
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
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub txtSupportEmailBody_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
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
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
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
    Private Sub btnSupportScripts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub TabPage7_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub MenuItem10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Process.Start("http://fdftoolkit.codeplex.com")
        Catch ex As Exception
        End Try
    End Sub
    Private Sub lblPDFModifyPW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub txtPDF_ModifyPassword_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub lblPDFURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub UPDATE_SMTPDELIVERYMETHOD()
    End Sub
    Private Sub SMTP_Delivery_Method_Specify_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        UPDATE_SMTPDELIVERYMETHOD()
    End Sub
    Private Sub SMTP_Delivery_Method_Network_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        UPDATE_SMTPDELIVERYMETHOD()
    End Sub
    Private Sub SMTP_Delivery_Method_IIS_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        UPDATE_SMTPDELIVERYMETHOD()
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub TabPage2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage2.Click
    End Sub
    Private Sub Label16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label16.Click
    End Sub
    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)
    End Sub
    Private Sub Label15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub TabPage8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub txtPDF_OpenPassword_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub Label21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub TabPage4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub txtSettingName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub TabPage6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub lnkLblPDFURL_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        Try
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub txtPDFURL_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub txtPDFURL_TextChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub TabPage3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
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
    Private Sub btnMailServerGmailSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
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
    Private Sub txtPassword_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub txtPassword_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub txtPassword_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub txtPassword_Credential_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Function GetScript2() As String
        Dim strBuilder As New System.Text.StringBuilder
        strBuilder.AppendLine("try{")
        strBuilder.AppendLine("var emptyFields = [];")
        strBuilder.AppendLine("for (var i=0; i<this.numFields; i++) {")
        strBuilder.AppendLine(" var f= this.getField(this.getNthFieldName(i));")
        strBuilder.AppendLine(" if (f.type!=""button"" && f.required ){")
        strBuilder.AppendLine("     if ((f.type==""text"" && f.value=="""") || (f.type==""checkbox"" && f.value==""Off""))")
        strBuilder.AppendLine("     {")
        strBuilder.AppendLine("         emptyFields.push(f.name);")
        strBuilder.AppendLine("     };")
        strBuilder.AppendLine(" };")
        strBuilder.AppendLine("};")
        strBuilder.AppendLine("if (emptyFields.length>0) {")
        strBuilder.AppendLine(" app.alert(""Error! You must fill in the following fields:  \n"" + emptyFields.join(""\n""));")
        strBuilder.AppendLine("} else {")
        If submitFormRadioButton.Checked Then
            Dim formatSubmit As String = "FDF"
            If RadioButton1.Checked Then
                formatSubmit = "PDF"
            ElseIf RadioButton2.Checked Then
                formatSubmit = "FDF"
            ElseIf RadioButton3.Checked Then
                formatSubmit = "XFDF"
            ElseIf RadioButton4.Checked Then
                formatSubmit = "XML"
            ElseIf RadioButton5.Checked Then
                formatSubmit = "XDP"
            ElseIf RadioButton6.Checked Then
                formatSubmit = "HTML"
            Else
                formatSubmit = "FDF"
            End If
            strBuilder.AppendLine("var to = ""<MESSAGE_TO>"";")
            strBuilder.AppendLine("var strUrl = ""mailto:""+to+""?subject=<MESSAGE_SUBJECT>&body=<MESSAGE_BODY>&cc=<MESSAGE_CC>&bcc=<MESSAGE_BCC>"";")
            strBuilder.AppendLine("var submitAs = """ & formatSubmit & """;")
            strBuilder.AppendLine("this.submitForm({cURL: strUrl, cSubmitAs:""" & formatSubmit & """});")
        ElseIf mailDocRadioButton.Checked Then
            strBuilder.AppendLine("var to = ""<MESSAGE_TO>"";")
            strBuilder.AppendLine("var cc = ""<MESSAGE_CC>"";")
            strBuilder.AppendLine("var bcc = ""<MESSAGE_BCC>"";")
            strBuilder.AppendLine("var subject = ""<MESSAGE_SUBJECT>"";")
            strBuilder.AppendLine("var body = ""<MESSAGE_BODY>"";")
            strBuilder.AppendLine("this.mailDoc({ bUI: true, cTo: to, cCc: cc, cBcc: bcc, cSubject: subject, cMsg: body });")
        ElseIf mailFormRadioButton.Checked Then
            'mailForm                    
            strBuilder.AppendLine("var to = ""<MESSAGE_TO>"";")
            strBuilder.AppendLine("var cc = ""<MESSAGE_CC>"";")
            strBuilder.AppendLine("var bcc = ""<MESSAGE_BCC>"";")
            strBuilder.AppendLine("var subject = ""<MESSAGE_SUBJECT>"";")
            strBuilder.AppendLine("var body = ""<MESSAGE_BODY>"";")
            strBuilder.AppendLine("this.mailForm({ bUI: true, cTo: to, cCc: cc, cBcc: bcc, cSubject: subject, cMsg: body });")
        ElseIf mailMsgRadioButton.Checked Then
            'mailForm                
            strBuilder.AppendLine("var to = ""<MESSAGE_TO>"";")
            strBuilder.AppendLine("var cc = ""<MESSAGE_CC>"";")
            strBuilder.AppendLine("var bcc = ""<MESSAGE_BCC>"";")
            strBuilder.AppendLine("var subject = ""<MESSAGE_SUBJECT>"";")
            strBuilder.AppendLine("var body = ""<MESSAGE_BODY>"";")
            strBuilder.AppendLine("this.mailMsg({ bUI: true, cTo: to, cCc: cc, cBcc: bcc, cSubject: subject, cMsg: body });")
        Else
            Dim formatSubmit As String = "FDF"
            If RadioButton1.Checked Then
                formatSubmit = "PDF"
            ElseIf RadioButton2.Checked Then
                formatSubmit = "FDF"
            ElseIf RadioButton3.Checked Then
                formatSubmit = "XFDF"
            ElseIf RadioButton4.Checked Then
                formatSubmit = "XML"
            ElseIf RadioButton5.Checked Then
                formatSubmit = "XDP"
            ElseIf RadioButton6.Checked Then
                formatSubmit = "HTML"
            Else
                formatSubmit = "FDF"
            End If
            strBuilder.AppendLine("var to = ""<MESSAGE_TO>"";")
            strBuilder.AppendLine("var strUrl = ""mailto:""+to+""?subject=<MESSAGE_SUBJECT>&body=<MESSAGE_BODY>&cc=<MESSAGE_CC>&bcc=<MESSAGE_BCC>"";")
            strBuilder.AppendLine("var submitAs = """ & formatSubmit & """;")
            strBuilder.AppendLine("this.submitForm({cURL: strUrl, cSubmitAs:""" & formatSubmit & """});")
        End If
        strBuilder.AppendLine("}")
        strBuilder.AppendLine("}catch(e){app.alert(e);}")
        Return strBuilder.ToString
    End Function
    Private Function GetScriptUrl2() As String
        Return ("mailto:<MESSAGE_TO>?subject=<MESSAGE_SUBJECT>&body=<MESSAGE_BODY>&cc=<MESSAGE_CC>&bcc=<MESSAGE_BCC>")
    End Function
    Private Sub Button1_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Public Function ReplaceFieldWithGetField(ByVal str As String) As String
        Dim cFDFDoc As FDFApp.FDFDoc_Class = DirectCast(Me.Owner, frmMain).cFDFDoc
        For Each frmField As FDFApp.FDFDoc_Class.FDFField In DirectCast(Me.Owner, frmMain).cFDFDoc.XDPGetAllFields()
            str = str.Replace("{" & frmField.FieldName.ToString & "}", """ + encodeURIComponent("""" + this.getField(""" & frmField.FieldName & """).value + """") + """)
            str = str.Replace(";;", ";")
        Next
        Return str & ""
    End Function
    Private Function GenerateScript_ASPnet_20(Optional ByVal returnUrl As Boolean = False) As String
        Application.DoEvents()
        Me.Refresh()
        Dim strScript As String
        Try
            strScript = GetScript2()
            submitFormUrl = GetScriptUrl2()
            Dim xMsgBody As String ', xMsgBodyHTML As String
            xMsgBody = ReplaceFieldWithGetField(Me.txtMessage_Body.Text)
            strScript = strScript.Replace("<MESSAGE_BODY>", ReplaceFieldWithGetField(xMsgBody.ToString & "").Replace(Environment.NewLine, "\n"))
            submitFormUrl = submitFormUrl.Replace("<MESSAGE_BODY>", (Me.txtMessage_Body.Text & ""))
            Dim xMsgBodyHTML As String = ReplaceFieldWithGetField(Me.txtMessage_Body.Text.Replace(Environment.NewLine, "\n"))
            strScript = strScript.Replace("<MESSAGE_BODY_HTML>", ReplaceFieldWithGetField(xMsgBodyHTML.ToString))
            submitFormUrl = submitFormUrl.Replace("<MESSAGE_BODY_HTML>", (Me.txtMessage_Body.Text))
            strScript = strScript.Replace("<MESSAGE_SUBJECT>", ReplaceFieldWithGetField(Me.txtMessage_Subject.Text.ToString))
            submitFormUrl = submitFormUrl.Replace("<MESSAGE_SUBJECT>", (Me.txtMessage_Subject.Text.ToString))
            strScript = strScript.Replace("<MESSAGE_TO>", ReplaceFieldWithGetField(Me.txtTo_Email.Text & ""))
            submitFormUrl = submitFormUrl.Replace("<MESSAGE_TO>", (Me.txtTo_Email.Text & ""))
            strScript = strScript.Replace("<MESSAGE_BCC>", ReplaceFieldWithGetField(Me.txtBCC_Email.Text.ToString & ""))
            submitFormUrl = submitFormUrl.Replace("<MESSAGE_BCC>", (Me.txtBCC_Email.Text.ToString & ""))
            strScript = strScript.Replace("<MESSAGE_CC>", ReplaceFieldWithGetField(Me.txtCC_Email.Text.ToString & ""))
            submitFormUrl = submitFormUrl.Replace("<MESSAGE_CC>", (Me.txtCC_Email.Text.ToString & ""))
            Me.lblStatus.Text = "Status: Script generated successfully!"
            If submitFormAction Or returnUrl Then
                Return submitFormUrl
            Else
                Return strScript.ToString
            End If
        Catch ex As Exception
            Me.lblStatus.Text = "Status: " & ex.Message
            Return ""
            Exit Function
        End Try
    End Function
    Private Sub btnCreateScript20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreateScript20.Click
        If submitFormAction Then
            txtScript.Text = GenerateScript_ASPnet_20(True) & ""
        Else
            txtScript.Text = GenerateScript_ASPnet_20(False) & ""
        End If
        btnCopy_Click(Me, New EventArgs())
    End Sub
    Private Sub txtSent_URL_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub submitFormRadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles submitFormRadioButton.CheckedChanged
        GroupBox2.Visible = submitFormRadioButton.Checked
        If submitFormRadioButton.Checked Then
            Label4.Text = "Information: submitForm() method submits the whole PDF format in Adobe Reader."
        ElseIf mailDocRadioButton.Checked Then
            Label4.Text = "Information: mailDoc() method submits whole PDF format. Extended Usage Rights required."
        ElseIf mailFormRadioButton.Checked Then
            Label4.Text = "Information: mailForm() method submits FDF format in Adobe Reader."
        ElseIf mailMsgRadioButton.Checked Then
            Label4.Text = "Information: mailMsg() method submits FDF format in Adobe Reader."
        Else
            Label4.Text = "Information: Select a method"
        End If
    End Sub
    Private Sub mailDocRadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mailDocRadioButton.CheckedChanged
        GroupBox2.Visible = submitFormRadioButton.Checked
        If submitFormRadioButton.Checked Then
            Label4.Text = "Information: submitForm() method submits the whole PDF format in Adobe Reader."
        ElseIf mailDocRadioButton.Checked Then
            Label4.Text = "Information: mailDoc() method submits whole PDF format. Extended Usage Rights required."
        ElseIf mailFormRadioButton.Checked Then
            Label4.Text = "Information: mailForm() method submits FDF format in Adobe Reader."
        ElseIf mailMsgRadioButton.Checked Then
            Label4.Text = "Information: mailMsg() method submits FDF format in Adobe Reader."
        Else
            Label4.Text = "Information: Select a method"
        End If
    End Sub
    Private Sub mailFormRadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mailFormRadioButton.CheckedChanged
        GroupBox2.Visible = submitFormRadioButton.Checked
        If submitFormRadioButton.Checked Then
            Label4.Text = "Information: submitForm() method submits the whole PDF format in Adobe Reader."
        ElseIf mailDocRadioButton.Checked Then
            Label4.Text = "Information: mailDoc() method submits whole PDF format. Extended Usage Rights required."
        ElseIf mailFormRadioButton.Checked Then
            Label4.Text = "Information: mailForm() method submits FDF format in Adobe Reader."
        ElseIf mailMsgRadioButton.Checked Then
            Label4.Text = "Information: mailMsg() method submits FDF format in Adobe Reader."
        Else
            Label4.Text = "Information: Select a method"
        End If
    End Sub
    Private Sub mailMsgRadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mailMsgRadioButton.CheckedChanged
        GroupBox2.Visible = submitFormRadioButton.Checked
        If submitFormRadioButton.Checked Then
            Label4.Text = "Information: submitForm() method submits the whole PDF format in Adobe Reader."
        ElseIf mailDocRadioButton.Checked Then
            Label4.Text = "Information: mailDoc() method submits whole PDF format. Extended Usage Rights required."
        ElseIf mailFormRadioButton.Checked Then
            Label4.Text = "Information: mailForm() method submits FDF format in Adobe Reader."
        ElseIf mailMsgRadioButton.Checked Then
            Label4.Text = "Information: mailMsg() method submits FDF format in Adobe Reader."
        Else
            Label4.Text = "Information: Select a method"
        End If
    End Sub
    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If txtScript.Text = "" Then
            If submitFormAction Then
                txtScript.Text = GenerateScript_ASPnet_20(True) & ""
            Else
                txtScript.Text = GenerateScript_ASPnet_20(False) & ""
            End If
        End If
        If submitFormAction Then
            DirectCast(Me.Owner, frmMain).PDFField_Action_Panel_SubmitForm_URL.Text = submitFormUrl & ""
        Else
            DirectCast(Me.Owner, frmMain).PDFField_Action_Panel_JavaScript_TextBox.Text = txtScript.Text & ""
        End If
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
    Private Sub RadioButton1_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        If submitFormRadioButton.Checked Then
            Label4.Text = "Information: submitForm() method submits the whole PDF format in Adobe Reader."
        ElseIf mailDocRadioButton.Checked Then
            Label4.Text = "Information: mailDoc() method submits whole PDF format. Extended Usage Rights required."
        ElseIf mailFormRadioButton.Checked Then
            Label4.Text = "Information: mailForm() method submits FDF format in Adobe Reader."
        ElseIf mailMsgRadioButton.Checked Then
            Label4.Text = "Information: mailMsg() method submits FDF format in Adobe Reader."
        Else
            Label4.Text = "Information: Select a method"
        End If
    End Sub
    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        If submitFormRadioButton.Checked Then
            Label4.Text = "Information: submitForm() method submits the whole FDF format in Adobe Reader."
        ElseIf mailDocRadioButton.Checked Then
            Label4.Text = "Information: mailDoc() method submits whole PDF format. Extended Usage Rights required."
        ElseIf mailFormRadioButton.Checked Then
            Label4.Text = "Information: mailForm() method submits FDF format in Adobe Reader."
        ElseIf mailMsgRadioButton.Checked Then
            Label4.Text = "Information: mailMsg() method submits FDF format in Adobe Reader."
        Else
            Label4.Text = "Information: Select a method"
        End If
    End Sub
    Private Sub RadioButton3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton3.CheckedChanged
        If submitFormRadioButton.Checked Then
            Label4.Text = "Information: submitForm() method submits the whole xFDF format in Adobe Reader."
        ElseIf mailDocRadioButton.Checked Then
            Label4.Text = "Information: mailDoc() method submits whole PDF format. Extended Usage Rights required."
        ElseIf mailFormRadioButton.Checked Then
            Label4.Text = "Information: mailForm() method submits FDF format in Adobe Reader."
        ElseIf mailMsgRadioButton.Checked Then
            Label4.Text = "Information: mailMsg() method submits FDF format in Adobe Reader."
        Else
            Label4.Text = "Information: Select a method"
        End If
    End Sub
    Private Sub RadioButton4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton4.CheckedChanged
        If submitFormRadioButton.Checked Then
            Label4.Text = "Information: submitForm() method submits the whole XML format in Adobe Reader."
        ElseIf mailDocRadioButton.Checked Then
            Label4.Text = "Information: mailDoc() method submits whole PDF format. Extended Usage Rights required."
        ElseIf mailFormRadioButton.Checked Then
            Label4.Text = "Information: mailForm() method submits FDF format in Adobe Reader."
        ElseIf mailMsgRadioButton.Checked Then
            Label4.Text = "Information: mailMsg() method submits FDF format in Adobe Reader."
        Else
            Label4.Text = "Information: Select a method"
        End If
    End Sub
    Private Sub RadioButton5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton5.CheckedChanged
        If submitFormRadioButton.Checked Then
            Label4.Text = "Information: submitForm() method submits the whole XDP format in Adobe Reader."
        ElseIf mailDocRadioButton.Checked Then
            Label4.Text = "Information: mailDoc() method submits whole PDF format. Extended Usage Rights required."
        ElseIf mailFormRadioButton.Checked Then
            Label4.Text = "Information: mailForm() method submits FDF format in Adobe Reader."
        ElseIf mailMsgRadioButton.Checked Then
            Label4.Text = "Information: mailMsg() method submits FDF format in Adobe Reader."
        Else
            Label4.Text = "Information: Select a method"
        End If
    End Sub
    Private Sub RadioButton6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton6.CheckedChanged
        If submitFormRadioButton.Checked Then
            Label4.Text = "Information: submitForm() method submits the whole HTML format in Adobe Reader."
        ElseIf mailDocRadioButton.Checked Then
            Label4.Text = "Information: mailDoc() method submits whole PDF format. Extended Usage Rights required."
        ElseIf mailFormRadioButton.Checked Then
            Label4.Text = "Information: mailForm() method submits FDF format in Adobe Reader."
        ElseIf mailMsgRadioButton.Checked Then
            Label4.Text = "Information: mailMsg() method submits FDF format in Adobe Reader."
        Else
            Label4.Text = "Information: Select a method"
        End If
    End Sub
End Class
