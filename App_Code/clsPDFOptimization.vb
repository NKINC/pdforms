Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.parser
Imports FreeImageAPI
Public Class clsPDFOptimization
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 Nicholas Kowalewicz All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>

    Public Class MyImageRenderListener
        Implements iTextSharp.text.pdf.parser.IRenderListener
        Public Pages As New Dictionary(Of Integer, List(Of ImageScaleInfo))()
        Public m_currentImage As System.Drawing.Image
        Public imageSize As System.Drawing.Size
        Public imageSizeOnForm As System.Drawing.Size
        Public Property CurrentImage() As System.Drawing.Image
            Get
                Return m_currentImage
            End Get
            Set(ByVal value As System.Drawing.Image)
                m_currentImage = value
            End Set
        End Property
        Public Property CurrentPage() As Integer
            Get
                Return m_CurrentPage
            End Get
            Set(ByVal value As Integer)
                m_CurrentPage = value
            End Set
        End Property
        Private m_CurrentPage As Integer

        Public Property CurrentPageUnits() As [Single]
            Get
                Return m_CurrentPageUnits
            End Get
            Set(ByVal value As [Single])
                m_CurrentPageUnits = value
            End Set
        End Property
        Private m_CurrentPageUnits As [Single]

        Public Sub renderImage(ByVal renderInfo As iTextSharp.text.pdf.parser.ImageRenderInfo) Implements iTextSharp.text.pdf.parser.IRenderListener.RenderImage
            Dim img = renderInfo.GetImage().GetDrawingImage()

            Dim imgWidth = img.Width
            Dim imgHeight = img.Height
            img.Dispose()

            Dim ctm = renderInfo.GetImageCTM()
            Dim ctmWidth = ctm(iTextSharp.text.pdf.parser.Matrix.I11)
            Dim ctmHeight = ctm(iTextSharp.text.pdf.parser.Matrix.I22)
            Dim objBytes() As Byte = renderInfo.GetImage().GetImageAsBytes
            If Not Me.Pages.ContainsKey(CurrentPage) Then
                Me.Pages.Add(CurrentPage, New List(Of ImageScaleInfo)())
            End If
            Me.Pages(CurrentPage).Add(New ImageScaleInfo(img, objBytes, imgWidth, imgHeight, ctmWidth, ctmHeight, Me.CurrentPageUnits))
        End Sub

        Public Sub BeginTextBlock() Implements iTextSharp.text.pdf.parser.IRenderListener.BeginTextBlock
        End Sub
        Public Sub EndTextBlock() Implements iTextSharp.text.pdf.parser.IRenderListener.EndTextBlock
        End Sub
        Public Sub RenderText(ByVal renderInfo As iTextSharp.text.pdf.parser.TextRenderInfo) Implements iTextSharp.text.pdf.parser.IRenderListener.RenderText
        End Sub
    End Class



    Public Class ImageScaleInfo
        Public Property PageUnits() As [Single]
            Get
                Return m_PageUnits
            End Get
            Set(ByVal value As [Single])
                m_PageUnits = value
            End Set
        End Property
        Private m_PageUnits As [Single]
        Public Property ImageObject() As System.Drawing.Image
            Get
                Return m_imageObject
            End Get
            Set(ByVal value As System.Drawing.Image)
                m_imageObject = value
            End Set
        End Property
        Private m_imageObject As System.Drawing.Image

        Public Property ImageBytes() As Byte()
            Get
                Return m_imageBytes
            End Get
            Set(ByVal value As Byte())
                m_imageBytes = value
            End Set
        End Property
        Private m_imageBytes As Byte()
        Public Property ImgSize() As System.Drawing.SizeF
            Get
                Return m_ImgSize
            End Get
            Set(ByVal value As System.Drawing.SizeF)
                m_ImgSize = value
            End Set
        End Property
        Private m_ImgSize As System.Drawing.SizeF

        Public Property CtmSize() As System.Drawing.SizeF
            Get
                Return m_CtmSize
            End Get
            Set(ByVal value As System.Drawing.SizeF)
                m_CtmSize = value
            End Set
        End Property
        Private m_CtmSize As System.Drawing.SizeF

        Public ReadOnly Property ImgWidthScale() As [Single]
            Get
                Return ImgSize.Width / CtmSize.Width
            End Get
        End Property
        Public ReadOnly Property ImgHeightScale() As [Single]
            Get
                Return ImgSize.Height / CtmSize.Height
            End Get
        End Property

        Public Sub New(ByVal imgObj As System.Drawing.Image, ByVal imgBytes() As Byte, ByVal imgWidth As [Single], ByVal imgHeight As [Single], ByVal ctmWidth As [Single], ByVal ctmHeight As [Single], ByVal pageUnits As [Single])
            Me.ImgSize = New System.Drawing.SizeF(imgWidth, imgHeight)
            Me.CtmSize = New System.Drawing.SizeF(ctmWidth, ctmHeight)
            Me.PageUnits = pageUnits
            Me.ImageObject = imgObj
            Me.ImageBytes = imgBytes
        End Sub
    End Class
    Function toImage(ByVal bm As System.Drawing.Bitmap, Optional ByVal ImageFormat1 As ImageFormat = Nothing) As System.Drawing.Image
        Dim img As System.Drawing.Image = Nothing
        Try
            Dim m As New MemoryStream
            If ImageFormat1 Is Nothing Then
                ImageFormat1 = System.Drawing.Imaging.ImageFormat.Jpeg
            End If
            bm.Save(m, ImageFormat1)
            img = System.Drawing.Image.FromStream(m)
            Return img
        Catch ex As Exception
            Err.Clear()
        End Try
        Return img
    End Function
    Function toBitmap(ByVal img As System.Drawing.Image) As System.Drawing.Bitmap
        Dim bm As System.Drawing.Bitmap = Nothing
        Try
            bm = New System.Drawing.Bitmap(DirectCast(img.Clone(), System.Drawing.Image))
            Return bm
        Catch ex As Exception
            Err.Clear()
        End Try
        Return bm
    End Function
    Public Function optimizeBitmap(ByVal OriginalBitmap As System.Drawing.Bitmap, ByVal scale As Single, ByVal format As System.Drawing.Imaging.ImageFormat, Optional ByVal InterpolationMode1 As Drawing2D.InterpolationMode = InterpolationMode.HighQualityBicubic, Optional ByVal SmoothingMode1 As SmoothingMode = SmoothingMode.HighQuality, Optional ByVal CompositingQuality1 As CompositingQuality = CompositingQuality.HighQuality, Optional ByVal AveragePixelColor As Integer = -1, Optional ByVal imageMask As System.Drawing.Bitmap = Nothing, Optional ByVal autoResizeImages As Boolean = False) As Byte()
        Try
            Dim iwidth As Integer = OriginalBitmap.Width
            Dim iheight As Integer = OriginalBitmap.Height
            Dim ipixelFormat As System.Drawing.Imaging.PixelFormat = OriginalBitmap.PixelFormat
            Dim isAlpha As Boolean = System.Drawing.Bitmap.IsAlphaPixelFormat(OriginalBitmap.PixelFormat)
            If Not imageMask Is Nothing Then
                ipixelFormat = PixelFormat.Format32bppArgb
                format = System.Drawing.Imaging.ImageFormat.Png
            ElseIf isAlpha Then
                ipixelFormat = PixelFormat.Format32bppArgb
                format = System.Drawing.Imaging.ImageFormat.Png
            End If
            Dim iImageFormat As System.Drawing.Imaging.ImageFormat = format
            Dim optRectangle As New System.Drawing.Rectangle(0, 0, CInt(iwidth * scale), CInt(iheight * scale))
            Dim originalRectangle As New System.Drawing.Rectangle(0, 0, iwidth, iheight)
            Dim newWidth As Integer = scale * OriginalBitmap.Width, newHeight As Integer = scale * OriginalBitmap.Height
            If SmoothingMode1 = SmoothingMode.AntiAlias And (originalRectangle.Size.Width > (optRectangle.Width) OrElse originalRectangle.Size.Height > (optRectangle.Height)) Then

            End If
            Dim objOriginalImage As System.Drawing.Bitmap = Nothing
            Try
                Select Case OriginalBitmap.PixelFormat
                    Case PixelFormat.Format1bppIndexed, PixelFormat.Format4bppIndexed, PixelFormat.Format8bppIndexed, PixelFormat.Indexed
                        ipixelFormat = PixelFormat.Format24bppRgb
                        objOriginalImage = New System.Drawing.Bitmap(originalRectangle.Width, originalRectangle.Height, ipixelFormat)
                        For x As Integer = 0 To objOriginalImage.Width - 1
                            For y As Integer = 0 To objOriginalImage.Height - 1
                                objOriginalImage.SetPixel(x, y, OriginalBitmap.GetPixel(x, y))
                            Next
                        Next
                    Case Else
                        objOriginalImage = OriginalBitmap.Clone
                End Select
            Catch ex As Exception
                ipixelFormat = PixelFormat.Format24bppRgb
                objOriginalImage = New System.Drawing.Bitmap(originalRectangle.Width, originalRectangle.Height, ipixelFormat)
                For x As Integer = 0 To objOriginalImage.Width - 1
                    For y As Integer = 0 To objOriginalImage.Height - 1
                        objOriginalImage.SetPixel(x, y, OriginalBitmap.GetPixel(x, y))
                    Next
                Next
            End Try
            Try
                If ipixelFormat = PixelFormat.Format32bppArgb Then
                    Dim q As New nQuant.WuQuantizer
                    Using m As New MemoryStream
                        Dim objImageOriginalClone As System.Drawing.Bitmap = OriginalBitmap.Clone
                        If Not imageMask Is Nothing Then
                            imageMask.MakeTransparent(Color.Black)
                            For x As Integer = 0 To objImageOriginalClone.Width - 1
                                For y As Integer = 0 To objImageOriginalClone.Height - 1
                                    Dim c As Color = imageMask.GetPixel(x, y)
                                    If c.A = 0 And c.R = 0 And c.G = 0 And c.B = 0 Then
                                        objImageOriginalClone.SetPixel(x, y, Color.Transparent)
                                    ElseIf c.A < 128 Then
                                        objImageOriginalClone.SetPixel(x, y, Color.Transparent)
                                    End If
                                Next
                            Next
                        End If
                        Dim objOptImage As System.Drawing.Bitmap = objOriginalImage.Clone
                        If autoResizeImages OrElse (Not optRectangle.Width = originalRectangle.Width And Not optRectangle.Height = originalRectangle.Height) Then
                            Using oGraphic As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(objOptImage)
                                With oGraphic
                                    .SmoothingMode = SmoothingMode1
                                    .PixelOffsetMode = PixelOffsetMode.None
                                    .CompositingQuality = CompositingQuality1
                                    .InterpolationMode = InterpolationMode1
                                    .CompositingMode = CompositingMode.SourceCopy
                                    .DrawImage(objOriginalImage.Clone, originalRectangle, originalRectangle, GraphicsUnit.Pixel)
                                    oGraphic.Dispose()
                                End With
                            End Using
                            objOptImage = New System.Drawing.Bitmap(objOptImage.Clone, optRectangle.Width, optRectangle.Height)
                            objOptImage.Save(m, format)
                            Return m.ToArray
                        Else
                            objOptImage.Save(m, format)
                            Return m.ToArray
                        End If
                        objOptImage.Save(m, format)
                        Return m.ToArray
                    End Using
                End If
            Catch exPNG As Exception
                Err.Clear()
            End Try
            Try
                Using oGraphic As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(objOriginalImage)
                    With oGraphic
                        .SmoothingMode = SmoothingMode.HighQuality
                        .PixelOffsetMode = PixelOffsetMode.None
                        .CompositingQuality = CompositingQuality.HighQuality
                        .InterpolationMode = InterpolationMode.HighQualityBicubic
                        .CompositingMode = CompositingMode.SourceCopy
                        If Not imageMask Is Nothing Then
                            .DrawImage(imageMask, originalRectangle, originalRectangle, GraphicsUnit.Pixel)
                            .DrawImage(objOriginalImage, originalRectangle, originalRectangle, GraphicsUnit.Pixel)
                        Else
                        End If
                        oGraphic.Dispose()
                    End With
                End Using
            Catch ex As Exception
                Err.Clear()
                objOriginalImage = New System.Drawing.Bitmap(originalRectangle.Width, originalRectangle.Height, ipixelFormat)
                For x As Integer = 0 To objOriginalImage.Width - 1
                    For y As Integer = 0 To objOriginalImage.Height - 1
                        objOriginalImage.SetPixel(x, y, OriginalBitmap.GetPixel(x, y))
                    Next
                Next
            End Try
            Using objOriginalImage
                Using m As New MemoryStream
                    Try
                        If Not imageMask Is Nothing Then
                            Dim objOriginalImageTemp As System.Drawing.Bitmap = objOriginalImage.Clone
                            For x As Long = 0 To objOriginalImage.Width - 1
                                For y As Long = 0 To objOriginalImage.Height - 1
                                    Dim c As System.Drawing.Color = imageMask.GetPixel(x, y)
                                    If (c.R = 0 And c.G = 0 And c.B = 0) Or c.A < 128 Then
                                        objOriginalImage.SetPixel(x, y, Color.Transparent)
                                    Else
                                        If AveragePixelColor > 0 Then
                                            c = getAveragePixelColor_Shared(objOriginalImageTemp.Clone(), x, y, AveragePixelColor)
                                        Else
                                            c = objOriginalImageTemp.GetPixel(x, y)
                                        End If
                                        objOriginalImage.SetPixel(x, y, c)
                                    End If
                                Next
                            Next
                        End If
                        Dim objOptImage As System.Drawing.Bitmap = objOriginalImage.Clone
                        Using oGraphic As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(objOptImage)
                            With oGraphic
                                .SmoothingMode = SmoothingMode1
                                .PixelOffsetMode = PixelOffsetMode.None
                                .CompositingQuality = CompositingQuality1
                                .InterpolationMode = InterpolationMode1
                                .CompositingMode = CompositingMode.SourceCopy
                                .DrawImage(objOriginalImage, originalRectangle, originalRectangle, GraphicsUnit.Pixel)
                                oGraphic.Dispose()
                            End With
                        End Using
                        If autoResizeImages OrElse (Not optRectangle.Width = originalRectangle.Width And Not optRectangle.Height = originalRectangle.Height) Then
                            objOptImage = New System.Drawing.Bitmap(objOptImage.Clone, optRectangle.Width, optRectangle.Height)
                            objOptImage.Save(m, format)
                        Else
                            objOriginalImage.Save(m, format)
                        End If
                        If m.CanSeek Then
                            m.Seek(0, SeekOrigin.Begin)
                        End If
                        Return m.ToArray
                    Catch ex As Exception
                        Throw ex
                    End Try
                End Using
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function
    Public Function optimizeBitmap(ByVal imgData() As Byte, ByVal imageMask As System.Drawing.Bitmap, ByVal scale As Single, ByVal format As System.Drawing.Imaging.ImageFormat, Optional ByVal InterpolationMode1 As Drawing2D.InterpolationMode = InterpolationMode.HighQualityBicubic, Optional ByVal SmoothingMode1 As SmoothingMode = SmoothingMode.HighQuality, Optional ByVal CompositingQuality1 As CompositingQuality = CompositingQuality.HighQuality, Optional ByVal AveragePixelColor As Integer = -1, Optional ByVal autoResizeImages As Boolean = False) As Byte()
        Try
            Dim OriginalBitmap As System.Drawing.Bitmap = DirectCast(System.Drawing.Bitmap.FromStream(New MemoryStream(imgData)), System.Drawing.Bitmap)
            Dim iwidth As Integer = OriginalBitmap.Width
            Dim iheight As Integer = OriginalBitmap.Height
            Dim ipixelFormat As System.Drawing.Imaging.PixelFormat = OriginalBitmap.PixelFormat
            Dim isAlpha As Boolean = System.Drawing.Bitmap.IsAlphaPixelFormat(OriginalBitmap.PixelFormat)
            If Not imageMask Is Nothing Then
                ipixelFormat = PixelFormat.Format32bppArgb
                format = System.Drawing.Imaging.ImageFormat.Png
            ElseIf isAlpha Then
                ipixelFormat = PixelFormat.Format32bppArgb
                format = System.Drawing.Imaging.ImageFormat.Png
            End If
            Dim iImageFormat As System.Drawing.Imaging.ImageFormat = format
            Dim optRectangle As New System.Drawing.Rectangle(0, 0, CInt(iwidth * scale), CInt(iheight * scale))
            Dim originalRectangle As New System.Drawing.Rectangle(0, 0, iwidth, iheight)
            Dim newWidth As Integer = scale * OriginalBitmap.Width, newHeight As Integer = scale * OriginalBitmap.Height
            If SmoothingMode1 = SmoothingMode.AntiAlias And (originalRectangle.Size.Width > (optRectangle.Width) OrElse originalRectangle.Size.Height > (optRectangle.Height)) Then

            End If
            Try
                Dim objOriginalImage As System.Drawing.Bitmap = Nothing
                Try
                    Select Case OriginalBitmap.PixelFormat
                        Case PixelFormat.Format1bppIndexed, PixelFormat.Format4bppIndexed, PixelFormat.Format8bppIndexed, PixelFormat.Indexed
                            ipixelFormat = PixelFormat.Format24bppRgb
                            objOriginalImage = New System.Drawing.Bitmap(originalRectangle.Width, originalRectangle.Height, ipixelFormat)
                            For x As Integer = 0 To objOriginalImage.Width - 1
                                For y As Integer = 0 To objOriginalImage.Height - 1
                                    objOriginalImage.SetPixel(x, y, OriginalBitmap.GetPixel(x, y))
                                Next
                            Next
                        Case Else
                            objOriginalImage = OriginalBitmap.Clone
                    End Select
                Catch ex As Exception
                    ipixelFormat = PixelFormat.Format24bppRgb
                    objOriginalImage = New System.Drawing.Bitmap(originalRectangle.Width, originalRectangle.Height, ipixelFormat)
                    For x As Integer = 0 To objOriginalImage.Width - 1
                        For y As Integer = 0 To objOriginalImage.Height - 1
                            objOriginalImage.SetPixel(x, y, OriginalBitmap.GetPixel(x, y))
                        Next
                    Next
                End Try
                Try
                    If ipixelFormat = PixelFormat.Format32bppArgb Then
                        Dim q As New nQuant.WuQuantizer
                        Using m As New MemoryStream
                            Dim objImageOriginalClone As System.Drawing.Bitmap = OriginalBitmap.Clone
                            If Not imageMask Is Nothing Then
                                imageMask.MakeTransparent(Color.Black)
                                For x As Integer = 0 To objImageOriginalClone.Width - 1
                                    For y As Integer = 0 To objImageOriginalClone.Height - 1
                                        Dim c As Color = imageMask.GetPixel(x, y)
                                        If c.A = 0 And c.R = 0 And c.G = 0 And c.B = 0 Then
                                            objImageOriginalClone.SetPixel(x, y, Color.Transparent)
                                        ElseIf c.A < 128 Then
                                            objImageOriginalClone.SetPixel(x, y, Color.Transparent)
                                        End If
                                    Next
                                Next
                            End If
                            Dim objOptImage As System.Drawing.Bitmap = objOriginalImage.Clone
                            If autoResizeImages OrElse (Not optRectangle.Width = originalRectangle.Width And Not optRectangle.Height = originalRectangle.Height) Then
                                Using oGraphic As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(objOptImage)
                                    With oGraphic
                                        .SmoothingMode = SmoothingMode1
                                        .PixelOffsetMode = PixelOffsetMode.None
                                        .CompositingQuality = CompositingQuality1
                                        .InterpolationMode = InterpolationMode1
                                        .CompositingMode = CompositingMode.SourceCopy
                                        .DrawImage(objOriginalImage.Clone, originalRectangle, originalRectangle, GraphicsUnit.Pixel)
                                        oGraphic.Dispose()
                                    End With
                                End Using
                                objOptImage = New System.Drawing.Bitmap(objOptImage.Clone, optRectangle.Width, optRectangle.Height)
                                objOptImage.Save(m, format)
                                Return m.ToArray
                            Else
                                objOptImage.Save(m, format)
                                Return m.ToArray
                            End If
                            objOptImage.Save(m, format)
                            Return m.ToArray
                        End Using
                    End If
                Catch exPNG As Exception
                    Err.Clear()
                End Try
                Try
                    Using oGraphic As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(objOriginalImage)
                        With oGraphic
                            .SmoothingMode = SmoothingMode.HighQuality
                            .PixelOffsetMode = PixelOffsetMode.None
                            .CompositingQuality = CompositingQuality.HighQuality
                            .InterpolationMode = InterpolationMode.HighQualityBicubic
                            .CompositingMode = CompositingMode.SourceCopy
                            If Not imageMask Is Nothing Then
                                .DrawImage(imageMask, originalRectangle, originalRectangle, GraphicsUnit.Pixel)
                                .DrawImage(objOriginalImage, originalRectangle, originalRectangle, GraphicsUnit.Pixel)
                            Else
                            End If
                            oGraphic.Dispose()
                        End With
                    End Using
                Catch ex As Exception
                    Err.Clear()
                    objOriginalImage = New System.Drawing.Bitmap(originalRectangle.Width, originalRectangle.Height, ipixelFormat)
                    For x As Integer = 0 To objOriginalImage.Width - 1
                        For y As Integer = 0 To objOriginalImage.Height - 1
                            objOriginalImage.SetPixel(x, y, OriginalBitmap.GetPixel(x, y))
                        Next
                    Next
                End Try
                Using objOriginalImage
                    Using m As New MemoryStream
                        Try
                            If Not imageMask Is Nothing Then
                                Dim objOriginalImageTemp As System.Drawing.Bitmap = objOriginalImage.Clone
                                For x As Long = 0 To objOriginalImage.Width - 1
                                    For y As Long = 0 To objOriginalImage.Height - 1
                                        Dim c As System.Drawing.Color = imageMask.GetPixel(x, y)
                                        If (c.R = 0 And c.G = 0 And c.B = 0) Or c.A < 128 Then
                                            objOriginalImage.SetPixel(x, y, Color.Transparent)
                                        Else
                                            If AveragePixelColor > 0 Then
                                                c = getAveragePixelColor_Shared(objOriginalImageTemp.Clone(), x, y, AveragePixelColor)
                                            Else
                                                c = objOriginalImageTemp.GetPixel(x, y)
                                            End If
                                            objOriginalImage.SetPixel(x, y, c)
                                        End If
                                    Next
                                Next
                            End If
                            Dim objOptImage As System.Drawing.Bitmap = objOriginalImage.Clone
                            Using oGraphic As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(objOptImage)
                                With oGraphic
                                    .SmoothingMode = SmoothingMode1
                                    .PixelOffsetMode = PixelOffsetMode.None
                                    .CompositingQuality = CompositingQuality1
                                    .InterpolationMode = InterpolationMode1
                                    .CompositingMode = CompositingMode.SourceCopy
                                    .DrawImage(objOriginalImage, originalRectangle, originalRectangle, GraphicsUnit.Pixel)
                                    oGraphic.Dispose()
                                End With
                            End Using
                            If autoResizeImages OrElse (Not optRectangle.Width = originalRectangle.Width And Not optRectangle.Height = originalRectangle.Height) Then
                                objOptImage = New System.Drawing.Bitmap(objOptImage.Clone, optRectangle.Width, optRectangle.Height)
                                objOptImage.Save(m, format)
                            Else
                                objOriginalImage.Save(m, format)
                            End If
                            If m.CanSeek Then
                                m.Seek(0, SeekOrigin.Begin)
                            End If
                            Return m.ToArray
                        Catch ex As Exception
                            Throw ex
                        End Try
                    End Using
                End Using
            Catch ex As Exception
                Throw ex
            End Try
        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function
    Public Function optimizeBitmap(ByVal imgData() As Byte, ByVal scale As Single, ByVal format As System.Drawing.Imaging.ImageFormat, Optional ByVal InterpolationMode1 As Drawing2D.InterpolationMode = InterpolationMode.HighQualityBicubic, Optional ByVal SmoothingMode1 As SmoothingMode = SmoothingMode.HighQuality, Optional ByVal CompositingQuality1 As CompositingQuality = CompositingQuality.HighQuality, Optional ByVal AveragePixelColor As Integer = -1, Optional ByVal imageMaskBytes() As Byte = Nothing, Optional ByVal autoResizeImages As Boolean = False, Optional ByRef ProgressBarX As ProgressBar = Nothing) As Byte()
        If Not ProgressBarX Is Nothing Then
            If ProgressBarX.GetType Is GetType(ProgressBar) Then
                If Not ProgressBarX.Enabled Then ProgressBarX.Enabled = True
                If Not ProgressBarX.Visible Then ProgressBarX.Visible = True
                ProgressBarX.Value = 0
            End If
        End If

        Try
            Dim OriginalBitmap As System.Drawing.Bitmap = DirectCast(System.Drawing.Bitmap.FromStream(New MemoryStream(imgData)), System.Drawing.Bitmap)
            Try
                If imageMaskBytes Is Nothing Then
                    imageMaskBytes = New Byte() {}
                End If
                If format Is Nothing Then
                    format = OriginalBitmap.RawFormat
                End If
                If format.GetHashCode() = System.Drawing.Imaging.ImageFormat.MemoryBmp.GetHashCode() Then
                    If Not imageMaskBytes Is Nothing Then
                        If imageMaskBytes.Length > 0 Then
                            format = System.Drawing.Imaging.ImageFormat.Png
                        Else
                            format = System.Drawing.Imaging.ImageFormat.Jpeg
                        End If
                    Else
                        format = System.Drawing.Imaging.ImageFormat.Jpeg
                    End If
                Else
                    If Not imageMaskBytes Is Nothing Then
                        If imageMaskBytes.Length > 0 Then
                            format = System.Drawing.Imaging.ImageFormat.Png
                        End If
                    End If
                End If
            Catch exFormat As Exception
                format = System.Drawing.Imaging.ImageFormat.Jpeg
                Err.Clear()
            End Try
            Dim iwidth As Integer = OriginalBitmap.Width
            Dim iheight As Integer = OriginalBitmap.Height
            Dim ipixelFormat As System.Drawing.Imaging.PixelFormat = OriginalBitmap.PixelFormat
            Dim iImageFormat As System.Drawing.Imaging.ImageFormat = format
            Dim optRectangle As New System.Drawing.Rectangle(0, 0, CInt(iwidth * scale), CInt(iheight * scale))
            Dim originalRectangle As New System.Drawing.Rectangle(0, 0, iwidth, iheight)
            Dim newWidth As Integer = scale * OriginalBitmap.Width, newHeight As Integer = scale * OriginalBitmap.Height
            Dim imageMask As System.Drawing.Bitmap = Nothing
            If imageMask Is Nothing And imageMaskBytes.Length > 0 Then
                imageMask = System.Drawing.Bitmap.FromStream(New MemoryStream(imageMaskBytes), True)
            End If
            Dim isAlpha As Boolean = System.Drawing.Bitmap.IsAlphaPixelFormat(OriginalBitmap.PixelFormat)
            If Not imageMask Is Nothing Then
                ipixelFormat = PixelFormat.Format32bppArgb
                format = System.Drawing.Imaging.ImageFormat.Png
            ElseIf isAlpha Then
                ipixelFormat = PixelFormat.Format32bppArgb
                format = System.Drawing.Imaging.ImageFormat.Png
            End If
            If SmoothingMode1 = SmoothingMode.AntiAlias And (originalRectangle.Size.Width > (optRectangle.Width) OrElse originalRectangle.Size.Height > (optRectangle.Height)) Then

            End If
            Try
                Dim objOriginalImage As System.Drawing.Bitmap = Nothing
                Try
                    Select Case OriginalBitmap.PixelFormat
                        Case PixelFormat.Format1bppIndexed, PixelFormat.Format4bppIndexed, PixelFormat.Format8bppIndexed, PixelFormat.Indexed
                            ipixelFormat = PixelFormat.Format24bppRgb
                            objOriginalImage = New System.Drawing.Bitmap(originalRectangle.Width, originalRectangle.Height, ipixelFormat)
                            For x As Integer = 0 To objOriginalImage.Width - 1
                                For y As Integer = 0 To objOriginalImage.Height - 1
                                    objOriginalImage.SetPixel(x, y, OriginalBitmap.GetPixel(x, y))
                                Next
                            Next
                        Case Else
                            objOriginalImage = OriginalBitmap.Clone
                    End Select
                Catch ex As Exception
                    ipixelFormat = PixelFormat.Format24bppRgb
                    objOriginalImage = New System.Drawing.Bitmap(originalRectangle.Width, originalRectangle.Height, ipixelFormat)
                    For x As Integer = 0 To objOriginalImage.Width - 1
                        For y As Integer = 0 To objOriginalImage.Height - 1
                            objOriginalImage.SetPixel(x, y, OriginalBitmap.GetPixel(x, y))
                        Next
                    Next
                End Try
                Try
                    If ipixelFormat = PixelFormat.Format32bppArgb Then
                        Dim q As New nQuant.WuQuantizer
                        Using m As New MemoryStream
                            Dim objImageOriginalClone As System.Drawing.Bitmap = OriginalBitmap.Clone
                            If imageMask Is Nothing And imageMaskBytes.Length > 0 Then
                                imageMask = System.Drawing.Bitmap.FromStream(New MemoryStream(imageMaskBytes), True)
                                imageMask.MakeTransparent(Color.Black)
                            ElseIf Not imageMask Is Nothing Then
                                imageMask.MakeTransparent(Color.Black)
                            End If
                            For x As Integer = 0 To objImageOriginalClone.Width - 1
                                For y As Integer = 0 To objImageOriginalClone.Height - 1
                                    Dim c As Color = imageMask.GetPixel(x, y)
                                    If c.A = 0 And c.R = 0 And c.G = 0 And c.B = 0 Then
                                        objImageOriginalClone.SetPixel(x, y, Color.Transparent)
                                    ElseIf c.A < 128 Then
                                        objImageOriginalClone.SetPixel(x, y, Color.Transparent)
                                    End If
                                Next
                            Next
                            Dim objOptImage As System.Drawing.Bitmap = objOriginalImage.Clone
                            If autoResizeImages OrElse (Not optRectangle.Width = originalRectangle.Width And Not optRectangle.Height = originalRectangle.Height) Then
                                Using oGraphic As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(objOptImage)
                                    With oGraphic
                                        .SmoothingMode = SmoothingMode1
                                        .PixelOffsetMode = PixelOffsetMode.None
                                        .CompositingQuality = CompositingQuality1
                                        .InterpolationMode = InterpolationMode1
                                        .CompositingMode = CompositingMode.SourceCopy
                                        .DrawImage(objOriginalImage.Clone, originalRectangle, originalRectangle, GraphicsUnit.Pixel)
                                        oGraphic.Dispose()
                                    End With
                                End Using
                                objOptImage = New System.Drawing.Bitmap(objOptImage.Clone, optRectangle.Width, optRectangle.Height)
                                objOptImage.Save(m, format)
                                Return m.ToArray
                            Else
                                objOptImage.Save(m, format)
                                Return m.ToArray
                            End If
                            objOptImage.Save(m, format)
                            Return m.ToArray
                        End Using
                    End If
                Catch exPNG As Exception
                    Err.Clear()
                End Try
                Try
                    Using oGraphic As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(objOriginalImage)
                        With oGraphic
                            .SmoothingMode = SmoothingMode.HighQuality
                            .PixelOffsetMode = PixelOffsetMode.None
                            .CompositingQuality = CompositingQuality.HighQuality
                            .InterpolationMode = InterpolationMode.HighQualityBicubic
                            .CompositingMode = CompositingMode.SourceCopy
                            If Not imageMask Is Nothing Then
                                .DrawImage(imageMask, originalRectangle, originalRectangle, GraphicsUnit.Pixel)
                                .DrawImage(objOriginalImage, originalRectangle, originalRectangle, GraphicsUnit.Pixel)
                            Else
                            End If
                            oGraphic.Dispose()
                        End With
                    End Using
                Catch ex As Exception
                    Err.Clear()
                    objOriginalImage = New System.Drawing.Bitmap(originalRectangle.Width, originalRectangle.Height, ipixelFormat)
                    For x As Integer = 0 To objOriginalImage.Width - 1
                        For y As Integer = 0 To objOriginalImage.Height - 1
                            objOriginalImage.SetPixel(x, y, OriginalBitmap.GetPixel(x, y))
                        Next
                    Next
                End Try
                Using objOriginalImage
                    Using m As New MemoryStream
                        Try
                            If Not imageMask Is Nothing Then
                                Dim objOriginalImageTemp As System.Drawing.Bitmap = objOriginalImage.Clone
                                For x As Long = 0 To objOriginalImage.Width - 1
                                    For y As Long = 0 To objOriginalImage.Height - 1
                                        Dim c As System.Drawing.Color = imageMask.GetPixel(x, y)
                                        If (c.R = 0 And c.G = 0 And c.B = 0) Or c.A < 128 Then
                                            objOriginalImage.SetPixel(x, y, Color.Transparent)
                                        Else
                                            If AveragePixelColor > 0 Then
                                                c = getAveragePixelColor_Shared(objOriginalImageTemp.Clone(), x, y, AveragePixelColor)
                                            Else
                                                c = objOriginalImageTemp.GetPixel(x, y)
                                            End If
                                            objOriginalImage.SetPixel(x, y, c)
                                        End If
                                    Next
                                Next
                            End If
                            Dim objOptImage As System.Drawing.Bitmap = objOriginalImage.Clone
                            Using oGraphic As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(objOptImage)
                                With oGraphic
                                    .SmoothingMode = SmoothingMode1
                                    .PixelOffsetMode = PixelOffsetMode.None
                                    .CompositingQuality = CompositingQuality1
                                    .InterpolationMode = InterpolationMode1
                                    .CompositingMode = CompositingMode.SourceCopy
                                    .DrawImage(objOriginalImage, originalRectangle, originalRectangle, GraphicsUnit.Pixel)
                                    oGraphic.Dispose()
                                End With
                            End Using
                            If autoResizeImages OrElse (Not optRectangle.Width = originalRectangle.Width And Not optRectangle.Height = originalRectangle.Height) Then
                                objOptImage = New System.Drawing.Bitmap(objOptImage.Clone, optRectangle.Width, optRectangle.Height)
                                objOptImage.Save(m, format)
                            Else
                                objOriginalImage.Save(m, format)
                            End If
                            If m.CanSeek Then
                                m.Seek(0, SeekOrigin.Begin)
                            End If
                            Return m.ToArray
                        Catch ex As Exception
                            Throw ex
                        End Try
                    End Using

                End Using
            Catch ex As Exception
                Throw ex
            End Try
        Catch ex As Exception
            Throw ex
        Finally
            If Not ProgressBarX Is Nothing Then
                If ProgressBarX.GetType Is GetType(ProgressBar) Then
                    If Not ProgressBarX.Enabled Then ProgressBarX.Enabled = True
                    If Not ProgressBarX.Visible Then ProgressBarX.Visible = True
                    ProgressBarX.Value = 100
                End If
            End If
        End Try
        Return Nothing
    End Function
    Public Function abortDelegate() As Boolean
        Return False
    End Function
    Public Shared Function abortDelegateShared() As Boolean
        Return False
    End Function
    Public Function getAveragePixelColor(ByVal b As System.Drawing.Bitmap, ByVal x As Integer, ByVal y As Integer, Optional ByVal surroundingPixelCount As Integer = 1) As System.Drawing.Color
        Try
            If surroundingPixelCount < 0 Then
                surroundingPixelCount = 0
            End If
            Dim x1 As Long = x - surroundingPixelCount
            Dim x2 As Long = x + surroundingPixelCount
            Dim y1 As Long = y - surroundingPixelCount
            Dim y2 As Long = y + surroundingPixelCount
            If x1 < 0 Then
                x1 = 0
            End If
            If x2 >= b.Width Then
                x2 = b.Width - 1
            End If
            If y1 < 0 Then
                y1 = 0
            End If
            If y2 >= b.Height Then
                y2 = b.Height - 1
            End If
            Dim c As Long = 0, aR As Long = 0, aG As Long = 0, aB As Long = 0, aA As Long = 0
            For x3 As Long = x1 To x2
                For y3 As Long = y1 To y2
                    Dim blnOk As Boolean = False
                    If surroundingPixelCount Mod 2 Then
                        If x3 Mod 2 And y3 Mod 2 Then
                            blnOk = True
                        End If
                    ElseIf surroundingPixelCount Mod 3 Then
                        If x3 Mod 3 And y3 Mod 3 Then
                            blnOk = True
                        End If
                    End If
                    If blnOk And surroundingPixelCount >= Math.Sqrt(((x - x3) ^ 2) + ((y - y3) ^ 2)) Then
                        With b.GetPixel(x3, y3)

                            c += 1
                            aR += .R
                            aG += .G
                            aB += .B
                            aA += .A
                        End With
                    End If
                Next
            Next
            Return System.Drawing.Color.FromArgb(CInt(aA / c), CInt(aR / c), CInt(aG / c), CInt(aB / c))
        Catch ex As Exception
            Err.Clear()
        Finally

        End Try
        Try
            Return b.GetPixel(x, y)
        Catch ex As Exception
            Err.Clear()
        End Try
        Return Nothing
    End Function
    Public Shared cancelOptimize_Shared As Boolean = False
    Public Sub abortOptimizeProcess()
        cancelOptimize = True
        cancelOptimize_Shared = True
    End Sub
    Public Shared Function getAveragePixelColor_Shared(ByVal b As System.Drawing.Bitmap, ByVal x As Integer, ByVal y As Integer, Optional ByVal surroundingPixelCount As Integer = 1) As System.Drawing.Color
        Try
            If surroundingPixelCount < 0 Then
                surroundingPixelCount = 0
            End If
            Dim x1 As Long = x - surroundingPixelCount
            Dim x2 As Long = x + surroundingPixelCount
            Dim y1 As Long = y - surroundingPixelCount
            Dim y2 As Long = y + surroundingPixelCount
            If x1 < 0 Then
                x1 = 0
            End If
            If x2 >= b.Width Then
                x2 = b.Width - 1
            End If
            If y1 < 0 Then
                y1 = 0
            End If
            If y2 >= b.Height Then
                y2 = b.Height - 1
            End If
            Dim c As Long = 0, aR As Long = 0, aG As Long = 0, aB As Long = 0, aA As Long = 0
            For x3 As Long = x1 To x2
                For y3 As Long = y1 To y2
                    If cancelOptimize_Shared Then Return Nothing
                    Dim blnOk As Boolean = False
                    If surroundingPixelCount Mod 2 Then
                        If x3 Mod 2 And y3 Mod 2 Then
                            blnOk = True
                        End If
                    ElseIf surroundingPixelCount Mod 3 Then
                        If x3 Mod 3 And y3 Mod 3 Then
                            blnOk = True
                        End If
                    End If
                    If blnOk And surroundingPixelCount >= Math.Sqrt(((x - x3) ^ 2) + ((y - y3) ^ 2)) Then
                        With b.GetPixel(x3, y3)
                            c += 1
                            aR += .R
                            aG += .G
                            aB += .B
                            aA += .A
                        End With
                    End If
                Next
            Next
            Return System.Drawing.Color.FromArgb(CInt(aA / c), CInt(aR / c), CInt(aG / c), CInt(aB / c))
        Catch ex As Exception
            Err.Clear()
        Finally

        End Try
        Try
            Return b.GetPixel(x, y)
        Catch ex As Exception
            Err.Clear()
        End Try
        Return Nothing
    End Function

    Public Function optimizeBitmap(ByVal OriginalBitmap As System.Drawing.Bitmap, ByVal newWidth As Integer, ByVal newHeight As Integer, ByVal format As System.Drawing.Imaging.ImageFormat, Optional ByVal InterpolationMode1 As Drawing2D.InterpolationMode = InterpolationMode.HighQualityBicubic, Optional ByVal SmoothingMode1 As SmoothingMode = SmoothingMode.HighQuality, Optional ByVal CompositingQuality1 As CompositingQuality = CompositingQuality.HighQuality, Optional ByVal AveragePixelColor As Integer = -1, Optional ByVal imageMask As System.Drawing.Bitmap = Nothing, Optional ByVal autoResizeImages As Boolean = False) As Byte()
        Try
            Dim iwidth As Integer = OriginalBitmap.Width
            Dim iheight As Integer = OriginalBitmap.Height
            Dim ipixelFormat As System.Drawing.Imaging.PixelFormat = OriginalBitmap.PixelFormat
            Dim isAlpha As Boolean = System.Drawing.Bitmap.IsAlphaPixelFormat(OriginalBitmap.PixelFormat)
            If Not imageMask Is Nothing Then
                ipixelFormat = PixelFormat.Format32bppArgb
                format = System.Drawing.Imaging.ImageFormat.Png
            ElseIf isAlpha Then
                ipixelFormat = PixelFormat.Format32bppArgb
                format = System.Drawing.Imaging.ImageFormat.Png
            End If
            Dim ipxfrmt As String = OriginalBitmap.PixelFormat.ToString

            Dim iImageFormat As System.Drawing.Imaging.ImageFormat = format
            Dim optRectangle As New System.Drawing.Rectangle(0, 0, newWidth, newHeight)
            Dim originalRectangle As New System.Drawing.Rectangle(0, 0, iwidth, iheight)
            If SmoothingMode1 = SmoothingMode.AntiAlias And (originalRectangle.Size.Width > (optRectangle.Width) OrElse originalRectangle.Size.Height > (optRectangle.Height)) Then

            End If
            Dim objOriginalImage As System.Drawing.Bitmap = Nothing
            Try
                Select Case OriginalBitmap.PixelFormat
                    Case PixelFormat.Format1bppIndexed, PixelFormat.Format4bppIndexed, PixelFormat.Format8bppIndexed, PixelFormat.Indexed
                        ipixelFormat = PixelFormat.Format24bppRgb
                        objOriginalImage = New System.Drawing.Bitmap(originalRectangle.Width, originalRectangle.Height, ipixelFormat)
                        For x As Integer = 0 To objOriginalImage.Width - 1
                            For y As Integer = 0 To objOriginalImage.Height - 1
                                objOriginalImage.SetPixel(x, y, OriginalBitmap.GetPixel(x, y))
                            Next
                        Next
                    Case Else
                        objOriginalImage = OriginalBitmap.Clone
                End Select
            Catch ex As Exception
                ipixelFormat = PixelFormat.Format24bppRgb
                objOriginalImage = New System.Drawing.Bitmap(originalRectangle.Width, originalRectangle.Height, ipixelFormat)
                For x As Integer = 0 To objOriginalImage.Width - 1
                    For y As Integer = 0 To objOriginalImage.Height - 1
                        objOriginalImage.SetPixel(x, y, OriginalBitmap.GetPixel(x, y))
                    Next
                Next
            End Try
            Try
                If ipixelFormat = PixelFormat.Format32bppArgb Then
                    Dim q As New nQuant.WuQuantizer
                    Using m As New MemoryStream
                        Dim objImageOriginalClone As System.Drawing.Bitmap = OriginalBitmap.Clone
                        If Not imageMask Is Nothing Then
                            imageMask.MakeTransparent(Color.Black)
                            For x As Integer = 0 To objImageOriginalClone.Width - 1
                                For y As Integer = 0 To objImageOriginalClone.Height - 1
                                    Dim c As Color = imageMask.GetPixel(x, y)
                                    If c.A = 0 And c.R = 0 And c.G = 0 And c.B = 0 Then
                                        objImageOriginalClone.SetPixel(x, y, Color.Transparent)
                                    ElseIf c.A < 128 Then
                                        objImageOriginalClone.SetPixel(x, y, Color.Transparent)
                                    End If
                                Next
                            Next
                        End If
                        Dim objOptImage As System.Drawing.Bitmap = objOriginalImage.Clone
                        If autoResizeImages OrElse (Not optRectangle.Width = originalRectangle.Width And Not optRectangle.Height = originalRectangle.Height) Then
                            Using oGraphic As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(objOptImage)
                                With oGraphic
                                    .SmoothingMode = SmoothingMode1
                                    .PixelOffsetMode = PixelOffsetMode.None
                                    .CompositingQuality = CompositingQuality1
                                    .InterpolationMode = InterpolationMode1
                                    .CompositingMode = CompositingMode.SourceCopy
                                    .DrawImage(objOriginalImage.Clone, originalRectangle, originalRectangle, GraphicsUnit.Pixel)
                                    oGraphic.Dispose()
                                End With
                            End Using
                            objOptImage = New System.Drawing.Bitmap(objOptImage.Clone, optRectangle.Width, optRectangle.Height)
                            objOptImage.Save(m, format)
                            Return m.ToArray
                        Else
                            objOptImage.Save(m, format)
                            Return m.ToArray
                        End If
                        objOptImage.Save(m, format)
                        Return m.ToArray
                    End Using
                End If
            Catch exPNG As Exception
                Err.Clear()
            End Try
            Try
                Using oGraphic As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(objOriginalImage)
                    With oGraphic
                        .SmoothingMode = SmoothingMode.AntiAlias
                        .PixelOffsetMode = PixelOffsetMode.None
                        .CompositingQuality = CompositingQuality.HighQuality
                        .InterpolationMode = InterpolationMode.HighQualityBicubic
                        .CompositingMode = CompositingMode.SourceCopy

                        If Not imageMask Is Nothing Then
                            .DrawImage(imageMask, originalRectangle, originalRectangle, GraphicsUnit.Pixel)
                            .DrawImage(objOriginalImage, originalRectangle, originalRectangle, GraphicsUnit.Pixel)
                        Else
                        End If
                        oGraphic.Dispose()
                    End With
                End Using
            Catch ex As Exception
                Err.Clear()
                objOriginalImage = New System.Drawing.Bitmap(originalRectangle.Width, originalRectangle.Height, ipixelFormat)
                For x As Integer = 0 To objOriginalImage.Width - 1
                    For y As Integer = 0 To objOriginalImage.Height - 1
                        objOriginalImage.SetPixel(x, y, OriginalBitmap.GetPixel(x, y))
                    Next
                Next
            End Try
            Using m As New MemoryStream
                Try
                    If Not imageMask Is Nothing Then
                        Dim objOriginalImageTemp As System.Drawing.Bitmap = objOriginalImage.Clone
                        For x As Long = 0 To objOriginalImage.Width - 1
                            For y As Long = 0 To objOriginalImage.Height - 1
                                Dim c As System.Drawing.Color = imageMask.GetPixel(x, y)
                                If (c.R = 0 And c.G = 0 And c.B = 0) Or c.A < 128 Then
                                    objOriginalImage.SetPixel(x, y, Color.Transparent)
                                Else
                                    If AveragePixelColor > 0 Then
                                        c = getAveragePixelColor_Shared(objOriginalImageTemp.Clone(), x, y, AveragePixelColor)
                                    Else
                                        c = objOriginalImageTemp.GetPixel(x, y)
                                    End If
                                    objOriginalImage.SetPixel(x, y, c)
                                End If
                            Next
                        Next
                    End If
                    Dim objOptImage As System.Drawing.Bitmap = objOriginalImage.Clone
                    Using oGraphic As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(objOptImage)
                        With oGraphic
                            .SmoothingMode = SmoothingMode1
                            .PixelOffsetMode = PixelOffsetMode.None
                            .CompositingQuality = CompositingQuality1
                            .InterpolationMode = InterpolationMode1
                            .CompositingMode = CompositingMode.SourceCopy
                            .DrawImage(objOriginalImage, originalRectangle, originalRectangle, GraphicsUnit.Pixel)
                            oGraphic.Dispose()
                        End With
                    End Using
                    If autoResizeImages OrElse (Not optRectangle.Width = originalRectangle.Width And Not optRectangle.Height = originalRectangle.Height) Then
                        objOptImage = New System.Drawing.Bitmap(objOptImage.Clone, optRectangle.Width, optRectangle.Height)
                        objOptImage.Save(m, format)
                    Else
                        objOriginalImage.Save(m, format)
                    End If
                    If m.CanSeek Then
                        m.Seek(0, SeekOrigin.Begin)
                    End If
                    Return m.ToArray
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function
    Public Shared Function optimizeBitmapShared(ByVal OriginalBitmap As System.Drawing.Bitmap, ByVal newWidth As Integer, ByVal newHeight As Integer, ByVal format As System.Drawing.Imaging.ImageFormat, Optional ByVal InterpolationMode1 As Drawing2D.InterpolationMode = InterpolationMode.HighQualityBicubic, Optional ByVal SmoothingMode1 As SmoothingMode = SmoothingMode.HighQuality, Optional ByVal CompositingQuality1 As CompositingQuality = CompositingQuality.HighQuality, Optional ByVal AveragePixelColor As Integer = -1, Optional ByVal imageMask As System.Drawing.Bitmap = Nothing, Optional ByVal autoResizeImages As Boolean = False) As Byte()
        Try
            Dim iwidth As Integer = OriginalBitmap.Width
            Dim iheight As Integer = OriginalBitmap.Height
            Dim ipixelFormat As System.Drawing.Imaging.PixelFormat = OriginalBitmap.PixelFormat
            Dim isAlpha As Boolean = System.Drawing.Bitmap.IsAlphaPixelFormat(OriginalBitmap.PixelFormat)
            If Not imageMask Is Nothing Then
                ipixelFormat = PixelFormat.Format32bppArgb
                format = System.Drawing.Imaging.ImageFormat.Png
            ElseIf isAlpha Then
                ipixelFormat = PixelFormat.Format32bppArgb
                format = System.Drawing.Imaging.ImageFormat.Png
            End If
            Dim iImageFormat As System.Drawing.Imaging.ImageFormat = format
            Dim optRectangle As New System.Drawing.Rectangle(0, 0, newWidth, newHeight)
            Dim originalRectangle As New System.Drawing.Rectangle(0, 0, iwidth, iheight)
            Dim tempStream As New MemoryStream
            OriginalBitmap.Save(tempStream, System.Drawing.Imaging.ImageFormat.Png)
            Using objOriginalImage As System.Drawing.Bitmap = System.Drawing.Bitmap.FromStream(tempStream)
                Using m As New MemoryStream
                    Try
                        Using oGraphic As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(objOriginalImage)
                            With oGraphic
                                .SmoothingMode = SmoothingMode.AntiAlias
                                .PixelOffsetMode = PixelOffsetMode.None
                                .CompositingQuality = CompositingQuality.HighQuality
                                .InterpolationMode = InterpolationMode.HighQualityBicubic
                                .CompositingMode = CompositingMode.SourceCopy
                                If Not imageMask Is Nothing Then
                                    .DrawImage(imageMask, originalRectangle, originalRectangle, GraphicsUnit.Pixel)
                                    .DrawImage(objOriginalImage, originalRectangle, originalRectangle, GraphicsUnit.Pixel)
                                Else
                                End If
                            End With
                        End Using
                        If Not imageMask Is Nothing Then
                            Dim objOriginalImageTemp As System.Drawing.Bitmap = objOriginalImage.Clone
                            For x As Long = 0 To objOriginalImage.Width - 1
                                For y As Long = 0 To objOriginalImage.Height - 1
                                    Dim c As System.Drawing.Color = imageMask.GetPixel(x, y)
                                    If (c.R = 0 And c.G = 0 And c.B = 0) Or c.A < 128 Then
                                        objOriginalImage.SetPixel(x, y, Color.Transparent)
                                    Else
                                        If AveragePixelColor > 0 Then
                                            c = getAveragePixelColor_Shared(objOriginalImageTemp.Clone(), x, y, AveragePixelColor)
                                        Else
                                            c = objOriginalImageTemp.GetPixel(x, y)
                                        End If
                                        objOriginalImage.SetPixel(x, y, c)
                                    End If
                                Next
                            Next
                        End If
                        If autoResizeImages And Not optRectangle.Width = originalRectangle.Width And Not optRectangle.Height = originalRectangle.Height Then
                            Dim objOptImage As New System.Drawing.Bitmap(objOriginalImage.Clone, optRectangle.Width, optRectangle.Height)
                            objOptImage.Save(m, format)
                        Else
                            objOriginalImage.Save(m, format)
                        End If
                        If m.CanSeek Then
                            m.Seek(0, SeekOrigin.Begin)
                        End If
                        Return m.ToArray
                    Catch ex As Exception
                        Throw ex
                    End Try
                    Return m.ToArray
                End Using
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function
    Public cancelOptimize As Boolean = False
    Public imgList As MyImageRenderListener = Nothing
    Public Sub LoadImageList(ByVal pdfbytes As Byte(), ByVal pdfownerPassword() As Byte)
        If Not imgList Is Nothing Then
            If imgList.Pages.Count >= 0 Then
                Return
            End If
        End If
        Dim reader As New PdfReader(pdfbytes, pdfownerPassword)
        Try
            Dim reader2 As PdfReader = reader.Clone()
            imgList = New MyImageRenderListener
            Dim proc As New iTextSharp.text.pdf.parser.PdfReaderContentParser(reader2)
            For pgNumber2 As Integer = 1 To reader2.NumberOfPages
                If cancelOptimize Or cancelOptimize_Shared Then
                    Return
                Else

                End If
                Dim page As PdfDictionary = reader2.GetPageN(pgNumber2)
                Dim pageUnits = (If(page.Contains(PdfName.USERUNIT), page.GetAsNumber(PdfName.USERUNIT).FloatValue, 72))
                imgList.CurrentPage = pgNumber2
                imgList.CurrentPageUnits = pageUnits
                If cancelOptimize Or cancelOptimize_Shared Then
                    Return
                Else

                End If
                proc.ProcessContent(pgNumber2, imgList)
            Next
        Catch exProcess As Exception
            Err.Clear()
        End Try
    End Sub
    Public Sub LoadImageList(ByVal pdfbytes As Byte(), ByVal pdfownerPassword() As Byte, ByVal pgNumber2 As Integer)
        Dim reader As New PdfReader(pdfbytes, pdfownerPassword)
        Try
            If Not imgList Is Nothing Then
                If imgList.Pages.Count >= 0 Then
                    Return
                End If
            End If
            Dim reader2 As PdfReader = reader.Clone()
            imgList = New MyImageRenderListener
            Dim proc As New iTextSharp.text.pdf.parser.PdfReaderContentParser(reader2)
            If pgNumber2 <= reader2.NumberOfPages And pgNumber2 > 0 Then
                If cancelOptimize Or cancelOptimize_Shared Then
                    Return
                Else

                End If
                Dim page As PdfDictionary = reader2.GetPageN(pgNumber2)
                Dim pageUnits = (If(page.Contains(PdfName.USERUNIT), page.GetAsNumber(PdfName.USERUNIT).FloatValue, 72))
                imgList.CurrentPage = pgNumber2
                imgList.CurrentPageUnits = pageUnits
                If cancelOptimize Or cancelOptimize_Shared Then
                    Return
                Else

                End If
                proc.ProcessContent(pgNumber2, imgList)
            End If
        Catch exProcess As Exception
            Err.Clear()
        End Try
    End Sub
    Public Class ScaleEvent
        Inherits PdfPageEventHelper
        Protected scale As Single = 1.0F
        Protected pageDict As PdfDictionary

        Public Sub New(scaleSingle As Single)
            scale = scaleSingle
        End Sub

        Public Sub setPageDict(pageDictPdfDict As PdfDictionary)
            pageDict = pageDictPdfDict
        End Sub
        Public Overrides Sub onStartPage(writer As PdfWriter, doc As Document)
            writer.AddPageDictEntry(PdfName.ROTATE, pageDict.GetAsNumber(PdfName.ROTATE))
            writer.AddPageDictEntry(PdfName.MEDIABOX, scaleDown(pageDict.GetAsArray(PdfName.MEDIABOX), scale))
            writer.AddPageDictEntry(PdfName.CROPBOX, scaleDown(pageDict.GetAsArray(PdfName.CROPBOX), scale))
        End Sub
        Public Function scaleDown(original As PdfArray, scale As Single) As PdfArray
            If (original Is Nothing) Then
                Return Nothing
            End If
            Dim width As Single = original.GetAsNumber(2).FloatValue() - original.GetAsNumber(0).FloatValue()
            Dim height As Single = original.GetAsNumber(3).FloatValue() - original.GetAsNumber(1).FloatValue()
            Return New PdfRectangle(width * scale, height * scale)
        End Function
    End Class

    Public Function scalePDF(ByRef reader As PdfReader, Optional scale As Single = 1) As PdfReader
        If scale = 1 Then
            Return reader.Clone
        End If
        Dim ScaleEvent1 As ScaleEvent = New ScaleEvent(scale)
        ScaleEvent1.setPageDict(reader.GetPageN(1))
        Dim n As Integer = reader.NumberOfPages()
        Dim Doc As New Document()
        Dim m As New MemoryStream
        Dim writer As PdfWriter = PdfWriter.GetInstance(Doc, m)
        Doc.Open()
        Dim cb As PdfContentByte = writer.DirectContent
        For pageNumber As Integer = 1 To reader.NumberOfPages
            Dim page As PdfImportedPage = writer.GetImportedPage(reader, pageNumber)
            cb.AddTemplate(page, reader.GetPageSize(pageNumber).Width * scale, 0, 0, reader.GetPageSize(pageNumber).Height * scale, 0, 0)
            Doc.NewPage()
        Next
        Doc.Close()
        Doc = Nothing
        Return New PdfReader(m.ToArray)
    End Function


    Public Function Optimize_Images(ByRef pdfBytes() As Byte, ByVal pdfOwnerPassword() As Byte, Optional ByVal scaleFactor As Single = 1.0F, Optional ByVal CompressionLevel As Integer = 100, Optional ByVal InterpolationMode1 As Drawing2D.InterpolationMode = InterpolationMode.HighQualityBicubic, Optional ByVal SmoothingMode1 As SmoothingMode = SmoothingMode.HighQuality, Optional ByVal CompositingQuality1 As CompositingQuality = CompositingQuality.HighQuality, Optional ByRef ProgressBarX As ProgressBar = Nothing, Optional ByVal pageScale As Single = 1.0F, Optional ByVal imageAveragePixel As Integer = -1, Optional ByVal autoResizeImages As Boolean = False, Optional ByVal optimizeJPXDECODE As Boolean = False, Optional optimizeSMaskImages As Boolean = False) As Byte()
        Try
            If autoResizeImages Then
                LoadImageList(pdfBytes, pdfOwnerPassword)
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
        Dim scaleFactorTemp As Single = scaleFactor
        Dim reader As New PdfReader(pdfBytes, pdfOwnerPassword)
        Try
            If Not pageScale = 1.0F Then
                reader = scalePDF(reader.Clone, pageScale)
                pdfBytes = frmMain.getPDFBytes(reader.Clone, False)
                pdfBytes = frmMain.EncryptPDFDocument(pdfBytes)
                reader = New PdfReader(pdfBytes, frmMain.getBytes(frmMain.pdfOwnerPassword))
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
        Dim pgNumber As Integer = 0
        Try
            Using fs As New MemoryStream
                Using stamper As New PdfStamper(reader, fs)
                    For pgNumber = 1 To reader.NumberOfPages
                        If cancelOptimize Or cancelOptimize_Shared Then
                            Return Nothing
                        Else

                        End If
                        If pgNumber = 16 Then
                            pgNumber = pgNumber
                        End If
                        If Not ProgressBarX Is Nothing Then
                            If ProgressBarX.GetType Is GetType(ProgressBar) Then
                                If Not ProgressBarX.Enabled Then ProgressBarX.Enabled = True
                                If Not ProgressBarX.Visible Then ProgressBarX.Visible = True
                                ProgressBarX.Value = CInt((pgNumber / reader.NumberOfPages) * 100)
                                Application.DoEvents()
                            End If
                        End If
                        If pgNumber = 10 Then
                        End If
                        Dim page As PdfDictionary = reader.GetPageN(pgNumber)
                        Dim resources As PdfDictionary = DirectCast(PdfReader.GetPdfObject(page.Get(PdfName.RESOURCES)), PdfDictionary)
                        Dim xobject As PdfDictionary = DirectCast(PdfReader.GetPdfObject(resources.Get(PdfName.XOBJECT)), PdfDictionary)
