Imports System.Runtime.CompilerServices

Module StringExtensions

    <Extension()>
    Public Function toBytes(ByVal aString As System.String) As System.Byte()
        Return System.Text.Encoding.Unicode.GetBytes(aString)
    End Function
    <Extension()>
    Public Function toBytes(ByVal aString As System.String, encoding As System.Text.Encoding) As System.Byte()
        Return encoding.GetBytes(aString)
    End Function
    <Extension()>
    Public Function toBytesUnicode(ByVal aString As System.String) As System.Byte()
        Return System.Text.Encoding.Unicode.GetBytes(aString)
    End Function
    <Extension()>
    Public Function toBytesUTF8(ByVal aString As System.String) As System.Byte()
        Return System.Text.Encoding.UTF8.GetBytes(aString)
    End Function
    <Extension()>
    Public Function toBytesASCII(ByVal aString As System.String) As System.Byte()
        Return System.Text.Encoding.ASCII.GetBytes(aString)
    End Function
End Module
