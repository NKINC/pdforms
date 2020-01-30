Imports System.Windows.Forms
Imports System.Drawing
Imports System.IO
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.Threading.Tasks
Class dialogWatermark
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 NK-INC.COM All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>
    Public watermarkBitmap As Bitmap = Nothing
    Public watermarkData() As Byte
    Public watermarkText As String
    Public watermarkWidth As Single = 0, watermarkHeight As Single = 0
    Public watermarkTextOnly As Boolean = False
    Public pdfBytesOriginal As Byte()
    Public pdfBytesWatermark As Byte()
    Public frmParent As Form
    Private Function AddWatermarkText(bytes As Byte(), bf As BaseFont, text As String, angle As Single, fontSize As Single, fontColor As BaseColor) As Byte()
        Using ms = New MemoryStream(10 * 1024)
            Using reader = New PdfReader(bytes)
                Using stamper = New PdfStamper(reader, ms)
                    Dim times As Integer = reader.NumberOfPages
                    For i As Integer = 1 To times
                        Dim dc = stamper.GetOverContent(i)
                        AddWaterMark(dc, text, bf, fontSize, angle, fontColor, reader.GetPageSizeWithRotation(i))
                    Next
                    stamper.Close()
                End Using
            End Using
            Return ms.ToArray()
        End Using
    End Function
    Private Function AddWatermarkTextToPage(bytes As Byte(), pageIndex As Integer, bf As BaseFont, text As String, angle As Single, fontSize As Single, fontColor As BaseColor) As Byte()
        Using ms = New MemoryStream()
            Using reader = New PdfReader(bytes)
                Using stamper = New PdfStamper(reader, ms)
                    Dim dc = stamper.GetOverContent(CInt(pageIndex + 1))
                    AddWaterMark(dc, text, bf, fontSize, angle, fontColor, reader.GetPageSizeWithRotation(CInt(pageIndex + 1)))
                    stamper.Close()
                End Using
            End Using
            Return ms.ToArray()
        End Using
    End Function
    Private Function AddWatermarkImage(bytes As Byte(), bf As BaseFont, text As String, angle As Single, fontSize As Single, fontColor As BaseColor) As Byte()
        Using ms = New MemoryStream(10 * 1024)
            Using reader = New PdfReader(bytes)
                Using stamper = New PdfStamper(reader, ms)
                    Dim times As Integer = reader.NumberOfPages
                    For i As Integer = 1 To times
                        Dim dc = stamper.GetOverContent(i)
                        AddWaterMark(dc, text, bf, fontSize, angle, fontColor, reader.GetPageSizeWithRotation(i))
                    Next
                    stamper.Close()
                End Using
            End Using
            Return ms.ToArray()
        End Using
    End Function
    Private Function AddWatermarkImageToPage(bytes As Byte(), pageIndex As Integer, bf As BaseFont, text As String, angle As Single, fontSize As Single, fontColor As BaseColor) As Byte()
        Using ms = New MemoryStream()
            Using reader = New PdfReader(bytes)
                Using stamper = New PdfStamper(reader, ms)
                    Dim dc = stamper.GetOverContent(CInt(pageIndex + 1))
                    AddWaterMark(dc, text, bf, fontSize, angle, fontColor, reader.GetPageSizeWithRotation(CInt(pageIndex + 1)))
                    stamper.Close()
                End Using
            End Using
            Return ms.ToArray()
        End Using
    End Function
    Public Sub AddWaterMark(dc As PdfContentByte, text As String, font As BaseFont, fontSize As Single, angle As Single, color As BaseColor, realPageSize As iTextSharp.text.Rectangle, Optional rect As iTextSharp.text.Rectangle = Nothing)
        Dim gstate = New PdfGState()
        dc.SaveState()
        dc.SetGState(gstate)
        dc.SetColorFill(color)
        dc.BeginText()
        dc.SetFontAndSize(font, fontSize)
        Dim ps = If(rect, realPageSize)
        Dim x = (ps.Right + ps.Left) / 2
        Dim y = (ps.Bottom + ps.Top) / 2
        dc.ShowTextAligned(Element.ALIGN_CENTER, text, x, y, angle)
        dc.EndText()
        dc.RestoreState()
    End Sub
    Public Function AddWaterMark(dc As PdfContentByte, imgWatermark As System.Drawing.Image, font As BaseFont, fontSize As Single, angle As Single, color As BaseColor, realPageSize As iTextSharp.text.Rectangle, Optional rect As iTextSharp.text.Rectangle = Nothing) As Byte()
        If frmParent.GetType() Is GetType(frmMain) Then
        End If
        Return Nothing
    End Function
    Public Function AddImageToPage(pdfBytes() As Byte, pageIndex As Integer, ByVal img As System.Drawing.Image, ByVal GetOverContent As Boolean, rectPDFWatermark As System.Drawing.RectangleF, rectPDF As System.Drawing.RectangleF) As Byte()
        If img Is Nothing Then
            Return pdfBytes
        ElseIf img.Width <= 0 And img.Height <= 0 Then
            Return pdfBytes
        End If
        Try
            Dim jpg As System.Drawing.Image = DirectCast(img.Clone(), System.Drawing.Image)
            Dim jpgStream As New MemoryStream
            Dim frmImgRot As New frmImageRotation
            Try
                jpg = frmImgRot.ImageRotation_PictureBox.Image.Clone
            Catch ex As Exception
                jpg = frmImgRot.ImageRotation_PictureBox.Image.Clone
            Finally
            End Try
            jpg.Save(jpgStream, System.Drawing.Imaging.ImageFormat.Png)
            If jpgStream.CanSeek Then
                jpgStream.Seek(0, SeekOrigin.Begin)
            End If
            jpg.Dispose()
            jpg = Nothing
            Dim bmp As New Bitmap(jpgStream)
            Dim page_width As Single = 0, page_height As Single = 0
            Dim rectImage As iTextSharp.text.Rectangle = Nothing
            Using mPDF As New MemoryStream()
                Try
                    page_width = rectPDF.Width
                    page_height = rectPDF.Height
                    rectImage = New iTextSharp.text.Rectangle(page_width, page_height)
                Catch ex As Exception
                    page_width = bmp.Width
                    page_height = bmp.Height
                End Try
                Dim r As New iTextSharp.text.Rectangle(CInt(page_width), CInt(page_height))
                Dim bytes() As Byte = pdfBytes
                Dim pdfReaderDoc As New PdfReader(bytes)
                Dim stamp As New PdfStamper(pdfReaderDoc, mPDF)
                Dim pages As Integer = bmp.GetFrameCount(Imaging.FrameDimension.Page)
                Dim image As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(bmp, System.Drawing.Imaging.ImageFormat.Png)
                If Not rectPDFWatermark = Nothing Then
                    If rectPDFWatermark.Width <= 5 Or rectPDFWatermark.Height <= 5 Then
                        image.SetAbsolutePosition(0, 0)
                        image.ScaleAbsoluteHeight(stamp.Reader.GetPageSizeWithRotation(CInt(pageIndex + 1)).Height)
                        image.ScaleAbsoluteWidth(stamp.Reader.GetPageSizeWithRotation(CInt(pageIndex + 1)).Width)
                    Else
                        image.SetAbsolutePosition(rectPDFWatermark.Left, rectPDFWatermark.Top - rectPDFWatermark.Height)
                        image.ScaleAbsoluteHeight(rectPDFWatermark.Height)
                        image.ScaleAbsoluteWidth(rectPDFWatermark.Width)
                    End If
                Else
                    image.SetAbsolutePosition(0, 0)
                    image.ScaleAbsoluteHeight(stamp.Reader.GetPageSizeWithRotation(CInt(pageIndex + 1)).Height)
                    image.ScaleAbsoluteWidth(stamp.Reader.GetPageSizeWithRotation(CInt(pageIndex + 1)).Width)
                End If
                If GetOverContent Then
                    stamp.GetOverContent(CInt(pageIndex + 1)).AddImage(image)
                Else
                    stamp.GetUnderContent(CInt(pageIndex + 1)).AddImage(image)
                End If
                stamp.Writer.CloseStream = False
                stamp.Close()
                stamp.Dispose()
                stamp = Nothing
                Return mPDF.ToArray()
            End Using
        Catch ex As Exception
            Throw ex
        Finally
        End Try
        Return Nothing
    End Function
    Public Sub New()
        InitializeComponent()
    End Sub
    Public Sub New(ByRef frmMain1 As frmMain)
        InitializeComponent()
        frmParent = frmMain1
    End Sub
    Private Function ResizeImage(newWidth As Integer, newHeight As Integer, imgPhoto As System.Drawing.Image) As System.Drawing.Image
        Dim sourceWidth As Integer = imgPhoto.Width
        Dim sourceHeight As Integer = imgPhoto.Height
        If sourceWidth < sourceHeight Then
            Dim buff As Integer = newWidth
            newWidth = newHeight
            newHeight = buff
        End If
        Dim sourceX As Integer = 0, sourceY As Integer = 0, destX As Integer = 0, destY As Integer = 0
        Dim nPercent As Single = 0, nPercentW As Single = 0, nPercentH As Single = 0
        nPercentW = (CSng(newWidth) / CSng(sourceWidth))
        nPercentH = (CSng(newHeight) / CSng(sourceHeight))
        If nPercentH < nPercentW Then
            nPercent = nPercentH
            destX = System.Convert.ToInt16((newWidth - (sourceWidth * nPercent)) / 2)
        Else
            nPercent = nPercentW
            destY = System.Convert.ToInt16((newHeight - (sourceHeight * nPercent)) / 2)
        End If
        Dim destWidth As Integer = CInt(sourceWidth * nPercent)
        Dim destHeight As Integer = CInt(sourceHeight * nPercent)
        Dim bmPhoto As New Bitmap(newWidth, newHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
        bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution)
        Dim grPhoto As Graphics = Graphics.FromImage(bmPhoto)
        grPhoto.FillRectangle(Brushes.White, 0, 0, bmPhoto.Width, bmPhoto.Height)
        grPhoto.SmoothingMode = Drawing2D.SmoothingMode.None
        grPhoto.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
        grPhoto.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
        If destX > 0 Or destY > 0 Then
            grPhoto.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBilinear
        Else
            grPhoto.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
        End If
        grPhoto.DrawImage(imgPhoto, New System.Drawing.Rectangle(destX, destY, destWidth, destHeight), New System.Drawing.Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Point)
        grPhoto.Dispose()
        Return bmPhoto
    End Function
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Dim frm As frmMain = DirectCast(Me.frmParent, frmMain)
        frm.DeleteTempFilesImageCache()
        frm.Session = frm.EncryptPDFDocument(addWatermark(""))
        frm.A0_LoadPDF()
        frm.refreshPDFImage()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Private Sub picBarcode_Click(sender As Object, e As EventArgs) Handles previewPicWatermark.Click
    End Sub
    Private Sub appearance_OpacityScroll_Scroll(sender As Object, e As EventArgs) Handles appearance_OpacityScroll.Scroll
        If Not appearance_OpacityScroll.Value = appearance_OpacityUpDwn.Value Then
            appearance_OpacityUpDwn.Value = appearance_OpacityScroll.Value
            displayPreviewDelay()
        End If
    End Sub
    Private Sub appearance_chkScaleRelativeToTargetPage_CheckedChanged(sender As Object, e As EventArgs) Handles appearance_chkScaleRelativeToTargetPage.CheckedChanged
        appearance_ScalePercentUpDwn.Enabled = appearance_chkScaleRelativeToTargetPage.Checked
    End Sub
    Private Sub appearance_OpacityUpDwn_ValueChanged(sender As Object, e As EventArgs) Handles appearance_OpacityUpDwn.ValueChanged
        If Not appearance_OpacityScroll.Value = appearance_OpacityUpDwn.Value Then
            appearance_OpacityScroll.Value = appearance_OpacityUpDwn.Value
            displayPreviewDelay()
        End If
    End Sub
    Private Sub source_radText_CheckedChanged(sender As Object, e As EventArgs) Handles source_radText.CheckedChanged
        source_pnlText.Enabled = source_radText.Checked
        source_pnlFile.Enabled = Not source_radText.Checked
    End Sub
    Private Sub source_radFile_CheckedChanged(sender As Object, e As EventArgs) Handles source_radFile.CheckedChanged
        source_pnlText.Enabled = source_radText.Checked
        source_pnlFile.Enabled = Not source_radText.Checked
    End Sub
    Private Sub source_textPicFontColor_Click(sender As Object, e As EventArgs) Handles source_textPicFontColor.Click
        Try
            ColorDialog1.Color = source_textPicFontColor.BackColor
            Select Case ColorDialog1.ShowDialog(Me)
                Case DialogResult.OK, DialogResult.Yes
                    source_textPicFontColor.BackColor = ColorDialog1.Color
                    displayPreviewDelay()
                Case Else
            End Select
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
    End Sub
    Private Sub dialogWatermark_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            pdfDelayPause = True
            cmbSavedSettings.SelectedIndex = 0
            source_textCmbFontSize.SelectedIndex = 0
            position_VDistUnits.SelectedIndex = 0
            position_VDistAlign.SelectedIndex = 1
            position_HDistUnits.SelectedIndex = 0
            position_HDistAlign.SelectedIndex = 1
            Label20.Text = " of " & DirectCast(frmParent, frmMain).btnPage.Items.Count.ToString()
            previewPageNumberUpDwn.Minimum = 1
            previewPageNumberUpDwn.Maximum = DirectCast(frmParent, frmMain).btnPage.Items.Count
            DirectCast(frmParent, frmMain).LoadFontsList(source_textCmbFont)
            If source_textCmbFont.Items.Count > 0 Then
                source_textCmbFont.SelectedIndex = 0
            End If
            source_textCmbFontSize.Text = "200"
            source_textCmbFont.SelectedItem = "Arial Black"
            position_HDistAlign.SelectedIndex = 1
            pdfDelayPause = False
            displayPreviewDelay()
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public Enum convertUnits
        Pixels = 0
        Inches = 1
        Percent = 2
        Centimeters = 3
        Millimeters = 4
        Picas = 5
        Points = 6
    End Enum
    Public Function convertToPixels(qty As Single, fromUnits As convertUnits) As Single
        Select Case fromUnits
            Case convertUnits.Pixels
                Return qty * 1
            Case convertUnits.Inches
                Return qty * 96
            Case convertUnits.Centimeters
                Return qty * 37.79527559055
            Case convertUnits.Millimeters
                Return qty * 3.779527559055
            Case convertUnits.Picas
                Return qty * 16
            Case convertUnits.Points
                Return qty * 1.333333333333
        End Select
    End Function
    Public Function convertFromPixels(qty As Single, toUnits As convertUnits) As Single
        Select Case toUnits
            Case convertUnits.Pixels
                Return qty * 1
            Case convertUnits.Inches
                Return qty * 0.01041666666666
            Case convertUnits.Centimeters
                Return qty * 0.02645833333333
            Case convertUnits.Millimeters
                Return qty * 0.2645833333333
            Case convertUnits.Picas
                Return qty * 0.0625
            Case convertUnits.Points
                Return qty / 0.75
        End Select
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        displayPreview()
    End Sub
    Dim pdfPageBytes() As Byte = Nothing
    Dim pdfPage As Long = -1
    Public pdfDelay As DateTime = DateTime.Now
    Public pdfDelayPause As Boolean = True
    Private Sub tmrDelayPreview_Tick(sender As Object, e As EventArgs) Handles tmrDelayPreview.Tick
        Try
            If pdfDelayPause Then Return
            If pdfDelay <= DateTime.Now Then
                tmrDelayPreview.Enabled = False
                displayPreview()
                Return
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public Sub displayPreviewDelay(Optional lngMilSec As Long = 1000)
        tmrDelayPreview.Enabled = False
        pdfDelay = DateTime.Now.AddMilliseconds(lngMilSec)
        tmrDelayPreview.Interval = lngMilSec + 10
        tmrDelayPreview.Enabled = True
    End Sub
    Public Sub displayPreview()
        Try
            Dim frm As frmMain = DirectCast(frmParent, frmMain)
            Dim r As PdfReader = Nothing
            If Not pdfPage = previewPageNumberUpDwn.Value Or pdfPageBytes Is Nothing Then
                pdfPageBytes = frm.Session
                r = New PdfReader(pdfPageBytes, frm.getBytes(frm.pdfOwnerPassword))
                r.SelectPages(previewPageNumberUpDwn.Value.ToString())
                pdfPage = previewPageNumberUpDwn.Value
                pdfPageBytes = frm.getPDFBytes(r)
            Else
                r = New PdfReader(pdfPageBytes)
            End If
            Dim pdfPageBytes2() As Byte = AddWatermark(pdfPageBytes, "")
            Dim img As Bitmap = Bitmap.FromStream(New MemoryStream(frm.A0_LoadImageGhostScript(pdfPageBytes2, frm.pdfOwnerPassword, 1, r.GetPageSizeWithRotation(1).Width, r.GetPageSizeWithRotation(1).Height, False)))
            previewPicWatermark.Image = img.Clone
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public pdfBytes() As Byte = Nothing
    Public Function addWatermark(Optional selectPages As String = "") As Byte()
        Try
            Dim frm As frmMain = DirectCast(frmParent, frmMain)
            pdfBytes = frm.Session
            Dim r As New PdfReader(pdfBytes, frm.getBytes(frm.pdfOwnerPassword))
            If Not selectPages = "" Then
                r.SelectPages(selectPages)
                pdfBytes = frm.getPDFBytes(r)
                pdfBytes = AddWatermark(pdfBytes, "")
            Else
                pdfBytes = AddWatermark(pdfBytes, frm.pdfOwnerPassword)
            End If
            Return pdfBytes
        Catch ex As Exception
            Err.Clear()
        End Try
        Return Nothing
    End Function
    Private Sub appearance_rotation45Neg_CheckedChanged(sender As Object, e As EventArgs) Handles appearance_rotation45Neg.CheckedChanged
        If appearance_rotation45Neg.Checked Then appearance_rotationCustomUpDwn.Value = -45
    End Sub
    Private Sub appearance_rotation0_CheckedChanged(sender As Object, e As EventArgs) Handles appearance_rotation0.CheckedChanged
        If appearance_rotation0.Checked Then appearance_rotationCustomUpDwn.Value = 0
    End Sub
    Private Sub appearance_rotation45_CheckedChanged(sender As Object, e As EventArgs) Handles appearance_rotation45.CheckedChanged
        If appearance_rotation45.Checked Then appearance_rotationCustomUpDwn.Value = 45
    End Sub
    Private Sub appearance_rotationCustom_CheckedChanged(sender As Object, e As EventArgs) Handles appearance_rotationCustom.CheckedChanged
    End Sub
    Dim fnWatermark As String = ""
    Private Sub source_fileBtnBrowse_Click(sender As Object, e As EventArgs) Handles source_fileBtnBrowse.Click
        Try
            Select Case OpenFileDialog1.ShowDialog(Me)
                Case DialogResult.Yes, DialogResult.OK
                    fnWatermark = OpenFileDialog1.FileName
                    picWatermark.Image = System.Drawing.Image.FromFile(fnWatermark)
                    displayPreviewDelay()
            End Select
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public Function RotateImg(ByVal bmpimage As Bitmap, ByVal angle As Single) As Bitmap
        Dim w As Integer = bmpimage.Width
        Dim h As Integer = bmpimage.Height
        Dim pf As System.Drawing.Imaging.PixelFormat = Nothing
        pf = bmpimage.PixelFormat
        Dim tempImg As New Bitmap(w, h, pf)
        Dim g As Graphics = Graphics.FromImage(tempImg)
        g.DrawImageUnscaled(bmpimage, 1, 1)
        g.Dispose()
        Dim path As New System.Drawing.Drawing2D.GraphicsPath()
        path.AddRectangle(New RectangleF(0.0F, 0.0F, w, h))
        Dim mtrx As New System.Drawing.Drawing2D.Matrix()
        mtrx.Rotate(angle)
        Dim rct As RectangleF = path.GetBounds(mtrx)
        Dim newImg As New Bitmap(Convert.ToInt32(rct.Width), Convert.ToInt32(rct.Height), pf)
        g = Graphics.FromImage(newImg)
        g.TranslateTransform(-rct.X, -rct.Y)
        g.RotateTransform(angle)
        g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
        g.DrawImageUnscaled(tempImg, 0, 0)
        g.Dispose()
        tempImg.Dispose()
        Return newImg
    End Function
    Public Shared Function ResizeImage(ByVal image As System.Drawing.Image, ByVal size As Size, Optional ByVal preserveAspectRatio As Boolean = True) As System.Drawing.Image
        Dim newWidth As Integer
        Dim newHeight As Integer
        If preserveAspectRatio Then
            Dim originalWidth As Integer = image.Width
            Dim originalHeight As Integer = image.Height
            Dim percentWidth As Single = CSng(size.Width) / CSng(originalWidth)
            Dim percentHeight As Single = CSng(size.Height) / CSng(originalHeight)
            Dim percent As Single = If(percentHeight < percentWidth,
                percentHeight, percentWidth)
            newWidth = CInt(originalWidth * percent)
            newHeight = CInt(originalHeight * percent)
        Else
            newWidth = size.Width
            newHeight = size.Height
        End If
        Dim newImage As System.Drawing.Image = New Bitmap(newWidth, newHeight)
        Using graphicsHandle As Graphics = Graphics.FromImage(newImage)
            graphicsHandle.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight)
        End Using
        Return newImage
    End Function
    Private Sub source_textTxtText_TextChanged(sender As Object, e As EventArgs) Handles source_textTxtText.TextChanged
        displayPreviewDelay()
    End Sub
    Private Sub source_textCmbFont_SelectedIndexChanged(sender As Object, e As EventArgs) Handles source_textCmbFont.SelectedIndexChanged
        displayPreviewDelay()
    End Sub
    Private Sub previewPageNumberUpDwn_ValueChanged(sender As Object, e As EventArgs) Handles previewPageNumberUpDwn.ValueChanged
        displayPreviewDelay()
    End Sub
    Private Sub source_textRadFontAlignLeft_CheckedChanged(sender As Object, e As EventArgs) Handles source_textRadFontAlignLeft.CheckedChanged
        displayPreviewDelay()
    End Sub
    Private Sub source_textRadFontAlignCenter_CheckedChanged(sender As Object, e As EventArgs) Handles source_textRadFontAlignCenter.CheckedChanged
        displayPreviewDelay()
    End Sub
    Private Sub source_textRadFontAlignRight_CheckedChanged(sender As Object, e As EventArgs) Handles source_textRadFontAlignRight.CheckedChanged
        displayPreviewDelay()
    End Sub
    Private Sub appearance_rotationCustomUpDwn_ValueChanged(sender As Object, e As EventArgs) Handles appearance_rotationCustomUpDwn.ValueChanged
        displayPreviewDelay()
    End Sub
    Private Sub appearance_radLocationBehindPage_CheckedChanged(sender As Object, e As EventArgs) Handles appearance_radLocationBehindPage.CheckedChanged
        displayPreviewDelay()
    End Sub
    Private Sub appearance_radLocationOnTop_CheckedChanged(sender As Object, e As EventArgs) Handles appearance_radLocationOnTop.CheckedChanged
        displayPreviewDelay()
    End Sub
    Private Sub position_VDistNumberUpDwn_ValueChanged(sender As Object, e As EventArgs) Handles position_VDistNumberUpDwn.ValueChanged
        displayPreviewDelay()
    End Sub
    Private Sub position_VDistUnits_SelectedIndexChanged(sender As Object, e As EventArgs) Handles position_VDistUnits.SelectedIndexChanged
        displayPreviewDelay()
    End Sub
    Private Sub position_VDistAlign_SelectedIndexChanged(sender As Object, e As EventArgs) Handles position_VDistAlign.SelectedIndexChanged
        displayPreviewDelay()
    End Sub
    Private Sub position_HDistNumberUpDwn_ValueChanged(sender As Object, e As EventArgs) Handles position_HDistNumberUpDwn.ValueChanged
        displayPreviewDelay()
    End Sub
    Private Sub position_HDistUnits_SelectedIndexChanged(sender As Object, e As EventArgs) Handles position_HDistUnits.SelectedIndexChanged
        displayPreviewDelay()
    End Sub
    Private Sub position_HDistAlign_SelectedIndexChanged(sender As Object, e As EventArgs) Handles position_HDistAlign.SelectedIndexChanged
        displayPreviewDelay()
    End Sub
    Private Sub source_textCmbFontSize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles source_textCmbFontSize.SelectedIndexChanged
        displayPreviewDelay()
    End Sub
    Public Function AddWatermark(pdfBytes() As Byte, pdfOwnerPassword As String) As Byte()
        Dim reader As New PdfReader(pdfBytes, DirectCast(frmParent, frmMain).getBytes(pdfOwnerPassword))
        Dim n As Integer = reader.NumberOfPages
        Dim m As New MemoryStream
        Dim stamper As New PdfStamper(reader, m)
        stamper.RotateContents = False
        Dim gs1 As New PdfGState()
        gs1.FillOpacity = CSng(appearance_OpacityScroll.Value / 100)
        Dim over As PdfContentByte = Nothing
        Dim pagesize As iTextSharp.text.Rectangle
        Dim x As Single, y As Single
        For i As Integer = 1 To n
            pagesize = reader.GetPageSize(i)
            Dim alignMent As Integer = Element.ALIGN_CENTER
            If source_textRadFontAlignLeft.Checked Then
                alignMent = Element.ALIGN_LEFT
            ElseIf source_textRadFontAlignCenter.Checked Then
                alignMent = Element.ALIGN_CENTER
            ElseIf source_textRadFontAlignRight.Checked Then
                alignMent = Element.ALIGN_RIGHT
            End If
            If source_radText.Checked Then
                Dim defaultFont As iTextSharp.text.Font = Nothing
                Try
                    Select Case source_textCmbFont.SelectedIndex
                        Case 0
                            defaultFont = FontFactory.GetFont(BaseFont.COURIER, BaseFont.WINANSI, False)
                        Case 1
                            defaultFont = FontFactory.GetFont(BaseFont.COURIER_BOLD, BaseFont.CP1252, True)
                        Case 2
                            defaultFont = FontFactory.GetFont(BaseFont.COURIER_BOLDOBLIQUE, BaseFont.WINANSI, False)
                        Case 3
                            defaultFont = FontFactory.GetFont(BaseFont.COURIER_OBLIQUE, BaseFont.WINANSI, False)
                        Case 4
                            defaultFont = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.WINANSI, False)
                        Case 5
                            defaultFont = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.WINANSI, False)
                        Case 6
                            defaultFont = FontFactory.GetFont(BaseFont.HELVETICA_BOLDOBLIQUE, BaseFont.WINANSI, False)
                        Case 7
                            defaultFont = FontFactory.GetFont(BaseFont.HELVETICA_OBLIQUE, BaseFont.WINANSI, False)
                        Case 8
                            defaultFont = FontFactory.GetFont(BaseFont.TIMES_ROMAN, BaseFont.WINANSI, False)
                        Case 9
                            defaultFont = FontFactory.GetFont(BaseFont.TIMES_BOLD, BaseFont.WINANSI, False)
                        Case 10
                            defaultFont = FontFactory.GetFont(BaseFont.TIMES_ITALIC, BaseFont.WINANSI, False)
                        Case 11
                            defaultFont = FontFactory.GetFont(BaseFont.TIMES_BOLDITALIC, BaseFont.WINANSI, False)
                        Case 12
                            defaultFont = FontFactory.GetFont(BaseFont.SYMBOL, BaseFont.WINANSI, False)
                        Case 13
                            defaultFont = FontFactory.GetFont(BaseFont.ZAPFDINGBATS, BaseFont.WINANSI, False)
                        Case Else
                            Try
                                defaultFont = New iTextSharp.text.Font(FontFactory.GetFont(source_textCmbFont.Items(source_textCmbFont.SelectedIndex).ToString(), CSng(source_textCmbFontSize.Text), iTextSharp.text.Font.NORMAL, New BaseColor(source_textPicFontColor.BackColor)))
                            Catch exCreateFontEmbeded As Exception
                                Err.Clear()
                            End Try
                    End Select
                    defaultFont.Size = CSng(source_textCmbFontSize.Text)
                    defaultFont.Color = New BaseColor(source_textPicFontColor.BackColor)
                Catch ex As Exception
                    Throw ex
                End Try
                If appearance_chkScaleRelativeToTargetPage.Checked Then
                    Dim scale As Single = appearance_ScalePercentUpDwn.Value / 100
                    Dim width As Single = pagesize.Width, height As Single = pagesize.Height
                    Dim fontSize As Integer = 1
                    Do While ((width * scale) >= (defaultFont.BaseFont.GetWidthPoint(source_textTxtText.Text, fontSize))) And ((height * scale) >= (defaultFont.BaseFont.GetAscentPoint(source_textTxtText.Text, fontSize) - defaultFont.BaseFont.GetDescentPoint(source_textTxtText.Text, fontSize)))
                        Try
                            fontSize += 1
                            defaultFont.Size = CSng(fontSize)
                        Catch ex As Exception
                            Throw ex
                        End Try
                    Loop
                    fontSize -= 1
                    defaultFont.Size = CSng(fontSize)
                    source_textCmbFontSize.Text = fontSize
                End If
                Select Case position_VDistAlign.SelectedIndex
                    Case 0
                        y = (pagesize.Top()) - ((defaultFont.BaseFont.GetAscentPoint(source_textTxtText.Text, CSng(source_textCmbFontSize.Text))) - (defaultFont.BaseFont.GetDescentPoint(source_textTxtText.Text, CSng(source_textCmbFontSize.Text))))
                        If position_VDistNumberUpDwn.Value <> 0 Then
                            y = y - convertToPixels(position_VDistNumberUpDwn.Value, position_VDistUnits.SelectedIndex)
                        End If
                    Case 1
                        y = ((pagesize.Top() + pagesize.Bottom()) / 2) - ((defaultFont.BaseFont.GetAscentPoint(source_textTxtText.Text, CSng(source_textCmbFontSize.Text)) - defaultFont.BaseFont.GetDescentPoint(source_textTxtText.Text, CSng(source_textCmbFontSize.Text))) / 2)
                        If position_VDistNumberUpDwn.Value <> 0 Then
                            y = y - convertToPixels(position_VDistNumberUpDwn.Value, position_VDistUnits.SelectedIndex)
                        End If
                    Case 2
                        y = (pagesize.Bottom())
                        If position_VDistNumberUpDwn.Value <> 0 Then
                            y = y - convertToPixels(position_VDistNumberUpDwn.Value, position_VDistUnits.SelectedIndex)
                        End If
                End Select
                Select Case position_HDistAlign.SelectedIndex
                    Case 0
                        x = ((pagesize.Left()) + convertToPixels((defaultFont.BaseFont.GetWidthPoint(source_textTxtText.Text, CSng(source_textCmbFontSize.Text))), convertUnits.Points) / 2.5)
                        If position_HDistNumberUpDwn.Value <> 0 Then
                            x = x - convertToPixels(position_HDistNumberUpDwn.Value, position_HDistUnits.SelectedIndex)
                        End If
                    Case 1
                        x = (pagesize.Left() + pagesize.Right()) / 2
                        If position_HDistNumberUpDwn.Value <> 0 Then
                            x = x - convertToPixels(position_HDistNumberUpDwn.Value, position_HDistUnits.SelectedIndex)
                        End If
                    Case 2
                        x = ((pagesize.Right()) - convertToPixels(defaultFont.BaseFont.GetWidthPoint(source_textTxtText.Text, CSng(source_textCmbFontSize.Text)), convertUnits.Points) / 2.5)
                        If position_HDistNumberUpDwn.Value <> 0 Then
                            x = x - convertToPixels(position_HDistNumberUpDwn.Value, position_HDistUnits.SelectedIndex)
                        End If
                End Select
                If appearance_radLocationOnTop.Checked Then
                    over = stamper.GetOverContent(i)
                Else
                    over = stamper.GetUnderContent(i)
                End If
                over.SaveState()
                over.SetGState(gs1)
                Dim p As New iTextSharp.text.Phrase(source_textTxtText.Text, defaultFont)
                ColumnText.ShowTextAligned(over, alignMent, p, x, y, appearance_rotationCustomUpDwn.Value)
            Else
                Dim bitmapWatermark As Bitmap = New Bitmap(picWatermark.Image.Clone(), CInt(picWatermark.Image.Width * CSng(source_fileAbsScale.Value / 100)), CInt(picWatermark.Image.Height * CSng(source_fileAbsScale.Value / 100)))
                Dim imgWatermark As System.Drawing.Image = Nothing
                If appearance_chkScaleRelativeToTargetPage.Checked Then
                    Dim scale As Single = appearance_ScalePercentUpDwn.Value / 100
                    Dim width As Single = pagesize.Width, height As Single = pagesize.Height
                    bitmapWatermark = ResizeImage(bitmapWatermark, New Drawing.Size(CInt(scale * width), CInt(scale * height)), True)
                End If
                If appearance_rotationCustomUpDwn.Value <> 0 Then
                    imgWatermark = RotateImg(bitmapWatermark, appearance_rotationCustomUpDwn.Value)
                Else
                    imgWatermark = bitmapWatermark
                End If
                Dim img__1 As iTextSharp.text.Image = Nothing
                Select Case Path.GetExtension(fnWatermark).ToString.TrimStart("."c)
                    Case "jpeg", "jpg"
                        img__1 = iTextSharp.text.Image.GetInstance(imgWatermark, System.Drawing.Imaging.ImageFormat.Jpeg)
                    Case "png"
                        img__1 = iTextSharp.text.Image.GetInstance(imgWatermark, System.Drawing.Imaging.ImageFormat.Png)
                    Case "gif"
                        img__1 = iTextSharp.text.Image.GetInstance(imgWatermark, System.Drawing.Imaging.ImageFormat.Gif)
                    Case "bmp"
                        img__1 = iTextSharp.text.Image.GetInstance(imgWatermark, System.Drawing.Imaging.ImageFormat.Bmp)
                    Case "tiff", "tif"
                        img__1 = iTextSharp.text.Image.GetInstance(imgWatermark, System.Drawing.Imaging.ImageFormat.Tiff)
                End Select
                If img__1 Is Nothing Then Return Nothing
                Dim w As Single = img__1.ScaledWidth()
                Dim h As Single = img__1.ScaledHeight()
                Select Case position_VDistAlign.SelectedIndex
                    Case 0
                        y = (pagesize.Top()) - h / 2
                        If position_VDistNumberUpDwn.Value <> 0 Then
                            y = y - convertToPixels(position_VDistNumberUpDwn.Value, position_VDistUnits.SelectedIndex)
                        End If
                    Case 1
                        y = ((pagesize.Top() + pagesize.Bottom()) / 2)
                        If position_VDistNumberUpDwn.Value <> 0 Then
                            y = y - convertToPixels(position_VDistNumberUpDwn.Value, position_VDistUnits.SelectedIndex)
                        End If
                    Case 2
                        y = (pagesize.Bottom()) + h / 2
                        If position_VDistNumberUpDwn.Value <> 0 Then
                            y = y - convertToPixels(position_VDistNumberUpDwn.Value, position_VDistUnits.SelectedIndex)
                        End If
                End Select
                Select Case position_HDistAlign.SelectedIndex
                    Case 0
                        x = (pagesize.Left()) + w / 2
                        If position_HDistNumberUpDwn.Value <> 0 Then
                            x = x - convertToPixels(position_HDistNumberUpDwn.Value, position_HDistUnits.SelectedIndex)
                        End If
                    Case 1
                        x = (pagesize.Left() + pagesize.Right()) / 2
                        If position_HDistNumberUpDwn.Value <> 0 Then
                            x = x - convertToPixels(position_HDistNumberUpDwn.Value, position_HDistUnits.SelectedIndex)
                        End If
                    Case 2
                        x = (pagesize.Right()) - w / 2
                        If position_HDistNumberUpDwn.Value <> 0 Then
                            x = x - convertToPixels(position_HDistNumberUpDwn.Value, position_HDistUnits.SelectedIndex)
                        End If
                End Select
                If appearance_radLocationOnTop.Checked Then
                    over = stamper.GetOverContent(i)
                Else
                    over = stamper.GetUnderContent(i)
                End If
                over.SaveState()
                over.SetGState(gs1)
                over.AddImage(img__1, w, 0, 0, h, x - (w / 2), y - (h / 2))
            End If
            over.RestoreState()
        Next
        stamper.Writer.CloseStream = False
        stamper.Close()
        reader.Close()
        Return m.ToArray
    End Function
    Private Sub source_textCmbFontSize_TextChanged(sender As Object, e As EventArgs) Handles source_textCmbFontSize.TextChanged
        displayPreviewDelay()
    End Sub
End Class
