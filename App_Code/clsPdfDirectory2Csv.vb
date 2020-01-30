Imports FDFApp.FDFDoc_Class
Imports FDFApp.FDFApp_Class
Imports System.IO
Public Class clsPdfDirectory2Csv
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 NK-INC.COM All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>

    Public Shared Sub createCsv(ByVal directory_path As String)
        If Not String.IsNullOrEmpty(directory_path & "") Then
            For Each f As String In Directory.GetFiles(directory_path)
                If File.Exists(f) Then
                    If Path.GetExtension(f).Replace(".", "") = "pdf" Then
                        Try
                            Dim p As New FDFApp.FDFApp_Class, fdf As New FDFApp.FDFDoc_Class
                            fdf = p.PDFOpenFromFile(f, True, True, "")
                            Dim csv As String = CreateCSVRow(fdf.XDPGetAllFields().ToList())
                            File.WriteAllText(Path.GetDirectoryName(f).TrimEnd("\") & "\" & Path.GetFileNameWithoutExtension(f) & ".csv", csv.ToString, System.Text.Encoding.UTF8)
                        Catch ex As Exception
                            Err.Clear()
                        End Try
                    End If
                End If
            Next
        End If
    End Sub
    Public Shared Function CreateCSVRow(ByVal fldArray As List(Of FDFApp.FDFDoc_Class.FDFField), Optional ByVal appendHeader As Boolean = True) As String
        Dim csvCols As New List(Of String)
        Dim csvValue As String
        Dim needQuotes As Boolean
        Dim strCsv As String = ""
        If appendHeader Then
            For i As Integer = 0 To fldArray.Count - 1
                If Not String.IsNullOrEmpty(strCsv & "") Then
                    strCsv &= ","
                End If
                strCsv &= """" & fldArray(i).FieldName.ToString() & """"
            Next
        End If
        For i As Integer = 0 To fldArray.Count - 1
            csvValue = fldArray(i).FieldValue(0)
            needQuotes = (csvValue.IndexOf(",", StringComparison.InvariantCulture) >= 0 _
                          OrElse csvValue.IndexOf("""", StringComparison.InvariantCulture) >= 0 _
                          OrElse csvValue.IndexOf(vbCrLf, StringComparison.InvariantCulture) >= 0)
            csvValue = csvValue.Replace("""", """""")
            csvCols.Add(If(needQuotes, """" & csvValue & """", csvValue))
        Next
        If appendHeader Then
            strCsv &= Environment.NewLine
        End If
        strCsv &= String.Join(",", csvCols.ToArray())
        Return strCsv
    End Function
End Class
