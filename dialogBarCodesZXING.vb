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
Class dialogBarCodesZXING
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 Nicholas Kowalewicz All Rights reserved.
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
        Return checkSum(eanBarcode) = CInt(eanBarcode.ToCharArray().Last().ToString())
    End Function
    Public Shared Function checkSum(code As [String]) As Integer
        Dim val As Integer = 0
        For i As Integer = 0 To code.Length() - 2
            val += CInt(Integer.Parse(code.Chars(i) + "")) * (If((i Mod 2 = 0), 1, 3))
        Next
        Dim checksum_digit As Integer = (10 - (val Mod 10)) Mod 10
        Return checksum_digit
    End Function
    Public Sub New()
        InitializeComponent()
        LoadBarcodeFormats()
    End Sub
    Public Sub New(ByRef frmMain1 As frmMain)
        InitializeComponent()
        frmParent = frmMain1
        LoadBarcodeFormats()
    End Sub
    Public Sub LoadBarcodeFormats()
        Try
            barcodeFormats.Clear()
            barcodeFormats.Add(ZXing.BarcodeFormat.QR_CODE.ToString().Replace("_", " "), ZXing.BarcodeFormat.QR_CODE)
            barcodeFormats.Add(ZXing.BarcodeFormat.PDF_417.ToString().Replace("_", " "), ZXing.BarcodeFormat.PDF_417)
            barcodeFormats.Add(ZXing.BarcodeFormat.DATA_MATRIX.ToString().Replace("_", " "), ZXing.BarcodeFormat.DATA_MATRIX)
            barcodeFormats.Add(ZXing.BarcodeFormat.AZTEC.ToString().Replace("_", " "), ZXing.BarcodeFormat.AZTEC)
            barcodeFormats.Add(ZXing.BarcodeFormat.CODE_128.ToString().Replace("_", " "), ZXing.BarcodeFormat.CODE_128)
            barcodeFormats.Add(ZXing.BarcodeFormat.EAN_13.ToString().Replace("_", " "), ZXing.BarcodeFormat.EAN_13)
            barcodeFormats.Add(ZXing.BarcodeFormat.EAN_8.ToString().Replace("_", " "), ZXing.BarcodeFormat.EAN_8)
            cmbSymbology.Items.Clear()
            For Each bcf As String In barcodeFormats.Keys.ToArray
                cmbSymbology.Items.Add(bcf.ToString().Replace("_", " "c))
            Next
            cmbSymbology.SelectedIndex = 0
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public Sub readBarCode(bm As Bitmap)
        Try
            barCodeBitmap = bm.Clone()
            If bm Is Nothing Then Return
            If bm.Width <= 0 Or bm.Height <= 0 Then Return
            txtBarcodeWidth.Text = bm.Width
            txtBarcodeHeight.Text = bm.Height
            bmr = New ZXing.BarcodeReader()
            If bmr.Options.PossibleFormats Is Nothing Then bmr.Options.PossibleFormats = New List(Of ZXing.BarcodeFormat)
            If Not bmr.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.QR_CODE) Then bmr.Options.PossibleFormats.Add(ZXing.BarcodeFormat.QR_CODE)
            If Not bmr.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.PDF_417) Then bmr.Options.PossibleFormats.Add(ZXing.BarcodeFormat.PDF_417)
            If Not bmr.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.DATA_MATRIX) Then bmr.Options.PossibleFormats.Add(ZXing.BarcodeFormat.DATA_MATRIX)
            If Not bmr.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.EAN_13) Then bmr.Options.PossibleFormats.Add(ZXing.BarcodeFormat.EAN_13)
            If Not bmr.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.EAN_8) Then bmr.Options.PossibleFormats.Add(ZXing.BarcodeFormat.EAN_8)
            If Not bmr.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.AZTEC) Then bmr.Options.PossibleFormats.Add(ZXing.BarcodeFormat.AZTEC)
            If Not bmr.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.CODE_128) Then bmr.Options.PossibleFormats.Add(ZXing.BarcodeFormat.CODE_128)
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
                If tryCOunt >= 0 Then chkPurBarCodeOnly.Checked = True Else chkPurBarCodeOnly.Checked = False
                If rslt.BarcodeFormat = Nothing Then
                    Return
                Else
                    Dim selIndex As Integer = -1
                    If rslt.BarcodeFormat Then
                        If rslt.BarcodeFormat = ZXing.BarcodeFormat.QR_CODE Then
                            txtMargin.Text = "0"
                        Else
                            txtMargin.Text = "-1"
                        End If
                        For selIndex = 0 To cmbSymbology.Items.Count - 1
                            If cmbSymbology.Items(selIndex).ToString.ToLower().Replace("_", " ") = rslt.BarcodeFormat.ToString.ToLower().Replace("_", " ") Then
                                cmbSymbology.SelectedIndex = selIndex
                                Label6.Text = ""
                                Exit For
                            End If
                        Next
                    Else
                        Label6.Text = "ERROR: Invlid Code"
                        Return
                    End If
                End If
                If rslt.Text = "" Then
                    Return
                Else
                    txtTextToEncode.Text = rslt.Text & ""
                End If
                Return
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public Function isValidBarCode(bm As Bitmap) As Boolean
        Try
            Label6.Text = ""
            If bm Is Nothing Then Return False
            If bm.Width <= 0 Or bm.Height <= 0 Then Return False
            bmr = New ZXing.BarcodeReader()
            If bmr.Options.PossibleFormats Is Nothing Then bmr.Options.PossibleFormats = New List(Of ZXing.BarcodeFormat)
            If Not bmr.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.QR_CODE) Then bmr.Options.PossibleFormats.Add(ZXing.BarcodeFormat.QR_CODE)
            If Not bmr.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.PDF_417) Then bmr.Options.PossibleFormats.Add(ZXing.BarcodeFormat.PDF_417)
            If Not bmr.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.DATA_MATRIX) Then bmr.Options.PossibleFormats.Add(ZXing.BarcodeFormat.DATA_MATRIX)
            If Not bmr.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.EAN_13) Then bmr.Options.PossibleFormats.Add(ZXing.BarcodeFormat.EAN_13)
            If Not bmr.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.EAN_8) Then bmr.Options.PossibleFormats.Add(ZXing.BarcodeFormat.EAN_8)
            If Not bmr.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.AZTEC) Then bmr.Options.PossibleFormats.Add(ZXing.BarcodeFormat.AZTEC)
            If Not bmr.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.CODE_128) Then bmr.Options.PossibleFormats.Add(ZXing.BarcodeFormat.CODE_128)
            Dim tryCOunt As Integer = -1
