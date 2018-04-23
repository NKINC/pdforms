Imports System.Windows.Forms
Imports System.Net
Imports System.IO
Imports Microsoft.VisualBasic
Public Class dialogFTP
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 Nicholas Kowalewicz All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>
    Public _frm As frmMain = Nothing
    Public _fileBytes() As Byte = Nothing
    Public _fileName As String = ""
    Public Property frm() As frmMain
        Get
            If _frm Is Nothing Then
                If Me.Owner.GetType Is GetType(frmMain) Then
                    _frm = DirectCast(Me.Owner, frmMain)
                End If
            End If
            Return _frm
        End Get
        Set(ByVal value As frmMain)
            _frm = value
        End Set
    End Property
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        frm.Show()
        frm.BringToFront()
        Me.Close()
    End Sub
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        frm.Show()
        frm.BringToFront()
        Me.Close()
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadFTP.Click
        Try
            ListDirectoryDetails()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public cancelProgress As Boolean = False
    Public Function UploadFile(ByVal fname As String, ByVal fbytes() As Byte, Optional ByVal overwriteFile As Boolean = False, Optional ByVal refreshList As Boolean = True) As Boolean
        Try
            If String.IsNullOrEmpty(CStr(fname & "").Trim()) Then Return False
            If Not fbytes Is Nothing Then
                If fbytes.Length <= 0 Then Return False
            Else
                Return False
            End If
            If fname.Contains("/"c) Then
                Return False
            End If
            btnFileUpload.Text = "Cancel Upload"
            cancelProgress = False
            _fileName = CStr(fname & "").Trim() & ""
            If overwriteFile = False Then
                For r As Integer = 0 To DataGridView1.RowCount - 1
                    If DataGridView1.Rows(r).Cells(1).Value.ToString.ToLower = fname.ToString.ToLower Then
                        Select Case MsgBox("Overwrite: " & fname & "", MsgBoxStyle.YesNoCancel Or MsgBoxStyle.Question Or MsgBoxStyle.ApplicationModal, "Overwrite?")
                            Case MsgBoxResult.Yes, MsgBoxResult.Ok
                                Exit For
                            Case Else
                                Return False
                        End Select
                    End If
                Next
            End If
            Dim ftpUploadPath As String = getFTPPath().ToString.TrimEnd("/"c) & "/"c & _fileName.ToString
            Dim request As FtpWebRequest = DirectCast(WebRequest.Create(ftpUploadPath & ""), FtpWebRequest)
            request.Method = WebRequestMethods.Ftp.UploadFile
            request.Credentials = New NetworkCredential(txtFTPUsername.Text & "", txtFTPPassword.Text & "")
            Dim fileContents As Byte() = fbytes
            request.ContentLength = fileContents.Length
            Dim requestStream As Stream = request.GetRequestStream()
            Dim bytesRemaining As Long = fileContents.Length, curByte As Long = 0, readByteLength As Long = 2047
            ProgressBar1.Value = 0
            ProgressBar1.Visible = True
            Dim strComplete As String = "Complete"
            Do While bytesRemaining > 0
                Application.DoEvents()
                If cancelProgress Then
                    strComplete = "CANCELLED"
                    Exit Do
                End If
                If bytesRemaining - readByteLength < 0 Then
                    readByteLength = bytesRemaining
                End If
                requestStream.Write(fileContents, CInt(curByte), CInt(readByteLength))
                curByte += readByteLength
                bytesRemaining -= readByteLength
                ProgressBar1.Value = CInt((curByte / fileContents.Length) * 100)
                Application.DoEvents()
                If cancelProgress Then
                    strComplete = "CANCELLED"
                    Exit Do
                End If
            Loop
