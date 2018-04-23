Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Web
Imports System.Net.Mail
Imports System.Net.Mail.MailMessage
Imports System.Diagnostics.Process
Imports System.Data.OleDb
Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography
' MAILMESSAGE CLASS
Namespace FDFApp
    Public Class Mail
        ''' <summary>
        ''' Created by Nicholas Kowalewicz - NK-INC.COM
        ''' Copyright (c) 2017 Nicholas Kowalewicz (NK-INC.COM). All rights reserved. 
        ''' Mail is a class in FDFToolkit.net (FDFApp).
        ''' FDFToolkit.net (FDFApp) utilizes iTextSharp technologies.
        ''' Website: www.fdftoolkit.net
        ''' </summary>
        Public Class SMTPServer
            Implements IDisposable
            Dim cdoSendUsingPort As Integer = 2
            Private _SMTPServer As New Net.Mail.SmtpClient
            Private _SMTPErrors As New FDFErrors
            Private _SMTPCredentials As New System.Net.NetworkCredential
            Enum FDFType
                FDF = 1
                xFDF = 2
                XML = 3
                PDF = 4
                XDP = 5
            End Enum
            Enum SMTPAuthenticate
                cdoNone = 0
                cdoBasic = 1
                cdoNTLM = 2
            End Enum

            Public Function SMTPHasErrors() As Boolean
                Return _SMTPErrors.FDFHasErrors
            End Function
            Public Sub ResetErrors()
                _SMTPErrors.ResetErrors()
            End Sub
            Private Property SMTPErrors() As FDFErrors
                Get
                    Return _SMTPErrors
                End Get
                Set(ByVal Value As FDFErrors)
                    _SMTPErrors = Value
                End Set
            End Property
            Public Function SMTPErrorsStr(Optional ByVal HTMLFormat As Boolean = False) As String
                Dim FDFErrors As FDFErrors
                Dim FDFError As FDFErrors.FDFError
                FDFErrors = _SMTPErrors
                Dim retString As String
                retString = CStr(IIf(HTMLFormat, "<br>", vbNewLine)) & "FDFDoc Errors:"
                For Each FDFError In FDFErrors.FDFErrors
                    retString = retString & CStr(IIf(HTMLFormat, "<br>", vbNewLine)) & vbTab & "Error: " & FDFError.FDFError & CStr(IIf(HTMLFormat, "<br>", vbNewLine)) & vbTab & "#: " & FDFError.FDFError_Number & CStr(IIf(HTMLFormat, "<br>", vbNewLine)) & vbTab & "Module: " & FDFError.FDFError_Module & CStr(IIf(HTMLFormat, "<br>", vbNewLine)) & vbTab & "Message: " & FDFError.FDFError_Msg & CStr(IIf(HTMLFormat, "<br>", vbNewLine))
                Next
                Return retString
            End Function
            Public Sub New()
                _SMTPServer = New Net.Mail.SmtpClient
                _SMTPServer.Port = 25
                _SMTPServer.Host = "localhost"
                _SMTPServer.Timeout = 30
                _SMTPCredentials = New System.Net.NetworkCredential
            End Sub
            Public Property SMTPConnectionTimeOut() As Integer
                Get
                    Return _SMTPServer.Timeout + 0
                End Get
                Set(ByVal Value As Integer)
                    _SMTPServer.Timeout = Value + 0
                End Set
            End Property
            Public Property SMTPServerPort() As Integer
                Get
                    Return _SMTPServer.Port + 0
                End Get
                Set(ByVal Value As Integer)
                    _SMTPServer.Port = Value + 0
                End Set
            End Property
            Public Property SMTPServerName() As String
                Get
                    Return _SMTPServer.Host & ""
                End Get
                Set(ByVal Value As String)
                    'If Not Value = "localhost" Then
                    '	_SMTPServer.SMTPUseAuthentication = True
                    'End If
                    _SMTPServer.Host = Value & ""
                End Set
            End Property
            Public Property SMTPPassword() As String
                Get
                    Return _SMTPCredentials.Password & ""
                End Get
                Set(ByVal Value As String)
                    _SMTPCredentials.Password = Value & ""
                End Set
            End Property
            Public Property SMTPUserName() As String
                Get
                    Return _SMTPCredentials.UserName & ""
                End Get
                Set(ByVal Value As String)
                    _SMTPCredentials.UserName = Value & ""
                End Set
            End Property
            Public Property SMTPCredentials() As Net.NetworkCredential
                Get
                    Return _SMTPCredentials
                End Get
                Set(ByVal value As Net.NetworkCredential)
                    _SMTPCredentials = value
                End Set
            End Property
            Public Sub NewNetworkCredentials(ByVal Username As String, ByVal Password As String, Optional ByVal Domain As String = "")
                If Domain = "" Then
                    _SMTPCredentials = New System.Net.NetworkCredential(Username, Password)
                Else
                    _SMTPCredentials = New System.Net.NetworkCredential(Username, Password, Domain)
                End If
            End Sub

            Public Function SendMail(ByVal MailMsg As Net.Mail.MailMessage, ByVal AttachFName As String, ByVal streamAttachment As Stream) As Boolean
                Dim _results As Boolean = True
                Dim cSmtp As New Net.Mail.SmtpClient
                cSmtp = _SMTPServer
                If Not streamAttachment Is Nothing Then
                    Dim MediaType As String = ""
                    streamAttachment.Position = 0
                    Dim att As New Net.Mail.Attachment(streamAttachment, AttachFName)
                    MailMsg.Attachments.Add(att)
                End If
                Try
                    If Not _SMTPCredentials Is Nothing Then
                        If _SMTPCredentials.UserName Is Nothing Or String_IsNullOrEmpty(_SMTPCredentials.UserName) Then
                            'cSmtp.DeliveryMethod = SmtpDeliveryMethod.Network
                            cSmtp.UseDefaultCredentials = True
                        Else
                            cSmtp.UseDefaultCredentials = False
                            'cSmtp.DeliveryMethod = SmtpDeliveryMethod.Network
                            cSmtp.Credentials = _SMTPCredentials
                        End If
                    Else
                        'cSmtp.DeliveryMethod = SmtpDeliveryMethod.Network
                        cSmtp.UseDefaultCredentials = True
                    End If
                    MailMsg.DeliveryNotificationOptions = Net.Mail.DeliveryNotificationOptions.OnFailure
                    cSmtp.Send(MailMsg)
                    _results = True
                    streamAttachment.Dispose()
                    MailMsg.Dispose()
                Catch ex As Exception
                    _SMTPErrors.FDFAddError(FDFErrors.FDFErc.FDFErcInternalError, "Error: " & ex.Message, "FDFApp.SMTPServer.SendMail", 1)
                    _results = False
                End Try
                Return _results

            End Function
            ' Return String if object is not null, else return empty.string
            Protected Function String_IsNullOrEmpty(ByVal s As Object) As Boolean
                If IsDBNull(s) Then
                    Return True
                Else
                    If String.IsNullOrEmpty(CStr(s) & "") Then
                        Return True
                    Else
                        Return False
                    End If
                End If
            End Function
            Public Property FDFSMTPClient() As Net.Mail.SmtpClient
                Get
                    Return _SMTPServer
                End Get
                Set(ByVal value As Net.Mail.SmtpClient)
                    _SMTPServer = value
                End Set
            End Property
            Public Function SendMail(ByVal MailMsg As Net.Mail.MailMessage, ByVal FileAttachment As String) As Boolean
                Dim _results As Boolean = True
                Dim cSmtp As New Net.Mail.SmtpClient
                cSmtp = _SMTPServer
                If Not FileAttachment Is Nothing Then
                    Dim att As New Net.Mail.Attachment(FileAttachment)
                    MailMsg.Attachments.Add(att)
                End If
                Try
                    If Not _SMTPCredentials Is Nothing Then
                        If _SMTPCredentials.UserName Is Nothing Or String_IsNullOrEmpty(_SMTPCredentials.UserName) Then
                            'cSmtp.DeliveryMethod = SmtpDeliveryMethod.Network
                            cSmtp.UseDefaultCredentials = True
                        Else
                            cSmtp.UseDefaultCredentials = False
                            'cSmtp.DeliveryMethod = SmtpDeliveryMethod.Network
                            cSmtp.Credentials = _SMTPCredentials
                        End If
                    Else
                        'cSmtp.DeliveryMethod = SmtpDeliveryMethod.Network
                        cSmtp.UseDefaultCredentials = True
                    End If
                    MailMsg.DeliveryNotificationOptions = Net.Mail.DeliveryNotificationOptions.OnFailure
                    cSmtp.Send(MailMsg)
                    MailMsg.Dispose()
                    _results = True
                Catch ex As Exception
                    _SMTPErrors.FDFAddError(FDFErrors.FDFErc.FDFErcInternalError, "Error: " & ex.Message, "FDFApp.SMTPServer.SendMail", 1)
                    _results = False
                End Try
                Return _results
            End Function

            Public Function SendMail(ByVal MailMsg As Net.Mail.MailMessage, ByVal AttachFName As String, ByVal bytAttachment() As Byte) As Boolean
                Dim _results As Boolean = True
                Dim cSmtp As New Net.Mail.SmtpClient
                cSmtp = _SMTPServer
                Dim xmem As New MemoryStream
                If Not bytAttachment Is Nothing Then
                    xmem = New MemoryStream(bytAttachment)
                    Dim att As New Net.Mail.Attachment(xmem, AttachFName)
                    MailMsg.Attachments.Add(att)
                End If

                Try
                    If Not _SMTPCredentials Is Nothing Then
                        If _SMTPCredentials.UserName Is Nothing Or String_IsNullOrEmpty(_SMTPCredentials.UserName) Then
                            'cSmtp.DeliveryMethod = SmtpDeliveryMethod.Network
                            cSmtp.UseDefaultCredentials = True
                        Else
                            cSmtp.UseDefaultCredentials = False
                            'cSmtp.DeliveryMethod = SmtpDeliveryMethod.Network
                            cSmtp.Credentials = _SMTPCredentials
                        End If
                    Else
                        'cSmtp.DeliveryMethod = SmtpDeliveryMethod.Network
                        cSmtp.UseDefaultCredentials = True
                    End If
                    MailMsg.DeliveryNotificationOptions = Net.Mail.DeliveryNotificationOptions.OnFailure
                    cSmtp.Send(MailMsg)
                    'Threading.Thread.Sleep(200)
                    _results = True
                    xmem.Dispose()
                    MailMsg.Dispose()
                Catch ex As Exception
                    _SMTPErrors.FDFAddError(FDFErrors.FDFErc.FDFErcInternalError, "Error: " & ex.Message, "FDFApp.SMTPServer.SendMail", 1)
                    _results = False
                End Try
                Return _results
            End Function
            Public Function PDFSendEmail(ByVal MailMsg As Net.Mail.MailMessage, ByVal PDFData As Byte(), Optional ByVal FileType As FDFDoc_Class.FDFType = FDFDoc_Class.FDFType.PDF, Optional ByVal Flatten As Boolean = False) As Boolean
                Dim _result As Boolean
                Try
                    _result = SendMail(MailMsg, "PDFFORMDATA.pdf", PDFData)
                    Return _result
                Catch Ex As Exception
                    _SMTPErrors.FDFAddError(FDFErrors.FDFErc.FDFErcFileSysErr, "Error: " & Ex.Message, "FDFApp.SMTPServer.PDFSendEmail", 3)
                    Return False
                End Try
            End Function
            Public Function PDFSendEmail(ByVal MailMsg As Net.Mail.MailMessage, ByVal PDFData As Byte(), ByVal PDF_ATTACHMENT_NAME As String, Optional ByVal FileType As FDFDoc_Class.FDFType = FDFDoc_Class.FDFType.PDF, Optional ByVal Flatten As Boolean = False) As Boolean
                Dim _result As Boolean
                Try
                    _result = SendMail(MailMsg, PDF_ATTACHMENT_NAME, PDFData)
                    Return _result
                Catch Ex As Exception
                    _SMTPErrors.FDFAddError(FDFErrors.FDFErc.FDFErcFileSysErr, "Error: " & Ex.Message, "FDFApp.SMTPServer.PDFSendEmail", 3)
                    Return False
                End Try
            End Function

            Public Function FDFSendEmail(ByVal MailMsg As Net.Mail.MailMessage, Optional ByVal FDFDocument As FDFDoc_Class = Nothing, Optional ByVal FileType As FDFDoc_Class.FDFType = FDFDoc_Class.FDFType.FDF) As Boolean
                Dim _result As Boolean
                'Dim xToday As String
                '            xToday = Today.ToString
                Try
                    FDFDocument.FDFData = FDFDocument.FDFSavetoStr(FileType, True)
                    Dim AttachX As Byte()
                    AttachX = FDFDocument.FDFSavetoBuf(FileType)
                    Dim FType As String = "fdf"
                    Select Case FileType
                        Case FDFDoc_Class.FDFType.FDF
                            FType = "fdf"
                        Case FDFDoc_Class.FDFType.xFDF
                            FType = "xfdf"
                        Case FDFDoc_Class.FDFType.XML
                            FType = "xml"
                        Case FDFDoc_Class.FDFType.XDP
                            FType = "xdp"
                        Case Else
                            FType = "fdf"
                    End Select
                    _result = SendMail(MailMsg, "PDFFORMDATA." & FType, AttachX)
                    Return _result
                Catch Ex As Exception
                    _SMTPErrors.FDFAddError(FDFErrors.FDFErc.FDFErcFileSysErr, "Error: " & Ex.Message, "FDFApp.SMTPServer.FDFSendEmail", 3)
                    Return False
                End Try
            End Function

            Public Function FDFSendEmail(ByVal MailMsg As Net.Mail.MailMessage, ByVal FDFDocument As FDFDoc_Class, ByVal FileName As String, Optional ByVal FileType As FDFDoc_Class.FDFType = FDFDoc_Class.FDFType.FDF) As Boolean
                Dim _result As Boolean
                'Dim xToday As String
                'xToday = Today
                Try
                    FDFDocument.FDFData = FDFDocument.FDFSavetoStr(FileType, True)
                    Dim AttachX As Byte()
                    AttachX = FDFDocument.FDFSavetoBuf(FileType)
                    Dim FType As String
                    Dim tmpFilename As String = ""
                    If FileName.LastIndexOf(".") > 0 Then
                        tmpFilename = FileName.Substring(0, FileName.LastIndexOf("."))
                        tmpFilename = tmpFilename.TrimEnd("."c)
                    Else
                        tmpFilename = FileName
                    End If
                    If Not tmpFilename.Length > 0 Then
                        tmpFilename = FileName
                    Else
                        tmpFilename = tmpFilename
                    End If
                    If tmpFilename.Length <= 0 Then
                        tmpFilename = "PDFAttachment"
                    End If
                    Select Case FileType
                        Case FDFDoc_Class.FDFType.FDF
                            FType = "fdf"
                        Case FDFDoc_Class.FDFType.xFDF
                            FType = "xfdf"
                        Case FDFDoc_Class.FDFType.XML
                            FType = "xml"
                        Case FDFDoc_Class.FDFType.XDP
                            FType = "xdp"
                        Case Else
                            FType = "fdf"
                    End Select
                    _result = SendMail(MailMsg, tmpFilename & "." & FType, AttachX)
                    Return _result
                Catch Ex As Exception
                    _SMTPErrors.FDFAddError(FDFErrors.FDFErc.FDFErcFileSysErr, "Error: " & Ex.Message, "FDFApp.SMTPServer.FDFSendEmail", 3)
                    Return False
                End Try
            End Function

            Private Function Determine_FDFType(ByVal FDFData As String) As FDFDoc_Class.FDFType
                If FDFData.StartsWith("%FDF") Then
                    Return FDFDoc_Class.FDFType.FDF
                ElseIf FDFData.StartsWith("<?xml version=""1.0""") Then
                    If InStrRev(FDFData, "<xfdf", , CompareMethod.Text) > 0 Then
                        Return FDFDoc_Class.FDFType.xFDF
                    Else
                        Return FDFDoc_Class.FDFType.XML
                    End If
                ElseIf FDFData.StartsWith("%PDF") Then
                    Return FDFDoc_Class.FDFType.PDF
                Else
                    Me._SMTPErrors.FDFAddError(FDFErrors.FDFErc.FDFErcBadFDF, "Error: Bad FDF or Unknown FDF Type", "Mail.FDFType", 1)
                    Return FDFDoc_Class.FDFType.FDF
                End If
            End Function
            Private Function CreateTempFileName(ByVal cFDFDoc As FDFDoc_Class, ByVal PathTempDirectory As String, Optional ByVal FileType As FDFDoc_Class.FDFType = FDFDoc_Class.FDFType.FDF) As String
                Dim strFN As String = ""
                Dim xRan As Integer = 0
                ' Random FileName
                Randomize()
                xRan = CInt(Rnd() * 9999) + 1
                Try
                    ' Save the FDF Locally
                    'Select Case Determine_FDFType(cFDFDoc.FDFData)
                    Select Case FileType
                        Case FDFDoc_Class.FDFType.FDF
                            If FileType = FDFDoc_Class.FDFType.PDF Then
                                FileType = FDFDoc_Class.FDFType.FDF
                            End If
                            strFN = "FDFAttachment_" & xRan
                            strFN = strFN & "." & "fdf"
                        Case FDFDoc_Class.FDFType.PDF
                            FileType = FDFDoc_Class.FDFType.PDF
                            strFN = "PDFAttachment_" & xRan
                            strFN = strFN & "." & "pdf"
                        Case FDFDoc_Class.FDFType.xFDF
                            If FileType = FDFDoc_Class.FDFType.PDF Then
                                FileType = FDFDoc_Class.FDFType.xFDF
                            End If
                            strFN = "xFDFAttachment_" & xRan
                            strFN = strFN & "." & "xfdf"
                        Case FDFDoc_Class.FDFType.XML
                            If FileType = FDFDoc_Class.FDFType.PDF Then
                                FileType = FDFDoc_Class.FDFType.XML
                            End If
                            strFN = "XMLAttachment_" & xRan
                            strFN = strFN & "." & "xml"
                    End Select

                    Dim strFDFPath As String = "", cFDFApp As New FDFApp_Class
                    strFDFPath = PathTempDirectory & strFN
                    If File.Exists(strFDFPath) Then
                        File.Delete(strFDFPath)
                    End If
                    'Dim binarywrite As New BinaryWriter(File.OpenWrite(strFDFPath))
                    Dim binarywrite As New BinaryWriter(File.Create(strFDFPath))
                    If FileType = FDFDoc_Class.FDFType.PDF Then
                        binarywrite.Write(cFDFDoc.PDFMergeFDF2Buf(cFDFDoc.FDFGetFile, True))
                    Else
                        binarywrite.Write(cFDFDoc.FDFSavetoBuf(FileType, True))
                    End If

                    binarywrite.Flush()
                    binarywrite.Close()

                    Return strFDFPath
                Catch Ex As Exception
                    _SMTPErrors.FDFAddError(FDFErrors.FDFErc.FDFErcFileSysErr, "Error: " & Ex.Message, "FDFApp.FDFCreateTempFileName", 1)
                    Return ""
                End Try
            End Function
            Private Function CreateTempPDFFileName(ByVal PDFPath As String, ByVal PathTempDirectory As String, Optional ByVal FileType As FDFDoc_Class.FDFType = FDFDoc_Class.FDFType.PDF) As String
                Dim strFN As String
                Dim xRan As Integer
                ' Random FileName
                Randomize()
                xRan = CInt(Rnd() * 9999) + 1
                strFN = "PDFAttachment_" & xRan
                Try
                    ' Save the FDF Locally
                    FileType = FDFDoc_Class.FDFType.PDF
                    strFN = strFN & "." & "pdf"

                    Dim strFDFPath As String, cFDFApp As New FDFApp_Class
                    strFDFPath = PathTempDirectory & strFN
                    If File.Exists(strFDFPath) Then
                        File.Delete(strFDFPath)
                    End If
                    Dim binarywrite As New BinaryWriter(File.OpenWrite(strFDFPath))
                    If FileType = FDFDoc_Class.FDFType.PDF Then
                        binarywrite.Write(cFDFApp.PDFSavetoBuf(PDFPath))
                    End If

                    binarywrite.Flush()
                    binarywrite.Close()

                    Return strFDFPath
                Catch Ex As Exception
                    _SMTPErrors.FDFAddError(FDFErrors.FDFErc.FDFErcFileSysErr, "Error: " & Ex.Message, "FDFApp.FDFCreateTempFileName", 1)
                    Return ""
                End Try
            End Function
            Private Function CreateTempPDFFileName(ByVal PDFData As Byte(), ByVal PathTempDirectory As String, Optional ByVal FileType As FDFDoc_Class.FDFType = FDFDoc_Class.FDFType.PDF) As String
                Dim strFN As String
                Dim xRan As Integer
                ' Random FileName
                Randomize()
                xRan = CInt(Rnd() * 9999) + 1
                strFN = "PDFAttachment_" & xRan
                Try
                    ' Save the FDF Locally
                    FileType = FDFDoc_Class.FDFType.PDF
                    strFN = strFN & "." & "pdf"

                    Dim strFDFPath As String, cFDFApp As New FDFApp_Class
                    strFDFPath = PathTempDirectory & strFN
                    If File.Exists(strFDFPath) Then
                        File.Delete(strFDFPath)
                    End If
                    Dim binarywrite As New BinaryWriter(File.OpenWrite(strFDFPath))
                    If FileType = FDFDoc_Class.FDFType.PDF Then
                        binarywrite.Write(PDFData)
                    End If

                    binarywrite.Flush()
                    binarywrite.Close()

                    Return strFDFPath
                Catch Ex As Exception
                    _SMTPErrors.FDFAddError(FDFErrors.FDFErc.FDFErcFileSysErr, "Error: " & Ex.Message, "FDFApp.FDFCreateTempFileName", 1)
                    Return ""
                End Try
            End Function
            Private Sub Add_Attachments(ByVal email As System.Net.Mail.MailMessage, ByVal strAttachments() As String)
                Dim sAttach_Array() As String, sAttach As String
                Dim nFileLen As Integer = 0, strFileName As String = "", attach As Net.Mail.Attachment
                Try
                    If Not strAttachments Is Nothing Then
                        sAttach_Array = strAttachments
                    Else
                        sAttach_Array = Nothing
                    End If
                    If Not sAttach_Array Is Nothing Then
                        For Each sAttach In sAttach_Array
                            If sAttach.Length > 0 Then
                                strFileName = sAttach
                                If File.Exists(strFileName) Then
                                    attach = New Net.Mail.Attachment(strFileName)
                                    email.Attachments.Add(attach)
                                End If
                            End If
                        Next
                    End If
                Catch ex As Exception
                    _SMTPErrors.FDFAddError(FDFErrors.FDFErc.FDFErcFileSysErr, "Error: " & ex.Message, "FDFApp.SMTPServer.Add_Attachment", 1)
                End Try
            End Sub
            Private Sub Delete_Attachments(ByVal strAttachments() As String, Optional ByVal DeleteFile As Boolean = False)
                Dim sAttach_Array() As String, sAttach As String
                Dim nFileLen As Integer = 0, strFileName As String
                Try
                    If Not strAttachments Is Nothing Then
                        sAttach_Array = strAttachments
                    Else
                        sAttach_Array = Nothing
                    End If
                    If Not sAttach_Array Is Nothing Then
                        For Each sAttach In sAttach_Array
                            If sAttach.Length > 0 Then
                                strFileName = sAttach
                                If File.Exists(strFileName) Then
                                    If DeleteFile = True Then
                                        File.Delete(strFileName)
                                    End If
                                End If
                            End If
                        Next
                    End If
                Catch ex As Exception
                    _SMTPErrors.FDFAddError(FDFErrors.FDFErc.FDFErcFileSysErr, "Error: " & ex.Message, "FDFApp.SMTPServer.Delete_Attachments", 1)
                End Try
            End Sub

            ' ADDED 2008-09-03
            Public Function FDFSendEmail(ByVal MailMsg As Net.Mail.MailMessage, ByVal FDFDocument As FDFDoc_Class, ByVal FileType As FDFDoc_Class.FDFType, ByVal EncryptionKey As String, Optional ByVal EncryptionProvider As Encryption.Symmetric.Provider = Encryption.Symmetric.Provider.Rijndael) As Boolean
                Dim _result As Boolean
                'Dim xToday As String
                'xToday = Today
                Try
                    FDFDocument.FDFData = FDFDocument.FDFSavetoStr(FileType, True)
                    Dim AttachX As Byte()
                    AttachX = FDFDocument.FDFSavetoBuf(FileType)
                    Dim clsEncrypt As New Encryption.Asymmetric()
                    Dim sym As New Encryption.Symmetric(EncryptionProvider)
                    sym.Key = New Encryption.Data(EncryptionKey)
                    Dim encryptedData As New Encryption.Data
                    encryptedData = sym.Encrypt(FDFDocument.FDFSavetoStream(FileType, True))

                    Dim FType As String = "fdf"
                    Select Case FileType
                        Case FDFDoc_Class.FDFType.FDF
                            FType = "fdf"
                        Case FDFDoc_Class.FDFType.xFDF
                            FType = "xfdf"
                        Case FDFDoc_Class.FDFType.XML
                            FType = "xml"
                        Case FDFDoc_Class.FDFType.XDP
                            FType = "xdp"
                        Case Else
                            FType = "fdf"
                    End Select
                    _result = SendMail(MailMsg, "PDFFORMDATA." & FType, encryptedData.Bytes)
                    Return _result
                Catch Ex As Exception
                    _SMTPErrors.FDFAddError(FDFErrors.FDFErc.FDFErcFileSysErr, "Error: " & Ex.Message, "FDFApp.SMTPServer.FDFSendEmail", 3)
                    Return False
                End Try
            End Function
            Public Function FDFSendEmail(ByVal MailMsg As Net.Mail.MailMessage, ByVal DecodedFDFDocument() As Byte, ByVal FileType As FDFDoc_Class.FDFType, ByVal EncryptionKey As String, Optional ByVal EncryptionProvider As Encryption.Symmetric.Provider = Encryption.Symmetric.Provider.Rijndael) As Boolean
                Dim _result As Boolean
                'Dim xToday As String
                'xToday = Today
                Try
                    Dim memstream As New MemoryStream(DecodedFDFDocument)
                    Dim clsEncrypt As New Encryption.Asymmetric()
                    Dim sym As New Encryption.Symmetric(EncryptionProvider)
                    sym.Key = New Encryption.Data(EncryptionKey)
                    Dim encryptedData As New Encryption.Data
                    encryptedData = sym.Encrypt(memstream)

                    Dim FType As String = "fdf"
                    Select Case FileType
                        Case FDFDoc_Class.FDFType.FDF
                            FType = "fdf"
                        Case FDFDoc_Class.FDFType.xFDF
                            FType = "xfdf"
                        Case FDFDoc_Class.FDFType.XML
                            FType = "xml"
                        Case FDFDoc_Class.FDFType.XDP
                            FType = "xdp"
                        Case Else
                            FType = "fdf"
                    End Select
                    _result = SendMail(MailMsg, "PDFFORMDATA." & FType, encryptedData.Bytes)
                    Return _result
                Catch Ex As Exception
                    _SMTPErrors.FDFAddError(FDFErrors.FDFErc.FDFErcFileSysErr, "Error: " & Ex.Message, "FDFApp.SMTPServer.FDFSendEmail", 3)
                    Return False
                End Try
            End Function
            Public Function FDFSendEmail(ByVal MailMsg As Net.Mail.MailMessage, ByVal EncodedFDFDocument() As Byte, ByVal FileType As FDFDoc_Class.FDFType) As Boolean
                Dim _result As Boolean
                'Dim xToday As String
                'xToday = Today
                Try
                    Dim memstream As New MemoryStream(EncodedFDFDocument)

                    Dim FType As String = "fdf"
                    Select Case FileType
                        Case FDFDoc_Class.FDFType.FDF
                            FType = "fdf"
                        Case FDFDoc_Class.FDFType.xFDF
                            FType = "xfdf"
                        Case FDFDoc_Class.FDFType.XML
                            FType = "xml"
                        Case FDFDoc_Class.FDFType.XDP
                            FType = "xdp"
                        Case Else
                            FType = "fdf"
                    End Select
                    _result = SendMail(MailMsg, "PDFFORMDATA." & FType, EncodedFDFDocument)
                    Return _result
                Catch Ex As Exception
                    _SMTPErrors.FDFAddError(FDFErrors.FDFErc.FDFErcFileSysErr, "Error: " & Ex.Message, "FDFApp.SMTPServer.FDFSendEmail", 3)
                    Return False
                End Try
            End Function

            Private disposedValue As Boolean = False  ' To detect redundant calls

            ' IDisposable
            Protected Overridable Sub Dispose(ByVal disposing As Boolean)
                If Not Me.disposedValue Then
                    If disposing Then
                        Try
                            _SMTPCredentials = Nothing
                            _SMTPErrors = Nothing
                            _SMTPServer = Nothing
                        Catch ex As Exception

                        End Try
                    End If

                End If
                Me.disposedValue = True
            End Sub

#Region " IDisposable Support "
            ' This code added by Visual Basic to correctly implement the disposable pattern.
            Public Sub Dispose() Implements IDisposable.Dispose
                ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
                Dispose(True)
                GC.SuppressFinalize(Me)
            End Sub
#End Region

        End Class
        'Public Enum MailFormat
        '	HTML = 1
        '	TEXT = 0
        'End Enum

        Public Class MailMessage
            Implements IDisposable

            Private _MailMessage As New System.Net.Mail.MailMessage
            Public Property msgMailMessage() As Net.Mail.MailMessage
                Get
                    Return _MailMessage
                End Get
                Set(ByVal value As Net.Mail.MailMessage)
                    _MailMessage = value
                End Set
            End Property

            Public Sub New()
                _MailMessage = New System.Net.Mail.MailMessage
            End Sub
            Public Sub New(ByVal msg As Net.Mail.MailMessage)
                _MailMessage = New System.Net.Mail.MailMessage
                _MailMessage = msg
            End Sub
            Public Enum MailFormat
                Text = 0
                HTML = 1
            End Enum
            Private Structure strucEmailAddress
                Dim RecName As String
                Dim RecEmail As String
            End Structure
            Public Sub Add_Recipient(ByVal Email As String, ByVal Name As String)
                Dim Addr As Net.Mail.MailAddress
                If Name.Length = 0 And Email.Length > 0 Then
                    Addr = New Net.Mail.MailAddress(Email)
                    _MailMessage.To.Add(Addr)
                ElseIf Email.Length > 0 And Name.Length > 0 Then
                    Addr = New Net.Mail.MailAddress(Email & "", Name & "")
                    _MailMessage.To.Add(Addr)
                End If
            End Sub
            Public Sub Add_Recipient(ByVal Email As String)
                _MailMessage.To.Add(Email)
            End Sub
            Public Sub Add_Recipients(ByVal EmailList As String)
                _MailMessage.To.Add(EmailList)
            End Sub
            Private Function Return_Recipients(ByVal EmailAddresses As MailAddressCollection) As String
                Dim strEmailList As String = ""
                If EmailAddresses.Count <= 0 Then
                    Return ""
                    Exit Function
                End If
                Dim EmailAddress As Net.Mail.MailAddress
                Try

                    For Each EmailAddress In EmailAddresses
                        Select Case EmailAddress.DisplayName
                            Case ""
                                If Not EmailAddress.Address = "" Then
                                    strEmailList = strEmailList & CStr(IIf(strEmailList = "", "", "; ")) & EmailAddress.Address
                                End If
                            Case Nothing
                                If Not EmailAddress.Address = "" Then
                                    strEmailList = strEmailList & CStr(IIf(strEmailList = "", "", "; ")) & EmailAddress.Address
                                End If
                            Case Else
                                If Not EmailAddress.Address = "" Then
                                    strEmailList = strEmailList & CStr(IIf(strEmailList = "", "", "; ")) & EmailAddress.DisplayName & " (" & EmailAddress.Address & ")"
                                End If
                        End Select
                    Next
                    Return strEmailList
                Catch ex As Exception
                    '_FDFErrors.FDFAddError(FDFErrors.FDFErc.FDFErcInternalError, "Error: " & ex.Message, "MailMessage.Return_Recipients", 1)
                    Return ""
                    Exit Function
                End Try

            End Function
            Public Sub Add_CcRecipient(ByVal Email As String, Optional ByVal Name As String = "")
                Dim Addr As Net.Mail.MailAddress
                If Name.Length = 0 And Email.Length > 0 Then
                    Addr = New Net.Mail.MailAddress(Email)
                    _MailMessage.CC.Add(Addr)
                ElseIf Email.Length > 0 And Name.Length > 0 Then
                    Addr = New Net.Mail.MailAddress(Email & "", Name & "")
                    _MailMessage.CC.Add(Addr)
                End If
            End Sub
            Public Sub Add_BccRecipient(ByVal Email As String, Optional ByVal Name As String = "")
                Dim Addr As Net.Mail.MailAddress
                If Name.Length = 0 And Email.Length > 0 Then
                    Addr = New Net.Mail.MailAddress(Email)
                    _MailMessage.Bcc.Add(Addr)
                ElseIf Email.Length > 0 And Name.Length > 0 Then
                    Addr = New Net.Mail.MailAddress(Email & "", Name & "")
                    _MailMessage.Bcc.Add(Addr)
                End If
            End Sub
            Public Property msgFromAddress() As MailAddress
                Get
                    Return _MailMessage.From
                End Get
                Set(ByVal value As MailAddress)
                    _MailMessage.From = value
                End Set
            End Property
            Public Sub Add_FromAddress(ByVal Email As String, Optional ByVal Name As String = "")
                Dim Addr As Net.Mail.MailAddress
                If Name.Length = 0 And Email.Length > 0 Then
                    Addr = New Net.Mail.MailAddress(Email)
                    _MailMessage.From = Addr
                    _MailMessage.Sender = Addr
                ElseIf Email.Length > 0 And Name.Length > 0 Then
                    Addr = New Net.Mail.MailAddress(Email, Name)
                    _MailMessage.From = Addr
                    _MailMessage.Sender = Addr
                End If
            End Sub
            Public Property msgTo() As Net.Mail.MailAddressCollection
                Get
                    Return _MailMessage.To
                End Get
                Set(ByVal Value As Net.Mail.MailAddressCollection)
                    _MailMessage.To.Clear()
                    For Each x As Net.Mail.MailAddress In Value
                        _MailMessage.To.Add(x)
                    Next
                End Set
            End Property
            Public Property msgSubject() As String
                Get
                    Return _MailMessage.Subject & ""
                End Get
                Set(ByVal Value As String)
                    _MailMessage.Subject = Value & ""
                End Set
            End Property
            Public Property msgBody() As String
                Get
                    Return _MailMessage.Body & ""
                End Get
                Set(ByVal Value As String)
                    _MailMessage.Body = Value & ""
                End Set
            End Property
            Public Property msgCc() As Net.Mail.MailAddressCollection
                Get
                    Return _MailMessage.CC
                End Get
                Set(ByVal Value As Net.Mail.MailAddressCollection)
                    _MailMessage.CC.Clear()
                    For Each x As Net.Mail.MailAddress In Value
                        _MailMessage.CC.Add(x)
                    Next
                End Set
            End Property
            Public Property msgBcc() As Net.Mail.MailAddressCollection
                Get
                    Return _MailMessage.Bcc
                End Get
                Set(ByVal Value As Net.Mail.MailAddressCollection)
                    _MailMessage.Bcc.Clear()
                    For Each x As Net.Mail.MailAddress In Value
                        _MailMessage.Bcc.Add(x)
                    Next
                End Set
            End Property

            Public Property msgBodyFormatIsHTML() As Boolean
                Get
                    Return _MailMessage.IsBodyHtml
                End Get
                Set(ByVal isHTML As Boolean)
                    _MailMessage.IsBodyHtml = isHTML
                End Set
            End Property

            Public Sub Add_Attachment(ByVal streamAttachment As Stream, ByVal FileName As String, ByVal MimeType As String)
                Dim nFileLen As Integer = 0, strFileName As String = "", attach As Net.Mail.Attachment
                Try
                    If Not streamAttachment Is Nothing Then
                        attach = New Net.Mail.Attachment(streamAttachment, FileName, MimeType)
                        _MailMessage.Attachments.Add(attach)
                    End If
                Catch ex As Exception
                    '_SMTPErrors.FDFAddError(FDFErrors.FDFErc.FDFErcFileSysErr, "Error: " & ex.Message, "FDFApp.SMTPServer.Add_Attachment", 1)
                End Try
            End Sub
            Public Sub Add_Attachment(ByVal bytAttachment() As Byte, ByVal FileName As String, ByVal MimeType As String)
                Dim nFileLen As Integer = 0, strFileName As String = "", attach As Net.Mail.Attachment
                Try
                    If Not bytAttachment Is Nothing Then
                        Dim streamAttachment As New MemoryStream(bytAttachment)
                        attach = New Net.Mail.Attachment(streamAttachment, FileName, MimeType)
                        _MailMessage.Attachments.Add(attach)
                    End If
                Catch ex As Exception
                    '_SMTPErrors.FDFAddError(FDFErrors.FDFErc.FDFErcFileSysErr, "Error: " & ex.Message, "FDFApp.SMTPServer.Add_Attachment", 1)
                End Try
            End Sub
            Public Sub Add_Attachment(ByVal FileName As String, ByVal MimeType As String)
                Dim nFileLen As Integer = 0, strFileName As String = "", attach As Net.Mail.Attachment
                Try
                    If File.Exists(FileName) Then
                        attach = New Net.Mail.Attachment(FileName, MimeType)
                        _MailMessage.Attachments.Add(attach)
                    End If
                Catch ex As Exception
                    '_SMTPErrors.FDFAddError(FDFErrors.FDFErc.FDFErcFileSysErr, "Error: " & ex.Message, "FDFApp.SMTPServer.Add_Attachment", 1)
                End Try
            End Sub
            Public Sub Add_Attachment(ByVal FileName As String)
                Dim nFileLen As Integer = 0, strFileName As String = "", attach As Net.Mail.Attachment
                Try
                    If File.Exists(FileName) Then
                        attach = New Net.Mail.Attachment(FileName)
                        _MailMessage.Attachments.Add(attach)
                    End If
                Catch ex As Exception
                    '_SMTPErrors.FDFAddError(FDFErrors.FDFErc.FDFErcFileSysErr, "Error: " & ex.Message, "FDFApp.SMTPServer.Add_Attachment", 1)
                End Try
            End Sub



            Private disposedValue As Boolean = False  ' To detect redundant calls

            ' IDisposable
            Protected Overridable Sub Dispose(ByVal disposing As Boolean)
                If Not Me.disposedValue Then
                    If disposing Then
                        ' TODO: free other state (managed objects).
                        Try
                            _MailMessage = Nothing

                        Catch ex As Exception

                        End Try


                    End If

                    ' TODO: free your own state (unmanaged objects).
                    ' TODO: set large fields to null.
                End If
                Me.disposedValue = True
            End Sub

#Region " IDisposable Support "
            ' This code added by Visual Basic to correctly implement the disposable pattern.
            Public Sub Dispose() Implements IDisposable.Dispose
                ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
                Dispose(True)
                GC.SuppressFinalize(Me)
            End Sub
#End Region

        End Class

    End Class
End Namespace
