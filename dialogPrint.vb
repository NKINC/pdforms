Imports System.Windows.Forms
Imports System.DirectoryServices
Imports System.Management
Imports iTextSharp.text.pdf
Public Class dialogPrint
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 Nicholas Kowalewicz All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>
    Public WithEvents docToPrint As New Printing.PrintDocument
    Private Const PRINTER_ENUM_DEFAULT = &H1
    Public frm As frmMain = Nothing
    Public pdfBytes() As Byte
    Public pdfReaderDocClone As PdfReader = Nothing
    Public r As PdfReader = Nothing
    Dim pageScale As Single = 0.0F
    Public WithEvents tmrScroller1 As New Timer
    Public tmrScroller1_Fired As Boolean = False
    Public Sub New()
        InitializeComponent()
    End Sub
    Private Sub PrintPageRangeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            PrintDialog1 = New PrintDialog
            pdfReaderDocClone = frm.pdfReaderDoc.Clone
            Dim readerTemp As PdfReader = pdfReaderDocClone.Clone
            PrintDialog1.AllowSomePages = True
            PrintDialog1.AllowCurrentPage = True
            PrintDialog1.AllowSelection = True
            PrintDialog1.AllowPrintToFile = False
            PrintDialog1.PrinterSettings.Collate = True
            Select Case PrintDialog1.ShowDialog(Me)
                Case Windows.Forms.DialogResult.OK, Windows.Forms.DialogResult.Yes
                    If Not pdfReaderDocClone Is Nothing Then
                        If pdfReaderDocClone.NumberOfPages > 0 Then
                            PrintDialog1.PrinterSettings.MinimumPage = 1
                            PrintDialog1.PrinterSettings.MaximumPage = pdfReaderDocClone.NumberOfPages
                            frm.StatusToolStrip = "Printing: please wait.."
                            Dim fnTempPrint As String = frm.ApplicationDataFolder(True, "temp") & "\print-" & System.IO.Path.GetFileName(frm.fpath & "").ToString().Replace(" ", "-").Replace("""", "").Replace("'"c, "") & ""
                            frm.LoadPDFReaderDoc(frm.pdfOwnerPassword, True)
                            r = pdfReaderDocClone.Clone
                            If r.NumberOfPages > 1 Then
                                Dim cdialog As New clsPromptDialog
                                Dim pgImportedStr As String = cdialog.ShowDialog("Enter page print range:", "Page Print Range:", Me, "1-" & r.NumberOfPages.ToString & "")
                                If Not String.IsNullOrEmpty(pgImportedStr & "") Then
                                    r.SelectPages(pgImportedStr.ToString & "")
                                End If
                            End If
                            Dim bPDF() As Byte = frm.getPDFBytes(r)
                            Dim exitCode As Integer = -1
                            Try
                                Me.SendToBack()
                                Dim PrinterName1 As String = PrintDialog1.PrinterSettings.PrinterName.ToString()
                                Dim numCopies As Integer = PrintDialog1.PrinterSettings.Copies
                                For copy As Integer = 1 To numCopies
                                    Dim i As Integer = -1
                                    Dim numPages As Integer = r.NumberOfPages
                                    Dim start As Integer = 1
                                    frm.printDocIndex = 0
                                    frm.printDocImageList = New List(Of System.Drawing.Image)
                                    frm.printDocHasMorePages = True
                                    frm.printDoc = New Printing.PrintDocument
                                    frm.printDoc.PrinterSettings.PrinterName = PrinterName1
                                    frm.printDoc.PrinterSettings.Collate = PrintDialog1.PrinterSettings.Collate
                                    frm.printDoc.PrinterSettings.Copies = PrintDialog1.PrinterSettings.Copies
                                    AddHandler frm.printDoc.PrintPage, AddressOf frm.PD_PrintPage
                                    frm.printTotalPages = numPages
                                    If frm.printDoc.PrinterSettings.Collate Then
                                        For i = r.NumberOfPages To 1 Step -1
                                            If frm.DoEvents_Wait(1000) Then
                                                frm.printDocPageNum = i
                                                frm.printDocImage = System.Drawing.Image.FromStream(New System.IO.MemoryStream(frm.A0_LoadImageGhostScript(r, i, CInt(r.GetPageSizeWithRotation(i).Width) * CSng(IIf(frm.getPercent(r.Clone, i) > 2, frm.getPercent(r.Clone, i), 2)), CInt(r.GetPageSizeWithRotation(i).Height * CSng(IIf(frm.getPercent(r.Clone, i) > 2, frm.getPercent(r.Clone, i), 2))), False)))
                                                frm.printDocImageList.Add(frm.printDocImage.Clone)
                                            End If
                                        Next
                                        frm.printTotalPages = r.NumberOfPages
                                        frm.printDocIndex = -1
                                        frm.printDoc.Print()
                                    Else
                                        frm.printTotalPages = r.NumberOfPages
                                        For i = 1 To r.NumberOfPages
                                            If frm.DoEvents_Wait(1000) Then
                                                frm.printDocPageNum = i
                                                frm.printDocImage = System.Drawing.Image.FromStream(New System.IO.MemoryStream(frm.A0_LoadImageGhostScript(r, i, CInt(r.GetPageSizeWithRotation(i).Width) * CSng(IIf(frm.getPercent(r.Clone, i) > 2, frm.getPercent(r.Clone, i), 2)), CInt(r.GetPageSizeWithRotation(i).Height) * CSng(IIf(frm.getPercent(r.Clone, i) > 2, frm.getPercent(r.Clone, i), 2)), False)))
                                                frm.printDocImageList.Add(frm.printDocImage.Clone)
                                            End If
                                        Next
                                        frm.printTotalPages = r.NumberOfPages
                                        frm.printDocIndex = -1
                                        frm.printDoc.Print()
                                    End If
                                    RemoveHandler frm.printDoc.PrintPage, AddressOf frm.PD_PrintPage
                                Next
                                frm.printDoc = Nothing
                                exitCode = 1
                                pdfReaderDocClone = readerTemp.Clone
                                frm.LoadPDFReaderDoc(frm.pdfOwnerPassword)
                                Try
                                    Dim cntr As Integer = 0
GOTO_PROCESS_WAIT_REDO:
GoTo_PROCESS_WAIT_OVER:
                                    If exitCode < 1 Then
                                    End If
                                Catch ex As Exception
                                    frm.TimeStampAdd(ex, frm.debugMode)
                                End Try
                            Catch exPrintProcess As Exception
                                MessageBox.Show(exPrintProcess.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Finally
                                Try
                                    If Not exitCode = 1 Then
                                        Exit Try
                                    End If
                                Catch ex As Exception
                                    frm.TimeStampAdd(ex, frm.debugMode)
                                End Try
                                Try
                                Catch exProcess As Exception
                                    frm.TimeStampAdd(exProcess, frm.debugMode)
                                End Try
                                Try
                                Catch ex As Exception
                                    frm.TimeStampAdd(ex, frm.debugMode)
                                End Try
                            End Try
                        End If
                    End If
            End Select
        Catch ex As Exception
            frm.TimeStampAdd(ex, frm.debugMode)
        Finally
            frm.StatusToolStrip = "Printing: completed."
            Me.BringToFront()
        End Try
    End Sub
    Public Sub New(ByRef frmMain1 As frmMain)
        InitializeComponent()
        frm = frmMain1
        pdfBytes = frm.Session
        pdfReaderDocClone = frm.pdfReaderDoc.Clone
    End Sub
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        PrintDialog1.PrinterSettings.PrinterName = comboInstalledPrinters.Items(comboInstalledPrinters.SelectedIndex).ToString()
        PrintDialog1.PrinterSettings.Collate = PageHandlingChkCollate.Checked
        PrintDialog1.PrinterSettings.Copies = PageHandlingUpDownCopies.Value
        PrintDialog1.PrinterSettings.PrintToFile = PrintToFileChk.Checked
        LoadImages()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Public Function RotateBitmap(ByRef bm As Bitmap) As Bitmap
        Try
            Dim bmTemp As Bitmap = bm.Clone
            bmTemp.RotateFlip(RotateFlipType.Rotate270FlipNone)
            Return bmTemp.Clone
        Catch ex As Exception
            Err.Clear()
        End Try
        Return bm.Clone
    End Function
    Private Sub dialogPrint_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If frm Is Nothing Then
                If Me.Owner.GetType = GetType(frmMain) Then
                    frm = CType(Me.Owner, frmMain)
                End If
            End If
            PopulateInstalledPrintersCombo()
            If CommentsAndFormsCombo.SelectedIndex < 0 Then
                CommentsAndFormsCombo.SelectedIndex = 0
            End If
            If PrintRangeTxtSubset.SelectedIndex < 0 Then
                PrintRangeTxtSubset.SelectedIndex = 0
            End If
            If PageHandlingCmbPageScaling.SelectedIndex < 0 Then
                PageHandlingCmbPageScaling.SelectedIndex = 1
            End If
            If MultiplePagesPerSheetCombo.SelectedIndex < 0 Then
                MultiplePagesPerSheetCombo.SelectedIndex = 0
            End If
            If MultiplePageOrderCombo.SelectedIndex < 0 Then
                MultiplePageOrderCombo.SelectedIndex = 0
            End If
            If BookletSubsetCombobox.SelectedIndex < 0 Then
                BookletSubsetCombobox.SelectedIndex = 0
            End If
            If BookletBindingCombobox.SelectedIndex < 0 Then
                BookletBindingCombobox.SelectedIndex = 0
            End If
            updatePdfReader()
            Dim i As Integer = PreviewTrackbarScale.Value
            If PreviewTrackbarScale.Value > 0 And PreviewTrackbarScale.Value <= r.NumberOfPages Then
                PreviewPictureBox1.BackgroundImageLayout = ImageLayout.Stretch
                Select Case PageHandlingCmbPageScaling.SelectedIndex
                    Case 0
                        pnlPrintableArea.Show()
                        pnlPrintableArea.BringToFront()
                        PrintableAreaChoosePaperSourceByPdfPageSizeCheckbox.Enabled = True
                        pageScale = CInt(r.GetPageSizeWithRotation(i).Height) / CInt(r.GetPageSizeWithRotation(i).Width)
                        PreviewPictureBox1.Width = 150
                        PreviewPictureBox1.Height = 150 * pageScale
                        PreviewPictureBox1.BackgroundImage = System.Drawing.Image.FromStream(New System.IO.MemoryStream(frm.A0_LoadImageGhostScript(r, i, CInt(PreviewPictureBox1.Width), CInt(PreviewPictureBox1.Height), False)))
                    Case 1
                        pnlPrintableArea.Show()
                        pnlPrintableArea.BringToFront()
                        pageScale = CInt(r.GetPageSizeWithRotation(i).Height) / CInt(r.GetPageSizeWithRotation(i).Width)
                        PreviewPictureBox1.Width = 150
                        PreviewPictureBox1.Height = 150 * pageScale
                        PreviewPictureBox1.BackgroundImage = System.Drawing.Image.FromStream(New System.IO.MemoryStream(frm.A0_LoadImageGhostScript(r, i, CInt(PreviewPictureBox1.Width), CInt(PreviewPictureBox1.Height), False)))
                    Case 2
                        pnlPrintableArea.Show()
                        pnlPrintableArea.BringToFront()
                        pageScale = CInt(r.GetPageSizeWithRotation(i).Height) / CInt(r.GetPageSizeWithRotation(i).Width)
                        PreviewPictureBox1.Width = 150
                        PreviewPictureBox1.Height = 150 * pageScale
                        PreviewPictureBox1.BackgroundImage = System.Drawing.Image.FromStream(New System.IO.MemoryStream(frm.A0_LoadImageGhostScript(r, i, CInt(PreviewPictureBox1.Width), CInt(PreviewPictureBox1.Height), False)))
                    Case 3, 4
                        pnlTile.Show()
                        pnlTile.BringToFront()
                    Case 5
                        pnlMultiple.Show()
                        pnlMultiple.BringToFront()
                    Case 6
                        pnlBooklet.Show()
                        pnlBooklet.BringToFront()
                End Select
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Enum PrinterStatus
        PrinterOther = 1
        PrinterUnkown = 2
        PrinterIdle = 3
        PrinterPrinting = 4
        PrinterWarmingUp = 5
        PrinterStopped = 6
        PrinterOffline = 7
        PrinterPaused = 8
        PrinterError = 9
        PrinterBusy = 10
        PrinterNotAvailable = 11
        PrinterWaiting = 12
        PrinterProcessing = 13
        PrinterInitialization = 14
        PrinterPowerSave = 15
        PrinterPendingDeletion = 16
        PrinterActive = 17
        PrinterManualFeed = 18
    End Enum
    Private Function PrinterStatusToString(ByVal ps As PrinterStatus) As String
        Dim s As String
        Select Case ps
            Case PrinterStatus.PrinterOther
                Return "other"
            Case PrinterStatus.PrinterUnkown
                Return "unknown"
            Case PrinterStatus.PrinterIdle
                Return "idle"
            Case PrinterStatus.PrinterPrinting
                Return "printing"
            Case PrinterStatus.PrinterWarmingUp
                Return "warmup"
            Case PrinterStatus.PrinterStopped
                Return "stopped"
            Case PrinterStatus.PrinterOffline
                Return "offline"
            Case PrinterStatus.PrinterPaused
                Return "paused"
            Case PrinterStatus.PrinterError
                Return "error"
            Case PrinterStatus.PrinterBusy
                Return "busy"
            Case PrinterStatus.PrinterNotAvailable
                Return "not available"
            Case PrinterStatus.PrinterWaiting
                Return "waiting"
            Case PrinterStatus.PrinterProcessing
                Return "processing"
            Case PrinterStatus.PrinterInitialization
                Return "initialization"
            Case PrinterStatus.PrinterPowerSave
                Return "power save"
            Case PrinterStatus.PrinterPendingDeletion
                Return "pending deletion"
            Case PrinterStatus.PrinterActive
                Return "active"
            Case PrinterStatus.PrinterManualFeed
                Return "manual feed"
            Case Else
                Return "unknown"
        End Select
        Return s
    End Function
    Public Function getPrinterStatusString(printerName As String)
        Dim strPrintServer As String = "localhost"
        Dim WMIObject As String = "winmgmts://" & strPrintServer
        Dim PrinterSet As Object
        Dim Printer As Object
        PrinterSet = GetObject(WMIObject).InstancesOf("win32_Printer")
        For Each Printer In PrinterSet
            If Printer.Name = printerName Then
                Return PrinterStatusToString(Printer.PrinterStatus)
            End If
        Next
        Return "unknown"
    End Function
    Private Sub PopulateInstalledPrintersCombo()
        Dim i As Integer
        Dim pkInstalledPrinters As String
        Dim selIndex As Integer = 0
        For i = 0 To System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count - 1
            Try
                pkInstalledPrinters = System.Drawing.Printing.PrinterSettings.InstalledPrinters.Item(i).ToString
                comboInstalledPrinters.Items.Add(pkInstalledPrinters)
                If docToPrint.DefaultPageSettings.PrinterSettings.PrinterName = pkInstalledPrinters Then
                    selIndex = i
                End If
            Catch ex As Exception
                Err.Clear()
            End Try
        Next
        If comboInstalledPrinters.Items.Count > 0 And comboInstalledPrinters.SelectedIndex < 0 Then
            comboInstalledPrinters.SelectedIndex = selIndex
        End If
    End Sub
    Private Sub comboInstalledPrinters_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboInstalledPrinters.SelectedIndexChanged
        Try
            If comboInstalledPrinters.SelectedIndex >= 0 Then
                Try
                    lblPrinterStatus.Text = getPrinterStatusString(comboInstalledPrinters.Items(comboInstalledPrinters.SelectedIndex).ToString)
                Catch ex1 As Exception
                    Err.Clear()
                End Try
                Try
                    getPrinterProperties(comboInstalledPrinters.Items(comboInstalledPrinters.SelectedIndex).ToString)
                Catch ex1 As Exception
                    Err.Clear()
                End Try
            Else
                lblPrinterStatus.Text = ""
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub btnPrinterProperties_Click(sender As Object, e As EventArgs) Handles btnPrinterProperties.Click
        If comboInstalledPrinters.SelectedIndex < 0 Then Return
        PrintDialog1.ShowHelp = True
        PrintDialog1.AllowCurrentPage = True
        PrintDialog1.AllowSelection = True
        PrintDialog1.AllowPrintToFile = True
        PrintDialog1.AllowSomePages = True
        PrintDialog1.ShowNetwork = True
        PrintDialog1.PrinterSettings.PrinterName = comboInstalledPrinters.Items(comboInstalledPrinters.SelectedIndex).ToString
        PrintDialog1.UseEXDialog = False
        PrintDialog1.Document = docToPrint
        Select Case PrintDialog1.ShowDialog(Me)
            Case DialogResult.OK, DialogResult.Yes
            Case Else
        End Select
        Me.BringToFront()
    End Sub
    Private Function getPrinterProperties(DeviceID As String) As String
        Dim consoleWriteLine As New System.Text.StringBuilder
        Try
            Dim searcher As ManagementObjectSearcher = New ManagementObjectSearcher("root\CIMV2", "SELECT * FROM Win32_Printer")
            Dim thisDevice As Boolean = False
            For Each queryObj As ManagementObject In searcher.[Get]()
                If Not String.IsNullOrEmpty(DeviceID & "") Then
                    If Not queryObj("DeviceID") Is Nothing Then
                        If queryObj("DeviceID") = DeviceID Then
                            thisDevice = True
                            consoleWriteLine = New System.Text.StringBuilder
                        End If
                    End If
                End If
                consoleWriteLine.AppendLine("-----------------------------------")
                consoleWriteLine.AppendLine("Win32_Printer instance")
                consoleWriteLine.AppendLine("-----------------------------------")
                consoleWriteLine.AppendLine(String.Format("Attributes: {0}", queryObj("Attributes")))
                consoleWriteLine.AppendLine(String.Format("Availability: {0}", queryObj("Availability")))
                If queryObj("AvailableJobSheets") Is Nothing Then
                    consoleWriteLine.AppendLine(String.Format("AvailableJobSheets: {0}", queryObj("AvailableJobSheets")))
                Else
                    Dim arrAvailableJobSheets As String() = CType((queryObj("AvailableJobSheets")), String())
                    For Each arrValue As String In arrAvailableJobSheets
                        consoleWriteLine.AppendLine(String.Format("AvailableJobSheets: {0}", arrValue))
                    Next
                End If
                consoleWriteLine.AppendLine(String.Format("AveragePagesPerMinute: {0}", queryObj("AveragePagesPerMinute")))
                If queryObj("Capabilities") Is Nothing Then
                    consoleWriteLine.AppendLine(String.Format("Capabilities: {0}", queryObj("Capabilities")))
                Else
                    Dim arrCapabilities As UInt16() = CType((queryObj("Capabilities")), UInt16())
                    For Each arrValue As UInt16 In arrCapabilities
                        consoleWriteLine.AppendLine(String.Format("Capabilities: {0}", arrValue))
                    Next
                End If
                If queryObj("CapabilityDescriptions") Is Nothing Then
                    consoleWriteLine.AppendLine(String.Format("CapabilityDescriptions: {0}", queryObj("CapabilityDescriptions")))
                Else
                    Dim arrCapabilityDescriptions As String() = CType((queryObj("CapabilityDescriptions")), String())
                    For Each arrValue As String In arrCapabilityDescriptions
                        consoleWriteLine.AppendLine(String.Format("CapabilityDescriptions: {0}", arrValue))
                    Next
                End If
                consoleWriteLine.AppendLine(String.Format("Caption: {0}", queryObj("Caption")))
                If queryObj("CharSetsSupported") Is Nothing Then
                    consoleWriteLine.AppendLine(String.Format("CharSetsSupported: {0}", queryObj("CharSetsSupported")))
                Else
                    Dim arrCharSetsSupported As String() = CType((queryObj("CharSetsSupported")), String())
                    For Each arrValue As String In arrCharSetsSupported
                        consoleWriteLine.AppendLine(String.Format("CharSetsSupported: {0}", arrValue))
                    Next
                End If
                consoleWriteLine.AppendLine(String.Format("Comment: {0}", queryObj("Comment")))
                consoleWriteLine.AppendLine(String.Format("ConfigManagerErrorCode: {0}", queryObj("ConfigManagerErrorCode")))
                consoleWriteLine.AppendLine(String.Format("ConfigManagerUserConfig: {0}", queryObj("ConfigManagerUserConfig")))
                consoleWriteLine.AppendLine(String.Format("CreationClassName: {0}", queryObj("CreationClassName")))
                If queryObj("CurrentCapabilities") Is Nothing Then
                    consoleWriteLine.AppendLine(String.Format("CurrentCapabilities: {0}", queryObj("CurrentCapabilities")))
                Else
                    Dim arrCurrentCapabilities As UInt16() = CType((queryObj("CurrentCapabilities")), UInt16())
                    For Each arrValue As UInt16 In arrCurrentCapabilities
                        consoleWriteLine.AppendLine(String.Format("CurrentCapabilities: {0}", arrValue))
                    Next
                End If
                consoleWriteLine.AppendLine(String.Format("CurrentCharSet: {0}", queryObj("CurrentCharSet")))
                consoleWriteLine.AppendLine(String.Format("CurrentLanguage: {0}", queryObj("CurrentLanguage")))
                consoleWriteLine.AppendLine(String.Format("CurrentMimeType: {0}", queryObj("CurrentMimeType")))
                consoleWriteLine.AppendLine(String.Format("CurrentNaturalLanguage: {0}", queryObj("CurrentNaturalLanguage")))
                consoleWriteLine.AppendLine(String.Format("CurrentPaperType: {0}", queryObj("CurrentPaperType")))
                consoleWriteLine.AppendLine(String.Format("Default: {0}", queryObj("Default")))
                If queryObj("DefaultCapabilities") Is Nothing Then
                    consoleWriteLine.AppendLine(String.Format("DefaultCapabilities: {0}", queryObj("DefaultCapabilities")))
                Else
                    Dim arrDefaultCapabilities As UInt16() = CType((queryObj("DefaultCapabilities")), UInt16())
                    For Each arrValue As UInt16 In arrDefaultCapabilities
                        consoleWriteLine.AppendLine(String.Format("DefaultCapabilities: {0}", arrValue))
                    Next
                End If
                consoleWriteLine.AppendLine(String.Format("DefaultCopies: {0}", queryObj("DefaultCopies")))
                consoleWriteLine.AppendLine(String.Format("DefaultLanguage: {0}", queryObj("DefaultLanguage")))
                consoleWriteLine.AppendLine(String.Format("DefaultMimeType: {0}", queryObj("DefaultMimeType")))
                consoleWriteLine.AppendLine(String.Format("DefaultNumberUp: {0}", queryObj("DefaultNumberUp")))
                consoleWriteLine.AppendLine(String.Format("DefaultPaperType: {0}", queryObj("DefaultPaperType")))
                consoleWriteLine.AppendLine(String.Format("DefaultPriority: {0}", queryObj("DefaultPriority")))
                consoleWriteLine.AppendLine(String.Format("Description: {0}", queryObj("Description")))
                consoleWriteLine.AppendLine(String.Format("DetectedErrorState: {0}", queryObj("DetectedErrorState")))
                consoleWriteLine.AppendLine(String.Format("DeviceID: {0}", queryObj("DeviceID")))
                consoleWriteLine.AppendLine(String.Format("Direct: {0}", queryObj("Direct")))
                consoleWriteLine.AppendLine(String.Format("DoCompleteFirst: {0}", queryObj("DoCompleteFirst")))
                consoleWriteLine.AppendLine(String.Format("DriverName: {0}", queryObj("DriverName")))
                If thisDevice Then
                    lblPrinterType.Text = "" & queryObj("DriverName") & ""
                End If
                consoleWriteLine.AppendLine(String.Format("EnableBIDI: {0}", queryObj("EnableBIDI")))
                consoleWriteLine.AppendLine(String.Format("EnableDevQueryPrint: {0}", queryObj("EnableDevQueryPrint")))
                consoleWriteLine.AppendLine(String.Format("ErrorCleared: {0}", queryObj("ErrorCleared")))
                consoleWriteLine.AppendLine(String.Format("ErrorDescription: {0}", queryObj("ErrorDescription")))
                If queryObj("ErrorInformation") Is Nothing Then
                    consoleWriteLine.AppendLine(String.Format("ErrorInformation: {0}", queryObj("ErrorInformation")))
                Else
                    Dim arrErrorInformation As String() = CType((queryObj("ErrorInformation")), String())
                    For Each arrValue As String In arrErrorInformation
                        consoleWriteLine.AppendLine(String.Format("ErrorInformation: {0}", arrValue))
                    Next
                End If
                consoleWriteLine.AppendLine(String.Format("ExtendedDetectedErrorState: {0}", queryObj("ExtendedDetectedErrorState")))
                consoleWriteLine.AppendLine(String.Format("ExtendedPrinterStatus: {0}", queryObj("ExtendedPrinterStatus")))
                consoleWriteLine.AppendLine(String.Format("Hidden: {0}", queryObj("Hidden")))
                consoleWriteLine.AppendLine(String.Format("HorizontalResolution: {0}", queryObj("HorizontalResolution")))
                consoleWriteLine.AppendLine(String.Format("InstallDate: {0}", queryObj("InstallDate")))
                consoleWriteLine.AppendLine(String.Format("JobCountSinceLastReset: {0}", queryObj("JobCountSinceLastReset")))
                consoleWriteLine.AppendLine(String.Format("KeepPrintedJobs: {0}", queryObj("KeepPrintedJobs")))
                If queryObj("LanguagesSupported") Is Nothing Then
                    consoleWriteLine.AppendLine(String.Format("LanguagesSupported: {0}", queryObj("LanguagesSupported")))
                Else
                    Dim arrLanguagesSupported As UInt16() = CType((queryObj("LanguagesSupported")), UInt16())
                    For Each arrValue As UInt16 In arrLanguagesSupported
                        consoleWriteLine.AppendLine(String.Format("LanguagesSupported: {0}", arrValue))
                    Next
                End If
                consoleWriteLine.AppendLine(String.Format("LastErrorCode: {0}", queryObj("LastErrorCode")))
                consoleWriteLine.AppendLine(String.Format("Local: {0}", queryObj("Local")))
                consoleWriteLine.AppendLine(String.Format("Location: {0}", queryObj("Location")))
                consoleWriteLine.AppendLine(String.Format("MarkingTechnology: {0}", queryObj("MarkingTechnology")))
                consoleWriteLine.AppendLine(String.Format("MaxCopies: {0}", queryObj("MaxCopies")))
                consoleWriteLine.AppendLine(String.Format("MaxNumberUp: {0}", queryObj("MaxNumberUp")))
                consoleWriteLine.AppendLine(String.Format("MaxSizeSupported: {0}", queryObj("MaxSizeSupported")))
                If queryObj("MimeTypesSupported") Is Nothing Then
                    consoleWriteLine.AppendLine(String.Format("MimeTypesSupported: {0}", queryObj("MimeTypesSupported")))
                Else
                    Dim arrMimeTypesSupported As String() = CType((queryObj("MimeTypesSupported")), String())
                    For Each arrValue As String In arrMimeTypesSupported
                        consoleWriteLine.AppendLine(String.Format("MimeTypesSupported: {0}", arrValue))
                    Next
                End If
                consoleWriteLine.AppendLine(String.Format("Name: {0}", queryObj("Name")))
                If queryObj("NaturalLanguagesSupported") Is Nothing Then
                    consoleWriteLine.AppendLine(String.Format("NaturalLanguagesSupported: {0}", queryObj("NaturalLanguagesSupported")))
                Else
                    Dim arrNaturalLanguagesSupported As String() = CType((queryObj("NaturalLanguagesSupported")), String())
                    For Each arrValue As String In arrNaturalLanguagesSupported
                        consoleWriteLine.AppendLine(String.Format("NaturalLanguagesSupported: {0}", arrValue))
                    Next
                End If
                consoleWriteLine.AppendLine(String.Format("Network: {0}", queryObj("Network")))
                If queryObj("PaperSizesSupported") Is Nothing Then
                    consoleWriteLine.AppendLine(String.Format("PaperSizesSupported: {0}", queryObj("PaperSizesSupported")))
                Else
                    Dim arrPaperSizesSupported As UInt16() = CType((queryObj("PaperSizesSupported")), UInt16())
                    For Each arrValue As UInt16 In arrPaperSizesSupported
                        consoleWriteLine.AppendLine(String.Format("PaperSizesSupported: {0}", arrValue))
                    Next
                End If
                If queryObj("PaperTypesAvailable") Is Nothing Then
                    consoleWriteLine.AppendLine(String.Format("PaperTypesAvailable: {0}", queryObj("PaperTypesAvailable")))
                Else
                    Dim arrPaperTypesAvailable As String() = CType((queryObj("PaperTypesAvailable")), String())
                    For Each arrValue As String In arrPaperTypesAvailable
                        consoleWriteLine.AppendLine(String.Format("PaperTypesAvailable: {0}", arrValue))
                    Next
                End If
                consoleWriteLine.AppendLine(String.Format("Parameters: {0}", queryObj("Parameters")))
                consoleWriteLine.AppendLine(String.Format("PNPDeviceID: {0}", queryObj("PNPDeviceID")))
                consoleWriteLine.AppendLine(String.Format("PortName: {0}", queryObj("PortName")))
                If queryObj("PowerManagementCapabilities") Is Nothing Then
                    consoleWriteLine.AppendLine(String.Format("PowerManagementCapabilities: {0}", queryObj("PowerManagementCapabilities")))
                Else
                    Dim arrPowerManagementCapabilities As UInt16() = CType((queryObj("PowerManagementCapabilities")), UInt16())
                    For Each arrValue As UInt16 In arrPowerManagementCapabilities
                        consoleWriteLine.AppendLine(String.Format("PowerManagementCapabilities: {0}", arrValue))
                    Next
                End If
                consoleWriteLine.AppendLine(String.Format("PowerManagementSupported: {0}", queryObj("PowerManagementSupported")))
                If queryObj("PrinterPaperNames") Is Nothing Then
                    consoleWriteLine.AppendLine(String.Format("PrinterPaperNames: {0}", queryObj("PrinterPaperNames")))
                Else
                    Dim arrPrinterPaperNames As String() = CType((queryObj("PrinterPaperNames")), String())
                    For Each arrValue As String In arrPrinterPaperNames
                        consoleWriteLine.AppendLine(String.Format("PrinterPaperNames: {0}", arrValue))
                    Next
                End If
                consoleWriteLine.AppendLine(String.Format("PrinterState: {0}", queryObj("PrinterState")))
                consoleWriteLine.AppendLine(String.Format("PrinterStatus: {0}", queryObj("PrinterStatus")))
                consoleWriteLine.AppendLine(String.Format("PrintJobDataType: {0}", queryObj("PrintJobDataType")))
                consoleWriteLine.AppendLine(String.Format("PrintProcessor: {0}", queryObj("PrintProcessor")))
                consoleWriteLine.AppendLine(String.Format("Priority: {0}", queryObj("Priority")))
                consoleWriteLine.AppendLine(String.Format("Published: {0}", queryObj("Published")))
                consoleWriteLine.AppendLine(String.Format("Queued: {0}", queryObj("Queued")))
                consoleWriteLine.AppendLine(String.Format("RawOnly: {0}", queryObj("RawOnly")))
                consoleWriteLine.AppendLine(String.Format("SeparatorFile: {0}", queryObj("SeparatorFile")))
                consoleWriteLine.AppendLine(String.Format("ServerName: {0}", queryObj("ServerName")))
                consoleWriteLine.AppendLine(String.Format("Shared: {0}", queryObj("Shared")))
                consoleWriteLine.AppendLine(String.Format("ShareName: {0}", queryObj("ShareName")))
                consoleWriteLine.AppendLine(String.Format("SpoolEnabled: {0}", queryObj("SpoolEnabled")))
                consoleWriteLine.AppendLine(String.Format("StartTime: {0}", queryObj("StartTime")))
                consoleWriteLine.AppendLine(String.Format("Status: {0}", queryObj("Status")))
                consoleWriteLine.AppendLine(String.Format("StatusInfo: {0}", queryObj("StatusInfo")))
                consoleWriteLine.AppendLine(String.Format("SystemCreationClassName: {0}", queryObj("SystemCreationClassName")))
                consoleWriteLine.AppendLine(String.Format("SystemName: {0}", queryObj("SystemName")))
                consoleWriteLine.AppendLine(String.Format("TimeOfLastReset: {0}", queryObj("TimeOfLastReset")))
                consoleWriteLine.AppendLine(String.Format("UntilTime: {0}", queryObj("UntilTime")))
                consoleWriteLine.AppendLine(String.Format("VerticalResolution: {0}", queryObj("VerticalResolution")))
                consoleWriteLine.AppendLine(String.Format("WorkOffline: {0}", queryObj("WorkOffline")))
                If thisDevice Then
                    Exit For
                End If
            Next
        Catch e As ManagementException
            consoleWriteLine.AppendLine(String.Format("An error occurred while querying for WMI data: " & e.Message))
        End Try
        Return consoleWriteLine.ToString()
    End Function
    Private Sub btnPrinterProp1_Click(sender As Object, e As EventArgs) Handles btnPrinterProp1.Click
        Try
            If comboInstalledPrinters.SelectedIndex >= 0 Then
                Try
                    txtPrinterInformation.Text = getPrinterProperties(comboInstalledPrinters.Items(comboInstalledPrinters.SelectedIndex).ToString)
                    groupboxPrinterInformation.Show()
                    groupboxPrinterInformation.BringToFront()
                Catch ex1 As Exception
                    Err.Clear()
                End Try
            Else
                lblPrinterStatus.Text = ""
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub btnPrinterInformationClose_Click(sender As Object, e As EventArgs) Handles btnPrinterInformationClose.Click
        Try
            groupboxPrinterInformation.Hide()
            groupboxPrinterInformation.SendToBack()
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub cmbPageScaling_SelectedIndexChanged(sender As Object, e As EventArgs) Handles PageHandlingCmbPageScaling.SelectedIndexChanged
        Try
            pnlPrintableArea.Hide()
            pnlTile.Hide()
            pnlMultiple.Hide()
            pnlBooklet.Hide()
            PrintableAreaChoosePaperSourceByPdfPageSizeCheckbox.Enabled = False
            Select Case PageHandlingCmbPageScaling.SelectedIndex
                Case 0
                    pnlPrintableArea.Show()
                    pnlPrintableArea.BringToFront()
                    PrintableAreaChoosePaperSourceByPdfPageSizeCheckbox.Enabled = True
                Case 1, 2
                    pnlPrintableArea.Show()
                    pnlPrintableArea.BringToFront()
                Case 3, 4
                    pnlTile.Show()
                    pnlTile.BringToFront()
                Case 5
                    pnlMultiple.Show()
                    pnlMultiple.BringToFront()
                Case 6
                    pnlBooklet.Show()
                    pnlBooklet.BringToFront()
            End Select
            tmrScroller1_Fired = True
            tmrScroller1.Enabled = True
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MultiplePagesPerSheetCombo.SelectedIndexChanged
        Try
            Select Case MultiplePagesPerSheetCombo.SelectedIndex
                Case 0, 1, 2, 3, 4
                    MultiplePagesPerSheetByColumns.Enabled = False
                    MultiplePagesPerSheetByRows.Enabled = False
                Case 5
                    MultiplePagesPerSheetByColumns.Enabled = True
                    MultiplePagesPerSheetByRows.Enabled = True
            End Select
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub PrintRangeOptPageRange_CheckedChanged(sender As Object, e As EventArgs) Handles PrintRangeOptPageRange.CheckedChanged
        PrintRangeTxtRange.Enabled = PrintRangeOptPageRange.Checked
        PrintRangeTxtSubset.Enabled = PrintRangeOptPageRange.Checked
        r = frm.pdfReaderDoc.Clone
        If r.NumberOfPages > 0 Then
            If PrintRangeOptPageRange.Checked Then
                If PrintRangeTxtRange.Text = "" Then
                    PrintRangeTxtRange.Text = "1-" & r.NumberOfPages
                End If
                r.SelectPages(PrintRangeTxtRange.Text)
            End If
        End If
        tmrScroller1_Fired = True
        tmrScroller1.Enabled = True
    End Sub
    Private Sub PrintRangeOptCurrentPage_CheckedChanged(sender As Object, e As EventArgs) Handles PrintRangeOptCurrentPage.CheckedChanged
        PrintRangeTxtRange.Enabled = PrintRangeOptPageRange.Checked
        PrintRangeTxtSubset.Enabled = PrintRangeOptPageRange.Checked
        tmrScroller1_Fired = True
        tmrScroller1.Enabled = True
    End Sub
    Private Sub PrintRangeOptCurrentView_CheckedChanged(sender As Object, e As EventArgs) Handles PrintRangeOptCurrentView.CheckedChanged
        PrintRangeTxtRange.Enabled = PrintRangeOptPageRange.Checked
        PrintRangeTxtSubset.Enabled = PrintRangeOptPageRange.Checked
        tmrScroller1_Fired = True
        tmrScroller1.Enabled = True
    End Sub
    Private Sub PrintRangeOptAll_CheckedChanged(sender As Object, e As EventArgs) Handles PrintRangeOptAll.CheckedChanged
        PrintRangeTxtRange.Enabled = PrintRangeOptPageRange.Checked
        PrintRangeTxtSubset.Enabled = PrintRangeOptPageRange.Checked
        tmrScroller1_Fired = True
        tmrScroller1.Enabled = True
    End Sub
    Public Sub tmrScroller1_Tick() Handles tmrScroller1.Tick
        tmrScroller1.Interval = 500
        If tmrScroller1_Fired Then
            tmrScroller1_Fired = False
            updatePdfReader()
        Else
            tmrScroller1.Enabled = False
            Try
                updatePdfReader()
                LoadImageCurrent()
            Catch ex As Exception
                Err.Clear()
            End Try
        End If
    End Sub
    Public Sub LoadImages()
        Try
            frm.printDocImageList = New List(Of System.Drawing.Image)
            frm.printDocImageList.Clear()
            Dim qualityScale As Double = 4.5F
            For i As Integer = 1 To r.NumberOfPages
                If PreviewTrackbarScale.Value > 0 And PreviewTrackbarScale.Value <= r.NumberOfPages Then
                    PreviewPictureBox1.BackgroundImageLayout = ImageLayout.Stretch
                    Dim img As System.Drawing.Bitmap = Nothing
                    Dim imgPaper As System.Drawing.Bitmap = Nothing
                    Select Case PageHandlingCmbPageScaling.SelectedIndex
                        Case 0
                            pnlPrintableArea.Show()
                            pnlPrintableArea.BringToFront()
                            PrintableAreaChoosePaperSourceByPdfPageSizeCheckbox.Enabled = True
                            pageScale = CInt(r.GetPageSizeWithRotation(i).Height) / CInt(r.GetPageSizeWithRotation(i).Width)
                            img = New System.Drawing.Bitmap(System.Drawing.Image.FromStream(New System.IO.MemoryStream(frm.A0_LoadImageGhostScript(r, i, r.GetPageSizeWithRotation(i).Width * qualityScale, r.GetPageSizeWithRotation(i).Height * qualityScale, False))))
                            imgPaper = New System.Drawing.Bitmap(CInt(docToPrint.DefaultPageSettings.PaperSize.Width * qualityScale), CInt(docToPrint.DefaultPageSettings.PaperSize.Height * qualityScale))
                            Dim x As Integer = 0, y As Integer = 0
                            If (PrintableAreaAutoRotateAndCenterCheckBox.Checked) Then
                                If img.Height < img.Width Then
                                    If Not docToPrint.DefaultPageSettings.Landscape Then
                                        img = RotateBitmap(img)
                                    End If
                                End If
                                x = (imgPaper.Width - img.Width) / 2
                                y = (imgPaper.Height - img.Height) / 2
                            End If
                            Using g As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(imgPaper)
                                g.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
                                g.PageUnit = GraphicsUnit.Pixel
                                g.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
                                g.SmoothingMode = Drawing2D.SmoothingMode.Default
                                g.DrawImage(img.Clone, x, y, img.Width, img.Height)
                                g.Dispose()
                            End Using
                            frm.printDocImageList.Add(imgPaper.Clone)
                        Case 1
                            pnlPrintableArea.Show()
                            pnlPrintableArea.BringToFront()
                            pageScale = CInt(docToPrint.DefaultPageSettings.PaperSize.Height) / CInt(docToPrint.DefaultPageSettings.PaperSize.Width)
                            imgPaper = New System.Drawing.Bitmap(System.Drawing.Image.FromStream(New System.IO.MemoryStream(frm.A0_LoadImageGhostScript(r, i, CInt(docToPrint.DefaultPageSettings.PaperSize.Width) * qualityScale, CInt(docToPrint.DefaultPageSettings.PaperSize.Height) * qualityScale, False))))
                            frm.printDocImageList.Add(imgPaper.Clone)
                        Case 2
                            pnlPrintableArea.Show()
                            pnlPrintableArea.BringToFront()
                            pageScale = CInt(r.GetPageSizeWithRotation(i).Height) / CInt(r.GetPageSizeWithRotation(i).Width)
                            img = New System.Drawing.Bitmap(System.Drawing.Image.FromStream(New System.IO.MemoryStream(frm.A0_LoadImageGhostScript(r, i, r.GetPageSizeWithRotation(i).Width * qualityScale, r.GetPageSizeWithRotation(i).Height * qualityScale, False))))
                            imgPaper = New System.Drawing.Bitmap(CInt(docToPrint.DefaultPageSettings.PaperSize.Width * qualityScale), CInt(docToPrint.DefaultPageSettings.PaperSize.Height * qualityScale))
                            Dim x As Integer = 0, y As Integer = 0
                            If (PrintableAreaAutoRotateAndCenterCheckBox.Checked) Then
                                x = (imgPaper.Width - img.Width) / 2
                                y = (imgPaper.Height - img.Height) / 2
                            End If
                            Using g As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(imgPaper)
                                g.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
                                g.PageUnit = GraphicsUnit.Pixel
                                g.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
                                g.SmoothingMode = Drawing2D.SmoothingMode.Default
                                If img.Width > imgPaper.Width Or img.Height > imgPaper.Height Then
                                    If img.Width > imgPaper.Width Then
                                        pageScale = imgPaper.Width / img.Width
                                        g.DrawImage(img.Clone, x, y, img.Width * pageScale, img.Height * pageScale)
                                    End If
                                    If img.Height > imgPaper.Height Then
                                        pageScale = imgPaper.Height / img.Height
                                        g.DrawImage(img.Clone, x, y, img.Width * pageScale, img.Height * pageScale)
                                    End If
                                Else
                                    g.DrawImage(img.Clone, x, y, img.Width, img.Height)
                                End If
                                g.Dispose()
                            End Using
                            frm.printDocImageList.Add(imgPaper.Clone)
                        Case 3, 4
                            pnlTile.Show()
                            pnlTile.BringToFront()
                            pageScale = CInt(r.GetPageSizeWithRotation(i).Height) / CInt(r.GetPageSizeWithRotation(i).Width)
                            PreviewPictureBox1.Width = 150
                            PreviewPictureBox1.Height = 150 * pageScale
                            frm.printDocImageList.Add(System.Drawing.Image.FromStream(New System.IO.MemoryStream(frm.A0_LoadImageGhostScript(r, i, CInt(r.GetPageSizeWithRotation(i).Width) * qualityScale, CInt(r.GetPageSizeWithRotation(i).Height) * qualityScale, False))))
                        Case 5
                            pnlMultiple.Show()
                            pnlMultiple.BringToFront()
                            pageScale = CInt(r.GetPageSizeWithRotation(i).Height) / CInt(r.GetPageSizeWithRotation(i).Width)
                            PreviewPictureBox1.Width = 150
                            PreviewPictureBox1.Height = 150 * pageScale
                            frm.printDocImageList.Add(System.Drawing.Image.FromStream(New System.IO.MemoryStream(frm.A0_LoadImageGhostScript(r, i, CInt(r.GetPageSizeWithRotation(i).Width) * qualityScale, CInt(r.GetPageSizeWithRotation(i).Height) * qualityScale, False))))
                        Case 6
                            pnlBooklet.Show()
                            pnlBooklet.BringToFront()
                            pageScale = CInt(r.GetPageSizeWithRotation(i).Height) / CInt(r.GetPageSizeWithRotation(i).Width)
                            PreviewPictureBox1.Width = 150
                            PreviewPictureBox1.Height = 150 * pageScale
                            frm.printDocImageList.Add(System.Drawing.Image.FromStream(New System.IO.MemoryStream(frm.A0_LoadImageGhostScript(r, i, CInt(r.GetPageSizeWithRotation(i).Width) * qualityScale, CInt(r.GetPageSizeWithRotation(i).Height) * qualityScale, False))))
                    End Select
                End If
            Next
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public Sub LoadImageCurrent()
        Dim i As Integer = PreviewTrackbarScale.Value
        Try
            If PreviewTrackbarScale.Value > 0 And PreviewTrackbarScale.Value <= r.NumberOfPages Then
                PreviewPictureBox1.BackgroundImageLayout = ImageLayout.Stretch
                Dim img As System.Drawing.Bitmap
                Dim imgPaper As System.Drawing.Bitmap
                Dim x As Integer = 0, y As Integer = 0
                Select Case PageHandlingCmbPageScaling.SelectedIndex
                    Case 0
                        pnlPrintableArea.Show()
                        pnlPrintableArea.BringToFront()
                        PrintableAreaChoosePaperSourceByPdfPageSizeCheckbox.Enabled = True
                        pageScale = CInt(r.GetPageSizeWithRotation(i).Height) / CInt(r.GetPageSizeWithRotation(i).Width)
                        img = New System.Drawing.Bitmap(System.Drawing.Image.FromStream(New System.IO.MemoryStream(frm.A0_LoadImageGhostScript(r, i, r.GetPageSizeWithRotation(i).Width, r.GetPageSizeWithRotation(i).Height, False))))
                        pageScale = CInt(docToPrint.DefaultPageSettings.PaperSize.Height) / CInt(docToPrint.DefaultPageSettings.PaperSize.Width)
                        If docToPrint.DefaultPageSettings.Landscape Then
                            imgPaper = New System.Drawing.Bitmap(CInt(docToPrint.DefaultPageSettings.PaperSize.Height), CInt(docToPrint.DefaultPageSettings.PaperSize.Width))
                            If img.Height > img.Width Then
                                img = RotateBitmap(img)
                            End If
                        Else
                            imgPaper = New System.Drawing.Bitmap(CInt(docToPrint.DefaultPageSettings.PaperSize.Width), CInt(docToPrint.DefaultPageSettings.PaperSize.Height))
                            PreviewPictureBox1.BackgroundImage = imgPaper.Clone
                        End If
                        If (PrintableAreaAutoRotateAndCenterCheckBox.Checked) Then
                            If img.Height < img.Width Then
                                If Not docToPrint.DefaultPageSettings.Landscape Then
                                    img = RotateBitmap(img)
                                End If
                            End If
                            x = (imgPaper.Width - img.Width) / 2
                            y = (imgPaper.Height - img.Height) / 2
                        End If
                        Using g As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(imgPaper)
                            g.DrawImage(img.Clone, x, y, img.Width, img.Height)
                            g.Dispose()
                        End Using
                        If imgPaper.Width > imgPaper.Height Then
                            PreviewPictureBox1.Width = 150 * pageScale
                            PreviewPictureBox1.Height = 150
                        Else
                            PreviewPictureBox1.Width = 150
                            PreviewPictureBox1.Height = 150 * pageScale
                        End If
                        PreviewPictureBox1.BackgroundImage = imgPaper.Clone
                    Case 1
                        pnlPrintableArea.Show()
                        pnlPrintableArea.BringToFront()
                        pageScale = CInt(docToPrint.DefaultPageSettings.PaperSize.Height) / CInt(docToPrint.DefaultPageSettings.PaperSize.Width)
                        If docToPrint.DefaultPageSettings.Landscape Then
                            imgPaper = System.Drawing.Image.FromStream(New System.IO.MemoryStream(frm.A0_LoadImageGhostScript(r, i, CInt(docToPrint.DefaultPageSettings.PaperSize.Width), CInt(docToPrint.DefaultPageSettings.PaperSize.Height), False)))
                            If imgPaper.Height > imgPaper.Width Then
                                imgPaper = RotateBitmap(imgPaper)
                            End If
                            If imgPaper.Width > imgPaper.Height Then
                                PreviewPictureBox1.Width = 150 * pageScale
                                PreviewPictureBox1.Height = 150
                            Else
                                PreviewPictureBox1.Width = 150
                                PreviewPictureBox1.Height = 150 * pageScale
                            End If
                            PreviewPictureBox1.BackgroundImage = imgPaper.Clone
                        Else
                            imgPaper = System.Drawing.Image.FromStream(New System.IO.MemoryStream(frm.A0_LoadImageGhostScript(r, i, CInt(docToPrint.DefaultPageSettings.PaperSize.Width), CInt(docToPrint.DefaultPageSettings.PaperSize.Height), False)))
                            If imgPaper.Width > imgPaper.Height Then
                                PreviewPictureBox1.Width = 150 * pageScale
                                PreviewPictureBox1.Height = 150
                            Else
                                PreviewPictureBox1.Width = 150
                                PreviewPictureBox1.Height = 150 * pageScale
                            End If
                            PreviewPictureBox1.BackgroundImage = imgPaper.Clone
                        End If
                    Case 2
                        pnlPrintableArea.Show()
                        pnlPrintableArea.BringToFront()
                        If docToPrint.DefaultPageSettings.Landscape Then
                            imgPaper = New System.Drawing.Bitmap(CInt(docToPrint.DefaultPageSettings.PaperSize.Height), CInt(docToPrint.DefaultPageSettings.PaperSize.Width))
                            img = System.Drawing.Image.FromStream(New System.IO.MemoryStream(frm.A0_LoadImageGhostScript(r, i, r.GetPageSizeWithRotation(i).Width, r.GetPageSizeWithRotation(i).Height, False)))
                            If (PrintableAreaAutoRotateAndCenterCheckBox.Checked) Then
                                If img.Height > img.Width Then
                                    img = RotateBitmap(img)
                                End If
                            End If
                            If img.Width > imgPaper.Width Then
                                pageScale = img.Height / imgPaper.Height
                                img = New System.Drawing.Bitmap(img, New System.Drawing.Size(img.Width * pageScale, img.Height * pageScale))
                            End If
                            If img.Height > imgPaper.Height Then
                                pageScale = img.Width / imgPaper.Width
                                img = New System.Drawing.Bitmap(img, New System.Drawing.Size(img.Width * pageScale, img.Height * pageScale))
                            End If
                            If (PrintableAreaAutoRotateAndCenterCheckBox.Checked) Then
                                x = (imgPaper.Width - img.Width) / 2
                                y = (imgPaper.Height - img.Height) / 2
                            Else
                            End If
                            Using g As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(imgPaper)
                                g.DrawImage(img.Clone, x, y, img.Width, img.Height)
                                g.Dispose()
                            End Using
                            If imgPaper.Width > imgPaper.Height Then
                                PreviewPictureBox1.Width = 150 * pageScale
                                PreviewPictureBox1.Height = 150
                            Else
                                PreviewPictureBox1.Width = 150
                                PreviewPictureBox1.Height = 150 * pageScale
                            End If
                            PreviewPictureBox1.BackgroundImage = imgPaper.Clone
                        Else
                            imgPaper = New System.Drawing.Bitmap(CInt(docToPrint.DefaultPageSettings.PaperSize.Width), CInt(docToPrint.DefaultPageSettings.PaperSize.Height))
                            img = New System.Drawing.Bitmap(System.Drawing.Image.FromStream(New System.IO.MemoryStream(frm.A0_LoadImageGhostScript(r, i, r.GetPageSizeWithRotation(i).Width, r.GetPageSizeWithRotation(i).Height, False))))
                            If (PrintableAreaAutoRotateAndCenterCheckBox.Checked) Then
                                If img.Width > img.Height Then
                                    img = RotateBitmap(img)
                                End If
                            End If
                            If img.Width > imgPaper.Width Then
                                pageScale = imgPaper.Width / img.Width
                                img = New System.Drawing.Bitmap(img, New System.Drawing.Size(img.Width * pageScale, img.Height * pageScale))
                            End If
                            If img.Height > imgPaper.Height Then
                                pageScale = imgPaper.Height / img.Height
                                img = New System.Drawing.Bitmap(img, New System.Drawing.Size(img.Width * pageScale, img.Height * pageScale))
                            End If
                            If (PrintableAreaAutoRotateAndCenterCheckBox.Checked) Then
                                x = (imgPaper.Width - img.Width) / 2
                                y = (imgPaper.Height - img.Height) / 2
                            End If
                            Using g As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(imgPaper)
                                g.DrawImage(img.Clone, x, y, img.Width, img.Height)
                                g.Dispose()
                            End Using
                            If imgPaper.Width > imgPaper.Height Then
                                PreviewPictureBox1.Width = 150 * pageScale
                                PreviewPictureBox1.Height = 150
                            Else
                                PreviewPictureBox1.Width = 150
                                PreviewPictureBox1.Height = 150 * pageScale
                            End If
                            PreviewPictureBox1.BackgroundImage = imgPaper.Clone
                        End If
                    Case 3, 4
                        pnlTile.Show()
                        pnlTile.BringToFront()
                        pageScale = CInt(r.GetPageSizeWithRotation(i).Height) / CInt(r.GetPageSizeWithRotation(i).Width)
                        PreviewPictureBox1.Width = 150
                        PreviewPictureBox1.Height = 150 * pageScale
                        PreviewPictureBox1.BackgroundImage = System.Drawing.Image.FromStream(New System.IO.MemoryStream(frm.A0_LoadImageGhostScript(r, i, CInt(r.GetPageSizeWithRotation(i).Width), CInt(r.GetPageSizeWithRotation(i).Height), False)))
                    Case 5
                        pnlMultiple.Show()
                        pnlMultiple.BringToFront()
                        pageScale = CInt(r.GetPageSizeWithRotation(i).Height) / CInt(r.GetPageSizeWithRotation(i).Width)
                        PreviewPictureBox1.Width = 150
                        PreviewPictureBox1.Height = 150 * pageScale
                        PreviewPictureBox1.BackgroundImage = System.Drawing.Image.FromStream(New System.IO.MemoryStream(frm.A0_LoadImageGhostScript(r, i, CInt(r.GetPageSizeWithRotation(i).Width), CInt(r.GetPageSizeWithRotation(i).Height), False)))
                    Case 6
                        pnlBooklet.Show()
                        pnlBooklet.BringToFront()
                        pageScale = CInt(r.GetPageSizeWithRotation(i).Height) / CInt(r.GetPageSizeWithRotation(i).Width)
                        PreviewPictureBox1.Width = 150
                        PreviewPictureBox1.Height = 150 * pageScale
                        PreviewPictureBox1.BackgroundImage = System.Drawing.Image.FromStream(New System.IO.MemoryStream(frm.A0_LoadImageGhostScript(r, i, CInt(r.GetPageSizeWithRotation(i).Width), CInt(r.GetPageSizeWithRotation(i).Height), False)))
                End Select
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub PreviewTrackbarScale_Scroll(sender As Object, e As EventArgs) Handles PreviewTrackbarScale.Scroll
        Try
            updatePdfReader()
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub PreviewTrackbarScale_MouseUp(sender As Object, e As MouseEventArgs) Handles PreviewTrackbarScale.MouseUp
        tmrScroller1_Fired = True
        tmrScroller1.Enabled = True
    End Sub
    Private Sub PrintRangeTxtRange_TextChanged(sender As Object, e As EventArgs) Handles PrintRangeTxtRange.TextChanged
        tmrScroller1_Fired = True
        tmrScroller1.Enabled = True
    End Sub
    Private Sub PrintRangeTxtRange_Leave(sender As Object, e As EventArgs) Handles PrintRangeTxtRange.Leave
        updatePdfReader()
    End Sub
    Public Sub updatePdfReader()
        Try
            r = frm.pdfReaderDoc.Clone
            If r.NumberOfPages > 0 Then
                If PrintRangeOptPageRange.Checked Then
                    If PrintRangeTxtRange.Text = "" Then
                        PrintRangeTxtRange.Text = "1-" & r.NumberOfPages
                    End If
                    r.SelectPages(PrintRangeTxtRange.Text)
                ElseIf PrintRangeOptAll.Checked Then
                ElseIf PrintRangeOptCurrentPage.Checked Then
                    r.SelectPages(frm.page.ToString())
                ElseIf PrintRangeOptCurrentView.Checked Then
                    r.SelectPages(frm.page.ToString())
                End If
            End If
            lblPageNumOfCount.Text = PreviewTrackbarScale.Value & "/" & r.NumberOfPages & " [" & r.NumberOfPages & "]"
            PreviewTrackbarScale.Maximum = r.NumberOfPages
            PreviewTrackbarScale.Minimum = 1
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub PreviewPictureBox1_Click(sender As Object, e As EventArgs) Handles PreviewPictureBox1.Click
    End Sub
    Private Sub PrintableAreaAutoRotateAndCenterCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles PrintableAreaAutoRotateAndCenterCheckBox.CheckedChanged
        tmrScroller1_Fired = True
        tmrScroller1.Enabled = True
    End Sub
    Private Sub PrintRangeCkReversePages_CheckedChanged(sender As Object, e As EventArgs) Handles PrintRangeCkReversePages.CheckedChanged
        tmrScroller1_Fired = True
        tmrScroller1.Enabled = True
    End Sub
End Class