GOTO_Restart_XOBJECT:
                        If Not xobject Is Nothing Then
                            Dim obj As PdfObject
                            For Each name As PdfName In xobject.Keys
                                If cancelOptimize Or cancelOptimize_Shared Then
                                    Return Nothing
                                Else

                                End If
                                obj = xobject.Get(name)
                                scaleFactorTemp = scaleFactor
                                If obj.IsIndirect() Then
                                    obj = xobject.GetAsIndirectObject(name)
                                    Dim imgObject As PdfDictionary = Nothing
                                    Try
                                        If PdfReader.GetPdfObject(obj).IsStream Then
                                            imgObject = DirectCast(PdfReader.GetPdfObject(obj), PdfDictionary)
                                        ElseIf PdfReader.GetPdfObject(obj).IsDictionary Then
                                            imgObject = DirectCast(PdfReader.GetPdfObject(obj), PdfDictionary)
                                        Else
                                            imgObject = DirectCast(PdfReader.GetPdfObject(obj), PdfDictionary)
                                        End If
                                    Catch ex As Exception
                                        Err.Clear()
                                    End Try
                                    If Not imgObject Is Nothing Then
                                        Dim subtype As PdfName = imgObject.GetAsName(PdfName.SUBTYPE)
                                        Try
                                            If imgObject.Get(PdfName.SUBTYPE).Equals(PdfName.FORM) Then
                                                resources = DirectCast(imgObject.GetAsDict(PdfName.RESOURCES), PdfDictionary)
                                                If Not resources Is Nothing Then
                                                    If Not resources.Get(PdfName.XOBJECT) Is Nothing Then
                                                        xobject = DirectCast(resources.GetAsDict(PdfName.XOBJECT), PdfDictionary)
                                                        GoTo GOTO_Restart_XOBJECT
                                                    End If
                                                End If
                                            ElseIf imgObject.Get(PdfName.SUBTYPE).Equals(PdfName.IMAGE) Then
                                                Dim filter As String = imgObject.GetAsName(PdfName.FILTER).ToString
                                                If Not optimizeSMaskImages And Not imgObject.Get(PdfName.SMASK) Is Nothing Then
                                                    GoTo Goto_NextImage
                                                End If
                                                If imgObject.GetDirectObject(PdfName.COLORSPACE).IsArray Then
                                                    Dim sColorSpace As PdfArray = imgObject.GetDirectObject(PdfName.COLORSPACE)
                                                    If Not optimizeSMaskImages And sColorSpace.Contains(PdfName.INDEXED) And sColorSpace.Contains(PdfName.DEVICEGRAY) Then
                                                        GoTo Goto_NextImage
                                                    End If
                                                End If
                                                If imgObject.Get(PdfName.FILTER).Equals(PdfName.FLATEDECODE) Then
                                                    Dim newImageStream As New MemoryStream
                                                    If Not imgObject.Get(PdfName.SMASK) Is Nothing Then

                                                        Try
                                                            Dim stream1 As PRStream = DirectCast(xobject.GetAsStream(name), PRStream)
                                                            Dim imgObj1 As New iTextSharp.text.pdf.parser.PdfImageObject(stream1)
                                                            Dim oldBytes() As Byte = imgObj1.GetImageAsBytes()

                                                            Dim imageDictionary As PdfDictionary = imgObj1.GetDictionary()

                                                            Dim w As Single = imgObj1.GetDrawingImage.Width
                                                            Dim h As Single = imgObj1.GetDrawingImage.Height
                                                            Using sourceMS As New MemoryStream(oldBytes)
                                                                If sourceMS.CanSeek Then
                                                                    sourceMS.Seek(0, SeekOrigin.Begin)
                                                                End If
                                                                Try
                                                                    Using oldImageBitmap As System.Drawing.Bitmap = DirectCast(System.Drawing.Bitmap.FromStream(sourceMS, True, True), System.Drawing.Bitmap)
                                                                        Dim newSize As Size = Nothing
                                                                        Try
                                                                            Dim imgScale As Single = scaleFactor
                                                                            If autoResizeImages Then
                                                                                Dim listImages As System.Collections.Generic.List(Of ImageScaleInfo) = imgList.Pages(pgNumber)
                                                                                For Each img As ImageScaleInfo In listImages
                                                                                    If frmMain.bytesMatch(img.ImageBytes, oldBytes) Then
                                                                                        newSize = New Size(CInt(img.CtmSize.Width), CInt(img.CtmSize.Height))
                                                                                        If newSize.Width <= 0 And newSize.Height <= 0 Then
                                                                                            newSize = New Size(CInt(img.ImgSize.Width), CInt(img.ImgSize.Height))
                                                                                        Else
                                                                                            If oldImageBitmap.Size.Width <> CInt(newSize.Width * scaleFactor) And oldImageBitmap.Size.Height <> CInt(newSize.Height * scaleFactor) Then
                                                                                                Dim ratioWH As Single = (oldImageBitmap.Height / oldImageBitmap.Width)
                                                                                                newSize = New Size(CInt(newSize.Width * scaleFactor), CInt(CInt(newSize.Width * ratioWH) * scaleFactor))
                                                                                                Exit For
                                                                                            End If
                                                                                        End If
                                                                                    End If
                                                                                Next
                                                                            Else
                                                                                newSize = New Size(CInt(oldImageBitmap.Width * scaleFactor), CInt(oldImageBitmap.Height * scaleFactor))
                                                                            End If
                                                                        Catch exGetWH As Exception
                                                                            Dim imgScale As Single = scaleFactor
                                                                            newSize = New Size(CInt(oldImageBitmap.Width * scaleFactor), CInt(oldImageBitmap.Height * scaleFactor))
                                                                            Err.Clear()
                                                                        End Try
                                                                        Dim maskStream As PRStream = CType(imageDictionary.GetAsStream(PdfName.SMASK), PRStream)
                                                                        Dim imageMask As System.Drawing.Image = Nothing
                                                                        Dim maskImage As PdfImageObject = Nothing
                                                                        Dim maskImageItext As iTextSharp.text.Image = Nothing
                                                                        If (Not (maskStream) Is Nothing) Then
                                                                            maskImage = New PdfImageObject(maskStream)
                                                                            imageMask = System.Drawing.Image.FromStream(New MemoryStream(maskImage.GetImageAsBytes()), False, False)
                                                                            maskImageItext = iTextSharp.text.Image.GetInstance(imageMask, System.Drawing.Imaging.ImageFormat.Png)
                                                                            maskImageItext.MakeMask()
                                                                        End If
                                                                        newImageStream = New MemoryStream(optimizeBitmap(DirectCast(oldImageBitmap.Clone(), System.Drawing.Bitmap), newSize.Width, newSize.Height, System.Drawing.Imaging.ImageFormat.Png, InterpolationMode1, SmoothingMode1, CompositingQuality1, imageAveragePixel, Nothing, autoResizeImages))
                                                                        If Not imageMask Is Nothing Then
                                                                            If imageDictionary.Get(PdfName.SMASK).IsIndirect Then
                                                                                PdfReader.KillIndirect(imageDictionary.GetAsIndirectObject(PdfName.SMASK))
                                                                            End If
                                                                            imageDictionary.Remove(PdfName.SMASK)
                                                                        End If
                                                                        If Not newImageStream Is Nothing Then
                                                                            If newImageStream.Length > 0 Then
                                                                                If newImageStream.CanSeek Then
                                                                                    newImageStream.Seek(0, SeekOrigin.Begin)
                                                                                End If
                                                                                Dim img As System.Drawing.Image = System.Drawing.Image.FromStream(newImageStream)
                                                                                Dim compressedImage As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(img, System.Drawing.Imaging.ImageFormat.Png)
                                                                                If Not imageMask Is Nothing Then
                                                                                    compressedImage.ImageMask = maskImageItext
                                                                                End If
                                                                                If Not compressedImage Is Nothing Then
                                                                                    PdfReader.KillIndirect(obj)
                                                                                    stamper.Writer.AddDirectImageSimple(compressedImage, DirectCast(obj, PRIndirectReference))
                                                                                End If
                                                                            Else
                                                                                Dim stopme As Boolean = False
                                                                            End If
                                                                        End If
                                                                    End Using
                                                                Catch ex As Exception
                                                                    Err.Clear()
                                                                End Try
                                                            End Using
                                                        Catch exOptimize As Exception
                                                            Err.Clear()
                                                        End Try
                                                    Else
                                                        Try
                                                            Dim stream1 As PRStream = DirectCast(xobject.GetAsStream(name), PRStream)
                                                            Dim imgObj1 As New iTextSharp.text.pdf.parser.PdfImageObject(stream1)
                                                            Dim oldBytes() As Byte = imgObj1.GetImageAsBytes()
                                                            Dim w As Single = imgObj1.GetDrawingImage.Width
                                                            Dim h As Single = imgObj1.GetDrawingImage.Height
                                                            Using sourceMS As New MemoryStream(oldBytes)
                                                                If sourceMS.CanSeek Then
                                                                    sourceMS.Seek(0, SeekOrigin.Begin)
                                                                End If
                                                                Try
                                                                    Using oldImageBitmap As System.Drawing.Bitmap = DirectCast(System.Drawing.Bitmap.FromStream(sourceMS, True, True), System.Drawing.Bitmap)
                                                                        Dim newSize As Size = Nothing
                                                                        Try
                                                                            Dim imgScale As Single = scaleFactor
                                                                            If autoResizeImages Then
                                                                                Dim listImages As System.Collections.Generic.List(Of ImageScaleInfo) = imgList.Pages(pgNumber)
                                                                                For Each img As ImageScaleInfo In listImages
                                                                                    If frmMain.bytesMatch(img.ImageBytes, oldBytes) Then
                                                                                        newSize = New Size(CInt(img.CtmSize.Width), CInt(img.CtmSize.Height))
                                                                                        If newSize.Width <= 0 And newSize.Height <= 0 Then
                                                                                            newSize = New Size(CInt(img.ImgSize.Width), CInt(img.ImgSize.Height))
                                                                                        Else
                                                                                            If oldImageBitmap.Size.Width <> CInt(newSize.Width * scaleFactor) And oldImageBitmap.Size.Height <> CInt(newSize.Height * scaleFactor) Then
                                                                                                Dim ratioWH As Single = (oldImageBitmap.Height / oldImageBitmap.Width)
                                                                                                newSize = New Size(CInt(newSize.Width * scaleFactor), CInt(CInt(newSize.Width * ratioWH) * scaleFactor))
                                                                                                Exit For
                                                                                            End If
                                                                                        End If
                                                                                    End If
                                                                                Next
                                                                            Else
                                                                                newSize = New Size(CInt(oldImageBitmap.Width * scaleFactor), CInt(oldImageBitmap.Height * scaleFactor))
                                                                            End If
                                                                        Catch exGetWH As Exception
                                                                            Dim imgScale As Single = scaleFactor
                                                                            newSize = New Size(CInt(oldImageBitmap.Width * scaleFactor), CInt(oldImageBitmap.Height * scaleFactor))
                                                                            Err.Clear()
                                                                        End Try
                                                                        newImageStream = New MemoryStream(optimizeBitmap(DirectCast(oldImageBitmap.Clone(), System.Drawing.Bitmap), newSize.Width, newSize.Height, System.Drawing.Imaging.ImageFormat.Bmp, InterpolationMode1, SmoothingMode1, CompositingQuality1, imageAveragePixel, Nothing, autoResizeImages))
                                                                        If Not newImageStream Is Nothing Then
                                                                            If newImageStream.Length > 0 Then
                                                                                If newImageStream.CanSeek Then
                                                                                    newImageStream.Seek(0, SeekOrigin.Begin)
                                                                                End If
                                                                                Dim compressedImage As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(newImageStream)

                                                                                If Not compressedImage Is Nothing Then
                                                                                    PdfReader.KillIndirect(obj)
                                                                                    stamper.Writer.AddDirectImageSimple(compressedImage, DirectCast(obj, PRIndirectReference))
                                                                                End If
                                                                            Else
                                                                                Dim stopme As Boolean = False
                                                                            End If
                                                                        End If
                                                                    End Using
                                                                Catch ex As Exception
                                                                    Err.Clear()
                                                                End Try
                                                            End Using
                                                        Catch exOptimize As Exception
                                                            Err.Clear()
                                                        End Try
                                                    End If
                                                ElseIf imgObject.Get(PdfName.FILTER).Equals(PdfName.DCTDECODE) Then
                                                    Dim newImageStream As New MemoryStream
                                                    If Not imgObject.Get(PdfName.SMASK) Is Nothing Then
                                                        Try
                                                            Dim stream1 As PRStream = DirectCast(xobject.GetAsStream(name), PRStream)
                                                            Dim imgObj1 As New iTextSharp.text.pdf.parser.PdfImageObject(stream1)
                                                            Dim oldBytes() As Byte = imgObj1.GetImageAsBytes()

                                                            Dim imageDictionary As PdfDictionary = imgObj1.GetDictionary()

                                                            Dim w As Single = imgObj1.GetDrawingImage.Width
                                                            Dim h As Single = imgObj1.GetDrawingImage.Height
                                                            Using sourceMS As New MemoryStream(oldBytes)
                                                                If sourceMS.CanSeek Then
                                                                    sourceMS.Seek(0, SeekOrigin.Begin)
                                                                End If
                                                                Try
                                                                    Using oldImageBitmap As System.Drawing.Bitmap = DirectCast(System.Drawing.Bitmap.FromStream(sourceMS, False, False), System.Drawing.Bitmap)
                                                                        Dim newSize As Size = Nothing
                                                                        Try
                                                                            Dim imgScale As Single = scaleFactor
                                                                            If autoResizeImages Then
                                                                                Dim listImages As System.Collections.Generic.List(Of ImageScaleInfo) = imgList.Pages(pgNumber)
                                                                                For Each img As ImageScaleInfo In listImages
                                                                                    If frmMain.bytesMatch(img.ImageBytes, oldBytes) Then
                                                                                        newSize = New Size(CInt(img.CtmSize.Width), CInt(img.CtmSize.Height))
                                                                                        If newSize.Width <= 0 And newSize.Height <= 0 Then
                                                                                            newSize = New Size(CInt(img.ImgSize.Width), CInt(img.ImgSize.Height))
                                                                                        Else
                                                                                            If oldImageBitmap.Size.Width <> CInt(newSize.Width * scaleFactor) And oldImageBitmap.Size.Height <> CInt(newSize.Height * scaleFactor) Then
                                                                                                Dim ratioWH As Single = (oldImageBitmap.Height / oldImageBitmap.Width)
                                                                                                newSize = New Size(CInt(newSize.Width * scaleFactor), CInt(CInt(newSize.Width * ratioWH) * scaleFactor))
                                                                                                Exit For
                                                                                            End If
                                                                                        End If
                                                                                    End If
                                                                                Next
                                                                            Else
                                                                                newSize = New Size(CInt(oldImageBitmap.Width * scaleFactor), CInt(oldImageBitmap.Height * scaleFactor))
                                                                            End If
                                                                        Catch exGetWH As Exception
                                                                            Dim imgScale As Single = scaleFactor
                                                                            newSize = New Size(CInt(oldImageBitmap.Width * scaleFactor), CInt(oldImageBitmap.Height * scaleFactor))
                                                                            Err.Clear()
                                                                        End Try
                                                                        Dim maskStream As PRStream = CType(imageDictionary.GetAsStream(PdfName.SMASK), PRStream)
                                                                        Dim imageMask As System.Drawing.Image = Nothing
                                                                        Dim imageMaskBitmap As System.Drawing.Bitmap = Nothing
                                                                        Dim imageMaskBytes() As Byte = Nothing
                                                                        Dim imageMaskStream As New System.IO.MemoryStream
                                                                        Dim maskImage As PdfImageObject = Nothing
                                                                        Dim maskImageItext As iTextSharp.text.Image = Nothing
                                                                        If (Not (maskStream) Is Nothing) Then
                                                                            maskImage = New PdfImageObject(maskStream)
                                                                            Try
                                                                                imageMask = System.Drawing.Image.FromStream(New MemoryStream(maskImage.GetImageAsBytes()), False, False)
                                                                            Catch ex As Exception
                                                                                Err.Clear()
                                                                                Try
                                                                                    If maskStream.Keys.Contains(PdfName.FILTER) Then
                                                                                        If maskStream.GetAsName(PdfName.SUBTYPE) Is PdfName.IMAGE Then
                                                                                            If maskStream.GetAsName(PdfName.FILTER) Is PdfName.JPXDECODE Then
                                                                                                If Not optimizeJPXDECODE Then
                                                                                                    GoTo SKIP_AHEAD
                                                                                                End If
                                                                                                Dim mMask As New MemoryStream(maskImage.GetImageAsBytes())
                                                                                                Dim dib As FIBITMAP = FreeImage.LoadFromStream(mMask)
                                                                                                If Not dib.IsNull Then
                                                                                                    imageMaskBitmap = FreeImage.GetBitmap(dib)
                                                                                                    imageMaskStream = Nothing

                                                                                                    Dim img As System.Drawing.Bitmap = oldImageBitmap.Clone
                                                                                                    Using imgTemp As System.Drawing.Bitmap = New System.Drawing.Bitmap(img.Width, img.Height, PixelFormat.Format32bppArgb)
                                                                                                        For x As Integer = 0 To imageMaskBitmap.Width - 1
                                                                                                            For y As Integer = 0 To imageMaskBitmap.Height - 1
                                                                                                                Dim pAvg As System.Drawing.Color = getAveragePixelColor_Shared(imageMaskBitmap.Clone, x, y, 1)
                                                                                                                Dim p As System.Drawing.Color = imageMaskBitmap.GetPixel(x, y)
                                                                                                                Dim a As Integer = p.A
                                                                                                                Dim r As Integer = p.R
                                                                                                                Dim g As Integer = p.G
                                                                                                                Dim b As Integer = p.B
                                                                                                                Dim avg As Integer = (r + g + b) / 3

                                                                                                                If avg >= 128 Then
                                                                                                                    Dim c1 As System.Drawing.Color = img.GetPixel(x, y)
                                                                                                                    c1 = System.Drawing.Color.FromArgb(pAvg.A, c1.R, c1.G, c1.B)
                                                                                                                    imgTemp.SetPixel(x, y, c1)
                                                                                                                Else
                                                                                                                    imgTemp.SetPixel(x, y, Color.Transparent)
                                                                                                                End If
                                                                                                                If cancelOptimize Or cancelOptimize_Shared Then
                                                                                                                    Return Nothing
                                                                                                                Else

                                                                                                                End If
                                                                                                            Next y
                                                                                                        Next x
                                                                                                        With imgTemp
                                                                                                            For x As Integer = 0 To .Width - 1
                                                                                                                For y As Integer = 0 To .Height - 1
                                                                                                                    Dim pAvg As System.Drawing.Color = getAveragePixelColor_Shared(.Clone, x, y, 1)
                                                                                                                    Dim p As System.Drawing.Color = .GetPixel(x, y)
                                                                                                                    Dim a As Integer = p.A
                                                                                                                    Dim r As Integer = p.R
                                                                                                                    Dim g As Integer = p.G
                                                                                                                    Dim b As Integer = p.B
                                                                                                                    Dim avg As Integer = (r + g + b) / 3
                                                                                                                    If (pAvg.A >= 0 And pAvg.A < 255) Then
                                                                                                                        Dim c1 As System.Drawing.Color = .GetPixel(x, y)
                                                                                                                        c1 = System.Drawing.Color.FromArgb(pAvg.A, pAvg.R, pAvg.G, pAvg.B)
                                                                                                                        .SetPixel(x, y, c1)
                                                                                                                    End If
                                                                                                                    If cancelOptimize Or cancelOptimize_Shared Then
                                                                                                                        Return Nothing
                                                                                                                    Else

                                                                                                                    End If
                                                                                                                Next y
                                                                                                            Next x
                                                                                                            .Save(newImageStream, System.Drawing.Imaging.ImageFormat.Png)
                                                                                                        End With
                                                                                                    End Using
                                                                                                    imageMask = Nothing
                                                                                                    imageMaskBitmap = Nothing
                                                                                                    imageMaskBytes = Nothing
                                                                                                    imageMaskStream = Nothing
                                                                                                    maskImageItext = Nothing
                                                                                                    FreeImage.UnloadEx(dib)
                                                                                                End If
                                                                                            End If
                                                                                        End If
                                                                                    End If
                                                                                Catch ex2 As Exception
                                                                                    Err.Clear()
                                                                                End Try
                                                                            End Try
                                                                            If Not imageMaskStream Is Nothing Then
                                                                                If imageMaskStream.Length > 0 Then
                                                                                    If imageMaskStream.CanSeek Then
                                                                                        imageMaskStream.Seek(0, SeekOrigin.Begin)
                                                                                    End If
                                                                                    maskImageItext = iTextSharp.text.Image.GetInstance(imageMaskStream)
                                                                                    Try
                                                                                        If Not maskImageItext.IsMask And maskImageItext.IsMaskCandidate Then
                                                                                            maskImageItext.MakeMask()
                                                                                        End If
                                                                                    Catch exMask1 As Exception
                                                                                        Err.Clear()
                                                                                    End Try

                                                                                ElseIf Not imageMask Is Nothing Then
                                                                                    Try
                                                                                        maskImageItext = iTextSharp.text.Image.GetInstance(imageMask, Nothing, False)
                                                                                        maskImageItext.Normalize()
                                                                                        maskImageItext.SimplifyColorspace()
                                                                                        maskImageItext.Inverted = True
                                                                                        maskImageItext.Interpolation = True
                                                                                        If Not maskImageItext.IsMask And maskImageItext.IsMaskCandidate Then
                                                                                            maskImageItext.MakeMask()
                                                                                        End If
                                                                                    Catch exMask1 As Exception
                                                                                        Err.Clear()
                                                                                    End Try
                                                                                ElseIf Not imageMaskBytes Is Nothing Then
                                                                                    If imageMaskBytes.Length > 0 Then
                                                                                        maskImageItext = iTextSharp.text.Image.GetInstance(imageMaskBytes)
                                                                                        maskImageItext.MakeMask()
                                                                                    End If
                                                                                End If

                                                                            ElseIf Not imageMask Is Nothing Then
                                                                                Try
                                                                                    maskImageItext = iTextSharp.text.Image.GetInstance(imageMask, Nothing, False)
                                                                                    maskImageItext.Normalize()
                                                                                    maskImageItext.SimplifyColorspace()
                                                                                    maskImageItext.Inverted = True
                                                                                    maskImageItext.Interpolation = True
                                                                                    If Not maskImageItext.IsMask And maskImageItext.IsMaskCandidate Then
                                                                                        maskImageItext.MakeMask()
                                                                                    End If
                                                                                Catch exMask1 As Exception
                                                                                    Err.Clear()
                                                                                End Try
                                                                            ElseIf Not imageMaskBytes Is Nothing Then
                                                                                If imageMaskBytes.Length > 0 Then
                                                                                    maskImageItext = iTextSharp.text.Image.GetInstance(imageMaskBytes)
                                                                                    maskImageItext.MakeMask()
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Not maskImageItext Is Nothing Then
                                                                            newImageStream = New MemoryStream()
                                                                            DirectCast(oldImageBitmap.Clone(), System.Drawing.Bitmap).Save(newImageStream, System.Drawing.Imaging.ImageFormat.Jpeg)
                                                                        Else
                                                                            If newImageStream.Length <= 0 Then
                                                                                newImageStream = New MemoryStream(optimizeBitmap(DirectCast(oldImageBitmap.Clone(), System.Drawing.Bitmap), newSize.Width, newSize.Height, System.Drawing.Imaging.ImageFormat.Jpeg, InterpolationMode1, SmoothingMode1, CompositingQuality1, imageAveragePixel, Nothing, autoResizeImages))
                                                                            End If
                                                                        End If
                                                                        If Not imageMask Is Nothing Or Not imageMaskBytes Is Nothing Or Not maskImageItext Is Nothing Then
                                                                            If imageDictionary.Get(PdfName.SMASK).IsIndirect Then
                                                                                PdfReader.KillIndirect(imageDictionary.GetAsIndirectObject(PdfName.SMASK))
                                                                            End If
                                                                            imageDictionary.Remove(PdfName.SMASK)
                                                                        End If
                                                                        If Not newImageStream Is Nothing Then
                                                                            If newImageStream.Length > 0 Then
                                                                                If newImageStream.CanSeek Then
                                                                                    newImageStream.Seek(0, SeekOrigin.Begin)
                                                                                End If
                                                                                Dim img As System.Drawing.Image = System.Drawing.Image.FromStream(newImageStream)
                                                                                Dim compressedImage As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(img, System.Drawing.Imaging.ImageFormat.Png)
                                                                                If Not imageMask Is Nothing Or Not imageMaskBytes Is Nothing Then
                                                                                    compressedImage.ImageMask = maskImageItext

                                                                                End If
                                                                                If Not compressedImage Is Nothing Then
                                                                                    PdfReader.KillIndirect(obj)
                                                                                    stamper.Writer.AddDirectImageSimple(compressedImage, DirectCast(obj, PRIndirectReference))
                                                                                End If
                                                                            Else
                                                                                Dim stopme As Boolean = False
                                                                            End If
                                                                        ElseIf Not maskImageItext Is Nothing Then
                                                                            Dim img As System.Drawing.Image = System.Drawing.Image.FromStream(newImageStream)
                                                                            Dim compressedImage As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(img, System.Drawing.Imaging.ImageFormat.Png)
                                                                            If Not imageMask Is Nothing Or Not imageMaskBytes Is Nothing Then
                                                                                compressedImage.ImageMask = maskImageItext
                                                                            End If
                                                                            If Not compressedImage Is Nothing Then
                                                                                PdfReader.KillIndirect(obj)
                                                                                stamper.Writer.AddDirectImageSimple(compressedImage, DirectCast(obj, PRIndirectReference))
                                                                            End If
                                                                        End If
                                                                    End Using
                                                                Catch ex As Exception
                                                                    Err.Clear()
                                                                End Try
                                                            End Using
                                                        Catch exOptimize As Exception
                                                            Err.Clear()
                                                        End Try
