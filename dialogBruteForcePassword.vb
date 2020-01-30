Imports System.Windows.Forms

Public Class dialogBruteForcePassword
    ''' <summary>
    ''' PdForms.net- Created by NK-INC.COM (www.PdForms.net)
    ''' Copyright 2017 NK-INC.COM All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Email Contact: hello@pdforms.net
    ''' Website: www.pdforms.net
    ''' </summary>

    Public frm As frmMain
    Public pdfBytes() As Byte
    Public Sub New(ByRef frmMain1 As frmMain, pdfBytes1() As Byte)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        frm = frmMain1
        pdfBytes = pdfBytes1
    End Sub
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If frm Is Nothing Then
            Return
        End If
        Try
            frm.Hide()
            If OK_Button.Text.ToLower = "stop".ToLower Then
                If Not frm.pwCrack Is Nothing Then
                    frm.pwCrack.forcePause = True
                End If
                clsBruteForcePw.InUse = False
                ProgressBar1.Value = 0
                ProgressBar2.Value = 0
            ElseIf OK_Button.Text.ToLower = "crack password".ToLower Then
                'Dim pdfBytes() As Byte = File.ReadAllBytes(frm.fpath)
                If Not frm.pwCrack Is Nothing Then
                    frm.pwCrack.forcePause = True
                End If
                ProgressBar1.Value = 0
                ProgressBar2.Value = 0
                frm.pwCrack = New clsBruteForcePw(frm, pdfBytes)
                frm.pwCrack.LoadChars(CheckBox5.Checked, CheckBox3.Checked, CheckBox1.Checked, CheckBox2.Checked, CheckBox4.Checked)
                frm.TimeStampAdd("BRUTE FORCE PASSWORD: STARTED")
                TextBox2.Visible = False
                Label3.Visible = False
                frm.pwCrack.forcePause = False
                clsBruteForcePw.InUse = True
                OK_Button.Text = "STOP"
                Dim strPw As String = frm.pwCrack.tryCrackingPasswordBruteForce("", TrackBar1.Value, True, frm.ToolStripStatusLabel2, TextBox1, CheckBox6.Checked, ProgressBar1, ProgressBar2)
                If Not String.IsNullOrEmpty(CStr(strPw) & "") Then
                    If frm.DoEvents_Wait(1000) Then
                        Beep()
                    End If
                    If frm.DoEvents_Wait(1000) Then
                        Beep()
                    End If
                    'MsgBox(strPw, MsgBoxStyle.OkOnly, "BRUTE FORCE PASSWORD CRACKED: see log")
                    TextBox2.Text = strPw
                    TextBox2.Visible = True
                    Label3.Visible = True
                    frm.TimeStampAdd("BRUTE FORCE FOUND PASSWORD: " & strPw)
                    frm.pdfOwnerPassword = strPw
                    clsBruteForcePw.InUse = False
                    If Not frm.pwCrack Is Nothing Then
                        frm.pwCrack.forcePause = True
                        frm.pwCrack = Nothing
                    End If
                    OK_Button.Text = "LOAD PDF"
                    ProgressBar1.Value = 100
                    ProgressBar2.Value = 100
                Else
                    frm.TimeStampAdd("BRUTE FORCE ATTEMPT FOUND NO PASSWORD")
                    clsBruteForcePw.InUse = False
                    If Not frm.pwCrack Is Nothing Then
                        frm.pwCrack.forcePause = True
                        frm.pwCrack = Nothing
                    End If
                    OK_Button.Text = "CRACK PASSWORD"
                End If
                clsBruteForcePw.InUse = False
            Else
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Me.Close()
            End If
        Catch ex As Exception
            Err.Clear()
        Finally
            frm.Show()
        End Try
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        If Not frm.pwCrack Is Nothing Then
            If clsBruteForcePw.InUse = True Then
                frm.pwCrack.forcePause = True
            End If
        End If
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        Try
            Label1.Text = "Character Limit: " & TrackBar1.Value.ToString()
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub

    Private Sub CheckBox5_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox5.CheckedChanged
        CheckBox1.Checked = CheckBox5.Checked
        CheckBox2.Checked = CheckBox5.Checked
        CheckBox3.Checked = CheckBox5.Checked
        CheckBox4.Checked = CheckBox5.Checked
        CheckBox1.Enabled = Not CheckBox5.Checked
        CheckBox2.Enabled = Not CheckBox5.Checked
        CheckBox3.Enabled = Not CheckBox5.Checked
        CheckBox4.Enabled = Not CheckBox5.Checked
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Try
            TextBox3.Text = CStr(IIf(Me.CheckBox6.Checked, TextBox1.Text.TrimStart() & "", TextBox1.Text & "")) & ""
        Catch ex As Exception
            TextBox3.Text = TextBox1.Text & ""
            frm.TimeStampAdd(ex)
        End Try
    End Sub
End Class
