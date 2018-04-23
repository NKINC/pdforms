Imports iTextSharp.text.pdf
Imports FDFApp
Public Class clsBruteForcePw
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 Nicholas Kowalewicz All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>
    Public r As PdfReader
    Public s As PdfStamper
    Public pdfBytes() As Byte
    Public frm As frmMain
    Public forcePause As Boolean = False
    Public forceStop As Boolean = False
    Public Shared InUse As Boolean = False
    Public Sub New()
        LoadChars(True)
    End Sub
    Public Sub New(ByRef f As frmMain)
        forcePause = False
        frm = f
        pdfBytes = frm.Session

        LoadChars(False, False, True, False, False)
        If Not frm.IsPasswordProtected(pdfBytes) Then
            forceStop = True
        Else
            forceStop = False
        End If
    End Sub
    Public Sub New(ByRef f As frmMain, bytes() As Byte)
        forcePause = False
        frm = f
        pdfBytes = bytes

        LoadChars(False, False, True, False, False)
        If Not frm.IsPasswordProtected(pdfBytes) Then
            forceStop = True
        Else
            forceStop = False
        End If
    End Sub
    Public chars As New List(Of Char)
    Public Sub LoadChars(all As Boolean, Optional numerals As Boolean = True, Optional lowerCase As Boolean = True, Optional upperCase As Boolean = True, Optional specials As Boolean = True, Optional space As Boolean = True)
        Dim lstArrays As New List(Of Integer())
        If all Then
            'lstArrays.Add(New Integer() {32, 126})
            If lowerCase Then
                lstArrays.Add(New Integer() {97, 122})
            End If
            If upperCase Then
                lstArrays.Add(New Integer() {65, 90})
            End If
            If numerals Then
                lstArrays.Add(New Integer() {48, 57})
            End If
            If specials Then
                lstArrays.Add(New Integer() {32, 47})
                lstArrays.Add(New Integer() {58, 64})
                lstArrays.Add(New Integer() {91, 96})
                lstArrays.Add(New Integer() {123, 126})
            End If
        Else
            If lowerCase Then
                lstArrays.Add(New Integer() {97, 122})
            End If
            If upperCase Then
                lstArrays.Add(New Integer() {65, 90})
            End If
            If numerals Then
                lstArrays.Add(New Integer() {48, 57})
            End If
            If specials Then
                lstArrays.Add(New Integer() {32, 47})
                lstArrays.Add(New Integer() {58, 64})
                lstArrays.Add(New Integer() {91, 96})
                lstArrays.Add(New Integer() {123, 126})
            End If
        End If
        chars = New List(Of Char)
        'Try
        '    If Not chars.Contains("") Then
        '        chars.Add("")
        '    End If
        'Catch ex As Exception
        '    Err.Clear()
        'End Try
        For charArrayIndex As Integer = 0 To lstArrays.Count - 1
            For charIdx1 As Byte = lstArrays(charArrayIndex)(0) To lstArrays(charArrayIndex)(1) Step 1
                Try
                    If Not chars.Contains(ChrW(charIdx1)) Then
                        chars.Add(ChrW(charIdx1))
                    End If
                Catch ex As Exception
                    Err.Clear()
                End Try
            Next
        Next
        If (Not all And Not specials) Or space = True Then
            If chars.Contains(" "c) Then
                chars.Remove(" "c)
            End If
            If Not chars.Contains(" "c) Then
                chars.Insert(0, " "c)
            End If
        End If
        If Not space = True Then
            If chars.Contains(" "c) Then
                chars.Remove(" "c)
            End If
        End If
    End Sub
    Public Function tryCrackingPasswordBruteForce(pw As String, Optional pwCharIdx As Integer = 0, Optional maxLength As Integer = 255, Optional checkAdditional As Boolean = True, Optional ByRef label1 As ToolStripLabel = Nothing, Optional textboxAttempting As TextBox = Nothing, Optional trimOnly As Boolean = False, Optional progressBar1 As ProgressBar = Nothing, Optional progressBar2 As ProgressBar = Nothing) As String
        Try
            If forcePause = True Then
                Do Until (forcePause = False Or forceStop = True)
                    frm.DoEvents_Wait(100)
                Loop
            End If
            If forceStop = True Then
                frm.TimeStampAdd("BRUTE FORCE PASSWORD: canceled")
                Return Nothing
            End If
            If pdfBytes Is Nothing Then
                pdfBytes = frm.Session
            End If

            Dim pwTemp1 As String = "", pwTemp2 As String = "", pwTemp3 As String = ""


            Dim pwLen As Integer = 1
            If Not String.IsNullOrEmpty(pw & "") Then
                'If pw.Length > 1 And pwCharIdx >= 0 Then
                '    Dim pwChar As Char = pw.Substring(pw.Length - 1, 1)
                '    Dim pwTemp = pw.Substring(0, pw.Length - 1)
                '    pwCharIdx = chars.LastIndexOf(pwChar)
                '    If pwCharIdx >= 0 Then
                '        Return tryCrackingPasswordBruteForce(pwTemp, pwCharIdx, maxLength, checkAdditional, label1, textboxAttempting, trimOnly, progressBar1, progressBar2)
                '    End If
                'End If
                pwLen = pw.Length + 1
            End If
            If pwCharIdx < 0 Then
                pwCharIdx = 0
            End If
            If pwLen <= 0 Then
                pwLen = 1
            End If
            If pwLen <= maxLength Then
                If True = True Then
                    If forceStop = False Then
                        For charIdx As Integer = pwCharIdx To chars.Count - 1 Step 1
                            If forcePause = True Then
                                Do Until (forcePause = False Or forceStop = True)
                                    frm.DoEvents_Wait(100)
                                Loop
                            End If
                            If frm.DoEvents_Wait(1) Then
                                If forceStop = True Then
                                    frm.TimeStampAdd("BRUTE FORCE PASSWORD: canceled")
                                    Return Nothing
                                End If
                            End If
                            Try
                                'For charIdx As Integer = 0 To chars.Count - 1 Step 1
                                'If trimOnly Then
                                '    If chars(charIdx).ToString = " " Then
                                '        GoTo GoTo_NEXT
                                '    End If
                                'End If
                                pwTemp1 = pw & chars(charIdx).ToString & ""
                                If Not textboxAttempting Is Nothing Then
                                    textboxAttempting.Text = CStr(IIf(trimOnly, pwTemp1.TrimStart(), pwTemp1))
                                    'textboxAttempting.Text = pwTemp1
                                    'textboxAttempting.Update()
                                End If
                                If Not label1 Is Nothing Then
                                    label1.Text = "ATTEMPTNG: """ & CStr(IIf(trimOnly, pwTemp1.TrimStart(), pwTemp1)) & """ - click this label to cancel brute force detection"
                                End If
                                Try
                                    If Not progressBar1 Is Nothing Then
                                        If Not progressBar1.Value = CInt(CSng((pwTemp1.TrimStart().Length) / maxLength) * 100) Then
                                            If progressBar1.Value < CInt(CSng((pwTemp1.TrimStart().Length) / maxLength) * 100) Then
                                                progressBar1.Value = CInt(CSng((pwTemp1.TrimStart().Length) / maxLength) * 100)
                                            End If
                                            'progressBar1.Update()
                                        End If
                                    End If
                                Catch ex As Exception
                                    Err.Clear()
                                End Try
                                Try
                                    If Not progressBar2 Is Nothing Then
                                        If Not progressBar2.Value = CInt(CSng(charIdx / chars.Count) * 100) Then
                                            progressBar2.Value = CInt(CSng(charIdx / chars.Count) * 100)
                                            'progressBar2.Update()
                                        End If
                                    End If
                                Catch ex As Exception
                                    Err.Clear()
                                End Try

                                If Not textboxAttempting Is Nothing Then
                                    textboxAttempting.Update()
                                End If
                                If Not progressBar1 Is Nothing Then
                                    progressBar1.Update()
                                End If
                                If Not progressBar2 Is Nothing Then
                                    progressBar2.Update()
                                End If
                                If frm.DoEvents_Wait(1) Then
                                    If forceStop = True Then
                                        frm.TimeStampAdd("BRUTE FORCE PASSWORD: canceled")
                                        Return Nothing
                                    End If
                                End If
                                If trimOnly Then

                                    If frm.IsPasswordValid(pdfBytes, frm.getBytes(CStr(pwTemp1))) And Not String.IsNullOrEmpty(pwTemp1) Then
                                        forceStop = True
                                        Return pwTemp1
                                        'ElseIf frm.IsPasswordValid(pdfBytes, frm.getBytes(CStr(pwTemp1.Trim()))) And Not String.IsNullOrEmpty(pwTemp1.Trim()) Then
                                        '   forceStop = True
                                        '  Return pwTemp1.Trim()
                                    ElseIf frm.IsPasswordValid(pdfBytes, frm.getBytes(CStr(pwTemp1.TrimStart()))) And Not String.IsNullOrEmpty(pwTemp1.TrimStart()) Then
                                        forceStop = True
                                        Return pwTemp1.TrimStart
                                        'ElseIf frm.IsPasswordValid(pdfBytes, frm.getBytes(CStr(pwTemp1.TrimEnd()))) And Not String.IsNullOrEmpty(pwTemp1.TrimEnd()) Then
                                        '   forceStop = True
                                        '  Return pwTemp1.TrimEnd
                                    ElseIf checkAdditional Then
                                        'If frm.DoEvents_Wait(1) Then
                                        'End If
                                        'pwTemp2 = tryCrackingPasswordBruteForce(pwTemp1, maxLength, True, label1, textboxAttempting, trimOnly, progressBar1, progressBar2)
                                        'If Not String.IsNullOrEmpty(pwTemp2) Then
                                        '    forceStop = True
                                        '    Return pwTemp2
                                        'End If
                                    End If
                                Else

                                    If frm.IsPasswordValid(pdfBytes, frm.getBytes(CStr(pwTemp1))) And Not String.IsNullOrEmpty(pwTemp1) Then
                                        forceStop = True
                                        Return pwTemp1
                                    ElseIf checkAdditional Then
                                        'If frm.IsPasswordValid(pdfBytes, frm.getBytes(CStr(pwTemp1.Trim()))) And Not String.IsNullOrEmpty(pwTemp1.Trim()) Then
                                        '    forceStop = True
                                        '    Return pwTemp1.Trim()
                                        'ElseIf frm.IsPasswordValid(pdfBytes, frm.getBytes(CStr(pwTemp1.TrimStart()))) And Not String.IsNullOrEmpty(pwTemp1.TrimStart()) Then
                                        '    forceStop = True
                                        '    Return pwTemp1.TrimStart
                                        'ElseIf frm.IsPasswordValid(pdfBytes, frm.getBytes(CStr(pwTemp1.TrimEnd()))) And Not String.IsNullOrEmpty(pwTemp1.TrimEnd()) Then
                                        '    forceStop = True
                                        '    Return pwTemp1.TrimEnd
                                        'End If
                                        'If frm.DoEvents_Wait(1) Then
                                        'End If
                                        'pwTemp2 = tryCrackingPasswordBruteForce(pwTemp1, maxLength, True, label1, textboxAttempting, trimOnly, progressBar1, progressBar2)
                                        'If Not String.IsNullOrEmpty(pwTemp2) Then
                                        '    forceStop = True
                                        '    Return pwTemp2
                                        'End If
                                    End If
                                End If

                            Catch ex As Exception
                                Err.Clear()
                            End Try
GoTo_NEXT:
                            If forcePause = True Then
                                Do Until (forcePause = False Or forceStop = True)
                                    frm.DoEvents_Wait(100)
                                Loop
                            End If
                            If forceStop Then
                                Return Nothing
                            End If
                            If checkAdditional Then

                                'For charIdx As Integer = 0 To chars.Count - 1 Step 1
                                'pwTemp1 = pw & chars(charIdx).ToString
                                pwTemp2 = tryCrackingPasswordBruteForce(pwTemp1, -1, maxLength, checkAdditional, label1, textboxAttempting, trimOnly, progressBar1, progressBar2)
                                If Not String.IsNullOrEmpty(pwTemp2) Then
                                    forceStop = True
                                    Return pwTemp2
                                End If
                                'Next
                            End If
                        Next

                    End If
                End If
            End If
        Catch ex As Exception
            frm.TimeStampAdd(ex)
        End Try
        Return Nothing
    End Function
End Class
