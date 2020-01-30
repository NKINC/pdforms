Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.parser
Public Class clsImageRenderListener
    Implements iTextSharp.text.pdf.parser.IRenderListener
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 NK-INC.COM All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>

    Private name As String = "PdfImage-"
    Private destinationFolder As String = ""
    Private counter As Integer = 100000
    Public Sub New(ByVal name As String)
        MyBase.New()
        Me.name = Me.name
    End Sub
    Public Sub New(ByVal prefix As String, ByVal dest As String)
        MyBase.New()
        Me.name = prefix
        Me.destinationFolder = dest
    End Sub
    Public Sub renderImage(ByVal renderInfo As iTextSharp.text.pdf.parser.ImageRenderInfo) Implements iTextSharp.text.pdf.parser.IRenderListener.RenderImage
        Try
            Dim image As PdfImageObject = renderInfo.GetImage
            If (image Is Nothing) Then
                Return
            End If
            Dim number As Integer = renderInfo.GetRef.Number
            If Not (renderInfo.GetRef Is Nothing) Then
                Me.counter = (Me.counter + 1)
                Dim filename As String = destinationFolder.ToString.TrimEnd("\".ToCharArray()) & "\" & String.Format("{0}-{1}.{2}", Me.name, number, image.GetFileType)
                System.IO.File.WriteAllBytes(filename, image.GetImageAsBytes())
                Dim imageDictionary As PdfDictionary = image.GetDictionary
                Dim maskStream As PRStream = CType(imageDictionary.GetAsStream(PdfName.SMASK), PRStream)
                If (Not (maskStream) Is Nothing) Then
                    Dim maskImage As PdfImageObject = New PdfImageObject(maskStream)
                    filename = destinationFolder.ToString.TrimEnd("\".ToCharArray()) & "\" & String.Format("{0}-{1}-mask.{2}", Me.name, number, maskImage.GetFileType)
                    System.IO.File.WriteAllBytes(filename, maskImage.GetImageAsBytes())
                End If
            End If
        Catch e As Exception
            Throw e
        End Try
    End Sub
    Public Sub BeginTextBlock() Implements iTextSharp.text.pdf.parser.IRenderListener.BeginTextBlock
    End Sub
    Public Sub EndTextBlock() Implements iTextSharp.text.pdf.parser.IRenderListener.EndTextBlock
    End Sub
    Public Sub RenderText(ByVal renderInfo As iTextSharp.text.pdf.parser.TextRenderInfo) Implements iTextSharp.text.pdf.parser.IRenderListener.RenderText
    End Sub
End Class
