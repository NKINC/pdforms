Public Class clsUserRect
    ''' <summary>
    ''' PdForms.net- Created by Nicholas Kowalewicz (www.PdForms.net)
    ''' Copyright 2017 NK-INC.COM All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Email Contact: support@nk-inc.ccom
    ''' Website: www.pdforms.net
    ''' </summary>

#Region "PUBLIC DECLARATIONS"
    Public mPictureBox As PictureBox
    Public imgPic As Image
    Public _rect As RectangleF = Nothing
    Public allowDeformingDuringMovement As Boolean = False
    Public mIsClick As Boolean = False
    Public oldX As Single
    Public oldY As Single
    Public sizeNodeRect As Integer = 10 '7
    Public mBmp As Bitmap = Nothing
    Public nodeSelected As PosSizableRect = PosSizableRect.None
    Public angle As Integer = 30
    Public penWidth As Integer = 2
    Public penColor As System.Drawing.Color = Drawing.Color.FromArgb(255, 255, 0, 0)
    Public pauseDraw As Boolean = False
    Public _highLightFieldName As String = ""
    Public rectOld As System.Drawing.RectangleF
    Public rectBackup As System.Drawing.RectangleF
    Public nodeSelectedTmp As PosSizableRect = PosSizableRect.None
    Public frm As frmMain
#End Region
#Region "PRIVATE DECLARATIONS"
    Private tmpRect As RectangleF = Nothing
    Private rectScreenTemp As RectangleF = Nothing
#End Region
    Public Sub DisposeMe()
        Try
            mPictureBox = Nothing 'PictureBox
            imgPic = Nothing 'Image
            _rect = Nothing 'RectangleF = Nothing
            allowDeformingDuringMovement = Nothing 'Boolean = False
            mIsClick = Nothing 'Boolean = False
            frm.mMove = Nothing 'Boolean = False
            oldX = Nothing 'Single
            oldY = Nothing 'Single
            sizeNodeRect = Nothing 'Integer = 7
            mBmp = Nothing 'Bitmap = Nothing
            nodeSelected = Nothing 'PosSizableRect = PosSizableRect.None
            angle = Nothing 'Integer = 30
            penWidth = Nothing 'Integer = 2
            penColor = Nothing 'System.Drawing.Color = Drawing.Color.FromArgb(255, 255, 0, 0)
            pauseDraw = Nothing 'Boolean = False
            _highLightFieldName = Nothing 'String = ""
            rectOld = Nothing 'System.Drawing.RectangleF
            rectBackup = Nothing 'System.Drawing.RectangleF
            nodeSelectedTmp = Nothing 'PosSizableRect = PosSizableRect.None
            frm = Nothing 'frmMain
            tmpRect = Nothing 'RectangleF = Nothing
            rectScreenTemp = Nothing 'RectangleF = Nothing
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    ''' <summary>
    ''' PDF Rectangle
    ''' </summary>
    ''' <returns></returns>
    Public Property rect() As RectangleF
        Get
            Return _rect
        End Get
        Set(ByVal value As RectangleF)
            _rect = value
            rectScreenTemp = rectScreen()
        End Set
    End Property
    ''' <summary>
    ''' iTextSharp.text.Rectangle
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property rect_iTextRectangle() As iTextSharp.text.Rectangle
        Get
            Return New iTextSharp.text.Rectangle(_rect.Left, frm.getPDFHeight() - _rect.Bottom, _rect.Right, frm.getPDFHeight() - _rect.Top)
        End Get
    End Property
    ''' <summary>
    ''' iTextSharp.text.pdf.PdfRectangle
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property rect_iTextPdfRectangle() As iTextSharp.text.pdf.PdfRectangle
        Get
            Return New iTextSharp.text.pdf.PdfRectangle(_rect.Left, frm.getPDFHeight() - _rect.Bottom, _rect.Right, frm.getPDFHeight() - _rect.Top)
        End Get
    End Property
    ''' <summary>
    ''' Screen Rectangle
    ''' </summary>
    ''' <returns></returns>
    Public Function rectScreen() As RectangleF
        If Not tmpRect = rect Then
            tmpRect = rect
            rectScreenTemp = Nothing
        End If
        If rect = Nothing Then
            Return Nothing
        End If
        rectScreenTemp = frm.getRectangleScreen(tmpRect)
        Return rectScreenTemp
    End Function
    'Public Function rectPDF() As RectangleF
    '    Return (rect)
    'End Function

    ''' <summary>
    ''' PDF Rectangle reversed
    ''' </summary>
    ''' <returns></returns>
    Public Function rectPDFReversed() As iTextSharp.text.Rectangle
        Try
            'Return frm.getRectanglePDF(rect)
            'Dim r As New RectangleF(CSng(frm.btnLeft.Text) * frm.getPercent(), CSng(frm.btnTop.Text) * frm.getPercent(), CSng(frm.btnRight.Text) * frm.getPercent(), CSng(frm.btnBottom.Text) * frm.getPercent())
            'Dim r As New RectangleF(CSng(frm.btnLeft.Text), CSng(frm.btnTop.Text), CSng(frm.btnRight.Text), CSng(frm.btnBottom.Text))
            'rect = r
            'Dim r As New iTextSharp.text.Rectangle(CSng(frm.btnLeft.Text), CSng(frm.btnBottom.Text), CSng(frm.btnRight.Text), CSng(frm.btnTop.Text)) 'Dim r As New iTextSharp.text.Rectangle(CSng(cUserRect.rect.Left), CSng(cUserRect.rect.Bottom), CSng(cUserRect.rect.Right), CSng(cUserRect.rect.Top))
            'rect = New RectangleF(r.Left, frm.getPDFHeight() - r.Bottom, r.Right, frm.getPDFHeight() - r.Top)
            'r = frm.GetFieldPositionsReverse(frm.Session(), r)
            'Return (New iTextSharp.text.Rectangle(r.Left, r.Bottom, r.Right, r.Top))
            Dim r As RectangleF = rect
            Return (New iTextSharp.text.Rectangle(r.Left, frm.getPDFHeight() - r.Bottom, r.Right, frm.getPDFHeight() - r.Top))
        Catch ex As Exception
            Err.Clear()
        End Try
    End Function
    Public Enum PosSizableRect
        UpMiddle
        LeftMiddle
        LeftBottom
        LeftUp
        RightUp
        RightMiddle
        RightBottom
        BottomMiddle
        None
        Middle
    End Enum
    Public Sub New()
        rect = New System.Drawing.RectangleF
        mIsClick = False
    End Sub
    Public Sub New(ByVal r As RectangleF, ByRef frm1 As frmMain)
        rect = r
        mIsClick = False
        frm = frm1
    End Sub
    Public Sub DrawPictureBoxImageBox()
        Try
            'Dim bmp As New Bitmap(imgPic.Width, imgPic.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
            'g.FillRectangle(New SolidBrush(Color.Black), 0, 0, bmp.Width, bmp.Height)
            'g.Dispose()
            Dim img As System.Drawing.Image = New System.Drawing.Bitmap(imgPic.Width, imgPic.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
            Dim g As Graphics = Graphics.FromImage(img)
            g = Graphics.FromImage(img)
            Dim attr As New System.Drawing.Imaging.ImageAttributes
            attr.SetColorKey(Color.Black, Color.Black)
            Dim dstRect As New System.Drawing.Rectangle(0, 0, imgPic.Width, imgPic.Height)
            g.DrawImage(imgPic, dstRect, 0, 0, imgPic.Width, imgPic.Height, GraphicsUnit.Pixel, attr)
            If pauseDraw = False Then
                If rect = Nothing Then Return
                If rect.Width <= 1 And rect.Height <= 1 Then Return
                If True = True Then 'If pauseDraw = False Then
                    Dim tmpScreenRect As RectangleF = rectScreen()
                    If tmpScreenRect = Nothing Then Return
                    If tmpScreenRect.Width <= 1 And tmpScreenRect.Height <= 1 Then Return
                    Dim p As New Pen(Color.Red)
                    p.Width = penWidth
                    g.DrawRectangle(p, CSng(tmpScreenRect.X), CSng(tmpScreenRect.Y), CSng(tmpScreenRect.Width), CSng(tmpScreenRect.Height))
                    For Each pos As PosSizableRect In [Enum].GetValues(GetType(PosSizableRect))
                        g.FillRectangle(New SolidBrush(penColor), frm.getRectangleScreen(GetRect(pos)))
                    Next
                End If
                g.Dispose()
                If pauseDraw = False Then
                    mPictureBox.BackColor = Color.Transparent
                    mPictureBox.Image = DirectCast(img.Clone(), System.Drawing.Image)
                End If
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public Function DrawPictureBoxImageBoxImage(ByRef g As System.Drawing.Graphics, ByVal rect1 As System.Drawing.RectangleF) As System.Drawing.Graphics
        Try
            If pauseDraw = False Then
                rect = rect1
                Dim tmpScreenRect As RectangleF = rectScreen()
                If tmpScreenRect = Nothing Then Return g
                If tmpScreenRect.Width <= 1 And tmpScreenRect.Height <= 1 Then Return g
                Dim p As New Pen(Color.Red)
                p.Width = penWidth
                g.DrawRectangle(p, CSng(tmpScreenRect.X), CSng(tmpScreenRect.Y), CSng(tmpScreenRect.Width), CSng(tmpScreenRect.Height))
                For Each pos As PosSizableRect In [Enum].GetValues(GetType(PosSizableRect))
                    g.FillRectangle(New SolidBrush(penColor), frm.getRectangleScreen(GetRect(pos)))
                Next
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
        Return g
    End Function
    Public Function DrawPictureBoxImageBoxImageScreen(ByRef g As System.Drawing.Graphics, ByVal rect1 As System.Drawing.RectangleF) As System.Drawing.Graphics
        Try
            If pauseDraw = False Then
                rect = frm.getRectanglePDF(rect1)
                Dim tmpScreenRect As RectangleF = rect1 'rectScreen()
                If tmpScreenRect = Nothing Then Return g
                If tmpScreenRect.Width <= 1 And tmpScreenRect.Height <= 1 Then Return g
                Dim p As New Pen(Color.Red)
                p.Width = penWidth
                g.DrawRectangle(p, CSng(tmpScreenRect.X), CSng(tmpScreenRect.Y), CSng(tmpScreenRect.Width), CSng(tmpScreenRect.Height))
                For Each pos As PosSizableRect In [Enum].GetValues(GetType(PosSizableRect))
                    g.FillRectangle(New SolidBrush(penColor), frm.getRectangleScreen(GetRect(pos)))
                Next
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
        Return g
    End Function
    Public Sub MoveOrResizeControl(ByVal fldName As String, Optional ByVal overRide As Boolean = False)
        Dim rectNew As New iTextSharp.text.pdf.PdfArray
        Dim rectTemp As System.Drawing.RectangleF = rect
        Dim fldKidIndexTemp As Integer = frm.fldKidIndex
        Try
            frm.mMove = True
            If frm.preventDragging And Not overRide Then
                Return
            End If
            Dim fldKidIndex As Integer = frm.fldKidIndex
            If Not fldName Is Nothing And Not frm.PDFField_Copy.Checked Then
                If Not String.IsNullOrEmpty(fldName) Then
                    Dim m As New System.IO.MemoryStream
                    frm.LoadPDFReaderDoc(frm.pdfOwnerPassword & "", True)
                    Dim s As iTextSharp.text.pdf.PdfStamper = frm.getStamper(frm.pdfReaderDoc, m)
                    Dim form As iTextSharp.text.pdf.AcroFields = s.AcroFields
                    Dim item As iTextSharp.text.pdf.AcroFields.Item = form.GetFieldItem(fldName)
                    Dim itemDict As iTextSharp.text.pdf.PdfDictionary = frm.iTextFieldItemPdfDictionary(fldName)
                    If Not rectTemp = Nothing Then
                        If rectTemp.Width > 0 And rectTemp.Height > 0 Then
                            If Not String.IsNullOrEmpty(frm.fldNameHighlighted & "") And frm.CheckfieldNameExits(frm.fldNameHighlighted & "") Then

                                Dim r As New iTextSharp.text.Rectangle(CSng(rectTemp.Left), frm.getPDFHeight() - CSng(rectTemp.Bottom), CSng(rectTemp.Right), frm.getPDFHeight() - CSng(rectTemp.Top))
                                Dim rF As New System.Drawing.RectangleF(CSng(rectTemp.Left), CSng(rectTemp.Top), CSng(rectTemp.Width), CSng(rectTemp.Height))
                                'r = frm.GetFieldPositionsReverse(frm.Session, r)
                                frm.clearImageCacheHistory()
                                frm.Session = frm.A0_PDFFormField_Modify(frm.Session, frm.fldNameHighlighted, frm.PDFField_Name.Text & "", New iTextSharp.text.BaseColor(frm.PDFField_TextColorPicker.BackColor.R, frm.PDFField_TextColorPicker.BackColor.G, frm.PDFField_TextColorPicker.BackColor.B, frm.PDFField_TextColorPicker.BackColor.A), New iTextSharp.text.BaseColor(frm.PDFField_BackgroundColorPicker.BackColor.R, frm.PDFField_BackgroundColorPicker.BackColor.G, frm.PDFField_BackgroundColorPicker.BackColor.B, frm.PDFField_BackgroundColorPicker.BackColor.A), New iTextSharp.text.BaseColor(frm.PDFField_BorderColorPicker.BackColor.R, frm.PDFField_BorderColorPicker.BackColor.G, frm.PDFField_BorderColorPicker.BackColor.B, frm.PDFField_BorderColorPicker.BackColor.A), r)
                                'frm.updatefield()
                                frm.mMove = False
                                frm.A0_LoadPDF()
                                frm.fldKidIndex = fldKidIndexTemp
                                frm.fldNameHighlighted = fldName
                                rect = rectTemp
                                frm.A0_PDFFormField_LoadFieldWithRectF(rF, fldName, Nothing, True)
                                'frm.refreshPDFImage()

                                'frm.A0_PDFFormField_LoadProperties(frm.Session, fldName, frm.btnPage.SelectedIndex + 1, fldKidIndexTemp)
                            ElseIf frm.CheckfieldNameExits(frm.PDFField_Name.Text & "") Then
                                Dim r As New iTextSharp.text.Rectangle(CSng(rectTemp.Left), frm.getPDFHeight() - CSng(rectTemp.Bottom), CSng(rectTemp.Right), frm.getPDFHeight() - CSng(rectTemp.Top))
                                Dim rF As New System.Drawing.RectangleF(CSng(rectTemp.Left), CSng(rectTemp.Top), CSng(rectTemp.Width), CSng(rectTemp.Height))
                                'r = frm.GetFieldPositionsReverse(frm.Session, r)
                                frm.clearImageCacheHistory()
                                frm.Session = frm.A0_PDFFormField_Modify(frm.Session, frm.PDFField_Name.Text & "", frm.PDFField_Name.Text & "", New iTextSharp.text.BaseColor(frm.PDFField_TextColorPicker.BackColor.R, frm.PDFField_TextColorPicker.BackColor.G, frm.PDFField_TextColorPicker.BackColor.B, frm.PDFField_TextColorPicker.BackColor.A), New iTextSharp.text.BaseColor(frm.PDFField_BackgroundColorPicker.BackColor.R, frm.PDFField_BackgroundColorPicker.BackColor.G, frm.PDFField_BackgroundColorPicker.BackColor.B, frm.PDFField_BackgroundColorPicker.BackColor.A), New iTextSharp.text.BaseColor(frm.PDFField_BorderColorPicker.BackColor.R, frm.PDFField_BorderColorPicker.BackColor.G, frm.PDFField_BorderColorPicker.BackColor.B, frm.PDFField_BorderColorPicker.BackColor.A), r)
                                'frm.updatefield()
                                frm.mMove = False
                                frm.A0_LoadPDF()
                                rect = rectTemp
                                frm.fldKidIndex = fldKidIndexTemp
                                frm.fldNameHighlighted = fldName
                                'frm.A0_PDFFormField_LoadProperties(frm.Session, fldName, -1, fldKidIndexTemp)
                                'frm.A0_PDFFormField_LoadProperties(frm.Session, fldName, frm.btnPage.SelectedIndex + 1, fldKidIndexTemp)
                                'frm.clearImageCacheHistory()
                                frm.A0_PDFFormField_LoadFieldWithRectF(rF, fldName, Nothing, True)
                                'frm.refreshPDFImage()
                            Else
                                frm.lblFieldType.Text = "PROPERTIES"
                                Dim r As New iTextSharp.text.Rectangle(CSng(rectTemp.Left), frm.getPDFHeight() - CSng(rectTemp.Bottom), CSng(rectTemp.Right), frm.getPDFHeight() - CSng(rectTemp.Top))
                                Dim rF As New System.Drawing.RectangleF(CSng(rectTemp.Left), CSng(rectTemp.Top), CSng(rectTemp.Width), CSng(rectTemp.Height))
                                'r = frm.GetFieldPositionsReverse(frm.Session, r)
                                frm.clearImageCacheHistory()
                                frm.Session = frm.A0_PDFFormField_Modify(frm.Session, "", frm.PDFField_Name.Text & "", New iTextSharp.text.BaseColor(frm.PDFField_TextColorPicker.BackColor.R, frm.PDFField_TextColorPicker.BackColor.G, frm.PDFField_TextColorPicker.BackColor.B, frm.PDFField_TextColorPicker.BackColor.A), New iTextSharp.text.BaseColor(frm.PDFField_BackgroundColorPicker.BackColor.R, frm.PDFField_BackgroundColorPicker.BackColor.G, frm.PDFField_BackgroundColorPicker.BackColor.B, frm.PDFField_BackgroundColorPicker.BackColor.A), New iTextSharp.text.BaseColor(frm.PDFField_BorderColorPicker.BackColor.R, frm.PDFField_BorderColorPicker.BackColor.G, frm.PDFField_BorderColorPicker.BackColor.B, frm.PDFField_BorderColorPicker.BackColor.A), r)
                                'rect = rectTemp
                                'frm.updatefield()
                                frm.mMove = False
                                frm.A0_LoadPDF()
                                rect = rectTemp
                                'frm.clearImageCacheHistory()
                                frm.A0_PDFFormField_LoadFieldWithRectF(rF, frm.PDFField_Name.Text & "", Nothing, True)
                                'frm.refreshPDFImage()
                                'frm.A0_PDFFormField_LoadProperties(frm.Session, frm.fldNameHighlighted, -1, 0)
                                'frm.A0_PDFFormField_LoadProperties(frm.Session, fldName, frm.btnPage.SelectedIndex + 1, fldKidIndexTemp)
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            Err.Clear()
        Finally
            frm.mMove = False
        End Try
    End Sub
    Public Sub setForm(ByRef frm1 As frmMain)
        frm = frm1
    End Sub
    Public Sub SetBitmapFile(ByVal filename As String)
        Me.mBmp = New Bitmap(filename)
    End Sub
    Public Sub SetImagePic(ByVal pic As Image)
        Me.imgPic = DirectCast(pic.Clone, System.Drawing.Image)
    End Sub
    Public Sub SetBitmap(ByVal bmp As Bitmap)
        Me.mBmp = bmp
    End Sub
    Public Sub SetPictureBox(ByRef p As PictureBox, Optional ByVal updateImagePic As Boolean = False)
        Me.mPictureBox = p
        Dim namePb As String = mPictureBox.Name
        Try
            If updateImagePic Then
                SetImagePic(DirectCast(mPictureBox.Image.Clone, Image))
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
        AddHandler mPictureBox.MouseDown, New MouseEventHandler(AddressOf mPictureBox_MouseDown)
        AddHandler mPictureBox.MouseMove, New MouseEventHandler(AddressOf mPictureBox_MouseMove)
        AddHandler mPictureBox.Paint, New PaintEventHandler(AddressOf mPictureBox_Paint)
    End Sub
    Public Sub RemovePictureBoxHandlers(ByVal p As PictureBox)
        Me.mPictureBox = p
        Try
            RemoveHandler mPictureBox.MouseDown, New MouseEventHandler(AddressOf mPictureBox_MouseDown)
        Catch ex As Exception
            Err.Clear()
        End Try
        Try
        Catch ex As Exception
            Err.Clear()
        End Try
        Try
        Catch ex As Exception
            Err.Clear()
        End Try
        Try
            RemoveHandler mPictureBox.MouseMove, New MouseEventHandler(AddressOf mPictureBox_MouseMove)
        Catch ex As Exception
            Err.Clear()
        End Try
        Try
            RemoveHandler mPictureBox.Paint, New PaintEventHandler(AddressOf mPictureBox_Paint)
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub mPictureBox_Paint(ByVal sender As Object, ByVal e As PaintEventArgs)
        If frm.getPercent() >= 1 Then
            sizeNodeRect = 16 / frm.getPercent()
        Else
            sizeNodeRect = 16 * frm.getPercent()
        End If

    End Sub
    Private Sub mPictureBox_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
        nodeSelectedTmp = GetNodeSelectable(e.Location)
        mIsClick = True
        pauseDraw = False
        nodeSelected = nodeSelectedTmp
        If rectScreen.Contains(New Point(e.X, e.Y)) Then
            frm.mMove = True
        End If
        oldX = e.X
        oldY = e.Y
        rectOld = rect
        rectBackup = rect
        If nodeSelectedTmp <> clsUserRect.PosSizableRect.None Then
            Return
        End If
        Try
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public moveResize As Boolean = False
    Public fldKidIndexPrevious As Integer = -1
    Public Sub mPictureBox_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs)
        Try

            mIsClick = False
            frm.mMove = False
            pauseDraw = False
            rectBackup = rect
            moveResize = False
            If Not rectOld = rectBackup Then
                If frm.preventDragging = False And Not rect = Nothing And Not frm.fldNameHighlighted = "" Then
                    ''frm.PDFField_Name.Text = frm.fldNameHighlighted
                    If fldKidIndexPrevious = frm.fldKidIndex And frm.fldNameHighlighted = frm.fldNameHighlightedCopy Then
                        fldKidIndexPrevious = frm.fldKidIndex
                        MoveOrResizeControl(frm.fldNameHighlighted & "")
                        frm.DrawImageFieldPositions()
                        If frm.fldKidIndex >= 0 And Not String.IsNullOrEmpty(frm.fldNameHighlighted) Then
                            frm.A0_PDFFormField_LoadProperties(frm.Session, frm.fldNameHighlighted, Nothing, frm.fldKidIndex)
                            frm.PnlFields_Position(True, True)
                        End If
                        'frm.A0_LoadPDF()
                        moveResize = True
                        'frm.refreshPDFImage()
                        'frm.DrawImageFieldPositions()
                        Return
                    End If
                End If
            End If
            rectOld = rectBackup
            fldKidIndexPrevious = frm.fldKidIndex
            frm.preventDragging = False
            frm.DrawImageFieldPositions()
            If (frm.fldNameHighlighted <> "" Or frm._dragging Or nodeSelected <> clsUserRect.PosSizableRect.None And pauseDraw = False) Then
                If Not frm.fldNameHighlighted = "" And frm.fldKidIndex >= 0 Then
                    If rect = Nothing Then
                        rect = frm.GetFieldPositionsReverse(frm.Session, frm.fldNameHighlighted)
                    End If
                End If
                If Not rect = Nothing Then
                    If rect.Width > 1 And rect.Height > 1 Then
                        DrawPictureBoxImageBox()
                        frm.PnlFields_Position(False)
                        If moveResize Then
                            frm.PDFField_Name.Text = frm.fldNameHighlighted
                            frm.fldKidIndex = frm.getKidFieldIndexByRectanglePDF(rectScreen)
                            frm.PDFField_Index.Text = CStr(frm.fldKidIndex + 0)
                            frm.A0_PDFFormField_LoadProperties(frm.Session(), frm.fldNameHighlighted, CInt(frm.btnPage.SelectedIndex) + 1, frm.fldKidIndex)
                        End If
                    End If
                End If
            ElseIf Not rect = Nothing Then
                If rect.Width > 1 And rect.Height > 1 Then
                    DrawPictureBoxImageBox()
                    frm.PnlFields_Position(False)
                End If
            End If
        Catch exMain As Exception
            frm.TimeStampAdd(exMain, frm.debugMode) ' NK 2016-06-30exMain Else Err.Clear() ' Err.Clear()  ' NK3 ' 
        Finally
            mIsClick = False
        End Try
    End Sub
    Public Sub mPictureBox_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
        '        If frm.mMove Then
        '        End If
        '        If Not frm.lockCursor And Not frm.isDragingImage And Not frm._dragging Then
        '            ChangeCursor(e.Location)
        '        End If
        '        If mIsClick = False Then
        '            GoTo GOTO_END
        '        ElseIf frm.preventDragging = True Then
        '        ElseIf Not frm._dragging Then
        '        End If
        '        Dim backupRect As RectangleF = rect
        '        Dim rectScreenTemp As RectangleF = rectScreen()
        '        If rectScreenTemp.Right > frm.A0_PictureBox1.Width Then
        '            rectScreenTemp = New System.Drawing.RectangleF(frm.A0_PictureBox1.Width - rectScreenTemp.Width - 1, rectScreenTemp.Top, rectScreenTemp.Width, rectScreenTemp.Height)
        '            frm._clickPoints.Clear()
        '            frm._clickPoints.Add(New PointF(rectScreenTemp.Left, rectScreenTemp.Bottom))
        '            frm._clickPoints.Add(New PointF(rectScreenTemp.Right, rectScreenTemp.Top))
        '        ElseIf rectScreenTemp.Bottom > frm.A0_PictureBox1.Height Then
        '            rectScreenTemp = New System.Drawing.RectangleF(rectScreenTemp.Left, frm.A0_PictureBox1.Height - rectScreenTemp.Height - 1, rectScreenTemp.Width, rectScreenTemp.Height)
        '            frm._clickPoints.Clear()
        '            frm._clickPoints.Add(New PointF(rectScreenTemp.Left, rectScreenTemp.Bottom))
        '            frm._clickPoints.Add(New PointF(rectScreenTemp.Right, rectScreenTemp.Top))
        '        End If
        '        Dim drawBox As Boolean = False
        '        If frm._dragging Or frm.mMove Or nodeSelectedTmp <> PosSizableRect.None Or mPictureBox.Cursor <> Cursors.Default Then
        '            Dim tmprect As RectangleF = rectScreenTemp
        '            If frm.preventDragging = False Then
        '                rectBackup = rect
        '                drawBox = True
        '                If nodeSelected <> PosSizableRect.None Then
        '                    If frm.pnlFields.Visible Then frm.pnlFields.Visible = False
        '                End If
        '                Select Case nodeSelectedTmp
        '                    Case PosSizableRect.LeftUp
        '                        tmprect.X += e.X - oldX
        '                        tmprect.Width -= e.X - oldX
        '                        tmprect.Y += e.Y - oldY
        '                        tmprect.Height -= e.Y - oldY
        '                        rect = frm.getRectanglePDF(tmprect)
        '                        frm.lockCursor = True
        '                        Exit Select
        '                    Case PosSizableRect.LeftMiddle
        '                        tmprect.X += e.X - oldX
        '                        tmprect.Width -= e.X - oldX
        '                        rect = frm.getRectanglePDF(tmprect)
        '                        frm.lockCursor = True
        '                        Exit Select
        '                    Case PosSizableRect.LeftBottom
        '                        tmprect.Width -= e.X - oldX
        '                        tmprect.X += e.X - oldX
        '                        tmprect.Height += e.Y - oldY
        '                        rect = frm.getRectanglePDF(tmprect)
        '                        frm.lockCursor = True
        '                        Exit Select
        '                    Case PosSizableRect.BottomMiddle
        '                        tmprect.Height += e.Y - oldY
        '                        rect = frm.getRectanglePDF(tmprect)
        '                        frm.lockCursor = True
        '                        Exit Select
        '                    Case PosSizableRect.RightUp
        '                        tmprect.Width += e.X - oldX
        '                        tmprect.Y += e.Y - oldY
        '                        tmprect.Height -= e.Y - oldY
        '                        rect = frm.getRectanglePDF(tmprect)
        '                        frm.lockCursor = True
        '                        Exit Select
        '                    Case PosSizableRect.RightBottom
        '                        tmprect.Width += e.X - oldX
        '                        tmprect.Height += e.Y - oldY
        '                        rect = frm.getRectanglePDF(tmprect)
        '                        frm.lockCursor = True
        '                        Exit Select
        '                    Case PosSizableRect.RightMiddle
        '                        tmprect.Width += e.X - oldX
        '                        rect = frm.getRectanglePDF(tmprect)
        '                        frm.lockCursor = True
        '                        Exit Select
        '                    Case PosSizableRect.UpMiddle
        '                        tmprect.Y += e.Y - oldY
        '                        tmprect.Height -= e.Y - oldY
        '                        rect = frm.getRectanglePDF(tmprect)
        '                        frm.lockCursor = True
        '                        Exit Select
        '                    Case PosSizableRect.Middle
        '                        tmprect.X = tmprect.X + e.X - oldX
        '                        tmprect.Y = tmprect.Y + e.Y - oldY
        '                        rect = frm.getRectanglePDF(tmprect)
        '                        frm.lockCursor = True
        '                        frm.mMove = True
        '                        Exit Select
        '                    Case Else
        '                        Exit Select
        '                End Select
        '                oldX = e.X
        '                oldY = e.Y
        '                If drawBox Then
        '                    DrawPictureBoxImageBox()
        '                End If
        '            End If
        '        End If
        '        Dim moveResize As Boolean = False
        '        Try
        '            Dim eMouse As Point = frm.A0_PictureBox2.PointToClient(Cursor.Position)
        '            If Not frm.lockCursor Then
        '            End If
        '            If nodeSelectedTmp = PosSizableRect.Middle Then
        '                GoTo GOTO_END
        '            End If
        '            If frm.mMove Then
        '            End If
        '            If frm.preventDragging = True Then
        '                GoTo GOTO_END
        '            End If
        '            rectScreenTemp = rectScreen()
        '            If frm.mMove Or mIsClick Then
        '                Dim x As Boolean = True
        '                If eMouse.X >= frm.A0_PictureBox1.Width Then
        '                ElseIf eMouse.Y >= frm.A0_PictureBox1.Height Then
        '                ElseIf eMouse.X <= 0 Or eMouse.Y <= 0 Then
        '                    If frm.preventDragging = True Then
        '                        GoTo GOTO_END
        '                    End If
        '                End If
        '                If frm._clickPoints.Count = 1 Then
        '                    frm._clickPoints.Add(eMouse)
        '                ElseIf frm._clickPoints.Count >= 2 Then
        '                    Do Until frm._clickPoints.Count = 2
        '                        frm._clickPoints.RemoveAt(0)
        '                    Loop
        '                    frm._clickPoints(frm._clickPoints.Count - 1) = (eMouse)
        '                End If
        '            Else
        '                If Not frm._dragging And frm._clickPoints.Count <= 1 Then
        '                    GoTo GOTO_END
        '                End If
        '            End If
        '            If frm._dragging And frm._clickPoints.Count > 1 Then
        '                Dim ptOrigin As New System.Drawing.Point
        '                If frm._clickPoints(0).Y > frm._clickPoints(1).Y Then
        '                    frm.btnTop.Text = frm._clickPoints(1).Y.ToString
        '                    frm.btnBottom.Text = frm._clickPoints(0).Y.ToString
        '                    ptOrigin.Y = CInt(frm._clickPoints(1).Y)
        '                Else
        '                    frm.btnTop.Text = frm._clickPoints(0).Y.ToString
        '                    frm.btnBottom.Text = frm._clickPoints(1).Y.ToString
        '                    ptOrigin.Y = CInt(frm._clickPoints(0).Y)
        '                End If
        '                frm.btnHeight.Text = Math.Abs(CSng(frm.btnTop.Text) - CSng(frm.btnBottom.Text)).ToString
        '                If frm._clickPoints(0).X > frm._clickPoints(1).X Then
        '                    frm.btnLeft.Text = frm._clickPoints(1).X.ToString
        '                    frm.btnRight.Text = frm._clickPoints(0).X.ToString
        '                    ptOrigin.X = CInt(frm._clickPoints(1).X)
        '                Else
        '                    frm.btnLeft.Text = frm._clickPoints(0).X.ToString
        '                    frm.btnRight.Text = frm._clickPoints(1).X.ToString
        '                    ptOrigin.X = CInt(frm._clickPoints(0).X)
        '                End If
        '                frm.btnWidth.Text = Math.Abs(CSng(frm.btnRight.Text) - CSng(frm.btnLeft.Text)).ToString
        '                If CSng(frm.btnHeight.Text) > 5 And CSng(frm.btnWidth.Text) > 5 Then
        '                    If Not frm._pictureBoxImage Is Nothing Then
        '                        Dim tmpRect As New RectangleF(ptOrigin.X, ptOrigin.Y, CSng(frm.btnWidth.Text), CSng(frm.btnHeight.Text))
        '                        rect = frm.getRectanglePDF(tmpRect)
        '                    End If
        '                End If
        '            Else
        '                frm.ToolStripStatusLabel_XY.Text = "" & eMouse.X & "," & eMouse.Y & ""
        '            End If
        '        Catch exMain As Exception
        '            frm.TimeStampAdd(exMain, frm.debugMode) ' NK 2016-06-30exMain Else Err.Clear() ' Err.Clear()  ' NK3 ' 
        '        Finally
        '            frm.preventDragging = False
        '            If Not rect = Nothing Then
        '                If rect.Width > 1 And rect.Height > 1 Or drawBox Then
        '                    If Not rectOld = rect Or Not nodeSelected = PosSizableRect.None Or drawBox Then
        '                        frm.A0_PictureBox2.Invalidate()
        '                    End If
        '                End If
        '            End If
        '        End Try
        'GOTO_END:
    End Sub
    Public Sub refreshDimensionsForm1()
        Try
            If Not frm Is Nothing Then
                If (rect.Width <= 0 And rect.Height <= 0) Then
                    Return
                End If
            End If
            frm.btnLeft.Text = rect.Left.ToString.ToString
            frm.btnRight.Text = rect.Right.ToString.ToString
            frm.btnTop.Text = rect.Top.ToString
            frm.btnBottom.Text = rect.Bottom.ToString
            frm.btnWidth.Text = rect.Width.ToString
            frm.btnHeight.Text = rect.Height.ToString
            Dim ptOrigin As New System.Drawing.Point
            If rect.Left > rect.Right Then
                frm.btnTop.Text = rect.Bottom.ToString '_clickPoints(1).Y
                frm.btnBottom.Text = rect.Top.ToString '_clickPoints(0).Y
                ptOrigin.Y = CInt(rect.Bottom) '_clickPoints(1).Y
            Else
                frm.btnTop.Text = rect.Top.ToString '_clickPoints(0).Y
                frm.btnBottom.Text = rect.Bottom.ToString '_clickPoints(1).Y
                ptOrigin.Y = CInt(rect.Top) '_clickPoints(0).Y
            End If
            frm.btnHeight.Text = Math.Abs(CSng(frm.btnBottom.Text) - CSng(frm.btnTop.Text)).ToString
            If rect.Left > rect.Right Then '_clickPoints(0).X > _clickPoints(1).X Then
                frm.btnLeft.Text = rect.Right.ToString '_clickPoints(1).X
                frm.btnRight.Text = rect.Left.ToString ' _clickPoints(0).X
                ptOrigin.X = CInt(rect.Right) '_clickPoints(1).X
            Else
                frm.btnLeft.Text = rect.Left.ToString.ToString '_clickPoints(0).X
                frm.btnRight.Text = rect.Right.ToString.ToString ' _clickPoints(1).X
                ptOrigin.X = CInt(rect.Left) '_clickPoints(0).X
            End If
            frm.btnWidth.Text = Math.Abs(CSng(frm.btnRight.Text) - CSng(frm.btnLeft.Text)).ToString
            Dim h As Single = mPictureBox.Height + 0
            frm.btnTop.Text = CStr(h - CSng(frm.btnTop.Text) + 0)
            frm.btnBottom.Text = CStr(h - CSng(frm.btnBottom.Text) + 0)
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub TestIfRectInsideArea()
        Dim rTmp As RectangleF = rect
        If rect.X < 0 Then
            rTmp.X = 0
            rect = rTmp
        End If
        If rect.Y < 0 Then
            rTmp.Y = 0
            rect = rTmp
        End If
        If rect.Width <= 0 Then
            rTmp.Width = 1
            rect = rTmp
        End If
        If rect.Height <= 0 Then
            rTmp.Height = 1
            rect = rTmp
        End If
    End Sub
    Private Function CreateRectSizableNode(ByVal x As Single, ByVal y As Single, Optional ByVal offset As Integer = 0) As Rectangle
        Return New Rectangle(CInt(x - (sizeNodeRect \ 2) + (offset \ 2)), CInt(y - (sizeNodeRect \ 2) - (offset \ 2)), (sizeNodeRect + offset), (sizeNodeRect + offset))
    End Function
    Private Function GetRect(ByVal p As PosSizableRect) As Rectangle
        Select Case p
            Case PosSizableRect.LeftUp
                Return CreateRectSizableNode(rect.X, rect.Y)
            Case PosSizableRect.LeftMiddle
                Return CreateRectSizableNode(rect.X, rect.Y + rect.Height / 2)
            Case PosSizableRect.LeftBottom
                Return CreateRectSizableNode(rect.X, rect.Y + rect.Height)
            Case PosSizableRect.BottomMiddle
                Return CreateRectSizableNode(rect.X + rect.Width / 2, rect.Y + rect.Height)
            Case PosSizableRect.RightUp
                Return CreateRectSizableNode(rect.X + rect.Width, rect.Y)
            Case PosSizableRect.RightBottom
                Return CreateRectSizableNode(rect.X + rect.Width, rect.Y + rect.Height)
            Case PosSizableRect.RightMiddle
                Return CreateRectSizableNode(rect.X + rect.Width, rect.Y + rect.Height / 2)
            Case PosSizableRect.UpMiddle
                Return CreateRectSizableNode(rect.X + rect.Width / 2, rect.Y)
            Case Else
                Return New Rectangle()
        End Select
    End Function
    Private Function GetRectScreen(ByVal p As PosSizableRect) As Rectangle
        Dim rectTemp As RectangleF = rectScreen() 'frm.getRectangleScreen(rect)
        Select Case p
            Case PosSizableRect.LeftUp
                Return CreateRectSizableNode(rectTemp.X, rectTemp.Y)
            Case PosSizableRect.LeftMiddle
                Return CreateRectSizableNode(rectTemp.X, rectTemp.Y + rectTemp.Height / 2)
            Case PosSizableRect.LeftBottom
                Return CreateRectSizableNode(rectTemp.X, rectTemp.Y + rectTemp.Height)
            Case PosSizableRect.BottomMiddle
                Return CreateRectSizableNode(rectTemp.X + rectTemp.Width / 2, rectTemp.Y + rectTemp.Height)
            Case PosSizableRect.RightUp
                Return CreateRectSizableNode(rectTemp.X + rectTemp.Width, rectTemp.Y)
            Case PosSizableRect.RightBottom
                Return CreateRectSizableNode(rectTemp.X + rectTemp.Width, rectTemp.Y + rectTemp.Height)
            Case PosSizableRect.RightMiddle
                Return CreateRectSizableNode(rectTemp.X + rectTemp.Width, rectTemp.Y + rectTemp.Height / 2)
            Case PosSizableRect.UpMiddle
                Return CreateRectSizableNode(rectTemp.X + rectTemp.Width / 2, rectTemp.Y)
            Case Else
                Return New Rectangle()
        End Select
    End Function
    Private Function GetRectScreen(ByVal p As PosSizableRect, ByVal offset As Integer) As Rectangle
        Dim rectTemp As RectangleF = rectScreen() 'frm.getRectangleScreen(rect)
        Select Case p
            Case PosSizableRect.LeftUp
                Return CreateRectSizableNode(rectTemp.X, rectTemp.Y, offset)
            Case PosSizableRect.LeftMiddle
                Return CreateRectSizableNode(rectTemp.X, rectTemp.Y + rectTemp.Height / 2, offset)
            Case PosSizableRect.LeftBottom
                Return CreateRectSizableNode(rectTemp.X, rectTemp.Y + rectTemp.Height, offset)
            Case PosSizableRect.BottomMiddle
                Return CreateRectSizableNode(rectTemp.X + rectTemp.Width / 2, rectTemp.Y + rectTemp.Height, offset)
            Case PosSizableRect.RightUp
                Return CreateRectSizableNode(rectTemp.X + rectTemp.Width, rectTemp.Y, offset)
            Case PosSizableRect.RightBottom
                Return CreateRectSizableNode(rectTemp.X + rectTemp.Width, rectTemp.Y + rectTemp.Height, offset)
            Case PosSizableRect.RightMiddle
                Return CreateRectSizableNode(rectTemp.X + rectTemp.Width, rectTemp.Y + rectTemp.Height / 2, offset)
            Case PosSizableRect.UpMiddle
                Return CreateRectSizableNode(rectTemp.X + rectTemp.Width / 2, rectTemp.Y, offset)
            Case Else
                Return New Rectangle()
        End Select
    End Function
    Public Function GetNodeSelectable(ByVal p As Point) As PosSizableRect
        For Each r As PosSizableRect In [Enum].GetValues(GetType(PosSizableRect))
            If GetRectScreen(r, sizeNodeRect).Contains(p) Then 'CInt(IIf(frm.getPercent() >= 1, sizeNodeRect / frm.getPercent(), sizeNodeRect * frm.getPercent()))
                Return r
            End If
        Next
        If rectScreen.Contains(p) Then
            Return PosSizableRect.Middle
        End If
        Return PosSizableRect.None
    End Function
    Public Function GetNodeSelectable(ByVal p As PointF) As PosSizableRect
        For Each r As PosSizableRect In [Enum].GetValues(GetType(PosSizableRect))
            If GetRectScreen(r, sizeNodeRect).Contains(CInt(p.X), CInt(p.Y)) Then
                Return r
            End If
        Next
        If rectScreen.Contains(p) Then
            Return PosSizableRect.Middle
        End If
        Return PosSizableRect.None
    End Function
    Public Function GetNodeSelectable(ByVal p As Point, ByVal offset As Integer) As PosSizableRect
        For Each r As PosSizableRect In [Enum].GetValues(GetType(PosSizableRect))
            If GetRectScreen(r, offset).Contains(p) Then
                Return r
            End If
        Next
        If rectScreen.Contains(p) Then
            Return PosSizableRect.Middle
        End If
        Return PosSizableRect.None
    End Function
    Public Sub ChangeCursor(ByVal p As Point)
        If frm.lockCursor = False Then
            mPictureBox.Cursor = GetCursor(GetNodeSelectable(p))
        End If
    End Sub
    ''' <summary>
    ''' Get cursor for the handle
    ''' </summary>
    ''' <param name="p"></param>
    ''' <returns></returns>
    Public Function GetCursor(ByVal p As PosSizableRect) As Cursor
        Select Case p
            Case PosSizableRect.LeftUp
                Return Cursors.SizeNWSE
            Case PosSizableRect.LeftMiddle
                Return Cursors.SizeWE
            Case PosSizableRect.LeftBottom
                Return Cursors.SizeNESW
            Case PosSizableRect.BottomMiddle
                Return Cursors.SizeNS
            Case PosSizableRect.RightUp
                Return Cursors.SizeNESW
            Case PosSizableRect.RightBottom
                Return Cursors.SizeNWSE
            Case PosSizableRect.RightMiddle
                Return Cursors.SizeWE
            Case PosSizableRect.UpMiddle
                Return Cursors.SizeNS
            Case PosSizableRect.Middle
                Return Cursors.Hand
            Case Else
                Try
                    If frm.cLinks Is Nothing Then
                        frm.cLinks = New clsLinks(frm.pdfReaderDoc, frm)
                    End If
                    Return frm.cLinks.mouseCursor
                Catch ex As Exception
                    Return Cursors.Default
                End Try
        End Select
    End Function
End Class