SKIP_AHEAD:
                                                    Else
                                                        Try
                                                            Dim oldBytes As Byte() = PdfReader.GetStreamBytesRaw(DirectCast(imgObject, PRStream))
                                                            Using sourceMS As New MemoryStream(oldBytes)
                                                                If sourceMS.CanSeek Then
                                                                    sourceMS.Seek(0, SeekOrigin.Begin)
                                                                End If
                                                                Try
                                                                    Using oldImageBitmap As System.Drawing.Bitmap = DirectCast(System.Drawing.Bitmap.FromStream(sourceMS, True, True), System.Drawing.Bitmap)
                                                                        Dim newSize As Size = Nothing
                                                                        Try
                                                                            Dim imgScale As Single = scaleFactor
                                                                            If autoResizeImages Then
                                                                                Dim listImages As System.Collections.Generic.List(Of ImageScaleInfo) = imgList.Pages(pgNumber)
                                                                                For Each img As ImageScaleInfo In listImages
                                                                                    If frmMain.bytesMatch(img.ImageBytes, oldBytes) Then

                                                                                        newSize = New Size(CInt(img.CtmSize.Width), CInt(img.CtmSize.Height))
                                                                                        If newSize.Width <= 0 And newSize.Height <= 0 Then
                                                                                            newSize = New Size(CInt(img.ImgSize.Width), CInt(img.ImgSize.Height))
                                                                                        Else
                                                                                            If oldImageBitmap.Size.Width <> CInt(newSize.Width * scaleFactor) And oldImageBitmap.Size.Height <> CInt(newSize.Height * scaleFactor) Then
                                                                                                Dim ratioWH As Single = (oldImageBitmap.Height / oldImageBitmap.Width)
                                                                                                newSize = New Size(CInt(newSize.Width * scaleFactor), CInt(CInt(newSize.Width * ratioWH) * scaleFactor))
                                                                                                Exit For
                                                                                            End If
                                                                                        End If

                                                                                    End If
                                                                                Next
                                                                            Else
                                                                                newSize = New Size(CInt(oldImageBitmap.Width * scaleFactor), CInt(oldImageBitmap.Height * scaleFactor))
                                                                            End If
                                                                        Catch exGetWH As Exception
                                                                            Dim imgScale As Single = scaleFactor
                                                                            newSize = New Size(CInt(oldImageBitmap.Width * scaleFactor), CInt(oldImageBitmap.Height * scaleFactor))
                                                                            Err.Clear()
                                                                        End Try
                                                                        newImageStream = New MemoryStream(optimizeBitmap(DirectCast(oldImageBitmap.Clone(), System.Drawing.Bitmap), newSize.Width, newSize.Height, System.Drawing.Imaging.ImageFormat.Jpeg, InterpolationMode1, SmoothingMode1, CompositingQuality1, imageAveragePixel, Nothing, autoResizeImages))
                                                                        If Not newImageStream Is Nothing Then
                                                                            If newImageStream.Length > 0 Then
                                                                                If newImageStream.CanSeek Then
                                                                                    newImageStream.Seek(0, SeekOrigin.Begin)
                                                                                End If
                                                                                Dim compressedImage As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(newImageStream.ToArray())
                                                                                If Not compressedImage Is Nothing Then
                                                                                    PdfReader.KillIndirect(obj)
                                                                                    stamper.Writer.AddDirectImageSimple(compressedImage, DirectCast(obj, PRIndirectReference))
                                                                                End If
                                                                            End If
                                                                        Else
                                                                            Dim stopme As Boolean = False
                                                                        End If
                                                                    End Using
                                                                Catch ex As Exception
                                                                    Err.Clear()
                                                                End Try
                                                            End Using
                                                        Catch exJPEG As Exception
                                                            Err.Clear()
                                                        End Try
                                                    End If
                                                ElseIf Not imgObject.Get(PdfName.FILTER) Is Nothing Then
                                                    Try
                                                        Dim strFilterName As String = imgObject.Get(PdfName.FILTER).ToString()
                                                        strFilterName = strFilterName
                                                    Catch ex As Exception
                                                        Err.Clear()
                                                    End Try
                                                End If

                                            End If
                                        Catch ex As Exception
                                            Err.Clear()
                                        End Try
                                    Else
                                        Dim blnStopMe As Boolean = True
                                    End If
                                End If