GOTO_RETRY:
            Dim rslt As ZXing.Result = bmr.Decode(bm.Clone())
            If rslt Is Nothing And tryCOunt < 0 Then
                tryCOunt += 1
                bmr.Options.TryHarder = True
                bmr.AutoRotate = True
                bmr.TryInverted = True
                bmr.Options.PureBarcode = False
                GoTo GOTO_RETRY
            Else
                If rslt Is Nothing Then
                    If tryCOunt <= 1 Then
                        tryCOunt += 1
                        bmr.Options.PureBarcode = True
                        GoTo GOTO_RETRY
                    Else
                        Return False
                    End If
                Else
                    If rslt.BarcodeFormat = Nothing Then
                        Return False
                    Else
                        Dim selIndex As Integer = -1
                        If rslt.BarcodeFormat Then
                            If rslt.BarcodeFormat = ZXing.BarcodeFormat.QR_CODE Then
                                txtMargin.Text = "0"
                            Else
                                txtMargin.Text = "-1"
                            End If
                            For selIndex = 0 To cmbSymbology.Items.Count - 1
                                If cmbSymbology.Items(selIndex).ToString.ToLower().Replace("_", " ") = rslt.BarcodeFormat.ToString.ToLower().Replace("_", " ") Then
                                    cmbSymbology.SelectedIndex = selIndex
                                    Label6.Text = ""
                                    chkPurBarCodeOnly.Checked = bmr.Options.PureBarcode
                                    Exit For
                                End If
                            Next
                        Else
                            Label6.Text = "ERROR: Invlid Code"
                            Return False
                        End If
                    End If
                End If
                If rslt.Text = "" Then
                    Return False
                Else
                    txtTextToEncode.Text = rslt.Text & ""
                End If
                Return True
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
        Return False
    End Function
    Public Function isValidBarCode(bmBytes() As Byte) As Boolean
        Try
            Label6.Text = ""
            Dim bm As Bitmap = Bitmap.FromStream(New MemoryStream(bmBytes))
            If bm Is Nothing Then Return False
            If bm.Width <= 0 Or bm.Height <= 0 Then Return False
            bmr = New ZXing.BarcodeReader()
            If bmr.Options.PossibleFormats Is Nothing Then bmr.Options.PossibleFormats = New List(Of ZXing.BarcodeFormat)
            If Not bmr.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.QR_CODE) Then bmr.Options.PossibleFormats.Add(ZXing.BarcodeFormat.QR_CODE)
            If Not bmr.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.PDF_417) Then bmr.Options.PossibleFormats.Add(ZXing.BarcodeFormat.PDF_417)
            If Not bmr.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.DATA_MATRIX) Then bmr.Options.PossibleFormats.Add(ZXing.BarcodeFormat.DATA_MATRIX)
            If Not bmr.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.EAN_13) Then bmr.Options.PossibleFormats.Add(ZXing.BarcodeFormat.EAN_13)
            If Not bmr.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.EAN_8) Then bmr.Options.PossibleFormats.Add(ZXing.BarcodeFormat.EAN_8)
            If Not bmr.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.AZTEC) Then bmr.Options.PossibleFormats.Add(ZXing.BarcodeFormat.AZTEC)
            If Not bmr.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.CODE_128) Then bmr.Options.PossibleFormats.Add(ZXing.BarcodeFormat.CODE_128)
            Dim tryCOunt As Integer = -1
