Imports System.Windows.Forms

Public Class dialogPopup
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Dim frmParent As Form = Nothing
    Public Sub setParent(ByRef frm As Form)
        frmParent = frm
    End Sub
    Dim frmLink As clsLinks.Link
    Public Sub setLink(ByRef lnk As clsLinks.Link)
        frmLink = lnk
    End Sub
    Public Sub setPopupText(strPopupText As String)
        txtPopupText.Text = strPopupText
    End Sub
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
End Class