Goto_NextImage:
                            Next
                        End If
                    Next
                    stamper.Writer.CloseStream = False
                    stamper.Close()
                    stamper.Dispose()
                    If Not pageScale = 1.0F Then
                        Return frmMain.EncryptPDFDocument(fs.ToArray(), Nothing)
                    End If
                    Return fs.ToArray
                End Using
            End Using
        Catch ex As Exception
            Err.Clear()
        Finally
            Try
                reader.Close()
                reader.Dispose()
                reader = Nothing
            Catch ex2 As Exception
                Err.Clear()
            End Try
        End Try
        Return pdfBytes
    End Function
    Public Function Replace_Image(ByRef pdfBytes() As Byte, ByVal pgNumber As Integer, ByVal pdfOwnerPassword() As Byte, ByVal OriginalImageRefNumber As Long, ByVal OriginalImageBytes() As Byte, ByVal OriginalImageFormat As System.Drawing.Imaging.ImageFormat, Optional ByVal scaleFactor As Single = 1.0F, Optional ByVal CompressionLevel As Integer = 100, Optional ByVal InterpolationMode1 As Drawing2D.InterpolationMode = InterpolationMode.HighQualityBicubic, Optional ByVal SmoothingMode1 As SmoothingMode = SmoothingMode.HighQuality, Optional ByVal CompositingQuality1 As CompositingQuality = CompositingQuality.HighQuality, Optional ByVal pageScale As Single = 1.0F, Optional ByVal imageAveragePixel As Integer = -1, Optional ByVal autoResizeImages As Boolean = False) As Byte()
        Dim reader As New PdfReader(pdfBytes, pdfOwnerPassword)
        Try
            Using fs As New MemoryStream
                Using stamper As New PdfStamper(reader, fs)
                    Dim page As PdfDictionary = reader.GetPageN(pgNumber)
                    Dim resources As PdfDictionary = DirectCast(PdfReader.GetPdfObject(page.Get(PdfName.RESOURCES)), PdfDictionary)
                    Dim xobject As PdfDictionary = DirectCast(PdfReader.GetPdfObject(resources.Get(PdfName.XOBJECT)), PdfDictionary)
