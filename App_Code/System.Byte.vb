Imports System.Runtime.CompilerServices
Imports System.IO
Module ByteExtensions

    <Extension()>
    Function GetBit(ByVal b As Byte, ByVal bitNumber As Integer) As Boolean
        Dim bits = New BitArray({b})
        Return bits.[Get](bitNumber)
    End Function

    <Extension()>
    Function Append(ByVal first As System.Byte(), ByVal toAppend As System.Byte()) As System.Byte()
        Dim result = New System.Byte(-1) {}
        Using stream = New System.IO.MemoryStream()
            stream.Write(first, 0, first.Length)
            stream.Write(toAppend, 0, toAppend.Length)
            result = stream.ToArray()
        End Using
        Return result
    End Function
    <Extension()>
    Function toString(ByVal first As System.Byte(), encoding As System.Text.Encoding) As String
        Return encoding.GetString(first)
    End Function
    <Extension()>
    Function toStringUnicode(ByVal first As System.Byte()) As String
        Return System.Text.Encoding.Unicode.GetString(first)
    End Function
    <Extension()>
    Function toStringASCII(ByVal first As System.Byte()) As String
        Return System.Text.Encoding.ASCII.GetString(first)
    End Function
    <Extension()>
    Function toStringUTF8(ByVal first As System.Byte()) As String
        Return System.Text.Encoding.UTF8.GetString(first)
    End Function
    <Extension()>
    Function toMemoryStream(ByVal first As System.Byte()) As System.IO.MemoryStream
        Return New System.IO.MemoryStream(first)
    End Function
End Module