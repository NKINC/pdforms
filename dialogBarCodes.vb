Imports System.Windows.Forms
Imports ZXing.BarcodeReader
Imports ZXing.BarcodeWriter
Imports ZXing.PDF417
Imports ZXing.Datamatrix
Imports ZXing.QrCode
Imports ZXing.Rendering
Imports System.Drawing
Imports System.IO
Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.Barcode
Class dialogBarCodes
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 NK-INC.COM All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>
    Public barCodeBitmap As Bitmap = Nothing
    Public barCodeData() As Byte
    Public barCodeText As String
    Public bmr As ZXing.BarcodeReader = Nothing
    Public bmw As ZXing.BarcodeWriter = Nothing
    Public barCodeWidth As Single = 0, barCodeHeight As Single = 0
    Public barCodeOnly As Boolean = False
    Public frmParent As Form
    Public Sub New()
        InitializeComponent()
    End Sub
    Public Sub New(ByRef frmMain1 As frmMain)
        InitializeComponent()
        frmParent = frmMain1
    End Sub
    Public Sub readBarCode(bm As Bitmap)
        Try
            barCodeBitmap = bm.Clone()
            bmr = New ZXing.BarcodeReader()
            If bmr.Options.PossibleFormats Is Nothing Then bmr.Options.PossibleFormats = New List(Of ZXing.BarcodeFormat)
            If Not bmr.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.QR_CODE) Then bmr.Options.PossibleFormats.Add(ZXing.BarcodeFormat.QR_CODE)
            If Not bmr.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.PDF_417) Then bmr.Options.PossibleFormats.Add(ZXing.BarcodeFormat.PDF_417)
            If Not bmr.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.DATA_MATRIX) Then bmr.Options.PossibleFormats.Add(ZXing.BarcodeFormat.DATA_MATRIX)
            Dim rslt As ZXing.Result = bmr.Decode(barCodeBitmap)
            If Not rslt Is Nothing Then
                ComboBox1.SelectedValue = rslt.BarcodeFormat.ToString().Replace("_"c, " "c)
                ComboBox1.SelectedText = rslt.BarcodeFormat.ToString().Replace("_"c, " "c)
                TextBox6.Text = rslt.Text
                PictureBox2.Image = barCodeBitmap.Clone()
            End If
        Catch ex As Exception
            Err.Clear()
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
    Public Function isValidBarCode(bm As Bitmap) As Boolean
        Try
            If bm Is Nothing Then Return False
            If bm.Width <= 0 Or bm.Height <= 0 Then Return False
            bmr = New ZXing.BarcodeReader()
            If Not bmr.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.QR_CODE) Then bmr.Options.PossibleFormats.Add(ZXing.BarcodeFormat.QR_CODE)
            If Not bmr.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.PDF_417) Then bmr.Options.PossibleFormats.Add(ZXing.BarcodeFormat.PDF_417)
            If Not bmr.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.DATA_MATRIX) Then bmr.Options.PossibleFormats.Add(ZXing.BarcodeFormat.DATA_MATRIX)
            Dim rslt As ZXing.Result = bmr.Decode(bm.Clone())
            If Not rslt Is Nothing Then
                If rslt.BarcodeFormat = Nothing Then
                    Return False
                End If
                If rslt.Text = "" Then
                    Return False
                End If
                writeBarCode()
                Return True
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
        Return False
    End Function
    Public Function isValidBarCode(bmBytes() As Byte) As Boolean
        Try
            Dim bm As Bitmap = Bitmap.FromStream(New MemoryStream(bmBytes))
            If bm Is Nothing Then Return False
            If bm.Width <= 0 Or bm.Height <= 0 Then Return False
            bmr = New ZXing.BarcodeReader()
            If bmr.Options.PossibleFormats Is Nothing Then bmr.Options.PossibleFormats = New List(Of ZXing.BarcodeFormat)
            If Not bmr.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.QR_CODE) Then bmr.Options.PossibleFormats.Add(ZXing.BarcodeFormat.QR_CODE)
            If Not bmr.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.PDF_417) Then bmr.Options.PossibleFormats.Add(ZXing.BarcodeFormat.PDF_417)
            If Not bmr.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.DATA_MATRIX) Then bmr.Options.PossibleFormats.Add(ZXing.BarcodeFormat.DATA_MATRIX)
            Dim tryCOunt As Integer = -1
