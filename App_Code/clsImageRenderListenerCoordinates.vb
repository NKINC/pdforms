Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.parser
Imports System.IO
Public Class clsImageCoordinateListener
    Implements iTextSharp.text.pdf.parser.IRenderListener
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 Nicholas Kowalewicz All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>

    Private name As String = "PdfImage-"
    Public parent As frmMain
    Public page As Integer
    Public Links As List(Of clsLinks.Link)
    Private destinationFolder As String = ""
    Private counter As Integer = 100000
    Public Sub New(ByVal name As String)
        MyBase.New()
        Me.name = Me.name
    End Sub
    Public Sub New(ByRef frm1 As frmMain, ByRef Links1 As List(Of clsLinks.Link))
        MyBase.New()
        Me.parent = frm1
        Me.Links = Links1
    End Sub
    Public Sub New(ByVal prefix As String, ByVal dest As String)
        MyBase.New()
        Me.name = prefix
        Me.destinationFolder = dest
    End Sub
    Public Shared Sub setImageCoordinatesFormMain(ByVal reader As PdfReader, ByVal page1 As Integer, ByRef frm1 As frmMain, ByRef Links1 As List(Of clsLinks.Link))
        Dim parser As iTextSharp.text.pdf.parser.PdfReaderContentParser = New iTextSharp.text.pdf.parser.PdfReaderContentParser(reader)
        Dim listener As clsImageCoordinateListener = New clsImageCoordinateListener(frm1, Links1)
        parser.ProcessContent(page1, listener)
    End Sub
    Public Sub renderImage(ByVal renderInfo As iTextSharp.text.pdf.parser.ImageRenderInfo) Implements iTextSharp.text.pdf.parser.IRenderListener.RenderImage
        Try
            Dim image As PdfImageObject = renderInfo.GetImage()
            If image Is Nothing Then
                Return
            End If
            Dim imageDictionary As PdfDictionary = image.GetDictionary()
            Dim startPoint1 As iTextSharp.text.pdf.parser.Vector = renderInfo.GetStartPoint
            Dim matrix1 As Matrix = renderInfo.GetImageCTM()
            Dim x As Single = matrix1(Matrix.I31)
            Dim y As Single = matrix1(Matrix.I32)
            Dim w As Single = matrix1(Matrix.I11)
            Dim h As Single = matrix1(Matrix.I22)
            Dim imageBytes() As Byte = Nothing
            Try
                Dim number As Integer = renderInfo.GetRef.Number
                Dim pdfRefName As PdfName = New PdfName(renderInfo.GetRef.ToString())
                If Not (renderInfo.GetRef Is Nothing) Then
                    Me.counter = (Me.counter + 1)
                    Dim imgFormatExt As String = image.GetImageBytesType().FileExtension
                    Dim imgFormat As System.Drawing.Imaging.ImageFormat
                    Select Case imgFormatExt.ToString.ToLower.TrimStart("."c)
                        Case "jpg", "jpeg"
                            imgFormat = System.Drawing.Imaging.ImageFormat.Jpeg
                        Case "png"
                            imgFormat = System.Drawing.Imaging.ImageFormat.Png
                        Case "bmp"
                            imgFormat = System.Drawing.Imaging.ImageFormat.Bmp
                        Case "gif"
                            imgFormat = System.Drawing.Imaging.ImageFormat.Gif
                        Case "tiff", "tif"
                            imgFormat = System.Drawing.Imaging.ImageFormat.Tiff
                        Case "wmf"
                            imgFormat = System.Drawing.Imaging.ImageFormat.Wmf
                        Case Else
                            imgFormat = System.Drawing.Imaging.ImageFormat.Jpeg
                    End Select
                    If Not image.GetImageAsBytes() Is Nothing Then
                        imageBytes = image.GetImageAsBytes()
                    End If
                    Dim imageMaskBytes() As Byte = Nothing
                    Dim maskStream As PRStream = CType(imageDictionary.GetAsStream(PdfName.SMASK), PRStream)
                    Dim maskImage As PdfImageObject = Nothing
                    If (Not (maskStream) Is Nothing) Then
                        maskImage = New PdfImageObject(maskStream)
                        imageMaskBytes = maskImage.GetImageAsBytes()
                    End If
                    If Not imageBytes Is Nothing Then
                        Dim lnk As New clsLinks.Link(parent.getRectangleScreen(New System.Drawing.RectangleF(x, parent.getPDFHeight - y - h, w, h)), New System.Drawing.SizeF(w, h), True, imageBytes, imageMaskBytes, imgFormat, number)
                        If Not Links.Contains(lnk) Then
                            Links.Add(lnk)
                        End If
                    End If
                End If
            Catch e As Exception
                Throw e
            End Try
        Catch e As IOException
            Err.Clear()
        End Try
    End Sub
    Public Sub BeginTextBlock() Implements iTextSharp.text.pdf.parser.IRenderListener.BeginTextBlock
    End Sub
    Public Sub EndTextBlock() Implements iTextSharp.text.pdf.parser.IRenderListener.EndTextBlock
    End Sub
    Public Sub RenderText(ByVal renderInfo As iTextSharp.text.pdf.parser.TextRenderInfo) Implements iTextSharp.text.pdf.parser.IRenderListener.RenderText
    End Sub
End Class