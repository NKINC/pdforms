Imports System.Net.Mail
Imports System.Net
Imports System.IO
Imports System.Text.Encoding
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.String
Public Class clsEmail
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 Nicholas Kowalewicz All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>

    Private _MailMsg As System.Net.Mail.MailMessage
    Private _SMTPServer As System.Net.Mail.SmtpClient
    Public Property MailMessage() As System.Net.Mail.MailMessage
        Get
            If Not _MailMsg Is Nothing Then
                Return _MailMsg
            Else
                _MailMsg = New System.Net.Mail.MailMessage
                Return _MailMsg
            End If
        End Get
        Set(ByVal value As System.Net.Mail.MailMessage)
            _MailMsg = value
        End Set
    End Property
    Public Property SMTPServer() As System.Net.Mail.SmtpClient
        Get
            Return _SMTPServer
        End Get
        Set(ByVal value As System.Net.Mail.SmtpClient)
            _SMTPServer = value
        End Set
    End Property
    Public Property MailSubject() As String
        Get
            Return _MailMsg.Subject & ""
        End Get
        Set(ByVal value As String)
            _MailMsg.Subject = value & ""
        End Set
    End Property
    Public WriteOnly Property MailSubjectAppend() As String
        Set(ByVal value As String)
            _MailMsg.Subject = _MailMsg.Subject & value & ""
        End Set
    End Property
    Public Property MailBody() As String
        Get
            Return _MailMsg.Body & ""
        End Get
        Set(ByVal value As String)
            _MailMsg.Body = value & ""
        End Set
    End Property
    Public Sub RecipientAppend(ByVal email As String, Optional ByVal name As String = "")
        If Not name.Length = 0 And Not email.Length = 0 Then
            _MailMsg.To.Add(New System.Net.Mail.MailAddress(email, name))
        ElseIf Not email.Length = 0 Then
            _MailMsg.To.Add(New System.Net.Mail.MailAddress(email))
        End If
    End Sub
    Public Sub MailFromAdd(ByVal email As String, Optional ByVal name As String = "")
        If Not name.Length = 0 And Not email.Length = 0 Then
            _MailMsg.From = New System.Net.Mail.MailAddress(email, name)
        ElseIf Not email.Length = 0 Then
            _MailMsg.From = New System.Net.Mail.MailAddress(email)
        End If
    End Sub
    Public WriteOnly Property MailBodyAppend() As String
        Set(ByVal value As String)
            _MailMsg.Body = _MailMsg.Body & value & ""
        End Set
    End Property
    Public Property IsBodyHtml() As Boolean
        Get
            Return _MailMsg.IsBodyHtml
        End Get
        Set(ByVal value As Boolean)
            _MailMsg.IsBodyHtml = value
        End Set
    End Property
    Public Sub New(ByVal MsgSubject As String, ByVal MsgBody As String, ByVal MsgFrom As System.Net.Mail.MailAddress, ByVal Recipients As System.Net.Mail.MailAddress(), Optional ByVal isHTML As Boolean = False)
        If MailMessage Is Nothing Then
            MailMessage = New System.Net.Mail.MailMessage
        End If
        MailMessage.Subject = MsgSubject
        If isHTML Then
            MailMessage.Body = MsgBody
            MailMessage.IsBodyHtml = isHTML
            Dim htmlEmail As New clsEmail_HTML
            MailMessage = htmlEmail.HtmlEmail_Text2HTML_Message(MsgBody, MailMessage)
        Else
            MailMessage.Body = MsgBody
            MailMessage.IsBodyHtml = isHTML
        End If
        MailMessage.From = MsgFrom
        For Each recipient As System.Net.Mail.MailAddress In Recipients
            MailMessage.To.Add(recipient)
        Next
    End Sub
    Public Sub New(ByVal MsgSubject As String, ByVal MsgBody As String, ByVal MsgFrom As System.Net.Mail.MailAddress, ByVal Recipients As System.Net.Mail.MailAddressCollection, Optional ByVal isHTML As Boolean = False)
        If MailMessage Is Nothing Then
            MailMessage = New System.Net.Mail.MailMessage
        End If
        MailMessage.Subject = MsgSubject
        If isHTML Then
            MailMessage.Body = MsgBody
            MailMessage.IsBodyHtml = isHTML
            Dim htmlEmail As New clsEmail_HTML
            MailMessage = htmlEmail.HtmlEmail_Text2HTML_Message(MsgBody, MailMessage)
        Else
            MailMessage.Body = MsgBody
            MailMessage.IsBodyHtml = isHTML
        End If
        MailMessage.From = MsgFrom
        For Each recipient As System.Net.Mail.MailAddress In Recipients
            MailMessage.To.Add(recipient)
        Next
    End Sub
    Public Sub New(ByVal MsgSubject As String, ByVal MsgBody As String, ByVal MsgFromEmailAddress As String, ByVal RecipientEmailAddresses As String(), Optional ByVal isHTML As Boolean = False)
        If MailMessage Is Nothing Then
            MailMessage = New System.Net.Mail.MailMessage
        End If
        MailMessage.Subject = MsgSubject
        If isHTML Then
            MailMessage.Body = MsgBody
            MailMessage.IsBodyHtml = isHTML
            Dim htmlEmail As New clsEmail_HTML
            MailMessage = htmlEmail.HtmlEmail_Text2HTML_Message(MsgBody, MailMessage)
        Else
            MailMessage.Body = MsgBody
            MailMessage.IsBodyHtml = isHTML
        End If
        MailMessage.From = New System.Net.Mail.MailAddress(MsgFromEmailAddress)
        For Each recipient As String In RecipientEmailAddresses
            MailMessage.To.Add(recipient)
        Next
    End Sub
    Public Sub New(ByRef msg As System.Net.Mail.MailMessage, MsgBody As String, Optional fileRootDirectory As String = "", Optional ByVal isHTML As Boolean = False)



        If isHTML Then
            msg.Body = MsgBody
            msg.IsBodyHtml = isHTML
            Dim htmlEmail As New clsEmail_HTML
            msg = htmlEmail.HtmlEmail_Text2HTML_Message(MsgBody, msg, fileRootDirectory)
        Else
            msg.Body = MsgBody
            msg.IsBodyHtml = isHTML
        End If
        MailMessage = msg
    End Sub
    Public Sub Add_Attachment(ByVal AttachmentBuffer As Byte(), ByVal FileName As String)
        If MailMessage Is Nothing Then
            MailMessage = New System.Net.Mail.MailMessage
        End If
        Dim cStream As MemoryStream = New MemoryStream(AttachmentBuffer)
        If cStream.CanRead Then cStream.Position = 0
        Dim Att1 As Attachment
        Att1 = New Attachment(CType(cStream, Stream), FileName)
        MailMessage.Attachments.Add(Att1)
    End Sub
    Public Sub Add_Attachment(ByVal AttachmentStream As Stream, ByVal FileName As String)
        If MailMessage Is Nothing Then
            MailMessage = New System.Net.Mail.MailMessage
        End If
        If AttachmentStream.CanRead Then AttachmentStream.Position = 0
        Dim Att1 As Attachment
        Att1 = New Attachment(CType(AttachmentStream, Stream), FileName)
        MailMessage.Attachments.Add(Att1)
    End Sub
    Public Sub Add_Attachment(ByVal AttachmentString As String, ByVal FileName As String)
        If MailMessage Is Nothing Then
            MailMessage = New System.Net.Mail.MailMessage
        End If
        Dim AttachmentBuffer As Byte() = Text.Encoding.Default.GetBytes(AttachmentString.ToString)
        Dim cStream As MemoryStream = New MemoryStream(AttachmentBuffer)
        If cStream.CanRead Then cStream.Position = 0
        Dim Att1 As Attachment
        Att1 = New Attachment(CType(cStream, Stream), FileName)
        MailMessage.Attachments.Add(Att1)
    End Sub
    Public Sub Add_Attachment(ByVal AttachmentFileNameOrUrl As String)
        If MailMessage Is Nothing Then
            MailMessage = New System.Net.Mail.MailMessage
        End If
        Dim PDFFile As String
        Dim bytes() As Byte = Nothing
        Dim FileName As String = ""
        If IsValidUrl(AttachmentFileNameOrUrl) Then
            FileName = AttachmentFileNameOrUrl
            Dim client As New System.Net.WebClient
            Dim input As New StreamReader(client.OpenRead(AttachmentFileNameOrUrl))
            AttachmentFileNameOrUrl = input.ReadToEnd
            bytes = Encoding.UTF8.GetBytes(AttachmentFileNameOrUrl)
            If FileName.LastIndexOf("/") >= 0 Then
                FileName = FileName.Substring(FileName.LastIndexOf("/") + 1, FileName.Length - FileName.LastIndexOf("/") - 1) & ""
            End If
        ElseIf File.Exists(AttachmentFileNameOrUrl) Then
            FileName = AttachmentFileNameOrUrl
            PDFFile = Me.OpenFile(AttachmentFileNameOrUrl)
            Dim FS As New FileStream(AttachmentFileNameOrUrl, FileMode.Open, FileAccess.Read, FileShare.Read)
            Dim reader As StreamReader = New StreamReader(FS)
            AttachmentFileNameOrUrl = reader.ReadToEnd
            bytes = Encoding.Unicode.GetBytes(AttachmentFileNameOrUrl)
            FS.Close()
            If FileName.LastIndexOf("\") >= 0 Then
                FileName = FileName.Substring(FileName.LastIndexOf("/") + 1, FileName.Length - FileName.LastIndexOf("/") - 1) & ""
            End If
        End If
        Dim cStream As MemoryStream = New MemoryStream(bytes)
        If cStream.CanRead Then cStream.Position = 0
        Dim Att1 As Attachment
        Att1 = New Attachment(CType(cStream, Stream), FileName)
        MailMessage.Attachments.Add(Att1)
    End Sub
    Public Function OpenFile(ByVal FullPath As String) As String
        Dim strContents As String
        Dim objReader As StreamReader
        Try



            If File.Exists(FullPath) Then

                objReader = New StreamReader(FullPath, True)
                strContents = objReader.ReadToEnd()
                objReader.Close()

                Return strContents
            Else

                Return ""
                Exit Function
            End If
        Catch ex As Exception
            Return Nothing
            Exit Function
        End Try
        Return ""
    End Function
    Public Function ReadAllBytes(ByVal FullPath As String) As Byte()
        Try
            If File.Exists(FullPath) Then

                Return System.IO.File.ReadAllBytes(FullPath)
            ElseIf File.Exists(Application.StartupPath.ToString.TrimEnd("\"c) & "\" & (FullPath)) Then

                Return System.IO.File.ReadAllBytes(Application.StartupPath.ToString.TrimEnd("\"c) & "\" & (FullPath))
            ElseIf IsValidUrl(FullPath) Then
                Dim wc As New WebClient
                Dim bytes() As Byte = wc.DownloadData(FullPath)
                wc.Dispose()
                Return bytes
            Else

                Return Nothing
                Exit Function
            End If
        Catch ex As Exception
            Return Nothing
            Exit Function
        End Try
        Return Nothing
    End Function
    Public Function IsValidUrl(ByVal url As String) As Boolean

        Return System.Text.RegularExpressions.Regex.IsMatch(url, "^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$")
    End Function
    Public Sub Send(Optional ByVal SMTPHost As String = "localhost", Optional ByVal SMTPPort As Integer = 25)
        SMTPServer.Timeout = 100000
        SMTPServer.Port = SMTPPort
        SMTPServer.Host = SMTPHost
        SMTPServer.Send(MailMessage)
    End Sub
    Public Function SMTPCredentials(ByVal username As String, ByVal password As String, Optional ByVal domain As String = "") As Net.NetworkCredential
        If domain.Length = 0 Then
            SMTPServer.Credentials = New System.Net.NetworkCredential(username, password)
        Else
            SMTPServer.Credentials = New System.Net.NetworkCredential(username, password, domain)
        End If
        Return SMTPServer.Credentials
    End Function
    Public Class clsEmail_HTML
#Region "HTML_To_MESSAGE"
        Public Shared Function HTML2PlainText(ByVal str As String) As String
            str = System.Text.RegularExpressions.Regex.Replace(str, "<(.|\n)*?>", String.Empty)
            str = str.TrimStart(" ".ToCharArray)
            str = str.TrimEnd(" ".ToCharArray)
            Dim strReader As New System.IO.StringReader(str), strOut As String = "", strLine As String = "", strRead As Boolean = True
            Dim lastLine1 As String = ""
            Dim lastLine2 As String = "", lastLine3 As String = ""
            Do Until strRead = False
                Try
                    lastLine3 = lastLine2
                    lastLine2 = lastLine1
                    lastLine1 = strLine
                    strLine = strReader.ReadLine()
                    strLine = strLine.TrimStart(" ".ToCharArray)
                    strLine = strLine.TrimEnd(" ".ToCharArray)
                    strLine = strLine.TrimStart("".ToCharArray)
                    strLine = strLine.TrimEnd("".ToCharArray)
                    strLine = strLine.TrimStart(Environment.NewLine.ToCharArray)
                    strLine = strLine.TrimEnd(Environment.NewLine.ToCharArray)
                    If String.IsNullOrEmpty(strLine) Then
                        strLine = ""
                        If Not String.IsNullOrEmpty(strOut & "") Then


                            strOut &= strLine & Environment.NewLine & ""



                        Else
                            strOut &= strLine & ""
                        End If
                    Else
                        strOut &= strLine & Environment.NewLine & ""
                    End If

                    strRead = True
                Catch ex As Exception
                    strRead = False
                    Exit Do
                End Try
            Loop
            Return strOut
        End Function


































        Private Function FetchImgSrcFromHTMLSource(ByVal htmlSource As String) As System.Collections.Generic.List(Of String)
            Dim links As New System.Collections.Generic.List(Of String)
            Dim regexImgSrc As String = "\<img[^>]*src\s*=\s*[""']?([^'"" >]+?)[ '""][^>]*?\>"
            Dim matchesImgSrc As MatchCollection = Regex.Matches(htmlSource, regexImgSrc, RegexOptions.IgnoreCase Or RegexOptions.Singleline)
            For Each m As Match In matchesImgSrc
                Dim href As String = m.Groups(1).Value
                links.Add(href)
            Next
            Return links
        End Function
        Private Function FetchCSSSrcFromHTMLSource(ByVal htmlSource As String) As System.Collections.Generic.List(Of String)
            Dim links As New System.Collections.Generic.List(Of String)
            Dim regexImgSrc As String = "\<link[^>]*href\s*=\s*[""']?([^'"" >]+?)[ '""][^>]*?\>"
            Dim matchesImgSrc As MatchCollection = Regex.Matches(htmlSource, regexImgSrc, RegexOptions.IgnoreCase Or RegexOptions.Singleline)
            For Each m As Match In matchesImgSrc
                Dim href As String = m.Groups(1).Value
                links.Add(href)
            Next
            Return links
        End Function
























































        Private Function HtmlEmail_PDFEmbed2(ByVal url As Uri, ByVal message As System.Net.Mail.MailMessage) As System.Net.Mail.MailMessage
            Dim strInlineCSSURL As String = ""
            Try
                message.IsBodyHtml = True
                Dim cw As New System.Net.WebClient
                Dim html As String = cw.DownloadString(url)

                Dim imgID As Integer = 0
                Dim picture As New System.Collections.Generic.List(Of System.Net.Mail.LinkedResource)
                For Each link As String In FetchImgSrcFromHTMLSource(html)
                    imgID += 1
                    Dim lnkOLD As String = link
                    If link.ToString.StartsWith("/") Then
                        Dim xurl As New Uri(url.ToString)
                        link = "http" & IIf(url.ToString.StartsWith("https"), "s", "") & "://" & xurl.Authority.ToString.TrimEnd("/") & "/" & link.ToString.TrimStart("/")
                    End If
                    Dim pictStream As New MemoryStream
                    If File.Exists(link.ToString()) Then
                        pictStream = New MemoryStream(File.ReadAllBytes(link.ToString))
                    Else
                        pictStream = New MemoryStream(cw.DownloadData(link.ToString))
                    End If
                    If pictStream.CanSeek Then
                        pictStream.Position = 0
                    End If

                    Select Case Path.GetExtension(link.ToString).ToLower.TrimStart(".")
                        Case "jpg"
                            picture.Add(New System.Net.Mail.LinkedResource(pictStream, "image/jpeg"))
                        Case "gif"
                            picture.Add(New System.Net.Mail.LinkedResource(pictStream, "image/gif"))
                        Case "png"
                            picture.Add(New System.Net.Mail.LinkedResource(pictStream, "image/png"))
                        Case "bmp"
                            picture.Add(New System.Net.Mail.LinkedResource(pictStream, "image/bmp"))
                        Case "tif"
                            picture.Add(New System.Net.Mail.LinkedResource(pictStream, "image/tif"))
                        Case Else
                            picture.Add(New System.Net.Mail.LinkedResource(pictStream, "image/jpeg"))
                    End Select
                    picture(picture.Count - 1).ContentId = "IMG" & imgID
                    html = html.Replace("'" & lnkOLD & "'", "'" & link & "'")
                    html = html.Replace("""" & lnkOLD & """", """" & link & """")
                    html = html.Replace("(" & lnkOLD & ")", "(" & link & ")")
                    html = html.Replace(link, "cid:" & picture(picture.Count - 1).ContentId)
                    'picture.CreateLinkedResourceFromString
                Next

                Dim css As New System.Collections.Generic.List(Of System.Net.Mail.LinkedResource)
                For Each link As String In FetchCSSSrcFromHTMLSource(html)
                    Dim strCSS As String
                    If File.Exists(link.ToString) Then
                        strCSS = File.ReadAllText(link.ToString)
                    Else
                        imgID += 1
                        strCSS = cw.DownloadString(link.ToString.Replace("&amp;", "&"))
                    End If
                    Dim strCssURLOld As String = ""
                    Dim lnkOLD As String = link
                    For Each strURL As String In FetchImgSrcFromCssSource2(strCSS)
                        strCssURLOld = strURL
                        Dim strPicBase64 As New StringBuilder
                        Dim imgBytes() As Byte = Nothing
                        If File.Exists(strURL) Then
                            imgBytes = File.ReadAllBytes(strURL)
                        Else
                            imgBytes = cw.DownloadData(strURL.ToString)
                        End If
                        If imgBytes.Length > 32768 Then
                            Dim pictStream As New MemoryStream(imgBytes)
                            If pictStream.CanSeek Then
                                pictStream.Position = 0
                            End If
                            Select Case Path.GetExtension(strURL.ToString).ToLower.TrimStart(".")
                                Case "jpg"
                                    picture.Add(New System.Net.Mail.LinkedResource(pictStream, "image/jpeg"))
                                Case "gif"
                                    picture.Add(New System.Net.Mail.LinkedResource(pictStream, "image/gif"))
                                Case "png"
                                    picture.Add(New System.Net.Mail.LinkedResource(pictStream, "image/png"))
                                Case "bmp"
                                    picture.Add(New System.Net.Mail.LinkedResource(pictStream, "image/bmp"))
                                Case "tif"
                                    picture.Add(New System.Net.Mail.LinkedResource(pictStream, "image/tif"))
                                Case "pdf"
                                    picture.Add(New System.Net.Mail.LinkedResource(pictStream, "image/tif"))
                                Case "fdf"
                                    picture.Add(New System.Net.Mail.LinkedResource(pictStream, "image/tif"))
                                Case "xml"
                                    picture.Add(New System.Net.Mail.LinkedResource(pictStream, "image/tif"))
                                Case Else
                                    picture.Add(New System.Net.Mail.LinkedResource(pictStream, "image/jpeg"))
                            End Select
                            picture(picture.Count - 1).ContentId = "CSSIMG" & imgID
                            strCSS = strCSS.Replace("'" & strCssURLOld & "'", "'" & strURL & "'")
                            strCSS = strCSS.Replace("""" & strCssURLOld & """", """" & strURL & """")
                            strCSS = strCSS.Replace("(" & strCssURLOld & ")", "(" & strURL & ")")
                            strCSS = strCSS.Replace(strURL, "cid:" & picture(picture.Count - 1).ContentId)
                        Else

                            Select Case Path.GetExtension(strURL.ToString).ToLower.TrimStart(".")
                                Case "jpg"
                                    strPicBase64 = New StringBuilder("data:" & "image/jpeg" & ";base64," & ConvertToBase64String(imgBytes) & "")
                                Case "gif"
                                    strPicBase64 = New StringBuilder("data:" & "image/gif" & ";base64," & ConvertToBase64String(imgBytes) & "")
                                Case "png"
                                    strPicBase64 = New StringBuilder("data:" & "image/png" & ";base64," & ConvertToBase64String(imgBytes) & "")
                                Case "bmp"
                                    strPicBase64 = New StringBuilder("data:" & "image/bmp" & ";base64," & ConvertToBase64String(imgBytes) & "")
                                Case "tif"
                                    strPicBase64 = New StringBuilder("data:" & "image/tif" & ";base64," & ConvertToBase64String(imgBytes) & "")
                                Case "pdf"
                                    strPicBase64 = New StringBuilder("data:" & "application/pdf" & ";base64," & ConvertToBase64String(imgBytes) & "")
                                Case "fdf"
                                    strPicBase64 = New StringBuilder("data:" & "application/vnd.fdf" & ";base64," & ConvertToBase64String(imgBytes) & "")
                                Case "xml"
                                    strPicBase64 = New StringBuilder("data:" & "text/xml" & ";base64," & ConvertToBase64String(imgBytes) & "")
                                Case Else
                                    strPicBase64 = New StringBuilder("data:" & "image/jpeg" & ";base64," & ConvertToBase64String(imgBytes) & "")
                            End Select
                            strCSS = strCSS.Replace("'" & strCssURLOld & "'", "'" & strURL & "'")
                            strCSS = strCSS.Replace("""" & strCssURLOld & """", """" & strURL & """")
                            strCSS = strCSS.Replace("(" & strCssURLOld & ")", "(" & strURL & ")")
                            strCSS = strCSS.Replace(strURL, strPicBase64.ToString)
                        End If
                    Next
                    Dim cssStream As New MemoryStream(System.Text.Encoding.UTF8.GetBytes(strCSS))
                    If cssStream.CanSeek Then
                        cssStream.Position = 0
                    End If
                    css.Add(New System.Net.Mail.LinkedResource(cssStream, ("text/css"))) 'New System.Net.Mime.ContentType
                    css(css.Count - 1).TransferEncoding = System.Net.Mime.TransferEncoding.SevenBit 'Mime.TransferEncoding.SevenBit
                    css(css.Count - 1).ContentType = New System.Net.Mime.ContentType("text/css")
                    css(css.Count - 1).ContentId = "CSS" & imgID
                    html = html.Replace(lnkOLD, "cid:" & css(css.Count - 1).ContentId)
                Next
                For Each strInlineCSSURL In FetchImgSrcFromInlineCssSource2(html)
                    imgID += 1
                    Dim strCssURLOld = strInlineCSSURL
                    If strInlineCSSURL.ToString.StartsWith("/") Or Not (strInlineCSSURL.ToString.StartsWith("http")) Then
                    Else

                    End If

                    html = html.Replace(strCssURLOld, strInlineCSSURL & "")
                    Dim pictStream As New MemoryStream()
                    If File.Exists(strInlineCSSURL.ToString()) Then
                        pictStream = New MemoryStream(File.ReadAllBytes(strInlineCSSURL.ToString))
                    Else
                        pictStream = New MemoryStream(cw.DownloadData(strInlineCSSURL.ToString))
                    End If
                    If pictStream.CanSeek Then
                        pictStream.Position = 0
                    End If
                    Select Case Path.GetExtension(strInlineCSSURL.ToString).ToLower.TrimStart(".")
                        Case "jpg"
                            picture.Add(New System.Net.Mail.LinkedResource(pictStream, "image/jpeg"))
                        Case "gif"
                            picture.Add(New System.Net.Mail.LinkedResource(pictStream, "image/gif"))
                        Case "png"
                            picture.Add(New System.Net.Mail.LinkedResource(pictStream, "image/png"))
                        Case "bmp"
                            picture.Add(New System.Net.Mail.LinkedResource(pictStream, "image/bmp"))
                        Case "tif"
                            picture.Add(New System.Net.Mail.LinkedResource(pictStream, "image/tif"))
                        Case Else
                            picture.Add(New System.Net.Mail.LinkedResource(pictStream, "image/jpeg"))
                    End Select
                    picture(picture.Count - 1).ContentId = "IMGCSS2" & imgID
                    html = html.Replace(strInlineCSSURL, "cid:" & picture(picture.Count - 1).ContentId)
                Next




                html = html.Trim
                message.Body = html
                message.BodyEncoding = System.Text.Encoding.UTF8
                Dim alternate As System.Net.Mail.AlternateView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(html, System.Text.Encoding.UTF8, "text/html") '"multipart/related")
                alternate.TransferEncoding = System.Net.Mime.TransferEncoding.SevenBit 'Mime.TransferEncoding.SevenBit
                alternate.BaseUri = New Uri(url.ToString)
                For pic As Integer = 0 To picture.Count - 1
                    alternate.LinkedResources.Add(picture(pic))
                Next

                For ss As Integer = 0 To css.Count - 1
                    alternate.LinkedResources.Add(css(ss))
                Next
                message.AlternateViews.Add(alternate)
                Return message
            Catch ex As Exception
                Throw ex
            End Try
            Return Nothing
        End Function
        Public Function HtmlEmail_Text2HTML_Message(ByVal htmlBody As String, ByRef message As System.Net.Mail.MailMessage, Optional fileRootDirectory As String = "") As System.Net.Mail.MailMessage
            Try
                message.IsBodyHtml = True
                Dim cw As New System.Net.WebClient
                Dim html As String = htmlBody & ""

                Dim imgID As Integer = 0
                Dim picture As New System.Collections.Generic.List(Of System.Net.Mail.LinkedResource)
                For Each link As String In FetchImgSrcFromHTMLSource(html)
                    Dim picBytes() As Byte
                    Try
                        imgID += 1
                        Dim lnkOLD As String = link
                        If link.ToString.StartsWith("/") Then
                        End If
                        If File.Exists(fileRootDirectory.ToString().TrimEnd("\"c) & "\"c & link.ToString().TrimStart("\"c)) Then
                            Try
                                picBytes = File.ReadAllBytes(fileRootDirectory.ToString().TrimEnd("\"c) & "\"c & link.ToString().TrimStart("\"c))
                            Catch ex As Exception
                                GoTo GOTO_Next
                            End Try
                        ElseIf File.Exists(link.ToString) Then
                            Try
                                picBytes = File.ReadAllBytes(link.ToString)
                            Catch ex As Exception
                                GoTo GOTO_Next
                            End Try
                        Else
                            Try
                                Dim uriX As New Uri(link.ToString)
                                cw.BaseAddress = link.ToString.Substring(0, link.ToString.LastIndexOf("/"c)) & "" '"http" & IIf(HttpContext.Current.Request.IsSecureConnection, "s", "") & "://" & uriX.Authority
                                picBytes = cw.DownloadData(uriX.AbsolutePath)
                            Catch ex As Exception
                                GoTo GOTO_Next
                            End Try

                        End If
                        Dim pictStream As New MemoryStream(picBytes)

                        If pictStream.CanSeek Then
                            pictStream.Position = 0
                        End If

                        Select Case Path.GetExtension(link.ToString).ToLower.TrimStart(".")
                            Case "jpg"
                                picture.Add(New System.Net.Mail.LinkedResource(pictStream, "image/jpeg"))
                            Case "gif"
                                picture.Add(New System.Net.Mail.LinkedResource(pictStream, "image/gif"))
                            Case "png"
                                picture.Add(New System.Net.Mail.LinkedResource(pictStream, "image/png"))
                            Case "bmp"
                                picture.Add(New System.Net.Mail.LinkedResource(pictStream, "image/bmp"))
                            Case "tif"
                                picture.Add(New System.Net.Mail.LinkedResource(pictStream, "image/tif"))
                            Case Else
                                picture.Add(New System.Net.Mail.LinkedResource(pictStream, "image/jpeg"))
                        End Select
                        picture(picture.Count - 1).ContentId = "IMG" & imgID
                        html = html.Replace(lnkOLD, "cid:" & picture(picture.Count - 1).ContentId)
                    Catch ex1 As Exception
                        GoTo GOTO_Next
                    End Try
GOTO_Next:

                Next
                Dim css As New System.Collections.Generic.List(Of System.Net.Mail.LinkedResource)
                For Each link As String In FetchCSSSrcFromHTMLSource(html)
                    If True = True Then
                        imgID += 1
                        Dim lnkOLD As String = link
                        If link.ToString.StartsWith("/") Or Not (link.ToString.StartsWith("http")) Then
                        Else

                        End If
                        Dim pictStream As New MemoryStream()
                        Dim strCSS As String
                        Dim uriX As New Uri(link.ToString)
                        If File.Exists(link.ToString()) Then
                            strCSS = File.ReadAllText(link.ToString)
                        ElseIf File.Exists(fileRootDirectory.ToString().TrimEnd("\"c) & "\"c & link.ToString().TrimStart("\"c)) Then
                            strCSS = File.ReadAllText(fileRootDirectory.ToString().TrimEnd("\"c) & "\"c & link.ToString().TrimStart("\"c))
                        Else
                            cw.BaseAddress = link.ToString.Substring(0, link.ToString.LastIndexOf("/"c)) & "" '"http" & IIf(HttpContext.Current.Request.IsSecureConnection, "s", "") & "://" & uriX.Authority
                            strCSS = cw.DownloadString(link.ToString)
                        End If
                        Dim strCssURLOld As String

                        For Each strURL As String In FetchImgSrcFromCssSource2(strCSS)
                            strCssURLOld = strURL
                            If strURL.ToString.StartsWith("/") Or Not (link.ToString.StartsWith("http")) Then
                            Else

                            End If
                            strCSS = strCSS.Replace(strCssURLOld, strURL & "")
                            uriX = New Uri(link.ToString)
                            If File.Exists(link.ToString()) Then
                                pictStream = New MemoryStream(File.ReadAllBytes(link.ToString))
                            ElseIf File.Exists(fileRootDirectory.ToString().TrimEnd("\"c) & "\"c & link.ToString().TrimStart("\"c)) Then
                                pictStream = New MemoryStream(File.ReadAllBytes(fileRootDirectory.ToString().TrimEnd("\"c) & "\"c & link.ToString().TrimStart("\"c)))
                            Else
                                cw.BaseAddress = link.ToString.Substring(0, link.ToString.LastIndexOf("/"c)) & "" '"http" & IIf(HttpContext.Current.Request.IsSecureConnection, "s", "") & "://" & uriX.Authority
                                pictStream = New MemoryStream(cw.DownloadData(uriX.AbsolutePath))
                            End If
                            If pictStream.CanSeek Then
                                pictStream.Position = 0
                            End If
                            Select Case Path.GetExtension(strURL.ToString).ToLower.TrimStart(".")
                                Case "jpg"
                                    picture.Add(New System.Net.Mail.LinkedResource(pictStream, "image/jpeg"))
                                Case "gif"
                                    picture.Add(New System.Net.Mail.LinkedResource(pictStream, "image/gif"))
                                Case "png"
                                    picture.Add(New System.Net.Mail.LinkedResource(pictStream, "image/png"))
                                Case "bmp"
                                    picture.Add(New System.Net.Mail.LinkedResource(pictStream, "image/bmp"))
                                Case "tif"
                                    picture.Add(New System.Net.Mail.LinkedResource(pictStream, "image/tif"))
                                Case Else
                                    picture.Add(New System.Net.Mail.LinkedResource(pictStream, "image/jpeg"))
                            End Select
                            picture(picture.Count - 1).ContentId = "IMGCSS" & imgID
                            strCSS = strCSS.Replace(strURL, "cid:" & picture(picture.Count - 1).ContentId)
                        Next

                        Dim cssStream As New System.IO.MemoryStream(System.Text.Encoding.ASCII.GetBytes(strCSS))
                        If cssStream.CanSeek Then
                            cssStream.Position = 0
                        End If
                        Select Case Path.GetExtension(link.ToString).ToLower.TrimStart(".")
                            Case ".css"
                                css.Add(New System.Net.Mail.LinkedResource(cssStream, "text/css"))
                            Case Else
                                css.Add(New System.Net.Mail.LinkedResource(cssStream, "text/css"))
                        End Select
                        css(css.Count - 1).ContentId = "CSS" & imgID
                        html = html.Replace(lnkOLD, "cid:" & css(css.Count - 1).ContentId)
                    End If
                Next

                For Each strInlineCSSURL As String In FetchImgSrcFromCssSource2(html)
                    imgID += 1
                    Dim strCssURLOld As String = strInlineCSSURL
                    If strInlineCSSURL.ToString.StartsWith("/") Or Not (strInlineCSSURL.ToString.StartsWith("http")) Then
                    Else

                    End If
                    html = html.Replace(strCssURLOld, strInlineCSSURL & "")
                    Dim picBytes() As Byte = Nothing
                    If File.Exists(strInlineCSSURL.ToString) Then
                        picBytes = File.ReadAllBytes(strInlineCSSURL.ToString)
                    ElseIf File.Exists(fileRootDirectory.ToString().TrimEnd("\"c) & "\"c & strInlineCSSURL.ToString().TrimStart("\"c)) Then
                        picBytes = File.ReadAllBytes(fileRootDirectory.ToString().TrimEnd("\"c) & "\"c & strInlineCSSURL.ToString().TrimStart("\"c))
                    Else
                        Dim uriX = New Uri(strInlineCSSURL.ToString)
                        cw.BaseAddress = strInlineCSSURL.ToString.Substring(0, strInlineCSSURL.ToString.LastIndexOf("/"c)) & "" '"http" & IIf(HttpContext.Current.Request.IsSecureConnection, "s", "") & "://" & uriX.Authority
                        cw.DownloadData(uriX.AbsolutePath)
                    End If
                    Dim pictStream As New System.IO.MemoryStream(picBytes.ToArray())
                    If pictStream.CanSeek Then
                        pictStream.Position = 0
                    End If
                    Select Case Path.GetExtension(strInlineCSSURL.ToString).ToLower.TrimStart(".")
                        Case "jpg"
                            picture.Add(New System.Net.Mail.LinkedResource(pictStream, "image/jpeg"))
                        Case "gif"
                            picture.Add(New System.Net.Mail.LinkedResource(pictStream, "image/gif"))
                        Case "png"
                            picture.Add(New System.Net.Mail.LinkedResource(pictStream, "image/png"))
                        Case "bmp"
                            picture.Add(New System.Net.Mail.LinkedResource(pictStream, "image/bmp"))
                        Case "tif"
                            picture.Add(New System.Net.Mail.LinkedResource(pictStream, "image/tif"))
                        Case Else
                            picture.Add(New System.Net.Mail.LinkedResource(pictStream, "image/jpeg"))
                    End Select
                    picture(picture.Count - 1).ContentId = "IMGCSS2" & imgID
                    html = html.Replace(strInlineCSSURL, "cid:" & picture(picture.Count - 1).ContentId)
                Next


                html = html.Trim
                message.Body = html
                message.BodyEncoding = System.Text.Encoding.UTF8
                Dim alternate As System.Net.Mail.AlternateView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(message.Body, Nothing, "text/html") '"multipart/related")
                For pic As Integer = 0 To picture.Count - 1
                    alternate.LinkedResources.Add(picture(pic))
                Next

                For ss As Integer = 0 To css.Count - 1
                    alternate.LinkedResources.Add(css(ss))
                Next
                message.AlternateViews.Add(alternate)
            Catch ex As Exception
                Throw ex
            End Try
            Return message
        End Function
        Private Function FetchImgSrcFromCssSource2(ByVal cssSource As String) As System.Collections.Generic.List(Of String)
            Dim links As New System.Collections.Generic.List(Of String)
            Dim regexImgSrc As String = "url\(([^'"" >]*)\)"
            Dim matchesImgSrc As MatchCollection = Regex.Matches(cssSource, regexImgSrc, RegexOptions.IgnoreCase Or RegexOptions.Singleline)
            For Each m As Match In matchesImgSrc
                Dim href As String = m.Groups(1).Value
                href = href.TrimStart("'")
                href = href.TrimEnd("'")
                href = href.TrimStart("""")
                href = href.TrimEnd("""")
                links.Add(href)
            Next
            Return links
        End Function
        Private Function FetchImgSrcFromInlineCssSource2(ByVal cssSource As String) As System.Collections.Generic.List(Of String)
            Dim links As New System.Collections.Generic.List(Of String)
            Dim regexImgSrc As String = "\<style\s*url\(([^'"" >]*)\)\s*\<\/style\>"
            Dim matchesImgSrc As MatchCollection = Regex.Matches(cssSource, regexImgSrc, RegexOptions.IgnoreCase Or RegexOptions.Singleline)
            For Each m As Match In matchesImgSrc
                Dim href As String = m.Groups(1).Value
                href = href.TrimStart("'")
                href = href.TrimEnd("'")
                href = href.TrimStart("""")
                href = href.TrimEnd("""")
                links.Add(href)
            Next
            Return links
        End Function
        Private Function ConvertToBase64String(ByVal Filebytes As Byte()) As String
            Dim strModified As String = ""
            strModified = System.Convert.ToBase64String(Filebytes)
            Return strModified
        End Function
        Private Function ConvertToBase64String(ByVal image As System.Drawing.Image, ByVal newFormat As System.Drawing.Imaging.ImageFormat) As String
            Dim strModified As String = ""
            Dim imgStream As New MemoryStream
            image.Save(imgStream, newFormat)
            If imgStream.CanSeek Then
                imgStream.Position = 0
            End If
            Dim imgBytes(imgStream.Length) As Byte
            imgStream.Read(imgBytes, 0, imgStream.Length)
            strModified = System.Convert.ToBase64String(imgBytes)
            Return strModified
        End Function
        Private Function ConvertToBase64String(ByVal image As System.Drawing.Image) As String
            Dim strModified As String = ""
            Dim imgStream As New MemoryStream
            image.Save(imgStream, image.RawFormat)
            If imgStream.CanSeek Then
                imgStream.Position = 0
            End If
            Dim imgBytes(imgStream.Length) As Byte
            imgStream.Read(imgBytes, 0, imgStream.Length)
            strModified = System.Convert.ToBase64String(imgBytes)
            Return strModified
        End Function
        Public Function ConvertToBase64Byte(ByVal FileBytes As Byte()) As Byte()
            Dim bytModified() As Byte, strModified As String
            strModified = System.Convert.ToBase64String(FileBytes)
            bytModified = System.Text.Encoding.Default.GetBytes(strModified)
            Return bytModified
        End Function
        Private Function ConvertFromBase64ToString(ByVal strEncodedBase64 As String, ByVal ToEncoding As System.Text.Encoding) As String
            Dim b As Byte() = System.Convert.FromBase64String(strEncodedBase64)
            Dim strEncoded As String = ToEncoding.GetString(b)
            Return strEncoded
        End Function
        Private Function ConvertFromBase64ToByte(ByVal strEncodedBase64 As String, ByVal ToEncoding As System.Text.Encoding) As Byte()
            Dim b As Byte() = System.Convert.FromBase64String(strEncodedBase64)
            Dim bytEncoded As Byte() = System.Text.Encoding.Convert(System.Text.UTF8Encoding.UTF8, ToEncoding, b)
            Return bytEncoded
        End Function
#End Region
    End Class
End Class
