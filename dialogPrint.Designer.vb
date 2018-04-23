<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dialogPrint
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dialogPrint))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnPrinterProp1 = New System.Windows.Forms.Button()
        Me.CommentsAndFormsCombo = New System.Windows.Forms.ComboBox()
        Me.btnPrinterProperties = New System.Windows.Forms.Button()
        Me.lblPrinterType = New System.Windows.Forms.Label()
        Me.lblPrinterStatus = New System.Windows.Forms.Label()
        Me.comboInstalledPrinters = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.PrintRangeCkReversePages = New System.Windows.Forms.CheckBox()
        Me.PrintRangeTxtSubset = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.PrintRangeTxtRange = New System.Windows.Forms.TextBox()
        Me.PrintRangeOptPageRange = New System.Windows.Forms.RadioButton()
        Me.PrintRangeOptCurrentPage = New System.Windows.Forms.RadioButton()
        Me.PrintRangeOptCurrentView = New System.Windows.Forms.RadioButton()
        Me.PrintRangeOptAll = New System.Windows.Forms.RadioButton()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.PageHandlingCmbPageScaling = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.PageHandlingChkCollate = New System.Windows.Forms.CheckBox()
        Me.PageHandlingUpDownCopies = New System.Windows.Forms.NumericUpDown()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.pnlPrintableArea = New System.Windows.Forms.Panel()
        Me.PrintableAreaAutoRotateAndCenterCheckBox = New System.Windows.Forms.CheckBox()
        Me.PrintableAreaChoosePaperSourceByPdfPageSizeCheckbox = New System.Windows.Forms.CheckBox()
        Me.pnlBooklet = New System.Windows.Forms.Panel()
        Me.BookletBindingCombobox = New System.Windows.Forms.ComboBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.BookletAutoRotatePagesCheckbox = New System.Windows.Forms.CheckBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.BookletSheetsToTxt = New System.Windows.Forms.TextBox()
        Me.BookletSheetsFromTxt = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.BookletSubsetCombobox = New System.Windows.Forms.ComboBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.pnlTile = New System.Windows.Forms.Panel()
        Me.TileLabelsCheckbox = New System.Windows.Forms.CheckBox()
        Me.TileCutMarksCheckbox = New System.Windows.Forms.CheckBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.TileOverlapInch = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.TileScalePercent = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.pnlMultiple = New System.Windows.Forms.Panel()
        Me.MultipleAutoRotatePagesChk = New System.Windows.Forms.CheckBox()
        Me.MultiplePrintPageBorderChk = New System.Windows.Forms.CheckBox()
        Me.MultiplePageOrderCombo = New System.Windows.Forms.ComboBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.MultiplePagesPerSheetByColumns = New System.Windows.Forms.TextBox()
        Me.MultiplePagesPerSheetCombo = New System.Windows.Forms.ComboBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.MultiplePagesPerSheetByRows = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.lblPageNumOfCount = New System.Windows.Forms.Label()
        Me.PreviewLblScale = New System.Windows.Forms.Label()
        Me.PreviewPictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PreviewLblUnits = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.PreviewTrackbarScale = New System.Windows.Forms.TrackBar()
        Me.PrintToFileChk = New System.Windows.Forms.CheckBox()
        Me.PrintColorAsBlackChk = New System.Windows.Forms.CheckBox()
        Me.groupboxPrinterInformation = New System.Windows.Forms.GroupBox()
        Me.txtPrinterInformation = New System.Windows.Forms.TextBox()
        Me.btnPrinterInformationClose = New System.Windows.Forms.Button()
        Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.PageHandlingUpDownCopies, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPrintableArea.SuspendLayout()
        Me.pnlBooklet.SuspendLayout()
        Me.pnlTile.SuspendLayout()
        Me.pnlMultiple.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.PreviewPictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PreviewTrackbarScale, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupboxPrinterInformation.SuspendLayout()
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
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(458, 529)
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
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnPrinterProp1)
        Me.GroupBox1.Controls.Add(Me.CommentsAndFormsCombo)
        Me.GroupBox1.Controls.Add(Me.btnPrinterProperties)
        Me.GroupBox1.Controls.Add(Me.lblPrinterType)
        Me.GroupBox1.Controls.Add(Me.lblPrinterStatus)
        Me.GroupBox1.Controls.Add(Me.comboInstalledPrinters)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(611, 107)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Printer"
        '
        'btnPrinterProp1
        '
        Me.btnPrinterProp1.Location = New System.Drawing.Point(484, 13)
        Me.btnPrinterProp1.Name = "btnPrinterProp1"
        Me.btnPrinterProp1.Size = New System.Drawing.Size(44, 23)
        Me.btnPrinterProp1.TabIndex = 9
        Me.btnPrinterProp1.Text = "?"
        Me.btnPrinterProp1.UseVisualStyleBackColor = True
        '
        'CommentsAndFormsCombo
        '
        Me.CommentsAndFormsCombo.FormattingEnabled = True
        Me.CommentsAndFormsCombo.Items.AddRange(New Object() {"Document", "Document and Markups", "Documents and Stamps", "Form fields only"})
        Me.CommentsAndFormsCombo.Location = New System.Drawing.Point(308, 58)
        Me.CommentsAndFormsCombo.Name = "CommentsAndFormsCombo"
        Me.CommentsAndFormsCombo.Size = New System.Drawing.Size(293, 21)
        Me.CommentsAndFormsCombo.TabIndex = 8
        '
        'btnPrinterProperties
        '
        Me.btnPrinterProperties.Location = New System.Drawing.Point(381, 13)
        Me.btnPrinterProperties.Name = "btnPrinterProperties"
        Me.btnPrinterProperties.Size = New System.Drawing.Size(97, 23)
        Me.btnPrinterProperties.TabIndex = 7
        Me.btnPrinterProperties.Text = "Properties"
        Me.btnPrinterProperties.UseVisualStyleBackColor = True
        '
        'lblPrinterType
        '
        Me.lblPrinterType.AutoSize = True
        Me.lblPrinterType.Location = New System.Drawing.Point(58, 66)
        Me.lblPrinterType.Name = "lblPrinterType"
        Me.lblPrinterType.Size = New System.Drawing.Size(39, 13)
        Me.lblPrinterType.TabIndex = 6
        Me.lblPrinterType.Text = "Label6"
        '
        'lblPrinterStatus
        '
        Me.lblPrinterStatus.AutoSize = True
        Me.lblPrinterStatus.Location = New System.Drawing.Point(58, 42)
        Me.lblPrinterStatus.Name = "lblPrinterStatus"
        Me.lblPrinterStatus.Size = New System.Drawing.Size(39, 13)
        Me.lblPrinterStatus.TabIndex = 5
        Me.lblPrinterStatus.Text = "Label5"
        '
        'comboInstalledPrinters
        '
        Me.comboInstalledPrinters.FormattingEnabled = True
        Me.comboInstalledPrinters.Location = New System.Drawing.Point(56, 13)
        Me.comboInstalledPrinters.Name = "comboInstalledPrinters"
        Me.comboInstalledPrinters.Size = New System.Drawing.Size(319, 21)
        Me.comboInstalledPrinters.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(305, 42)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(111, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Comments and Forms:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 66)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(34, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Type:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Status:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Name:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.PrintRangeCkReversePages)
        Me.GroupBox2.Controls.Add(Me.PrintRangeTxtSubset)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.PrintRangeTxtRange)
        Me.GroupBox2.Controls.Add(Me.PrintRangeOptPageRange)
        Me.GroupBox2.Controls.Add(Me.PrintRangeOptCurrentPage)
        Me.GroupBox2.Controls.Add(Me.PrintRangeOptCurrentView)
        Me.GroupBox2.Controls.Add(Me.PrintRangeOptAll)
        Me.GroupBox2.Location = New System.Drawing.Point(0, 113)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(303, 188)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Print Range"
        '
        'PrintRangeCkReversePages
        '
        Me.PrintRangeCkReversePages.AutoSize = True
        Me.PrintRangeCkReversePages.Location = New System.Drawing.Point(16, 152)
        Me.PrintRangeCkReversePages.Name = "PrintRangeCkReversePages"
        Me.PrintRangeCkReversePages.Size = New System.Drawing.Size(99, 17)
        Me.PrintRangeCkReversePages.TabIndex = 11
        Me.PrintRangeCkReversePages.Text = "Reverse Pages"
        Me.PrintRangeCkReversePages.UseVisualStyleBackColor = True
        '
        'PrintRangeTxtSubset
        '
        Me.PrintRangeTxtSubset.FormattingEnabled = True
        Me.PrintRangeTxtSubset.Items.AddRange(New Object() {"All pages in range", "Odd pages only", "Even pages only"})
        Me.PrintRangeTxtSubset.Location = New System.Drawing.Point(75, 116)
        Me.PrintRangeTxtSubset.Name = "PrintRangeTxtSubset"
        Me.PrintRangeTxtSubset.Size = New System.Drawing.Size(222, 21)
        Me.PrintRangeTxtSubset.TabIndex = 10
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(26, 119)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(43, 13)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "Subset:"
        '
        'PrintRangeTxtRange
        '
        Me.PrintRangeTxtRange.Location = New System.Drawing.Point(75, 88)
        Me.PrintRangeTxtRange.Name = "PrintRangeTxtRange"
        Me.PrintRangeTxtRange.Size = New System.Drawing.Size(222, 20)
        Me.PrintRangeTxtRange.TabIndex = 4
        '
        'PrintRangeOptPageRange
        '
        Me.PrintRangeOptPageRange.AutoSize = True
        Me.PrintRangeOptPageRange.Location = New System.Drawing.Point(15, 88)
        Me.PrintRangeOptPageRange.Name = "PrintRangeOptPageRange"
        Me.PrintRangeOptPageRange.Size = New System.Drawing.Size(58, 17)
        Me.PrintRangeOptPageRange.TabIndex = 3
        Me.PrintRangeOptPageRange.TabStop = True
        Me.PrintRangeOptPageRange.Text = "Pages:"
        Me.PrintRangeOptPageRange.UseVisualStyleBackColor = True
        '
        'PrintRangeOptCurrentPage
        '
        Me.PrintRangeOptCurrentPage.AutoSize = True
        Me.PrintRangeOptCurrentPage.Location = New System.Drawing.Point(15, 65)
        Me.PrintRangeOptCurrentPage.Name = "PrintRangeOptCurrentPage"
        Me.PrintRangeOptCurrentPage.Size = New System.Drawing.Size(87, 17)
        Me.PrintRangeOptCurrentPage.TabIndex = 2
        Me.PrintRangeOptCurrentPage.TabStop = True
        Me.PrintRangeOptCurrentPage.Text = "Current Page"
        Me.PrintRangeOptCurrentPage.UseVisualStyleBackColor = True
        '
        'PrintRangeOptCurrentView
        '
        Me.PrintRangeOptCurrentView.AutoSize = True
        Me.PrintRangeOptCurrentView.Location = New System.Drawing.Point(15, 42)
        Me.PrintRangeOptCurrentView.Name = "PrintRangeOptCurrentView"
        Me.PrintRangeOptCurrentView.Size = New System.Drawing.Size(88, 17)
        Me.PrintRangeOptCurrentView.TabIndex = 1
        Me.PrintRangeOptCurrentView.TabStop = True
        Me.PrintRangeOptCurrentView.Text = "Currrent View"
        Me.PrintRangeOptCurrentView.UseVisualStyleBackColor = True
        '
        'PrintRangeOptAll
        '
        Me.PrintRangeOptAll.AutoSize = True
        Me.PrintRangeOptAll.Checked = True
        Me.PrintRangeOptAll.Location = New System.Drawing.Point(15, 19)
        Me.PrintRangeOptAll.Name = "PrintRangeOptAll"
        Me.PrintRangeOptAll.Size = New System.Drawing.Size(36, 17)
        Me.PrintRangeOptAll.TabIndex = 0
        Me.PrintRangeOptAll.TabStop = True
        Me.PrintRangeOptAll.Text = "All"
        Me.PrintRangeOptAll.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.PageHandlingCmbPageScaling)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.PageHandlingChkCollate)
        Me.GroupBox3.Controls.Add(Me.PageHandlingUpDownCopies)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.pnlPrintableArea)
        Me.GroupBox3.Controls.Add(Me.pnlBooklet)
        Me.GroupBox3.Controls.Add(Me.pnlTile)
        Me.GroupBox3.Controls.Add(Me.pnlMultiple)
        Me.GroupBox3.Location = New System.Drawing.Point(2, 304)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(303, 188)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Page Handling"
        '
        'PageHandlingCmbPageScaling
        '
        Me.PageHandlingCmbPageScaling.FormattingEnabled = True
        Me.PageHandlingCmbPageScaling.Items.AddRange(New Object() {"none", "Fit to Printable Area", "Shrink to Printable Area", "Tile large pages", "Tile all pages", "Multiple pages per sheet", "Booklet Printing"})
        Me.PageHandlingCmbPageScaling.Location = New System.Drawing.Point(90, 57)
        Me.PageHandlingCmbPageScaling.Name = "PageHandlingCmbPageScaling"
        Me.PageHandlingCmbPageScaling.Size = New System.Drawing.Size(207, 21)
        Me.PageHandlingCmbPageScaling.TabIndex = 16
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(13, 60)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(71, 13)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "Page scaling:"
        '
        'PageHandlingChkCollate
        '
        Me.PageHandlingChkCollate.AutoSize = True
        Me.PageHandlingChkCollate.Location = New System.Drawing.Point(166, 28)
        Me.PageHandlingChkCollate.Name = "PageHandlingChkCollate"
        Me.PageHandlingChkCollate.Size = New System.Drawing.Size(58, 17)
        Me.PageHandlingChkCollate.TabIndex = 14
        Me.PageHandlingChkCollate.Text = "Collate"
        Me.PageHandlingChkCollate.UseVisualStyleBackColor = True
        '
        'PageHandlingUpDownCopies
        '
        Me.PageHandlingUpDownCopies.Location = New System.Drawing.Point(56, 26)
        Me.PageHandlingUpDownCopies.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.PageHandlingUpDownCopies.Name = "PageHandlingUpDownCopies"
        Me.PageHandlingUpDownCopies.Size = New System.Drawing.Size(104, 20)
        Me.PageHandlingUpDownCopies.TabIndex = 13
        Me.PageHandlingUpDownCopies.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(13, 28)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(42, 13)
        Me.Label8.TabIndex = 12
        Me.Label8.Text = "Copies:"
        '
        'pnlPrintableArea
        '
        Me.pnlPrintableArea.Controls.Add(Me.PrintableAreaAutoRotateAndCenterCheckBox)
        Me.pnlPrintableArea.Controls.Add(Me.PrintableAreaChoosePaperSourceByPdfPageSizeCheckbox)
        Me.pnlPrintableArea.Location = New System.Drawing.Point(8, 88)
        Me.pnlPrintableArea.Name = "pnlPrintableArea"
        Me.pnlPrintableArea.Size = New System.Drawing.Size(295, 99)
        Me.pnlPrintableArea.TabIndex = 12
        '
        'PrintableAreaAutoRotateAndCenterCheckBox
        '
        Me.PrintableAreaAutoRotateAndCenterCheckBox.AutoSize = True
        Me.PrintableAreaAutoRotateAndCenterCheckBox.Location = New System.Drawing.Point(17, 20)
        Me.PrintableAreaAutoRotateAndCenterCheckBox.Name = "PrintableAreaAutoRotateAndCenterCheckBox"
        Me.PrintableAreaAutoRotateAndCenterCheckBox.Size = New System.Drawing.Size(132, 17)
        Me.PrintableAreaAutoRotateAndCenterCheckBox.TabIndex = 17
        Me.PrintableAreaAutoRotateAndCenterCheckBox.Text = "Auto-rotate and center"
        Me.PrintableAreaAutoRotateAndCenterCheckBox.UseVisualStyleBackColor = True
        '
        'PrintableAreaChoosePaperSourceByPdfPageSizeCheckbox
        '
        Me.PrintableAreaChoosePaperSourceByPdfPageSizeCheckbox.AutoSize = True
        Me.PrintableAreaChoosePaperSourceByPdfPageSizeCheckbox.Location = New System.Drawing.Point(17, 48)
        Me.PrintableAreaChoosePaperSourceByPdfPageSizeCheckbox.Name = "PrintableAreaChoosePaperSourceByPdfPageSizeCheckbox"
        Me.PrintableAreaChoosePaperSourceByPdfPageSizeCheckbox.Size = New System.Drawing.Size(213, 17)
        Me.PrintableAreaChoosePaperSourceByPdfPageSizeCheckbox.TabIndex = 18
        Me.PrintableAreaChoosePaperSourceByPdfPageSizeCheckbox.Text = "Choose paper source by PDF page size"
        Me.PrintableAreaChoosePaperSourceByPdfPageSizeCheckbox.UseVisualStyleBackColor = True
        '
        'pnlBooklet
        '
        Me.pnlBooklet.Controls.Add(Me.BookletBindingCombobox)
        Me.pnlBooklet.Controls.Add(Me.Label23)
        Me.pnlBooklet.Controls.Add(Me.BookletAutoRotatePagesCheckbox)
        Me.pnlBooklet.Controls.Add(Me.Label22)
        Me.pnlBooklet.Controls.Add(Me.BookletSheetsToTxt)
        Me.pnlBooklet.Controls.Add(Me.BookletSheetsFromTxt)
        Me.pnlBooklet.Controls.Add(Me.Label21)
        Me.pnlBooklet.Controls.Add(Me.BookletSubsetCombobox)
        Me.pnlBooklet.Controls.Add(Me.Label20)
        Me.pnlBooklet.Location = New System.Drawing.Point(10, 90)
        Me.pnlBooklet.Name = "pnlBooklet"
        Me.pnlBooklet.Size = New System.Drawing.Size(292, 97)
        Me.pnlBooklet.TabIndex = 21
        '
        'BookletBindingCombobox
        '
        Me.BookletBindingCombobox.FormattingEnabled = True
        Me.BookletBindingCombobox.Items.AddRange(New Object() {"Left", "Right", "Left [Tall]", "Right [Tall]"})
        Me.BookletBindingCombobox.Location = New System.Drawing.Point(192, 67)
        Me.BookletBindingCombobox.Name = "BookletBindingCombobox"
        Me.BookletBindingCombobox.Size = New System.Drawing.Size(97, 21)
        Me.BookletBindingCombobox.TabIndex = 24
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(148, 70)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(45, 13)
        Me.Label23.TabIndex = 23
        Me.Label23.Text = "Binding:"
        '
        'BookletAutoRotatePagesCheckbox
        '
        Me.BookletAutoRotatePagesCheckbox.AutoSize = True
        Me.BookletAutoRotatePagesCheckbox.Location = New System.Drawing.Point(24, 69)
        Me.BookletAutoRotatePagesCheckbox.Name = "BookletAutoRotatePagesCheckbox"
        Me.BookletAutoRotatePagesCheckbox.Size = New System.Drawing.Size(116, 17)
        Me.BookletAutoRotatePagesCheckbox.TabIndex = 22
        Me.BookletAutoRotatePagesCheckbox.Text = "Auto-Rotate Pages"
        Me.BookletAutoRotatePagesCheckbox.UseVisualStyleBackColor = True
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(189, 39)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(16, 13)
        Me.Label22.TabIndex = 21
        Me.Label22.Text = "to"
        '
        'BookletSheetsToTxt
        '
        Me.BookletSheetsToTxt.Location = New System.Drawing.Point(215, 36)
        Me.BookletSheetsToTxt.Name = "BookletSheetsToTxt"
        Me.BookletSheetsToTxt.Size = New System.Drawing.Size(74, 20)
        Me.BookletSheetsToTxt.TabIndex = 20
        '
        'BookletSheetsFromTxt
        '
        Me.BookletSheetsFromTxt.Location = New System.Drawing.Point(105, 36)
        Me.BookletSheetsFromTxt.Name = "BookletSheetsFromTxt"
        Me.BookletSheetsFromTxt.Size = New System.Drawing.Size(74, 20)
        Me.BookletSheetsFromTxt.TabIndex = 19
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(34, 39)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(63, 13)
        Me.Label21.TabIndex = 18
        Me.Label21.Text = "Sheets from"
        '
        'BookletSubsetCombobox
        '
        Me.BookletSubsetCombobox.FormattingEnabled = True
        Me.BookletSubsetCombobox.Items.AddRange(New Object() {"Both sides", "Front side only", "Back side only"})
        Me.BookletSubsetCombobox.Location = New System.Drawing.Point(105, 9)
        Me.BookletSubsetCombobox.Name = "BookletSubsetCombobox"
        Me.BookletSubsetCombobox.Size = New System.Drawing.Size(184, 21)
        Me.BookletSubsetCombobox.TabIndex = 17
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(17, 12)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(82, 13)
        Me.Label20.TabIndex = 10
        Me.Label20.Text = "Booklet Subset:"
        '
        'pnlTile
        '
        Me.pnlTile.Controls.Add(Me.TileLabelsCheckbox)
        Me.pnlTile.Controls.Add(Me.TileCutMarksCheckbox)
        Me.pnlTile.Controls.Add(Me.Label16)
        Me.pnlTile.Controls.Add(Me.TileOverlapInch)
        Me.pnlTile.Controls.Add(Me.Label15)
        Me.pnlTile.Controls.Add(Me.TileScalePercent)
        Me.pnlTile.Controls.Add(Me.Label5)
        Me.pnlTile.Controls.Add(Me.Label6)
        Me.pnlTile.Location = New System.Drawing.Point(10, 88)
        Me.pnlTile.Name = "pnlTile"
        Me.pnlTile.Size = New System.Drawing.Size(291, 97)
        Me.pnlTile.TabIndex = 19
        '
        'TileLabelsCheckbox
        '
        Me.TileLabelsCheckbox.AutoSize = True
        Me.TileLabelsCheckbox.Location = New System.Drawing.Point(173, 66)
        Me.TileLabelsCheckbox.Name = "TileLabelsCheckbox"
        Me.TileLabelsCheckbox.Size = New System.Drawing.Size(57, 17)
        Me.TileLabelsCheckbox.TabIndex = 7
        Me.TileLabelsCheckbox.Text = "Labels"
        Me.TileLabelsCheckbox.UseVisualStyleBackColor = True
        '
        'TileCutMarksCheckbox
        '
        Me.TileCutMarksCheckbox.AutoSize = True
        Me.TileCutMarksCheckbox.Location = New System.Drawing.Point(78, 66)
        Me.TileCutMarksCheckbox.Name = "TileCutMarksCheckbox"
        Me.TileCutMarksCheckbox.Size = New System.Drawing.Size(74, 17)
        Me.TileCutMarksCheckbox.TabIndex = 6
        Me.TileCutMarksCheckbox.Text = "Cut Marks"
        Me.TileCutMarksCheckbox.UseVisualStyleBackColor = True
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(256, 21)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(18, 13)
        Me.Label16.TabIndex = 5
        Me.Label16.Text = "in."
        '
        'TileOverlapInch
        '
        Me.TileOverlapInch.Location = New System.Drawing.Point(187, 17)
        Me.TileOverlapInch.Name = "TileOverlapInch"
        Me.TileOverlapInch.Size = New System.Drawing.Size(68, 20)
        Me.TileOverlapInch.TabIndex = 4
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(139, 21)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(47, 13)
        Me.Label15.TabIndex = 3
        Me.Label15.Text = "Overlap:"
        '
        'TileScalePercent
        '
        Me.TileScalePercent.Location = New System.Drawing.Point(77, 18)
        Me.TileScalePercent.Name = "TileScalePercent"
        Me.TileScalePercent.Size = New System.Drawing.Size(43, 20)
        Me.TileScalePercent.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(14, 20)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(57, 13)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Tile Scale:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(118, 20)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(15, 13)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "%"
        '
        'pnlMultiple
        '
        Me.pnlMultiple.Controls.Add(Me.MultipleAutoRotatePagesChk)
        Me.pnlMultiple.Controls.Add(Me.MultiplePrintPageBorderChk)
        Me.pnlMultiple.Controls.Add(Me.MultiplePageOrderCombo)
        Me.pnlMultiple.Controls.Add(Me.Label19)
        Me.pnlMultiple.Controls.Add(Me.MultiplePagesPerSheetByColumns)
        Me.pnlMultiple.Controls.Add(Me.MultiplePagesPerSheetCombo)
        Me.pnlMultiple.Controls.Add(Me.Label17)
        Me.pnlMultiple.Controls.Add(Me.MultiplePagesPerSheetByRows)
        Me.pnlMultiple.Controls.Add(Me.Label18)
        Me.pnlMultiple.Location = New System.Drawing.Point(8, 88)
        Me.pnlMultiple.Name = "pnlMultiple"
        Me.pnlMultiple.Size = New System.Drawing.Size(295, 99)
        Me.pnlMultiple.TabIndex = 20
        '
        'MultipleAutoRotatePagesChk
        '
        Me.MultipleAutoRotatePagesChk.AutoSize = True
        Me.MultipleAutoRotatePagesChk.Location = New System.Drawing.Point(165, 74)
        Me.MultipleAutoRotatePagesChk.Name = "MultipleAutoRotatePagesChk"
        Me.MultipleAutoRotatePagesChk.Size = New System.Drawing.Size(116, 17)
        Me.MultipleAutoRotatePagesChk.TabIndex = 30
        Me.MultipleAutoRotatePagesChk.Text = "Auto-Rotate Pages"
        Me.MultipleAutoRotatePagesChk.UseVisualStyleBackColor = True
        '
        'MultiplePrintPageBorderChk
        '
        Me.MultiplePrintPageBorderChk.AutoSize = True
        Me.MultiplePrintPageBorderChk.Location = New System.Drawing.Point(15, 74)
        Me.MultiplePrintPageBorderChk.Name = "MultiplePrintPageBorderChk"
        Me.MultiplePrintPageBorderChk.Size = New System.Drawing.Size(107, 17)
        Me.MultiplePrintPageBorderChk.TabIndex = 29
        Me.MultiplePrintPageBorderChk.Text = "Print page border"
        Me.MultiplePrintPageBorderChk.UseVisualStyleBackColor = True
        '
        'MultiplePageOrderCombo
        '
        Me.MultiplePageOrderCombo.FormattingEnabled = True
        Me.MultiplePageOrderCombo.Items.AddRange(New Object() {"Horizontal", "Horizontal Reversed", "Vertical", "Vertical Reversed"})
        Me.MultiplePageOrderCombo.Location = New System.Drawing.Point(82, 44)
        Me.MultiplePageOrderCombo.Name = "MultiplePageOrderCombo"
        Me.MultiplePageOrderCombo.Size = New System.Drawing.Size(148, 21)
        Me.MultiplePageOrderCombo.TabIndex = 28
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(12, 47)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(64, 13)
        Me.Label19.TabIndex = 27
        Me.Label19.Text = "Page Order:"
        '
        'MultiplePagesPerSheetByColumns
        '
        Me.MultiplePagesPerSheetByColumns.Enabled = False
        Me.MultiplePagesPerSheetByColumns.Location = New System.Drawing.Point(194, 15)
        Me.MultiplePagesPerSheetByColumns.Name = "MultiplePagesPerSheetByColumns"
        Me.MultiplePagesPerSheetByColumns.Size = New System.Drawing.Size(33, 20)
        Me.MultiplePagesPerSheetByColumns.TabIndex = 24
        '
        'MultiplePagesPerSheetCombo
        '
        Me.MultiplePagesPerSheetCombo.FormattingEnabled = True
        Me.MultiplePagesPerSheetCombo.Items.AddRange(New Object() {"2", "4", "6", "9", "16", "Custom..."})
        Me.MultiplePagesPerSheetCombo.Location = New System.Drawing.Point(96, 14)
        Me.MultiplePagesPerSheetCombo.Name = "MultiplePagesPerSheetCombo"
        Me.MultiplePagesPerSheetCombo.Size = New System.Drawing.Size(92, 21)
        Me.MultiplePagesPerSheetCombo.TabIndex = 23
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(12, 17)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(87, 13)
        Me.Label17.TabIndex = 22
        Me.Label17.Text = "Pages per sheet:"
        '
        'MultiplePagesPerSheetByRows
        '
        Me.MultiplePagesPerSheetByRows.Enabled = False
        Me.MultiplePagesPerSheetByRows.Location = New System.Drawing.Point(246, 15)
        Me.MultiplePagesPerSheetByRows.Name = "MultiplePagesPerSheetByRows"
        Me.MultiplePagesPerSheetByRows.Size = New System.Drawing.Size(33, 20)
        Me.MultiplePagesPerSheetByRows.TabIndex = 25
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(228, 17)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(18, 13)
        Me.Label18.TabIndex = 26
        Me.Label18.Text = "by"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.lblPageNumOfCount)
        Me.GroupBox4.Controls.Add(Me.PreviewLblScale)
        Me.GroupBox4.Controls.Add(Me.PreviewPictureBox1)
        Me.GroupBox4.Controls.Add(Me.PreviewLblUnits)
        Me.GroupBox4.Controls.Add(Me.Label12)
        Me.GroupBox4.Controls.Add(Me.Label13)
        Me.GroupBox4.Controls.Add(Me.PreviewTrackbarScale)
        Me.GroupBox4.Location = New System.Drawing.Point(303, 113)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(308, 379)
        Me.GroupBox4.TabIndex = 2
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Preview: Composite"
        '
        'lblPageNumOfCount
        '
        Me.lblPageNumOfCount.AutoSize = True
        Me.lblPageNumOfCount.Location = New System.Drawing.Point(8, 356)
        Me.lblPageNumOfCount.Name = "lblPageNumOfCount"
        Me.lblPageNumOfCount.Size = New System.Drawing.Size(39, 13)
        Me.lblPageNumOfCount.TabIndex = 13
        Me.lblPageNumOfCount.Text = "1/1 [1]"
        '
        'PreviewLblScale
        '
        Me.PreviewLblScale.AutoSize = True
        Me.PreviewLblScale.Location = New System.Drawing.Point(156, 298)
        Me.PreviewLblScale.Name = "PreviewLblScale"
        Me.PreviewLblScale.Size = New System.Drawing.Size(45, 13)
        Me.PreviewLblScale.TabIndex = 12
        Me.PreviewLblScale.Text = "Label10"
        '
        'PreviewPictureBox1
        '
        Me.PreviewPictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PreviewPictureBox1.Location = New System.Drawing.Point(8, 19)
        Me.PreviewPictureBox1.Name = "PreviewPictureBox1"
        Me.PreviewPictureBox1.Size = New System.Drawing.Size(195, 213)
        Me.PreviewPictureBox1.TabIndex = 5
        Me.PreviewPictureBox1.TabStop = False
        '
        'PreviewLblUnits
        '
        Me.PreviewLblUnits.AutoSize = True
        Me.PreviewLblUnits.Location = New System.Drawing.Point(39, 298)
        Me.PreviewLblUnits.Name = "PreviewLblUnits"
        Me.PreviewLblUnits.Size = New System.Drawing.Size(45, 13)
        Me.PreviewLblUnits.TabIndex = 11
        Me.PreviewLblUnits.Text = "Label11"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(113, 298)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(37, 13)
        Me.Label12.TabIndex = 10
        Me.Label12.Text = "Scale:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(4, 298)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(34, 13)
        Me.Label13.TabIndex = 9
        Me.Label13.Text = "Units:"
        '
        'PreviewTrackbarScale
        '
        Me.PreviewTrackbarScale.Location = New System.Drawing.Point(2, 317)
        Me.PreviewTrackbarScale.Minimum = 1
        Me.PreviewTrackbarScale.Name = "PreviewTrackbarScale"
        Me.PreviewTrackbarScale.Size = New System.Drawing.Size(300, 45)
        Me.PreviewTrackbarScale.TabIndex = 14
        Me.PreviewTrackbarScale.Value = 1
        '
        'PrintToFileChk
        '
        Me.PrintToFileChk.AutoSize = True
        Me.PrintToFileChk.Location = New System.Drawing.Point(16, 501)
        Me.PrintToFileChk.Name = "PrintToFileChk"
        Me.PrintToFileChk.Size = New System.Drawing.Size(75, 17)
        Me.PrintToFileChk.TabIndex = 3
        Me.PrintToFileChk.Text = "Print to file"
        Me.PrintToFileChk.UseVisualStyleBackColor = True
        '
        'PrintColorAsBlackChk
        '
        Me.PrintColorAsBlackChk.AutoSize = True
        Me.PrintColorAsBlackChk.Location = New System.Drawing.Point(16, 524)
        Me.PrintColorAsBlackChk.Name = "PrintColorAsBlackChk"
        Me.PrintColorAsBlackChk.Size = New System.Drawing.Size(116, 17)
        Me.PrintColorAsBlackChk.TabIndex = 4
        Me.PrintColorAsBlackChk.Text = "Print color as black"
        Me.PrintColorAsBlackChk.UseVisualStyleBackColor = True
        '
        'groupboxPrinterInformation
        '
        Me.groupboxPrinterInformation.Controls.Add(Me.txtPrinterInformation)
        Me.groupboxPrinterInformation.Controls.Add(Me.btnPrinterInformationClose)
        Me.groupboxPrinterInformation.Location = New System.Drawing.Point(0, 113)
        Me.groupboxPrinterInformation.Name = "groupboxPrinterInformation"
        Me.groupboxPrinterInformation.Size = New System.Drawing.Size(611, 455)
        Me.groupboxPrinterInformation.TabIndex = 10
        Me.groupboxPrinterInformation.TabStop = False
        Me.groupboxPrinterInformation.Text = "Printer Information"
        Me.groupboxPrinterInformation.Visible = False
        '
        'txtPrinterInformation
        '
        Me.txtPrinterInformation.Location = New System.Drawing.Point(12, 39)
        Me.txtPrinterInformation.Multiline = True
        Me.txtPrinterInformation.Name = "txtPrinterInformation"
        Me.txtPrinterInformation.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtPrinterInformation.Size = New System.Drawing.Size(593, 410)
        Me.txtPrinterInformation.TabIndex = 1
        '
        'btnPrinterInformationClose
        '
        Me.btnPrinterInformationClose.Location = New System.Drawing.Point(530, 13)
        Me.btnPrinterInformationClose.Name = "btnPrinterInformationClose"
        Me.btnPrinterInformationClose.Size = New System.Drawing.Size(75, 23)
        Me.btnPrinterInformationClose.TabIndex = 0
        Me.btnPrinterInformationClose.Text = "Hide"
        Me.btnPrinterInformationClose.UseVisualStyleBackColor = True
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'dialogPrint
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(616, 570)
        Me.Controls.Add(Me.PrintColorAsBlackChk)
        Me.Controls.Add(Me.PrintToFileChk)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.groupboxPrinterInformation)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dialogPrint"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Print"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.PageHandlingUpDownCopies, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPrintableArea.ResumeLayout(False)
        Me.pnlPrintableArea.PerformLayout()
        Me.pnlBooklet.ResumeLayout(False)
        Me.pnlBooklet.PerformLayout()
        Me.pnlTile.ResumeLayout(False)
        Me.pnlTile.PerformLayout()
        Me.pnlMultiple.ResumeLayout(False)
        Me.pnlMultiple.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.PreviewPictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PreviewTrackbarScale, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupboxPrinterInformation.ResumeLayout(False)
        Me.groupboxPrinterInformation.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents CommentsAndFormsCombo As ComboBox
    Friend WithEvents btnPrinterProperties As Button
    Friend WithEvents lblPrinterType As Label
    Friend WithEvents lblPrinterStatus As Label
    Friend WithEvents comboInstalledPrinters As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents PrintRangeCkReversePages As CheckBox
    Friend WithEvents PrintRangeTxtSubset As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents PrintRangeTxtRange As TextBox
    Friend WithEvents PrintRangeOptPageRange As RadioButton
    Friend WithEvents PrintRangeOptCurrentPage As RadioButton
    Friend WithEvents PrintRangeOptCurrentView As RadioButton
    Friend WithEvents PrintRangeOptAll As RadioButton
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents PrintableAreaChoosePaperSourceByPdfPageSizeCheckbox As CheckBox
    Friend WithEvents PrintableAreaAutoRotateAndCenterCheckBox As CheckBox
    Friend WithEvents PageHandlingCmbPageScaling As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents PageHandlingChkCollate As CheckBox
    Friend WithEvents PageHandlingUpDownCopies As NumericUpDown
    Friend WithEvents Label8 As Label
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents PreviewTrackbarScale As TrackBar
    Friend WithEvents lblPageNumOfCount As Label
    Friend WithEvents PreviewLblScale As Label
    Friend WithEvents PreviewPictureBox1 As PictureBox
    Friend WithEvents PreviewLblUnits As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents PrintToFileChk As CheckBox
    Friend WithEvents PrintColorAsBlackChk As CheckBox
    Friend WithEvents btnPrinterProp1 As Button
    Friend WithEvents groupboxPrinterInformation As GroupBox
    Friend WithEvents txtPrinterInformation As TextBox
    Friend WithEvents btnPrinterInformationClose As Button
    Friend WithEvents pnlPrintableArea As Panel
    Friend WithEvents pnlTile As Panel
    Friend WithEvents Label16 As Label
    Friend WithEvents TileOverlapInch As TextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents TileScalePercent As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TileLabelsCheckbox As CheckBox
    Friend WithEvents TileCutMarksCheckbox As CheckBox
    Friend WithEvents pnlMultiple As Panel
    Friend WithEvents Label19 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents MultiplePagesPerSheetByRows As TextBox
    Friend WithEvents MultiplePagesPerSheetByColumns As TextBox
    Friend WithEvents MultiplePagesPerSheetCombo As ComboBox
    Friend WithEvents Label17 As Label
    Friend WithEvents MultipleAutoRotatePagesChk As CheckBox
    Friend WithEvents MultiplePrintPageBorderChk As CheckBox
    Friend WithEvents MultiplePageOrderCombo As ComboBox
    Friend WithEvents pnlBooklet As Panel
    Friend WithEvents Label21 As Label
    Friend WithEvents BookletSubsetCombobox As ComboBox
    Friend WithEvents Label20 As Label
    Friend WithEvents BookletBindingCombobox As ComboBox
    Friend WithEvents Label23 As Label
    Friend WithEvents BookletAutoRotatePagesCheckbox As CheckBox
    Friend WithEvents Label22 As Label
    Friend WithEvents BookletSheetsToTxt As TextBox
    Friend WithEvents BookletSheetsFromTxt As TextBox
    Public WithEvents PageSetupDialog1 As PageSetupDialog
    Public WithEvents PrintDialog1 As PrintDialog
    Public WithEvents PrintDocument1 As Printing.PrintDocument
    Public WithEvents PrintPreviewDialog1 As PrintPreviewDialog
End Class
