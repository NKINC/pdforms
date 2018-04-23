Imports System.Drawing
Imports System.Drawing.Image
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing.Text
Imports System.Drawing.Brush
Imports System.Drawing.Graphics
Imports System.Drawing.Bitmap
Imports System.Drawing.Pen
Imports System.Drawing.Rectangle
Imports System.Drawing.Color
Imports System.Drawing.Point
Imports System.Drawing.SolidBrush
Public Class clsPictureBox1_DrawSignature
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 Nicholas Kowalewicz All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>

#Region "GLOBAL VARIABLES"
    Public _form1 As Global.PDFormsNet.frmSignature
    Public _MapImageBytes() As Byte
    Public _MapImage As Image
    Public _Mouse_Down As Boolean = False
    Public _Mouse_DrawMode As Boolean = False
    Public _Mouse_Point As Point
    Public _Mouse_Point_Previous As Point
    Public _Mouse_GraphicsPath As New GraphicsPath
    Private m_BufferBitmap As Bitmap
    Private m_BufferGraphics As Graphics
    Public Sub New()
    End Sub
    Public Sub New(ByRef formNew As Global.PDFormsNet.frmSignature)
        _form1 = formNew
    End Sub
#End Region
#Region "DRAW SIGNATURES"
    Public Function SaveSnapshot(ByVal picCanvas As PictureBox, ByVal bgColor As System.Drawing.Color) As Bitmap
        Dim new_bitmap As Bitmap
        new_bitmap = New Bitmap(picCanvas.Size.Width, picCanvas.Size.Height, Imaging.PixelFormat.Format32bppArgb)
        m_BufferGraphics = Graphics.FromImage(new_bitmap)
        m_BufferGraphics.Clear(bgColor)
        Try
            If Not (m_BufferBitmap Is Nothing) Then
                m_BufferGraphics.DrawImage(m_BufferBitmap, 0, 0)
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
        m_BufferBitmap = new_bitmap.Clone
        picCanvas.Image = new_bitmap.Clone
        SaveSnapshot = new_bitmap.Clone
        new_bitmap.Dispose()
        new_bitmap = Nothing
    End Function
    Public Function SaveSnapshot(ByVal width As Integer, ByVal height As Integer, ByVal bgColor As System.Drawing.Color) As Bitmap
        Dim new_bitmap As Bitmap
        new_bitmap = New Bitmap(width, height, Imaging.PixelFormat.Format32bppArgb)
        m_BufferGraphics = Graphics.FromImage(new_bitmap)
        m_BufferGraphics.Clear(bgColor)
        If Not (m_BufferBitmap Is Nothing) Then
            m_BufferGraphics.DrawImage(m_BufferBitmap, 0, 0)
        End If
        m_BufferBitmap = new_bitmap
        m_BufferBitmap = new_bitmap.Clone
        SaveSnapshot = new_bitmap.Clone
        new_bitmap.Dispose()
        new_bitmap = Nothing
    End Function
    Public Function SaveSnapshot(ByVal new_bitmap As Bitmap, ByVal width As Integer, ByVal height As Integer, ByVal bgColor As System.Drawing.Color) As Bitmap
        Dim new_bitmap2 = New Bitmap(width, height, Imaging.PixelFormat.Format32bppArgb)
        m_BufferGraphics = Graphics.FromImage(new_bitmap2)
        m_BufferGraphics.DrawImage(new_bitmap, 0, 0)
        m_BufferBitmap = new_bitmap2
        m_BufferBitmap = new_bitmap.Clone
        SaveSnapshot = new_bitmap.Clone
        new_bitmap.Dispose()
        new_bitmap = Nothing
    End Function
    Public Sub DrawSignature(Optional ByVal clearScreen As Boolean = False, Optional ByVal updateScreen As Boolean = True)
        Try
            If Not _Mouse_DrawMode Then Return
            Dim g As Graphics = _form1.CreateGraphics
            Try
                g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            Catch ex As Exception
                Err.Clear()
            End Try
            Dim myPen As Pen = New Pen(Black, 3)
            g.DrawLine(myPen, _Mouse_Point_Previous, _Mouse_Point)
        Catch ex As Exception
            Err.Clear()
        Finally
        End Try
        GC.Collect()
    End Sub
    Public Sub DrawSignature(ByRef pictureBoxTemp As PictureBox, ByRef pictureBoxSolidTemp As Bitmap, ByVal lineColor As System.Drawing.Color, Optional ByVal clearScreen As Boolean = False, Optional ByVal updateScreen As Boolean = True, Optional ByVal LineWidth As Integer = 6)
        Try
            If Not _Mouse_DrawMode Then Return
            Try
                If Not _Mouse_Point_Previous = _Mouse_Point Then
                    Dim img As Image = pictureBoxTemp.Image.Clone
                    Dim myPen As Pen = New Pen(Black, LineWidth)
                    myPen.Brush = Brushes.Black
                    myPen.Color = lineColor
                    myPen.DashStyle = Drawing2D.DashStyle.Solid
                    myPen.EndCap = LineCap.Round
                    myPen.LineJoin = Drawing2D.LineJoin.Round
                    myPen.StartCap = LineCap.Round
                    myPen.Width = LineWidth
                    Using g As Graphics = Graphics.FromImage(img)
                        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
                        g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
                        g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBilinear
                        g.DrawLine(myPen, _Mouse_Point_Previous, _Mouse_Point)
                        pictureBoxTemp.Image = img.Clone
                        pictureBoxTemp.Refresh()
                    End Using
                    img = pictureBoxSolidTemp.Clone
                    Using g As Graphics = Graphics.FromImage(img)
                        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
                        g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
                        g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBilinear
                        g.DrawLine(myPen, _Mouse_Point_Previous, _Mouse_Point)
                        pictureBoxSolidTemp = img.Clone
                    End Using
                End If
            Catch ex As Exception
                Throw ex
            End Try
        Catch ex As Exception
            Throw ex
        Finally
        End Try
        GC.Collect()
    End Sub
    Public Sub LoadSignatureImage(ByRef pictureBoxTemp As PictureBox, ByRef pictureBoxSolidTemp As Bitmap)
        Try
            Try

                pictureBoxTemp.Image = pictureBoxSolidTemp
            Catch ex As Exception
                Throw ex
            End Try
        Catch ex As Exception
            Throw ex
        Finally
        End Try
        GC.Collect()
    End Sub
#End Region
End Class
