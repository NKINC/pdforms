Imports System.Windows.Forms
Public Class dialogBruteForcePassword
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 Nicholas Kowalewicz All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>
    Public frm As frmMain
    Public pdfBytes() As Byte
    <System.Runtime.InteropServices.DllImport("kernel32.dll")>
    Private Shared Function SetThreadExecutionState(ByVal newExecutionState As ExecutionState) As ExecutionState
    End Function

    <Flags()>
    Public Enum ExecutionState As UInteger
        ES_SYSTEM_REQUIRED = 1UI
        ES_DISPLAY_REQUIRED = 2UI
        ES_USER_PRESENT = 4UI
        ES_CONTINUOUS = &H80000000UI
    End Enum

    Private Shared Sub SuspendSleep()
        ' This will prevent system entering sleep state due to inactivity.
        ' The user can still shutdown if they want to though.
        Dim oldState As ExecutionState = SetThreadExecutionState(ExecutionState.ES_CONTINUOUS Or ExecutionState.ES_DISPLAY_REQUIRED Or ExecutionState.ES_SYSTEM_REQUIRED)
    End Sub

    Private Shared Sub SetExecutionStateBackToNormal()
        ' When your app closes, things reset to normal anyway as this thread ends.
        ' You can send just es_continuous to reset whilst your app is still running.
        SetThreadExecutionState(ExecutionState.ES_CONTINUOUS)
    End Sub

    Public Sub New(ByRef frmMain1 As frmMain, pdfBytes1() As Byte)
        InitializeComponent()
        frm = frmMain1
        pdfBytes = pdfBytes1
    End Sub
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If frm Is Nothing Then
            Return
        End If
        If OK_Button.Text.ToLower = "stop".ToLower Then
            SetExecutionStateBackToNormal()
            If Not frm.pwCrack Is Nothing Then
                frm.pwCrack.forceStop = True
            End If
            clsBruteForcePw.InUse = False
            ProgressBar1.Value = 0
            ProgressBar2.Value = 0
            Button1.Enabled = False
        ElseIf OK_Button.Text.ToLower = "pause".ToLower Then
            SetExecutionStateBackToNormal()
            If Not frm.pwCrack Is Nothing Then
                frm.pwCrack.forcePause = True
            End If
            clsBruteForcePw.InUse = False
            OK_Button.Text = "CONTINUE"
            Button1.Enabled = True
        ElseIf OK_Button.Text.ToLower = "continue".ToLower Then
            SuspendSleep()
            If Not frm.pwCrack Is Nothing Then
                frm.pwCrack.forcePause = False
            End If
            clsBruteForcePw.InUse = True
            OK_Button.Text = "PAUSE"
            Button1.Enabled = True
        ElseIf OK_Button.Text.ToLower = "crack password".ToLower Then
            SuspendSleep()
            TextBox1.Text = ""
            Button1.Enabled = True
            If Not frm.pwCrack Is Nothing Then
                frm.pwCrack.forcePause = True
            End If
            ProgressBar1.Value = 0
            ProgressBar2.Value = 0
            frm.pwCrack = New clsBruteForcePw(frm, pdfBytes)
            frm.pwCrack.LoadChars(CheckBox5.Checked, CheckBox3.Checked, CheckBox1.Checked, CheckBox2.Checked, CheckBox4.Checked, CheckBox7.Checked)
            frm.TimeStampAdd("BRUTE FORCE PASSWORD: STARTED")
            TextBox2.Visible = False
            Label3.Visible = False
            frm.pwCrack.forcePause = False
            clsBruteForcePw.InUse = True
            'OK_Button.Text = "STOP"
            OK_Button.Text = "PAUSE"
            Dim pw As String = TextBox1.Text & ""
            Dim pwCharIdx As Integer = -1
            'If Not String.IsNullOrEmpty(TextBox1.Text) Then
            '    Dim pwChar As Char = pw.Substring(pw.Length - 1, 1)
            '    pw = pw.Substring(0, pw.Length - 1)
            '    pwCharIdx = frm.pwCrack.chars.LastIndexOf(pwChar)
            'End If
            Dim strPw As String = frm.pwCrack.tryCrackingPasswordBruteForce(pw, pwCharIdx, TrackBar1.Value, True, frm.ToolStripStatusLabel2, TextBox1, CheckBox6.Checked, ProgressBar1, ProgressBar2)
            If Not String.IsNullOrEmpty(CStr(strPw) & "") Then
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
        ElseIf OK_Button.Text.ToLower = "LOAD PDF".ToLower Then
            frm.pdfOwnerPassword = Me.TextBox2.Text & ""
            frm.OpenFile_WithPassword(frm.fpath, True, True, frm.pdfOwnerPassword & "")
            SetExecutionStateBackToNormal()
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        Else
            SetExecutionStateBackToNormal()
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        End If
    End Sub
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Try
            SetExecutionStateBackToNormal()
            If Not frm.pwCrack Is Nothing Then
                frm.pwCrack.forceStop = True
            End If
            clsBruteForcePw.InUse = False
        Catch ex As Exception
            Err.Clear()
        Finally
            ProgressBar1.Value = 0
            ProgressBar2.Value = 0
        End Try
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
        CheckBox7.Checked = CheckBox5.Checked
        CheckBox1.Enabled = Not CheckBox5.Checked
        CheckBox2.Enabled = Not CheckBox5.Checked
        CheckBox3.Enabled = Not CheckBox5.Checked
        CheckBox4.Enabled = Not CheckBox5.Checked
        CheckBox7.Enabled = Not CheckBox5.Checked
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub TextBox2_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles TextBox2.MouseDoubleClick
        If Not TextBox2.Text = "" Then
            TextBox2.SelectAll()
            Clipboard.SetText(TextBox2.Text.ToString())
            MsgBox("Copied passwword to clipboard", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.ApplicationModal)
        End If
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        If Not TextBox2.Text = "" Then
            TextBox2.SelectAll()
            Clipboard.SetText(TextBox2.Text.ToString())
            MsgBox("Copied passwword to clipboard", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.ApplicationModal)
        End If
    End Sub

    Private Sub Label3_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles Label3.MouseDoubleClick
        If Not TextBox2.Text = "" Then
            TextBox2.SelectAll()
            Clipboard.SetText(TextBox2.Text.ToString())
            MsgBox("Copied passwword to clipboard", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.ApplicationModal)
        End If
    End Sub

    Private Sub CheckBox7_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox7.CheckedChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            SetExecutionStateBackToNormal()
            If Not frm.pwCrack Is Nothing Then
                frm.pwCrack.forceStop = True
            End If
            clsBruteForcePw.InUse = False
        Catch ex As Exception
            Err.Clear()
        Finally
            ProgressBar1.Value = 0
            ProgressBar2.Value = 0
        End Try
    End Sub
End Class
