Public Class frmSaveAs
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 Nicholas Kowalewicz All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>
    Public o As New SaveFileDialog
    Public dialogResult_1 As DialogResult = Windows.Forms.DialogResult.None
    Public Sub New()
        InitializeComponent()
        frmSaveAs_TextFilePath.Text = ""
        SaveAsFileDialog_InitialDirectory = DirectoryName
    End Sub
    Public Sub New(ByVal strDefaultDirectory As String, ByVal strDefaultFileName As String, ByVal ExtensionFilters As String, ByVal DefaultExtension As String)
        InitializeComponent()
        SaveAsFileDialog_InitialDirectory = strDefaultDirectory & ""
        SaveAsFileDialog_ExtensionFilters = ExtensionFilters & ""
        SaveAsFileDialog_DefaultExtension = DefaultExtension & ""
        frmSaveAs_TextFilePath.Text = strDefaultFileName & ""
    End Sub
    Public Sub New(ByVal strDefaultFileName As String)
        InitializeComponent()
        frmSaveAs_TextFilePath.Text = strDefaultFileName & ""
        SaveAsFileDialog_InitialDirectory = DirectoryName
    End Sub
    Private Sub frmSaveAs_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.Visible Or dialogResult_1 = Windows.Forms.DialogResult.Cancel Then
            frm_Close()
            Me.Hide()
            e.Cancel = True
            If dialogResult_1 = Windows.Forms.DialogResult.None Then
                dialogResult_1 = Windows.Forms.DialogResult.Cancel
            End If
        End If
    End Sub
    Private Sub frmSaveAs_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If Not frmMain1 Is Nothing Then
                frmMain1 = DirectCast(frmMain1, frmMain)
                If Not String.IsNullOrEmpty(frmMain1.pdfOwnerPassword) Then
                    chkRemoveUnusedObjects.Checked = False
                Else
                    chkRemoveUnusedObjects.Checked = True
                End If
            Else
                chkRemoveUnusedObjects.Checked = True
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public Sub ShowFrmSaveAs_PDFVersion(Optional ByVal blnShow As Boolean = True)
        Try
            frmSaveAs_PDFVersion.Visible = blnShow
            frmSaveAs_PDFVersion_Label.Visible = blnShow
            blnShowPDFVersion = blnShow
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public Sub frm_Close()
        Try
            If Not Me.Owner Is Nothing Then
                If TypeOf (Me.Owner) Is frmMain Then
                    Me.Owner.Show()
                    Me.Owner.BringToFront()
                End If
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public frmMain1 As frmMain = Nothing
    Public Sub frm_ShowForm(ByVal showDialogFirst As Boolean, ByVal ownerForm As Form)
        Try
            Me.Owner = ownerForm
            If Not Me.Owner Is Nothing Then
                If TypeOf (Me.Owner) Is frmMain Then
                    Me.Owner.Hide()
                End If
            End If
            If Not ownerForm Is Nothing Then
                If ownerForm.GetType() = frmMain.GetType() Then
                    frmMain1 = DirectCast(ownerForm, frmMain)
                    If Not String.IsNullOrEmpty(frmMain1.pdfOwnerPassword) Then
                        chkRemoveUnusedObjects.Checked = False
                    End If
                End If
            End If
        Catch ex As Exception
            Err.Clear()
        Finally
            If showDialogFirst Then
                Me.Hide()
                frmSaveAs_FileSelection_Click(Me, New EventArgs())
            Else
                Me.Show()
                Me.BringToFront()
            End If
        End Try
    End Sub
    Public Property FilePath() As String
        Get
            Return frmSaveAs_TextFilePath.Text & ""
        End Get
        Set(ByVal value As String)
            frmSaveAs_TextFilePath.Text = value & ""
        End Set
    End Property
    Public ReadOnly Property FilePathExtension() As String
        Get
            Try
                If Not String.IsNullOrEmpty(frmSaveAs_TextFilePath.Text & "") Then
                    Return System.IO.Path.GetExtension(frmSaveAs_TextFilePath.Text & "")
                End If
            Catch ex As Exception
                Err.Clear()
            End Try
            Return ""
        End Get
    End Property
    Public ReadOnly Property FileName() As String
        Get
            Try
                If Not String.IsNullOrEmpty(frmSaveAs_TextFilePath.Text & "") Then
                    Return System.IO.Path.GetFileName(frmSaveAs_TextFilePath.Text & "")
                End If
            Catch ex As Exception
                Err.Clear()
            End Try
            Return ""
        End Get
    End Property
    Public ReadOnly Property FileNameWithoutExtension() As String
        Get
            Try
                If Not String.IsNullOrEmpty(frmSaveAs_TextFilePath.Text & "") Then
                    Return System.IO.Path.GetFileNameWithoutExtension(frmSaveAs_TextFilePath.Text & "")
                End If
            Catch ex As Exception
                Err.Clear()
            End Try
            Return ""
        End Get
    End Property
    Public ReadOnly Property DirectoryName() As String
        Get
            Try
                If Not String.IsNullOrEmpty(frmSaveAs_TextFilePath.Text & "") Then
                    Return System.IO.Path.GetDirectoryName(frmSaveAs_TextFilePath.Text & "")
                Else
                    Return Application.StartupPath.ToString.TrimEnd("\"c) & "\"
                End If
            Catch ex As Exception
                Err.Clear()
            End Try
            Return ""
        End Get
    End Property
    Public Property PDFVersion() As String
        Get
            Try
                If Not frmSaveAs_PDFVersion.SelectedIndex <= 0 Then
                    Return frmSaveAs_PDFVersion.Items(frmSaveAs_PDFVersion.SelectedIndex).ToString & ""
                End If
            Catch ex As Exception
                Err.Clear()
            End Try
            Return frmSaveAs_PDFVersion.Items(frmSaveAs_PDFVersion.Items.Count - 1).ToString & ""
        End Get
        Set(ByVal value As String)
            Try
                frmSaveAs_PDFVersion.SelectedItem = value & ""
                Return
            Catch ex As Exception
                Err.Clear()
            End Try
            frmSaveAs_PDFVersion.SelectedItem = "1.7"
        End Set
    End Property
    Public Property SaveAsFileDialog_ExtensionFilters() As String
        Get
            Try
                Return o.Filter.ToString & ""
            Catch ex As Exception
                Err.Clear()
            End Try
            Return ""
        End Get
        Set(ByVal value As String)
            Try
                o.Filter = value & ""
            Catch ex As Exception
                Err.Clear()
            End Try
        End Set
    End Property
    Public Property SaveAsFileDialog_DefaultExtension() As String
        Get
            Try
                Return o.DefaultExt.ToString & ""
            Catch ex As Exception
                Err.Clear()
            End Try
            Return ""
        End Get
        Set(ByVal value As String)
            Try
                o.DefaultExt = value & ""
            Catch ex As Exception
                Err.Clear()
            End Try
        End Set
    End Property
    Public Property SaveAsFileDialog_Title() As String
        Get
            Try
                Return o.Title.ToString & ""
            Catch ex As Exception
                Err.Clear()
            End Try
            Return ""
        End Get
        Set(ByVal value As String)
            Try
                o.Title = value & ""
            Catch ex As Exception
                Err.Clear()
            End Try
        End Set
    End Property
    Public Property SaveAsFileDialog_FileName() As String
        Get
            Try
                Return o.FileName.ToString & ""
            Catch ex As Exception
                Err.Clear()
            End Try
            Return ""
        End Get
        Set(ByVal value As String)
            Try
                o.FileName = value & ""
                frmSaveAs_TextFilePath.Text = value & ""
            Catch ex As Exception
                Err.Clear()
            End Try
        End Set
    End Property
    Public Property SaveAsFileDialog_InitialDirectory() As String
        Get
            Try
                If Not String.IsNullOrEmpty(o.InitialDirectory.ToString & "") Then
                    Return o.InitialDirectory.ToString & ""
                Else
                    Return DirectoryName
                End If
            Catch ex As Exception
                Err.Clear()
            End Try
            Return ""
        End Get
        Set(ByVal value As String)
            Try
                If Not String.IsNullOrEmpty(value & "") Then
                    o.InitialDirectory = value & ""
                Else
                    o.InitialDirectory = DirectoryName & ""
                End If
            Catch ex As Exception
                Err.Clear()
            End Try
        End Set
    End Property
    Public Property SaveAsFileDialog_CheckFileExists() As Boolean
        Get
            Try
                Return o.CheckFileExists
            Catch ex As Exception
                Err.Clear()
            End Try
            Return False
        End Get
        Set(ByVal value As Boolean)
            Try
                o.CheckFileExists = value
            Catch ex As Exception
                Err.Clear()
            End Try
        End Set
    End Property
    Public Property SaveAsFileDialog_AddExtension() As Boolean
        Get
            Try
                Return o.AddExtension
            Catch ex As Exception
                Err.Clear()
            End Try
            Return False
        End Get
        Set(ByVal value As Boolean)
            Try
                o.AddExtension = value
            Catch ex As Exception
                Err.Clear()
            End Try
        End Set
    End Property
    Public blnShowPDFVersion As Boolean
    Public Sub frmSaveAs_FileSelection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frmSaveAs_FileSelection.Click
        Try
            Me.Hide()
            Select Case o.ShowDialog(Me)
                Case DialogResult.Yes, DialogResult.OK
                    FilePath = o.FileName
                    If FilePathExtension.ToString.ToLower.TrimStart("."c) = "pdf".ToLower Then
                        If frmSaveAs_PDFVersion.SelectedIndex < 0 Then frmSaveAs_PDFVersion.SelectedIndex = frmSaveAs_PDFVersion.Items.Count - 1
                        If blnShowPDFVersion Then
                            ShowFrmSaveAs_PDFVersion(True)
                            lblStatus.Text = "Status: Select PDF version and then click the ""Save"" button"
                        Else
                            lblStatus.Text = "Status: Click the ""Save"" button"
                        End If
                    Else
                        ShowFrmSaveAs_PDFVersion(False)
                        lblStatus.Text = "Status: Click the ""Save"" button"
                    End If
                    Me.Show()
                    Me.BringToFront()
                    If frmSaveAs_PDFVersion.Visible Then
                        frmSaveAs_PDFVersion.Focus()
                    Else
                        frmSaveAs_ButtonSaveAs.Focus()
                    End If
                Case Else
                    Me.Owner.Show()
                    Me.dialogResult_1 = DialogResult.Cancel
                    Me.Close()
                    Return
            End Select
        Catch exOpenDialog As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub frmSaveAs_ButtonSaveAs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frmSaveAs_ButtonSaveAs.Click
        dialogResult_1 = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
    Private Sub frmSaveAs_ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frmSaveAs_ButtonCancel.Click
        dialogResult_1 = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Private Sub frmSaveAs_TextFilePath_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frmSaveAs_TextFilePath.TextChanged
        If Not frmSaveAs_TextFilePath.Text = "" Then
            frmSaveAs_TextFileName.Text = System.IO.Path.GetFileName(CStr(frmSaveAs_TextFilePath.Text & ""))
        End If
    End Sub
End Class
