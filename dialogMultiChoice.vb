Imports System.Windows.Forms
Public Class dialogMultiChoice
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 Nicholas Kowalewicz All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>
    Dim frm As frmMain = Nothing
    Public Sub New(ByRef ownerFrmMain As System.Windows.Forms.IWin32Window)
        InitializeComponent()
        If Not Me.Owner Is Nothing Then
            If Me.Owner.GetType Is GetType(frmMain) Then
                frm = DirectCast(Me.Owner, frmMain)
            End If
        End If
        Me.DialogResult = Windows.Forms.DialogResult.None
    End Sub
    Public Sub New()
        InitializeComponent()
        Me.DialogResult = Windows.Forms.DialogResult.None
    End Sub
    Public DefaultButtonIndex As Integer = 0
    Public dialogResult1 As Integer = Windows.Forms.DialogResult.Yes
    Public dialogResult2 As Integer = Windows.Forms.DialogResult.No
    Public dialogResult3 As Integer = Windows.Forms.DialogResult.Cancel
    Public dialogResult4 As Integer = Windows.Forms.DialogResult.Abort
    Public Class clsButton
        Public buttonText As String = ""
        Public buttonVisible As Boolean = False
        Public buttonResult As Integer = -1
        Public Sub New(ByVal buttonText1 As String, ByVal buttonVisible1 As Boolean, ByVal buttonResult1 As Integer)
            buttonText = buttonText1
            buttonVisible = buttonVisible1
            buttonResult = buttonResult1
        End Sub
        Public Function add(ByRef lstClsButton As List(Of clsButton), ByVal buttonText As String, ByVal buttonVisible As Boolean, ByVal buttonResult As Integer) As List(Of clsButton)
            lstClsButton.Add(New clsButton(buttonText, buttonVisible, buttonResult))
            Return lstClsButton
        End Function
    End Class
    Public buttons As New List(Of clsButton)
    Public Sub ButtonAdd(ByVal buttonText As String, ByVal buttonVisible As Boolean, ByVal buttonResult As Integer)
        buttons.Add(New clsButton(buttonText, buttonVisible, buttonResult))
    End Sub
    Public Sub ButtonsClear()
        If buttons Is Nothing Then
            buttons = New List(Of clsButton)
        ElseIf buttons.Count > 0 Then
            buttons.Clear()
        End If
    End Sub
    Public Function ButtonsArray() As clsButton()
        If buttons Is Nothing Then
            buttons = New List(Of clsButton)
        End If
        Return buttons.ToArray
    End Function
    Public Overloads Function ShowDialog(ByRef meOwner As frmMain, ByVal message As String, ByVal title As String, ByVal buttonClasses() As clsButton) As DialogResult
        Me.Owner = meOwner
        Me.lblMessage.Text = message & ""
        Me.Text = title & ""
        Try
            Button1.Text = ""
            Button1.Visible = False
            dialogResult1 = -1
            Button2.Text = ""
            Button2.Visible = False
            dialogResult2 = -1
            Button3.Text = ""
            Button3.Visible = False
            dialogResult3 = -1
            Button4.Text = ""
            Button4.Visible = False
            dialogResult4 = -1
            If buttonClasses.Count > 0 Then
                Button1.Text = buttonClasses(0).buttonText
                Button1.Visible = buttonClasses(0).buttonVisible
                dialogResult1 = buttonClasses(0).buttonResult
                If buttonClasses.Count > 1 Then
                    Button2.Text = buttonClasses(1).buttonText
                    Button2.Visible = buttonClasses(1).buttonVisible
                    dialogResult2 = buttonClasses(1).buttonResult
                    If buttonClasses.Count > 2 Then
                        Button3.Text = buttonClasses(2).buttonText
                        Button3.Visible = buttonClasses(2).buttonVisible
                        dialogResult3 = buttonClasses(2).buttonResult
                        If buttonClasses.Count > 3 Then
                            Button4.Text = buttonClasses(3).buttonText
                            Button4.Visible = buttonClasses(3).buttonVisible
                            dialogResult4 = buttonClasses(3).buttonResult
                        End If
                    End If
                End If
            End If
            Select Case DefaultButtonIndex
                Case 0
                    AcceptButton = Button1
                Case 1
                    AcceptButton = Button2
                Case 2
                    AcceptButton = Button3
                Case 3
                    AcceptButton = Button4
                Case Else
                    AcceptButton = Button1
            End Select
            If Not Me.Owner Is Nothing Then
                If Me.Owner.GetType Is GetType(frmMain) Then
                    frm = DirectCast(Me.Owner, frmMain)
                    Return Me.ShowDialog(Me.Owner)
                End If
            End If
            Return Me.DialogResult
        Catch ex As Exception
            Err.Clear()
        Finally
            Me.Close()
        End Try
    End Function
    Public Overloads Function ShowDialog(ByRef meOwner As frmMain, ByVal title As String, ByVal buttonClasses() As clsButton) As DialogResult
        Me.Owner = meOwner
        Me.Text = title & ""
        Try
            Button1.Text = ""
            Button1.Visible = False
            dialogResult1 = -1
            Button2.Text = ""
            Button2.Visible = False
            dialogResult2 = -1
            Button3.Text = ""
            Button3.Visible = False
            dialogResult3 = -1
            Button4.Text = ""
            Button4.Visible = False
            dialogResult4 = -1
            If buttonClasses.Count > 0 Then
                Button1.Text = buttonClasses(0).buttonText
                Button1.Visible = buttonClasses(0).buttonVisible
                dialogResult1 = buttonClasses(0).buttonResult
                If buttonClasses.Count > 1 Then
                    Button2.Text = buttonClasses(1).buttonText
                    Button2.Visible = buttonClasses(1).buttonVisible
                    dialogResult2 = buttonClasses(1).buttonResult
                    If buttonClasses.Count > 2 Then
                        Button3.Text = buttonClasses(2).buttonText
                        Button3.Visible = buttonClasses(2).buttonVisible
                        dialogResult3 = buttonClasses(2).buttonResult
                        If buttonClasses.Count > 3 Then
                            Button4.Text = buttonClasses(3).buttonText
                            Button4.Visible = buttonClasses(3).buttonVisible
                            dialogResult4 = buttonClasses(3).buttonResult
                        End If
                    End If
                End If
            End If
            Select Case DefaultButtonIndex
                Case 0
                    AcceptButton = Button1
                Case 1
                    AcceptButton = Button2
                Case 2
                    AcceptButton = Button3
                Case 3
                    AcceptButton = Button4
                Case Else
                    AcceptButton = Button1
            End Select
            If Not Me.Owner Is Nothing Then
                If Me.Owner.GetType Is GetType(frmMain) Then
                    frm = DirectCast(Me.Owner, frmMain)
                    Return Me.ShowDialog(Me.Owner)
                End If
            End If
            Return Me.DialogResult
        Catch ex As Exception
            Err.Clear()
        Finally
            Me.Close()
        End Try
    End Function
    Public Overloads Function ShowDialog(ByVal message As String, ByVal title As String, ByVal buttonText() As String, ByVal buttonVisible() As Boolean, ByVal buttonResults() As Integer) As DialogResult
        Me.lblMessage.Text = message & ""
        Me.Text = title & ""
        Try
            Button1.Text = ""
            Button1.Visible = False
            dialogResult1 = -1
            Button2.Text = ""
            Button2.Visible = False
            dialogResult2 = -1
            Button3.Text = ""
            Button3.Visible = False
            dialogResult3 = -1
            Button4.Text = ""
            Button4.Visible = False
            dialogResult4 = -1
            If buttonText.Length > 0 Then
                Button1.Text = buttonText(0)
                Button1.Visible = buttonVisible(0)
                dialogResult1 = buttonResults(0)
                If buttonText.Length > 1 Then
                    Button2.Text = buttonText(1)
                    Button2.Visible = buttonVisible(1)
                    dialogResult2 = buttonResults(1)
                    If buttonText.Length > 2 Then
                        Button3.Text = buttonText(2)
                        Button3.Visible = buttonVisible(2)
                        dialogResult3 = buttonResults(2)
                        If buttonText.Length > 3 Then
                            Button4.Text = buttonText(3)
                            Button4.Visible = buttonVisible(3)
                            dialogResult4 = buttonResults(3)
                        End If
                    End If
                End If
            End If
            Select Case DefaultButtonIndex
                Case 0
                    AcceptButton = Button1
                Case 1
                    AcceptButton = Button2
                Case 2
                    AcceptButton = Button3
                Case 3
                    AcceptButton = Button4
                Case Else
                    AcceptButton = Button1
            End Select
            If Not Me.Owner Is Nothing Then
                If Me.Owner.GetType Is GetType(frmMain) Then
                    frm = DirectCast(Me.Owner, frmMain)
                    Return Me.ShowDialog(Me.Owner)
                End If
            End If
            Return Me.DialogResult
        Catch ex As Exception
            Err.Clear()
        Finally
            Me.Close()
        End Try
    End Function
    Public Overloads Function ShowDialog(ByVal message As String, ByVal MsgBoxStyleInt As Integer, ByVal title As String) As DialogResult
        Me.lblMessage.Text = message & ""
        Me.Text = title & ""
        Try
            If (MsgBoxStyleInt + MsgBoxStyle.YesNo) > 0 Then
                Button1.Text = "YES"
                Button2.Text = "No"
                Button3.Text = "Cancel"
                Button4.Text = "Don't Ask"
                Button1.Visible = True
                Button2.Visible = True
                Button3.Visible = False
                Button4.Visible = False
                dialogResult1 = Windows.Forms.DialogResult.Yes
                dialogResult2 = Windows.Forms.DialogResult.No
                dialogResult3 = Windows.Forms.DialogResult.Cancel
                dialogResult4 = Windows.Forms.DialogResult.Abort
            ElseIf (MsgBoxStyleInt + MsgBoxStyle.YesNoCancel) > 0 Then
                Button1.Text = "YES"
                Button2.Text = "No"
                Button3.Text = "Cancel"
                Button4.Text = "Don't Ask"
                Button1.Visible = True
                Button2.Visible = True
                Button4.Visible = True
                Button3.Visible = False
                dialogResult1 = Windows.Forms.DialogResult.Yes
                dialogResult2 = Windows.Forms.DialogResult.No
                dialogResult3 = Windows.Forms.DialogResult.Cancel
                dialogResult4 = Windows.Forms.DialogResult.Abort
            ElseIf (MsgBoxStyleInt + MsgBoxStyle.OkOnly) > 0 Then
                Button1.Text = "Ok"
                Button2.Text = "No"
                Button3.Text = "Cancel"
                Button4.Text = "Don't Ask"
                Button1.Visible = True
                Button2.Visible = False
                Button4.Visible = False
                Button3.Visible = False
                dialogResult1 = Windows.Forms.DialogResult.OK
                dialogResult2 = Windows.Forms.DialogResult.No
                dialogResult3 = Windows.Forms.DialogResult.Cancel
                dialogResult4 = Windows.Forms.DialogResult.Abort
            ElseIf (MsgBoxStyleInt + MsgBoxStyle.OkCancel) > 0 Then
                Button1.Text = "Ok"
                Button2.Text = "No"
                Button3.Text = "Cancel"
                Button4.Text = "Don't Ask"
                Button1.Visible = True
                Button2.Visible = False
                Button3.Visible = True
                Button4.Visible = False
                dialogResult1 = Windows.Forms.DialogResult.OK
                dialogResult2 = Windows.Forms.DialogResult.No
                dialogResult3 = Windows.Forms.DialogResult.Cancel
                dialogResult4 = Windows.Forms.DialogResult.Abort
            ElseIf (MsgBoxStyleInt + MsgBoxStyle.AbortRetryIgnore) > 0 Then
                Button1.Text = "Ok"
                Button2.Text = "Ignore"
                Button3.Text = "Retry"
                Button4.Text = "Abort"
                Button1.Visible = False
                Button2.Visible = True
                Button3.Visible = True
                Button4.Visible = True
                dialogResult1 = Windows.Forms.DialogResult.OK
                dialogResult2 = Windows.Forms.DialogResult.Ignore
                dialogResult3 = Windows.Forms.DialogResult.Retry
                dialogResult4 = Windows.Forms.DialogResult.Abort
            Else
                Button1.Text = "YES"
                Button2.Text = "No"
                Button3.Text = "Cancel"
                Button4.Text = "Yes,Don't ask"
                Button1.Visible = True
                Button2.Visible = True
                Button4.Visible = True
                Button3.Visible = True
                dialogResult1 = Windows.Forms.DialogResult.Yes
                dialogResult2 = Windows.Forms.DialogResult.No
                dialogResult3 = Windows.Forms.DialogResult.Cancel
                dialogResult4 = Windows.Forms.DialogResult.Abort
            End If
            Select Case DefaultButtonIndex
                Case 0
                    AcceptButton = Button1
                Case 1
                    AcceptButton = Button2
                Case 2
                    AcceptButton = Button3
                Case 3
                    AcceptButton = Button4
                Case Else
                    AcceptButton = Button1
            End Select
            If Not Me.Owner Is Nothing Then
                If Me.Owner.GetType Is GetType(frmMain) Then
                    frm = DirectCast(Me.Owner, frmMain)
                    Return Me.ShowDialog(Me.Owner)
                End If
            End If
            Return Me.DialogResult
        Catch ex As Exception
            Err.Clear()
        Finally
            Me.Close()
        End Try
    End Function
    Dim blnSelected As Boolean = False
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.DialogResult = DirectCast(dialogResult1, DialogResult)
        If frm.GetType Is GetType(frmMain) And Not frm Is Nothing Then
            blnSelected = True
        Else
            blnSelected = True
        End If
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.DialogResult = DirectCast(dialogResult2, DialogResult)
        If frm.GetType Is GetType(frmMain) And Not frm Is Nothing Then
            blnSelected = True
        Else
            blnSelected = True
        End If
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.DialogResult = DirectCast(dialogResult3, DialogResult)
        If frm.GetType Is GetType(frmMain) And Not frm Is Nothing Then
            blnSelected = True
        Else
            blnSelected = True
        End If
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.DialogResult = DirectCast(dialogResult4, DialogResult)
        If frm.GetType Is GetType(frmMain) And Not frm Is Nothing Then
            blnSelected = True
        Else
            blnSelected = True
        End If
    End Sub
    Private Sub dialogMultiChoice_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub
End Class