GOTO_Restart_XOBJECT:
                    If Not xobject Is Nothing Then
                        Dim obj As PdfObject
                        For Each name As PdfName In xobject.Keys
                            obj = xobject.Get(name)
                            If obj.IsIndirect() Then
                                obj = xobject.GetAsIndirectObject(name)
                                If OriginalImageRefNumber = xobject.GetAsIndirectObject(name).Number Then
                                    Dim imgObject As PdfDictionary = Nothing
                                    Try
                                        If PdfReader.GetPdfObject(obj).IsStream Then
                                            imgObject = DirectCast(PdfReader.GetPdfObject(obj), PdfDictionary)
                                        ElseIf PdfReader.GetPdfObject(obj).IsDictionary Then
                                            imgObject = DirectCast(PdfReader.GetPdfObject(obj), PdfDictionary)
                                        Else
                                            imgObject = DirectCast(PdfReader.GetPdfObject(obj), PdfDictionary)
                                        End If
                                    Catch ex As Exception
                                        Err.Clear()
                                    End Try
                                    If Not imgObject Is Nothing Then
                                        Dim subtype As PdfName = imgObject.GetAsName(PdfName.SUBTYPE)
                                        Try
                                            If imgObject.Get(PdfName.SUBTYPE).Equals(PdfName.FORM) Then
                                                resources = DirectCast(imgObject.GetAsDict(PdfName.RESOURCES), PdfDictionary)
                                                If Not resources Is Nothing Then
                                                    If Not resources.Get(PdfName.XOBJECT) Is Nothing Then
                                                        xobject = DirectCast(resources.GetAsDict(PdfName.XOBJECT), PdfDictionary)
                                                        GoTo GOTO_Restart_XOBJECT
                                                    End If
                                                End If
                                            ElseIf imgObject.Get(PdfName.SUBTYPE).Equals(PdfName.IMAGE) Then
                                                Dim filter As String = imgObject.GetAsName(PdfName.FILTER).ToString
                                                If imgObject.Get(PdfName.FILTER).Equals(PdfName.FLATEDECODE) Then
                                                    Dim newImageStream As New MemoryStream
                                                    If Not imgObject.Get(PdfName.SMASK) Is Nothing Then
                                                        Try
                                                            Dim stream1 As PRStream = DirectCast(xobject.GetAsStream(name), PRStream)
                                                            Dim imgObj1 As New iTextSharp.text.pdf.parser.PdfImageObject(stream1)
                                                            Dim oldBytes() As Byte = imgObj1.GetImageAsBytes()

                                                            Dim imageDictionary As PdfDictionary = imgObj1.GetDictionary()
                                                            Dim w As Single = imgObj1.GetDrawingImage.Width
                                                            Dim h As Single = imgObj1.GetDrawingImage.Height
                                                            Using bm As System.Drawing.Bitmap = System.Drawing.Bitmap.FromStream(New MemoryStream(OriginalImageBytes))
                                                                newImageStream = New MemoryStream(optimizeBitmap(bm, bm.Width, bm.Height, bm.RawFormat, InterpolationMode1, SmoothingMode1, CompositingQuality1, imageAveragePixel, Nothing, autoResizeImages))
                                                                If Not newImageStream Is Nothing Then
                                                                    If newImageStream.Length > 0 Then
                                                                        If newImageStream.CanSeek Then
                                                                            newImageStream.Seek(0, SeekOrigin.Begin)
                                                                        End If
                                                                        Dim compressedImage As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(newImageStream.ToArray)
                                                                        If Not compressedImage Is Nothing Then
                                                                            PdfReader.KillIndirect(obj)
                                                                            stamper.Writer.AddDirectImageSimple(compressedImage, DirectCast(obj, PRIndirectReference))
                                                                        End If
                                                                    Else
                                                                        Dim stopme As Boolean = False
                                                                    End If
                                                                End If
                                                            End Using
                                                        Catch exOptimize As Exception
                                                            Err.Clear()
                                                        End Try
                                                    Else
                                                        Try
                                                            Dim stream1 As PRStream = DirectCast(xobject.GetAsStream(name), PRStream)
                                                            Dim imgObj1 As New iTextSharp.text.pdf.parser.PdfImageObject(stream1)
                                                            Dim oldBytes() As Byte = imgObj1.GetImageAsBytes()
                                                            Dim w As Single = imgObj1.GetDrawingImage.Width
                                                            Dim h As Single = imgObj1.GetDrawingImage.Height
                                                            Using bm As System.Drawing.Bitmap = System.Drawing.Bitmap.FromStream(New MemoryStream(OriginalImageBytes))
                                                                newImageStream = New MemoryStream(optimizeBitmap(bm, bm.Width, bm.Height, bm.RawFormat, InterpolationMode1, SmoothingMode1, CompositingQuality1, imageAveragePixel, Nothing, autoResizeImages))
                                                                If Not newImageStream Is Nothing Then
                                                                    If newImageStream.Length > 0 Then
                                                                        If newImageStream.CanSeek Then
                                                                            newImageStream.Seek(0, SeekOrigin.Begin)
                                                                        End If
                                                                        Dim compressedImage As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(newImageStream.ToArray)
                                                                        If Not compressedImage Is Nothing Then
                                                                            PdfReader.KillIndirect(obj)
                                                                            stamper.Writer.AddDirectImageSimple(compressedImage, DirectCast(obj, PRIndirectReference))
                                                                        End If
                                                                    Else
                                                                        Dim stopme As Boolean = False
                                                                    End If
                                                                End If
                                                            End Using
                                                        Catch exOptimize As Exception
                                                            Err.Clear()
                                                        End Try
                                                    End If
                                                ElseIf imgObject.Get(PdfName.FILTER).Equals(PdfName.DCTDECODE) Then
                                                    Dim newImageStream As New MemoryStream
                                                    Try
                                                        Dim oldBytes As Byte() = PdfReader.GetStreamBytesRaw(DirectCast(imgObject, PRStream))
                                                        Using sourceMS As New MemoryStream(oldBytes)
                                                            If sourceMS.CanSeek Then
                                                                sourceMS.Seek(0, SeekOrigin.Begin)
                                                            End If
                                                            Try
                                                                Using oldImageBitmap As System.Drawing.Bitmap = DirectCast(System.Drawing.Bitmap.FromStream(sourceMS, True, True), System.Drawing.Bitmap)
                                                                    Using bm As System.Drawing.Bitmap = System.Drawing.Bitmap.FromStream(New MemoryStream(OriginalImageBytes))
                                                                        newImageStream = New MemoryStream(optimizeBitmap(bm, bm.Width, bm.Height, bm.RawFormat, InterpolationMode1, SmoothingMode1, CompositingQuality1, imageAveragePixel, Nothing, autoResizeImages))
                                                                        If Not newImageStream Is Nothing Then
                                                                            If newImageStream.Length > 0 Then
                                                                                If newImageStream.CanSeek Then
                                                                                    newImageStream.Seek(0, SeekOrigin.Begin)
                                                                                End If
                                                                                Dim compressedImage As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(newImageStream.ToArray)
                                                                                If Not compressedImage Is Nothing Then
                                                                                    PdfReader.KillIndirect(obj)
                                                                                    stamper.Writer.AddDirectImageSimple(compressedImage, DirectCast(obj, PRIndirectReference))
                                                                                End If
                                                                            Else
                                                                                Dim stopme As Boolean = False
                                                                            End If
                                                                        End If
                                                                    End Using
                                                                End Using
                                                            Catch ex As Exception
                                                                Err.Clear()
                                                            End Try
                                                        End Using
                                                    Catch exJPEG As Exception
                                                        Err.Clear()
                                                    End Try
                                                ElseIf Not imgObject.Get(PdfName.FILTER) Is Nothing Then
                                                    Try
                                                        Dim strFilterName As String = imgObject.Get(PdfName.FILTER).ToString()
                                                        strFilterName = strFilterName
                                                    Catch ex As Exception
                                                        Err.Clear()
                                                    End Try
                                                End If
                                            End If
                                            Exit For
                                        Catch ex As Exception
                                            Err.Clear()
                                        End Try
                                    Else
                                        Dim blnStopMe As Boolean = True
                                    End If
                                End If
                            End If
                        Next
                    End If

                    stamper.Writer.CloseStream = False
                    stamper.Close()
                    stamper.Dispose()
                    If Not pageScale = 1.0F Then
                        Return frmMain.EncryptPDFDocument(fs.ToArray(), Nothing)
                    End If
                    Return fs.ToArray
                End Using
            End Using
        Catch ex As Exception
            Throw ex
        Finally
            Try
                reader.Close()
                reader.Dispose()
                reader = Nothing
            Catch ex2 As Exception
                Err.Clear()
            End Try
        End Try
        Return pdfBytes
    End Function
    Public Shared Function Replace_Image_Shared(ByRef pdfBytes() As Byte, ByVal pgNumber As Integer, ByVal pdfOwnerPassword() As Byte, ByVal OriginalImageRefNumber As Long, ByVal NewImageBytes() As Byte, ByVal OriginalImageFormat As System.Drawing.Imaging.ImageFormat, Optional ByVal scaleFactor As Single = 1.0F, Optional ByVal CompressionLevel As Integer = 100, Optional ByVal InterpolationMode1 As Drawing2D.InterpolationMode = InterpolationMode.HighQualityBicubic, Optional ByVal SmoothingMode1 As SmoothingMode = SmoothingMode.HighQuality, Optional ByVal CompositingQuality1 As CompositingQuality = CompositingQuality.HighQuality, Optional ByVal pageScale As Single = 1.0F, Optional ByVal imageAveragePixel As Integer = -1, Optional ByVal autoResizeImages As Boolean = False) As Byte()
        Dim reader As New PdfReader(pdfBytes, pdfOwnerPassword)
        Try
            Using fs As New MemoryStream
                Using stamper As New PdfStamper(reader, fs)
                    Dim page As PdfDictionary = reader.GetPageN(pgNumber)
                    Dim resources As PdfDictionary = DirectCast(PdfReader.GetPdfObject(page.Get(PdfName.RESOURCES)), PdfDictionary)
                    Dim xobject As PdfDictionary = DirectCast(PdfReader.GetPdfObject(resources.Get(PdfName.XOBJECT)), PdfDictionary)
