Imports iTextSharp.text.pdf
Imports System.IO
Public Class clsAutocomplete
    Inherits ListBox
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 NK-INC.COM All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>

    Public WithEvents TextBoxField As TextBox
    Public fieldNames As New List(Of String)
    Public TextFieldTabIndexOriginal As Integer = 0
    Public AddAllPDFFields As Boolean = False
    Public AddAllFormImages As Boolean = False
    Public AddFormFields As Boolean = False
    Public prefix As String = "{"
    Public postfix As String = "}"

    Public frmMain1 As frmMain = Nothing
    Public Sub SendKeyString(ByVal strng As String)

        SendKeys.Send(strng)
    End Sub
    Public Property Autocomplete_Width() As Integer
        Get
            Return Me.Width
        End Get
        Set(ByVal value As Integer)
            Me.Width = value
        End Set
    End Property
    Public Property Autocomplete_Height() As Integer
        Get
            Return Me.Height
        End Get
        Set(ByVal value As Integer)
            Me.Height = value
        End Set
    End Property
    Public Property Autocomplete_Left() As Integer
        Get
            Return Me.Left
        End Get
        Set(ByVal value As Integer)
            Me.Left = value
        End Set
    End Property
    Public Property Autocomplete_Top() As Integer
        Get
            Return Me.Top
        End Get
        Set(ByVal value As Integer)
            Me.Top = value
        End Set
    End Property
    Public Property Autocomplete_Position() As Point
        Get
            Return Me.Location
        End Get
        Set(ByVal value As Point)
            Me.Location = value
        End Set
    End Property
    Public Sub New(ByVal tb As TextBox)
        AddHandler tb.KeyDown, AddressOf TextBoxField_KeyDown
        AddHandler tb.GotFocus, AddressOf TextBoxField_GotFocus
        TextBoxField = tb
        TextFieldTabIndexOriginal = tb.TabIndex
        Me.TabIndex = TextFieldTabIndexOriginal + 1
        Me.Hide()
        TextBoxField.Parent.Controls.Add(Me)
    End Sub
    Public Sub New(ByVal tb As TextBox, ByVal fieldNamesArray As String())
        fieldNames.Clear()
        fieldNames.AddRange(fieldNamesArray.ToArray)
        AddHandler tb.KeyDown, AddressOf TextBoxField_KeyDown
        AddHandler tb.GotFocus, AddressOf TextBoxField_GotFocus
        AddHandler tb.LostFocus, AddressOf TextBoxField_LostFocus
        TextBoxField = tb
        TextFieldTabIndexOriginal = tb.TabIndex
        Me.TabIndex = TextFieldTabIndexOriginal + 1
        Me.Hide()
        TextBoxField.Parent.Controls.Add(Me)
    End Sub
    Public Sub New(ByVal tb As TextBox, ByVal fieldNamesArray As String(), ByVal addAllFieldsOptionToList As Boolean, Optional ByVal prefix1 As String = "{", Optional ByVal postfix1 As String = "}")
        prefix = prefix1
        postfix = postfix1
        AddAllPDFFields = addAllFieldsOptionToList
        fieldNames.Clear()
        fieldNames.AddRange(fieldNamesArray.ToArray)
        AddHandler tb.KeyDown, AddressOf TextBoxField_KeyDown
        AddHandler tb.GotFocus, AddressOf TextBoxField_GotFocus
        AddHandler tb.LostFocus, AddressOf TextBoxField_LostFocus
        TextBoxField = tb
        TextFieldTabIndexOriginal = tb.TabIndex
        Me.TabIndex = TextFieldTabIndexOriginal + 1
        Me.Hide()
        TextBoxField.Parent.Controls.Add(Me)
    End Sub
    Public Sub New(ByVal tb As TextBox, ByVal fieldNamesArray As String(), ByVal addAllFieldsOptionToList As Boolean, addAllPageImages As Boolean, frm As frmMain, Optional ByVal prefix1 As String = "{", Optional ByVal postfix1 As String = "}")
        prefix = prefix1
        AddAllFormImages = addAllPageImages
        frmMain1 = frm
        postfix = postfix1
        AddAllPDFFields = addAllFieldsOptionToList
        fieldNames.Clear()
        fieldNames.AddRange(fieldNamesArray.ToArray)
        AddHandler tb.KeyDown, AddressOf TextBoxField_KeyDown
        AddHandler tb.GotFocus, AddressOf TextBoxField_GotFocus
        AddHandler tb.LostFocus, AddressOf TextBoxField_LostFocus
        TextBoxField = tb
        TextFieldTabIndexOriginal = tb.TabIndex
        Me.TabIndex = TextFieldTabIndexOriginal + 1
        Me.Hide()
        TextBoxField.Parent.Controls.Add(Me)
    End Sub
    Public Sub New(ByVal tb As TextBox, ByVal fieldNamesArray As String(), ByVal addAllFieldsOptionToList As Boolean, addAllPageImages As Boolean, addFormFieldsToPageImages As Boolean, frm As frmMain, Optional ByVal prefix1 As String = "{", Optional ByVal postfix1 As String = "}")
        prefix = prefix1
        AddAllFormImages = addAllPageImages
        frmMain1 = frm
        postfix = postfix1
        AddAllPDFFields = addAllFieldsOptionToList
        AddFormFields = addFormFieldsToPageImages
        fieldNames.Clear()
        fieldNames.AddRange(fieldNamesArray.ToArray)
        AddHandler tb.KeyDown, AddressOf TextBoxField_KeyDown
        AddHandler tb.GotFocus, AddressOf TextBoxField_GotFocus
        AddHandler tb.LostFocus, AddressOf TextBoxField_LostFocus
        TextBoxField = tb
        TextFieldTabIndexOriginal = tb.TabIndex
        Me.TabIndex = TextFieldTabIndexOriginal + 1
        Me.Hide()
        TextBoxField.Parent.Controls.Add(Me)
    End Sub
    Public Sub closeDispose()
        Try
            RemoveHandler TextBoxField.KeyDown, AddressOf TextBoxField_KeyDown
        Catch ex As Exception
            Err.Clear()
        End Try
        Try
            RemoveHandler TextBoxField.GotFocus, AddressOf TextBoxField_GotFocus
        Catch ex As Exception
            Err.Clear()
        End Try
        Try
            RemoveHandler TextBoxField.LostFocus, AddressOf TextBoxField_LostFocus
        Catch ex As Exception
            Err.Clear()
        End Try
        Try
            fieldNames.Clear()
            fieldNames = Nothing
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public Sub New()
        Me.Hide()
    End Sub
    Public Sub TextBoxField_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    End Sub
    Public Sub TextBoxField_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not Me.Focused Then
            Me.Hide()
            Return
        End If
    End Sub
    Public Sub TextBoxField_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            Dim pt As Point = Cursor.Position ' New Point(Cursor.Position.X, Cursor.Position.Y)
            If e.KeyCode = Keys.Escape Then
                e.SuppressKeyPress = True
                Me.Hide()
                Exit Sub
            End If
            If (e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down) And Me.Visible Then
                Me.Focus()
                Exit Sub
            End If
            If (e.KeyCode = Keys.Tab And Me.Visible) Then
                e.SuppressKeyPress = True
                Me.Focus()
                Exit Sub
            ElseIf (e.KeyCode = Keys.Tab And Me.Visible = False) Then
                Me.Hide()
                Exit Sub
            End If
            Dim curLoc As Integer = TextBoxField.SelectionStart
            Dim prevtext As String = ""
            If InStrRev(TextBoxField.Text, " ", curLoc, CompareMethod.Text) < 0 Then
                prevtext = TextBoxField.Text.Substring(0, curLoc)
            ElseIf TextBoxField.Text.Contains(vbLf) And TextBoxField.Multiline = True Then
                prevtext = TextBoxField.Text.Replace(vbLf, " ").Substring(InStrRev(TextBoxField.Text.Replace(vbLf, " "), " ", curLoc, CompareMethod.Text), curLoc - InStrRev(TextBoxField.Text.Replace(vbLf, " "), " ", curLoc, CompareMethod.Text))
            Else
                prevtext = TextBoxField.Text.Substring(InStrRev(TextBoxField.Text, " ", curLoc, CompareMethod.Text), curLoc - InStrRev(TextBoxField.Text, " ", curLoc, CompareMethod.Text))
            End If
            If prevtext <> "" Then
                Me.Items.Clear()
                If AddAllPDFFields Then Me.Items.Add(prefix & "Add All PDF Fields" & postfix)
                If AddAllFormImages Then Me.Items.Add(prefix & "Add All Form Images" & postfix)
                If AddAllPDFFields Then Me.Items.Add(prefix & "Add All Page Images And Fields" & postfix)
                Try
                    If AddAllFormImages Then
                        If Not frmMerge1 Is Nothing Then
                            frmMerge1.chkBodyIsHtml.Checked = True
                        End If
                    End If
                Catch ex As Exception
                    Err.Clear()
                End Try
                For Each Str As String In fieldNames.ToArray
                    If Str.ToLower.Contains(prevtext.ToLower) Then
                        If Not String.IsNullOrEmpty(Str & "") Then
                            Me.Items.Add(prefix & Str.ToString & postfix)
                        End If
                    End If
                Next
                If (Me.Items.Count > 1 And AddAllPDFFields) Or (Items.Count > 0 And Not AddAllPDFFields) Then
                    Dim ptNew As Point = (TextBoxField.GetPositionFromCharIndex(TextBoxField.SelectionStart - 1)) 'New Point(pt.X, pt.Y + Me.Height)
                    ptNew = New Point(ptNew.X + TextBoxField.Left, ptNew.Y + TextBoxField.Top + 22)
                    If ptNew.X + Me.Width > TextBoxField.Parent.Width Then
                        ptNew = New Point(TextBoxField.Right - Me.Width - 10, ptNew.Y)
                    End If
                    If ptNew.Y + Me.Height > TextBoxField.Parent.Height Then
                        ptNew = New Point(ptNew.X, TextBoxField.Bottom - Me.Height - 22)
                    End If
                    Me.Location = (ptNew)
                    Me.Height = 135
                    Me.ScrollAlwaysVisible = True
                    Me.Width = 260
                    Me.BringToFront()
                    Me.Show()
                    If Me.SelectedIndex < 0 Then
                        Me.SelectedIndex = 0
                    End If
                    TextBoxField.Focus()
                Else
                    Me.SelectedIndex = -1
                    Me.Hide()
                    Exit Sub
                End If
            Else
                Me.SelectedIndex = -1
                Me.Hide()
                Exit Sub
            End If
        Catch ex As Exception
            Err.Clear()
        Finally
            If Me.Items.Count <= 0 Then
                Me.Hide()
            End If
        End Try
    End Sub
    Private Sub ListBoxAutocomplete_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.GotFocus
    End Sub
    Private Sub ListBoxAutocomplete_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.SelectedIndexChanged
    End Sub
    Private Sub ListBoxAutocomplete_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode <> Keys.Up And e.KeyCode <> Keys.Down Then
                If e.KeyCode = Keys.Escape Then
                    Me.Hide()
                ElseIf e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Space Or e.KeyCode = Keys.Execute Or e.KeyCode = Keys.Insert Then
                    Try
                        e.SuppressKeyPress = True
                        Dim selItem As String = Me.Items(Me.SelectedIndex).ToString
                        Dim curLoc As Integer = TextBoxField.SelectionStart
                        Dim spaceLoc As Integer = InStrRev(TextBoxField.Text.ToString.Replace(vbLf, " "), " ", curLoc, CompareMethod.Text) ', curLoc, CompareMethod.Text)
                        TextBoxField.SelectionStart = spaceLoc
                        TextBoxField.SelectionLength = Math.Abs(spaceLoc - curLoc)
                        Try
                            If Me.SelectedItem.ToString().ToLower.Contains("Add All PDF Fields".ToLower) Then
                                Dim strInser As New System.Text.StringBuilder
                                For Each strFld As String In fieldNames.ToArray
                                    If Not strFld.ToLower.Contains("Add All PDF Fields".ToLower) Then
                                        strInser.AppendLine(strFld.ToString.Replace("_", " ").Replace("-", " ").Replace(".", " ").Replace("  ", " ") & ": " & prefix & strFld.ToString & postfix)
                                    End If
                                Next
                                TextBoxField.SelectedText = strInser.ToString
                            ElseIf Me.SelectedItem.ToString().ToLower.Contains("Add All Form Images".ToLower) Then
                                Dim strInser As New System.Text.StringBuilder
                                'Dim frmMain1 As frmMain = DirectCast(Me.Parent, frmMain)
                                If Not frmMain1 Is Nothing Then
                                    For i As Integer = 1 To frmMain1.pdfReaderDoc.NumberOfPages
                                        strInser.AppendLine("<img src=""{pageImage_" & i.ToString() & "}"" style=""width:100%;height:auto;display:block;"" /><br/>")
                                    Next
                                    TextBoxField.SelectedText = strInser.ToString
                                End If
                            ElseIf Me.SelectedItem.ToString().ToLower.Contains("Add All Page Images And Fields".ToLower) Then
                                Dim strInser As New System.Text.StringBuilder
                                'Dim frmMain1 As frmMain = DirectCast(Me.Parent, frmMain)
                                If Not frmMain1 Is Nothing Then
                                    'For i As Integer = 1 To frmMain1.pdfReaderDoc.NumberOfPages
                                    '    strInser.AppendLine("<img src=""{pageImage_" & i.ToString() & "}"" style=""width:100%;height:auto;display:block;"" /><br/>")
                                    'Next
                                    strInser.Append(createHTMLFile(frmMain1, frmMerge1, "", True, frmMerge1, ""))
                                    TextBoxField.SelectedText = strInser.ToString
                                End If
                            Else
                                TextBoxField.SelectedText = CStr(Me.SelectedItem.ToString() & "")
                            End If
                        Catch ex As Exception
                            TextBoxField.SelectedText = CStr(Me.SelectedItem.ToString() & "")
                        End Try
                        TextBoxField.Focus()
                        TextBoxField.SelectionStart = TextBoxField.SelectionStart + TextBoxField.SelectedText.ToString.Length
                        TextBoxField.SelectionLength = 0
                        Me.Hide()
                    Catch ex As Exception
                        Err.Clear()
                    End Try
                End If
            Else
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public Function createHTMLFile(frm As frmMain, frmMerge1 As frmMerge, Optional pageRange As String = "", Optional blnIncludeFields As Boolean = True, Optional ByRef ParentForm As Form = Nothing, Optional strBody As String = "") As String
        frm.pnlFields.Hide()
        Try
            If Not ParentForm Is Nothing Then
                Me.Show()
                ParentForm.Hide()
            End If
            Dim reader As PdfReader = frm.pdfReaderDoc.Clone
            Dim b() As Byte = frm.Session
            Dim chtml As New PDF2HTMLnet.PDF2HTMLnet(frm.Session, frm.pdfOwnerPassword, "form1", "", "_self")
            chtml.formMethod = "post"
            chtml.ShowTitles = True
            chtml.TitleReplaceStringsWithSpace = New String() {"_", "-", "."}
            'Dim fileDirectory As String = Path.GetDirectoryName(fpath).ToString.TrimEnd("\"c) & "\"c
            'Dim fn As String = appPathTemp & Path.GetFileNameWithoutExtension(fpath & "") & ".htm" & ""
            Dim fn As String = frm.fileDirectory() & "html\" & Path.GetFileNameWithoutExtension(frm.fpath) & "\" & Path.GetFileNameWithoutExtension(frm.fpath & "") & ".htm" & ""
            'Dim sd As New SaveFileDialog
            'sd.CheckPathExists = True
            'sd.FileName = Path.GetFileName(fn & "")
            'If Not Directory.Exists(fileDirectory() & "html\") Then
            '    Directory.CreateDirectory(fileDirectory() & "html\")
            'End If
            'If Not Directory.Exists(fileDirectory() & "html\" & Path.GetFileNameWithoutExtension(fpath) & "\") Then
            '    Directory.CreateDirectory(fileDirectory() & "html\" & Path.GetFileNameWithoutExtension(fpath) & "\")
            'End If
            'sd.InitialDirectory = fileDirectory() & "html\" & Path.GetFileNameWithoutExtension(fpath) & "\"
            ''Dim sd As New SaveFileDialog
            ''sd.CheckPathExists = True
            ''sd.FileName = Path.GetFileName(fn & "")
            ''sd.InitialDirectory = appPathTemp
            'sd.Filter = "HTM|*.htm|HTML|*.html|Text|*.txt|All Files|*.*"
            'sd.FilterIndex = 0
            'sd.DefaultExt = ".htm"
            ''Me.Hide()
            'Me.Show()
            'Select Case sd.ShowDialog(Me)
            '    Case Windows.Forms.DialogResult.OK, Windows.Forms.DialogResult.Yes
            '        fn = sd.FileName & ""
            '    Case Else
            '        Retu2rn
            '        Exit Select
            'End Select
            'Select Case MsgBox("Export background and layout exactly as it appears?", MsgBoxStyle.YesNoCancel + MsgBoxStyle.Question + MsgBoxStyle.ApplicationModal, "Export HTML:")
            'Case MsgBoxResult.Yes, MsgBoxResult.Ok
            Try
                Dim totalPageHeight As Single = 0, pageWidth As Single, pageHeight As Single
                Dim strHTML As String = ""
                Dim useBase64ImageURL As Boolean = False
                'If blnIncludeFields Then
                '    'Select Case MsgBox("Inline base64 image URLs?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.ApplicationModal, "Inline Image URLs:")
                '    '    Case MsgBoxResult.Yes, MsgBoxResult.Ok
                '    '        useBase64ImageURL = True
                '    '    Case Else
                '    '        useBase64ImageURL = False
                '    'End Select
                '    useBase64ImageURL = True
                'Else
                '    useBase64ImageURL = False
                'End If
                'cmbPercent.SelectedIndex = 0
                'strHTML &= "<html>"
                strHTML &= "<div style=""margin:0;padding:0;width:100%;min-width:100%;max-width:100%;"">"
                'strHTML &= "<head>"
                'If blnIncludeFields Then
                '    strHTML &= "<style media=""all"" type=""text/css"">.lazy {display: none;}" & Environment.NewLine & "@media screen and (-ms-high-contrast: active), (-ms-high-contrast: none){html,body,form{font-size:28px}}" & Environment.NewLine & "</style>"
                '    strHTML &= "<script type=""text/javascript"">"
                '    'strHTML &= File.ReadAllText(frm.ApplicationDataFolder(True, "") & "\" & "resources/jquery.1.10.2.min.js") & ""
                '    strHTML &= File.ReadAllText(frm.ApplicationDataFolder(True, "") & "\" & "resources/jquery-3.1.1.min.js") & ""
                '    strHTML &= "</script>"
                '    'strHTML &= "<script type=""text/javascript"">"
                '    'strHTML &= File.ReadAllText(frm.ApplicationDataFolder(True, "") & "\" & "resources/flowtype.js") & ""
                '    'strHTML &= "</script>"
                '    'strHTML &= "<script type=""text/javascript"">"
                '    'strHTML &= File.ReadAllText(frm.ApplicationDataFolder(True, "") & "\" & "resources/lazyload.js") & ""
                '    'strHTML &= "</script>"
                '    strHTML &= "<script type=""text/javascript"">"
                '    strHTML &= File.ReadAllText(frm.ApplicationDataFolder(True, "") & "\" & "resources/jquery-mask.js") & ""
                '    strHTML &= "</script>"
                'End If
                'strHTML &= "</head>"
                strHTML &= "<div style=""margin:0;padding:0;width:100%;min-width:100%;max-width:100%;"">"
                Dim clsInput As New clsPromptDialog
                If Not blnIncludeFields Then
                    strHTML &= Environment.NewLine & "<form id=""form"" style=""margin:0;padding:0;width:100%;min-width:100%;max-width:100%;"">" & Environment.NewLine
                    strHTML &= "<div>" & strBody & "</div>" & Environment.NewLine
                Else
                    Dim htmlSubmitAction As String = clsInput.ShowDialog("HTML submit action URL?", "HTML Submit Action URL?", frmMerge1, "", "OK")
                    strHTML &= Environment.NewLine & "<form action=""" & htmlSubmitAction & """ method=""post"" id=""form"" style=""margin:0;padding:0;width:100%;min-width:100%;max-width:100%;"">" & Environment.NewLine
                End If
                Dim pField As New List(Of String)
                'ToolStripProgressBar1.Maximum = reader.NumberOfPages
                'ToolStripProgressBar1.Minimum = 1
                'ToolStripProgressBar1.Value = 1
                'ToolStripProgressBar1.Visible = True
                Dim dir As String = Path.GetDirectoryName(fn).ToString.TrimEnd("\"c) & "\"
                If frm.cLinks Is Nothing Then
                    frm.cLinks = New clsLinks(reader, frm)
                ElseIf frm.cLinks.Links.Count <= 0 Then
                    frm.cLinks = New clsLinks(frm.pdfReaderDoc, frm)
                    frm.cLinks.LoadLinksOnPage(CInt(frm.btnPage.SelectedIndex))
                End If
                Dim fieldListFlowType As New Dictionary(Of String, Single)
                Dim fldTabIndex As Integer = 0
                Dim fldTabMax As Integer = 0
                If Not pageRange = "" Then
                    reader.SelectPages(pageRange)
                End If
                For p As Integer = 1 To reader.NumberOfPages
                    frm.btnPage.SelectedIndex = p - 1
                    'ToolStripProgressBar1.Value = p
                    'Application.DoEvents()
                    frm.pnlFieldTabOrder_Load(True)
                    Dim lstFields As Dictionary(Of String, List(Of frmMain.fieldInfo)) = frm.GetAllFieldsOnPageiText(frm.Session.ToArray(), p, True)
                    pageWidth = reader.GetPageSizeWithRotation(p).Width
                    pageHeight = reader.GetPageSizeWithRotation(p).Height
                    frm.LoadPDFReaderDoc(frm.pdfOwnerPassword, True)
                    Dim pdfReaderDocClone As PdfReader = reader.Clone
                    If blnIncludeFields Then
                        pdfReaderDocClone.RemoveFields()
                    End If
                    Dim imgBytes() As Byte = frm.A0_LoadImageGhostScript(frm.getPDFBytes(pdfReaderDocClone), frm.pdfOwnerPassword, p, pageWidth * frm.getPercent() * 1.3F, pageHeight * frm.getPercent() * 1.3F, False)
                    Dim img As System.Drawing.Image = System.Drawing.Image.FromStream(New MemoryStream(imgBytes))
                    Dim imgStream As New MemoryStream
                    img.Save(imgStream, System.Drawing.Imaging.ImageFormat.Png)
                    strHTML &= "<div name=""page_" & p & """ id=""page_" & p & """ style=""background-size: 100%;margin:0 auto;padding:0;display:block;width:100%;max-width:100%;height:auto;position:relative;"">" 'width:" & CInt(pageWidth * frm.getPercent()) & ";height:" & CInt(pageHeight * frm.getPercent()) & ";
                    If blnIncludeFields Then
                        If useBase64ImageURL Then
                            'strHTML &= "<img " & CStr(IIf(p > 1, "class=""lazy""", "")) & " " & CStr(IIf(p > 1, "data-original", "src")) & "=""data:image/jpeg;base64," & System.Convert.ToBase64String(imgStream.ToArray()).ToString.Replace(" ", "+") & """ style=""width:100%;max-width:100%;height:auto;margin:0 auto;z-index:-1000;""/>"
                            'strHTML &= "<noscript><img src=""data:image/jpeg;base64," & System.Convert.ToBase64String(imgStream.ToArray()).ToString.Replace(" ", "+") & """ style=""width:100%;max-width:100%;height:auto;margin:0 auto;z-index:-1000;""/></noscript>"
                            strHTML &= "<img src=""data:image/jpeg;base64," & System.Convert.ToBase64String(imgStream.ToArray()).ToString.Replace(" ", "+") & """ style=""width:100%;max-width:100%;height:auto;margin:0 auto;z-index:-1000;""/>"
                        Else
                            'Dim imgFileName As String = dir & "images\" & p.ToString() & "-" & Path.GetFileNameWithoutExtension(fn) & ".png"
                            'If Not Directory.Exists(dir & "images\") Then
                            '    Directory.CreateDirectory(dir & "images\")
                            'End If
                            'File.WriteAllBytes(imgFileName, imgStream.ToArray())
                            'strHTML &= "<img " & CStr(IIf(p > 1, "class=""lazy""", "")) & " " & CStr(IIf(p > 1, "data-original", "src")) & "=""" & "images/" & p.ToString() & "-" & Path.GetFileNameWithoutExtension(fn) & ".png" & """ style=""width:100%;max-width:100%;height:auto;margin:0 auto;z-index:-1000;""/>"
                            'strHTML &= "<noscript><img src=""" & "images/" & p.ToString() & "-" & Path.GetFileNameWithoutExtension(fn) & ".png" & """ style=""width:100%;max-width:100%;height:auto;margin:0 auto;z-index:-1000;""/></noscript>"
                            strHTML &= "<img src=""" & "{pageImage_" & p.ToString() & "}" & """ style=""width:100%;max-width:100%;height:auto;margin:0 auto;z-index:-1000;""/>"
                        End If
                    Else
                        If useBase64ImageURL Then
                            'strHTML &= "<img " & CStr(IIf(p > 1, "class=""lazy""", "")) & " " & CStr(IIf(p > 1, "data-original", "src")) & "=""data:image/jpeg;base64," & System.Convert.ToBase64String(imgStream.ToArray()).ToString.Replace(" ", "+") & """ style=""width:100%;max-width:100%;height:auto;margin:0 auto;z-index:-1000;""/>"
                            'strHTML &= "<noscript><img src=""data:image/jpeg;base64," & System.Convert.ToBase64String(imgStream.ToArray()).ToString.Replace(" ", "+") & """ style=""width:100%;max-width:100%;height:auto;margin:0 auto;z-index:-1000;""/></noscript>"
                            strHTML &= "<img width=""" & pageWidth * frm.getPercent() & """ height=""" & pageHeight * frm.getPercent() & """ src=""data:image/jpeg;base64," & System.Convert.ToBase64String(imgStream.ToArray()).ToString.Replace(" ", "+") & """ style=""width:100%;max-width:100%;height:auto;margin:0 auto;z-index:-1000;""/>"
                        Else
                            'Dim imgFileName As String = dir & "images\" & p.ToString() & "-" & Path.GetFileNameWithoutExtension(fn) & ".png"
                            'If Not Directory.Exists(dir & "images\") Then
                            '    Directory.CreateDirectory(dir & "images\")
                            'End If
                            'File.WriteAllBytes(imgFileName, imgStream.ToArray())
                            'strHTML &= "<img " & CStr(IIf(p > 1, "class=""lazy""", "")) & " " & CStr(IIf(p > 1, "data-original", "src")) & "=""" & "images/" & p.ToString() & "-" & Path.GetFileNameWithoutExtension(fn) & ".png" & """ style=""width:100%;max-width:100%;height:auto;margin:0 auto;z-index:-1000;""/>"
                            'strHTML &= "<noscript><img src=""" & "images/" & p.ToString() & "-" & Path.GetFileNameWithoutExtension(fn) & ".png" & """ style=""width:100%;max-width:100%;height:auto;margin:0 auto;z-index:-1000;""/></noscript>"
                            strHTML &= "<img width=""" & pageWidth * frm.getPercent() & """ height=""" & pageHeight * frm.getPercent() & """ src=""{pageImage_" & p.ToString() & "}"" style=""width:100%;max-width:100%;height:auto;margin:0 auto;z-index:-1000;""/>"
                        End If
                    End If
                    If blnIncludeFields Then
                        frm.cLinks.LoadLinksOnPage(CInt(p - 1))
                        If frm.cLinks.Links.Count > 0 Then
                            For lnkIndex As Integer = 0 To frm.cLinks.Links.Count - 1
                                'Dim lnkRectScreen As RectangleF = getRectangleScreen(frm.cLinks.Links(lnkIndex).Link_Rect, p) 'cLinks.Links(lnkIndex).Link_Rect 
                                Dim lnkRectScreen As RectangleF = frm.cLinks.Links(lnkIndex).Link_Rect
                                Dim strHTMLField As String = ""
                                Dim strHtmlBuilder As New System.Text.StringBuilder
                                Dim perc As Single = frm.getPercent() 'getPercentPageNumber(p)
                                If frm.cLinks.Links(lnkIndex).Link_Destination_PageIndex >= 0 And String.IsNullOrEmpty(frm.cLinks.Links(lnkIndex).Link_Destination_URI) And frm.cLinks.Links(lnkIndex).Link_ImageBytes Is Nothing Then
                                    Dim intDestPage As Integer = frm.cLinks.Links(lnkIndex).Link_Destination_PageIndex + 0
                                    If intDestPage >= 0 Then
                                        strHtmlBuilder.AppendLine("<a name='p" & p & "_lnk" & lnkIndex & "' id='p" & p & "_lnk" & lnkIndex & "' href=""#page_" & CInt(intDestPage + 1).ToString() & """ ")
                                        Dim r As System.Drawing.RectangleF = lnkRectScreen
                                        strHtmlBuilder.Append(" style=""")
                                        strHtmlBuilder.Append("position:absolute;left:" & (r.Left / (pageWidth * perc)) * 100 & "%;right:" & (r.Right / (pageWidth * perc)) * 100 & "%;top:" & ((r.Top) / (pageHeight * perc)) * 100 & "%;bottom:" & ((r.Bottom) / (pageHeight * perc)) * 100 & "%;")
                                        strHtmlBuilder.Append("height:" & (r.Height / (pageHeight * perc)) * 100 & "%;width:" & (r.Width / (pageWidth * perc)) * 100 & "%;")
                                        strHtmlBuilder.Append("background-color: Transparent;")
                                        strHtmlBuilder.Append("border-color:Transparent;")

                                        strHtmlBuilder.Append("border-width:0px;")
                                        strHtmlBuilder.Append(""" ")

                                        strHtmlBuilder.Append("> </a>")
                                        strHTML &= strHtmlBuilder.ToString
                                    End If
                                ElseIf Not String.IsNullOrEmpty(frm.cLinks.Links(lnkIndex).Link_Destination_URI) And frm.cLinks.Links(lnkIndex).Link_ImageBytes Is Nothing Then
                                    Dim strDestUri As String = frm.cLinks.Links(lnkIndex).Link_Destination_URI & ""
                                    strHtmlBuilder.AppendLine("<a target=""_blank"" name='p" & p & "_lnk" & lnkIndex & "' id='p" & p & "_lnk" & lnkIndex & "' href=""" & strDestUri & """ ")
                                    Dim r As System.Drawing.RectangleF = lnkRectScreen
                                    strHtmlBuilder.Append(" style=""")
                                    strHtmlBuilder.Append("position:absolute;left:" & (r.Left / (pageWidth * perc)) * 100 & "%;right:" & (r.Right / (pageWidth * perc)) * 100 & "%;top:" & ((r.Top) / ((pageHeight * perc))) * 100 & "%;bottom:" & ((r.Bottom) / ((pageHeight * perc))) * 100 & "%;")
                                    strHtmlBuilder.Append("height:" & (r.Height / (pageHeight * perc)) * 100 & "%;width:" & (r.Width / (pageWidth * perc)) * 100 & "%;")
                                    strHtmlBuilder.Append("background-color: Transparent;")
                                    strHtmlBuilder.Append("border-color:Transparent;")

                                    strHtmlBuilder.Append("border-width:0px;")
                                    strHtmlBuilder.Append(""" ")
                                    strHtmlBuilder.Append("> </a>")
                                    strHTML &= strHtmlBuilder.ToString
                                End If
                            Next
                        End If
                    End If

                    strHTML &= (Environment.NewLine & "")
                    If blnIncludeFields Then
                        Try
                            frm.calculationOrderList = frmMain.CalculationOrder
                        Catch ex As Exception
                            frm.TimeStampAdd(ex, frm.debugMode) ' NK 2016-06-30() ' Throw Ex  ' NK2 '
                        End Try
                        'data:image/jpeg;base64," & System.Convert.ToBase64String(imgStream.ToArray()).ToString.Replace(" ", "+") & "
                        Dim fldTabIndexTemp As Integer = fldTabIndex
                        For Each fldNm As String In lstFields.Keys.ToArray
                            strHTML &= (Environment.NewLine & "")
                            Try
                                frm.calculationOrderList = frmMain.CalculationOrder
                                fldTabIndexTemp = 0
                                Try
                                    frm.fields_tab_order = frmMain.FieldTabOrder(True)
                                    'pnlFieldTabOrder_Listbox.Items.Clear()
                                    Dim iTab As Integer = 0
                                    For Each fldRect As frmMain.FieldName_Rectangle In frm.fields_tab_order.ToArray()
                                        If Not fldRect.field_name Is Nothing Then
                                            If Not String.IsNullOrEmpty(fldRect.field_name) Then
                                                If fldRect.field_name.ToString.ToLower() & "" = fldNm.ToString.ToLower() Then
                                                    fldTabIndexTemp = fldTabIndex + iTab
                                                    Exit For
                                                    'pnlFieldTabOrder_Listbox.Items.Add(fld.field_name.ToString() & "")
                                                End If
                                            End If
                                        End If
                                        iTab += 1
                                    Next
                                Catch ex As Exception
                                    frm.TimeStampAdd(ex, frm.debugMode) ' NK 2016-06-30() ' Throw Ex  ' NK2 '
                                End Try
                                If fldTabIndexTemp > fldTabMax Then
                                    fldTabMax = fldTabIndexTemp
                                End If
                                'fldTabIndexTemp = -1
                            Catch ex As Exception
                                frm.TimeStampAdd(ex, frm.debugMode) ' NK 2016-06-30() ' Throw Ex  ' NK2 '
                            End Try
                            'For Each fld As fieldInfo In lstFields(fldNm).ToArray
                            Dim fld As frmMain.fieldInfo = lstFields(fldNm)(0)
                            Dim strHTMLField As String = ""
                            Dim strHtmlBuilder As New System.Text.StringBuilder
                            Dim defaultFontSize As Integer = 12
                            Select Case fld.fieldType
                                Case iTextSharp.text.pdf.AcroFields.FIELD_TYPE_CHECKBOX
                                    frmMain.A0_PDFFormField_LoadProperties(frmMain.Session, fldNm, p, fld.fieldIndex)
                                    If Not fld.fieldDictionary Is Nothing Then
                                        If Not fld.fieldDictionary.Get(PdfName.AP) Is Nothing Then
                                            Dim tmpDict As PdfDictionary = fld.fieldDictionary.GetAsDict(PdfName.AP)
                                            If Not tmpDict.Get(PdfName.N) Is Nothing Then
                                                tmpDict = tmpDict.GetAsDict(PdfName.N)
                                                For Each k As PdfName In tmpDict.Keys.ToArray()
                                                    If Not k.ToString().TrimStart("/"c) = "Off" Then
                                                        fld.fieldValue = k.ToString().TrimStart("/"c)
                                                    End If
                                                Next
                                            End If
                                        End If
                                    End If
                                    strHtmlBuilder.AppendLine("<input tabIndex=""" & fldTabIndexTemp & """ type='checkbox' name='" & fld.fieldName & "' id='" & fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex) & "' value='" & fld.fieldValue & "' ")
                                    'strHtmlBuilder.AppendLine("<input tabIndex=""" & fldTabIndexTemp & """ type='checkbox' name='" & fld.fieldNameLong & "' id='" & fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex) & "' value='" & fld.fieldValue & "' ")
                                    strHtmlBuilder.Append(IIf(frmMain.PDFField_Value_Checked.Checked, " CHECKED ", ""))
                                    Dim r As System.Drawing.RectangleF = fld.fieldPositionScreen
                                    strHtmlBuilder.Append(" style=""-moz-appearance: none;")
                                    'strHtmlBuilder.Append("position:absolute;left:" & r.Left & "px;right:" & r.Right & "px;top:" & r.Top + totalPageHeight & "px;bottom:" & r.Bottom + totalPageHeight & "px;")
                                    'strHtmlBuilder.TEMP.Append("position:absolute;left:" & (r.Left / pageWidth) * 100 & "%;right:" & (r.Right / pageWidth) * 100 & "%;top:" & ((totalPageHeight + r.Top) / (totalPageHeight + pageHeight)) * 100 & "%;bottom:" & ((totalPageHeight + r.Bottom) / (totalPageHeight + pageHeight)) * 100 & "%;")
                                    strHtmlBuilder.Append("position:absolute;left:" & (r.Left / pageWidth) * 100 & "%;right:" & (r.Right / pageWidth) * 100 & "%;top:" & ((r.Top) / (pageHeight)) * 100 & "%;bottom:" & ((r.Bottom) / (pageHeight)) * 100 & "%;")
                                    strHtmlBuilder.Append("height:" & (r.Height / pageHeight) * 100 & "%;width:" & (r.Width / pageWidth) * 100 & "%;")
                                    strHtmlBuilder.Append("position:absolute;")
                                    'strHtmlBuilder.TEMP.Append("position:absolute;left:" & (pageWidth / r.Left) & "%;right:" & (pageHeight / r.Right) & "%;top:" & (totalPageHeight / (totalPageHeight + r.Top)) & "%;bottom:" & (totalPageHeight / (totalPageHeight + r.Bottom)) & "%;")
                                    strHtmlBuilder.Append("text-align:" & frmMain.PDFField_TextAlign.Text & ";")
                                    If (frmMain.PDFField_FontSize.Text.ToLower) <> "Auto".ToLower Then
                                        strHtmlBuilder.Append("font-size:" & String.Format(CSng(frmMain.PDFField_FontSize.Text / 14), "#.00") & "em;")
                                        defaultFontSize = frmMain.PDFField_FontSize.Text
                                        fieldListFlowType.Add(fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex), CSng(r.Width / CSng(frmMain.PDFField_FontSize.Text)))
                                    Else
                                        strHtmlBuilder.Append("font-size:12px;")
                                        fieldListFlowType.Add(fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex), CSng(r.Width / CSng(12)))
                                    End If
                                    strHtmlBuilder.Append("color:" & ColorTranslator.ToHtml(frmMain.PDFField_TextColorPicker.BackColor) & ";")
                                    If Not (frmMain.PDFField_BackgroundColorPicker.BackColor) = System.Drawing.Color.Transparent Then
                                        strHtmlBuilder.Append("background-color:" & ColorTranslator.ToHtml(frmMain.PDFField_BackgroundColorPicker.BackColor) & ";")
                                    Else
                                        strHtmlBuilder.Append("background-color:" & ColorTranslator.ToHtml(System.Drawing.Color.White) & ";")
                                    End If
                                    strHtmlBuilder.Append("outline-color:" & ColorTranslator.ToHtml(frmMain.PDFField_BorderColorPicker.BackColor) & ";")
                                    Dim strStyle As String = frmMain.PDFField_BorderStyle.Text
                                    Select Case strStyle.ToString.ToLower
                                        Case "solid", "dashed", "dotted", "inset"
                                            strStyle = strStyle & ""
                                        Case "beveled"
                                            strStyle = "ridge"
                                        Case "underline"
                                            strStyle = "solid"
                                    End Select
                                    strHtmlBuilder.Append("outline-style:" & strStyle & ";")
                                    strStyle = CStr(frmMain.PDFField_BorderWidth.SelectedIndex + 1).ToString
                                    strHtmlBuilder.Append("outline-width:" & strStyle & "px;")
                                    strHtmlBuilder.Append(""" ")
                                    strHtmlBuilder.Append(" class=""resizeText"" ")
                                    strHtmlBuilder.Append(" defaultFontSize=""" & CStr(IIf(String.IsNullOrEmpty(defaultFontSize & ""), "12", defaultFontSize)) & """ ")
                                    strHtmlBuilder.Append(" />")
                                    strHTML &= strHtmlBuilder.ToString
                                Case iTextSharp.text.pdf.AcroFields.FIELD_TYPE_COMBO
                                    frmMain.A0_PDFFormField_LoadProperties(frmMain.Session, fldNm, p, fld.fieldIndex)
                                    Dim valList As New List(Of String)
                                    For Each val1 As String In frmMain.ComboBox_ItemDisplay.Items
                                        valList.Add(val1)
                                    Next
                                    fld.fieldOptionDisplay = valList.ToArray
                                    valList = New List(Of String)
                                    For Each val1 As String In frmMain.ComboBox_ItemValue.Items
                                        valList.Add(val1)
                                    Next
                                    fld.fieldOptionExport = valList.ToArray
                                    valList = New List(Of String)
                                    For Each val1 As String In frmMain.ComboBox_ItemDisplay.SelectedItems
                                        valList.Add(val1)
                                    Next
                                    fld.fieldListSelection = valList.ToArray
                                    strHtmlBuilder.AppendLine("<select tabIndex=""" & fldTabIndexTemp & """ class=""resizeText"" name='" & fld.fieldName & "' id='" & fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex) & "' " & " ")
                                    'strHtmlBuilder.AppendLine("<select tabIndex=""" & fldTabIndexTemp & """ class=""resizeText"" name='" & fld.fieldNameLong & "' id='" & fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex) & "' " & " ")
                                    'IIf(ListBox_Options_MultipleSelection.Checked, " MULTIPLE ", " ") & 
                                    '& IIf(fld.fieldType = iTextSharp.text.pdf.AcroFields.FIELD_TYPE_LIST, IIf(fld.fieldSize > 1, "size='" & fld.fieldSize & "' ", "size='1' "), "")
                                    Dim r As System.Drawing.RectangleF = fld.fieldPositionScreen
                                    strHtmlBuilder.Append(" style=""") '-moz-appearance: none;
                                    'strHtmlBuilder.Append("position:absolute;left:" & r.Left & "px;right:" & r.Right & "px;top:" & r.Top + totalPageHeight & "px;bottom:" & r.Bottom + totalPageHeight & "px;")
                                    strHtmlBuilder.Append("position:absolute;left:" & (r.Left / pageWidth) * 100 & "%;right:" & (r.Right / pageWidth) * 100 & "%;top:" & ((r.Top) / (pageHeight)) * 100 & "%;bottom:" & ((r.Bottom) / (pageHeight)) * 100 & "%;")
                                    strHtmlBuilder.Append("height:" & (r.Height / pageHeight) * 100 & "%;width:" & (r.Width / pageWidth) * 100 & "%;")
                                    strHtmlBuilder.Append("position:absolute;")
                                    strHtmlBuilder.Append("text-align:" & frmMain.PDFField_TextAlign.Text & ";")
                                    If (frmMain.PDFField_FontSize.Text.ToLower) <> "Auto".ToLower Then
                                        strHtmlBuilder.Append("font-size:" & String.Format(CSng(frmMain.PDFField_FontSize.Text / 14), "#.00") & "em;")
                                        defaultFontSize = frmMain.PDFField_FontSize.Text
                                        fieldListFlowType.Add(fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex), CSng(r.Width / CSng(frmMain.PDFField_FontSize.Text)))
                                    Else
                                        strHtmlBuilder.Append("font-size:12px;")
                                        fieldListFlowType.Add(fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex), CSng(r.Width / CSng(12)))
                                    End If
                                    'strHtmlBuilder.Append("height:" & r.Height & "px;width:" & r.Width & "px;")
                                    strHtmlBuilder.Append("color:" & ColorTranslator.ToHtml(frmMain.PDFField_TextColorPicker.BackColor) & ";")
                                    If Not (frmMain.PDFField_BackgroundColorPicker.BackColor) = System.Drawing.Color.Transparent Then
                                        strHtmlBuilder.Append("background-color:" & ColorTranslator.ToHtml(frmMain.PDFField_BackgroundColorPicker.BackColor) & ";")
                                    Else
                                        strHtmlBuilder.Append("background-color:" & ColorTranslator.ToHtml(System.Drawing.Color.White) & ";")
                                    End If
                                    strHtmlBuilder.Append("border-color:" & ColorTranslator.ToHtml(frmMain.PDFField_BorderColorPicker.BackColor) & ";")
                                    Dim strStyle As String = frmMain.PDFField_BorderStyle.Text
                                    Select Case strStyle.ToString.ToLower
                                        Case "solid", "dashed", "dotted", "inset"
                                            strStyle = strStyle & ""
                                        Case "beveled"
                                            strStyle = "ridge"
                                        Case "underline"
                                            strStyle = "solid"
                                    End Select
                                    strHtmlBuilder.Append("border-style:" & strStyle & ";")

                                    strStyle = CStr(frmMain.PDFField_BorderWidth.SelectedIndex + 1).ToString
                                    strHtmlBuilder.Append("border-width:" & strStyle & "px;")
                                    strHtmlBuilder.Append(""" ")
                                    If frmMain.PDFField_Required.Checked Then
                                        strHtmlBuilder.Append(" required")
                                    End If
                                    strHtmlBuilder.Append(" defaultFontSize=""" & CStr(IIf(String.IsNullOrEmpty(defaultFontSize & ""), "12", defaultFontSize)) & """ ")
                                    strHtmlBuilder.Append(">")
                                    If fld.fieldOptionExport.Length = fld.fieldOptionDisplay.Length Then
                                        For intVal As Integer = 0 To fld.fieldOptionExport.Length - 1
                                            If frmMain.ComboBox_ItemDisplay.SelectedItems Is Nothing Then
                                                strHtmlBuilder.AppendLine("<option value='" & fld.fieldOptionExport(intVal) & "'>" & fld.fieldOptionDisplay(intVal) & "</option>")
                                            Else
                                                If fld.fieldListSelection.Contains(fld.fieldOptionDisplay(intVal)) Then
                                                    strHtmlBuilder.AppendLine("<option value='" & fld.fieldOptionExport(intVal) & "' selected ")
                                                    'strHtmlBuilder.Append(" class=""resizeText"" ")
                                                    strHtmlBuilder.AppendLine(">" & fld.fieldOptionDisplay(intVal) & "</option>")
                                                Else
                                                    strHtmlBuilder.AppendLine("<option value='" & fld.fieldOptionExport(intVal) & "' ")
                                                    'strHtmlBuilder.Append(" class=""resizeText"" ")
                                                    strHtmlBuilder.AppendLine(">" & fld.fieldOptionDisplay(intVal) & "</option>")
                                                End If
                                            End If
                                        Next
                                    Else
                                        If fld.fieldOptionExport.Length > 0 Then
                                            Dim intVal As Integer = 0

                                            For Each fldvalue As String In fld.fieldOptionExport
                                                If frmMain.ComboBox_ItemDisplay.SelectedItems Is Nothing Then
                                                    strHtmlBuilder.AppendLine("<option value='" & fldvalue & "'>" & fldvalue & "</option>")
                                                Else
                                                    If fld.fieldListSelection.Contains(fld.fieldOptionDisplay(intVal)) Then
                                                        strHtmlBuilder.AppendLine("<option value='" & fld.fieldOptionExport(intVal) & "' selected ")
                                                        'strHtmlBuilder.Append(" class=""resizeText"" ")
                                                        strHtmlBuilder.AppendLine(">" & fld.fieldOptionDisplay(intVal) & "</option>")
                                                    Else
                                                        strHtmlBuilder.AppendLine("<option value='" & fld.fieldOptionExport(intVal) & "' ")
                                                        'strHtmlBuilder.Append(" class=""resizeText"" ")
                                                        strHtmlBuilder.AppendLine(">" & fld.fieldOptionDisplay(intVal) & "</option>")
                                                    End If
                                                End If
                                                intVal += 1
                                            Next
                                        ElseIf fld.fieldOptionDisplay.Length > 0 Then
                                            For Each fldvalue As String In fld.fieldOptionDisplay
                                                If frmMain.ComboBox_ItemDisplay.SelectedItems Is Nothing Then
                                                    strHtmlBuilder.AppendLine("<option value='" & fldvalue & "'>" & fldvalue & "</option>")
                                                Else
                                                    If fld.fieldListSelection.Contains(fldvalue) Then
                                                        strHtmlBuilder.AppendLine("<option value='" & fldvalue & "' selected ")
                                                        'strHtmlBuilder.Append(" class=""resizeText"" ")
                                                        strHtmlBuilder.AppendLine(">" & fldvalue & "</option>")
                                                    Else
                                                        strHtmlBuilder.AppendLine("<option value='" & fldvalue & "' ")
                                                        'strHtmlBuilder.Append(" class=""resizeText"" ")
                                                        strHtmlBuilder.AppendLine(">" & fldvalue & "</option>")
                                                    End If
                                                End If


                                            Next
                                        End If
                                    End If

                                    strHtmlBuilder.AppendLine("</select>")
                                    strHTML &= strHtmlBuilder.ToString
                                Case iTextSharp.text.pdf.AcroFields.FIELD_TYPE_LIST
                                    frmMain.A0_PDFFormField_LoadProperties(frmMain.Session, fldNm, p, fld.fieldIndex)
                                    Dim valList As New List(Of String)
                                    Dim valListValues As New List(Of String)
                                    Dim valListDisplay As New List(Of String)
                                    'For Each val1 As String In ComboBox_ItemDisplay.Items
                                    '    valList.Add(val1)
                                    'Next
                                    'fld.fieldOptionDisplay = valList.ToArray
                                    'valList = New List(Of String)
                                    'For Each val1 As String In ComboBox_ItemValue.Items
                                    '    valList.Add(val1)
                                    'Next
                                    'fld.fieldOptionExport = valList.ToArray
                                    'valList = New List(Of String)
                                    'For Each val1 As String In ComboBox_ItemDisplay.SelectedItems
                                    '    valList.Add(val1)
                                    'Next


                                    If Not fld.fieldDictionary Is Nothing Then
                                        If Not fld.fieldDictionary.Get(PdfName.OPT) Is Nothing Then
                                            Dim tmpArray As PdfArray = fld.fieldDictionary.GetAsArray(PdfName.OPT)
                                            For Each k As PdfArray In tmpArray.ArrayList().ToArray
                                                If k.Size > 1 Then
                                                    If k(0).IsString Then
                                                        valListDisplay.Add(k.GetAsString(0).ToString().TrimStart("/"c))
                                                    End If
                                                    If k(1).IsString Then
                                                        valListValues.Add(k.GetAsString(1).ToString().TrimStart("/"c))
                                                    End If
                                                ElseIf k.Size = 1 Then
                                                    If k(0).IsString Then
                                                        valListDisplay.Add(k.GetAsString(0).ToString().TrimStart("/"c))
                                                        valListValues.Add(k.GetAsString(0).ToString().TrimStart("/"c))
                                                    End If
                                                End If
                                            Next
                                            fld.fieldOptionDisplay = valListDisplay.ToArray
                                            If valListDisplay.Count = valListValues.Count Then
                                                fld.fieldOptionExport = valListValues.ToArray
                                            Else
                                                fld.fieldOptionExport = valListDisplay.ToArray
                                            End If
                                        End If
                                        If Not fld.fieldDictionary.Get(PdfName.V) Is Nothing Then
                                            If fld.fieldDictionary.Get(PdfName.V).IsString() Then
                                                fld.fieldListSelection = New String() {fld.fieldDictionary.GetAsString(PdfName.V).ToUnicodeString().TrimStart("/"c)}
                                                fld.fieldValue = fld.fieldDictionary.GetAsString(PdfName.V).ToUnicodeString().TrimStart("/"c)
                                            ElseIf fld.fieldDictionary.Get(PdfName.V).IsArray() Then
                                                'fld.fieldListSelection = New String() {fld.fieldDictionary.GetAsString(PdfName.V).ToUnicodeString().TrimStart("/"c)}
                                                'fld.fieldValue = fld.fieldDictionary.GetAsString(PdfName.V).ToUnicodeString().TrimStart("/"c)
                                            End If
                                        End If
                                    End If
                                    'fld.fieldListSelection = valList.ToArray
                                    strHtmlBuilder.AppendLine("<select tabIndex=""" & fldTabIndexTemp & """ class=""resizeText"" name='" & fld.fieldName & "' id='" & fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex) & "' " & IIf(fld.fieldType = iTextSharp.text.pdf.AcroFields.FIELD_TYPE_LIST, IIf(fld.fieldSize > 1, "size='" & fld.fieldSize & "' ", "size='3' "), "") & IIf(frm.ListBox_Options_MultipleSelection.Checked, " MULTIPLE ", " ") & " ")
                                    'strHtmlBuilder.AppendLine("<select tabIndex=""" & fldTabIndexTemp & """ class=""resizeText"" name='" & fld.fieldNameLong & "' id='" & fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex) & "' " & IIf(fld.fieldType = iTextSharp.text.pdf.AcroFields.FIELD_TYPE_LIST, IIf(fld.fieldSize > 1, "size='" & fld.fieldSize & "' ", "size='3' "), "") & IIf(ListBox_Options_MultipleSelection.Checked, " MULTIPLE ", " ") & " ")
                                    Dim r As System.Drawing.RectangleF = fld.fieldPositionScreen
                                    strHtmlBuilder.Append(" style=""") '-moz-appearance: none;
                                    'strHtmlBuilder.Append("position:absolute;left:" & r.Left & "px;right:" & r.Right & "px;top:" & r.Top + totalPageHeight & "px;bottom:" & r.Bottom + totalPageHeight & "px;")
                                    'strHtmlBuilder.Append("position:absolute;left:" & (r.Left / pageWidth) * 100 & "%;right:" & (r.Right / pageWidth) * 100 & "%;top:" & ((r.Top) / (pageHeight)) * 100 & "%;bottom:" & ((r.Bottom) / (pageHeight)) * 100 & "%;")
                                    strHtmlBuilder.Append("position:absolute;left:" & (r.Left / pageWidth) * 100 & "%;right:" & (r.Right / pageWidth) * 100 & "%;top:" & ((r.Top) / (pageHeight)) * 100 & "%;bottom:" & ((r.Bottom) / (pageHeight)) * 100 & "%;")
                                    strHtmlBuilder.Append("height:" & (r.Height / pageHeight) * 100 & "%;width:" & (r.Width / pageWidth) * 100 & "%;")
                                    strHtmlBuilder.Append("position:absolute;")
                                    strHtmlBuilder.Append("text-align:" & frmMain.PDFField_TextAlign.Text & ";")
                                    If (frmMain.PDFField_FontSize.Text.ToLower) <> "Auto".ToLower Then
                                        strHtmlBuilder.Append("font-size:" & String.Format(CSng(frmMain.PDFField_FontSize.Text / 14), "#.00") & "em;")
                                        defaultFontSize = frmMain.PDFField_FontSize.Text
                                        fieldListFlowType.Add(fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex), CSng(r.Width / CSng(frmMain.PDFField_FontSize.Text)))
                                    Else
                                        strHtmlBuilder.Append("font-size:12px;")
                                        fieldListFlowType.Add(fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex), CSng(r.Width / CSng(12)))
                                    End If
                                    'strHtmlBuilder.Append("height:" & r.Height & "px;width:" & r.Width & "px;")
                                    strHtmlBuilder.Append("color:" & ColorTranslator.ToHtml(frmMain.PDFField_TextColorPicker.BackColor) & ";")
                                    If Not (frmMain.PDFField_BackgroundColorPicker.BackColor) = System.Drawing.Color.Transparent Then
                                        strHtmlBuilder.Append("background-color:" & ColorTranslator.ToHtml(frmMain.PDFField_BackgroundColorPicker.BackColor) & ";")
                                    Else
                                        strHtmlBuilder.Append("background-color:" & ColorTranslator.ToHtml(System.Drawing.Color.White) & ";")
                                    End If
                                    strHtmlBuilder.Append("border-color:" & ColorTranslator.ToHtml(frmMain.PDFField_BorderColorPicker.BackColor) & ";")
                                    Dim strStyle As String = frmMain.PDFField_BorderStyle.Text
                                    Select Case strStyle.ToString.ToLower
                                        Case "solid", "dashed", "dotted", "inset"
                                            strStyle = strStyle & ""
                                        Case "beveled"
                                            strStyle = "ridge"
                                        Case "underline"
                                            strStyle = "solid"
                                    End Select
                                    strHtmlBuilder.Append("border-style:" & strStyle & ";")

                                    strStyle = CStr(frmMain.PDFField_BorderWidth.SelectedIndex + 1).ToString
                                    strHtmlBuilder.Append("border-width:" & strStyle & "px;")
                                    strHtmlBuilder.Append(""" ")
                                    If frmMain.PDFField_Required.Checked Then
                                        strHtmlBuilder.Append(" required")
                                    End If
                                    strHtmlBuilder.Append(" defaultFontSize=""" & CStr(IIf(String.IsNullOrEmpty(defaultFontSize & ""), "12", defaultFontSize)) & """ ")
                                    strHtmlBuilder.Append(">")
                                    If fld.fieldOptionExport.Length = fld.fieldOptionDisplay.Length Then
                                        For intVal As Integer = 0 To fld.fieldOptionExport.Length - 1
                                            If frmMain.ComboBox_ItemDisplay.SelectedItems Is Nothing Then
                                                strHtmlBuilder.AppendLine("<option value='" & fld.fieldOptionExport(intVal) & "'>" & fld.fieldOptionDisplay(intVal) & "</option>")
                                            Else
                                                If fld.fieldListSelection.Contains(fld.fieldOptionDisplay(intVal)) Then
                                                    strHtmlBuilder.AppendLine("<option value='" & fld.fieldOptionExport(intVal) & "' ") 'selected 
                                                    'strHtmlBuilder.Append(" class=""resizeText"" ")
                                                    strHtmlBuilder.AppendLine(">" & fld.fieldOptionDisplay(intVal) & "</option>")
                                                Else
                                                    strHtmlBuilder.AppendLine("<option value='" & fld.fieldOptionExport(intVal) & "' ")
                                                    'strHtmlBuilder.Append(" class=""resizeText"" ")
                                                    strHtmlBuilder.AppendLine(">" & fld.fieldOptionDisplay(intVal) & "</option>")
                                                End If
                                            End If
                                        Next
                                    Else
                                        If fld.fieldOptionExport.Length > 0 Then
                                            Dim intVal As Integer = 0
                                            For Each fldvalue As String In fld.fieldOptionExport
                                                If frmMain.ComboBox_ItemDisplay.SelectedItems Is Nothing Then
                                                    strHtmlBuilder.AppendLine("<option value='" & fldvalue & "'>" & fldvalue & "</option>")
                                                Else
                                                    If fld.fieldListSelection.Contains(fld.fieldOptionDisplay(intVal)) Then
                                                        strHtmlBuilder.AppendLine("<option value='" & fld.fieldOptionExport(intVal) & "' ")  'selected 
                                                        'strHtmlBuilder.Append(" class=""resizeText"" ")
                                                        strHtmlBuilder.AppendLine(">" & fld.fieldOptionDisplay(intVal) & "</option>")
                                                    Else
                                                        strHtmlBuilder.AppendLine("<option value='" & fld.fieldOptionExport(intVal) & "' ")
                                                        'strHtmlBuilder.Append(" class=""resizeText"" ")
                                                        strHtmlBuilder.AppendLine(">" & fld.fieldOptionDisplay(intVal) & "</option>")
                                                    End If
                                                End If
                                                intVal += 1
                                            Next
                                        ElseIf fld.fieldOptionDisplay.Length > 0 Then
                                            For Each fldvalue As String In fld.fieldOptionDisplay
                                                If frmMain.ComboBox_ItemDisplay.SelectedItems Is Nothing Then
                                                    strHtmlBuilder.AppendLine("<option value='" & fldvalue & "'>" & fldvalue & "</option>")
                                                Else
                                                    If fld.fieldListSelection.Contains(fldvalue) Then
                                                        strHtmlBuilder.AppendLine("<option value='" & fldvalue & "' ") 'selected 
                                                        'strHtmlBuilder.Append(" class=""resizeText"" ")
                                                        strHtmlBuilder.AppendLine(">" & fldvalue & "</option>")
                                                    Else
                                                        strHtmlBuilder.AppendLine("<option value='" & fldvalue & "' ")
                                                        'strHtmlBuilder.Append(" class=""resizeText"" ")
                                                        strHtmlBuilder.AppendLine(">" & fldvalue & "</option>")
                                                    End If
                                                End If


                                            Next
                                        End If
                                    End If
                                    strHtmlBuilder.AppendLine("</select>")
                                    strHTML &= strHtmlBuilder.ToString
                                Case iTextSharp.text.pdf.AcroFields.FIELD_TYPE_PUSHBUTTON
                                    frmMain.A0_PDFFormField_LoadProperties(frmMain.Session, fldNm, p, fld.fieldIndex)
                                    If Not frmMain.PDFField_MultiLine.Checked Then
                                        'strHtmlBuilder.AppendLine("<input tabIndex=""" & fldTabIndexTemp & """ type='button' name='" & fld.fieldName & "' id='" & fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex) & "' ")
                                        Dim labelLength As Integer = 0
                                        Select Case frmMain.PuchButton_Options_Behavior.SelectedIndex
                                            Case 0 'None
                                                frmMain.PuchButton_Options_State.SelectedIndex = 0
                                                If frmMain.PuchButton_Options_Label.Text.ToString.ToLower.Contains("reset") Then
                                                    strHtmlBuilder.AppendLine("<input tabIndex=""" & fldTabIndexTemp & """ type='reset' name='" & fld.fieldName & "' id='" & fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex) & "' ")
                                                    'strHtmlBuilder.AppendLine("<input tabIndex=""" & fldTabIndexTemp & """ type='reset' name='" & fld.fieldNameLong & "' id='" & fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex) & "' ")
                                                Else
                                                    strHtmlBuilder.AppendLine("<input tabIndex=""" & fldTabIndexTemp & """ type='submit' name='" & fld.fieldName & "' id='" & fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex) & "' ")
                                                    'strHtmlBuilder.AppendLine("<input tabIndex=""" & fldTabIndexTemp & """ type='submit' name='" & fld.fieldNameLong & "' id='" & fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex) & "' ")
                                                End If
                                                strHtmlBuilder.Append(" value ='" & frmMain.PuchButton_Options_Label.Text & "' ")
                                                labelLength = frmMain.PuchButton_Options_Label.Text.Length
                                            Case 1 'Push
                                                frmMain.PuchButton_Options_State.SelectedIndex = 0
                                                If frm.PuchButton_Options_Label.Text.ToString.ToLower.Contains("reset") Then
                                                    strHtmlBuilder.AppendLine("<input tabIndex=""" & fldTabIndexTemp & """ type='reset' name='" & fld.fieldName & "' id='" & fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex) & "' ")
                                                    'strHtmlBuilder.AppendLine("<input tabIndex=""" & fldTabIndexTemp & """ type='reset' name='" & fld.fieldNameLong & "' id='" & fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex) & "' ")
                                                Else
                                                    strHtmlBuilder.AppendLine("<input tabIndex=""" & fldTabIndexTemp & """ type='submit' name='" & fld.fieldName & "' id='" & fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex) & "' ")
                                                    'strHtmlBuilder.AppendLine("<input tabIndex=""" & fldTabIndexTemp & """ type='submit' name='" & fld.fieldNameLong & "' id='" & fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex) & "' ")
                                                End If
                                                strHtmlBuilder.Append(" value ='" & frmMain.PuchButton_Options_Label.Text & "' ") 'up
                                                strHtmlBuilder.Append(" onmouseout='JavaScript:this.value=""" & frmMain.PuchButton_Options_Label.Text & """;' ") 'up
                                                strHtmlBuilder.Append(" onmouseup='JavaScript:this.value=""" & frmMain.PuchButton_Options_Label.Text & """;' ") 'up
                                                labelLength = frmMain.PuchButton_Options_Label.Text.Length
                                                frmMain.PuchButton_Options_State.SelectedIndex = 1
                                                strHtmlBuilder.Append(" onmousedown='JavaScript:this.value=""" & frmMain.PuchButton_Options_Label.Text & """;' ") 'down
                                                If labelLength < frmMain.PuchButton_Options_Label.Text.Length Then
                                                    labelLength = frmMain.PuchButton_Options_Label.Text.Length
                                                End If
                                                frmMain.PuchButton_Options_State.SelectedIndex = 2
                                                strHtmlBuilder.Append(" onmouseover='JavaScript:this.value=""" & frmMain.PuchButton_Options_Label.Text & """;' ") 'rollover
                                                If labelLength < frmMain.PuchButton_Options_Label.Text.Length Then
                                                    labelLength = frmMain.PuchButton_Options_Label.Text.Length
                                                End If
                                            Case 2 'Outline
                                                frmMain.PuchButton_Options_State.SelectedIndex = 0
                                                If frmMain.PuchButton_Options_Label.Text.ToString.ToLower.Contains("reset") Then
                                                    strHtmlBuilder.AppendLine("<input tabIndex=""" & fldTabIndexTemp & """ type='reset' name='" & fld.fieldName & "' id='" & fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex) & "' ")
                                                    'strHtmlBuilder.AppendLine("<input tabIndex=""" & fldTabIndexTemp & """ type='reset' name='" & fld.fieldNameLong & "' id='" & fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex) & "' ")
                                                Else
                                                    strHtmlBuilder.AppendLine("<input tabIndex=""" & fldTabIndexTemp & """ type='submit' name='" & fld.fieldName & "' id='" & fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex) & "' ")
                                                    'strHtmlBuilder.AppendLine("<input tabIndex=""" & fldTabIndexTemp & """ type='submit' name='" & fld.fieldNameLong & "' id='" & fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex) & "' ")
                                                End If
                                                strHtmlBuilder.Append(" value ='" & frmMain.PuchButton_Options_Label.Text & "' ")
                                                labelLength = frmMain.PuchButton_Options_Label.Text.Length
                                            Case 3 'Invert
                                                frmMain.PuchButton_Options_State.SelectedIndex = 0
                                                If frmMain.PuchButton_Options_Label.Text.ToString.ToLower.Contains("reset") Then
                                                    strHtmlBuilder.AppendLine("<input tabIndex=""" & fldTabIndexTemp & """ type='reset' name='" & fld.fieldName & "' id='" & fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex) & "' ")
                                                    'strHtmlBuilder.AppendLine("<input tabIndex=""" & fldTabIndexTemp & """ type='reset' name='" & fld.fieldNameLong & "' id='" & fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex) & "' ")
                                                Else
                                                    strHtmlBuilder.AppendLine("<input tabIndex=""" & fldTabIndexTemp & """ type='submit' name='" & fld.fieldName & "' id='" & fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex) & "' ")
                                                    'strHtmlBuilder.AppendLine("<input tabIndex=""" & fldTabIndexTemp & """ type='submit' name='" & fld.fieldNameLong & "' id='" & fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex) & "' ")
                                                End If
                                                strHtmlBuilder.Append(" value ='" & frmMain.PuchButton_Options_Label.Text & "' ")
                                                labelLength = frmMain.PuchButton_Options_Label.Text.Length
                                            Case Else
                                                If frmMain.PuchButton_Options_Label.Text.ToString.ToLower.Contains("reset") Then
                                                    strHtmlBuilder.AppendLine("<input tabIndex=""" & fldTabIndexTemp & """ type='reset' name='" & fld.fieldName & "' id='" & fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex) & "' ")
                                                    'strHtmlBuilder.AppendLine("<input tabIndex=""" & fldTabIndexTemp & """ type='reset' name='" & fld.fieldNameLong & "' id='" & fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex) & "' ")
                                                Else
                                                    strHtmlBuilder.AppendLine("<input tabIndex=""" & fldTabIndexTemp & """ type='submit' name='" & fld.fieldName & "' id='" & fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex) & "' ")
                                                    'strHtmlBuilder.AppendLine("<input tabIndex=""" & fldTabIndexTemp & """ type='submit' name='" & fld.fieldNameLong & "' id='" & fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex) & "' ")
                                                End If
                                        End Select
                                        'If frm.PDFField_MaxLenChk.Checked And IsNumeric(frm.PDFField_MaxLen.Text) > 0 Then
                                        '    strHtmlBuilder.Append(" maxlength=""" & frm.PDFField_MaxLen.Text & """ ")
                                        'End If
                                        'strHtmlBuilder.Append(IIf(String.IsNullOrEmpty(fld.cssClass), "", "class='" & fld.cssClass & "' "))
                                        'strHtmlBuilder.Append(IIf(String.IsNullOrEmpty(fld.style), "", "style='" & fld.style & "' "))
                                        'Dim r As System.Drawing.RectangleF = fld.fieldPositionScreen    'GetFieldPositionsReverse(fld.fieldPositioniText, p)
                                        Dim r As System.Drawing.RectangleF = fld.fieldPositionScreen
                                        'Dim r As System.Drawing.RectangleF = New System.Drawing.RectangleF(CInt(btnLeft.Text), CInt(btnTop.Text), CInt(btnWidth.Text), CInt(btnHeight.Text))
                                        strHtmlBuilder.Append(" style=""-moz-appearance: none;")
                                        'strHtmlBuilder.Append("position:absolute;left:" & r.Left & "px;right:" & r.Right & "px;top:" & r.Top + totalPageHeight & "px;bottom:" & r.Bottom + totalPageHeight & "px;")
                                        'strHtmlBuilder.Append("position:absolute;left:" & (r.Left / pageWidth) * 100 & "%;right:" & (r.Right / pageWidth) * 100 & "%;top:" & ((r.Top) / (pageHeight)) * 100 & "%;bottom:" & ((r.Bottom) / (pageHeight)) * 100 & "%;")
                                        strHtmlBuilder.Append("position:absolute;left:" & (r.Left / pageWidth) * 100 & "%;right:" & (r.Right / pageWidth) * 100 & "%;top:" & ((r.Top) / (pageHeight)) * 100 & "%;bottom:" & ((r.Bottom) / (pageHeight)) * 100 & "%;")
                                        strHtmlBuilder.Append("height:" & (r.Height / pageHeight) * 100 & "%;width:" & (r.Width / pageWidth) * 100 & "%;")
                                        strHtmlBuilder.Append("position:absolute;")
                                        strHtmlBuilder.Append("cursor:pointer;")
                                        'strHtmlBuilder.Append("text-align:" & frm.PDFField_TextAlign.Text & ";")
                                        strHtmlBuilder.Append("text-align:" & "center" & ";")
                                        If (frmMain.PDFField_FontSize.Text.ToLower) <> "Auto".ToLower Then
                                            strHtmlBuilder.Append("font-size:" & String.Format(CSng(frmMain.PDFField_FontSize.Text / 14), "#.00") & "em;")
                                            defaultFontSize = frmMain.PDFField_FontSize.Text
                                            fieldListFlowType.Add(fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex), CSng(r.Width / CInt(frmMain.PDFField_FontSize.Text)))
                                        Else
                                            Dim ftSize As Integer = 12
                                            'If Not String.IsNullOrEmpty(PuchButton_Options_Label.Text) Then
                                            If labelLength > 0 Then
                                                Dim continueFontSize As Boolean = True
                                                Do Until continueFontSize = False
                                                    If (ftSize + 1) > r.Height - 4 Then
                                                        continueFontSize = False
                                                        '
                                                    ElseIf ((ftSize + 1) * 0.75) * labelLength >= r.Width - 4 Then
                                                        continueFontSize = False
                                                    End If
                                                    If continueFontSize Then
                                                        ftSize += 1
                                                    End If
                                                Loop
                                            End If
                                            strHtmlBuilder.Append("font-size:" & String.Format(CSng(ftSize / 14), "#.00") & "em;")
                                            fieldListFlowType.Add(fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex), CSng(r.Width / ftSize))
                                        End If
                                        'strHtmlBuilder.Append("height:" & r.Height & "px;width:" & r.Width & "px;")
                                        strHtmlBuilder.Append("color:" & ColorTranslator.ToHtml(frmMain.PDFField_TextColorPicker.BackColor) & ";")
                                        If Not (frmMain.PDFField_BackgroundColorPicker.BackColor) = System.Drawing.Color.Transparent Then
                                            strHtmlBuilder.Append("background-color:" & ColorTranslator.ToHtml(frmMain.PDFField_BackgroundColorPicker.BackColor) & ";")
                                        Else
                                            strHtmlBuilder.Append("background-color:" & ColorTranslator.ToHtml(System.Drawing.Color.White) & ";")
                                        End If
                                        strHtmlBuilder.Append("border-color:" & ColorTranslator.ToHtml(frmMain.PDFField_BorderColorPicker.BackColor) & ";")
                                        Dim strStyle As String = frmMain.PDFField_BorderStyle.Text
                                        Select Case strStyle.ToString.ToLower
                                            Case "solid", "dashed", "dotted", "inset"
                                                strStyle = strStyle & ""
                                            Case "beveled"
                                                strStyle = "ridge"
                                            Case "underline"
                                                strStyle = "solid"
                                        End Select
                                        strHtmlBuilder.Append("border-style:" & strStyle & ";")

                                        strStyle = CStr(frmMain.PDFField_BorderWidth.SelectedIndex + 1).ToString
                                        strHtmlBuilder.Append("border-width:" & strStyle & "px;")
                                        strHtmlBuilder.Append(""" ")
                                        strHtmlBuilder.Append(" class=""resizeText"" ")
                                        strHtmlBuilder.Append(" defaultFontSize=""" & CStr(IIf(String.IsNullOrEmpty(defaultFontSize & ""), "12", defaultFontSize)) & """ ")
                                        strHtmlBuilder.Append(" />")
                                        strHTML &= strHtmlBuilder.ToString
                                    End If
                                Case iTextSharp.text.pdf.AcroFields.FIELD_TYPE_RADIOBUTTON
                                    If lstFields(fldNm).Count > 0 Then
                                        For intValues As Integer = 0 To lstFields(fldNm).Count - 1
                                            frmMain.A0_PDFFormField_LoadProperties(frmMain.Session, fldNm, p, intValues)
                                            fld = lstFields(fldNm)(intValues)
                                            'pField.Add(frm.cFDFDoc.getFieldName(fld.fieldName))
                                            'If Not fld.fieldDictionary Is Nothing Then
                                            '    If Not fld.fieldDictionary.Get(PdfName.AS) Is Nothing Then
                                            '        fld.fieldValue = fld.fieldDictionary.GetAsName(PdfName.AS).ToString.TrimStart("/"c)
                                            '    End If
                                            'End If
                                            If Not fld.fieldDictionary Is Nothing Then
                                                If Not fld.fieldDictionary.Get(PdfName.AP) Is Nothing Then
                                                    Dim tmpDict As PdfDictionary = fld.fieldDictionary.GetAsDict(PdfName.AP)
                                                    If Not tmpDict.Get(PdfName.N) Is Nothing Then
                                                        tmpDict = tmpDict.GetAsDict(PdfName.N)
                                                        For Each k As PdfName In tmpDict.Keys.ToArray()
                                                            If Not k.ToString().TrimStart("/"c) = "Off" Then
                                                                fld.fieldValue = k.ToString().TrimStart("/"c)
                                                            End If
                                                        Next
                                                    End If
                                                End If
                                            End If
                                            strHtmlBuilder.AppendLine("<input tabIndex=""" & fldTabIndexTemp & """ type='radio' name='" & frmMain.cFDFDoc.getFieldName(fld.fieldName) & "' id='" & frm.cFDFDoc.getFieldName(fld.fieldName) & IIf(Not pField.Contains(frm.cFDFDoc.getFieldName(fld.fieldName)), "", "_" & p) & "' value='" & fld.fieldValue & "' ")
                                            'strHtmlBuilder.AppendLine("<input tabIndex=""" & fldTabIndexTemp & """ type='radio' name='" & fld.fieldNameLong & "' id='" & frm.cFDFDoc.getFieldName(fld.fieldName) & IIf(Not pField.Contains(frm.cFDFDoc.getFieldName(fld.fieldName)), "", "_" & p) & "' value='" & fld.fieldValue & "' ")
                                            If Not pField.Contains(frmMain.cFDFDoc.getFieldName(fld.fieldName)) Then
                                                pField.Add(frmMain.cFDFDoc.getFieldName(fld.fieldName))
                                            End If
                                            strHtmlBuilder.Append(IIf(frmMain.PDFField_Value_Checked.Checked, " CHECKED ", ""))
                                            If frmMain.PDFField_Required.Checked Then
                                                strHtmlBuilder.Append(" required")
                                            End If
                                            Dim r As System.Drawing.RectangleF = fld.fieldPositionScreen
                                            strHtmlBuilder.Append(" style=""-moz-appearance: none;") '
                                            'strHtmlBuilder.Append("position:absolute;left:" & r.Left & "px;right:" & r.Right & "px;top:" & r.Top + totalPageHeight & "px;bottom:" & r.Bottom + totalPageHeight & "px;")
                                            'strHtmlBuilder.Append("position:absolute;left:" & (r.Left / pageWidth) * 100 & "%;right:" & (r.Right / pageWidth) * 100 & "%;top:" & ((r.Top) / (pageHeight)) * 100 & "%;bottom:" & ((r.Bottom) / (pageHeight)) * 100 & "%;")
                                            strHtmlBuilder.Append("position:absolute;left:" & (r.Left / pageWidth) * 100 & "%;right:" & (r.Right / pageWidth) * 100 & "%;top:" & ((r.Top) / (pageHeight)) * 100 & "%;bottom:" & ((r.Bottom) / (pageHeight)) * 100 & "%;")
                                            strHtmlBuilder.Append("height:" & (r.Height / pageHeight) * 100 & "%;width:" & (r.Width / pageWidth) * 100 & "%;")
                                            strHtmlBuilder.Append("position:absolute;")
                                            strHtmlBuilder.Append("text-align:" & frmMain.PDFField_TextAlign.Text & ";")
                                            If (frmMain.PDFField_FontSize.Text.ToLower) <> "Auto".ToLower Then
                                                strHtmlBuilder.Append("font-size:" & String.Format(CSng(frmMain.PDFField_FontSize.Text / 14), "#.00") & "em;")
                                                defaultFontSize = frmMain.PDFField_FontSize.Text
                                                'fieldListFlowType.Add(fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex),CSng(r.Width / CSng(frm.PDFField_FontSize.Text)))
                                            Else
                                                strHtmlBuilder.Append("font-size:12px;")
                                                'fieldListFlowType.Add(fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex), CSng(r.Width / CSng(12)))
                                            End If
                                            'strHtmlBuilder.Append("height:" & r.Height & "px;width:" & r.Width & "px;")
                                            strHtmlBuilder.Append("color:" & ColorTranslator.ToHtml(frmMain.PDFField_TextColorPicker.BackColor) & ";")
                                            If Not (frmMain.PDFField_BackgroundColorPicker.BackColor) = System.Drawing.Color.Transparent Then
                                                strHtmlBuilder.Append("background-color:" & ColorTranslator.ToHtml(frmMain.PDFField_BackgroundColorPicker.BackColor) & ";")
                                            Else
                                                strHtmlBuilder.Append("background-color:" & ColorTranslator.ToHtml(System.Drawing.Color.White) & ";")
                                            End If
                                            strHtmlBuilder.Append("border-color:" & ColorTranslator.ToHtml(frmMain.PDFField_BorderColorPicker.BackColor) & ";")
                                            Dim strStyle As String = frmMain.PDFField_BorderStyle.Text
                                            Select Case strStyle.ToString.ToLower
                                                Case "solid", "dashed", "dotted", "inset"
                                                    strStyle = strStyle & ""
                                                Case "beveled"
                                                    strStyle = "ridge"
                                                Case "underline"
                                                    strStyle = "solid"
                                            End Select
                                            strHtmlBuilder.Append("border-style:" & strStyle & ";")

                                            strStyle = CStr(frmMain.PDFField_BorderWidth.SelectedIndex + 1).ToString
                                            strHtmlBuilder.Append("border-width:" & strStyle & "px;")

                                            strHtmlBuilder.Append(""" ")
                                            strHtmlBuilder.AppendLine(" />")
                                            'strHtmlBuilder.Append(IIf(String.IsNullOrEmpty(fld.value(intValues)), fld.fieldName, fld.value(intValues)) & "")
                                        Next
                                        strHTML &= strHtmlBuilder.ToString
                                    End If
                                Case iTextSharp.text.pdf.AcroFields.FIELD_TYPE_SIGNATURE

                                Case iTextSharp.text.pdf.AcroFields.FIELD_TYPE_TEXT
                                    frmMain.A0_PDFFormField_LoadProperties(frmMain.Session, fldNm, p, fld.fieldIndex)
                                    If Not frm.PDFField_MultiLine.Checked Then
                                        strHtmlBuilder.AppendLine("<input tabIndex=""" & fldTabIndexTemp & """ type='text' name='" & fld.fieldName & "' id='" & fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex) & "' value='" & frm.PDFField_Value.Text & "' ")
                                        'strHtmlBuilder.AppendLine("<input tabIndex=""" & fldTabIndexTemp & """ type='text' name='" & fld.fieldNameLong & "' id='" & fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex) & "' value='" & frm.PDFField_Value.Text & "' ")
                                        If frm.PDFField_MaxLenChk.Checked And IsNumeric(frm.PDFField_MaxLen.Text) > 0 Then
                                            strHtmlBuilder.Append(" maxlength=""" & frm.PDFField_MaxLen.Text & """ ")
                                        End If
                                        'strHtmlBuilder.Append(IIf(String.IsNullOrEmpty(fld.cssClass), "", "class='" & fld.cssClass & "' "))
                                        'strHtmlBuilder.Append(IIf(String.IsNullOrEmpty(fld.style), "", "style='" & fld.style & "' "))
                                        'Dim r As System.Drawing.RectangleF = fld.fieldPositionScreen    'GetFieldPositionsReverse(fld.fieldPositioniText, p)
                                        Dim r As System.Drawing.RectangleF = fld.fieldPositionScreen
                                        'Dim r As System.Drawing.RectangleF = New System.Drawing.RectangleF(CInt(btnLeft.Text), CInt(btnTop.Text), CInt(btnWidth.Text), CInt(btnHeight.Text))
                                        strHtmlBuilder.Append(" style=""")      '-moz-appearance: none;
                                        'strHtmlBuilder.Append("position:absolute;left:" & r.Left & "px;right:" & r.Right & "px;top:" & r.Top + totalPageHeight & "px;bottom:" & r.Bottom + totalPageHeight & "px;")
                                        'strHtmlBuilder.Append("position:absolute;left:" & (r.Left / pageWidth) * 100 & "%;right:" & (r.Right / pageWidth) * 100 & "%;top:" & ((r.Top) / (pageHeight)) * 100 & "%;bottom:" & ((r.Bottom) / (pageHeight)) * 100 & "%;")
                                        strHtmlBuilder.Append("position:absolute;left:" & (r.Left / pageWidth) * 100 & "%;right:" & (r.Right / pageWidth) * 100 & "%;top:" & ((r.Top) / (pageHeight)) * 100 & "%;bottom:" & ((r.Bottom) / (pageHeight)) * 100 & "%;")
                                        strHtmlBuilder.Append("height:" & (r.Height / pageHeight) * 100 & "%;width:" & (r.Width / pageWidth) * 100 & "%;")
                                        strHtmlBuilder.Append("position:absolute;")
                                        strHtmlBuilder.Append("text-align:" & frm.PDFField_TextAlign.Text & ";")
                                        'If (frm.PDFField_FontSize.Text.ToLower) <> "Auto".ToLower Then
                                        'strHtmlBuilder.Append("font-size:" & String.Format(CSng(frm.PDFField_FontSize.Text / 14), "#.00") & "em;")
                                        'End If
                                        If (frm.PDFField_FontSize.Text.ToLower) <> "Auto".ToLower Then
                                            strHtmlBuilder.Append("font-size:" & String.Format(CSng(frm.PDFField_FontSize.Text / 14), "#.00") & "em;")
                                            defaultFontSize = frm.PDFField_FontSize.Text
                                            'Dim ftRatio As Integer = 12
                                            'If CInt(frm.PDFField_FontSize.Text) < 12 Then
                                            '    ftRatio = 12 + (12 - CInt(frm.PDFField_FontSize.Text))
                                            'ElseIf CInt(frm.PDFField_FontSize.Text) > 12 Then
                                            '    ftRatio = 12 - (12 - CInt(frm.PDFField_FontSize.Text))
                                            'End If
                                            'If ftRatio <= 0 Then
                                            '    ftRatio = 12
                                            'ElseIf ftRatio > 30 Then
                                            '    ftRatio = 30
                                            'End If
                                            'fieldListFlowType.Add(fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex), ftRatio)
                                            fieldListFlowType.Add(fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex), CSng(r.Width / CSng(frm.PDFField_FontSize.Text)))
                                        Else
                                            Dim ftSize As Integer = 12
                                            If Not String.IsNullOrEmpty(frm.PDFField_Value.Text) Then
                                                Dim continueFontSize As Boolean = True
                                                Do Until continueFontSize = False
                                                    If (ftSize + 1) > r.Height - 4 Then
                                                        continueFontSize = False
                                                    ElseIf (ftSize + 1) * frm.PDFField_Value.Text.Length >= r.Width - 4 Then
                                                        continueFontSize = False
                                                    End If
                                                    If continueFontSize Then
                                                        ftSize += 1
                                                    End If
                                                Loop
                                            End If
                                            strHtmlBuilder.Append("font-size:" & String.Format(CSng(ftSize / 14), "#.00") & "em;")
                                            fieldListFlowType.Add(fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex), CSng(r.Width / ftSize))
                                        End If
                                        'strHtmlBuilder.Append("height:" & r.Height & "px;width:" & r.Width & "px;")
                                        strHtmlBuilder.Append("color:" & ColorTranslator.ToHtml(frm.PDFField_TextColorPicker.BackColor) & ";")
                                        If Not (frm.PDFField_BackgroundColorPicker.BackColor) = System.Drawing.Color.Transparent Then
                                            strHtmlBuilder.Append("background-color:" & ColorTranslator.ToHtml(frm.PDFField_BackgroundColorPicker.BackColor) & ";")
                                        Else
                                            strHtmlBuilder.Append("background-color:" & ColorTranslator.ToHtml(System.Drawing.Color.White) & ";")
                                        End If
                                        strHtmlBuilder.Append("border-color:" & ColorTranslator.ToHtml(frm.PDFField_BorderColorPicker.BackColor) & ";")
                                        Dim strStyle As String = frm.PDFField_BorderStyle.Text
                                        Select Case strStyle.ToString.ToLower
                                            Case "solid", "dashed", "dotted", "inset"
                                                strStyle = strStyle & ""
                                            Case "beveled"
                                                strStyle = "ridge"
                                            Case "underline"
                                                strStyle = "solid"
                                        End Select
                                        strHtmlBuilder.Append("border-style:" & strStyle & ";")

                                        strStyle = CStr(frm.PDFField_BorderWidth.SelectedIndex + 1).ToString
                                        strHtmlBuilder.Append("border-width:" & strStyle & "px;")
                                        strHtmlBuilder.Append(""" ")
                                        strHtmlBuilder.Append(" class=""resizeText"" ")
                                        If frm.PDFField_Required.Checked Then
                                            strHtmlBuilder.Append(" required")
                                        End If
                                        strHtmlBuilder.Append(" defaultFontSize=""" & CStr(IIf(String.IsNullOrEmpty(defaultFontSize & ""), "12", defaultFontSize)) & """ ")
                                        strHtmlBuilder.Append(" />")
                                        strHTML &= strHtmlBuilder.ToString
                                    Else
                                        strHtmlBuilder.AppendLine("<textarea tabIndex=""" & fldTabIndexTemp & """ name='" & fld.fieldName & "' id='" & fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex) & "' ")
                                        'strHtmlBuilder.AppendLine("<textarea tabIndex=""" & fldTabIndexTemp & """ name='" & fld.fieldNameLong & "' id='" & fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex) & "' ")
                                        If frm.PDFField_MaxLenChk.Checked And IsNumeric(frm.PDFField_MaxLen.Text) > 0 Then
                                            strHtmlBuilder.Append(" maxlength=""" & frm.PDFField_MaxLen.Text & """ ")
                                        End If
                                        'strHtmlBuilder.Append(IIf(String.IsNullOrEmpty(fld.cssClass), "", "class='" & fld.cssClass & "' "))
                                        'strHtmlBuilder.Append(IIf(String.IsNullOrEmpty(fld.style), "", "style='" & fld.style & "' "))
                                        'Dim r As System.Drawing.RectangleF = fld.fieldPositionScreen    'GetFieldPositionsReverse(fld.fieldPositioniText, p)
                                        Dim r As System.Drawing.RectangleF = fld.fieldPositionScreen
                                        'Dim r As System.Drawing.RectangleF = New System.Drawing.RectangleF(CInt(btnLeft.Text), CInt(btnTop.Text), CInt(btnWidth.Text), CInt(btnHeight.Text))
                                        strHtmlBuilder.Append(" style=""") '-moz-appearance: none;
                                        'strHtmlBuilder.Append("position:absolute;left:" & r.Left & "px;right:" & r.Right & "px;top:" & r.Top + totalPageHeight & "px;bottom:" & r.Bottom + totalPageHeight & "px;")
                                        'strHtmlBuilder.Append("position:absolute;left:" & (r.Left / pageWidth) * 100 & "%;right:" & (r.Right / pageWidth) * 100 & "%;top:" & ((r.Top) / (pageHeight)) * 100 & "%;bottom:" & ((r.Bottom) / (pageHeight)) * 100 & "%;")
                                        strHtmlBuilder.Append("position:absolute;left:" & (r.Left / pageWidth) * 100 & "%;right:" & (r.Right / pageWidth) * 100 & "%;top:" & ((r.Top) / (pageHeight)) * 100 & "%;bottom:" & ((r.Bottom) / (pageHeight)) * 100 & "%;")
                                        strHtmlBuilder.Append("height:" & (r.Height / pageHeight) * 100 & "%;width:" & (r.Width / pageWidth) * 100 & "%;")
                                        strHtmlBuilder.Append("position:absolute;")
                                        strHtmlBuilder.Append("text-align:" & frm.PDFField_TextAlign.Text & ";")
                                        If (frm.PDFField_FontSize.Text.ToLower) <> "Auto".ToLower Then
                                            strHtmlBuilder.Append("font-size:" & String.Format(CSng(frm.PDFField_FontSize.Text / 14), "#.00") & "em;")
                                            defaultFontSize = frm.PDFField_FontSize.Text
                                            fieldListFlowType.Add(fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex), CSng(r.Width / CSng(frm.PDFField_FontSize.Text)))

                                        Else
                                            Dim ftSize As Integer = 12
                                            'If frm.PDFField_Value.Text.Contains(Environment.NewLine) Then
                                            '    For Each ln As String In frm.PDFField_Value.Text.ToString.Split(Environment.NewLine)
                                            '        If Not String.IsNullOrEmpty(ln) Then
                                            '            Dim ftTemp As Integer = 12
                                            '            Dim continueFontSize As Boolean = True
                                            '            Do Until continueFontSize = False
                                            '                If (ftTemp + 1) > r.Height - 4 Then
                                            '                    continueFontSize = False
                                            '                ElseIf (ftTemp + 1) * ln.Length >= r.Width - 4 Then
                                            '                    continueFontSize = False
                                            '                End If
                                            '                If continueFontSize Then
                                            '                    ftTemp += 1
                                            '                End If
                                            '            Loop
                                            '            If ftTemp < ftSize Then
                                            '                ftSize = ftTemp
                                            '            End If
                                            '        End If
                                            '    Next
                                            'End If
                                            strHtmlBuilder.Append("font-size:" & String.Format(CSng(ftSize / 14), "#.00") & "em;")
                                            fieldListFlowType.Add(fld.fieldName & IIf(Not pField.Contains(fld.fieldName), "", "_" & p & "_" & fld.fieldIndex), CSng(r.Width / ftSize))
                                        End If
                                        'strHtmlBuilder.Append("height:" & r.Height & "px;width:" & r.Width & "px;")
                                        strHtmlBuilder.Append("color:" & ColorTranslator.ToHtml(frm.PDFField_TextColorPicker.BackColor) & ";")
                                        If Not (frm.PDFField_BackgroundColorPicker.BackColor) = System.Drawing.Color.Transparent Then
                                            strHtmlBuilder.Append("background-color:" & ColorTranslator.ToHtml(frm.PDFField_BackgroundColorPicker.BackColor) & ";")
                                        Else
                                            strHtmlBuilder.Append("background-color:" & ColorTranslator.ToHtml(System.Drawing.Color.White) & ";")
                                        End If
                                        strHtmlBuilder.Append("border-color:" & ColorTranslator.ToHtml(frm.PDFField_BorderColorPicker.BackColor) & ";")
                                        Dim strStyle As String = frm.PDFField_BorderStyle.Text
                                        Select Case strStyle.ToString.ToLower
                                            Case "solid", "dashed", "dotted", "inset"
                                                strStyle = strStyle & ""
                                            Case "beveled"
                                                strStyle = "ridge"
                                            Case "underline"
                                                strStyle = "solid"
                                        End Select
                                        strHtmlBuilder.Append("border-style:" & strStyle & ";")

                                        strStyle = CStr(frm.PDFField_BorderWidth.SelectedIndex + 1).ToString
                                        strHtmlBuilder.Append("border-width:" & strStyle & "px;")
                                        strHtmlBuilder.Append(""" ")
                                        strHtmlBuilder.Append(" class=""resizeText"" ")
                                        If frm.PDFField_Required.Checked Then
                                            strHtmlBuilder.Append(" required")
                                        End If
                                        strHtmlBuilder.Append(" defaultFontSize=""" & CStr(IIf(String.IsNullOrEmpty(defaultFontSize & ""), "12", defaultFontSize)) & """ ")
                                        strHtmlBuilder.Append(">" & frm.PDFField_Value.Text & "</textarea>")
                                        strHTML &= strHtmlBuilder.ToString
                                    End If

                            End Select
                            'Next
                            If Not pField.Contains(fld.fieldName) Then
                                pField.Add(fld.fieldName)
                            End If

                        Next
                    End If
                    If fldTabIndex < fldTabMax Then
                        fldTabIndex = fldTabMax
                    End If
                    strHTML &= "</div>" & Environment.NewLine
                    totalPageHeight += reader.GetPageSizeWithRotation(p).Height * frm.getPercent()
                    'If blnIncludeFields Then
                    '    strHTML &= "<script type=""text/javascript"">" & Environment.NewLine
                    '    'strHTML &= Environment.NewLine & "$(document).ready(function() {var resizeText = function () {var preferredFontSize = 90;/*8.5 x 11= 2550px * 3300px*/var preferredSize = " & pageWidth & " * " & pageHeight & ";var currentSize = $(window).width() * $(window).height();var scalePercentage = Math.sqrt(currentSize) / Math.sqrt(preferredSize);var newFontSize = preferredFontSize * scalePercentage;$('#page_" & p & "').css('font-size', newFontSize + '%');};$(window).bind('resize', function() {resizeText();}).trigger('resize');});"
                    '    strHTML &= Environment.NewLine & "$(document).ready(function() {var resizeText = function () {$('#page_" & p & " > input').each(function(index, element){if(!$(element).attr('defaultFontSize')==''){var preferredFontSize = parseInt(parseFloat($(element).attr('defaultFontSize') / 16)*100);var preferredSize = " & pageWidth & " * " & pageHeight & ";var currentSize = $(window).width() * $(window).height();var scalePercentage = Math.sqrt(currentSize) / Math.sqrt(preferredSize);var newFontSize = preferredFontSize * scalePercentage;console.log($(element).attr('id') + ':' + $(element).attr('defaultFontSize') + ':' + preferredFontSize);$(element).css('font-size',newFontSize + '%');};});};$(window).bind('resize', function() {resizeText();}).trigger('resize');});"
                    '    strHTML &= "</script>" & Environment.NewLine
                    'End If

                Next
                If blnIncludeFields Then
                    'strHTML &= "<script type=""text/javascript"">" & Environment.NewLine
                    'strHTML &= "" & Environment.NewLine
                    'strHTML &= "//https://www.appelsiini.net/projects/lazyload" & Environment.NewLine
                    'strHTML &= "$(function() {" & Environment.NewLine
                    ''strHTML &= Environment.NewLine & "//$(""img.lazy"").lazyload();" & Environment.NewLine
                    ''strHTML &= Environment.NewLine & "//$("".resizeText"").flowtype();" 'minimum: 500,maximum: 1200,minFont: 12,maxFont: 40,fontRatio: 30
                    'strHTML &= Environment.NewLine & "//$('.social').mask('000-00-0000');"
                    'strHTML &= Environment.NewLine & "//$('.datemask').mask('00/00/0000');"
                    ''For kIdx As Integer = 0 To fieldListFlowType.Count - 1
                    ''strHTML &= Environment.NewLine & "/////$(""#" & fieldListFlowType.Keys(kIdx) & """).flowtype({fontRatio:" & fieldListFlowType.Values(kIdx).ToString() & "});"
                    ''Next
                    ''strHTML &= Environment.NewLine & "$(""img.lazy"").show().lazyload();" & Environment.NewLine
                    'strHTML &= Environment.NewLine & "}"c & ")"c & ";"c & Environment.NewLine
                    'strHTML &= "" & Environment.NewLine
                    'strHTML &= "</script>" & Environment.NewLine
                End If
                'strHTML &= "<script type=""text/javascript"">" & Environment.NewLine
                'strHTML &= File.ReadAllText(frm.ApplicationDataFolder(True, "") & "\" & "html\downloadFDF.js").ToString().Replace("{ PDFPATH }", frm.fpath.ToString.Replace("\", "\\\\"))
                'strHTML &= "</script>"
                strHTML &= Environment.NewLine & "</form>" & Environment.NewLine
                strHTML &= "</div>" & Environment.NewLine
                'strHTML &= "</body>" & Environment.NewLine
                'strHTML &= "</html>" & Environment.NewLine
                strHTML &= "</div>" & Environment.NewLine
                Return strHTML
                'strHTML = chtml.OutPutHTMLToString() 'chtml.toHTMLFields(True, New String() {"_", "-", "."}, "", "", 1, "")
                'If Not String.IsNullOrEmpty(fn) Then
                '    System.IO.File.WriteAllText(fn, strHTML)
                '    'Process.Start(Path.GetDirectoryName(fn))
                '    'Process.Start(fn)
                'Else
                '    Throw New Exception("File name Is empty")
                'End If
                'Dim fn As String = appPathTemp & Path.GetFileNameWithoutExtension(fpath & "") & ".htm" & ""
                'Dim sd As New SaveFileDialog
                'sd.CheckPathExists = True
                'sd.FileName = Path.GetFileName(fn & "")
                'sd.InitialDirectory = appPathTemp
                'sd.Filter = "HTM|*.htm|HTML|*.html|Text|*.txt|All Files|*.*"
                'sd.FilterIndex = 0
                'sd.DefaultExt = ".htm"
                'Me.Hide()
                'Select Case sd.ShowDialog(Me)
                '    Case Windows.Forms.DialogResult.OK, Windows.Forms.DialogResult.Yes
                '        fn = sd.FileName & ""
                '        If Not String.IsNullOrEmpty(fn) Then
                '            System.IO.File.WriteAllText(fn, strHTML)
                '            Process.Start(fn)
                '        Else
                '            Throw New Exception("File name Is empty")
                '        End If
                '    Case Else
                '        Exit Select
                'End Select
                frm.StatusToolStrip = "Status: HTML File created"
            Catch ex As Exception
                frm.StatusToolStrip = "Status: Error File NOT created - " & ex.Message.ToString
                frm.TimeStampAdd(ex, frm.debugMode) ' NK 2016-06-30() ' Err.Clear()  ' NK3 ' 
            Finally
                frm.ToolStripProgressBar1.Visible = False
                frm.DeleteTempFilesImageCache()
                frm.Session = b
                frm.LoadPDFReaderDoc(frm.pdfOwnerPassword)
                frm.A0_LoadPDF()
                Me.Show()
                Me.BringToFront()
            End Try
        Catch ex123 As Exception
            frm.StatusToolStrip = "Status: Error File NOT created - " & ex123.Message.ToString
            frm.TimeStampAdd(ex123, frm.debugMode) ' NK 2016-06-30() ' Err.Clear()  ' NK3 ' 
        Finally
            If Not ParentForm Is Nothing Then
                'Me.Hide()
                ParentForm.Show()
            End If
        End Try
        Return ""
    End Function

    Private Sub ListBoxAutocomplete_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.DoubleClick
        Try
            If Me.SelectedIndex >= 0 Then
                TextBoxField.Focus()
                Dim selItem As String = Me.Items(Me.SelectedIndex).ToString
                Dim curLoc As Integer = TextBoxField.SelectionStart
                Dim spaceLoc As Integer = InStrRev(TextBoxField.Text.ToString.Replace(vbLf, " "), " ", curLoc, CompareMethod.Text) ', curLoc, CompareMethod.Text)
                TextBoxField.SelectionStart = spaceLoc
                TextBoxField.SelectionLength = Math.Abs(spaceLoc - curLoc)
                Try
                    If Me.SelectedItem.ToString().ToLower.Contains("Add All PDF Fields".ToLower) Then
                        Dim strInser As New System.Text.StringBuilder
                        For Each strFld As String In fieldNames.ToArray
                            If Not strFld.ToLower.Contains("Add All PDF Fields".ToLower) Then
                                strInser.AppendLine(strFld.ToString.Replace("_", " ").Replace("-", " ").Replace(".", " ").Replace("  ", " ") & ": " & prefix & strFld.ToString & postfix)
                            End If
                        Next
                        TextBoxField.SelectedText = strInser.ToString
                    ElseIf Me.SelectedItem.ToString().ToLower.Contains("Add All Form Images".ToLower) Then
                        Dim strInser As New System.Text.StringBuilder
                        'Dim frmMain1 As frmMain = DirectCast(Me.Parent, frmMain)
                        If Not frmMain1 Is Nothing Then
                            strInser.AppendLine("<br/>")
                            For i As Integer = 1 To frmMain1.pdfReaderDoc.NumberOfPages
                                strInser.AppendLine("<img src=""{pageImage_" & i.ToString() & "}"" style=""width:100%;max-width:100%;min-width:100%;height:auto;display:block;"" /><br/>")
                            Next
                            TextBoxField.SelectedText = strInser.ToString
                        End If
                    ElseIf Me.SelectedItem.ToString().ToLower.Contains("Add All Page Images And Fields".ToLower) Then
                        Dim strInser As New System.Text.StringBuilder
                        'Dim frmMain1 As frmMain = DirectCast(Me.Parent, frmMain)
                        If Not frmMain1 Is Nothing Then
                            'For i As Integer = 1 To frmMain1.pdfReaderDoc.NumberOfPages
                            '    strInser.AppendLine("<img src=""{pageImage_" & i.ToString() & "}"" style=""width:100%;height:auto;display:block;"" /><br/>")
                            'Next
                            strInser.Append(createHTMLFile(frmMain1, frmMerge1, "", True, frmMerge1, ""))
                            TextBoxField.SelectedText = strInser.ToString
                        End If
                    Else
                        TextBoxField.SelectedText = CStr(Me.SelectedItem.ToString() & "")
                    End If
                Catch ex As Exception
                    TextBoxField.SelectedText = CStr(Me.SelectedItem.ToString() & "")
                End Try
                TextBoxField.Focus()
                TextBoxField.SelectionStart = TextBoxField.SelectionStart + TextBoxField.SelectedText.ToString.Length
                TextBoxField.SelectionLength = 0
                Me.Hide()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
