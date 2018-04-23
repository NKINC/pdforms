<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dialogFTP
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
        Me.components = New System.ComponentModel.Container()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.btnLoadFTP = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.txtFTPRoot = New System.Windows.Forms.TextBox()
        Me.txtFTPUsername = New System.Windows.Forms.TextBox()
        Me.txtFTPPassword = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtFTPCurrentFolder = New System.Windows.Forms.TextBox()
        Me.btnFTPUp = New System.Windows.Forms.Button()
        Me.txtFileName = New System.Windows.Forms.TextBox()
        Me.lblUpload_FileName = New System.Windows.Forms.Label()
        Me.btnFileUpload = New System.Windows.Forms.Button()
        Me.ContextMenuStripFile1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.UpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UploadFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenWithPDFEditorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenFtpURLToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenHttpURLToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenWithToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DownloadFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RenameFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewFolderToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStripFolder1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BackToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenFolderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewFolderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RenameFolderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteFolderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBoxFile1 = New System.Windows.Forms.GroupBox()
        Me.btnFileOpen = New System.Windows.Forms.Button()
        Me.btnFileDelete = New System.Windows.Forms.Button()
        Me.btnFileRename = New System.Windows.Forms.Button()
        Me.btnFileOpenDialog1 = New System.Windows.Forms.Button()
        Me.btnFileDownload = New System.Windows.Forms.Button()
        Me.btnFileGetPdfFromEditor = New System.Windows.Forms.Button()
        Me.btnFileLoadPdfFileIntoPdfEditor = New System.Windows.Forms.Button()
        Me.GroupBoxFolder1 = New System.Windows.Forms.GroupBox()
        Me.btnFolderNew = New System.Windows.Forms.Button()
        Me.btnFolderBrowse = New System.Windows.Forms.Button()
        Me.btnFolderDelete = New System.Windows.Forms.Button()
        Me.btnFolderRename = New System.Windows.Forms.Button()
        Me.txtFolderName = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStripFile1.SuspendLayout()
        Me.ContextMenuStripFolder1.SuspendLayout()
        Me.GroupBoxFile1.SuspendLayout()
        Me.GroupBoxFolder1.SuspendLayout()
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
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(550, 440)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(154, 80)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(5, 21)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 37)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(82, 21)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 37)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'btnLoadFTP
        '
        Me.btnLoadFTP.Location = New System.Drawing.Point(8, 8)
        Me.btnLoadFTP.Name = "btnLoadFTP"
        Me.btnLoadFTP.Size = New System.Drawing.Size(72, 56)
        Me.btnLoadFTP.TabIndex = 1
        Me.btnLoadFTP.Text = "Load"
        Me.btnLoadFTP.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(0, 72)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox1.Size = New System.Drawing.Size(704, 120)
        Me.TextBox1.TabIndex = 2
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(0, 440)
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox2.Size = New System.Drawing.Size(544, 48)
        Me.TextBox2.TabIndex = 3
        Me.TextBox2.TabStop = False
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DataGridView1.Location = New System.Drawing.Point(0, 72)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(704, 288)
        Me.DataGridView1.TabIndex = 4
        '
        'txtFTPRoot
        '
        Me.txtFTPRoot.Location = New System.Drawing.Point(136, 8)
        Me.txtFTPRoot.Name = "txtFTPRoot"
        Me.txtFTPRoot.Size = New System.Drawing.Size(376, 20)
        Me.txtFTPRoot.TabIndex = 5
        Me.txtFTPRoot.Text = "ftp://"
        '
        'txtFTPUsername
        '
        Me.txtFTPUsername.Location = New System.Drawing.Point(584, 8)
        Me.txtFTPUsername.Name = "txtFTPUsername"
        Me.txtFTPUsername.Size = New System.Drawing.Size(112, 20)
        Me.txtFTPUsername.TabIndex = 6
        '
        'txtFTPPassword
        '
        Me.txtFTPPassword.Location = New System.Drawing.Point(584, 40)
        Me.txtFTPPassword.Name = "txtFTPPassword"
        Me.txtFTPPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtFTPPassword.Size = New System.Drawing.Size(112, 20)
        Me.txtFTPPassword.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(80, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "FTP Root:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(520, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "User name:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(528, 40)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Password:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(96, 40)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(39, 13)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Folder:"
        '
        'txtFTPCurrentFolder
        '
        Me.txtFTPCurrentFolder.Location = New System.Drawing.Point(136, 40)
        Me.txtFTPCurrentFolder.Name = "txtFTPCurrentFolder"
        Me.txtFTPCurrentFolder.Size = New System.Drawing.Size(336, 20)
        Me.txtFTPCurrentFolder.TabIndex = 12
        Me.txtFTPCurrentFolder.Text = "/"
        '
        'btnFTPUp
        '
        Me.btnFTPUp.Location = New System.Drawing.Point(472, 40)
        Me.btnFTPUp.Name = "btnFTPUp"
        Me.btnFTPUp.Size = New System.Drawing.Size(40, 24)
        Me.btnFTPUp.TabIndex = 13
        Me.btnFTPUp.Text = "Up"
        Me.btnFTPUp.UseVisualStyleBackColor = True
        '
        'txtFileName
        '
        Me.txtFileName.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFileName.Location = New System.Drawing.Point(104, 24)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.Size = New System.Drawing.Size(320, 23)
        Me.txtFileName.TabIndex = 14
        '
        'lblUpload_FileName
        '
        Me.lblUpload_FileName.Location = New System.Drawing.Point(8, 24)
        Me.lblUpload_FileName.Name = "lblUpload_FileName"
        Me.lblUpload_FileName.Size = New System.Drawing.Size(96, 48)
        Me.lblUpload_FileName.TabIndex = 15
        Me.lblUpload_FileName.Text = "File Name:"
        Me.lblUpload_FileName.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'btnFileUpload
        '
        Me.btnFileUpload.Location = New System.Drawing.Point(424, 48)
        Me.btnFileUpload.Name = "btnFileUpload"
        Me.btnFileUpload.Size = New System.Drawing.Size(128, 24)
        Me.btnFileUpload.TabIndex = 16
        Me.btnFileUpload.Text = "Upload File"
        Me.btnFileUpload.UseVisualStyleBackColor = True
        '
        'ContextMenuStripFile1
        '
        Me.ContextMenuStripFile1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UpToolStripMenuItem, Me.UploadFileToolStripMenuItem, Me.OpenWithPDFEditorToolStripMenuItem, Me.OpenFileToolStripMenuItem, Me.OpenFtpURLToolStripMenuItem, Me.OpenHttpURLToolStripMenuItem, Me.OpenWithToolStripMenuItem, Me.DownloadFileToolStripMenuItem, Me.RenameFileToolStripMenuItem, Me.DeleteFileToolStripMenuItem, Me.NewFolderToolStripMenuItem1})
        Me.ContextMenuStripFile1.Name = "ContextMenuStripFile1"
        Me.ContextMenuStripFile1.Size = New System.Drawing.Size(207, 246)
        '
        'UpToolStripMenuItem
        '
        Me.UpToolStripMenuItem.Name = "UpToolStripMenuItem"
        Me.UpToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
        Me.UpToolStripMenuItem.Text = "<< Back"
        '
        'UploadFileToolStripMenuItem
        '
        Me.UploadFileToolStripMenuItem.Name = "UploadFileToolStripMenuItem"
        Me.UploadFileToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
        Me.UploadFileToolStripMenuItem.Text = "Upload File"
        Me.UploadFileToolStripMenuItem.Visible = False
        '
        'OpenWithPDFEditorToolStripMenuItem
        '
        Me.OpenWithPDFEditorToolStripMenuItem.Name = "OpenWithPDFEditorToolStripMenuItem"
        Me.OpenWithPDFEditorToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
        Me.OpenWithPDFEditorToolStripMenuItem.Text = "Edit in PDForms"
        '
        'OpenFileToolStripMenuItem
        '
        Me.OpenFileToolStripMenuItem.Name = "OpenFileToolStripMenuItem"
        Me.OpenFileToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
        Me.OpenFileToolStripMenuItem.Text = "Open with default viewer"
        '
        'OpenFtpURLToolStripMenuItem
        '
        Me.OpenFtpURLToolStripMenuItem.Name = "OpenFtpURLToolStripMenuItem"
        Me.OpenFtpURLToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
        Me.OpenFtpURLToolStripMenuItem.Text = "Open FTP URL"
        '
        'OpenHttpURLToolStripMenuItem
        '
        Me.OpenHttpURLToolStripMenuItem.Name = "OpenHttpURLToolStripMenuItem"
        Me.OpenHttpURLToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
        Me.OpenHttpURLToolStripMenuItem.Text = "Open Http URL"
        '
        'OpenWithToolStripMenuItem
        '
        Me.OpenWithToolStripMenuItem.Name = "OpenWithToolStripMenuItem"
        Me.OpenWithToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
        Me.OpenWithToolStripMenuItem.Text = "Open With.."
        '
        'DownloadFileToolStripMenuItem
        '
        Me.DownloadFileToolStripMenuItem.Name = "DownloadFileToolStripMenuItem"
        Me.DownloadFileToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
        Me.DownloadFileToolStripMenuItem.Text = "Download File"
        '
        'RenameFileToolStripMenuItem
        '
        Me.RenameFileToolStripMenuItem.Name = "RenameFileToolStripMenuItem"
        Me.RenameFileToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
        Me.RenameFileToolStripMenuItem.Text = "Rename File"
        Me.RenameFileToolStripMenuItem.Visible = False
        '
        'DeleteFileToolStripMenuItem
        '
        Me.DeleteFileToolStripMenuItem.Name = "DeleteFileToolStripMenuItem"
        Me.DeleteFileToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
        Me.DeleteFileToolStripMenuItem.Text = "Delete File"
        '
        'NewFolderToolStripMenuItem1
        '
        Me.NewFolderToolStripMenuItem1.Name = "NewFolderToolStripMenuItem1"
        Me.NewFolderToolStripMenuItem1.Size = New System.Drawing.Size(206, 22)
        Me.NewFolderToolStripMenuItem1.Text = "New Folder"
        '
        'ContextMenuStripFolder1
        '
        Me.ContextMenuStripFolder1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BackToolStripMenuItem, Me.OpenFolderToolStripMenuItem, Me.NewFolderToolStripMenuItem, Me.RenameFolderToolStripMenuItem, Me.DeleteFolderToolStripMenuItem})
        Me.ContextMenuStripFolder1.Name = "ContextMenuStripFolder1"
        Me.ContextMenuStripFolder1.Size = New System.Drawing.Size(154, 114)
        '
        'BackToolStripMenuItem
        '
        Me.BackToolStripMenuItem.Name = "BackToolStripMenuItem"
        Me.BackToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.BackToolStripMenuItem.Text = "<< Back"
        '
        'OpenFolderToolStripMenuItem
        '
        Me.OpenFolderToolStripMenuItem.Name = "OpenFolderToolStripMenuItem"
        Me.OpenFolderToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.OpenFolderToolStripMenuItem.Text = "Browse Folder"
        '
        'NewFolderToolStripMenuItem
        '
        Me.NewFolderToolStripMenuItem.Name = "NewFolderToolStripMenuItem"
        Me.NewFolderToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.NewFolderToolStripMenuItem.Text = "New Folder"
        Me.NewFolderToolStripMenuItem.Visible = False
        '
        'RenameFolderToolStripMenuItem
        '
        Me.RenameFolderToolStripMenuItem.Name = "RenameFolderToolStripMenuItem"
        Me.RenameFolderToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.RenameFolderToolStripMenuItem.Text = "Rename Folder"
        Me.RenameFolderToolStripMenuItem.Visible = False
        '
        'DeleteFolderToolStripMenuItem
        '
        Me.DeleteFolderToolStripMenuItem.Name = "DeleteFolderToolStripMenuItem"
        Me.DeleteFolderToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.DeleteFolderToolStripMenuItem.Text = "Delete Folder"
        '
        'GroupBoxFile1
        '
        Me.GroupBoxFile1.Controls.Add(Me.btnFileOpen)
        Me.GroupBoxFile1.Controls.Add(Me.btnFileDelete)
        Me.GroupBoxFile1.Controls.Add(Me.btnFileRename)
        Me.GroupBoxFile1.Controls.Add(Me.btnFileOpenDialog1)
        Me.GroupBoxFile1.Controls.Add(Me.btnFileDownload)
        Me.GroupBoxFile1.Controls.Add(Me.txtFileName)
        Me.GroupBoxFile1.Controls.Add(Me.btnFileUpload)
        Me.GroupBoxFile1.Controls.Add(Me.lblUpload_FileName)
        Me.GroupBoxFile1.Location = New System.Drawing.Point(0, 360)
        Me.GroupBoxFile1.Name = "GroupBoxFile1"
        Me.GroupBoxFile1.Size = New System.Drawing.Size(560, 80)
        Me.GroupBoxFile1.TabIndex = 17
        Me.GroupBoxFile1.TabStop = False
        Me.GroupBoxFile1.Text = "File Options:"
        '
        'btnFileOpen
        '
        Me.btnFileOpen.Location = New System.Drawing.Point(488, 24)
        Me.btnFileOpen.Name = "btnFileOpen"
        Me.btnFileOpen.Size = New System.Drawing.Size(64, 24)
        Me.btnFileOpen.TabIndex = 24
        Me.btnFileOpen.Text = "Open.."
        Me.btnFileOpen.UseVisualStyleBackColor = True
        '
        'btnFileDelete
        '
        Me.btnFileDelete.Location = New System.Drawing.Point(200, 48)
        Me.btnFileDelete.Name = "btnFileDelete"
        Me.btnFileDelete.Size = New System.Drawing.Size(88, 24)
        Me.btnFileDelete.TabIndex = 20
        Me.btnFileDelete.Text = "Delete"
        Me.btnFileDelete.UseVisualStyleBackColor = True
        '
        'btnFileRename
        '
        Me.btnFileRename.Location = New System.Drawing.Point(104, 48)
        Me.btnFileRename.Name = "btnFileRename"
        Me.btnFileRename.Size = New System.Drawing.Size(88, 24)
        Me.btnFileRename.TabIndex = 18
        Me.btnFileRename.Text = "Rename"
        Me.btnFileRename.UseVisualStyleBackColor = True
        '
        'btnFileOpenDialog1
        '
        Me.btnFileOpenDialog1.Location = New System.Drawing.Point(424, 24)
        Me.btnFileOpenDialog1.Name = "btnFileOpenDialog1"
        Me.btnFileOpenDialog1.Size = New System.Drawing.Size(64, 24)
        Me.btnFileOpenDialog1.TabIndex = 17
        Me.btnFileOpenDialog1.Text = "browse.."
        Me.btnFileOpenDialog1.UseVisualStyleBackColor = True
        '
        'btnFileDownload
        '
        Me.btnFileDownload.Location = New System.Drawing.Point(296, 48)
        Me.btnFileDownload.Name = "btnFileDownload"
        Me.btnFileDownload.Size = New System.Drawing.Size(88, 24)
        Me.btnFileDownload.TabIndex = 19
        Me.btnFileDownload.Text = "Download"
        Me.btnFileDownload.UseVisualStyleBackColor = True
        '
        'btnFileGetPdfFromEditor
        '
        Me.btnFileGetPdfFromEditor.Location = New System.Drawing.Point(560, 368)
        Me.btnFileGetPdfFromEditor.Name = "btnFileGetPdfFromEditor"
        Me.btnFileGetPdfFromEditor.Size = New System.Drawing.Size(144, 32)
        Me.btnFileGetPdfFromEditor.TabIndex = 23
        Me.btnFileGetPdfFromEditor.Text = "Load from PDFEdior"
        Me.btnFileGetPdfFromEditor.UseVisualStyleBackColor = True
        '
        'btnFileLoadPdfFileIntoPdfEditor
        '
        Me.btnFileLoadPdfFileIntoPdfEditor.Location = New System.Drawing.Point(560, 408)
        Me.btnFileLoadPdfFileIntoPdfEditor.Name = "btnFileLoadPdfFileIntoPdfEditor"
        Me.btnFileLoadPdfFileIntoPdfEditor.Size = New System.Drawing.Size(144, 32)
        Me.btnFileLoadPdfFileIntoPdfEditor.TabIndex = 22
        Me.btnFileLoadPdfFileIntoPdfEditor.Text = "Edit in PDForms"
        Me.btnFileLoadPdfFileIntoPdfEditor.UseVisualStyleBackColor = True
        '
        'GroupBoxFolder1
        '
        Me.GroupBoxFolder1.Controls.Add(Me.btnFolderNew)
        Me.GroupBoxFolder1.Controls.Add(Me.btnFolderBrowse)
        Me.GroupBoxFolder1.Controls.Add(Me.btnFolderDelete)
        Me.GroupBoxFolder1.Controls.Add(Me.btnFolderRename)
        Me.GroupBoxFolder1.Controls.Add(Me.txtFolderName)
        Me.GroupBoxFolder1.Controls.Add(Me.Label5)
        Me.GroupBoxFolder1.Location = New System.Drawing.Point(0, 360)
        Me.GroupBoxFolder1.Name = "GroupBoxFolder1"
        Me.GroupBoxFolder1.Size = New System.Drawing.Size(560, 80)
        Me.GroupBoxFolder1.TabIndex = 23
        Me.GroupBoxFolder1.TabStop = False
        Me.GroupBoxFolder1.Text = "Folder Options:"
        '
        'btnFolderNew
        '
        Me.btnFolderNew.Location = New System.Drawing.Point(408, 48)
        Me.btnFolderNew.Name = "btnFolderNew"
        Me.btnFolderNew.Size = New System.Drawing.Size(88, 24)
        Me.btnFolderNew.TabIndex = 22
        Me.btnFolderNew.Text = "*New Folder"
        Me.btnFolderNew.UseVisualStyleBackColor = True
        '
        'btnFolderBrowse
        '
        Me.btnFolderBrowse.Location = New System.Drawing.Point(120, 48)
        Me.btnFolderBrowse.Name = "btnFolderBrowse"
        Me.btnFolderBrowse.Size = New System.Drawing.Size(88, 24)
        Me.btnFolderBrowse.TabIndex = 21
        Me.btnFolderBrowse.Text = "Browse"
        Me.btnFolderBrowse.UseVisualStyleBackColor = True
        '
        'btnFolderDelete
        '
        Me.btnFolderDelete.Location = New System.Drawing.Point(216, 48)
        Me.btnFolderDelete.Name = "btnFolderDelete"
        Me.btnFolderDelete.Size = New System.Drawing.Size(88, 24)
        Me.btnFolderDelete.TabIndex = 20
        Me.btnFolderDelete.Text = "Delete"
        Me.btnFolderDelete.UseVisualStyleBackColor = True
        '
        'btnFolderRename
        '
        Me.btnFolderRename.Location = New System.Drawing.Point(312, 48)
        Me.btnFolderRename.Name = "btnFolderRename"
        Me.btnFolderRename.Size = New System.Drawing.Size(88, 24)
        Me.btnFolderRename.TabIndex = 18
        Me.btnFolderRename.Text = "Rename"
        Me.btnFolderRename.UseVisualStyleBackColor = True
        '
        'txtFolderName
        '
        Me.txtFolderName.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFolderName.Location = New System.Drawing.Point(120, 24)
        Me.txtFolderName.Name = "txtFolderName"
        Me.txtFolderName.Size = New System.Drawing.Size(376, 23)
        Me.txtFolderName.TabIndex = 14
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(8, 20)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(112, 24)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Folder Name:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ProgressBar1
        '
        Me.ProgressBar1.BackColor = System.Drawing.SystemColors.Control
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 496)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(544, 23)
        Me.ProgressBar1.TabIndex = 24
        '
        'dialogFTP
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(708, 521)
        Me.Controls.Add(Me.btnFTPUp)
        Me.Controls.Add(Me.btnFileGetPdfFromEditor)
        Me.Controls.Add(Me.btnFileLoadPdfFileIntoPdfEditor)
        Me.Controls.Add(Me.txtFTPCurrentFolder)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtFTPPassword)
        Me.Controls.Add(Me.txtFTPUsername)
        Me.Controls.Add(Me.txtFTPRoot)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.btnLoadFTP)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.GroupBoxFile1)
        Me.Controls.Add(Me.GroupBoxFolder1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dialogFTP"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "FTP"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStripFile1.ResumeLayout(False)
        Me.ContextMenuStripFolder1.ResumeLayout(False)
        Me.GroupBoxFile1.ResumeLayout(False)
        Me.GroupBoxFile1.PerformLayout()
        Me.GroupBoxFolder1.ResumeLayout(False)
        Me.GroupBoxFolder1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents btnLoadFTP As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnFTPUp As System.Windows.Forms.Button
    Friend WithEvents lblUpload_FileName As System.Windows.Forms.Label
    Friend WithEvents btnFileUpload As System.Windows.Forms.Button
    Friend WithEvents ContextMenuStripFile1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ContextMenuStripFolder1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DownloadFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RenameFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UploadFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RenameFolderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewFolderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteFolderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenFolderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GroupBoxFile1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnFileOpenDialog1 As System.Windows.Forms.Button
    Friend WithEvents btnFileRename As System.Windows.Forms.Button
    Friend WithEvents btnFileDelete As System.Windows.Forms.Button
    Friend WithEvents btnFileDownload As System.Windows.Forms.Button
    Friend WithEvents btnFileLoadPdfFileIntoPdfEditor As System.Windows.Forms.Button
    Friend WithEvents GroupBoxFolder1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnFolderBrowse As System.Windows.Forms.Button
    Friend WithEvents btnFolderDelete As System.Windows.Forms.Button
    Friend WithEvents btnFolderRename As System.Windows.Forms.Button
    Friend WithEvents txtFolderName As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnFileGetPdfFromEditor As System.Windows.Forms.Button
    Friend WithEvents btnFileOpen As System.Windows.Forms.Button
    Friend WithEvents UpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BackToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents OpenWithPDFEditorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Public WithEvents txtFTPRoot As System.Windows.Forms.TextBox
    Public WithEvents txtFTPUsername As System.Windows.Forms.TextBox
    Public WithEvents txtFTPPassword As System.Windows.Forms.TextBox
    Public WithEvents txtFTPCurrentFolder As System.Windows.Forms.TextBox
    Public WithEvents txtFileName As System.Windows.Forms.TextBox
    Friend WithEvents OpenWithToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnFolderNew As System.Windows.Forms.Button
    Friend WithEvents NewFolderToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenHttpURLToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenFtpURLToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