GOTO_RETRY:
            Dim rslt As ZXing.Result = bmr.Decode(bm.Clone())
            If rslt Is Nothing And tryCOunt < 0 Then
                tryCOunt += 1
                bmr.Options.TryHarder = True
                bmr.AutoRotate = True
                bmr.TryInverted = True
                bmr.Options.PureBarcode = True
                GoTo GOTO_RETRY
            Else
                If rslt.BarcodeFormat = Nothing Then
                    Return False
                Else
                    Select Case rslt.BarcodeFormat
                        Case ZXing.BarcodeFormat.DATA_MATRIX
                            ComboBox1.SelectedIndex = 2
                        Case ZXing.BarcodeFormat.PDF_417
                            ComboBox1.SelectedIndex = 0
                        Case ZXing.BarcodeFormat.QR_CODE
                            ComboBox1.SelectedIndex = 1
                    End Select
                End If
                If rslt.Text = "" Then
                    Return False
                Else
                    TextBox6.Text = rslt.Text & ""
                End If
                writeBarCode()
                Return True
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
        Return False
    End Function
    Public Shared Function isValidBarCode_Shared(bm As Bitmap) As Boolean
        Try
            If bm Is Nothing Then Return False
            If bm.Width <= 0 Or bm.Height <= 0 Then Return False
            bm = bm.Clone()
            Dim bmr1 = New ZXing.BarcodeReader()
            If bmr1.Options.PossibleFormats Is Nothing Then bmr1.Options.PossibleFormats = New List(Of ZXing.BarcodeFormat)
            bmr1.Options.PossibleFormats.Add(ZXing.BarcodeFormat.QR_CODE)
            bmr1.Options.PossibleFormats.Add(ZXing.BarcodeFormat.PDF_417)
            bmr1.Options.PossibleFormats.Add(ZXing.BarcodeFormat.DATA_MATRIX)
            Dim rslt As ZXing.Result = bmr1.Decode(bm)
            If Not rslt Is Nothing Then
                If rslt.BarcodeFormat = Nothing Then
                    Return False
                End If
                If rslt.Text = "" Then
                    Return False
                End If
                Return True
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
        Return False
    End Function
    Public Shared Function isValidBarCode_Shared(bmBytes() As Byte) As Boolean
        Try
            Dim bm As Bitmap = Bitmap.FromStream(New MemoryStream(bmBytes))
            If bm Is Nothing Then Return False
            If bm.Width <= 0 Or bm.Height <= 0 Then Return False
            bm = bm.Clone()
            Dim bmr1 = New ZXing.BarcodeReader()
            If bmr1.Options.PossibleFormats Is Nothing Then bmr1.Options.PossibleFormats = New List(Of ZXing.BarcodeFormat)
            bmr1.Options.PossibleFormats.Add(ZXing.BarcodeFormat.QR_CODE)
            bmr1.Options.PossibleFormats.Add(ZXing.BarcodeFormat.PDF_417)
            bmr1.Options.PossibleFormats.Add(ZXing.BarcodeFormat.DATA_MATRIX)
            Dim rslt As ZXing.Result = bmr1.Decode(bm)
            If Not rslt Is Nothing Then
                If rslt.BarcodeFormat = Nothing Then
                    Return False
                End If
                If rslt.Text = "" Then
                    Return False
                End If
                Return True
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
        Return False
    End Function
    Public Sub writeBarCode()
        Try
            If String.IsNullOrEmpty(TextBox6.Text & "") Then
                Return
            End If
            bmw = New ZXing.BarcodeWriter()
            Select Case ComboBox1.SelectedItem().ToString().ToString.Replace("_"c, " "c).ToLower()
                Case "QR Code".ToLower
                    bmw.Format = ZXing.BarcodeFormat.QR_CODE
                    bmw.Options.Width = barCodeWidth
                    bmw.Options.Height = barCodeHeight
                    bmw.Options.PureBarcode = barCodeOnly
                    barCodeBitmap = bmw.Write(TextBox6.Text.ToString()).Clone()
                    PictureBox2.Image = barCodeBitmap.Clone()
                Case "DATA MATRIX".ToLower
                    bmw.Format = ZXing.BarcodeFormat.DATA_MATRIX
                    bmw.Options.Width = barCodeWidth
                    bmw.Options.Height = barCodeHeight
                    bmw.Options.PureBarcode = barCodeOnly
                    barCodeBitmap = bmw.Write(TextBox6.Text.ToString()).Clone()
                    PictureBox2.Image = barCodeBitmap.Clone()
                    Return
                Case "EAN 13".ToLower
                    bmw.Format = ZXing.BarcodeFormat.EAN_13
                    bmw.Options.Width = barCodeWidth
                    bmw.Options.Height = barCodeHeight
                    bmw.Options.PureBarcode = barCodeOnly
                    barCodeBitmap = bmw.Write(TextBox6.Text.ToString()).Clone()
                    PictureBox2.Image = barCodeBitmap.Clone()
                    Return
                Case Else
                    Return
            End Select
            If Math.Abs(CInt(barCodeBitmap.Width) - barCodeWidth) > 2 Or Math.Abs(CInt(barCodeBitmap.Height) - barCodeHeight) > 2 Then
                If barCodeWidth > 0 Or barCodeHeight > 0 Then
                    barCodeBitmap = ResizeImage(barCodeWidth, barCodeHeight, barCodeBitmap.Clone())
                    PictureBox2.Image = barCodeBitmap.Clone()
                End If
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Function ResizeImage(newWidth As Integer, newHeight As Integer, imgPhoto As Image) As Image
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
        Dim bmPhoto As New Bitmap(newWidth, newHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb)
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
        grPhoto.DrawImage(imgPhoto, New Rectangle(destX, destY, destWidth, destHeight), New Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Point)
        grPhoto.Dispose()
        Return bmPhoto
    End Function
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        writeBarCode()
    End Sub
    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
        writeBarCode()
    End Sub
    Private Sub dialogBarCodes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub
End Class
