Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.parser
Imports System.IO
Public Class clsGetImageSize
    Implements iTextSharp.text.pdf.parser.IRenderListener
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 NK-INC.COM All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>

    Private name As String = "PdfImage-"
    Public parent As frmMain
    Public page As Integer
    Public Links As List(Of clsLinks.Link)
    Private destinationFolder As String = ""
    Private counter As Integer = 100000
    Public ImageBytesRaw() As Byte
    Public Shared imageSizeF As System.Drawing.SizeF
    Public Shared imageSize As System.Drawing.Size
    Public imageStartPointF As System.Drawing.PointF
    Public Sub New(ByVal name As String)
        MyBase.New()
        Me.name = Me.name
    End Sub
    Public Sub New(ByRef ImageBytesRaw1() As Byte)
        MyBase.New()
        Me.ImageBytesRaw = ImageBytesRaw1
        'ByRef frm1 As frmMain, Me.parent = frm1
    End Sub
    Public Sub New(ByVal prefix As String, ByVal dest As String)
        MyBase.New()
        Me.name = prefix
        Me.destinationFolder = dest
    End Sub
    Public Function bytesMatch(ByVal b1() As Byte, ByVal b2() As Byte) As Boolean
        Try
            If b1 Is Nothing And Not b2 Is Nothing Then
                Return False
            ElseIf Not b1 Is Nothing And b2 Is Nothing Then
                Return False
            ElseIf b1 Is Nothing And b2 Is Nothing Then
                Return True
            ElseIf b1.Length = 0 And b2.Length = 0 Then
                Return True
            ElseIf b1.Length <= 0 Or b2.Length <= 0 Then
                Return False
            ElseIf b1.ToArray Is b2.ToArray Then
                Return False
            End If
        Catch ex As Exception
        End Try
        Try
            If Not b1.Length = b2.Length Then
                Return False
            Else
                For i As Integer = 0 To b1.Length - 1
                    If b1(i) <> b2(i) Then
                        Return False
                    End If
                Next
            End If
        Catch ex As Exception
        End Try
        Return True
    End Function
    Public Shared Function bytesMatchShared(ByVal b1() As Byte, ByVal b2() As Byte) As Boolean
        Try
            If b1 Is Nothing And Not b2 Is Nothing Then
                Return False
            ElseIf Not b1 Is Nothing And b2 Is Nothing Then
                Return False
            ElseIf b1 Is Nothing And b2 Is Nothing Then
                Return True
            ElseIf b1.Length = 0 And b2.Length = 0 Then
                Return True
            ElseIf b1.Length <= 0 Or b2.Length <= 0 Then
                Return False
            ElseIf b1.ToArray Is b2.ToArray Then
                Return False
            End If
        Catch ex As Exception
        End Try
        Try
            If Not b1.Length = b2.Length Then
                Return False
            Else
                For i As Integer = 0 To b1.Length - 1
                    If b1(i) <> b2(i) Then
                        Return False
                    End If
                Next
            End If
        Catch ex As Exception
        End Try
        Return True
    End Function
    Public Shared Function getImageSizeF(ByVal reader As PdfReader, ByVal page1 As Integer, ByRef ImageBytesRaw1() As Byte) As System.Drawing.SizeF
        Dim parser As iTextSharp.text.pdf.parser.PdfReaderContentParser = New iTextSharp.text.pdf.parser.PdfReaderContentParser(reader)
        Dim listener As clsGetImageSize = New clsGetImageSize(ImageBytesRaw1)
        parser.ProcessContent(page1, listener)
        Return imageSizeF
    End Function
    Public Shared Function getImageSize(ByRef reader As PdfReader, ByVal page1 As Integer, ByRef ImageBytesRaw1() As Byte) As System.Drawing.Size
        Dim parser As iTextSharp.text.pdf.parser.PdfReaderContentParser = New iTextSharp.text.pdf.parser.PdfReaderContentParser(reader)
        Dim listener As clsGetImageSize = New clsGetImageSize(ImageBytesRaw1)
        parser.ProcessContent(page1, listener)
        Return imageSize
    End Function
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
            Dim lnks As New List(Of clsLinks.Link)
            Dim imageBytes() As Byte = Nothing
            Try
                Dim number As Integer = renderInfo.GetRef.Number
                If Not (renderInfo.GetRef Is Nothing) Then
                    Me.counter = (Me.counter + 1)
                    Dim maskStream As PRStream = CType(imageDictionary.GetAsStream(PdfName.SMASK), PRStream)
                    Dim maskImage As PdfImageObject = renderInfo.GetImage() 'New PdfImageObject(maskStream)
                    imageBytes = maskImage.GetImageAsBytes()
                    If bytesMatch(imageBytes, ImageBytesRaw) Then
                        Try
                            Try
                                imageSizeF = New System.Drawing.SizeF(CSng(w), CSng(h))
                                imageSize = New System.Drawing.Size(CInt(w), CInt(h))
                            Catch exGetWH As Exception
                                Err.Clear()
                            End Try
                        Catch ex As Exception
                            Err.Clear()
                        End Try
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