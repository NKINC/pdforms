Imports System.Windows.Forms
Imports System.Net
Imports System.IO
Imports Microsoft.VisualBasic
Imports iTextSharp.text.pdf
Public Class dialogListFields
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
        Try
            SaveFdfData(fields)
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Catch ex As Exception
            Throw ex
        Finally
            Me.Close()
        End Try
    End Sub
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Public cancelProgress As Boolean = False
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
    Public fsItems As New SortedBindingList(Of FieldSystemItemVB)
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
    Public fields As New List(Of FieldSystemItemVB)
    Public Property fieldsItem(ByVal strName As String) As FieldSystemItemVB
        Get
            For i As Integer = 0 To fields.Count - 1
                If fields(i).FieldName = strName Then
                    Return fields(i)
                End If
            Next
            Return Nothing
        End Get
        Set(ByVal value As FieldSystemItemVB)
            For i As Integer = 0 To fields.Count - 1
                If fields(i).FieldName = strName Then
                    fields(i) = value
                End If
            Next
        End Set
    End Property
    Public hiddenRows As Integer = 0
    Public Sub ListFieldsDetails(Optional ByVal excludeFieldsUpdate As Boolean = False)
        Try
            fsItems.Clear()
            fsItems = New SortedBindingList(Of FieldSystemItemVB)
            hiddenRows = 0
            Dim strReader As String = ""
            Dim line As String = ""
            Dim lines As New List(Of String)
            If fields.Count <= 0 Or excludeFieldsUpdate = False Then
                fields = New List(Of FieldSystemItemVB)
            End If
            Dim item As FieldSystemItemVB
            Try
                Dim lst As New List(Of String)
                Dim cfdfDoc As FDFApp.FDFDoc_Class = frm.cFDFApp.PDFOpenFromBuf(frm.Session, True, True, frm.pdfOwnerPassword & "")
                For Each fld As FDFApp.FDFDoc_Class.FDFField In cfdfDoc.XDPGetAllFields().ToArray
                    line = fld.FieldName.ToString & ""
                    If Not lines.Contains(line) Then
                        lines.Add(line)
                        item = New FieldSystemItemVB(frm, fld)
                        Select Case item.FieldType
                            Case iTextSharp.text.pdf.AcroFields.FIELD_TYPE_CHECKBOX
                                If excludeFieldsUpdate = False Then
                                    fields.Add(item)
                                End If
                                fsItems.Add(item)
                            Case iTextSharp.text.pdf.AcroFields.FIELD_TYPE_COMBO, iTextSharp.text.pdf.AcroFields.FIELD_TYPE_LIST, iTextSharp.text.pdf.AcroFields.FIELD_TYPE_RADIOBUTTON, iTextSharp.text.pdf.AcroFields.FIELD_TYPE_TEXT
                                If excludeFieldsUpdate = False Then
                                    fields.Add(item)
                                End If
                                fsItems.Add(item)
                            Case Else
                        End Select
                        If Not line Is Nothing Then
                            strReader &= Environment.NewLine
                        End If
                    End If
                Next
            Catch exTRy As Exception
                Err.Clear()
            End Try
            DataGridView1.AutoGenerateColumns = True
            DataGridView1.DataSource = fsItems
            Try
                DataGridView1.AutoSize = False
                DataGridView1.AllowUserToResizeColumns = True
                DataGridView1.AllowUserToOrderColumns = True
                DataGridView1.BorderStyle = BorderStyle.None
                DataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
                DataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect
            Catch ex As Exception
                If frm.debugMode Then Throw ex Else Err.Clear()
            End Try
            For c As Integer = DataGridView1.Columns.Count - 1 To 1 Step -1
                DataGridView1.Columns.RemoveAt(1)
            Next
            DataGridView1.Columns(0).ReadOnly = True
            Try
                Dim rc As Integer = DataGridView1.RowCount
                For r As Integer = 0 To rc - 1
                    Dim fldName1 As String = DataGridView1.Rows(r).Cells(0).Value.ToString & ""
                    Select Case frm.GetFormFieldType(frm.Session, fldName1)
                        Case iTextSharp.text.pdf.AcroFields.FIELD_TYPE_TEXT
                        Case iTextSharp.text.pdf.AcroFields.FIELD_TYPE_RADIOBUTTON
                        Case iTextSharp.text.pdf.AcroFields.FIELD_TYPE_CHECKBOX
                        Case iTextSharp.text.pdf.AcroFields.FIELD_TYPE_COMBO
                        Case iTextSharp.text.pdf.AcroFields.FIELD_TYPE_LIST
                        Case iTextSharp.text.pdf.AcroFields.FIELD_TYPE_PUSHBUTTON, iTextSharp.text.pdf.AcroFields.FIELD_TYPE_SIGNATURE
                            Dim bm As BindingManagerBase = DataGridView1.BindingContext(DataGridView1.DataSource)
                            bm.SuspendBinding()
                            DataGridView1.Rows(r).Visible = False
                            bm.ResumeBinding()
                        Case Else
                            Dim bm As BindingManagerBase = DataGridView1.BindingContext(DataGridView1.DataSource)
                            bm.SuspendBinding()
                            DataGridView1.Rows(r).Visible = False
                            bm.ResumeBinding()
                    End Select
                Next
                hiddenRows = 0
                For r As Integer = rc - 1 To 0 Step -1
                    If DataGridView1.Rows(r).Visible = False Then
                        hiddenRows += 1
                    End If
                Next
            Catch ex As Exception
                Throw ex
            End Try
            Console.WriteLine(strReader)
            Console.WriteLine("Directory List Complete, status {0}", "Success")
            TextBox1.Text &= Environment.NewLine & strReader
        Catch exMain As Exception
            MsgBox(exMain.Message)
            If frm.debugMode Then Throw exMain Else Err.Clear()
        Finally
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
            End If
            If DataGridView1.RowCount - hiddenRows > 1 Then
                DataGridView1.Focus()
                DataGridView1.Select()
                DataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically
                DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
                If DataGridView1.SelectedRows.Count <= 0 Then DataGridView1.Rows(0).Selected = True
            End If
        End Try
        DataGridView1.Focus()
    End Sub
    Public Sub SaveFdfData(ByVal fieldsList As List(Of FieldSystemItemVB))
        Try
            Try
                Dim cFDFDoc As FDFApp.FDFDoc_Class = frm.cFDFApp.PDFOpenFromBuf(frm.Session, True, True, frm.pdfOwnerPassword & "")
                Dim rc As Integer = fieldsList.Count
                For r As Integer = 0 To rc - 1
                    Try
                        Dim fldName1 As String = fieldsList(r).FieldName & ""
                        Dim item As FieldSystemItemVB = fieldsList(r)
                        Select Case item.FieldType
                            Case iTextSharp.text.pdf.AcroFields.FIELD_TYPE_TEXT
                                cFDFDoc.FDFSetValue(fldName1.ToString, item.FieldValue.ToString, True, True)
                            Case iTextSharp.text.pdf.AcroFields.FIELD_TYPE_RADIOBUTTON
                                cFDFDoc.FDFSetValue(fldName1.ToString, item.FieldValue.ToString, True, True)
                            Case iTextSharp.text.pdf.AcroFields.FIELD_TYPE_CHECKBOX
                                If CBool(item.FieldValue) = True Then
                                    cFDFDoc.FDFSetValue(fldName1, iText_GetField_V(item.FieldName), True, True)
                                Else
                                    cFDFDoc.FDFSetValue(fldName1, "Off", True, True)
                                End If
                            Case iTextSharp.text.pdf.AcroFields.FIELD_TYPE_COMBO
                                cFDFDoc.FDFSetValue(fldName1.ToString, item.FieldValue.ToString, True, True)
                            Case iTextSharp.text.pdf.AcroFields.FIELD_TYPE_LIST
                                cFDFDoc.FDFSetValues(fldName1, item.getFieldValues(), True)
                            Case iTextSharp.text.pdf.AcroFields.FIELD_TYPE_PUSHBUTTON, iTextSharp.text.pdf.AcroFields.FIELD_TYPE_SIGNATURE
                            Case Else
                        End Select
                    Catch ex As Exception
                        Throw ex
                    End Try
                Next
                frm.Session = cFDFDoc.PDFMergeFDF2Buf(frm.Session, False, frm.pdfOwnerPassword & "")
                frm.A0_LoadPDF(True, True, True, CInt(frm.btnPage.SelectedIndex) + 1, True)
            Catch ex As Exception
                Throw ex
            End Try
        Catch exMain As Exception
            MsgBox(exMain.Message)
            If frm.debugMode Then Throw exMain Else Err.Clear()
        Finally
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End Try
    End Sub
    Public Function GetDirectoryFileName1(ByVal strPath As String) As String
        Return CStr(strPath.ToString.TrimEnd("\"c).Substring(strPath.LastIndexOf("\"c), strPath.Length - strPath.LastIndexOf("\"c)) & "").TrimEnd("\"c).TrimStart("\"c)
    End Function
    Public Function getSubDirectoriesAndFiles(ByVal dirPath As String, ByVal directoriesOnly As Boolean, ByVal filesOnly As Boolean) As List(Of FieldSystemItemVB)
        Dim fsItems2 As New List(Of FieldSystemItemVB)
        Try
            Dim line As String = ""
            Dim lines As New List(Of String)
            Dim fields As New List(Of FieldSystemItemVB)
            Dim folders As New List(Of FieldSystemItemVB)
            Dim item As FieldSystemItemVB
            Try
                Dim cfdfDoc As FDFApp.FDFDoc_Class = frm.cFDFApp.PDFOpenFromBuf(frm.Session, True, True, frm.pdfOwnerPassword & "")
                For Each fld As FDFApp.FDFDoc_Class.FDFField In cfdfDoc.XDPGetAllFields().ToArray
                    line = fld.FieldName.ToString & ""
                    If Not lines.Contains(line) Then
                        lines.Add(line)
                        item = New FieldSystemItemVB(frm, fld)
                        fields.Add(item)
                        fsItems.Add(item)
                    End If
                Next
            Catch exTRy As Exception
                Err.Clear()
            End Try
            If directoriesOnly Then
                fsItems2.AddRange(folders.ToArray)
            ElseIf filesOnly Then
                fsItems2.AddRange(fields.ToArray)
            Else
                fsItems2.AddRange(folders.ToArray)
                fsItems2.AddRange(fields.ToArray)
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
                LoadDialog(True)
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public Sub LoadDialog(ByVal blnShow As Boolean)
        Try
            If Not frm Is Nothing Then
                If Not frm.Session Is Nothing Then
                    If frm.Session.Length > 0 Then
                        _fileBytes = frm.Session
                        _fileName = Path.GetFileName(frm.fpath & "")
                        ListFieldsDetails(False)
                    End If
                End If
            End If
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        Finally
            Me.Visible = blnShow
        End Try
    End Sub
    Public Class FieldSystemItemVB
        Inherits System.ComponentModel.BindingList(Of FieldSystemItemVB)
        Private _frm As frmMain
        Private _Field As FDFApp.FDFDoc_Class.FDFField
        Private _FieldType As Integer
        Private _FieldName As String
        Private _FieldValues() As String
        Private _FieldDisplayNames() As String
        Private _FieldDisplayValues() As String
        Private _FieldDefaultValue() As String
        Private _FieldLevelLong As String
        Private _FieldNum As String
        Private _FieldEnabled As Boolean
        Private _ImageBase64 As String
        Public Function getListArray(ByVal lst As List(Of String)) As String()
            If Not lst Is Nothing Then
                If lst.Count > 0 Then
                    Return lst.ToArray
                End If
            End If
            Dim str() As String = {}
            Return str
        End Function
        Public Sub New(ByRef frmMain1 As frmMain, ByRef field1 As FDFApp.FDFDoc_Class.FDFField)
            _frm = frmMain1
            _Field = field1
            _FieldName = field1.FieldName
            _FieldType = _frm.GetFormFieldType(_frm.Session, field1.FieldName)
            _FieldValues = getListArray(field1.FieldValue)
            _FieldDisplayNames = getListArray(field1.DisplayName)
            _FieldDisplayValues = getListArray(field1.DisplayValue)
            _FieldDefaultValue = getListArray(field1.DefaultValue)
            _FieldLevelLong = field1.FieldLevelLong
            _FieldNum = field1.FieldNum.ToString
            _FieldEnabled = field1.FieldEnabled
            _ImageBase64 = field1.ImageBase64
        End Sub
        <System.ComponentModel.DisplayName("Name")>
        Public Property FieldName() As String
            Get
                Return _FieldName
            End Get
            Set(ByVal value As String)
                _FieldName = value
            End Set
        End Property
        <System.ComponentModel.DisplayName("Type")>
        Public Property FieldType() As Integer
            Get
                Return _FieldType
            End Get
            Set(ByVal value As Integer)
                _FieldType = value
            End Set
        End Property
        <System.ComponentModel.DisplayName("Values")>
        Private Property FieldValues() As String()
            Get
                Return (_FieldValues)
            End Get
            Set(ByVal value As String())
                _FieldValues = value
            End Set
        End Property
        Public Function getFieldValues() As String()
            Return _FieldValues
        End Function
        Public Sub setFieldValues(ByVal vals As String())
            _FieldValues = vals
        End Sub
        Public Function getFieldDisplayNames() As String()
            Return _FieldDisplayNames
        End Function
        Public Function getFieldDisplayValues() As String()
            Return _FieldDisplayValues
        End Function
        <System.ComponentModel.DisplayName("Value")>
        Public Property FieldValue() As Object
            Get
                If (_FieldValues.Count >= 1) Then
                    If FieldType = iTextSharp.text.pdf.AcroFields.FIELD_TYPE_CHECKBOX Then
                        If CStr(_FieldValues(0)).ToString.ToLower = "off" Then
                            Return False
                        Else
                            Return True
                        End If
                    Else
                        Return CStr(_FieldValues(0)) & ""
                    End If
                Else
                    If FieldType = iTextSharp.text.pdf.AcroFields.FIELD_TYPE_CHECKBOX Then
                        Return False
                    Else
                        Return ""
                    End If
                End If
            End Get
            Set(ByVal value As Object)
                _FieldValues = New String() {value.ToString}
            End Set
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
    End Sub
    Public listSelectedPaths As New List(Of String)
    Private Sub DataGridView1_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGridView1.MouseClick
    End Sub
    Private Sub DataGridView1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGridView1.MouseDoubleClick
    End Sub
    Public Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        Try
            If DataGridView1.RowCount - hiddenRows <= 0 Then Return
            If DataGridView1.SelectedRows.Count <= 0 Then Return
            Dim fieldName As String = DataGridView1.SelectedRows(0).Cells(0).Value.ToString & ""
            If String.IsNullOrEmpty(fieldName) Then Return
            Dim fld As FieldSystemItemVB = fieldsItem(fieldName)
            Me.FieldValue_TextBox.Visible = False
            Me.FieldValue_ListBoxDisplayNames.Visible = False
            Me.FieldValue_ListBoxExportValues.Visible = False
            Me.FieldValue_ComboBoxValues.Visible = False
            Me.FieldValue_ComboBoxDisplayNames.Visible = False
            Me.FieldValue_Checkbox.Visible = False
            lblFieldName.Text = fieldName & ""
            Select Case fld.FieldType
                Case iTextSharp.text.pdf.AcroFields.FIELD_TYPE_TEXT
                    Me.FieldValue_TextBox.Visible = True
                    Me.FieldValue_TextBox.Text = fld.FieldValue.ToString
                    FieldValue_TextBox.BringToFront()
                    FieldValue_TextBox.Focus()
                Case iTextSharp.text.pdf.AcroFields.FIELD_TYPE_RADIOBUTTON
                    Me.FieldValue_ComboBoxDisplayNames.Visible = True
                    Me.FieldValue_ComboBoxValues.Visible = False
                    Me.FieldValue_ComboBoxDisplayNames.Items.Clear()
                    Me.FieldValue_ComboBoxValues.Items.Clear()
                    For Each dv As String In fld.getFieldDisplayValues.ToArray
                        Me.FieldValue_ComboBoxValues.Items.Add(dv)
                    Next
                    If fld.getFieldDisplayNames.Length = fld.getFieldDisplayValues.Length And fld.getFieldDisplayValues.Length > 0 Then
                        For Each dv As String In fld.getFieldDisplayNames.ToArray
                            Me.FieldValue_ComboBoxDisplayNames.Items.Add(dv)
                        Next
                    Else
                        For Each dv As String In fld.getFieldDisplayValues.ToArray
                            Me.FieldValue_ComboBoxDisplayNames.Items.Add(dv)
                        Next
                    End If
                    If Not String.IsNullOrEmpty(fld.FieldValue.ToString() & "") Then
                        Me.FieldValue_ComboBoxValues.SelectedItem = fld.FieldValue.ToString() & ""
                        Me.FieldValue_ComboBoxValues_SelectedIndexChanged(Me, New EventArgs())
                    End If
                    FieldValue_ComboBoxValues.BringToFront()
                    FieldValue_ComboBoxValues.Focus()
                Case iTextSharp.text.pdf.AcroFields.FIELD_TYPE_CHECKBOX
                    Me.FieldValue_Checkbox.Visible = True
                    FieldValue_Checkbox.Text = ""
                    If Not String.IsNullOrEmpty(fld.FieldValue.ToString() & "") Then
                        If CBool(fld.FieldValue) = True Then
                            FieldValue_Checkbox.Checked = True
                        Else
                            FieldValue_Checkbox.Checked = False
                        End If
                    Else
                        FieldValue_Checkbox.Checked = False
                    End If
                    FieldValue_Checkbox.BringToFront()
                    FieldValue_Checkbox.Focus()
                Case iTextSharp.text.pdf.AcroFields.FIELD_TYPE_COMBO
                    Me.FieldValue_ComboBoxDisplayNames.Visible = True
                    Me.FieldValue_ComboBoxValues.Visible = False
                    Me.FieldValue_ComboBoxDisplayNames.Items.Clear()
                    Me.FieldValue_ComboBoxValues.Items.Clear()
                    For Each dv As String In fld.getFieldDisplayValues.ToArray
                        Me.FieldValue_ComboBoxValues.Items.Add(dv)
                    Next
                    For Each dv As String In fld.getFieldDisplayNames.ToArray
                        Me.FieldValue_ComboBoxDisplayNames.Items.Add(dv)
                    Next
                    Try
                        If Not String.IsNullOrEmpty(fld.FieldValue.ToString() & "") Then
                            Me.FieldValue_ComboBoxValues.SelectedItem = fld.FieldValue.ToString() & ""
                            Me.FieldValue_ComboBoxValues_SelectedIndexChanged(Me, New EventArgs())
                        End If
                    Catch exVal As Exception
                        Err.Clear()
                    End Try
                    FieldValue_ComboBoxDisplayNames.BringToFront()
                    FieldValue_ComboBoxDisplayNames.Focus()
                Case iTextSharp.text.pdf.AcroFields.FIELD_TYPE_LIST
                    Me.FieldValue_ListBoxExportValues.Enabled = False
                    Me.FieldValue_ListBoxDisplayNames.Enabled = False
                    Me.FieldValue_ListBoxExportValues.Visible = True
                    Me.FieldValue_ListBoxDisplayNames.Visible = True
                    Me.FieldValue_ListBoxExportValues.Items.Clear()
                    Me.FieldValue_ListBoxDisplayNames.Items.Clear()
                    Me.FieldValue_ListBoxExportValues.SendToBack()
                    Me.FieldValue_ListBoxDisplayNames.BringToFront()
                    FieldValue_ListBoxDisplayNames.SelectionMode = SelectionMode.MultiSimple
                    FieldValue_ListBoxExportValues.SelectionMode = SelectionMode.MultiSimple
                    If fld.getFieldDisplayNames.Length = fld.getFieldDisplayValues.Length And fld.getFieldDisplayValues.Length > 0 Then
                        For Each dv As String In fld.getFieldDisplayValues.ToArray
                            Me.FieldValue_ListBoxExportValues.Items.Add(dv)
                        Next
                        For Each dn As String In fld.getFieldDisplayNames.ToArray
                            Me.FieldValue_ListBoxDisplayNames.Items.Add(dn)
                        Next
                        If (fld.getFieldValues.Length > 0) Then
                            For Each v As String In fld.getFieldValues()
                                Me.FieldValue_ListBoxExportValues.SelectedItems.Add(v)
                            Next
                            For Each v As Integer In Me.FieldValue_ListBoxExportValues.SelectedIndices
                                Me.FieldValue_ListBoxDisplayNames.SelectedItems.Add(fld.getFieldDisplayNames(v))
                            Next
                        End If
                    ElseIf fld.getFieldDisplayNames.Length > 0 Then
                        For Each dn As String In fld.getFieldDisplayNames.ToArray
                            Me.FieldValue_ListBoxDisplayNames.Items.Add(dn)
                            Me.FieldValue_ListBoxExportValues.Items.Add(dn)
                        Next
                        If (fld.getFieldValues.Length > 0) Then
                            For Each v As String In fld.getFieldValues()
                                Me.FieldValue_ListBoxExportValues.SelectedItems.Add(v)
                            Next
                            For Each v As Integer In Me.FieldValue_ListBoxExportValues.SelectedIndices
                                Me.FieldValue_ListBoxDisplayNames.SelectedItems.Add(fld.getFieldDisplayNames(v))
                            Next
                        End If
                    End If
                    Me.FieldValue_ListBoxExportValues.Enabled = True
                    Me.FieldValue_ListBoxDisplayNames.Enabled = True
                    FieldValue_ListBoxDisplayNames.Focus()
            End Select
        Catch ex As Exception
            If frm.debugMode Then Throw ex Else Err.Clear()
        End Try
    End Sub
    Public Sub New(ByRef frmMain1 As frmMain)
        Try
            InitializeComponent()
            frm = frmMain1
            LoadDialog(False)
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
    Private Sub btnFileGetPdfFromEditor_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileGetPdfFromEditor.Click
        ListFieldsDetails(False)
    End Sub
    Private Sub GroupBoxFile1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBoxFile1.Enter
    End Sub
    Private sfd As New FolderBrowserDialog
    Private Sub btnFileLoadPdfFileInPdfEditor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If DataGridView1.SelectedRows.Count <= 0 Then Return
    End Sub
    Private Sub DataGridView1_Scroll(ByVal sender As Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles DataGridView1.Scroll
    End Sub
    Private Sub ContextMenuStripFile1_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
    End Sub
    Private Sub OpenWithPDFEditorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If DataGridView1.SelectedRows.Count <= 0 Then Return
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
    Private Sub OpenWithToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
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
    Private Sub NewFolderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        GroupBoxFile1.Visible = False
    End Sub
    Private Sub NewFolderToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        GroupBoxFile1.Visible = False
    End Sub
    Private Sub DataGridView1_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SizeChanged
    End Sub
    Private Sub RenameFolderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        GroupBoxFile1.Visible = False
    End Sub
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
    Dim fieldNames As New List(Of String)
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
    Private Sub btnFTPOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
    End Sub
    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Tab Then
            Try
            Catch ex As Exception
                Err.Clear()
                DataGridView1.Rows(0).Selected = True
            End Try
        End If
    End Sub
    Private Sub FieldValue_ListBoxDisplayNames_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FieldValue_ListBoxDisplayNames.KeyDown
        tabPressed(e)
    End Sub
    Private Sub FieldValue_ListBoxDisplayNames_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FieldValue_ListBoxDisplayNames.SelectedIndexChanged
        If Not FieldValue_ListBoxDisplayNames.Enabled Then Return
        If Not FieldValue_ListBoxDisplayNames.Visible Then Return
        If String.IsNullOrEmpty(lblFieldName.Text & "") Then Return
        If FieldValue_ListBoxDisplayNames.SelectedIndices.Count >= 0 Then
            FieldValue_ListBoxExportValues.Enabled = False
            FieldValue_ListBoxExportValues.SelectedItems.Clear()
            Dim strVals As New List(Of String)
            For i As Integer = 0 To FieldValue_ListBoxDisplayNames.Items.Count - 1
                If FieldValue_ListBoxDisplayNames.SelectedIndices.Contains(i) Then
                    FieldValue_ListBoxExportValues.SelectedItems.Add(FieldValue_ListBoxExportValues.Items(i))
                    strVals.Add(FieldValue_ListBoxExportValues.Items(i).ToString)
                End If
            Next
            FieldValue_ListBoxExportValues.Enabled = True
            If strVals.Count > 0 Then
                fieldsItem(lblFieldName.Text).setFieldValues(strVals.ToArray())
            Else
                fieldsItem(lblFieldName.Text).setFieldValues(New String() {})
            End If
        End If
    End Sub
    Private Sub FieldValue_ListBoxExportValues_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FieldValue_ListBoxExportValues.SelectedIndexChanged
        If Not FieldValue_ListBoxExportValues.Enabled Then Return
        If Not FieldValue_ListBoxDisplayNames.Visible Then Return
        If String.IsNullOrEmpty(lblFieldName.Text & "") Then Return
        Try
            If FieldValue_ListBoxExportValues.SelectedIndices.Count >= 0 Then
                FieldValue_ListBoxDisplayNames.Enabled = False
                FieldValue_ListBoxDisplayNames.SelectedItems.Clear()
                For i As Integer = 0 To FieldValue_ListBoxExportValues.Items.Count - 1
                    If FieldValue_ListBoxExportValues.SelectedIndices.Contains(i) Then
                        FieldValue_ListBoxDisplayNames.SelectedItems.Add(FieldValue_ListBoxDisplayNames.Items(i))
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        Finally
            FieldValue_ListBoxDisplayNames.Enabled = True
        End Try
    End Sub
    Private Sub FieldValue_ComboBoxValues_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FieldValue_ComboBoxValues.SelectedIndexChanged
        If String.IsNullOrEmpty(lblFieldName.Text & "") Then Return
        Try
            FieldValue_ComboBoxDisplayNames.SelectedIndex = FieldValue_ComboBoxValues.SelectedIndex
            fieldsItem(lblFieldName.Text).FieldValue = FieldValue_ComboBoxValues.Items(FieldValue_ComboBoxValues.SelectedIndex).ToString & ""
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub FieldValue_ComboBoxDisplayNames_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FieldValue_ComboBoxDisplayNames.KeyDown
        tabPressed(e)
    End Sub
    Private Sub FieldValue_ComboBoxDisplayNames_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FieldValue_ComboBoxDisplayNames.SelectedIndexChanged
        If String.IsNullOrEmpty(lblFieldName.Text & "") Then Return
        Try
            FieldValue_ComboBoxValues.SelectedIndex = FieldValue_ComboBoxDisplayNames.SelectedIndex
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public Function iText_GetField_V(ByVal fldName As String) As String
        If Not frm.iTextFieldItemPdfDictionary(fldName).Get(PdfName.V) Is Nothing Then
            Dim nm As PdfName = frm.iTextFieldItemPdfDictionary(fldName).GetAsName(PdfName.V)
            Return nm.ToString.TrimStart("\"c).TrimStart("/"c).ToString & ""
        End If
        Return "Yes"
    End Function
    Private Sub FieldValue_Checkbox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FieldValue_Checkbox.CheckedChanged
        If String.IsNullOrEmpty(lblFieldName.Text & "") Then Return
        Try
            If CBool(FieldValue_Checkbox.Checked) Then
                fieldsItem(lblFieldName.Text).FieldValue = iText_GetField_V(lblFieldName.Text) & ""
            Else
                fieldsItem(lblFieldName.Text).FieldValue = "Off"
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub FieldValue_TextBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FieldValue_TextBox.KeyDown
        tabPressed(e)
    End Sub
    Private Sub FieldValue_TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FieldValue_TextBox.TextChanged
        If String.IsNullOrEmpty(lblFieldName.Text & "") Then Return
        Try
            fieldsItem(lblFieldName.Text).FieldValue = FieldValue_TextBox.Text & ""
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub FieldValue_Checkbox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FieldValue_Checkbox.KeyDown
        tabPressed(e)
    End Sub
    Sub tabPressed(ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Tab Then
            Dim shifted As Boolean = e.Shift
            DataGridView1.Select()
            DataGridView1.Focus()
            e.SuppressKeyPress = True
            Try
                Dim cntVisible As Integer = 0
                Dim selIndex As Integer = 0
                If DataGridView1.SelectedRows(0).Cells(0).RowIndex <= 0 And shifted Then
                    selIndex = DataGridView1.RowCount - 1
                Else
                    For i As Integer = 0 To DataGridView1.RowCount - 1
                        If DataGridView1.Rows(i).Visible Then cntVisible += 1
                        If i = DataGridView1.SelectedRows(0).Cells(0).RowIndex + CInt(IIf(shifted, -1, 1)) Then
                            selIndex = i
                            Exit For
                        End If
                    Next
                End If
                If selIndex >= DataGridView1.RowCount Then
                    selIndex = 0
                ElseIf selIndex < 0 Then
                    selIndex = DataGridView1.RowCount - 1
                End If
                If selIndex < DataGridView1.RowCount And selIndex >= 0 Then
                    DataGridView1.Rows(selIndex).Cells(0).Selected = True
                    DataGridView1.Rows(selIndex).Selected = True
                Else
                    DataGridView1.Rows(selIndex).Cells(0).Selected = True
                    DataGridView1.Rows(0).Selected = True
                End If
            Catch ex As Exception
                Err.Clear()
                DataGridView1.Rows(0).Selected = True
            End Try
        End If
    End Sub
End Class
