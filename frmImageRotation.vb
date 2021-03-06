Public Class frmImageRotation
    ''' <summary>
    ''' PdForms.net- Created by Nicholas Kowalewicz (www.PdForms.net)
    ''' Copyright 2017 NK-INC.COM All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Email Contact: support@nk-inc.ccom
    ''' Website: www.pdforms.net
    ''' </summary>

    Public CloseForm As Boolean = False
    Public rotType As System.Drawing.RotateFlipType = RotateFlipType.RotateNoneFlipNone
    Public img As System.Drawing.Image
    Public imgOriginal As System.Drawing.Image = Nothing
    Public imgScale As Single = 0.0F
    Public imgRect As RectangleF
    Public imgFormat As System.Drawing.Imaging.ImageFormat = System.Drawing.Imaging.ImageFormat.Png
    Public imgMaskBytes() As Byte = Nothing


    'Try
    '    If Not frmMainParent() Is Nothing Then
    '        If frmMainParent.GetType Is GetType(frmMain) Then
    '            If Not String.IsNullOrEmpty(frmMainParent.fpath) Then
    '                fn = System.IO.Path.GetDirectoryName(frmMainParent.fpath)
    '            End If
    '        End If
    '    End If
    'Catch ex As Exception
    '    Err.Clear()
    'End Try
    Public parentfrmMain As frmMain = Nothing
    Public Function frmMainParent(Optional thisForm As Form = Nothing) As frmMain
        Try
            If thisForm Is Nothing Then
                Try
                    If Not parentfrmMain Is Nothing Then
                        If parentfrmMain.GetType Is GetType(frmMain) Then
                            Return parentfrmMain
                        End If
                    Else
                        If Not Me.ParentForm Is Nothing Then
                            If (Me.ParentForm.GetType) Is GetType(frmMain) Then
                                parentfrmMain = Me.ParentForm
                                Return parentfrmMain
                            End If
                        ElseIf Not Me.Owner Is Nothing Then
                            If (Me.Owner.GetType) Is GetType(frmMain) Then
                                parentfrmMain = Me.Owner
                                Return parentfrmMain
                            End If
                        End If
                    End If
                Catch ex As Exception
                    Err.Clear()
                End Try
            Else
                If Not parentfrmMain Is Nothing Then
                    If parentfrmMain.GetType Is GetType(frmMain) Then
                        Return parentfrmMain
                    End If
                Else
                    If Not thisForm.ParentForm Is Nothing Then
                        If (thisForm.ParentForm.GetType) Is GetType(frmMain) Then
                            parentfrmMain = thisForm.ParentForm
                            Return parentfrmMain
                        End If
                    ElseIf Not thisForm.Owner Is Nothing Then
                        If (thisForm.Owner.GetType) Is GetType(frmMain) Then
                            parentfrmMain = thisForm.Owner
                            Return parentfrmMain
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
        Return Nothing
    End Function

    Public ReadOnly Property imgBytes() As Byte()
        Get
            Dim m As New System.IO.MemoryStream
            Try
                If Not imgFormat.GetHashCode() = System.Drawing.Imaging.ImageFormat.MemoryBmp().GetHashCode() Then
                    ImageRotation_PictureBox.Image.Save(m, imgFormat)
                Else
                    ImageRotation_PictureBox.Image.Save(m, System.Drawing.Imaging.ImageFormat.Png)
                End If
            Catch ex As Exception
                Err.Clear()
            End Try
            Return m.ToArray
        End Get
        'Set(ByVal value As Byte())
        '    ImageRotation_PictureBox.Image = Image.FromStream(New System.IO.MemoryStream(value))
        'End Set
    End Property

    Private Sub frmImageRotation_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            CloseForm = True
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub

    Dim cropX As Integer
    Dim cropY As Integer
    Dim cropWidth As Integer
    Dim cropHeight As Integer

    Dim oCropX As Integer
    Dim oCropY As Integer
    Dim cropBitmap As Bitmap

    Public cropPen As Pen
    Public cropPenSize As Integer = 4 '2
    Public cropDashStyle As Drawing2D.DashStyle = Drawing2D.DashStyle.Solid
    Public cropPenColor As Color = Color.Red
    'Private Sub RotateBtn_Click(sender As System.Object, e As EventArgs) Handles RotateBtn.Click

    '    ' RotateImage(PreviewPictureBox.Image, offset:=, angle:=90)
    '    ImageRotation_PictureBox.Image.RotateFlip(RotateFlipType.Rotate270FlipNone)
    '    'PreviewPictureBox.Image.RotateFlip(RotateFlipType.Rotate270FlipNone)
    '    '(45, PreviewPictureBox.Image)
    'End Sub
    Private Sub ImageRotation_PictureBox_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ImageRotation_PictureBox.MouseDown
        Try

            If e.Button = Windows.Forms.MouseButtons.Left Then

                cropX = e.X
                cropY = e.Y
                'cropPen = New Pen(cropPenColor, CSng((ImageRotation_PictureBox.Image.Width / ImageRotation_PictureBox.Width) * cropPenSize))
                cropPen = New Pen(cropPenColor, CSng((ImageRotation_PictureBox.Width / ImageRotation_PictureBox.Image.Width) * cropPenSize))
                cropPen.DashStyle = Drawing2D.DashStyle.DashDotDot
                Cursor = Cursors.Cross

            End If
            ImageRotation_PictureBox.Refresh()
        Catch exc As Exception
        End Try
    End Sub
    Dim tmppoint As Point
    Private Sub ImageRotation_PictureBox_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ImageRotation_PictureBox.MouseMove
        Try

            If ImageRotation_PictureBox.Image Is Nothing Then Exit Sub

            If e.Button = Windows.Forms.MouseButtons.Left Then

                ImageRotation_PictureBox.Refresh()
                cropWidth = e.X - cropX
                cropHeight = e.Y - cropY
                ImageRotation_PictureBox.CreateGraphics.DrawRectangle(cropPen, cropX, cropY, cropWidth, cropHeight)

            End If
            ' GC.Collect()

        Catch exc As Exception

            If Err.Number = 5 Then Exit Sub
        End Try

    End Sub
    Public Sub resizePictureBox()
        Try
            Dim scaleX As Single = 0, scaleY As Single = 0
            Dim imgW As Integer = 0, imgH As Integer = 0
            Dim pbW As Integer = 0, pbH As Integer = 0
            imgW = imgOriginal.Width
            imgH = imgOriginal.Height

            pbW = Panel1.Left
            pbH = Panel1.Height

            ImageRotation_PictureBox.Left = 0
            ImageRotation_PictureBox.Top = Panel1.Top

            Dim newH As Integer = 0, newW As Integer = 0
            If imgH > imgW Then
                ImageRotation_PictureBox.Height = pbH
                scaleY = pbH / imgH
                scaleX = scaleY 'imgW / scaleY
                newW = imgW * scaleX
                newH = pbH 'imgH * scaleX
            ElseIf imgW > imgH Then
                ImageRotation_PictureBox.Width = pbW
                scaleX = imgW / scaleY
                scaleY = scaleX 'imgH / pbH
                newW = pbW 'imgW * scaleX
                newH = imgH * scaleY
            End If
            ImageRotation_PictureBox.Width = newW
            ImageRotation_PictureBox.Height = newH

            ImageRotation_PictureBox.SizeMode = PictureBoxSizeMode.Zoom
            ImageRotation_PictureBox.Refresh()
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub ImageRotation_PictureBox_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ImageRotation_PictureBox.MouseUp
        Try
            Cursor = Cursors.Default
            Try

                If cropWidth < 1 Then
                    Exit Sub
                End If

                'ImageRotation_PictureBox.Refresh()
                cropWidth = e.X - cropX
                cropHeight = e.Y - cropY

                'Dim rect As Rectangle = New Rectangle(cropX, cropY, cropWidth, cropHeight)
                'Dim bit As Bitmap = New Bitmap(ImageRotation_PictureBox.Image, ImageRotation_PictureBox.Width, ImageRotation_PictureBox.Height)

                'cropBitmap = New Bitmap(cropWidth, cropHeight)
                'Dim g As Graphics = Graphics.FromImage(cropBitmap)
                'g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                'g.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
                'g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
                'g.DrawImage(bit, 0, 0, rect, GraphicsUnit.Pixel)
                '' g.DrawImage(bit, 0, 0, rect,GraphicsUnit.Pixel)
                ''PreviewPictureBox.Image = cropBitmap
                'ImageRotation_PictureBox.Image = cropBitmap

            Catch exc As Exception
            End Try
        Catch exc As Exception
        End Try
    End Sub
    'shareimprove this answer
    'Public Function imageCrop(bitmapCopy As Bitmap, rectangleCrop As RectangleF) As Bitmap
    '    Try
    '        Dim bm As Bitmap = bitmapCopy.Clone
    '        Dim bmOut As New Bitmap(rectangleCrop.Width, rectangleCrop.Height, bitmapCopy.PixelFormat)
    '        Using g As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(bmOut)
    '            g.DrawImage(bm, New Point() {New Point(0, 0)}, rectangleCrop, System.Drawing.GraphicsUnit.Pixel)
    '        End Using
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
    Dim xyPoint As New Point(0, 0)
    Private Sub frmImageRotation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ImageRotation_ImageRotation.SelectedIndex = 0
            ImageRotation_ImageFlip.SelectedIndex = 0
            ImageRotation_Dimensions_Resized.Visible = False
            xyPoint = New Point(ImageRotation_PictureBox.Location.X, ImageRotation_PictureBox.Location.Y)
            Me.Focus()
            Me.BringToFront()
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public Sub LoadPictureBox(ByVal imgImage As System.Drawing.Image)
        Try
            If Not imgImage Is Nothing Then
                img = DirectCast(imgImage.Clone, System.Drawing.Image)
                If imgOriginal Is Nothing Then
                    imgOriginal = DirectCast(img.Clone, System.Drawing.Image)
                    ImageRotation_Dimensions_Original.Text = String.Format("Original Dimensions: {0} x {1}", imgOriginal.Width, imgOriginal.Height)
                    ImageRotation_Dimensions_Resized.Text = String.Format("Resized Dimensions: {0} x {1}", imgOriginal.Width, imgOriginal.Height)
                    ImageRotation_chkResizeProportional.Checked = False
                    ImageRotation_txtResizeWidth.Text = imgOriginal.Width.ToString
                    ImageRotation_txtResizeHeight.Text = imgOriginal.Height.ToString
                    ImageRotation_chkResizeProportional.Checked = True
                End If
                ImageRotation_PictureBox.Image = DirectCast(img.Clone, System.Drawing.Image)
                resizePictureBox()
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public Sub LoadPictureBox(ByVal imgData() As Byte)
        Try
            If Not imgData Is Nothing Then
                If imgData.Length > 0 Then
                    img = System.Drawing.Image.FromStream(New System.IO.MemoryStream(imgData))
                    If imgOriginal Is Nothing Then
                        imgOriginal = DirectCast(img.Clone, System.Drawing.Image)
                        ImageRotation_Dimensions_Original.Text = String.Format("Original Dimensions: {0} x {1}", imgOriginal.Width, imgOriginal.Height)
                        ImageRotation_Dimensions_Resized.Text = String.Format("Resized Dimensions: {0} x {1}", imgOriginal.Width, imgOriginal.Height)
                        ImageRotation_chkResizeProportional.Checked = False
                        ImageRotation_txtResizeWidth.Text = imgOriginal.Width.ToString
                        ImageRotation_txtResizeHeight.Text = imgOriginal.Height.ToString
                        ImageRotation_chkResizeProportional.Checked = True
                        ImageRotation_PictureBox.Image = DirectCast(img.Clone, System.Drawing.Image)
                        resizePictureBox()
                    End If
                End If
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public Sub LoadPictureBox(ByVal imgPath As String)
        Try
            Dim imgData() As Byte = System.IO.File.ReadAllBytes(imgPath & "")
            If Not imgData Is Nothing Then
                If imgData.Length > 0 Then
                    img = System.Drawing.Image.FromStream(New System.IO.MemoryStream(imgData))
                    If imgOriginal Is Nothing Then
                        imgOriginal = DirectCast(img.Clone, System.Drawing.Image)
                        ImageRotation_Dimensions_Original.Text = String.Format("Original Dimensions: {0} x {1}", imgOriginal.Width, imgOriginal.Height)
                        ImageRotation_Dimensions_Resized.Text = String.Format("Resized Dimensions: {0} x {1}", imgOriginal.Width, imgOriginal.Height)
                        ImageRotation_chkResizeProportional.Checked = False
                        ImageRotation_txtResizeWidth.Text = imgOriginal.Width.ToString
                        ImageRotation_txtResizeHeight.Text = imgOriginal.Height.ToString
                        ImageRotation_chkResizeProportional.Checked = True
                    End If
                    ImageRotation_PictureBox.Image = DirectCast(img.Clone, System.Drawing.Image)
                    resizePictureBox()
                End If
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public Function getImageRotation() As System.Drawing.RotateFlipType
        Try
            Select Case ImageRotation_ImageRotation.SelectedIndex
                Case 0 'none
                    Select Case ImageRotation_ImageFlip.SelectedIndex
                        Case 0 'none
                            rotType = RotateFlipType.RotateNoneFlipNone
                        Case 1 'y
                            rotType = RotateFlipType.RotateNoneFlipY
                        Case 2 'x
                            rotType = RotateFlipType.RotateNoneFlipX
                        Case 3 'xy
                            rotType = RotateFlipType.RotateNoneFlipXY
                    End Select
                Case 1 '90
                    Select Case ImageRotation_ImageFlip.SelectedIndex
                        Case 0 'none
                            rotType = RotateFlipType.Rotate90FlipNone
                        Case 1 'y
                            rotType = RotateFlipType.Rotate90FlipY
                        Case 2 'x
                            rotType = RotateFlipType.Rotate90FlipX
                        Case 3 'xy
                            rotType = RotateFlipType.Rotate90FlipXY
                    End Select
                Case 2 '180
                    Select Case ImageRotation_ImageFlip.SelectedIndex
                        Case 0 'none
                            rotType = RotateFlipType.Rotate180FlipNone
                        Case 1 'y
                            rotType = RotateFlipType.Rotate180FlipY
                        Case 2 'x
                            rotType = RotateFlipType.Rotate180FlipX
                        Case 3 'xy
                            rotType = RotateFlipType.Rotate180FlipXY
                    End Select
                Case 3 '270
                    Select Case ImageRotation_ImageFlip.SelectedIndex
                        Case 0 'none
                            rotType = RotateFlipType.Rotate270FlipNone
                        Case 1 'y
                            rotType = RotateFlipType.Rotate270FlipY
                        Case 2 'x
                            rotType = RotateFlipType.Rotate270FlipX
                        Case 3 'xy
                            rotType = RotateFlipType.Rotate270FlipXY
                    End Select
                Case 4 '360
                    rotType = RotateFlipType.RotateNoneFlipX
                Case Else
                    Select Case ImageRotation_ImageFlip.SelectedIndex
                        Case 0 'none
                            rotType = RotateFlipType.RotateNoneFlipNone
                        Case 1 'y
                            rotType = RotateFlipType.RotateNoneFlipY
                        Case 2 'x
                            rotType = RotateFlipType.RotateNoneFlipX
                        Case 3 'xy
                            rotType = RotateFlipType.RotateNoneFlipXY
                    End Select
            End Select
        Catch ex As Exception
            Err.Clear()
        End Try
        Return rotType
    End Function


    Private Sub ImageRotation_btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImageRotation_btnOK.Click
        Try
            rotType = getImageRotation()
            cancelled = False
            CloseForm = True
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public cancelled As Boolean = False
    Public deleteImage As Boolean = False
    Private Sub ImageRotation_btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImageRotation_btnCancel.Click
        Try
            rotType = RotateFlipType.RotateNoneFlipNone
            cancelled = True
            CloseForm = True
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public WriteOnly Property showImageResizeOptions() As Boolean
        Set(ByVal value As Boolean)
            ImageRotation_Dimensions_Original.Visible = True
            ImageRotation_txtResizeHeight.Visible = value
            ImageRotation_txtResizeWidth.Visible = value
            ImageRotation_chkResizeProportional.Visible = value
            ImageRotation_btnResizeImage.Visible = value
            lblResizeHeight.Visible = value
            lblResizeWidth.Visible = value
            GroupBox1.Visible = value
            ImageRotation_CheckboxResize.Visible = False
            ImageRotation_Dimensions_Resized.Visible = False
        End Set
    End Property
    Public Sub ResizeRotateImage()
        Dim imgTemp As Image = Nothing
        Try
            Try
                If GroupBox1.Visible Then
                    If IsNumeric(Me.ImageRotation_txtResizeWidth.Text & "") And IsNumeric(Me.ImageRotation_txtResizeHeight.Text & "") Then
                        If CSng(Me.ImageRotation_txtResizeWidth.Text) > 0 And CSng(Me.ImageRotation_txtResizeHeight.Text) > 0 Then
                            If True = True Then
                                ImageRotation_Dimensions_Original.Text = String.Format("Original Dimensions: {0} x {1}", ImageRotation_PictureBox.Image.Width, ImageRotation_PictureBox.Image.Height)
                                If True = True Then
                                    'imgTemp = DirectCast(ImageRotation_PictureBox.Image.Clone, System.Drawing.Image)
                                    Dim imgStream As New System.IO.MemoryStream
                                    If imgFormat.GetHashCode() = System.Drawing.Imaging.ImageFormat.MemoryBmp().GetHashCode() Then
                                        imgFormat = System.Drawing.Imaging.ImageFormat.Png
                                        'Else
                                        '    imgFormat = System.Drawing.Imaging.ImageFormat.Jpeg
                                    End If

                                    ImageRotation_PictureBox.Image.Save(imgStream, imgFormat)
                                    imgTemp = System.Drawing.Image.FromStream(imgStream, True, False) 'ImageRotation_PictureBox.Image.Clone
                                    If Not rotType = RotateFlipType.RotateNoneFlipNone Then
                                        imgTemp.RotateFlip(rotType)
                                    End If
                                    Dim imgRectF As RectangleF = Nothing
                                    imgRectF = New RectangleF(0.0F, 0.0F, CSng(Me.ImageRotation_txtResizeWidth.Text) + 0, CSng(Me.ImageRotation_txtResizeHeight.Text) + 0)
                                    If Not imgRectF.Width = imgTemp.Width Or Not imgRectF.Height = imgTemp.Height Then
                                        imgTemp = clsPDFOptimization.ResizeImage(imgTemp, imgRectF.Width, imgRectF.Height).Clone
                                    End If
                                    'Dim newImage As System.Drawing.Image = New Bitmap(CInt(imgRectF.Width), CInt(imgRectF.Height))
                                    'Using graphicsHandle As Graphics = Graphics.FromImage(newImage)
                                    '    graphicsHandle.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                                    '    graphicsHandle.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
                                    '    graphicsHandle.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
                                    '    graphicsHandle.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
                                    '    graphicsHandle.DrawImage(imgTemp, 0.0F, 0.0F, imgRectF.Width, imgRectF.Height)
                                    'End Using
                                    Me.ImageRotation_PictureBox.Image = DirectCast(imgTemp.Clone, System.Drawing.Image)
                                    Me.ImageRotation_PictureBox.Update()
                                    ImageRotation_Dimensions_Resized.Visible = True
                                    ImageRotation_Dimensions_Resized.Text = String.Format("Resized Dimensions: {0} x {1}", imgRectF.Width, imgRectF.Height)
                                    'If Not rotType = RotateFlipType.RotateNoneFlipNone Then
                                    '    imgTemp = DirectCast(Me.ImageRotation_PictureBox.Image.Clone, System.Drawing.Image)
                                    '    imgTemp.RotateFlip(rotType)
                                    '    Me.ImageRotation_PictureBox.Image = DirectCast(imgTemp.Clone(), System.Drawing.Image)
                                    '    Me.ImageRotation_PictureBox.Update()
                                    'End If
                                    Return
                                    'End If
                                End If
                            End If
                        End If
                    End If
                Else
                    imgTemp = DirectCast(ImageRotation_PictureBox.Image.Clone, System.Drawing.Image)
                    imgTemp.RotateFlip(rotType)
                    Me.ImageRotation_PictureBox.Image = DirectCast(imgTemp.Clone(), System.Drawing.Image)
                End If
            Catch ex As Exception
                Err.Clear()
            Finally

            End Try

        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub ImageRotation_CheckboxResize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImageRotation_CheckboxResize.CheckedChanged
    End Sub
    Private Sub ImageRotation_txtResizeWidth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImageRotation_txtResizeWidth.TextChanged
        Try
            If ImageRotation_chkResizeProportional.Checked Then
                If IsNumeric(ImageRotation_txtResizeWidth.Text) Then
                    Dim sngDim As Single = Math.Abs(CSng(ImageRotation_txtResizeWidth.Text) + 0) + 0
                    If sngDim > 0 Then
                        imgScale = CSng(ImageRotation_PictureBox.Image.Width / ImageRotation_PictureBox.Image.Height)
                        ImageRotation_chkResizeProportional.Checked = False
                        ImageRotation_txtResizeHeight.Text = CStr(CInt(sngDim / imgScale)) & ""
                        ImageRotation_chkResizeProportional.Checked = True
                    End If
                End If
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub ImageRotation_txtResizeHeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImageRotation_txtResizeHeight.TextChanged
        Try
            If ImageRotation_chkResizeProportional.Checked Then
                If IsNumeric(ImageRotation_txtResizeHeight.Text) Then
                    Dim sngDim As Single = Math.Abs(CSng(ImageRotation_txtResizeHeight.Text) + 0) + 0
                    If sngDim > 0 Then
                        imgScale = CSng(ImageRotation_PictureBox.Image.Height / ImageRotation_PictureBox.Image.Width)
                        ImageRotation_chkResizeProportional.Checked = False
                        ImageRotation_txtResizeWidth.Text = CStr(CInt(sngDim / imgScale)) & ""
                        ImageRotation_chkResizeProportional.Checked = True
                    End If
                End If
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub ImageRotation_chkResizeProportional_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImageRotation_chkResizeProportional.CheckedChanged
    End Sub
    Private Sub ImageRotation_btnResizeImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImageRotation_btnResizeImage.Click
        ResizeRotateImage()
    End Sub
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub ImageRotation_PictureBox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImageRotation_PictureBox.Click

    End Sub

    Private Sub ImageRotation_PictureBox_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ImageRotation_PictureBox.DoubleClick
        SaveImage()
    End Sub

    Private Sub ImageRotation_btnResizeRevert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImageRotation_btnResizeRevert.Click
        If Not imgOriginal Is Nothing Then
            ImageRotation_PictureBox.Image = imgOriginal.Clone
            ImageRotation_Dimensions_Original.Text = String.Format("Original Dimensions: {0} x {1}", imgOriginal.Width, imgOriginal.Height)
            ImageRotation_Dimensions_Resized.Text = String.Format("Resized Dimensions: {0} x {1}", imgOriginal.Width, imgOriginal.Height)
            ImageRotation_chkResizeProportional.Checked = False
            ImageRotation_txtResizeWidth.Text = imgOriginal.Width.ToString
            ImageRotation_txtResizeHeight.Text = imgOriginal.Height.ToString
            ImageRotation_chkResizeProportional.Checked = True
        End If
    End Sub

    Public Sub SaveImage()
        Try
            Dim fn As String = Application.StartupPath.ToString.TrimEnd("\"c) & "\temp\", saveDialog As New System.Windows.Forms.SaveFileDialog
            saveDialog.Filter = "JPG|*.jpg|PNG|*.png|BMP|*.bmp|GIF|*.gif|TIFF|*.tif"
            saveDialog.DefaultExt = "jpg"
            saveDialog.FilterIndex = 0
            Try
                If Not frmMainParent() Is Nothing Then
                    If frmMainParent.GetType Is GetType(frmMain) Then
                        If Not String.IsNullOrEmpty(frmMainParent.fpath) Then
                            fn = System.IO.Path.GetDirectoryName(frmMainParent.fpath)
                        End If
                    End If
                End If
            Catch ex As Exception
                Err.Clear()
            End Try
            saveDialog.InitialDirectory = fn
            saveDialog.CheckFileExists = False
            saveDialog.CheckPathExists = True
            saveDialog.OverwritePrompt = True
            saveDialog.Title = "Save Image"
            Select Case saveDialog.ShowDialog(Me)
                Case Windows.Forms.DialogResult.Yes, Windows.Forms.DialogResult.OK
                    fn = saveDialog.FileName
                    Select Case System.IO.Path.GetExtension(fn).ToString.TrimStart(".")
                        Case "jpg"
                            ImageRotation_PictureBox.Image.Save(fn, System.Drawing.Imaging.ImageFormat.Jpeg)
                        Case "png"
                            ImageRotation_PictureBox.Image.Save(fn, System.Drawing.Imaging.ImageFormat.Png)
                        Case "bmp"
                            ImageRotation_PictureBox.Image.Save(fn, System.Drawing.Imaging.ImageFormat.Bmp)
                        Case "gif"
                            ImageRotation_PictureBox.Image.Save(fn, System.Drawing.Imaging.ImageFormat.Gif)
                        Case "tif"
                            ImageRotation_PictureBox.Image.Save(fn, System.Drawing.Imaging.ImageFormat.Tiff)
                    End Select
                Case Else
                    Return
            End Select
        Catch ex As Exception
            Err.Clear()
            Return
        End Try
    End Sub
    Private Sub SaveImageToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveImageToolStripMenuItem1.Click
        SaveImage()
    End Sub

    Private Sub LoadImageToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadImageToolStripMenuItem.Click
        LoadImage()
    End Sub
    Public Sub LoadImage()
        Try
            Dim fn As String = Application.StartupPath.ToString.TrimEnd("\"c) & "\temp\", saveDialog As New System.Windows.Forms.OpenFileDialog
            Try
                If Not frmMainParent() Is Nothing Then
                    If frmMainParent.GetType Is GetType(frmMain) Then
                        If Not String.IsNullOrEmpty(frmMainParent.fpath) Then
                            fn = System.IO.Path.GetDirectoryName(frmMainParent.fpath)
                        End If
                    End If
                End If
            Catch ex As Exception
                Err.Clear()
            End Try
            saveDialog.Filter = "ALL Images|*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.tif;*.tiff|JPG|*.jpg|JPEG|*.jpeg|PNG|*.png|BMP|*.bmp|GIF|*.gif|TIF|*.tif|TIFF|*.tiff"
            'saveDialog.DefaultExt = "jpg"
            saveDialog.FilterIndex = 0
            saveDialog.InitialDirectory = fn
            saveDialog.CheckFileExists = True
            saveDialog.CheckPathExists = True
            saveDialog.Title = "Load Image"
            Select Case saveDialog.ShowDialog(Me)
                Case Windows.Forms.DialogResult.Yes, Windows.Forms.DialogResult.OK
                    fn = saveDialog.FileName
                    Select Case System.IO.Path.GetExtension(fn).ToString.TrimStart(".")
                        Case "jpg", "jpeg"
                            img = System.Drawing.Image.FromFile(fn)
                            ImageRotation_PictureBox.Image = img.Clone
                            imgFormat = System.Drawing.Imaging.ImageFormat.Jpeg
                            ResizeRotateImage()
                            img = DirectCast(ImageRotation_PictureBox.Image.Clone(), System.Drawing.Image)
                        Case "png"
                            img = System.Drawing.Image.FromFile(fn)
                            ImageRotation_PictureBox.Image = img.Clone
                            imgFormat = System.Drawing.Imaging.ImageFormat.Png
                            ResizeRotateImage()
                            img = DirectCast(ImageRotation_PictureBox.Image.Clone(), System.Drawing.Image)
                        Case "bmp"
                            img = System.Drawing.Image.FromFile(fn)
                            ImageRotation_PictureBox.Image = img.Clone
                            imgFormat = System.Drawing.Imaging.ImageFormat.Bmp
                            ResizeRotateImage()
                            img = DirectCast(ImageRotation_PictureBox.Image.Clone(), System.Drawing.Image)
                        Case "gif"
                            img = System.Drawing.Image.FromFile(fn)
                            ImageRotation_PictureBox.Image = img.Clone
                            imgFormat = System.Drawing.Imaging.ImageFormat.Gif
                            ResizeRotateImage()
                            img = DirectCast(ImageRotation_PictureBox.Image.Clone(), System.Drawing.Image)
                        Case "tif", "tiff"
                            img = System.Drawing.Image.FromFile(fn)
                            ImageRotation_PictureBox.Image = img.Clone
                            imgFormat = System.Drawing.Imaging.ImageFormat.Tiff
                            ResizeRotateImage()
                            img = DirectCast(ImageRotation_PictureBox.Image.Clone(), System.Drawing.Image)
                    End Select
                Case Else
                    Return
            End Select
        Catch ex As Exception
            Err.Clear()
            Return
        End Try
    End Sub

    Public Shared Function IsValidEan13(eanBarcode As String) As Boolean
        Return IsValidEan(eanBarcode, 13)
    End Function

    Public Shared Function IsValidEan12(eanBarcode As String) As Boolean
        Return IsValidEan(eanBarcode, 12)
    End Function

    Public Shared Function IsValidEan14(eanBarcode As String) As Boolean
        Return IsValidEan(eanBarcode, 14)
    End Function

    Public Shared Function IsValidEan8(eanBarcode As String) As Boolean
        Return IsValidEan(eanBarcode, 8)
    End Function

    Private Shared Function IsValidEan(eanBarcode As String, length As Integer) As Boolean
        If eanBarcode.Length <> length Then
            Return False
        End If
        Dim allDigits = eanBarcode.[Select](Function(c) Integer.Parse(c.ToString(System.Globalization.CultureInfo.InvariantCulture))).ToArray()
        Dim s = If(length Mod 2 = 0, 3, 1)
        Dim s2 = If(s = 3, 1, 3)
        Return allDigits.Last() = (10 - (allDigits.Take(length - 1).[Select](Function(c, ci) c * (If(ci Mod 2 = 0, s, s2))).Sum() Mod 10)) Mod 10
    End Function

    Private Sub CopyImageToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyImageToolStripMenuItem.Click
        Try
            If Not ImageRotation_PictureBox.Image Is Nothing Then
                Clipboard.SetImage(ImageRotation_PictureBox.Image)
                MessageBox.Show(Me, "Copied image to clipboard.", "Clipboard", vbOKOnly, MsgBoxStyle.Information)
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If imgFormat.GetHashCode() = System.Drawing.Imaging.ImageFormat.MemoryBmp().GetHashCode() Then
            imgFormat = System.Drawing.Imaging.ImageFormat.Png
            'Else
            '    imgFormat = System.Drawing.Imaging.ImageFormat.Jpeg
        End If
        Dim imgStream As New System.IO.MemoryStream
        Dim imgTemp As System.Drawing.Image = imgOriginal.Clone
        imgTemp.Save(imgStream, imgFormat)
        'System.Drawing.Image.FromStream(imgStream, True, False) 'ImageRotation_PictureBox.Image.Clone
        Dim imgBytes() As Byte = imgStream.ToArray
        Dim frmDialogImageOptimization As dialogImageOptimization = New dialogImageOptimization(imgBytes, imgFormat, imgMaskBytes)
        frmDialogImageOptimization.frmMainParent = Nothing
        Try
            If Not imgMaskBytes Is Nothing Then
                If imgMaskBytes.Length > 0 Then
                    frmDialogImageOptimization.chkOptimizePngMasks.Checked = True
                    frmDialogImageOptimization.chkAutoResize.Checked = False
                    imgFormat = System.Drawing.Imaging.ImageFormat.Png
                End If
            End If
            Select Case frmDialogImageOptimization.ShowDialog(Me)
                Case Windows.Forms.DialogResult.OK, Windows.Forms.DialogResult.Yes
                    imgStream = New System.IO.MemoryStream(frmDialogImageOptimization.imgBytes)
                    ImageRotation_PictureBox.Image = System.Drawing.Image.FromStream(imgStream)
                    'ImageRotation_PictureBox.Image = DirectCast(imgTemp.Clone, System.Drawing.Image)
                    ImageRotation_PictureBox.Update()
                    ImageRotation_Dimensions_Resized.Visible = True
                    Dim imgRectF As System.Drawing.Rectangle = New System.Drawing.Rectangle(0, 0, ImageRotation_PictureBox.Image.Width, ImageRotation_PictureBox.Image.Height)
                    ImageRotation_Dimensions_Resized.Text = String.Format("Optimized Dimensions: {0} x {1}", imgRectF.Width, imgRectF.Height)
                Case Else
                    Return
            End Select
        Catch ex As Exception
            Err.Clear() 'TimeStampAdd(ex, debugMode) ' NK 2016-06-30() ' Throw Ex  ' NK2 '
        Finally
            If Not frmDialogImageOptimization Is Nothing Then
                If frmDialogImageOptimization.Visible Then frmDialogImageOptimization.Visible = False
                frmDialogImageOptimization.Dispose()
                frmDialogImageOptimization = Nothing
            End If
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            rotType = getImageRotation()
            cancelled = False
            CloseForm = True
            deleteImage = True
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub

    Private Sub ImageRotation_ImageRotation_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImageRotation_ImageRotation.SelectedIndexChanged
        If ImageRotation_ImageRotation.SelectedIndex = 4 Then
            ImageRotation_ImageFlip.SelectedIndex = 2
            ImageRotation_ImageFlip.Enabled = False
        Else
            ImageRotation_ImageFlip.Enabled = True
        End If
        rotType = getImageRotation()
        If Not img Is Nothing Then
            Dim imgTemp As System.Drawing.Image = DirectCast(img.Clone(), System.Drawing.Image)
            imgTemp.RotateFlip(rotType)
            ImageRotation_PictureBox.Image = DirectCast(imgTemp.Clone(), System.Drawing.Image)
        End If
    End Sub
    Private Sub ImageRotation_ImageFlip_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImageRotation_ImageFlip.SelectedIndexChanged
        rotType = getImageRotation()
        If Not img Is Nothing Then
            Dim imgTemp As System.Drawing.Image = DirectCast(img.Clone(), System.Drawing.Image)
            imgTemp.RotateFlip(rotType)
            ImageRotation_PictureBox.Image = DirectCast(imgTemp.Clone(), System.Drawing.Image)
        End If
    End Sub

    Private Sub PasteImageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasteImageToolStripMenuItem.Click
        Try
            If Not Clipboard.GetImage() Is Nothing Then
                ImageRotation_PictureBox.Image = Clipboard.GetImage()
                MessageBox.Show(Me, "Pasted image from clipboard.", "Clipboard", vbOKOnly, MsgBoxStyle.Information)
            Else
                MessageBox.Show(Me, "Clipboard has no image", "Clipboard", vbOKOnly, MsgBoxStyle.Information)
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub

    Private Sub CropImageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CropImageToolStripMenuItem.Click
        Try
            Cursor = Cursors.Default
            Try

                If cropWidth < 1 Then
                    Exit Sub
                End If
                Dim scale As Single = 1.0F
                Dim offsetXPoints As New Point(0, 0)
                'MsgBox(offsetY2 & "," & scaleX2)
                Dim scaleX2 As Single = 0.0F, scaleY As Single = 0.0F, scaleX As Single = 0.0F
                Dim offsetY As Single = 0.0F, offsetX As Single = 0.0F
                If ImageRotation_PictureBox.Image.Height > ImageRotation_PictureBox.Image.Width Then
                    scaleY = ImageRotation_PictureBox.Height / ImageRotation_PictureBox.Image.Height
                    offsetX = (ImageRotation_PictureBox.Width - (scaleY * ImageRotation_PictureBox.Image.Width)) / 2
                    scale = CSng(ImageRotation_PictureBox.Image.Height / (ImageRotation_PictureBox.Height))
                ElseIf ImageRotation_PictureBox.Image.Height < ImageRotation_PictureBox.Image.Width Then
                    scaleX = ImageRotation_PictureBox.Width / ImageRotation_PictureBox.Image.Width
                    offsetY = (ImageRotation_PictureBox.Height - (scaleX * ImageRotation_PictureBox.Image.Height)) / 2
                    scale = CSng(ImageRotation_PictureBox.Image.Width / ImageRotation_PictureBox.Width)
                End If
                Dim rect As Rectangle = New Rectangle((cropX * scale) - offsetX, (cropY - offsetY) * scale, cropWidth * scale, cropHeight * scale)
                Dim destRect As Rectangle = New Rectangle(0, 0, rect.Width, rect.Height)
                Dim bit As Bitmap = ImageRotation_PictureBox.Image.Clone
                Using cropBitmap As Bitmap = New Bitmap(rect.Width, rect.Height)
                    Using g As Graphics = Graphics.FromImage(cropBitmap)
                        g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                        g.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
                        g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
                        g.DrawImage(bit, destRect, rect, GraphicsUnit.Pixel)
                        ' g.DrawImage(bit, 0, 0, rect,GraphicsUnit.Pixel)
                        'PreviewPictureBox.Image = cropBitmap
                        g.Dispose()
                    End Using
                    'imgOriginal = cropBitmap.Clone
                    ImageRotation_PictureBox.Image = cropBitmap.Clone 'imgOriginal
                    ImageRotation_PictureBox.Refresh()
                    resizePictureBox()
                End Using
            Catch exc As Exception
                Err.Clear()
            End Try
        Catch exc As Exception
            Err.Clear()
        End Try
    End Sub

    Private Sub frmImageRotation_ResizeEnd(sender As Object, e As EventArgs) Handles Me.ResizeEnd
        resizePictureBox()
    End Sub
End Class
