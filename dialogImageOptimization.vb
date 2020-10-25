Imports System.Windows.Forms
Public Class dialogImageOptimization
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 Nicholas Kowalewicz All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>
    Public b() As Byte = Nothing
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            If Not b Is Nothing Then
                If b.Length > 0 Then
                    If Not frmMainParent Is Nothing Then
                        frmMainParent.Session = b
                        frmMainParent.A0_LoadPDF()
                        Me.DialogResult = System.Windows.Forms.DialogResult.OK
                    Else
                        imgBytes = b
                        Me.DialogResult = System.Windows.Forms.DialogResult.OK
                    End If
                    imgBytes = b
                    Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Else
                    Me.DialogResult = System.Windows.Forms.DialogResult.Abort
                End If
            Else
                Me.DialogResult = System.Windows.Forms.DialogResult.Abort
            End If
        Catch ex As Exception
            Me.DialogResult = System.Windows.Forms.DialogResult.Abort
            Err.Clear()
        Finally
            If Not frmMainParent Is Nothing Then
                frmMainParent.Show()
            End If
            Me.Close()
        End Try
    End Sub
    Public Sub CloseForm()
        Try
            cOptimize.abortOptimizeProcess()
            If imgBytes Is Nothing And Not frmMainParent Is Nothing Then
                Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
            End If
        Catch ex As Exception
            Err.Clear()
        Finally
            If Not frmMainParent Is Nothing Then
                frmMainParent.Show()
            End If
        End Try
    End Sub
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        If Not sBytes Is Nothing Then
            If sBytes.Length > 0 Then
                If Not frmMainParent Is Nothing Then
                    frmMainParent.Session = sBytes
                    frmMainParent.A0_LoadPDF()
                End If
            End If
        End If
        Me.Close()
    End Sub
    Public frmMainParent As frmMain = Nothing
    Public imgBytes() As Byte = Nothing
    Dim imgMaskBytes() As Byte = Nothing
    Dim imgFormat As System.Drawing.Imaging.ImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg
    Public Sub New(ByRef frmMain1 As frmMain)
        InitializeComponent()
        frmMainParent = frmMain1
    End Sub
    Public Sub New(ByRef imageBytes() As Byte, Optional ByVal imageFormat As System.Drawing.Imaging.ImageFormat = Nothing, Optional ByVal imageMaskBytes() As Byte = Nothing)
        InitializeComponent()
        frmMainParent = Nothing
        imgBytes = imageBytes
        imgMaskBytes = imageMaskBytes
        imgFormat = imageFormat
    End Sub
    Public cOptimize As New clsPDFOptimization()
    Private Sub dialogImageOptimization_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        CloseForm()
    End Sub
    Private Sub dialogImageOptimization_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            For i As Integer = 5000 To 2000 Step -500
                cmb_ImageResizeSizePercent.Items.Add(i.ToString & "% size")
                cmb_PageResizeSizePercent.Items.Add(i.ToString & "% size")
            Next
            For i As Integer = 1900 To 1000 Step -100
                cmb_ImageResizeSizePercent.Items.Add(i.ToString & "% size")
                cmb_PageResizeSizePercent.Items.Add(i.ToString & "% size")
            Next
            For i As Integer = 950 To 500 Step -50
                cmb_ImageResizeSizePercent.Items.Add(i.ToString & "% size")
                cmb_PageResizeSizePercent.Items.Add(i.ToString & "% size")
            Next
            For i As Integer = 475 To 200 Step -25
                cmb_ImageResizeSizePercent.Items.Add(i.ToString & "% size")
                cmb_PageResizeSizePercent.Items.Add(i.ToString & "% size")
            Next
            For i As Integer = 190 To 0 Step -10
                cmb_ImageResizeSizePercent.Items.Add(i.ToString & "% size")
                cmb_PageResizeSizePercent.Items.Add(i.ToString & "% size")
            Next
            cmb_SmoothingMode.SelectedIndex = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
            cmb_CompositingQuality.SelectedIndex = System.Drawing.Drawing2D.CompositingQuality.HighQuality
            cmb_InterpolationMode.SelectedIndex = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
            If Not imgMaskBytes Is Nothing Then
                If imgMaskBytes.Length > 0 Then
                    chkOptimizePngMasks.Checked = True
                    chkAutoResize.Checked = False
                Else
                    chkAutoResize.Checked = True
                End If
            Else
                chkAutoResize.Checked = True
            End If
            For i As Integer = 0 To cmb_PageResizeSizePercent.Items.Count - 1
                If chkAutoResize.Checked = True Then
                    If cmb_ImageResizeSizePercent.Items(i).ToString = ("100".ToString & "% size") Then
                        cmb_ImageResizeSizePercent.SelectedIndex = i
                    End If
                Else
                    If cmb_ImageResizeSizePercent.Items(i).ToString = ("100".ToString & "% size") Then
                        cmb_ImageResizeSizePercent.SelectedIndex = i
                    End If
                End If
                If cmb_PageResizeSizePercent.Items(i).ToString = ("100".ToString & "% size") Then
                    cmb_PageResizeSizePercent.SelectedIndex = i
                    Exit For
                End If
            Next
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub cmb_ImageResizeSizePercent_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_ImageResizeSizePercent.SelectedIndexChanged
    End Sub
    Public Function getMegaBytesText(ByVal differenceTemp As Single) As String
        Dim diffText As String = "bytes"
        Dim difference As Single = Math.Abs(differenceTemp) + 0
        If difference > 1024 Then
            difference = difference / 1024
            diffText = "kb"
            If difference > 1024 Then
                difference = difference / 1024
                diffText = "Mb"
                If difference > 1024 Then
                    difference = difference / 1024
                    diffText = "GB"
                    If difference > 1024 Then
                        difference = difference / 1024
                        diffText = "TB"
                    End If
                End If
            End If
        End If
        If differenceTemp < 0 Then
            difference = difference * -1
        End If
        Return CStr(Math.Round(difference, 2) & " " & diffText).ToString & ""
    End Function
    Public sBytes() As Byte = Nothing
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If sBytes Is Nothing Then
                If Not frmMainParent Is Nothing Then
                    sBytes = frmMainParent.Session
                End If
            End If
            If imgBytes Is Nothing Then
                cOptimize = New clsPDFOptimization
                cOptimize.cancelOptimize = False
                clsPDFOptimization.cancelOptimize_Shared = False
                b = cOptimize.Optimize_Images(sBytes, frmMainParent.getBytes(frmMainParent.pdfOwnerPassword & ""), CSng(cmb_ImageResizeSizePercent.SelectedItem.ToString.Replace("% size", "")) / 100, 100, DirectCast(cmb_InterpolationMode.SelectedIndex, System.Drawing.Drawing2D.InterpolationMode), DirectCast(cmb_SmoothingMode.SelectedIndex, System.Drawing.Drawing2D.SmoothingMode), DirectCast(cmb_CompositingQuality.SelectedIndex, System.Drawing.Drawing2D.CompositingQuality), ProgressBar1, CSng(cmb_PageResizeSizePercent.SelectedItem.ToString.Replace("% size", "")) / 100, 3, chkAutoResize.Checked, chkOptimizePngMasks.Checked)
                Dim difference As Single = (sBytes.Length - b.Length)
                lblResults.Text = "Original: " & getMegaBytesText(sBytes.Length) & Environment.NewLine & "Optimized: " & getMegaBytesText(b.Length) & Environment.NewLine & CStr(IIf(difference <= 0, "Increase: ", "Decrease: ")) & getMegaBytesText(Math.Abs(difference)) & ""
                OK_Button.Visible = True
            ElseIf frmMainParent Is Nothing Then
                cOptimize = New clsPDFOptimization
                cOptimize.cancelOptimize = False
                clsPDFOptimization.cancelOptimize_Shared = False
                sBytes = imgBytes
                b = cOptimize.optimizeBitmap(imgBytes, CSng(cmb_ImageResizeSizePercent.SelectedItem.ToString.Replace("% size", "")) / 100, imgFormat, DirectCast(cmb_InterpolationMode.SelectedIndex, System.Drawing.Drawing2D.InterpolationMode), DirectCast(cmb_SmoothingMode.SelectedIndex, System.Drawing.Drawing2D.SmoothingMode), DirectCast(cmb_CompositingQuality.SelectedIndex, System.Drawing.Drawing2D.CompositingQuality), 3, imgMaskBytes, chkAutoResize.Checked, ProgressBar1)
                Dim difference As Single = (sBytes.Length - b.Length)
                lblResults.Text = "Original: " & getMegaBytesText(sBytes.Length) & Environment.NewLine & "Optimized: " & getMegaBytesText(b.Length) & Environment.NewLine & CStr(IIf(difference <= 0, "Increase: ", "Decrease: ")) & getMegaBytesText(Math.Abs(difference)) & ""
                OK_Button.Visible = True
            End If
        Catch ex As Exception
            Err.Clear()
        Finally
            Me.Show()
            Me.BringToFront()
        End Try
    End Sub
    Private Sub chkAllowTransparent_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAllowTransparent.CheckedChanged
    End Sub
    Private Sub dialogImageOptimization_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Me.Focus()
            Me.BringToFront()
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
End Class
