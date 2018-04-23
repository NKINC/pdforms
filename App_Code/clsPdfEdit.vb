Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO
Imports System.Net
Imports System.Web
Imports System.String
Imports System.Drawing.Drawing2D
Imports System.Drawing
Imports System.Drawing.Imaging
Imports FDFApp
Imports FDFApp.FDFApp_Class
Imports FDFApp.FDFDoc_Class
Public Class clsPdfEdit
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 Nicholas Kowalewicz All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>

    Private appPath As String = Application.StartupPath.ToString.TrimEnd("\"c) & "\"
    Private fpath As String = appPath & "acro.pdf"
    Private ipath As String = appPath & "acro2.jpg"
    Private _pdfURI As String
    Private _pdfData() As Byte
    Private _fdfData() As Byte
    Private _outPutType As FDFApp.FDFApp_Class.FDFType
    Private _flatten As Boolean = False
    Private _openPassword As String = ""
    Private _ownerPassword As String = ""
    Private cFDFApp As New FDFApp.FDFApp_Class
    Private cFDFDoc As New FDFApp.FDFDoc_Class
    Private pdfParameters() As List(Of String)
    Private _sid As String = "input"
    Private Function GetUsedBytesOnly(ByRef m As MemoryStream, Optional ByVal closeStream As Boolean = False) As Byte()
        If m.CanSeek Then m.Position = 0
        Dim bytes As Byte() = m.GetBuffer()
        Dim i As Integer = 0
        For i = bytes.Length - 1 To 1 Step -1
            If bytes(i) <> 0 Then
                Exit For
            End If
        Next
        Dim newBytes As Byte() = New Byte(i - 1) {}
        Array.Copy(bytes, newBytes, i)
        ReDim bytes(0)
        bytes = Nothing
        If closeStream Then
            m.Close()
            m.Dispose()
        End If
        Return newBytes
    End Function
    Public Function GetUsedBytesOnly(ByRef b() As Byte) As Byte()
        Dim bytes As Byte() = b
        Dim i As Integer = 0
        For i = bytes.Length - 1 To 1 Step -1
            If bytes(i) <> 0 Then
                Exit For
            End If
        Next
        Dim newBytes As Byte() = New Byte(i - 1) {}
        Array.Copy(bytes, newBytes, i)
        ReDim bytes(0)
        bytes = Nothing
        Return newBytes
    End Function
    Dim memName As String
    Public mem As New Dictionary(Of String, Byte())
    Private Property Load2Memory(ByVal name As String) As Byte()
        Get
            If String.IsNullOrEmpty(name & "") Then
                memName = "default"
            End If
            If mem(memName) Is Nothing Then
                Return Nothing
            End If
            Return mem(memName)
        End Get
        Set(ByVal value As Byte())
            mem(name) = value
        End Set
    End Property
    Public dicColors As New Dictionary(Of String, iTextSharp.text.BaseColor)
    Public ReadOnly WHITE As BaseColor = New iTextSharp.text.BaseColor(255, 255, 255)
    Public ReadOnly LIGHT_GRAY As BaseColor = New iTextSharp.text.BaseColor(192, 192, 192)
    Public ReadOnly GRAY As BaseColor = New iTextSharp.text.BaseColor(128, 128, 128)
    Public ReadOnly DARK_GRAY As BaseColor = New iTextSharp.text.BaseColor(64, 64, 64)
    Public ReadOnly BLACK As BaseColor = New iTextSharp.text.BaseColor(0, 0, 0)
    Public ReadOnly RED As BaseColor = New iTextSharp.text.BaseColor(255, 0, 0)
    Public ReadOnly PINK As BaseColor = New iTextSharp.text.BaseColor(255, 175, 175)
    Public ReadOnly ORANGE As BaseColor = New iTextSharp.text.BaseColor(255, 200, 0)
    Public ReadOnly YELLOW As BaseColor = New iTextSharp.text.BaseColor(255, 255, 0)
    Public ReadOnly GREEN As BaseColor = New iTextSharp.text.BaseColor(0, 255, 0)
    Public ReadOnly MAGENTA As BaseColor = New iTextSharp.text.BaseColor(255, 0, 255)
    Public ReadOnly CYAN As BaseColor = New iTextSharp.text.BaseColor(0, 255, 255)
    Public ReadOnly BLUE As BaseColor = New iTextSharp.text.BaseColor(0, 0, 255)
    Public Sub New()
        loadColors()
    End Sub
    Public Function rgbaColor(ByVal red As Integer, ByVal green As Integer, ByVal blue As Integer, Optional ByVal alpha As Integer = 255, Optional ByVal name As String = "") As iTextSharp.text.BaseColor
        dicColors.Add(CStr(IIf(String.IsNullOrEmpty(name & ""), "rgba{" & red & "," & green & "," & blue & "," & alpha & "}", name & "")), New iTextSharp.text.BaseColor(red, green, blue, alpha))
        If Not ColorList Is Nothing Then
            ColorList.Items.Clear()
            For Each dicColor As String In dicColors.Keys
                ColorList.Items.Add(dicColor)
            Next
        End If
        Return New iTextSharp.text.BaseColor(red, green, blue, alpha)
    End Function
    Public Function rgbColor(ByVal red As Integer, ByVal green As Integer, ByVal blue As Integer, Optional ByVal name As String = "") As iTextSharp.text.BaseColor
        dicColors.Add(CStr(IIf(String.IsNullOrEmpty(name & ""), "rgb{" & red & "," & green & "," & blue & "}", name & "")), New iTextSharp.text.BaseColor(red, green, blue))
        If Not ColorList Is Nothing Then
            ColorList.Items.Clear()
            For Each dicColor As String In dicColors.Keys
                ColorList.Items.Add(dicColor)
            Next
        End If
        Return New iTextSharp.text.BaseColor(red, green, blue, 255)
    End Function
    Public Function rgbColor(ByVal color As System.Drawing.Color, Optional ByVal name As String = "") As iTextSharp.text.BaseColor
        dicColors.Add(CStr(IIf(String.IsNullOrEmpty(name & ""), color.Name.ToString & "{" & color.R & "," & color.G & "," & color.B & "," & color.A & "}", name & "")), New iTextSharp.text.BaseColor(color))
        If Not ColorList Is Nothing Then
            ColorList.Items.Clear()
            For Each dicColor As String In dicColors.Keys
                ColorList.Items.Add(dicColor)
            Next
        End If
        Return New iTextSharp.text.BaseColor(color)
    End Function
    Private Sub loadColors()
        dicColors.Clear()
        dicColors.Add("Black", BLACK)
        dicColors.Add("White", WHITE)
        dicColors.Add("Light Gray", LIGHT_GRAY)
        dicColors.Add("Gray", GRAY)
        dicColors.Add("Dark Gray", DARK_GRAY)
        dicColors.Add("Red", RED)
        dicColors.Add("Pink", PINK)
        dicColors.Add("Orange", ORANGE)
        dicColors.Add("Yellow", YELLOW)
        dicColors.Add("Green", GREEN)
        dicColors.Add("Magenta", MAGENTA)
        dicColors.Add("Cyan", CYAN)
        dicColors.Add("Blue", BLUE)
    End Sub
    Public WithEvents ColorList As New System.Windows.Forms.ComboBox
    Private Sub LoadColorList(ByRef ddl As System.Windows.Forms.ComboBox)
        ColorList = ddl
        ddl.Items.Clear()
        For Each dicColor As String In dicColors.Keys
            ddl.Items.Add(dicColor)
        Next
    End Sub
    Public Sub Load_DropDowns(ByRef btnBackgroundColor As System.Windows.Forms.ComboBox, ByRef btnBorderColor As System.Windows.Forms.ComboBox, ByRef btnTextColor As System.Windows.Forms.ComboBox, ByRef btnTextAlignment As System.Windows.Forms.ComboBox, ByRef btnBorderStyle As System.Windows.Forms.ComboBox, ByRef btnLinkList As System.Windows.Forms.ComboBox)
        loadColors()
        LoadColorList(btnBackgroundColor)
        LoadColorList(btnBorderColor)
        LoadColorList(btnTextColor)
        btnTextAlignment.Items.Clear()
        btnTextAlignment.Items.Add(New String() {"left", CStr(0)})
        btnTextAlignment.Items.Add(New String() {"center", CStr(1)})
        btnTextAlignment.Items.Add(New String() {"right", CStr(2)})
        btnBorderStyle.Items.Clear()
        btnBorderStyle.Items.Add(New String() {"solid", CStr(0)})
        btnBorderStyle.Items.Add(New String() {"dashed", CStr(1)})
        btnBorderStyle.Items.Add(New String() {"beveled", CStr(2)})
        btnBorderStyle.Items.Add(New String() {"inset", CStr(3)})
        btnBorderStyle.Items.Add(New String() {"underline", CStr(4)})
        LoadLinksList(btnLinkList)
        btnBorderStyle.Items.Add(New String() {"solid", CStr(0)})
    End Sub
    Private Sub LoadLinksList(ByVal ddl As System.Windows.Forms.ComboBox)
        ddl.Items.Clear()
        Dim strURL As String
        strURL = ""
        ddl.Items.Add(New String() {"PDF", strURL & "download/default.aspx?type=pdf"})
        ddl.Items.Add(New String() {"PDF Flat", strURL & "download/default.aspx?type=pdf&flatten=True"})
        ddl.Items.Add(New String() {"FDF", strURL & "download/default.aspx?type=fdf"})
        ddl.Items.Add(New String() {"XML", strURL & "download/default.aspx?type=xml"})
        ddl.Items.Add(New String() {"XFDF", strURL & "download/default.aspx?type=xfdf"})
        ddl.Items.Add(New String() {"XDP", strURL & "download/default.aspx?type=xdp"})
        ddl.Items.Add(New String() {"Email", strURL & "email/default.aspx"})
        ddl.Items.Add(New String() {"Parse", strURL & "parse/default.aspx"})
        ddl.Items.Add(New String() {"Program", strURL & "program/default.aspx"})
        ddl.Items.Add(New String() {"View", strURL & "view/default.aspx"})
        ddl.Items.Add(New String() {"Message", strURL & "HttpContext.Current.Response/default.aspx?type=fdf"})
        ddl.Items.Add(New String() {"*Merge", strURL & "merge/"})
        ddl.Items.Add(New String() {"*Print", strURL & "print/"})
    End Sub
    Private Function LoadPageList(ByVal ddl As System.Windows.Forms.ComboBox) As PointF
        Try
            Dim pdfFn As String = ""
            Dim pdfRead As New PdfReader(cFDFDoc.FDFGetFile)
            Dim numPages As Integer = pdfRead.NumberOfPages + 0
            Dim memStream As New MemoryStream
            Dim pdfStamp As New iTextSharp.text.pdf.PdfStamper(pdfRead, memStream)
            _pdfH = pdfStamp.Reader.GetPageSizeWithRotation(1).Height
            _pdfW = pdfStamp.Reader.GetPageSizeWithRotation(1).Width
            pdfRead.Close()
            ddl.Items.Clear()
            For idx As Integer = 0 To numPages - 1 Step 1
                ddl.Items.Add(New String() {"Page #" & idx + 1, idx.ToString})
            Next
            Return New PointF(_pdfW, _pdfW)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Private Sub LoadPDF()
        Try
            cFDFDoc = cFDFApp.FDFOpenFromBuf(Load2Memory("input"), True, True)
        Catch ex As Exception
        End Try
        If Not Load2Memory("output") Is Nothing Then
            LoadPDF()
        ElseIf Not Load2Memory(_sid) Is Nothing Then
            Dim pdfBytes() As Byte = cFDFDoc.PDFMergeFDF2Buf(cFDFDoc.FDFGetFile, False, "")
            Load2Memory("input") = pdfBytes
            LoadPDF()
        ElseIf Not Load2Memory("input") Is Nothing Then
            Dim pdfBytes() As Byte = cFDFDoc.PDFMergeFDF2Buf(cFDFDoc.FDFGetFile, False, "")
            Load2Memory("input") = pdfBytes
            LoadPDF()
        End If
    End Sub
    ''' <summary>
    ''' Adds a Submit Button to Existing PDF Form
    ''' </summary>
    ''' <param name="PDFForm">Byte Array containing Existing PDF Form to add a button to</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Add_Submit_Button(ByVal PDFForm() As Byte, ByVal btnName As System.Windows.Forms.TextBox, ByVal btnPage As System.Windows.Forms.ComboBox, ByVal btnSubmitURL As System.Windows.Forms.TextBox, ByVal btnLeft As System.Windows.Forms.TextBox, ByVal btnRight As System.Windows.Forms.TextBox, ByVal btnTop As System.Windows.Forms.TextBox, ByVal btnBottom As System.Windows.Forms.TextBox, ByVal btnBackgroundColor As System.Windows.Forms.ComboBox, ByVal btnTextColor As System.Windows.Forms.ComboBox, ByVal btnText As System.Windows.Forms.TextBox, ByVal btnVisibility As System.Windows.Forms.ComboBox, ByVal btnTextAlignment As System.Windows.Forms.ComboBox, ByVal btnBorderColor As System.Windows.Forms.ComboBox, ByVal btnBorderStyle As System.Windows.Forms.ComboBox) As Byte()
        loadColors()
        Dim pdfReader As New iTextSharp.text.pdf.PdfReader(PDFForm)
        Dim memStream As New MemoryStream
        Dim pdfStamp As New iTextSharp.text.pdf.PdfStamper(pdfReader, memStream)
        Dim submitBtn As New iTextSharp.text.pdf.PushbuttonField(pdfStamp.Writer, New iTextSharp.text.Rectangle(CSng(btnLeft.Text), CSng(btnTop.Text), CSng(btnRight.Text), CSng(btnBottom.Text)), btnName.Text)
        submitBtn.TextColor = dicColors.Values(btnTextColor.SelectedIndex)
        submitBtn.BackgroundColor = dicColors.Values(btnBackgroundColor.SelectedIndex)
        submitBtn.BorderStyle = CInt(btnBorderStyle.SelectedItem)
        submitBtn.BorderColor = dicColors.Values(btnBorderColor.SelectedIndex)
        submitBtn.Text = btnText.Text
        submitBtn.Alignment = CInt(btnTextAlignment.SelectedItem)
        submitBtn.Text = btnText.Text & ""
        Dim submitField As iTextSharp.text.pdf.PdfFormField = submitBtn.Field
        submitField.MKTextPosition = CInt(btnTextAlignment.SelectedItem) + 0
        submitField.Page = CInt(btnPage.SelectedValue) + 1
        submitField.Action = iTextSharp.text.pdf.PdfAction.CreateSubmitForm(btnSubmitURL.Text, Nothing, PdfAction.SUBMIT_INCLUDE_NO_VALUE_FIELDS)
        pdfStamp.AddAnnotation(submitField, CInt(btnPage.SelectedValue) + 1)
        Dim bytes() As Byte
        Try
            pdfStamp.Writer.CloseStream = False
            pdfStamp.Close()
            If memStream.CanSeek Then
                memStream.Position = 0
            End If
            bytes = memStream.GetBuffer
            pdfReader.Close()
            memStream.Close()
            memStream.Dispose()
            Return cFDFDoc.GetUsedBytesOnly(bytes)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private _pdfW As Single = 0, _pdfH As Single = 0
    Public Property PDFWidth(ByVal PDFForm() As Byte) As Single
        Get
            Return _pdfW
        End Get
        Set(ByVal value As Single)
            _pdfW = value
        End Set
    End Property
    Public Property PDFHeight(ByVal PDFForm() As Byte) As Single
        Get
            Return _pdfH
        End Get
        Set(ByVal value As Single)
            _pdfH = value
        End Set
    End Property
    Public Property PDFWidth(ByVal PDFForm As String) As Single
        Get
            Return _pdfW
        End Get
        Set(ByVal value As Single)
            _pdfW = value
        End Set
    End Property
    Public Property PDFHeight(ByVal PDFForm As String) As Single
        Get
            Return _pdfH
        End Get
        Set(ByVal value As Single)
            _pdfH = value
        End Set
    End Property
    Public Property PDFWidth() As Single
        Get
            Return _pdfW
        End Get
        Set(ByVal value As Single)
            _pdfW = value
        End Set
    End Property
    Public Property PDFHeight() As Single
        Get
            Return _pdfH
        End Get
        Set(ByVal value As Single)
            _pdfH = value
        End Set
    End Property
    Public Function GetPDFWidth(ByVal PDFForm As String) As Single
        Dim pdfReader As New iTextSharp.text.pdf.PdfReader(PDFForm)
        Dim memStream As New MemoryStream
        Dim pdfStamp As New iTextSharp.text.pdf.PdfStamper(pdfReader, memStream)
        Dim intDim As Single = pdfStamp.Reader.GetPageSizeWithRotation(1).Width
        Return intDim
    End Function
    Public Function GetPDFHeight(ByVal PDFForm As String) As Single
        Dim pdfReader As New iTextSharp.text.pdf.PdfReader(PDFForm)
        Dim memStream As New MemoryStream
        Dim pdfStamp As New iTextSharp.text.pdf.PdfStamper(pdfReader, memStream)
        Dim intDim As Single = pdfStamp.Reader.GetPageSizeWithRotation(1).Height
        Return intDim
    End Function
    Private Function ReplaceXMLReservedCharacters(ByVal strInput As String) As String
        strInput = strInput.Replace("&", "_")
        strInput = strInput.Replace("<", "_")
        strInput = strInput.Replace(">", "_")
        strInput = strInput.Replace("""", "_")
        strInput = strInput.Replace("'", "_")
        strInput = strInput.Replace(" ", "_")
        Return strInput & ""
    End Function
    Private Function CheckXMLReservedWords(ByVal strInput As String) As String
        strInput = strInput.Replace("&amp;", "&")
        strInput = strInput.Replace("&", "&amp;")
        strInput = strInput.Replace("<", "&lt;")
        strInput = strInput.Replace(">", "&gt;")
        strInput = strInput.Replace("""", "&quot;")
        strInput = strInput.Replace("'", "&apos;")
        Return strInput & ""
    End Function
    ''' <summary>
    ''' Downloads Restricted File into a Stream
    ''' </summary>
    ''' <param name="PDF_URL">Name of PDF or File to download</param>
    ''' <returns>Stream containing restricted file</returns>
    ''' <remarks></remarks>
    Public Function Download_RestrictedFile(ByVal PDF_URL As String) As Stream
        Dim myCache As New System.Net.CredentialCache
        Dim myWebClient As System.Net.WebClient
        Dim fs As New MemoryStream
        Try
            myWebClient = New System.Net.WebClient
            Dim bytes() As Byte
            bytes = myWebClient.DownloadData(PDF_URL)
            fs.Write(bytes, 0, bytes.Length)
            fs.Position = 0
            Return fs
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try
    End Function
    ''' <summary>
    ''' Downloads Restricted File into a Stream
    ''' </summary>
    ''' <param name="PDF_URL">Name of PDF or File to download</param>
    ''' <param name="Username">Credential Username</param>
    ''' <param name="Password">Credential Password</param>
    ''' <returns>Stream containing restricted file</returns>
    ''' <remarks></remarks>
    Public Function Download_RestrictedFile(ByVal PDF_URL As String, ByVal Username As String, ByVal Password As String) As Stream
        Dim myCache As New System.Net.CredentialCache
        Dim myWebClient As System.Net.WebClient
        Dim fs As New MemoryStream
        Try
            myWebClient = New System.Net.WebClient
            Dim bytes() As Byte
            Dim creds As New System.Net.NetworkCredential(Username, Password)
            myWebClient.Credentials = creds
            bytes = myWebClient.DownloadData(PDF_URL)
            fs.Write(bytes, 0, bytes.Length)
            fs.Position = 0
            Return fs
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try
    End Function
    ''' <summary>
    ''' Downloads Restricted File into a Stream
    ''' </summary>
    ''' <param name="PDF_URL">Name of PDF or File to download</param>
    ''' <param name="Username">Credential Username</param>
    ''' <param name="Password">Credential Password</param>
    ''' <param name="Domain">Credential Domain</param>
    ''' <returns>Stream containing restricted file</returns>
    ''' <remarks></remarks>
    Public Function Download_RestrictedFile(ByVal PDF_URL As String, ByVal Username As String, ByVal Password As String, ByVal Domain As String) As Stream
        Dim myCache As New System.Net.CredentialCache
        Dim myWebClient As System.Net.WebClient
        Dim fs As New MemoryStream
        Try
            myWebClient = New System.Net.WebClient
            Dim bytes() As Byte
            Dim creds As New System.Net.NetworkCredential(Username, Password, Domain)
            myWebClient.Credentials = creds
            bytes = myWebClient.DownloadData(PDF_URL)
            fs.Write(bytes, 0, bytes.Length)
            fs.Position = 0
            Return fs
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try
    End Function
    ''' <summary>
    ''' Forces download of PDF file
    ''' </summary>
    ''' <param name="FileBytes">Byte array of file</param>
    ''' <param name="FileName">PDF File name</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PDFForceDownload(ByVal FileBytes() As Byte, Optional ByVal FileName As String = "PDFDownloadForm.pdf") As Boolean
    End Function
    Private Function XDPCheckChar(ByVal strINPUT As String) As String
        If strINPUT.Length <= 0 Then
            Return ""
            Exit Function
        End If
        strINPUT = strINPUT.Replace("&", "&&38;")
        strINPUT = strINPUT.Replace("#", "&#35;")
        strINPUT = strINPUT.Replace("&&38;", "&#38;")
        strINPUT = strINPUT.Replace("<", "&#60;")
        strINPUT = strINPUT.Replace(">", "&#62;")
        strINPUT = strINPUT.Replace("(", "&#40;")
        strINPUT = strINPUT.Replace(")", "&#41;")
        strINPUT = strINPUT.Replace("'", "&#39;")
        strINPUT = strINPUT.Replace("`", "&#39;")
        strINPUT = strINPUT.Replace("""", "&#34;")
        strINPUT = strINPUT.Replace("‚", "&#44;")
        strINPUT = strINPUT.Replace("’", "&#8217;")
        strINPUT = strINPUT.Replace("$", "&#36;")
        Return strINPUT & ""
    End Function
    Private Function XDPCheckCharReverse(ByVal strINPUT As String) As String
        If strINPUT.Length <= 0 Then
            Return ""
            Exit Function
        End If
        Return strINPUT & ""
        Exit Function
        strINPUT = strINPUT.Replace("&&38;", "&#38;")
        strINPUT = strINPUT.Replace("&#60;", "<")
        strINPUT = strINPUT.Replace("&#62;", ">")
        strINPUT = strINPUT.Replace("&#40;", "(")
        strINPUT = strINPUT.Replace("&#41;", ")")
        strINPUT = strINPUT.Replace("&#39;", "'")
        strINPUT = strINPUT.Replace("&#39;", "`")
        strINPUT = strINPUT.Replace("&#34;", """")
        strINPUT = strINPUT.Replace("&#44;", "‚")
        strINPUT = strINPUT.Replace("&#39;", "'")
        strINPUT = strINPUT.Replace("&#8217;", "’")
        strINPUT = strINPUT.Replace("&#36;", "$")
        strINPUT = strINPUT.Replace("&#35;", "#")
        strINPUT = strINPUT.Replace("&#38;", "&")
        Return strINPUT & ""
    End Function
    Private Function IsValidUrl(ByVal url As String) As Boolean
        Return System.Text.RegularExpressions.Regex.IsMatch(url, "^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$")
    End Function
    Public Function DownloadFile(ByVal bstrFileName As String, Optional ByVal FDFInitialize As Boolean = False, Optional ByVal ownerPassword As String = "") As Byte()
        Try
            Dim fBytes() As Byte
            If IsValidUrl(bstrFileName) Then
                Dim wclient As New System.Net.WebClient()
                fBytes = wclient.DownloadData(bstrFileName)
                Return fBytes
            ElseIf File.Exists(bstrFileName) Then
                Dim fs As New System.IO.FileStream(bstrFileName, FileMode.Open, FileAccess.Read, FileShare.None)
                ReDim fBytes(CInt(fs.Length))
                fs.Read(fBytes, 0, CInt(fs.Length))
                fs.Close()
                fs.Dispose()
                Return fBytes
            ElseIf File.Exists(appPath & (bstrFileName)) Then
                Dim fs As New System.IO.FileStream(appPath & (bstrFileName), FileMode.Open, FileAccess.Read, FileShare.None)
                ReDim fBytes(CInt(fs.Length))
                fs.Read(fBytes, 0, CInt(fs.Length))
                fs.Close()
                fs.Dispose()
                Return fBytes
            Else
                Return Nothing
            End If
            Return Nothing
        Catch ex As Exception
            Return Nothing
            Exit Function
        End Try
    End Function
    Public Function Add_Submit_Button(ByVal PDFForm() As Byte, ByVal PdfActionFormat As Integer, ByVal btnPage As System.Windows.Forms.ComboBox, ByVal btnTextAlignment As System.Windows.Forms.ComboBox, ByVal btnText As System.Windows.Forms.TextBox, ByVal btnTextColor As System.Windows.Forms.ComboBox, ByVal btnBackgroundColor As System.Windows.Forms.ComboBox, ByVal btnBorderStyle As System.Windows.Forms.ComboBox, ByVal btnBorderColor As System.Windows.Forms.ComboBox, ByVal btnSubmitURL As System.Windows.Forms.TextBox, ByVal btnTop As System.Windows.Forms.TextBox, ByVal btnRight As System.Windows.Forms.TextBox, ByVal btnBottom As System.Windows.Forms.TextBox, ByVal btnLeft As System.Windows.Forms.TextBox, ByVal btnName As System.Windows.Forms.TextBox) As Byte()
        Dim pdfReader As New iTextSharp.text.pdf.PdfReader(PDFForm)
        Dim memStream As New MemoryStream()
        Dim pdfStamp As New iTextSharp.text.pdf.PdfStamper(pdfReader, memStream)
        Dim submitBtn As New iTextSharp.text.pdf.PushbuttonField(pdfStamp.Writer, New iTextSharp.text.Rectangle(CSng(btnLeft.Text), CSng(btnTop.Text), CSng(btnRight.Text), CSng(btnBottom.Text)), btnName.Text)
        submitBtn.TextColor = dicColors.Values(btnTextColor.SelectedIndex)
        submitBtn.BackgroundColor = dicColors.Values(btnBackgroundColor.SelectedIndex)
        submitBtn.BorderStyle = CInt(btnBorderStyle.SelectedItem)
        submitBtn.BorderColor = dicColors.Values(btnBorderColor.SelectedIndex)
        submitBtn.Text = btnText.Text
        submitBtn.Alignment = CInt(btnTextAlignment.SelectedItem)
        submitBtn.Text = btnText.Text & ""
        Dim submitField As iTextSharp.text.pdf.PdfFormField = submitBtn.Field
        submitField.MKTextPosition = CInt(btnTextAlignment.SelectedItem) + 0
        submitField.Page = CInt(btnPage.SelectedValue) + 1
        submitField.Action = iTextSharp.text.pdf.PdfAction.CreateSubmitForm(btnSubmitURL.Text, Nothing, PdfActionFormat)
        pdfStamp.AddAnnotation(submitField, CInt(btnPage.SelectedValue) + 1)
        Dim bytes() As Byte
        Try
            pdfStamp.Writer.CloseStream = False
            pdfStamp.Close()
            If memStream.CanSeek Then
                memStream.Position = 0
            End If
            bytes = memStream.GetBuffer
            pdfReader.Close()
            memStream.Close()
            memStream.Dispose()
            Return cFDFDoc.GetUsedBytesOnly(bytes)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Add_Doc_Javascript_Button(ByVal PDFForm() As Byte, ByVal PdfActionJavascriptCode As String, Optional ByVal PdfActionJavascriptName As String = "docjavascript001") As Byte()
        Dim pdfReader As New iTextSharp.text.pdf.PdfReader(PDFForm)
        Dim memStream As New MemoryStream()
        Dim pdfStamp As New iTextSharp.text.pdf.PdfStamper(pdfReader, memStream)
        If String.IsNullOrEmpty(PdfActionJavascriptName & "") Then
            PdfActionJavascriptName = "docjavascript001"
        End If
        pdfStamp.Writer.AddJavaScript(PdfActionJavascriptName, PdfActionJavascriptCode)
        Dim bytes() As Byte
        Try
            pdfStamp.Writer.CloseStream = False
            pdfStamp.Close()
            If memStream.CanSeek Then
                memStream.Position = 0
            End If
            bytes = memStream.GetBuffer
            pdfReader.Close()
            memStream.Close()
            memStream.Dispose()
            Return cFDFDoc.GetUsedBytesOnly(bytes)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Add_Javascript_Button(ByVal PDFForm() As Byte, ByVal PdfActionJavascriptCode As String, ByVal btnPage As System.Windows.Forms.ComboBox, ByVal btnTextAlignment As System.Windows.Forms.ComboBox, ByVal btnText As System.Windows.Forms.TextBox, ByVal btnTextColor As System.Windows.Forms.ComboBox, ByVal btnBackgroundColor As System.Windows.Forms.ComboBox, ByVal btnBorderStyle As System.Windows.Forms.ComboBox, ByVal btnBorderColor As System.Windows.Forms.ComboBox, ByVal btnTop As System.Windows.Forms.TextBox, ByVal btnRight As System.Windows.Forms.TextBox, ByVal btnBottom As System.Windows.Forms.TextBox, ByVal btnLeft As System.Windows.Forms.TextBox, ByVal btnName As System.Windows.Forms.TextBox) As Byte()
        Dim pdfReader As New iTextSharp.text.pdf.PdfReader(PDFForm)
        Dim memStream As New MemoryStream()
        Dim pdfStamp As New iTextSharp.text.pdf.PdfStamper(pdfReader, memStream)
        Dim submitBtn As New iTextSharp.text.pdf.PushbuttonField(pdfStamp.Writer, New iTextSharp.text.Rectangle(CSng(btnLeft.Text), CSng(btnTop.Text), CSng(btnRight.Text), CSng(btnBottom.Text)), btnName.Text)
        submitBtn.TextColor = dicColors.Values(btnTextColor.SelectedIndex)
        submitBtn.BackgroundColor = dicColors.Values(btnBackgroundColor.SelectedIndex)
        submitBtn.BorderStyle = CInt(btnBorderStyle.SelectedValue) + 0
        submitBtn.BorderColor = dicColors.Values(btnBorderColor.SelectedIndex)
        submitBtn.Text = btnText.Text
        submitBtn.Alignment = CInt(btnTextAlignment.SelectedValue) + 0
        submitBtn.Options = iTextSharp.text.pdf.PushbuttonField.VISIBLE_BUT_DOES_NOT_PRINT
        Dim submitField As iTextSharp.text.pdf.PdfFormField = submitBtn.Field
        submitField.Page = CInt(btnPage.SelectedValue) + 1
        submitField.Action = iTextSharp.text.pdf.PdfAction.JavaScript(PdfActionJavascriptCode, pdfStamp.Writer)
        pdfStamp.AddAnnotation(submitField, CInt(btnPage.SelectedValue) + 1)
        Dim bytes() As Byte
        Try
            pdfStamp.Writer.CloseStream = False
            pdfStamp.Close()
            If memStream.CanSeek Then
                memStream.Position = 0
            End If
            bytes = memStream.GetBuffer
            pdfReader.Close()
            memStream.Close()
            memStream.Dispose()
            Return cFDFDoc.GetUsedBytesOnly(bytes)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Add_Print_Button(ByVal PDFForm() As Byte, ByVal btnPage As System.Windows.Forms.ComboBox, ByVal btnTextAlignment As System.Windows.Forms.ComboBox, ByVal btnText As System.Windows.Forms.TextBox, ByVal btnTextColor As System.Windows.Forms.ComboBox, ByVal btnBackgroundColor As System.Windows.Forms.ComboBox, ByVal btnBorderStyle As System.Windows.Forms.ComboBox, ByVal btnBorderColor As System.Windows.Forms.ComboBox, ByVal btnTop As System.Windows.Forms.TextBox, ByVal btnRight As System.Windows.Forms.TextBox, ByVal btnBottom As System.Windows.Forms.TextBox, ByVal btnLeft As System.Windows.Forms.TextBox, ByVal btnName As System.Windows.Forms.TextBox) As Byte()
        Dim pdfReader As New iTextSharp.text.pdf.PdfReader(PDFForm)
        Dim memStream As New MemoryStream()
        Dim pdfStamp As New iTextSharp.text.pdf.PdfStamper(pdfReader, memStream)
        Dim submitBtn As New iTextSharp.text.pdf.PushbuttonField(pdfStamp.Writer, New iTextSharp.text.Rectangle(CSng(btnLeft.Text), CSng(btnTop.Text), CSng(btnRight.Text), CSng(btnBottom.Text)), btnName.Text)
        submitBtn.TextColor = dicColors.Values(btnTextColor.SelectedIndex)
        submitBtn.BackgroundColor = dicColors.Values(btnBackgroundColor.SelectedIndex)
        submitBtn.BorderStyle = CInt(btnBorderStyle.SelectedItem)
        submitBtn.BorderColor = dicColors.Values(btnBorderColor.SelectedIndex)
        submitBtn.Text = btnText.Text
        submitBtn.Alignment = CInt(btnTextAlignment.SelectedItem)
        submitBtn.Visibility = iTextSharp.text.pdf.PushbuttonField.VISIBLE_BUT_DOES_NOT_PRINT
        submitBtn.Options = iTextSharp.text.pdf.PushbuttonField.VISIBLE_BUT_DOES_NOT_PRINT
        submitBtn.Text = btnText.Text & ""
        Dim submitField As iTextSharp.text.pdf.PdfFormField = submitBtn.Field
        submitField.MKTextPosition = CInt(btnTextAlignment.SelectedItem) + 0
        submitField.MKBorderColor = Me.dicColors.Values(btnTextColor.SelectedIndex)
        submitField.MKBackgroundColor = Me.dicColors.Values(btnBackgroundColor.SelectedIndex)
        submitField.Page = CInt(btnPage.SelectedValue) + 1
        submitField.Action = iTextSharp.text.pdf.PdfAction.JavaScript("this.print();", pdfStamp.Writer)
        pdfStamp.AddAnnotation(submitField, CInt(btnPage.SelectedValue) + 1)
        Dim bytes() As Byte
        Try
            pdfStamp.Writer.CloseStream = False
            pdfStamp.Close()
            If memStream.CanSeek Then
                memStream.Position = 0
            End If
            bytes = memStream.GetBuffer
            pdfReader.Close()
            memStream.Close()
            memStream.Dispose()
            Return cFDFDoc.GetUsedBytesOnly(bytes)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Add_Reset_Button(ByVal PDFForm() As Byte, ByVal btnPage As System.Windows.Forms.ComboBox, ByVal btnTextAlignment As System.Windows.Forms.ComboBox, ByVal btnText As System.Windows.Forms.TextBox, ByVal btnTextColor As System.Windows.Forms.ComboBox, ByVal btnBackgroundColor As System.Windows.Forms.ComboBox, ByVal btnBorderStyle As System.Windows.Forms.ComboBox, ByVal btnBorderColor As System.Windows.Forms.ComboBox, ByVal btnTop As System.Windows.Forms.TextBox, ByVal btnRight As System.Windows.Forms.TextBox, ByVal btnBottom As System.Windows.Forms.TextBox, ByVal btnLeft As System.Windows.Forms.TextBox, ByVal btnName As System.Windows.Forms.TextBox) As Byte()
        Dim pdfReader As New iTextSharp.text.pdf.PdfReader(PDFForm)
        Dim memStream As New MemoryStream()
        Dim pdfStamp As New iTextSharp.text.pdf.PdfStamper(pdfReader, memStream)
        Dim submitBtn As New iTextSharp.text.pdf.PushbuttonField(pdfStamp.Writer, New iTextSharp.text.Rectangle(CSng(btnLeft.Text), CSng(btnTop.Text), CSng(btnRight.Text), CSng(btnBottom.Text)), btnName.Text)
        submitBtn.TextColor = dicColors.Values(btnTextColor.SelectedIndex)
        submitBtn.BackgroundColor = dicColors.Values(btnBackgroundColor.SelectedIndex)
        submitBtn.BorderStyle = CInt(btnBorderStyle.SelectedItem)
        submitBtn.BorderColor = dicColors.Values(btnBorderColor.SelectedIndex)
        submitBtn.Text = btnText.Text
        submitBtn.Alignment = CInt(btnTextAlignment.SelectedItem)
        submitBtn.Text = btnText.Text & ""
        Dim submitField As iTextSharp.text.pdf.PdfFormField = submitBtn.Field
        submitField.Page = CInt(btnPage.SelectedValue) + 1
        submitField.Action = iTextSharp.text.pdf.PdfAction.JavaScript("this.resetForm();", pdfStamp.Writer)
        pdfStamp.AddAnnotation(submitField, CInt(btnPage.SelectedValue) + 1)
        Dim bytes() As Byte
        Try
            pdfStamp.Writer.CloseStream = False
            pdfStamp.Close()
            If memStream.CanSeek Then
                memStream.Position = 0
            End If
            bytes = memStream.GetBuffer
            pdfReader.Close()
            memStream.Close()
            memStream.Dispose()
            Return cFDFDoc.GetUsedBytesOnly(bytes)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Add_Submit_Button(ByVal PDFForm() As Byte, ByVal btnPage As System.Windows.Forms.ComboBox, ByVal btnTextAlignment As System.Windows.Forms.ComboBox, ByVal btnText As System.Windows.Forms.TextBox, ByVal btnTextColor As System.Windows.Forms.ComboBox, ByVal btnBackgroundColor As System.Windows.Forms.ComboBox, ByVal btnBorderStyle As System.Windows.Forms.ComboBox, ByVal btnBorderColor As System.Windows.Forms.ComboBox, ByVal btnSubmitURL As System.Windows.Forms.TextBox, ByVal btnTop As System.Windows.Forms.TextBox, ByVal btnRight As System.Windows.Forms.TextBox, ByVal btnBottom As System.Windows.Forms.TextBox, ByVal btnLeft As System.Windows.Forms.TextBox, ByVal btnName As System.Windows.Forms.TextBox) As Byte()
        Dim pdfReader As New iTextSharp.text.pdf.PdfReader(PDFForm)
        Dim memStream As New MemoryStream()
        Dim pdfStamp As New iTextSharp.text.pdf.PdfStamper(pdfReader, memStream)
        Dim submitBtn As New iTextSharp.text.pdf.PushbuttonField(pdfStamp.Writer, New iTextSharp.text.Rectangle(CSng(btnLeft.Text), CSng(btnTop.Text), CSng(btnRight.Text), CSng(btnBottom.Text)), btnName.Text)
        submitBtn.TextColor = dicColors.Values(btnTextColor.SelectedIndex)
        submitBtn.BackgroundColor = dicColors.Values(btnBackgroundColor.SelectedIndex)
        submitBtn.BorderStyle = CInt(btnBorderStyle.SelectedItem)
        submitBtn.BorderColor = dicColors.Values(btnBorderColor.SelectedIndex)
        submitBtn.Text = btnText.Text
        submitBtn.Alignment = CInt(btnTextAlignment.SelectedItem)
        submitBtn.Text = btnText.Text & ""
        Dim submitField As iTextSharp.text.pdf.PdfFormField = submitBtn.Field
        submitField.MKTextPosition = CInt(btnTextAlignment.SelectedItem) + 0
        submitField.Page = CInt(btnPage.SelectedValue) + 1
        submitField.Action = iTextSharp.text.pdf.PdfAction.CreateSubmitForm(btnSubmitURL.Text, Nothing, PdfAction.SUBMIT_XFDF + PdfAction.SUBMIT_INCLUDE_NO_VALUE_FIELDS)
        pdfStamp.AddAnnotation(submitField, CInt(btnPage.SelectedValue) + 1)
        Dim bytes() As Byte
        Try
            pdfStamp.Writer.CloseStream = False
            pdfStamp.Close()
            If memStream.CanSeek Then
                memStream.Position = 0
            End If
            bytes = memStream.GetBuffer
            pdfReader.Close()
            memStream.Close()
            memStream.Dispose()
            Return cFDFDoc.GetUsedBytesOnly(bytes)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Add_List_Field(ByVal PDFForm() As Byte, ByVal btnPage As System.Windows.Forms.ComboBox, ByVal btnVisibility As System.Windows.Forms.ComboBox, ByVal btnWidth As System.Windows.Forms.TextBox, ByVal btnHeight As System.Windows.Forms.TextBox, ByVal btnFieldItems As System.Windows.Forms.ComboBox, ByVal btnTextAlignment As System.Windows.Forms.ComboBox, ByVal btnText As System.Windows.Forms.TextBox, ByVal btnTextColor As System.Windows.Forms.ComboBox, ByVal btnBackgroundColor As System.Windows.Forms.ComboBox, ByVal btnBorderStyle As System.Windows.Forms.ComboBox, ByVal btnBorderColor As System.Windows.Forms.ComboBox, ByVal btnSubmitURL As System.Windows.Forms.TextBox, ByVal btnTop As System.Windows.Forms.TextBox, ByVal btnRight As System.Windows.Forms.TextBox, ByVal btnBottom As System.Windows.Forms.TextBox, ByVal btnLeft As System.Windows.Forms.TextBox, ByVal btnName As System.Windows.Forms.TextBox) As Byte()
        loadColors()
        Dim pdfReader As New iTextSharp.text.pdf.PdfReader(PDFForm)
        Dim memStream As New MemoryStream
        Dim pdfStamp As New iTextSharp.text.pdf.PdfStamper(pdfReader, memStream)
        Dim comboItemsDisplay As New List(Of String), comboItemsValues As New List(Of String)
        For Each combo_item As String() In btnFieldItems.Items
            comboItemsDisplay.Add(combo_item(0).Split("|"c)(0))
            comboItemsValues.Add(combo_item(1).ToString)
        Next
        Dim comboFld As iTextSharp.text.pdf.TextField = New iTextSharp.text.pdf.TextField(pdfStamp.Writer, New iTextSharp.text.Rectangle(CSng(btnLeft.Text), CSng(btnBottom.Text), CSng(btnRight.Text), CSng(btnTop.Text)), btnName.Text)
        comboFld.FieldName = btnName.Text
        Dim app As iTextSharp.text.pdf.PdfAppearance = iTextSharp.text.pdf.PdfAppearance.CreateAppearance(pdfStamp.Writer, CSng(btnWidth.Text), CSng(btnHeight.Text))
        app.Rectangle(1, 1, CSng(btnWidth.Text) - 2, CSng(btnHeight.Text) - 2)
        app.Stroke()
        comboFld.TextColor = dicColors.Values(btnTextColor.SelectedIndex)
        comboFld.BackgroundColor = dicColors.Values(btnBackgroundColor.SelectedIndex)
        comboFld.BorderStyle = CInt(CInt(btnBorderStyle.SelectedValue) + 0) + 0
        comboFld.BorderColor = dicColors.Values(btnBorderColor.SelectedIndex)
        comboFld.Alignment = CInt(CInt(btnTextAlignment.SelectedValue) + 0) + 0
        comboFld.Visibility = CInt(btnVisibility.SelectedValue) + 0
        comboFld.Choices = comboItemsDisplay.ToArray
        comboFld.ChoiceExports = comboItemsValues.ToArray
        If CInt(btnFieldItems.SelectedIndex) >= 0 Then
            If btnFieldItems.SelectedIndex < comboItemsValues.Count And comboItemsValues.Count > 0 Then
                comboFld.ChoiceSelection = btnFieldItems.SelectedIndex
            End If
        End If
        pdfStamp.AddAnnotation(comboFld.GetListField, CInt(btnPage.SelectedValue) + 1)
        Dim bytes() As Byte
        Try
            pdfStamp.Writer.CloseStream = False
            pdfStamp.Close()
            If memStream.CanSeek Then
                memStream.Position = 0
            End If
            bytes = memStream.GetBuffer
            pdfReader.Close()
            memStream.Close()
            memStream.Dispose()
            Return cFDFDoc.GetUsedBytesOnly(bytes)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Add_Radio_Button(ByVal PDFForm() As Byte, ByVal btnPage As System.Windows.Forms.ComboBox, ByVal btnVisibility As System.Windows.Forms.ComboBox, ByVal btnWidth As System.Windows.Forms.TextBox, ByVal btnHeight As System.Windows.Forms.TextBox, ByVal btnFieldItems As System.Windows.Forms.ComboBox, ByVal btnTextAlignment As System.Windows.Forms.ComboBox, ByVal btnText As System.Windows.Forms.TextBox, ByVal btnTextColor As System.Windows.Forms.ComboBox, ByVal btnBackgroundColor As System.Windows.Forms.ComboBox, ByVal btnBorderStyle As System.Windows.Forms.ComboBox, ByVal btnBorderColor As System.Windows.Forms.ComboBox, ByVal btnSubmitURL As System.Windows.Forms.TextBox, ByVal btnTop As System.Windows.Forms.TextBox, ByVal btnRight As System.Windows.Forms.TextBox, ByVal btnBottom As System.Windows.Forms.TextBox, ByVal btnLeft As System.Windows.Forms.TextBox, ByVal btnName As System.Windows.Forms.TextBox, ByVal RadioButton_OnValue As System.Windows.Forms.TextBox, ByVal RadioButton_FieldCheckType As System.Windows.Forms.ComboBox) As Byte()
        loadColors()
        Dim pdfReader As New iTextSharp.text.pdf.PdfReader(PDFForm)
        Dim memStream As New MemoryStream
        Dim pdfStamp As New iTextSharp.text.pdf.PdfStamper(pdfReader, memStream)
        Dim comboItemsDisplay As New List(Of String), comboItemsValues As New List(Of String)
        For Each combo_item As String() In btnFieldItems.Items
            comboItemsDisplay.Add(combo_item(0).Split("|"c)(0))
            comboItemsValues.Add(combo_item(1).ToString)
        Next
        Dim comboFld As iTextSharp.text.pdf.RadioCheckField = New iTextSharp.text.pdf.RadioCheckField(pdfStamp.Writer, New iTextSharp.text.Rectangle(CSng(btnLeft.Text), CSng(btnBottom.Text), CSng(btnRight.Text), CSng(btnTop.Text)), btnName.Text, RadioButton_OnValue.Text)
        comboFld.FieldName = btnName.Text
        Dim app As iTextSharp.text.pdf.PdfAppearance = iTextSharp.text.pdf.PdfAppearance.CreateAppearance(pdfStamp.Writer, CSng(btnWidth.Text), CSng(btnHeight.Text))
        app.Rectangle(1, 1, CSng(btnWidth.Text) - 2, CSng(btnHeight.Text) - 2)
        app.Stroke()
        comboFld.TextColor = dicColors.Values(btnTextColor.SelectedIndex)
        comboFld.BackgroundColor = dicColors.Values(btnBackgroundColor.SelectedIndex)
        comboFld.BorderStyle = CInt(CInt(btnBorderStyle.SelectedValue) + 0) + 0
        comboFld.BorderColor = dicColors.Values(btnBorderColor.SelectedIndex)
        comboFld.Alignment = CInt(CInt(btnTextAlignment.SelectedValue) + 0) + 0
        comboFld.Visibility = CInt(btnVisibility.SelectedValue) + 0
        comboFld.OnValue = RadioButton_OnValue.Text & ""
        comboFld.CheckType = CInt(RadioButton_FieldCheckType.SelectedValue) + 0
        If String.IsNullOrEmpty(RadioButton_OnValue.Text & "") And Not String.IsNullOrEmpty(RadioButton_OnValue.Text & "") Then
            comboFld.OnValue = CStr(btnFieldItems.SelectedValue)
        End If
        pdfStamp.AddAnnotation(comboFld.CheckField, CInt(btnPage.SelectedValue) + 1)
        Dim bytes() As Byte
        Try
            pdfStamp.Writer.CloseStream = False
            pdfStamp.Close()
            If memStream.CanSeek Then
                memStream.Position = 0
            End If
            bytes = memStream.GetBuffer
            pdfReader.Close()
            memStream.Close()
            memStream.Dispose()
            Return cFDFDoc.GetUsedBytesOnly(bytes)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function Add_Check_Button(ByVal PDFForm() As Byte, ByVal btnPage As System.Windows.Forms.ComboBox, ByVal btnVisibility As System.Windows.Forms.ComboBox, ByVal btnWidth As System.Windows.Forms.TextBox, ByVal btnHeight As System.Windows.Forms.TextBox, ByVal btnFieldItems As System.Windows.Forms.ComboBox, ByVal btnTextAlignment As System.Windows.Forms.ComboBox, ByVal btnText As System.Windows.Forms.TextBox, ByVal btnTextColor As System.Windows.Forms.ComboBox, ByVal btnBackgroundColor As System.Windows.Forms.ComboBox, ByVal btnBorderStyle As System.Windows.Forms.ComboBox, ByVal btnBorderColor As System.Windows.Forms.ComboBox, ByVal btnSubmitURL As System.Windows.Forms.TextBox, ByVal btnTop As System.Windows.Forms.TextBox, ByVal btnRight As System.Windows.Forms.TextBox, ByVal btnBottom As System.Windows.Forms.TextBox, ByVal btnLeft As System.Windows.Forms.TextBox, ByVal btnName As System.Windows.Forms.TextBox, ByVal RadioButton_OnValue As System.Windows.Forms.TextBox, ByVal RadioButton_FieldCheckType As System.Windows.Forms.ComboBox) As Byte()
        loadColors()
        Dim pdfReader As New iTextSharp.text.pdf.PdfReader(PDFForm)
        Dim memStream As New MemoryStream
        Dim pdfStamp As New iTextSharp.text.pdf.PdfStamper(pdfReader, memStream)
        Dim comboItemsDisplay As New List(Of String), comboItemsValues As New List(Of String)
        For Each combo_item As String() In btnFieldItems.Items
            comboItemsDisplay.Add(combo_item(0).Split("|"c)(0))
            comboItemsValues.Add(combo_item(1).ToString)
        Next
        Dim comboFld As iTextSharp.text.pdf.RadioCheckField = New iTextSharp.text.pdf.RadioCheckField(pdfStamp.Writer, New iTextSharp.text.Rectangle(CSng(btnLeft.Text), CSng(btnBottom.Text), CSng(btnRight.Text), CSng(btnTop.Text)), btnName.Text, RadioButton_OnValue.Text)
        comboFld.FieldName = btnName.Text
        Dim app As iTextSharp.text.pdf.PdfAppearance = iTextSharp.text.pdf.PdfAppearance.CreateAppearance(pdfStamp.Writer, CSng(btnWidth.Text), CSng(btnHeight.Text))
        app.Rectangle(1, 1, CSng(btnWidth.Text) - 2, CSng(btnHeight.Text) - 2)
        app.Stroke()
        comboFld.TextColor = dicColors.Values(btnTextColor.SelectedIndex)
        comboFld.BackgroundColor = dicColors.Values(btnBackgroundColor.SelectedIndex)
        comboFld.BorderStyle = CInt(CInt(btnBorderStyle.SelectedValue) + 0) + 0
        comboFld.BorderColor = dicColors.Values(btnBorderColor.SelectedIndex)
        comboFld.Alignment = CInt(CInt(btnTextAlignment.SelectedValue) + 0) + 0
        comboFld.Visibility = CInt(btnVisibility.SelectedValue) + 0
        comboFld.OnValue = RadioButton_OnValue.Text & ""
        comboFld.CheckType = CInt(RadioButton_FieldCheckType.SelectedValue) + 0
        If String.IsNullOrEmpty(RadioButton_OnValue.Text & "") And Not String.IsNullOrEmpty(RadioButton_OnValue.Text & "") Then
            comboFld.OnValue = CStr(btnFieldItems.SelectedValue)
        End If
        pdfStamp.AddAnnotation(comboFld.CheckField, CInt(btnPage.SelectedValue) + 1)
        Dim bytes() As Byte
        Try
            pdfStamp.Writer.CloseStream = False
            pdfStamp.Close()
            If memStream.CanSeek Then
                memStream.Position = 0
            End If
            bytes = memStream.GetBuffer
            pdfReader.Close()
            memStream.Close()
            memStream.Dispose()
            Return cFDFDoc.GetUsedBytesOnly(bytes)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Add_Combo_Field(ByVal PDFForm() As Byte, ByVal btnPage As System.Windows.Forms.ComboBox, ByVal btnVisibility As System.Windows.Forms.ComboBox, ByVal btnWidth As System.Windows.Forms.TextBox, ByVal btnHeight As System.Windows.Forms.TextBox, ByVal btnFieldItems As System.Windows.Forms.ComboBox, ByVal btnTextAlignment As System.Windows.Forms.ComboBox, ByVal btnText As System.Windows.Forms.TextBox, ByVal btnTextColor As System.Windows.Forms.ComboBox, ByVal btnBackgroundColor As System.Windows.Forms.ComboBox, ByVal btnBorderStyle As System.Windows.Forms.ComboBox, ByVal btnBorderColor As System.Windows.Forms.ComboBox, ByVal btnSubmitURL As System.Windows.Forms.TextBox, ByVal btnTop As System.Windows.Forms.TextBox, ByVal btnRight As System.Windows.Forms.TextBox, ByVal btnBottom As System.Windows.Forms.TextBox, ByVal btnLeft As System.Windows.Forms.TextBox, ByVal btnName As System.Windows.Forms.TextBox) As Byte()
        loadColors()
        Dim pdfReader As New iTextSharp.text.pdf.PdfReader(PDFForm)
        Dim memStream As New MemoryStream
        Dim pdfStamp As New iTextSharp.text.pdf.PdfStamper(pdfReader, memStream)
        Dim comboItemsDisplay As New List(Of String), comboItemsValues As New List(Of String)
        For Each combo_item As String() In btnFieldItems.Items
            comboItemsDisplay.Add(combo_item(0).Split("|"c)(0))
            comboItemsValues.Add(combo_item(1).ToString)
        Next
        Dim comboFld As iTextSharp.text.pdf.TextField = New iTextSharp.text.pdf.TextField(pdfStamp.Writer, New iTextSharp.text.Rectangle(CSng(btnLeft.Text), CSng(btnBottom.Text), CSng(btnRight.Text), CSng(btnTop.Text)), btnName.Text)
        comboFld.FieldName = btnName.Text
        Dim app As iTextSharp.text.pdf.PdfAppearance = iTextSharp.text.pdf.PdfAppearance.CreateAppearance(pdfStamp.Writer, CSng(btnWidth.Text), CSng(btnHeight.Text))
        app.Rectangle(1, 1, CSng(btnWidth.Text) - 2, CSng(btnHeight.Text) - 2)
        app.Stroke()
        comboFld.TextColor = dicColors.Values(btnTextColor.SelectedIndex)
        comboFld.BackgroundColor = dicColors.Values(btnBackgroundColor.SelectedIndex)
        comboFld.BorderStyle = CInt(CInt(btnBorderStyle.SelectedValue) + 0) + 0
        comboFld.BorderColor = dicColors.Values(btnBorderColor.SelectedIndex)
        comboFld.Alignment = CInt(CInt(btnTextAlignment.SelectedValue) + 0) + 0
        comboFld.Visibility = CInt(btnVisibility.SelectedValue) + 0
        comboFld.Choices = comboItemsDisplay.ToArray
        comboFld.ChoiceExports = comboItemsValues.ToArray
        If CInt(btnFieldItems.SelectedIndex) >= 0 Then
            If btnFieldItems.SelectedIndex < comboItemsValues.Count And comboItemsValues.Count > 0 Then
                comboFld.ChoiceSelection = btnFieldItems.SelectedIndex
            End If
        End If
        pdfStamp.AddAnnotation(comboFld.GetComboField, CInt(btnPage.SelectedValue) + 1)
        Dim bytes() As Byte
        Try
            pdfStamp.Writer.CloseStream = False
            pdfStamp.Close()
            If memStream.CanSeek Then
                memStream.Position = 0
            End If
            bytes = memStream.GetBuffer
            pdfReader.Close()
            memStream.Close()
            memStream.Dispose()
            Return cFDFDoc.GetUsedBytesOnly(bytes)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Add_Textbox_Button(ByVal PDFForm() As Byte, ByVal btnPage As System.Windows.Forms.ComboBox, ByVal btnVisibility As System.Windows.Forms.ComboBox, ByVal btnWidth As System.Windows.Forms.TextBox, ByVal btnHeight As System.Windows.Forms.TextBox, ByVal btnTextAlignment As System.Windows.Forms.ComboBox, ByVal btnText As System.Windows.Forms.TextBox, ByVal btnTextColor As System.Windows.Forms.ComboBox, ByVal btnBackgroundColor As System.Windows.Forms.ComboBox, ByVal btnBorderStyle As System.Windows.Forms.ComboBox, ByVal btnBorderColor As System.Windows.Forms.ComboBox, ByVal btnTop As System.Windows.Forms.TextBox, ByVal btnRight As System.Windows.Forms.TextBox, ByVal btnBottom As System.Windows.Forms.TextBox, ByVal btnLeft As System.Windows.Forms.TextBox, ByVal btnName As System.Windows.Forms.TextBox) As Byte()
        loadColors()
        Dim pdfReader As New iTextSharp.text.pdf.PdfReader(PDFForm)
        Dim memStream As New MemoryStream
        Dim pdfStamp As New iTextSharp.text.pdf.PdfStamper(pdfReader, memStream)
        Dim textFld As New iTextSharp.text.pdf.TextField(pdfStamp.Writer, New iTextSharp.text.Rectangle(CSng(btnLeft.Text), CSng(btnTop.Text), CSng(btnRight.Text), CSng(btnBottom.Text)), btnName.Text)
        textFld.TextColor = dicColors.Values(btnTextColor.SelectedIndex)
        textFld.BackgroundColor = dicColors.Values(btnBackgroundColor.SelectedIndex)
        textFld.BorderStyle = CInt(btnBorderStyle.SelectedIndex + 0) + 0
        textFld.BorderColor = dicColors.Values(btnBorderColor.SelectedIndex)
        textFld.Alignment = CInt(CInt(btnTextAlignment.SelectedValue) + 0) + 0
        textFld.Visibility = CInt(btnVisibility.SelectedIndex) + 0
        textFld.Text = btnText.Text & ""
        Dim submitField As iTextSharp.text.pdf.PdfFormField = textFld.GetTextField()
        submitField.MKTextPosition = CInt(CInt(btnTextAlignment.SelectedValue) + 0) + 0
        submitField.MKBorderColor = dicColors.Values(btnTextColor.SelectedIndex)
        submitField.MKBackgroundColor = dicColors.Values(btnBackgroundColor.SelectedIndex)
        submitField.Page = CInt(btnPage.SelectedValue) + 1 + 0
        pdfStamp.AddAnnotation(submitField, CInt(btnPage.SelectedValue) + 1)
        Dim bytes() As Byte
        Try
            pdfStamp.Writer.CloseStream = False
            pdfStamp.Close()
            If memStream.CanSeek Then
                memStream.Position = 0
            End If
            bytes = memStream.GetBuffer
            pdfReader.Close()
            memStream.Close()
            memStream.Dispose()
            Return cFDFDoc.GetUsedBytesOnly(bytes)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function getPDFWidth(ByVal pdfData() As Byte) As Integer
        Dim pdfDoc As New PdfReader(pdfData)
        Dim dimPDF As Single = pdfDoc.GetPageSizeWithRotation(1).Width
        pdfDoc.Close()
        Return CInt(dimPDF)
    End Function
    Public Function getPDFHeight(ByVal pdfData() As Byte) As Integer
        Dim pdfDoc As New PdfReader(pdfData)
        Dim dimPDF As Single = pdfDoc.GetPageSizeWithRotation(1).Height
        pdfDoc.Close()
        Return CInt(dimPDF)
    End Function
End Class
