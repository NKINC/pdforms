Public Class frmFlashObject
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 NK-INC.COM All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>
    Public CloseForm As Boolean = False
    Public flashFormat As String = "application/shockwave-flash"
    Public flashBytes() As Byte = Nothing
    Public flashFileName As String = ""
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
    Public cancelled As Boolean = False
    Public deleteImage As Boolean = False
    Private Sub ImageRotation_btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImageRotation_btnCancel.Click
        Try
            cancelled = True
            CloseForm = True
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public Sub New()
        InitializeComponent()
    End Sub
    Private Sub ImageRotation_PictureBox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImageRotation_PictureBox.Click
    End Sub
    Private Sub ImageRotation_PictureBox_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ImageRotation_PictureBox.DoubleClick
        SaveFlashObject()
    End Sub
    Public Sub SaveFlashObject()
        Try
            Dim fn As String = Application.StartupPath.ToString.TrimEnd("\"c) & "\temp\", saveDialog As New System.Windows.Forms.SaveFileDialog
            saveDialog.Filter = "Shockwave Flash .swf|*.swf"
            saveDialog.DefaultExt = "jpg"
            saveDialog.FilterIndex = 0
            saveDialog.InitialDirectory = fn
            saveDialog.CheckFileExists = False
            saveDialog.CheckPathExists = True
            saveDialog.OverwritePrompt = True
            saveDialog.Title = "Save Flash Object"
            Select Case saveDialog.ShowDialog(Me)
                Case Windows.Forms.DialogResult.Yes, Windows.Forms.DialogResult.OK
                    fn = saveDialog.FileName
                    Select Case System.IO.Path.GetExtension(fn).ToString.TrimStart(".")
                        Case "swf"
                            System.IO.File.WriteAllBytes(fn, flashBytes)
                            flashFileName = fn
                        Case Else
                            Return
                    End Select
                Case Else
                    Return
            End Select
        Catch ex As Exception
            Err.Clear()
            Return
        End Try
    End Sub
    Public Sub SaveImage()
        Try
            Dim fn As String = Application.StartupPath.ToString.TrimEnd("\"c) & "\temp\", saveDialog As New System.Windows.Forms.SaveFileDialog
            saveDialog.Filter = "JPG|*.jpg|PNG|*.png|BMP|*.bmp|GIF|*.gif|TIFF|*.tif"
            saveDialog.DefaultExt = "jpg"
            saveDialog.FilterIndex = 0
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
        LoadFlash()
    End Sub
    Public Sub LoadFlash()
        Try
            Dim fn As String = Application.StartupPath.ToString.TrimEnd("\"c) & "\temp\", saveDialog As New System.Windows.Forms.OpenFileDialog
            saveDialog.Filter = "SWF: Shockwave Flash|*.swf"
            saveDialog.FilterIndex = 0
            saveDialog.InitialDirectory = fn
            saveDialog.CheckFileExists = True
            saveDialog.CheckPathExists = True
            saveDialog.Title = "Load Flash"
            Select Case saveDialog.ShowDialog(Me)
                Case Windows.Forms.DialogResult.Yes, Windows.Forms.DialogResult.OK
                    fn = saveDialog.FileName
                    Select Case System.IO.Path.GetExtension(fn).ToString.TrimStart(".")
                        Case "swf"
                            Me.WebBrowser1.Navigate(fn)
                            flashBytes = System.IO.File.ReadAllBytes(fn)
                            flashFileName = fn
                        Case Else
                            Return
                    End Select
                Case Else
                    Return
            End Select
        Catch ex As Exception
            Err.Clear()
            Return
        End Try
    End Sub
    Private Sub CopyImageToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyImageToolStripMenuItem.Click
        Try
            If Not ImageRotation_PictureBox.Image Is Nothing Then
                Clipboard.SetData(flashFormat, flashBytes)
                MessageBox.Show(Me, "Copied flash object to clipboard.", "Clipboard", vbOKOnly, MsgBoxStyle.Information)
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub ImageRotation_btnOK_Click(sender As Object, e As EventArgs) Handles ImageRotation_btnOK.Click
        Try
            cancelled = False
            CloseForm = True
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs)
    End Sub
End Class
