Imports System.Windows.Forms
Public Class dialogWebBrowser
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
    Private Sub textboxURL_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles textboxURL.KeyUp
        Try
            If e.KeyCode = Keys.Enter Then
                WebBrowser1.Url = New System.Uri(textboxURL.Text)
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub textboxURL_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles textboxURL.MouseUp
    End Sub
    Private Sub textboxURL_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles textboxURL.TextChanged
    End Sub
    Public Sub New()
        InitializeComponent()
    End Sub
    Public frm As frmMain = Nothing
    Public Sub New(ByRef frmMain1 As frmMain)
        InitializeComponent()
        frm = frmMain1
    End Sub
    Public Sub LoadURL(ByVal strURL As String)
        Try
            textboxURL.Text = strURL
            WebBrowser1.Url = New System.Uri(textboxURL.Text)
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub dialogWebBrowser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub
    Private Sub SaveAs_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveAs_Button.Click
        Try
            WebBrowser1.ShowSaveAsDialog()
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public Function GenerateScreenshot(ByVal url As String) As Bitmap
        Return GenerateScreenshot(url, -1, -1)
    End Function
    Public Function GenerateScreenshotAlternative(ByVal url As String, ByVal width As Integer, ByVal height As Integer) As Bitmap
        Dim wb As New WebBrowser
        Try
            wb.ScrollBarsEnabled = False
            wb.ScriptErrorsSuppressed = True
            Dim strHeaders As String = ""
            strHeaders &= "User-Agent: mozilla/5.0 (windows nt 6.1; trident/7.0; rv:11.0) like gecko" & vbCrLf
            strHeaders &= "Referer: http://www.google.com/" & vbCrLf
            wb.Navigate(url, "_self", Nothing, strHeaders.ToString)
            While wb.ReadyState <> WebBrowserReadyState.Complete
                Application.DoEvents()
            End While
            wb.Width = width
            wb.Height = height
            If width <= 0 Then
                wb.Width = wb.Document.Body.ScrollRectangle.Width
            End If
            If height <= 0 Then
                wb.Height = wb.Document.Body.ScrollRectangle.Height
            End If
            Dim bitmap As New Bitmap(wb.Width, wb.Height)
            wb.DrawToBitmap(bitmap, New Rectangle(0, 0, wb.Width, wb.Height))
            wb.Dispose()
            Return bitmap
        Catch ex As Exception
            Err.Clear()
        End Try
        Return Nothing
    End Function
    Public Function GenerateScreenshot(ByVal url As String, ByVal width As Integer, ByVal height As Integer) As Bitmap
        Dim wb As New WebBrowser
        Try
            wb.ScrollBarsEnabled = False
            wb.ScriptErrorsSuppressed = True
            Dim strHeaders As String = ""
            strHeaders &= "User-Agent: mozilla/5.0 (windows nt 6.1; trident/7.0; rv:11.0) like gecko" & vbCrLf
            strHeaders &= "Referer: http://www.google.com/" & vbCrLf
            wb.Navigate(url, "_self", Nothing, strHeaders.ToString)
            While wb.ReadyState <> WebBrowserReadyState.Complete
                Application.DoEvents()
            End While
            wb.Width = width
            wb.Height = height
            If width <= 0 Then
                wb.Width = wb.Document.Body.ScrollRectangle.Width
            End If
            If height <= 0 Then
                wb.Height = wb.Document.Body.ScrollRectangle.Height
            End If
            Dim img As System.Drawing.Image = New System.Drawing.Bitmap(wb.Width, wb.Height)
            Utilities.NativeMethods.GetImage(wb, img, Color.Transparent)
            wb.Dispose()
            Dim bitmap As New System.Drawing.Bitmap(img, wb.Width, wb.Height)
            Return bitmap
        Catch ex As Exception
            Err.Clear()
        End Try
        Return Nothing
    End Function
    Private Sub Print_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Print_Button.Click
        Try
            WebBrowser1.ShowPrintDialog()
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub Import_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Import_Button.Click
        Try
            If System.IO.File.Exists(WebBrowser1.Url.AbsolutePath.ToString) Then
                Dim wb_type As FDFApp.FDFDoc_Class.FDFType = frm.cFDFDoc.Determine_Type(WebBrowser1.Url.AbsolutePath.ToString)
                Select Case wb_type
                    Case FDFApp.FDFDoc_Class.FDFType.PDF, FDFApp.FDFDoc_Class.FDFType.XFA, FDFApp.FDFDoc_Class.FDFType.XPDF
                        frm.OpenFile(WebBrowser1.Url.AbsolutePath.ToString())
                        Return
                    Case FDFApp.FDFDoc_Class.FDFType.FDF, FDFApp.FDFDoc_Class.FDFType.XDP, FDFApp.FDFDoc_Class.FDFType.xFDF, FDFApp.FDFDoc_Class.FDFType.XML
                        frm.OpenFile(WebBrowser1.Url.AbsolutePath.ToString())
                        Return
                    Case Else
                        Dim dMultipleChoice As New dialogMultiChoice(Me)
                        dMultipleChoice.lblMessage.Text = "Import HTML page as..."
                        Dim clsBut As New List(Of dialogMultiChoice.clsButton)
                        Dim btn As dialogMultiChoice.clsButton
                        btn = New dialogMultiChoice.clsButton("as HTML", True, DialogResult.OK)
                        clsBut.Add(btn)
                        btn = New dialogMultiChoice.clsButton("as Image", True, DialogResult.Yes)
                        clsBut.Add(btn)
                        btn = New dialogMultiChoice.clsButton("", False, DialogResult.No)
                        clsBut.Add(btn)
                        btn = New dialogMultiChoice.clsButton("Cancel", True, DialogResult.Cancel)
                        clsBut.Add(btn)
                        Select Case dMultipleChoice.ShowDialog(frm, "Import HTML page as:", clsBut.ToArray())
                            Case DialogResult.OK
                                frm.Convert_ImportURl2HTMLFile(Me.WebBrowser1.Url.AbsolutePath.ToString() & "")
                            Case DialogResult.Yes
                                frm.Convert_ImportURl2ImageFile(Me.WebBrowser1.Url.AbsolutePath.ToString() & "")
                            Case Else
                                Exit Select
                        End Select
                End Select
            ElseIf frm.IsValidUrl(WebBrowser1.Url.AbsoluteUri.ToString) Then
                Dim wb_type As FDFApp.FDFDoc_Class.FDFType = frm.cFDFDoc.Determine_Type(WebBrowser1.Url.AbsoluteUri.ToString)
                Select Case wb_type
                    Case FDFApp.FDFDoc_Class.FDFType.PDF, FDFApp.FDFDoc_Class.FDFType.XFA, FDFApp.FDFDoc_Class.FDFType.XPDF
                        frm.OpenFileFromUrl(WebBrowser1.Url.AbsoluteUri.ToString())
                        Return
                    Case FDFApp.FDFDoc_Class.FDFType.FDF, FDFApp.FDFDoc_Class.FDFType.XDP, FDFApp.FDFDoc_Class.FDFType.xFDF, FDFApp.FDFDoc_Class.FDFType.XML
                        frm.OpenFileFromUrl(WebBrowser1.Url.ToString())
                        Return
                    Case Else
                        Dim dMultipleChoice As New dialogMultiChoice(Me)
                        dMultipleChoice.lblMessage.Text = "Import HTML page as..."
                        Dim clsBut As New List(Of dialogMultiChoice.clsButton)
                        Dim btn As dialogMultiChoice.clsButton
                        btn = New dialogMultiChoice.clsButton("as HTML", True, DialogResult.OK)
                        clsBut.Add(btn)
                        btn = New dialogMultiChoice.clsButton("as Image", True, DialogResult.Yes)
                        clsBut.Add(btn)
                        btn = New dialogMultiChoice.clsButton("", False, DialogResult.No)
                        clsBut.Add(btn)
                        btn = New dialogMultiChoice.clsButton("Cancel", True, DialogResult.Cancel)
                        clsBut.Add(btn)
                        Select Case dMultipleChoice.ShowDialog(frm, "Import HTML page as:", clsBut.ToArray())
                            Case DialogResult.OK
                                frm.Convert_ImportURl2HTMLFile(Me.WebBrowser1.Url.AbsolutePath.ToString() & "")
                            Case DialogResult.Yes
                                frm.Convert_ImportURl2ImageFile(Me.WebBrowser1.Url.AbsolutePath.ToString() & "")
                            Case Else
                                Exit Select
                        End Select
                End Select
            End If
        Catch ex As Exception
            Err.Clear()
        Finally
            frm.Show()
            frm.BringToFront()
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        End Try
    End Sub
End Class