GOTO_Restart_XOBJECT:
                    If Not xobject Is Nothing Then
                        Dim obj As PdfObject
                        For Each name As PdfName In xobject.Keys
                            obj = xobject.Get(name)
                            If obj.IsIndirect() Then
                                obj = xobject.GetAsIndirectObject(name)
                                Dim imgObject As PdfDictionary = Nothing
                                Try
                                    If PdfReader.GetPdfObject(obj).IsStream Then
                                        imgObject = DirectCast(PdfReader.GetPdfObject(obj), PdfDictionary)
                                    ElseIf PdfReader.GetPdfObject(obj).IsDictionary Then
                                        imgObject = DirectCast(PdfReader.GetPdfObject(obj), PdfDictionary)
                                    Else
                                        imgObject = DirectCast(PdfReader.GetPdfObject(obj), PdfDictionary)
                                    End If
                                Catch ex As Exception
                                    Err.Clear()
                                End Try
                                Dim tmpIndRefNo As Long = DirectCast(obj, PRIndirectReference).Number

                                If OriginalImageRefNumber = tmpIndRefNo Then
                                    If Not imgObject Is Nothing Then
                                        Dim subtype As PdfName = imgObject.GetAsName(PdfName.SUBTYPE)
                                        Try
                                            If imgObject.Get(PdfName.SUBTYPE).Equals(PdfName.FORM) Then
                                                resources = DirectCast(imgObject.GetAsDict(PdfName.RESOURCES), PdfDictionary)
                                                If Not resources Is Nothing Then
                                                    If Not resources.Get(PdfName.XOBJECT) Is Nothing Then
                                                        xobject = DirectCast(resources.GetAsDict(PdfName.XOBJECT), PdfDictionary)
                                                        GoTo GOTO_Restart_XOBJECT
                                                    End If
                                                End If
                                            ElseIf imgObject.Get(PdfName.SUBTYPE).Equals(PdfName.IMAGE) Then
                                                Dim filter As String = imgObject.GetAsName(PdfName.FILTER).ToString
                                                If imgObject.Get(PdfName.FILTER).Equals(PdfName.FLATEDECODE) Then
                                                    Dim newImageStream As New MemoryStream
                                                    If Not imgObject.Get(PdfName.SMASK) Is Nothing Then
                                                        Try
                                                            Dim stream1 As PRStream = DirectCast(xobject.GetAsStream(name), PRStream)
                                                            Dim imgObj1 As New iTextSharp.text.pdf.parser.PdfImageObject(stream1)
                                                            Dim oldBytes() As Byte = imgObj1.GetImageAsBytes()
                                                            Dim imageDictionary As PdfDictionary = imgObj1.GetDictionary()
                                                            Dim w As Single = imgObj1.GetDrawingImage.Width
                                                            Dim h As Single = imgObj1.GetDrawingImage.Height
                                                            Using bm As System.Drawing.Bitmap = System.Drawing.Bitmap.FromStream(New MemoryStream(NewImageBytes))
                                                                newImageStream = New MemoryStream(optimizeBitmapShared(bm, bm.Width, bm.Height, bm.RawFormat, InterpolationMode1, SmoothingMode1, CompositingQuality1, imageAveragePixel, Nothing, autoResizeImages))
                                                                If Not newImageStream Is Nothing Then
                                                                    If newImageStream.Length > 0 Then
                                                                        If newImageStream.CanSeek Then
                                                                            newImageStream.Seek(0, SeekOrigin.Begin)
                                                                        End If
                                                                        Dim compressedImage As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(newImageStream.ToArray)
                                                                        If Not compressedImage Is Nothing Then
                                                                            PdfReader.KillIndirect(obj)
                                                                            stamper.Writer.AddDirectImageSimple(compressedImage, DirectCast(obj, PRIndirectReference))
                                                                            Exit For
                                                                        End If
                                                                    Else
                                                                        Dim stopme As Boolean = False
                                                                    End If
                                                                End If
                                                            End Using
                                                        Catch exOptimize As Exception
                                                            Err.Clear()
                                                        End Try
                                                    Else
                                                        Try
                                                            Dim stream1 As PRStream = DirectCast(xobject.GetAsStream(name), PRStream)
                                                            Dim imgObj1 As New iTextSharp.text.pdf.parser.PdfImageObject(stream1)
                                                            Dim oldBytes() As Byte = imgObj1.GetImageAsBytes()
                                                            Dim w As Single = imgObj1.GetDrawingImage.Width
                                                            Dim h As Single = imgObj1.GetDrawingImage.Height
                                                            Using bm As System.Drawing.Bitmap = System.Drawing.Bitmap.FromStream(New MemoryStream(NewImageBytes))
                                                                newImageStream = New MemoryStream(optimizeBitmapShared(bm, bm.Width, bm.Height, bm.RawFormat, InterpolationMode1, SmoothingMode1, CompositingQuality1, imageAveragePixel, Nothing, autoResizeImages))
                                                                If Not newImageStream Is Nothing Then
                                                                    If newImageStream.Length > 0 Then
                                                                        If newImageStream.CanSeek Then
                                                                            newImageStream.Seek(0, SeekOrigin.Begin)
                                                                        End If
                                                                        Dim compressedImage As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(newImageStream.ToArray)
                                                                        If Not compressedImage Is Nothing Then
                                                                            PdfReader.KillIndirect(obj)
                                                                            stamper.Writer.AddDirectImageSimple(compressedImage, DirectCast(obj, PRIndirectReference))
                                                                            Exit For
                                                                        End If
                                                                    Else
                                                                        Dim stopme As Boolean = False
                                                                    End If
                                                                End If
                                                            End Using
                                                        Catch exOptimize As Exception
                                                            Err.Clear()
                                                        End Try
                                                    End If
                                                ElseIf imgObject.Get(PdfName.FILTER).Equals(PdfName.DCTDECODE) Then
                                                    Dim newImageStream As New MemoryStream
                                                    Try
                                                        Dim oldBytes As Byte() = PdfReader.GetStreamBytesRaw(DirectCast(imgObject, PRStream))
                                                        Using sourceMS As New MemoryStream(oldBytes)
                                                            If sourceMS.CanSeek Then
                                                                sourceMS.Seek(0, SeekOrigin.Begin)
                                                            End If
                                                            Try
                                                                Using oldImageBitmap As System.Drawing.Bitmap = DirectCast(System.Drawing.Bitmap.FromStream(sourceMS, True, True), System.Drawing.Bitmap)
                                                                    Using bm As System.Drawing.Bitmap = System.Drawing.Bitmap.FromStream(New MemoryStream(NewImageBytes))
                                                                        newImageStream = New MemoryStream(optimizeBitmapShared(bm, bm.Width, bm.Height, bm.RawFormat, InterpolationMode1, SmoothingMode1, CompositingQuality1, imageAveragePixel, Nothing, autoResizeImages))
                                                                        If Not newImageStream Is Nothing Then
                                                                            If newImageStream.Length > 0 Then
                                                                                If newImageStream.CanSeek Then
                                                                                    newImageStream.Seek(0, SeekOrigin.Begin)
                                                                                End If
                                                                                Dim compressedImage As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(newImageStream.ToArray)
                                                                                If Not compressedImage Is Nothing Then
                                                                                    PdfReader.KillIndirect(obj)
                                                                                    stamper.Writer.AddDirectImageSimple(compressedImage, DirectCast(obj, PRIndirectReference))
                                                                                    Exit For
                                                                                End If
                                                                            Else
                                                                                Dim stopme As Boolean = False
                                                                            End If
                                                                        End If
                                                                    End Using
                                                                End Using
                                                            Catch ex As Exception
                                                                Err.Clear()
                                                            End Try
                                                        End Using
                                                    Catch exJPEG As Exception
                                                        Err.Clear()
                                                    End Try
                                                ElseIf Not imgObject.Get(PdfName.FILTER) Is Nothing Then
                                                    Try
                                                        Dim strFilterName As String = imgObject.Get(PdfName.FILTER).ToString()
                                                        strFilterName = strFilterName
                                                    Catch ex As Exception
                                                        Err.Clear()
                                                    End Try
                                                End If
                                            End If

                                        Catch ex As Exception
                                            Err.Clear()
                                        End Try
                                    Else
                                        Dim blnStopMe As Boolean = True
                                    End If
                                End If
                            End If
                        Next
                    End If

                    stamper.Writer.CloseStream = False
                    stamper.Close()
                    stamper.Dispose()
                    If Not pageScale = 1.0F Then
                        Return frmMain.EncryptPDFDocument(fs.ToArray(), Nothing)
                    End If
                    Return fs.ToArray
                End Using
            End Using
        Catch ex As Exception
            Throw ex
        Finally
            Try
                reader.Close()
                reader.Dispose()
                reader = Nothing
            Catch ex2 As Exception
                Err.Clear()
            End Try
        End Try
        Return pdfBytes
    End Function
    Public Shared Function Replace_Image_Shared(ByRef pdfBytes() As Byte, ByVal pgNumber As Integer, ByVal pdfOwnerPassword() As Byte, ByVal OriginalImageRefNumber As Long, ByVal NewImage As System.Drawing.Bitmap, ByVal OriginalImageFormat As System.Drawing.Imaging.ImageFormat, Optional ByVal scaleFactor As Single = 1.0F, Optional ByVal CompressionLevel As Integer = 100, Optional ByVal InterpolationMode1 As Drawing2D.InterpolationMode = InterpolationMode.HighQualityBicubic, Optional ByVal SmoothingMode1 As SmoothingMode = SmoothingMode.HighQuality, Optional ByVal CompositingQuality1 As CompositingQuality = CompositingQuality.HighQuality, Optional ByVal pageScale As Single = 1.0F, Optional ByVal imageAveragePixel As Integer = -1) As Byte()
        Dim reader As New PdfReader(pdfBytes, pdfOwnerPassword)
        Try
            Using fs As New MemoryStream
                Using stamper As New PdfStamper(reader, fs)
                    Dim page As PdfDictionary = reader.GetPageN(pgNumber)
                    Dim resources As PdfDictionary = DirectCast(PdfReader.GetPdfObject(page.Get(PdfName.RESOURCES)), PdfDictionary)
                    Dim xobject As PdfDictionary = DirectCast(PdfReader.GetPdfObject(resources.Get(PdfName.XOBJECT)), PdfDictionary)
GOTO_Restart_XOBJECT:
                    If Not xobject Is Nothing Then
                        Dim obj As PdfObject
                        For Each name As PdfName In xobject.Keys
                            obj = xobject.Get(name)
                            If obj.IsIndirect() Then
                                obj = xobject.GetAsIndirectObject(name)
                                Dim imgObject As PdfDictionary = Nothing
                                Try
                                    If PdfReader.GetPdfObject(obj).IsStream Then
                                        imgObject = DirectCast(PdfReader.GetPdfObject(obj), PdfDictionary)
                                    ElseIf PdfReader.GetPdfObject(obj).IsDictionary Then
                                        imgObject = DirectCast(PdfReader.GetPdfObject(obj), PdfDictionary)
                                    Else
                                        imgObject = DirectCast(PdfReader.GetPdfObject(obj), PdfDictionary)
                                    End If
                                Catch ex As Exception
                                    Err.Clear()
                                End Try
                                Dim tmpIndRefNo As Long = DirectCast(obj, PRIndirectReference).Number

                                If OriginalImageRefNumber = tmpIndRefNo Then
                                    If Not imgObject Is Nothing Then
                                        Dim subtype As PdfName = imgObject.GetAsName(PdfName.SUBTYPE)
                                        Try
                                            If imgObject.Get(PdfName.SUBTYPE).Equals(PdfName.FORM) Then
                                                resources = DirectCast(imgObject.GetAsDict(PdfName.RESOURCES), PdfDictionary)
                                                If Not resources Is Nothing Then
                                                    If Not resources.Get(PdfName.XOBJECT) Is Nothing Then
                                                        xobject = DirectCast(resources.GetAsDict(PdfName.XOBJECT), PdfDictionary)
                                                        GoTo GOTO_Restart_XOBJECT
                                                    End If
                                                End If
                                            ElseIf imgObject.Get(PdfName.SUBTYPE).Equals(PdfName.IMAGE) Then
                                                Dim filter As String = imgObject.GetAsName(PdfName.FILTER).ToString
                                                If imgObject.Get(PdfName.FILTER).Equals(PdfName.FLATEDECODE) Then
                                                    Dim newImageStream As New MemoryStream
                                                    If Not imgObject.Get(PdfName.SMASK) Is Nothing Then
                                                        Try
                                                            Dim stream1 As PRStream = DirectCast(xobject.GetAsStream(name), PRStream)
                                                            Dim imgObj1 As New iTextSharp.text.pdf.parser.PdfImageObject(stream1)
                                                            Dim oldBytes() As Byte = imgObj1.GetImageAsBytes()
                                                            Dim imageDictionary As PdfDictionary = imgObj1.GetDictionary()
                                                            Dim w As Single = imgObj1.GetDrawingImage.Width
                                                            Dim h As Single = imgObj1.GetDrawingImage.Height
                                                            Using bm As System.Drawing.Bitmap = NewImage.Clone
                                                                Dim strFormat As String = OriginalImageFormat.ToString
                                                                Dim imgFrmt As System.Drawing.Imaging.ImageFormat = OriginalImageFormat
                                                                If imgFrmt.GetHashCode() = System.Drawing.Imaging.ImageFormat.MemoryBmp.GetHashCode() Then
                                                                    imgFrmt = System.Drawing.Imaging.ImageFormat.Jpeg
                                                                End If
                                                                Dim compressedImage As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(bm, imgFrmt)
                                                                If Not compressedImage Is Nothing Then
                                                                    PdfReader.KillIndirect(obj)
                                                                    Dim maskImage As iTextSharp.text.Image = compressedImage.ImageMask
                                                                    If Not maskImage Is Nothing Then
                                                                        stamper.Writer.AddDirectImageSimple(maskImage)
                                                                    End If
                                                                    stamper.Writer.AddDirectImageSimple(compressedImage, DirectCast(obj, PRIndirectReference))
                                                                    Exit For
                                                                End If
                                                            End Using
                                                        Catch exOptimize As Exception
                                                            Err.Clear()
                                                        End Try
                                                    Else
                                                        Try
                                                            Dim stream1 As PRStream = DirectCast(xobject.GetAsStream(name), PRStream)
                                                            Dim imgObj1 As New iTextSharp.text.pdf.parser.PdfImageObject(stream1)
                                                            Dim oldBytes() As Byte = imgObj1.GetImageAsBytes()
                                                            Dim w As Single = imgObj1.GetDrawingImage.Width
                                                            Dim h As Single = imgObj1.GetDrawingImage.Height
                                                            Using bm As System.Drawing.Bitmap = NewImage.Clone
                                                                Dim strFormat As String = OriginalImageFormat.ToString
                                                                Dim imgFrmt As System.Drawing.Imaging.ImageFormat = OriginalImageFormat
                                                                If imgFrmt.GetHashCode() = System.Drawing.Imaging.ImageFormat.MemoryBmp.GetHashCode() Then
                                                                    imgFrmt = System.Drawing.Imaging.ImageFormat.Jpeg
                                                                End If
                                                                Dim compressedImage As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(bm, imgFrmt)
                                                                If Not compressedImage Is Nothing Then
                                                                    PdfReader.KillIndirect(obj)
                                                                    Dim maskImage As iTextSharp.text.Image = compressedImage.ImageMask
                                                                    If Not maskImage Is Nothing Then
                                                                        stamper.Writer.AddDirectImageSimple(maskImage)
                                                                    End If
                                                                    stamper.Writer.AddDirectImageSimple(compressedImage, DirectCast(obj, PRIndirectReference))
                                                                    Exit For
                                                                End If
                                                            End Using
                                                        Catch exOptimize As Exception
                                                            Err.Clear()
                                                        End Try
                                                    End If
                                                ElseIf imgObject.Get(PdfName.FILTER).Equals(PdfName.DCTDECODE) Then
                                                    Dim newImageStream As New MemoryStream
                                                    Try
                                                        Dim oldBytes As Byte() = PdfReader.GetStreamBytesRaw(DirectCast(imgObject, PRStream))
                                                        Using sourceMS As New MemoryStream(oldBytes)
                                                            If sourceMS.CanSeek Then
                                                                sourceMS.Seek(0, SeekOrigin.Begin)
                                                            End If
                                                            Try
                                                                Using bm As System.Drawing.Bitmap = NewImage.Clone
                                                                    Dim strFormat As String = NewImage.RawFormat.ToString
                                                                    Dim imgFrmt As System.Drawing.Imaging.ImageFormat = NewImage.RawFormat
                                                                    If imgFrmt.GetHashCode() = System.Drawing.Imaging.ImageFormat.MemoryBmp.GetHashCode() Then
                                                                        imgFrmt = System.Drawing.Imaging.ImageFormat.Jpeg
                                                                    End If
                                                                    Dim compressedImage As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(bm, imgFrmt)
                                                                    If Not compressedImage Is Nothing Then
                                                                        PdfReader.KillIndirect(obj)
                                                                        Dim maskImage As iTextSharp.text.Image = compressedImage.ImageMask
                                                                        If Not maskImage Is Nothing Then
                                                                            stamper.Writer.AddDirectImageSimple(maskImage)
                                                                        End If
                                                                        stamper.Writer.AddDirectImageSimple(compressedImage, DirectCast(obj, PRIndirectReference))
                                                                        Exit For
                                                                    End If
                                                                End Using
                                                            Catch ex As Exception
                                                                Err.Clear()
                                                            End Try
                                                        End Using
                                                    Catch exJPEG As Exception
                                                        Err.Clear()
                                                    End Try
                                                ElseIf Not imgObject.Get(PdfName.FILTER) Is Nothing Then
                                                    Try
                                                        Dim strFilterName As String = imgObject.Get(PdfName.FILTER).ToString()
                                                        strFilterName = strFilterName
                                                    Catch ex As Exception
                                                        Err.Clear()
                                                    End Try
                                                End If
                                            End If

                                        Catch ex As Exception
                                            Err.Clear()
                                        End Try
                                    Else
                                        Dim blnStopMe As Boolean = True
                                    End If
                                End If
                            End If
                        Next
                    End If

                    stamper.Writer.CloseStream = False
                    stamper.Close()
                    stamper.Dispose()
                    If Not pageScale = 1.0F Then
                        Return frmMain.EncryptPDFDocument(fs.ToArray(), Nothing)
                    End If
                    Return fs.ToArray
                End Using
            End Using
        Catch ex As Exception
            Throw ex
        Finally
            Try
                reader.Close()
                reader.Dispose()
                reader = Nothing
            Catch ex2 As Exception
                Err.Clear()
            End Try
        End Try
        Return pdfBytes
    End Function
    Private Shared Sub RemoveIndirectReferences(dict As PdfDictionary, refNumbers As Integer)
        Dim newDict As PdfDictionary
        Dim arrayData As PdfArray
        Dim indirect As PdfIndirectReference
        Dim i As Integer

        For Each key As PdfName In dict.Keys

            If dict.Get(key).IsDictionary Then
                Try
                    newDict = dict.GetAsDict(key)
                    RemoveIndirectReferences(newDict, refNumbers)
                Catch ex As Exception
                    Err.Clear()
                End Try
            ElseIf dict.Get(key).IsIndirect Then
                Try

                    If dict.GetDirectObject(key).IsArray Then
                        arrayData = dict.GetAsArray(key)
                        If arrayData IsNot Nothing Then
                            i = 0
                            While i < arrayData.Size
                                If arrayData(i).IsIndirect Then
                                    indirect = arrayData.GetAsIndirectObject(i)
                                    If refNumbers = (indirect.Number) Then
                                        PdfReader.KillIndirect(indirect)
                                        arrayData.Remove(i)
                                    End If
                                End If
                                i += 1
                            End While
                        End If
                    ElseIf dict.GetDirectObject(key).IsDictionary Then
                        Try
                            newDict = dict.GetAsDict(key)
                            If refNumbers = (dict.GetAsIndirectObject(key).Number) Then
                                PdfReader.KillIndirect(dict.GetAsIndirectObject(key))
                                RemoveIndirectReferences(newDict, refNumbers)
                                dict.Remove(key)
                            End If
                        Catch ex As Exception
                            Err.Clear()
                        End Try
                    End If
                Catch ex As Exception
                    Err.Clear()
                End Try
            ElseIf dict.Get(key).IsArray Then
                Try
                    arrayData = dict.GetAsArray(key)
                    If arrayData IsNot Nothing Then
                        i = 0
                        While i < arrayData.Size
                            If arrayData(i).IsIndirect Then
                                indirect = arrayData.GetAsIndirectObject(i)
                                If refNumbers = (indirect.Number) Then
                                    PdfReader.KillIndirect(indirect)
                                    arrayData.Remove(i)
                                Else

                                    If arrayData.GetDirectObject(i).IsArray Then
                                        arrayData = arrayData.GetAsArray(i)
                                        If arrayData IsNot Nothing Then
                                            i = 0
                                            While i < arrayData.Size
                                                If arrayData(i).IsIndirect Then
                                                    indirect = arrayData.GetAsIndirectObject(i)
                                                    If refNumbers = (indirect.Number) Then
                                                        PdfReader.KillIndirect(indirect)
                                                        arrayData.Remove(i)
                                                    End If
                                                End If
                                                i += 1
                                            End While
                                        End If
                                    ElseIf arrayData.GetDirectObject(i).IsDictionary Then
                                        Try
                                            newDict = arrayData.GetAsDict(i)
                                            If refNumbers = (arrayData.GetAsIndirectObject(i).Number) Then
                                                PdfReader.KillIndirect(arrayData.GetAsIndirectObject(i))
                                                arrayData.Remove(i)
                                                RemoveIndirectReferences(newDict, refNumbers)
                                            End If
                                        Catch ex As Exception
                                            Err.Clear()
                                        End Try
                                    End If
                                End If
                            End If
                            i += 1
                        End While
                    End If
                Catch ex As Exception
                    Err.Clear()
                End Try
            End If
        Next
    End Sub
    Public Shared Function RemoveImageFromPage(ByVal PageNumber As Integer, ByRef frmMain As frmMain, reader As PdfReader, ByVal OriginalImageRefNumber As Long, OriginalRefString As PdfName) As Byte()

        Dim pdfNameRef As PdfName = Nothing
        If True = True Then

            Dim i As Integer

            Dim pg As PdfDictionary
            Dim res As PdfDictionary
            Dim xobj As PdfDictionary
            Dim obj As PdfObject
            Dim tg As PdfDictionary
            Dim type As PdfName

            pg = reader.GetPageN(PageNumber)
            res = PdfReader.GetPdfObject(pg.Get(PdfName.RESOURCES))
            xobj = PdfReader.GetPdfObject(res.Get(PdfName.XOBJECT))

            If Not (xobj Is Nothing) Then
                For i = 0 To xobj.Keys().Count - 1
                    Try
                        obj = xobj.Get(xobj.Keys(i))
                        If obj.IsIndirect Then
                            If xobj.GetAsIndirectObject(xobj.Keys(i)).Number = OriginalImageRefNumber Then
                                pdfNameRef = DirectCast(xobj.Keys(i), PdfName)
                                tg = PdfReader.GetPdfObject(obj)
                                If Not (tg Is Nothing) Then
                                    type = PdfReader.GetPdfObject(tg.Get(PdfName.SUBTYPE))
                                    If PdfName.IMAGE.Equals(type) Then
                                        If xobj.GetDirectObject(xobj.Keys(i)).IsStream Then
                                            Dim s As PRStream = DirectCast(xobj.GetDirectObject(xobj.Keys(i)), PRStream)
                                            ClearStream(s)
                                            PdfReader.KillIndirect(s)
                                        End If
                                        xobj.Remove(xobj.Keys(i))
                                        PdfReader.KillIndirect(obj)
                                        RemoveIndirectReferences(pg, OriginalImageRefNumber)
                                        Exit For
                                    End If
                                End If
                            End If
                        End If
                    Catch ex As Exception
                        Err.Clear()
                    End Try

                Next i
            End If
            For Each k As PdfName In pg.Keys
                If pg.Get(k).IsIndirect Then
                    If pg.GetAsIndirectObject(k).Number = OriginalImageRefNumber Then
                        pg.Remove(k)
                    End If
                ElseIf pg.Get(k).IsDictionary Then
                    RemoveIndirectReferences(pg.GetAsDict(k), OriginalImageRefNumber)
                End If
            Next
        End If
        Dim ms As MemoryStream = New MemoryStream
        Dim pdfByteContent() As Byte = reader.GetPageContent(PageNumber)
        Dim cp As PdfContentParser = New PdfContentParser(New PRTokeniser(New RandomAccessFileOrArray(pdfByteContent)))
        Dim first As PdfName = Nothing
        While True
            Dim ar As List(Of PdfObject) = cp.Parse(Nothing)
            If ar.Count = 0 Then
                Exit While
            End If

            If "Do".Equals(ar(ar.Count - 1).ToString) Then
                first = CType(ar(0), PdfName)

                If Not CType(first, PdfName).Equals(pdfNameRef) Then
                    For Each o As PdfObject In ar
                        o.ToPdf(Nothing, ms)
                        ms.WriteByte(CType(Asc(vbLf), Byte))
                    Next
                Else
                    Dim bas As Boolean = True
                    Dim bas2 As Boolean = bas

                End If
            Else
                For Each o As PdfObject In ar
                    o.ToPdf(Nothing, ms)
                    ms.WriteByte(CType(Asc(vbLf), Byte))
                Next
            End If
        End While
        reader.SetPageContent(PageNumber, ms.ToArray())
        reader.RemoveUnusedObjects()
        Return frmMain.getPDFBytes(reader)
    End Function
    Public Shared Sub ClearStream(ByRef orig As PRStream)
        orig.Clear()
        Dim baos As New System.IO.MemoryStream
        orig.SetData(baos.ToArray, False)
    End Sub
    Private regEx As New System.Text.RegularExpressions.Regex("\nBI.*?\nEI", System.Text.RegularExpressions.RegexOptions.Compiled)
    Private Sub CleanStream(obj As PdfObject)
        Dim stream = DirectCast(obj, PRStream)
        Dim data = PdfReader.GetStreamBytes(stream)

        Dim currentContent = System.Text.Encoding.ASCII.GetString(data)
        Dim newContent = regEx.Replace(currentContent, "")
        Dim newData = System.Text.Encoding.ASCII.GetBytes(newContent)

        stream.SetData(newData)
    End Sub
    Public Shared Function delete_Image_Shared(ByRef pdfBytes() As Byte, ByVal pgNumber As Integer, ByVal pdfOwnerPassword() As Byte, ByVal OriginalImageRefNumber As Long, ByVal OriginalImageFormat As System.Drawing.Imaging.ImageFormat, Optional ByVal scaleFactor As Single = 1.0F, Optional ByVal CompressionLevel As Integer = 100, Optional ByVal InterpolationMode1 As Drawing2D.InterpolationMode = InterpolationMode.HighQualityBicubic, Optional ByVal SmoothingMode1 As SmoothingMode = SmoothingMode.HighQuality, Optional ByVal CompositingQuality1 As CompositingQuality = CompositingQuality.HighQuality, Optional ByVal pageScale As Single = 1.0F, Optional ByVal imageAveragePixel As Integer = -1) As Byte()
        Dim reader As New PdfReader(pdfBytes, pdfOwnerPassword)
        Try
            Using fs As New MemoryStream
                Using stamper As New PdfStamper(reader, fs)
                    Dim page As PdfDictionary = reader.GetPageN(pgNumber)
                    Dim resources As PdfDictionary = DirectCast(PdfReader.GetPdfObject(page.Get(PdfName.RESOURCES)), PdfDictionary)
                    Dim xobject As PdfDictionary = DirectCast(PdfReader.GetPdfObject(resources.Get(PdfName.XOBJECT)), PdfDictionary)