GOTO_RETRY:
            Dim rslt As ZXing.Result = bmr.Decode(bm.Clone())
            If rslt Is Nothing And tryCOunt < 0 Then
                tryCOunt += 1
                bmr.Options.TryHarder = True
                bmr.AutoRotate = True
                bmr.TryInverted = True
                bmr.Options.PureBarcode = False
                GoTo GOTO_RETRY
            Else
                If rslt Is Nothing Then
                    If tryCOunt <= 1 Then
                        tryCOunt += 1
                        bmr.Options.PureBarcode = True
                        GoTo GOTO_RETRY
                    Else
                        Return False
                    End If
                Else
                    If rslt.BarcodeFormat = Nothing Then
                        Return False
                    Else
                        Dim selIndex As Integer = -1
                        If rslt.BarcodeFormat Then
                            If rslt.BarcodeFormat = ZXing.BarcodeFormat.QR_CODE Then
                                txtMargin.Text = "0"
                            Else
                                txtMargin.Text = "-1"
                            End If
                            For selIndex = 0 To cmbSymbology.Items.Count - 1
                                If cmbSymbology.Items(selIndex).ToString.ToLower().Replace("_", " ") = rslt.BarcodeFormat.ToString.ToLower().Replace("_", " ") Then
                                    cmbSymbology.SelectedIndex = selIndex
                                    chkPurBarCodeOnly.Checked = bmr.Options.PureBarcode
                                    Label6.Text = ""
                                    Exit For
                                End If
                            Next
                        Else
                            Label6.Text = "ERROR: Invlid Code"
                            Return False
                        End If
                    End If
                End If
                If rslt.Text = "" Then
                    Return False
                Else
                    txtTextToEncode.Text = rslt.Text & ""
                End If
                Return True
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
        Return False
    End Function
    Public barcodeFormats As New Dictionary(Of String, ZXing.BarcodeFormat)
    Public Shared Function isValidBarCode_Shared(bm As Bitmap) As Boolean
        Try
            If bm Is Nothing Then Return False
            If bm.Width <= 0 Or bm.Height <= 0 Then Return False
            bm = bm.Clone()
            Dim bmr1 = New ZXing.BarcodeReader()
            If bmr1.Options.PossibleFormats Is Nothing Then bmr1.Options.PossibleFormats = New List(Of ZXing.BarcodeFormat)
            If Not bmr1.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.QR_CODE) Then bmr1.Options.PossibleFormats.Add(ZXing.BarcodeFormat.QR_CODE)
            If Not bmr1.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.PDF_417) Then bmr1.Options.PossibleFormats.Add(ZXing.BarcodeFormat.PDF_417)
            If Not bmr1.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.DATA_MATRIX) Then bmr1.Options.PossibleFormats.Add(ZXing.BarcodeFormat.DATA_MATRIX)
            If Not bmr1.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.EAN_13) Then bmr1.Options.PossibleFormats.Add(ZXing.BarcodeFormat.EAN_13)
            If Not bmr1.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.EAN_8) Then bmr1.Options.PossibleFormats.Add(ZXing.BarcodeFormat.EAN_8)
            If Not bmr1.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.AZTEC) Then bmr1.Options.PossibleFormats.Add(ZXing.BarcodeFormat.AZTEC)
            If Not bmr1.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.CODE_128) Then bmr1.Options.PossibleFormats.Add(ZXing.BarcodeFormat.CODE_128)
            Dim tryCOunt As Integer = -1
