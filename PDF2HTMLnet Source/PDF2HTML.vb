Imports iTextSharp.text.pdf
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Collections
Imports System.Collections.Generic
Imports System.Text
Imports System.Xml
Imports System.Xml.Serialization
Imports Microsoft.Win32.SafeHandles
Imports System.Runtime.InteropServices
Public Class PDF2HTMLnet
    Implements IDisposable
    ''' <summary>
    ''' PDF2HTMLnet - Created by NK-INC.COM (NK-INC.COM)
    ''' Copyright 2018 NK-INC.COM All Rights reserved.
    ''' PDF2HTMLnet utilizes iTextSharp technologies.
    ''' Website: www.nk-inc.com
    ''' </summary>
    Public _HTMLFields As Dictionary(Of String, HTMLFormField)
    'Public Shared HTMLFields As New System.Collections.Generic.List(Of HTMLFormField)
    Public _htmlform As String = ""
    Public ExcludeFormTag As Boolean = False
    Public Property HTMLFields() As Dictionary(Of String, HTMLFormField)
        Get
            Return _HTMLFields
        End Get
        Set(ByVal value As Dictionary(Of String, HTMLFormField))
            _HTMLFields = value
        End Set
    End Property
    Public Property HTMLField(ByVal strFieldName As String) As HTMLFormField
        Get
            If Not _HTMLFields(strFieldName) Is Nothing Then
                Return _HTMLFields(strFieldName)
            Else
                Return Nothing
            End If

        End Get
        Set(ByVal value As HTMLFormField)
            If _HTMLFields.ContainsKey(strFieldName) Then
                _HTMLFields(strFieldName) = value
            Else
                _HTMLFields.Add(strFieldName, value)
            End If
        End Set
    End Property
    Public Function OutPutHTMLToStream() As System.IO.Stream
        RefreshHTML()
        Dim bytesOut() As Byte = System.Text.Encoding.Default.GetBytes(_htmlform)
        Dim pageStream As New System.IO.MemoryStream(bytesOut)
        If pageStream.CanSeek And pageStream.Position > 0 Then
            pageStream.Position = 0
        End If
        Return pageStream
    End Function
    Public Function OutPutHTMLToString() As String
        RefreshHTML()
        Return _htmlform
    End Function
    Public Function OutPutHTMLToBuffer() As Byte()
        RefreshHTML()
        Return System.Text.Encoding.Default.GetBytes(_htmlform)
    End Function
    Public Property field_name(ByVal fName As String) As String
        Get
            If Not HTMLField(fName) Is Nothing Then
                Return HTMLFields(fName).name
            End If
            Return Nothing
        End Get
        Set(ByVal value As String)

            If Not HTMLField(fName) Is Nothing Then
                _HTMLFields(fName).name = value
                'fld.name = value
            Else
                HTMLFields(fName).name = value
            End If
        End Set
    End Property
    Public Property field_id(ByVal fName As String) As String
        Get
            If Not HTMLField(fName) Is Nothing Then
                Return HTMLFields(fName).id
            End If
            Return Nothing

        End Get
        Set(ByVal value As String)
            If Not HTMLField(fName) Is Nothing Then
                _HTMLFields(fName).id = value
                'fld.name = value
            Else
                HTMLFields(fName).id = value
            End If
        End Set
    End Property
    Public Property field_title(ByVal fName As String) As String
        Get
            If Not HTMLField(fName) Is Nothing Then
                Return HTMLFields(fName).title
            End If
            Return Nothing

        End Get
        Set(ByVal value As String)
            If Not HTMLField(fName) Is Nothing Then
                _HTMLFields(fName).title = value
                'fld.name = value
            Else
                HTMLFields(fName).title = value
            End If
        End Set
    End Property

    Public Function FieldIndex(ByVal fName As String) As Integer
        Dim intFld As Integer = -1
        For Each fld As HTMLFormField In HTMLFields.Values
            intFld += 1
            If fld.name.ToLower = fName.ToLower Then
                Return intFld
            End If
        Next
        Return -1
    End Function
    Public Property field_values(ByVal fName As String) As String()
        Get
            If Not HTMLField(fName) Is Nothing Then
                Return HTMLFields(fName).value
            End If
            Return Nothing

        End Get
        Set(ByVal value As String())
            If Not HTMLField(fName) Is Nothing Then
                _HTMLFields(fName).value = value
                'fld.name = value
            Else
                HTMLFields(fName).value = value
            End If
        End Set
    End Property
    Public Property field_opt(ByVal fName As String) As String()
        Get
            If Not HTMLField(fName) Is Nothing Then
                Return HTMLFields(fName).value
            End If
            Return Nothing

        End Get
        Set(ByVal value As String())
            If Not HTMLField(fName) Is Nothing Then
                _HTMLFields(fName).value = value
                _HTMLFields(fName).display = value
                'fld.name = value
            Else
                HTMLFields(fName).value = value
                HTMLFields(fName).display = value
            End If
        End Set
    End Property
    Public Property field_display(ByVal fName As String) As String()
        Get
            If Not HTMLField(fName) Is Nothing Then
                Return HTMLFields(fName).display
            End If
            Return Nothing

        End Get
        Set(ByVal value As String())
            If Not HTMLField(fName) Is Nothing Then
                _HTMLFields(fName).display = value
                'fld.name = value
            Else
                HTMLFields(fName).display = value
            End If
        End Set
    End Property
    Public Property field_list(ByVal fName As String) As String()
        Get
            If Not HTMLField(fName) Is Nothing Then
                Return HTMLFields(fName).value
            End If
            Return Nothing

        End Get
        Set(ByVal value As String())
            If Not HTMLField(fName) Is Nothing Then
                _HTMLFields(fName).value = value
                _HTMLFields(fName).display = value
                'fld.name = value
            Else
                HTMLFields(fName).value = value
                HTMLFields(fName).display = value
            End If
        End Set
    End Property
    Public Property field_value(ByVal fName As String) As String
        Get
            If Not HTMLField(fName) Is Nothing Then
                Return HTMLFields(fName).value(0)
            End If
            Return Nothing

        End Get
        Set(ByVal value As String)
            If Not HTMLField(fName) Is Nothing Then
                _HTMLFields(fName).value(0) = value
                'fld.name = value
            Else
                HTMLFields(fName).value(0) = value
            End If
        End Set
    End Property
    Public Property field_selected(ByVal fName As String) As String()
        Get
            If Not HTMLField(fName) Is Nothing Then
                Return HTMLFields(fName).selected
            End If
            Return Nothing

        End Get
        Set(ByVal value As String())
            If Not HTMLField(fName) Is Nothing Then
                _HTMLFields(fName).selected = value
                'fld.name = value
            Else
                HTMLFields(fName).selected = value
            End If
        End Set
    End Property
    Public Property field_type(ByVal fName As String) As HTMLFieldType
        Get
            If Not HTMLField(fName) Is Nothing Then
                Return HTMLFields(fName).type
            End If
            Return Nothing

        End Get
        Set(ByVal value As HTMLFieldType)
            If Not HTMLField(fName) Is Nothing Then
                _HTMLFields(fName).type = value
                'fld.name = value
            Else
                HTMLFields(fName).type = value
            End If
        End Set
    End Property
    Public Property field_style(ByVal fName As String) As String
        Get
            If Not HTMLField(fName) Is Nothing Then
                Return HTMLFields(fName).style
            End If
            Return Nothing

        End Get
        Set(ByVal value As String)
            If Not HTMLField(fName) Is Nothing Then
                _HTMLFields(fName).style = value
                'fld.name = value
            Else
                HTMLFields(fName).style = value
            End If
        End Set
    End Property
    Public Property field_cssClass(ByVal fName As String) As String
        Get
            If Not HTMLField(fName) Is Nothing Then
                Return HTMLFields(fName).cssClass
            End If
            Return Nothing

        End Get
        Set(ByVal value As String)
            If Not HTMLField(fName) Is Nothing Then
                _HTMLFields(fName).cssClass = value
                'fld.name = value
            Else
                HTMLFields(fName).cssClass = value
            End If
        End Set
    End Property
    Public WriteOnly Property field_cssClass_append(ByVal fName As String) As String
        Set(ByVal value As String)
            If Not HTMLField(fName) Is Nothing Then
                _HTMLFields(fName).cssClass = _HTMLFields(fName).cssClass & " " & value
                'fld.name = value
            Else
                HTMLFields(fName).cssClass = _HTMLFields(fName).cssClass & " " & value
            End If
        End Set
    End Property
    Public WriteOnly Property field_cssClass_join(ByVal fname As String, ByVal classes As Object()) As String
        Set(ByVal value As String)
            Dim intFld As Integer = -1
            For Each fld As HTMLFormField In HTMLFields.Values
                intFld += 1
                Select Case fld.GetType.Name.ToLower
                    Case GetType(String).Name
                        If fld.name.ToLower = fname.ToLower Then
                            'fld.cssClass = value
                            For Each cls As String In classes
                                _HTMLFields(fname).cssClass = _HTMLFields(fname).cssClass & " " & cls
                            Next
                            Return
                        End If
                    Case Else

                End Select
            Next
        End Set
    End Property
    Public Property field_width(ByVal fName As String) As Integer
        Get
            If Not HTMLField(fName) Is Nothing Then
                Return HTMLFields(fName).width
            End If
            Return Nothing

        End Get
        Set(ByVal value As Integer)
            If Not HTMLField(fName) Is Nothing Then
                _HTMLFields(fName).width = value
                'fld.name = value
            Else
                HTMLFields(fName).width = value
            End If
        End Set
    End Property
    Public Property field_height(ByVal fName As String) As Integer
        Get
            If Not HTMLField(fName) Is Nothing Then
                Return HTMLFields(fName).height
            End If
            Return Nothing

        End Get
        Set(ByVal value As Integer)
            If Not HTMLField(fName) Is Nothing Then
                _HTMLFields(fName).height = value
                'fld.name = value
            Else
                HTMLFields(fName).height = value
            End If
        End Set
    End Property
    Public Property field_cols(ByVal fName As String) As Integer
        Get
            If Not HTMLField(fName) Is Nothing Then
                Return HTMLFields(fName).cols
            End If
            Return Nothing

        End Get
        Set(ByVal value As Integer)
            If Not HTMLField(fName) Is Nothing Then
                _HTMLFields(fName).cols = value
                'fld.name = value
            Else
                HTMLFields(fName).cols = value
            End If
        End Set
    End Property
    Public Property field_rows(ByVal fName As String) As Integer
        Get
            If Not HTMLField(fName) Is Nothing Then
                Return HTMLFields(fName).rows
            End If
            Return Nothing

        End Get
        Set(ByVal value As Integer)
            If Not HTMLField(fName) Is Nothing Then
                _HTMLFields(fName).rows = value
                'fld.name = value
            Else
                HTMLFields(fName).rows = value
            End If
        End Set
    End Property
    Public Property field_group(ByVal fName As String) As String
        Get
            If Not HTMLField(fName) Is Nothing Then
                Return HTMLFields(fName).group
            End If
            Return Nothing

        End Get
        Set(ByVal value As String)
            If Not HTMLField(fName) Is Nothing Then
                _HTMLFields(fName).group = value
                'fld.name = value
            Else
                HTMLFields(fName).group = value
            End If
        End Set
    End Property
    Public Property field_multiple(ByVal fName As String) As Boolean
        Get
            If Not HTMLField(fName) Is Nothing Then
                Return HTMLFields(fName).multiple
            End If
            Return Nothing

        End Get
        Set(ByVal value As Boolean)
            If Not HTMLField(fName) Is Nothing Then
                _HTMLFields(fName).multiple = value
                'fld.name = value
            Else
                HTMLFields(fName).multiple = value
            End If
        End Set
    End Property
    Public Property field_size(ByVal fName As String) As Integer
        Get
            If Not HTMLField(fName) Is Nothing Then
                Return HTMLFields(fName).size
            End If
            Return Nothing

        End Get
        Set(ByVal value As Integer)
            If Not HTMLField(fName) Is Nothing Then
                _HTMLFields(fName).size = value
                'fld.name = value
            Else
                HTMLFields(fName).size = value
            End If
        End Set
    End Property
    Public Property field_imageBase64(ByVal fName As String) As String
        Get
            If Not HTMLField(fName) Is Nothing Then
                Return HTMLFields(fName).size
            End If
            Return Nothing

        End Get
        Set(ByVal value As String)
            If Not HTMLField(fName) Is Nothing Then
                _HTMLFields(fName).imageBase64 = value
                _HTMLFields(fName).imageBytes = Me.Base64ToBytes(value)
                'fld.name = value
            Else
                HTMLFields(fName).imageBase64 = value
                HTMLFields(fName).imageBytes = Me.Base64ToBytes(value)
            End If

        End Set

    End Property
    Public Property field_imageBytes(ByVal fName As String) As Byte()
        Get
            If Not HTMLField(fName) Is Nothing Then
                Return HTMLFields(fName).imageBytes
            End If
            Return Nothing

        End Get
        Set(ByVal value As Byte())
            If Not HTMLField(fName) Is Nothing Then
                _HTMLFields(fName).imageBase64 = Me.BytesToBase64(value)
                _HTMLFields(fName).imageBytes = value
                'fld.name = value
            Else
                HTMLFields(fName).imageBase64 = Me.BytesToBase64(value)
                HTMLFields(fName).imageBytes = value
            End If

        End Set
    End Property
    Public ReadOnly Property FieldCount() As Integer
        Get
            'Return HTMLFields.Count
            Return HTMLFields.Count
        End Get
    End Property
    Enum HTMLFieldType
        button = 1
        checkbox = 2
        file = 3
        hidden = 4
        image = 5
        password = 6
        radio = 7
        reset = 8
        submit = 9
        text = 10
        textarea = 11
        combo = 12
        list = 13
    End Enum
    Private _pdfForm As String = ""
    Private _fName As String = "html2PDFForm"
    Private _fAction As String = ""
    Private _fTarget As String = "_self"
    Private _fRunAt As String = "server"
    Private _fOnSubmit As String = ""
    Private _fMethod As String = "post"
    Public _fFormCSSClass As String = ""
    Public _fFormCSSStyle As String = ""
    Public _fTableCSSClass As String = ""
    Public _fTableCSSStyle As String = ""
    Private Shared _fShowTitles As Boolean = False
    Private Shared _fTitleSpaceStrings() As String = New String() {}
    Public _LockFormFields As Boolean = False
    Public Property ShowTitles() As Boolean
        Get
            Return _fShowTitles
        End Get
        Set(ByVal value As Boolean)
            _fShowTitles = value
        End Set
    End Property
    Public Property TitleReplaceStringsWithSpace() As String()
        Get
            Return _fTitleSpaceStrings
        End Get
        Set(ByVal value As String())
            _fTitleSpaceStrings = value
            If _fTitleSpaceStrings.Length > 0 Then
                For Each strTItle As String In _fTitleSpaceStrings
                    If Not String.IsNullOrEmpty(strTItle) Then
                        For Each _htmlField As HTMLFormField In _HTMLFields.Values
                            If Not String.IsNullOrEmpty(_htmlField.title & "") Then
                                _HTMLFields(_htmlField.name).title = _HTMLFields(_htmlField.name).title.Replace(strTItle, "")
                            Else
                                _HTMLFields(_htmlField.name).title = _HTMLFields(_htmlField.name).name
                                _HTMLFields(_htmlField.name).title = _HTMLFields(_htmlField.name).title.Replace(strTItle, "")
                            End If
                        Next
                        'For Each fld As HTMLFormField In HTMLFields.values
                        'If Not String.IsNullOrEmpty(fld.title & "") Then
                        '    fld.title = strTItle.Replace(strTItle, fld.title)
                        'Else
                        '    fld.title = fld.name
                        '    fld.title = strTItle.Replace(strTItle, fld.title)
                        'End If
                        'Next
                    End If
                Next
            End If
        End Set
    End Property
    'Public Sub setTitleReplaceStringsWithSpace(ByRef _htmlFields As Dictionary(Of String, HTMLFormField), ByVal replaceValues As String())

    '	_fTitleSpaceStrings = replaceValues
    '	If replaceValues Is Nothing Then
    '		Return
    '	ElseIf replaceValues.Length <= 0 Then
    '		Return
    '	End If

    '	If _fTitleSpaceStrings.Length > 0 Then
    '		For Each strTItle As String In _fTitleSpaceStrings
    '			If Not String.IsNullOrEmpty(strTItle) Then
    '				For Each _htmlField As HTMLFormField In _htmlFields.Values
    '					If Not String.IsNullOrEmpty(_htmlField.title & "") Then
    '						_htmlFields(_htmlField.name).title = _htmlFields(_htmlField.name).title.Replace(strTItle, "")
    '					Else
    '						_htmlFields(_htmlField.name).title = _htmlFields(_htmlField.name).name
    '						_htmlFields(_htmlField.name).title = _htmlFields(_htmlField.name).title.Replace(strTItle, "")
    '					End If
    '				Next
    '			End If
    '		Next
    '	End If

    '	Return

    'End Sub
    Public Sub setTitleReplaceStringsWithSpace(ByRef _htmlFields As Dictionary(Of String, HTMLFormField), ByVal replaceValues As String())

        _fTitleSpaceStrings = replaceValues
        If replaceValues Is Nothing Then
            Return
        ElseIf replaceValues.Length <= 0 Then
            Return
        End If
        If _fTitleSpaceStrings.Length > 0 Then
            For Each _htmlField As HTMLFormField In _htmlFields.Values
                For Each strTItle As String In _fTitleSpaceStrings
                    If Not String.IsNullOrEmpty(strTItle) Then
                        If Not String.IsNullOrEmpty(_htmlField.title & "") Then
                            _htmlFields(_htmlField.name).title = _htmlFields(_htmlField.name).title.Replace(strTItle, "")
                        Else
                            _htmlFields(_htmlField.name).title = _htmlFields(_htmlField.name).name
                            _htmlFields(_htmlField.name).title = _htmlFields(_htmlField.name).title.Replace(strTItle, "")
                        End If
                    End If
                Next
            Next

        End If
        Return
    End Sub
    Class HTMLFormField
        Private abcde As Integer
        Public name As String
        Public id As String
        Public title As String
        Public caption As String
        Public value As String()
        Public display As String()
        Public selected As String()
        Public type As HTMLFieldType
        Public style As String = "font-size:14px;"
        Public cssClass As String
        Public width As String = "95%"
        Public height As String
        Public cols As Integer
        Public rows As Integer
        Public group As String
        Public multiple As Boolean
        Public size As Integer
        Public imageBase64 As String
        Public imageBytes As Byte()
        Public imageMime As String
        Public pdfOwnerPassword As String = ""
        Public WriteOnly Property _TitleReplaceStringWithSpace() As String()
            Set(ByVal value As String())
                '_fTitleSpaceStrings = value
                If value.Length > 0 Then
                    For Each strTItle As String In value
                        If Not String.IsNullOrEmpty(strTItle) Then
                            'For intFld As Integer = 0 To HTMLFields.Length - 1
                            If Not String.IsNullOrEmpty(title & "") Then
                                title = strTItle.Replace(strTItle, title)
                            Else
                                title = name
                                title = strTItle.Replace(strTItle, title)
                            End If
                            'Next
                            'For Each fld As HTMLFormField In HTMLFields.values
                            'If Not String.IsNullOrEmpty(fld.title & "") Then
                            '    fld.title = strTItle.Replace(strTItle, fld.title)
                            'Else
                            '    fld.title = fld.name
                            '    fld.title = strTItle.Replace(strTItle, fld.title)
                            'End If
                            'Next
                        End If
                    Next
                End If
            End Set
        End Property
        '     Public Property field_name() As String
        'Get
        '    Return name

        'End Get
        'Set(ByVal value As String)
        '    name = value
        'End Set
        '     End Property

        '     Public Property field_id() As String
        'Get
        '    Return id

        'End Get
        'Set(ByVal value As String)
        '    id = value

        'End Set
        '     End Property
        '     Public Property field_title() As String
        'Get
        '    Return title
        'End Get
        'Set(ByVal value As String)
        '    title = value
        'End Set
        '     End Property
        '     Public Property field_display() As String()
        'Get
        '    Return display
        'End Get
        'Set(ByVal value As String())
        '    display = value
        'End Set
        '     End Property
        '     Public Property field_value() As String()
        'Get
        '    Return value
        'End Get
        'Set(ByVal val As String())
        '    value = val
        'End Set
        '     End Property
        '     Public Property field_selected() As String()
        'Get
        '    Return selected
        'End Get
        'Set(ByVal value As String())
        '    selected = value
        'End Set
        '     End Property
        '     Public Property field_type() As HTMLFieldType
        'Get
        '    Return type
        'End Get
        'Set(ByVal value As HTMLFieldType)
        '    type = value
        'End Set
        '     End Property
        '     Public Property field_style() As String
        'Get
        '    Return style
        'End Get
        'Set(ByVal value As String)
        '    style = value
        'End Set
        '     End Property
        '     Public Property field_cssClass() As String
        'Get
        '    Return cssClass
        'End Get
        'Set(ByVal value As String)
        '    cssClass = value
        'End Set
        '     End Property
        '     Public Property field_width() As Integer
        'Get
        '    Return width

        'End Get
        'Set(ByVal value As Integer)
        '    width = value

        'End Set
        '     End Property
        '     Public Property field_height() As Integer
        'Get
        '    Return height
        'End Get
        'Set(ByVal value As Integer)
        '    height = value

        'End Set
        '     End Property
        '     Public Property field_cols() As Integer
        'Get
        '    Return cols

        'End Get
        'Set(ByVal value As Integer)
        '    cols = value

        'End Set
        '     End Property
        '     Public Property field_rows() As Integer
        'Get
        '    Return rows

        'End Get
        'Set(ByVal value As Integer)
        '    rows = value

        'End Set
        '     End Property
        '     Public Property field_group() As String
        'Get
        '    Return group

        'End Get
        'Set(ByVal value As String)
        '    group = value

        'End Set
        '     End Property
        '     Public Property field_multiple() As Boolean
        'Get
        '    Return multiple
        'End Get
        'Set(ByVal value As Boolean)
        '    multiple = value

        'End Set
        '     End Property
        '     Public Property field_size() As Integer
        'Get
        '    Return size
        'End Get
        'Set(ByVal value As Integer)
        '    size = value

        'End Set
        '     End Property
        '     Public Property field_imageBase64() As String
        'Get
        '    Return imageBase64
        'End Get
        'Set(ByVal value As String)
        '    imageBase64 = value

        'End Set
        '     End Property
        '     Public Property field_imageBytes() As Byte()
        'Get
        '    Return imageBytes
        'End Get
        'Set(ByVal value As Byte())
        '    imageBytes = value

        'End Set
        '     End Property
        '     
        Public Property _name() As String
            Get
                Return name
            End Get
            Set(ByVal value As String)
                name = value
                If Not String.IsNullOrEmpty(name & "") And _fShowTitles = True Then
                    If _fTitleSpaceStrings.Length > 0 Then

                    End If
                End If
            End Set
        End Property
        Public Sub New(ByVal fname As String, ByVal ftype As HTMLFieldType, ByVal fvalue As String(), ByVal fdisplay As String(), ByVal fsize As Integer)
            name = fname
            id = fname
            type = ftype
            value = fvalue
            display = fdisplay
            size = fsize
        End Sub
        Public Sub New(ByVal fname As String, ByVal ftype As HTMLFieldType, ByVal fvalue As String(), ByVal fdisplay As String(), ByVal fselected As String(), ByVal fsize As Integer)
            name = fname
            id = fname
            type = ftype
            value = fvalue
            display = fdisplay
            size = fsize
            selected = fselected
        End Sub
        Public Sub New(ByVal fname As String, ByVal ftype As HTMLFieldType, ByVal fvalue As String(), ByVal fsize As Integer)
            name = fname
            id = fname
            type = ftype
            value = fvalue
            size = fsize
        End Sub
        Public Sub New(ByVal fname As String, ByVal ftype As HTMLFieldType, ByVal fvalue As String())
            name = fname
            id = fname
            type = ftype
            value = fvalue
        End Sub
        Public Sub New(ByVal fname As String, ByVal ftype As HTMLFieldType, ByVal fvalue64 As Char())
            name = fname
            id = fname
            type = ftype
            value = New String() {fvalue64.ToString}
        End Sub
        ''' <summary>
        ''' Image Base64
        ''' </summary>
        ''' <param name="fname"></param>
        ''' <param name="ftype"></param>
        ''' <param name="fvalue64">Base64 Character Array</param>
        ''' <param name="mimeType"></param>
        ''' <param name="empty"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal fname As String, ByVal ftype As HTMLFieldType, ByVal fvalue64 As Char(), ByVal mimeType As String, ByVal empty As Boolean)
            name = fname
            id = fname
            type = ftype
            imageBase64 = Convert.ToString(fvalue64)
            imageMime = mimeType
        End Sub
        ''' <summary>
        ''' Image HREF
        ''' </summary>
        ''' <param name="fname"></param>
        ''' <param name="ftype"></param>
        ''' <param name="mimeType"></param>
        ''' <param name="fhref">HREF of Image</param>
        ''' <param name="empty"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal fname As String, ByVal ftype As HTMLFieldType, ByVal mimeType As String, ByVal fhref As String, ByVal empty As Boolean)
            name = fname
            id = fname
            type = ftype
            'value = New String() {fvalue64.ToString}
            'imageBase64 = fvalue
            value = New String() {fhref}
            imageMime = mimeType
        End Sub
        Public Sub New(ByVal fname As String, ByVal ftype As HTMLFieldType, ByVal fvalue As String(), ByVal fdisplay As String())
            name = fname
            id = fname
            type = ftype
            value = fvalue
            display = fdisplay
        End Sub
        Public Sub New(ByVal fname As String, ByVal ftype As HTMLFieldType, ByVal fvalue As String)
            name = fname
            id = fname
            type = ftype
            value = New String() {fvalue}
        End Sub
        Public Sub New(ByVal fname As String, ByVal ftype As HTMLFieldType, ByVal fvalue As String, ByVal fgroup As String)
            name = fname
            id = fname
            type = ftype
            value = New String() {fvalue}
            group = fgroup
        End Sub
        Public Sub ImageImportBase64(ByVal imgBuffer As Byte())
            type = HTMLFieldType.image
            value = New String() {System.Convert.ToBase64String(imgBuffer)}
        End Sub
    End Class

    Public Property formMethod() As String
        Get
            Return _fMethod
        End Get
        Set(ByVal value As String)
            _fMethod = value
        End Set
    End Property
    Public pdfOwnerPassword As String = ""
    Public Sub New(ByVal pdfFormBuffer As Byte(), pwOwner As String, ByVal fName As String, ByVal fAction As String, ByVal fTarget As String)
        _htmlform = ""
        _pdfForm = ""
        _fName = fName
        _fAction = fAction
        _fTarget = fTarget
        _fRunAt = "server"
        _fOnSubmit = ""
        pdfOwnerPassword = pwOwner
        _htmlform = toHTMLForm(pdfFormBuffer, pwOwner, fName, fAction, fTarget)
    End Sub
    Public Sub New(ByVal pdfFormBuffer As Byte(), pwOwner As String, ByVal fName As String, ByVal fAction As String, ByVal fTarget As String, ByVal showTitles As Boolean, ByVal titleSpaceStrings As String())
        _htmlform = ""
        _pdfForm = ""
        _fName = fName
        _fAction = fAction
        _fTarget = fTarget
        _fRunAt = "server"
        _fOnSubmit = ""
        _fShowTitles = showTitles
        _fTitleSpaceStrings = titleSpaceStrings
        pdfOwnerPassword = pwOwner
        _htmlform = toHTMLForm(pdfFormBuffer, pwOwner, fName, fAction, fTarget, _fShowTitles, _fTitleSpaceStrings)
    End Sub
    Public Sub New()
        _htmlform = ""
        _pdfForm = ""
        _fName = ""
        _fAction = ""
        _fTarget = ""
        _fRunAt = "server"
        _fOnSubmit = ""
        _htmlform = ""
    End Sub
    Public Sub New(ByVal pdfFormPath As String, pwOwner As String, ByVal fName As String, ByVal fAction As String, ByVal fTarget As String)
        _htmlform = ""
        _pdfForm = pdfFormPath
        _fName = fName
        _fAction = fAction
        _fTarget = fTarget
        _fRunAt = "server"
        _fOnSubmit = ""
        pdfOwnerPassword = pwOwner
        _htmlform = toHTMLForm(pdfFormPath, pwOwner, fName, fAction, fTarget, "server")
    End Sub
    Public Sub New(ByVal pdfFormPath As String, pwOwner As String, ByVal fName As String, ByVal fAction As String, ByVal fTarget As String, ByVal showTitles As Boolean, ByVal titleSpaceStrings As String())
        'RIGHT HERE
        _htmlform = ""
        _pdfForm = pdfFormPath
        _fName = fName
        _fAction = fAction
        _fTarget = fTarget
        _fRunAt = "server"
        _fOnSubmit = ""
        _fShowTitles = showTitles
        _fTitleSpaceStrings = titleSpaceStrings
        pdfOwnerPassword = pwOwner
        _htmlform = toHTMLForm(pdfFormPath, pwOwner, fName, fAction, fTarget, showTitles, titleSpaceStrings)
    End Sub
    Public Sub New(ByVal pdfFormPath As String, pwOwner As String, ByVal fName As String, ByVal fAction As String, ByVal fOnSubmit As String, ByVal fTarget As String)
        _htmlform = ""
        _pdfForm = pdfFormPath
        _fName = fName
        _fAction = fAction
        _fTarget = fTarget
        _fRunAt = "server"
        _fOnSubmit = ""
        pdfOwnerPassword = pwOwner
        _htmlform = toHTMLForm(pdfFormPath, pwOwner, fName, fAction, fTarget, "server", fOnSubmit, True)
    End Sub
    Public Sub New(ByVal pdfFormPath As String, pwOwner As String, ByVal fName As String, ByVal fAction As String, ByVal fOnSubmit As String, ByVal fTarget As String, ByVal fRunAt As String)
        _htmlform = ""
        _pdfForm = pdfFormPath
        _fName = fName
        _fAction = fAction
        _fTarget = fTarget
        _fRunAt = "server"
        _fOnSubmit = ""
        pdfOwnerPassword = pwOwner
        _htmlform = toHTMLForm(pdfFormPath, pwOwner, fName, fAction, fTarget, fRunAt, fOnSubmit, True)
    End Sub
    Public Sub RefreshHTML()
        If _fShowTitles Then
            '_htmlform = toHTMLForm(_pdfForm, _fName, _fAction, _fTarget, _fRunAt, _fOnSubmit, True, _fTitleSpaceStrings)
            _htmlform = toHTMLForm(_pdfForm, pdfOwnerPassword, _fAction, _fTarget, _fRunAt, _fOnSubmit, False, _fShowTitles, _fTitleSpaceStrings)
        Else
            ShowTitles = _fShowTitles
            _htmlform = toHTMLForm(_pdfForm, pdfOwnerPassword, _fName, _fAction, _fTarget, _fRunAt, _fOnSubmit, False)
        End If
        '_htmlform = toHTMLForm(pdfFormPath, fName, fAction, fTarget, fRunAt, fOnSubmit)
    End Sub
    Private Function toHTMLForm(ByVal pdfForm As String, pdfownerPassword As String, ByVal fName As String, ByVal fAction As String, ByVal fTarget As String)
        Dim fRunat As String = "server"
        If fRunat = "client" Then
            fRunat = ""
        End If
        _pdfForm = pdfForm
        _fName = fName
        _fAction = fAction
        _fTarget = fTarget
        _fRunAt = "server"
        _fOnSubmit = ""
        HTMLFields = LoadPDFFormFields(_pdfForm, pdfownerPassword & "")
        If Not _fTitleSpaceStrings Is Nothing Then
            If Not _fTitleSpaceStrings.Length <= 0 Then
                setTitleReplaceStringsWithSpace(HTMLFields, _fTitleSpaceStrings)
            End If
        End If
        Dim strForm As New StringBuilder("")
        strForm.Append("<form name='" & _fName & "' id='" & _fName & "' action='" & _fAction & "' target='" & _fTarget & "' " & IIf(String.IsNullOrEmpty(_fRunAt), "", "runat='" & _fRunAt & "' ") & IIf(String.IsNullOrEmpty(_fMethod), "", "method='" & _fMethod & "' ") & IIf(Not String.IsNullOrEmpty(_fFormCSSClass & ""), " class='" & _fFormCSSClass & "'", "") & IIf(Not String.IsNullOrEmpty(_fFormCSSStyle & ""), " style='" & _fFormCSSStyle & "'", "") & " enctype=""multipart/form-data"">")
        strForm.Append(toHTMLFields("", "<br/>"))
        strForm.Append("</form>")
        Return strForm.ToString()
    End Function
    Private Function toHTMLForm(ByVal pdfForm As String, pdfownerPassword As String, ByVal fName As String, ByVal fAction As String, ByVal fTarget As String, ByVal fShowTitles As String, ByVal fTitleReplaceStrings() As String)
        Dim fRunat As String = "server"
        If fRunat = "client" Then
            fRunat = ""
        End If
        _pdfForm = pdfForm
        _fName = fName
        _fAction = fAction
        _fTarget = fTarget
        _fRunAt = "server"
        _fOnSubmit = ""
        _fTitleSpaceStrings = fTitleReplaceStrings
        HTMLFields = LoadPDFFormFields(_pdfForm, pdfownerPassword & "")
        If Not _fTitleSpaceStrings Is Nothing Then
            If Not _fTitleSpaceStrings.Length <= 0 Then
                setTitleReplaceStringsWithSpace(HTMLFields, _fTitleSpaceStrings)
            End If
        End If
        Dim strForm As New StringBuilder("")
        strForm.Append("<form name='" & _fName & "' id='" & _fName & "' action='" & _fAction & "' target='" & _fTarget & "' " & IIf(String.IsNullOrEmpty(_fRunAt), "", "runat='" & _fRunAt & "' ") & IIf(String.IsNullOrEmpty(_fMethod), "", "method='" & _fMethod & "' ") & IIf(Not String.IsNullOrEmpty(_fFormCSSClass & ""), " class='" & _fFormCSSClass & "'", "") & IIf(Not String.IsNullOrEmpty(_fFormCSSStyle & ""), " style='" & _fFormCSSStyle & "'", "") & " enctype=""multipart/form-data"">")
        strForm.Append(toHTMLFields(fShowTitles, fTitleReplaceStrings, "", "<br/>"))
        strForm.Append("</form>")
        Return strForm.ToString()
    End Function
    Private Function toHTMLForm(ByVal pdfForm As Byte(), pdfownerPassword As String, ByVal fName As String, ByVal fAction As String, ByVal fTarget As String)
        Dim fRunat As String = "server"
        If fRunat = "client" Then
            fRunat = ""
        End If
        _pdfForm = ""
        _fName = fName
        _fAction = fAction
        _fTarget = fTarget
        _fRunAt = "server"
        _fOnSubmit = ""
        HTMLFields = LoadPDFFormFields(pdfForm, pdfownerPassword)
        If Not _fTitleSpaceStrings Is Nothing Then
            If Not _fTitleSpaceStrings.Length <= 0 Then
                setTitleReplaceStringsWithSpace(HTMLFields, _fTitleSpaceStrings)
            End If
        End If
        Dim strForm As New StringBuilder("")
        If Not ExcludeFormTag Then
            strForm.Append("<form name='" & _fName & "' id='" & _fName & "' action='" & _fAction & "' target='" & _fTarget & "' " & IIf(String.IsNullOrEmpty(_fRunAt), "", "runat='" & _fRunAt & "' ") & IIf(String.IsNullOrEmpty(_fMethod), "", "method='" & _fMethod & "' ") & IIf(Not String.IsNullOrEmpty(_fFormCSSClass & ""), " class='" & _fFormCSSClass & "'", "") & IIf(Not String.IsNullOrEmpty(_fFormCSSStyle & ""), " style='" & _fFormCSSStyle & "'", "") & " enctype=""multipart/form-data"">")
        End If
        strForm.Append(toHTMLFields("", "<br/>"))
        If Not ExcludeFormTag Then
            strForm.Append("</form>")
        End If
        Return strForm.ToString()
    End Function
    Private Function toHTMLForm(ByVal pdfForm As Byte(), pdfownerPassword As String, ByVal fName As String, ByVal fAction As String, ByVal fTarget As String, ByVal fShowTitles As String, ByVal fTitleReplaceStrings() As String)
        Dim fRunat As String = "server"
        If fRunat = "client" Then
            fRunat = ""
        End If
        _pdfForm = ""
        _fName = fName
        _fAction = fAction
        _fTarget = fTarget
        _fRunAt = "server"
        _fOnSubmit = ""
        _fTitleSpaceStrings = fTitleReplaceStrings
        HTMLFields = LoadPDFFormFields(pdfForm, "")
        If Not _fTitleSpaceStrings Is Nothing Then
            If Not _fTitleSpaceStrings.Length <= 0 Then
                setTitleReplaceStringsWithSpace(HTMLFields, _fTitleSpaceStrings)
            End If
        End If
        Dim strForm As New StringBuilder("")
        If Not ExcludeFormTag Then
            strForm.Append("<form name='" & _fName & "' id='" & _fName & "' action='" & _fAction & "' target='" & _fTarget & "' " & IIf(String.IsNullOrEmpty(_fRunAt), "", "runat='" & _fRunAt & "' ") & IIf(String.IsNullOrEmpty(_fMethod), "", "method='" & _fMethod & "' ") & IIf(Not String.IsNullOrEmpty(_fFormCSSClass & ""), " class='" & _fFormCSSClass & "'", "") & IIf(Not String.IsNullOrEmpty(_fFormCSSStyle & ""), " style='" & _fFormCSSStyle & "'", "") & " enctype=""multipart/form-data"">")

        End If


        'strForm.Append(toHTMLFields("", "<br/>"))
        strForm.Append(toHTMLFields(fShowTitles, fTitleReplaceStrings, "", "<br/>"))
        If Not ExcludeFormTag Then

            strForm.Append("</form>")
        End If
        Return strForm.ToString()
    End Function

    Private Function strSubmit(Optional ByVal btnText As String = "Submit")
        Return "<input type=""submit"" name=""btnSubmit"" id=""btnSubmit"" value=""" & IIf(String.IsNullOrEmpty(btnText & ""), "Submit", btnText) & """ />"
    End Function
    Private Function strReset(Optional ByVal btnText As String = "Reset")
        Return "<input type=""reset"" name=""btnReset"" id=""btnReset"" value=""" & IIf(String.IsNullOrEmpty(btnText & ""), "Reset", btnText) & """ />"
    End Function
    Private Function toHTMLForm(ByVal pdfForm As String, pdfownerPassword As String, ByVal fName As String, ByVal fAction As String, ByVal fTarget As String, ByVal fRunat As String, ByVal fOnSubmit As String, ByVal fShowTitles As String, ByVal fTitleReplaceStrings As String())
        If fRunat = "client" Then
            fRunat = ""
        End If
        _pdfForm = pdfForm
        _fName = fName
        _fAction = fAction
        _fTarget = fTarget
        _fRunAt = fRunat
        _fOnSubmit = fOnSubmit
        _fTitleSpaceStrings = fTitleReplaceStrings
        HTMLFields = LoadPDFFormFields(_pdfForm, pdfownerPassword & "")
        If Not _fTitleSpaceStrings Is Nothing Then
            If Not _fTitleSpaceStrings.Length <= 0 Then
                setTitleReplaceStringsWithSpace(HTMLFields, _fTitleSpaceStrings)
            End If
        End If
        Dim strForm As New StringBuilder("")
        If Not ExcludeFormTag Then
            strForm.Append("<form name='" & _fName & "' id='" & _fName & "' action='" & _fAction & "' target='" & _fTarget & "' " & IIf(String.IsNullOrEmpty(_fRunAt), "", "runat='" & _fRunAt & "' ") & IIf(String.IsNullOrEmpty(_fOnSubmit), "", "onsubmit='" & _fOnSubmit & "' ") & IIf(String.IsNullOrEmpty(_fMethod), "", "method='" & _fMethod & "' ") & " >")
        End If

        If fShowTitles Then
            strForm.Append(toHTMLFields(fShowTitles, fTitleReplaceStrings, "", "", "", ""))
        Else
            strForm.Append(toHTMLFields("", "<br/>"))
        End If
        If Not ExcludeFormTag Then
            strForm.Append("</form>")
        End If

        Return strForm.ToString()
    End Function
    Private Function toHTMLForm(ByVal pdfForm As String, pdfownerPassword As String, ByVal fName As String, ByVal fAction As String, ByVal fTarget As String, ByVal fRunat As String, ByVal fOnSubmit As String, ByVal loadForm As Boolean)
        If fRunat = "client" Then
            fRunat = ""
        End If
        _pdfForm = pdfForm
        _fName = fName
        _fAction = fAction
        _fTarget = fTarget
        _fRunAt = fRunat
        _fOnSubmit = fOnSubmit
        If loadForm Then
            HTMLFields = LoadPDFFormFields(_pdfForm, pdfownerPassword & "")
        End If
        If Not _fTitleSpaceStrings Is Nothing Then
            If Not _fTitleSpaceStrings.Length <= 0 Then
                setTitleReplaceStringsWithSpace(HTMLFields, _fTitleSpaceStrings)
            End If
        End If
        Dim strForm As New StringBuilder("")
        If Not ExcludeFormTag Then
            strForm.Append("<form name='" & _fName & "' id='" & _fName & "' action='" & _fAction & "' target='" & _fTarget & "' " & IIf(String.IsNullOrEmpty(_fRunAt), "", "runat='" & _fRunAt & "' ") & IIf(String.IsNullOrEmpty(_fOnSubmit), "", "onsubmit='" & _fOnSubmit & "' ") & IIf(String.IsNullOrEmpty(_fMethod), "", "method='" & _fMethod & "' ") & " >")

        End If
        strForm.Append(toHTMLFields("", "<br/>"))

        If Not ExcludeFormTag Then
            strForm.Append("</form>")
        End If

        Return strForm.ToString()
    End Function
    Private Function toHTMLForm(ByVal pdfForm As String, pdfownerPassword As String, ByVal fName As String, ByVal fAction As String, ByVal fTarget As String, ByVal fRunat As String, ByVal fOnSubmit As String, ByVal loadForm As Boolean, ByVal showTitles As Boolean, ByVal TitleReplaceStrings() As String)
        If fRunat = "client" Then
            fRunat = ""
        End If
        _fTitleSpaceStrings = TitleReplaceStrings
        _fShowTitles = showTitles
        _pdfForm = pdfForm
        _fName = fName
        _fAction = fAction
        _fTarget = fTarget
        _fRunAt = fRunat
        _fOnSubmit = fOnSubmit
        If loadForm Then
            HTMLFields = LoadPDFFormFields(_pdfForm, pdfownerPassword & "")
        End If
        If Not _fTitleSpaceStrings Is Nothing Then
            If Not _fTitleSpaceStrings.Length <= 0 Then
                setTitleReplaceStringsWithSpace(HTMLFields, _fTitleSpaceStrings)
            End If
        End If
        Dim strForm As New StringBuilder("")
        If Not ExcludeFormTag Then
            strForm.Append("<form name='" & _fName & "' id='" & _fName & "' action='" & _fAction & "' target='" & _fTarget & "' " & IIf(String.IsNullOrEmpty(_fRunAt), "", "runat='" & _fRunAt & "' ") & IIf(String.IsNullOrEmpty(_fOnSubmit), "", "onsubmit='" & _fOnSubmit & "' ") & IIf(String.IsNullOrEmpty(_fMethod), "", "method='" & _fMethod & "' ") & " >")

        End If
        strForm.Append(toHTMLFields(showTitles, TitleReplaceStrings))
        If Not ExcludeFormTag Then
            strForm.Append("</form>")

        End If
        Return strForm.ToString()
    End Function
    Private Function toHTMLForm(ByVal pdfForm As String, pdfownerPassword As String, ByVal fName As String, ByVal fAction As String, ByVal fTarget As String, ByVal fRunat As String)
        If fRunat = "client" Then
            fRunat = ""
        End If
        _pdfForm = pdfForm
        _fName = fName
        _fAction = fAction
        _fTarget = fTarget
        _fRunAt = fRunat
        _fOnSubmit = ""
        HTMLFields = LoadPDFFormFields(_pdfForm, pdfownerPassword & "")
        If Not _fTitleSpaceStrings Is Nothing Then
            If Not _fTitleSpaceStrings.Length <= 0 Then
                setTitleReplaceStringsWithSpace(HTMLFields, _fTitleSpaceStrings)
            End If
        End If
        Dim strForm As New StringBuilder("")
        If Not ExcludeFormTag Then
            strForm.Append("<form name='" & _fName & "' id='" & _fName & "' action='" & _fAction & "' target='" & _fTarget & "' " & IIf(String.IsNullOrEmpty(_fRunAt), "", "runat='" & _fRunAt & "' ") & IIf(String.IsNullOrEmpty(_fMethod), "", "method='" & _fMethod & "' ") & IIf(Not String.IsNullOrEmpty(_fFormCSSClass & ""), " class='" & _fFormCSSClass & "'", "") & IIf(Not String.IsNullOrEmpty(_fFormCSSStyle & ""), " style='" & _fFormCSSStyle & "'", "") & " enctype=""multipart/form-data"">")

        End If
        strForm.Append(toHTMLFields("", "<br/>"))
        If Not ExcludeFormTag Then
            strForm.Append("</form>")

        End If
        Return strForm.ToString()
    End Function
    Public Function toHTMLFields(Optional ByVal preFix As String = "", Optional ByVal suffix As String = "") As String
        Dim strHTML As New StringBuilder("")
        On Error Resume Next
        'For Each fld As HTMLFormField In HTMLFields.values.ToArray
        If Not _fTitleSpaceStrings Is Nothing Then
            If Not _fTitleSpaceStrings.Length <= 0 Then
                setTitleReplaceStringsWithSpace(HTMLFields, _fTitleSpaceStrings)
            End If
        End If
        For Each fld As HTMLFormField In HTMLFields.Values
            If Not String.IsNullOrEmpty(preFix) Then
                strHTML.AppendLine(preFix)
            End If
            Select Case fld.type
                Case HTMLFieldType.text
                    strHTML.AppendLine("<input type='text' name='" & fld.name.Replace("[", "_").Replace("]", "") & "' id='" & fld.name.Replace("[", "_").Replace("]", "") & "' value='" & fld.value(0) & "' ")
                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.cssClass), "", "class='" & fld.cssClass & "' "))
                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.style), "", "style='" & fld.style & "' "))
                    strHTML.Append(" />")
                Case HTMLFieldType.textarea
                    strHTML.AppendLine("<input type='text' name='" & fld.name.Replace("[", "_").Replace("]", "") & "' id='" & fld.name.Replace("[", "_").Replace("]", "") & "' value='" & fld.value(0) & "' ")
                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.cssClass), "", "class='" & fld.cssClass & "' "))
                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.style), "", "style='" & fld.style & "' "))
                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.rows), "", "rows='" & fld.rows & "' "))
                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.cols), "", "cols='" & fld.cols & "' "))
                    strHTML.Append(" />")
                Case HTMLFieldType.hidden
                    strHTML.AppendLine("<input type='hidden' name='" & fld.name.Replace("[", "_").Replace("]", "") & "' id='" & fld.name.Replace("[", "_").Replace("]", "") & "' value='" & fld.value(0) & "'" & " />")
                Case HTMLFieldType.checkbox
                    strHTML.AppendLine("<input type='checkbox' name='" & fld.name.Replace("[", "_").Replace("]", "") & "' id='" & fld.name.Replace("[", "_").Replace("]", "") & "' value='" & fld.value(0) & "' ")
                    strHTML.AppendLine(IIf(contains(fld.value, fld.selected), " CHECKED ", ""))
                    strHTML.AppendLine(" />")
                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), fld.name, fld.title))
                    strHTML.Append("")
                Case HTMLFieldType.radio
                    'strHTML.AppendLine("<input type='radio' name='" & fld.name.Replace("[", "_").Replace("]", "") & "' id='" & fld.name.Replace("[", "_").Replace("]", "") & "' value='" & fld.value(0) & "' />" & IIf(String.IsNullOrEmpty(fld.value(0)), fld.name, fld.value(0)) & "")
                    If fld.value.Length > 0 Then
                        For intValues As Integer = 0 To fld.value.Length - 1
                            strHTML.AppendLine("<input type='radio' name='" & fld.name.Replace("[", "_").Replace("]", "") & "' id='" & fld.name.Replace("[", "_").Replace("]", "") & "' value='" & fld.value(intValues) & "' ")
                            strHTML.AppendLine(IIf(contains(fld.value(intValues), fld.selected), " CHECKED ", ""))
                            strHTML.AppendLine(" />")
                            strHTML.Append(IIf(String.IsNullOrEmpty(fld.value(intValues)), fld.name, fld.value(intValues)) & "")
                        Next
                    End If
                Case HTMLFieldType.image
                    'strHTML.AppendLine("<input type='radio' name='" & fld.name.Replace("[", "_").Replace("]", "") & "' id='" & fld.name.Replace("[", "_").Replace("]", "") & "' value='" & fld.value(0) & "' />" & IIf(String.IsNullOrEmpty(fld.value(0)), fld.name, fld.value(0)) & "")
                    strHTML.AppendLine("<img ")
                    If Not fld.id Is Nothing Then
                        strHTML.Append("id='" & fld.id.Replace("[", "_").Replace("]", "") & "' ")
                    End If
                    If Not fld.name Is Nothing Then
                        strHTML.Append("name='" & fld.name.Replace("[", "_").Replace("]", "") & "' ")
                    End If
                    If Not fld.imageBase64 Is Nothing Then
                        If String.IsNullOrEmpty(fld.imageMime & "") Or fld.imageMime = "0" Then
                            fld.imageMime = "image/jpeg"
                        End If
                        strHTML.Append("src='data:" & fld.imageMime & ";base64," & fld.imageBase64 & "' ")
                    ElseIf Not fld.value(0) Is Nothing Then
                        strHTML.Append("src='" & fld.value(0) & "' ")
                    End If
                    If Not fld.width = Nothing Then
                        strHTML.Append("width='" & fld.width & "' ")
                    End If
                    If Not fld.height = Nothing Then
                        strHTML.Append("height='" & fld.height & "' ")
                    End If
                    If Not fld.style Is Nothing Then
                        strHTML.Append("style='" & fld.style & "' ")
                    End If
                    If Not fld.cssClass Is Nothing Then
                        strHTML.Append("class='" & fld.cssClass & "' ")
                    End If
                    If Not fld.title Is Nothing Then
                        strHTML.Append("title='" & fld.title & "' ")
                        strHTML.Append("alt='" & fld.title & "' ")
                    End If
                    strHTML.Append(" />")
                    'If sender.Attributes("onchange") Is Nothing Then
                    '    sender.Attributes.Add("onchange", "function() {document.getElementById(""form"").submit();};")
                    'End If
                    'strHTML.Append("<input type=""file"" id=""uploadfile__" & fld.id.Replace("[", "_").Replace("]", "") & """ name=""uploadfile__" & fld.name.Split(CStr("["))(0) & """ />")
                    'strHTML.Append("<input type=""hidden"" id=""filebase64__" & fld.id.Replace("[", "_").Replace("]", "") & """ name=""filebase64__" & fld.id.Replace("[", "_").Replace("]", "") & """ value="""" />")
                    'strHTML.Append("<script type=""text/javascript"">window.onload = new function() {try {var handleFileSelect = function(evt) {var files = evt.target.files;var file = files[0];if (files && file) {var reader = new FileReader();reader.onload = function(readerEvt) {var binaryString = readerEvt.target.result;document.getElementById('" & fld.id.Replace("[", "_").Replace("]", "") & "').setAttribute('src', 'data:image/jpeg;base64,' + btoa(binaryString));document.getElementById(""filebase64__" & fld.id.Replace("[", "_").Replace("]", "") & """).value = btoa(binaryString);};reader.readAsBinaryString(file);};};if (window.File && window.FileReader && window.FileList && window.Blob) {document.getElementById('uploadfile__" & fld.id.Replace("[", "_").Replace("]", "") & "').addEventListener('change', handleFileSelect, false);};} catch (e) { alert(e); };};</script>")

                    If _LockFormFields = False Then
                        strHTML.Append("<input type=""file"" id=""uploadfile__" & fld.id.Replace("[", "_").Replace("]", "") & """ name=""uploadfile__" & fld.id.Replace("[", "_").Replace("]", "") & """ />")
                        strHTML.Append("<input type=""hidden"" id=""filebase64__" & fld.id.Replace("[", "_").Replace("]", "") & """ name=""filebase64__" & fld.id.Replace("[", "_").Replace("]", "") & """ value="""" />")
                        strHTML.Append("<script type=""text/javascript"">window.onload = new function() {try {var handleFileSelect = function(evt) {var files = evt.target.files;var file = files[0];if (files && file) {var reader = new FileReader();reader.onload = function(readerEvt) {var binaryString = readerEvt.target.result;document.getElementById('" & fld.id.Replace("[", "_").Replace("]", "") & "').setAttribute('src', 'data:image/jpeg;base64,' + btoa(binaryString));document.getElementById(""filebase64__" & fld.id.Replace("[", "_").Replace("]", "") & """).value = btoa(binaryString);};reader.readAsBinaryString(file);};};if (window.File && window.FileReader && window.FileList && window.Blob) {document.getElementById('uploadfile__" & fld.id.Replace("[", "_").Replace("]", "") & "').addEventListener('change', handleFileSelect, false);};} catch (e) { alert(e); };};</script>")
                        strHTML.Append("<script type=""text/javascript"">")
                        strHTML.Append("window.onload = new function() {try {")
                        strHTML.Append("    var fldUpload=document.getElementById('uploadfile__" & fld.id.Replace("[", "_").Replace("]", "") & "');")
                        strHTML.Append("    function uploadfile__" & fld.id.Replace("[", "_").Replace("]", "") & "_onlick(){")
                        strHTML.Append("        fldUpload.style.display = 'block';")
                        strHTML.Append("        fldUpload.focus();")
                        strHTML.Append("        fldUpload.click();")
                        strHTML.Append("        fldUpload.style.display = 'none';")
                        strHTML.Append("    };")
                        strHTML.Append("    fldUpload.style.display = 'none';")
                        strHTML.Append("    document.getElementById('" & fld.id.Replace("[", "_").Replace("]", "") & "').onclick=function(){uploadfile__" & fld.id.Replace("[", "_").Replace("]", "") & "_onlick();};")
                        strHTML.Append("} catch (e) { alert('error:'+e.description); };};</script>")

                    End If
                Case HTMLFieldType.list
                    strHTML.AppendLine("<select name='" & fld.name.Replace("[", "_").Replace("]", "") & "' id='" & fld.name.Replace("[", "_").Replace("]", "") & "' " & IIf(fld.type = HTMLFieldType.list, IIf(fld.size > 1, "size='" & fld.size & "' ", "size='3' "), "") & IIf(fld.multiple = True, " MULTIPLE ", " ") & ">")
                    If fld.value.Length = fld.display.Length Then
                        For intVal As Integer = 0 To fld.value.Length - 1
                            If fld.selected Is Nothing Then
                                strHTML.AppendLine("<option value='" & fld.value(intVal) & "'>" & fld.display(intVal) & "</option>")
                            Else
                                If contains(fld.selected, fld.value(intVal)) Then
                                    strHTML.AppendLine("<option value='" & fld.value(intVal) & "' selected >" & fld.display(intVal) & "</option>")
                                Else
                                    strHTML.AppendLine("<option value='" & fld.value(intVal) & "'>" & fld.display(intVal) & "</option>")
                                End If
                            End If
                        Next
                    Else
                        If fld.value.Length > 0 Then
                            For Each fldvalue As String In fld.value
                                If fld.selected Is Nothing Then
                                    strHTML.AppendLine("<option value='" & fldvalue & "'>" & fldvalue & "</option>")
                                Else
                                    If contains(fld.selected, fldvalue) Then
                                        strHTML.AppendLine("<option value='" & fldvalue & "' selected >" & fldvalue & "</option>")
                                    Else
                                        strHTML.AppendLine("<option value='" & fldvalue & "'>" & fldvalue & "</option>")
                                    End If
                                End If
                            Next
                        ElseIf fld.display.Length > 0 Then
                            For Each fldvalue As String In fld.display
                                If fld.selected Is Nothing Then
                                    strHTML.AppendLine("<option value='" & fldvalue & "'>" & fldvalue & "</option>")
                                Else
                                    If contains(fld.selected, fldvalue) Then
                                        strHTML.AppendLine("<option value='" & fldvalue & "' selected >" & fldvalue & "</option>")
                                    Else
                                        strHTML.AppendLine("<option value='" & fldvalue & "'>" & fldvalue & "</option>")
                                    End If
                                End If


                            Next
                        End If
                    End If
                    strHTML.AppendLine("</select>")
                Case HTMLFieldType.combo
                    strHTML.AppendLine("<select name='" & fld.name.Replace("[", "_").Replace("]", "") & "' id='" & fld.name.Replace("[", "_").Replace("]", "") & "' " & IIf(fld.type = HTMLFieldType.list, IIf(fld.size > 1, "size='" & fld.size & "' ", "size='3' "), "") & ">")
                    If fld.value.Length = fld.display.Length Then
                        For intVal As Integer = 0 To fld.value.Length - 1
                            If fld.selected Is Nothing Then
                                strHTML.AppendLine("<option value='" & fld.value(intVal) & "'>" & fld.display(intVal) & "</option>")
                            Else
                                If contains(fld.selected, fld.value(intVal)) Then
                                    strHTML.AppendLine("<option value='" & fld.value(intVal) & "' selected >" & fld.display(intVal) & "</option>")
                                Else
                                    strHTML.AppendLine("<option value='" & fld.value(intVal) & "'>" & fld.display(intVal) & "</option>")
                                End If
                            End If
                        Next
                    Else
                        If fld.value.Length > 0 Then
                            For Each fldvalue As String In fld.value
                                If fld.selected Is Nothing Then
                                    strHTML.AppendLine("<option value='" & fldvalue & "'>" & fldvalue & "</option>")
                                Else
                                    If contains(fld.selected, fldvalue) Then
                                        strHTML.AppendLine("<option value='" & fldvalue & "' selected >" & fldvalue & "</option>")
                                    Else
                                        strHTML.AppendLine("<option value='" & fldvalue & "'>" & fldvalue & "</option>")
                                    End If
                                End If

                            Next
                        ElseIf fld.display.Length > 0 Then
                            For Each fldvalue As String In fld.display
                                If fld.selected Is Nothing Then
                                    strHTML.AppendLine("<option value='" & fldvalue & "'>" & fldvalue & "</option>")
                                Else
                                    If contains(fld.selected, fldvalue) Then
                                        strHTML.AppendLine("<option value='" & fldvalue & "' selected >" & fldvalue & "</option>")
                                    Else
                                        strHTML.AppendLine("<option value='" & fldvalue & "'>" & fldvalue & "</option>")
                                    End If
                                End If
                            Next
                        End If
                    End If
                    strHTML.AppendLine("</select>")
            End Select
            If Not String.IsNullOrEmpty(suffix) Then
                strHTML.AppendLine(suffix)
            End If

        Next

        ' SUBMIT BUTTON
        strHTML.AppendLine(preFix)
        strHTML.AppendLine(strSubmit)
        strHTML.AppendLine(suffix)

        ' RESET BUTTON
        strHTML.AppendLine(preFix)
        strHTML.AppendLine(strReset)
        strHTML.AppendLine(suffix)

        Return strHTML.ToString
    End Function
    Public Function toHTMLASPXFields(ByVal pageObj As System.Web.UI.Page, Optional ByVal preFix As String = "", Optional ByVal suffix As String = "") As String
        Dim strHTML As New StringBuilder("")
        On Error Resume Next
        'For Each fld As HTMLFormField In HTMLFields.values.ToArray
        If Not _fTitleSpaceStrings Is Nothing Then
            If Not _fTitleSpaceStrings.Length <= 0 Then
                setTitleReplaceStringsWithSpace(HTMLFields, _fTitleSpaceStrings)
            End If
        End If
        For Each fld As HTMLFormField In HTMLFields.Values
            If Not String.IsNullOrEmpty(preFix) Then
                strHTML.AppendLine(preFix)
            End If
            Select Case fld.type
                Case HTMLFieldType.text
                    Dim aspFld As New System.Web.UI.WebControls.TextBox
                    aspFld.ID = fld.name
                    aspFld.Text = fld.value(0)
                    If Not String.IsNullOrEmpty(fld.cssClass & "") Then
                        aspFld.CssClass = fld.cssClass
                    End If
                    If (String.IsNullOrEmpty(fld.style)) Then
                        aspFld.Style.Clear()
                        For Each css As String In fld.style.ToString.Split(";")
                            If Not String.IsNullOrEmpty(css) Then
                                aspFld.Style.Add(css.Split(":")(0) & "", css.Split(":")(1) & "")
                            End If
                        Next
                    End If
                    pageObj.Form.Controls.Add(aspFld)
                Case HTMLFieldType.textarea
                    Dim aspFld As New System.Web.UI.WebControls.TextBox
                    aspFld.ID = fld.name
                    aspFld.TextMode = Web.UI.WebControls.TextBoxMode.MultiLine
                    aspFld.Text = fld.value(0)
                    If Not String.IsNullOrEmpty(fld.cssClass & "") Then
                        aspFld.CssClass = fld.cssClass
                    End If
                    If (String.IsNullOrEmpty(fld.style)) Then
                        aspFld.Style.Clear()
                        For Each css As String In fld.style.ToString.Split(";")
                            If Not String.IsNullOrEmpty(css) Then
                                aspFld.Style.Add(css.Split(":")(0) & "", css.Split(":")(1) & "")
                            End If
                        Next
                    End If
                    pageObj.Form.Controls.Add(aspFld)
                Case HTMLFieldType.hidden
                    strHTML.AppendLine("<input type='hidden' name='" & fld.name.Replace("[", "_").Replace("]", "") & "' id='" & fld.name.Replace("[", "_").Replace("]", "") & "' value='" & fld.value(0) & "'" & " />")
                Case HTMLFieldType.checkbox
                    Dim aspFld As New System.Web.UI.WebControls.CheckBox
                    aspFld.ID = fld.name
                    aspFld.Text = fld.value(0)
                    If contains(fld.value, fld.selected) Then
                        aspFld.Checked = True
                    End If
                    If Not String.IsNullOrEmpty(fld.cssClass & "") Then
                        aspFld.CssClass = fld.cssClass
                    End If
                    If (String.IsNullOrEmpty(fld.style)) Then
                        aspFld.Style.Clear()
                        For Each css As String In fld.style.ToString.Split(";")
                            If Not String.IsNullOrEmpty(css) Then
                                aspFld.Style.Add(css.Split(":")(0) & "", css.Split(":")(1) & "")
                            End If
                        Next
                    End If
                    pageObj.Form.Controls.Add(aspFld)
                Case HTMLFieldType.radio
                    'strHTML.AppendLine("<input type='radio' name='" & fld.name.Replace("[", "_").Replace("]", "") & "' id='" & fld.name.Replace("[", "_").Replace("]", "") & "' value='" & fld.value(0) & "' />" & IIf(String.IsNullOrEmpty(fld.value(0)), fld.name, fld.value(0)) & "")
                    If fld.value.Length > 0 Then
                        For intValues As Integer = 0 To fld.value.Length - 1
                            Dim aspFld As New System.Web.UI.WebControls.RadioButton
                            aspFld.ID = fld.name
                            If String.IsNullOrEmpty(fld.value(intValues)) Then
                                aspFld.Text = fld.name
                            Else
                                aspFld.Text = fld.value(intValues)
                            End If
                            aspFld.Text = fld.value(intValues)
                            If contains(fld.value, fld.selected) Then
                                aspFld.Checked = True
                            End If
                            If Not String.IsNullOrEmpty(fld.cssClass & "") Then
                                aspFld.CssClass = fld.cssClass
                            End If
                            If (String.IsNullOrEmpty(fld.style)) Then
                                aspFld.Style.Clear()
                                For Each css As String In fld.style.ToString.Split(";")
                                    If Not String.IsNullOrEmpty(css) Then
                                        aspFld.Style.Add(css.Split(":")(0) & "", css.Split(":")(1) & "")
                                    End If
                                Next
                            End If
                            If String.IsNullOrEmpty(fld.value(intValues)) Then

                            End If
                            pageObj.Form.Controls.Add(aspFld)
                        Next
                    End If
                Case HTMLFieldType.image
                    'strHTML.AppendLine("<input type='radio' name='" & fld.name.Replace("[", "_").Replace("]", "") & "' id='" & fld.name.Replace("[", "_").Replace("]", "") & "' value='" & fld.value(0) & "' />" & IIf(String.IsNullOrEmpty(fld.value(0)), fld.name, fld.value(0)) & "")

                    Dim aspFld As New System.Web.UI.WebControls.Image
                    aspFld.ID = fld.name
                    If Not String.IsNullOrEmpty(fld.cssClass & "") Then
                        aspFld.CssClass = fld.cssClass
                    End If
                    If (String.IsNullOrEmpty(fld.style)) Then
                        aspFld.Style.Clear()
                        For Each css As String In fld.style.ToString.Split(";")
                            If Not String.IsNullOrEmpty(css) Then
                                aspFld.Style.Add(css.Split(":")(0) & "", css.Split(":")(1) & "")
                            End If
                        Next
                    End If
                    If Not fld.imageBase64 Is Nothing Then
                        aspFld.ImageUrl = ("data:" & fld.imageMime & ";base64," & fld.imageBase64 & "")
                    ElseIf Not fld.value(0) Is Nothing Then
                        aspFld.ImageUrl = fld.value(0)
                    End If
                    If Not fld.width = Nothing Then
                        aspFld.Width = fld.width
                    End If
                    If Not fld.height = Nothing Then
                        aspFld.Height = fld.height
                    End If
                    If Not fld.title Is Nothing Then
                        aspFld.AlternateText = fld.title
                    End If
                    pageObj.Form.Controls.Add(aspFld)
                Case HTMLFieldType.list
                    strHTML.AppendLine("<select name='" & fld.name.Replace("[", "_").Replace("]", "") & "' id='" & fld.name.Replace("[", "_").Replace("]", "") & "' " & IIf(fld.type = HTMLFieldType.list, IIf(fld.size > 1, "size='" & fld.size & "' ", "size='3' "), "") & IIf(fld.multiple = True, " MULTIPLE ", " ") & ">")
                    If fld.value.Length = fld.display.Length Then
                        For intVal As Integer = 0 To fld.value.Length - 1
                            If fld.selected Is Nothing Then
                                strHTML.AppendLine("<option value='" & fld.value(intVal) & "'>" & fld.display(intVal) & "</option>")
                            Else
                                If contains(fld.selected, fld.value(intVal)) Then
                                    strHTML.AppendLine("<option value='" & fld.value(intVal) & "' selected >" & fld.display(intVal) & "</option>")
                                Else
                                    strHTML.AppendLine("<option value='" & fld.value(intVal) & "'>" & fld.display(intVal) & "</option>")
                                End If
                            End If
                        Next
                    Else
                        If fld.value.Length > 0 Then
                            For Each fldvalue As String In fld.value
                                If fld.selected Is Nothing Then
                                    strHTML.AppendLine("<option value='" & fldvalue & "'>" & fldvalue & "</option>")
                                Else
                                    If contains(fld.selected, fldvalue) Then
                                        strHTML.AppendLine("<option value='" & fldvalue & "' selected >" & fldvalue & "</option>")
                                    Else
                                        strHTML.AppendLine("<option value='" & fldvalue & "'>" & fldvalue & "</option>")
                                    End If
                                End If
                            Next
                        ElseIf fld.display.Length > 0 Then
                            For Each fldvalue As String In fld.display
                                If fld.selected Is Nothing Then
                                    strHTML.AppendLine("<option value='" & fldvalue & "'>" & fldvalue & "</option>")
                                Else
                                    If contains(fld.selected, fldvalue) Then
                                        strHTML.AppendLine("<option value='" & fldvalue & "' selected >" & fldvalue & "</option>")
                                    Else
                                        strHTML.AppendLine("<option value='" & fldvalue & "'>" & fldvalue & "</option>")
                                    End If
                                End If


                            Next
                        End If
                    End If
                    strHTML.AppendLine("</select>")
                Case HTMLFieldType.combo
                    strHTML.AppendLine("<select name='" & fld.name.Replace("[", "_").Replace("]", "") & "' id='" & fld.name.Replace("[", "_").Replace("]", "") & "' " & IIf(fld.type = HTMLFieldType.list, IIf(fld.size > 1, "size='" & fld.size & "' ", "size='3' "), "") & ">")
                    If fld.value.Length = fld.display.Length Then
                        For intVal As Integer = 0 To fld.value.Length - 1
                            If fld.selected Is Nothing Then
                                strHTML.AppendLine("<option value='" & fld.value(intVal) & "'>" & fld.display(intVal) & "</option>")
                            Else
                                If contains(fld.selected, fld.value(intVal)) Then
                                    strHTML.AppendLine("<option value='" & fld.value(intVal) & "' selected >" & fld.display(intVal) & "</option>")
                                Else
                                    strHTML.AppendLine("<option value='" & fld.value(intVal) & "'>" & fld.display(intVal) & "</option>")
                                End If
                            End If
                        Next
                    Else
                        If fld.value.Length > 0 Then
                            For Each fldvalue As String In fld.value
                                If fld.selected Is Nothing Then
                                    strHTML.AppendLine("<option value='" & fldvalue & "'>" & fldvalue & "</option>")
                                Else
                                    If contains(fld.selected, fldvalue) Then
                                        strHTML.AppendLine("<option value='" & fldvalue & "' selected >" & fldvalue & "</option>")
                                    Else
                                        strHTML.AppendLine("<option value='" & fldvalue & "'>" & fldvalue & "</option>")
                                    End If
                                End If

                            Next
                        ElseIf fld.display.Length > 0 Then
                            For Each fldvalue As String In fld.display
                                If fld.selected Is Nothing Then
                                    strHTML.AppendLine("<option value='" & fldvalue & "'>" & fldvalue & "</option>")
                                Else
                                    If contains(fld.selected, fldvalue) Then
                                        strHTML.AppendLine("<option value='" & fldvalue & "' selected >" & fldvalue & "</option>")
                                    Else
                                        strHTML.AppendLine("<option value='" & fldvalue & "'>" & fldvalue & "</option>")
                                    End If
                                End If
                            Next
                        End If
                    End If
                    strHTML.AppendLine("</select>")
            End Select
            If Not String.IsNullOrEmpty(suffix) Then
                strHTML.AppendLine(suffix)
            End If

        Next

        ' SUBMIT BUTTON
        strHTML.AppendLine(preFix)
        strHTML.AppendLine(strSubmit)
        strHTML.AppendLine(suffix)

        ' RESET BUTTON
        strHTML.AppendLine(preFix)
        strHTML.AppendLine(strReset)
        strHTML.AppendLine(suffix)

        Return strHTML.ToString
    End Function
    Public Function toHTMLFields(ByVal parentControl As System.Web.UI.WebControls.WebControl, Optional ByVal preFix As String = "", Optional ByVal suffix As String = "") As String
        Dim strHTML As New StringBuilder("")
        On Error Resume Next
        'For Each fld As HTMLFormField In HTMLFields.values.ToArray
        If Not _fTitleSpaceStrings Is Nothing Then
            If Not _fTitleSpaceStrings.Length <= 0 Then
                setTitleReplaceStringsWithSpace(HTMLFields, _fTitleSpaceStrings)
            End If
        End If
        For Each fld As HTMLFormField In HTMLFields.Values
            If Not String.IsNullOrEmpty(preFix) Then
                strHTML.AppendLine(preFix)
            End If

            Select Case fld.type
                Case HTMLFieldType.text
                    Dim fldControl As New System.Web.UI.WebControls.TextBox()
                    If Not (String.IsNullOrEmpty(fld.name)) Then
                        fldControl.ID = fld.name
                        fldControl.Attributes.Add("name", fld.name)
                    End If
                    If Not (String.IsNullOrEmpty(fld.value(0))) Then
                        fldControl.Text = fld.value(0)
                    End If

                    If Not (String.IsNullOrEmpty(fld.cssClass)) Then
                        fldControl.CssClass = fld.cssClass
                    End If
                    parentControl.Controls.Add(fldControl)
                Case HTMLFieldType.textarea
                    Dim fldControl As New System.Web.UI.WebControls.TextBox()
                    If Not (String.IsNullOrEmpty(fld.name)) Then
                        fldControl.ID = fld.name
                        fldControl.Attributes.Add("name", fld.name)
                    End If
                    If Not (String.IsNullOrEmpty(fld.value(0))) Then
                        fldControl.Text = fld.value(0)
                    End If

                    If Not (String.IsNullOrEmpty(fld.cssClass)) Then
                        fldControl.CssClass = fld.cssClass
                    End If
                    If Not (String.IsNullOrEmpty(fld.rows)) Then
                        fldControl.Rows = fld.rows
                    End If
                    If Not (String.IsNullOrEmpty(fld.cols)) Then
                        fldControl.Columns = fld.cols
                    End If
                    fldControl.TextMode = Web.UI.WebControls.TextBoxMode.MultiLine
                    parentControl.Controls.Add(fldControl)
                Case HTMLFieldType.hidden
                    Dim fldControl As New System.Web.UI.WebControls.HiddenField
                    If Not (String.IsNullOrEmpty(fld.name)) Then
                        fldControl.ID = fld.name
                    End If
                    If Not (String.IsNullOrEmpty(fld.value(0))) Then
                        fldControl.Value = fld.value(0)
                    End If
                    parentControl.Controls.Add(fldControl)
                Case HTMLFieldType.checkbox
                    Dim fldControl As New System.Web.UI.WebControls.CheckBox
                    If Not (String.IsNullOrEmpty(fld.name)) Then
                        fldControl.ID = fld.name
                        fldControl.Attributes.Add("name", fld.name)
                    End If
                    If Not (String.IsNullOrEmpty(fld.value(0))) Then
                        fldControl.Text = fld.value(0)
                    End If

                    If Not (String.IsNullOrEmpty(fld.cssClass)) Then
                        fldControl.CssClass = fld.cssClass
                    End If
                    If fld.value(0).Contains(fld.selected(0)) Then
                        fldControl.Checked = True
                    End If
                    fldControl.Text = fld.name
                    parentControl.Controls.Add(fldControl)
                Case HTMLFieldType.radio
                    'strHTML.AppendLine("<input type='radio' name='" & fld.name.Replace("[", "_").Replace("]", "") & "' id='" & fld.name.Replace("[", "_").Replace("]", "") & "' value='" & fld.value(0) & "' />" & IIf(String.IsNullOrEmpty(fld.value(0)), fld.name, fld.value(0)) & "")
                    If fld.value.Length > 0 Then
                        For intValues As Integer = 0 To fld.value.Length - 1
                            Dim fldControl As New System.Web.UI.WebControls.RadioButton
                            If Not (String.IsNullOrEmpty(fld.name)) Then
                                fldControl.ID = fld.name
                                fldControl.Attributes.Add("name", fld.name)
                            End If
                            If Not (String.IsNullOrEmpty(fld.value(0))) Then
                                fldControl.Text = fld.value(0)
                            End If

                            If Not (String.IsNullOrEmpty(fld.cssClass)) Then
                                fldControl.CssClass = fld.cssClass
                            End If
                            If fld.value(0).Contains(fld.selected(0)) Then
                                fldControl.Checked = True
                            End If
                            If String.IsNullOrEmpty(fld.value(intValues)) Then
                                fldControl.Text = fld.name
                            Else
                                fldControl.Text = fld.value(intValues)
                            End If
                            parentControl.Controls.Add(fldControl)
                        Next
                    End If
                Case HTMLFieldType.image
                    'strHTML.AppendLine("<input type='radio' name='" & fld.name.Replace("[", "_").Replace("]", "") & "' id='" & fld.name.Replace("[", "_").Replace("]", "") & "' value='" & fld.value(0) & "' />" & IIf(String.IsNullOrEmpty(fld.value(0)), fld.name, fld.value(0)) & "")
                    Dim fldControl As New System.Web.UI.WebControls.Image
                    If Not (String.IsNullOrEmpty(fld.name)) Then
                        fldControl.ID = fld.name
                        fldControl.Attributes.Add("name", fld.name)
                    End If
                    If Not fld.imageBase64 Is Nothing Then
                        fldControl.ImageUrl = "data:" & fld.imageMime & ";base64," & fld.imageBase64 & "' "
                    ElseIf Not fld.value(0) Is Nothing Then
                        If Not (String.IsNullOrEmpty(fld.value(0))) Then
                            fldControl.ImageUrl = fld.value(0)
                        End If
                    End If
                    If Not (String.IsNullOrEmpty(fld.cssClass)) Then
                        fldControl.CssClass = fld.cssClass
                    End If
                    If Not (String.IsNullOrEmpty(fld.width)) Then
                        fldControl.Width = fld.width
                    End If
                    If Not (String.IsNullOrEmpty(fld.height)) Then
                        fldControl.Height = fld.height
                    End If
                    If Not (String.IsNullOrEmpty(fld.title)) Then
                        fldControl.AlternateText = fld.title
                    End If
                    Dim fldControlUpload As New System.Web.UI.WebControls.FileUpload
                    parentControl.Controls.Add(fldControl)
                Case HTMLFieldType.list
                    strHTML.AppendLine("<select name='" & fld.name.Replace("[", "_").Replace("]", "") & "' id='" & fld.name.Replace("[", "_").Replace("]", "") & "' " & IIf(fld.type = HTMLFieldType.list, IIf(fld.size > 1, "size='" & fld.size & "' ", "size='3' "), "") & IIf(fld.multiple = True, " MULTIPLE ", " ") & ">")
                    If fld.value.Length = fld.display.Length Then
                        For intVal As Integer = 0 To fld.value.Length - 1
                            If fld.selected Is Nothing Then
                                strHTML.AppendLine("<option value='" & fld.value(intVal) & "'>" & fld.display(intVal) & "</option>")
                            Else
                                If contains(fld.selected, fld.value(intVal)) Then
                                    strHTML.AppendLine("<option value='" & fld.value(intVal) & "' selected >" & fld.display(intVal) & "</option>")
                                Else
                                    strHTML.AppendLine("<option value='" & fld.value(intVal) & "'>" & fld.display(intVal) & "</option>")
                                End If
                            End If
                        Next
                    Else
                        If fld.value.Length > 0 Then
                            For Each fldvalue As String In fld.value
                                If fld.selected Is Nothing Then
                                    strHTML.AppendLine("<option value='" & fldvalue & "'>" & fldvalue & "</option>")
                                Else
                                    If contains(fld.selected, fldvalue) Then
                                        strHTML.AppendLine("<option value='" & fldvalue & "' selected >" & fldvalue & "</option>")
                                    Else
                                        strHTML.AppendLine("<option value='" & fldvalue & "'>" & fldvalue & "</option>")
                                    End If
                                End If
                            Next
                        ElseIf fld.display.Length > 0 Then
                            For Each fldvalue As String In fld.display
                                If fld.selected Is Nothing Then
                                    strHTML.AppendLine("<option value='" & fldvalue & "'>" & fldvalue & "</option>")
                                Else
                                    If contains(fld.selected, fldvalue) Then
                                        strHTML.AppendLine("<option value='" & fldvalue & "' selected >" & fldvalue & "</option>")
                                    Else
                                        strHTML.AppendLine("<option value='" & fldvalue & "'>" & fldvalue & "</option>")
                                    End If
                                End If


                            Next
                        End If
                    End If
                    strHTML.AppendLine("</select>")
                Case HTMLFieldType.combo
                    strHTML.AppendLine("<select name='" & fld.name.Replace("[", "_").Replace("]", "") & "' id='" & fld.name.Replace("[", "_").Replace("]", "") & "' " & IIf(fld.type = HTMLFieldType.list, IIf(fld.size > 1, "size='" & fld.size & "' ", "size='3' "), "") & ">")
                    If fld.value.Length = fld.display.Length Then
                        For intVal As Integer = 0 To fld.value.Length - 1
                            If fld.selected Is Nothing Then
                                strHTML.AppendLine("<option value='" & fld.value(intVal) & "'>" & fld.display(intVal) & "</option>")
                            Else
                                If contains(fld.selected, fld.value(intVal)) Then
                                    strHTML.AppendLine("<option value='" & fld.value(intVal) & "' selected >" & fld.display(intVal) & "</option>")
                                Else
                                    strHTML.AppendLine("<option value='" & fld.value(intVal) & "'>" & fld.display(intVal) & "</option>")
                                End If
                            End If
                        Next
                    Else
                        If fld.value.Length > 0 Then
                            For Each fldvalue As String In fld.value
                                If fld.selected Is Nothing Then
                                    strHTML.AppendLine("<option value='" & fldvalue & "'>" & fldvalue & "</option>")
                                Else
                                    If contains(fld.selected, fldvalue) Then
                                        strHTML.AppendLine("<option value='" & fldvalue & "' selected >" & fldvalue & "</option>")
                                    Else
                                        strHTML.AppendLine("<option value='" & fldvalue & "'>" & fldvalue & "</option>")
                                    End If
                                End If

                            Next
                        ElseIf fld.display.Length > 0 Then
                            For Each fldvalue As String In fld.display
                                If fld.selected Is Nothing Then
                                    strHTML.AppendLine("<option value='" & fldvalue & "'>" & fldvalue & "</option>")
                                Else
                                    If contains(fld.selected, fldvalue) Then
                                        strHTML.AppendLine("<option value='" & fldvalue & "' selected >" & fldvalue & "</option>")
                                    Else
                                        strHTML.AppendLine("<option value='" & fldvalue & "'>" & fldvalue & "</option>")
                                    End If
                                End If
                            Next
                        End If
                    End If
                    strHTML.AppendLine("</select>")
            End Select
            If Not String.IsNullOrEmpty(suffix) Then
                strHTML.AppendLine(suffix)
            End If

        Next

        ' SUBMIT BUTTON
        strHTML.AppendLine(preFix)
        strHTML.AppendLine(strSubmit)
        strHTML.AppendLine(suffix)

        ' RESET BUTTON
        strHTML.AppendLine(preFix)
        strHTML.AppendLine(strReset)
        strHTML.AppendLine(suffix)

        Return strHTML.ToString
    End Function
    Public Function toHTMLFields(ByVal _showTitle As Boolean, ByVal _TitleReplaceStrings As String(), Optional ByVal preFix As String = "", Optional ByVal suffix As String = "", Optional ByVal tableSpacing As String = "1", Optional ByVal tableColspan As String = "") As String
        Dim strHTML As New StringBuilder("")
        If _showTitle Then
            _fShowTitles = _showTitle
            'setTitleReplaceStringsWithSpace(HTMLFields, _TitleReplaceStrings)
        End If
        If Not _fTitleSpaceStrings Is Nothing Then
            If Not _fTitleSpaceStrings.Length <= 0 Then
                setTitleReplaceStringsWithSpace(HTMLFields, _fTitleSpaceStrings)
            End If
        End If
        On Error Resume Next
        'For Each fld As HTMLFormField In HTMLFields.values.ToArray
        Dim strTitle As New StringBuilder
        strHTML.AppendLine("<table" & IIf(Not String.IsNullOrEmpty(_fTableCSSClass & ""), " class='" & _fTableCSSClass & "'", "") & IIf(Not String.IsNullOrEmpty(_fTableCSSStyle & ""), " style='" & _fTableCSSStyle & "'", "") & ">")
        For Each fld As HTMLFormField In HTMLFields.Values
            If Not String.IsNullOrEmpty(preFix) Then
                strHTML.AppendLine(preFix)
            End If
            Select Case fld.type
                Case HTMLFieldType.text

                    strHTML.AppendLine("<tr>")
                    strHTML.Append("<td>")
                    strTitle = New StringBuilder
                    strTitle.Append("<strong>")
                    If String.IsNullOrEmpty(fld.title & "") Then
                        strTitle.Append(fld.name & "")
                    Else
                        strTitle.Append(fld.title & "")
                    End If
                    strTitle.Append("</strong>")
                    strHTML.Append(strTitle.ToString)
                    strHTML.Append("</td>")
                    strHTML.Append("</tr>")

                    strHTML.AppendLine("<tr>")
                    strHTML.Append("<td>")
                    strHTML.Append("<input type='text' name='" & fld.name.Replace("[", "_").Replace("]", "") & "' id='" & fld.name.Replace("[", "_").Replace("]", "") & "' value='" & fld.value(0) & "' ")
                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.cssClass), "", "class='" & fld.cssClass & "' "))
                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.style), "", "style='" & fld.style & "' "))
                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.width), "", "width='" & fld.width & "' "))
                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.height), "", "height='" & fld.height & "' "))
                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), "", "title='" & fld.title & "' "))
                    strHTML.Append(" />")
                    strHTML.AppendLine("</td>")
                    strHTML.AppendLine("</tr>")

                Case HTMLFieldType.textarea
                    strHTML.AppendLine("<tr>")
                    strHTML.Append("<td>")
                    strTitle = New StringBuilder
                    strTitle.Append("<strong>")
                    If String.IsNullOrEmpty(fld.title & "") Then
                        strTitle.Append(fld.name & "")
                    Else
                        strTitle.Append(fld.title & "")
                    End If
                    strTitle.Append("</strong>")
                    strHTML.Append(strTitle.ToString)
                    strHTML.Append("</td>")
                    strHTML.Append("</tr>")

                    strHTML.AppendLine("<tr>")
                    strHTML.Append("<td>")

                    strHTML.AppendLine("<input type='text' name='" & fld.name.Replace("[", "_").Replace("]", "") & "' id='" & fld.name.Replace("[", "_").Replace("]", "") & "' value='" & fld.value(0) & "' ")
                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.cssClass), "", "class='" & fld.cssClass & "' "))
                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.style), "", "style='" & fld.style & "' "))
                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.rows), "", "rows='" & fld.rows & "' "))
                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.cols), "", "cols='" & fld.cols & "' "))

                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.width), "", "width='" & fld.width & "' "))
                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.height), "", "height='" & fld.height & "' "))
                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), "", "title='" & fld.title & "' "))
                    strHTML.Append(" />")

                    strHTML.AppendLine("</td>")
                    strHTML.AppendLine("</tr>")
                Case HTMLFieldType.hidden
                    strHTML.AppendLine("<tr>")
                    strHTML.Append("<td>")
                    strTitle = New StringBuilder
                    strTitle.Append("<strong>")
                    If String.IsNullOrEmpty(fld.title & "") Then
                        strTitle.Append(fld.name & "")
                    Else
                        strTitle.Append(fld.title & "")
                    End If
                    strTitle.Append("</strong>")
                    strHTML.Append(strTitle.ToString)
                    strHTML.Append("</td>")
                    strHTML.Append("</tr>")

                    strHTML.AppendLine("<tr>")
                    strHTML.Append("<td>")

                    strHTML.AppendLine("<input type='hidden' name='" & fld.name.Replace("[", "_").Replace("]", "") & "' id='" & fld.name.Replace("[", "_").Replace("]", "") & "' value='" & fld.value(0) & "'" & " />")

                    'strHTML.Append("<input type='text' name='" & fld.name.Replace("[", "_").Replace("]", "") & "' id='" & fld.name.Replace("[", "_").Replace("]", "") & "' value='" & fld.value(0) & "' ")
                    'strHTML.Append(IIf(String.IsNullOrEmpty(fld.cssClass), "", "class='" & fld.cssClass & "' "))
                    'strHTML.Append(IIf(String.IsNullOrEmpty(fld.style), "", "style='" & fld.style & "' "))
                    'strHTML.Append(" />")
                    strHTML.AppendLine("</td>")
                    strHTML.AppendLine("</tr>")

                Case HTMLFieldType.checkbox
                    strHTML.AppendLine("<tr>")
                    strHTML.Append("<td>")
                    strTitle = New StringBuilder
                    strTitle.Append("<strong>")
                    If String.IsNullOrEmpty(fld.title & "") Then
                        strTitle.Append(fld.name & "")
                    Else
                        strTitle.Append(fld.title & "")
                    End If
                    strTitle.Append("</strong>")
                    strHTML.Append(strTitle.ToString)
                    strHTML.Append("</td>")
                    strHTML.Append("</tr>")

                    strHTML.AppendLine("<tr>")
                    strHTML.Append("<td>")

                    strHTML.AppendLine("<input type='checkbox' name='" & fld.name.Replace("[", "_").Replace("]", "") & "' id='" & fld.name.Replace("[", "_").Replace("]", "") & "' value='" & fld.value(0) & "' ")
                    strHTML.AppendLine(IIf(contains(fld.value, fld.selected), " CHECKED ", ""))
                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.width), "", "width='" & fld.width & "' "))
                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.height), "", "height='" & fld.height & "' "))
                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), "", "title='" & fld.title & "' "))
                    strHTML.AppendLine(" />")
                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), fld.name, fld.title))
                    strHTML.Append("")

                    ''strHTML.Append("<input type='text' name='" & fld.name.Replace("[", "_").Replace("]", "") & "' id='" & fld.name.Replace("[", "_").Replace("]", "") & "' value='" & fld.value(0) & "' ")
                    ''strHTML.Append(IIf(String.IsNullOrEmpty(fld.cssClass), "", "class='" & fld.cssClass & "' "))
                    ''strHTML.Append(IIf(String.IsNullOrEmpty(fld.style), "", "style='" & fld.style & "' "))
                    ''strHTML.Append(" />")
                    strHTML.AppendLine("</td>")
                    strHTML.AppendLine("</tr>")

                Case HTMLFieldType.radio
                    'strHTML.AppendLine("<input type='radio' name='" & fld.name.Replace("[", "_").Replace("]", "") & "' id='" & fld.name.Replace("[", "_").Replace("]", "") & "' value='" & fld.value(0) & "' />" & IIf(String.IsNullOrEmpty(fld.value(0)), fld.name, fld.value(0)) & "")
                    strHTML.AppendLine("<tr>")
                    strHTML.Append("<td>")
                    strTitle = New StringBuilder
                    strTitle.Append("<strong>")
                    If String.IsNullOrEmpty(fld.title & "") Then
                        strTitle.Append(fld.name & "")
                    Else
                        strTitle.Append(fld.title & "")
                    End If
                    strTitle.Append("</strong>")
                    strHTML.Append(strTitle.ToString)
                    strHTML.Append("</td>")
                    strHTML.Append("</tr>")

                    strHTML.AppendLine("<tr>")
                    strHTML.Append("<td>")

                    If fld.value.Length > 0 Then
                        For intValues As Integer = 0 To fld.value.Length - 1
                            strHTML.AppendLine("<input type='radio' name='" & fld.name.Replace("[", "_").Replace("]", "") & "' id='" & fld.name.Replace("[", "_").Replace("]", "") & "' value='" & fld.value(intValues) & "' ")
                            strHTML.AppendLine(IIf(contains(fld.value(intValues), fld.selected), " CHECKED ", ""))
                            strHTML.Append(IIf(String.IsNullOrEmpty(fld.width), "", "width='" & fld.width & "' "))
                            strHTML.Append(IIf(String.IsNullOrEmpty(fld.height), "", "height='" & fld.height & "' "))
                            strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), "", "title='" & fld.title & "' "))
                            strHTML.AppendLine(" />")
                            strHTML.Append(IIf(String.IsNullOrEmpty(fld.value(intValues)), fld.name, fld.value(intValues)) & "")
                        Next
                    End If

                    'strHTML.Append("<input type='text' name='" & fld.name.Replace("[", "_").Replace("]", "") & "' id='" & fld.name.Replace("[", "_").Replace("]", "") & "' value='" & fld.value(0) & "' ")
                    'strHTML.Append(IIf(String.IsNullOrEmpty(fld.cssClass), "", "class='" & fld.cssClass & "' "))
                    'strHTML.Append(IIf(String.IsNullOrEmpty(fld.style), "", "style='" & fld.style & "' "))
                    'strHTML.Append(" />")
                    strHTML.AppendLine("</td>")
                    strHTML.AppendLine("</tr>")

                Case HTMLFieldType.image
                    'strHTML.AppendLine("<input type='radio' name='" & fld.name.Replace("[", "_").Replace("]", "") & "' id='" & fld.name.Replace("[", "_").Replace("]", "") & "' value='" & fld.value(0) & "' />" & IIf(String.IsNullOrEmpty(fld.value(0)), fld.name, fld.value(0)) & "")
                    strHTML.AppendLine("<tr>")
                    strHTML.Append("<td>")
                    strTitle = New StringBuilder
                    strTitle.Append("<strong>")
                    If String.IsNullOrEmpty(fld.title & "") Then
                        strTitle.Append(fld.name & "")
                    Else
                        strTitle.Append(fld.title & "")
                    End If
                    strTitle.Append("</strong>")
                    strHTML.Append(strTitle.ToString)
                    strHTML.Append("</td>")
                    strHTML.Append("</tr>")

                    strHTML.AppendLine("<tr>")
                    strHTML.Append("<td>")

                    strHTML.AppendLine("<img ")
                    If Not fld.id Is Nothing Then
                        strHTML.Append("id='" & fld.id.Replace("[", "_").Replace("]", "") & "' ")
                    End If
                    If Not fld.name Is Nothing Then
                        strHTML.Append("name='" & fld.id.Replace("[", "_").Replace("]", "") & "' ")
                    End If
                    If Not fld.imageBase64 Is Nothing Then
                        If String.IsNullOrEmpty(fld.imageMime & "") Or fld.imageMime = "0" Then
                            fld.imageMime = "image/jpeg"
                        End If
                        strHTML.Append("src='data:" & fld.imageMime & ";base64," & fld.imageBase64 & "' ")
                    ElseIf Not fld.value(0) Is Nothing Then
                        strHTML.Append("src='" & fld.value(0) & "' ")
                    End If
                    If Not fld.width = Nothing Then
                        strHTML.Append("width='" & fld.width & "' ")
                    End If
                    If Not fld.height = Nothing Then
                        strHTML.Append("height='" & fld.height & "' ")
                    End If
                    If Not fld.style Is Nothing Then
                        strHTML.Append("style='" & fld.style & "' ")
                    End If
                    If Not fld.cssClass Is Nothing Then
                        strHTML.Append("class='" & fld.cssClass & "' ")
                    End If
                    If Not fld.title Is Nothing Then
                        strHTML.Append("title='" & fld.title & "' ")
                        strHTML.Append("alt='" & fld.title & "' ")
                    End If
                    'strHTML.Append(" onclick=""Javascript:uploadfile__" & fld.id.Replace("[", "_").Replace("]", "") & "_onlick();"" />")
                    'strHTML.Append("<input type='text' name='" & fld.name.Replace("[", "_").Replace("]", "") & "' id='" & fld.name.Replace("[", "_").Replace("]", "") & "' value='" & fld.value(0) & "' ")
                    'strHTML.Append(IIf(String.IsNullOrEmpty(fld.cssClass), "", "class='" & fld.cssClass & "' "))
                    'strHTML.Append(IIf(String.IsNullOrEmpty(fld.style), "", "style='" & fld.style & "' "))
                    strHTML.Append(" />")
                    If _LockFormFields = False Then
                        strHTML.Append("<input type=""file"" id=""uploadfile__" & fld.id.Replace("[", "_").Replace("]", "") & """ name=""uploadfile__" & fld.id.Replace("[", "_").Replace("]", "") & """ />")
                        strHTML.Append("<input type=""hidden"" id=""filebase64__" & fld.id.Replace("[", "_").Replace("]", "") & """ name=""filebase64__" & fld.id.Replace("[", "_").Replace("]", "") & """ value="""" />")
                        strHTML.Append("<script type=""text/javascript"">window.onload = new function() {try {var handleFileSelect = function(evt) {var files = evt.target.files;var file = files[0];if (files && file) {var reader = new FileReader();reader.onload = function(readerEvt) {var binaryString = readerEvt.target.result;document.getElementById('" & fld.id.Replace("[", "_").Replace("]", "") & "').setAttribute('src', 'data:image/jpeg;base64,' + btoa(binaryString));document.getElementById(""filebase64__" & fld.id.Replace("[", "_").Replace("]", "") & """).value = btoa(binaryString);};reader.readAsBinaryString(file);};};if (window.File && window.FileReader && window.FileList && window.Blob) {document.getElementById('uploadfile__" & fld.id.Replace("[", "_").Replace("]", "") & "').addEventListener('change', handleFileSelect, false);};} catch (e) { alert(e); };};</script>")
                        strHTML.Append("<script type=""text/javascript"">")
                        strHTML.Append("window.onload = new function() {try {")
                        strHTML.Append("    var fldUpload=document.getElementById('uploadfile__" & fld.id.Replace("[", "_").Replace("]", "") & "');")
                        strHTML.Append("    function uploadfile__" & fld.id.Replace("[", "_").Replace("]", "") & "_onlick(){")
                        strHTML.Append("        fldUpload.style.display = 'block';")
                        strHTML.Append("        fldUpload.focus();")
                        strHTML.Append("        fldUpload.click();")
                        strHTML.Append("        fldUpload.style.display = 'none';")
                        strHTML.Append("    };")
                        strHTML.Append("    fldUpload.style.display = 'none';")
                        strHTML.Append("    document.getElementById('" & fld.id.Replace("[", "_").Replace("]", "") & "').onclick=function(){uploadfile__" & fld.id.Replace("[", "_").Replace("]", "") & "_onlick();};")
                        strHTML.Append("} catch (e) { alert('error:'+e.description); };};</script>")

                    End If
                    strHTML.AppendLine("</td>")
                    strHTML.AppendLine("</tr>")
                Case HTMLFieldType.list
                    strHTML.AppendLine("<tr>")
                    strHTML.Append("<td>")
                    strTitle = New StringBuilder
                    strTitle.Append("<strong>")
                    If String.IsNullOrEmpty(fld.title & "") Then
                        strTitle.Append(fld.name & "")
                    Else
                        strTitle.Append(fld.title & "")
                    End If
                    strTitle.Append("</strong>")
                    strHTML.Append(strTitle.ToString)
                    strHTML.Append("</td>")
                    strHTML.Append("</tr>")

                    strHTML.AppendLine("<tr>")
                    strHTML.Append("<td>")

                    strHTML.AppendLine("<select name='" & fld.name.Replace("[", "_").Replace("]", "") & "' id='" & fld.name.Replace("[", "_").Replace("]", "") & "' " & IIf(fld.type = HTMLFieldType.list, IIf(fld.size > 1, "size='" & fld.size & "' ", "size='3' "), "") & IIf(fld.multiple = True, " MULTIPLE ", " ") & ">")
                    If fld.value.Length = fld.display.Length Then
                        For intVal As Integer = 0 To fld.value.Length - 1
                            If fld.selected Is Nothing Then
                                strHTML.AppendLine("<option value='" & fld.value(intVal) & "'>" & fld.display(intVal) & "</option>")
                            Else
                                If contains(fld.selected, fld.value(intVal)) Then
                                    strHTML.AppendLine("<option value='" & fld.value(intVal) & "' selected ")
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.width), "", "width='" & fld.width & "' "))
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.height), "", "height='" & fld.height & "' "))
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), "", "title='" & fld.title & "' "))
                                    strHTML.AppendLine(">" & fld.display(intVal) & "</option>")
                                Else
                                    strHTML.AppendLine("<option value='" & fld.value(intVal) & "'")
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.width), "", "width='" & fld.width & "' "))
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.height), "", "height='" & fld.height & "' "))
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), "", "title='" & fld.title & "' "))
                                    strHTML.AppendLine(">" & fld.display(intVal) & "</option>")
                                End If
                            End If
                        Next
                    Else
                        If fld.value.Length > 0 Then
                            For Each fldvalue As String In fld.value
                                If fld.selected Is Nothing Then
                                    strHTML.AppendLine("<option value='" & fldvalue & "'>" & fldvalue & "</option>")
                                Else
                                    If contains(fld.selected, fldvalue) Then
                                        strHTML.AppendLine("<option value='" & fldvalue & "' selected ")
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.width), "", "width='" & fld.width & "' "))
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.height), "", "height='" & fld.height & "' "))
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), "", "title='" & fld.title & "' "))
                                        strHTML.AppendLine(">" & fldvalue & "</option>")
                                    Else
                                        strHTML.AppendLine("<option value='" & fldvalue & "' ")
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.width), "", "width='" & fld.width & "' "))
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.height), "", "height='" & fld.height & "' "))
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), "", "title='" & fld.title & "' "))
                                        strHTML.AppendLine(">" & fldvalue & "</option>")
                                    End If
                                End If
                            Next
                        ElseIf fld.display.Length > 0 Then
                            For Each fldvalue As String In fld.display
                                If fld.selected Is Nothing Then
                                    strHTML.AppendLine("<option value='" & fldvalue & "' ")
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.width), "", "width='" & fld.width & "' "))
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.height), "", "height='" & fld.height & "' "))
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), "", "title='" & fld.title & "' "))
                                    strHTML.AppendLine(">" & fldvalue & "</option>")
                                Else
                                    If contains(fld.selected, fldvalue) Then
                                        strHTML.AppendLine("<option value='" & fldvalue & "' selected ")
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.width), "", "width='" & fld.width & "' "))
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.height), "", "height='" & fld.height & "' "))
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), "", "title='" & fld.title & "' "))
                                        strHTML.AppendLine(">" & fldvalue & "</option>")
                                    Else
                                        strHTML.AppendLine("<option value='" & fldvalue & "' ")
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.width), "", "width='" & fld.width & "' "))
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.height), "", "height='" & fld.height & "' "))
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), "", "title='" & fld.title & "' "))
                                        strHTML.AppendLine(">" & fldvalue & "</option>")
                                    End If
                                End If


                            Next
                        End If
                    End If
                    strHTML.AppendLine("</select>")

                    'strHTML.Append("<input type='text' name='" & fld.name.Replace("[", "_").Replace("]", "") & "' id='" & fld.name.Replace("[", "_").Replace("]", "") & "' value='" & fld.value(0) & "' ")
                    'strHTML.Append(IIf(String.IsNullOrEmpty(fld.cssClass), "", "class='" & fld.cssClass & "' "))
                    'strHTML.Append(IIf(String.IsNullOrEmpty(fld.style), "", "style='" & fld.style & "' "))
                    'strHTML.Append(" />")
                    strHTML.AppendLine("</td>")
                    strHTML.AppendLine("</tr>")

                Case HTMLFieldType.combo
                    strHTML.AppendLine("<tr>")
                    strHTML.Append("<td>")
                    strTitle = New StringBuilder
                    strTitle.Append("<strong>")
                    If String.IsNullOrEmpty(fld.title & "") Then
                        strTitle.Append(fld.name & "")
                    Else
                        strTitle.Append(fld.title & "")
                    End If
                    strTitle.Append("</strong>")
                    strHTML.Append(strTitle.ToString)
                    strHTML.Append("</td>")
                    strHTML.Append("</tr>")

                    strHTML.AppendLine("<tr>")
                    strHTML.Append("<td>")

                    strHTML.AppendLine("<select name='" & fld.name.Replace("[", "_").Replace("]", "") & "' id='" & fld.name.Replace("[", "_").Replace("]", "") & "' " & IIf(fld.type = HTMLFieldType.list, IIf(fld.size > 1, "size='" & fld.size & "' ", "size='3' "), "") & ">")
                    If fld.value.Length = fld.display.Length Then
                        For intVal As Integer = 0 To fld.value.Length - 1
                            If fld.selected Is Nothing Then
                                strHTML.AppendLine("<option value='" & fld.value(intVal) & "'>" & fld.display(intVal) & "</option>")
                            Else
                                If contains(fld.selected, fld.value(intVal)) Then
                                    strHTML.AppendLine("<option value='" & fld.value(intVal) & "' selected ")
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.width), "", "width='" & fld.width & "' "))
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.height), "", "height='" & fld.height & "' "))
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), "", "title='" & fld.title & "' "))
                                    strHTML.AppendLine(">" & fld.display(intVal) & "</option>")
                                Else
                                    strHTML.AppendLine("<option value='" & fld.value(intVal) & "' ")
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.width), "", "width='" & fld.width & "' "))
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.height), "", "height='" & fld.height & "' "))
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), "", "title='" & fld.title & "' "))
                                    strHTML.AppendLine(">" & fld.display(intVal) & "</option>")
                                End If
                            End If
                        Next
                    Else
                        If fld.value.Length > 0 Then
                            For Each fldvalue As String In fld.value
                                If fld.selected Is Nothing Then
                                    strHTML.AppendLine("<option value='" & fldvalue & "' ")
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.width), "", "width='" & fld.width & "' "))
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.height), "", "height='" & fld.height & "' "))
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), "", "title='" & fld.title & "' "))
                                    strHTML.AppendLine(">" & fldvalue & "</option>")
                                Else
                                    If contains(fld.selected, fldvalue) Then
                                        strHTML.AppendLine("<option value='" & fldvalue & "' selected ")
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.width), "", "width='" & fld.width & "' "))
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.height), "", "height='" & fld.height & "' "))
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), "", "title='" & fld.title & "' "))
                                        strHTML.AppendLine(">" & fldvalue & "</option>")
                                    Else
                                        strHTML.AppendLine("<option value='" & fldvalue & "' ")
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.width), "", "width='" & fld.width & "' "))
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.height), "", "height='" & fld.height & "' "))
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), "", "title='" & fld.title & "' "))
                                        strHTML.AppendLine(">" & fldvalue & "</option>")
                                    End If
                                End If

                            Next
                        ElseIf fld.display.Length > 0 Then
                            For Each fldvalue As String In fld.display
                                If fld.selected Is Nothing Then
                                    strHTML.AppendLine("<option value='" & fldvalue & "' ")
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.width), "", "width='" & fld.width & "' "))
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.height), "", "height='" & fld.height & "' "))
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), "", "title='" & fld.title & "' "))
                                    strHTML.AppendLine(">" & fldvalue & "</option>")
                                Else
                                    If contains(fld.selected, fldvalue) Then
                                        strHTML.AppendLine("<option value='" & fldvalue & "' selected ")
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.width), "", "width='" & fld.width & "' "))
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.height), "", "height='" & fld.height & "' "))
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), "", "title='" & fld.title & "' "))
                                        strHTML.AppendLine(">" & fldvalue & "</option>")
                                    Else
                                        strHTML.AppendLine("<option value='" & fldvalue & "' ")
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.width), "", "width='" & fld.width & "' "))
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.height), "", "height='" & fld.height & "' "))
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), "", "title='" & fld.title & "' "))
                                        strHTML.AppendLine(">" & fldvalue & "</option>")
                                    End If
                                End If
                            Next
                        End If
                    End If
                    strHTML.AppendLine("</select>")

                    'strHTML.Append("<input type='text' name='" & fld.name.Replace("[", "_").Replace("]", "") & "' id='" & fld.name.Replace("[", "_").Replace("]", "") & "' value='" & fld.value(0) & "' ")
                    'strHTML.Append(IIf(String.IsNullOrEmpty(fld.cssClass), "", "class='" & fld.cssClass & "' "))
                    'strHTML.Append(IIf(String.IsNullOrEmpty(fld.style), "", "style='" & fld.style & "' "))
                    'strHTML.Append(" />")
                    strHTML.AppendLine("</td>")
                    strHTML.AppendLine("</tr>")

            End Select
            If Not String.IsNullOrEmpty(suffix) Then
                strHTML.AppendLine(suffix)
            End If

        Next

        ' SUBMIT BUTTON
        strHTML.AppendLine("<tr>")
        strHTML.Append("<td>")
        strTitle = New StringBuilder
        strTitle.Append("<strong>")
        strTitle.Append("Submit")
        strTitle.Append("</strong>")
        strHTML.Append(strTitle.ToString)
        strHTML.Append("</td>")
        strHTML.Append("</tr>")

        strHTML.AppendLine("<tr>")
        strHTML.Append("<td>")
        strHTML.AppendLine(preFix)
        strHTML.AppendLine(strSubmit)
        strHTML.AppendLine(suffix)
        strHTML.Append("</td>")
        strHTML.Append("</tr>")

        ' RESET BUTTON
        strHTML.AppendLine("<tr>")
        strHTML.Append("<td>")
        strTitle = New StringBuilder
        strTitle.Append("<strong>")
        strTitle.Append("Reset")
        strTitle.Append("</strong>")
        strHTML.Append(strTitle.ToString)
        strHTML.Append("</td>")
        strHTML.Append("</tr>")

        strHTML.AppendLine("<tr>")
        strHTML.Append("<td>")
        strHTML.AppendLine(preFix)
        strHTML.AppendLine(strReset)
        strHTML.AppendLine(suffix)
        strHTML.Append("</td>")
        strHTML.Append("</tr>")

        strHTML.AppendLine("</table>")
        Return strHTML.ToString
    End Function
    Public Function toHTMLFieldsTable(ByVal _showTitle As Boolean, ByVal _TitleReplaceStrings As String(), Optional ByVal preFix As String = "", Optional ByVal suffix As String = "", Optional ByVal tableSpacing As String = "1", Optional ByVal tableColspan As String = "", Optional ByVal SubmitButtonText As String = "Submit") As String
        Dim strHTML As New StringBuilder("")
        If _showTitle Then
            _fShowTitles = _showTitle
            'setTitleReplaceStringsWithSpace(HTMLFields, _TitleReplaceStrings)
        End If
        If Not _fTitleSpaceStrings Is Nothing Then
            If Not _fTitleSpaceStrings.Length <= 0 Then
                setTitleReplaceStringsWithSpace(HTMLFields, _fTitleSpaceStrings)
            End If
        End If
        On Error Resume Next
        'For Each fld As HTMLFormField In HTMLFields.values.ToArray
        Dim strTitle As New StringBuilder
        strHTML.AppendLine("<table" & IIf(Not String.IsNullOrEmpty(_fTableCSSClass & ""), " class='" & _fTableCSSClass & "'", "") & IIf(Not String.IsNullOrEmpty(_fTableCSSStyle & ""), " style='" & _fTableCSSStyle & "'", "") & ">")
        For Each fld As HTMLFormField In HTMLFields.Values
            If Not String.IsNullOrEmpty(preFix) Then
                strHTML.AppendLine(preFix)
            End If
            Dim fldname As String = fld.name & ""
            If fldname.Contains("[") Then
                fldname = fldname.Substring(0, fldname.Length - (fldname.Length - fldname.IndexOf("[")))
            End If
            Select Case fld.type
                Case HTMLFieldType.text

                    strHTML.AppendLine("<tr>")
                    strHTML.Append("<td>")
                    strTitle = New StringBuilder
                    strTitle.Append("<strong>")
                    If String.IsNullOrEmpty(fld.title & "") Then
                        strTitle.Append(fldname & "")
                    Else
                        strTitle.Append(fld.title & "")
                    End If
                    strTitle.Append("</strong>")
                    strHTML.Append(strTitle.ToString)
                    strHTML.Append("</td>")
                    strHTML.Append("</tr>")

                    strHTML.AppendLine("<tr>")
                    strHTML.Append("<td>")

                    strHTML.Append("<input type='text' name='" & fldname & "' id='" & fldname & "' value='" & fld.value(0) & "' ")
                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.cssClass), "", "class='" & fld.cssClass & "' "))
                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.style), "", "style='" & fld.style & "' "))
                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.width), "", "width='" & fld.width & "' "))
                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.height), "", "height='" & fld.height & "' "))
                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), "", "title='" & fld.title & "' "))
                    strHTML.Append(" />")
                    strHTML.AppendLine("</td>")
                    strHTML.AppendLine("</tr>")

                Case HTMLFieldType.textarea
                    strHTML.AppendLine("<tr>")
                    strHTML.Append("<td>")
                    strTitle = New StringBuilder
                    strTitle.Append("<strong>")
                    If String.IsNullOrEmpty(fld.title & "") Then
                        strTitle.Append(fldname & "")
                    Else
                        strTitle.Append(fld.title & "")
                    End If
                    strTitle.Append("</strong>")
                    strHTML.Append(strTitle.ToString)
                    strHTML.Append("</td>")
                    strHTML.Append("</tr>")

                    strHTML.AppendLine("<tr>")
                    strHTML.Append("<td>")

                    strHTML.AppendLine("<input type='text' name='" & fldname & "' id='" & fldname & "' value='" & fld.value(0) & "' ")
                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.cssClass), "", "class='" & fld.cssClass & "' "))
                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.style), "", "style='" & fld.style & "' "))
                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.rows), "", "rows='" & fld.rows & "' "))
                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.cols), "", "cols='" & fld.cols & "' "))

                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.width), "", "width='" & fld.width & "' "))
                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.height), "", "height='" & fld.height & "' "))
                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), "", "title='" & fld.title & "' "))
                    strHTML.Append(" />")

                    strHTML.AppendLine("</td>")
                    strHTML.AppendLine("</tr>")
                Case HTMLFieldType.hidden
                    strHTML.AppendLine("<tr>")
                    strHTML.Append("<td>")
                    strTitle = New StringBuilder
                    strTitle.Append("<strong>")
                    If String.IsNullOrEmpty(fld.title & "") Then
                        strTitle.Append(fldname & "")
                    Else
                        strTitle.Append(fld.title & "")
                    End If
                    strTitle.Append("</strong>")
                    strHTML.Append(strTitle.ToString)
                    strHTML.Append("</td>")
                    strHTML.Append("</tr>")

                    strHTML.AppendLine("<tr>")
                    strHTML.Append("<td>")

                    strHTML.AppendLine("<input type='hidden' name='" & fldname & "' id='" & fldname & "' value='" & fld.value(0) & "'" & " />")

                    'strHTML.Append("<input type='text' name='" & fldname & "' id='" & fldname & "' value='" & fld.value(0) & "' ")
                    'strHTML.Append(IIf(String.IsNullOrEmpty(fld.cssClass), "", "class='" & fld.cssClass & "' "))
                    'strHTML.Append(IIf(String.IsNullOrEmpty(fld.style), "", "style='" & fld.style & "' "))
                    'strHTML.Append(" />")
                    strHTML.AppendLine("</td>")
                    strHTML.AppendLine("</tr>")

                Case HTMLFieldType.checkbox
                    strHTML.AppendLine("<tr>")
                    strHTML.Append("<td>")
                    strTitle = New StringBuilder
                    strTitle.Append("<strong>")
                    If String.IsNullOrEmpty(fld.title & "") Then
                        strTitle.Append(fldname & "")
                    Else
                        strTitle.Append(fld.title & "")
                    End If
                    strTitle.Append("</strong>")
                    strHTML.Append(strTitle.ToString)
                    strHTML.Append("</td>")
                    strHTML.Append("</tr>")

                    strHTML.AppendLine("<tr>")
                    strHTML.Append("<td>")

                    strHTML.AppendLine("<input type='checkbox' name='" & fldname & "' id='" & fldname & "' value='" & fld.value(0) & "' ")
                    strHTML.AppendLine(IIf(contains(fld.value, fld.selected), " CHECKED ", ""))
                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.width), "", "width='" & fld.width & "' "))
                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.height), "", "height='" & fld.height & "' "))
                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), "", "title='" & fld.title & "' "))
                    strHTML.AppendLine(" />")
                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), fldname, fld.title))
                    strHTML.Append("")

                    ''strHTML.Append("<input type='text' name='" & fldname & "' id='" & fldname & "' value='" & fld.value(0) & "' ")
                    ''strHTML.Append(IIf(String.IsNullOrEmpty(fld.cssClass), "", "class='" & fld.cssClass & "' "))
                    ''strHTML.Append(IIf(String.IsNullOrEmpty(fld.style), "", "style='" & fld.style & "' "))
                    ''strHTML.Append(" />")
                    strHTML.AppendLine("</td>")
                    strHTML.AppendLine("</tr>")

                Case HTMLFieldType.radio
                    'strHTML.AppendLine("<input type='radio' name='" & fldname & "' id='" & fldname & "' value='" & fld.value(0) & "' />" & IIf(String.IsNullOrEmpty(fld.value(0)), fldname, fld.value(0)) & "")
                    strHTML.AppendLine("<tr>")
                    strHTML.Append("<td>")
                    strTitle = New StringBuilder
                    strTitle.Append("<strong>")
                    If String.IsNullOrEmpty(fld.title & "") Then
                        strTitle.Append(fldname & "")
                    Else
                        strTitle.Append(fld.title & "")
                    End If
                    strTitle.Append("</strong>")
                    strHTML.Append(strTitle.ToString)
                    strHTML.Append("</td>")
                    strHTML.Append("</tr>")

                    strHTML.AppendLine("<tr>")
                    strHTML.Append("<td>")

                    If fld.value.Length > 0 Then
                        For intValues As Integer = 0 To fld.value.Length - 1
                            strHTML.AppendLine("<input type='radio' name='" & fldname & "' id='" & fldname & "' value='" & fld.value(intValues) & "' ")
                            strHTML.AppendLine(IIf(contains(fld.value(intValues), fld.selected), " CHECKED ", ""))
                            strHTML.Append(IIf(String.IsNullOrEmpty(fld.width), "", "width='" & fld.width & "' "))
                            strHTML.Append(IIf(String.IsNullOrEmpty(fld.height), "", "height='" & fld.height & "' "))
                            strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), "", "title='" & fld.title & "' "))
                            strHTML.AppendLine(" />")
                            strHTML.Append(IIf(String.IsNullOrEmpty(fld.value(intValues)), fldname, fld.value(intValues)) & "")
                        Next
                    End If

                    'strHTML.Append("<input type='text' name='" & fldname & "' id='" & fldname & "' value='" & fld.value(0) & "' ")
                    'strHTML.Append(IIf(String.IsNullOrEmpty(fld.cssClass), "", "class='" & fld.cssClass & "' "))
                    'strHTML.Append(IIf(String.IsNullOrEmpty(fld.style), "", "style='" & fld.style & "' "))
                    'strHTML.Append(" />")
                    strHTML.AppendLine("</td>")
                    strHTML.AppendLine("</tr>")

                Case HTMLFieldType.image
                    'strHTML.AppendLine("<input type='radio' name='" & fldname & "' id='" & fldname & "' value='" & fld.value(0) & "' />" & IIf(String.IsNullOrEmpty(fld.value(0)), fldname, fld.value(0)) & "")
                    strHTML.AppendLine("<tr>")
                    strHTML.Append("<td>")
                    strTitle = New StringBuilder
                    strTitle.Append("<strong>")
                    If String.IsNullOrEmpty(fld.title & "") Then
                        strTitle.Append(fldname & "")
                    Else
                        strTitle.Append(fld.title & "")
                    End If
                    strTitle.Append("</strong>")
                    strHTML.Append(strTitle.ToString)
                    strHTML.Append("</td>")
                    strHTML.Append("</tr>")

                    strHTML.AppendLine("<tr>")
                    strHTML.Append("<td>")

                    strHTML.AppendLine("<img ")
                    If Not fld.id Is Nothing Then
                        strHTML.Append("id='" & fld.id.Replace("[", "_").Replace("]", "") & "' ")
                    End If
                    If Not fldname Is Nothing Then
                        strHTML.Append("name='" & fld.id.Replace("[", "_").Replace("]", "") & "' ")
                    End If
                    If Not fld.imageBase64 Is Nothing Then
                        If String.IsNullOrEmpty(fld.imageMime & "") Or fld.imageMime = "0" Then
                            fld.imageMime = "image/jpeg"
                        End If
                        strHTML.Append("src='data:" & fld.imageMime & ";base64," & fld.imageBase64 & "' ")
                    ElseIf Not fld.value(0) Is Nothing Then
                        strHTML.Append("src='" & fld.value(0) & "' ")
                    End If
                    If Not fld.width = Nothing Then
                        strHTML.Append("width='" & fld.width & "' ")
                    End If
                    If Not fld.height = Nothing Then
                        strHTML.Append("height='" & fld.height & "' ")
                    End If
                    If Not fld.style Is Nothing Then
                        strHTML.Append("style='" & fld.style & "' ")
                    End If
                    If Not fld.cssClass Is Nothing Then
                        strHTML.Append("class='" & fld.cssClass & "' ")
                    End If
                    If Not fld.title Is Nothing Then
                        strHTML.Append("title='" & fld.title & "' ")
                        strHTML.Append("alt='" & fld.title & "' ")
                    End If
                    strHTML.Append(" />")
                    If _LockFormFields = False Then
                        strHTML.Append("<input type=""file"" id=""uploadfile__" & fld.id.Replace("[", "_").Replace("]", "") & """ name=""uploadfile__" & fld.id.Replace("[", "_").Replace("]", "") & """ />")
                        strHTML.Append("<input type=""hidden"" id=""filebase64__" & fld.id.Replace("[", "_").Replace("]", "") & """ name=""filebase64__" & fld.id.Replace("[", "_").Replace("]", "") & """ value="""" />")
                        strHTML.Append("<script type=""text/javascript"">window.onload = new function() {try {var handleFileSelect = function(evt) {var files = evt.target.files;var file = files[0];if (files && file) {var reader = new FileReader();reader.onload = function(readerEvt) {var binaryString = readerEvt.target.result;document.getElementById('" & fld.id.Replace("[", "_").Replace("]", "") & "').setAttribute('src', 'data:image/jpeg;base64,' + btoa(binaryString));document.getElementById(""filebase64__" & fld.id.Replace("[", "_").Replace("]", "") & """).value = btoa(binaryString);};reader.readAsBinaryString(file);};};if (window.File && window.FileReader && window.FileList && window.Blob) {document.getElementById('uploadfile__" & fld.id.Replace("[", "_").Replace("]", "") & "').addEventListener('change', handleFileSelect, false);};} catch (e) { alert(e); };};</script>")
                        strHTML.Append("<script type=""text/javascript"">")
                        strHTML.Append("window.onload = new function() {try {")
                        strHTML.Append("    var fldUpload=document.getElementById('uploadfile__" & fld.id.Replace("[", "_").Replace("]", "") & "');")
                        strHTML.Append("    function uploadfile__" & fld.id.Replace("[", "_").Replace("]", "") & "_onlick(){")
                        strHTML.Append("        fldUpload.style.display = 'block';")
                        strHTML.Append("        fldUpload.focus();")
                        strHTML.Append("        fldUpload.click();")
                        strHTML.Append("        fldUpload.style.display = 'none';")
                        strHTML.Append("    };")
                        strHTML.Append("    fldUpload.style.display = 'none';")
                        strHTML.Append("    document.getElementById('" & fld.id.Replace("[", "_").Replace("]", "") & "').onclick=function(){uploadfile__" & fld.id.Replace("[", "_").Replace("]", "") & "_onlick();};")
                        strHTML.Append("} catch (e) { alert('error:'+e.description); };};</script>")

                    End If
                    strHTML.AppendLine("</td>")
                    strHTML.AppendLine("</tr>")
                Case HTMLFieldType.list
                    strHTML.AppendLine("<tr>")
                    strHTML.Append("<td>")
                    strTitle = New StringBuilder
                    strTitle.Append("<strong>")
                    If String.IsNullOrEmpty(fld.title & "") Then
                        strTitle.Append(fldname & "")
                    Else
                        strTitle.Append(fld.title & "")
                    End If
                    strTitle.Append("</strong>")
                    strHTML.Append(strTitle.ToString)
                    strHTML.Append("</td>")
                    strHTML.Append("</tr>")

                    strHTML.AppendLine("<tr>")
                    strHTML.Append("<td>")

                    strHTML.AppendLine("<select name='" & fldname & "' id='" & fldname & "' " & IIf(fld.type = HTMLFieldType.list, IIf(fld.size > 1, "size='" & fld.size & "' ", "size='3' "), "") & IIf(fld.multiple = True, " MULTIPLE ", " ") & ">")
                    If fld.value.Length = fld.display.Length Then
                        For intVal As Integer = 0 To fld.value.Length - 1
                            If fld.selected Is Nothing Then
                                strHTML.AppendLine("<option value='" & fld.value(intVal) & "'>" & fld.display(intVal) & "</option>")
                            Else
                                If contains(fld.selected, fld.value(intVal)) Then
                                    strHTML.AppendLine("<option value='" & fld.value(intVal) & "' selected ")
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.width), "", "width='" & fld.width & "' "))
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.height), "", "height='" & fld.height & "' "))
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), "", "title='" & fld.title & "' "))
                                    strHTML.AppendLine(">" & fld.display(intVal) & "</option>")
                                Else
                                    strHTML.AppendLine("<option value='" & fld.value(intVal) & "'")
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.width), "", "width='" & fld.width & "' "))
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.height), "", "height='" & fld.height & "' "))
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), "", "title='" & fld.title & "' "))
                                    strHTML.AppendLine(">" & fld.display(intVal) & "</option>")
                                End If
                            End If
                        Next
                    Else
                        If fld.value.Length > 0 Then
                            For Each fldvalue As String In fld.value
                                If fld.selected Is Nothing Then
                                    strHTML.AppendLine("<option value='" & fldvalue & "'>" & fldvalue & "</option>")
                                Else
                                    If contains(fld.selected, fldvalue) Then
                                        strHTML.AppendLine("<option value='" & fldvalue & "' selected ")
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.width), "", "width='" & fld.width & "' "))
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.height), "", "height='" & fld.height & "' "))
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), "", "title='" & fld.title & "' "))
                                        strHTML.AppendLine(">" & fldvalue & "</option>")
                                    Else
                                        strHTML.AppendLine("<option value='" & fldvalue & "' ")
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.width), "", "width='" & fld.width & "' "))
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.height), "", "height='" & fld.height & "' "))
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), "", "title='" & fld.title & "' "))
                                        strHTML.AppendLine(">" & fldvalue & "</option>")
                                    End If
                                End If
                            Next
                        ElseIf fld.display.Length > 0 Then
                            For Each fldvalue As String In fld.display
                                If fld.selected Is Nothing Then
                                    strHTML.AppendLine("<option value='" & fldvalue & "' ")
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.width), "", "width='" & fld.width & "' "))
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.height), "", "height='" & fld.height & "' "))
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), "", "title='" & fld.title & "' "))
                                    strHTML.AppendLine(">" & fldvalue & "</option>")
                                Else
                                    If contains(fld.selected, fldvalue) Then
                                        strHTML.AppendLine("<option value='" & fldvalue & "' selected ")
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.width), "", "width='" & fld.width & "' "))
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.height), "", "height='" & fld.height & "' "))
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), "", "title='" & fld.title & "' "))
                                        strHTML.AppendLine(">" & fldvalue & "</option>")
                                    Else
                                        strHTML.AppendLine("<option value='" & fldvalue & "' ")
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.width), "", "width='" & fld.width & "' "))
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.height), "", "height='" & fld.height & "' "))
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), "", "title='" & fld.title & "' "))
                                        strHTML.AppendLine(">" & fldvalue & "</option>")
                                    End If
                                End If


                            Next
                        End If
                    End If
                    strHTML.AppendLine("</select>")

                    'strHTML.Append("<input type='text' name='" & fldname & "' id='" & fldname & "' value='" & fld.value(0) & "' ")
                    'strHTML.Append(IIf(String.IsNullOrEmpty(fld.cssClass), "", "class='" & fld.cssClass & "' "))
                    'strHTML.Append(IIf(String.IsNullOrEmpty(fld.style), "", "style='" & fld.style & "' "))
                    'strHTML.Append(" />")
                    strHTML.AppendLine("</td>")
                    strHTML.AppendLine("</tr>")

                Case HTMLFieldType.combo
                    strHTML.AppendLine("<tr>")
                    strHTML.Append("<td>")
                    strTitle = New StringBuilder
                    strTitle.Append("<strong>")
                    If String.IsNullOrEmpty(fld.title & "") Then
                        strTitle.Append(fldname & "")
                    Else
                        strTitle.Append(fld.title & "")
                    End If
                    strTitle.Append("</strong>")
                    strHTML.Append(strTitle.ToString)
                    strHTML.Append("</td>")
                    strHTML.Append("</tr>")

                    strHTML.AppendLine("<tr>")
                    strHTML.Append("<td>")

                    strHTML.AppendLine("<select name='" & fldname & "' id='" & fldname & "' " & IIf(fld.type = HTMLFieldType.list, IIf(fld.size > 1, "size='" & fld.size & "' ", "size='3' "), "") & ">")
                    If fld.value.Length = fld.display.Length Then
                        For intVal As Integer = 0 To fld.value.Length - 1
                            If fld.selected Is Nothing Then
                                strHTML.AppendLine("<option value='" & fld.value(intVal) & "'>" & fld.display(intVal) & "</option>")
                            Else
                                If contains(fld.selected, fld.value(intVal)) Then
                                    strHTML.AppendLine("<option value='" & fld.value(intVal) & "' selected ")
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.width), "", "width='" & fld.width & "' "))
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.height), "", "height='" & fld.height & "' "))
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), "", "title='" & fld.title & "' "))
                                    strHTML.AppendLine(">" & fld.display(intVal) & "</option>")
                                Else
                                    strHTML.AppendLine("<option value='" & fld.value(intVal) & "' ")
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.width), "", "width='" & fld.width & "' "))
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.height), "", "height='" & fld.height & "' "))
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), "", "title='" & fld.title & "' "))
                                    strHTML.AppendLine(">" & fld.display(intVal) & "</option>")
                                End If
                            End If
                        Next
                    Else
                        If fld.value.Length > 0 Then
                            For Each fldvalue As String In fld.value
                                If fld.selected Is Nothing Then
                                    strHTML.AppendLine("<option value='" & fldvalue & "' ")
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.width), "", "width='" & fld.width & "' "))
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.height), "", "height='" & fld.height & "' "))
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), "", "title='" & fld.title & "' "))
                                    strHTML.AppendLine(">" & fldvalue & "</option>")
                                Else
                                    If contains(fld.selected, fldvalue) Then
                                        strHTML.AppendLine("<option value='" & fldvalue & "' selected ")
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.width), "", "width='" & fld.width & "' "))
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.height), "", "height='" & fld.height & "' "))
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), "", "title='" & fld.title & "' "))
                                        strHTML.AppendLine(">" & fldvalue & "</option>")
                                    Else
                                        strHTML.AppendLine("<option value='" & fldvalue & "' ")
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.width), "", "width='" & fld.width & "' "))
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.height), "", "height='" & fld.height & "' "))
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), "", "title='" & fld.title & "' "))
                                        strHTML.AppendLine(">" & fldvalue & "</option>")
                                    End If
                                End If

                            Next
                        ElseIf fld.display.Length > 0 Then
                            For Each fldvalue As String In fld.display
                                If fld.selected Is Nothing Then
                                    strHTML.AppendLine("<option value='" & fldvalue & "' ")
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.width), "", "width='" & fld.width & "' "))
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.height), "", "height='" & fld.height & "' "))
                                    strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), "", "title='" & fld.title & "' "))
                                    strHTML.AppendLine(">" & fldvalue & "</option>")
                                Else
                                    If contains(fld.selected, fldvalue) Then
                                        strHTML.AppendLine("<option value='" & fldvalue & "' selected ")
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.width), "", "width='" & fld.width & "' "))
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.height), "", "height='" & fld.height & "' "))
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), "", "title='" & fld.title & "' "))
                                        strHTML.AppendLine(">" & fldvalue & "</option>")
                                    Else
                                        strHTML.AppendLine("<option value='" & fldvalue & "' ")
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.width), "", "width='" & fld.width & "' "))
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.height), "", "height='" & fld.height & "' "))
                                        strHTML.Append(IIf(String.IsNullOrEmpty(fld.title), "", "title='" & fld.title & "' "))
                                        strHTML.AppendLine(">" & fldvalue & "</option>")
                                    End If
                                End If
                            Next
                        End If
                    End If
                    strHTML.AppendLine("</select>")

                    'strHTML.Append("<input type='text' name='" & fldname & "' id='" & fldname & "' value='" & fld.value(0) & "' ")
                    'strHTML.Append(IIf(String.IsNullOrEmpty(fld.cssClass), "", "class='" & fld.cssClass & "' "))
                    'strHTML.Append(IIf(String.IsNullOrEmpty(fld.style), "", "style='" & fld.style & "' "))
                    'strHTML.Append(" />")
                    strHTML.AppendLine("</td>")
                    strHTML.AppendLine("</tr>")

            End Select
            If Not String.IsNullOrEmpty(suffix) Then
                strHTML.AppendLine(suffix)
            End If

        Next

        ' SUBMIT BUTTON
        If Not _LockFormFields Then
            strHTML.AppendLine("<tr>")
            strHTML.Append("<td>")
            strTitle = New StringBuilder
            strTitle.Append("<strong>")
            strTitle.Append("Submit")
            strTitle.Append("</strong>")
            strHTML.Append(strTitle.ToString)
            strHTML.Append("</td>")
            strHTML.Append("</tr>")
            strHTML.AppendLine("<tr>")
            strHTML.Append("<td>")
            strHTML.AppendLine(preFix)
            strHTML.AppendLine(strSubmit(SubmitButtonText))
            strHTML.AppendLine(suffix)
            strHTML.Append("</td>")
            strHTML.Append("</tr>")
        End If

        ' RESET BUTTON
        If Not _LockFormFields Then
            strHTML.AppendLine("<tr>")
            strHTML.Append("<td>")
            strTitle = New StringBuilder
            strTitle.Append("<strong>")
            strTitle.Append("Reset")
            strTitle.Append("</strong>")
            strHTML.Append(strTitle.ToString)
            strHTML.Append("</td>")
            strHTML.Append("</tr>")

            strHTML.AppendLine("<tr>")
            strHTML.Append("<td>")
            strHTML.AppendLine(preFix)
            strHTML.AppendLine(strReset)
            strHTML.AppendLine(suffix)
            strHTML.Append("</td>")
            strHTML.Append("</tr>")
        End If


        strHTML.AppendLine("</table>")
        Return strHTML.ToString
    End Function
    Private Function LoadPDFFormFieldsList(ByVal pdfPath As String, Optional ByVal ownerPassword As String = "") As List(Of HTMLFormField)
        Dim myFields As New System.Collections.Generic.List(Of HTMLFormField)
        Dim reader As iTextSharp.text.pdf.PdfReader
        If String.IsNullOrEmpty(ownerPassword) Then
            reader = New iTextSharp.text.pdf.PdfReader(pdfPath)
        Else
            reader = New iTextSharp.text.pdf.PdfReader(pdfPath, System.Text.Encoding.Default.GetBytes(ownerPassword))
        End If
        Dim af As iTextSharp.text.pdf.PRAcroForm
        af = reader.AcroForm

        Dim fld As iTextSharp.text.pdf.PRAcroForm.FieldInformation

        Dim flds As iTextSharp.text.pdf.AcroFields = reader.AcroFields
        'Dim fields As ArrayList = 
        On Error Resume Next
        Dim strHTMLFormFields As String = ""
        For Each fld In af.Fields.ToArray()
            Dim fieldName As String = fld.Name
            If InStr(fieldName, "].") Then
                Dim fldstart As Integer = fieldName.LastIndexOf("].")
                fieldName = fieldName.Substring(fldstart + 2, fieldName.Length - fldstart - 2)
            End If
            Dim val As String = ""
            val = flds.GetField(fieldName) & ""
            'flds.GetFieldPositions
            Dim fldItem As iTextSharp.text.pdf.AcroFields.Item = flds.GetFieldItem(fieldName)
            If flds.GetFieldType(fieldName) = iTextSharp.text.pdf.AcroFields.FIELD_TYPE_TEXT Then
                'strHTMLFormFields &= "<input type='text' name='" & fieldName & "' value='" & val & "' />"
                myFields.Add(New HTMLFormField(fieldName, HTMLFieldType.text, val))
            ElseIf flds.GetFieldType(fieldName) = iTextSharp.text.pdf.AcroFields.FIELD_TYPE_CHECKBOX Then
                'strHTMLFormFields &= "<input type='text' name='" & fieldName & "' value='" & val & "' />"
                myFields.Add(New HTMLFormField(fieldName, HTMLFieldType.checkbox, val))
            ElseIf flds.GetFieldType(fieldName) = iTextSharp.text.pdf.AcroFields.FIELD_TYPE_LIST Then
                Dim lstD As String(), lstV As String(), lstS As String(), intSize As Integer = 5
                lstD = flds.GetListOptionDisplay(fieldName)
                lstV = flds.GetListOptionExport(fieldName)
                lstS = flds.GetListSelection(fieldName)
                'intSize = fldItem.Size
                'strHTMLFormFields &= "<select name='" & fieldName & "'>"
                'For intLst As Integer = 0 To lstD.Length
                'strHTMLFormFields &= "<listitem value='" & lstV(intLst) & "'>" & lstD(intLst) & "</listitem>"
                'Next
                'strHTMLFormFields &= "</select>"
                myFields.Add(New HTMLFormField(fieldName, HTMLFieldType.list, lstV, lstD, lstS, intSize))
            ElseIf flds.GetFieldType(fieldName) = iTextSharp.text.pdf.AcroFields.FIELD_TYPE_COMBO Then
                Dim lstD As String(), lstV As String(), lstS As String() = {}
                lstD = flds.GetListOptionDisplay(fieldName)
                lstV = flds.GetListOptionExport(fieldName)
                ReDim Preserve lstS(0)
                'flds.GetListSelection
                lstS(0) = flds.GetField(fieldName)
                'strHTMLFormFields &= "<select name='" & fieldName & "'>"
                'For intLst As Integer = 0 To lstD.Length
                'strHTMLFormFields &= "<listitem value='" & lstV(intLst) & "'>" & lstD(intLst) & "</listitem>"
                'Next
                'strHTMLFormFields &= "</select>"
                myFields.Add(New HTMLFormField(fieldName, HTMLFieldType.combo, lstV, lstD, lstS, 1))
            ElseIf flds.GetFieldType(fieldName) = iTextSharp.text.pdf.AcroFields.FIELD_TYPE_RADIOBUTTON Then
                Dim lstD As String() = {}, lstV As New ArrayList
                '     For v As Integer = 0 To fldItem.Size - 1
                'val = fldItem.GetValue(v)
                ''lstV.Add(fldItem.values(v))
                '     Next
                Dim states() As String = flds.GetAppearanceStates(fld.Name)
                For i As Integer = 0 To states.Length - 1
                    val = states(i)
                    ReDim Preserve lstD(i)
                    'lstV.Add(val)
                    lstD(i) = val
                    'System.out.println("Possible values
                Next
                'val = flds.GetField(fieldName) & ""
                'val = lstV(0)
                'val = lstV(1)
                'lstD = lstV.ToArray
                Dim blnFound As Boolean = False
                For Each radioFld As HTMLFormField In myFields
                    If radioFld.name = fieldName Then
                        blnFound = True
                        Exit For
                    End If
                Next
                If Not blnFound Then
                    myFields.Add(New HTMLFormField(fieldName, HTMLFieldType.radio, lstD))
                End If
            ElseIf Not flds.GetFieldType(fieldName) = Nothing Or flds.GetFieldType(fieldName) = iTextSharp.text.pdf.AcroFields.FIELD_TYPE_PUSHBUTTON Or flds.GetFieldType(fieldName) = iTextSharp.text.pdf.AcroFields.FIELD_TYPE_SIGNATURE Then

                Dim intType As Integer = flds.GetFieldType(fieldName)
                Dim lstD As String() = {}, lstV As New ArrayList

                For v As Integer = 0 To fldItem.Size - 1
                    'val = fldItem.GetValue(v)
                    lstV.Add(fldItem.GetValue(v))
                Next

                Dim states() As String = flds.GetAppearanceStates(fld.Name)
                For i As Integer = 0 To states.Length - 1
                    val = states(i)
                    ReDim Preserve lstD(i)
                    'lstV.Add(val)
                    lstD(i) = val
                    'System.out.println("Possible values
                Next
                Dim blnFound As Boolean = False
                For Each radioFld As HTMLFormField In myFields
                    If radioFld.name = fieldName Then
                        blnFound = True
                        Exit For
                    End If
                Next
                If Not blnFound Then
                    If isBase64(lstV(0)) Then
                        'myFields.Add(New HTMLFormField(fieldName, HTMLFieldType.image, val.ToCharArray, "image/jpeg", False))
                        'myFields.Add(New HTMLFormField(fieldName, HTMLFieldType.image, lstV(0).ToString.ToCharArray(), "image/jpeg", False))
                        myFields.Add(New HTMLFormField(fieldName, HTMLFieldType.image, lstV(0).ToString.ToCharArray(), "image/jpeg", False))
                    Else
                        'myFields.Add(New HTMLFormField(fieldName, HTMLFieldType.image, lstV(0).ToString))
                    End If
                End If
            End If
        Next
        Return myFields
    End Function
    Private Function GetUsedBytesOnly(ByRef b() As Byte) As Byte()
        Dim bytes As Byte() = b
        Dim i As Integer = 0
        For i = bytes.Length - 1 To 1 Step -1
            If bytes(i) <> 0 Then
                Exit For
            End If
        Next
        Dim newBytes As Byte() = New Byte(i - 1) {}
        Array.Copy(bytes, newBytes, i)
        ReDim bytes(0)
        bytes = Nothing
        Return newBytes
    End Function
    Private Function GetUsedBytesOnly(ByVal strURL As String) As Byte()
        Dim cweb As New System.Net.WebClient()
        Dim bytes As Byte() = GetUsedBytesOnly(cweb.DownloadData(strURL))
        Dim i As Integer = 0
        For i = bytes.Length - 1 To 1 Step -1
            If bytes(i) <> 0 Then
                Exit For
            End If
        Next
        Dim newBytes As Byte() = New Byte(i - 1) {}
        Array.Copy(bytes, newBytes, i)
        ReDim bytes(0)
        bytes = Nothing
        Return newBytes
    End Function
    Private Function LoadPDFFormFields(ByVal pdfPath As String, Optional ByVal ownerPassword As String = "") As Dictionary(Of String, HTMLFormField)
        Dim myFields As New Dictionary(Of String, HTMLFormField)
        Dim reader As iTextSharp.text.pdf.PdfReader
        Dim streamRead As System.IO.StreamReader
        If System.IO.File.Exists(pdfPath) Then
            streamRead = New System.IO.StreamReader(pdfPath, True)
        Else
            streamRead = New System.IO.StreamReader(New System.IO.MemoryStream(GetUsedBytesOnly(pdfPath)), True)
        End If
        Dim pdfBuffer(streamRead.BaseStream.Length) As Byte
        streamRead.BaseStream.Read(pdfBuffer, 0, pdfBuffer.Length)
        If String.IsNullOrEmpty(ownerPassword) Then
            reader = New iTextSharp.text.pdf.PdfReader(pdfBuffer)
        Else
            reader = New iTextSharp.text.pdf.PdfReader(pdfBuffer, System.Text.Encoding.ASCII.GetBytes(ownerPassword))
        End If
        Dim xfaFrm As New iTextSharp.text.pdf.XfaForm(reader)

        Dim isXFA As Boolean = False
        isXFA = xfaFrm.XfaPresent

        Dim af As iTextSharp.text.pdf.PRAcroForm
        af = reader.AcroForm

        Dim fld As iTextSharp.text.pdf.PRAcroForm.FieldInformation

        Dim flds As iTextSharp.text.pdf.AcroFields = reader.AcroFields
        'Dim fields As ArrayList = af.Fields
        On Error Resume Next
        Dim strHTMLFormFields As String = ""
        For Each fld In af.Fields.ToArray()
            Dim fieldName As String = fld.Name
            If InStr(fieldName, "].") Then
                Dim fldstart As Integer = fieldName.LastIndexOf("].")
                fieldName = fieldName.Substring(fldstart + 2, fieldName.Length - fldstart - 2)
            End If
            Dim val As String = ""
            val = flds.GetField(fieldName) & ""
            'flds.GetFieldPositions
            Dim fldItem As iTextSharp.text.pdf.AcroFields.Item = flds.GetFieldItem(fieldName)
            Dim fldType As Integer = flds.GetFieldType(fieldName) + 0
            If fldType = iTextSharp.text.pdf.AcroFields.FIELD_TYPE_TEXT Then
                'strHTMLFormFields &= "<input type='text' name='" & fieldName & "' value='" & val & "' />"
                myFields.Add(fieldName, New HTMLFormField(fieldName, HTMLFieldType.text, val))
            ElseIf fldType = iTextSharp.text.pdf.AcroFields.FIELD_TYPE_CHECKBOX Then
                'strHTMLFormFields &= "<input type='text' name='" & fieldName & "' value='" & val & "' />"
                myFields.Add(fieldName, New HTMLFormField(fieldName, HTMLFieldType.checkbox, val))
            ElseIf fldType = iTextSharp.text.pdf.AcroFields.FIELD_TYPE_LIST Then
                Dim lstD As String(), lstV As String(), lstS As String(), intSize As Integer = 5
                lstD = flds.GetListOptionDisplay(fieldName)
                lstV = flds.GetListOptionExport(fieldName)
                lstS = flds.GetListSelection(fieldName)
                'intSize = fldItem.Size
                'strHTMLFormFields &= "<select name='" & fieldName & "'>"
                'For intLst As Integer = 0 To lstD.Length
                'strHTMLFormFields &= "<listitem value='" & lstV(intLst) & "'>" & lstD(intLst) & "</listitem>"
                'Next
                'strHTMLFormFields &= "</select>"
                myFields.Add(fieldName, New HTMLFormField(fieldName, HTMLFieldType.list, lstV, lstD, lstS, intSize))

            ElseIf fldType = iTextSharp.text.pdf.AcroFields.FIELD_TYPE_RADIOBUTTON Then
                Dim lstD As String() = {}, lstV As New ArrayList, lstS As String()
                '     For v As Integer = 0 To fldItem.Size - 1
                'val = fldItem.GetValue(v)
                ''lstV.Add(fldItem.values(v))
                '     Next
                If isXFA Then
                    ReDim Preserve lstS(0)
                    lstS = LoadXFAFieldValues(pdfBuffer, fieldName)
                Else
                    ReDim Preserve lstS(0)
                    'flds.GetListSelection
                    lstS(0) = flds.GetField(fieldName)
                End If
                Dim states() As String = flds.GetAppearanceStates(fld.Name)
                For i As Integer = 0 To states.Length - 1
                    val = states(i)
                    ReDim Preserve lstD(i)
                    'lstV.Add(val)
                    lstD(i) = val
                    'System.out.println("Possible values
                Next
                'val = flds.GetField(fieldName) & ""
                'val = lstV(0)
                'val = lstV(1)
                'lstD = lstV.ToArray
                Dim blnFound As Boolean = False
                For Each radioFld As HTMLFormField In myFields.Values
                    If radioFld.name = fieldName Then
                        blnFound = True
                        Exit For
                    End If
                Next
                If Not blnFound Then
                    myFields.Add(fieldName, New HTMLFormField(fieldName, HTMLFieldType.radio, lstD, lstD, lstS, 1))
                End If
            ElseIf Not fldType = Nothing Then

                Dim intType As Integer = flds.GetFieldType(fieldName)
                Dim lstD As String() = {}, lstV As New ArrayList, lstS As String() = {}

                For v As Integer = 0 To fldItem.Size - 1
                    'val = fldItem.GetValue(v)
                    lstV.Add(fldItem.GetValue(v))
                Next

                Dim states() As String = flds.GetAppearanceStates(fld.Name)
                For i As Integer = 0 To states.Length - 1
                    val = states(i)
                    ReDim Preserve lstD(i)
                    'lstV.Add(val)
                    lstD(i) = val
                    'System.out.println("Possible values
                Next
                If isXFA Then
                    ReDim Preserve lstS(0)
                    lstS = LoadXFAFieldValues(pdfBuffer, fieldName)

                Else
                    ReDim Preserve lstS(0)
                    'flds.GetListSelection
                    lstS(0) = flds.GetField(fieldName)
                End If

                Dim blnFound As Boolean = False
                For Each radioFld As HTMLFormField In myFields.Values
                    If radioFld.name = fieldName Then
                        blnFound = True
                        Exit For
                    End If
                Next
                If Not blnFound Then
                    If lstS.Length = 3 Then
                        If Not lstS(1) Is Nothing Then
                            lstS(1) = lstS(1).Replace("image/jpg", "image/jpeg")
                        End If
                        If isBase64(lstS(0)) Then

                            myFields.Add(fieldName, New HTMLFormField(fieldName, HTMLFieldType.image, lstS(0).ToCharArray, lstS(1), False))
                        Else
                            'myFields.Add(fieldName, New HTMLFormField(fieldName, HTMLFieldType.submit, lstV(0).ToString, lstS(1).ToString, False))
                        End If
                    ElseIf lstS.Length = 1 Then
                        ReDim Preserve lstS(2)
                        If String.IsNullOrEmpty(lstS(1) & "") Then
                            lstS(1) = "image/jpeg"
                            lstS(2) = "image"
                        End If
                        If isBase64(lstS(0)) Then
                            myFields.Add(fieldName, New HTMLFormField(fieldName, HTMLFieldType.image, lstS(0).ToCharArray, lstS(1), False))
                        Else
                            'myFields.Add(fieldName, New HTMLFormField(fieldName, HTMLFieldType.submit, lstV(0).ToString, lstS(1).ToString, False))
                        End If
                    End If
                End If
            End If
        Next

        If Not _fTitleSpaceStrings Is Nothing Then
            If Not _fTitleSpaceStrings.Length <= 0 Then
                setTitleReplaceStringsWithSpace(HTMLFields, _fTitleSpaceStrings)
            End If
        End If

        Return myFields
    End Function
    Private Function LoadPDFFormFields(ByVal pdfBuffer As Byte(), Optional ByVal ownerPassword As String = "") As Dictionary(Of String, HTMLFormField)
        Dim myFields As New Dictionary(Of String, HTMLFormField)
        Dim reader As iTextSharp.text.pdf.PdfReader
        'Dim streamRead As New System.IO.StreamReader(pdfPath, True)
        'Dim PDFBytes(streamRead.BaseStream.Length) As Byte
        'streamRead.BaseStream.Read(PDFBytes, 0, PDFBytes.Length)
        If Not pdfBuffer.Length > 0 Then
            Return Nothing
        Else
            If String.IsNullOrEmpty(ownerPassword & "") Then
                reader = New iTextSharp.text.pdf.PdfReader(pdfBuffer)
            Else
                reader = New iTextSharp.text.pdf.PdfReader(pdfBuffer, System.Text.Encoding.ASCII.GetBytes(ownerPassword))
            End If
        End If
        Dim xfaFrm As New iTextSharp.text.pdf.XfaForm(reader)

        Dim isXFA As Boolean = False
        isXFA = xfaFrm.XfaPresent

        Dim af As iTextSharp.text.pdf.PRAcroForm
        af = reader.AcroForm

        Dim fld As iTextSharp.text.pdf.PRAcroForm.FieldInformation

        Dim flds As iTextSharp.text.pdf.AcroFields = reader.AcroFields
        'Dim fields As ArrayList = af.Fields
        On Error Resume Next
        Dim strHTMLFormFields As String = ""
        For Each fld In af.Fields.ToArray()
            Dim fieldName As String = fld.Name
            If InStr(fieldName, "].") Then
                Dim fldstart As Integer = fieldName.LastIndexOf("].")
                fieldName = fieldName.Substring(fldstart + 2, fieldName.Length - fldstart - 2)
            End If
            Dim val As String = ""
            val = flds.GetField(fieldName) & ""
            'flds.GetFieldPositions
            Dim fldItem As iTextSharp.text.pdf.AcroFields.Item = flds.GetFieldItem(fieldName)
            Dim fldType As Integer = flds.GetFieldType(fieldName) + 0
            If fldType = iTextSharp.text.pdf.AcroFields.FIELD_TYPE_TEXT Then
                'strHTMLFormFields &= "<input type='text' name='" & fieldName & "' value='" & val & "' />"
                myFields.Add(fieldName, New HTMLFormField(fieldName, HTMLFieldType.text, val))
            ElseIf fldType = iTextSharp.text.pdf.AcroFields.FIELD_TYPE_CHECKBOX Then
                'strHTMLFormFields &= "<input type='text' name='" & fieldName & "' value='" & val & "' />"
                myFields.Add(fieldName, New HTMLFormField(fieldName, HTMLFieldType.checkbox, val))
            ElseIf fldType = iTextSharp.text.pdf.AcroFields.FIELD_TYPE_LIST Then
                Dim lstD As String(), lstV As String(), lstS As String(), intSize As Integer = 5
                lstD = flds.GetListOptionDisplay(fieldName)
                lstV = flds.GetListOptionExport(fieldName)
                lstS = flds.GetListSelection(fieldName)
                'intSize = fldItem.Size
                'strHTMLFormFields &= "<select name='" & fieldName & "'>"
                'For intLst As Integer = 0 To lstD.Length
                'strHTMLFormFields &= "<listitem value='" & lstV(intLst) & "'>" & lstD(intLst) & "</listitem>"
                'Next
                'strHTMLFormFields &= "</select>"
                myFields.Add(fieldName, New HTMLFormField(fieldName, HTMLFieldType.list, lstV, lstD, lstS, intSize))

            ElseIf fldType = iTextSharp.text.pdf.AcroFields.FIELD_TYPE_RADIOBUTTON Then
                Dim lstD As String() = {}, lstV As New ArrayList, lstS As String()
                '     For v As Integer = 0 To fldItem.Size - 1
                'val = fldItem.GetValue(v)
                ''lstV.Add(fldItem.values(v))
                '     Next
                If isXFA Then
                    ReDim Preserve lstS(0)
                    lstS = LoadXFAFieldValues(pdfBuffer, fieldName)
                Else
                    ReDim Preserve lstS(0)
                    'flds.GetListSelection
                    lstS(0) = flds.GetField(fieldName)
                End If
                Dim states() As String = flds.GetAppearanceStates(fld.Name)
                For i As Integer = 0 To states.Length - 1
                    val = states(i)
                    ReDim Preserve lstD(i)
                    'lstV.Add(val)
                    lstD(i) = val
                    'System.out.println("Possible values
                Next
                'val = flds.GetField(fieldName) & ""
                'val = lstV(0)
                'val = lstV(1)
                'lstD = lstV.ToArray
                Dim blnFound As Boolean = False
                For Each radioFld As HTMLFormField In myFields.Values
                    If radioFld.name = fieldName Then
                        blnFound = True
                        Exit For
                    End If
                Next
                If Not blnFound Then
                    myFields.Add(fieldName, New HTMLFormField(fieldName, HTMLFieldType.radio, lstD, lstD, lstS, 1))
                End If
            ElseIf Not fldType = Nothing Then

                Dim intType As Integer = flds.GetFieldType(fieldName)
                Dim lstD As String() = {}, lstV As New ArrayList, lstS As String() = {}

                For v As Integer = 0 To fldItem.Size - 1
                    'val = fldItem.GetValue(v)
                    lstV.Add(fldItem.GetValue(v))
                Next

                Dim states() As String = flds.GetAppearanceStates(fld.Name)
                For i As Integer = 0 To states.Length - 1
                    val = states(i)
                    ReDim Preserve lstD(i)
                    'lstV.Add(val)
                    lstD(i) = val
                    'System.out.println("Possible values
                Next
                If isXFA Then
                    ReDim Preserve lstS(0)
                    lstS = LoadXFAFieldValues(pdfBuffer, fieldName)

                Else
                    ReDim Preserve lstS(0)
                    'flds.GetListSelection
                    lstS(0) = flds.GetField(fieldName)
                End If

                Dim blnFound As Boolean = False
                For Each radioFld As HTMLFormField In myFields.Values
                    If radioFld.name = fieldName Then
                        blnFound = True
                        Exit For
                    End If
                Next
                Dim strButtons() As String
                If Not blnFound Then
                    If lstS.Length = 3 Then
                        If Not lstS(1) Is Nothing Then
                            lstS(1) = lstS(1).Replace("image/jpg", "image/jpeg")
                        End If
                        If isBase64(lstS(0)) Then

                            myFields.Add(fieldName, New HTMLFormField(fieldName, HTMLFieldType.image, lstS(0).ToCharArray, lstS(1), False))
                        Else
                            'myFields.Add(fieldName, New HTMLFormField(fieldName, HTMLFieldType.submit, lstV(0).ToString, lstS(1).ToString, False))
                        End If
                    ElseIf lstS.Length = 1 Then
                        ReDim Preserve lstS(2)
                        If String.IsNullOrEmpty(lstS(1) & "") Then
                            lstS(1) = "image/jpeg"
                            lstS(2) = "image"
                        End If
                        If isBase64(lstS(0)) Then
                            myFields.Add(fieldName, New HTMLFormField(fieldName, HTMLFieldType.image, lstS(0).ToCharArray, lstS(1), False))
                        Else
                            'myFields.Add(fieldName, New HTMLFormField(fieldName, HTMLFieldType.submit, lstV(0).ToString, lstS(1).ToString, False))
                        End If
                    End If
                End If
            End If
        Next
        Return myFields
    End Function
    Private Function LoadPDFFormFields(ByVal pdfBuffer As Byte(), Optional ByVal ownerPassword As Byte() = Nothing) As Dictionary(Of String, HTMLFormField)
        Dim myFields As New Dictionary(Of String, HTMLFormField)
        Dim reader As iTextSharp.text.pdf.PdfReader
        'Dim streamRead As New System.IO.StreamReader(pdfPath, True)
        'Dim PDFBytes(streamRead.BaseStream.Length) As Byte
        'streamRead.BaseStream.Read(PDFBytes, 0, PDFBytes.Length)
        If Not pdfBuffer.Length > 0 Then
            Return Nothing
        Else
            If (ownerPassword) Is Nothing Then
                reader = New iTextSharp.text.pdf.PdfReader(pdfBuffer)
            Else
                reader = New iTextSharp.text.pdf.PdfReader(pdfBuffer, (ownerPassword))
            End If
        End If
        Dim xfaFrm As New iTextSharp.text.pdf.XfaForm(reader)

        Dim isXFA As Boolean = False
        isXFA = xfaFrm.XfaPresent

        Dim af As iTextSharp.text.pdf.PRAcroForm
        af = reader.AcroForm

        Dim fld As iTextSharp.text.pdf.PRAcroForm.FieldInformation

        Dim flds As iTextSharp.text.pdf.AcroFields = reader.AcroFields
        'Dim fields As ArrayList = af.Fields
        On Error Resume Next
        Dim strHTMLFormFields As String = ""
        For Each fld In af.Fields.ToArray()
            Dim fieldName As String = fld.Name
            If InStr(fieldName, "].") Then
                Dim fldstart As Integer = fieldName.LastIndexOf("].")
                fieldName = fieldName.Substring(fldstart + 2, fieldName.Length - fldstart - 2)
            End If
            Dim val As String = ""
            val = flds.GetField(fieldName) & ""
            'flds.GetFieldPositions
            Dim fldItem As iTextSharp.text.pdf.AcroFields.Item = flds.GetFieldItem(fieldName)
            Dim fldType As Integer = flds.GetFieldType(fieldName) + 0
            If fldType = iTextSharp.text.pdf.AcroFields.FIELD_TYPE_TEXT Then
                'strHTMLFormFields &= "<input type='text' name='" & fieldName & "' value='" & val & "' />"
                myFields.Add(fieldName, New HTMLFormField(fieldName, HTMLFieldType.text, val))
            ElseIf fldType = iTextSharp.text.pdf.AcroFields.FIELD_TYPE_CHECKBOX Then
                'strHTMLFormFields &= "<input type='text' name='" & fieldName & "' value='" & val & "' />"
                myFields.Add(fieldName, New HTMLFormField(fieldName, HTMLFieldType.checkbox, val))
            ElseIf fldType = iTextSharp.text.pdf.AcroFields.FIELD_TYPE_LIST Then
                Dim lstD As String(), lstV As String(), lstS As String(), intSize As Integer = 5
                lstD = flds.GetListOptionDisplay(fieldName)
                lstV = flds.GetListOptionExport(fieldName)
                lstS = flds.GetListSelection(fieldName)
                'intSize = fldItem.Size
                'strHTMLFormFields &= "<select name='" & fieldName & "'>"
                'For intLst As Integer = 0 To lstD.Length
                'strHTMLFormFields &= "<listitem value='" & lstV(intLst) & "'>" & lstD(intLst) & "</listitem>"
                'Next
                'strHTMLFormFields &= "</select>"
                myFields.Add(fieldName, New HTMLFormField(fieldName, HTMLFieldType.list, lstV, lstD, lstS, intSize))

            ElseIf fldType = iTextSharp.text.pdf.AcroFields.FIELD_TYPE_RADIOBUTTON Then
                Dim lstD As String() = {}, lstV As New ArrayList, lstS As String()
                '     For v As Integer = 0 To fldItem.Size - 1
                'val = fldItem.GetValue(v)
                ''lstV.Add(fldItem.values(v))
                '     Next
                If isXFA Then
                    ReDim Preserve lstS(0)
                    lstS = LoadXFAFieldValues(pdfBuffer, fieldName)
                Else
                    ReDim Preserve lstS(0)
                    'flds.GetListSelection
                    lstS(0) = flds.GetField(fieldName)
                End If
                Dim states() As String = flds.GetAppearanceStates(fld.Name)
                For i As Integer = 0 To states.Length - 1
                    val = states(i)
                    ReDim Preserve lstD(i)
                    'lstV.Add(val)
                    lstD(i) = val
                    'System.out.println("Possible values
                Next
                'val = flds.GetField(fieldName) & ""
                'val = lstV(0)
                'val = lstV(1)
                'lstD = lstV.ToArray
                Dim blnFound As Boolean = False
                For Each radioFld As HTMLFormField In myFields.Values
                    If radioFld.name = fieldName Then
                        blnFound = True
                        Exit For
                    End If
                Next
                If Not blnFound Then
                    myFields.Add(fieldName, New HTMLFormField(fieldName, HTMLFieldType.radio, lstD, lstD, lstS, 1))
                End If
            ElseIf Not fldType = Nothing Then

                Dim intType As Integer = flds.GetFieldType(fieldName)
                Dim lstD As String() = {}, lstV As New ArrayList, lstS As String() = {}

                For v As Integer = 0 To fldItem.Size - 1
                    'val = fldItem.GetValue(v)
                    lstV.Add(fldItem.GetValue(v))
                Next

                Dim states() As String = flds.GetAppearanceStates(fld.Name)
                For i As Integer = 0 To states.Length - 1
                    val = states(i)
                    ReDim Preserve lstD(i)
                    'lstV.Add(val)
                    lstD(i) = val
                    'System.out.println("Possible values
                Next
                If isXFA Then
                    ReDim Preserve lstS(0)
                    lstS = LoadXFAFieldValues(pdfBuffer, fieldName)

                Else
                    ReDim Preserve lstS(0)
                    'flds.GetListSelection
                    lstS(0) = flds.GetField(fieldName)
                End If

                Dim blnFound As Boolean = False
                For Each radioFld As HTMLFormField In myFields.Values
                    If radioFld.name = fieldName Then
                        blnFound = True
                        Exit For
                    End If
                Next
                Dim strButtons() As String
                If Not blnFound Then
                    If lstS.Length = 3 Then
                        If Not lstS(1) Is Nothing Then
                            lstS(1) = lstS(1).Replace("image/jpg", "image/jpeg")
                        End If
                        If isBase64(lstS(0)) Then

                            myFields.Add(fieldName, New HTMLFormField(fieldName, HTMLFieldType.image, lstS(0).ToCharArray, lstS(1), False))
                        Else
                            'myFields.Add(fieldName, New HTMLFormField(fieldName, HTMLFieldType.submit, lstV(0).ToString, lstS(1).ToString, False))
                        End If
                    ElseIf lstS.Length = 1 Then
                        ReDim Preserve lstS(2)
                        If String.IsNullOrEmpty(lstS(1) & "") Then
                            lstS(1) = "image/jpeg"
                            lstS(2) = "image"
                        End If
                        If isBase64(lstS(0)) Then
                            myFields.Add(fieldName, New HTMLFormField(fieldName, HTMLFieldType.image, lstS(0).ToCharArray, lstS(1), False))
                        Else
                            'myFields.Add(fieldName, New HTMLFormField(fieldName, HTMLFieldType.submit, lstV(0).ToString, lstS(1).ToString, False))
                        End If
                    End If
                End If
            End If
        Next
        Return myFields
    End Function
    Public Function GetPushButtonFieldNames(ByVal PDF As Byte()) As String()
        Dim reader As iTextSharp.text.pdf.PdfReader
        reader = New iTextSharp.text.pdf.PdfReader(PDF)
        Dim xfaFrm As New iTextSharp.text.pdf.XfaForm(reader)
        Dim isXFA As Boolean = False
        isXFA = xfaFrm.XfaPresent
        Dim af As iTextSharp.text.pdf.PRAcroForm
        af = reader.AcroForm
        Dim fld As iTextSharp.text.pdf.PRAcroForm.FieldInformation
        Dim flds As iTextSharp.text.pdf.AcroFields = reader.AcroFields
        'Dim fields As ArrayList = af.Fields
        On Error Resume Next
        Dim FieldNames As New System.Collections.Generic.List(Of String)
        For Each fld In af.Fields.ToArray()
            Dim fieldName As String = fld.Name
            If InStr(fieldName, "].") Then
                Dim fldstart As Integer = fieldName.LastIndexOf("].")
                fieldName = fieldName.Substring(fldstart + 2, fieldName.Length - fldstart - 2)
            End If
            Dim val As String = ""
            val = flds.GetField(fieldName) & ""
            If flds.GetFieldType(fieldName) = iTextSharp.text.pdf.AcroFields.FIELD_TYPE_PUSHBUTTON Then
                FieldNames.Add(fieldName)
            End If
        Next
        Return FieldNames.ToArray
    End Function
    Public Function GetSubmitFieldsList(ByVal pdfBuffer As Byte(), Optional ByVal ownerPassword As String = "") As List(Of String)
        'Dim myFields As New Dictionary(Of String, HTMLFormField)
        Dim reader As iTextSharp.text.pdf.PdfReader
        'Dim streamRead As New System.IO.StreamReader(pdfPath, True)
        'Dim PDFBytes(streamRead.BaseStream.Length) As Byte
        'streamRead.BaseStream.Read(PDFBytes, 0, PDFBytes.Length)
        If Not pdfBuffer.Length > 0 Then
            Return Nothing
        Else
            If String.IsNullOrEmpty(ownerPassword & "") Then
                reader = New iTextSharp.text.pdf.PdfReader(pdfBuffer)
            Else
                reader = New iTextSharp.text.pdf.PdfReader(pdfBuffer, System.Text.Encoding.Default.GetBytes(ownerPassword))
            End If
        End If
        Dim xfaFrm As New iTextSharp.text.pdf.XfaForm(reader)

        Dim isXFA As Boolean = False
        isXFA = xfaFrm.XfaPresent

        Dim af As iTextSharp.text.pdf.PRAcroForm
        af = reader.AcroForm

        Dim fld As iTextSharp.text.pdf.PRAcroForm.FieldInformation

        Dim flds As iTextSharp.text.pdf.AcroFields = reader.AcroFields
        'Dim fields As ArrayList = af.Fields
        On Error Resume Next
        Dim strHTMLFormFields As String = ""
        Dim pushButtonFieldName As New List(Of String)
        For Each fld In af.Fields.ToArray()
            Dim fieldName As String = fld.Name
            If InStr(fieldName, "].") Then
                Dim fldstart As Integer = fieldName.LastIndexOf("].")
                fieldName = fieldName.Substring(fldstart + 2, fieldName.Length - fldstart - 2)
            End If
            Dim val As String = ""
            val = flds.GetField(fieldName) & ""
            'flds.GetFieldPositions
            Dim fldItem As iTextSharp.text.pdf.AcroFields.Item = flds.GetFieldItem(fieldName)
            If flds.GetFieldType(fieldName) = iTextSharp.text.pdf.AcroFields.FIELD_TYPE_PUSHBUTTON Then
                'strHTMLFormFields &= "<input type='text' name='" & fieldName & "' value='" & val & "' />"
                'myFields.Add(fieldName, New HTMLFormField(fieldName, HTMLFieldType.text, val))
                pushButtonFieldName.Add(fieldName)
            End If
        Next
        Return pushButtonFieldName
    End Function
    Private Function LoadXFAFieldValues(ByVal PDFBuffer As Byte(), ByVal fName As String) As String()
        'PDFBuffer = _defaultEncoding.GetBytes(ByteArrayToString(PDFBuffer))
        Dim reader As New iTextSharp.text.pdf.PdfReader(PDFBuffer)
        Dim xfaFrm As New iTextSharp.text.pdf.XfaForm(reader)

        Dim isXFA As Boolean = False
        isXFA = xfaFrm.XfaPresent
        If Not isXFA Then
            reader.Close()
            reader = Nothing
            xfaFrm = Nothing
            'GC.Collect()
            Return Nothing
            Exit Function
        End If

        On Error Resume Next
        Dim FileNameorURL As String = ""
        Dim xData As iTextSharp.text.pdf.XfaForm.Xml2SomDatasets
        Dim xForm As iTextSharp.text.pdf.XfaForm.Xml2SomTemplate
        xData = xfaFrm.DatasetsSom
        Dim xmlString As String = xfaFrm.DomDocument.InnerXml
        Dim FilePath As String = ""
        Dim fpathStart As Integer = xmlString.IndexOf("<output>") + 8
        FilePath = xmlString.Substring(xmlString.IndexOf("<uri>", fpathStart) + 5, xmlString.IndexOf("</uri>", fpathStart) - (xmlString.IndexOf("<uri>", fpathStart) + 5))
        If Not FilePath = "" Then
            FileNameorURL = FilePath
        End If

        'Dim nodeLst As XmlAttributeCollection = xFormNodes.Attributes
        'For Each xNode As XmlAttribute In nodeLst
        '    Dim x As String = xNode.Name
        'Next


        Dim xFld As Dictionary(Of String, Xml.XmlNode) = xData.Name2Node
        Dim formName(1) As String
        Dim Val As String() = {""}
        For Each xString As String In xFld.Keys
            Dim fieldName As String = xString & "" 'xString.Key
            Dim fld As Xml.XmlElement
            fld = xData.Name2Node(fieldName)
            Dim form As Xml.XmlElement
            form = fld.ParentNode
            Dim short_fieldName As String = ""
            Dim FormFieldNames() As String = CStr(xString).Split(".")
            If FormFieldNames.Length >= 2 Then
                formName(0) = FormFieldNames(0)
                'formName(0) = form.Name
                short_fieldName = FormFieldNames(FormFieldNames.Length - 1)
                formName(0) = form.Name
                'short_fieldName = fld.Name
            End If
            If Not short_fieldName = fName Then
                GoTo nextField
            End If
            'formName(0) = form.Name
            'short_fieldName = fld.Name

            Dim strHref As String = ""
            If Not String.IsNullOrEmpty(fld.InnerText & "") Then
                Val(0) = fld.InnerText
                If fld.HasAttribute("xfa:contentType") Then
                    Val = New String() {Val(0), fld.Attributes("xfa:contentType").Value.ToString(), "image"}
                End If
            Else
                If fld.HasAttribute("href") Then
                    Val(0) = fld.Attributes("href").Value.ToString()
                End If
                If fld.HasAttribute("image/mime") Then
                    Val = New String() {Val(0), fld.Attributes("image/mime").Value.ToString(), "image"}
                End If
            End If

            formName(1) = formName(0)

            If Not Val Is Nothing Then
                'FDFDox.XDPAddField(short_fieldName, Val, formName(0), FDFDoc_Class.FieldType.FldTextual, True, False)
                Return Val
            Else
                'Dim str As String = fld.ReadElementContentAsString
                'FDFDox.XDPAddField(short_fieldName, "", formName(0), FDFDoc_Class.FieldType.FldTextual, True, False)
                Return New String() {}
            End If
