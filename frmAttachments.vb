Imports System
Imports System.Diagnostics
Imports System.Threading
Public Class frmAttachments
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 Nicholas Kowalewicz All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>
    Public frmMain1 As frmMain
    Public attachmentSelectedName As String = ""
    Public lstAtt As New List(Of frmMain.FileContent)
    Dim lstAttDataGrid As New List(Of frmMain.FileContentDataGrid)
    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub
    Private Sub frmAttachments_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not Me.ParentForm Is Nothing Then
            frmMain1 = DirectCast(Me.ParentForm, frmMain)
        ElseIf Not Me.Owner Is Nothing Then
            frmMain1 = DirectCast(Me.Owner, frmMain)
        End If
        If Not frmMain1 Is Nothing Then
            lstAtt = frmMain1.GetAttachments(frmMain1.pdfReaderDoc.Clone)
            If lstAtt.Count > 0 Then
                DataGridView1.Enabled = True
            Else
                DataGridView1.Enabled = False
            End If
            lstAttDataGrid.Clear()
            For Each fContent As frmMain.FileContent In lstAtt.ToArray
                Dim fContentDataGrid As New frmMain.FileContentDataGrid()
                fContentDataGrid.Name = fContent.Name
                fContentDataGrid.Length = fContent.Content.Length
                fContentDataGrid.Modified = fContent.Modified
                fContentDataGrid.Created = fContent.Created
                lstAttDataGrid.Add(fContentDataGrid)
            Next
            DataGridView1.DataSource = lstAttDataGrid.ToList
            If DataGridView1.Rows.Count > 0 Then
                DataGridView1.Rows(0).Selected = False
            End If
            If Not String.IsNullOrEmpty(attachmentSelectedName & "") And lstAtt.Count > 0 Then
                For i As Integer = 0 To DataGridView1.Rows.Count - 1
                    If DataGridView1.Rows(i).Cells(0).Value.ToString() = attachmentSelectedName Then
                        DataGridView1.Rows(i).Selected = True
                        Exit For
                    End If
                Next
            End If
        End If
    End Sub
    Public Sub New()
        InitializeComponent()
    End Sub
    Public Sub New(ByRef frmMain2 As frmMain, Optional selectedName As String = "")
        InitializeComponent()
        frmMain1 = frmMain2
        attachmentSelectedName = selectedName
    End Sub
    Private Sub btnAttachmentsAdd_Click(sender As Object, e As EventArgs) Handles btnAttachmentsAdd.Click
        Try
            Dim o As New OpenFileDialog
            o.CheckFileExists = True
            o.Filter = "All Files|*.*"
            o.FilterIndex = 0
            o.InitialDirectory = System.IO.Path.GetDirectoryName(frmMain1.fpath)
            o.Multiselect = False
            o.Title = "Select file"
            Select Case o.ShowDialog(Me)
                Case DialogResult.Yes, DialogResult.OK
                    Dim PDFD As New iTextSharp.text.Document(iTextSharp.text.PageSize.LETTER)
                    Dim m As New System.IO.MemoryStream()
                    Dim r As iTextSharp.text.pdf.PdfReader = frmMain1.pdfReaderDoc.Clone
                    Dim s As iTextSharp.text.pdf.PdfStamper = frmMain1.getStamper(r, m)
                    Dim w As iTextSharp.text.pdf.PdfWriter = s.Writer
                    Dim pfs As iTextSharp.text.pdf.PdfFileSpecification = iTextSharp.text.pdf.PdfFileSpecification.FileEmbedded(w, o.FileName, System.IO.Path.GetFileName(o.FileName), System.IO.File.ReadAllBytes(o.FileName))
                    Try
                        Dim params As New iTextSharp.text.pdf.PdfDictionary()
                        Dim ct As DateTime = System.IO.File.GetCreationTime(o.FileName)
                        Dim ctString As String = New iTextSharp.text.pdf.PdfDate(ct).ToUnicodeString()
                        params.Put(iTextSharp.text.pdf.PdfName.MODDATE, New iTextSharp.text.pdf.PdfString(New iTextSharp.text.pdf.PdfDate(System.IO.File.GetLastWriteTime(o.FileName)).ToUnicodeString()))
                        params.Put(iTextSharp.text.pdf.PdfName.SIZE, New iTextSharp.text.pdf.PdfString(New System.IO.FileInfo(o.FileName).Length))
                        params.Put(iTextSharp.text.pdf.PdfName.CREATIONDATE, New iTextSharp.text.pdf.PdfString(ctString))
                        pfs = iTextSharp.text.pdf.PdfFileSpecification.FileEmbedded(w, o.FileName, System.IO.Path.GetFileName(o.FileName), System.IO.File.ReadAllBytes(o.FileName), False, "application/octet-stream", params)
                        pfs.AddDescription(o.FileName, True)
                        w.AddFileAttachment(pfs)
                    Catch exParams As Exception
                        Err.Clear()
                    End Try
                    s.Writer.CloseStream = False
                    s.Close()
                    frmMain1.Session = m.ToArray
                    frmMain1.A0_LoadPDF(True, True, True, frmMain1.btnPage.SelectedIndex + 1, True)
                    If Not frmMain1 Is Nothing Then
                        attachmentSelectedName = System.IO.Path.GetFileName(o.FileName)
                        lstAtt = frmMain1.GetAttachments(frmMain1.pdfReaderDoc.Clone)
                        If lstAtt.Count > 0 Then
                            DataGridView1.Enabled = True
                        Else
                            DataGridView1.Enabled = False
                        End If
                        lstAttDataGrid.Clear()
                        For Each fContent As frmMain.FileContent In lstAtt.ToArray
                            Dim fContentDataGrid As New frmMain.FileContentDataGrid()
                            fContentDataGrid.Name = fContent.Name
                            fContentDataGrid.Length = fContent.Content.Length
                            fContentDataGrid.Modified = fContent.Modified
                            fContentDataGrid.Created = fContent.Created
                            lstAttDataGrid.Add(fContentDataGrid)
                        Next
                        DataGridView1.DataSource = lstAttDataGrid.ToList
                        If DataGridView1.Rows.Count > 0 Then
                            DataGridView1.Rows(0).Selected = False
                        End If
                        If Not String.IsNullOrEmpty(attachmentSelectedName & "") And lstAtt.Count > 0 Then
                            For i As Integer = 0 To DataGridView1.Rows.Count - 1
                                If DataGridView1.Rows(i).Cells(0).Value.ToString() = attachmentSelectedName Then
                                    DataGridView1.Rows(i).Selected = True
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                Case Else
                    If Not frmMain1 Is Nothing Then
                        lstAtt = frmMain1.GetAttachments(frmMain1.pdfReaderDoc.Clone)
                        If lstAtt.Count > 0 Then
                            DataGridView1.Enabled = True
                        Else
                            DataGridView1.Enabled = False
                        End If
                        lstAttDataGrid.Clear()
                        For Each fContent As frmMain.FileContent In lstAtt.ToArray
                            Dim fContentDataGrid As New frmMain.FileContentDataGrid()
                            fContentDataGrid.Name = fContent.Name
                            fContentDataGrid.Length = fContent.Content.Length
                            fContentDataGrid.Modified = fContent.Modified
                            fContentDataGrid.Created = fContent.Created
                            lstAttDataGrid.Add(fContentDataGrid)
                        Next
                        DataGridView1.DataSource = lstAttDataGrid.ToList
                    End If
                    Return
            End Select
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public Sub AddAttachment(ByVal FileName As String)
        Try
            Dim o As New OpenFileDialog
            Dim PDFD As New iTextSharp.text.Document(iTextSharp.text.PageSize.LETTER)
            Dim m As New System.IO.MemoryStream()
            Dim r As iTextSharp.text.pdf.PdfReader = frmMain1.pdfReaderDoc.Clone
            Dim s As iTextSharp.text.pdf.PdfStamper = frmMain1.getStamper(r, m)
            Dim w As iTextSharp.text.pdf.PdfWriter = s.Writer
            Dim pfs As iTextSharp.text.pdf.PdfFileSpecification = iTextSharp.text.pdf.PdfFileSpecification.FileEmbedded(w, FileName, System.IO.Path.GetFileName(FileName), System.IO.File.ReadAllBytes(FileName))
            Try
                Dim params As New iTextSharp.text.pdf.PdfDictionary()
                Dim ct As DateTime = System.IO.File.GetCreationTime(FileName)
                Dim ctString As String = New iTextSharp.text.pdf.PdfDate(ct).ToUnicodeString()
                params.Put(iTextSharp.text.pdf.PdfName.MODDATE, New iTextSharp.text.pdf.PdfString(New iTextSharp.text.pdf.PdfDate(System.IO.File.GetLastWriteTime(FileName)).ToUnicodeString()))
                params.Put(iTextSharp.text.pdf.PdfName.SIZE, New iTextSharp.text.pdf.PdfString(New System.IO.FileInfo(FileName).Length))
                params.Put(iTextSharp.text.pdf.PdfName.CREATIONDATE, New iTextSharp.text.pdf.PdfString(ctString))
                pfs = iTextSharp.text.pdf.PdfFileSpecification.FileEmbedded(w, FileName, System.IO.Path.GetFileName(FileName), System.IO.File.ReadAllBytes(FileName), False, "application/octet-stream", params)
                pfs.AddDescription(FileName, True)
                w.AddFileAttachment(pfs)
            Catch exParams As Exception
                Err.Clear()
            End Try
            s.Writer.CloseStream = False
            s.Close()
            frmMain1.Session = m.ToArray
            frmMain1.A0_LoadPDF(True, True, True, frmMain1.btnPage.SelectedIndex + 1, True)
            If Not frmMain1 Is Nothing Then
                attachmentSelectedName = System.IO.Path.GetFileName(FileName)
                lstAtt = frmMain1.GetAttachments(frmMain1.pdfReaderDoc.Clone)
                If lstAtt.Count > 0 Then
                    DataGridView1.Enabled = True
                Else
                    DataGridView1.Enabled = False
                End If
                lstAttDataGrid.Clear()
                For Each fContent As frmMain.FileContent In lstAtt.ToArray
                    Dim fContentDataGrid As New frmMain.FileContentDataGrid()
                    fContentDataGrid.Name = fContent.Name
                    fContentDataGrid.Length = fContent.Content.Length
                    fContentDataGrid.Modified = fContent.Modified
                    fContentDataGrid.Created = fContent.Created
                    lstAttDataGrid.Add(fContentDataGrid)
                Next
                DataGridView1.DataSource = lstAttDataGrid.ToList
                If DataGridView1.Rows.Count > 0 Then
                    DataGridView1.Rows(0).Selected = False
                End If
                If Not String.IsNullOrEmpty(attachmentSelectedName & "") And lstAtt.Count > 0 Then
                    For i As Integer = 0 To DataGridView1.Rows.Count - 1
                        If DataGridView1.Rows(i).Cells(0).Value.ToString() = attachmentSelectedName Then
                            DataGridView1.Rows(i).Selected = True
                            Exit For
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub btnAttachmentsRemove_Click(sender As Object, e As EventArgs) Handles btnAttachmentsRemove.Click
        Try
            If Not DataGridView1.SelectedRows Is Nothing Then
                If DataGridView1.SelectedRows(0).Index >= 0 And DataGridView1.Enabled Then
                    Dim selIndex As Integer = DataGridView1.SelectedRows(0).Index
                    frmMain1.pdfReaderDoc = frmMain1.RemoveAttachment(frmMain1.pdfReaderDoc.Clone, lstAtt(DataGridView1.SelectedRows(0).Index).Name.ToString(), DataGridView1.SelectedRows(0).Index).Clone
                    frmMain1.Session = frmMain1.getPDFBytes(frmMain1.pdfReaderDoc, True)
                    frmMain1.A0_LoadPDF(True, True, True, frmMain1.btnPage.SelectedIndex + 1, True)
                    If Not frmMain1 Is Nothing Then
                        lstAtt = frmMain1.GetAttachments(frmMain1.pdfReaderDoc.Clone)
                        lstAttDataGrid.Clear()
                        For Each fContent As frmMain.FileContent In lstAtt.ToArray
                            Dim fContentDataGrid As New frmMain.FileContentDataGrid()
                            fContentDataGrid.Name = fContent.Name
                            fContentDataGrid.Length = fContent.Content.Length
                            fContentDataGrid.Modified = fContent.Modified
                            fContentDataGrid.Created = fContent.Created
                            lstAttDataGrid.Add(fContentDataGrid)
                        Next
                        DataGridView1.DataSource = lstAttDataGrid.ToList
                        attachmentSelectedName = ""
                        If DataGridView1.Rows.Count > 0 Then
                            DataGridView1.Rows(0).Selected = False
                        End If
                        If selIndex < DataGridView1.Rows.Count Then
                            DataGridView1.Rows(selIndex).Selected = True
                        ElseIf selIndex >= DataGridView1.Rows.Count Then
                            DataGridView1.Rows(DataGridView1.Rows.Count - 1).Selected = True
                        ElseIf DataGridView1.Rows.Count >= 0 Then
                            DataGridView1.Rows(0).Selected = True
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub btnOpenFile_Click(sender As Object, e As EventArgs) Handles btnOpenFile.Click
        Try
            If Not DataGridView1.SelectedRows Is Nothing Then
                If DataGridView1.SelectedRows(0).Index >= 0 And DataGridView1.Enabled Then
                    If Not frmMain1 Is Nothing Then
                        Dim strFp As String = frmMain1.appPathTemp.ToString.TrimEnd("\"c) & "\attachment-" & lstAtt(DataGridView1.SelectedRows(0).Index).Name
                        System.IO.File.WriteAllBytes(strFp, lstAtt(DataGridView1.SelectedRows(0).Index).Content)
                        If ProcessClass.OpenAsDoc(strFp) Then
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
    End Sub
    Private Sub DataGridView1_CellMouseUp(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseUp
        Try
            If e.RowIndex >= 0 Then
                DataGridView1.Rows(e.RowIndex).Selected = True
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public WithEvents myProcess As New Process
    Public elapsedTime As Integer
    Public eventHandled As Boolean
    Public Event Exited As EventHandler
    Private Sub myProcess_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles myProcess.Exited
        eventHandled = True
    End Sub
    Private Sub btnEditFileWait_Click(sender As Object, e As EventArgs) Handles btnEditFileWait.Click
        Try
            If Not DataGridView1.SelectedRows Is Nothing Then
                If DataGridView1.SelectedRows(0).Index >= 0 And DataGridView1.Enabled Then
                    If Not frmMain1 Is Nothing Then
                        If Not System.IO.Directory.Exists(frmMain1.appPathTemp & "attachment") Then
                            System.IO.Directory.CreateDirectory(frmMain1.appPathTemp & "attachment")
                        End If
                        Dim fileName As String = frmMain1.appPathTemp & "attachment\" & lstAtt(DataGridView1.SelectedRows(0).Index).Name
                        System.IO.File.WriteAllBytes(fileName, lstAtt(DataGridView1.SelectedRows(0).Index).Content)
                        Try
                            If ProcessClass.OpenDoc(fileName, "Open") = True Then
                                If ProcessClass.eventHandled Then
                                    If ProcessClass.exitCode <> -1 Then
                                        If Not frmMain1.bytesMatch(lstAtt(DataGridView1.SelectedRows(0).Index).Content, System.IO.File.ReadAllBytes(fileName)) Then
                                            Select Case MsgBox("Save changes to attachment?", MsgBoxStyle.YesNoCancel + MsgBoxStyle.Question + MsgBoxStyle.ApplicationModal, "Save Changes:")
                                                Case MsgBoxResult.Yes
                                                    frmMain1.pdfReaderDoc = frmMain1.RemoveAttachment(frmMain1.pdfReaderDoc.Clone, lstAtt(DataGridView1.SelectedRows(0).Index).Name, DataGridView1.SelectedRows(0).Index)
                                                    AddAttachment(fileName)
                                                Case Else
                                                    Return
                                            End Select
                                        End If
                                    End If
                                End If
                            End If
                        Catch ex As Exception
                            Console.WriteLine("An error occurred trying to print ""{0}"":" &
                                vbCrLf & ex.Message, fileName)
                            Return
                        End Try
                        If System.IO.File.Exists(fileName) Then
                            System.IO.File.Delete(fileName)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public Class ProcessClass
        Private Shared WithEvents myProcess As New Process
        Private Shared elapsedTime As Integer = 0
        Public Shared eventHandled As Boolean = False
        Public Shared exitCode As Integer = -1
        Private Shared Event Exited As EventHandler
        ''' <summary>
        ''' Open Doc
        ''' </summary>
        ''' <param name="fileName">Path to File</param>
        ''' <param name="verb">Open,Print,Edit?</param>
        ''' <returns></returns>
        Public Shared Function OpenDoc(ByVal fileName As String, Optional verb As String = "Open") As Boolean
            Try
                elapsedTime = 0
                eventHandled = False
                Try
                    myProcess = New Process
                    myProcess.StartInfo.FileName = fileName
                    myProcess.StartInfo.Verb = verb
                    myProcess.StartInfo.CreateNoWindow = False
                    myProcess.EnableRaisingEvents = True
                    myProcess.Start()
                    myProcess.WaitForExit()
                Catch ex As Exception
                    Console.WriteLine("An error occurred trying to print ""{0}"":" &
                        vbCrLf & ex.Message, fileName)
                    Return False
                End Try
                Const SLEEP_AMOUNT As Integer = 250
                Do While Not eventHandled
                    elapsedTime += SLEEP_AMOUNT
                    If elapsedTime > 30000 Then
                        Exit Do
                    End If
                    Thread.Sleep(SLEEP_AMOUNT)
                Loop
                Return True
            Catch exMain As Exception
                Err.Clear()
            End Try
            Return False
        End Function
        Public Shared Function OpenAsDoc(ByVal fileName As String, Optional verb As String = "Open") As Boolean
            Try
                elapsedTime = 0
                eventHandled = False
                Try
                    myProcess = New Process
                    myProcess.StartInfo.FileName = "RUNDLL32.EXE"
                    myProcess.StartInfo.CreateNoWindow = False
                    myProcess.EnableRaisingEvents = True
                    myProcess.StartInfo.Arguments = "shell32.dll,OpenAs_RunDLL " & fileName
                    myProcess.Start()
                    myProcess.WaitForExit()
                Catch ex As Exception
                    Console.WriteLine("An error occurred trying to print ""{0}"":" &
                        vbCrLf & ex.Message, fileName)
                    Return False
                End Try
                Const SLEEP_AMOUNT As Integer = 250
                Do While Not eventHandled
                    elapsedTime += SLEEP_AMOUNT
                    If elapsedTime > 30000 Then
                        Exit Do
                    End If
                    Thread.Sleep(SLEEP_AMOUNT)
                Loop
                Return True
            Catch exMain As Exception
                Err.Clear()
            End Try
            Return False
        End Function
        Private Shared Sub myProcess_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles myProcess.Exited
            eventHandled = True
            exitCode = myProcess.ExitCode
            Console.WriteLine("Exit time:    {0}" & vbCrLf &
                "Exit code:    {1}" & vbCrLf & "Elapsed time: {2}",
                myProcess.ExitTime, myProcess.ExitCode, elapsedTime)
        End Sub
    End Class
End Class