GOTO_Restart_XOBJECT:
                    If Not xobject Is Nothing Then
                        Dim obj As PdfObject
                        For Each name As PdfName In xobject.Keys
                            obj = xobject.Get(name)
                            If obj.IsIndirect() Then
                                obj = xobject.GetAsIndirectObject(name)
                                Dim imgObject As PdfDictionary = Nothing
                                Try
                                    If PdfReader.GetPdfObject(obj).IsStream Then
                                        imgObject = DirectCast(PdfReader.GetPdfObject(obj), PdfDictionary)
                                    ElseIf PdfReader.GetPdfObject(obj).IsDictionary Then
                                        imgObject = DirectCast(PdfReader.GetPdfObject(obj), PdfDictionary)
                                    Else
                                        imgObject = DirectCast(PdfReader.GetPdfObject(obj), PdfDictionary)
                                    End If
                                Catch ex As Exception
                                    Err.Clear()
                                End Try
                                Dim tmpIndRefNo As Long = DirectCast(obj, PRIndirectReference).Number

                                If OriginalImageRefNumber = tmpIndRefNo Then
                                    If Not imgObject Is Nothing Then
                                        Dim subtype As PdfName = imgObject.GetAsName(PdfName.SUBTYPE)
                                        Try
                                            If imgObject.Get(PdfName.SUBTYPE).Equals(PdfName.FORM) Then
                                                resources = DirectCast(imgObject.GetAsDict(PdfName.RESOURCES), PdfDictionary)
                                                If Not resources Is Nothing Then
                                                    If Not resources.Get(PdfName.XOBJECT) Is Nothing Then
                                                        xobject = DirectCast(resources.GetAsDict(PdfName.XOBJECT), PdfDictionary)
                                                        GoTo GOTO_Restart_XOBJECT
                                                    End If
                                                End If
                                            ElseIf imgObject.Get(PdfName.SUBTYPE).Equals(PdfName.IMAGE) Then
                                                Dim filter As String = imgObject.GetAsName(PdfName.FILTER).ToString
                                                If imgObject.Get(PdfName.FILTER).Equals(PdfName.FLATEDECODE) Then
                                                    Dim newImageStream As New MemoryStream
                                                    If Not imgObject.Get(PdfName.SMASK) Is Nothing Then
                                                        Try
                                                            PdfReader.KillIndirect(xobject.Get(name))


                                                        Catch exOptimize As Exception
                                                            Err.Clear()
                                                        End Try
                                                    Else
                                                        Try
                                                            PdfReader.KillIndirect(xobject.Get(name))
                                                        Catch exOptimize As Exception
                                                            Err.Clear()
                                                        End Try
                                                    End If
                                                ElseIf imgObject.Get(PdfName.FILTER).Equals(PdfName.DCTDECODE) Then
                                                    Dim newImageStream As New MemoryStream
                                                    Try
                                                        Try

                                                            PdfReader.KillIndirect(xobject.Get(name))
                                                        Catch ex As Exception
                                                            Err.Clear()
                                                        End Try
                                                    Catch exJPEG As Exception
                                                        Err.Clear()
                                                    End Try
                                                ElseIf Not imgObject.Get(PdfName.FILTER) Is Nothing Then
                                                    Try
                                                        Dim strFilterName As String = imgObject.Get(PdfName.FILTER).ToString()
                                                        strFilterName = strFilterName
                                                    Catch ex As Exception
                                                        Err.Clear()
                                                    End Try
                                                End If
                                            End If

                                        Catch ex As Exception
                                            Err.Clear()
                                        End Try
                                    Else
                                        Dim blnStopMe As Boolean = True
                                    End If
                                End If
                            End If
                        Next
                    End If

                    stamper.Writer.CloseStream = False
                    stamper.Close()
                    stamper.Dispose()
                    If Not pageScale = 1.0F Then
                        Return frmMain.EncryptPDFDocument(fs.ToArray(), Nothing)
                    End If
                    Return fs.ToArray
                End Using
            End Using
        Catch ex As Exception
            Throw ex
        Finally
            Try
                reader.Close()
                reader.Dispose()
                reader = Nothing
            Catch ex2 As Exception
                Err.Clear()
            End Try
        End Try
        Return pdfBytes
    End Function
    Public Function Optimize_Images(ByRef reader As PdfReader, Optional ByVal scaleFactor As Single = 1.0F, Optional ByVal CompressionLevel As Integer = 100, Optional ByVal InterpolationMode1 As Drawing2D.InterpolationMode = InterpolationMode.HighQualityBicubic, Optional ByVal SmoothingMode1 As SmoothingMode = SmoothingMode.HighQuality, Optional ByVal CompositingQuality1 As CompositingQuality = CompositingQuality.HighQuality, Optional ByRef ProgressBarX As ProgressBar = Nothing, Optional ByVal imageAveragePixel As Integer = -1, Optional ByVal autoResizeImages As Boolean = False, Optional ByVal optimizeJPXDECODE As Boolean = False, Optional optimizeSMaskImages As Boolean = False, Optional ByVal pageScale As Single = 1.0F) As Byte()
        Dim startCount As Integer = 0
GoTo_StartOver:
        startCount += 1
        If reader Is Nothing Then Return Nothing
        Dim scaleFactorTemp As Single = scaleFactor
        Dim pgNumber As Integer = 0
        Try
            If Not pageScale = 1.0F Then
                reader = scalePDF(reader.Clone, pageScale)
                Dim pdfBytes() As Byte = frmMain.EncryptPDFDocument(frmMain.getPDFBytes(reader, False))
                reader = New PdfReader(pdfBytes, frmMain.getBytes(frmMain.pdfOwnerPassword))
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
        Try
            Using fs As New MemoryStream
                Using stamper As New PdfStamper(reader, fs)
                    For pgNumber = 1 To reader.NumberOfPages
                        If cancelOptimize Or cancelOptimize_Shared Then
                            Return Nothing
                        Else

                        End If
                        If pgNumber = 16 Then
                            pgNumber = pgNumber
                        End If
                        If Not ProgressBarX Is Nothing Then
                            If ProgressBarX.GetType Is GetType(ProgressBar) Then
                                If Not ProgressBarX.Enabled Then ProgressBarX.Enabled = True
                                If Not ProgressBarX.Visible Then ProgressBarX.Visible = True
                                ProgressBarX.Value = CInt((pgNumber / reader.NumberOfPages) * 100)
                                If cancelOptimize Or cancelOptimize_Shared Then
                                    Return Nothing
                                Else
                                    Application.DoEvents()
                                End If
                            End If
                        End If
                        If pgNumber = 10 Then
                        End If
                        Dim page As PdfDictionary = reader.GetPageN(pgNumber)
                        Dim resources As PdfDictionary = DirectCast(PdfReader.GetPdfObject(page.Get(PdfName.RESOURCES)), PdfDictionary)
                        Dim xobject As PdfDictionary = DirectCast(PdfReader.GetPdfObject(resources.Get(PdfName.XOBJECT)), PdfDictionary)