nextField:
        Next
        Return New String() {}
    End Function
    Private Function isBase64(ByVal strBase64 As String) As Boolean
        Dim strBld As New StringBuilder(strBase64)
        Dim imgBytes() As Byte
        Try
            imgBytes = System.Convert.FromBase64String(strBld.ToString)
        Catch ex As Exception
            imgBytes = Nothing
        End Try
        If imgBytes Is Nothing Then
            strBld = Nothing
            Return False
        Else
            If imgBytes.Length = 0 Then
                Return False
            End If
            Try
                Dim mImg As New System.IO.MemoryStream(imgBytes)
                If mImg.CanSeek Then
                    mImg.Position = 0
                End If
                Dim img As System.Drawing.Image = System.Drawing.Image.FromStream(mImg, True, True)
                Dim bm As New System.Drawing.Bitmap(img)
                'Return True
                If Not img Is Nothing And Not bm Is Nothing Then
                    imgBytes = Nothing
                    strBld = Nothing
                    img.Dispose()
                    bm.Dispose()
                    Return True
                End If
                img.Dispose()
                bm.Dispose()
            Catch ex As Exception
                Err.Clear()
            End Try
            imgBytes = Nothing
            strBld = Nothing
            Return False
        End If
    End Function
    Private Function LoadXFAFormFields(ByVal pdfPath As String, Optional ByVal ownerPassword As String = "") As HTMLFormField()
        Dim myFields As New System.Collections.Generic.List(Of HTMLFormField)
        Dim reader As iTextSharp.text.pdf.PdfReader
        If String.IsNullOrEmpty(ownerPassword) Then
            reader = New iTextSharp.text.pdf.PdfReader(pdfPath)
        Else
            reader = New iTextSharp.text.pdf.PdfReader(pdfPath, System.Text.Encoding.Default.GetBytes(ownerPassword))
        End If
        Dim af As iTextSharp.text.pdf.PRAcroForm
        af = reader.AcroForm

        Dim fld As iTextSharp.text.pdf.PRAcroForm.FieldInformation

        Dim flds As iTextSharp.text.pdf.AcroFields = reader.AcroFields
        'Dim fields As ArrayList = af.Fields
        On Error Resume Next
        Dim strHTMLFormFields As String = ""
        For Each fld In af.Fields.ToArray()
            Dim fieldName As String = fld.Name
            If InStr(fieldName, "].") Then
                Dim fldstart As Integer = fieldName.LastIndexOf("].")
                fieldName = fieldName.Substring(fldstart + 2, fieldName.Length - fldstart - 2)
            End If
            Dim val As String = ""
            val = flds.GetField(fieldName) & ""
            'flds.GetFieldPositions
            Dim fldItem As iTextSharp.text.pdf.AcroFields.Item = flds.GetFieldItem(fieldName)
            If flds.GetFieldType(fieldName) = iTextSharp.text.pdf.AcroFields.FIELD_TYPE_TEXT Then
                'strHTMLFormFields &= "<input type='text' name='" & fieldName & "' value='" & val & "' />"
                myFields.Add(New HTMLFormField(fieldName, HTMLFieldType.text, val))
            ElseIf flds.GetFieldType(fieldName) = iTextSharp.text.pdf.AcroFields.FIELD_TYPE_CHECKBOX Then
                'strHTMLFormFields &= "<input type='text' name='" & fieldName & "' value='" & val & "' />"
                myFields.Add(New HTMLFormField(fieldName, HTMLFieldType.checkbox, val))
            ElseIf flds.GetFieldType(fieldName) = iTextSharp.text.pdf.AcroFields.FIELD_TYPE_LIST Then
                Dim lstD As String(), lstV As String(), lstS As String(), intSize As Integer = 5
                lstD = flds.GetListOptionDisplay(fieldName)
                lstV = flds.GetListOptionExport(fieldName)
                lstS = flds.GetListSelection(fieldName)
                'intSize = fldItem.Size
                'strHTMLFormFields &= "<select name='" & fieldName & "'>"
                'For intLst As Integer = 0 To lstD.Length
                'strHTMLFormFields &= "<listitem value='" & lstV(intLst) & "'>" & lstD(intLst) & "</listitem>"
                'Next
                'strHTMLFormFields &= "</select>"
                myFields.Add(New HTMLFormField(fieldName, HTMLFieldType.list, lstV, lstD, lstS, intSize))
            ElseIf flds.GetFieldType(fieldName) = iTextSharp.text.pdf.AcroFields.FIELD_TYPE_COMBO Then
                Dim lstD As String(), lstV As String(), lstS As String() = {}
                lstD = flds.GetListOptionDisplay(fieldName)
                lstV = flds.GetListOptionExport(fieldName)
                ReDim Preserve lstS(0)
                'flds.GetListSelection
                lstS(0) = flds.GetField(fieldName)
                'strHTMLFormFields &= "<select name='" & fieldName & "'>"
                'For intLst As Integer = 0 To lstD.Length
                'strHTMLFormFields &= "<listitem value='" & lstV(intLst) & "'>" & lstD(intLst) & "</listitem>"
                'Next
                'strHTMLFormFields &= "</select>"
                myFields.Add(New HTMLFormField(fieldName, HTMLFieldType.combo, lstV, lstD, lstS, 1))
            ElseIf flds.GetFieldType(fieldName) = iTextSharp.text.pdf.AcroFields.FIELD_TYPE_RADIOBUTTON Then
                Dim lstD As String() = {}, lstV As New ArrayList
                '     For v As Integer = 0 To fldItem.Size - 1
                'val = fldItem.GetValue(v)
                ''lstV.Add(fldItem.values(v))
                '     Next
                Dim states() As String = flds.GetAppearanceStates(fld.Name)
                For i As Integer = 0 To states.Length - 1
                    val = states(i)
                    ReDim Preserve lstD(i)
                    'lstV.Add(val)
                    lstD(i) = val
                    'System.out.println("Possible values
                Next
                'val = flds.GetField(fieldName) & ""
                'val = lstV(0)
                'val = lstV(1)
                'lstD = lstV.ToArray
                Dim blnFound As Boolean = False
                For Each radioFld As HTMLFormField In myFields
                    If radioFld.name = fieldName Then
                        blnFound = True
                        Exit For
                    End If
                Next
                If Not blnFound Then
                    myFields.Add(New HTMLFormField(fieldName, HTMLFieldType.radio, lstD))
                End If
            ElseIf Not flds.GetFieldType(fieldName) = Nothing Or flds.GetFieldType(fieldName) = iTextSharp.text.pdf.AcroFields.FIELD_TYPE_PUSHBUTTON Or flds.GetFieldType(fieldName) = iTextSharp.text.pdf.AcroFields.FIELD_TYPE_SIGNATURE Then

                Dim intType As Integer = flds.GetFieldType(fieldName)
                Dim lstD As String() = {}, lstV As New ArrayList

                For v As Integer = 0 To fldItem.Size - 1
                    'val = fldItem.GetValue(v)
                    lstV.Add(fldItem.GetValue(v))
                Next

                Dim states() As String = flds.GetAppearanceStates(fld.Name)
                For i As Integer = 0 To states.Length - 1
                    val = states(i)
                    ReDim Preserve lstD(i)
                    'lstV.Add(val)
                    lstD(i) = val
                    'System.out.println("Possible values
                Next
                Dim blnFound As Boolean = False
                For Each radioFld As HTMLFormField In myFields
                    If radioFld.name = fieldName Then
                        blnFound = True
                        Exit For
                    End If
                Next
                If Not blnFound Then
                    If isBase64(lstV(0)) Then
                        myFields.Add(New HTMLFormField(fieldName, HTMLFieldType.image, lstV(0).ToString.ToCharArray(), "image/jpeg", False))
                        'myFields.Add(New HTMLFormField(fieldName, HTMLFieldType.image, lstV(0).ToCharArray, "image/jpeg", False))
                        'myFields.Add(New HTMLFormField(fieldName, HTMLFieldType.image, lstV(0).ToString))
                    Else
                        'myFields.Add(New HTMLFormField(fieldName, HTMLFieldType.image, val))
                    End If
                End If
            End If
        Next
        Return myFields.ToArray
    End Function

    Private Function contains(ByVal arr As String(), ByVal val As String) As Boolean
        For Each strVal As String In arr
            If strVal = val Then
                Return True
                Exit Function
            End If
        Next
        Return False
    End Function
    Private Function contains(ByVal arr As String(), ByVal val As String()) As Boolean
        For Each strVal As String In arr
            For Each strVal2 As String In val
                If strVal = strVal2 Then
                    Return True
                    Exit Function
                End If
            Next
        Next
        Return False
    End Function
    Private Function contains(ByVal val As String, ByVal arr As String()) As Boolean
        For Each strVal As String In arr
            If strVal = val Then
                Return True
                Exit Function
            End If
        Next
        Return False
    End Function
    Private Function contains(ByVal arr As String, ByVal val As String) As Boolean
        If arr = val Then
            Return True
            Exit Function
        End If
        Return False
    End Function
    Public Function BytesToBase64(ByVal bytes() As Byte) As String
        If bytes.Length <= 0 Then
            Return Nothing
        End If
        Try
            Return Convert.ToBase64String(bytes, 0, bytes.Length, Base64FormattingOptions.None)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Base64ToBytes(ByVal base64String As String) As Byte()
        If isBase64(base64String) Then
            Return Nothing
        End If
        Try
            Return Convert.FromBase64String(base64String)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Base64ToBytes(ByVal base64CharArray() As Char) As Byte()
        If isBase64(base64CharArray.ToString) Then
            Return Nothing
        End If
        Try
            Return Convert.FromBase64CharArray(base64CharArray, 0, base64CharArray.Length)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ImageLocal2Base64(ByVal path As System.IO.Path) As String
        Dim fs As New System.IO.FileStream(path.ToString, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
        Dim strFile As New System.IO.MemoryStream(System.Text.Encoding.Default.GetBytes(New System.IO.StreamReader(fs).ReadToEnd))
        Dim byteFile() As Byte = strFile.GetBuffer
        If byteFile.Length > 0 Then
            Return BytesToBase64(byteFile)
        End If
        Return Nothing
    End Function

    Public Function ImageLocal2Base64(ByVal path As String) As String
        Dim fs As New System.IO.FileStream(path.ToString, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
        Dim strFile As New System.IO.MemoryStream(System.Text.Encoding.Default.GetBytes(New System.IO.StreamReader(fs).ReadToEnd))
        Dim byteFile() As Byte = strFile.GetBuffer
        If byteFile.Length > 0 Then
            Return BytesToBase64(byteFile)
        End If
        Return Nothing
    End Function
    Public Function ImageWeb2Base64(ByVal url As Uri) As String
        'Dim wClient As New System.Net.WebClient()
        Dim byteFile() As Byte = GetUsedBytesOnly(url.ToString)
        If byteFile.Length > 0 Then
            Return BytesToBase64(byteFile)
        End If
        Return Nothing
    End Function

#Region "STREAMS"
    Private Function SerializeObject(ByVal obj As Object)
        Dim s As XmlSerializer
        's = New XmlSerializer(GetType(Web_User_Experience))
        s = New XmlSerializer(obj.GetType)
        Dim memStream As New System.IO.MemoryStream
        s.Serialize(memStream, obj)
        memStream.Position = 0
        Return memStream
    End Function
    Private Function SerializeObjectXML(ByVal obj As Object)
        Dim s As XmlSerializer, XMLDoc As New XmlDocument
        s = New XmlSerializer(obj.GetType)
        Dim memStream As New System.IO.MemoryStream
        s.Serialize(memStream, obj)
        Dim strXML As String, bytes(memStream.Length) As Byte
        memStream.Read(bytes, 0, memStream.Length)
        strXML = Encoding.Default.GetString(bytes)
        XMLDoc.LoadXml(strXML)
        Return XMLDoc
    End Function
    Private Sub Serialize_ObjectOutputStream(ByVal obj As Object, ByRef pageStream As System.IO.Stream)
        Dim s As XmlSerializer
        s = New XmlSerializer(obj.GetType)
        Dim memStream As New System.IO.MemoryStream
        s.Serialize(memStream, obj)
        memStream.Position = 0
        'OutPutBufferToStream(memStream.GetBuffer, pageStream)
    End Sub
#End Region

#Region " IDisposable Support "
    'Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    'Protected Overridable Sub Dispose(ByVal disposing As Boolean)
    '    If Not Me.disposedValue Then
    '        If disposing Then
    '            ' TODO: free other state (managed objects).
    '        End If

    '        ' TODO: free your own state (unmanaged objects).
    '        ' TODO: set large fields to null.
    '    End If
    '    Me.disposedValue = True
    'End Sub
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    'Public Sub Dispose() Implements IDisposable.Dispose
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(True)
    '    GC.SuppressFinalize(Me)
    'End Sub
    Dim disposed As Boolean = False
    ' Instantiate a SafeHandle instance.
    Dim handle As SafeHandle = New SafeFileHandle(IntPtr.Zero, True)

    ' Public implementation of Dispose pattern callable by consumers.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    ' Protected implementation of Dispose pattern.
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If disposed Then Return

        If disposing Then
            handle.Dispose()
            ' Free any other managed objects here.
            '
        End If

        ' Free any unmanaged objects here.
        '
        disposed = True
    End Sub

#End Region

End Class
