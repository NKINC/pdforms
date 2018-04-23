Imports System.IO
Imports iTextSharp
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html
Public Class clsHTML2PDFiText
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 Nicholas Kowalewicz All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>

    Public Shared Function HTML2PDF(ByVal example_html As String) As Byte()


        Dim bytes() As Byte



        Using ms = New MemoryStream()


            Using doc = New Document()


                Using writer = PdfWriter.GetInstance(doc, ms)


                    doc.Open()














                    Using htmlWorker As iTextSharp.text.html.simpleparser.HTMLWorker = New iTextSharp.text.html.simpleparser.HTMLWorker(doc)


                        Using sr = New StringReader(example_html)

                            htmlWorker.Parse(sr)
                        End Using
                    End Using




































                    doc.Close()
                End Using
            End Using



            bytes = ms.ToArray()
        End Using







        Return bytes







    End Function
    Public Shared Function IsValidUrl(ByVal url As String) As Boolean
        Dim sPattern As String = ""

        sPattern = "http(s)?://([\w+?\.\w+])+([a-zA-Z0-9\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]*)?"
        Return System.Text.RegularExpressions.Regex.IsMatch(url, sPattern)
    End Function
    Public Shared Function FileExists(ByVal strFilePath As String) As Boolean
        Try
            If String.IsNullOrEmpty(strFilePath & "") Then
                Return False
            End If
            If My.Computer.FileSystem.FileExists(strFilePath) Then
                Return True
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
        Return False
    End Function

    Public Shared Function HTML2PDFCss(ByVal example_html As String, ByVal pgWidth As Integer, ByVal pgHeight As Integer, Optional ByVal printOnly As Boolean = False, Optional ByVal printInclude As Boolean = False, Optional ByVal baseURL As String = "", Optional ByVal injectCss As Boolean = False) As Byte()


        Dim bytes() As Byte



        Using ms As MemoryStream = New MemoryStream()

            If pgWidth <= 0 Then
                pgWidth = iTextSharp.text.PageSize.LETTER.Width
            End If
            If pgHeight <= 0 Then
                pgHeight = iTextSharp.text.PageSize.LETTER.Height
            End If
            Dim doc As iTextSharp.text.Document


            Dim cssLinks As New List(Of String)
            Dim example_htmlNew As String = example_html.Trim()
            Try



                For Each m As System.Text.RegularExpressions.Match In System.Text.RegularExpressions.Regex.Matches(example_html, "(<link\s+(?:[^>]*)>)", System.Text.RegularExpressions.RegexOptions.IgnoreCase + System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace + System.Text.RegularExpressions.RegexOptions.Singleline)
                    If m.Success Then
                        If m.Groups.Count >= 1 Then

                            If Not String.IsNullOrEmpty(m.Groups(0).Value & "") Then
                                If m.Groups(0).Value.ToString.ToLower.Contains("stylesheet".ToLower) Then
                                    If printOnly Then

                                        If m.Groups(0).Value.ToString.ToLower.Contains("print".ToLower) Or m.Groups(0).Value.ToString.ToLower.Contains("all".ToLower) Then


                                            For Each m2 As System.Text.RegularExpressions.Match In System.Text.RegularExpressions.Regex.Matches(m.Groups(0).Value, "<link\s+(?:[^>]*?\s+)?href=""([^""]*)", System.Text.RegularExpressions.RegexOptions.IgnoreCase + System.Text.RegularExpressions.RegexOptions.Singleline)
                                                If Not String.IsNullOrEmpty(m2.Groups(1).Value) Then
                                                    cssLinks.Add(m2.Groups(1).Value.ToString)
                                                End If
                                            Next
                                            example_htmlNew = example_htmlNew.Trim().Replace(m.Groups(0).Value, "")
                                        Else





                                            example_htmlNew = example_htmlNew.Replace(m.Groups(0).Value, "")
                                        End If
                                    Else

                                        If m.Groups(0).Value.ToString.ToLower.Contains("print".ToLower) Then
                                            If printInclude Then

                                                For Each m2 As System.Text.RegularExpressions.Match In System.Text.RegularExpressions.Regex.Matches(m.Groups(0).Value, "<link\s+(?:[^>]*?\s+)?href=""([^""]*)", System.Text.RegularExpressions.RegexOptions.IgnoreCase + System.Text.RegularExpressions.RegexOptions.Singleline)
                                                    If Not String.IsNullOrEmpty(m2.Groups(1).Value) Then
                                                        cssLinks.Add(m2.Groups(1).Value.ToString)
                                                    End If
                                                Next
                                                example_htmlNew = example_htmlNew.Replace(m.Groups(0).Value, "")
                                            Else
                                                example_htmlNew = example_htmlNew.Replace(m.Groups(0).Value, "")
                                            End If
                                        Else

                                            For Each m2 As System.Text.RegularExpressions.Match In System.Text.RegularExpressions.Regex.Matches(m.Groups(0).Value, "<link\s+(?:[^>]*?\s+)?href=""([^""]*)", System.Text.RegularExpressions.RegexOptions.IgnoreCase + System.Text.RegularExpressions.RegexOptions.Singleline)
                                                If Not String.IsNullOrEmpty(m2.Groups(1).Value) Then
                                                    cssLinks.Add(m2.Groups(1).Value.ToString)
                                                End If
                                            Next
                                            example_htmlNew = example_htmlNew.Replace(m.Groups(0).Value, "")
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                Next
            Catch ex As Exception
                Err.Clear()
            End Try
            If pgHeight <= 0 Then
                Dim wb As New WebBrowser
                wb.ScriptErrorsSuppressed = True



                wb.DocumentText = (example_htmlNew.Trim()) & ""

                While wb.ReadyState <> WebBrowserReadyState.Complete
                    Application.DoEvents()
                End While
                Dim widthDefault As Single = -1, heightDefault As Single = -1
                widthDefault = wb.Document.Body.ScrollRectangle.Width
                pgHeight = CInt(wb.Document.Body.ScrollRectangle.Height / 1.65)
            End If
            Dim pgSize As New iTextSharp.text.Rectangle(pgWidth, pgHeight)
            doc = New iTextSharp.text.Document(pgSize)
            doc.SetPageSize(pgSize)
            Dim writer As iTextSharp.text.pdf.PdfWriter = PdfWriter.GetInstance(doc, ms)
            doc.Open()

            Dim allCss As New System.Text.StringBuilder
            Dim wc As New System.Net.WebClient

            If cssLinks.Count > 0 And injectCss Then
                For Each strCss As String In cssLinks.ToArray

                    Dim strCss2 As String = ""
                    If IsValidUrl(strCss.ToString().Replace("&amp;", "&")) Then
                        strCss2 = System.Text.Encoding.UTF8.GetString(wc.DownloadData(strCss.ToString().Replace("&amp;", "&"))).Trim()
                        For Each r As String In wc.ResponseHeaders.Keys
                            Try
                                Dim x1 = r
                                Dim x2 = wc.ResponseHeaders(x1)
                                Dim x3 = x1 & "=" & x2
                                If r.ToString.ToLower = "Content-Type".ToLower Then
                                    If x2.ToString.ToLower.Contains("text/css") Then
                                        allCss.AppendLine(strCss2)
                                        Exit For
                                    End If
                                End If
                            Catch ex As Exception
                                Err.Clear()
                            End Try

                        Next
                    ElseIf IsValidUrl(baseURL.TrimEnd("/") & "/" & strCss.ToString().TrimStart("/").Replace("&amp;", "&")) Then
                        strCss2 = System.Text.Encoding.UTF8.GetString(wc.DownloadData(baseURL.TrimEnd("/") & "/" & strCss.ToString().TrimStart("/").Replace("&amp;", "&"))).Trim()
                        For Each r As String In wc.ResponseHeaders.Keys
                            Try
                                Dim x1 = r
                                Dim x2 = wc.ResponseHeaders(x1)
                                Dim x3 = x1 & "=" & x2
                                If r.ToString.ToLower = "Content-Type".ToLower Then
                                    If x2.ToString.ToLower.Contains("text/css") Then
                                        allCss.AppendLine(strCss2)
                                        Exit For
                                    End If
                                End If
                            Catch ex As Exception
                                Err.Clear()
                            End Try

                        Next
                    ElseIf FileExists(baseURL.TrimEnd("/").TrimEnd("\").Replace("/"c, "\"c).ToString() & "\" & strCss.ToString().Replace("&amp;", "&")) Then
                        strCss2 = System.Text.Encoding.UTF8.GetString(File.ReadAllBytes(baseURL.TrimEnd("/").TrimEnd("\").Replace("/"c, "\"c).ToString() & "\" & strCss.ToString().Replace("&amp;", "&"))).Trim()
                        For Each r As String In wc.ResponseHeaders.Keys
                            Try
                                allCss.AppendLine(strCss2)
                            Catch ex As Exception
                                Err.Clear()
                            End Try

                        Next
                    ElseIf FileExists(strCss.ToString().Replace("&amp;", "&").Replace("file://", "")) Then
                        strCss2 = System.Text.Encoding.UTF8.GetString(File.ReadAllBytes(strCss.ToString().Replace("&amp;", "&").Replace("file://", ""))).Trim()
                        Try
                            allCss.AppendLine(strCss2)
                        Catch ex As Exception
                            Err.Clear()
                        End Try
                    End If
                Next
            End If
            If Not allCss.ToString() = "" Then
                example_htmlNew = example_htmlNew.ToString().Replace("</head>", "<style media=""all"" type=""text/css"">" & Environment.NewLine & allCss.ToString() & Environment.NewLine & "</style></head>")
            End If
            Using msHtml As MemoryStream = New MemoryStream(System.Text.Encoding.UTF8.GetBytes(example_htmlNew.Trim()))
                iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msHtml, System.Text.Encoding.UTF8)
                doc.Close()
            End Using
            bytes = ms.ToArray()
        End Using
        Return bytes
    End Function
End Class