GOTO_RETRY:
            Dim rslt As ZXing.Result = bmr1.Decode(bm)
            If rslt Is Nothing And tryCOunt < 0 Then
                tryCOunt += 1
                bmr1.Options.TryHarder = True
                bmr1.AutoRotate = True
                bmr1.TryInverted = True
                bmr1.Options.PureBarcode = False
                GoTo GOTO_RETRY
            Else
                If rslt Is Nothing Then
                    If tryCOunt <= 1 Then
                        tryCOunt += 1
                        bmr1.Options.PureBarcode = True
                        GoTo GOTO_RETRY
                    Else
                        Return False
                    End If
                Else
                    If rslt.BarcodeFormat = Nothing Then
                        Return False
                    Else
                        Dim selIndex As Integer = -1
                        If rslt.BarcodeFormat Then
                            Return True
                        Else
                            Return False
                        End If
                    End If
                End If
                If rslt.Text = "" Then
                    Return False
                Else
                    Return True
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
            If Not bmr1.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.QR_CODE) Then bmr1.Options.PossibleFormats.Add(ZXing.BarcodeFormat.QR_CODE)
            If Not bmr1.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.PDF_417) Then bmr1.Options.PossibleFormats.Add(ZXing.BarcodeFormat.PDF_417)
            If Not bmr1.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.DATA_MATRIX) Then bmr1.Options.PossibleFormats.Add(ZXing.BarcodeFormat.DATA_MATRIX)
            If Not bmr1.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.EAN_13) Then bmr1.Options.PossibleFormats.Add(ZXing.BarcodeFormat.EAN_13)
            If Not bmr1.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.EAN_8) Then bmr1.Options.PossibleFormats.Add(ZXing.BarcodeFormat.EAN_8)
            If Not bmr1.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.AZTEC) Then bmr1.Options.PossibleFormats.Add(ZXing.BarcodeFormat.AZTEC)
            If Not bmr1.Options.PossibleFormats.Contains(ZXing.BarcodeFormat.CODE_128) Then bmr1.Options.PossibleFormats.Add(ZXing.BarcodeFormat.CODE_128)
            Dim tryCOunt As Integer = -1
