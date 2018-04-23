<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dialogDataSource
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dialogDataSource))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.cmnDlg = New System.Windows.Forms.OpenFileDialog()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.pnlFields = New System.Windows.Forms.Panel()
        Me.lstMappedFields = New System.Windows.Forms.ListBox()
        Me.txtMappingRaw = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.lstDBFields = New System.Windows.Forms.CheckedListBox()
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
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.pnlFilterPrint = New System.Windows.Forms.Panel()
        Me.lstFilter = New System.Windows.Forms.ListBox()
        Me.btnSavePDFs = New System.Windows.Forms.Button()
        Me.cmbComparison = New System.Windows.Forms.ComboBox()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.cmbDBFieldFilter = New System.Windows.Forms.ComboBox()
        Me.btnRecords_Affected = New System.Windows.Forms.Button()
        Me.txtFieldValue = New System.Windows.Forms.TextBox()
        Me.lnkRemoveFilter = New System.Windows.Forms.LinkLabel()
        Me.lnkAddFilter = New System.Windows.Forms.LinkLabel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmbDBTablesFilter = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.chkExchange = New System.Windows.Forms.CheckBox()
        Me.chkFDFTKSetup = New System.Windows.Forms.CheckBox()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.pnlDataSource = New System.Windows.Forms.Panel()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.btnSelectSource = New System.Windows.Forms.Button()
        Me.txtDataFields_1 = New System.Windows.Forms.TextBox()
        Me.txtDataFields_2 = New System.Windows.Forms.TextBox()
        Me.txtDataFields_0 = New System.Windows.Forms.TextBox()
        Me.txtDataFields_3 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblConnected = New System.Windows.Forms.Label()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.fldOutput = New System.Windows.Forms.FolderBrowserDialog()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.GroupBoxNavigate = New System.Windows.Forms.GroupBox()
        Me.btnRecordNav_NewRecord = New System.Windows.Forms.Button()
        Me.btnRecordNav_DelRecord = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.btnUpdateDatabase = New System.Windows.Forms.Button()
        Me.btnMinimize = New System.Windows.Forms.Button()
        Me.btnRecordNav_First = New System.Windows.Forms.Button()
        Me.btnRecordNav_Last = New System.Windows.Forms.Button()
        Me.btnRecordNav_Previous = New System.Windows.Forms.Button()
        Me.btnRecordNav_Next = New System.Windows.Forms.Button()
        Me.ddlRecord = New System.Windows.Forms.ComboBox()
        Me.SaveFileSettings = New System.Windows.Forms.SaveFileDialog()
        Me.OpenFileSettings = New System.Windows.Forms.OpenFileDialog()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.pnlFields.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.pnlFilterPrint.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.pnlDataSource.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage6.SuspendLayout()
        Me.GroupBoxNavigate.SuspendLayout()
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
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(592, 476)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "EDIT"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Close"
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.pnlFields)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(736, 401)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "2) Field Mappings"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'pnlFields
        '
        Me.pnlFields.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlFields.BackColor = System.Drawing.Color.Transparent
        Me.pnlFields.Controls.Add(Me.lstMappedFields)
        Me.pnlFields.Controls.Add(Me.txtMappingRaw)
        Me.pnlFields.Controls.Add(Me.Label20)
        Me.pnlFields.Controls.Add(Me.Label17)
        Me.pnlFields.Controls.Add(Me.lstDBFields)
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
        Me.pnlFields.Size = New System.Drawing.Size(720, 384)
        Me.pnlFields.TabIndex = 94
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
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.pnlFilterPrint)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(736, 401)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "3) Data Filters"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'pnlFilterPrint
        '
        Me.pnlFilterPrint.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlFilterPrint.BackColor = System.Drawing.Color.Transparent
        Me.pnlFilterPrint.Controls.Add(Me.lstFilter)
        Me.pnlFilterPrint.Controls.Add(Me.btnSavePDFs)
        Me.pnlFilterPrint.Controls.Add(Me.cmbComparison)
        Me.pnlFilterPrint.Controls.Add(Me.btnPrint)
        Me.pnlFilterPrint.Controls.Add(Me.cmbDBFieldFilter)
        Me.pnlFilterPrint.Controls.Add(Me.btnRecords_Affected)
        Me.pnlFilterPrint.Controls.Add(Me.txtFieldValue)
        Me.pnlFilterPrint.Controls.Add(Me.lnkRemoveFilter)
        Me.pnlFilterPrint.Controls.Add(Me.lnkAddFilter)
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
        Me.pnlFilterPrint.Size = New System.Drawing.Size(718, 382)
        Me.pnlFilterPrint.TabIndex = 95
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
        'lblMessage
        '
        Me.lblMessage.BackColor = System.Drawing.Color.Transparent
        Me.lblMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessage.Location = New System.Drawing.Point(-2, 462)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(586, 56)
        Me.lblMessage.TabIndex = 151
        Me.lblMessage.Text = "Status: ready..."
        '
        'chkExchange
        '
        Me.chkExchange.BackColor = System.Drawing.Color.Transparent
        Me.chkExchange.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.chkExchange.Location = New System.Drawing.Point(46, 313)
        Me.chkExchange.Name = "chkExchange"
        Me.chkExchange.Size = New System.Drawing.Size(112, 24)
        Me.chkExchange.TabIndex = 143
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
        Me.chkFDFTKSetup.Location = New System.Drawing.Point(46, 283)
        Me.chkFDFTKSetup.Name = "chkFDFTKSetup"
        Me.chkFDFTKSetup.Size = New System.Drawing.Size(184, 24)
        Me.chkFDFTKSetup.TabIndex = 141
        Me.chkFDFTKSetup.Text = "FDFToolkit.net is Installed"
        Me.chkFDFTKSetup.UseVisualStyleBackColor = False
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(2, 439)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(744, 23)
        Me.ProgressBar1.TabIndex = 149
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.pnlDataSource)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(736, 401)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "1) DataSources"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'pnlDataSource
        '
        Me.pnlDataSource.BackColor = System.Drawing.Color.Transparent
        Me.pnlDataSource.Controls.Add(Me.Label15)
        Me.pnlDataSource.Controls.Add(Me.Label14)
        Me.pnlDataSource.Controls.Add(Me.Label13)
        Me.pnlDataSource.Controls.Add(Me.Label12)
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
        Me.pnlDataSource.Size = New System.Drawing.Size(720, 379)
        Me.pnlDataSource.TabIndex = 93
        '
        'Label15
        '
        Me.Label15.Location = New System.Drawing.Point(16, 160)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(100, 16)
        Me.Label15.TabIndex = 94
        Me.Label15.Text = "Label15"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(16, 136)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(100, 16)
        Me.Label14.TabIndex = 93
        Me.Label14.Text = "Label14"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(16, 112)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(100, 16)
        Me.Label13.TabIndex = 92
        Me.Label13.Text = "Label13"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(16, 88)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(100, 16)
        Me.Label12.TabIndex = 91
        Me.Label12.Text = "Label12"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblStatus
        '
        Me.lblStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblStatus.ForeColor = System.Drawing.Color.Lime
        Me.lblStatus.Location = New System.Drawing.Point(8, 312)
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
        Me.txtDataFields_1.Location = New System.Drawing.Point(120, 111)
        Me.txtDataFields_1.Name = "txtDataFields_1"
        Me.txtDataFields_1.Size = New System.Drawing.Size(354, 20)
        Me.txtDataFields_1.TabIndex = 81
        Me.txtDataFields_1.Text = "TextBox1"
        '
        'txtDataFields_2
        '
        Me.txtDataFields_2.BackColor = System.Drawing.Color.Black
        Me.txtDataFields_2.ForeColor = System.Drawing.Color.White
        Me.txtDataFields_2.Location = New System.Drawing.Point(120, 136)
        Me.txtDataFields_2.Name = "txtDataFields_2"
        Me.txtDataFields_2.Size = New System.Drawing.Size(354, 20)
        Me.txtDataFields_2.TabIndex = 83
        Me.txtDataFields_2.Text = "TextBox2"
        '
        'txtDataFields_0
        '
        Me.txtDataFields_0.BackColor = System.Drawing.Color.Black
        Me.txtDataFields_0.ForeColor = System.Drawing.Color.White
        Me.txtDataFields_0.Location = New System.Drawing.Point(120, 86)
        Me.txtDataFields_0.Name = "txtDataFields_0"
        Me.txtDataFields_0.Size = New System.Drawing.Size(354, 20)
        Me.txtDataFields_0.TabIndex = 82
        Me.txtDataFields_0.Text = "TextBox1"
        '
        'txtDataFields_3
        '
        Me.txtDataFields_3.BackColor = System.Drawing.Color.Black
        Me.txtDataFields_3.ForeColor = System.Drawing.Color.White
        Me.txtDataFields_3.Location = New System.Drawing.Point(120, 158)
        Me.txtDataFields_3.Name = "txtDataFields_3"
        Me.txtDataFields_3.Size = New System.Drawing.Size(354, 20)
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
        Me.lblConnected.Location = New System.Drawing.Point(120, 192)
        Me.lblConnected.Name = "lblConnected"
        Me.lblConnected.Size = New System.Drawing.Size(592, 72)
        Me.lblConnected.TabIndex = 90
        Me.lblConnected.Text = "Test: Not Connected"
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
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Button5)
        Me.TabPage1.Controls.Add(Me.Button4)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(736, 401)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Settings"
        Me.TabPage1.UseVisualStyleBackColor = True
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
        Me.btnNext.Location = New System.Drawing.Point(538, 71)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(202, 64)
        Me.btnNext.TabIndex = 145
        Me.btnNext.UseVisualStyleBackColor = False
        Me.btnNext.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage6)
        Me.TabControl1.Location = New System.Drawing.Point(2, 8)
        Me.TabControl1.Multiline = True
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(744, 427)
        Me.TabControl1.TabIndex = 150
        '
        'TabPage6
        '
        Me.TabPage6.Controls.Add(Me.GroupBoxNavigate)
        Me.TabPage6.Location = New System.Drawing.Point(4, 22)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Size = New System.Drawing.Size(736, 401)
        Me.TabPage6.TabIndex = 5
        Me.TabPage6.Text = "4) Records"
        Me.TabPage6.UseVisualStyleBackColor = True
        '
        'GroupBoxNavigate
        '
        Me.GroupBoxNavigate.Controls.Add(Me.btnRecordNav_NewRecord)
        Me.GroupBoxNavigate.Controls.Add(Me.btnRecordNav_DelRecord)
        Me.GroupBoxNavigate.Controls.Add(Me.Button2)
        Me.GroupBoxNavigate.Controls.Add(Me.btnUpdateDatabase)
        Me.GroupBoxNavigate.Controls.Add(Me.btnMinimize)
        Me.GroupBoxNavigate.Controls.Add(Me.btnRecordNav_First)
        Me.GroupBoxNavigate.Controls.Add(Me.btnRecordNav_Last)
        Me.GroupBoxNavigate.Controls.Add(Me.btnRecordNav_Previous)
        Me.GroupBoxNavigate.Controls.Add(Me.btnRecordNav_Next)
        Me.GroupBoxNavigate.Controls.Add(Me.ddlRecord)
        Me.GroupBoxNavigate.Location = New System.Drawing.Point(8, 16)
        Me.GroupBoxNavigate.Name = "GroupBoxNavigate"
        Me.GroupBoxNavigate.Size = New System.Drawing.Size(362, 150)
        Me.GroupBoxNavigate.TabIndex = 54
        Me.GroupBoxNavigate.TabStop = False
        Me.GroupBoxNavigate.Text = "Navigate Records"
        '
        'btnRecordNav_NewRecord
        '
        Me.btnRecordNav_NewRecord.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnRecordNav_NewRecord.Font = New System.Drawing.Font("Microsoft Sans Serif", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRecordNav_NewRecord.ForeColor = System.Drawing.Color.Black
        Me.btnRecordNav_NewRecord.Location = New System.Drawing.Point(291, 17)
        Me.btnRecordNav_NewRecord.Margin = New System.Windows.Forms.Padding(1)
        Me.btnRecordNav_NewRecord.Name = "btnRecordNav_NewRecord"
        Me.btnRecordNav_NewRecord.Size = New System.Drawing.Size(32, 32)
        Me.btnRecordNav_NewRecord.TabIndex = 971
        Me.btnRecordNav_NewRecord.Text = "*"
        Me.btnRecordNav_NewRecord.UseVisualStyleBackColor = False
        '
        'btnRecordNav_DelRecord
        '
        Me.btnRecordNav_DelRecord.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnRecordNav_DelRecord.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRecordNav_DelRecord.ForeColor = System.Drawing.Color.Black
        Me.btnRecordNav_DelRecord.Location = New System.Drawing.Point(325, 17)
        Me.btnRecordNav_DelRecord.Margin = New System.Windows.Forms.Padding(1)
        Me.btnRecordNav_DelRecord.Name = "btnRecordNav_DelRecord"
        Me.btnRecordNav_DelRecord.Size = New System.Drawing.Size(32, 32)
        Me.btnRecordNav_DelRecord.TabIndex = 972
        Me.btnRecordNav_DelRecord.Text = "del"
        Me.btnRecordNav_DelRecord.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Button2.Location = New System.Drawing.Point(189, 80)
        Me.Button2.Margin = New System.Windows.Forms.Padding(1)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(168, 32)
        Me.Button2.TabIndex = 56
        Me.Button2.Text = "Edit"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'btnUpdateDatabase
        '
        Me.btnUpdateDatabase.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnUpdateDatabase.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdateDatabase.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnUpdateDatabase.Location = New System.Drawing.Point(8, 80)
        Me.btnUpdateDatabase.Margin = New System.Windows.Forms.Padding(1)
        Me.btnUpdateDatabase.Name = "btnUpdateDatabase"
        Me.btnUpdateDatabase.Size = New System.Drawing.Size(168, 32)
        Me.btnUpdateDatabase.TabIndex = 55
        Me.btnUpdateDatabase.Text = "Update DataSource"
        Me.btnUpdateDatabase.UseVisualStyleBackColor = False
        '
        'btnMinimize
        '
        Me.btnMinimize.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnMinimize.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMinimize.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnMinimize.Location = New System.Drawing.Point(8, 48)
        Me.btnMinimize.Margin = New System.Windows.Forms.Padding(1)
        Me.btnMinimize.Name = "btnMinimize"
        Me.btnMinimize.Size = New System.Drawing.Size(349, 32)
        Me.btnMinimize.TabIndex = 54
        Me.btnMinimize.Text = "Compact View"
        Me.btnMinimize.UseVisualStyleBackColor = False
        '
        'btnRecordNav_First
        '
        Me.btnRecordNav_First.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnRecordNav_First.ForeColor = System.Drawing.Color.Black
        Me.btnRecordNav_First.Location = New System.Drawing.Point(8, 16)
        Me.btnRecordNav_First.Margin = New System.Windows.Forms.Padding(1)
        Me.btnRecordNav_First.Name = "btnRecordNav_First"
        Me.btnRecordNav_First.Size = New System.Drawing.Size(35, 32)
        Me.btnRecordNav_First.TabIndex = 49
        Me.btnRecordNav_First.Text = "|<<"
        Me.btnRecordNav_First.UseVisualStyleBackColor = False
        '
        'btnRecordNav_Last
        '
        Me.btnRecordNav_Last.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnRecordNav_Last.ForeColor = System.Drawing.Color.Black
        Me.btnRecordNav_Last.Location = New System.Drawing.Point(254, 17)
        Me.btnRecordNav_Last.Margin = New System.Windows.Forms.Padding(1)
        Me.btnRecordNav_Last.Name = "btnRecordNav_Last"
        Me.btnRecordNav_Last.Size = New System.Drawing.Size(32, 32)
        Me.btnRecordNav_Last.TabIndex = 53
        Me.btnRecordNav_Last.Text = ">>|"
        Me.btnRecordNav_Last.UseVisualStyleBackColor = False
        '
        'btnRecordNav_Previous
        '
        Me.btnRecordNav_Previous.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnRecordNav_Previous.ForeColor = System.Drawing.Color.Black
        Me.btnRecordNav_Previous.Location = New System.Drawing.Point(45, 16)
        Me.btnRecordNav_Previous.Margin = New System.Windows.Forms.Padding(1)
        Me.btnRecordNav_Previous.Name = "btnRecordNav_Previous"
        Me.btnRecordNav_Previous.Size = New System.Drawing.Size(35, 32)
        Me.btnRecordNav_Previous.TabIndex = 50
        Me.btnRecordNav_Previous.Text = "&<"
        Me.btnRecordNav_Previous.UseVisualStyleBackColor = False
        '
        'btnRecordNav_Next
        '
        Me.btnRecordNav_Next.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnRecordNav_Next.ForeColor = System.Drawing.Color.Black
        Me.btnRecordNav_Next.Location = New System.Drawing.Point(220, 17)
        Me.btnRecordNav_Next.Margin = New System.Windows.Forms.Padding(1)
        Me.btnRecordNav_Next.Name = "btnRecordNav_Next"
        Me.btnRecordNav_Next.Size = New System.Drawing.Size(32, 32)
        Me.btnRecordNav_Next.TabIndex = 52
        Me.btnRecordNav_Next.Text = "&>"
        Me.btnRecordNav_Next.UseVisualStyleBackColor = False
        '
        'ddlRecord
        '
        Me.ddlRecord.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlRecord.Location = New System.Drawing.Point(83, 17)
        Me.ddlRecord.Margin = New System.Windows.Forms.Padding(2)
        Me.ddlRecord.Name = "ddlRecord"
        Me.ddlRecord.Size = New System.Drawing.Size(133, 28)
        Me.ddlRecord.TabIndex = 51
        '
        'SaveFileSettings
        '
        Me.SaveFileSettings.DefaultExt = "xml"
        Me.SaveFileSettings.FileName = "settings-merge.xml"
        Me.SaveFileSettings.Filter = "XML Settings | *.xml"
        Me.SaveFileSettings.Title = "Save Settings"
        '
        'OpenFileSettings
        '
        Me.OpenFileSettings.DefaultExt = "xml"
        Me.OpenFileSettings.FileName = "settings-merge.xml"
        Me.OpenFileSettings.Filter = "XML Settings | *.xml"
        Me.OpenFileSettings.Title = "Load Settings"
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
        Me.Button1.Location = New System.Drawing.Point(28, 325)
        Me.Button1.Margin = New System.Windows.Forms.Padding(0)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(179, 46)
        Me.Button1.TabIndex = 146
        Me.Button1.Text = "Data Sources"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage
        Me.Button1.UseCompatibleTextRendering = True
        Me.Button1.UseMnemonic = False
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label23
        '
        Me.Label23.ForeColor = System.Drawing.Color.Black
        Me.Label23.Location = New System.Drawing.Point(31, 266)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(100, 23)
        Me.Label23.TabIndex = 144
        Me.Label23.Text = "Server Settings"
        '
        'dialogDataSource
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(750, 517)
        Me.Controls.Add(Me.lblMessage)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.chkExchange)
        Me.Controls.Add(Me.chkFDFTKSetup)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dialogDataSource"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Data Source"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.pnlFields.ResumeLayout(False)
        Me.pnlFields.PerformLayout()
        Me.TabPage4.ResumeLayout(False)
        Me.pnlFilterPrint.ResumeLayout(False)
        Me.pnlFilterPrint.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.pnlDataSource.ResumeLayout(False)
        Me.pnlDataSource.PerformLayout()
        Me.TabPage1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage6.ResumeLayout(False)
        Me.GroupBoxNavigate.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents cmnDlg As System.Windows.Forms.OpenFileDialog
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents pnlFields As System.Windows.Forms.Panel
    Friend WithEvents txtMappingRaw As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents lstDBFields As System.Windows.Forms.CheckedListBox
    Friend WithEvents lnkLoadPDF As System.Windows.Forms.LinkLabel
    Friend WithEvents btnLoadPDF As System.Windows.Forms.Button
    Friend WithEvents lnkAddNew As System.Windows.Forms.LinkLabel
    Public WithEvents cmbDBTables As System.Windows.Forms.ComboBox
    Public WithEvents cmbPDFFields As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lnkRemove As System.Windows.Forms.LinkLabel
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents pnlFilterPrint As System.Windows.Forms.Panel
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
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents lblMessage As System.Windows.Forms.Label
    Friend WithEvents chkExchange As System.Windows.Forms.CheckBox
    Friend WithEvents chkFDFTKSetup As System.Windows.Forms.CheckBox
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents pnlDataSource As System.Windows.Forms.Panel
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents btnSelectSource As System.Windows.Forms.Button
    Friend WithEvents txtDataFields_1 As System.Windows.Forms.TextBox
    Friend WithEvents txtDataFields_2 As System.Windows.Forms.TextBox
    Friend WithEvents txtDataFields_0 As System.Windows.Forms.TextBox
    Friend WithEvents txtDataFields_3 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblConnected As System.Windows.Forms.Label
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents fldOutput As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents SaveFileSettings As System.Windows.Forms.SaveFileDialog
    Friend WithEvents OpenFileSettings As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
    Public WithEvents btnRecordNav_First As System.Windows.Forms.Button
    Public WithEvents btnRecordNav_Previous As System.Windows.Forms.Button
    Public WithEvents ddlRecord As System.Windows.Forms.ComboBox
    Public WithEvents btnRecordNav_Next As System.Windows.Forms.Button
    Public WithEvents btnRecordNav_Last As System.Windows.Forms.Button
    Friend WithEvents GroupBoxNavigate As System.Windows.Forms.GroupBox
    Public WithEvents btnMinimize As System.Windows.Forms.Button
    Public WithEvents lstMappedFields As System.Windows.Forms.ListBox
    Public WithEvents btnUpdateDatabase As System.Windows.Forms.Button
    Public WithEvents Button2 As System.Windows.Forms.Button
    Public WithEvents btnRecordNav_NewRecord As Button
    Public WithEvents btnRecordNav_DelRecord As Button
End Class
