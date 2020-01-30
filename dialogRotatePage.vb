Imports System.Windows.Forms
Public Class dialogRotatePage
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 NK-INC.COM All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Public _rotationDegrees As Integer = 0
    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        Try
            If RadioButton1.Checked Then
                _rotationDegrees = 0
                PictureBox1.Image = Rotated_Image().Clone
            End If
        Catch ex As Exception
            _rotationDegrees = 0
        End Try
    End Sub
    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        Try
            If RadioButton2.Checked Then
                _rotationDegrees = 90
                PictureBox1.Image = Rotated_Image().Clone
            End If
        Catch ex As Exception
            _rotationDegrees = 0
        End Try
    End Sub
    Private Sub RadioButton3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton3.CheckedChanged
        Try
            If RadioButton3.Checked Then
                _rotationDegrees = 180
                PictureBox1.Image = Rotated_Image().Clone
            End If
        Catch ex As Exception
            _rotationDegrees = 0
        End Try
    End Sub
    Private Sub RadioButton4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton4.CheckedChanged
        Try
            If RadioButton4.Checked Then
                _rotationDegrees = 270
                PictureBox1.Image = Rotated_Image().Clone
            End If
        Catch ex As Exception
            _rotationDegrees = 0
        End Try
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            If RadioButton1.Checked Then
                _rotationDegrees = 90
                RadioButton2.Checked = True
            ElseIf RadioButton2.Checked Then
                _rotationDegrees = 180
                RadioButton3.Checked = True
            ElseIf RadioButton3.Checked Then
                _rotationDegrees = 270
                RadioButton4.Checked = True
            ElseIf RadioButton4.Checked Then
                _rotationDegrees = 0
                RadioButton1.Checked = True
            Else
                _rotationDegrees = 0
                RadioButton1.Checked = True
            End If
        Catch ex As Exception
            _rotationDegrees = 0
            RadioButton1.Checked = True
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If RadioButton1.Checked Then
                _rotationDegrees = 270
                RadioButton4.Checked = True
            ElseIf RadioButton2.Checked Then
                _rotationDegrees = 0
                RadioButton1.Checked = True
            ElseIf RadioButton3.Checked Then
                _rotationDegrees = 90
                RadioButton2.Checked = True
            ElseIf RadioButton4.Checked Then
                _rotationDegrees = 180
                RadioButton3.Checked = True
            Else
                _rotationDegrees = 0
                RadioButton1.Checked = True
            End If
        Catch ex As Exception
            _rotationDegrees = 0
            RadioButton1.Checked = True
        End Try
    End Sub
    Private Sub dialogRotatePage_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub
    Public pauseSetDegree As Boolean = False
    Public Sub New(ByVal img As System.Drawing.Image, ByVal rotationInitial As Integer, ByVal width As Integer, ByVal height As Integer)
        Try
            InitializeComponent()
            Image_NoRotation = img.Clone
            rotationDegrees = (rotationInitial)
            PictureBox1.Image = Rotated_Image().Clone
            Try
            Catch ex As Exception
                Err.Clear()
            End Try
            PictureBox1.Width = 300
            PictureBox1.Height = 300
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public Image_NoRotation As System.Drawing.Image
    Public Function Rotated_Image() As System.Drawing.Image
        Try
            Using g As Image = Image_NoRotation.Clone
                Select Case (_rotationDegrees)
                    Case 0
                        Return g.Clone
                    Case 90
                        g.RotateFlip(RotateFlipType.Rotate90FlipNone)
                        Return g.Clone
                    Case 180
                        g.RotateFlip(RotateFlipType.Rotate180FlipNone)
                        Return g.Clone
                    Case 270
                        g.RotateFlip(RotateFlipType.Rotate270FlipNone)
                        Return g.Clone
                    Case Else
                        Return g.Clone
                End Select
            End Using
        Catch ex As Exception
            Err.Clear()
        End Try
        Return Image_NoRotation.Clone
    End Function
    Public Property rotationDegrees() As Integer
        Get
            Try
                Return _rotationDegrees
            Catch ex As Exception
                Return _rotationDegrees
            End Try
        End Get
        Set(ByVal value As Integer)
            Try
                txtDegrees.Text = value.ToString
                _rotationDegrees = value
                If value = 0 Then
                    RadioButton1.Checked = True
                ElseIf value = 90 Then
                    RadioButton2.Checked = True
                ElseIf value = 180 Then
                    RadioButton3.Checked = True
                ElseIf value = 270 Then
                    RadioButton4.Checked = True
                Else
                End If
            Catch ex As Exception
                Err.Clear()
            End Try
        End Set
    End Property
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        If Not PictureBox1.Image Is Nothing Then
            Clipboard.SetImage(PictureBox1.Image.Clone())
            MessageBox.Show(Me, "Copied image to clipboard.", "Clipboard", vbOKOnly, MsgBoxStyle.Information)
        End If
    End Sub
End Class