GOTO_RETRY:
            Dim rslt As ZXing.Result = bmr1.Decode(bm)
            If rslt Is Nothing And tryCOunt < 0 Then
                tryCOunt += 1
                bmr1.Options.TryHarder = True
                bmr1.AutoRotate = True
                bmr1.TryInverted = True
                bmr1.Options.PureBarcode = False
                GoTo GOTO_RETRY
            Else
                If rslt Is Nothing Then
                    If tryCOunt <= 1 Then
                        tryCOunt += 1
                        bmr1.Options.PureBarcode = True
                        GoTo GOTO_RETRY
                    Else
                        Return False
                    End If
                Else
                    If rslt.BarcodeFormat = Nothing Then
                        Return False
                    Else
                        Dim selIndex As Integer = -1
                        If rslt.BarcodeFormat Then
                            Return True
                        Else
                            Return False
                        End If
                    End If
                End If
                If rslt.Text = "" Then
                    Return False
                Else
                    Return True
                End If
                Return True
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
        Return False
    End Function
    Public Sub writeBarCode()
        If String.IsNullOrEmpty(txtTextToEncode.Text & "") Then
            Return
        End If
        Try
            Label6.Text = ""
            bmw = New ZXing.BarcodeWriter()
            barCodeOnly = chkPurBarCodeOnly.Checked
            OK_Button.Enabled = True
            Dim margin As Integer = -1
            Try
                If (txtMargin.Text = "" Or Not IsNumeric(txtMargin.Text)) Then margin = 0 Else margin = CInt(txtMargin.Text)
            Catch exOptions As Exception
                Err.Clear()
            End Try
            Try
                If IsNumeric(txtBarcodeWidth.Text) Then barCodeWidth = CInt(txtBarcodeWidth.Text)
            Catch exOptions As Exception
                Err.Clear()
            End Try
            Try
                If IsNumeric(txtBarcodeHeight.Text) Then barCodeHeight = CInt(txtBarcodeHeight.Text)
            Catch exOptions As Exception
                Err.Clear()
            End Try
            picBarcode.Visible = True
            Select Case cmbSymbology.SelectedItem().ToString().ToString.Replace("_"c, " "c).ToLower()
                Case "QR Code".ToLower
                    bmw.Format = ZXing.BarcodeFormat.QR_CODE
                    bmw.Options.Width = barCodeWidth
                    bmw.Options.Height = barCodeHeight
                    If margin >= 0 Then bmw.Options.Margin = margin
                    bmw.Options.PureBarcode = barCodeOnly
                    barCodeBitmap = bmw.Write(txtTextToEncode.Text.ToString()).Clone()
                    If barCodeBitmap.Width <> barCodeWidth Or barCodeBitmap.Height <> barCodeHeight Then
                        Dim imgBc As New Bitmap(barCodeWidth, barCodeHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
                        Using g As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(imgBc)
                            g.DrawImage(barCodeBitmap, CInt((barCodeWidth - barCodeBitmap.Width) / 2), CInt((barCodeHeight - barCodeBitmap.Height) / 2))
                        End Using
                        barCodeBitmap = New Bitmap(imgBc)
                    End If
                    picBarcode.Image = barCodeBitmap.Clone()
                Case "DATA MATRIX".ToLower
                    bmw.Format = ZXing.BarcodeFormat.DATA_MATRIX
                    bmw.Options.Width = barCodeWidth
                    bmw.Options.Height = barCodeHeight
                    If margin >= 0 Then bmw.Options.Margin = margin
                    bmw.Options.PureBarcode = barCodeOnly
                    barCodeBitmap = bmw.Write(txtTextToEncode.Text.ToString()).Clone()
                    If barCodeBitmap.Width <> barCodeWidth Or barCodeBitmap.Height <> barCodeHeight Then
                        Dim imgBc As New Bitmap(barCodeWidth, barCodeHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
                        Using g As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(imgBc)
                            g.DrawImage(barCodeBitmap, CInt((barCodeWidth - barCodeBitmap.Width) / 2), CInt((barCodeHeight - barCodeBitmap.Height) / 2))
                        End Using
                        barCodeBitmap = New Bitmap(imgBc)
                    End If
                    picBarcode.Image = barCodeBitmap.Clone()
                    Return
                Case "PDF 417".ToLower
                    bmw.Format = ZXing.BarcodeFormat.PDF_417
                    bmw.Options.Width = barCodeWidth
                    bmw.Options.Height = barCodeHeight
                    If margin >= 0 Then bmw.Options.Margin = margin
                    bmw.Options.PureBarcode = barCodeOnly
                    barCodeBitmap = bmw.Write(txtTextToEncode.Text.ToString()).Clone()
                    If barCodeBitmap.Width <> barCodeWidth Or barCodeBitmap.Height <> barCodeHeight Then
                        Dim imgBc As New Bitmap(barCodeWidth, barCodeHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
                        Using g As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(imgBc)
                            g.DrawImage(barCodeBitmap, CInt((barCodeWidth - barCodeBitmap.Width) / 2), CInt((barCodeHeight - barCodeBitmap.Height) / 2))
                        End Using
                        barCodeBitmap = New Bitmap(imgBc)
                    End If
                    picBarcode.Image = barCodeBitmap.Clone()
                    Return
                Case Else
                    Dim selIndex As Integer = -1
                    For Each bcf As ZXing.BarcodeFormat In barcodeFormats.Values.ToArray
                        selIndex += 1
                        If cmbSymbology.SelectedItem().ToString().ToString.Replace("_"c, " "c).ToLower() = bcf.ToString.Replace("_"c, " "c).ToLower() Then
                            Try
                                Select Case bcf.ToString().Replace("_"c, " "c).ToLower
                                    Case "EAN 8".ToLower, "EAN8".ToLower
                                        If Not IsValidEan8(txtTextToEncode.Text.ToString()) Then
                                            Label6.Text = "ERROR: Invlid Code"
                                            OK_Button.Enabled = False
                                            picBarcode.Image = Nothing
                                            picBarcode.Visible = False
                                            Return
                                        End If
                                    Case "EAN 12".ToLower, "EAN12".ToLower
                                        If Not IsValidEan12(txtTextToEncode.Text.ToString()) Then
                                            Label6.Text = "ERROR: Invlid Code"
                                            OK_Button.Enabled = False
                                            picBarcode.Image = Nothing
                                            picBarcode.Visible = False
                                            Return
                                        End If
                                    Case "EAN 13".ToLower, "EAN13".ToLower
                                        If Not IsValidEan13(txtTextToEncode.Text.ToString()) Then
                                            Label6.Text = "ERROR: Invlid Code"
                                            OK_Button.Enabled = False
                                            picBarcode.Image = Nothing
                                            picBarcode.Visible = False
                                            Return
                                        End If
                                    Case "EAN 14".ToLower, "EAN14".ToLower
                                        If Not IsValidEan14(txtTextToEncode.Text.ToString()) Then
                                            Label6.Text = "ERROR: Invlid Code"
                                            OK_Button.Enabled = False
                                            picBarcode.Image = Nothing
                                            picBarcode.Visible = False
                                            Return
                                        End If
                                End Select
                                bmw.Format = bcf
                                bmw.Options.Width = barCodeWidth
                                bmw.Options.Height = barCodeHeight
                                If margin >= 0 Then bmw.Options.Margin = margin
                                bmw.Options.PureBarcode = barCodeOnly
                                barCodeBitmap = bmw.Write(txtTextToEncode.Text.ToString()).Clone()
                                If barCodeBitmap.Width <> barCodeWidth Or barCodeBitmap.Height <> barCodeHeight Then
                                    Dim imgBc As New Bitmap(barCodeWidth, barCodeHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
                                    Using g As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(imgBc)
                                        g.DrawImage(barCodeBitmap, CInt((barCodeWidth - barCodeBitmap.Width) / 2), CInt((barCodeHeight - barCodeBitmap.Height) / 2))
                                    End Using
                                    barCodeBitmap = New Bitmap(imgBc)
                                End If
                                picBarcode.Image = barCodeBitmap.Clone()
                                picBarcode.Refresh()
                            Catch ex As Exception
                                Label6.Text = "ERROR: Invlid Code"
                                OK_Button.Enabled = False
                                picBarcode.Image = Nothing
                                picBarcode.Visible = False
                                Return
                            End Try
                            Exit Select
                        End If
                    Next
            End Select
            If Math.Abs(CInt(barCodeBitmap.Width) - barCodeWidth) > 2 Or Math.Abs(CInt(barCodeBitmap.Height) - barCodeHeight) > 2 Then
                If barCodeWidth > 0 Or barCodeHeight > 0 Then
                    barCodeBitmap = ResizeImage(barCodeWidth, barCodeHeight, barCodeBitmap.Clone())
                    picBarcode.Image = barCodeBitmap.Clone()
                End If
            End If
        Catch ex As Exception
            OK_Button.Enabled = False
            picBarcode.Image = Nothing
            picBarcode.Visible = False
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
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSymbology.SelectedIndexChanged
        writeBarCode()
    End Sub
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles chkPurBarCodeOnly.CheckedChanged
        writeBarCode()
    End Sub
    Private Sub dialogBarCodesZXING_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If barCodeWidth > 0 And barCodeHeight > 0 And Me.txtBarcodeWidth.Text = "-1" And Me.txtBarcodeHeight.Text = "-1" Then
                Me.txtBarcodeWidth.Text = barCodeWidth
                Me.txtBarcodeHeight.Text = barCodeHeight
            End If
            If Not barCodeText = "" And Me.txtTextToEncode.Text = "" Then
                Me.txtTextToEncode.Text = barCodeText
            End If
            If Not barCodeBitmap Is Nothing And Me.picBarcode.Image Is Nothing Then
                Me.picBarcode.Image = barCodeBitmap
                readBarCode(barCodeBitmap)
            End If
            setMouseWheelIncrement(Me.txtMargin)
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub txtBarcodeWidth_TextChanged(sender As Object, e As EventArgs) Handles txtBarcodeWidth.TextChanged
        writeBarCode()
    End Sub
    Private Sub txtBarcodeHeight_TextChanged(sender As Object, e As EventArgs) Handles txtBarcodeHeight.TextChanged
        writeBarCode()
    End Sub
    Private Sub txtMargin_TextChanged(sender As Object, e As EventArgs) Handles txtMargin.TextChanged
        writeBarCode()
    End Sub
    Private Sub picBarcode_Click(sender As Object, e As EventArgs) Handles picBarcode.Click
        If Not picBarcode.Image Is Nothing Then
            Clipboard.SetImage(picBarcode.Image.Clone())
            MessageBox.Show(Me, "Copied image to clipboard.", "Clipboard", vbOKOnly, MsgBoxStyle.Information)
        End If
    End Sub
    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles txtTextToEncode.TextChanged
        writeBarCode()
    End Sub
    Public Sub setMouseWheelIncrement(ByRef txtControl As TextBox)
        Try
            AddHandler txtControl.MouseWheel, AddressOf mouseWheelIncrementEvent
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public Sub mouseWheelIncrementEvent(ByVal sender As System.Object, ByVal e As MouseEventArgs)
        Try
            Dim txtControl As TextBox = DirectCast(sender, TextBox)
            If IsNumeric(txtControl.Text) Then
                Dim sngMargin As Integer = CInt(txtControl.Text) + 0
                sngMargin += IIf(e.Delta > 0, 1, -1)
                If sngMargin < -1 Then sngMargin = -1
                txtControl.Text = CInt(sngMargin.ToString)
            Else
                txtControl.Text = "0"
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
End Class
