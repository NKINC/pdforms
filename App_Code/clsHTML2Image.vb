Public Class clsHTML2Image
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 NK-INC.COM All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>

    Public Function GenerateScreenshot(ByVal url As String) As Bitmap
        Return GenerateScreenshot(url, -1, -1)
    End Function
    Public Function GenerateScreenshotAlternative(ByVal url As String, ByVal width As Integer, ByVal height As Integer) As Bitmap
        Dim wb As New WebBrowser
        Try

            wb.ScrollBarsEnabled = False
            wb.ScriptErrorsSuppressed = True
            Dim strHeaders As String = "" '"Accept: */*" & vbCrLf '& Chr(13) & Chr(10)
            strHeaders &= "User-Agent: mozilla/5.0 (windows nt 6.1; trident/7.0; rv:11.0) like gecko" & vbCrLf '& Chr(13) & Chr(10)
            strHeaders &= "Referer: http://www.google.com/" & vbCrLf '& Chr(13) & Chr(10)
            wb.ScrollBarsEnabled = False
            wb.ScriptErrorsSuppressed = True
            If frmMain.IsValidUrl(url) Then
                wb.Navigate(url, "_self", Nothing, strHeaders.ToString)
                While wb.ReadyState <> WebBrowserReadyState.Complete
                    Application.DoEvents()
                End While
            ElseIf System.IO.File.Exists(url) Then
                Dim htmlDoc As String = System.IO.File.ReadAllText(url, System.Text.Encoding.UTF8)

                If Not htmlDoc.ToString.ToLower.Contains("<base ".ToLower) Then
                    If htmlDoc.ToString.Contains("</head>".ToLower) Then
                        htmlDoc = htmlDoc.ToString.Replace("</head>", "<base href='" & url & "'/></head>")
                    ElseIf htmlDoc.ToString.Contains("</html>".ToLower) Then
                        htmlDoc = htmlDoc.Replace("</html>", "<base href='" & url & "'/></html>")
                    ElseIf htmlDoc.ToString.Contains("</body>".ToLower) Then
                        htmlDoc = htmlDoc.ToString.Replace("</body>", "<base href='" & url & "'/></body>")
                    Else
                        htmlDoc = "<base href='" & url & "'/>" & htmlDoc.ToString
                    End If
                End If

                url = Application.StartupPath.ToString.TrimEnd("\"c) & "\temp\" & System.IO.Path.GetFileName(url)
                System.IO.File.WriteAllText(url, htmlDoc)
                wb.Navigate(url, "_self", Nothing, strHeaders.ToString)
                While wb.ReadyState <> WebBrowserReadyState.Complete
                    Application.DoEvents()
                End While
            ElseIf System.IO.File.Exists(Application.StartupPath.ToString.TrimEnd("\"c) & "\"c & url) Then
                url = Application.StartupPath.ToString.TrimEnd("\"c) & "\"c & System.IO.Path.GetFileName(url)
                Dim htmlDoc As String = System.IO.File.ReadAllText(url, System.Text.Encoding.UTF8)

                If Not htmlDoc.ToString.ToLower.Contains("<base ".ToLower) Then
                    If htmlDoc.ToString.Contains("</head>".ToLower) Then
                        htmlDoc = htmlDoc.ToString.Replace("</head>", "<base href='" & url & "'/></head>")
                    ElseIf htmlDoc.ToString.Contains("</html>".ToLower) Then
                        htmlDoc = htmlDoc.Replace("</html>", "<base href='" & url & "'/></html>")
                    ElseIf htmlDoc.ToString.Contains("</body>".ToLower) Then
                        htmlDoc = htmlDoc.ToString.Replace("</body>", "<base href='" & url & "'/></body>")
                    Else
                        htmlDoc = "<base href='" & url & "'/>" & htmlDoc.ToString
                    End If
                End If
                url = Application.StartupPath.ToString.TrimEnd("\"c) & "\temp\" & System.IO.Path.GetFileName(url)
                System.IO.File.WriteAllText(url, htmlDoc)
                wb.Navigate(url, "_self", Nothing, strHeaders.ToString)
                While wb.ReadyState <> WebBrowserReadyState.Complete
                    Application.DoEvents()
                End While
            End If
            wb.Width = width
            wb.Height = height
            If width <= 0 Then
                wb.Width = wb.Document.Body.ScrollRectangle.Width
            End If
            If height <= 0 Then
                wb.Height = wb.Document.Body.ScrollRectangle.Height
            End If
            Dim bitmap As New Bitmap(wb.Width, wb.Height)
            wb.DrawToBitmap(bitmap, New Rectangle(0, 0, wb.Width, wb.Height))
            wb.Dispose()
            Return bitmap
        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function
    Public Function GenerateScreenshot(ByVal url As String, ByVal width As Integer, ByVal height As Integer) As Bitmap
        Dim wb As New WebBrowser
        Try
            wb.ScrollBarsEnabled = False
            wb.ScriptErrorsSuppressed = True
            Dim strHeaders As String = "" '"Accept: */*" & vbCrLf '& Chr(13) & Chr(10)
            strHeaders &= "User-Agent: mozilla/5.0 (windows nt 6.1; trident/7.0; rv:11.0) like gecko" & vbCrLf '& Chr(13) & Chr(10)
            strHeaders &= "Referer: http://www.google.com/" & vbCrLf '& Chr(13) & Chr(10)
            If frmMain.IsValidUrl(url) Then
                wb.Navigate(url, "_self", Nothing, strHeaders.ToString)
                While wb.ReadyState <> WebBrowserReadyState.Complete
                    Application.DoEvents()
                End While
            ElseIf System.IO.File.Exists(url) Then
                Dim htmlDoc As String = System.IO.File.ReadAllText(url, System.Text.Encoding.UTF8)

                If Not htmlDoc.ToString.ToLower.Contains("<base ".ToLower) Then
                    If htmlDoc.ToString.Contains("</head>".ToLower) Then
                        htmlDoc = htmlDoc.ToString.Replace("</head>", "<base href='" & url & "'/></head>")
                    ElseIf htmlDoc.ToString.Contains("</html>".ToLower) Then
                        htmlDoc = htmlDoc.Replace("</html>", "<base href='" & url & "'/></html>")
                    ElseIf htmlDoc.ToString.Contains("</body>".ToLower) Then
                        htmlDoc = htmlDoc.ToString.Replace("</body>", "<base href='" & url & "'/></body>")
                    Else
                        htmlDoc = "<base href='" & url & "'/>" & htmlDoc.ToString
                    End If
                End If

                url = Application.StartupPath.ToString.TrimEnd("\"c) & "\temp\" & System.IO.Path.GetFileName(url)
                System.IO.File.WriteAllText(url, htmlDoc)
                wb.Navigate(url, "_self", Nothing, strHeaders.ToString)
                While wb.ReadyState <> WebBrowserReadyState.Complete
                    Application.DoEvents()
                End While
            ElseIf System.IO.File.Exists(Application.StartupPath.ToString.TrimEnd("\"c) & "\"c & url) Then

                url = Application.StartupPath.ToString.TrimEnd("\"c) & "\"c & System.IO.Path.GetFileName(url)
                Dim htmlDoc As String = System.IO.File.ReadAllText(url, System.Text.Encoding.UTF8)

                If Not htmlDoc.ToString.ToLower.Contains("<base ".ToLower) Then
                    If htmlDoc.ToString.Contains("</head>".ToLower) Then
                        htmlDoc = htmlDoc.ToString.Replace("</head>", "<base href='" & url & "'/></head>")
                    ElseIf htmlDoc.ToString.Contains("</html>".ToLower) Then
                        htmlDoc = htmlDoc.Replace("</html>", "<base href='" & url & "'/></html>")
                    ElseIf htmlDoc.ToString.Contains("</body>".ToLower) Then
                        htmlDoc = htmlDoc.ToString.Replace("</body>", "<base href='" & url & "'/></body>")
                    Else
                        htmlDoc = "<base href='" & url & "'/>" & htmlDoc.ToString
                    End If
                End If

                url = Application.StartupPath.ToString.TrimEnd("\"c) & "\temp\" & System.IO.Path.GetFileName(url)
                System.IO.File.WriteAllText(url, htmlDoc)
                wb.Navigate(url, "_self", Nothing, strHeaders.ToString)
                While wb.ReadyState <> WebBrowserReadyState.Complete
                    Application.DoEvents()
                End While
            End If
            wb.Width = width
            wb.Height = height
            If width <= 0 Then
                wb.Width = wb.Document.Body.ScrollRectangle.Width
            End If
            If height <= 0 Then
                wb.Height = wb.Document.Body.ScrollRectangle.Height
            End If
            Dim img As System.Drawing.Image = New System.Drawing.Bitmap(wb.Width, wb.Height)
            Utilities.NativeMethods.GetImage(wb, img, Color.Transparent)
            wb.Dispose()
            Dim bitmap As New System.Drawing.Bitmap(img, wb.Width, wb.Height)
            Return bitmap
        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function
    Public Function GenerateScreenshot(ByVal url As String, tempFolderPath As String, ByVal width As Integer, ByVal height As Integer) As Bitmap
        Dim wb As New WebBrowser
        Try
            wb.ScrollBarsEnabled = False
            wb.ScriptErrorsSuppressed = True
            Dim strHeaders As String = "" '"Accept: */*" & vbCrLf '& Chr(13) & Chr(10)
            strHeaders &= "User-Agent: mozilla/5.0 (windows nt 6.1; trident/7.0; rv:11.0) like gecko" & vbCrLf '& Chr(13) & Chr(10)
            strHeaders &= "Referer: http://www.google.com/" & vbCrLf '& Chr(13) & Chr(10)
            If frmMain.IsValidUrl(url) Then
                wb.Navigate(url, "_self", Nothing, strHeaders.ToString)
                While wb.ReadyState <> WebBrowserReadyState.Complete
                    Application.DoEvents()
                End While
            ElseIf System.IO.File.Exists(url) Then
                Dim htmlDoc As String = System.IO.File.ReadAllText(url, System.Text.Encoding.UTF8)
                If Not htmlDoc.ToString.ToLower.Contains("<base ".ToLower) Then
                    If htmlDoc.ToString.Contains("</head>".ToLower) Then
                        htmlDoc = htmlDoc.ToString.Replace("</head>", "<base href='" & url & "'/></head>")
                    ElseIf htmlDoc.ToString.Contains("</html>".ToLower) Then
                        htmlDoc = htmlDoc.Replace("</html>", "<base href='" & url & "'/></html>")
                    ElseIf htmlDoc.ToString.Contains("</body>".ToLower) Then
                        htmlDoc = htmlDoc.ToString.Replace("</body>", "<base href='" & url & "'/></body>")
                    Else
                        htmlDoc = "<base href='" & url & "'/>" & htmlDoc.ToString
                    End If
                End If
                url = tempFolderPath.ToString.TrimEnd("\"c) & "\" & System.IO.Path.GetFileName(url)
                System.IO.File.WriteAllText(url, htmlDoc)
                wb.Navigate(url, "_self", Nothing, strHeaders.ToString)
                While wb.ReadyState <> WebBrowserReadyState.Complete
                    Application.DoEvents()
                End While
            ElseIf System.IO.File.Exists(Application.StartupPath.ToString.TrimEnd("\"c) & "\"c & url) Then

                url = Application.StartupPath.ToString.TrimEnd("\"c) & "\"c & System.IO.Path.GetFileName(url)
                Dim htmlDoc As String = System.IO.File.ReadAllText(url, System.Text.Encoding.UTF8)

                If Not htmlDoc.ToString.ToLower.Contains("<base ".ToLower) Then
                    If htmlDoc.ToString.Contains("</head>".ToLower) Then
                        htmlDoc = htmlDoc.ToString.Replace("</head>", "<base href='" & url & "'/></head>")
                    ElseIf htmlDoc.ToString.Contains("</html>".ToLower) Then
                        htmlDoc = htmlDoc.Replace("</html>", "<base href='" & url & "'/></html>")
                    ElseIf htmlDoc.ToString.Contains("</body>".ToLower) Then
                        htmlDoc = htmlDoc.ToString.Replace("</body>", "<base href='" & url & "'/></body>")
                    Else
                        htmlDoc = "<base href='" & url & "'/>" & htmlDoc.ToString
                    End If
                End If

                url = tempFolderPath.ToString.TrimEnd("\"c) & "\" & System.IO.Path.GetFileName(url)
                System.IO.File.WriteAllText(url, htmlDoc)
                wb.Navigate(url, "_self", Nothing, strHeaders.ToString)
                While wb.ReadyState <> WebBrowserReadyState.Complete
                    Application.DoEvents()
                End While
            End If
            wb.Width = width
            wb.Height = height
            If width <= 0 Then
                wb.Width = wb.Document.Body.ScrollRectangle.Width
            End If
            If height <= 0 Then
                wb.Height = wb.Document.Body.ScrollRectangle.Height
            End If
            Dim img As System.Drawing.Image = New System.Drawing.Bitmap(wb.Width, wb.Height)
            Utilities.NativeMethods.GetImage(wb, img, Color.Transparent)
            wb.Dispose()
            Dim bitmap As New System.Drawing.Bitmap(img, wb.Width, wb.Height)
            Return bitmap
        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function
End Class
