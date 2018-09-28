Imports System.Runtime.CompilerServices

Module StringExtensions

    <Extension()>
    Public Function toBytes(ByVal aString As System.String) As System.Byte()
        Return System.Text.Encoding.UTF8.GetBytes(aString)
    End Function
    <Extension()>
    Public Function isNullOrEmpty(ByVal aString As System.String) As System.Boolean
        Try
            If String.IsNullOrEmpty(aString & "") Then
                Return True
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
        Return False
    End Function
    <Extension()>
    Public Function toBase64Bytes(ByVal aString As System.String) As System.Byte()
        If aString.isNullOrEmpty() Then
            Return Nothing
        End If
        Return System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(aString), Base64FormattingOptions.None).toBytesUTF8()
    End Function
    <Extension()>
    Public Function toBase64Bytes(ByVal aString As System.String, encoding As System.Text.Encoding) As System.Byte()
        If aString.isNullOrEmpty() Then
            Return Nothing
        End If
        Return System.Convert.ToBase64String(encoding.GetBytes(aString), Base64FormattingOptions.None).toBytes(encoding)
    End Function
    <Extension()>
    Public Function toBase64String(ByVal aString As System.String) As System.String
        If aString.isNullOrEmpty() Then
            Return Nothing
        End If
        Return System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(aString))
    End Function
    <Extension()>
    Public Function toBase64String(ByVal aString As System.String, encoding As System.Text.Encoding) As System.String
        If aString.isNullOrEmpty() Then
            Return Nothing
        End If
        Return System.Convert.ToBase64String(encoding.GetBytes(aString), Base64FormattingOptions.None)
    End Function
    <Extension()>
    Public Function toBytesPdfOwnerPassword(ByVal aString As System.String) As System.Byte()
        If aString.isNullOrEmpty() Then
            Return Nothing
        End If
        Return System.Text.Encoding.ASCII.GetBytes(aString)
    End Function
    <Extension()>
    Public Function toBytes(ByVal aString As System.String, encoding As System.Text.Encoding) As System.Byte()
        If aString.isNullOrEmpty() Then
            Return Nothing
        End If
        Return encoding.GetBytes(aString)
    End Function
    <Extension()>
    Public Function toBytesUnicode(ByVal aString As System.String) As System.Byte()
        If aString.isNullOrEmpty() Then
            Return Nothing
        End If
        Return System.Text.Encoding.Unicode.GetBytes(aString)
    End Function
    <Extension()>
    Public Function toBytesUTF8(ByVal aString As System.String) As System.Byte()
        If aString.isNullOrEmpty() Then
            Return Nothing
        End If
        Return System.Text.Encoding.UTF8.GetBytes(aString)
    End Function
    <Extension()>
    Public Function toBytesASCII(ByVal aString As System.String) As System.Byte()
        If aString.isNullOrEmpty() Then
            Return Nothing
        End If
        Return System.Text.Encoding.ASCII.GetBytes(aString)
    End Function
End Module
