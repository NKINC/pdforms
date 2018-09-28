Imports System.Windows.Forms
Imports System.Net
Imports System.IO
Imports Microsoft.VisualBasic
Imports iTextSharp.text.pdf
Public Class dialogListOpenWith
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 Nicholas Kowalewicz All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>
    Public _frm As frmMain = Nothing
    Public _fileBytes() As Byte = Nothing
    Public _fileName As String = ""
    Public colSortDirection As System.ComponentModel.ListSortDirection
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
        Me.frm.dlgValues.Clear()
        Me.frm.dlgValues.Add(txtFTPRoot.Text.TrimEnd("\"c) & "\"c & DataGridView1.SelectedRows(0).Cells(2).Value().ToString())
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.frm.dlgValues.Clear()
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
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
            If fname.Contains("\"c) Then
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
            Dim strComplete As String = "Success"
            Dim ftpUploadPath As String = getFTPPath() & _fileName.ToString
            File.WriteAllBytes(ftpUploadPath, _fileBytes)
GOTO_CLOSE:
            Console.WriteLine("Upload File " & strComplete & ", status {0}" & Environment.NewLine & "Uploaded File Name:{1}", "Success", _fileName.ToString)
            TextBox2.Text = String.Format("Upload File " & strComplete & ", status {0}" & Environment.NewLine & "Uploaded File Name:{1}", "Success", _fileName.ToString) & Environment.NewLine & TextBox2.Text
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
            If fname.Contains("\"c) Then
                Return -1
            End If
            fname = CStr(fname & "").Trim() & ""
            Dim ftpUploadPath As String = getFTPPath() & fname.ToString
            Using fs As New FileStream(ftpUploadPath, FileMode.Open)
                Try
                    Return fs.Length
                Catch ex As Exception
                    Err.Clear()
                Finally
                    fs.Close()
                    fs.Dispose()
                End Try
            End Using
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        End Try
        Return -1
    End Function
    Public Sub RenameFile(ByVal old_fname As String, ByVal new_fname As String)
        Try
            If String.IsNullOrEmpty(CStr(old_fname & "").Trim()) Or String.IsNullOrEmpty(CStr(new_fname & "").Trim()) Then Return
            If new_fname.Contains("\"c) Then
                Return
            End If
            _fileName = CStr(old_fname & "").Trim() & ""
            Dim ftpUploadPath As String = getFTPPath() & new_fname & ""
            If File.Exists(ftpUploadPath) Then
                Select Case MsgBox("Overwrite existing file: " & new_fname & "", MsgBoxStyle.YesNoCancel Or MsgBoxStyle.Critical Or MsgBoxStyle.ApplicationModal, "Confirm Rename:")
                    Case MsgBoxResult.Yes, MsgBoxResult.Ok
                        File.WriteAllBytes(ftpUploadPath, File.ReadAllBytes(getFTPPath() & old_fname))
                End Select
            Else
                File.WriteAllBytes(ftpUploadPath, File.ReadAllBytes(getFTPPath() & old_fname))
            End If
            Console.WriteLine("Renamed File Complete, status {0}" & Environment.NewLine & "Old File Name: {1}, New File Name:{2}", "Success", old_fname.ToString, new_fname.ToString)
            TextBox2.Text = String.Format("Renamed File Complete, status {0}" & Environment.NewLine & "Old File Name: {1}, New File Name:{2}", "Success", old_fname.ToString, new_fname.ToString) & Environment.NewLine & TextBox2.Text
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
            If Not old_fname.ToLower.StartsWith(CStr(getFTPPath()).ToLower) Then
                ftpUploadPath = CStr(getFTPPath() & _fileName.ToString)
            End If
            If cancelProgress = True Then
                Return
            End If
            File.Delete(ftpUploadPath)
            Console.WriteLine("Deleted File Complete, status {0}" & Environment.NewLine & "Old File Name: {1}", "Success", old_fname.ToString)
            TextBox2.Text = String.Format("Deleted File Complete, status {0}" & Environment.NewLine & "Old File Name: {1}", "Success", old_fname.ToString) & Environment.NewLine & TextBox2.Text
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
            Dim ftpUploadPath As String = old_directory.ToString.TrimStart("\"c).TrimEnd("\"c) & "\"
            If Not old_directory.ToLower.StartsWith(CStr(getFTPPath()).ToLower) Then
                ftpUploadPath = CStr(getFTPPath()) & old_directory.ToString.TrimStart("\"c).TrimEnd("\"c) & "\"
            End If
            If cancelProgress = True Then
                Return
            End If
            Dim status As String = "Success"
            If Not DeleteFolder(ftpUploadPath) Then
                status = "Failed"
            Else
                status = "Success"
            End If
            Console.WriteLine("Removed Directory Complete, status {0}" & Environment.NewLine & "Old Directory Name: {1}", status, old_directory.ToString)
            TextBox2.Text = String.Format("Removed Directory Complete, status {0}" & Environment.NewLine & "Old Directory Name: {1}", status, old_directory.ToString) & Environment.NewLine & TextBox2.Text
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
            If new_fname.Contains("\"c) Then
                Return
            End If
            _fileName = getFTPPath() & CStr(old_fname & "").Trim() & "\"c
            Dim ftpUploadPath As String = getFTPPath() & new_fname.ToString.TrimEnd("\"c) & "\"c
            If Not String.IsNullOrEmpty(ftpUploadPath) Then
                If Directory.Exists(_fileName) Then
                    If Directory.Exists(ftpUploadPath) Then
                        Select Case MsgBox("Delete existing folder: " & new_fname & "", MsgBoxStyle.YesNoCancel Or MsgBoxStyle.Critical Or MsgBoxStyle.ApplicationModal, "Confirm Rename")
                            Case MsgBoxResult.Yes, MsgBoxResult.Ok
                                Directory.Delete(ftpUploadPath)
                        End Select
                    End If
                    Directory.Move(getFTPPath() & old_fname, getFTPPath() & new_fname)
                End If
            End If
            TextBox2.Text = String.Format("Renamed Folder Complete, status {0}" & Environment.NewLine & "Old Folder Name: {1}, New Folder Name:{2}", "Success", old_fname.ToString, new_fname.ToString) & Environment.NewLine & TextBox2.Text
        Catch ex As Exception
            TextBox2.Text = String.Format("Message: Renaming Folder - Error: {0}" & Environment.NewLine & "Old Folder Name: {1}, New Folder Name:{2}", ex.Message.ToString(), old_fname.ToString, new_fname.ToString) & Environment.NewLine & TextBox2.Text
            If frm.debugMode Then Throw ex Else Err.Clear()
        End Try
        ListDirectoryDetails()
    End Sub
    Public Sub MakeNewFolder(ByVal new_fname As String)
        Try
            If String.IsNullOrEmpty(CStr(new_fname & "").Trim()) Then Return
            If new_fname.Contains("\"c) Then
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
            Dim ftpUploadPath As String = getFTPPath() & _fileName.ToString
            Directory.CreateDirectory(ftpUploadPath)
            Console.WriteLine("New Folder Complete, status {0}" & Environment.NewLine & "New Folder Name: {1}", "Success", new_fname.ToString)
            TextBox2.Text = String.Format("New Folder Complete, status {0}" & Environment.NewLine & "New Folder Name: {1}", "Success", new_fname.ToString) & Environment.NewLine & TextBox2.Text
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        End Try
        ListDirectoryDetails()
    End Sub
    Public Function DownloadFile(ByVal fileName As String, ByVal destinationFilePath As String, Optional ByVal openDirectory As Boolean = False, Optional ByVal openDefaultViewer As Boolean = False, Optional ByVal openPdfEditor As Boolean = False, Optional ByVal overWrite As Boolean = False) As Byte()
        Dim bytesDownload() As Byte = Nothing
        Try
            Dim filePath As String = getFTPPath.ToString.TrimEnd("\"c) & "\"c & fileName
            Dim strComplete As String = "Complete"
            cancelProgress = False
            Using fs As New IO.MemoryStream(File.ReadAllBytes(filePath))
                btnFileDownload.Text = "Cancel Download"
                DownloadFileToolStripMenuItem.Text = btnFileDownload.Text
                Application.DoEvents()
                If cancelProgress Then
                    strComplete = "CANCELLED"
                    fs.Flush()
                    GoTo GOTO_CLOSE
                End If
                Try
                Catch ex As Exception
                    If frm.debugMode Then Throw ex Else Err.Clear()
                End Try
                Application.DoEvents()
                If cancelProgress Then
                    strComplete = "CANCELLED"
                    fs.Flush()
                    GoTo GOTO_CLOSE
                End If
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
            TextBox2.Text = String.Format("Download " & strComplete & ", status {0}", "Success!") & Environment.NewLine & TextBox2.Text
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
                SaveAsFileDialog1.o.InitialDirectory = frm.ApplicationDataFolder(True, "") & "\" & ""
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
    Public Function getFTPPath() As String
        Dim ftpPath As String = ""
        If Not String.IsNullOrEmpty(txtFTPRoot.Text.ToString.Replace(Environment.NewLine, "") & "") Then
            ftpPath = CStr(txtFTPRoot.Text.ToString.Replace(Environment.NewLine, "") & "").ToString.TrimEnd("\"c) & "\"
        Else
            ftpPath = frm.ApplicationDataFolder(True, "") & "\" & ""
            txtFTPRoot.Text = ftpPath
        End If
        Return ftpPath.ToString.TrimEnd("\"c) & "\"
    End Function
    Public fsItems As New SortedBindingList(Of FileSystemItemVB)
    Public Function getParentFolder() As String
        Dim ftpPath As String = ""
        ftpPath = getFTPPath()
        ftpPath = ftpPath.TrimEnd("\"c)
        If String.IsNullOrEmpty(ftpPath) Then
            ftpPath = "\"
        End If
        Dim i As Integer = ftpPath.LastIndexOf("\"c)
        If i > 0 Then
            ftpPath = ftpPath.Substring(0, i)
        Else
            ftpPath = "\"
        End If
        If String.IsNullOrEmpty(ftpPath) Then
            ftpPath = "\"
        End If
        Return ftpPath
    End Function
    Public Class SortedBindingList(Of T)
        Inherits System.ComponentModel.BindingList(Of T)
        Private m_bIsSorted As Boolean = False
        Private m_SortDirection As System.ComponentModel.ListSortDirection
        Private m_SortProperty As System.ComponentModel.PropertyDescriptor
        Protected Overrides ReadOnly Property SupportsSortingCore() As Boolean
            Get
                Return True
            End Get
        End Property
        Protected Overrides ReadOnly Property IsSortedCore() As Boolean
            Get
                Return m_bIsSorted
            End Get
        End Property
        Protected Overrides Sub RemoveSortCore()
            m_bIsSorted = False
        End Sub
        Protected Overrides Sub ApplySortCore(ByVal prop As System.ComponentModel.PropertyDescriptor, ByVal direction As System.ComponentModel.ListSortDirection)
            Dim myitems As List(Of T) = TryCast(Me.Items, List(Of T))
            If myitems IsNot Nothing Then
                m_SortDirection = direction
                m_SortProperty = prop
                Dim pc As PropertyComparer(Of T) =
                  New PropertyComparer(Of T)(prop, direction)
                myitems.Sort(pc)
                m_bIsSorted = True
            Else
                m_bIsSorted = False
            End If
            Me.OnListChanged(New System.ComponentModel.ListChangedEventArgs(System.ComponentModel.ListChangedType.Reset, -1))
        End Sub
        Protected Overrides ReadOnly Property SortPropertyCore() As System.ComponentModel.PropertyDescriptor
            Get
                Return m_SortProperty
            End Get
        End Property
        Protected Overrides ReadOnly Property SortDirectionCore() As System.ComponentModel.ListSortDirection
            Get
                Return m_SortDirection
            End Get
        End Property
    End Class
    Public Class PropertyComparer(Of T)
        Inherits System.Collections.Generic.Comparer(Of T)
        Private _property As System.ComponentModel.PropertyDescriptor
        Private _direction As System.ComponentModel.ListSortDirection
        Public Sub New(ByVal [property] As System.ComponentModel.PropertyDescriptor, ByVal direction As System.ComponentModel.ListSortDirection)
            _property = [property]
            _direction = direction
        End Sub
#Region "IComparer<T>"
        Public Overrides Function Compare(ByVal xWord As T, ByVal yWord As T) As Integer
            Dim xValue As Object = GetPropertyValue(xWord, _property.Name)
            Dim yValue As Object = GetPropertyValue(yWord, _property.Name)
            If _direction = System.ComponentModel.ListSortDirection.Ascending Then
                Return CompareAscending(xValue, yValue)
            Else
                Return CompareDescending(xValue, yValue)
            End If
        End Function
        Public Overloads Function Equals(ByVal xWord As T, ByVal yWord As T) As Boolean
            Return xWord.Equals(yWord)
        End Function
        Public Overloads Function GetHashCode(ByVal obj As T) As Integer
            Return obj.GetHashCode()
        End Function
#End Region
        Private Function CompareAscending(ByVal xValue As Object, ByVal yValue As Object) As Integer
            Dim result As Integer
            If TypeOf xValue Is IComparable Then
                result = (DirectCast(xValue, IComparable)).CompareTo(yValue)
            Else
                If xValue.Equals(yValue) Then
                    result = 0
                Else
                    result = xValue.ToString().CompareTo(yValue.ToString())
                End If
            End If
            Return result
        End Function
        Private Function CompareDescending(ByVal xValue As Object, ByVal yValue As Object) As Integer
            Return CompareAscending(xValue, yValue) * -1
        End Function
        Private Function GetPropertyValue(ByVal value As T, ByVal [property] As String) As Object
            Dim propertyInfo1 As System.Reflection.PropertyInfo = value.[GetType]().GetProperty([property])
            Return propertyInfo1.GetValue(value, Nothing)
        End Function
    End Class
    Public Sub getOpenWithFrequent()
        Try
            If System.IO.File.Exists(DirectCast(Me.Owner, frmMain).ApplicationDataFolder(False, "") & "settings-openwith.txt") Then
                For Each line As String In System.IO.File.ReadAllLines(DirectCast(Me.Owner, frmMain).ApplicationDataFolder(False, "") & "settings-openwith.txt", System.Text.Encoding.UTF8).Reverse()
                    Dim fi As New System.IO.FileInfo(line)
                    Dim item As New FileSystemItemVB(frm, getFTPPath() & "", fi.Extension.ToString() & "", CInt("1"), "owner", "group", fi.Length.ToString & "", fi.LastWriteTime().Month.ToString & "", fi.LastWriteTime().Day.ToString & "", fi.LastWriteTime().Year.ToString & "", Path.GetFileName(line) & "")
                    fsItems.Insert(0, item)
                Next
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public Sub ListDirectoryDetails()
        Try
            fsItems.Clear()
            fsItems = New SortedBindingList(Of FileSystemItemVB)
            Dim strReader As String = ""
            Dim line As String = ""
            Dim lines As New List(Of String)
            Dim files As New List(Of FileSystemItemVB)
            Dim folders As New List(Of FileSystemItemVB)
            Dim item As FileSystemItemVB
            Try
                Dim backUp As New FileSystemItemVB(frm, getFTPPath() & "", "File Folder", 1, "owner", "group", "0", DateTime.Now.AddDays(1).Month.ToString, DateTime.Now.AddDays(1).Day.ToString, DateTime.Now.AddDays(1).Year.ToString, "▲ Up")
                fsItems.Add(backUp)
                For Each line In Directory.GetDirectories(getFTPPath())
                    Try
                        If Not lines.Contains(line) Then
                            lines.Add(line)
                            Dim fi As New DirectoryInfo(line)
                            If fi.Exists Then
                                item = New FileSystemItemVB(frm, getFTPPath() & "", "File Folder", CInt("1"), "owner", "group", "0", fi.LastWriteTime().Month.ToString & "", fi.LastWriteTime().Day.ToString & "", fi.LastWriteTime().Year.ToString & "", GetDirectoryFileName1(line) & "")
                                folders.Add(item)
                                fsItems.Add(item)
                            End If
                            If Not line Is Nothing Then
                                strReader &= Environment.NewLine
                            End If
                        End If
                    Catch exReadLines As Exception
                        If frm.debugMode Then Throw exReadLines Else Err.Clear()
                    End Try
                Next
            Catch ex As Exception
                Err.Clear()
            End Try
            Try
                For Each line In Directory.GetFiles(getFTPPath())
                    Try
                        If Not lines.Contains(line) Then
                            lines.Add(line)
                            Dim fi As New FileInfo(line)
                            If fi.Exists Then
                                Dim ext As String = System.IO.Path.GetExtension(line & "").Trim("."c)
                                If ext = "exe" Or ext = "com" Or ext = "bat" Or ext = "cmd" Then
                                    item = New FileSystemItemVB(frm, getFTPPath() & "", fi.Extension.ToString() & "", CInt("1"), "owner", "group", fi.Length.ToString & "", fi.LastWriteTime().Month.ToString & "", fi.LastWriteTime().Day.ToString & "", fi.LastWriteTime().Year.ToString & "", Path.GetFileName(line) & "")
                                    files.Add(item)
                                    fsItems.Add(item)
                                End If
                            End If
                            If Not line Is Nothing Then
                                strReader &= Environment.NewLine
                            End If
                        End If
                    Catch exReadLines As Exception
                        If frm.debugMode Then Throw exReadLines Else Err.Clear()
                    End Try
                Next
            Catch ex As Exception
                Err.Clear()
            End Try
            If folders.Count > 0 Then
            End If
            If files.Count > 0 Then
            End If
            DataGridView1.AutoGenerateColumns = True
            DataGridView1.DataSource = fsItems
            DataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically
            If DataGridView1.Columns(1).CellType Is GetType(DataGridViewImageCell) Then
                Try
                    DataGridView1.AutoSize = False
                    DataGridView1.AllowUserToResizeColumns = True
                    DataGridView1.AllowUserToOrderColumns = True
                    Dim colIcon As DataGridViewImageColumn = DirectCast(DataGridView1.Columns(1), DataGridViewImageColumn)
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
            Dim chk As New DataGridViewCheckBoxColumn()
            chk.HeaderText = ""
            chk.Name = "chkSelected"
            chk.Width = 24
            chk.ReadOnly = False
            chk.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            If Not DataGridView1.Columns.Contains("chkSelected") Then
                DataGridView1.Columns.Insert(0, chk)
            End If
            DataGridView1.Columns(0).ReadOnly = False
            For c As Integer = 1 To DataGridView1.Columns.Count - 1
                DataGridView1.Columns(c).ReadOnly = True
            Next
            For r As Integer = 0 To DataGridView1.RowCount - 1
                DirectCast(DataGridView1.Rows(r).Cells(0), DataGridViewCheckBoxCell).ReadOnly = False
                If listSelectedPaths.Contains(CStr(getFTPPath() & DirectCast(DataGridView1.Rows(r).Cells("FileName"), DataGridViewCell).Value.ToString).ToString()) Then
                    DirectCast(DataGridView1.Rows(r).Cells(0), DataGridViewCheckBoxCell).Value = True
                Else
                    DirectCast(DataGridView1.Rows(r).Cells(0), DataGridViewCheckBoxCell).Value = False
                End If
            Next
            Console.WriteLine(strReader)
            Console.WriteLine("Directory List Complete, status {0}", "Success")
            TextBox1.Text &= Environment.NewLine & strReader
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
                txtFolderName.Text = ""
                If txtFolderName.Text.ToString.Contains("\"c) Then
                    txtFolderName.Text = txtFolderName.Text.Substring(txtFolderName.Text.ToString.LastIndexOf("\"c), txtFolderName.Text.Length - txtFolderName.Text.ToString.LastIndexOf("\"c))
                    txtFolderName.Text = txtFolderName.Text.TrimStart("\"c).TrimEnd("\"c)
                End If
            End If
            If DataGridView1.RowCount > 1 Then
                DataGridView1.Focus()
                DataGridView1.Select()
            End If
            Dim dcc As Integer = DataGridView1.Columns.Count
            For c As Integer = 6 To dcc - 1
                DataGridView1.Columns.RemoveAt(6)
            Next
            dcc = DataGridView1.Columns.Count
            DataGridView1.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            DataGridView1.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            For c As Integer = 0 To dcc - 1
                DataGridView1.Columns(c).AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            Next
            DataGridView1.Columns(0).Width = 24
            DataGridView1.Columns(1).Width = 36
            DataGridView1.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            DataGridView1.Columns(2).MinimumWidth = 220
            DataGridView1.Columns(3).Width = 75
            DataGridView1.Columns(4).Width = 110
            DataGridView1.Columns(5).Width = 130
            DataGridView1.AutoResizeColumn(2)
        End Try
    End Sub
    Public Sub ListFileDetails(ByVal lst As List(Of String))
        Try
            fsItems.Clear()
            fsItems = New SortedBindingList(Of FileSystemItemVB)
            Dim strReader As String = ""
            Dim line As String = ""
            Dim lines As New List(Of String)
            Dim files As New List(Of FileSystemItemVB)
            Dim folders As New List(Of FileSystemItemVB)
            Dim item As FileSystemItemVB
            Try
                Dim backUp As New FileSystemItemVB(frm, getFTPPath() & "", "File Folder", 1, "owner", "group", "0", DateTime.Now.AddDays(1).Month.ToString, DateTime.Now.AddDays(1).Day.ToString, DateTime.Now.AddDays(1).Year.ToString, "▲ Up")
                fsItems.Add(backUp)
                For Each line In lst.ToArray()
                    If Directory.Exists(line & "") Then
                        Try
                            For Each lineFile As String In Directory.GetFiles(line)
                                Try
                                    If Not lines.Contains(lineFile) Then
                                        lines.Add(lineFile)
                                        Dim fi As New FileInfo(lineFile)
                                        If fi.Exists Then
                                            Dim ext As String = System.IO.Path.GetExtension(line & "").Trim("."c)
                                            If ext = "exe" Or ext = "com" Or ext = "bat" Or ext = "cmd" Then
                                                item = New FileSystemItemVB(frm, Path.GetDirectoryName(lineFile), fi.Extension.ToString() & "", CInt("1"), "owner", "group", fi.Length.ToString & "", fi.LastWriteTime().Month.ToString & "", fi.LastWriteTime().Day.ToString & "", fi.LastWriteTime().Year.ToString & "", Path.GetFileName(lineFile) & "")
                                                files.Add(item)
                                                fsItems.Add(item)
                                            End If
                                        End If
                                        If Not line Is Nothing Then
                                            strReader &= Environment.NewLine
                                        End If
                                    End If
                                Catch exReadLines As Exception
                                    If frm.debugMode Then Throw exReadLines Else Err.Clear()
                                End Try
                            Next
                        Catch ex As Exception
                            Err.Clear()
                        End Try
                    ElseIf File.Exists(line & "") Then
                        Try
                            If Not lines.Contains(line) Then
                                lines.Add(line)
                                Dim fi As New FileInfo(line)
                                If fi.Exists Then
                                    item = New FileSystemItemVB(frm, Path.GetDirectoryName(line), fi.Extension.ToString() & "", CInt("1"), "owner", "group", fi.Length.ToString & "", fi.LastWriteTime().Month.ToString & "", fi.LastWriteTime().Day.ToString & "", fi.LastWriteTime().Year.ToString & "", Path.GetFileName(line) & "")
                                    files.Add(item)
                                    fsItems.Add(item)
                                End If
                                If Not line Is Nothing Then
                                    strReader &= Environment.NewLine
                                End If
                            End If
                        Catch exReadLines As Exception
                            If frm.debugMode Then Throw exReadLines Else Err.Clear()
                        End Try
                    End If
                Next
            Catch exTRy As Exception
            End Try
            If folders.Count > 0 Then
            End If
            If files.Count > 0 Then
            End If
            DataGridView1.AutoGenerateColumns = True
            DataGridView1.DataSource = fsItems
            DataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically
            For c As Integer = 6 To DataGridView1.Columns.Count - 1
                DataGridView1.Columns.RemoveAt(6)
            Next
            If DataGridView1.Columns(1).CellType Is GetType(DataGridViewImageCell) Then
                Try
                    DataGridView1.AutoSize = False
                    DataGridView1.AllowUserToResizeColumns = True
                    DataGridView1.AllowUserToOrderColumns = True
                    Dim colIcon As DataGridViewImageColumn = DirectCast(DataGridView1.Columns(1), DataGridViewImageColumn)
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
            Dim chk As New DataGridViewCheckBoxColumn()
            chk.HeaderText = ""
            chk.Name = "chkSelected"
            chk.Width = 24
            chk.ReadOnly = False
            chk.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            If Not DataGridView1.Columns.Contains("chkSelected") Then
                DataGridView1.Columns.Insert(0, chk)
            End If
            DataGridView1.Columns(0).ReadOnly = False
            For c As Integer = 1 To DataGridView1.Columns.Count - 1
                DataGridView1.Columns(c).ReadOnly = True
            Next
            For r As Integer = 0 To DataGridView1.RowCount - 1
                DirectCast(DataGridView1.Rows(r).Cells(0), DataGridViewCheckBoxCell).ReadOnly = False
                DirectCast(DataGridView1.Rows(r).Cells(0), DataGridViewCheckBoxCell).Value = True
            Next
            Console.WriteLine(strReader)
            Console.WriteLine("Directory List Complete, status {0}", "Success")
            TextBox1.Text &= Environment.NewLine & strReader
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
                txtFolderName.Text = ""
                If txtFolderName.Text.ToString.Contains("\"c) Then
                    txtFolderName.Text = txtFolderName.Text.Substring(txtFolderName.Text.ToString.LastIndexOf("\"c), txtFolderName.Text.Length - txtFolderName.Text.ToString.LastIndexOf("\"c))
                    txtFolderName.Text = txtFolderName.Text.TrimStart("\"c).TrimEnd("\"c)
                End If
            End If
            If DataGridView1.RowCount > 1 Then
            End If
        End Try
    End Sub
    Public Function GetDirectoryFileName1(ByVal strPath As String) As String
        Return CStr(strPath.ToString.TrimEnd("\"c).Substring(strPath.LastIndexOf("\"c), strPath.Length - strPath.LastIndexOf("\"c)) & "").TrimEnd("\"c).TrimStart("\"c)
    End Function
    Public Function getSubDirectoriesAndFiles(ByVal dirPath As String, ByVal directoriesOnly As Boolean, ByVal filesOnly As Boolean) As List(Of FileSystemItemVB)
        Dim fsItems2 As New List(Of FileSystemItemVB)
        Try
            Dim line As String = ""
            Dim lines As New List(Of String)
            Dim files As New List(Of FileSystemItemVB)
            Dim folders As New List(Of FileSystemItemVB)
            Dim item As FileSystemItemVB
            For Each line In Directory.GetDirectories(dirPath)
                Try
                    If Not lines.Contains(line) Then
                        lines.Add(line)
                        Dim fi As New DirectoryInfo(line)
                        If fi.Exists Then
                            item = New FileSystemItemVB(frm, line & "", "File Folder", CInt("1"), "owner", "group", "0", fi.LastWriteTime().Month.ToString & "", fi.LastWriteTime().Day.ToString & "", fi.LastWriteTime().Year.ToString & "", GetDirectoryFileName1(line) & "")
                            folders.Add(item)
                        End If
                        If Not line Is Nothing Then
                        End If
                    End If
                Catch exReadLines As Exception
                    If frm.debugMode Then Throw exReadLines Else Err.Clear()
                End Try
            Next
            For Each line In Directory.GetFiles(dirPath)
                Try
                    If Not lines.Contains(line) Then
                        lines.Add(line)
                        Dim fi As New FileInfo(line)
                        If fi.Exists Then
                            Dim ext As String = System.IO.Path.GetExtension(line & "").Trim("."c)
                            If ext = "exe" Or ext = "com" Or ext = "bat" Or ext = "cmd" Then
                                item = New FileSystemItemVB(frm, Path.GetDirectoryName(line) & "", fi.Extension.ToString() & "", CInt("1"), "owner", "group", fi.Length.ToString & "", fi.LastWriteTime().Month.ToString & "", fi.LastWriteTime().Day.ToString & "", fi.LastWriteTime().Year.ToString & "", Path.GetFileName(line) & "")
                                files.Add(item)
                            End If
                        End If
                        If Not line Is Nothing Then
                        End If
                    End If
                Catch exReadLines As Exception
                    If frm.debugMode Then Throw exReadLines Else Err.Clear()
                End Try
            Next
            If directoriesOnly Then
                fsItems2.AddRange(folders.ToArray)
            ElseIf filesOnly Then
                fsItems2.AddRange(files.ToArray)
            Else
                fsItems2.AddRange(folders.ToArray)
                fsItems2.AddRange(files.ToArray)
            End If
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
            If Me.Owner.GetType Is GetType(frmMain) Then
                frm = DirectCast(Me.Owner, frmMain)
                colSortDirection = Nothing
                fsItems.Clear()
                fsItems = New SortedBindingList(Of FileSystemItemVB)
                getOpenWithFrequent()
                If fsItems.Count > 0 Then
                    Try
                        DataGridView1.AutoGenerateColumns = True
                        DataGridView1.DataSource = fsItems
                        DataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically
                        For c As Integer = 6 To DataGridView1.Columns.Count - 1
                            DataGridView1.Columns.RemoveAt(6)
                        Next
                        If DataGridView1.Columns(1).CellType Is GetType(DataGridViewImageCell) Then
                            Try
                                DataGridView1.AutoSize = False
                                DataGridView1.AllowUserToResizeColumns = True
                                DataGridView1.AllowUserToOrderColumns = True
                                Dim colIcon As DataGridViewImageColumn = DirectCast(DataGridView1.Columns(1), DataGridViewImageColumn)
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
                        Dim chk As New DataGridViewCheckBoxColumn()
                        chk.HeaderText = ""
                        chk.Name = "chkSelected"
                        chk.Width = 24
                        chk.ReadOnly = False
                        chk.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                        If Not DataGridView1.Columns.Contains("chkSelected") Then
                            DataGridView1.Columns.Insert(0, chk)
                        End If
                        DataGridView1.Columns(0).ReadOnly = False
                        For c As Integer = 1 To DataGridView1.Columns.Count - 1
                            DataGridView1.Columns(c).ReadOnly = True
                        Next
                        For r As Integer = 0 To DataGridView1.RowCount - 1
                            DirectCast(DataGridView1.Rows(r).Cells(0), DataGridViewCheckBoxCell).ReadOnly = False
                            DirectCast(DataGridView1.Rows(r).Cells(0), DataGridViewCheckBoxCell).Value = True
                        Next
                        Console.WriteLine("Directory List Complete, status {0}", "Success")
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
                            txtFolderName.Text = ""
                            If txtFolderName.Text.ToString.Contains("\"c) Then
                                txtFolderName.Text = txtFolderName.Text.Substring(txtFolderName.Text.ToString.LastIndexOf("\"c), txtFolderName.Text.Length - txtFolderName.Text.ToString.LastIndexOf("\"c))
                                txtFolderName.Text = txtFolderName.Text.TrimStart("\"c).TrimEnd("\"c)
                            End If
                        End If
                        If DataGridView1.RowCount > 1 Then
                        End If
                    End Try
                Else
                    If txtFTPRoot.Text = "" Then
                        LoadDialog(System.Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "", False)
                        ListDirectoryDetails()
                    End If
                End If
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public Sub LoadDialog(ByVal blnFolderRoot As String, ByVal fname As String, ByVal blnShow As Boolean)
        Try
            ShowUploadControls = False
            If Not frm Is Nothing Then
                If Not frm.Session Is Nothing Then
                    If frm.Session.Length > 0 Then
                        If Not fname = "" Then
                            _fileName = Path.GetFileName(fname & "")
                            _fileBytes = File.ReadAllBytes(blnFolderRoot.ToString.TrimEnd("\"c) & "\" & fname.ToString)
                            lblUpload_FileName.Text = "File Name: " & Environment.NewLine & "Size: " & frm.getMegaBytesText(_fileBytes.Length) & " "
                            txtFileName.Text = _fileName
                        End If
                        If frm.dlgValues.Count = 6 Then
                            If frm.dlgValues(0).ToString.ToLower = _fileName.ToString.ToLower Then
                                txtFolderName.Text = frm.dlgValues(1)
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
                                Return
                            End If
                        End If
                    End If
                End If
                txtFTPRoot.Text = blnFolderRoot
                ListDirectoryDetails()
                If Not fname = "" Then
                    SelectFileName(fname)
                End If
            End If
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        Finally
            Me.Visible = blnShow
        End Try
    End Sub
    Public Sub SelectFileName(ByVal strFN As String)
        For r As Integer = 0 To DataGridView1.RowCount
            If DataGridView1.Rows(r).Cells(2).Value.ToString.ToLower = strFN.ToString.ToLower Then
                DataGridView1.Focus()
                DataGridView1.Rows(r).Cells(2).Selected = True
                DataGridView1.Rows(r).Selected = True
                Exit For
            End If
        Next
    End Sub
    Public Class FileSystemItemVB
        Inherits System.ComponentModel.BindingList(Of FileSystemItemVB)
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
            FullPath = _RootPath.ToString.Trim().TrimEnd("\"c) & "\"c & FileName
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
            If type.ToString.ToLower = "File Folder".ToLower Then
                setFolder = True
            Else
                setFolder = False
            End If
            FileIcon = getIcon(FileName)
        End Sub
        Public Sub New(ByRef frmMain1 As frmMain, ByVal _RootPath As String, ByVal type As String, ByVal numberOfOwners As Integer, ByVal nameOfOwner As String, ByVal group As String, ByVal sizeBytes As String, ByVal monthDt As String, ByVal dayDt As String, ByVal yearDt As String, ByVal name As String)
            _frm = frmMain1
            FileName = name.ToString.TrimEnd(" "c).TrimEnd(","c).TrimEnd(";"c)
            FullPath = _RootPath.ToString.Trim().TrimEnd("\"c) & "\"c & FileName
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
            If type.ToString.ToLower = "File Folder".ToLower Then
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
        <System.ComponentModel.DisplayName("Path")>
        Public Function getFullPath() As String
            Return _FullPath
        End Function
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
    Private direction As System.ComponentModel.ListSortDirection
    Private Sub DataGridView1_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.ColumnHeaderMouseClick
        Try
            Dim newColumn As DataGridViewColumn = DataGridView1.Columns(e.ColumnIndex)
            Dim oldColumn As DataGridViewColumn = DataGridView1.SortedColumn
            If oldColumn IsNot Nothing Then
                If oldColumn.Name.ToString = newColumn.Name.ToString And direction = System.ComponentModel.ListSortDirection.Ascending Then
                    direction = System.ComponentModel.ListSortDirection.Descending
                Else
                    direction = System.ComponentModel.ListSortDirection.Ascending
                End If
            Else
                direction = System.ComponentModel.ListSortDirection.Ascending
            End If
            If newColumn Is Nothing Then
            Else
                DataGridView1.Sort(newColumn, direction)
                If direction = System.ComponentModel.ListSortDirection.Ascending Then
                    newColumn.HeaderCell.SortGlyphDirection = SortOrder.Ascending
                Else
                    newColumn.HeaderCell.SortGlyphDirection = SortOrder.Descending
                End If
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public listSelectedPaths As New List(Of String)
    Private Sub DataGridView1_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGridView1.MouseClick
        Try
            ShowUploadControls = False
            If DataGridView1.RowCount <= 0 Then Return
            If e.Button = Windows.Forms.MouseButtons.Right Then
                Dim rowIndex As Integer = DataGridView1.HitTest(e.Location.X, e.Location.Y).RowIndex + 0
                If rowIndex >= 0 Then
                    DataGridView1.Rows(rowIndex).Selected = True
                    If DataGridView1.SelectedRows.Count > 0 Then
                        If DataGridView1.SelectedRows(0).Cells("FileType").Value.ToString.ToLower = "file folder" Then
                            Dim selFolder As String = CStr(DataGridView1.SelectedRows(0).Cells("FileName").Value).ToString.TrimStart("\"c).TrimEnd("\"c)
                            If selFolder.ToString.ToLower() = "▲ Up".ToLower() Then
                                ShowUploadControls = False
                                OpenFolderToolStripMenuItem.Visible = False
                                DeleteFolderToolStripMenuItem.Visible = False
                                NewFolderToolStripMenuItem.Visible = False
                                RenameFolderToolStripMenuItem.Visible = False
                                OpenHTTPURLToolStripMenuItem.Visible = False
                                OpenFTPURLToolStripMenuItem.Visible = False
                                If ContextMenuStripFile1.Visible Then ContextMenuStripFile1.Hide()
                                ContextMenuStripFolder1.Show(DataGridView1, e.Location)
                            Else
                                OpenFolderToolStripMenuItem.Visible = True
                                DeleteFolderToolStripMenuItem.Visible = True
                                NewFolderToolStripMenuItem.Visible = False
                                RenameFolderToolStripMenuItem.Visible = False
                                OpenHTTPURLToolStripMenuItem.Visible = False
                                OpenFTPURLToolStripMenuItem.Visible = False
                                If ContextMenuStripFile1.Visible Then ContextMenuStripFile1.Hide()
                                ContextMenuStripFolder1.Show(DataGridView1, e.Location)
                            End If
                        Else
                            Try
                                If Not frm Is Nothing Then
                                    If Not frm.Session Is Nothing Then
                                        If frm.Session.Length > 0 Then
                                            If CStr(DataGridView1.Rows(rowIndex).Cells(1).Value).ToLower = Path.GetFileName(_fileName).ToString.ToLower Then
                                                OpenHTTPURLToolStripMenuItem.Visible = True
                                                OpenFTPURLToolStripMenuItem.Visible = True
                                                ShowUploadControls = True
                                                Return
                                            End If
                                            If frm.dlgValues.Count = 6 Then
                                                If frm.dlgValues(0).ToString.ToLower = Path.GetFileName(_fileName).ToString.ToLower Then
                                                    For r As Integer = 0 To DataGridView1.RowCount - 1
                                                        If CStr(DataGridView1.SelectedRows(0).Cells("FileName").Value) = frm.dlgValues(0) & "" Then
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
                                If Path.GetExtension(CStr(DataGridView1.SelectedRows(0).Cells("FileName").Value)).ToString().ToLower().TrimStart("."c) = "vb".ToLower() Then
                                    RemoveVbNetCommentsToolStripMenuItem.Visible = True
                                Else
                                    RemoveVbNetCommentsToolStripMenuItem.Visible = False
                                End If
                                ContextMenuStripFile1.Show(DataGridView1, e.Location)
                            End Try
                        End If
                    End If
                End If
            ElseIf e.Button = Windows.Forms.MouseButtons.Left Then
                Dim hittestInfo1 As Windows.Forms.DataGridView.HitTestInfo = DataGridView1.HitTest(e.Location.X, e.Location.Y)
                If hittestInfo1.RowIndex > 0 Then
                    DataGridView1.Rows(hittestInfo1.RowIndex).Selected = True
                    If hittestInfo1.ColumnIndex = 0 Then
                        If DataGridView1.Rows(hittestInfo1.RowIndex).Cells("chkSelected").GetType Is GetType(DataGridViewCheckBoxCell) Then
                            If CBool(DirectCast(DataGridView1.Rows(hittestInfo1.RowIndex).Cells("chkSelected"), DataGridViewCheckBoxCell).Value) = False Then
                                DirectCast(DataGridView1.Rows(hittestInfo1.RowIndex).Cells("chkSelected"), DataGridViewCheckBoxCell).Value = True
                            Else
                                DirectCast(DataGridView1.Rows(hittestInfo1.RowIndex).Cells("chkSelected"), DataGridViewCheckBoxCell).Value = False
                            End If
                            If Not DataGridView1.Rows(hittestInfo1.RowIndex).Cells("FileName").Value.ToString.ToLower.Contains("▲ Up".ToLower) Then
                                Dim fpth As String = DirectCast(fsItems.Item(hittestInfo1.RowIndex), FileSystemItemVB).getFullPath.ToString()
                                If Directory.Exists(fpth) Then
                                    If CBool(DirectCast(DataGridView1.Rows(hittestInfo1.RowIndex).Cells("chkSelected"), DataGridViewCheckBoxCell).Value) = True Then
                                        If Not listSelectedPaths.Contains(fpth) Then
                                            Try
                                                For Each lineDir As String In Directory.GetFiles(fpth)
                                                    Try
                                                        If Not listSelectedPaths.Contains(lineDir) Then
                                                            listSelectedPaths.Add(lineDir)
                                                        End If
                                                    Catch exReadLines As Exception
                                                        If frm.debugMode Then Throw exReadLines Else Err.Clear()
                                                    End Try
                                                Next
                                            Catch ex As Exception
                                                Err.Clear()
                                            End Try
                                        End If
                                    Else
                                        Try
                                            For Each lineDir As String In Directory.GetFiles(fpth)
                                                Try
                                                    If listSelectedPaths.Contains(lineDir) Then
                                                        listSelectedPaths.Remove(lineDir)
                                                    End If
                                                Catch exReadLines As Exception
                                                    If frm.debugMode Then Throw exReadLines Else Err.Clear()
                                                End Try
                                            Next
                                        Catch ex As Exception
                                            Err.Clear()
                                        End Try
                                    End If
                                ElseIf File.Exists(fpth) Then
                                    If CBool(DirectCast(DataGridView1.Rows(hittestInfo1.RowIndex).Cells("chkSelected"), DataGridViewCheckBoxCell).Value) = True Then
                                        If Not listSelectedPaths.Contains(fpth) Then
                                            listSelectedPaths.Add(fpth)
                                        End If
                                    Else
                                        If listSelectedPaths.Contains(fpth) Then
                                            listSelectedPaths.Remove(fpth)
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                ElseIf hittestInfo1.ColumnIndex = 0 Then
                    DataGridView1.Rows(hittestInfo1.RowIndex).Selected = True
                    If hittestInfo1.ColumnIndex = 0 Then
                        If DataGridView1.Rows(hittestInfo1.RowIndex).Cells("chkSelected").GetType Is GetType(DataGridViewCheckBoxCell) Then
                            If CBool(DirectCast(DataGridView1.Rows(hittestInfo1.RowIndex).Cells("chkSelected"), DataGridViewCheckBoxCell).Value) = False Then
                                DirectCast(DataGridView1.Rows(hittestInfo1.RowIndex).Cells("chkSelected"), DataGridViewCheckBoxCell).Value = True
                            Else
                                DirectCast(DataGridView1.Rows(hittestInfo1.RowIndex).Cells("chkSelected"), DataGridViewCheckBoxCell).Value = False
                            End If
                            For r As Integer = 0 To DataGridView1.RowCount - 1
                                DirectCast(DataGridView1.Rows(r).Cells("chkSelected"), DataGridViewCheckBoxCell).Value = DirectCast(DataGridView1.Rows(hittestInfo1.RowIndex).Cells("chkSelected"), DataGridViewCheckBoxCell).Value
                                If Not DataGridView1.Rows(r).Cells("FileName").Value.ToString.ToLower.Contains("▲ Up".ToLower) Then
                                    Dim fpth As String = DirectCast(fsItems.Item(r), FileSystemItemVB).getFullPath.ToString()
                                    If Directory.Exists(fpth) Then
                                        If CBool(DirectCast(DataGridView1.Rows(r).Cells("chkSelected"), DataGridViewCheckBoxCell).Value) = True Then
                                            If Not listSelectedPaths.Contains(fpth) Then
                                                Try
                                                    For Each lineDir As String In Directory.GetFiles(fpth)
                                                        Try
                                                            If Not listSelectedPaths.Contains(lineDir) Then
                                                                listSelectedPaths.Add(lineDir)
                                                            End If
                                                        Catch exReadLines As Exception
                                                            If frm.debugMode Then Throw exReadLines Else Err.Clear()
                                                        End Try
                                                    Next
                                                Catch ex As Exception
                                                    Err.Clear()
                                                End Try
                                            End If
                                        Else
                                            Try
                                                For Each lineDir As String In Directory.GetFiles(fpth)
                                                    Try
                                                        If listSelectedPaths.Contains(lineDir) Then
                                                            listSelectedPaths.Remove(lineDir)
                                                        End If
                                                    Catch exReadLines As Exception
                                                        If frm.debugMode Then Throw exReadLines Else Err.Clear()
                                                    End Try
                                                Next
                                            Catch ex As Exception
                                                Err.Clear()
                                            End Try
                                        End If
                                    ElseIf File.Exists(fpth) Then
                                        If CBool(DirectCast(DataGridView1.Rows(r).Cells("chkSelected"), DataGridViewCheckBoxCell).Value) = True Then
                                            If Not listSelectedPaths.Contains(fpth) Then
                                                listSelectedPaths.Add(fpth)
                                            End If
                                        Else
                                            If listSelectedPaths.Contains(fpth) Then
                                                listSelectedPaths.Remove(fpth)
                                            End If
                                        End If
                                    End If
                                End If
                            Next
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
        Dim hittestInfo1 As Windows.Forms.DataGridView.HitTestInfo = DataGridView1.HitTest(e.Location.X, e.Location.Y)
        If hittestInfo1.ColumnIndex = 0 Then
            Return
        End If
        Try
            If DataGridView1.SelectedRows(0).Cells("FileType").Value.ToString.ToLower = "file folder" Then
                Dim strCurFolder As String = getFTPPath() & ""
                Dim selFolder As String = CStr(DataGridView1.SelectedRows(0).Cells("FileName").Value).ToString.TrimStart("\"c).TrimEnd("\"c)
                If selFolder.ToString.ToLower() = "▲ Up".ToLower() Then
                    txtFTPRoot.Text = getParentFolder()
                    ListDirectoryDetails()
                    Return
                End If
                txtFTPRoot.Text = strCurFolder.TrimEnd("\"c) & "\"c & selFolder.TrimEnd("\"c) & "\"c
                ListDirectoryDetails()
            Else
                Try
                    If DataGridView1.RowCount <= 0 Then Return
                    If e.Button = Windows.Forms.MouseButtons.Left Then
                        Dim rowIndex As Integer = DataGridView1.HitTest(e.Location.X, e.Location.Y).RowIndex + 0
                        If rowIndex >= 0 Then
                            DataGridView1.Rows(rowIndex).Selected = True
                            If DataGridView1.SelectedRows.Count > 0 Then
                                If DataGridView1.SelectedRows(0).Cells("FileType").Value.ToString.ToLower = "file folder" Then
                                    DataGridView1_MouseClick(Me, e)
                                    If ContextMenuStripFile1.Visible Then ContextMenuStripFile1.Hide()
                                    ContextMenuStripFolder1.Show(DataGridView1, e.Location)
                                Else
                                    DataGridView1_MouseClick(Me, e)
                                    If ContextMenuStripFolder1.Visible Then ContextMenuStripFolder1.Hide()
                                    If Path.GetExtension(CStr(DataGridView1.SelectedRows(0).Cells("FileName").Value)).ToString().ToLower().TrimStart("."c) = "vb".ToLower() Then
                                        RemoveVbNetCommentsToolStripMenuItem.Visible = True
                                    Else
                                        RemoveVbNetCommentsToolStripMenuItem.Visible = False
                                    End If
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
        AppendToPDFEditorToolStripMenuItem.Visible = False
        Try
            If DataGridView1.SelectedRows.Count <= 0 Then
                txtFolderName.Text = ""
                txtFileName.Text = ""
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
            If DataGridView1.SelectedRows(0).Cells("FileType").Value.ToString.ToLower = "file folder" Then
                GroupBoxFolder1.Visible = True
                GroupBoxFile1.Visible = False
                OpenFolderToolStripMenuItem.Visible = True
                DeleteFolderToolStripMenuItem.Visible = True
                If DataGridView1.SelectedRows(0).Cells("FileName").Value.ToString.TrimStart("\"c).TrimEnd("\"c) = "▲ Up".ToLower Then
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
                    txtFolderName.Text = CStr(DataGridView1.SelectedRows(0).Cells("FileName").Value)
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
                txtFileName.Text = CStr(DataGridView1.SelectedRows(0).Cells("FileName").Value)
                Try
                    lblUpload_FileName.Text = "File Name: " & Environment.NewLine & "Size: " & CStr(DataGridView1.SelectedRows(0).Cells("FileName").Value) & ""
                Catch ex As Exception
                    lblUpload_FileName.Text = "File Name: "
                    Err.Clear()
                End Try
                Select Case Path.GetExtension(txtFileName.Text & "").ToString.ToLower.TrimStart("."c)
                    Case "pdf", "fdf", "xfdf", "xdp", "xml"
                        If Path.GetExtension(txtFileName.Text & "").ToString.ToLower.TrimStart("."c) = "pdf" Then
                            If Not frm.Session Is Nothing Then
                                If frm.Session.Length > 0 Then
                                    AppendToPDFEditorToolStripMenuItem.Visible = True
                                End If
                            End If
                        End If
                        btnFileLoadPdfFileIntoPdfEditor.Visible = True
                        btnFileGetPdfFromEditor.Visible = True
                        OpenWithPDFEditorToolStripMenuItem.Visible = True
                        If Not String.IsNullOrEmpty(txtFileName.Text & "") Then
                            btnFileLoadPdfFileIntoPdfEditor.Visible = True
                        Else
                            btnFileLoadPdfFileIntoPdfEditor.Visible = False
                        End If
                    Case "jpg", "jpeg", "bmp", "gif", "png", "tif", "tiff"
                        If Not frm.Session Is Nothing Then
                            If frm.Session.Length > 0 Then
                                AppendToPDFEditorToolStripMenuItem.Visible = True
                            End If
                        End If
                        btnFileLoadPdfFileIntoPdfEditor.Visible = True
                        btnFileGetPdfFromEditor.Visible = False
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
        txtFileName.Text = ""
        txtFolderName.Text = ""
    End Sub
    Private Sub btnFTPUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFTPUp.Click
        Try
            Dim strCurFolder As String = getParentFolder() & ""
            If strCurFolder.Length > 0 Then
                txtFTPRoot.Text = CStr(strCurFolder & "")
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
    Public Sub New(ByRef frmMain1 As frmMain, Optional ByVal initialDirectory As String = "")
        Try
            InitializeComponent()
            ShowUploadControls = False
            frm = frmMain1
            If Not String.IsNullOrEmpty(initialDirectory & "") Then
                Me.txtFTPRoot.Text = initialDirectory
            End If
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        End Try
    End Sub
    Public Sub New()
        InitializeComponent()
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
                od.InitialDirectory = frm.ApplicationDataFolder(True, "") & "\"
            End If
            od.Multiselect = False
            Select Case od.ShowDialog(Me)
                Case Windows.Forms.DialogResult.OK, Windows.Forms.DialogResult.Yes
                    DataGridView1.ClearSelection()
                    _fileBytes = File.ReadAllBytes(od.FileName)
                    _fileName = Path.GetFileName(od.FileName)
                    txtFileName.Text = _fileName
                    lblUpload_FileName.Text = "File Name: " & Environment.NewLine & "Size: " & frm.getMegaBytesText(_fileBytes.Length) & " "
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
        RenameFile(CStr(DataGridView1.SelectedRows(0).Cells("FileName").Value), txtFileName.Text)
    End Sub
    Private Sub btnFolderRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFolderRename.Click
        If DataGridView1.SelectedRows.Count <= 0 Then Return
        RenameFolder(CStr(DataGridView1.SelectedRows(0).Cells("FileName").Value), txtFolderName.Text)
    End Sub
    Private Sub btnFolderBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFolderBrowse.Click
        If DataGridView1.SelectedRows.Count <= 0 Then Return
        If DataGridView1.SelectedRows(0).Cells("FileType").Value.ToString.ToLower = "file folder" Then
            Dim strCurFolder As String = getFTPPath() & ""
            Dim selFolder As String = CStr(DataGridView1.SelectedRows(0).Cells("FileName").Value).ToString.TrimStart("\"c).TrimEnd("\"c)
            strCurFolder = strCurFolder.ToString.TrimEnd("\"c) & "\"c & selFolder & "\"
            txtFTPRoot.Text = strCurFolder
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
            sfd.SelectedPath = frm.ApplicationDataFolder(True, "temp") & "\"
        End If
        sfd.Description = "Select destination folder:"
        Select Case sfd.ShowDialog(Me)
            Case Windows.Forms.DialogResult.OK, Windows.Forms.DialogResult.Yes
                If Directory.Exists(sfd.SelectedPath & "") Then
                    Dim openDefaultViewer As Boolean = False
                    Dim openPdfEditor As Boolean = False
                    Dim openFolder As Boolean = False
GOTO_DOWNLOAD:
                    DownloadFile(CStr(DataGridView1.SelectedRows(0).Cells("FileName").Value), sfd.SelectedPath.ToString.TrimEnd("\"c) & "\"c & CStr(DataGridView1.SelectedRows(0).Cells("FileName").Value), openFolder, openDefaultViewer, openPdfEditor, False)
                    If openPdfEditor Then
                        frm.dlgValues.Clear()
                        frm.dlgValues.Add(txtFileName.Text)
                        frm.dlgValues.Add(txtFolderName.Text)
                        frm.dlgValues.Add("")
                        frm.dlgValues.Add(txtFTPPassword.Text)
                        frm.dlgValues.Add(txtFTPRoot.Text.ToString.Replace(Environment.NewLine, ""))
                        frm.dlgValues.Add(txtFTPUsername.Text)
                        Me.DialogResult = Windows.Forms.DialogResult.OK
                        Me.Close()
                    End If
                End If
        End Select
    End Sub
    Private Sub btnFileLoadPdfFileInPdfEditor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileLoadPdfFileIntoPdfEditor.Click
        If DataGridView1.SelectedRows.Count <= 0 Then Return
        Try
            Me.Hide()
            Me.DialogResult = Windows.Forms.DialogResult.OK
            frm.OpenFile(getFTPPath.ToString.TrimEnd("\"c) & "\" & CStr(DataGridView1.SelectedRows(0).Cells("FileName").Value), True)
        Catch ex As Exception
            Err.Clear()
        Finally
            Me.Close()
            Me.Dispose()
        End Try
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
            For Each fsItem As FileSystemItemVB In getSubDirectoriesAndFiles(ftpPath, False, True).ToArray()
                If cancelProgress = True Then
                    Return False
                End If
                Try
                    DeleteFile(ftpPath.ToString.TrimEnd("\"c) & "\"c & fsItem.FileName, False, False)
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
                    If Not DeleteFiles(ftpPath.ToString.TrimEnd("\"c) & "\"c & fsItem.FileName & "\") Then
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
                    If Not DeleteSubfolders(ftpPath.ToString.TrimEnd("\"c) & "\"c & fsItem.FileName & "\") Then
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
                    RemoveDirectory(ftpPath.ToString.TrimEnd("\"c) & "\"c & fsItem.FileName & "\", False, False)
                Catch exRemoveDirectory As Exception
                    Return False
                    Err.Clear()
                End Try
                If cancelProgress = True Then
                    Return False
                End If
            Else
            End If
        Next
        Return True
    End Function
    Public Function DeleteFolder(ByVal ftpPath As String) As Boolean
        Try
            If cancelProgress = True Then
                Return False
            End If
            Directory.Delete(ftpPath, True)
            Return True
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
        If DataGridView1.SelectedRows(0).Cells("FileType").Value.ToString.ToLower = "file folder" Then
            Dim strCurFolder As String = getFTPPath()
            Dim selFolder As String = CStr(DataGridView1.SelectedRows(0).Cells("FileName").Value).ToString.TrimStart("\"c).TrimEnd("\"c)
            Select Case MsgBox("Confirm Delete Folder: " & selFolder & Environment.NewLine & "Warning: Contents will be permanently lost.", MsgBoxStyle.Critical Or MsgBoxStyle.YesNoCancel Or MsgBoxStyle.ApplicationModal, "WARNING!")
                Case MsgBoxResult.Yes, MsgBoxResult.Ok
                    btnFolderDelete.Text = "Cancel Delete"
                    cancelProgress = False
                    If DeleteFolder(strCurFolder & selFolder & "\") Then
                        ListDirectoryDetails()
                        TextBox2.Text = "Folder deleted successfully: " & getFTPPath() & selFolder & "\"c & Environment.NewLine & TextBox2.Text & ""
                    Else
                        TextBox2.Text = "Folder NOT deleted: " & getFTPPath() & selFolder & "\"c & Environment.NewLine & TextBox2.Text & ""
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
        If Not DataGridView1.SelectedRows(0).Cells("FileType").Value.ToString.ToLower = "file folder" Then
            Dim strCurFolder As String = getFTPPath() & ""
            Dim selFile As String = CStr(DataGridView1.SelectedRows(0).Cells("FileName").Value).ToString.TrimStart("\"c).TrimEnd("\"c)
            DeleteFile(selFile)
            ListDirectoryDetails()
        End If
    End Sub
    Private Sub btnFileOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileOpen.Click
        If DataGridView1.SelectedRows.Count <= 0 Then Return
        Try
            Dim tmpFolderPath As String = frm.ApplicationDataFolder(True, "temp") & "\"
            DownloadFile(CStr(DataGridView1.SelectedRows(0).Cells("FileName").Value), tmpFolderPath & CStr(DataGridView1.SelectedRows(0).Cells("FileName").Value), False, True, False)
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
        frm.dlgValues.Clear()
        frm.dlgValues.Add(txtFileName.Text)
        frm.dlgValues.Add(txtFolderName.Text)
        frm.dlgValues.Add("")
        frm.dlgValues.Add(txtFTPPassword.Text)
        frm.dlgValues.Add(txtFTPRoot.Text.ToString.Replace(Environment.NewLine, ""))
        frm.dlgValues.Add(txtFTPUsername.Text)
        Me.DialogResult = Windows.Forms.DialogResult.OK
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
                        Dim tmpFn As String = frm.ApplicationDataFolder(True, "temp") & "\" & System.IO.Path.GetFileNameWithoutExtension(frm.fpath) & ".pdf"
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
        GroupBoxFolder1.Visible = True
        GroupBoxFile1.Visible = False
    End Sub
    Private Sub btnFolderNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFolderNew.Click
        MakeNewFolder(txtFolderName.Text)
    End Sub
    Private Sub NewFolderToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewFolderToolStripMenuItem1.Click
        txtFolderName.Text = ""
        GroupBoxFolder1.Visible = True
        GroupBoxFile1.Visible = False
    End Sub
    Private Sub OpenURLToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenHTTPURLToolStripMenuItem.Click
        Try
            If Not DataGridView1.SelectedRows.Count > 0 Then Return
            Dim strURL As String = getFTPPath().ToString.TrimEnd("\"c)
            Dim strFN As String = CStr(DataGridView1.SelectedRows(0).Cells("FileName").Value).ToString
            strURL = strURL.Replace("ftp://", "http://")
            strURL &= "\"c & strFN
            Process.Start(strURL)
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub DataGridView1_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SizeChanged
    End Sub
    Private Sub RenameFolderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RenameFolderToolStripMenuItem.Click
        GroupBoxFolder1.Visible = True
        GroupBoxFile1.Visible = False
    End Sub
    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim fs As New FolderSelect.FolderSelectDialog
            fs.InitialDirectory = getFTPPath()
            fs.Title = "Select Directory:"
            Select Case fs.ShowDialog(Me.Handle)
                Case True
                    If Directory.Exists(CStr(fs.FileName.ToString.TrimEnd("\"c) & "\"c)) Then
                        txtFTPRoot.Text = CStr(fs.FileName.ToString.TrimEnd("\"c) & "\"c)
                        ListDirectoryDetails()
                        fs = Nothing
                    End If
                Case False
                    Return
            End Select
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public Function mergeFiles(ByVal files As List(Of String), Optional ByVal smart As Boolean = True) As Byte()
        Dim document As New iTextSharp.text.Document
        Dim copy As iTextSharp.text.pdf.PdfCopy
        Dim m As New MemoryStream
        If smart Then
            copy = New iTextSharp.text.pdf.PdfSmartCopy(document, m)
        Else
            copy = New iTextSharp.text.pdf.PdfCopy(document, m)
        End If
        document.Open()
        Dim reader() As iTextSharp.text.pdf.PdfReader = New iTextSharp.text.pdf.PdfReader(2) {}
        For i As Integer = 0 To files.Count - 1
            reader(i) = New iTextSharp.text.pdf.PdfReader(files(i))
            copy.AddDocument(reader(i))
            copy.FreeReader(reader(i))
            reader(i).Close()
        Next
        copy.CloseStream = False
        document.Close()
        Return m.ToArray
    End Function
    Public Function BuildMultiPagePDF(ByVal fileArray()() As Byte) As Byte()
        Using outPutPDF As New MemoryStream
            Try
                Dim reader As iTextSharp.text.pdf.PdfReader = Nothing
                Dim pageCount As Integer = 0
                Dim currentPage As Integer = 0
                Dim pdfDoc As iTextSharp.text.Document = Nothing
                Dim writer As iTextSharp.text.pdf.PdfCopy = Nothing
                Dim page As iTextSharp.text.pdf.PdfImportedPage = Nothing
                Dim currentPDF As Integer = 0
                If fileArray.Length > 0 Then
                    reader = New iTextSharp.text.pdf.PdfReader(fileArray(currentPDF))
                    pdfDoc = New iTextSharp.text.Document(reader.GetPageSizeWithRotation(1))
                    writer = New iTextSharp.text.pdf.PdfCopy(pdfDoc, outPutPDF)
                    pageCount = reader.NumberOfPages
                    While currentPDF < fileArray.Length
                        pdfDoc.Open()
                        While currentPage < pageCount
                            currentPage += 1
                            pdfDoc.SetPageSize(reader.GetPageSizeWithRotation(currentPage))
                            pdfDoc.NewPage()
                            page = writer.GetImportedPage(reader, currentPage)
                            writer.AddPage(page)
                        End While
                        currentPDF += 1
                        If currentPDF < fileArray.Length Then
                            reader = New iTextSharp.text.pdf.PdfReader(fileArray(currentPDF))
                            pageCount = reader.NumberOfPages
                            currentPage = 0
                        End If
                    End While
                    writer.CloseStream = False
                    pdfDoc.Close()
                Else
                End If
                If Not outPutPDF Is Nothing Then
                    If outPutPDF.Length > 0 Then
                        Return outPutPDF.ToArray
                    End If
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
                outPutPDF.Close()
            End Try
        End Using
        Return Nothing
    End Function
    Public Function mergeFiles(ByVal files As List(Of Byte()), Optional ByVal smart As Boolean = True) As Byte()
        Dim document As New iTextSharp.text.Document
        Dim copy As iTextSharp.text.pdf.PdfCopy
        Dim m As New MemoryStream
        If smart Then
            copy = New iTextSharp.text.pdf.PdfSmartCopy(document, m)
        Else
            copy = New iTextSharp.text.pdf.PdfCopy(document, m)
        End If
        document.Open()
        Dim reader() As iTextSharp.text.pdf.PdfReader = New iTextSharp.text.pdf.PdfReader(2) {}
        For i As Integer = 0 To files.Count - 1
            reader(i) = New iTextSharp.text.pdf.PdfReader(files(i))
            copy.AddDocument(reader(i))
            copy.FreeReader(reader(i))
        Next
        copy.SetMergeFields()
        copy.CloseStream = False
        document.Close()
        For i As Integer = 0 To files.Count - 1
            reader(i).Close()
        Next
        copy.Close()
        Return m.ToArray
    End Function
    Public Function mergeForms(ByVal files()() As Byte) As Byte()
        Dim m As New MemoryStream
        Dim copier As New PdfCopyFields(m)
        Dim i As Integer = 0
        For Each f As Byte() In files.ToArray
            Dim reader As New PdfReader(f)
            i += 1
            reader = renameFields(reader, i)
            copier.AddDocument(reader)
        Next
        copier.Close()
        Return m.ToArray
    End Function
    Dim fieldNames As New List(Of String)
    Private Sub renameFields(ByRef fields As AcroFields, ByVal i As Integer)
    End Sub
    Dim lstFields As New List(Of String)
    Public Function renameFields(ByVal reader As PdfReader, ByVal i As Integer) As PdfReader
        Dim form As iTextSharp.text.pdf.AcroFields = reader.AcroFields
        Dim keys() As String = form.Fields.Keys.ToArray
        For Each key As String In keys
            If lstFields.Contains(key.ToLower) Then
                form.RenameField(key, CStr(key & "_" & i.ToString))
            Else
                lstFields.Add(key.ToLower)
            End If
        Next
        Return reader
    End Function
    Public Function manipulatePdf(ByVal src()() As Byte) As Byte()
        Dim document As New iTextSharp.text.Document
        Dim m As New MemoryStream
        Dim copy As New PdfSmartCopy(document, m)
        copy.SetMergeFields()
        document.Open()
        Dim readers As New List(Of PdfReader)
        Dim i As Integer = 0
        keyNames = New List(Of String)
        For Each srcDoc As Byte() In src.ToArray
            i += 1
            Dim reader As New PdfReader(renameFields2(frm.UnlockSecurePDF(srcDoc.ToArray()), i))
            readers.Add(reader)
            copy.AddDocument(reader)
        Next
        copy.CloseStream = False
        document.Close()
        For iCntr As Integer = 0 To i - 1
            readers(iCntr).Close()
        Next
        Return m.ToArray
    End Function
    Dim keyNames As List(Of String)
    Public Function renameFields2(ByVal src() As Byte, ByVal i As Integer) As Byte()
        Dim baos As New MemoryStream
        Dim reader As New PdfReader(src)
        Dim stamper As New PdfStamper(reader, baos)
        Dim form As AcroFields = stamper.AcroFields
        For Each key As String In form.Fields.Keys.ToArray
            If keyNames.Contains(key.ToLower) Then
                form.RenameField(key, String.Format("%s_%d", key, i))
            Else
                keyNames.Add(key.ToLower & "")
            End If
        Next
        stamper.Writer.CloseStream = False
        stamper.Close()
        reader.Close()
        Return baos.ToArray
    End Function
    Private Sub AppendToPDFEditorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AppendToPDFEditorToolStripMenuItem.Click
        If DataGridView1.SelectedRows.Count <= 0 Then Return
        Dim fn As String = getFTPPath() & CStr(DataGridView1.SelectedRows(0).Cells("FileName").Value) & ""
        If Not frm.Session Is Nothing Then
            If frm.Session.Length > 0 Then
                Select Case Path.GetExtension(fn).ToString.TrimStart("."c).ToLower()
                    Case "jpeg", "jpg", "bmp", "png", "tif", "tiff", "gif"
                        frm.ImportImage(fn, False)
                    Case "pdf"
                        Dim lst As New List(Of Byte())
                        lst.AddRange(New Byte()() {frm.Session, File.ReadAllBytes(fn)})
                        frm.mem.Clear()
                        frm.Session = frm.EncryptPDFDocument(frm.cFDFDoc.PDFConcatenateForms2Buf(lst.ToArray))
                        frm.LoadPDFReaderDoc(frm.pdfOwnerPassword & "", True)
                        frm.cUserRect.pauseDraw = True
                        frm._cFDFDoc = frm.cFDFDoc
                        frm.LoadPageList(frm.btnPage, frm.cFDFDoc)
                        frm.btnPage.SelectedIndex = 0
                        frm.cmbPercent.SelectedIndex = 3
                        frm.cUserRect.pauseDraw = False
                        frm.A0_LoadPDF(True, True, True, -1, True)
                        If frm.pnlFields.Visible Then frm.pnlFields.Visible = False
                        frm.A0_PictureBox1.Enabled = True
                        frm.btnPage.SelectedIndex = 0
                    Case Else
                End Select
            End If
        Else
        End If
    End Sub
    Private Sub btnFTPOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFTPOpen.Click
        Process.Start(txtFTPRoot.Text)
    End Sub
    Private Sub OpenFolderToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenFolderToolStripMenuItem1.Click
        If DataGridView1.SelectedRows.Count <= 0 Then Return
        If DataGridView1.SelectedRows(0).Cells("FileType").Value.ToString.ToLower = "file folder" Then
            Dim strCurFolder As String = getFTPPath() & ""
            Dim selFolder As String = CStr(DataGridView1.SelectedRows(0).Cells("FileName").Value).ToString.TrimStart("\"c).TrimEnd("\"c)
            strCurFolder = strCurFolder.ToString.TrimEnd("\"c) & "\"c & selFolder & "\"
            Process.Start(strCurFolder)
        End If
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            ListFileDetails(listSelectedPaths)
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub OpenFTPURLToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenFTPURLToolStripMenuItem.Click
        Try
            If Not DataGridView1.SelectedRows.Count > 0 Then Return
            Dim strURL As String = getFTPPath().ToString.TrimEnd("\"c)
            Dim strFN As String = CStr(DataGridView1.SelectedRows(0).Cells("FileName").Value).ToString
            strURL &= "\"c & strFN
            Process.Start(strURL)
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub RemoveCommentsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveVbNetCommentsToolStripMenuItem.Click
        Try
            If DataGridView1.SelectedRows.Count <= 0 Then Return
            Dim fn As String = getFTPPath.ToString.TrimEnd("\"c) & "\"c & txtFileName.Text
            Dim b() As Byte = File.ReadAllBytes(fn)
            Dim strReader As New StreamReader(New MemoryStream(b.ToArray()), True)
            Dim strBuilder As New System.Text.StringBuilder
            Do While Not strReader.EndOfStream
                Try
                    Dim line As String = strReader.ReadLine & ""
                    If line.TrimStart(" "c).Trim(vbTab).StartsWith(CStr("''' ")) Then
                        strBuilder.AppendLine(line)
                    ElseIf line.TrimStart(" "c).Trim(vbTab).StartsWith(CStr("'")) Then
                        Dim ln As String = line
                    Else
                        If Not String.IsNullOrEmpty(line.Trim(" "c).Trim(vbTab) & "") Then
                            strBuilder.AppendLine(line)
                        End If
                    End If
                Catch ex As Exception
                    Err.Clear()
                End Try
            Loop
            File.WriteAllText(fn, strBuilder.ToString)
            MsgBox("Removed Commenting completed")
        Catch ex As Exception
            Err.Clear()
            MsgBox("Removed Commenting error: " & ex.Message)
        End Try
    End Sub
    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
    End Sub
End Class
