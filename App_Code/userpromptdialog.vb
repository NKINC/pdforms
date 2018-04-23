Public Class clsPromptDialog
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 Nicholas Kowalewicz All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>

    Dim prompt As Form
    Public Sub New()
    End Sub
    Public Function ShowDialog(ByVal text As String, ByVal caption As String, ByVal owner As Form, Optional ByVal defaultValue As String = "", Optional ByVal confirmationButtonText As String = "OK") As String
        Try
            prompt = New Form
            prompt.Width = 600
            prompt.Height = 150
            prompt.FormBorderStyle = FormBorderStyle.FixedDialog
            prompt.Text = caption
            prompt.StartPosition = FormStartPosition.CenterScreen
            Dim textLabel As New Label()
            textLabel.Left = 50
            textLabel.Top = prompt.Top + 20
            textLabel.Text = text
            textLabel.Width = prompt.Width - 100
            textLabel.TextAlign = ContentAlignment.BottomLeft
            textLabel.Font = New System.Drawing.Font(System.Drawing.FontFamily.GenericSansSerif, 11, FontStyle.Regular, GraphicsUnit.Pixel)
            Dim heightLabel As Integer = 14
            heightLabel += (text.Split(Chr(10)).Length * 14)
            textLabel.Height = heightLabel
            prompt.Controls.Add(textLabel)
            Dim textBox As New TextBox()
            textBox.Left = 50
            textBox.Top = textLabel.Bottom + 5
            textBox.Font = New System.Drawing.Font(System.Drawing.FontFamily.GenericSansSerif, 16, FontStyle.Regular, GraphicsUnit.Pixel)
            textBox.Width = prompt.Width - 100
            textBox.BackColor = Color.White
            textBox.BorderStyle = BorderStyle.FixedSingle
            If Not String.IsNullOrEmpty(defaultValue & "") Then textBox.Text = defaultValue & ""
            prompt.Controls.Add(textBox)
            Dim confirmation As New Button()
            confirmation.Text = confirmationButtonText
            confirmation.Width = confirmationButtonText.ToCharArray().Length * 16 + 20
            confirmation.Left = textBox.Right - confirmation.Width
            confirmation.Top = textBox.Bottom + 10
            confirmation.DialogResult = DialogResult.OK
            Dim heightConfirm As Integer = 16
            heightConfirm += (confirmationButtonText.Split(Chr(10)).Length * 16)
            confirmation.Height = 36
            confirmation.Font = New System.Drawing.Font(System.Drawing.FontFamily.GenericSansSerif, 16, FontStyle.Bold, GraphicsUnit.Pixel)
            confirmation.ForeColor = Color.DarkBlue
            prompt.Height = 20 + textLabel.Height + 10 + textBox.Height + 20 + confirmation.Height + 10 + 40
            prompt.Controls.Add(confirmation)
            prompt.AcceptButton = confirmation
            Select Case prompt.ShowDialog(owner)
                Case DialogResult.OK, DialogResult.Yes
                    Return textBox.Text & ""
                Case Else
                    Return textBox.Text & ""
            End Select
            Return ""
        Catch ex As Exception
            Err.Clear()
        Finally
            closeDialog()
        End Try
        Return ""
    End Function
    Private WithEvents textBoxFileSelection As New TextBox
    Private WithEvents btnOpenFile As New Button
    Private strdefaultextensionFilter As String = "PDF|*.pdf|All files|*.*"
    Public Function ShowDialogFileSelection(ByVal text As String, ByVal defaultValue As String, ByVal caption As String, ByVal owner As Form, Optional ByVal defaultextensionFilter As String = "PDF|*.pdf|All files|*.*") As String
        Try
            If Not String.IsNullOrEmpty(defaultextensionFilter & "") Then
                strdefaultextensionFilter = defaultextensionFilter
            End If
            prompt = New Form
            prompt.Width = 600
            prompt.Height = 150
            prompt.FormBorderStyle = FormBorderStyle.FixedDialog
            prompt.Text = caption
            prompt.StartPosition = FormStartPosition.CenterScreen
            Dim textLabel As New Label()
            textLabel.Left = 50
            textLabel.Top = 20
            textLabel.Text = text
            textLabel.Width = prompt.Width - 100
            prompt.Controls.Add(textLabel)
            textBoxFileSelection = New TextBox
            textBoxFileSelection.Left = 50
            textBoxFileSelection.Top = textLabel.Bottom
            textBoxFileSelection.Width = prompt.Width - 165
            textBoxFileSelection.BackColor = Color.White
            textBoxFileSelection.BorderStyle = BorderStyle.FixedSingle
            textBoxFileSelection.Text = defaultValue & ""
            prompt.Controls.Add(textBoxFileSelection)
            btnOpenFile.Left = textBoxFileSelection.Right + 10
            btnOpenFile.Top = textBoxFileSelection.Top
            btnOpenFile.Width = 50
            btnOpenFile.Height = textBoxFileSelection.Height
            btnOpenFile.BackColor = Color.White
            btnOpenFile.Text = "file.."
            prompt.Controls.Add(btnOpenFile)
            Dim confirmation As New Button()
            confirmation.Text = "Ok"
            confirmation.Left = textBoxFileSelection.Right - 100
            confirmation.Width = 100
            confirmation.Top = textBoxFileSelection.Bottom + 10
            confirmation.DialogResult = DialogResult.OK
            prompt.Controls.Add(confirmation)
            prompt.AcceptButton = confirmation
            Select Case prompt.ShowDialog(owner)
                Case DialogResult.OK, DialogResult.Yes
                    Return textBoxFileSelection.Text & ""
                Case Else
                    Return ""
            End Select
            Return ""
        Catch ex As Exception
            Err.Clear()
        Finally
            closeDialog()
        End Try
        Return ""
    End Function
    Public Sub openFileDialog(ByVal sender As Object, ByVal e As EventArgs) Handles btnOpenFile.Click
        Dim openfile1 As New System.Windows.Forms.OpenFileDialog
        Try
            openfile1.InitialDirectory = Application.StartupPath.ToString() & ""
            openfile1.FileName = ""
            If Not String.IsNullOrEmpty(textBoxFileSelection.Text & "") Then
                openfile1.InitialDirectory = System.IO.Path.GetDirectoryName(textBoxFileSelection.Text & "")
                openfile1.FileName = System.IO.Path.GetFileName(textBoxFileSelection.Text & "")
            End If
            openfile1.Multiselect = False
            If Not String.IsNullOrEmpty(strdefaultextensionFilter & "") Then
                openfile1.Filter = strdefaultextensionFilter & ""
                If openfile1.Filter.ToString().StartsWith("PDF") Then
                    openfile1.DefaultExt = ".pdf"
                End If
            Else
                openfile1.Filter = "PDF|*.pdf|All files|*.*"
                openfile1.DefaultExt = ".pdf"
            End If
            openfile1.FilterIndex = 0
            Select Case openfile1.ShowDialog()
                Case DialogResult.OK, DialogResult.Yes
                    textBoxFileSelection.Text = openfile1.FileName & ""
                Case Else
            End Select
            Return
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public Sub closeDialog()
        If Not prompt Is Nothing Then prompt.Close()
    End Sub
End Class