GOTO_CLOSE:
            requestStream.Close()
            Dim response As FtpWebResponse = DirectCast(request.GetResponse(), FtpWebResponse)
            Console.WriteLine("Upload File " & strComplete & ", status {0}" & Environment.NewLine & "Uploaded File Name:{1}", response.StatusDescription, _fileName.ToString)
            TextBox2.Text = String.Format("Upload File " & strComplete & ", status {0}" & Environment.NewLine & "Uploaded File Name:{1}", response.StatusDescription, _fileName.ToString) & Environment.NewLine & TextBox2.Text
            response.Close()
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        Finally
            ProgressBar1.Visible = False
            btnFileUpload.Text = "Upload File"
            cancelProgress = False
        End Try
        If refreshList Then
            ListDirectoryDetails()
        End If
        Return True
    End Function
    Public Function FTP_FileGetByteLength(ByVal fname As String) As Long
        Try
            If String.IsNullOrEmpty(CStr(fname & "").Trim()) Then Return -1
            If fname.Contains("/"c) Then
                Return -1
            End If
            fname = CStr(fname & "").Trim() & ""
            Dim ftpUploadPath As String = getFTPPath().ToString.TrimEnd("/"c) & "/"c & fname.ToString
            Dim request As FtpWebRequest = DirectCast(WebRequest.Create(ftpUploadPath & ""), FtpWebRequest)
            request.Method = WebRequestMethods.Ftp.GetFileSize
            request.Credentials = New NetworkCredential(txtFTPUsername.Text & "", txtFTPPassword.Text & "")
            Dim response As FtpWebResponse = DirectCast(request.GetResponse(), FtpWebResponse)
            Dim fileSize As Long = response.ContentLength
            Return fileSize
            response.Close()
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        End Try
        Return -1
    End Function
    Public Sub RenameFile(ByVal old_fname As String, ByVal new_fname As String)
        Try
            If String.IsNullOrEmpty(CStr(old_fname & "").Trim()) Or String.IsNullOrEmpty(CStr(new_fname & "").Trim()) Then Return
            If new_fname.Contains("/"c) Then
                Return
            End If
            _fileName = CStr(old_fname & "").Trim() & ""
            Dim ftpUploadPath As String = getFTPPath().ToString.TrimEnd("/"c) & "/"c & _fileName.ToString
            Dim request As FtpWebRequest = DirectCast(WebRequest.Create(ftpUploadPath & ""), FtpWebRequest)
            request.Method = WebRequestMethods.Ftp.Rename
            request.Credentials = New NetworkCredential(txtFTPUsername.Text & "", txtFTPPassword.Text & "")
            request.RenameTo = new_fname
            Dim response As FtpWebResponse = DirectCast(request.GetResponse(), FtpWebResponse)
            Console.WriteLine("Renamed File Complete, status {0}" & Environment.NewLine & "Old File Name: {1}, New File Name:{2}", response.StatusDescription, old_fname.ToString, new_fname.ToString)
            TextBox2.Text = String.Format("Renamed File Complete, status {0}" & Environment.NewLine & "Old File Name: {1}, New File Name:{2}", response.StatusDescription, old_fname.ToString, new_fname.ToString) & Environment.NewLine & TextBox2.Text
            response.Close()
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        End Try
        ListDirectoryDetails()
    End Sub
    Public Sub DeleteFile(ByVal old_fname As String, Optional ByVal confirmDelete As Boolean = True, Optional ByVal refreshDirectoryList As Boolean = True)
        Try
            If cancelProgress = True Then
                Return
            End If
            If String.IsNullOrEmpty(CStr(old_fname & "").Trim()) Then Return
            If confirmDelete Then
                Select Case MsgBox("Confirm delete: " & old_fname, MsgBoxStyle.Question Or MsgBoxStyle.YesNoCancel Or MsgBoxStyle.ApplicationModal, "Confirm Delete:")
                    Case MsgBoxResult.Yes
                        Exit Select
                    Case Else
                        Return
                End Select
            End If
            _fileName = CStr(old_fname & "").Trim() & ""
            Dim ftpUploadPath As String = _fileName & ""
            If Not old_fname.ToLower.StartsWith(CStr(getFTPPath().ToString.TrimEnd("/"c) & "/").ToLower) Then
                ftpUploadPath = CStr(getFTPPath().ToString.TrimEnd("/"c) & "/"c & _fileName.ToString)
            End If
            If cancelProgress = True Then
                Return
            End If
            Dim request As FtpWebRequest = DirectCast(WebRequest.Create(ftpUploadPath & ""), FtpWebRequest)
            request.Method = WebRequestMethods.Ftp.DeleteFile
            request.Credentials = New NetworkCredential(txtFTPUsername.Text & "", txtFTPPassword.Text & "")
            Dim response As FtpWebResponse = DirectCast(request.GetResponse(), FtpWebResponse)
            Console.WriteLine("Deleted File Complete, status {0}" & Environment.NewLine & "Old File Name: {1}", response.StatusDescription, old_fname.ToString)
            TextBox2.Text = String.Format("Deleted File Complete, status {0}" & Environment.NewLine & "Old File Name: {1}", response.StatusDescription, old_fname.ToString) & Environment.NewLine & TextBox2.Text
            response.Close()
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        End Try
        If refreshDirectoryList Then
            ListDirectoryDetails()
        End If
    End Sub
    Public Sub RemoveDirectory(ByVal old_directory As String, Optional ByVal confirmRemove As Boolean = True, Optional ByVal refreshDirectoryList As Boolean = True)
        If cancelProgress = True Then
            Return
        End If
        Try
            If String.IsNullOrEmpty(CStr(old_directory & "").Trim()) Then Return
            If confirmRemove Then
                Select Case MsgBox("Confirm delete: " & old_directory, MsgBoxStyle.Question Or MsgBoxStyle.YesNoCancel Or MsgBoxStyle.ApplicationModal, "Confirm Delete:")
                    Case MsgBoxResult.Yes
                        Exit Select
                    Case Else
                        Return
                End Select
            End If
            _fileName = CStr(old_directory & "").Trim() & ""
            Dim ftpUploadPath As String = old_directory.ToString.TrimStart("/"c).TrimEnd("/"c) & "/"
            If Not old_directory.ToLower.StartsWith(CStr(getFTPPath().ToString.TrimEnd("/"c) & "/").ToLower) Then
                ftpUploadPath = getFTPPath().ToString.TrimEnd("/"c) & "/"c & old_directory.ToString.TrimStart("/"c).TrimEnd("/"c) & "/"
            End If
            If cancelProgress = True Then
                Return
            End If
            Dim request As FtpWebRequest = DirectCast(WebRequest.Create(ftpUploadPath & ""), FtpWebRequest)
            request.Method = WebRequestMethods.Ftp.RemoveDirectory
            request.Credentials = New NetworkCredential(txtFTPUsername.Text & "", txtFTPPassword.Text & "")
            Dim response As FtpWebResponse = DirectCast(request.GetResponse(), FtpWebResponse)
            Console.WriteLine("Removed Directory Complete, status {0}" & Environment.NewLine & "Old Directory Name: {1}", response.StatusDescription, old_directory.ToString)
            TextBox2.Text = String.Format("Removed Directory Complete, status {0}" & Environment.NewLine & "Old Directory Name: {1}", response.StatusDescription, old_directory.ToString) & Environment.NewLine & TextBox2.Text
            response.Close()
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        End Try
        If refreshDirectoryList Then
            ListDirectoryDetails()
        End If
    End Sub
    Public Sub RenameFolder(ByVal old_fname As String, ByVal new_fname As String)
        Try
            If String.IsNullOrEmpty(CStr(old_fname & "").Trim()) Or String.IsNullOrEmpty(CStr(new_fname & "").Trim()) Then Return
            If new_fname.Contains("/"c) Then
                Return
            End If
            _fileName = CStr(old_fname & "").Trim() & ""
            Dim ftpUploadPath As String = getFTPPath().ToString.TrimEnd("/"c) & "/"c & _fileName.ToString
            Dim request As FtpWebRequest = DirectCast(WebRequest.Create(ftpUploadPath & ""), FtpWebRequest)
            request.Method = WebRequestMethods.Ftp.Rename
            request.Credentials = New NetworkCredential(txtFTPUsername.Text & "", txtFTPPassword.Text & "")
            request.RenameTo = new_fname
            Dim response As FtpWebResponse = DirectCast(request.GetResponse(), FtpWebResponse)
            Console.WriteLine("Renamed Folder Complete, status {0}" & Environment.NewLine & "Old Folder Name: {1}, New Folder Name:{2}", response.StatusDescription, old_fname.ToString, new_fname.ToString)
            TextBox2.Text = String.Format("Renamed Folder Complete, status {0}" & Environment.NewLine & "Old Folder Name: {1}, New Folder Name:{2}", response.StatusDescription, old_fname.ToString, new_fname.ToString) & Environment.NewLine & TextBox2.Text
            response.Close()
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        End Try
        ListDirectoryDetails()
    End Sub
    Public Sub MakeNewFolder(ByVal new_fname As String)
        Try
            If String.IsNullOrEmpty(CStr(new_fname & "").Trim()) Then Return
            If new_fname.Contains("/"c) Then
                Return
            End If
            For Each item As FileSystemItemVB In fsItems
                If item.IsFolder Then
                    If item.FileName.ToString.ToLower = new_fname.ToString.ToLower Then
                        Exit Sub
                    End If
                End If
            Next
            _fileName = CStr(new_fname & "").Trim() & ""
            Dim ftpUploadPath As String = getFTPPath().ToString.TrimEnd("/"c) & "/"c & _fileName.ToString
            Dim request As FtpWebRequest = DirectCast(WebRequest.Create(ftpUploadPath & ""), FtpWebRequest)
            request.Method = WebRequestMethods.Ftp.MakeDirectory
            request.Credentials = New NetworkCredential(txtFTPUsername.Text & "", txtFTPPassword.Text & "")
            request.RenameTo = new_fname
            Dim response As FtpWebResponse = DirectCast(request.GetResponse(), FtpWebResponse)
            Console.WriteLine("New Folder Complete, status {0}" & Environment.NewLine & "New Folder Name: {1}", response.StatusDescription, new_fname.ToString)
            TextBox2.Text = String.Format("New Folder Complete, status {0}" & Environment.NewLine & "New Folder Name: {1}", response.StatusDescription, new_fname.ToString) & Environment.NewLine & TextBox2.Text
            response.Close()
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        End Try
        ListDirectoryDetails()
    End Sub
    Public Function DownloadFile(ByVal fileName As String, ByVal destinationFilePath As String, Optional ByVal openDirectory As Boolean = False, Optional ByVal openDefaultViewer As Boolean = False, Optional ByVal openPdfEditor As Boolean = False, Optional ByVal overWrite As Boolean = False) As Byte()
        Dim bytesDownload() As Byte = Nothing
        Try
            Dim filePath As String = getFTPPath.ToString.TrimEnd("/"c) & "/"c & fileName
            Dim request As FtpWebRequest = DirectCast(WebRequest.Create(filePath), FtpWebRequest)
            request.KeepAlive = False
            request.UseBinary = True
            request.Method = WebRequestMethods.Ftp.DownloadFile
            request.Credentials = New NetworkCredential(txtFTPUsername.Text & "", txtFTPPassword.Text & "")
            Dim strComplete As String = "Complete"
            cancelProgress = False
            Using FtpResponse As FtpWebResponse = CType(request.GetResponse, FtpWebResponse)
                Using ResponseStream As IO.Stream = FtpResponse.GetResponseStream
                    Using fs As New IO.MemoryStream()
                        Dim bytesLength As Long = FTP_FileGetByteLength(fileName)
                        Dim bytesRemaining As Long = bytesLength, curByte As Long = 0, readByteLength As Long = 2047
                        Dim buffer(CInt(readByteLength)) As Byte
                        Dim read As Integer = 0
                        btnFileDownload.Text = "Cancel Download"
                        DownloadFileToolStripMenuItem.Text = btnFileDownload.Text
                        ProgressBar1.Value = 0
                        ProgressBar1.Visible = True
                        Do
                            Application.DoEvents()
                            If cancelProgress Then
                                strComplete = "CANCELLED"
                                ResponseStream.Close()
                                fs.Flush()
                                GoTo GOTO_CLOSE
                                Exit Do
                            End If
                            Try
                                read = ResponseStream.Read(buffer, 0, buffer.Length)
                                If Not read = 0 Then
                                    fs.Write(buffer, 0, read)
                                End If
                                Console.Write("."c)
                            Catch ex As Exception
                                If frm.debugMode Then Throw ex Else Err.Clear()
                            End Try
                            Try
                                curByte += buffer.Length
                                bytesRemaining -= buffer.Length
                                ProgressBar1.Value = CInt((curByte / bytesLength) * 100)
                            Catch ex As Exception
                                If frm.debugMode Then Throw ex Else Err.Clear()
                            End Try
                            Application.DoEvents()
                            If cancelProgress Then
                                strComplete = "CANCELLED"
                                ResponseStream.Close()
                                fs.Flush()
                                GoTo GOTO_CLOSE
                                Exit Do
                            End If
                        Loop Until read = 0
                        ResponseStream.Close()
                        fs.Flush()
                        bytesDownload = fs.ToArray
                        If File.Exists(destinationFilePath) Then
                            If overWrite Then
                                File.WriteAllBytes(destinationFilePath, bytesDownload)
                                If openDirectory Then
                                    Process.Start(IO.Path.GetDirectoryName(destinationFilePath))
                                End If
                                If openDefaultViewer Then
                                    Process.Start(destinationFilePath)
                                End If
                                If openPdfEditor Then
                                    frm.OpenFile(destinationFilePath, False)
                                End If
                            Else
                                If SaveAs(destinationFilePath, bytesDownload) Then
                                    If openDirectory Then
                                        Process.Start(IO.Path.GetDirectoryName(destinationFilePath))
                                    End If
                                    If openDefaultViewer Then
                                        Process.Start(destinationFilePath)
                                    End If
                                    If openPdfEditor Then
                                        frm.OpenFile(destinationFilePath, False)
                                    End If
                                End If
                            End If
                        Else
                            File.WriteAllBytes(destinationFilePath, bytesDownload)
                            If openDirectory Then
                                Process.Start(IO.Path.GetDirectoryName(destinationFilePath))
                            End If
                            If openDefaultViewer Then
                                Process.Start(destinationFilePath)
                            End If
                            If openPdfEditor Then
                                frm.OpenFile(destinationFilePath, False)
                            End If
                        End If
