Imports iTextSharp.text.pdf
Imports FDFApp
Public Class clsBruteForcePw
    ''' <summary>
    ''' PdForms.net- Created by NK-INC.COM (www.PdForms.net)
    ''' Copyright 2017 NK-INC.COM All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Email Contact: hello@pdforms.net
    ''' Website: www.pdforms.net
    ''' </summary>

    Public r As PdfReader
    Public s As PdfStamper
    Public pdfBytes() As Byte
    Public frm As frmMain
    Public forcePause As Boolean = False
    Public Shared InUse As Boolean = False
    Public Sub New()
        LoadChars(True)
    End Sub
    Public Sub New(ByRef f As frmMain)
        frm = f
        pdfBytes = frm.Session
        'LoadChars(True)
        LoadChars(False, False, True, False, False)
        If Not frm.IsPasswordProtected(pdfBytes) Then
            forcePause = True
        Else
            forcePause = False
        End If
    End Sub
    Public Sub New(ByRef f As frmMain, bytes() As Byte)
        frm = f
        pdfBytes = bytes
        'LoadChars(True)
        LoadChars(False, False, True, False, False)
        If Not frm.IsPasswordProtected(pdfBytes) Then
            forcePause = True
        Else
            forcePause = False
        End If
    End Sub
    Public chars As New List(Of Char)
    Public Sub LoadChars(all As Boolean, Optional numerals As Boolean = True, Optional lowerCase As Boolean = True, Optional upperCase As Boolean = True, Optional specials As Boolean = True)
        Dim lstArrays As New List(Of Integer())
        If all Then
            lstArrays.Add(New Integer() {32, 126})
        Else
            'lstArrays.Add(New Integer() {0, 1})
            If numerals Then
                lstArrays.Add(New Integer() {48, 57}) ''0-9
            End If
            If lowerCase Then
                lstArrays.Add(New Integer() {97, 122}) 'a-z
            End If
            If upperCase Then
                lstArrays.Add(New Integer() {65, 90}) 'A-Z
            End If
            If specials Then
                lstArrays.Add(New Integer() {32, 47})
                lstArrays.Add(New Integer() {58, 64})
                lstArrays.Add(New Integer() {91, 96})
                lstArrays.Add(New Integer() {123, 126})
            End If
            'lstArrays.Add(New Integer() {32, 255})
            'lstArrays.Add(New Integer() {32, 126})

        End If
        chars = New List(Of Char)
        If Not all And Not specials Then
            chars.Add(" ")
        End If
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
        If chars.Contains(" "c) Then
            chars.Remove(" "c)
        End If
        chars.Insert(0, " "c)
    End Sub

    Dim csize As Integer = 0
    Public upto As String = ""
    Public Function tryCrackingPasswordBruteForce(pw As String, Optional maxLength As Integer = 255, Optional checkAdditional As Boolean = True, Optional ByRef label1 As ToolStripLabel = Nothing, Optional textboxAttempting As TextBox = Nothing, Optional trimOnly As Boolean = False, Optional progressBar1 As ProgressBar = Nothing, Optional progressBar2 As ProgressBar = Nothing) As String
        Try
            csize = chars.Count - 1
        Catch ex As Exception
            Err.Clear()
        End Try
        Try
            If pdfBytes Is Nothing Then
                pdfBytes = frm.Session
            End If
            'Dim pwMaxPossibilities As Long = maxLength ^ maxLength
            Dim pwTemp1 As String = "", pwTemp2 As String = "", pwTemp3 As String = ""

            'r = New PdfReader(pdfBytes, Nothing)
            Dim pwLen As Integer = 1
            'pw = pw.Trim
            If Not String.IsNullOrEmpty(pw & "") Then
                pwLen = pw.Length + 1
            End If
            If pwLen <= 0 Then
                pwLen = 1
            End If

            If pwLen <= maxLength Then
                If True = True Then ' frm.IsPasswordProtected(pdfBytes) Then
                    If forcePause Then
                        Return Nothing
                    End If
                    If forcePause = False Then
                        upto = ""
                        Try
                            Dim strPw As String = Nothing
                            strPw = tryCrackingPasswordBruteForceNew_recursion(0, maxLength, pw, maxLength, checkAdditional, label1, textboxAttempting, trimOnly, progressBar1, progressBar2)
                            If Not strPw Is Nothing Then
                                Return strPw
                            End If
                        Catch ex As Exception
                            Err.Clear()
                        End Try

                        'Next
                    End If
                End If
            End If
        Catch ex As Exception
            frm.TimeStampAdd(ex)
        End Try
        Return Nothing
    End Function
    Public current As New List(Of Char)
    Private Function tryCrackingPasswordBruteForceNew_recursion(ByVal index As Integer, ByVal depth As Integer, ByRef pw As String, Optional ByRef maxLength As Integer = 255, Optional ByRef checkAdditional As Boolean = True, Optional ByRef label1 As ToolStripLabel = Nothing, Optional ByRef textboxAttempting As TextBox = Nothing, Optional ByRef trimOnly As Boolean = False, Optional ByRef progressBar1 As ProgressBar = Nothing, Optional ByRef progressBar2 As ProgressBar = Nothing) As String
        Try
            If forcePause = True Then
                frm.TimeStampAdd("BRUTE FORCE PASSWORD: canceled")
                Return Nothing
            End If
            If Not upto Is Nothing Then
                If upto.Length > 0 Then
                    current.Clear()
                    current.AddRange(upto.ToCharArray)
                End If
            End If
            pw = CStr(current.ToArray())
            For i As Integer = 0 To csize
                If forcePause = True Then
                    frm.TimeStampAdd("BRUTE FORCE PASSWORD: canceled")
                    Return Nothing
                End If
                If current.Count > index Then
                    current(index) = chars(i)
                Else
                    current.Add(chars(i))
                End If
                upto = CStr(current.ToArray())
                pw = upto
                Dim pwTemp1 As String = upto
                If Not textboxAttempting Is Nothing Then
                    textboxAttempting.Text = pwTemp1
                    textboxAttempting.Update()
                End If
                If Not label1 Is Nothing Then
                    label1.Text = "ATTEMPTNG: """ & pwTemp1 & """ - click this label to cancel brute force detection"
                End If
                Try
                    If Not progressBar1 Is Nothing Then
                        If Not progressBar1.Value = CInt(CSng((pwTemp1.TrimStart().Length) / maxLength) * 100) Then ' + CInt(CSng(charIdx / chars.Count) * (1 / chars.Count))
                            progressBar1.Value = CInt(CSng((pwTemp1.TrimStart().Length) / maxLength) * 100)
                            progressBar1.Update()
                        End If
                    End If
                Catch ex As Exception
                    Err.Clear()
                End Try
                Try
                    If frm.DoEvents_Wait(1) Then
                    End If
                    If trimOnly Then
                        If frm.IsPasswordValid(pdfBytes, frm.getBytes(CStr(pwTemp1.TrimStart()))) And Not String.IsNullOrEmpty(pwTemp1.TrimStart()) Then
                            forcePause = True
                            Return pwTemp1.TrimStart()
                        End If
                    Else
                        If frm.IsPasswordValid(pdfBytes, frm.getBytes(CStr(pwTemp1))) And Not String.IsNullOrEmpty(pwTemp1) Then
                            forcePause = True
                            Return pwTemp1
                        ElseIf checkAdditional Then
                            If forcePause = True Then
                                frm.TimeStampAdd("BRUTE FORCE PASSWORD: canceled")
                                Return Nothing
                            End If
                            If frm.IsPasswordValid(pdfBytes, frm.getBytes(CStr(pwTemp1.TrimStart()))) And Not String.IsNullOrEmpty(pwTemp1.TrimStart()) Then
                                forcePause = True
                                Return pwTemp1.TrimStart()
                            End If
                        End If
                    End If
                Catch ex As Exception
                    Err.Clear()
                End Try
                If index < (depth) Then
                    If forcePause = True Then
                        frm.TimeStampAdd("BRUTE FORCE PASSWORD: canceled")
                        Return Nothing
                    End If
                    Dim strCheck As String = tryCrackingPasswordBruteForceNew_recursion(index + 1, depth, pw, maxLength, checkAdditional, label1, textboxAttempting, trimOnly, progressBar1, progressBar2)
                    If Not strCheck Is Nothing Then
                        If Not strCheck = "" Then
                            Return strCheck
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
            frm.TimeStampAdd(ex)
        End Try
        Return Nothing
    End Function

End Class
