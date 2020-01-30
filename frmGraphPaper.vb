Public Class frmGraphPaper
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 NK-INC.COM All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>
    Public CloseForm As Boolean = False
    Public img As System.Drawing.Image
    Public imgOriginal As System.Drawing.Image = Nothing
    Public imgScale As Single = 0.0F
    Public imgRect As RectangleF
    Public imgFormat As System.Drawing.Imaging.ImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg
    Public imgMaskBytes() As Byte = Nothing
    Public ReadOnly Property imgBytes() As Byte()
        Get
            Dim m As New System.IO.MemoryStream
            Try
                If Not imgFormat.GetHashCode() = System.Drawing.Imaging.ImageFormat.MemoryBmp().GetHashCode() Then
                    graphPaper_PictureBox1.Image.Save(m, imgFormat)
                Else
                    graphPaper_PictureBox1.Image.Save(m, System.Drawing.Imaging.ImageFormat.Jpeg)
                End If
            Catch ex As Exception
                Err.Clear()
            End Try
            Return m.ToArray
        End Get
    End Property
    Private Sub frmImageRotation_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            CloseForm = True
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub frmImageRotation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Focus()
            Me.BringToFront()
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub ImageRotation_btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImageRotation_btnOK.Click
        Try
            generateGraphPaper()
            Me.img = Me.graphPaper_PictureBox1.Image.Clone
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
    Private Sub ImageRotation_chkResizeProportional_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Public Sub New()
        InitializeComponent()
    End Sub
    Private Sub ImageRotation_PictureBox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles graphPaper_PictureBox1.Click
    End Sub
    Private Sub CopyImageToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyImageToolStripMenuItem.Click
        Try
            If Not graphPaper_PictureBox1.Image Is Nothing Then
                Clipboard.SetImage(graphPaper_PictureBox1.Image)
                MessageBox.Show(Me, "Copied image to clipboard.", "Clipboard", vbOKOnly, MsgBoxStyle.Information)
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If imgFormat.GetHashCode() = System.Drawing.Imaging.ImageFormat.MemoryBmp().GetHashCode() Then
            imgFormat = System.Drawing.Imaging.ImageFormat.Jpeg
        End If
        Dim imgStream As New System.IO.MemoryStream
        graphPaper_PictureBox1.Image.Save(imgStream, imgFormat)
        Dim imgTemp As System.Drawing.Image = System.Drawing.Image.FromStream(imgStream, True, False)
        Dim imgBytes() As Byte = imgStream.ToArray
        Dim frmDialogImageOptimization As dialogImageOptimization = New dialogImageOptimization(imgBytes, Nothing, imgMaskBytes)
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
                    graphPaper_PictureBox1.Image = System.Drawing.Image.FromStream(imgStream)
                    graphPaper_PictureBox1.Update()
                    Dim imgRectF As System.Drawing.Rectangle = New System.Drawing.Rectangle(0, 0, graphPaper_PictureBox1.Image.Width, graphPaper_PictureBox1.Image.Height)
                Case Else
                    Return
            End Select
        Catch ex As Exception
            Err.Clear()
        Finally
            If Not frmDialogImageOptimization Is Nothing Then
                If frmDialogImageOptimization.Visible Then frmDialogImageOptimization.Visible = False
                frmDialogImageOptimization.Dispose()
                frmDialogImageOptimization = Nothing
            End If
        End Try
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Try
            cancelled = False
            CloseForm = True
            deleteImage = True
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        generateGraphPaper()
    End Sub
    Public Sub generateGraphPaper()
        Try
            Dim x, y As Integer, margin_top As Integer = 0, margin_bottom As Integer = 0, margin_left As Integer = 0, margin_right As Integer = 0, height As Integer = 0, width As Integer = 0
            margin_top = CInt(graphPaper_MarginTop.Text)
            margin_bottom = CInt(graphPaper_MarginBottom.Text)
            margin_left = CInt(graphPaper_MarginLeft.Text)
            margin_right = CInt(graphPaper_MarginRight.Text)
            width = CInt(graphPaper_Width.Text)
            height = CInt(graphPaper_Height.Text)
            Dim b As New Bitmap(CInt(Me.graphPaper_Width.Text) + margin_left + margin_right, CInt(Me.graphPaper_Height.Text) + margin_top + margin_bottom)
            Using g As Graphics = Graphics.FromImage(b)
                g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
                g.InterpolationMode = Drawing2D.InterpolationMode.Bilinear
                g.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
                g.SmoothingMode = Drawing2D.SmoothingMode.None
                If Not PictureBox5.BackColor = Color.Transparent Then
                    g.FillRectangle(New System.Drawing.SolidBrush(PictureBox5.BackColor), New System.Drawing.Rectangle(New Point(margin_left, margin_top), New System.Drawing.Size(width, height)))
                End If
                If CInt(Me.graphPaper_XSize.Text) > 0 Then
                    For x = margin_left To margin_left + width Step CInt(Me.graphPaper_X.Text)
                        Dim intX As Integer = IIf(CInt(Me.graphPaper_X2Size.Text) > 0, (x - margin_left) Mod CInt(Me.graphPaper_X2.Text), -1)
                        If intX <> 0 Then
                            g.DrawLine(New Pen(PictureBox1.BackColor, CInt(Me.graphPaper_XSize.Text)), x, margin_top, x, margin_top + height)
                        End If
                    Next
                End If
                If CInt(Me.graphPaper_YSize.Text) > 0 Then
                    For y = margin_top To margin_top + height Step CInt(Me.graphPaper_Y.Text)
                        Dim intY As Integer = IIf(CInt(Me.graphPaper_Y2Size.Text) > 0, (y - margin_top) Mod CInt(Me.graphPaper_Y2.Text), -1)
                        If intY <> 0 Then
                            g.DrawLine(New Pen(PictureBox2.BackColor, CInt(Me.graphPaper_YSize.Text)), margin_left, y, margin_left + width, y)
                        End If
                    Next
                End If
                If CInt(Me.graphPaper_X2.Text) > 0 And CInt(Me.graphPaper_X2Size.Text) > 0 Then
                    For x = margin_left To margin_left + width Step CInt(Me.graphPaper_X2.Text)
                        Dim intX As Integer = IIf(CInt(Me.graphPaper_X2Size.Text) > 0, (x - margin_left) Mod CInt(Me.graphPaper_X2.Text), -1)
                        If intX = 0 Then
                            Dim margin_offset_top As Integer = 0, margin_offset_bottom As Integer = 0
                            margin_offset_top = CInt(IIf(CInt(Me.graphPaper_Y2Size.Text) > 0, CInt(Me.graphPaper_Y2Size.Text) / 2, CInt(Me.graphPaper_Y2Size.Text)))
                            margin_offset_bottom = CInt(IIf(CInt(Me.graphPaper_Y2Size.Text) > 0, CInt(Me.graphPaper_Y2Size.Text) / 2, CInt(Me.graphPaper_Y2Size.Text)))
                            Dim modOffset As Integer = CInt(Me.graphPaper_Y2Size.Text) Mod 2
                            If CInt(Me.graphPaper_Y2Size.Text) > 1 And modOffset <> 0 Then
                                margin_offset_top -= 1
                                margin_offset_bottom -= 1
                            End If
                            g.DrawLine(New Pen(PictureBox3.BackColor, CInt(Me.graphPaper_X2Size.Text)), x, margin_top - margin_offset_top, x, margin_top + height + margin_offset_bottom)
                        Else
                        End If
                    Next
                Else
                    If CInt(Me.graphPaper_XSize.Text) > 0 Then
                        g.DrawLine(New Pen(PictureBox2.BackColor, CInt(Me.graphPaper_XSize.Text)), margin_left, margin_top, margin_left, margin_top + height)
                        g.DrawLine(New Pen(PictureBox2.BackColor, CInt(Me.graphPaper_XSize.Text)), margin_left + width, margin_top, margin_left + width, margin_top + height)
                    End If
                End If
                If CInt(Me.graphPaper_Y2.Text) > 0 And CInt(Me.graphPaper_Y2Size.Text) > 0 Then
                    For y = margin_top To margin_top + height Step CInt(Me.graphPaper_Y2.Text)
                        Dim intY As Integer = IIf(CInt(Me.graphPaper_Y2Size.Text) > 0, (y - margin_top) Mod CInt(Me.graphPaper_Y2.Text), -1)
                        If intY = 0 Then
                            Dim margin_offset_left As Integer = 0, margin_offset_right As Integer = 0
                            margin_offset_left = CInt(IIf(CInt(Me.graphPaper_X2Size.Text) > 1, CInt(Me.graphPaper_X2Size.Text) / 2, CInt(Me.graphPaper_X2Size.Text)))
                            Dim modOffset As Integer = CInt(Me.graphPaper_X2Size.Text) Mod 2
                            If CInt(Me.graphPaper_X2Size.Text) > 1 And modOffset <> 0 Then
                                margin_offset_left += 1
                            End If
                            g.DrawLine(New Pen(PictureBox4.BackColor, CInt(Me.graphPaper_Y2Size.Text)), margin_left - margin_offset_left, y, margin_left + width + CInt(IIf(CInt(Me.graphPaper_X2Size.Text) > 1, CInt(Me.graphPaper_X2Size.Text) / 2, 0)), y)
                        Else
                        End If
                    Next
                Else
                    If CInt(Me.graphPaper_YSize.Text) > 0 Then
                        g.DrawLine(New Pen(PictureBox2.BackColor, CInt(Me.graphPaper_YSize.Text)), margin_left, margin_top, margin_left, margin_top + height)
                        g.DrawLine(New Pen(PictureBox2.BackColor, CInt(Me.graphPaper_YSize.Text)), margin_left + width, margin_top, margin_left + width, margin_top + height)
                    End If
                End If
            End Using
            graphPaper_PictureBox1.Image = b.Clone()
            graphPaper_PictureBox1.Refresh()
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub ImageRotation_btnCancel_Click(sender As Object, e As EventArgs) Handles ImageRotation_btnCancel.Click
        Try
            cancelled = True
            CloseForm = True
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        ColorDialog1.Color = PictureBox1.BackColor
        Select Case ColorDialog1.ShowDialog(Me)
            Case DialogResult.Yes, DialogResult.OK
                PictureBox1.BackColor = ColorDialog1.Color
        End Select
    End Sub
    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        ColorDialog1.Color = PictureBox2.BackColor
        Select Case ColorDialog1.ShowDialog(Me)
            Case DialogResult.Yes, DialogResult.OK
                PictureBox2.BackColor = ColorDialog1.Color
        End Select
    End Sub
    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        ColorDialog1.Color = PictureBox3.BackColor
        Select Case ColorDialog1.ShowDialog(Me)
            Case DialogResult.Yes, DialogResult.OK
                PictureBox3.BackColor = ColorDialog1.Color
        End Select
    End Sub
    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        ColorDialog1.Color = PictureBox4.BackColor
        Select Case ColorDialog1.ShowDialog(Me)
            Case DialogResult.Yes, DialogResult.OK
                PictureBox4.BackColor = ColorDialog1.Color
        End Select
    End Sub
    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
        ColorDialog1.Color = PictureBox5.BackColor
        Select Case ColorDialog1.ShowDialog(Me)
            Case DialogResult.Yes, DialogResult.OK
                PictureBox5.BackColor = ColorDialog1.Color
        End Select
    End Sub
End Class
