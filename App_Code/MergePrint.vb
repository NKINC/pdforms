Imports System.IO
Public Module MergePrint
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 NK-INC.COM All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>

    Const TIMEOUT As Integer = 25
    Const ADS_JOB_SPOOLING = &H8
    Const ADS_JOB_PRINTING = &H10
    Dim sConn, sSource, Rec, fld, firstJob, readerProcess
    Dim objRS, objShell
    Private Declare Function GetComputerName Lib "kernel32" Alias "GetComputerNameA" (ByVal lpBuffer As String, ByVal nSize As Long) As Long
    Public Function ComputerName() As String
        Dim sBuffer As String
        Dim lAns As Long
        Dim strComputerName As String = ""
        sBuffer = Space$(255)
        lAns = GetComputerName(sBuffer, 255)
        If lAns <> 0 Then
            strComputerName = Left$(sBuffer, InStr(sBuffer, Chr(0)) - 1)
        Else
            Err.Raise(Err.LastDllError, ,
              "A system call returned an error code of " _
               & Err.LastDllError)
        End If
        Return strComputerName
    End Function
    Public Sub MergeAndPrint(ByVal dSet As DataSet, ByVal pdfPath As String, ByVal prntrName As String)
        Dim base As String
        Dim xfdfFileName As String
        Dim Rec As Integer
        objShell = CreateObject("WScript.Shell")
        If Not File.Exists(pdfPath) Then
            pdfPath = pdfPath
            If Not File.Exists(pdfPath) Then
                MsgBox("ERROR - File Not Found: '" & pdfPath & "'")
                Exit Sub
            End If
        End If
        base = Application.StartupPath & "\"
        firstJob = True
        Rec = 1
        Try
            Dim dRow As DataRow
            For Each dRow In dSet.Tables(0).Rows
                xfdfFileName = base & "\" & Rec & ".pdf"
                Dim Contents As String = ""
                Dim bAns As Boolean = False
                Dim tmpFDF As New FDFApp.FDFDoc_Class
                Dim tmpApp As New FDFApp.FDFApp_Class
                tmpFDF = tmpApp.FDFCreate
                If tmpFDF.PDFisXFA(System.IO.File.ReadAllBytes(pdfPath)) Then
                    If tmpFDF.PDFisXFADynamic(System.IO.File.ReadAllBytes(pdfPath)) Then
                        Throw New Exception("UNSUPPORTED PDF FORMAT (DYNAMIC XFA)")
                    Else
                        tmpFDF = tmpApp.PDFOpenFromFile(pdfPath)
                        tmpFDF.XDPSetValuesFromDataRow(dRow)
                        tmpFDF.FDFSetFile(pdfPath)
                        tmpFDF.PDFMergeXDP2File(xfdfFileName, pdfPath, True, "")
                    End If
                Else
                    tmpFDF = tmpApp.PDFOpenFromFile(pdfPath)
                    tmpFDF.XDPSetValuesFromDataRow(dRow)
                    tmpFDF.FDFSetFile(pdfPath)
                    tmpFDF.PDFMergeFDF2File(xfdfFileName, pdfPath, True, "")
                End If
                PrintPDF(xfdfFileName, pdfPath, prntrName)
                Rec = Rec + 1
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub MergeAndSavePDF(ownerFrmMain As frmMain, ByVal dSet As DataSet, ByVal FolderPath As String, ByVal FILENAME As String, ByVal File_FORMAT As String, ByVal pdfownerPw As String, ByVal SaveFieldNames As String, ByVal Pdfpath As String, Optional ByVal FieldNameSeparator As String = "_", Optional ByVal progress As ProgressBar = Nothing, Optional ByVal FieldMappings As ListBox = Nothing, Optional ByRef lblStatus As Label = Nothing, Optional ByVal FlattenPDF As Boolean = False)
        Dim base As String
        Dim xfdfFileName As String
        Dim Rec As Integer
        objShell = CreateObject("WScript.Shell")
        If String.IsNullOrEmpty(FolderPath) Then
            Exit Sub
        End If
        If Not System.IO.Directory.Exists(FolderPath) Then
            System.IO.Directory.CreateDirectory(FolderPath)
        End If
        If Not FolderPath.EndsWith("\"c) Then
            FolderPath = FolderPath & "\"
        End If
        base = FolderPath
        firstJob = True
        Rec = 1
        Dim dRow As DataRow
        Dim progRowCount As Integer = dSet.Tables(0).Rows.Count
        Dim progRowIndex As Integer = 0
        If Not progress Is Nothing Then
            progress.Value = 0
        End If
        Dim recordIndex As Integer = -1
        For Each dRow In dSet.Tables(0).Rows
            recordIndex += 1
            Application.DoEvents()
            If frmMerge1.blnCancelProcess = True Then
                lblStatus.Text &= " - Cancelled process"
                frmMerge1.blnCancelProcess = False
                Exit Sub
            End If
            Dim bAns As Boolean = False
            xfdfFileName = base & FILENAME
            If Not System.IO.Directory.Exists(FolderPath) Then
                System.IO.Directory.CreateDirectory(FolderPath)
            End If
            Dim xFDFApp As New FDFApp.FDFApp_Class
            Dim xFDFDoc As New FDFApp.FDFDoc_Class
            xFDFDoc = xFDFApp.PDFOpenFromBuf(frmMerge1.SessionBytes, True, True, frmMerge1.pdfOwnerPassword & "")
            If Not FieldMappings Is Nothing Then
                For Each item As String In FieldMappings.Items
                    If item.ToString.Split("="c).Length = 2 Then
                        Dim PDFField As String = item.ToString.Split("="c)(0).ToString
                        Dim DBField As String = item.ToString.Split("="c)(1).ToString
                        Dim strConcate As String = ""
                        If ownerFrmMain.GetFormFieldType(frmMerge1.SessionBytes, PDFField & "") = iTextSharp.text.pdf.AcroFields.FIELD_TYPE_CHECKBOX Then
                            Dim radioGroup As iTextSharp.text.pdf.PdfDictionary = ownerFrmMain.iTextFieldItemPdfDictionary(PDFField & "")
                            Dim da As iTextSharp.text.pdf.PdfName = radioGroup.GetAsName(iTextSharp.text.pdf.PdfName.AS)
                            Dim val As String = "Off"
                            Dim valExport As String = "Yes"
                            If Not da Is Nothing Then
                                val = da.ToString.TrimStart("/"c).TrimEnd("/"c).ToString & ""
                                Dim vName As iTextSharp.text.pdf.PdfName = radioGroup.GetAsName(iTextSharp.text.pdf.PdfName.V)
                                Dim vStr As String = vName.ToString.TrimStart("/"c).TrimEnd("/"c).ToString & ""
                                If Not da Is Nothing Then
                                    val = da.ToString.TrimStart("/"c)
                                Else
                                    val = "Off"
                                End If
                            End If
                            Dim v As iTextSharp.text.pdf.PdfName = radioGroup.GetAsName(iTextSharp.text.pdf.PdfName.V)
                            If Not v Is Nothing Then
                                valExport = v.ToString.TrimStart("/"c).TrimEnd("/"c).ToString & ""
                            End If
                            Select Case dSet.Tables(0).Columns(DBField.ToString.TrimStart("{"c).TrimEnd("}"c)).DataType.ToString
                                Case GetType(System.Boolean).ToString
                                    Select Case dSet.Tables(0).Rows(recordIndex)(DBField.ToString.TrimStart("{"c).TrimEnd("}"c).ToString)
                                        Case False
                                            xFDFDoc.FDFSetValue(PDFField & "", InjectFieldNameValues(DBField, "Off") & "", True, True)
                                        Case Else
                                            Try
                                                xFDFDoc.FDFSetValue(PDFField & "", InjectFieldNameValues(DBField, valExport.ToString) & "", True, True)
                                            Catch exBln As Exception
                                                Err.Clear()
                                            End Try
                                    End Select
                                Case Else
                                    xFDFDoc.FDFSetValue(PDFField & "", InjectFieldNameValues(DBField, val.ToString) & "", True, True)
                            End Select
                        Else
                            xFDFDoc.FDFSetValue(PDFField & "", InjectFieldNameValues(DBField, dRow) & "", True, True)
                        End If
                    End If
                Next
            Else
                xFDFDoc.FDFSetValuesFromDataRow(dRow, Nothing)
            End If
            xFDFDoc.FDFSetFile(Pdfpath)
            Dim pdfStream As MemoryStream
            Select Case File_FORMAT.ToString.ToLower.Trim("."c)
                Case "pdf"
                    pdfStream = New MemoryStream(xFDFDoc.PDFMergeFDF2Buf(Pdfpath, FlattenPDF, pdfownerPw))
                Case "fdf"
                    pdfStream = New MemoryStream(xFDFDoc.FDFSavetoBuf(FDFApp.FDFDoc_Class.FDFType.FDF))
                Case "xfdf"
                    pdfStream = New MemoryStream(xFDFDoc.FDFSavetoBuf(FDFApp.FDFDoc_Class.FDFType.xFDF))
                Case "xdp"
                    pdfStream = New MemoryStream(xFDFDoc.FDFSavetoBuf(FDFApp.FDFDoc_Class.FDFType.XDP))
                Case "xml"
                    pdfStream = New MemoryStream(xFDFDoc.FDFSavetoBuf(FDFApp.FDFDoc_Class.FDFType.XML))
                Case Else
                    pdfStream = New MemoryStream(xFDFDoc.PDFMergeFDF2Buf(Pdfpath, FlattenPDF, pdfownerPw))
            End Select
            If xfdfFileName.Contains("."c) Then
                xfdfFileName = xfdfFileName.Replace("." & Path.GetExtension("." & xfdfFileName).ToString.Trim("."c), "." & File_FORMAT.ToString.ToLower.Trim("."c))
            Else
                xfdfFileName = xfdfFileName & "." & File_FORMAT.ToString.ToLower.Trim("."c)
            End If
            If pdfStream.CanSeek Then
                pdfStream.Seek(0, SeekOrigin.Begin)
            End If
            Dim docBytes() As Byte = pdfStream.ToArray()
            xfdfFileName = InjectFieldNameValues(xfdfFileName, xFDFDoc)
            Do While xfdfFileName.Contains(".." & File_FORMAT.Replace(".", ""))
                xfdfFileName = xfdfFileName.Replace(".." & File_FORMAT.Replace(".", ""), "." & File_FORMAT.Replace(".", ""))
            Loop
            File.WriteAllBytes(xfdfFileName, docBytes.ToArray())
            If Not progress Is Nothing Then
                progRowIndex = Rec
                progress.Value = CInt((progRowIndex / progRowCount) * 100)
                If Not lblStatus Is Nothing Then
                    lblStatus.Text = "Status: " & progress.Value & "%" & " completed"
                End If
            End If
            Rec = Rec + 1
        Next
        lblStatus.Text = "Status: 100% completed"
    End Sub
    Public Function MergeRecordPDF(ByVal recordIndex As Integer, ByVal dSet As DataSet, ByVal File_FORMAT As String, ByVal pdfownerPw As String, ByVal pdfBytes As Byte(), ByRef ownerFrmMain As frmMain, Optional ByVal FieldMappings As ListBox = Nothing) As Byte()
        Try
            Dim progRowCount As Integer = dSet.Tables(0).Rows.Count
            Dim progRowIndex As Integer = recordIndex
            Dim dRow As DataRow = dSet.Tables(0).Rows(recordIndex)
            Dim bAns As Boolean = False
            Dim xFDFApp As New FDFApp.FDFApp_Class
            Dim xFDFDoc As New FDFApp.FDFDoc_Class
            If pdfBytes Is Nothing Then
                pdfBytes = ownerFrmMain.Session("savedSource")
            End If
            If pdfBytes Is Nothing Then
                pdfBytes = ownerFrmMain.Session()
            End If
            xFDFDoc = xFDFApp.PDFOpenFromBuf(pdfBytes, True, True, pdfownerPw & "")
            If Not FieldMappings Is Nothing Then
                For Each item As String In FieldMappings.Items
                    If item.ToString.Split("="c).Length = 2 Then
                        Dim PDFField As String = item.ToString.Split("="c)(0).ToString
                        Dim DBField As String = item.ToString.Split("="c)(1).ToString
                        Dim strConcate As String = ""
                        If ownerFrmMain.GetFormFieldType(pdfBytes, PDFField & "") = iTextSharp.text.pdf.AcroFields.FIELD_TYPE_CHECKBOX Then
                            Dim radioGroup As iTextSharp.text.pdf.PdfDictionary = ownerFrmMain.iTextFieldItemPdfDictionary(PDFField & "")
                            Dim da As iTextSharp.text.pdf.PdfName = Nothing
                            If Not radioGroup.Get(iTextSharp.text.pdf.PdfName.AS) Is Nothing Then
                                da = radioGroup.GetAsName(iTextSharp.text.pdf.PdfName.AS)
                            End If
                            Dim val As String = "Off"
                            Dim valExport As String = "Yes"
                            If Not da Is Nothing Then
                                val = da.ToString.TrimStart("/"c).TrimEnd("/"c).ToString & ""
                                Dim vName As iTextSharp.text.pdf.PdfName = New iTextSharp.text.pdf.PdfName("Off")
                                If Not radioGroup.Get(iTextSharp.text.pdf.PdfName.V) Is Nothing Then
                                    vName = radioGroup.GetAsName(iTextSharp.text.pdf.PdfName.V)
                                End If
                                Dim vStr As String = vName.ToString.TrimStart("/"c).TrimEnd("/"c).ToString & ""
                                If Not da Is Nothing Then
                                    val = da.ToString.TrimStart("/"c)
                                Else
                                    val = "Off"
                                End If
                            End If
                            Dim v As iTextSharp.text.pdf.PdfName = Nothing
                            If Not radioGroup.Get(iTextSharp.text.pdf.PdfName.V) Is Nothing Then
                                v = radioGroup.GetAsName(iTextSharp.text.pdf.PdfName.V)
                            End If
                            If Not v Is Nothing Then
                                valExport = v.ToString.TrimStart("/"c).TrimEnd("/"c).ToString & ""
                            End If
                            Select Case dSet.Tables(0).Columns(DBField.ToString.TrimStart("{"c).TrimEnd("}"c)).DataType.ToString
                                Case GetType(System.Boolean).ToString
                                    Select Case dSet.Tables(0).Rows(recordIndex)(DBField.ToString.TrimStart("{"c).TrimEnd("}"c).ToString)
                                        Case False
                                            xFDFDoc.FDFSetValue(PDFField & "", InjectFieldNameValues(DBField, "Off") & "", True, True)
                                        Case Else
                                            Try
                                                xFDFDoc.FDFSetValue(PDFField & "", InjectFieldNameValues(DBField, valExport.ToString) & "", True, True)
                                            Catch exBln As Exception
                                                Err.Clear()
                                            End Try
                                    End Select
                                Case Else
                                    xFDFDoc.FDFSetValue(PDFField & "", InjectFieldNameValues(DBField, val.ToString) & "", True, True)
                            End Select
                        Else
                            xFDFDoc.FDFSetValue(PDFField & "", InjectFieldNameValues(DBField, dRow) & "", True, True)
                        End If
                    End If
                Next
            Else
                xFDFDoc.FDFSetValuesFromDataRow(dRow, Nothing)
            End If
            Dim pdfStream As MemoryStream
            Select Case File_FORMAT.ToString.ToLower.Trim("."c)
                Case "pdf"
                    pdfStream = New MemoryStream(xFDFDoc.PDFMergeFDF2Buf(pdfBytes, False, pdfownerPw))
                Case "fdf"
                    pdfStream = New MemoryStream(xFDFDoc.FDFSavetoBuf(FDFApp.FDFDoc_Class.FDFType.FDF))
                Case "xfdf"
                    pdfStream = New MemoryStream(xFDFDoc.FDFSavetoBuf(FDFApp.FDFDoc_Class.FDFType.xFDF))
                Case "xdp"
                    pdfStream = New MemoryStream(xFDFDoc.FDFSavetoBuf(FDFApp.FDFDoc_Class.FDFType.XDP))
                Case "xml"
                    pdfStream = New MemoryStream(xFDFDoc.FDFSavetoBuf(FDFApp.FDFDoc_Class.FDFType.XML))
                Case Else
                    pdfStream = New MemoryStream(xFDFDoc.PDFMergeFDF2Buf(pdfBytes, False, pdfownerPw))
            End Select
            If pdfStream.CanSeek Then
                pdfStream.Seek(0, SeekOrigin.Begin)
            End If
            Return pdfStream.ToArray
        Catch ex As Exception
            Err.Clear()
        End Try
        Return Nothing
    End Function
    Public Function InjectFieldNameValues(ByVal strInput As String, ByVal cfdfDoc As FDFApp.FDFDoc_Class) As String
        Dim strTmp As String = strInput & ""
        If String.IsNullOrEmpty(strInput & "") Then
            Return strInput & ""
        End If
        For Each fld As FDFApp.FDFDoc_Class.FDFField In cfdfDoc.XDPGetAllFields().ToArray
            Try
                strTmp = strTmp.Replace("{" & fld.FieldName.ToString & "}", cfdfDoc.FDFGetValue(fld.FieldName.ToString.ToString.Replace("{", "").Replace("}", "").ToString(), False) & "")
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
                If IsDBNull(dr(fld.ColumnName.ToString & "")) Then
                    strTmp = strTmp.Replace("{" & fld.ColumnName.ToString & "}", "")
                ElseIf String.IsNullOrEmpty(dr(fld.ColumnName.ToString & "")) Then
                    strTmp = strTmp.Replace("{" & fld.ColumnName.ToString & "}", "")
                Else
                    strTmp = strTmp.Replace("{" & fld.ColumnName.ToString & "}", CStr(dr(fld.ColumnName.ToString & "")) & "")
                End If
            Catch ex As Exception
                Err.Clear()
            End Try
        Next
        Return strTmp.ToString & ""
    End Function
    Public Function InjectFieldNameValues(ByVal strInput As String, ByVal val As String) As String
        Dim strTmp As String = strInput & ""
        If String.IsNullOrEmpty(strInput & "") Then
            Return strInput & ""
        End If
        Try
            strTmp = strTmp.Replace(strInput, CStr(val) & "")
        Catch ex As Exception
            Err.Clear()
        End Try
        Return strTmp.ToString & ""
    End Function
    Public Sub MergeAndEmailPDF(ByVal dSet As DataSet, ByVal SMTPClient1 As System.Net.Mail.SmtpClient, ByVal MSG_BODY As String, ByVal MSG_SUBJECT As String, ByVal MSG_TO As String, ByVal MSG_CC As String, ByVal MSG_BCC As String, ByVal MSG_FROM_Email As String, ByVal MSG_FROM_Name As String, ByVal MSG_ATTACHMENT_FILENAME As String, ByVal MSG_ATTACHMENT_FORMAT As String, ByVal pdfownerPw As String, ByVal SaveFieldNames As String, ByVal Pdfpath As String, Optional ByVal FieldNameSeparator As String = "_", Optional ByVal progress As ProgressBar = Nothing, Optional ByVal FieldMappings As ListBox = Nothing, Optional ByRef lblStatus As Label = Nothing, Optional ByVal FlattenPDF As Boolean = False)
        Dim Rec As Integer
        objShell = CreateObject("WScript.Shell")
        firstJob = True
        Rec = 1
        Dim dRow As DataRow
        Dim progRowCount As Integer = dSet.Tables(0).Rows.Count
        Dim progRowIndex As Integer = 0
        If Not progress Is Nothing Then
            progress.Value = 0
        End If
        For Each dRow In dSet.Tables(0).Rows
            Application.DoEvents()
            If frmMerge1.blnCancelProcess = True Then
                lblStatus.Text &= " - Cancelled process"
                frmMerge1.blnCancelProcess = False
                Exit Sub
            End If
            Dim timeStart As DateTime = DateTime.Now.ToUniversalTime
            Try
                Dim tmpMSG_BODY As String = InjectFieldNameValues(MSG_BODY & "", dRow)
                Dim tmpMSG_SUBJECT As String = InjectFieldNameValues(MSG_SUBJECT & "", dRow)
                Dim tmpMSG_TO As String = InjectFieldNameValues(MSG_TO & "", dRow)
                Dim tmpMSG_CC As String = InjectFieldNameValues(MSG_CC & "", dRow)
                Dim tmpMSG_BCC As String = InjectFieldNameValues(MSG_BCC & "", dRow)
                Dim tmpMSG_ATTACHMENT_FILENAME As String = InjectFieldNameValues(MSG_ATTACHMENT_FILENAME & "", dRow)
                Dim tmpMSG_FromEmail As String = InjectFieldNameValues(MSG_FROM_Email & "", dRow)
                Dim tmpMSG_FromName As String = InjectFieldNameValues(MSG_FROM_Name & "", dRow)
                Dim bAns As Boolean = False
                Dim xFDFApp As New FDFApp.FDFApp_Class
                Dim xFDFDoc As New FDFApp.FDFDoc_Class
                xFDFDoc = xFDFApp.PDFOpenFromBuf(frmMerge1.SessionBytes, True, True, frmMerge1.pdfOwnerPassword & "")
                If Not FieldMappings Is Nothing Then
                    For Each item As String In FieldMappings.Items
                        If item.ToString.Split("="c).Length = 2 Then
                            Dim PDFField As String = item.ToString.Split("="c)(0).ToString
                            Dim DBField As String = item.ToString.Split("="c)(1).ToString
                            Dim strConcate As String = ""
                            xFDFDoc.FDFSetValue(PDFField & "", InjectFieldNameValues(DBField, dRow) & "", True, True)
                        End If
                    Next
                Else
                    xFDFDoc.FDFSetValuesFromDataRow(dRow, Nothing)
                End If
                Dim pdfStream As MemoryStream
                xFDFDoc.FDFSetFile(Pdfpath)
                Select Case MSG_ATTACHMENT_FORMAT.ToString.ToLower.Trim("."c)
                    Case "pdf"
                        pdfStream = New MemoryStream(xFDFDoc.PDFMergeFDF2Buf(Pdfpath, FlattenPDF, pdfownerPw))
                    Case "fdf"
                        pdfStream = New MemoryStream(xFDFDoc.FDFSavetoBuf(FDFApp.FDFDoc_Class.FDFType.FDF))
                    Case "xfdf"
                        pdfStream = New MemoryStream(xFDFDoc.FDFSavetoBuf(FDFApp.FDFDoc_Class.FDFType.xFDF))
                    Case "xdp"
                        pdfStream = New MemoryStream(xFDFDoc.FDFSavetoBuf(FDFApp.FDFDoc_Class.FDFType.XDP))
                    Case "xml"
                        pdfStream = New MemoryStream(xFDFDoc.FDFSavetoBuf(FDFApp.FDFDoc_Class.FDFType.XML))
                    Case Else
                        pdfStream = New MemoryStream(xFDFDoc.PDFMergeFDF2Buf(Pdfpath, FlattenPDF, pdfownerPw))
                End Select
                If tmpMSG_ATTACHMENT_FILENAME.Contains("."c) Then
                    tmpMSG_ATTACHMENT_FILENAME = tmpMSG_ATTACHMENT_FILENAME.Replace(Path.GetExtension("." & tmpMSG_ATTACHMENT_FILENAME).ToString.Trim("."c), "." & MSG_ATTACHMENT_FORMAT.ToString.ToLower.Trim("."c))
                Else
                    tmpMSG_ATTACHMENT_FILENAME = tmpMSG_ATTACHMENT_FILENAME & "." & MSG_ATTACHMENT_FORMAT.ToString.ToLower.Trim("."c)
                End If
                Do While tmpMSG_ATTACHMENT_FILENAME.Contains(".." & MSG_ATTACHMENT_FORMAT.Replace(".", ""))
                    tmpMSG_ATTACHMENT_FILENAME = tmpMSG_ATTACHMENT_FILENAME.Replace(".." & MSG_ATTACHMENT_FORMAT.Replace(".", ""), "." & MSG_ATTACHMENT_FORMAT.Replace(".", ""))
                Loop
                Dim MailMessage1 As New System.Net.Mail.MailMessage()
                If Not String.IsNullOrEmpty(tmpMSG_FromName & "") Then
                    MailMessage1.From = New System.Net.Mail.MailAddress(tmpMSG_FromEmail & "", tmpMSG_FromName & "")
                Else
                    MailMessage1.From = New System.Net.Mail.MailAddress(tmpMSG_FromEmail & "")
                End If
                MailMessage1.Body = tmpMSG_BODY & ""
                MailMessage1.Subject = tmpMSG_SUBJECT & ""
                If Not String.IsNullOrEmpty(tmpMSG_TO & "") Then
                    MailMessage1.To.Add(tmpMSG_TO & "")
                End If
                If Not String.IsNullOrEmpty(tmpMSG_CC & "") Then
                    MailMessage1.CC.Add(tmpMSG_CC & "")
                End If
                If Not String.IsNullOrEmpty(tmpMSG_BCC & "") Then
                    MailMessage1.Bcc.Add(tmpMSG_BCC & "")
                End If
                If pdfStream.CanSeek Then
                    pdfStream.Position = 0
                End If
                Dim MSG_Attachment As New System.Net.Mail.Attachment(pdfStream, tmpMSG_ATTACHMENT_FILENAME)
                MailMessage1.Attachments.Add(MSG_Attachment)
                Try
                    SMTPClient1.Send(MailMessage1)
                Catch ex As Exception
                    Err.Clear()
                Finally
                    MailMessage1.Dispose()
                    MailMessage1 = Nothing
                End Try
            Catch ex As Exception
                Err.Clear()
            End Try
            If Not progress Is Nothing Then
                progRowIndex = Rec
                progress.Value = CInt((progRowIndex / progRowCount) * 100)
                If Not lblStatus Is Nothing Then
                    lblStatus.Text = "Status: " & progress.Value & "%" & " completed"
                End If
            End If
            Rec = Rec + 1
            Application.DoEvents()
            Dim totalMilliseconds As Integer = CInt(1000 - DateTime.Now.ToUniversalTime.Subtract(timeStart).TotalMilliseconds)
            If totalMilliseconds > 0 Then
                Threading.Thread.Sleep(totalMilliseconds)
            End If
        Next
        lblStatus.Text = "Status: 100% completed"
    End Sub
    Public Function ReplaceFieldString(ByRef stringToFill As String, ByVal cFDFDoc As FDFApp.FDFDoc_Class) As String
        If cFDFDoc.XDPGetFields.Length > 0 Then
            For Each _fld As FDFApp.FDFDoc_Class.FDFField In cFDFDoc.XDPGetFields()
                If Not String.IsNullOrEmpty(_fld.FieldName & "") Then
                    stringToFill = stringToFill.ToString.Replace("<" & _fld.FieldName.ToString & ">", cFDFDoc.XDPGetValue(_fld.FieldName)) & ""
                    stringToFill = stringToFill.ToString.Replace("[" & _fld.FieldName.ToString & "]", cFDFDoc.XDPGetValue(_fld.FieldName)) & ""
                    stringToFill = stringToFill.ToString.Replace("(" & _fld.FieldName.ToString & ")", cFDFDoc.XDPGetValue(_fld.FieldName)) & ""
                    stringToFill = stringToFill.ToString.Replace("{" & _fld.FieldName.ToString & "}", cFDFDoc.XDPGetValue(_fld.FieldName)) & ""
                End If
            Next
        End If
        Return stringToFill
    End Function
    Public Function Format_Filename(ByVal SourceString As String) As String
        Dim rgPattern As String = "[\\\/:\*\?""<>|]"
        Dim objNotNaturalPattern As New System.Text.RegularExpressions.Regex(rgPattern)
        Dim strOut As String = objNotNaturalPattern.Replace(SourceString, "")
        Return strOut & ""
    End Function
    Sub WaitForPrintJob(ByVal printerName As String, ByVal printFileName As String)
        Dim submitTime As DateTime, startTime As DateTime, secs As Long, pName As String
        Dim objPrinter, colPrintJobs, objPrintJob
        Dim printJobSpooling, printJobStarted, jobFileName
        On Error Resume Next
        printJobStarted = False
        printJobSpooling = False
        submitTime = Now()
        startTime = Now()
        pName = Replace(printerName, "\", "/")
        If InStr(pName, "/") = 0 Then
            pName = "//" & ComputerName() & "/" & pName
        End If
        objPrinter = GetObject("WinNT:" & pName)
        colPrintJobs = objPrinter.PrintJobs
        printFileName = UCase(printFileName)
        Do
            printJobSpooling = False
            For Each objPrintJob In colPrintJobs
                jobFileName = UCase(objPrintJob.Description)
                If (InStr(printFileName, jobFileName) > 0) Or (InStr(jobFileName, printFileName) > 0) Then
                    secs = DateDiff("s", submitTime, objPrintJob.TimeSubmitted)
                    If (secs < 30) And (secs > 1) Then
                        If (objPrintJob.Status = 0) Then
                            printJobStarted = True
                            printJobSpooling = False
                        ElseIf (objPrintJob.Status And ADS_JOB_SPOOLING) Then
                            printJobStarted = True
                            printJobSpooling = True
                            startTime = Now()
                        ElseIf (objPrintJob.Status And ADS_JOB_PRINTING) Then
                            printJobStarted = True
                        Else
                            printJobSpooling = False
                        End If
                    End If
                Else
                    printJobSpooling = False
                End If
            Next
            If printJobStarted And (Not printJobSpooling) Then
                Exit Do
            End If
            If DateDiff("s", startTime, Now()) > TIMEOUT Then
                Exit Do
            End If
            System.Windows.Forms.Application.DoEvents()
        Loop
    End Sub
    Sub PrintPDF(ByVal xfdfFileName, ByVal pdfFileName, ByVal printerName)
        Dim printcmd, printJob, pdfKey, arDefaultPrinter
        pdfKey = objShell.RegRead("HKCR\.pdf\")
        If printerName = "" Then

            printerName = objShell.RegRead("HKCU\Software\Microsoft\Windows NT\CurrentVersion\Windows\Device")
            arDefaultPrinter = Split(printerName, ",")
            If IsArray(arDefaultPrinter) Then
                printerName = arDefaultPrinter(0)
            End If
        End If
        printcmd = objShell.RegRead("HKCR\" & pdfKey & "\shell\printto\command\")
        printJob = Replace(printcmd, "%1", xfdfFileName)
        printJob = Replace(printJob, "%2", printerName)
        printJob = Replace(printJob, "%3", "")
        printJob = Replace(printJob, "%4", "")
        If firstJob Then

            readerProcess = objShell.Exec(printJob)
            firstJob = False
        Else
            objShell.Exec(printJob)
        End If
        WaitForPrintJob(printerName, pdfFileName)
    End Sub
End Module
