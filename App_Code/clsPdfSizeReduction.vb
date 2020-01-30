Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Public Class clsPdfSizeReduction
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 NK-INC.COM All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>

    Public Sub New()
    End Sub
    Public Function ReducePDFImages(ByVal largePDF() As Byte, ByVal compressionLevel As Integer, Optional ByVal ownerPassword As String = "") As Byte()
        If Not compressionLevel = Nothing Then
            If compressionLevel < 0 Then
                compressionLevel = 0
            ElseIf compressionLevel > 100 Then
                compressionLevel = 100
            End If
        Else
            compressionLevel = 0
        End If
        If ownerPassword Is Nothing Then
            ownerPassword = ""
        End If
        Dim pw() As Byte = frmMain.getBytes(ownerPassword)
        Dim reader As New PdfReader(largePDF, pw)
        Using fs As New MemoryStream()
            Using stamper As New PdfStamper(reader, fs, iTextSharp.text.pdf.PdfWriter.VERSION_1_7)
                For p As Integer = 1 To stamper.Reader.NumberOfPages
                    Dim page As PdfDictionary = reader.GetPageN(p)
                    Dim resources As PdfDictionary = DirectCast(PdfReader.GetPdfObject(page.[Get](PdfName.RESOURCES)), PdfDictionary)
                    Dim xobject As PdfDictionary = DirectCast(PdfReader.GetPdfObject(resources.[Get](PdfName.XOBJECT)), PdfDictionary)
                    If xobject IsNot Nothing Then
                        Dim obj As PdfObject
                        For Each name As PdfName In xobject.Keys
                            obj = xobject.[Get](name)
                            If obj.IsIndirect() Then
                                Dim imgObject As PdfDictionary = DirectCast(PdfReader.GetPdfObject(obj), PdfDictionary)
                                If imgObject.[Get](PdfName.SUBTYPE).Equals(PdfName.IMAGE) Then
                                    If imgObject.[Get](PdfName.FILTER).Equals(PdfName.DCTDECODE) Then
                                        Dim oldBytes As Byte() = PdfReader.GetStreamBytesRaw(DirectCast(imgObject, PRStream))
                                        Dim newBytes As Byte()
                                        Using sourceMS As New MemoryStream(oldBytes)
                                            Using oldImage As System.Drawing.Image = Bitmap.FromStream(sourceMS)
                                                newBytes = ConvertImageToBytes(oldImage, compressionLevel)
                                            End Using
                                        End Using
                                        Dim compressedImage As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(newBytes)
                                        PdfReader.KillIndirect(obj)
                                        stamper.Writer.AddDirectImageSimple(compressedImage, DirectCast(obj, PRIndirectReference))
                                    End If
                                End If
                            End If
                        Next
                    End If
                Next
            End Using
            Return fs.ToArray()
        End Using
        Return largePDF.ToArray()
    End Function
    Public Shared Function imageQuality(ByRef imageX As System.Drawing.Image, Optional ByVal InterpolationMode As InterpolationMode = InterpolationMode.HighQualityBicubic, Optional ByVal compositingQuality As CompositingQuality = CompositingQuality.HighQuality, Optional ByVal smoothingMode As SmoothingMode = SmoothingMode.AntiAlias) As System.Drawing.Image
        Try
            Dim g As System.Drawing.Graphics = Drawing.Graphics.FromImage(imageX)
            g.InterpolationMode = InterpolationMode
            g.CompositingQuality = compositingQuality
            g.SmoothingMode = smoothingMode
            g.DrawImage(imageX, 0, 0, imageX.Width, imageX.Height)
            Dim newImageStream As New MemoryStream
            imageX.Save(newImageStream, System.Drawing.Imaging.ImageFormat.Bmp)
            imageX = System.Drawing.Image.FromStream(newImageStream)
            Return imageX
        Catch ex As Exception
            Throw ex
            Err.Clear()
        End Try
        Return Nothing
    End Function
    Public Shared Function imageQuality(ByRef imageStream As System.IO.MemoryStream, Optional ByVal InterpolationMode As InterpolationMode = InterpolationMode.HighQualityBicubic, Optional ByVal compositingQuality As CompositingQuality = CompositingQuality.HighQuality, Optional ByVal smoothingMode As SmoothingMode = SmoothingMode.AntiAlias) As System.Drawing.Image
        Try
            Dim imageX As System.Drawing.Image = System.Drawing.Image.FromStream(imageStream)
            Dim g As System.Drawing.Graphics = Drawing.Graphics.FromImage(imageX)
            g.InterpolationMode = InterpolationMode
            g.CompositingQuality = compositingQuality
            g.SmoothingMode = smoothingMode
            g.DrawImage(imageX, 0, 0, imageX.Width, imageX.Height)
            Dim newImageStream As New MemoryStream
            imageX.Save(newImageStream, System.Drawing.Imaging.ImageFormat.Bmp)
            If newImageStream.CanSeek Then
                newImageStream.Seek(0, SeekOrigin.Begin)
            End If
            imageX = System.Drawing.Image.FromStream(newImageStream)
            Return imageX
        Catch ex As Exception
            Throw ex
            Err.Clear()
        End Try
        Return Nothing
    End Function
    Private Shared Function ConvertImageToBytes(ByVal image As System.Drawing.Image, ByVal compressionLevel As Long) As Byte()
        If compressionLevel < 0 Then
            compressionLevel = 0
        ElseIf compressionLevel > 100 Then
            compressionLevel = 100
        End If
        Dim jgpEncoder As ImageCodecInfo = GetEncoder(ImageFormat.Jpeg)
        Dim myEncoder As System.Drawing.Imaging.Encoder = System.Drawing.Imaging.Encoder.Quality
        Dim myEncoderParameters As New EncoderParameters(1)
        Dim myEncoderParameter As New EncoderParameter(myEncoder, compressionLevel)
        myEncoderParameters.Param(0) = myEncoderParameter
        Using ms As New MemoryStream()
            image.Save(ms, jgpEncoder, myEncoderParameters)
            Return ms.ToArray()
        End Using
    End Function
    Private Shared Function GetEncoder(ByVal format As ImageFormat) As ImageCodecInfo
        Dim codecs As ImageCodecInfo() = ImageCodecInfo.GetImageDecoders()
        For Each codec As ImageCodecInfo In codecs
            If codec.FormatID = format.Guid Then
                Return codec
            End If
        Next
        Return Nothing
    End Function
    Private Shared Function ShrinkImage(ByVal sourceImage As System.Drawing.Image, ByVal scaleFactor As Single) As System.Drawing.Image
        Dim newWidth As Integer = Convert.ToInt32(sourceImage.Width * scaleFactor)
        Dim newHeight As Integer = Convert.ToInt32(sourceImage.Height * scaleFactor)
        Dim thumbnailBitmap As New Bitmap(newWidth, newHeight)
        Using g As Graphics = Graphics.FromImage(thumbnailBitmap)
            g.CompositingQuality = CompositingQuality.HighQuality
            g.SmoothingMode = SmoothingMode.HighQuality
            g.InterpolationMode = InterpolationMode.HighQualityBicubic
            Dim imageRectangle As New System.Drawing.Rectangle(0, 0, newWidth, newHeight)
            g.DrawImage(sourceImage, imageRectangle)
        End Using
        Return thumbnailBitmap
    End Function
End Class