GOTO_Restart_XOBJECT:
                        If Not xobject Is Nothing Then
                            Dim obj As PdfObject
                            For Each name As PdfName In xobject.Keys
                                If cancelOptimize Or cancelOptimize_Shared Then
                                    Return Nothing
                                Else

                                End If
                                obj = xobject.Get(name)
                                scaleFactorTemp = scaleFactor
                                If obj.IsIndirect() Then
                                    obj = xobject.GetAsIndirectObject(name)
                                    Dim imgObject As PdfDictionary = Nothing
                                    Try
                                        If PdfReader.GetPdfObject(obj).IsStream Then
                                            imgObject = DirectCast(PdfReader.GetPdfObject(obj), PdfDictionary)
                                        ElseIf PdfReader.GetPdfObject(obj).IsDictionary Then
                                            imgObject = DirectCast(PdfReader.GetPdfObject(obj), PdfDictionary)
                                        Else
                                            imgObject = DirectCast(PdfReader.GetPdfObject(obj), PdfDictionary)
                                        End If
                                    Catch ex As Exception
                                        Err.Clear()
                                    End Try
                                    If Not imgObject Is Nothing Then
                                        Dim subtype As PdfName = imgObject.GetAsName(PdfName.SUBTYPE)
                                        Try
                                            If imgObject.Get(PdfName.SUBTYPE).Equals(PdfName.FORM) Then
                                                resources = DirectCast(imgObject.GetAsDict(PdfName.RESOURCES), PdfDictionary)
                                                If Not resources Is Nothing Then
                                                    If Not resources.Get(PdfName.XOBJECT) Is Nothing Then
                                                        xobject = DirectCast(resources.GetAsDict(PdfName.XOBJECT), PdfDictionary)
                                                        GoTo GOTO_Restart_XOBJECT
                                                    End If
                                                End If
                                            ElseIf imgObject.Get(PdfName.SUBTYPE).Equals(PdfName.IMAGE) Then
                                                Dim filter As String = imgObject.GetAsName(PdfName.FILTER).ToString
                                                If (optimizeSMaskImages = False) Then
                                                    Dim sMask1 As PdfStream = imgObject.Get(PdfName.SMASK)
                                                    If sMask1 Is Nothing Then
                                                        If imgObject.GetDirectObject(PdfName.COLORSPACE).IsArray Then
                                                            Dim sColorSpace As PdfArray = imgObject.GetDirectObject(PdfName.COLORSPACE)
                                                            If sColorSpace.Contains(PdfName.INDEXED) And sColorSpace.Contains(PdfName.DEVICEGRAY) Then
                                                                GoTo Goto_NextImage
                                                            End If
                                                        End If

                                                    Else
                                                        GoTo Goto_NextImage
                                                    End If
                                                End If
                                                If imgObject.Get(PdfName.FILTER).Equals(PdfName.FLATEDECODE) Then
                                                    Dim newImageStream As New MemoryStream
                                                    If Not imgObject.Get(PdfName.SMASK) Is Nothing Then
                                                        Try
                                                            Dim stream1 As PRStream = DirectCast(xobject.GetAsStream(name), PRStream)
                                                            Dim imgObj1 As New iTextSharp.text.pdf.parser.PdfImageObject(stream1)
                                                            Dim oldBytes() As Byte = imgObj1.GetImageAsBytes()

                                                            Dim imageDictionary As PdfDictionary = imgObj1.GetDictionary()
                                                            Dim w As Single = imgObj1.GetDrawingImage.Width
                                                            Dim h As Single = imgObj1.GetDrawingImage.Height
                                                            Using sourceMS As New MemoryStream(oldBytes)
                                                                If sourceMS.CanSeek Then
                                                                    sourceMS.Seek(0, SeekOrigin.Begin)
                                                                End If
                                                                Try
                                                                    Using oldImageBitmap As System.Drawing.Bitmap = DirectCast(System.Drawing.Bitmap.FromStream(sourceMS, True, True), System.Drawing.Bitmap)
                                                                        Dim newSize As Size = Nothing
                                                                        Try
                                                                            Dim imgScale As Single = scaleFactor
                                                                            newSize = clsGetImageSize.getImageSize(reader, pgNumber, oldBytes.ToArray())
                                                                            If newSize = Nothing Then
                                                                                newSize = New Size(CInt(oldImageBitmap.Width * scaleFactor), CInt(oldImageBitmap.Height * scaleFactor))
                                                                                If Not imgObject.Get(PdfName.WIDTH) Is Nothing And Not imgObject.Get(PdfName.HEIGHT) Is Nothing Then
                                                                                    If imgObject.Get(PdfName.WIDTH).IsNumber And imgObject.Get(PdfName.HEIGHT).IsNumber Then
                                                                                        Dim widthImage As Integer = imgObject.GetAsNumber(PdfName.WIDTH).IntValue
                                                                                        Dim heightImage As Integer = imgObject.GetAsNumber(PdfName.HEIGHT).IntValue
                                                                                        Dim pageWidth As Integer = CInt(reader.GetPageSizeWithRotation(pgNumber).Width)
                                                                                        Dim pageHeight As Integer = CInt(reader.GetPageSizeWithRotation(pgNumber).Height)
                                                                                        If oldImageBitmap.Width > pageWidth Or oldImageBitmap.Height > pageHeight Then
                                                                                            imgScale = CInt((pageWidth / oldImageBitmap.Width) * scaleFactor)
                                                                                            newSize = New Size(CInt(oldImageBitmap.Width * imgScale), CInt(oldImageBitmap.Height * imgScale))
                                                                                        ElseIf oldImageBitmap.Width > pageWidth Or oldImageBitmap.Height > pageHeight Then
                                                                                            imgScale = CInt((pageHeight / oldImageBitmap.Height) * scaleFactor)
                                                                                            newSize = New Size(CInt(oldImageBitmap.Width * imgScale), CInt(oldImageBitmap.Height * imgScale))
                                                                                        Else
                                                                                            newSize = New Size(CInt(oldImageBitmap.Width * scaleFactor), CInt(oldImageBitmap.Height * scaleFactor))
                                                                                        End If
                                                                                        scaleFactorTemp = imgScale
                                                                                    End If
                                                                                End If
                                                                            End If
                                                                        Catch exGetWH As Exception
                                                                            Dim imgScale As Single = scaleFactor
                                                                            newSize = New Size(CInt(oldImageBitmap.Width * scaleFactor), CInt(oldImageBitmap.Height * scaleFactor))
                                                                            Err.Clear()
                                                                        End Try
                                                                        Dim maskStream As PRStream = CType(imageDictionary.GetAsStream(PdfName.SMASK), PRStream)
                                                                        Dim imageMask As System.Drawing.Image = Nothing
                                                                        Dim maskImage As PdfImageObject = Nothing
                                                                        Dim maskImageItext As iTextSharp.text.Image = Nothing
                                                                        If (Not (maskStream) Is Nothing) Then
                                                                            maskImage = New PdfImageObject(maskStream)
                                                                            imageMask = System.Drawing.Image.FromStream(New MemoryStream(maskImage.GetImageAsBytes()), False, False)
                                                                            maskImageItext = iTextSharp.text.Image.GetInstance(imageMask, System.Drawing.Imaging.ImageFormat.Png)
                                                                            maskImageItext.MakeMask()
                                                                        End If
                                                                        newImageStream = New MemoryStream(optimizeBitmap(DirectCast(oldImageBitmap.Clone(), System.Drawing.Bitmap), newSize.Width, newSize.Height, System.Drawing.Imaging.ImageFormat.Png, InterpolationMode1, SmoothingMode1, CompositingQuality1, imageAveragePixel, Nothing, autoResizeImages))
                                                                        If Not imageMask Is Nothing Then
                                                                            If imageDictionary.Get(PdfName.SMASK).IsIndirect Then
                                                                                PdfReader.KillIndirect(imageDictionary.GetAsIndirectObject(PdfName.SMASK))
                                                                            End If
                                                                            imageDictionary.Remove(PdfName.SMASK)
                                                                        End If
                                                                        If Not newImageStream Is Nothing Then
                                                                            If newImageStream.Length > 0 Then
                                                                                If newImageStream.CanSeek Then
                                                                                    newImageStream.Seek(0, SeekOrigin.Begin)
                                                                                End If
                                                                                Dim img As System.Drawing.Image = System.Drawing.Image.FromStream(newImageStream)
                                                                                Dim compressedImage As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(img, System.Drawing.Imaging.ImageFormat.Png)
                                                                                If Not imageMask Is Nothing Then
                                                                                    compressedImage.ImageMask = maskImageItext
                                                                                End If
                                                                                If Not compressedImage Is Nothing Then
                                                                                    PdfReader.KillIndirect(obj)
                                                                                    stamper.Writer.AddDirectImageSimple(compressedImage, DirectCast(obj, PRIndirectReference))
                                                                                End If
                                                                            Else
                                                                                Dim stopme As Boolean = False
                                                                            End If
                                                                        End If
                                                                    End Using
                                                                Catch ex As Exception
                                                                    Err.Clear()
                                                                End Try
                                                            End Using
                                                        Catch exOptimize As Exception
                                                            Err.Clear()
                                                        End Try
                                                    Else
                                                        Try
                                                            Dim stream1 As PRStream = DirectCast(xobject.GetAsStream(name), PRStream)
                                                            Dim imgObj1 As New iTextSharp.text.pdf.parser.PdfImageObject(stream1)
                                                            Dim oldBytes() As Byte = imgObj1.GetImageAsBytes()
                                                            Dim w As Single = imgObj1.GetDrawingImage.Width
                                                            Dim h As Single = imgObj1.GetDrawingImage.Height
                                                            Using sourceMS As New MemoryStream(oldBytes)
                                                                If sourceMS.CanSeek Then
                                                                    sourceMS.Seek(0, SeekOrigin.Begin)
                                                                End If
                                                                Try
                                                                    Using oldImageBitmap As System.Drawing.Bitmap = DirectCast(System.Drawing.Bitmap.FromStream(sourceMS, True, True), System.Drawing.Bitmap)
                                                                        Dim newSize As Size = Nothing
                                                                        Try
                                                                            Dim imgScale As Single = scaleFactor
                                                                            newSize = clsGetImageSize.getImageSize(reader, pgNumber, oldBytes.ToArray())
                                                                            If newSize = Nothing Then
                                                                                newSize = New Size(CInt(oldImageBitmap.Width * scaleFactor), CInt(oldImageBitmap.Height * scaleFactor))
                                                                                If Not imgObject.Get(PdfName.WIDTH) Is Nothing And Not imgObject.Get(PdfName.HEIGHT) Is Nothing Then
                                                                                    If imgObject.Get(PdfName.WIDTH).IsNumber And imgObject.Get(PdfName.HEIGHT).IsNumber Then
                                                                                        Dim widthImage As Integer = imgObject.GetAsNumber(PdfName.WIDTH).IntValue
                                                                                        Dim heightImage As Integer = imgObject.GetAsNumber(PdfName.HEIGHT).IntValue
                                                                                        Dim pageWidth As Integer = CInt(reader.GetPageSizeWithRotation(pgNumber).Width)
                                                                                        Dim pageHeight As Integer = CInt(reader.GetPageSizeWithRotation(pgNumber).Height)
                                                                                        If oldImageBitmap.Width > pageWidth Or oldImageBitmap.Height > pageHeight Then
                                                                                            imgScale = CInt((pageWidth / oldImageBitmap.Width) * scaleFactor)
                                                                                            newSize = New Size(CInt(oldImageBitmap.Width * imgScale), CInt(oldImageBitmap.Height * imgScale))
                                                                                        ElseIf oldImageBitmap.Width > pageWidth Or oldImageBitmap.Height > pageHeight Then
                                                                                            imgScale = CInt((pageHeight / oldImageBitmap.Height) * scaleFactor)
                                                                                            newSize = New Size(CInt(oldImageBitmap.Width * imgScale), CInt(oldImageBitmap.Height * imgScale))
                                                                                        Else
                                                                                            newSize = New Size(CInt(oldImageBitmap.Width * scaleFactor), CInt(oldImageBitmap.Height * scaleFactor))
                                                                                        End If
                                                                                        scaleFactorTemp = imgScale
                                                                                    End If
                                                                                End If
                                                                            End If
                                                                        Catch exGetWH As Exception
                                                                            Dim imgScale As Single = scaleFactor
                                                                            newSize = New Size(CInt(oldImageBitmap.Width * scaleFactor), CInt(oldImageBitmap.Height * scaleFactor))
                                                                            Err.Clear()
                                                                        End Try
                                                                        newImageStream = New MemoryStream(optimizeBitmap(DirectCast(oldImageBitmap.Clone(), System.Drawing.Bitmap), newSize.Width, newSize.Height, System.Drawing.Imaging.ImageFormat.Bmp, InterpolationMode1, SmoothingMode1, CompositingQuality1, imageAveragePixel, Nothing, autoResizeImages))
                                                                        If Not newImageStream Is Nothing Then
                                                                            If newImageStream.Length > 0 Then
                                                                                If newImageStream.CanSeek Then
                                                                                    newImageStream.Seek(0, SeekOrigin.Begin)
                                                                                End If
                                                                                Dim compressedImage As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(newImageStream)

                                                                                If Not compressedImage Is Nothing Then
                                                                                    PdfReader.KillIndirect(obj)
                                                                                    stamper.Writer.AddDirectImageSimple(compressedImage, DirectCast(obj, PRIndirectReference))
                                                                                End If
                                                                            Else
                                                                                Dim stopme As Boolean = False
                                                                            End If
                                                                        End If
                                                                    End Using
                                                                Catch ex As Exception
                                                                    Err.Clear()
                                                                End Try
                                                            End Using
                                                        Catch exOptimize As Exception
                                                            Err.Clear()
                                                        End Try
                                                    End If
                                                ElseIf imgObject.Get(PdfName.FILTER).Equals(PdfName.DCTDECODE) Then
                                                    Dim newImageStream As New MemoryStream
                                                    If Not imgObject.Get(PdfName.SMASK) Is Nothing Then
                                                        Try
                                                            Dim stream1 As PRStream = DirectCast(xobject.GetAsStream(name), PRStream)
                                                            Dim imgObj1 As New iTextSharp.text.pdf.parser.PdfImageObject(stream1)
                                                            Dim oldBytes() As Byte = imgObj1.GetImageAsBytes()

                                                            Dim imageDictionary As PdfDictionary = imgObj1.GetDictionary()
                                                            Dim w As Single = imgObj1.GetDrawingImage.Width
                                                            Dim h As Single = imgObj1.GetDrawingImage.Height
                                                            Using sourceMS As New MemoryStream(oldBytes)
                                                                If sourceMS.CanSeek Then
                                                                    sourceMS.Seek(0, SeekOrigin.Begin)
                                                                End If
                                                                Try
                                                                    Using oldImageBitmap As System.Drawing.Bitmap = DirectCast(System.Drawing.Bitmap.FromStream(sourceMS, True, True), System.Drawing.Bitmap)
                                                                        Dim newSize As Size = Nothing
                                                                        Try
                                                                            Dim imgScale As Single = scaleFactor
                                                                            newSize = clsGetImageSize.getImageSize(reader, pgNumber, oldBytes.ToArray())
                                                                            If newSize = Nothing Then
                                                                                newSize = New Size(CInt(oldImageBitmap.Width * scaleFactor), CInt(oldImageBitmap.Height * scaleFactor))
                                                                                If Not imgObject.Get(PdfName.WIDTH) Is Nothing And Not imgObject.Get(PdfName.HEIGHT) Is Nothing Then
                                                                                    If imgObject.Get(PdfName.WIDTH).IsNumber And imgObject.Get(PdfName.HEIGHT).IsNumber Then
                                                                                        Dim widthImage As Integer = imgObject.GetAsNumber(PdfName.WIDTH).IntValue
                                                                                        Dim heightImage As Integer = imgObject.GetAsNumber(PdfName.HEIGHT).IntValue
                                                                                        Dim pageWidth As Integer = CInt(reader.GetPageSizeWithRotation(pgNumber).Width)
                                                                                        Dim pageHeight As Integer = CInt(reader.GetPageSizeWithRotation(pgNumber).Height)
                                                                                        If oldImageBitmap.Width > pageWidth Or oldImageBitmap.Height > pageHeight Then
                                                                                            imgScale = CInt((pageWidth / oldImageBitmap.Width) * scaleFactor)
                                                                                            newSize = New Size(CInt(oldImageBitmap.Width * imgScale), CInt(oldImageBitmap.Height * imgScale))
                                                                                        ElseIf oldImageBitmap.Width > pageWidth Or oldImageBitmap.Height > pageHeight Then
                                                                                            imgScale = CInt((pageHeight / oldImageBitmap.Height) * scaleFactor)
                                                                                            newSize = New Size(CInt(oldImageBitmap.Width * imgScale), CInt(oldImageBitmap.Height * imgScale))
                                                                                        Else
                                                                                            newSize = New Size(CInt(oldImageBitmap.Width * scaleFactor), CInt(oldImageBitmap.Height * scaleFactor))
                                                                                        End If
                                                                                        scaleFactorTemp = imgScale
                                                                                    End If
                                                                                End If
                                                                            End If
                                                                        Catch exGetWH As Exception
                                                                            Dim imgScale As Single = scaleFactor
                                                                            newSize = New Size(CInt(oldImageBitmap.Width * scaleFactor), CInt(oldImageBitmap.Height * scaleFactor))
                                                                            Err.Clear()
                                                                        End Try
                                                                        Dim maskStream As PRStream = CType(imageDictionary.GetAsStream(PdfName.SMASK), PRStream)
                                                                        Dim imageMask As System.Drawing.Image = Nothing
                                                                        Dim imageMaskBitmap As System.Drawing.Bitmap = Nothing
                                                                        Dim imageMaskBytes() As Byte = Nothing
                                                                        Dim imageMaskStream As New System.IO.MemoryStream
                                                                        Dim maskImage As PdfImageObject = Nothing
                                                                        Dim maskImageItext As iTextSharp.text.Image = Nothing
                                                                        If (Not (maskStream) Is Nothing) Then
                                                                            maskImage = New PdfImageObject(maskStream)
                                                                            Try
                                                                                imageMask = System.Drawing.Image.FromStream(New MemoryStream(maskImage.GetImageAsBytes()), False, False)
                                                                            Catch ex As Exception
                                                                                Err.Clear()
                                                                                Try
                                                                                    If maskStream.Keys.Contains(PdfName.FILTER) Then
                                                                                        If maskStream.GetAsName(PdfName.SUBTYPE) Is PdfName.IMAGE Then
                                                                                            If maskStream.GetAsName(PdfName.FILTER) Is PdfName.JPXDECODE Then
                                                                                                If Not optimizeJPXDECODE Then
                                                                                                    GoTo SKIP_AHEAD
                                                                                                End If
                                                                                                Dim mMask As New MemoryStream(maskImage.GetImageAsBytes())
                                                                                                Dim dib As FIBITMAP = FreeImage.LoadFromStream(mMask)
                                                                                                If Not dib.IsNull Then
                                                                                                    Dim strFreeImage As New System.IO.MemoryStream
                                                                                                    imageMaskBitmap = FreeImage.GetBitmap(dib)
                                                                                                    If strFreeImage.CanSeek Then strFreeImage.Seek(0, SeekOrigin.Begin)
                                                                                                    Dim img As System.Drawing.Bitmap = oldImageBitmap.Clone
                                                                                                    Dim imgTemp As System.Drawing.Bitmap = New System.Drawing.Bitmap(img.Width, img.Height, PixelFormat.Format32bppArgb)
                                                                                                    For x As Integer = 0 To imageMaskBitmap.Width - 1
                                                                                                        For y As Integer = 0 To imageMaskBitmap.Height - 1
                                                                                                            Dim p As System.Drawing.Color = getAveragePixelColor_Shared(imageMaskBitmap.Clone, x, y, 1)
                                                                                                            Dim a As Integer = p.A
                                                                                                            Dim r As Integer = p.R
                                                                                                            Dim g As Integer = p.G
                                                                                                            Dim b As Integer = p.B
                                                                                                            Dim avg As Integer = (r + g + b) / 3
                                                                                                            If avg >= 128 Then
                                                                                                                imgTemp.SetPixel(x, y, img.GetPixel(x, y))
                                                                                                            Else
                                                                                                                imgTemp.SetPixel(x, y, Color.Transparent)
                                                                                                            End If
                                                                                                        Next y
                                                                                                    Next x
                                                                                                    newImageStream = New MemoryStream()
                                                                                                    imgTemp.Save(newImageStream, System.Drawing.Imaging.ImageFormat.Png)
                                                                                                    imageMask = Nothing
                                                                                                    imageMaskBitmap = Nothing
                                                                                                    imageMaskBytes = Nothing
                                                                                                    imageMaskStream = Nothing
                                                                                                    FreeImage.UnloadEx(dib)
                                                                                                End If
                                                                                            End If
                                                                                        End If
                                                                                    End If
                                                                                Catch ex2 As Exception
                                                                                    Err.Clear()
                                                                                End Try
                                                                            End Try
                                                                            If Not imageMaskStream Is Nothing Then
                                                                                If imageMaskStream.Length > 0 Then
                                                                                    If imageMaskStream.CanSeek Then
                                                                                        imageMaskStream.Seek(0, SeekOrigin.Begin)
                                                                                    End If
                                                                                    maskImageItext = iTextSharp.text.Image.GetInstance(imageMaskStream)
                                                                                    Try
                                                                                        If Not maskImageItext.IsMask And maskImageItext.IsMaskCandidate Then
                                                                                            maskImageItext.MakeMask()
                                                                                        End If
                                                                                    Catch exMask1 As Exception
                                                                                        Err.Clear()
                                                                                    End Try

                                                                                ElseIf Not imageMask Is Nothing Then
                                                                                    Try
                                                                                        maskImageItext = iTextSharp.text.Image.GetInstance(imageMask, Nothing, False)
                                                                                        maskImageItext.Normalize()
                                                                                        maskImageItext.SimplifyColorspace()
                                                                                        maskImageItext.Inverted = True
                                                                                        maskImageItext.Interpolation = True
                                                                                        If Not maskImageItext.IsMask And maskImageItext.IsMaskCandidate Then
                                                                                            maskImageItext.MakeMask()
                                                                                        End If
                                                                                    Catch exMask1 As Exception
                                                                                        Err.Clear()
                                                                                    End Try
                                                                                ElseIf Not imageMaskBytes Is Nothing Then
                                                                                    If imageMaskBytes.Length > 0 Then
                                                                                        maskImageItext = iTextSharp.text.Image.GetInstance(imageMaskBytes)
                                                                                        maskImageItext.MakeMask()
                                                                                    End If
                                                                                End If

                                                                            ElseIf Not imageMask Is Nothing Then
                                                                                Try
                                                                                    maskImageItext = iTextSharp.text.Image.GetInstance(imageMask, Nothing, False)
                                                                                    maskImageItext.Normalize()
                                                                                    maskImageItext.SimplifyColorspace()
                                                                                    maskImageItext.Inverted = True
                                                                                    maskImageItext.Interpolation = True
                                                                                    If Not maskImageItext.IsMask And maskImageItext.IsMaskCandidate Then
                                                                                        maskImageItext.MakeMask()
                                                                                    End If
                                                                                Catch exMask1 As Exception
                                                                                    Err.Clear()
                                                                                End Try
                                                                            ElseIf Not imageMaskBytes Is Nothing Then
                                                                                If imageMaskBytes.Length > 0 Then
                                                                                    maskImageItext = iTextSharp.text.Image.GetInstance(imageMaskBytes)
                                                                                    maskImageItext.MakeMask()
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Not maskImageItext Is Nothing Then
                                                                            newImageStream = New MemoryStream()
                                                                            DirectCast(oldImageBitmap.Clone(), System.Drawing.Bitmap).Save(newImageStream, System.Drawing.Imaging.ImageFormat.Jpeg)
                                                                        Else
                                                                            If newImageStream.Length <= 0 Then
                                                                                newImageStream = New MemoryStream(optimizeBitmap(DirectCast(oldImageBitmap.Clone(), System.Drawing.Bitmap), newSize.Width, newSize.Height, System.Drawing.Imaging.ImageFormat.Jpeg, InterpolationMode1, SmoothingMode1, CompositingQuality1, imageAveragePixel, Nothing, autoResizeImages))
                                                                            End If
                                                                        End If
                                                                        If Not imageMask Is Nothing Or Not imageMaskBytes Is Nothing Then
                                                                            If imageDictionary.Get(PdfName.SMASK).IsIndirect Then
                                                                                PdfReader.KillIndirect(imageDictionary.GetAsIndirectObject(PdfName.SMASK))
                                                                            End If
                                                                            imageDictionary.Remove(PdfName.SMASK)
                                                                        End If
                                                                        If Not newImageStream Is Nothing Then
                                                                            If newImageStream.Length > 0 Then
                                                                                If newImageStream.CanSeek Then
                                                                                    newImageStream.Seek(0, SeekOrigin.Begin)
                                                                                End If
                                                                                Dim img As System.Drawing.Image = System.Drawing.Image.FromStream(newImageStream)
                                                                                Dim compressedImage As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(img, System.Drawing.Imaging.ImageFormat.Png)
                                                                                If Not imageMask Is Nothing Or Not imageMaskBytes Is Nothing Then
                                                                                    compressedImage.ImageMask = maskImageItext
                                                                                End If
                                                                                If Not compressedImage Is Nothing Then
                                                                                    PdfReader.KillIndirect(obj)
                                                                                    stamper.Writer.AddDirectImageSimple(compressedImage, DirectCast(obj, PRIndirectReference))
                                                                                End If
                                                                            Else
                                                                                Dim stopme As Boolean = False
                                                                            End If
                                                                        End If
                                                                    End Using
                                                                Catch ex As Exception
                                                                    Err.Clear()
                                                                End Try
                                                            End Using
                                                        Catch exOptimize As Exception
                                                            Err.Clear()
                                                        End Try
SKIP_AHEAD:
                                                    Else
                                                        Try
                                                            Dim oldBytes As Byte() = PdfReader.GetStreamBytesRaw(DirectCast(imgObject, PRStream))
                                                            Using sourceMS As New MemoryStream(oldBytes)
                                                                If sourceMS.CanSeek Then
                                                                    sourceMS.Seek(0, SeekOrigin.Begin)
                                                                End If
                                                                Try
                                                                    Using oldImageBitmap As System.Drawing.Bitmap = DirectCast(System.Drawing.Bitmap.FromStream(sourceMS, True, True), System.Drawing.Bitmap)
                                                                        Dim newSize As Size = Nothing
                                                                        Try
                                                                            Dim imgScale As Single = scaleFactor
                                                                            newSize = clsGetImageSize.getImageSize(reader, pgNumber, oldBytes.ToArray())
                                                                            If newSize = Nothing Then
                                                                                newSize = New Size(CInt(oldImageBitmap.Width * scaleFactor), CInt(oldImageBitmap.Height * scaleFactor))
                                                                                If Not imgObject.Get(PdfName.WIDTH) Is Nothing And Not imgObject.Get(PdfName.HEIGHT) Is Nothing Then
                                                                                    If imgObject.Get(PdfName.WIDTH).IsNumber And imgObject.Get(PdfName.HEIGHT).IsNumber Then
                                                                                        Dim widthImage As Integer = imgObject.GetAsNumber(PdfName.WIDTH).IntValue
                                                                                        Dim heightImage As Integer = imgObject.GetAsNumber(PdfName.HEIGHT).IntValue
                                                                                        Dim pageWidth As Integer = CInt(reader.GetPageSizeWithRotation(pgNumber).Width)
                                                                                        Dim pageHeight As Integer = CInt(reader.GetPageSizeWithRotation(pgNumber).Height)
                                                                                        If oldImageBitmap.Width > pageWidth Or oldImageBitmap.Height > pageHeight Then
                                                                                            imgScale = CInt((pageWidth / oldImageBitmap.Width) * scaleFactor)
                                                                                            newSize = New Size(CInt(oldImageBitmap.Width * imgScale), CInt(oldImageBitmap.Height * imgScale))
                                                                                        ElseIf oldImageBitmap.Width > pageWidth Or oldImageBitmap.Height > pageHeight Then
                                                                                            imgScale = CInt((pageHeight / oldImageBitmap.Height) * scaleFactor)
                                                                                            newSize = New Size(CInt(oldImageBitmap.Width * imgScale), CInt(oldImageBitmap.Height * imgScale))
                                                                                        Else
                                                                                            newSize = New Size(CInt(oldImageBitmap.Width * scaleFactor), CInt(oldImageBitmap.Height * scaleFactor))
                                                                                        End If
                                                                                        scaleFactorTemp = imgScale
                                                                                    End If
                                                                                End If
                                                                            End If
                                                                        Catch exGetWH As Exception
                                                                            Dim imgScale As Single = scaleFactor
                                                                            newSize = New Size(CInt(oldImageBitmap.Width * scaleFactor), CInt(oldImageBitmap.Height * scaleFactor))
                                                                            Err.Clear()
                                                                        End Try
                                                                        newImageStream = New MemoryStream(optimizeBitmap(DirectCast(oldImageBitmap.Clone(), System.Drawing.Bitmap), newSize.Width, newSize.Height, System.Drawing.Imaging.ImageFormat.Jpeg, InterpolationMode1, SmoothingMode1, CompositingQuality1, imageAveragePixel, Nothing, autoResizeImages))
                                                                        If Not newImageStream Is Nothing Then
                                                                            If newImageStream.Length > 0 Then
                                                                                If newImageStream.CanSeek Then
                                                                                    newImageStream.Seek(0, SeekOrigin.Begin)
                                                                                End If
                                                                                Dim compressedImage As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(newImageStream.ToArray())
                                                                                If Not compressedImage Is Nothing Then
                                                                                    PdfReader.KillIndirect(obj)
                                                                                    stamper.Writer.AddDirectImageSimple(compressedImage, DirectCast(obj, PRIndirectReference))
                                                                                End If
                                                                            End If
                                                                        Else
                                                                            Dim stopme As Boolean = False
                                                                        End If
                                                                    End Using
                                                                Catch ex As Exception
                                                                    Err.Clear()
                                                                End Try
                                                            End Using
                                                        Catch exJPEG As Exception
                                                            Err.Clear()
                                                        End Try
                                                    End If
                                                ElseIf Not imgObject.Get(PdfName.FILTER) Is Nothing Then
                                                    Try
                                                        Dim strFilterName As String = imgObject.Get(PdfName.FILTER).ToString()
                                                        strFilterName = strFilterName
                                                    Catch ex As Exception
                                                        Err.Clear()
                                                    End Try
                                                End If

                                            End If
                                        Catch ex As Exception
                                            Err.Clear()
                                        End Try
                                    Else
                                        Dim blnStopMe As Boolean = True
                                    End If
                                End If
Goto_NextImage:
                            Next
                        End If
                    Next
                    stamper.Writer.CloseStream = False
                    stamper.Close()
                    stamper.Dispose()
                    Return fs.ToArray
                End Using
            End Using
        Catch ex As Exception
            Err.Clear()
        Finally
            Try
                reader.Close()
                reader.Dispose()
                reader = Nothing
            Catch ex2 As Exception
                Err.Clear()
            End Try
        End Try
        Return frmMain.getPDFBytes(reader)
    End Function
    Public Shared Function ConvertImageToBytes(ByVal image As System.Drawing.Image, ByVal compressionLevel As Long) As Byte()
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
    Public Shared Function GetEncoder(ByVal format As ImageFormat) As ImageCodecInfo
        Dim codecs As ImageCodecInfo() = ImageCodecInfo.GetImageDecoders()
        For Each codec As ImageCodecInfo In codecs
            If codec.FormatID = format.Guid Then
                Return codec
            End If
        Next
        Return Nothing
    End Function
    Public Shared Function ShrinkImage(ByVal sourceImage As System.Drawing.Image, ByVal scaleFactor As Single) As System.Drawing.Image
        Dim newWidth As Integer = Convert.ToInt32(sourceImage.Width * scaleFactor)
        Dim newHeight As Integer = Convert.ToInt32(sourceImage.Height * scaleFactor)
        Dim thumbnailBitmap As New System.Drawing.Bitmap(newWidth, newHeight)
        Using g As Graphics = Graphics.FromImage(thumbnailBitmap)
            g.CompositingQuality = CompositingQuality.HighQuality
            g.SmoothingMode = SmoothingMode.HighQuality
            g.PixelOffsetMode = PixelOffsetMode.None
            g.InterpolationMode = InterpolationMode.HighQualityBicubic
            Dim imageRectangle As New System.Drawing.Rectangle(0, 0, newWidth, newHeight)
            g.DrawImage(sourceImage, imageRectangle)
        End Using
        Return thumbnailBitmap
    End Function
    Public Shared Function ResizeBitmap(ByVal InputImage As System.Drawing.Bitmap, ByVal newWidth As Integer, ByVal newHeight As Integer) As System.Drawing.Bitmap
        Return New System.Drawing.Bitmap(InputImage, New Size(newWidth, newHeight))
    End Function
    Public Shared Function ResizeImage(ByVal InputImage As System.Drawing.Image, ByVal newWidth As Integer, ByVal newHeight As Integer) As System.Drawing.Image
        Return New System.Drawing.Bitmap(InputImage, New Size(newWidth, newHeight))
    End Function
End Class
