Public Class clsLogs
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 Nicholas Kowalewicz All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>

    Public ThrowErrors As Boolean = True
    Public Sub ThrowError(ByVal err As Exception)
        If ThrowErrors Then
            Throw err
        End If
    End Sub
    Public Class FDFError
        Public FDFException As Exception = Nothing
        Public FDFError_Name As String = ""
        Public FDFError_Message As String = ""
        Public FDFError_Module As String = ""
        Public FDFError_Class As String = ""
        Public FDFError_Number As Integer = -1
        Public FDFError_Code As String = ""
        Public Sub New(ByRef ex As Exception)
            FDFException = ex
            FDFError_Name = ex.TargetSite.Name().ToString
            FDFError_Message = ex.Message
            FDFError_Module = ex.TargetSite.Module.FullyQualifiedName
            FDFError_Class = ex.TargetSite.DeclaringType.ToString
        End Sub
        Public Sub New()
        End Sub
    End Class
    Private _FDFErrors As New System.Collections.Generic.List(Of FDFError)
    Public Sub FDFAddError(ByRef ex As Exception)
        FDFErrors.Add(New FDFError(ex))
    End Sub
    Public Property FDFErrors() As System.Collections.Generic.List(Of FDFError)
        Get
            If Not _FDFErrors Is Nothing Then
                If _FDFErrors.Count > 0 Then
                    Return _FDFErrors
                End If
            End If
            _FDFErrors = New System.Collections.Generic.List(Of FDFError)
            Return _FDFErrors
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of FDFError))
            If Not value Is Nothing Then
                If value.Count > 0 Then
                    _FDFErrors = value
                    Return
                End If
            End If
            _FDFErrors = New System.Collections.Generic.List(Of FDFError)
        End Set
    End Property
    Public Function FDFHasErrors() As Boolean
        If _FDFErrors.Count > 0 Then
            Return True
        End If
        Return False
    End Function
    Public Sub ResetErrors()
        _FDFErrors = New System.Collections.Generic.List(Of FDFError)
    End Sub
    Public Function FDFErrorsStr(Optional ByVal HTMLFormat As Boolean = False) As String
        If FDFErrors Is Nothing Then
            Return ""
        End If
        Dim FDFErrorx As FDFError
        Dim retString As String
        retString = CStr(IIf(HTMLFormat, "<br>", vbNewLine)) & "FDF Errors:"
        If FDFErrors.Count <= 0 Then Return ""
        For Each FDFErrorx In FDFErrors
            retString = retString & CStr(IIf(HTMLFormat, "<br>", vbNewLine)) & vbTab & "Error: " & FDFErrorx.FDFError_Code & " - " & CStr(IIf(HTMLFormat, "<br>", vbNewLine)) & vbTab & "#: " & FDFErrorx.FDFError_Number & CStr(IIf(HTMLFormat, "<br>", vbNewLine)) & vbTab & "Module: " & FDFErrorx.FDFError_Module & CStr(IIf(HTMLFormat, "<br>", vbNewLine)) & vbTab & "Message: " & FDFErrorx.FDFError_Message & CStr(IIf(HTMLFormat, "<br>", vbNewLine))
        Next
        Return retString
    End Function
#Region " IDisposable Support "
    Private disposedValue As Boolean = False
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                _FDFErrors.Clear()
                _FDFErrors = Nothing
            End If
        End If
        Me.disposedValue = True
    End Sub
#End Region
    Public Sub New()
        _FDFErrors = New System.Collections.Generic.List(Of FDFError)
    End Sub
End Class
