Imports System.Windows.Forms
Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.security
Public Class dialogSecurityPassword
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 Nicholas Kowalewicz All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>
    Public frm As frmMain = Nothing
    Public docPropertiesDialog As dialogDocumentProperties = Nothing
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            Dim pdfBytes() As Byte = frm.EncryptPDFDocument(frm.Session.ToArray())
            frm.Session = pdfBytes.ToArray()
            frm.LoadPDFReaderDoc(frm.pdfOwnerPassword, True)
            If Not docPropertiesDialog Is Nothing Then
                docPropertiesDialog.Show()
                docPropertiesDialog.BringToFront()
            ElseIf Not frm Is Nothing Then
                frm.Show()
                frm.BringToFront()
            End If
        Catch ex As Exception
            frm.TimeStampAdd(ex, frm.debugMode)
        Finally
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        End Try
    End Sub
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Try
            If Not docPropertiesDialog Is Nothing Then
                docPropertiesDialog.Show()
                docPropertiesDialog.BringToFront()
            ElseIf Not frm Is Nothing Then
                frm.Show()
                frm.BringToFront()
            End If
        Catch ex As Exception
            frm.TimeStampAdd(ex, frm.debugMode)
        Finally
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Close()
        End Try
    End Sub
    Private Sub pnlPDFEncryption_OpenPasswordChkRequired_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pnlPDFEncryption_OpenPasswordChkRequired.CheckedChanged
        pnlPDFEncryption_OpenPasswordTxtUserPassword.Enabled = pnlPDFEncryption_OpenPasswordChkRequired.Checked
        If pnlPDFEncryption_OpenPasswordChkRequired.Checked Then
            pnlPDFEncryption_OpenPasswordTxtUserPassword.Focus()
        End If
    End Sub
    Private Sub pnlPDFEncryption_PermissionsChkRestrictDocument_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pnlPDFEncryption_PermissionsChkRestrictDocument.CheckedChanged
        Me.groupBoxPermissions.Enabled = pnlPDFEncryption_PermissionsChkRestrictDocument.Checked
        If pnlPDFEncryption_PermissionsChkRestrictDocument.Checked Then
            pnlPDFEncryption_PermissionsTxtOwnerPassword.Focus()
        End If
    End Sub
    Private Sub pnlPDFEncryption_CompatibilityCmb_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pnlPDFEncryption_CompatibilityCmb.SelectedIndexChanged
        Try
            Select Case pnlPDFEncryption_CompatibilityCmb.SelectedIndex
                Case 0
                    pnlPDFEncryption_EncryptionRadAll.Enabled = True
                    pnlPDFEncryption_EncryptionRadAll.Checked = True
                    pnlPDFEncryption_EncryptionRadAllExceptMeta.Enabled = False
                    pnlPDFEncryption_EncryptionRadFileAttachment.Enabled = False
                    If pnlPDFEncryption_EncryptionRadFileAttachment.Checked Or pnlPDFEncryption_EncryptionRadAllExceptMeta.Checked Then
                        pnlPDFEncryption_EncryptionRadAll.Checked = True
                    End If
                    pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared.Visible = False
                    pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared.Enabled = False
                    pnlPDFEncryption_EncryptionCmbStrength.SelectedIndex = 1
                Case 1
                    pnlPDFEncryption_EncryptionRadAll.Enabled = True
                    pnlPDFEncryption_EncryptionRadAllExceptMeta.Enabled = False
                    pnlPDFEncryption_EncryptionRadFileAttachment.Enabled = False
                    If pnlPDFEncryption_EncryptionRadFileAttachment.Checked Or pnlPDFEncryption_EncryptionRadAllExceptMeta.Checked Then
                        pnlPDFEncryption_EncryptionRadAll.Checked = True
                    End If
                    pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared.Visible = True
                    pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared.Enabled = True
                    pnlPDFEncryption_EncryptionCmbStrength.SelectedIndex = 2
                Case 2
                    pnlPDFEncryption_EncryptionRadAll.Enabled = True
                    pnlPDFEncryption_EncryptionRadAllExceptMeta.Enabled = True
                    pnlPDFEncryption_EncryptionRadFileAttachment.Enabled = False
                    If pnlPDFEncryption_EncryptionRadFileAttachment.Checked Or pnlPDFEncryption_EncryptionRadAllExceptMeta.Checked Then
                        pnlPDFEncryption_EncryptionRadAll.Checked = True
                    End If
                    pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared.Visible = True
                    pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared.Enabled = True
                    pnlPDFEncryption_EncryptionCmbStrength.SelectedIndex = 2
                Case 3
                    pnlPDFEncryption_EncryptionRadAll.Enabled = True
                    pnlPDFEncryption_EncryptionRadAllExceptMeta.Enabled = True
                    pnlPDFEncryption_EncryptionRadFileAttachment.Enabled = True
                    If pnlPDFEncryption_EncryptionRadFileAttachment.Checked Or pnlPDFEncryption_EncryptionRadAllExceptMeta.Checked Then
                    End If
                    pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared.Visible = True
                    pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared.Enabled = True
                    pnlPDFEncryption_EncryptionCmbStrength.SelectedIndex = 3
                Case 4
                    pnlPDFEncryption_EncryptionRadAll.Enabled = True
                    pnlPDFEncryption_EncryptionRadAllExceptMeta.Enabled = True
                    pnlPDFEncryption_EncryptionRadFileAttachment.Enabled = True
                    If pnlPDFEncryption_EncryptionRadFileAttachment.Checked Or pnlPDFEncryption_EncryptionRadAllExceptMeta.Checked Then
                    End If
                    pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared.Visible = True
                    pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared.Enabled = True
                    pnlPDFEncryption_EncryptionCmbStrength.SelectedIndex = 3
                Case Else
                    pnlPDFEncryption_CompatibilityCmb.SelectedIndex = 4
            End Select
        Catch ex As Exception
            frm.TimeStampAdd(ex, frm.debugMode)
        End Try
    End Sub
    Private Sub pnlPDFEncryption_EncryptionCmbStrength_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pnlPDFEncryption_EncryptionCmbStrength.SelectedIndexChanged
        Try
            If pnlPDFEncryption_EncryptionCmbStrength.SelectedIndex >= 2 Then
                pnlPDFEncryption_PermissionsChk_Assembly.Enabled = True
                pnlPDFEncryption_PermissionsChk_FillIn.Enabled = True
                pnlPDFEncryption_PermissionsChk_Annotations.Enabled = True
                pnlPDFEncryption_PermissionsChk_Contents.Enabled = True
                pnlPDFEncryption_PermissionsChk_Assembly.Visible = True
                pnlPDFEncryption_PermissionsChk_FillIn.Visible = True
                pnlPDFEncryption_PermissionsChk_Annotations.Visible = True
                pnlPDFEncryption_PermissionsChk_Contents.Visible = True
                Try
                    Dim selIndex As Integer = pnlPDFEncryption_PermissionsCmbPrintingRestrictions.SelectedIndex
                    pnlPDFEncryption_PermissionsCmbPrintingRestrictions.Items.Clear()
                    pnlPDFEncryption_PermissionsCmbPrintingRestrictions.Items.AddRange(New String() {"None", "Low Resolution (150 dpi)", "High Resolution (128-bit only)"})
                    If (selIndex) >= 0 Then
                        If pnlPDFEncryption_PermissionsCmbPrintingRestrictions.Items.Count > selIndex Then
                            pnlPDFEncryption_PermissionsCmbPrintingRestrictions.SelectedIndex = selIndex
                        End If
                    Else
                        pnlPDFEncryption_PermissionsCmbPrintingRestrictions.SelectedIndex = IIf(PdfEncryptor.IsPrintingAllowed(frm.pdfReaderDoc.Permissions), IIf(pnlPDFEncryption_PermissionsCmbPrintingRestrictions.Items.Count >= 3, 2, 1), IIf(PdfEncryptor.IsDegradedPrintingAllowed(frm.pdfReaderDoc.Permissions), 1, 0))
                    End If
                Catch ex As Exception
                    frm.TimeStampAdd(ex, frm.debugMode)
                End Try
                pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared.Enabled = True
            Else
                pnlPDFEncryption_PermissionsChk_Assembly.Enabled = False
                pnlPDFEncryption_PermissionsChk_FillIn.Enabled = False
                pnlPDFEncryption_PermissionsChk_Annotations.Enabled = True
                pnlPDFEncryption_PermissionsChk_Contents.Enabled = True
                pnlPDFEncryption_PermissionsChk_Assembly.Visible = False
                pnlPDFEncryption_PermissionsChk_FillIn.Visible = False
                pnlPDFEncryption_PermissionsChk_Annotations.Visible = True
                pnlPDFEncryption_PermissionsChk_Contents.Visible = True
                Try
                    Dim selIndex As Integer = pnlPDFEncryption_PermissionsCmbPrintingRestrictions.SelectedIndex
                    pnlPDFEncryption_PermissionsCmbPrintingRestrictions.Items.Clear()
                    pnlPDFEncryption_PermissionsCmbPrintingRestrictions.Items.AddRange(New String() {"None", "Low Resolution (150 dpi)"})
                    If (selIndex) >= 0 Then
                        If pnlPDFEncryption_PermissionsCmbPrintingRestrictions.Items.Count > selIndex Then
                            pnlPDFEncryption_PermissionsCmbPrintingRestrictions.SelectedIndex = selIndex
                        ElseIf selIndex = 2 Then
                            pnlPDFEncryption_PermissionsCmbPrintingRestrictions.SelectedIndex = 1
                        ElseIf selIndex <= 0 Then
                            pnlPDFEncryption_PermissionsCmbPrintingRestrictions.SelectedIndex = 0
                        End If
                    Else
                        pnlPDFEncryption_PermissionsCmbPrintingRestrictions.SelectedIndex = IIf(PdfEncryptor.IsPrintingAllowed(frm.pdfReaderDoc.Permissions), IIf(pnlPDFEncryption_PermissionsCmbPrintingRestrictions.Items.Count >= 3, 2, 1), IIf(PdfEncryptor.IsDegradedPrintingAllowed(frm.pdfReaderDoc.Permissions), 1, 0))
                    End If
                Catch ex As Exception
                    frm.TimeStampAdd(ex, frm.debugMode)
                End Try
                pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared.Enabled = False
            End If
        Catch ex As Exception
            frm.TimeStampAdd(ex, frm.debugMode)
        End Try
    End Sub
    Private Sub pnlPDFEncryption_PermissionsCmbChangeRestrictions_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pnlPDFEncryption_PermissionsCmbChangeRestrictions.SelectedIndexChanged
        Try
            frm.changingPermissionRestrictionCombo = True
            If pnlPDFEncryption_EncryptionCmbStrength.SelectedIndex >= 2 Then
                pnlPDFEncryption_PermissionsChk_Assembly.Enabled = True
                pnlPDFEncryption_PermissionsChk_FillIn.Enabled = True
                pnlPDFEncryption_PermissionsChk_Annotations.Enabled = True
                pnlPDFEncryption_PermissionsChk_Contents.Enabled = True
                pnlPDFEncryption_PermissionsChk_Assembly.Visible = True
                pnlPDFEncryption_PermissionsChk_FillIn.Visible = True
                pnlPDFEncryption_PermissionsChk_Annotations.Visible = True
                pnlPDFEncryption_PermissionsChk_Contents.Visible = True
            Else
                pnlPDFEncryption_PermissionsChk_Assembly.Enabled = False
                pnlPDFEncryption_PermissionsChk_FillIn.Enabled = False
                pnlPDFEncryption_PermissionsChk_Annotations.Enabled = True
                pnlPDFEncryption_PermissionsChk_Contents.Enabled = True
                pnlPDFEncryption_PermissionsChk_Assembly.Visible = False
                pnlPDFEncryption_PermissionsChk_FillIn.Visible = False
                pnlPDFEncryption_PermissionsChk_Annotations.Visible = True
                pnlPDFEncryption_PermissionsChk_Contents.Visible = True
            End If
            Select Case pnlPDFEncryption_PermissionsCmbChangeRestrictions.SelectedIndex
                Case 0
                    pnlPDFEncryption_PermissionsChk_Assembly.Checked = False
                    pnlPDFEncryption_PermissionsChk_FillIn.Checked = False
                    pnlPDFEncryption_PermissionsChk_Annotations.Checked = False
                    pnlPDFEncryption_PermissionsChk_Contents.Checked = False
                Case 1
                    pnlPDFEncryption_PermissionsChk_Assembly.Checked = True
                    pnlPDFEncryption_PermissionsChk_FillIn.Checked = False
                    pnlPDFEncryption_PermissionsChk_Annotations.Checked = False
                    pnlPDFEncryption_PermissionsChk_Contents.Checked = False
                    If pnlPDFEncryption_EncryptionCmbStrength.SelectedIndex >= 2 Then
                        pnlPDFEncryption_PermissionsChk_Contents.Checked = True
                    Else
                        pnlPDFEncryption_PermissionsChk_Contents.Checked = False
                    End If
                Case 2
                    pnlPDFEncryption_PermissionsChk_Assembly.Checked = False
                    pnlPDFEncryption_PermissionsChk_FillIn.Checked = True
                    If pnlPDFEncryption_EncryptionCmbStrength.SelectedIndex >= 2 Then
                        pnlPDFEncryption_PermissionsChk_Assembly.Checked = False
                        pnlPDFEncryption_PermissionsChk_FillIn.Checked = True
                    Else
                        pnlPDFEncryption_PermissionsChk_Assembly.Checked = False
                        pnlPDFEncryption_PermissionsChk_FillIn.Checked = False
                    End If
                    pnlPDFEncryption_PermissionsChk_Annotations.Checked = False
                    pnlPDFEncryption_PermissionsChk_Contents.Checked = False
                Case 3
                    pnlPDFEncryption_PermissionsChk_Assembly.Checked = False
                    pnlPDFEncryption_PermissionsChk_FillIn.Checked = True
                    If pnlPDFEncryption_EncryptionCmbStrength.SelectedIndex >= 2 Then
                        pnlPDFEncryption_PermissionsChk_Assembly.Checked = False
                        pnlPDFEncryption_PermissionsChk_FillIn.Checked = True
                    Else
                        pnlPDFEncryption_PermissionsChk_Assembly.Checked = False
                        pnlPDFEncryption_PermissionsChk_FillIn.Checked = False
                    End If
                    pnlPDFEncryption_PermissionsChk_Annotations.Checked = True
                    pnlPDFEncryption_PermissionsChk_Contents.Checked = False
                Case 4
                    pnlPDFEncryption_PermissionsChk_Assembly.Checked = False
                    pnlPDFEncryption_PermissionsChk_FillIn.Checked = True
                    If pnlPDFEncryption_EncryptionCmbStrength.SelectedIndex >= 2 Then
                        pnlPDFEncryption_PermissionsChk_Assembly.Checked = False
                        pnlPDFEncryption_PermissionsChk_FillIn.Checked = True
                    Else
                        pnlPDFEncryption_PermissionsChk_Assembly.Checked = False
                        pnlPDFEncryption_PermissionsChk_FillIn.Checked = False
                    End If
                    pnlPDFEncryption_PermissionsChk_Annotations.Checked = True
                    pnlPDFEncryption_PermissionsChk_Contents.Checked = True
                Case 5
                    Dim perms As Integer = frm.pdfReaderDoc.Permissions, encryptStrength As Integer = frm.pdfReaderDoc.GetCryptoMode() + 0
                    pnlPDFEncryption_PermissionsChk_Assembly.Checked = PdfEncryptor.IsAssemblyAllowed(perms) + 0
                    pnlPDFEncryption_PermissionsChk_FillIn.Checked = PdfEncryptor.IsFillInAllowed(perms) + 0
                    pnlPDFEncryption_PermissionsChk_Annotations.Checked = PdfEncryptor.IsModifyAnnotationsAllowed(perms) + 0
                    pnlPDFEncryption_PermissionsChk_Contents.Checked = PdfEncryptor.IsModifyContentsAllowed(perms) + 0
                Case Else
                    pnlPDFEncryption_PermissionsChk_Assembly.Enabled = True
                    pnlPDFEncryption_PermissionsChk_FillIn.Enabled = True
                    pnlPDFEncryption_PermissionsChk_Annotations.Enabled = True
                    pnlPDFEncryption_PermissionsChk_Contents.Enabled = True
            End Select
        Catch ex As Exception
            frm.TimeStampAdd(ex, frm.debugMode)
        Finally
            frm.changingPermissionRestrictionCombo = False
        End Try
    End Sub
    Public Sub UpdatePermissionsCmbChangeRestrictions()
        If frm.changingPermissionRestrictionCombo Then Return
        If pnlPDFEncryption_PermissionsChk_Assembly.Checked = False And pnlPDFEncryption_PermissionsChk_FillIn.Checked = True And pnlPDFEncryption_PermissionsChk_Annotations.Checked = True And pnlPDFEncryption_PermissionsChk_Contents.Checked = True Then
            pnlPDFEncryption_PermissionsCmbChangeRestrictions.SelectedIndex = 4
        ElseIf pnlPDFEncryption_PermissionsChk_Assembly.Checked = False And pnlPDFEncryption_PermissionsChk_FillIn.Checked = True And pnlPDFEncryption_PermissionsChk_Annotations.Checked = True And pnlPDFEncryption_PermissionsChk_Contents.Checked = False Then
            pnlPDFEncryption_PermissionsCmbChangeRestrictions.SelectedIndex = 3
        ElseIf pnlPDFEncryption_PermissionsChk_Assembly.Checked = False And pnlPDFEncryption_PermissionsChk_FillIn.Checked = True And pnlPDFEncryption_PermissionsChk_Annotations.Checked = False And pnlPDFEncryption_PermissionsChk_Contents.Checked = False Then
            pnlPDFEncryption_PermissionsCmbChangeRestrictions.SelectedIndex = 2
        ElseIf pnlPDFEncryption_PermissionsChk_Assembly.Checked = True And pnlPDFEncryption_PermissionsChk_FillIn.Checked = False And pnlPDFEncryption_PermissionsChk_Annotations.Checked = False And (pnlPDFEncryption_PermissionsChk_Contents.Checked = True And pnlPDFEncryption_EncryptionCmbStrength.SelectedIndex >= 2) Then
            pnlPDFEncryption_PermissionsCmbChangeRestrictions.SelectedIndex = 1
        ElseIf (pnlPDFEncryption_PermissionsChk_Assembly.Checked = True And pnlPDFEncryption_EncryptionCmbStrength.SelectedIndex < 2) And pnlPDFEncryption_PermissionsChk_FillIn.Checked = False And pnlPDFEncryption_PermissionsChk_Annotations.Checked = False And pnlPDFEncryption_PermissionsChk_Contents.Checked = False Then
            pnlPDFEncryption_PermissionsCmbChangeRestrictions.SelectedIndex = 1
        ElseIf pnlPDFEncryption_PermissionsChk_Assembly.Checked = False And pnlPDFEncryption_PermissionsChk_FillIn.Checked = False And pnlPDFEncryption_PermissionsChk_Annotations.Checked = False And pnlPDFEncryption_PermissionsChk_Contents.Checked = False Then
            pnlPDFEncryption_PermissionsCmbChangeRestrictions.SelectedIndex = 0
        Else
            pnlPDFEncryption_PermissionsCmbChangeRestrictions.SelectedIndex = 5
        End If
    End Sub
    Private Sub pnlPDFEncryption_PermissionsChk_Annotations_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pnlPDFEncryption_PermissionsChk_Annotations.CheckedChanged
        Try
            UpdatePermissionsCmbChangeRestrictions()
        Catch ex As Exception
            frm.TimeStampAdd(ex, frm.debugMode)
        End Try
    End Sub
    Private Sub pnlPDFEncryption_PermissionsChk_Contents_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pnlPDFEncryption_PermissionsChk_Contents.CheckedChanged
        Try
            UpdatePermissionsCmbChangeRestrictions()
        Catch ex As Exception
            frm.TimeStampAdd(ex, frm.debugMode)
        End Try
    End Sub
    Private Sub pnlPDFEncryption_PermissionsChk_FillIn_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pnlPDFEncryption_PermissionsChk_FillIn.CheckedChanged
        Try
            UpdatePermissionsCmbChangeRestrictions()
        Catch ex As Exception
            frm.TimeStampAdd(ex, frm.debugMode)
        End Try
    End Sub
    Private Sub pnlPDFEncryption_PermissionsChk_Assembly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pnlPDFEncryption_PermissionsChk_Assembly.CheckedChanged
        Try
            UpdatePermissionsCmbChangeRestrictions()
        Catch ex As Exception
            frm.TimeStampAdd(ex, frm.debugMode)
        End Try
    End Sub
    Public Sub New()
        Me.Hide()
        InitializeComponent()
        If Me.Owner.GetType Is GetType(frmMain) Then
            frm = DirectCast(Me.Owner, frmMain)
        ElseIf Me.Owner.GetType Is GetType(dialogDocumentProperties) Then
            docPropertiesDialog = DirectCast(Me.Owner, dialogDocumentProperties)
            If docPropertiesDialog.frm.GetType Is GetType(frmMain) Then
                frm = DirectCast(docPropertiesDialog.frm, frmMain)
            End If
        End If
    End Sub
    Public Sub New(ByRef frmMain1 As frmMain)
        Me.Hide()
        InitializeComponent()
        If frmMain1.GetType Is GetType(frmMain) Then
            frm = DirectCast(frmMain1, frmMain)
        End If
    End Sub
    Public Sub New(ByRef frmDocumentProperties As dialogDocumentProperties)
        Me.Hide()
        docPropertiesDialog = frmDocumentProperties
        InitializeComponent()
        If frmDocumentProperties.frm.GetType Is GetType(frmMain) Then
            frm = DirectCast(frmDocumentProperties.frm, frmMain)
        End If
    End Sub
    Public Sub pnlPDFEncryption_BtnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pnlPDFEncryption_BtnClear.Click
        Try
            Dim strModified As New System.IO.MemoryStream(frm.UnlockSecurePDF(frm.Session))
            frm.ClearPDFEncryptionPanel(True, True)
            frm.Session = strModified.ToArray()
            frm.LoadPDFEncryptionPanel(frm.Session, True)
            MsgBox("This document is now unlocked.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.ApplicationModal, "Success!")
        Catch ex As Exception
            Throw ex
        Finally
            iTextSharp.text.pdf.PdfReader.unethicalreading = False
        End Try
    End Sub
    Public Function EncryptPDFDocument(ByVal PDFData() As Byte, Optional ByVal pdfVersion As String = Nothing) As Byte()
        Dim reader As PdfReader = Nothing
        If String.IsNullOrEmpty(pnlPDFEncryption_PermissionsTxtOwnerPassword.Text & "") Then
            If Not pdfVersion Is Nothing Then
                If Not String.IsNullOrEmpty(frm.pdfOwnerPassword & "") Then
                    reader = New PdfReader(PDFData, frm.getBytes(frm.pdfOwnerPassword & ""))
                Else
                    reader = New PdfReader(PDFData)
                End If
                Dim strModified As New System.IO.MemoryStream
                If Not pdfVersion Is Nothing Then
                    pdfVersion = pdfVersion.Replace("1.", "")
                Else
                    Select Case pnlPDFEncryption_CompatibilityCmb.SelectedIndex
                        Case 0
                            pdfVersion = "3"
                        Case 1
                            pdfVersion = "5"
                        Case 2
                            pdfVersion = "6"
                        Case 3
                            pdfVersion = "7"
                        Case Else
                            pdfVersion = "7"
                    End Select
                End If
                If String.IsNullOrEmpty(pdfVersion & "") Then
                    pdfVersion = "7"
                End If
                Dim stamper As PdfStamper = New PdfStamper(reader, strModified, pdfVersion)
                stamper.Writer.CloseStream = False
                stamper.Close()
                Return strModified.ToArray()
            Else
                Return PDFData
            End If
        ElseIf String.IsNullOrEmpty(pnlPDFEncryption_PermissionsTxtOwnerPassword.Text & "") And String.IsNullOrEmpty(pnlPDFEncryption_OpenPasswordTxtUserPassword.Text & "") Then
            If Not pdfVersion Is Nothing Then
                If Not String.IsNullOrEmpty(frm.pdfOwnerPassword & "") Then
                    reader = New PdfReader(PDFData, frm.getBytes(frm.pdfOwnerPassword & ""))
                Else
                    reader = New PdfReader(PDFData)
                End If
                Dim strModified As New System.IO.MemoryStream
                If Not pdfVersion Is Nothing Then
                    pdfVersion = pdfVersion.Replace("1.", "")
                Else
                    Select Case pnlPDFEncryption_CompatibilityCmb.SelectedIndex
                        Case 0
                            pdfVersion = "3"
                        Case 1
                            pdfVersion = "5"
                        Case 2
                            pdfVersion = "6"
                        Case 3
                            pdfVersion = "7"
                        Case Else
                            pdfVersion = "7"
                    End Select
                End If
                If String.IsNullOrEmpty(pdfVersion & "") Then
                    pdfVersion = "7"
                End If
                Dim stamper As PdfStamper = New PdfStamper(reader, strModified, pdfVersion)
                stamper.Writer.CloseStream = False
                stamper.Close()
                Return strModified.ToArray()
            Else
                Return PDFData
            End If
        ElseIf pnlPDFEncryption_EncryptionCmbStrength.SelectedIndex = 0 Then
            If Not pdfVersion Is Nothing Then
                If Not String.IsNullOrEmpty(frm.pdfOwnerPassword & "") Then
                    reader = New PdfReader(PDFData, frm.getBytes(frm.pdfOwnerPassword & ""))
                Else
                    reader = New PdfReader(PDFData)
                End If
                Dim strModified As New System.IO.MemoryStream
                If Not pdfVersion Is Nothing Then
                    pdfVersion = pdfVersion.Replace("1.", "")
                Else
                    Select Case pnlPDFEncryption_CompatibilityCmb.SelectedIndex
                        Case 0
                            pdfVersion = "3"
                        Case 1
                            pdfVersion = "5"
                        Case 2
                            pdfVersion = "6"
                        Case 3
                            pdfVersion = "7"
                        Case Else
                            pdfVersion = "7"
                    End Select
                End If
                If String.IsNullOrEmpty(pdfVersion & "") Then
                    pdfVersion = "7"
                End If
                Dim stamper As PdfStamper = New PdfStamper(reader, strModified, pdfVersion)
                stamper.Writer.CloseStream = False
                stamper.Close()
                Return strModified.ToArray()
            Else
                Return PDFData
            End If
        End If
        Try
            If Not String.IsNullOrEmpty(frm.pdfOwnerPassword & "") Then
                reader = New PdfReader(PDFData, frm.getBytes(frm.pdfOwnerPassword & ""))
            Else
                reader = New PdfReader(PDFData)
            End If
            Dim strModified As New System.IO.MemoryStream
            If Not pdfVersion Is Nothing Then
                pdfVersion = pdfVersion.Replace("1.", "")
            Else
                Select Case pnlPDFEncryption_CompatibilityCmb.SelectedIndex
                    Case 0
                        pdfVersion = "3"
                    Case 1
                        pdfVersion = "5"
                    Case 2
                        pdfVersion = "6"
                    Case 3
                        pdfVersion = "7"
                    Case Else
                        pdfVersion = "7"
                End Select
            End If
            Dim stamper As PdfStamper = New PdfStamper(reader, strModified, pdfVersion)
            Dim perms As Integer = 0, encryptStrength As Integer = 0
            Select Case pnlPDFEncryption_EncryptionCmbStrength.SelectedIndex
                Case 1
                    encryptStrength += PdfWriter.STANDARD_ENCRYPTION_40
                Case 2
                    encryptStrength += PdfWriter.STANDARD_ENCRYPTION_128
                Case 3
                    encryptStrength += PdfWriter.ENCRYPTION_AES_128
                Case 4
                    encryptStrength += PdfWriter.ENCRYPTION_AES_256
                Case Else
                    encryptStrength = 0
            End Select
            If pnlPDFEncryption_PermissionsChkRestrictDocument.Checked Then
                Select Case pnlPDFEncryption_PermissionsCmbChangeRestrictions.SelectedIndex
                    Case 0
                        perms += 0
                        If pnlPDFEncryption_PermissionsChkRestrictions_Copy.Checked Then
                            perms += PdfWriter.ALLOW_COPY
                        End If
                        Select Case pnlPDFEncryption_PermissionsCmbPrintingRestrictions.SelectedIndex
                            Case 0
                                perms += 0
                            Case 1
                                If (encryptStrength = PdfWriter.STANDARD_ENCRYPTION_128 Or encryptStrength = PdfWriter.ENCRYPTION_AES_128 Or encryptStrength = PdfWriter.ENCRYPTION_AES_256) Then
                                    perms += PdfWriter.ALLOW_DEGRADED_PRINTING
                                Else
                                    perms += PdfWriter.ALLOW_PRINTING
                                End If
                            Case 2
                                perms += PdfWriter.ALLOW_PRINTING
                            Case Else
                                perms += 0
                        End Select
                        If pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared.Checked And pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared.Enabled Then
                            If (encryptStrength = PdfWriter.STANDARD_ENCRYPTION_128 Or encryptStrength = PdfWriter.ENCRYPTION_AES_128 Or encryptStrength = PdfWriter.ENCRYPTION_AES_256) Then
                                perms += PdfWriter.ALLOW_SCREENREADERS
                            End If
                        End If
                    Case 1
                        perms += PdfWriter.ALLOW_ASSEMBLY
                        If (encryptStrength = PdfWriter.STANDARD_ENCRYPTION_128 Or encryptStrength = PdfWriter.ENCRYPTION_AES_128 Or encryptStrength = PdfWriter.ENCRYPTION_AES_256) Then
                            perms += PdfWriter.ALLOW_MODIFY_CONTENTS
                        End If
                        Select Case pnlPDFEncryption_PermissionsCmbPrintingRestrictions.SelectedIndex
                            Case 0
                                perms += 0
                            Case 1
                                If (encryptStrength = PdfWriter.STANDARD_ENCRYPTION_128 Or encryptStrength = PdfWriter.ENCRYPTION_AES_128 Or encryptStrength = PdfWriter.ENCRYPTION_AES_256) Then
                                    perms += PdfWriter.ALLOW_DEGRADED_PRINTING
                                Else
                                    perms += PdfWriter.ALLOW_PRINTING
                                End If
                            Case 2
                                perms += PdfWriter.ALLOW_PRINTING
                            Case Else
                                perms += 0
                        End Select
                        If pnlPDFEncryption_PermissionsChkRestrictions_Copy.Checked Then
                            perms += PdfWriter.ALLOW_COPY
                        End If
                        If pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared.Checked And pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared.Enabled Then
                            If (encryptStrength = PdfWriter.STANDARD_ENCRYPTION_128 Or encryptStrength = PdfWriter.ENCRYPTION_AES_128 Or encryptStrength = PdfWriter.ENCRYPTION_AES_256) Then
                                perms += PdfWriter.ALLOW_SCREENREADERS
                            End If
                        End If
                    Case 2
                        perms += PdfWriter.ALLOW_FILL_IN
                        If pnlPDFEncryption_PermissionsChkRestrictions_Copy.Checked Then
                            perms += PdfWriter.ALLOW_COPY
                        End If
                        Select Case pnlPDFEncryption_PermissionsCmbPrintingRestrictions.SelectedIndex
                            Case 0
                                perms += 0
                            Case 1
                                If (encryptStrength = PdfWriter.STANDARD_ENCRYPTION_128 Or encryptStrength = PdfWriter.ENCRYPTION_AES_128 Or encryptStrength = PdfWriter.ENCRYPTION_AES_256) Then
                                    perms += PdfWriter.ALLOW_DEGRADED_PRINTING
                                Else
                                    perms += PdfWriter.ALLOW_PRINTING
                                End If
                            Case 2
                                perms += PdfWriter.ALLOW_PRINTING
                            Case Else
                                perms += 0
                        End Select
                        If pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared.Checked And pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared.Enabled Then
                            If (encryptStrength = PdfWriter.STANDARD_ENCRYPTION_128 Or encryptStrength = PdfWriter.ENCRYPTION_AES_128 Or encryptStrength = PdfWriter.ENCRYPTION_AES_256) Then
                                perms += PdfWriter.ALLOW_SCREENREADERS
                            End If
                        End If
                    Case 3
                        perms += PdfWriter.ALLOW_FILL_IN + PdfWriter.ALLOW_MODIFY_ANNOTATIONS
                        If pnlPDFEncryption_PermissionsChkRestrictions_Copy.Checked Then
                            perms += PdfWriter.ALLOW_COPY
                        End If
                        Select Case pnlPDFEncryption_PermissionsCmbPrintingRestrictions.SelectedIndex
                            Case 0
                                perms += 0
                            Case 1
                                If (encryptStrength = PdfWriter.STANDARD_ENCRYPTION_128 Or encryptStrength = PdfWriter.ENCRYPTION_AES_128 Or encryptStrength = PdfWriter.ENCRYPTION_AES_256) Then
                                    perms += PdfWriter.ALLOW_DEGRADED_PRINTING
                                Else
                                    perms += PdfWriter.ALLOW_PRINTING
                                End If
                            Case 2
                                perms += PdfWriter.ALLOW_PRINTING
                            Case Else
                                perms += 0
                        End Select
                        If pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared.Checked And pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared.Enabled Then
                            If (encryptStrength = PdfWriter.STANDARD_ENCRYPTION_128 Or encryptStrength = PdfWriter.ENCRYPTION_AES_128 Or encryptStrength = PdfWriter.ENCRYPTION_AES_256) Then
                                perms += PdfWriter.ALLOW_SCREENREADERS
                            End If
                        End If
                    Case 4
                        perms += PdfWriter.ALLOW_FILL_IN + PdfWriter.ALLOW_MODIFY_ANNOTATIONS + PdfWriter.ALLOW_MODIFY_CONTENTS
                        If pnlPDFEncryption_PermissionsChkRestrictions_Copy.Checked Then
                            perms += PdfWriter.ALLOW_COPY
                        End If
                        Select Case pnlPDFEncryption_PermissionsCmbPrintingRestrictions.SelectedIndex
                            Case 0
                                perms += 0
                            Case 1
                                If (encryptStrength = PdfWriter.STANDARD_ENCRYPTION_128 Or encryptStrength = PdfWriter.ENCRYPTION_AES_128 Or encryptStrength = PdfWriter.ENCRYPTION_AES_256) Then
                                    perms += PdfWriter.ALLOW_DEGRADED_PRINTING
                                Else
                                    perms += PdfWriter.ALLOW_PRINTING
                                End If
                            Case 2
                                perms += PdfWriter.ALLOW_PRINTING
                            Case Else
                                perms += 0
                        End Select
                        If pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared.Checked And pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared.Enabled Then
                            If (encryptStrength = PdfWriter.STANDARD_ENCRYPTION_128 Or encryptStrength = PdfWriter.ENCRYPTION_AES_128 Or encryptStrength = PdfWriter.ENCRYPTION_AES_256) Then
                                perms += PdfWriter.ALLOW_SCREENREADERS
                            End If
                        End If
                    Case 5
                        If pnlPDFEncryption_PermissionsChk_Annotations.Checked Then perms += PdfWriter.ALLOW_MODIFY_ANNOTATIONS
                        If pnlPDFEncryption_PermissionsChk_Assembly.Checked Then perms += PdfWriter.ALLOW_ASSEMBLY
                        If pnlPDFEncryption_PermissionsChk_Contents.Checked Then perms += PdfWriter.ALLOW_MODIFY_CONTENTS
                        If pnlPDFEncryption_PermissionsChk_FillIn.Checked Then perms += PdfWriter.ALLOW_FILL_IN
                        If pnlPDFEncryption_PermissionsChkRestrictions_Copy.Checked Then
                            perms += PdfWriter.ALLOW_COPY
                        End If
                        Select Case pnlPDFEncryption_PermissionsCmbPrintingRestrictions.SelectedIndex
                            Case 0
                                perms += 0
                            Case 1
                                If (encryptStrength = PdfWriter.STANDARD_ENCRYPTION_128 Or encryptStrength = PdfWriter.ENCRYPTION_AES_128 Or encryptStrength = PdfWriter.ENCRYPTION_AES_256) Then
                                    perms += PdfWriter.ALLOW_DEGRADED_PRINTING
                                Else
                                    perms += PdfWriter.ALLOW_PRINTING
                                End If
                            Case 2
                                perms += PdfWriter.ALLOW_PRINTING
                            Case Else
                                perms += 0
                        End Select
                        If pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared.Checked And pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared.Enabled Then
                            If (encryptStrength = PdfWriter.STANDARD_ENCRYPTION_128 Or encryptStrength = PdfWriter.ENCRYPTION_AES_128 Or encryptStrength = PdfWriter.ENCRYPTION_AES_256) Then
                                perms += PdfWriter.ALLOW_SCREENREADERS
                            End If
                        End If
                    Case Else
                        perms += 0
                End Select
            End If
            If Not encryptStrength = 0 Then
                If pnlPDFEncryption_EncryptionRadAll.Checked Then
                ElseIf pnlPDFEncryption_EncryptionRadAllExceptMeta.Checked Or (encryptStrength = PdfWriter.ENCRYPTION_AES_128) Or (encryptStrength = PdfWriter.ENCRYPTION_AES_256) Then
                    encryptStrength += PdfWriter.DO_NOT_ENCRYPT_METADATA
                ElseIf pnlPDFEncryption_EncryptionRadFileAttachment.Checked Then
                    encryptStrength += PdfWriter.EMBEDDED_FILES_ONLY
                End If
            End If
            If pnlPDFEncryption_OpenPasswordChkRequired.Checked Then
                stamper.SetEncryption(frm.getBytes(pnlPDFEncryption_OpenPasswordTxtUserPassword.Text & ""), frm.getBytes(pnlPDFEncryption_PermissionsTxtOwnerPassword.Text & ""), perms, encryptStrength)
            Else
                stamper.SetEncryption(Nothing, frm.getBytes(pnlPDFEncryption_PermissionsTxtOwnerPassword.Text & ""), perms, encryptStrength)
            End If
            stamper.CreateXmpMetadata()
            frm.pdfOwnerPassword = pnlPDFEncryption_PermissionsTxtOwnerPassword.Text & ""
            stamper.Writer.CloseStream = False
            stamper.Close()
            reader.Close()
            PDFData = strModified.ToArray()
        Catch ex As Exception
            MsgBox("Error: " & ex.Message().ToString(), MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.ApplicationModal, "Error!")
        End Try
        Try
            Me.Hide()
        Catch ex As Exception
            frm.TimeStampAdd(ex, frm.debugMode)
        End Try
        Return PDFData.ToArray()
    End Function
    Public Sub LoadPDFEncryptionPanel(ByVal pdfBytes As Byte(), Optional ByVal Panel_Visibility As Boolean = True)
        Try
            Dim pdfVersion As String = "1." & frm.pdfReaderDoc.PdfVersion.ToString() & ""
            If pdfVersion = "1.4" Then
                pdfVersion = "1.5"
            End If
            pnlPDFEncryption_CompatibilityCmb.SelectedItem = pdfVersion
            pnlPDFEncryption_CompatibilityCmb_SelectedIndexChanged(Me, New EventArgs())
        Catch ex As Exception
            pnlPDFEncryption_CompatibilityCmb.SelectedIndex = pnlPDFEncryption_CompatibilityCmb.Items.Count - 1
            frm.TimeStampAdd(ex, frm.debugMode)
        End Try
        Try
            Dim perms As Integer = frm.pdfReaderDoc.Permissions, encryptStrength As Integer = frm.pdfReaderDoc.GetCryptoMode() + 0
            If frm.pdfReaderDoc.IsEncrypted Then
                If (encryptStrength = PdfWriter.STANDARD_ENCRYPTION_40) Then
                    pnlPDFEncryption_EncryptionCmbStrength.SelectedIndex = 1
                ElseIf (encryptStrength = PdfWriter.STANDARD_ENCRYPTION_128) Then
                    pnlPDFEncryption_EncryptionCmbStrength.SelectedIndex = 2
                ElseIf (encryptStrength = PdfWriter.ENCRYPTION_AES_128) Then
                    pnlPDFEncryption_EncryptionCmbStrength.SelectedIndex = 3
                ElseIf (encryptStrength = PdfWriter.ENCRYPTION_AES_256) Then
                    pnlPDFEncryption_EncryptionCmbStrength.SelectedIndex = 4
                Else
                    pnlPDFEncryption_EncryptionCmbStrength.SelectedIndex = 1
                End If
                If (encryptStrength And PdfWriter.DO_NOT_ENCRYPT_METADATA) = PdfWriter.DO_NOT_ENCRYPT_METADATA Then
                    pnlPDFEncryption_EncryptionRadAllExceptMeta.Checked = True
                ElseIf (encryptStrength And PdfWriter.EMBEDDED_FILES_ONLY) = PdfWriter.EMBEDDED_FILES_ONLY Then
                    pnlPDFEncryption_EncryptionRadFileAttachment.Checked = True
                Else
                    pnlPDFEncryption_EncryptionRadAll.Checked = True
                End If
            Else
                pnlPDFEncryption_EncryptionCmbStrength.SelectedIndex = 0
            End If
            If pnlPDFEncryption_EncryptionCmbStrength.SelectedIndex >= 2 Then
                pnlPDFEncryption_PermissionsCmbPrintingRestrictions.Items.Clear()
                Try
                    Dim selIndex As Integer = pnlPDFEncryption_PermissionsCmbPrintingRestrictions.SelectedIndex
                    pnlPDFEncryption_PermissionsCmbPrintingRestrictions.Items.Clear()
                    pnlPDFEncryption_PermissionsCmbPrintingRestrictions.Items.AddRange(New String() {"None", "Low Resolution (150 dpi)", "High Resolution (128-bit only)"})
                    If (selIndex) >= 0 Then
                        If pnlPDFEncryption_PermissionsCmbPrintingRestrictions.Items.Count > selIndex Then
                            pnlPDFEncryption_PermissionsCmbPrintingRestrictions.SelectedIndex = selIndex
                        End If
                    Else
                        pnlPDFEncryption_PermissionsCmbPrintingRestrictions.SelectedIndex = IIf(PdfEncryptor.IsPrintingAllowed(perms), IIf(pnlPDFEncryption_PermissionsCmbPrintingRestrictions.Items.Count >= 3, 2, 1), IIf(PdfEncryptor.IsDegradedPrintingAllowed(perms), 1, 0))
                    End If
                Catch ex As Exception
                    frm.TimeStampAdd(ex, frm.debugMode)
                End Try
            Else
                pnlPDFEncryption_PermissionsCmbPrintingRestrictions.Items.Clear()
                Try
                    Dim selIndex As Integer = pnlPDFEncryption_PermissionsCmbPrintingRestrictions.SelectedIndex
                    pnlPDFEncryption_PermissionsCmbPrintingRestrictions.Items.Clear()
                    pnlPDFEncryption_PermissionsCmbPrintingRestrictions.Items.AddRange(New String() {"None", "Low Resolution (150 dpi)"})
                    If (selIndex) >= 0 Then
                        If pnlPDFEncryption_PermissionsCmbPrintingRestrictions.Items.Count > selIndex Then
                            pnlPDFEncryption_PermissionsCmbPrintingRestrictions.SelectedIndex = selIndex
                        ElseIf selIndex = 2 Then
                            pnlPDFEncryption_PermissionsCmbPrintingRestrictions.SelectedIndex = 1
                        ElseIf selIndex <= 0 Then
                            pnlPDFEncryption_PermissionsCmbPrintingRestrictions.SelectedIndex = 0
                        End If
                    Else
                        pnlPDFEncryption_PermissionsCmbPrintingRestrictions.SelectedIndex = IIf(PdfEncryptor.IsPrintingAllowed(perms), IIf(pnlPDFEncryption_PermissionsCmbPrintingRestrictions.Items.Count >= 3, 2, 1), IIf(PdfEncryptor.IsDegradedPrintingAllowed(perms), 1, 0))
                    End If
                Catch ex As Exception
                    frm.TimeStampAdd(ex, frm.debugMode)
                End Try
            End If
            Dim strPermissions As New System.Text.StringBuilder
            strPermissions.AppendLine("IsAssemblyAllowed=" & PdfEncryptor.IsAssemblyAllowed(perms))
            strPermissions.AppendLine("IsFillInAllowed=" & PdfEncryptor.IsFillInAllowed(perms))
            strPermissions.AppendLine("IsModifyAnnotationsAllowed=" & PdfEncryptor.IsModifyAnnotationsAllowed(perms))
            strPermissions.AppendLine("IsModifyContentsAllowed=" & PdfEncryptor.IsModifyContentsAllowed(perms))
            pnlPDFEncryption_PermissionsChk_Assembly.Enabled = True
            pnlPDFEncryption_PermissionsChk_FillIn.Enabled = True
            pnlPDFEncryption_PermissionsChk_Annotations.Enabled = True
            pnlPDFEncryption_PermissionsChk_Contents.Enabled = True
            pnlPDFEncryption_PermissionsChk_Assembly.Visible = True
            pnlPDFEncryption_PermissionsChk_FillIn.Visible = True
            pnlPDFEncryption_PermissionsChk_Annotations.Visible = True
            pnlPDFEncryption_PermissionsChk_Contents.Visible = True
            frm.changingPermissionRestrictionCombo = True
            pnlPDFEncryption_PermissionsChk_Assembly.Checked = PdfEncryptor.IsAssemblyAllowed(perms)
            pnlPDFEncryption_PermissionsChk_FillIn.Checked = PdfEncryptor.IsFillInAllowed(perms)
            pnlPDFEncryption_PermissionsChk_Annotations.Checked = PdfEncryptor.IsModifyAnnotationsAllowed(perms)
            pnlPDFEncryption_PermissionsChk_Contents.Checked = PdfEncryptor.IsModifyContentsAllowed(perms)
            Dim strPerm As String = strPermissions.ToString
            If PdfEncryptor.IsAssemblyAllowed(perms) = False And PdfEncryptor.IsFillInAllowed(perms) And PdfEncryptor.IsModifyAnnotationsAllowed(perms) And PdfEncryptor.IsModifyContentsAllowed(perms) Then
                pnlPDFEncryption_PermissionsCmbChangeRestrictions.SelectedIndex = 4
            ElseIf PdfEncryptor.IsAssemblyAllowed(perms) = False And PdfEncryptor.IsFillInAllowed(perms) And PdfEncryptor.IsModifyAnnotationsAllowed(perms) And PdfEncryptor.IsModifyContentsAllowed(perms) = False Then
                pnlPDFEncryption_PermissionsCmbChangeRestrictions.SelectedIndex = 3
            ElseIf ((((encryptStrength = PdfWriter.STANDARD_ENCRYPTION_128) Or (encryptStrength = PdfWriter.ENCRYPTION_AES_128) Or (encryptStrength = PdfWriter.ENCRYPTION_AES_256)))) And PdfEncryptor.IsAssemblyAllowed(perms) = False And PdfEncryptor.IsFillInAllowed(perms) = True And PdfEncryptor.IsModifyAnnotationsAllowed(perms) = False And PdfEncryptor.IsModifyContentsAllowed(perms) = False Then
                pnlPDFEncryption_PermissionsCmbChangeRestrictions.SelectedIndex = 2
            ElseIf ((encryptStrength = PdfWriter.STANDARD_ENCRYPTION_128 Or encryptStrength = PdfWriter.ENCRYPTION_AES_128 Or encryptStrength = PdfWriter.ENCRYPTION_AES_256)) And PdfEncryptor.IsAssemblyAllowed(perms) = True And PdfEncryptor.IsFillInAllowed(perms) = False And PdfEncryptor.IsModifyAnnotationsAllowed(perms) = False And PdfEncryptor.IsModifyContentsAllowed(perms) = True Then
                pnlPDFEncryption_PermissionsCmbChangeRestrictions.SelectedIndex = 1
            ElseIf PdfEncryptor.IsAssemblyAllowed(perms) = True And PdfEncryptor.IsFillInAllowed(perms) = False And PdfEncryptor.IsModifyAnnotationsAllowed(perms) = False And PdfEncryptor.IsModifyContentsAllowed(perms) = False Then
                pnlPDFEncryption_PermissionsCmbChangeRestrictions.SelectedIndex = 1
            Else
                pnlPDFEncryption_PermissionsCmbChangeRestrictions.SelectedIndex = 5
            End If
            If PdfEncryptor.IsCopyAllowed(perms) Then
                pnlPDFEncryption_PermissionsChkRestrictions_Copy.Checked = True
            Else
                pnlPDFEncryption_PermissionsChkRestrictions_Copy.Checked = False
            End If
            If (PdfEncryptor.IsScreenReadersAllowed(perms) And ((encryptStrength = PdfWriter.STANDARD_ENCRYPTION_128) Or (encryptStrength = PdfWriter.ENCRYPTION_AES_128) Or (encryptStrength = PdfWriter.ENCRYPTION_AES_256))) Then
                pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared.Checked = True
            Else
                pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared.Checked = False
            End If
            Try
                If Not frm.pdfReaderDoc.ComputeUserPassword() Is Nothing Then
                    If frm.pdfReaderDoc.ComputeUserPassword().Length > 0 Then
                        pnlPDFEncryption_OpenPasswordTxtUserPassword.Text = System.Text.Encoding.ASCII.GetString(frm.pdfReaderDoc.ComputeUserPassword()) & ""
                    Else
                        pnlPDFEncryption_OpenPasswordTxtUserPassword.Text = ""
                    End If
                Else
                    pnlPDFEncryption_OpenPasswordTxtUserPassword.Text = ""
                End If
            Catch ex3 As Exception
                frm.TimeStampAdd(ex3, frm.debugMode)
            End Try
            If Not String.IsNullOrEmpty(pnlPDFEncryption_OpenPasswordTxtUserPassword.Text & "") Then
                pnlPDFEncryption_OpenPasswordChkRequired.Checked = True
            Else
                pnlPDFEncryption_OpenPasswordChkRequired.Checked = False
            End If
            pnlPDFEncryption_OpenPasswordChkRequired_CheckedChanged(Me, New EventArgs())
            pnlPDFEncryption_PermissionsTxtOwnerPassword.Text = frm.pdfOwnerPassword & ""
            If Not String.IsNullOrEmpty(pnlPDFEncryption_OpenPasswordTxtUserPassword.Text & "") Then
                pnlPDFEncryption_OpenPasswordChkRequired.Checked = True
            Else
                pnlPDFEncryption_OpenPasswordChkRequired.Checked = False
            End If
            If Not String.IsNullOrEmpty(pnlPDFEncryption_PermissionsTxtOwnerPassword.Text & "") Then
                pnlPDFEncryption_PermissionsChkRestrictDocument.Checked = True
            Else
                pnlPDFEncryption_PermissionsChkRestrictDocument.Checked = False
            End If
            pnlPDFEncryption_PermissionsChkRestrictDocument_CheckedChanged(Me, New EventArgs())
        Catch ex As Exception
            frm.TimeStampAdd(ex, frm.debugMode)
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.ApplicationModal + MsgBoxStyle.OkOnly, "Error!")
            Throw ex
        Finally
            frm.changingPermissionRestrictionCombo = False
        End Try
        Try
            If Panel_Visibility Then
                If Not Me.Visible Then
                    Me.Show()
                    Me.BringToFront()
                End If
            Else
                If Me.Visible Then
                    Me.Hide()
                End If
            End If
        Catch ex As Exception
            frm.TimeStampAdd(ex, frm.debugMode)
        End Try
    End Sub
    Public Sub ClearPDFEncryptionPanel(Optional ByVal blnClearOwnerPw As Boolean = True, Optional ByVal blnClearOwnerPwVariable As Boolean = False)
        Try
            If blnClearOwnerPw Then
                pnlPDFEncryption_PermissionsTxtOwnerPassword.Text = ""
            End If
            If blnClearOwnerPwVariable Then
                frm.pdfOwnerPassword = ""
            End If
            pnlPDFEncryption_OpenPasswordTxtUserPassword.Text = ""
            pnlPDFEncryption_OpenPasswordChkRequired.Checked = False
            pnlPDFEncryption_PermissionsCmbPrintingRestrictions.SelectedIndex = 0
            pnlPDFEncryption_PermissionsCmbChangeRestrictions.SelectedIndex = 0
            pnlPDFEncryption_PermissionsChkRestrictions_Copy.Checked = False
            pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared.Checked = False
            pnlPDFEncryption_PermissionsChkRestrictions_TextAccessVisuallyImpared.Enabled = True
            pnlPDFEncryption_EncryptionRadAllExceptMeta.Checked = False
            pnlPDFEncryption_PermissionsChkRestrictDocument.Checked = False
            pnlPDFEncryption_CompatibilityCmb.SelectedIndex = 4
        Catch ex As Exception
            frm.TimeStampAdd(ex, frm.debugMode)
        End Try
    End Sub
    Public Function UnlockSecurePDF(ByVal pdfBytes() As Byte) As Byte()
        Dim reader As PdfReader = Nothing
        Dim stamper As PdfStamper = Nothing
        Dim strModified As New System.IO.MemoryStream
        Try
            If pdfBytes Is Nothing Then
                Return Nothing
            ElseIf pdfBytes.Length <= 0 Then
                Return pdfBytes
            End If
            Try
                If Not String.IsNullOrEmpty(frm.pdfOwnerPassword & "") Then
                    PdfReader.unethicalreading = True
                    reader = New PdfReader(pdfBytes, frm.getBytes(frm.pdfOwnerPassword & ""))
                    reader.encrypted = False
                Else
                    PdfReader.unethicalreading = True
                    reader = New PdfReader(pdfBytes)
                    reader.encrypted = False
                End If
            Catch badPassword As iTextSharp.text.exceptions.BadPasswordException
