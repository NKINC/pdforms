Imports System.IO
Imports System.Collections
Imports System.Reflection
Imports System.Xml.Serialization
Imports System.Runtime.Serialization.Formatters
Imports System.Runtime.Serialization.Formatters.FormatterTypeStyle
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
Imports System.Text.RegularExpressions
Public Class clsFileXML
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 NK-INC.COM All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>

    Private _Databases As Database
    Private _PDFs As PDF
    Private _FilterFields As New System.Collections.Generic.List(Of FilterField)
    Private _Data_SourcePath As String = ""
    Private _frm As frmMerge = Nothing
    Private _dialog As dialogDataSource = Nothing
    Private _Output_Folder As String = ""
    Private _Output_FileName As String = ""
    Private _NameConventions As New System.Collections.Generic.List(Of String)
    Public SMTP_Authentication_Username As String = ""
    Public SMTP_Authentication_Password As String = ""
    Public SMTP_Credential_Username As String = ""
    Public SMTP_Credential_Password As String = ""
    Public SMTP_Credential_Domain As String = ""
    Public SMTP_Message_From_Name As String = ""
    Public SMTP_Message_From_Email As String = ""
    Public SMTP_Message_TO As String = ""
    Public SMTP_Message_CC As String = ""
    Public SMTP_Message_BCC As String = ""
    Public SMTP_Message_Subject As String = ""
    Public SMTP_Message_Body As String = ""
    Public SMTP_Message_AttachmentFileName As String = ""
    Public Sub SetForm(ByRef frm As frmMerge)
        _frm = frm
    End Sub
    Public Sub SetForm(ByRef frm As dialogDataSource)
        _dialog = frm
    End Sub
    Public Property Output_Folder() As String
        Get
            Return _Output_Folder
        End Get
        Set(ByVal value As String)
            _Output_Folder = value
        End Set
    End Property
    Public Property Output_Filename() As String
        Get
            Return _Output_FileName
        End Get
        Set(ByVal value As String)
            _Output_FileName = value
        End Set
    End Property
    Public Sub New()
    End Sub
    Public Sub New(ByRef frm As frmMerge)
        _frm = frm
    End Sub
    Public Sub New(ByRef frm As dialogDataSource)
        _dialog = frm
    End Sub
    Public Property Data_SourcePath() As String
        Get
            Return _Data_SourcePath
        End Get
        Set(ByVal value As String)
            _Data_SourcePath = value
        End Set
    End Property
    Public Property Data_ConnectionString() As String
        Get
            Return modSources.Database_Connection & ""
        End Get
        Set(ByVal value As String)
            modSources.Database_Connection = value
        End Set
    End Property
    Private _Data_DatabasePassword As String = ""
    Public Property Data_DatabasePassword() As String
        Get
            Return _Data_DatabasePassword
        End Get
        Set(ByVal value As String)
            _Data_DatabasePassword = value
        End Set
    End Property
    Private _Data_Username As String = ""
    Public Property Data_Username() As String
        Get
            Return _Data_Username
        End Get
        Set(ByVal value As String)
            _Data_Username = value
        End Set
    End Property
    Private _Data_Password As String = ""
    Public Property Data_Password() As String
        Get
            Return _Data_Password
        End Get
        Set(ByVal value As String)
            _Data_Password = value
        End Set
    End Property
    Public Property Tables() As Table()
        Get
            Return modSources.Tables.ToArray
        End Get
        Set(ByVal value As Table())
            modSources.Tables.Clear()
            modSources.Tables.AddRange(value)
        End Set
    End Property
    Public Property DBFields() As modSources.DBField()
        Get
            Return modSources.DBFields.ToArray
        End Get
        Set(ByVal value As modSources.DBField())
            modSources.DBFields.Clear()
            modSources.DBFields.AddRange(value)
        End Set
    End Property
    Public Property PDFFields() As modSources.PDFField()
        Get
            Return modSources.PDFFields.ToArray
        End Get
        Set(ByVal value As modSources.PDFField())
            modSources.PDFFields.Clear()
            modSources.PDFFields.AddRange(value)
        End Set
    End Property
    Public Property FieldMap() As modSources.FieldMap()
        Get
            Return modSources.FieldsMaps.ToArray
        End Get
        Set(ByVal value As modSources.FieldMap())
            modSources.FieldsMaps.Clear()
            modSources.FieldsMaps.AddRange(value)
        End Set
    End Property
    Public Property arrFieldMaps() As FieldMap()
        Get
            Return modSources.FieldsMaps.ToArray
        End Get
        Set(ByVal Value As FieldMap())
            modSources.FieldsMaps.Clear()
            modSources.FieldsMaps.AddRange(Value)
        End Set
    End Property
    Public Property NameConventions() As String()
        Get
            Return _NameConventions.ToArray
        End Get
        Set(ByVal value As String())
            _NameConventions.Clear()
            _NameConventions.AddRange(value)
        End Set
    End Property
    Public Sub NameConventionsAddArray(ByVal values() As String)
        _NameConventions.Clear()
        _NameConventions.AddRange(values)
    End Sub
    Public Sub NameConventionsAddItem(ByVal value As String)
        _NameConventions.Add(value)
    End Sub
    Public Sub NameConventionsClear()
        _NameConventions.Clear()
    End Sub
    Public Property Database_Connection() As String
        Get
            Return modSources.Database_Connection
        End Get
        Set(ByVal Value As String)
            modSources.Database_Connection = Value
        End Set
    End Property
    Public Property PDF_Source() As String
        Get
            Return modSources.PDF_Source
        End Get
        Set(ByVal Value As String)
            modSources.PDF_Source = Value
        End Set
    End Property
    Public Property FieldFilters() As FilterField()
        Get
            Return modSources.FilterFields.ToArray
        End Get
        Set(ByVal Value As FilterField())
            modSources.FilterFields.Clear()
            modSources.FilterFields.AddRange(Value)
        End Set
    End Property
    Private Property FieldMaps() As System.Collections.Generic.List(Of FieldMap)
        Get
            Return modSources.FieldsMaps
        End Get
        Set(ByVal Value As System.Collections.Generic.List(Of FieldMap))
            modSources.FieldsMaps = Value
        End Set
    End Property
    Public Sub SavePDFEmail(ByVal fp As String)
        Dim objStreamWriter As StreamWriter = Nothing
        Try
            If String.IsNullOrEmpty(fp & "") Then
                Return
            End If
            objStreamWriter = New StreamWriter(fp)
            Dim x As New XmlSerializer(Me.GetType)
            x.Serialize(objStreamWriter, Me)
        Catch ex As Exception
        Finally
            objStreamWriter.Close()
        End Try
    End Sub
    Public Sub LoadSerialized(ByVal fp As String)
        Dim objStreamWriter As StreamWriter = Nothing
        If String.IsNullOrEmpty(fp & "") Then
            Return
        End If
        If Not _frm Is Nothing Then
            Dim x As New XmlSerializer(_frm.clsFileXML1.GetType)
            Dim objStreamReader As New StreamReader(fp)
            Try
                _frm.clsFileXML1 = DirectCast(x.Deserialize(objStreamReader), clsFileXML)
            Catch e As Exception
            Finally
                objStreamReader.Close()
            End Try
        ElseIf Not _dialog Is Nothing Then
            Dim x As New XmlSerializer(_dialog.clsFileXML1.GetType)
            Dim objStreamReader As New StreamReader(fp)
            Try
                _dialog.clsFileXML1 = DirectCast(x.Deserialize(objStreamReader), clsFileXML)
            Catch e As Exception
            Finally
                objStreamReader.Close()
            End Try
        End If
    End Sub
    Public Sub LoadSerialized(ByVal fp As String, ByRef frm As frmMerge)
        _frm = frm
        Dim objStreamWriter As StreamWriter = Nothing
        If String.IsNullOrEmpty(fp & "") Then
            Return
        End If
        Dim x As New XmlSerializer(_frm.clsFileXML1.GetType)
        Dim objStreamReader As New StreamReader(fp)
        Try
            _frm.clsFileXML1 = DirectCast(x.Deserialize(objStreamReader), clsFileXML)
        Catch e As Exception
        Finally
            objStreamReader.Close()
        End Try
    End Sub
    Public Sub LoadSerialized(ByVal fp As String, ByRef frm As dialogDataSource)
        _dialog = frm
        Dim objStreamWriter As StreamWriter = Nothing
        If String.IsNullOrEmpty(fp & "") Then
            Return
        End If
        Dim x As New XmlSerializer(_dialog.clsFileXML1.GetType)
        Dim objStreamReader As New StreamReader(fp)
        Try
            _dialog.clsFileXML1 = DirectCast(x.Deserialize(objStreamReader), clsFileXML)
        Catch e As Exception
        Finally
            objStreamReader.Close()
        End Try
    End Sub
End Class
