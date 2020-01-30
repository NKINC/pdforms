Imports System.Windows.Forms
Imports System.IO
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.Collections.Generic
Public Class dialogDocumentProperties
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 NK-INC.COM All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>
    Public pdfBytesModified() As Byte = Nothing
    Public frm As frmMain = Nothing
    Public fpath As String = ""
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            pdfBytesModified = ModifyDocumentProperties()
            If Not pdfBytesModified Is Nothing Then
                If pdfBytesModified.Length > 0 Then
                    frm.Session = pdfBytesModified.ToArray()
                    Me.DialogResult = System.Windows.Forms.DialogResult.OK
                    Return
                End If
            End If
            Me.DialogResult = System.Windows.Forms.DialogResult.Abort
            Return
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        Finally
            Me.Close()
        End Try
    End Sub
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Public Sub New()
        InitializeComponent()
    End Sub
    Public Sub New(ByRef frmMain1 As frmMain)
        InitializeComponent()
        Try
            frm = frmMain1
            LoadDocumentProperties()
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        End Try
    End Sub
    Public Sub New(ByRef frmMain1 As frmMain, ByVal isVisible As Boolean)
        InitializeComponent()
        Try
            frm = frmMain1
            LoadDocumentProperties()
            If isVisible Then
                frm.ShowDialog(Me)
            Else
            End If
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        End Try
    End Sub
    Public Sub LoadDocumentProperties()
        If frmMain.Session Is Nothing Then Return
        If frmMain.Session.Length <= 0 Then Return
        If frmMain.pdfReaderDoc Is Nothing Then Return
        If String.IsNullOrEmpty(frmMain.fpath & "") Then Return
        Try
            Label54.Text = frmMain.getMegaBytesText(frmMain.Session.Length)
        Catch ex As Exception
            Err.Clear()
        End Try
        Try
            Using r As PdfReader = frmMain.pdfReaderDoc.Clone
                Dim m As New MemoryStream
                Try
                    Label33.Text = " of " & r.NumberOfPages.ToString & ""
                    TextBox11.Text = "1"
                    ComboBox4.SelectedIndex = 0
                    Try
                    Catch exPageMode As Exception
                        Err.Clear()
                    End Try
                    Try
                        If Not r.Catalog.Get(PdfName.OPENACTION) Is Nothing Then
                            If r.Catalog.Get(PdfName.OPENACTION).IsDictionary Or r.Catalog.Get(PdfName.OPENACTION).IsIndirect Then
                                Dim oa As iTextSharp.text.pdf.PdfDictionary = r.Catalog.GetAsDict(PdfName.OPENACTION)
                                Dim i As Integer = 0
                                For i = 0 To oa.GetAsArray(PdfName.D).Size - 1
                                    If oa.GetAsArray(PdfName.D)(i).IsName Or oa.GetAsArray(PdfName.D)(i).IsIndirect Then
                                        Dim pm As String = ""
                                        If Not oa.GetAsArray(PdfName.D)(i) Is Nothing Then
                                            If oa.GetAsArray(PdfName.D)(i).IsName Then
                                                pm = oa.GetAsArray(PdfName.D).GetAsName(i).ToString & ""
                                            End If
                                        End If
                                        Select Case pm & ""
                                            Case PdfName.FITB.ToString
                                                ComboBox4.SelectedIndex = 0
                                            Case PdfName.XYZ.ToString
                                                ComboBox4.SelectedIndex = 1
                                                If Not oa.GetAsArray(PdfName.D)(4) Is Nothing Then
                                                    If oa.GetAsArray(PdfName.D)(4).IsNumber Or oa.GetAsArray(PdfName.D)(4).IsIndirect Then
                                                        Dim scaleFit As Single = CSng(oa.GetAsArray(PdfName.D).GetAsNumber(4).DoubleValue)
                                                        pm = CStr(CSng(scaleFit * 100)) & ""
                                                        If IsNumeric(pm.Replace("%", "")) Then
                                                            If pm.Contains("%") Then
                                                                If ComboBox4.Items.Contains(pm.Replace(".", "").Replace("%", "") & "%") Then
                                                                    ComboBox4.SelectedItem = pm.Replace(".", "").Replace("%", "") & "%"
                                                                Else
                                                                    ComboBox4.Items.Add(pm.Replace(".", "").Replace("%", "") & "%")
                                                                    ComboBox4.SelectedItem = pm.Replace(".", "").Replace("%", "") & "%"
                                                                End If
                                                            ElseIf pm.Contains("."c) Then
                                                                If ComboBox4.Items.Contains(pm.Replace(".", "").Replace("%", "") & "%") Then
                                                                    ComboBox4.SelectedItem = pm.Replace(".", "").Replace("%", "") & "%"
                                                                Else
                                                                    ComboBox4.Items.Add(pm.Replace(".", "").Replace("%", "") & "%")
                                                                    ComboBox4.SelectedItem = pm.Replace(".", "").Replace("%", "") & "%"
                                                                End If
                                                            Else
                                                                If ComboBox4.Items.Contains(pm.Replace(".", "").Replace("%", "") & "%") Then
                                                                    ComboBox4.SelectedItem = pm.Replace(".", "").Replace("%", "") & "%"
                                                                Else
                                                                    ComboBox4.Items.Add(pm.Replace(".", "").Replace("%", "") & "%")
                                                                    ComboBox4.SelectedItem = pm.Replace(".", "").Replace("%", "") & "%"
                                                                End If
                                                            End If
                                                        End If
                                                    End If
                                                End If
                                            Case PdfName.FIT.ToString
                                                ComboBox4.SelectedIndex = 2
                                            Case PdfName.FITH.ToString
                                                ComboBox4.SelectedIndex = 3
                                            Case PdfName.FITV.ToString
                                                ComboBox4.SelectedIndex = 4
                                            Case PdfName.FIT.ToString
                                                ComboBox4.SelectedIndex = 5
                                            Case Else
                                                If IsNumeric(pm.Replace("%", "")) Then
                                                    If pm.Contains("%") Then
                                                        If ComboBox4.Items.Contains(pm.Replace(".", "").Replace("%", "") & "%") Then
                                                            ComboBox4.SelectedItem = pm.Replace(".", "").Replace("%", "") & "%"
                                                        Else
                                                            ComboBox4.Items.Add(pm.Replace(".", "").Replace("%", "") & "%")
                                                            ComboBox4.SelectedItem = pm.Replace(".", "").Replace("%", "") & "%"
                                                        End If
                                                    ElseIf pm.Contains("."c) Then
                                                        If ComboBox4.Items.Contains(pm.Replace(".", "").Replace("%", "") & "%") Then
                                                            ComboBox4.SelectedItem = pm.Replace(".", "").Replace("%", "") & "%"
                                                        Else
                                                            ComboBox4.Items.Add(pm.Replace(".", "").Replace("%", "") & "%")
                                                            ComboBox4.SelectedItem = pm.Replace(".", "").Replace("%", "") & "%"
                                                        End If
                                                    Else
                                                        If ComboBox4.Items.Contains(pm.Replace(".", "").Replace("%", "") & "%") Then
                                                            ComboBox4.SelectedItem = pm.Replace(".", "").Replace("%", "") & "%"
                                                        Else
                                                            ComboBox4.Items.Add(pm.Replace(".", "").Replace("%", "") & "%")
                                                            ComboBox4.SelectedItem = pm.Replace(".", "").Replace("%", "") & "%"
                                                        End If
                                                    End If
                                                End If
                                        End Select
                                        If oa.GetAsArray(PdfName.D)(i).IsIndirect And i = 0 Then
                                            Dim dl As PdfIndirectReference = oa.GetAsArray(PdfName.D).GetAsIndirectObject(i)
                                            Dim pg As Integer = clsLinks.findPageIndex(dl.Number, r.Catalog.GetAsDict(PdfName.PAGES))
                                            If pg >= 0 Then
                                                TextBox11.Text = CStr(CInt(pg + 1)).ToString
                                            End If
                                        End If
                                    ElseIf oa.GetAsArray(PdfName.D)(i).IsIndirect And i = 0 Then
                                        Dim dl As PdfIndirectReference = oa.GetAsArray(PdfName.D).GetAsIndirectObject(i)
                                        Dim pg As Integer = clsLinks.findPageIndex(dl.Number, r.Catalog.GetAsDict(PdfName.PAGES))
                                        If pg >= 0 Then
                                            TextBox11.Text = CStr(CInt(pg + 1)).ToString
                                        End If
                                    End If
                                Next
                            End If
                        End If
                    Catch exSub As Exception
                        frm.TimeStampAdd(exSub)
                    End Try
                    ComboBox2.SelectedIndex = 0
                    CheckBox3.Checked = False
                    Try
                        If Not r.Catalog.Get(PdfName.PAGEMODE) Is Nothing Then
                            If Not r.Catalog.GetAsName(PdfName.PAGEMODE) Is Nothing Then
                                Dim nm As PdfName = r.Catalog.GetAsName(PdfName.PAGEMODE)
                                Dim nmStr As String = nm.ToString()
                                Select Case nmStr
                                    Case PdfName.USENONE.ToString
                                        ComboBox2.SelectedIndex = 0
                                    Case PdfName.USEOUTLINES.ToString
                                        ComboBox2.SelectedIndex = 1
                                    Case PdfName.USETHUMBS.ToString
                                        ComboBox2.SelectedIndex = 2
                                    Case PdfName.USEATTACHMENTS.ToString
                                        ComboBox2.SelectedIndex = 3
                                    Case PdfName.USEOC.ToString
                                        ComboBox2.SelectedIndex = 4
                                    Case PdfName.FULLSCREEN.ToString()
                                        CheckBox3.Checked = True
                                        If Not r.Catalog.Get(PdfName.VIEWERPREFERENCES) Is Nothing Then
                                            If r.Catalog.Get(PdfName.VIEWERPREFERENCES).IsDictionary Or r.Catalog.Get(PdfName.VIEWERPREFERENCES).IsIndirect Then
                                                Dim vp As PdfDictionary = r.Catalog.GetAsDict(PdfName.VIEWERPREFERENCES)
                                                If Not vp.Get(PdfName.NONFULLSCREENPAGEMODE) Is Nothing Then
                                                    If vp.Get(PdfName.NONFULLSCREENPAGEMODE).IsName Then
                                                        CheckBox3.Checked = True
                                                        Dim nmFullScreen As PdfName = vp.GetAsName(PdfName.NONFULLSCREENPAGEMODE)
                                                        Dim nmFullScreenStr As String = nmFullScreen.ToString() & ""
                                                        Select Case nmFullScreenStr & ""
                                                            Case PdfName.USENONE.ToString
                                                                ComboBox2.SelectedIndex = 0
                                                            Case PdfName.USEOUTLINES.ToString
                                                                ComboBox2.SelectedIndex = 1
                                                            Case PdfName.USETHUMBS.ToString
                                                                ComboBox2.SelectedIndex = 2
                                                            Case PdfName.USEATTACHMENTS.ToString
                                                                ComboBox2.SelectedIndex = 3
                                                            Case PdfName.USEOC.ToString
                                                                ComboBox2.SelectedIndex = 4
                                                            Case Else
                                                                ComboBox2.SelectedIndex = 0
                                                        End Select
                                                    End If
                                                End If
                                            End If
                                        End If
                                    Case Else
                                        ComboBox2.SelectedIndex = 0
                                End Select
                            End If
                        End If
                    Catch exSub As Exception
                        frm.TimeStampAdd(exSub)
                    End Try
                    ComboBox5.SelectedIndex = 0
                    CheckBox1.Checked = False
                    CheckBox2.Checked = False
                    CheckBox4.Checked = False
                    CheckBox5.Checked = False
                    CheckBox6.Checked = False
                    Try
                        If Not r.Catalog.Get(PdfName.VIEWERPREFERENCES) Is Nothing Then
                            If r.Catalog.Get(PdfName.VIEWERPREFERENCES).IsDictionary Or r.Catalog.Get(PdfName.VIEWERPREFERENCES).IsIndirect Then
                                Dim vp As PdfDictionary = r.Catalog.GetAsDict(PdfName.VIEWERPREFERENCES)
                                Try
                                    If Not vp.Get(PdfName.DISPLAYDOCTITLE) Is Nothing Then
                                        If vp.Get(PdfName.DISPLAYDOCTITLE).IsBoolean Or vp.Get(PdfName.DISPLAYDOCTITLE).IsIndirect Then
                                            Dim ddt As PdfBoolean = vp.GetAsBoolean(PdfName.DISPLAYDOCTITLE)
                                            If ddt.BooleanValue Then
                                                ComboBox5.SelectedIndex = 1
                                            Else
                                                ComboBox5.SelectedIndex = 0
                                            End If
                                        End If
                                    End If
                                Catch exSub1 As Exception
                                    If frm.debugMode Then Throw exSub1 Else Err.Clear()
                                End Try
                                Try
                                    If Not vp.Get(PdfName.FITWINDOW) Is Nothing Then
                                        If vp.Get(PdfName.FITWINDOW).IsBoolean Or vp.Get(PdfName.FITWINDOW).IsIndirect Then
                                            Dim bv As PdfBoolean = vp.GetAsBoolean(PdfName.FITWINDOW)
                                            CheckBox1.Checked = bv.BooleanValue
                                        End If
                                    End If
                                Catch exSub1 As Exception
                                    If frm.debugMode Then Throw exSub1 Else Err.Clear()
                                End Try
                                Try
                                    If Not vp.Get(PdfName.CENTERWINDOW) Is Nothing Then
                                        If vp.Get(PdfName.CENTERWINDOW).IsBoolean Or vp.Get(PdfName.CENTERWINDOW).IsIndirect Then
                                            Dim bv As PdfBoolean = vp.GetAsBoolean(PdfName.CENTERWINDOW)
                                            CheckBox2.Checked = bv.BooleanValue
                                        End If
                                    End If
                                Catch exSub1 As Exception
                                    If frm.debugMode Then Throw exSub1 Else Err.Clear()
                                End Try
                                Try
                                    If Not vp.Get(PdfName.HIDEMENUBAR) Is Nothing Then
                                        If vp.Get(PdfName.HIDEMENUBAR).IsBoolean Or vp.Get(PdfName.HIDEMENUBAR).IsIndirect Then
                                            Dim bv As PdfBoolean = vp.GetAsBoolean(PdfName.HIDEMENUBAR)
                                            CheckBox4.Checked = bv.BooleanValue
                                        End If
                                    End If
                                Catch exSub1 As Exception
                                    If frm.debugMode Then Throw exSub1 Else Err.Clear()
                                End Try
                                Try
                                    If Not vp.Get(PdfName.HIDETOOLBAR) Is Nothing Then
                                        If vp.Get(PdfName.HIDETOOLBAR).IsBoolean Or vp.Get(PdfName.HIDETOOLBAR).IsIndirect Then
                                            Dim bv As PdfBoolean = vp.GetAsBoolean(PdfName.HIDETOOLBAR)
                                            CheckBox5.Checked = bv.BooleanValue
                                        End If
                                    End If
                                Catch exSub1 As Exception
                                    If frm.debugMode Then Throw exSub1 Else Err.Clear()
                                End Try
                                Try
                                    If Not vp.Get(PdfName.HIDEWINDOWUI) Is Nothing Then
                                        If vp.Get(PdfName.HIDEWINDOWUI).IsBoolean Or vp.Get(PdfName.HIDEWINDOWUI).IsIndirect Then
                                            Dim bv As PdfBoolean = vp.GetAsBoolean(PdfName.HIDEWINDOWUI)
                                            CheckBox6.Checked = bv.BooleanValue
                                        End If
                                    End If
                                Catch exSub1 As Exception
                                    If frm.debugMode Then Throw exSub1 Else Err.Clear()
                                End Try
                            End If
                        End If
                    Catch exSub As Exception
                        frm.TimeStampAdd(exSub)
                    End Try
                Catch exIntialDisplay As Exception
                    If frm.debugMode Then Throw exIntialDisplay Else Err.Clear()
                End Try
                Try
                    Dim info As System.Collections.Generic.Dictionary(Of String, String) = r.Info
                    If Not info Is Nothing Then
                        If info.Count > 0 Then
                            Try
                                If info.ContainsKey("Producer") Then
                                    TextBox10.Text = info("Producer") & ""
                                Else
                                    TextBox10.Text = ""
                                End If
                            Catch exSub1 As Exception
                                If frm.debugMode Then Throw exSub1 Else Err.Clear()
                            End Try
                            Try
                                If info.ContainsKey(Meta.AUTHOR) Then
                                    TextBox2.Text = info(Meta.AUTHOR) & ""
                                Else
                                    TextBox2.Text = ""
                                End If
                            Catch exSub1 As Exception
                                If frm.debugMode Then Throw exSub1 Else Err.Clear()
                            End Try
                            Try
                                If info.ContainsKey(Meta.TITLE) Then
                                    TextBox1.Text = info(Meta.TITLE) & ""
                                Else
                                    TextBox1.Text = ""
                                End If
                            Catch exSub1 As Exception
                                If frm.debugMode Then Throw exSub1 Else Err.Clear()
                            End Try
                            Try
                                If info.ContainsKey(Meta.SUBJECT) Then
                                    TextBox3.Text = info(Meta.SUBJECT) & ""
                                Else
                                    TextBox3.Text = ""
                                End If
                            Catch exSub1 As Exception
                                If frm.debugMode Then Throw exSub1 Else Err.Clear()
                            End Try
                            Try
                                If info.ContainsKey(Meta.KEYWORDS) Then
                                    TextBox4.Text = info(Meta.KEYWORDS) & ""
                                Else
                                    TextBox4.Text = ""
                                End If
                            Catch exSub1 As Exception
                                If frm.debugMode Then Throw exSub1 Else Err.Clear()
                            End Try
                            Dim fileInfo1 As FileInfo = Nothing
                            Try
                                Label47.Text = Path.GetFileName(frmMain.fpath & "")
                                If frmMain.IsValidUrl(frmMain.fpath) Then
                                    Label53.Text = frmMain.fpath.ToString.Substring(0, frmMain.fpath.ToString.LastIndexOf("/"))
                                    If info.ContainsKey(Meta.CREATIONDATE) Then
                                        Label48.Text = info(Meta.CREATIONDATE) & ""
                                    Else
                                        Label48.Text = "unknown"
                                    End If
                                    Label49.Text = "unknown"
                                Else
                                    fileInfo1 = New FileInfo(frmMain.fpath & "")
                                    Label49.Text = fileInfo1.LastWriteTime.ToString()
                                    If info.ContainsKey(Meta.CREATIONDATE) Then
                                        Label48.Text = info(Meta.CREATIONDATE) & ""
                                    Else
                                        Label48.Text = fileInfo1.CreationTime.ToString() & ""
                                    End If
                                End If
                            Catch exSub As Exception
                                frm.TimeStampAdd(exSub)
                            Finally
                                If Not fileInfo1 Is Nothing Then
                                    fileInfo1 = Nothing
                                End If
                            End Try
                            Try
                                If info.ContainsKey("Creator") Then
                                    Label50.Text = info("Creator") & ""
                                Else
                                    Label50.Text = ""
                                End If
                            Catch exSub1 As Exception
                                If frm.debugMode Then Throw exSub1 Else Err.Clear()
                            End Try
                        End If
                    End If
                Catch exSub As Exception
                    frm.TimeStampAdd(exSub)
                End Try
                Label57.Text = "False"
                Try
                    If r.IsTagged Then
                        Label57.Text = "True"
                    Else
                        Label57.Text = "False"
                    End If
                Catch exSub As Exception
                    frm.TimeStampAdd(exSub)
                End Try
                Dim pl As String = ""
                ComboBox3.SelectedIndex = 0
                Try
                    If Not r.Catalog.Get(PdfName.PAGELAYOUT) Is Nothing Then
                        If r.Catalog.Get(PdfName.PAGELAYOUT).IsName Then
                            pl = r.Catalog.GetAsName(PdfName.PAGELAYOUT).ToString & ""
                        End If
                    End If
                    Select Case pl
                        Case PdfName.DEFAULT.ToString
                            ComboBox3.SelectedIndex = 0
                        Case PdfName.SINGLEPAGE.ToString
                            ComboBox3.SelectedIndex = 1
                        Case PdfName.ONECOLUMN.ToString
                            ComboBox3.SelectedIndex = 2
                        Case PdfName.TWOPAGELEFT.ToString
                            ComboBox3.SelectedIndex = 3
                        Case PdfName.TWOCOLUMNLEFT.ToString
                            ComboBox3.SelectedIndex = 4
                        Case PdfName.TWOPAGERIGHT.ToString
                            ComboBox3.SelectedIndex = 5
                        Case PdfName.TWOCOLUMNRIGHT.ToString
                            ComboBox3.SelectedIndex = 6
                        Case Else
                            ComboBox3.SelectedIndex = 0
                    End Select
                Catch exSub As Exception
                    frm.TimeStampAdd(exSub)
                End Try
                Try
                    If frmMain.btnPage.SelectedIndex >= 0 Then
                        Label55.Text = CStr(CSng(r.GetPageSizeWithRotation(frmMain.btnPage.SelectedIndex + 1).Width / 72).ToString("#,##0.00").TrimEnd("0"c).TrimEnd("0"c).TrimEnd("."c) & """ x " & CStr(CSng(r.GetPageSizeWithRotation(frmMain.btnPage.SelectedIndex + 1).Height / 72).ToString("#,##0.00").TrimEnd("0"c).TrimEnd("0"c).TrimEnd("."c) & """"))
                        Label56.Text = CStr(r.NumberOfPages.ToString & "")
                    Else
                        Label55.Text = ""
                        Label56.Text = CStr(r.NumberOfPages.ToString & "")
                    End If
                Catch exSub As Exception
                    frm.TimeStampAdd(exSub)
                End Try
                Try
                    Select Case CStr(r.PdfVersion)
                        Case "3"
                            Label52.Text = "1." & CStr(r.PdfVersion) & " (Acrobat " & CInt(CInt(r.PdfVersion.ToString) + 1) & ".x)"
                        Case "4"
                            Label52.Text = "1." & CStr(r.PdfVersion) & " (Acrobat " & CInt(CInt(r.PdfVersion.ToString) + 1) & ".x)"
                        Case "5"
                            Label52.Text = "1." & CStr(r.PdfVersion) & " (Acrobat " & CInt(CInt(r.PdfVersion.ToString) + 1) & ".x)"
                        Case "6"
                            Label52.Text = "1." & CStr(r.PdfVersion) & " (Acrobat " & CInt(CInt(r.PdfVersion.ToString) + 1) & ".x)"
                        Case "7"
                            Label52.Text = "1." & CStr(r.PdfVersion) & " (Acrobat " & CInt(CInt(r.PdfVersion.ToString) + 1) & ".x)"
                        Case "8"
                            Label52.Text = "1." & CStr(r.PdfVersion) & " (Acrobat " & CInt(CInt(r.PdfVersion.ToString) + 1) & ".x)"
                        Case Else
                            Label52.Text = "1." & CStr(r.PdfVersion) & " (Acrobat " & CInt(CInt(r.PdfVersion.ToString) + 1) & ".x)"
                    End Select
                Catch exSub As Exception
                    frm.TimeStampAdd(exSub)
                End Try
                If Not r Is Nothing Then
                    r.Close()
                    r.Dispose()
                End If
            End Using
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        Finally
        End Try
    End Sub
    Public Property frmMain() As frmMain
        Get
            If Not frm Is Nothing Then
                Return frm
            Else
                If frm Is Nothing Then
                    If Me.Owner.GetType Is GetType(frmMain) Then
                        frm = DirectCast(Me.Owner, frmMain)
                        Return frm
                    End If
                End If
                Return Nothing
            End If
        End Get
        Set(ByVal value As frmMain)
            frm = value
        End Set
    End Property
    Public Function ModifyDocumentProperties() As Byte()
        Try
            If frmMain.Session Is Nothing Then Return Nothing
            If frmMain.Session.Length <= 0 Then Return Nothing
            If frmMain.pdfReaderDoc Is Nothing Then Return Nothing
            If String.IsNullOrEmpty(frmMain.fpath & "") Then Return Nothing
            Dim magnify As PdfDestination = Nothing
            Dim pageNumberToOpenTo As Integer = CInt(TextBox11.Text)
            Dim pageMode As Integer = 0
            Dim pageModeExtras As Integer = 0
            Dim pageLayout As Integer = 0
            Dim fileInfo1 As New FileInfo(frmMain.fpath & "")
            Try
                Using r As PdfReader = frmMain.pdfReaderDoc.Clone
                    Dim m As New MemoryStream
                    Using s As PdfStamper = New PdfStamper(r, m)
                        Try
                            Try
                            Catch exPageMode As Exception
                                Err.Clear()
                            End Try
                            Try
                                If CheckBox3.Checked Then
                                    pageModeExtras += PdfWriter.PageModeFullScreen
                                    Select Case ComboBox2.SelectedIndex
                                        Case 0
                                            s.AddViewerPreference(PdfName.NONFULLSCREENPAGEMODE, PdfName.USENONE)
                                        Case 1
                                            s.AddViewerPreference(PdfName.NONFULLSCREENPAGEMODE, PdfName.USEOUTLINES)
                                        Case 2
                                            s.AddViewerPreference(PdfName.NONFULLSCREENPAGEMODE, PdfName.USETHUMBS)
                                        Case 3
                                            s.AddViewerPreference(PdfName.NONFULLSCREENPAGEMODE, PdfName.USEATTACHMENTS)
                                        Case 4
                                            s.AddViewerPreference(PdfName.NONFULLSCREENPAGEMODE, PdfName.USEOC)
                                        Case Else
                                            s.AddViewerPreference(PdfName.NONFULLSCREENPAGEMODE, PdfName.USENONE)
                                    End Select
                                Else
                                    Select Case ComboBox2.SelectedIndex
                                        Case 0
                                            pageMode = PdfWriter.PageModeUseNone
                                        Case 1
                                            pageMode = PdfWriter.PageModeUseOutlines
                                        Case 2
                                            pageMode = PdfWriter.PageModeUseThumbs
                                        Case 3
                                            pageMode = PdfWriter.PageModeUseAttachments
                                        Case 4
                                            pageMode = PdfWriter.PageModeUseOC
                                        Case Else
                                            pageMode = PdfWriter.PageModeUseNone
                                    End Select
                                End If
                                Select Case ComboBox3.SelectedIndex
                                    Case 0
                                        pageLayout = 0
                                    Case 1
                                        pageLayout = PdfWriter.PageLayoutSinglePage
                                    Case 2
                                        pageLayout = PdfWriter.PageLayoutOneColumn
                                    Case 3
                                        pageLayout = PdfWriter.PageLayoutTwoPageLeft
                                    Case 4
                                        pageLayout = PdfWriter.PageLayoutTwoColumnLeft
                                    Case 5
                                        pageLayout = PdfWriter.PageLayoutTwoPageRight
                                    Case 6
                                        pageLayout = PdfWriter.PageLayoutTwoColumnRight
                                    Case Else
                                        pageLayout = 0
                                End Select
                                If CheckBox3.Checked Then
                                End If
                                If CheckBox1.Checked Then
                                End If
                                If CheckBox2.Checked Then
                                End If
                                Try
                                    If True = True Then
                                        If True = True Then
                                            If ComboBox5.SelectedIndex = 1 Then
                                                s.AddViewerPreference(PdfName.DISPLAYDOCTITLE, New PdfBoolean(True))
                                            Else
                                                s.AddViewerPreference(PdfName.DISPLAYDOCTITLE, New PdfBoolean(False))
                                            End If
                                            Try
                                                If CheckBox1.Checked Then
                                                    s.AddViewerPreference(PdfName.FITWINDOW, New PdfBoolean(True))
                                                Else
                                                    s.AddViewerPreference(PdfName.FITWINDOW, New PdfBoolean(False))
                                                End If
                                            Catch exSub1 As Exception
                                                If frm.debugMode Then Throw exSub1 Else Err.Clear()
                                            End Try
                                            Try
                                                If CheckBox2.Checked Then
                                                    s.AddViewerPreference(PdfName.CENTERWINDOW, New PdfBoolean(True))
                                                Else
                                                    s.AddViewerPreference(PdfName.CENTERWINDOW, New PdfBoolean(False))
                                                End If
                                            Catch exSub1 As Exception
                                                If frm.debugMode Then Throw exSub1 Else Err.Clear()
                                            End Try
                                            Try
                                                If CheckBox3.Checked Then
                                                End If
                                            Catch exSub1 As Exception
                                                If frm.debugMode Then Throw exSub1 Else Err.Clear()
                                            End Try
                                            Try
                                                If CheckBox4.Checked Then
                                                    s.AddViewerPreference(PdfName.HIDEMENUBAR, New PdfBoolean(True))
                                                Else
                                                    s.AddViewerPreference(PdfName.HIDEMENUBAR, New PdfBoolean(False))
                                                End If
                                            Catch exSub1 As Exception
                                                If frm.debugMode Then Throw exSub1 Else Err.Clear()
                                            End Try
                                            Try
                                                If CheckBox5.Checked Then
                                                    s.AddViewerPreference(PdfName.HIDETOOLBAR, New PdfBoolean(True))
                                                Else
                                                    s.AddViewerPreference(PdfName.HIDETOOLBAR, New PdfBoolean(False))
                                                End If
                                            Catch exSub1 As Exception
                                                If frm.debugMode Then Throw exSub1 Else Err.Clear()
                                            End Try
                                            Try
                                                If CheckBox6.Checked Then
                                                    s.AddViewerPreference(PdfName.HIDEWINDOWUI, New PdfBoolean(True))
                                                Else
                                                    s.AddViewerPreference(PdfName.HIDEWINDOWUI, New PdfBoolean(False))
                                                End If
                                            Catch exSub1 As Exception
                                                If frm.debugMode Then Throw exSub1 Else Err.Clear()
                                            End Try
                                        End If
                                    End If
                                Catch exSub As Exception
                                    frm.TimeStampAdd(exSub)
                                End Try
                            Catch exIntialDisplay As Exception
                                If frm.debugMode Then Throw exIntialDisplay Else Err.Clear()
                            End Try
                            s.ViewerPreferences = pageMode + pageLayout + pageModeExtras
                            Try
                                Select Case ComboBox4.SelectedIndex
                                    Case 0
                                        magnify = New PdfDestination(PdfDestination.FITB)
                                    Case 1
                                        magnify = New PdfDestination(PdfDestination.XYZ, -1, -1, 1)
                                    Case 2
                                        magnify = New PdfDestination(PdfDestination.FIT)
                                    Case 3
                                        magnify = New PdfDestination(PdfDestination.FITH, r.GetPageSizeWithRotation(pageNumberToOpenTo).Top)
                                    Case 4
                                        magnify = New PdfDestination(PdfDestination.FITV, r.GetPageSizeWithRotation(pageNumberToOpenTo).Top)
                                    Case 5
                                        magnify = New PdfDestination(PdfDestination.FIT, r.GetPageSizeWithRotation(pageNumberToOpenTo).Top)
                                    Case Else
                                        magnify = New PdfDestination(PdfDestination.XYZ, -1, -1, CSng(CSng(ComboBox4.SelectedItem.ToString.Replace("%", "").ToString()) / 100))
                                End Select
                                Dim zoom As PdfAction = PdfAction.GotoLocalPage(CInt(pageNumberToOpenTo), magnify, s.Writer)
                                s.Writer.SetOpenAction(zoom)
                            Catch ex As Exception
                                frm.TimeStampAdd(ex)
                            End Try
                        Catch exSub As Exception
                            frm.TimeStampAdd(exSub)
                        End Try
                        Dim info As New System.Collections.Generic.Dictionary(Of String, String)
                        Try
                            If Not r.Info Is Nothing Then
                                If r.Info.Count > 0 Then
                                    info = r.Info
                                End If
                            End If
                            If info.ContainsKey(Meta.TITLE) Then
                                info(Meta.TITLE) = TextBox1.Text & ""
                            Else
                                info.Add(Meta.TITLE, TextBox1.Text & "")
                            End If
                            If info.ContainsKey(Meta.SUBJECT) Then
                                info(Meta.SUBJECT) = TextBox3.Text & ""
                            Else
                                info.Add(Meta.SUBJECT, TextBox3.Text & "")
                            End If
                            If info.ContainsKey(Meta.KEYWORDS) Then
                                info(Meta.KEYWORDS) = TextBox4.Text & ""
                            Else
                                info.Add(Meta.KEYWORDS, TextBox4.Text & "")
                            End If
                            If info.ContainsKey(Meta.AUTHOR) Then
                                info(Meta.AUTHOR) = TextBox2.Text & ""
                            Else
                                info.Add(Meta.AUTHOR, TextBox2.Text & "")
                            End If
                            If info.ContainsKey("Creator") Then
                                info("Creator") = Label50.Text & ""
                            Else
                                info.Add("Creator", Label50.Text & "")
                            End If
                        Catch exSub As Exception
                            frm.TimeStampAdd(exSub)
                        Finally
                            s.MoreInfo = info
                        End Try
                        If Not r Is Nothing Then
                            s.Writer.CloseStream = False
                            s.Close()
                            r.Close()
                            r.Dispose()
                            pdfBytesModified = m.ToArray
                        End If
                    End Using
                End Using
            Catch ex As Exception
                If frm.debugMode Then Throw ex Else Err.Clear()
            Finally
                fileInfo1 = Nothing
            End Try
            Return pdfBytesModified
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        End Try
        Return Nothing
    End Function
    Private Sub TextBox10_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox10.MouseDoubleClick
    End Sub
    Private Sub TextBox10_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox10.TextChanged
    End Sub
    Private Sub Label53_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label53.Click
    End Sub
    Private Sub Label53_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Label53.MouseDoubleClick
        Try
            If Not String.IsNullOrEmpty(Label53.Text & "") Then
                If Directory.Exists(Label53.Text & "") Then
                    Process.Start(CStr(Label53.Text & ""))
                ElseIf frmMain.IsValidUrl(Label53.Text & "") Then
                    Process.Start(Label53.Text & "")
                End If
            End If
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        End Try
    End Sub
    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub TabPage1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage1.Click
    End Sub
    Private Sub dlgDocumentProperties_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            If Not Me.DialogResult = System.Windows.Forms.DialogResult.OK And Not Me.DialogResult = System.Windows.Forms.DialogResult.Cancel Then
                Me.DialogResult = System.Windows.Forms.DialogResult.Abort
            End If
            Return
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        End Try
    End Sub
    Private Sub dialogDocumentProperties_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Using frmsecurityPasswrord As New dialogSecurityPassword(Me.frm)
                frmsecurityPasswrord.LoadPDFEncryptionPanel(frm.Session, False)
                Select Case frmsecurityPasswrord.ShowDialog(Me)
                    Case Windows.Forms.DialogResult.Yes, Windows.Forms.DialogResult.OK
                    Case Else
                End Select
                frmsecurityPasswrord.blnCloseForm = True
                frmsecurityPasswrord.Close()
            End Using
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        Finally
            Me.frm.Hide()
            Me.Show()
            Me.BringToFront()
        End Try
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Select Case ComboBox1.SelectedIndex
            Case 0
                Button1.Enabled = False
            Case Else
                Button1.Enabled = True
        End Select
    End Sub
End Class