TRYAGAIN:
                Dim userOrOwnerPw As String = New clsPromptDialog().ShowDialog("The document appears to be double-encrypted with an open AND a modify password." & Environment.NewLine & Environment.NewLine & "Please type either the ""Open"" OR the ""Modify"" password in the textbox:", "Double Encryption: Error", Me, "", "FORCE UNLOCK")
                Try
                    If String.IsNullOrEmpty(userOrOwnerPw & "") Then
                        Return Nothing
                    End If
                    PdfReader.unethicalreading = True
                    reader = New PdfReader(pdfBytes, frm.getBytes(userOrOwnerPw & ""))
                    reader.encrypted = False
                Catch ex2 As iTextSharp.text.exceptions.BadPasswordException
                    Return Nothing
                Finally
                End Try
            End Try
            stamper = frm.getStamper(reader, strModified)
            stamper.CreateXmpMetadata()
            stamper.Writer.CloseStream = False
            stamper.Close()
            Return strModified.ToArray()
        Catch ex As Exception
            If Not stamper Is Nothing Then
                stamper.Writer.CloseStream = False
                stamper.Close()
            End If
            Throw ex
        Finally
            PdfReader.unethicalreading = False
        End Try
        Return pdfBytes
    End Function
    Public Function GetDecryptedPDFBytes(ByVal pdfBytes() As Byte) As Byte()
        Dim reader As PdfReader = Nothing
        Try
            PdfReader.unethicalreading = True
            If Not String.IsNullOrEmpty(frm.pdfOwnerPassword & "") Then
                reader = New PdfReader(frm.Session, frm.getBytes(frm.pdfOwnerPassword & ""))
            Else
                reader = New PdfReader(frm.Session)
            End If
            reader.encrypted = False
            Dim strModified As New System.IO.MemoryStream
            Dim stamper As PdfStamper = frm.getStamper(reader, strModified)
            stamper.CreateXmpMetadata()
            stamper.Writer.CloseStream = False
            stamper.Close()
            Return strModified.ToArray()
        Catch ex As Exception
            Throw ex
        Finally
            PdfReader.unethicalreading = False
        End Try
        Return pdfBytes
    End Function
    Public blnCloseForm As Boolean = False
    Private Sub dialogSecurityPassword_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            If blnCloseForm = False Then
                e.Cancel = True
                Me.Hide()
            Else
                Return
            End If
        Catch ex As Exception
            Err.Clear()
        Finally
            If Not docPropertiesDialog Is Nothing Then
                docPropertiesDialog.Show()
                docPropertiesDialog.BringToFront()
            ElseIf Not frm Is Nothing Then
                frm.Show()
                frm.BringToFront()
            End If
        End Try
    End Sub
End Class
