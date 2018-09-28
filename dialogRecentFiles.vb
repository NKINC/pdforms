Imports System.Windows.Forms
Imports System.Net
Imports System.IO
Imports Microsoft.VisualBasic
Imports iTextSharp.text.pdf
Public Class dialogRecentFiles
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
        Try
            If DataGridView1.SelectedRows(0).Cells("FileType").Value.ToString.ToLower = "file folder" Then
            Else
                Try
                    If DataGridView1.RowCount <= 0 Then Return
                    If DataGridView1.SelectedRows.Count > 0 Then
                        If DataGridView1.SelectedRows(0).Cells("FileType").Value.ToString.ToLower = "file folder" Then
                        Else
                            If Not frm Is Nothing Then
                                frm.OpenFile(fsItems(DataGridView1.SelectedRows(0).Index).getFullPath, True, True)
                                frm.A0_LoadPDF()
                                Me.Close()
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
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.frm.dlgValues.Clear()
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
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
    Public Function getFTPPath() As String
        Dim ftpPath As String = txtFTPRoot.Text
        Return ftpPath.ToString
    End Function
    Public Sub ListDirectoryDetails(fileName As String)
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
                For Each line In System.IO.File.ReadLines(fileName).ToArray()
                    Try
                        If Not lines.Contains(line) Then
                            lines.Add(line)
                            Dim fi As New FileInfo(line)
                            If fi.Exists Then
                                Dim ext As String = System.IO.Path.GetExtension(line & "").Trim("."c)
                                If ext = "exe" Or ext = "com" Or ext = "bat" Or ext = "cmd" Then
                                    item = New FileSystemItemVB(frm, System.IO.Path.GetDirectoryName(line) & "", fi.Extension.ToString() & "", CInt("1"), "owner", "group", fi.Length.ToString & "", fi.LastWriteTime().Month.ToString & "", fi.LastWriteTime().Day.ToString & "", fi.LastWriteTime().Year.ToString & "", Path.GetFileName(line) & "")
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
            Console.WriteLine(strReader)
            Console.WriteLine("Directory List Complete, status {0}", "Success")
        Catch exMain As Exception
            MsgBox(exMain.Message)
            If frm.debugMode Then Throw exMain Else Err.Clear()
        Finally
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
            DataGridView1.Columns(0).Width = 36
            DataGridView1.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            DataGridView1.Columns(1).MinimumWidth = 220
            DataGridView1.Columns(2).Width = 75
            DataGridView1.Columns(3).Width = 110
            DataGridView1.Columns(4).Width = 130
            DataGridView1.AutoResizeColumn(1)
        End Try
    End Sub
    Public Function getReverseOrderListString(lst As List(Of String)) As List(Of String)
        Try
            Dim l As New List(Of String)
            For Each str As String In lst.ToArray
                Try
                    If frm.IsValidUrl(str) Then
                        l.Insert(0, str)
                    ElseIf System.IO.File.Exists(str) Then
                        l.Insert(0, str)
                    End If
                Catch ex2 As Exception
                    Err.Clear()
                End Try
            Next
            Return l
        Catch ex As Exception
            Err.Clear()
        End Try
        Return lst
    End Function
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
                For Each line In getReverseOrderListString(lst)
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
            Dim dcc As Integer = DataGridView1.Columns.Count
            DataGridView1.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            DataGridView1.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            For c As Integer = 0 To dcc - 1
                DataGridView1.Columns(c).AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            Next
            DataGridView1.Columns(0).Width = 36
            DataGridView1.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            DataGridView1.Columns(2).Width = 75
            DataGridView1.Columns(3).Width = 110
            DataGridView1.Columns(4).Width = 130
            DataGridView1.AutoResizeColumn(1)
            Console.WriteLine(strReader)
            Console.WriteLine("Directory List Complete, status {0}", "Success")
        Catch exMain As Exception
            MsgBox(exMain.Message)
            If frm.debugMode Then Throw exMain Else Err.Clear()
        Finally
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
    Private Sub dialogRecentFiles_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If Me.Owner.GetType Is GetType(frmMain) Then
                frm = DirectCast(Me.Owner, frmMain)
                colSortDirection = Nothing
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
                        Console.WriteLine("Directory List Complete, status {0}", "Success")
                    Catch exMain As Exception
                        MsgBox(exMain.Message)
                        If frm.debugMode Then Throw exMain Else Err.Clear()
                    Finally
                        If DataGridView1.RowCount > 1 Then
                        End If
                    End Try
                Else
                End If
            End If
        Catch ex As Exception
            Err.Clear()
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
                Return Nothing
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
            If DataGridView1.RowCount <= 0 Then Return
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        End Try
    End Sub
    Private Sub DataGridView1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGridView1.MouseDoubleClick
        If DataGridView1.SelectedRows.Count <= 0 Then Return
        Dim hittestInfo1 As Windows.Forms.DataGridView.HitTestInfo = DataGridView1.HitTest(e.Location.X, e.Location.Y)
        Try
            If DataGridView1.SelectedRows(0).Cells("FileType").Value.ToString.ToLower = "file folder" Then
            Else
                Try
                    If DataGridView1.RowCount <= 0 Then Return
                    If e.Button = Windows.Forms.MouseButtons.Left Then
                        Dim rowIndex As Integer = DataGridView1.HitTest(e.Location.X, e.Location.Y).RowIndex + 0
                        If rowIndex >= 0 Then
                            DataGridView1.Rows(rowIndex).Selected = True
                            If DataGridView1.SelectedRows.Count > 0 Then
                                If DataGridView1.SelectedRows(0).Cells("FileType").Value.ToString.ToLower = "file folder" Then
                                Else
                                    If Not frm Is Nothing Then
                                        frm.OpenFile(fsItems(rowIndex).getFullPath, True, True)
                                        frm.A0_LoadPDF()
                                        Me.Close()
                                    End If
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
        Try
            If DataGridView1.SelectedRows.Count <= 0 Then
                Return
            End If
            If DataGridView1.SelectedRows(0).Cells("FileType").Value.ToString.ToLower = "file folder" Then
                If DataGridView1.SelectedRows(0).Cells("FileName").Value.ToString.TrimStart("\"c).TrimEnd("\"c) = "▲ Up".ToLower Then
                Else
                End If
                Return
            Else
                Return
            End If
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        End Try
    End Sub
    Public Sub New(ByRef frmMain1 As frmMain, Optional ByVal initialRecentFilesPath As String = "")
        Try
            InitializeComponent()
            frm = frmMain1
            If Not String.IsNullOrEmpty(initialRecentFilesPath & "") Then
                txtFTPRoot.Text = initialRecentFilesPath
            End If
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        End Try
    End Sub
    Public Sub New()
        InitializeComponent()
    End Sub
    Private sfd As New FolderBrowserDialog
    Private Sub txtFTPRoot_TextChanged(sender As Object, e As EventArgs) Handles txtFTPRoot.TextChanged
        ListFileDetails(System.IO.File.ReadAllLines(txtFTPRoot.Text).ToList())
    End Sub
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.frm.dlgValues.Clear()
        Try
            If DataGridView1.SelectedRows(0).Cells("FileType").Value.ToString.ToLower = "file folder" Then
            Else
                Try
                    If DataGridView1.RowCount <= 0 Then Return
                    If DataGridView1.SelectedRows.Count > 0 Then
                        If DataGridView1.SelectedRows(0).Cells("FileType").Value.ToString.ToLower = "file folder" Then
                        Else
                            If Not frm Is Nothing Then
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
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
End Class