GOTO_CLOSE:
                        fs.Close()
                        fs.Dispose()
                    End Using
                End Using
                Console.WriteLine("Download " & strComplete & ", status {0}", FtpResponse.StatusDescription)
                TextBox2.Text = String.Format("Download " & strComplete & ", status {0}", FtpResponse.StatusDescription) & Environment.NewLine & TextBox2.Text
                FtpResponse.Close()
            End Using
            Return bytesDownload
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        Finally
            btnFileDownload.Text = "Download File"
            DownloadFileToolStripMenuItem.Text = btnFileDownload.Text
            cancelProgress = False
            ProgressBar1.Visible = False
        End Try
        Return Nothing
    End Function
    Public Function SaveAs(ByVal fpath As String, ByVal fileBytes() As Byte) As Boolean
        Dim SaveAsFileDialog1 As New frmSaveAs()
        Try
            If String.IsNullOrEmpty(fpath & "") Then
                SaveAsFileDialog1.o.InitialDirectory = frm.appPath & ""
            Else
                SaveAsFileDialog1.o.InitialDirectory = System.IO.Path.GetDirectoryName(fpath)
            End If
            Dim defExt As String = ""
            Try
                defExt = Path.GetExtension(fpath).ToString.TrimStart("."c) & ""
            Catch exExt As Exception
                defExt = "pdf"
                If frm.debugMode Then Throw exExt Else Err.Clear()
            End Try
            SaveAsFileDialog1.o.Filter = defExt.ToUpper & "|*." & defExt & "|All files|*.*"
            SaveAsFileDialog1.o.FilterIndex = 1
            If String.IsNullOrEmpty(fpath & "") Then
                SaveAsFileDialog1.o.FileName = ""
                SaveAsFileDialog1.FilePath = ""
                SaveAsFileDialog1.frmSaveAs_TextFilePath.Text = ""
            Else
                SaveAsFileDialog1.o.FileName = System.IO.Path.GetFileName(fpath & "")
                SaveAsFileDialog1.FilePath = fpath
                SaveAsFileDialog1.frmSaveAs_TextFilePath.Text = fpath
            End If
            SaveAsFileDialog1.o.AutoUpgradeEnabled = True
            SaveAsFileDialog1.o.FilterIndex = 0
            SaveAsFileDialog1.o.Title = "Save As"
            SaveAsFileDialog1.o.ValidateNames = True
            SaveAsFileDialog1.o.OverwritePrompt = True
            Try
            Catch ex As Exception
                If frm.debugMode Then Throw ex Else Err.Clear()
            End Try
            Select Case SaveAsFileDialog1.o.ShowDialog(Me)
                Case Windows.Forms.DialogResult.OK, Windows.Forms.DialogResult.Yes
                    If Not String.IsNullOrEmpty(SaveAsFileDialog1.frmSaveAs_TextFilePath.Text) Then
                        Dim fn As String = SaveAsFileDialog1.frmSaveAs_TextFilePath.Text & ""
                        File.WriteAllBytes(fn, fileBytes.ToArray)
                        Return True
                    End If
            End Select
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        End Try
        Return False
    End Function
    Public Function modelPopup_DoEvents(ByRef blnDone As MsgBoxResult) As Boolean
        Do While blnDone = MsgBoxResult.Ignore
            Application.DoEvents()
        Loop
        Return True
    End Function
    Public Function DoUntil_Boolean(ByRef blnValue As Boolean, Optional ByVal blnBreakValue As Boolean = False) As Boolean
        Do Until blnValue = blnBreakValue
            Application.DoEvents()
        Loop
        Return True
    End Function
    Private Function modelPopupFrmSaveAs_DoEvents(ByRef frmSaveAsDialog As frmSaveAs) As Boolean
        Do While frmSaveAsDialog.dialogResult_1 = Windows.Forms.DialogResult.None
            Application.DoEvents()
        Loop
        Return True
    End Function
    Public Function DoEvents_Wait(ByVal WaitTimeMilliseconds As Integer) As Boolean
        Try
            Dim dt As DateTime = DateTime.Now
            Dim dtSave As DateTime = DateTime.Now
            Do While dt <= dtSave.AddMilliseconds(CInt(WaitTimeMilliseconds + 0))
                Application.DoEvents()
                dt = DateTime.Now
            Loop
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        End Try
        Return True
    End Function
    Public Sub ListDirectory()
        Dim request As FtpWebRequest = DirectCast(WebRequest.Create(getFTPPath() & ""), FtpWebRequest)
        request.Method = WebRequestMethods.Ftp.ListDirectory
        request.Credentials = New NetworkCredential(txtFTPUsername.Text & "", txtFTPPassword.Text & "")
        Dim response As FtpWebResponse = DirectCast(request.GetResponse(), FtpWebResponse)
        Dim responseStream As Stream = response.GetResponseStream()
        Dim reader As New StreamReader(responseStream)
        Dim line As String = reader.ReadLine()
        Dim lines As New List(Of String)
        Dim strReader As String = ""
        Do Until line Is Nothing
            Try
                If Not lines.Contains(line) Then
                    lines.Add(line)
                    strReader &= Environment.NewLine & line
                End If
                line = reader.ReadLine()
            Catch exReadLines As Exception
                If frm.debugMode Then Throw exReadLines Else Err.Clear()
            End Try
        Loop
        Console.WriteLine(strReader)
        Console.WriteLine("Directory List Complete, status {0}", response.StatusDescription)
        TextBox2.Text = String.Format("Directory List Complete, status {0}", response.StatusDescription) & Environment.NewLine & TextBox2.Text
        TextBox1.Text &= Environment.NewLine & strReader
        reader.Close()
        response.Close()
    End Sub
    Public Function getFTPPath() As String
        Dim ftpPath As String = ""
        If Not String.IsNullOrEmpty(txtFTPRoot.Text & "") Then
            ftpPath = CStr(txtFTPRoot.Text & "").ToString.TrimEnd("/"c) & "/"
            If Not String.IsNullOrEmpty(txtFTPCurrentFolder.Text & "") Then
                If Not txtFTPCurrentFolder.Text & "" = "/"c And Not String.IsNullOrEmpty(txtFTPCurrentFolder.Text & "") Then
                    ftpPath &= CStr(txtFTPCurrentFolder.Text & "").ToString.TrimStart("/"c).TrimEnd("/"c) & "/"
                End If
            End If
        End If
        Return ftpPath
    End Function
    Public fsItems As New List(Of FileSystemItemVB)
    Public Function getParentFolder(Optional ByVal getFullParentPath As Boolean = True) As String
        Dim ftpPath As String = ""
        ftpPath = txtFTPCurrentFolder.Text & ""
        ftpPath = ftpPath.TrimEnd("/"c)
        If String.IsNullOrEmpty(ftpPath) Then
            ftpPath = "/"
        End If
        Dim i As Integer = ftpPath.LastIndexOf("/"c)
        If i > 0 Then
            ftpPath = ftpPath.Substring(0, i)
        Else
            ftpPath = "/"
        End If
        If String.IsNullOrEmpty(ftpPath) Then
            ftpPath = "/"
        End If
        If Not getFullParentPath Then
            ftpPath = ftpPath.Replace(CStr(txtFTPRoot.Text & "").ToString.TrimEnd("/"c) & "/", "/")
        Else
            If CStr(txtFTPRoot.Text & "").Length > ftpPath.Length Then
                ftpPath = txtFTPRoot.Text & ""
            End If
        End If
        Return ftpPath
    End Function
    Public Sub ListDirectoryDetails()
        Try
            Dim request As FtpWebRequest = DirectCast(WebRequest.Create(getFTPPath() & ""), FtpWebRequest)
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails
            request.Credentials = New NetworkCredential(txtFTPUsername.Text & "", txtFTPPassword.Text & "")
            Dim response As FtpWebResponse = DirectCast(request.GetResponse(), FtpWebResponse)
            Dim fs1 As New List(Of FileSystemItemVB)
            fsItems.Clear()
            fsItems = New List(Of FileSystemItemVB)
            Dim responseStream As Stream = response.GetResponseStream()
            Dim reader As New StreamReader(responseStream)
            Dim strReader As String = ""
            Dim line As String = reader.ReadLine()
            Dim lines As New List(Of String)
            Dim files As New List(Of FileSystemItemVB)
            Dim folders As New List(Of FileSystemItemVB)
            Dim item As FileSystemItemVB
            Do Until line Is Nothing
                Try
                    If Not lines.Contains(line) Then
                        lines.Add(line)
                        Dim data() As String = line.Split(" ".ToArray(), StringSplitOptions.RemoveEmptyEntries)
                        Dim fname As String = ""
                        If data.Length > 0 Then
                            For i As Integer = 0 To data.Length - 1
                                If Not String.IsNullOrEmpty(data(i) & "") Then
                                    If data.Length > 9 Then
                                        If i >= 8 Then
                                            strReader &= data(i) & " "
                                            fname &= data(i) & " "
                                        Else
                                            strReader &= data(i) & ";"
                                        End If
                                    Else
                                        strReader &= data(i) & ";"
                                        fname = data(data.Length - 1)
                                    End If
                                Else
                                    strReader &= data(i) & ";"
                                End If
                            Next
                            strReader = strReader.TrimEnd(","c)
                            strReader = strReader.TrimEnd(";"c)
                            strReader = strReader.TrimEnd(" "c)
                            fname = fname.TrimEnd(","c)
                            fname = fname.TrimEnd(";"c)
                            fname = fname.TrimEnd(" "c)
                        End If
                        item = New FileSystemItemVB(frm, getFTPPath() & "", data(0) & "", CInt(data(1) & ""), data(2) & "", data(3) & "", data(4) & "", data(5) & "", data(6) & "", data(7) & "", fname)
                        If item.IsFolder Then
                            folders.Add(item)
                        Else
                            files.Add(item)
                        End If
                    End If
                    line = reader.ReadLine()
                    If Not line Is Nothing Then
                        strReader &= Environment.NewLine
                    End If
                Catch exReadLines As Exception
                    If frm.debugMode Then Throw exReadLines Else Err.Clear()
                End Try
            Loop
            If Not txtFTPCurrentFolder.Text = "/"c And Not String.IsNullOrEmpty(txtFTPCurrentFolder.Text) Then
                Dim backUp As New FileSystemItemVB(frm, getFTPPath() & "", "drwxrwxrwx", 1, "owner", "group", "0", DateTime.Now.AddDays(1).Month.ToString, DateTime.Now.AddDays(1).Day.ToString, DateTime.Now.AddDays(1).Year.ToString, "<< back")
                fs1.Add(backUp)
            End If
            If folders.Count > 0 Then
                fs1.AddRange(folders.ToArray)
                fsItems.AddRange(folders.ToArray)
            End If
            If files.Count > 0 Then
                fs1.AddRange(files.ToArray)
                fsItems.AddRange(files.ToArray)
            End If
            DataGridView1.AutoGenerateColumns = True
            DataGridView1.DataSource = fs1
            If DataGridView1.Columns(0).CellType Is GetType(DataGridViewImageCell) Then
                Try
                    DataGridView1.AutoSize = False
                    DataGridView1.AllowUserToResizeColumns = True
                    Dim colIcon As DataGridViewImageColumn = DirectCast(DataGridView1.Columns(0), DataGridViewImageColumn)
                    colIcon.Name = "FileIcon"
                    colIcon.HeaderText = ""
                    colIcon.ImageLayout = DataGridViewImageCellLayout.Zoom
                    colIcon.DefaultCellStyle.SelectionBackColor = Color.White
                    colIcon.DefaultCellStyle.Padding = New System.Windows.Forms.Padding(2, 2, 1, 2)
                    colIcon.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    colIcon.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    Try
                        colIcon.Width = 18
                        colIcon.DividerWidth = 0
                    Catch ex2 As Exception
                        Err.Clear()
                    End Try
                    DataGridView1.BorderStyle = BorderStyle.None
                    DataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
                    DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
                Catch ex As Exception
                    If frm.debugMode Then Throw ex Else Err.Clear()
                End Try
            End If
            For c As Integer = 0 To DataGridView1.Columns.Count - 1
                DataGridView1.Columns(c).ReadOnly = True
            Next
            Console.WriteLine(strReader)
            Console.WriteLine("Directory List Complete, status {0}", response.StatusDescription)
            TextBox1.Text &= Environment.NewLine & strReader
            reader.Close()
            response.Close()
        Catch exMain As Exception
            MsgBox(exMain.Message)
            If frm.debugMode Then Throw exMain Else Err.Clear()
        Finally
            GroupBoxFolder1.Visible = False
            GroupBoxFile1.Visible = False
            If Not _fileBytes Is Nothing Then
                If _fileBytes.Length > 0 Then
                    If Not String.IsNullOrEmpty(_fileName & "") Then
                        GroupBoxFile1.Visible = True
                    End If
                End If
            End If
            If GroupBoxFile1.Visible = False Then
                GroupBoxFile1.Visible = True
                txtFolderName.Text = CStr(txtFTPCurrentFolder.Text & "").TrimStart("/"c).TrimEnd("/"c)
                If txtFolderName.Text.ToString.Contains("/"c) Then
                    txtFolderName.Text = txtFolderName.Text.Substring(txtFolderName.Text.ToString.LastIndexOf("/"c), txtFolderName.Text.Length - txtFolderName.Text.ToString.LastIndexOf("/"c))
                    txtFolderName.Text = txtFolderName.Text.TrimStart("/"c).TrimEnd("/"c)
                End If
            End If
            If DataGridView1.RowCount > 1 Then
                DataGridView1.Focus()
                DataGridView1.Select()
            End If
        End Try
    End Sub
    Public Function getSubDirectoriesAndFiles(ByVal dirPath As String, ByVal directoriesOnly As Boolean, ByVal filesOnly As Boolean) As List(Of FileSystemItemVB)
        Dim fsItems2 As New List(Of FileSystemItemVB)
        Try
            Dim request As FtpWebRequest = DirectCast(WebRequest.Create(dirPath & ""), FtpWebRequest)
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails
            request.Credentials = New NetworkCredential(txtFTPUsername.Text & "", txtFTPPassword.Text & "")
            Using response As FtpWebResponse = DirectCast(request.GetResponse(), FtpWebResponse)
                Dim responseStream As Stream = response.GetResponseStream()
                Using reader As New StreamReader(responseStream)
                    Dim strReader As String = ""
                    Dim line As String = reader.ReadLine()
                    Dim lines As New List(Of String)
                    Dim files As New List(Of FileSystemItemVB)
                    Dim folders As New List(Of FileSystemItemVB)
                    Dim item As FileSystemItemVB
                    Do Until line Is Nothing
                        Try
                            If Not lines.Contains(line) Then
                                lines.Add(line)
                                Dim data() As String = line.Split(" ".ToArray(), StringSplitOptions.RemoveEmptyEntries)
                                Dim fname As String = ""
                                If data.Length > 0 Then
                                    For i As Integer = 0 To data.Length - 1
                                        If Not String.IsNullOrEmpty(data(i) & "") Then
                                            If data.Length > 9 Then
                                                If i >= 8 Then
                                                    strReader &= data(i) & " "
                                                    fname &= data(i) & " "
                                                Else
                                                    strReader &= data(i) & ";"
                                                End If
                                            Else
                                                strReader &= data(i) & ";"
                                                fname = data(data.Length - 1)
                                            End If
                                        Else
                                            strReader &= data(i) & ";"
                                        End If
                                    Next
                                    strReader = strReader.TrimEnd(","c)
                                    strReader = strReader.TrimEnd(";"c)
                                    strReader = strReader.TrimEnd(" "c)
                                    fname = fname.TrimEnd(","c)
                                    fname = fname.TrimEnd(";"c)
                                    fname = fname.TrimEnd(" "c)
                                End If
                                item = New FileSystemItemVB(frm, dirPath & "", data(0) & "", CInt(data(1) & ""), data(2) & "", data(3) & "", data(4) & "", data(5) & "", data(6) & "", data(7) & "", fname)
                                If item.IsFolder Then
                                    folders.Add(item)
                                Else
                                    files.Add(item)
                                End If
                            End If
                            line = reader.ReadLine()
                            If Not line Is Nothing Then
                                strReader &= Environment.NewLine
                            End If
                        Catch exReadLines As Exception
                            If frm.debugMode Then Throw exReadLines Else Err.Clear()
                        End Try
                    Loop
                    If directoriesOnly Then
                        fsItems2.AddRange(folders.ToArray)
                    ElseIf filesOnly Then
                        fsItems2.AddRange(files.ToArray)
                    Else
                        fsItems2.AddRange(folders.ToArray)
                        fsItems2.AddRange(files.ToArray)
                    End If
                    reader.Close()
                End Using
                response.Close()
            End Using
            Return fsItems2
        Catch exMain As Exception
            MsgBox(exMain.Message)
            If frm.debugMode Then Throw exMain Else Err.Clear()
        End Try
        Return fsItems2
    End Function
    Private Sub dialogFTP_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not (Me.DialogResult = Windows.Forms.DialogResult.OK Or Me.DialogResult = Windows.Forms.DialogResult.Cancel) Then
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        End If
        frm.BringToFront()
    End Sub
    Public Sub dialogFTP_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            LoadDialog(True)
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public Sub LoadDialog(ByVal blnShow As Boolean)
        Try
            ShowUploadControls = False
            If Not frm Is Nothing Then
                If Not frm.Session Is Nothing Then
                    If frm.Session.Length > 0 Then
                        _fileBytes = frm.Session
                        _fileName = Path.GetFileName(frm.fpath & "")
                        lblUpload_FileName.Text = "File Name: " & Environment.NewLine & "Size: " & frm.getMegaBytesText(_fileBytes.Length) & " "
                        txtFileName.Text = _fileName
                        If frm.dlgValues.Count = 6 Then
                            If frm.dlgValues(0).ToString.ToLower = _fileName.ToString.ToLower Then
                                txtFolderName.Text = frm.dlgValues(1)
                                txtFTPCurrentFolder.Text = frm.dlgValues(2)
                                txtFTPPassword.Text = frm.dlgValues(3)
                                txtFTPRoot.Text = frm.dlgValues(4)
                                txtFTPUsername.Text = frm.dlgValues(5)
                                ListDirectoryDetails()
                                txtFileName.Text = frm.dlgValues(0)
                                For r As Integer = 0 To DataGridView1.RowCount - 1
                                    If CStr(DataGridView1.Rows(r).Cells(1).Value).ToLower = _fileName.ToString.ToLower Then
                                        DataGridView1.Rows(r).Selected = True
                                        _fileBytes = frm.Session
                                        _fileName = Path.GetFileName(frm.fpath & "")
                                        lblUpload_FileName.Text = "File Name: " & Environment.NewLine & "Size: " & frm.getMegaBytesText(_fileBytes.Length) & " "
                                        ShowUploadControls = True
                                        Return
                                    End If
                                Next
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        Finally
            Me.Visible = blnShow
        End Try
    End Sub
    Public Class FileSystemItemVB
        Private _RootPath As String
        Private _FileType As String
        Private _FileOwner As String
        Private _FileGroup As String
        Private _FileName As String
        Private _FullPath As String
        Private _Size As Long
        Private _CreationTime As New DateTime
        Private _LastAccessTime As New DateTime
        Private _LastWriteTime As New DateTime
        Private _IsFolder As Boolean
        Private _FileIcon As Icon
        Private _frm As frmMain
        Public Property FileIcon() As Icon
            Get
                Return _FileIcon
            End Get
            Set(ByVal value As Icon)
                _FileIcon = value
            End Set
        End Property
        Public Sub New(ByVal file As FileInfo)
            FileName = file.Name
            FullPath = file.FullName
            Size = file.Length
            CreationTime = file.CreationTime
            LastAccessTime = file.LastAccessTime
            LastWriteTime = file.LastWriteTime
            setFolder = False
            FileIcon = getIcon(FileName)
        End Sub
        Public Function getIcon(ByVal fname As String) As System.Drawing.Icon
            If _IsFolder Then
                Dim img As System.Drawing.Image = System.Drawing.Bitmap.FromFile(Application.StartupPath.ToString.TrimEnd("\"c) & "\folder.png")
                Dim bitmap As System.Drawing.Bitmap = New System.Drawing.Bitmap(img, img.Width, img.Height)
                Return Icon.FromHandle(bitmap.GetHicon)
            End If
            Dim fileName As String = Application.StartupPath.ToString.TrimEnd("\"c) & "\temp\tmp_icon." & Path.GetExtension(fname).ToString.TrimStart("."c)
            Dim ico As Icon = Nothing
            If File.Exists(fileName) Then
                ico = Drawing.Icon.ExtractAssociatedIcon(fileName)
            Else
                File.Create(fileName)
                ico = Drawing.Icon.ExtractAssociatedIcon(fileName)
            End If
            Return ico
        End Function
        Public Sub New(ByVal _RootPath As String, ByVal type As String, ByVal numberOfOwners As Integer, ByVal nameOfOwner As String, ByVal group As String, ByVal sizeBytes As String, ByVal monthDt As String, ByVal dayDt As String, ByVal yearDt As String, ByVal name As String)
            FileName = name.ToString.TrimEnd(" "c).TrimEnd(","c).TrimEnd(";"c)
            FullPath = _RootPath.ToString.Trim().TrimEnd("/"c) & "/"c & FileName
            If IsNumeric(sizeBytes & "") Then
                Size = CLng(sizeBytes.Trim())
            Else
                Size = 0
            End If
            CreationTime = DateTime.MinValue
            LastAccessTime = DateTime.MinValue
            Dim m As String = "01"
            Select Case monthDt.ToString.ToLower
                Case "jan".ToLower
                    m = "01"
                Case "feb".ToLower
                    m = "02"
                Case "mar".ToLower
                    m = "03"
                Case "apr".ToLower
                    m = "04"
                Case "may".ToLower
                    m = "05"
                Case "jun".ToLower
                    m = "06"
                Case "jul".ToLower
                    m = "07"
                Case "aug".ToLower
                    m = "08"
                Case "sep".ToLower
                    m = "09"
                Case "oct".ToLower
                    m = "10"
                Case "nov".ToLower
                    m = "11"
                Case "dec".ToLower
                    m = "12"
                Case Else
                    If IsNumeric(monthDt & "") Then
                        If CInt(monthDt) < 12 And CInt(monthDt) >= 1 Then
                            If CInt(monthDt) < 10 Then
                                m = "0" & CInt(monthDt).ToString
                            Else
                                m = CInt(monthDt).ToString
                            End If
                        End If
                    Else
                        If DateTime.Now.Month < 10 Then
                            m = "0" & DateTime.Now.Month.ToString
                        Else
                            m = DateTime.Now.Month.ToString
                        End If
                    End If
            End Select
            Try
                Dim hrStr As String = "0", minStr As String = "00"
                If yearDt.Contains(":"c) Then
                    hrStr = yearDt.Split(":"c)(0)
                    minStr = yearDt.Split(":"c)(1)
                    yearDt = DateTime.Now.Year.ToString
                End If
                LastWriteTime = New DateTime(CInt(yearDt), CInt(m), CInt(dayDt), CInt(hrStr), CInt(minStr), 0).ToUniversalTime
            Catch ex As Exception
                LastWriteTime = DateTime.MaxValue
            End Try
            If type.ToString.ToLower = "-rwxrwxrwx".ToLower Then
                setFolder = False
            ElseIf type.ToString.ToLower = "drwxrwxrwx".ToLower Then
                setFolder = True
            Else
                setFolder = False
            End If
            FileIcon = getIcon(FileName)
        End Sub
        Public Sub New(ByRef frmMain1 As frmMain, ByVal _RootPath As String, ByVal type As String, ByVal numberOfOwners As Integer, ByVal nameOfOwner As String, ByVal group As String, ByVal sizeBytes As String, ByVal monthDt As String, ByVal dayDt As String, ByVal yearDt As String, ByVal name As String)
            _frm = frmMain1
            FileName = name.ToString.TrimEnd(" "c).TrimEnd(","c).TrimEnd(";"c)
            FullPath = _RootPath.ToString.Trim().TrimEnd("/"c) & "/"c & FileName
            If IsNumeric(sizeBytes & "") Then
                Size = CLng(sizeBytes.Trim())
            Else
                Size = 0
            End If
            CreationTime = DateTime.MinValue
            LastAccessTime = DateTime.MinValue
            Dim m As String = "01"
            Select Case monthDt.ToString.ToLower
                Case "jan".ToLower
                    m = "01"
                Case "feb".ToLower
                    m = "02"
                Case "mar".ToLower
                    m = "03"
                Case "apr".ToLower
                    m = "04"
                Case "may".ToLower
                    m = "05"
                Case "jun".ToLower
                    m = "06"
                Case "jul".ToLower
                    m = "07"
                Case "aug".ToLower
                    m = "08"
                Case "sep".ToLower
                    m = "09"
                Case "oct".ToLower
                    m = "10"
                Case "nov".ToLower
                    m = "11"
                Case "dec".ToLower
                    m = "12"
                Case Else
                    If IsNumeric(monthDt & "") Then
                        If CInt(monthDt) < 12 And CInt(monthDt) >= 1 Then
                            If CInt(monthDt) < 10 Then
                                m = "0" & CInt(monthDt).ToString
                            Else
                                m = CInt(monthDt).ToString
                            End If
                        End If
                    Else
                        If DateTime.Now.Month < 10 Then
                            m = "0" & DateTime.Now.Month.ToString
                        Else
                            m = DateTime.Now.Month.ToString
                        End If
                    End If
            End Select
            Try
                Dim hrStr As String = "0", minStr As String = "00"
                If yearDt.Contains(":"c) Then
                    hrStr = yearDt.Split(":"c)(0)
                    minStr = yearDt.Split(":"c)(1)
                    yearDt = DateTime.Now.Year.ToString
                End If
                LastWriteTime = New DateTime(CInt(yearDt), CInt(m), CInt(dayDt), CInt(hrStr), CInt(minStr), 0).ToUniversalTime
            Catch ex As Exception
                LastWriteTime = DateTime.MaxValue
            End Try
            If type.ToString.ToLower = "-rwxrwxrwx".ToLower Then
                setFolder = False
            ElseIf type.ToString.ToLower = "drwxrwxrwx".ToLower Then
                setFolder = True
            Else
                setFolder = False
            End If
            FileIcon = getIcon(FileName)
        End Sub
        Public Sub New(ByVal folder As DirectoryInfo)
            Me.FileName = folder.Name
            Me.FullPath = folder.FullName
            Me.Size = Nothing
            Me.LastWriteTime = folder.LastWriteTime
            Me.setFolder = True
            FileIcon = getIcon(FileName)
        End Sub
        <System.ComponentModel.DisplayName("Owner")>
        Private Property FileOwner() As String
            Get
                Return _FileOwner
            End Get
            Set(ByVal value As String)
                _FileOwner = value
            End Set
        End Property
        <System.ComponentModel.DisplayName("Group")>
        Private Property FileGroup() As String
            Get
                Return _FileGroup
            End Get
            Set(ByVal value As String)
                _FileGroup = value
            End Set
        End Property
        <System.ComponentModel.DisplayName("Name")>
        Public Property FileName() As String
            Get
                Return _FileName
            End Get
            Set(ByVal value As String)
                _FileName = value
            End Set
        End Property
        <System.ComponentModel.DisplayName("Path")>
        Private Property FullPath() As String
            Get
                Return _FullPath
            End Get
            Set(ByVal value As String)
                _FullPath = value
            End Set
        End Property
        <System.ComponentModel.DisplayName("Size")>
        Private Property Size() As Long
            Get
                Return _Size
            End Get
            Set(ByVal value As Long)
                _Size = value
            End Set
        End Property
        <System.ComponentModel.DisplayName("Size")>
        Public ReadOnly Property SizeStr() As String
            Get
                Return _frm.getMegaBytesText(_Size)
            End Get
        End Property
        <System.ComponentModel.DisplayName("Creation")>
        Private Property CreationTime() As DateTime
            Get
                Return _CreationTime
            End Get
            Set(ByVal value As DateTime)
                _CreationTime = value
            End Set
        End Property
        <System.ComponentModel.DisplayName("Accessed")>
        Private Property LastAccessTime() As DateTime
            Get
                Return _LastAccessTime
            End Get
            Set(ByVal value As DateTime)
                _LastAccessTime = value
            End Set
        End Property
        <System.ComponentModel.DisplayName("Last Modified")>
        Public Property LastWriteTime() As DateTime
            Get
                Return _LastWriteTime
            End Get
            Set(ByVal value As DateTime)
                _LastWriteTime = value
            End Set
        End Property
        <System.ComponentModel.DisplayName("Folder")>
        Public Function IsFolder() As Boolean
            Return _IsFolder
        End Function
        Public WriteOnly Property setFolder() As Boolean
            Set(ByVal value As Boolean)
                _IsFolder = value
            End Set
        End Property
        <System.ComponentModel.DisplayName("Type")>
        Public ReadOnly Property FileType() As String
            Get
                If Me.IsFolder Then
                    Return "File folder"
                Else
                    Dim extension As String = Path.GetExtension(Me.FileName)
                    If IsMatch(extension, ".txt") Then
                        Return "Text file"
                    ElseIf IsMatch(extension, ".pdf") Then
                        Return "PDF file"
                    ElseIf IsMatch(extension, ".doc", ".docx") Then
                        Return "Microsoft Word document"
                    ElseIf IsMatch(extension, ".xls", ".xlsx") Then
                        Return "Microsoft Excel document"
                    ElseIf IsMatch(extension, ".jpg", ".jpeg") Then
                        Return "JPEG image file"
                    ElseIf IsMatch(extension, ".gif") Then
                        Return "GIF image file"
                    ElseIf IsMatch(extension, ".png") Then
                        Return "PNG image file"
                    End If
                    If String.IsNullOrEmpty(extension) Then
                        Return "Unknown file type"
                    Else
                        Return extension.Substring(1).ToUpper() & " file"
                    End If
                End If
            End Get
        End Property
        Private Function IsMatch(ByVal extension As String, ByVal ParamArray extensionsToCheck As String()) As Boolean
            For Each str As String In extensionsToCheck
                If String.CompareOrdinal(extension, str) = 0 Then
                    Return True
                End If
            Next
            Return False
        End Function
    End Class
    Private Sub DataGridView1_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGridView1.MouseClick
        Try
            ShowUploadControls = False
            If DataGridView1.RowCount <= 0 Then Return
            If e.Button = Windows.Forms.MouseButtons.Right Then
                Dim rowIndex As Integer = DataGridView1.HitTest(e.Location.X, e.Location.Y).RowIndex + 0
                If rowIndex >= 0 Then
                    DataGridView1.Rows(rowIndex).Selected = True
                    If DataGridView1.SelectedRows.Count > 0 Then
                        If DataGridView1.SelectedRows(0).Cells(4).Value.ToString.ToLower = "file folder" Then
                            Dim selFolder As String = CStr(DataGridView1.SelectedRows(0).Cells(1).Value).ToString.TrimStart("/"c).TrimEnd("/"c)
                            If selFolder.ToString.ToLower = "<< back" Then
                                ShowUploadControls = False
                                OpenFolderToolStripMenuItem.Visible = False
                                DeleteFolderToolStripMenuItem.Visible = False
                                NewFolderToolStripMenuItem.Visible = False
                                RenameFolderToolStripMenuItem.Visible = False
                                OpenFtpURLToolStripMenuItem.Visible = False
                                OpenHttpURLToolStripMenuItem.Visible = False
                                If ContextMenuStripFile1.Visible Then ContextMenuStripFile1.Hide()
                                ContextMenuStripFolder1.Show(DataGridView1, e.Location)
                            Else
                                OpenFolderToolStripMenuItem.Visible = True
                                DeleteFolderToolStripMenuItem.Visible = True
                                NewFolderToolStripMenuItem.Visible = False
                                RenameFolderToolStripMenuItem.Visible = False
                                OpenFtpURLToolStripMenuItem.Visible = False
                                OpenHttpURLToolStripMenuItem.Visible = False
                                If ContextMenuStripFile1.Visible Then ContextMenuStripFile1.Hide()
                                ContextMenuStripFolder1.Show(DataGridView1, e.Location)
                            End If
                        Else
                            Try
                                If Not frm Is Nothing Then
                                    If Not frm.Session Is Nothing Then
                                        If frm.Session.Length > 0 Then
                                            If CStr(DataGridView1.Rows(rowIndex).Cells(1).Value).ToLower = _fileName.ToString.ToLower Then
                                                OpenFtpURLToolStripMenuItem.Visible = True
                                                OpenHttpURLToolStripMenuItem.Visible = True
                                                ShowUploadControls = True
                                                Return
                                            End If
                                            If frm.dlgValues.Count = 6 Then
                                                If frm.dlgValues(0).ToString.ToLower = _fileName.ToString.ToLower Then
                                                    For r As Integer = 0 To DataGridView1.RowCount - 1
                                                        If CStr(DataGridView1.SelectedRows(0).Cells(1).Value) = frm.dlgValues(0) & "" Then
                                                            ShowUploadControls = True
                                                            Return
                                                        End If
                                                    Next
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            Catch ex As Exception
                                If frm.debugMode Then Throw ex Else Err.Clear()
                            Finally
                                If ContextMenuStripFolder1.Visible Then ContextMenuStripFolder1.Hide()
                                ContextMenuStripFile1.Show(DataGridView1, e.Location)
                            End Try
                        End If
                    End If
                End If
            Else
                If DataGridView1.RowCount <= 0 Then Return
                Dim rowIndex As Integer = DataGridView1.HitTest(e.Location.X, e.Location.Y).RowIndex + 0
                If rowIndex >= 0 Then
                    DataGridView1.Rows(rowIndex).Selected = True
                    If DataGridView1.SelectedRows.Count > 0 Then
                        If Not DataGridView1.SelectedRows(0).Cells(4).Value.ToString.ToLower = "file folder" Then
                            If Not _fileBytes Is Nothing Then
                                If _fileBytes.Length > 0 Then
                                    ShowUploadControls = True
                                End If
                            End If
                        End If
                    End If
                Else
                    DataGridView1.ClearSelection()
                    txtFileName.Text = CStr(IIf(frm.fpath = "", "", Path.GetFileName(frm.fpath)))
                    If Not txtFileName.Text = "" And Not frm.Session Is Nothing Then
                        If frm.Session.Length > 0 Then
                            btnFileUpload.Visible = True
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        End Try
    End Sub
    Private Sub DataGridView1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGridView1.MouseDoubleClick
        If DataGridView1.SelectedRows.Count <= 0 Then Return
        Try
            If DataGridView1.SelectedRows(0).Cells(4).Value.ToString.ToLower = "file folder" Then
                Dim strCurFolder As String = txtFTPCurrentFolder.Text & ""
                Dim selFolder As String = CStr(DataGridView1.SelectedRows(0).Cells(1).Value).ToString.TrimStart("/"c).TrimEnd("/"c)
                If selFolder.ToString.ToLower = "<< back" Then
                    txtFTPCurrentFolder.Text = getParentFolder(False)
                    ListDirectoryDetails()
                    Return
                End If
                strCurFolder = strCurFolder.ToString.TrimEnd("/"c) & "/"c & selFolder & "/"
                txtFTPCurrentFolder.Text = strCurFolder & ""
                ListDirectoryDetails()
            Else
                Try
                    If DataGridView1.RowCount <= 0 Then Return
                    If e.Button = Windows.Forms.MouseButtons.Left Then
                        Dim rowIndex As Integer = DataGridView1.HitTest(e.Location.X, e.Location.Y).RowIndex + 0
                        If rowIndex >= 0 Then
                            DataGridView1.Rows(rowIndex).Selected = True
                            If DataGridView1.SelectedRows.Count > 0 Then
                                If DataGridView1.SelectedRows(0).Cells(4).Value.ToString.ToLower = "file folder" Then
                                    DataGridView1_MouseClick(Me, e)
                                    If ContextMenuStripFile1.Visible Then ContextMenuStripFile1.Hide()
                                    ContextMenuStripFolder1.Show(DataGridView1, e.Location)
                                Else
                                    DataGridView1_MouseClick(Me, e)
                                    If ContextMenuStripFolder1.Visible Then ContextMenuStripFolder1.Hide()
                                    ContextMenuStripFile1.Show(DataGridView1, e.Location)
                                End If
                            End If
                        End If
                    End If
                Catch ex As Exception
                    If frm.debugMode Then Throw ex Else Err.Clear()
                End Try
            End If
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        End Try
    End Sub
    Public Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        ShowUploadControls = False
        Try
            If DataGridView1.SelectedRows.Count <= 0 Then
                txtFolderName.Text = ""
                txtFileName.Text = CStr(IIf(frm.fpath = "", "", Path.GetFileName(frm.fpath)))
                If Not txtFileName.Text = "" And Not frm.Session Is Nothing Then
                    If frm.Session.Length > 0 Then
                        btnFileUpload.Visible = True
                    End If
                End If
                GroupBoxFolder1.Visible = True
                btnFolderBrowse.Visible = False
                btnFolderDelete.Visible = False
                btnFolderRename.Visible = False
                btnFolderNew.Visible = True
                If Not frm.Session Is Nothing Then
                    If frm.Session.Length > 0 Then
                        If Not String.IsNullOrEmpty(frm.fpath) Then
                            btnFileGetPdfFromEditor.Visible = True
                        End If
                    End If
                End If
                Return
            End If
            If DataGridView1.SelectedRows(0).Cells(4).Value.ToString.ToLower = "file folder" Then
                GroupBoxFolder1.Visible = True
                GroupBoxFile1.Visible = False
                OpenFolderToolStripMenuItem.Visible = True
                DeleteFolderToolStripMenuItem.Visible = True
                If DataGridView1.SelectedRows(0).Cells(1).Value.ToString.TrimStart("/"c).TrimEnd("/"c) = "<< back".ToLower Then
                    txtFolderName.Text = ""
                    btnFileLoadPdfFileIntoPdfEditor.Visible = False
                    btnFileGetPdfFromEditor.Visible = False
                    ShowUploadControls = False
                    OpenFolderToolStripMenuItem.Visible = False
                    DeleteFolderToolStripMenuItem.Visible = False
                    NewFolderToolStripMenuItem.Visible = False
                    RenameFolderToolStripMenuItem.Visible = False
                    GroupBoxFolder1.Visible = True
                    btnFolderBrowse.Visible = False
                    btnFolderDelete.Visible = False
                    btnFolderRename.Visible = False
                    If Not frm.Session Is Nothing Then
                        If frm.Session.Length > 0 Then
                            If Not String.IsNullOrEmpty(frm.fpath) Then
                                btnFileGetPdfFromEditor.Visible = True
                            End If
                        End If
                    End If
                Else
                    txtFolderName.Text = CStr(DataGridView1.SelectedRows(0).Cells(1).Value)
                    btnFileLoadPdfFileIntoPdfEditor.Visible = False
                    btnFileGetPdfFromEditor.Visible = False
                    ShowUploadControls = True
                    OpenFolderToolStripMenuItem.Visible = True
                    DeleteFolderToolStripMenuItem.Visible = True
                    NewFolderToolStripMenuItem.Visible = False
                    RenameFolderToolStripMenuItem.Visible = False
                    GroupBoxFolder1.Visible = True
                    btnFolderBrowse.Visible = True
                    btnFolderDelete.Visible = True
                    btnFolderRename.Visible = True
                    If Not frm.Session Is Nothing Then
                        If frm.Session.Length > 0 Then
                            If Not String.IsNullOrEmpty(frm.fpath) Then
                                btnFileGetPdfFromEditor.Visible = True
                            End If
                        End If
                    End If
                End If
                Return
            Else
                GroupBoxFolder1.Visible = False
                GroupBoxFile1.Visible = True
                txtFileName.Text = CStr(DataGridView1.SelectedRows(0).Cells(1).Value)
                Try
                    lblUpload_FileName.Text = "File Name: " & Environment.NewLine & "Size: " & CStr(DataGridView1.SelectedRows(0).Cells(2).Value) & ""
                Catch ex As Exception
                    lblUpload_FileName.Text = "File Name: "
                    Err.Clear()
                End Try
                Select Case Path.GetExtension(txtFileName.Text & "").ToString.ToLower.TrimStart("."c)
                    Case "pdf", "fdf", "xfdf", "xdp", "xml"
                        btnFileLoadPdfFileIntoPdfEditor.Visible = True
                        btnFileGetPdfFromEditor.Visible = True
                        OpenWithPDFEditorToolStripMenuItem.Visible = True
                        If Not String.IsNullOrEmpty(txtFileName.Text & "") Then
                            btnFileLoadPdfFileIntoPdfEditor.Visible = True
                        Else
                            btnFileLoadPdfFileIntoPdfEditor.Visible = False
                        End If
                    Case Else
                        btnFileLoadPdfFileIntoPdfEditor.Visible = False
                        btnFileGetPdfFromEditor.Visible = True
                        OpenWithPDFEditorToolStripMenuItem.Visible = False
                        ShowUploadControls = False
                End Select
                If Not _fileBytes Is Nothing Then
                    ShowUploadControls = True
                End If
                If Not frm.Session Is Nothing Then
                    If frm.Session.Length > 0 Then
                        If Not String.IsNullOrEmpty(frm.fpath) Then
                            btnFileGetPdfFromEditor.Visible = True
                        End If
                    End If
                End If
                Return
            End If
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        End Try
        txtFolderName.Text = ""
    End Sub
    Private Sub btnFTPUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFTPUp.Click
        Try
            Dim strCurFolder As String = CStr(txtFTPCurrentFolder.Text & "").TrimEnd("/"c)
            If strCurFolder.Length > 0 Then
                If strCurFolder.LastIndexOf("/"c) > 0 Then
                    Dim selFolder As String = strCurFolder.ToString.Substring(0, strCurFolder.LastIndexOf("/"c))
                    txtFTPCurrentFolder.Text = "/"c & CStr(selFolder & "").TrimEnd("/"c).TrimStart("/"c) & "/"
                Else
                    txtFTPCurrentFolder.Text = "/"
                End If
                ListDirectoryDetails()
            End If
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        End Try
    End Sub
    Public Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileUpload.Click
        If btnFileUpload.Text.ToString.ToLower = "Cancel Upload".ToLower Then
            cancelProgress = True
        Else
            UploadFile(txtFileName.Text & "", _fileBytes)
        End If
    End Sub
    Public Sub New(ByRef frmMain1 As frmMain)
        Try
            InitializeComponent()
            ShowUploadControls = False
            frm = frmMain1
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        End Try
    End Sub
    Public Sub New()
        InitializeComponent()
    End Sub
    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
    End Sub
    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub DataGridView1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGridView1.MouseUp
    End Sub
    Private od As New OpenFileDialog
    Private Sub btnFileOpenDialog1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileOpenDialog1.Click
        Try
            ShowUploadControls = False
            od.Title = "Select a file"
            od.CheckFileExists = True
            od.CheckPathExists = True
            od.FileName = ""
            od.Filter = "All Files|*.*"
            od.FilterIndex = 0
            If od.InitialDirectory = "" Then
                od.InitialDirectory = frm.appPath
            End If
            od.Multiselect = False
            Select Case od.ShowDialog(Me)
                Case Windows.Forms.DialogResult.OK, Windows.Forms.DialogResult.Yes
                    _fileBytes = File.ReadAllBytes(od.FileName)
                    _fileName = Path.GetFileName(od.FileName)
                    txtFileName.Text = _fileName
                    lblUpload_FileName.Text = "File Name: " & Environment.NewLine & "Size: " & frm.getMegaBytesText(_fileBytes.Length) & " "
                    DataGridView1.ClearSelection()
                    ShowUploadControls = True
                Case Else
            End Select
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        End Try
    End Sub
    Public Property ShowUploadControls() As Boolean
        Get
            Return btnFileUpload.Visible
        End Get
        Set(ByVal value As Boolean)
            btnFileUpload.Visible = value
            UploadFileToolStripMenuItem.Visible = value
        End Set
    End Property
    Private Sub btnFileGetPdfFromEditor_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileGetPdfFromEditor.Click
        Try
            Try
                If Not frm Is Nothing Then
                    If Not frm.Session Is Nothing Then
                        If frm.Session.Length > 0 Then
                            _fileBytes = frm.Session
                            _fileName = Path.GetFileName(frm.fpath & "")
                            txtFileName.Text = _fileName
                            lblUpload_FileName.Text = "File Name: " & Environment.NewLine & "Size: " & frm.getMegaBytesText(_fileBytes.Length) & " "
                            For r As Integer = 0 To DataGridView1.RowCount - 1
                                If CStr(DataGridView1.Rows(r).Cells(1).Value).ToLower = _fileName.ToString.ToLower Then
                                    DataGridView1.Rows(r).Selected = True
                                    _fileBytes = frm.Session
                                    _fileName = Path.GetFileName(frm.fpath & "")
                                    lblUpload_FileName.Text = "File Name: " & Environment.NewLine & "Size: " & frm.getMegaBytesText(_fileBytes.Length) & " "
                                    ShowUploadControls = True
                                    Return
                                End If
                            Next
                            If frm.dlgValues.Count = 6 Then
                                If frm.dlgValues(0).ToString.ToLower = _fileName.ToString.ToLower Then
                                    txtFolderName.Text = frm.dlgValues(1)
                                    txtFTPCurrentFolder.Text = frm.dlgValues(2)
                                    txtFTPPassword.Text = frm.dlgValues(3)
                                    txtFTPRoot.Text = frm.dlgValues(4)
                                    txtFTPUsername.Text = frm.dlgValues(5)
                                    ListDirectoryDetails()
                                    txtFileName.Text = frm.dlgValues(0)
                                    For r As Integer = 0 To DataGridView1.RowCount - 1
                                        If CStr(DataGridView1.Rows(r).Cells(1).Value).ToLower = _fileName.ToString.ToLower Then
                                            DataGridView1.Rows(r).Selected = True
                                            DataGridView1_SelectionChanged(Me, New EventArgs())
                                            _fileBytes = frm.Session
                                            _fileName = Path.GetFileName(frm.fpath & "")
                                            lblUpload_FileName.Text = "File Name: " & Environment.NewLine & "Size: " & frm.getMegaBytesText(_fileBytes.Length) & " "
                                            ShowUploadControls = True
                                            Return
                                        End If
                                    Next
                                    ShowUploadControls = True
                                    Return
                                End If
                            End If
                            ShowUploadControls = True
                            Return
                        End If
                    End If
                End If
                DataGridView1.ClearSelection()
            Catch ex As Exception
                If frm.debugMode Then Throw ex Else Err.Clear()
            End Try
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        End Try
    End Sub
    Private Sub btnFileRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileRename.Click
        If DataGridView1.SelectedRows.Count <= 0 Then Return
        RenameFile(CStr(DataGridView1.SelectedRows(0).Cells(1).Value), txtFileName.Text)
    End Sub
    Private Sub btnFolderRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFolderRename.Click
        If DataGridView1.SelectedRows.Count <= 0 Then Return
        RenameFolder(CStr(DataGridView1.SelectedRows(0).Cells(1).Value), txtFolderName.Text)
    End Sub
    Private Sub btnFolderBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFolderBrowse.Click
        If DataGridView1.SelectedRows.Count <= 0 Then Return
        If DataGridView1.SelectedRows(0).Cells(4).Value.ToString.ToLower = "file folder" Then
            Dim strCurFolder As String = txtFTPCurrentFolder.Text & ""
            Dim selFolder As String = CStr(DataGridView1.SelectedRows(0).Cells(1).Value).ToString.TrimStart("/"c).TrimEnd("/"c)
            strCurFolder = strCurFolder.ToString.TrimEnd("/"c) & "/"c & selFolder & "/"
            txtFTPCurrentFolder.Text = strCurFolder & ""
            ListDirectoryDetails()
        End If
    End Sub
    Private Sub GroupBoxFile1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBoxFile1.Enter
    End Sub
    Private sfd As New FolderBrowserDialog
    Private Sub btnFileDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileDownload.Click
        If DataGridView1.SelectedRows.Count <= 0 Then Return
        sfd.ShowNewFolderButton = True
        If sfd.SelectedPath = "" Then
            sfd.SelectedPath = frm.appPath & "temp\"
        End If
        sfd.Description = "Select destination folder:"
        Select Case sfd.ShowDialog(Me)
            Case Windows.Forms.DialogResult.OK, Windows.Forms.DialogResult.Yes
                If Directory.Exists(sfd.SelectedPath & "") Then
                    Dim openDefaultViewer As Boolean = False
                    Dim openPdfEditor As Boolean = False
                    Dim openFolder As Boolean = False
GOTO_DOWNLOAD:
                    DownloadFile(CStr(DataGridView1.SelectedRows(0).Cells(1).Value), sfd.SelectedPath.ToString.TrimEnd("\"c) & "\"c & CStr(DataGridView1.SelectedRows(0).Cells(1).Value), openFolder, openDefaultViewer, openPdfEditor, False)
                    If openPdfEditor Then
                        frm.dlgValues.Clear()
                        frm.dlgValues.Add(txtFileName.Text)
                        frm.dlgValues.Add(txtFolderName.Text)
                        frm.dlgValues.Add(txtFTPCurrentFolder.Text)
                        frm.dlgValues.Add(txtFTPPassword.Text)
                        frm.dlgValues.Add(txtFTPRoot.Text)
                        frm.dlgValues.Add(txtFTPUsername.Text)
                        Me.DialogResult = Windows.Forms.DialogResult.Abort
                        Me.Close()
                    End If
                End If
        End Select
    End Sub
    Private Sub btnFileLoadPdfFileInPdfEditor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileLoadPdfFileIntoPdfEditor.Click
        If DataGridView1.SelectedRows.Count <= 0 Then Return
        DownloadFile(CStr(DataGridView1.SelectedRows(0).Cells(1).Value), frm.appPath & "temp\" & CStr(DataGridView1.SelectedRows(0).Cells(1).Value), False, False, True)
        frm.dlgValues.Clear()
        frm.dlgValues.Add(txtFileName.Text)
        frm.dlgValues.Add(txtFolderName.Text)
        frm.dlgValues.Add(txtFTPCurrentFolder.Text)
        frm.dlgValues.Add(txtFTPPassword.Text)
        frm.dlgValues.Add(txtFTPRoot.Text)
        frm.dlgValues.Add(txtFTPUsername.Text)
        Me.DialogResult = Windows.Forms.DialogResult.Abort
        Me.Close()
    End Sub
    Private Sub DataGridView1_Scroll(ByVal sender As Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles DataGridView1.Scroll
    End Sub
    Private Sub OpenFolderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenFolderToolStripMenuItem.Click
        Try
            btnFolderBrowse_Click(Me, New EventArgs())
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        End Try
    End Sub
    Public Function DeleteFiles(ByVal ftpPath As String) As Boolean
        Try
            If cancelProgress = True Then
                Return False
            End If
            For Each fsItem As FileSystemItemVB In getSubDirectoriesAndFiles(ftpPath, False, True)
                If cancelProgress = True Then
                    Return False
                End If
                Try
                    DeleteFile(ftpPath.ToString.TrimEnd("/"c) & "/"c & fsItem.FileName, False, False)
                Catch exDel As Exception
                    Return False
                    Err.Clear()
                End Try
            Next
        Catch ex As Exception
            Err.Clear()
        End Try
        Return True
    End Function
    Public Function DeleteSubfolders(ByVal ftpPath As String) As Boolean
        For Each fsItem As FileSystemItemVB In getSubDirectoriesAndFiles(ftpPath, False, False)
            If cancelProgress = True Then
                Return False
            End If
            If fsItem.IsFolder Then
                If cancelProgress = True Then
                    Return False
                End If
                Try
                    If Not DeleteFiles(ftpPath.ToString.TrimEnd("/"c) & "/"c & fsItem.FileName & "/") Then
                        Return False
                    End If
                Catch exDelFiles As Exception
                    Return False
                    Err.Clear()
                End Try
                If cancelProgress = True Then
                    Return False
                End If
                Try
                    If Not DeleteSubfolders(ftpPath.ToString.TrimEnd("/"c) & "/"c & fsItem.FileName & "/") Then
                        Return False
                    End If
                Catch exDelSubfolders As Exception
                    Return False
                    Err.Clear()
                End Try
                If cancelProgress = True Then
                    Return False
                End If
                Try
                    RemoveDirectory(ftpPath.ToString.TrimEnd("/"c) & "/"c & fsItem.FileName & "/", False, False)
                Catch exRemoveDirectory As Exception
                    Return False
                    Err.Clear()
                End Try
                If cancelProgress = True Then
                    Return False
                End If
            Else
                If cancelProgress = True Then
                    Return False
                End If
                Try
                    DeleteFile(ftpPath.ToString.TrimEnd("/"c) & "/"c & fsItem.FileName, False, False)
                Catch ex As Exception
                    Return False
                    Err.Clear()
                End Try
            End If
        Next
        Return True
    End Function
    Public Function DeleteFolder(ByVal ftpPath As String) As Boolean
        Try
            If cancelProgress = True Then
                Return False
            End If
            If DeleteSubfolders(ftpPath) Then
                If cancelProgress = True Then
                    Return False
                End If
                RemoveDirectory(ftpPath, False)
                Return True
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
        Return False
    End Function
    Private Sub DeleteFolderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteFolderToolStripMenuItem.Click
        Try
            btnFolderDelete_Click(Me, New EventArgs())
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        End Try
    End Sub
    Private Sub btnFolderDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFolderDelete.Click
        If btnFolderDelete.Text.ToString.ToLower = "Cancel Delete".ToLower Then
            cancelProgress = True
            If frm.DoEvents_Wait(2000) Then
                ListDirectoryDetails()
            End If
            Return
        End If
        If DataGridView1.SelectedRows.Count <= 0 Then Return
        If DataGridView1.SelectedRows(0).Cells(4).Value.ToString.ToLower = "file folder" Then
            Dim strCurFolder As String = txtFTPCurrentFolder.Text & ""
            Dim selFolder As String = CStr(DataGridView1.SelectedRows(0).Cells(1).Value).ToString.TrimStart("/"c).TrimEnd("/"c)
            Select Case MsgBox("Confirm Delete Folder: " & selFolder & Environment.NewLine & "Warning: Contents will be permanently lost.", MsgBoxStyle.Critical Or MsgBoxStyle.YesNoCancel Or MsgBoxStyle.ApplicationModal, "WARNING!")
                Case MsgBoxResult.Yes, MsgBoxResult.Ok
                    btnFolderDelete.Text = "Cancel Delete"
                    cancelProgress = False
                    If DeleteFolder(getFTPPath().ToString.TrimEnd("/"c) & "/"c & selFolder & "/") Then
                        ListDirectoryDetails()
                        TextBox2.Text = "Folder deleted successfully: " & getFTPPath().ToString.TrimEnd("/"c) & "/"c & selFolder & "/"c & Environment.NewLine & TextBox2.Text & ""
                    Else
                        TextBox2.Text = "Folder NOT deleted: " & getFTPPath().ToString.TrimEnd("/"c) & "/"c & selFolder & "/"c & Environment.NewLine & TextBox2.Text & ""
                    End If
                    cancelProgress = False
                Case Else
                    Return
            End Select
            btnFolderDelete.Text = "Delete Folder"
        End If
    End Sub
    Private Sub DownloadFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DownloadFileToolStripMenuItem.Click
        Try
            btnFileDownload_Click(Me, New EventArgs())
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        End Try
    End Sub
    Private Sub DeleteFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteFileToolStripMenuItem.Click
        Try
            btnFileDelete_Click(Me, New EventArgs())
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        End Try
    End Sub
    Private Sub btnFileDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileDelete.Click
        If DataGridView1.SelectedRows.Count <= 0 Then Return
        If Not DataGridView1.SelectedRows(0).Cells(4).Value.ToString.ToLower = "file folder" Then
            Dim strCurFolder As String = txtFTPCurrentFolder.Text & ""
            Dim selFile As String = CStr(DataGridView1.SelectedRows(0).Cells(1).Value).ToString.TrimStart("/"c).TrimEnd("/"c)
            DeleteFile(selFile)
            ListDirectoryDetails()
        End If
    End Sub
    Private Sub btnFileOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileOpen.Click
        If DataGridView1.SelectedRows.Count <= 0 Then Return
        Try
            Dim tmpFolderPath As String = frm.appPath & "temp\"
            DownloadFile(CStr(DataGridView1.SelectedRows(0).Cells(1).Value), tmpFolderPath & CStr(DataGridView1.SelectedRows(0).Cells(1).Value), False, True, False)
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        End Try
    End Sub
    Private Sub OpenFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenFileToolStripMenuItem.Click
        Try
            btnFileOpen_Click(Me, New EventArgs())
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        End Try
    End Sub
    Private Sub UpToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpToolStripMenuItem.Click
        Try
            btnFTPUp_Click(Me, New EventArgs())
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        End Try
    End Sub
    Private Sub BackToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BackToolStripMenuItem.Click
        Try
            btnFTPUp_Click(Me, New EventArgs())
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        End Try
    End Sub
    Private Sub ContextMenuStripFile1_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStripFile1.Opening
    End Sub
    Private Sub OpenWithPDFEditorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenWithPDFEditorToolStripMenuItem.Click
        If DataGridView1.SelectedRows.Count <= 0 Then Return
        DownloadFile(CStr(DataGridView1.SelectedRows(0).Cells(1).Value), frm.appPath & "temp\" & CStr(DataGridView1.SelectedRows(0).Cells(1).Value), False, False, True)
        frm.dlgValues.Clear()
        frm.dlgValues.Add(txtFileName.Text)
        frm.dlgValues.Add(txtFolderName.Text)
        frm.dlgValues.Add(txtFTPCurrentFolder.Text)
        frm.dlgValues.Add(txtFTPPassword.Text)
        frm.dlgValues.Add(txtFTPRoot.Text)
        frm.dlgValues.Add(txtFTPUsername.Text)
        Me.DialogResult = Windows.Forms.DialogResult.Abort
        Me.Close()
    End Sub
    Private Sub OpenWithToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenWithToolStripMenuItem.Click
        Try
            Dim OpenFileDialog4 As New OpenFileDialog
            OpenFileDialog4.CheckFileExists = True
            OpenFileDialog4.CheckPathExists = True
            OpenFileDialog4.DefaultExt = ".exe"
            OpenFileDialog4.Filter = "EXE|*.exe|COM|*.com|All files|*.*"
            OpenFileDialog4.FilterIndex = 0
            OpenFileDialog4.InitialDirectory = Application.StartupPath
            OpenFileDialog4.FileName = ""
            OpenFileDialog4.Multiselect = False
            OpenFileDialog4.Title = "SELECT A PROGRAM"
            Select Case OpenFileDialog4.ShowDialog(Me)
                Case Windows.Forms.DialogResult.OK, Windows.Forms.DialogResult.Yes
                    Dim appSel As String = OpenFileDialog4.FileName.ToString & ""
                    If System.IO.File.Exists(appSel) Then
                        Dim tmpFn As String = frm.appPath & "temp/" & System.IO.Path.GetFileNameWithoutExtension(frm.fpath) & ".pdf"
                        If Not String.IsNullOrEmpty(tmpFn) Then
                            File.WriteAllBytes(tmpFn, DownloadFile(txtFileName.Text, tmpFn, False, False))
                            Process.Start("" & appSel & "", """" & tmpFn & """")
                            Return
                        End If
                    End If
                Case Else
                    Return
            End Select
            Return
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
            Return
        End Try
    End Sub
    Private Sub UploadFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UploadFileToolStripMenuItem.Click
        Try
            btnUpload_Click(Me, New EventArgs())
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
            Return
        End Try
    End Sub
    Private Sub NewFolderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewFolderToolStripMenuItem.Click
        txtFolderName.Text = ""
    End Sub
    Private Sub btnFolderNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFolderNew.Click
        MakeNewFolder(txtFolderName.Text)
    End Sub
    Private Sub NewFolderToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewFolderToolStripMenuItem1.Click
        txtFolderName.Text = ""
    End Sub
    Private Sub OpenHttpURLToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenHttpURLToolStripMenuItem.Click
        Try
            If Not DataGridView1.SelectedRows.Count > 0 Then Return
            Dim strURL As String = getFTPPath().ToString.TrimEnd("/"c)
            Dim strFN As String = CStr(DataGridView1.SelectedRows(0).Cells(1).Value).ToString
            strURL = strURL.Replace("ftp://", "http://")
            strURL &= "/"c & strFN
            Process.Start(strURL)
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub OpenFtpURLToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenFtpURLToolStripMenuItem.Click
        Try
            If Not DataGridView1.SelectedRows.Count > 0 Then Return
            Dim strURL As String = getFTPPath().ToString.TrimEnd("/"c)
            Dim strFN As String = CStr(DataGridView1.SelectedRows(0).Cells(1).Value).ToString
            strURL &= "/"c & strFN
            Process.Start(strURL)
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
End Class
