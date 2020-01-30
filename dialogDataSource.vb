Imports System.Windows.Forms
Public Class dialogDataSource
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 NK-INC.COM All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        disableLoad = False
        If ddlRecord.SelectedIndex >= 0 Then
            UpdateMessage("Entering Record Edit Mode: @ " & DateTime.Now.ToString)
        End If
        Me.Close()
    End Sub
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        disableLoad = True
        UpdateMessage("Exiting Record Edit Mode: @ " & DateTime.Now.ToString)
        Me.Close()
    End Sub
    Dim Database_Format(5) As String
    Dim Data_Fields(4, 4) As String
    Dim Database_Filter As String
    Dim XML_RAW_NAME As String = ""
    Public SessionBytes() As Byte = Nothing
    Public pdfOwnerPassword As String = ""
    Private strNameConventions(0) As String
    Public clsFileXML1 As New clsFileXML(Me)
    Public blnCancelProcess As Boolean = False
    Public Sub LoadPDF(ByVal session() As Byte, ByVal pdfownerPw As String, ByRef frmMain2 As frmMain)
        SessionBytes = session
        pdfOwnerPassword = pdfownerPw
        frmMain1 = frmMain2
    End Sub
    Private Sub btnSelectSource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectSource.Click
        SelectSource()
    End Sub
    Public Sub SelectSource(Optional ByVal fn As String = "")
        Try
            lblStatus.Text = ""
            Dim strFilnm As String
            If Not (String.IsNullOrEmpty(fn & "")) Then
                If System.IO.File.Exists(fn & "") Then
                    cmnDlg.InitialDirectory = System.IO.Path.GetDirectoryName(fn) & ""
                    cmnDlg.FileName = fn
                Else
                    cmnDlg.InitialDirectory = Application.StartupPath.ToString() & ""
                    cmnDlg.Filter = Database_Filter
                    If Not String.IsNullOrEmpty(txtDataFields_0.Text & "") Then
                        If Not CStr(txtDataFields_0.Text & "").ToLower = "datasource".ToLower() Then
                            cmnDlg.FileName = txtDataFields_0.Text & ""
                        Else
                            cmnDlg.FileName = ""
                        End If
                    Else
                        cmnDlg.FileName = ""
                    End If
                    cmnDlg.ShowDialog()
                End If
            Else
                cmnDlg.InitialDirectory = Application.StartupPath.ToString() & ""
                cmnDlg.Filter = Database_Filter
                If Not String.IsNullOrEmpty(txtDataFields_0.Text & "") Then
                    If Not CStr(txtDataFields_0.Text & "").ToLower = "datasource".ToLower() Then
                        cmnDlg.FileName = txtDataFields_0.Text & ""
                    Else
                        cmnDlg.FileName = ""
                    End If
                Else
                    cmnDlg.FileName = ""
                End If
                cmnDlg.ShowDialog()
            End If
            strFilnm = cmnDlg.FileName
            txtDataFields_0.Text = cmnDlg.FileName
            Select Case New System.IO.FileInfo(cmnDlg.FileName).Extension
                Case ".xml"
                    modSources.Database_Connection = cmnDlg.FileName
                    txtDataFields_0.Visible = True
                    txtDataFields_1.Visible = False
                    txtDataFields_2.Visible = False
                    txtDataFields_3.Visible = False
                    Label12.Text = "Username:"
                    Label13.Text = "Password:"
                    clsFileXML1.Data_SourcePath = cmnDlg.FileName & ""
                    clsFileXML1.Data_Username = txtDataFields_1.Text & ""
                    clsFileXML1.Data_Password = txtDataFields_2.Text & ""
                    clsFileXML1.Data_DatabasePassword = txtDataFields_3.Text
                    clsFileXML1.Database_Connection = cmnDlg.FileName
                Case ".json"
                    modSources.Database_Connection = cmnDlg.FileName
                    txtDataFields_0.Visible = True
                    txtDataFields_1.Visible = False
                    txtDataFields_2.Visible = False
                    txtDataFields_3.Visible = False
                    Label12.Text = "Username:"
                    Label13.Text = "Password:"
                    clsFileXML1.Data_SourcePath = cmnDlg.FileName & ""
                    clsFileXML1.Data_Username = txtDataFields_1.Text & ""
                    clsFileXML1.Data_Password = txtDataFields_2.Text & ""
                    clsFileXML1.Data_DatabasePassword = txtDataFields_3.Text
                    clsFileXML1.Database_Connection = cmnDlg.FileName
                Case ".mdb"
                    If txtDataFields_3.Text = "Database Password" Or txtDataFields_3.Text = "" Then
                        modADOConn.OLEDB_MSJet(strFilnm, CStr(IIf(txtDataFields_1.Text = "Username", "", txtDataFields_1.Text)), CStr(IIf(txtDataFields_2.Text = "Password", "", txtDataFields_2.Text)))
                    Else
                        modADOConn.OLEDB_MSJetDBPassword(strFilnm, txtDataFields_3.Text, CStr(IIf(txtDataFields_1.Text = "Username", "", txtDataFields_1.Text)), CStr(IIf(txtDataFields_2.Text = "Password", "", txtDataFields_2.Text)))
                    End If
                    modSources.Database_Connection = modADOConn.Connection_String_VB
                    clsFileXML1.Data_SourcePath = cmnDlg.FileName & ""
                    clsFileXML1.Data_Username = txtDataFields_1.Text & ""
                    clsFileXML1.Data_Password = txtDataFields_2.Text & ""
                    clsFileXML1.Data_DatabasePassword = txtDataFields_3.Text
                    clsFileXML1.Database_Connection = modSources.Database_Connection & ""
                    Label12.Text = "Username:"
                    Label13.Text = "Password:"
                    Label14.Text = "dB Password:"
                    txtDataFields_0.Visible = True
                    txtDataFields_1.Visible = True
                    txtDataFields_2.Visible = True
                    txtDataFields_3.Visible = True
                Case ".xls"
                    modADOConn.OLEDB_MSJetExcell(cmnDlg.FileName, True)
                    modSources.Database_Connection = modADOConn.Connection_String_Net
                    clsFileXML1.Data_SourcePath = cmnDlg.FileName & ""
                    clsFileXML1.Data_Username = txtDataFields_1.Text & ""
                    clsFileXML1.Data_Password = txtDataFields_2.Text & ""
                    Label12.Text = "Username:"
                    Label13.Text = "Password:"
                    clsFileXML1.Data_DatabasePassword = txtDataFields_3.Text
                    clsFileXML1.Database_Connection = modSources.Database_Connection & ""
                    txtDataFields_0.Visible = True
                    txtDataFields_1.Visible = True
                    txtDataFields_2.Visible = True
                    txtDataFields_3.Visible = True
            End Select
            Label12.Visible = txtDataFields_0.Visible
            Label13.Visible = txtDataFields_1.Visible
            Label14.Visible = txtDataFields_2.Visible
            Label15.Visible = txtDataFields_3.Visible
            lblStatus.Text = modSources.Database_Connection
            If Test_Connection() Then
                lblConnected.Text = "Test: Connected"
                btnNext.Visible = True
            Else
                lblConnected.Text = "Test: Not Connected"
                btnNext.Visible = False
            End If
            If PopulateTables() Then
                Populate_TableCombo(cmbDBTables)
                Populate_TableCombo(cmbDBTablesFilter)
            End If
            Exit Sub
        Catch ex As Exception
            lblStatus.Text = "Datastructure Not Loaded"
            frmMain1.TimeStampAdd(ex, frmMain1.debugMode)
            Err.Clear()
        End Try
        Return
errHandler:
        lblStatus.Text = "Datastructure Not Loaded"
    End Sub
    Public Function Populate_TableCombo(ByVal cTables As ComboBox) As Boolean
        Dim xTables As Table
        cTables.Items.Clear()
        For Each xTables In Tables.ToArray
            If Not xTables.Name Is Nothing Then
                cTables.Items.Add(xTables.Name & "")
            End If
        Next
        If cTables.Items.Count > 0 Then
            If cmbDBTables.Items.Count = cTables.Items.Count Then
                If cmbDBTables.SelectedIndex >= 0 Then
                    cTables.SelectedIndex = cmbDBTables.SelectedIndex
                Else
                    cTables.SelectedIndex = 0
                End If
            Else
                cTables.SelectedIndex = cmbDBTables.SelectedIndex
            End If
        End If
        Populate_TableCombo = True
    End Function
    Private Sub dialogDataSource_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If btnMinimize.Text.ToLower = "Restore".ToLower Then
            e.Cancel = True
            btnMinimize_Click(Me, New EventArgs())
            Return
        End If
        RemoveHandler btnRecordNav_Next.Click, AddressOf btnNav_Next_Click
        RemoveHandler btnRecordNav_Previous.Click, AddressOf btnNav_Previous_Click
        RemoveHandler btnRecordNav_Last.Click, AddressOf btnNav_Last_Click
        RemoveHandler btnRecordNav_First.Click, AddressOf btnNav_First_Click
        RemoveHandler ddlRecord.SelectedIndexChanged, AddressOf ddlRecord_SelectedIndexChanged
        frm_Close()
    End Sub
    Public disableLoad As Boolean = False
    Private Sub frmDataSource_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        frmMain1 = DirectCast(Me.Owner, frmMain)
        If disableLoad Then
            AddHandler btnRecordNav_Next.Click, AddressOf btnNav_Next_Click
            AddHandler btnRecordNav_Previous.Click, AddressOf btnNav_Previous_Click
            AddHandler btnRecordNav_Last.Click, AddressOf btnNav_Last_Click
            AddHandler btnRecordNav_First.Click, AddressOf btnNav_First_Click
            AddHandler ddlRecord.SelectedIndexChanged, AddressOf ddlRecord_SelectedIndexChanged
            Return
        End If
        Try
            modSources.DBFields.Clear()
            modSources.PDFFields.Clear()
            modSources.FieldsMaps.Clear()
            lstMappedFields.Items.Clear()
            modSources.FilterFields.Clear()
        Catch ex As Exception
            If frmMain1.debugMode Then Throw ex Else Err.Clear()
        End Try
        Try
            frmMain1.Session("savedSource") = frmMain1.Session()
            SessionBytes = frmMain1.Session()
            pdfOwnerPassword = frmMain1.pdfOwnerPassword & ""
        Catch ex As Exception
            If frmMain1.debugMode Then Throw ex Else Err.Clear()
        End Try
        ShowPanel(pnlDataSource)
        Data_Fields(0, 0) = "Datasource"
        Data_Fields(1, 0) = "admin"
        Data_Fields(2, 0) = "Password"
        Data_Fields(3, 0) = "Database Password"
        txtDataFields_0.Text = Data_Fields(0, 0)
        txtDataFields_1.Text = Data_Fields(1, 0)
        txtDataFields_2.Text = Data_Fields(2, 0)
        txtDataFields_3.Text = Data_Fields(3, 0)
        If txtDataFields_0.Text = "" Then
            txtDataFields_0.Visible = False
        Else
            txtDataFields_0.Visible = True
        End If
        If txtDataFields_1.Text = "" Then
            txtDataFields_1.Visible = False
        Else
            txtDataFields_1.Visible = True
        End If
        If txtDataFields_2.Text = "" Then
            txtDataFields_2.Visible = False
        Else
            txtDataFields_2.Visible = True
        End If
        If txtDataFields_3.Text = "" Then
            txtDataFields_3.Visible = False
        Else
            txtDataFields_3.Visible = True
        End If
        Database_Filter = "XML|*.XML|Json|*.json|MDB|*.MDB|XLS|*.XLS"
        AddHandler btnRecordNav_Next.Click, AddressOf btnNav_Next_Click
        AddHandler btnRecordNav_Previous.Click, AddressOf btnNav_Previous_Click
        AddHandler btnRecordNav_Last.Click, AddressOf btnNav_Last_Click
        AddHandler btnRecordNav_First.Click, AddressOf btnNav_First_Click
        AddHandler ddlRecord.SelectedIndexChanged, AddressOf ddlRecord_SelectedIndexChanged
    End Sub
    Private Sub btnLoadPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadPDF.Click
        If Load_PDF(SessionBytes, pdfOwnerPassword) Then
            lnkAddNew.Enabled = True
            lnkRemove.Enabled = True
            cmbPDFFields.Enabled = True
            cmbPDFFields.Items.Clear()
            lstMappedFields.Items.Clear()
            Populate_PDFFields_Combo(cmbPDFFields, lstMappedFields, Me)
            UpdateMessage("Loaded PDF form success!")
        Else
            lnkAddNew.Enabled = False
            lnkRemove.Enabled = False
            cmbPDFFields.Enabled = False
            cmbPDFFields.Items.Clear()
            UpdateMessage("Error Loading PDF form.")
        End If
    End Sub
    Public fieldNames As New List(Of String)
    Public Function getAllFieldNames(Optional ByVal force As Boolean = False) As String()
        Try
            If force Or fieldNames.Count <= 0 Then
                fieldNames = New List(Of String)
                If Not SessionBytes Is Nothing Then
                    If SessionBytes.Length > 0 Then
                        Dim MySource As New List(Of String)
                        Dim cfdf As New FDFApp.FDFDoc_Class
                        Dim cpdf As New FDFApp.FDFApp_Class
                        cfdf = cpdf.PDFOpenFromBuf(SessionBytes, True, True, pdfOwnerPassword)
                        For Each fld As FDFApp.FDFDoc_Class.FDFField In cfdf.XDPGetAllFields()
                            If Not fld Is Nothing Then
                                If Not String.IsNullOrEmpty(fld.FieldName.ToString.Trim() & "") Then
                                    MySource.Add("" & fld.FieldName & "")
                                    fieldNames.Add("" & fld.FieldName & "")
                                End If
                            End If
                        Next
                        Return MySource.ToArray
                    End If
                End If
            End If
        Catch ex As Exception
            If frmMain1.debugMode Then Throw ex Else Err.Clear()
        End Try
        Return Nothing
    End Function
    Private lv As New List(Of clsAutocomplete)
    Public cfdf As New FDFApp.FDFDoc_Class
    Public cpdf As New FDFApp.FDFApp_Class
    Public Function InjectFieldNameValues(ByVal strInput As String, ByVal cfdfDoc As FDFApp.FDFDoc_Class) As String
        Dim strTmp As String = strInput & ""
        If String.IsNullOrEmpty(strInput & "") Then
            Return strInput & ""
        End If
        For Each fld As String In fieldNames
            Try
                strTmp = strTmp.Replace("{" & fld.ToString & "}", cfdfDoc.FDFGetValue(fld.ToString.ToString.Replace("{", "").Replace("}", "").ToString(), False) & "")
            Catch ex As Exception
                If frmMain1.debugMode Then Throw ex Else Err.Clear()
            End Try
        Next
        Return strTmp.ToString & ""
    End Function
    Public Function InjectFieldNameValues(ByVal strInput As String, ByVal dr As DataRow) As String
        Dim strTmp As String = strInput & ""
        If String.IsNullOrEmpty(strInput & "") Then
            Return strInput & ""
        End If
        For Each fld As DataColumn In dr.Table.Columns
            Try
                strTmp = strTmp.Replace("{" & fld.ColumnName.ToString & "}", CStr(dr(fld.ColumnName.ToString & "")) & "")
            Catch ex As Exception
                If frmMain1.debugMode Then Throw ex Else Err.Clear()
            End Try
        Next
        Return strTmp.ToString & ""
    End Function
    Public Sub addAutoCompleteFields(ByVal sessionBytesTemp() As Byte, ByVal pdfownerPasswordTemp As String)
        Try
            SessionBytes = sessionBytesTemp
            pdfOwnerPassword = pdfownerPasswordTemp
            cfdf = cpdf.PDFOpenFromBuf(SessionBytes, True, True, pdfOwnerPassword)
            Dim MySource As New AutoCompleteStringCollection()
            For Each fld As String In getAllFieldNames(True)
                If Not String.IsNullOrEmpty(fld.ToString.Trim() & "") Then
                    MySource.Add("{" & fld.ToString.Trim() & "}")
                End If
            Next
            lv.Add(New clsAutocomplete(txtMappingRaw, fieldNames.ToArray(), False))
        Catch ex As Exception
            If frmMain1.debugMode Then Throw ex Else Err.Clear()
        End Try
    End Sub
    Private Sub lnkLoadPDF_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkLoadPDF.LinkClicked
        If Load_PDF(SessionBytes, pdfOwnerPassword) Then
            lnkAddNew.Enabled = True
            lnkRemove.Enabled = True
            cmbPDFFields.Enabled = True
            cmbPDFFields.Items.Clear()
            For Each itm As String In lstMappedFields.Items
                Try
                    RemoveMap(itm.ToString & "")
                Catch ex As Exception
                    If frmMain1.debugMode Then Throw ex Else Err.Clear()
                End Try
            Next
            lstMappedFields.Items.Clear()
            Populate_PDFFields_Combo(cmbPDFFields, lstMappedFields, Me)
            addAutoCompleteFields(SessionBytes, pdfOwnerPassword)
            btnRecords_Affected_Click(Me, New EventArgs())
        Else
            lnkAddNew.Enabled = False
            lnkRemove.Enabled = False
            For Each itm As String In lstMappedFields.Items
                Try
                    RemoveMap(itm.ToString & "")
                Catch ex As Exception
                    If frmMain1.debugMode Then Throw ex Else Err.Clear()
                End Try
            Next
            lstMappedFields.Items.Clear()
            cmbPDFFields.Enabled = False
            cmbPDFFields.Items.Clear()
        End If
    End Sub
    Private Sub cmbDBTables_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDBTables.SelectedIndexChanged
        If cmbDBTables.SelectedIndex >= 0 Then
            Populate_DBFieldsList(lstDBFields, cmbDBTables.Text)
        End If
    End Sub
    Private Sub lnkAddNew_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkAddNew.LinkClicked
        Dim strX As String = ""
        Dim xItems As CheckedListBox.CheckedItemCollection, intSelCnt As Integer
        xItems = lstDBFields.CheckedItems
        For intSelCnt = 0 To xItems.Count - 1
            strX = strX & CStr(IIf(strX = "", "", " ")) & "{" & CStr(xItems(intSelCnt)) & "}"
        Next
        If Not String.IsNullOrEmpty(Me.txtMappingRaw.Text) Then
            Me.txtMappingRaw.Text &= "{" & strX & "}"
            AddMap(cmbPDFFields.Text, strX, cmbDBTables.Text, Me.txtMappingRaw.Text)
            lstMappedFields.Items.Add(Me.txtMappingRaw.Text)
        Else
            Me.txtMappingRaw.Text = cmbPDFFields.Text & "=" & "{" & strX & "}"
            AddMap(cmbPDFFields.Text, strX, cmbDBTables.Text, Me.txtMappingRaw.Text)
            lstMappedFields.Items.Add("{" & strX & "}")
        End If
        lstDBFields.ClearSelected()
    End Sub
    Private Sub lnkRemove_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkRemove.LinkClicked
        If lstMappedFields.SelectedIndex > -1 Then
            If Not lstMappedFields.SelectedItem Is Nothing Then
                modSources.RemoveMap(lstMappedFields.SelectedItem.ToString.Split("="c)(0))
                lstMappedFields.Items.Remove(lstMappedFields.SelectedItem)
                UpdateMappFieldsList()
                Return
            End If
        End If
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        dsx = New DataSet
        If Database_Connection.ToString.ToLower.EndsWith(".xml") Then
            dsx = MergeXML()
        ElseIf Database_Connection.ToString.ToLower.EndsWith(".json") Then
            dsx = MergeJSON()
        Else
            dsx = Merge()
        End If
    End Sub
    Private Sub txtDataFields_0_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)
    End Sub
    Public Function Add_FilterField(ByVal FieldName As String, ByVal FieldType As Integer, ByVal TableName As String, ByVal FieldValue As String, ByVal Comparison As String) As FilterField
        If Not FieldName = "" Then
            Dim _FilterField As New FilterField
            _FilterField.DBField = FieldName
            _FilterField.DBTable = TableName
            _FilterField.DBValue = FieldValue
            _FilterField.FieldType = FieldType
            Select Case FieldType
                Case ADODB.DataTypeEnum.adDate, ADODB.DataTypeEnum.adDBDate, ADODB.DataTypeEnum.adDBTime, ADODB.DataTypeEnum.adDBTimeStamp
                    _FilterField.DBSql = "[" & TableName & "].[" & FieldName & "] " & Comparison & " #" & FieldValue & "#"
                Case ADODB.DataTypeEnum.adBigInt, ADODB.DataTypeEnum.adCurrency, ADODB.DataTypeEnum.adDecimal, ADODB.DataTypeEnum.adDouble, ADODB.DataTypeEnum.adInteger, ADODB.DataTypeEnum.adNumeric, ADODB.DataTypeEnum.adSingle, ADODB.DataTypeEnum.adSmallInt, ADODB.DataTypeEnum.adTinyInt, ADODB.DataTypeEnum.adVarNumeric
                    _FilterField.DBSql = "[" & TableName & "].[" & FieldName & "]  " & Comparison & FieldValue
                Case ADODB.DataTypeEnum.adBSTR, ADODB.DataTypeEnum.adVarWChar, ADODB.DataTypeEnum.adWChar, ADODB.DataTypeEnum.adVarChar, ADODB.DataTypeEnum.adChar, ADODB.DataTypeEnum.adLongVarChar, ADODB.DataTypeEnum.adLongVarWChar, ADODB.DataTypeEnum.adWChar
                    If Comparison.ToLower = "like" Then
                        _FilterField.DBSql = "[" & TableName & "].[" & FieldName & "]  " & Comparison & "  '%" & FieldValue & "%'"
                    ElseIf Comparison.ToLower = "not null" Then
                        _FilterField.DBSql = "[" & TableName & "].[" & FieldName & "] IS NOT NULL"
                    ElseIf Comparison.ToLower = "null" Then
                        _FilterField.DBSql = "[" & TableName & "].[" & FieldName & "] IS NULL"
                    Else
                        _FilterField.DBSql = "[" & TableName & "].[" & FieldName & "]  " & Comparison & "  '" & FieldValue & "'"
                    End If
                Case ADODB.DataTypeEnum.adBoolean
                    _FilterField.DBSql = "[" & TableName & "].[" & FieldName & "]  " & Comparison & "  " & FieldValue
                Case Else
                    _FilterField.DBSql = "[" & TableName & "].[" & FieldName & "]  " & Comparison & "  " & FieldValue
            End Select
            modSources.FilterFields.Add(_FilterField)
            Return _FilterField
        End If
        Return Nothing
    End Function
    Private Sub lnkAddFilter_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkAddFilter.LinkClicked
        Add_FilterField(cmbDBFieldFilter.Text, cDBFieldType(cmbDBFieldFilter.SelectedIndex + 1), cmbDBTables.Text, txtFieldValue.Text & "", cmbComparison.Text & "")
        lstFilter.Items.Add(cmbDBFieldFilter.Text & " " & cmbComparison.Text & " " & txtFieldValue.Text & "")
        If Database_Connection.ToString.ToLower.EndsWith(".xml") Then
            dsx = MergeXML()
        ElseIf Database_Connection.ToString.ToLower.EndsWith(".json") Then
            dsx = MergeJSON()
        Else
            dsx = Merge()
        End If
        btnRecords_Affected.Text = "Records Affected = " & dsx.Tables(0).Rows.Count
        LoadRecordsGroup(ddlRecord)
    End Sub
    Private Sub cmbDBFieldFilter_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDBFieldFilter.SelectedIndexChanged
        If cmbDBFieldFilter.SelectedIndex < 0 Then Exit Sub
        txtFieldValue.Text = ""
        cmbComparison.Items.Clear()
        Select Case cDBFieldType(cmbDBFieldFilter.SelectedIndex + 1)
            Case ADODB.DataTypeEnum.adDate, ADODB.DataTypeEnum.adDBDate, ADODB.DataTypeEnum.adDBTime, ADODB.DataTypeEnum.adDBTimeStamp
                txtFieldValue.Text = Today.Date.ToString
                cmbComparison.Items.Add("="c)
                cmbComparison.Items.Add(">")
                cmbComparison.Items.Add("<")
                cmbComparison.Items.Add(">=")
                cmbComparison.Items.Add("<=")
                cmbComparison.Items.Add("NOT =")
            Case ADODB.DataTypeEnum.adBigInt, ADODB.DataTypeEnum.adCurrency, ADODB.DataTypeEnum.adDecimal, ADODB.DataTypeEnum.adDouble, ADODB.DataTypeEnum.adInteger, ADODB.DataTypeEnum.adNumeric, ADODB.DataTypeEnum.adSingle, ADODB.DataTypeEnum.adSmallInt, ADODB.DataTypeEnum.adTinyInt, ADODB.DataTypeEnum.adVarNumeric
                txtFieldValue.Text = "0"
                cmbComparison.Items.Add("="c)
                cmbComparison.Items.Add(">")
                cmbComparison.Items.Add("<")
                cmbComparison.Items.Add(">=")
                cmbComparison.Items.Add("<=")
                cmbComparison.Items.Add("NOT =")
            Case ADODB.DataTypeEnum.adBSTR, ADODB.DataTypeEnum.adChar, ADODB.DataTypeEnum.adLongVarChar, ADODB.DataTypeEnum.adLongVarWChar, ADODB.DataTypeEnum.adWChar, ADODB.DataTypeEnum.adVarWChar, ADODB.DataTypeEnum.adVarChar
                txtFieldValue.Text = ""
                cmbComparison.Items.Add("="c)
                cmbComparison.Items.Add("LIKE")
                cmbComparison.Items.Add("<>")
            Case ADODB.DataTypeEnum.adBoolean
                txtFieldValue.Text = "False"
                cmbComparison.Items.Add("="c)
            Case Else
                cmbComparison.Items.Add("="c)
                txtFieldValue.Text = ""
        End Select
    End Sub
    Private Sub cmbDBTablesFilter_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDBTablesFilter.SelectedIndexChanged
        If cmbDBTables.SelectedIndex < 0 Then Exit Sub
        Populate_DBFieldsCombo(cmbDBFieldFilter, cmbDBTablesFilter.Text & "")
    End Sub
    Public Sub Populate_DBFieldsCombo(ByVal cFields As ComboBox, ByVal sTableName As String)
        Dim xField As DBField
        cFields.Items.Clear()
        PopulateFields(sTableName)
        If modSources.DBFields.Count <= 0 Then Exit Sub
        For Each xField In modSources.DBFields
            If Not xField.Name Is Nothing Then
                cFields.Items.Add(xField.Name)
            End If
        Next
    End Sub
    Private Sub btnRecords_Affected_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRecords_Affected.Click
        dsx = New DataSet
        If Database_Connection.ToString.ToLower.EndsWith(".xml") Then
            dsx = MergeXML()
        ElseIf Database_Connection.ToString.ToLower.EndsWith(".json") Then
            dsx = MergeJSON()
        Else
            dsx = Merge()
        End If
        btnRecords_Affected.Text = "Records Affected = " & dsx.Tables(0).Rows.Count
        LoadRecordsGroup(ddlRecord)
    End Sub
    Public dsx As New DataSet
    Public rowIndex As Integer = -1
    Public Sub LoadRecordsGroup(ByRef ddlRecord1 As ComboBox, Optional selectedIndex As Integer = 0)
        If Database_Connection.ToString.ToLower.EndsWith(".xml") Then
            dsx = MergeXML()
        ElseIf Database_Connection.ToString.ToLower.EndsWith(".json") Then
            dsx = MergeJSON()
        Else
            dsx = Merge()
        End If
        ddlRecord1.Items.Clear()
        For r As Integer = 1 To dsx.Tables(0).Rows.Count
            ddlRecord1.Items.Add(CInt(r).ToString() & " of " & dsx.Tables(0).Rows.Count.ToString & "")
        Next
        ddlRecord1.SelectedIndex = selectedIndex
    End Sub
    Public Sub UpdateMappFieldsList()
        lstMappedFields.Items.Clear()
        For Each FieldMapX As FieldMap In modSources.FieldsMaps
            lstMappedFields.Items.Add(FieldMapX.DBMapRaw)
        Next
    End Sub
    Private Sub lnkSelect_Folder_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        fldOutput.ShowDialog()
    End Sub
    Private Sub pnlOutputPDFs_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)
    End Sub
    Private Sub pnlDataSource_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)
    End Sub
    Private Sub btnFilterMergePanel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ShowPanel(pnlFilterPrint)
        Populate_TableCombo(Me.cmbDBTablesFilter)
    End Sub
    Private Sub btnStartOver_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ShowPanel(pnlDataSource)
    End Sub
    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        cmbDBTables.Items.Clear()
        If Populate_TableCombo(cmbDBTables) Then
            ShowPanel(pnlFields)
        End If
    End Sub
    Private Sub lnkRemoveFilter_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkRemoveFilter.LinkClicked
        If Me.lstFilter.SelectedIndex >= 0 And Me.lstFilter.Items.Count > 0 Then
            RemoveFilter(Me.lstFilter.SelectedIndex)
            Me.lstFilter.Items.RemoveAt(Me.lstFilter.SelectedIndex)
        End If
        dsx = New DataSet
        If Database_Connection.ToString.ToLower.EndsWith(".xml") Then
            dsx = MergeXML()
        ElseIf Database_Connection.ToString.ToLower.EndsWith(".json") Then
            dsx = MergeJSON()
        Else
            dsx = Merge()
        End If
        btnRecords_Affected.Text = "Records Affected = " & dsx.Tables(0).Rows.Count
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
            If frmMain1.debugMode Then Throw ex Else Err.Clear()
        Finally
            Me.Visible = False
        End Try
    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub
    Private Sub TabPage2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ShowPanel(pnlDataSource)
    End Sub
    Public Sub ShowPanel(ByVal pnl As Panel)
        Me.pnlDataSource.Visible = True
        Me.pnlFields.Visible = True
        Me.pnlFilterPrint.Visible = True
        pnl.Show()
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub lstNameConventions_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub lstMappedFields_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstMappedFields.SelectedIndexChanged
        If lstMappedFields.SelectedIndex >= 0 Then
            Me.txtMappingRaw.Text = modSources.FieldsMaps(Me.lstMappedFields.SelectedIndex).DBMapRaw.ToString
        Else
            Me.txtMappingRaw.Text = ""
        End If
    End Sub
    Private Sub txtMappingRaw_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMappingRaw.TextChanged
        If lstMappedFields.SelectedIndex >= 0 Then
            DBRawFieldMapping(Me.lstMappedFields.SelectedIndex) = Me.txtMappingRaw.Text & ""
            Dim xSelect As Integer = Me.lstMappedFields.SelectedIndex
            UpdateMappFieldsList()
            Me.lstMappedFields.SelectedIndex = xSelect + 0
        End If
    End Sub
    Private Sub Button4_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim _r As DialogResult
        With SaveFileSettings
            _r = .ShowDialog()
            Select Case _r
                Case Windows.Forms.DialogResult.OK
                    clsFileXML1.Data_SourcePath = cmnDlg.FileName & ""
                    clsFileXML1.Data_Username = txtDataFields_1.Text & ""
                    clsFileXML1.Data_Password = txtDataFields_2.Text & ""
                    clsFileXML1.Data_DatabasePassword = txtDataFields_3.Text
                    clsFileXML1.Database_Connection = modSources.Database_Connection & ""
                    clsFileXML1.NameConventionsClear()
                    Try
                    Catch ex As Exception
                    End Try
                    clsFileXML1.SavePDFEmail(.FileName)
                    UpdateMessage("File saved success!")
                Case Else
                    UpdateMessage("Error - File not saved!")
                    Return
            End Select
        End With
    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim _r As DialogResult
        clsFileXML1 = New clsFileXML(Me)
        With OpenFileSettings
            _r = OpenFileSettings.ShowDialog()
            Select Case _r
                Case Windows.Forms.DialogResult.OK
                    Try
                        clsFileXML1.Data_SourcePath = OpenFileSettings.FileName
                        clsFileXML1.LoadSerialized(OpenFileSettings.FileName, Me)
                    Catch ex As Exception
                        UpdateMessage("Error loading settings file")
                        If frmMain1.debugMode Then Throw ex Else Err.Clear()
                        Return
                    End Try
                    Try
                        OpenFileSettings.FileName = clsFileXML1.Data_SourcePath & ""
                        txtDataFields_1.Text = clsFileXML1.Data_Username & ""
                        txtDataFields_2.Text = clsFileXML1.Data_Password & ""
                        txtDataFields_3.Text = clsFileXML1.Data_DatabasePassword & ""
                        modSources.Database_Connection = clsFileXML1.Database_Connection & ""
                    Catch ex As Exception
                        If frmMain1.debugMode Then Throw ex Else Err.Clear()
                    End Try
                    modSources.DBFields.Clear()
                    modSources.PDFFields.Clear()
                    modSources.FieldsMaps.Clear()
                    lstMappedFields.Items.Clear()
                    modSources.FilterFields.Clear()
                    Try
                        UpdateMappFieldsList()
                        For Each fld As FilterField In modSources.FilterFields
                            lstFilter.Items.Add(fld.DBSql)
                        Next
                    Catch ex As Exception
                        If frmMain1.debugMode Then Throw ex Else Err.Clear()
                    End Try
                    Me.txtDataFields_0.Text = clsFileXML1.Data_SourcePath & ""
                    Try
                        Select Case New System.IO.FileInfo(.FileName).Extension
                            Case ".xml"
                                modSources.Database_Connection = .FileName
                                txtDataFields_0.Visible = True
                                txtDataFields_1.Visible = False
                                txtDataFields_2.Visible = False
                                txtDataFields_3.Visible = False
                                clsFileXML1.Data_SourcePath = .FileName & ""
                                clsFileXML1.Data_Username = txtDataFields_1.Text & ""
                                clsFileXML1.Data_Password = txtDataFields_2.Text & ""
                                clsFileXML1.Data_DatabasePassword = txtDataFields_3.Text
                                clsFileXML1.Database_Connection = .FileName
                                SelectSource(.FileName)
                            Case ".json"
                                modSources.Database_Connection = .FileName
                                txtDataFields_0.Visible = True
                                txtDataFields_1.Visible = False
                                txtDataFields_2.Visible = False
                                txtDataFields_3.Visible = False
                                clsFileXML1.Data_SourcePath = .FileName & ""
                                clsFileXML1.Data_Username = txtDataFields_1.Text & ""
                                clsFileXML1.Data_Password = txtDataFields_2.Text & ""
                                clsFileXML1.Data_DatabasePassword = txtDataFields_3.Text
                                clsFileXML1.Database_Connection = .FileName
                                SelectSource(.FileName)
                            Case ".mdb"
                                If txtDataFields_3.Text = "Database Password" Or txtDataFields_3.Text = "" Then
                                    modADOConn.OLEDB_MSJet(.FileName, CStr(IIf(txtDataFields_1.Text = "Username", "", txtDataFields_1.Text)), CStr(IIf(txtDataFields_2.Text = "Password", "", txtDataFields_2.Text)))
                                Else
                                    modADOConn.OLEDB_MSJetDBPassword(.FileName, txtDataFields_3.Text, CStr(IIf(txtDataFields_1.Text = "Username", "", txtDataFields_1.Text)), CStr(IIf(txtDataFields_2.Text = "Password", "", txtDataFields_2.Text)))
                                End If
                                modSources.Database_Connection = modADOConn.Connection_String_VB
                                clsFileXML1.Data_SourcePath = .FileName & ""
                                clsFileXML1.Data_Username = txtDataFields_1.Text & ""
                                clsFileXML1.Data_Password = txtDataFields_2.Text & ""
                                clsFileXML1.Data_DatabasePassword = txtDataFields_3.Text
                                clsFileXML1.Database_Connection = modSources.Database_Connection & ""
                                txtDataFields_0.Visible = True
                                txtDataFields_1.Visible = True
                                txtDataFields_2.Visible = True
                                txtDataFields_3.Visible = True
                                SelectSource(.FileName)
                            Case ".xls"
                                modADOConn.OLEDB_MSJetExcell(.FileName, True)
                                modSources.Database_Connection = modADOConn.Connection_String_Net
                                clsFileXML1.Data_SourcePath = .FileName & ""
                                clsFileXML1.Data_Username = txtDataFields_1.Text & ""
                                clsFileXML1.Data_Password = txtDataFields_2.Text & ""
                                clsFileXML1.Data_DatabasePassword = txtDataFields_3.Text
                                clsFileXML1.Database_Connection = modSources.Database_Connection & ""
                                txtDataFields_0.Visible = True
                                txtDataFields_1.Visible = True
                                txtDataFields_2.Visible = True
                                txtDataFields_3.Visible = True
                                SelectSource(.FileName)
                        End Select
                    Catch ex As Exception
                        If frmMain1.debugMode Then Throw ex Else Err.Clear()
                    End Try
                    Try
                        lblStatus.Text = modSources.Database_Connection
                        If Test_Connection() Then
                            lblConnected.Text = "Test: Connected"
                            btnNext.Visible = True
                        Else
                            lblConnected.Text = "Test: Not Connected"
                            btnNext.Visible = False
                        End If
                    Catch ex As Exception
                        If frmMain1.debugMode Then Throw ex Else Err.Clear()
                    End Try
                    Try
                        If Not String.IsNullOrEmpty(clsFileXML1.PDF_Source & "") Then
                            If Load_PDF(SessionBytes, pdfOwnerPassword) Then
                                lnkAddNew.Enabled = True
                                lnkRemove.Enabled = True
                                cmbPDFFields.Enabled = True
                                If cmbPDFFields.Items.Count <= 0 Then
                                    cmbPDFFields.Items.Clear()
                                    lstMappedFields.Items.Clear()
                                    Populate_PDFFields_Combo(cmbPDFFields, lstMappedFields, Me)
                                End If
                            Else
                                lnkAddNew.Enabled = False
                                lnkRemove.Enabled = False
                                cmbPDFFields.Enabled = False
                                cmbPDFFields.Items.Clear()
                            End If
                        End If
                    Catch ex As Exception
                        If frmMain1.debugMode Then Throw ex Else Err.Clear()
                    End Try
                    Try
                        btnRecords_Affected_Click(sender, e)
                    Catch ex As Exception
                        If frmMain1.debugMode Then Throw ex Else Err.Clear()
                    End Try
                    Try
                        If PopulateTables() Then
                            cmbDBTables.Items.Clear()
                            cmbDBTablesFilter.Items.Clear()
                            Populate_TableCombo(cmbDBTables)
                            Populate_TableCombo(cmbDBTablesFilter)
                            If cmbDBTables.Items.Count = 1 Then cmbDBTables.SelectedIndex = 0
                            lnkLoadPDF_LinkClicked(Me, New System.Windows.Forms.LinkLabelLinkClickedEventArgs(lnkLoadPDF.Links(0)))
                        End If
                    Catch ex As Exception
                        If frmMain1.debugMode Then Throw ex Else Err.Clear()
                    End Try
                    Try
                        clsFileXML1.SetForm(Me)
                    Catch ex As Exception
                        If frmMain1.debugMode Then Throw ex Else Err.Clear()
                    End Try
                    UpdateMessage("Settings file loaded successfully!")
                Case Else
                    UpdateMessage("Error - File not loaded.")
                    Return
            End Select
        End With
    End Sub
    Public Sub UpdateMessage(ByVal strMessage As String, Optional ByVal prefix As String = "Status: ")
        lblMessage.Text = prefix & strMessage
        frmMain1.ToolStripStatusLabel2.Text = lblMessage.Text
    End Sub
    Private Sub TabPage8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub TabPage9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub OUTPUT_FORMAT_XML_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub OUTPUT_FORMAT_XDP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Public Function Merge() As DataSet
        Dim SQL As String
        Dim FldMap As FieldMap, xTable(3) As String
        SQL = "SELECT * "
        Dim xFilter As FilterField, Tmp_SQL As String
        For Each FldMap In FieldsMaps
            If Not FldMap.DBTable Is Nothing Then
                xTable(0) = FldMap.DBTable
                If Not xTable(1) = xTable(0) Then
                    If xTable(2) = "" Then
                        If Not xTable(0) Is Nothing Then
                            xTable(2) = xTable(0)
                            xTable(1) = xTable(0)
                        End If
                    Else
                        If Not xTable(0) Is Nothing Then
                            xTable(2) = xTable(2) & ", " & xTable(0)
                            xTable(1) = xTable(0)
                        End If
                    End If
                End If
            End If
        Next
        Tmp_SQL = ""
        If modSources.FilterFields.Count >= 1 Then
            For Each xFilter In modSources.FilterFields
                xTable(0) = xFilter.DBTable
                If InStr(xTable(2), xTable(0), CompareMethod.Text) < 0 Then
                    xTable(2) = xTable(2) & ", " & xTable(0)
                End If
                If Not xFilter.DBSql = "" Then
                    If xFilter.DBSql.Contains("LIKE") Then
                        If Tmp_SQL = "" Then
                            Tmp_SQL = Tmp_SQL & " " & xFilter.DBSql & " "
                        Else
                            Tmp_SQL = Tmp_SQL & " AND " & xFilter.DBSql & " "
                        End If
                    Else
                        If Tmp_SQL = "" Then
                            Tmp_SQL = Tmp_SQL & " " & xFilter.DBSql & " "
                        Else
                            Tmp_SQL = Tmp_SQL & " AND " & xFilter.DBSql & " "
                        End If
                    End If
                End If
            Next
        End If
        If Not Tmp_SQL = "" Then
            Tmp_SQL = " WHERE " & Tmp_SQL
            SQL = SQL & " FROM " & xTable(0)
            SQL = SQL & Tmp_SQL
        Else
            If xTable(0) = "" Then
                If cmbDBTablesFilter.Items.Count = 1 Then
                    xTable(0) = cmbDBTablesFilter.Items(0).ToString & ""
                End If
            End If
            SQL = SQL & " FROM " & xTable(0)
        End If
        SQL = SQL & ";"
        Dim da As New OleDb.OleDbDataAdapter(SQL, Database_Connection)
        Dim ds As New DataSet
        da.Fill(ds)
        Return ds
    End Function
    Public Function MergeXML() As DataSet
        Dim SQL As String
        Dim FldMap As FieldMap, xTable(3) As String
        SQL = "SELECT * "
        Dim xFilter As FilterField, Tmp_SQL As String
        For Each FldMap In FieldsMaps
            If Not FldMap.DBTable Is Nothing Then
                xTable(0) = FldMap.DBTable
                If Not xTable(1) = xTable(0) Then
                    If xTable(2) = "" Then
                        If Not xTable(0) Is Nothing Then
                            xTable(2) = xTable(0)
                            xTable(1) = xTable(0)
                        End If
                    Else
                        If Not xTable(0) Is Nothing Then
                            xTable(2) = xTable(2) & ", " & xTable(0)
                            xTable(1) = xTable(0)
                        End If
                    End If
                End If
            End If
        Next
        Tmp_SQL = ""
        If modSources.FilterFields.Count >= 1 Then
            For Each xFilter In modSources.FilterFields
                xTable(0) = xFilter.DBTable
                If InStr(xTable(2), xTable(0), CompareMethod.Text) < 0 Then
                    xTable(2) = xTable(2) & ", " & xTable(0)
                End If
                If Not xFilter.DBSql = "" Then
                    If xFilter.DBSql.Contains("LIKE") Then
                        If Tmp_SQL = "" Then
                            Tmp_SQL = Tmp_SQL & " " & xFilter.DBSql & " "
                        Else
                            Tmp_SQL = Tmp_SQL & " AND " & xFilter.DBSql & " "
                        End If
                    Else
                        If Tmp_SQL = "" Then
                            Tmp_SQL = Tmp_SQL & " " & xFilter.DBSql & " "
                        Else
                            Tmp_SQL = Tmp_SQL & " AND " & xFilter.DBSql & " "
                        End If
                    End If
                    Tmp_SQL = Tmp_SQL.Replace("[" & xTable(0) & "].", "")
                    Tmp_SQL = Tmp_SQL.Replace("[" & xTable(1) & "].", "")
                    Tmp_SQL = Tmp_SQL.Replace("[" & xTable(2) & "].", "")
                End If
            Next
        End If
        Dim ds As New DataSet
        ds.ReadXml(modSources._Databases.Connection)
        Dim dv As New DataView(ds.Tables(xTable(2)), Tmp_SQL, "", DataViewRowState.CurrentRows)
        Dim dsTbl As New DataSet
        dsTbl.Tables.Add(dv.ToTable())
        Return dsTbl
    End Function
    Public Function MergeJSON() As DataSet
        Dim SQL As String
        Dim FldMap As FieldMap, xTable(3) As String
        SQL = "SELECT * "
        Dim xFilter As FilterField, Tmp_SQL As String
        For Each FldMap In FieldsMaps
            If Not FldMap.DBTable Is Nothing Then
                xTable(0) = FldMap.DBTable
                If Not xTable(1) = xTable(0) Then
                    If xTable(2) = "" Then
                        If Not xTable(0) Is Nothing Then
                            xTable(2) = xTable(0)
                            xTable(1) = xTable(0)
                        End If
                    Else
                        If Not xTable(0) Is Nothing Then
                            xTable(2) = xTable(2) & ", " & xTable(0)
                            xTable(1) = xTable(0)
                        End If
                    End If
                End If
            End If
        Next
        Tmp_SQL = ""
        If modSources.FilterFields.Count >= 1 Then
            For Each xFilter In modSources.FilterFields
                xTable(0) = xFilter.DBTable
                If InStr(xTable(2), xTable(0), CompareMethod.Text) < 0 Then
                    xTable(2) = xTable(2) & ", " & xTable(0)
                End If
                If Not xFilter.DBSql = "" Then
                    If xFilter.DBSql.Contains("LIKE") Then
                        If Tmp_SQL = "" Then
                            Tmp_SQL = Tmp_SQL & " " & xFilter.DBSql & " "
                        Else
                            Tmp_SQL = Tmp_SQL & " AND " & xFilter.DBSql & " "
                        End If
                    Else
                        If Tmp_SQL = "" Then
                            Tmp_SQL = Tmp_SQL & " " & xFilter.DBSql & " "
                        Else
                            Tmp_SQL = Tmp_SQL & " AND " & xFilter.DBSql & " "
                        End If
                    End If
                    Tmp_SQL = Tmp_SQL.Replace("[" & xTable(0) & "].", "")
                    Tmp_SQL = Tmp_SQL.Replace("[" & xTable(1) & "].", "")
                    Tmp_SQL = Tmp_SQL.Replace("[" & xTable(2) & "].", "")
                End If
            Next
        End If
        Dim ds As New DataSet
        Dim xmlStream As New System.IO.MemoryStream
        Dim doc As System.Xml.XmlDocument = DirectCast(Newtonsoft.Json.JsonConvert.DeserializeXmlNode(System.IO.File.ReadAllText(modSources._Databases.Connection)), System.Xml.XmlDocument)
        ds.ReadXml(New System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(doc.OuterXml)), XmlReadMode.Auto)
        Dim dv As New DataView(ds.Tables(xTable(2)), Tmp_SQL, "", DataViewRowState.CurrentRows)
        Dim dsTbl As New DataSet
        dsTbl.Tables.Add(dv.ToTable())
        Return dsTbl
    End Function
    Public frmMain1 As frmMain = Nothing
    Public Sub ddlRecord_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim bytes() As Byte = Nothing
            If ddlRecord.SelectedIndex < 0 Then
                frmMain1.Session() = frmMain1.Session("savedSource")
                frmMain1.A0_LoadPDF()
            Else
                RefreshPDF(ddlRecord.SelectedIndex, True, True)
            End If
            Dim dl As Boolean = disableLoad
            disableLoad = True
            Dim selIndex As Integer = ddlRecord.SelectedIndex
            frmMain1.ddlRecord.Items.Clear()
            For r As Integer = 1 To dsx.Tables(0).Rows.Count
                frmMain1.ddlRecord.Items.Add(CInt(r).ToString() & " of " & dsx.Tables(0).Rows.Count.ToString & "")
            Next
            frmMain1.ddlRecord.SelectedIndex = ddlRecord.SelectedIndex
            frmMain1.dsBrowser.disableLoad = dl
            disableLoad = dl
            UpdateMessage("Selected Record #:" & ddlRecord.SelectedIndex & " @ " & DateTime.Now.ToString())
        Catch ex As Exception
            UpdateMessage("Error: " & ex.Message.ToString() & " @ " & DateTime.Now.ToString())
            If frmMain1.debugMode Then Throw ex Else Err.Clear()
        End Try
    End Sub
    Public Sub btnNav_Next_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If ddlRecord.SelectedIndex < dsx.Tables(0).Rows.Count Then
                ddlRecord.SelectedIndex += 1
            End If
        Catch ex As Exception
            If frmMain1.debugMode Then Throw ex Else Err.Clear()
        End Try
    End Sub
    Public Sub btnNav_Last_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            ddlRecord.SelectedIndex = ddlRecord.Items.Count - 1
        Catch ex As Exception
            If frmMain1.debugMode Then Throw ex Else Err.Clear()
        End Try
    End Sub
    Public Sub btnNav_Previous_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If ddlRecord.SelectedIndex > 0 Then
                ddlRecord.SelectedIndex -= 1
            End If
        Catch ex As Exception
            If frmMain1.debugMode Then Throw ex Else Err.Clear()
        End Try
    End Sub
    Public Sub btnNav_First_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If ddlRecord.Items.Count > 0 Then
                ddlRecord.SelectedIndex = 0
            Else
                ddlRecord.SelectedIndex = 0
            End If
        Catch ex As Exception
            If frmMain1.debugMode Then Throw ex Else Err.Clear()
        End Try
    End Sub
    Public Sub ddlRecord_SelectedIndexChanged(ByRef frmMain2 As frmMain, ByRef ddlRecord1 As ComboBox)
        Try
            Dim bytes() As Byte = Nothing
            frmMain1 = frmMain2
            If ddlRecord1.SelectedIndex < 0 Then
                frmMain1.Session() = frmMain1.Session("savedSource")
                frmMain1.A0_LoadPDF()
            Else
                RefreshPDF(ddlRecord1.SelectedIndex, True, True)
            End If
        Catch ex As Exception
            If frmMain1.debugMode Then Throw ex Else Err.Clear()
        End Try
    End Sub
    Public Sub btnNav_Next_Click(ByRef frmMain2 As frmMain, ByRef ddlRecord1 As ComboBox)
        Try
            If ddlRecord1.SelectedIndex < dsx.Tables(0).Rows.Count Then
                ddlRecord1.SelectedIndex += 1
            End If
        Catch ex As Exception
            If frmMain1.debugMode Then Throw ex Else Err.Clear()
        End Try
    End Sub
    Public Sub btnNav_New_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRecordNav_NewRecord.Click
        Try
            If dsx.Tables(0).Rows.Count >= 0 Then
                Dim dr As DataRow = dsx.Tables(0).NewRow
                dsx.Tables(0).Rows.Add(dr)
                Dim dl As Boolean = disableLoad
                disableLoad = True
                btnUpdateDatabase_Click(Me, New EventArgs())
                Dim selIndex As Integer = dsx.Tables(0).Rows.Count - 1
                ddlRecord.Items.Clear()
                frmMain1.ddlRecord.Items.Clear()
                For r As Integer = 1 To dsx.Tables(0).Rows.Count
                    ddlRecord.Items.Add(CInt(r).ToString() & " of " & dsx.Tables(0).Rows.Count.ToString & "")
                    frmMain1.ddlRecord.Items.Add(CInt(r).ToString() & " of " & dsx.Tables(0).Rows.Count.ToString & "")
                Next
                ddlRecord.SelectedIndex = selIndex
                frmMain1.ddlRecord.SelectedIndex = selIndex
                disableLoad = dl
                UpdateMessage("New Record: Click update when done... @ " & DateTime.Now.ToString())
            End If
        Catch ex As Exception
            UpdateMessage("Error: New Record - " & ex.Message & " @ " & DateTime.Now.ToString())
            If frmMain1.debugMode Then Throw ex Else Err.Clear()
        End Try
    End Sub
    Public Sub btnNav_Del_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRecordNav_DelRecord.Click
        Try
            If dsx.Tables(0).Rows.Count > 0 And ddlRecord.SelectedIndex >= 0 Then
                Dim selIndex As Integer = ddlRecord.SelectedIndex - 1
                If ddlRecord.SelectedIndex >= 0 Then
                    dsx.Tables(0).Rows.RemoveAt(ddlRecord.SelectedIndex)
                End If
                Dim dl As Boolean = disableLoad
                disableLoad = True
                btnUpdateDatabase_Click(Me, New EventArgs())
                ddlRecord.Items.Clear()
                frmMain1.ddlRecord.Items.Clear()
                For r As Integer = 1 To dsx.Tables(0).Rows.Count
                    ddlRecord.Items.Add(CInt(r).ToString() & " of " & dsx.Tables(0).Rows.Count.ToString & "")
                    frmMain1.ddlRecord.Items.Add(CInt(r).ToString() & " of " & dsx.Tables(0).Rows.Count.ToString & "")
                Next
                If ddlRecord.Items.Count > selIndex Then
                    frmMain1.ddlRecord.SelectedIndex = selIndex
                    ddlRecord.SelectedIndex = selIndex
                    ddlRecord_SelectedIndexChanged(frmMain1, ddlRecord)
                ElseIf ddlRecord.Items.Count > 0 Then
                    ddlRecord.SelectedIndex = 0
                    frmMain1.ddlRecord.SelectedIndex = 0
                    ddlRecord_SelectedIndexChanged(frmMain1, ddlRecord)
                Else
                    ddlRecord.Enabled = False
                    frmMain1.ddlRecord.Enabled = False
                End If
                disableLoad = dl
                UpdateMessage("Deleteed Record: #" & CInt(selIndex + 1).ToString() & " @ " & DateTime.Now.ToString())
            End If
        Catch ex As Exception
            UpdateMessage("Error: Deleteed Record - " & ex.Message.ToString() & " @ " & DateTime.Now.ToString())
            If frmMain1.debugMode Then Throw ex Else Err.Clear()
        End Try
    End Sub
    Public Sub btnNav_Last_Click(ByRef frmMain2 As frmMain, ByRef ddlRecord1 As ComboBox)
        Try
            ddlRecord1.SelectedIndex = ddlRecord.Items.Count - 1
        Catch ex As Exception
            If frmMain1.debugMode Then Throw ex Else Err.Clear()
        End Try
    End Sub
    Public Sub btnNav_Previous_Click(ByRef frmMain2 As frmMain, ByRef ddlRecord1 As ComboBox)
        Try
            If ddlRecord1.SelectedIndex > 0 Then
                ddlRecord1.SelectedIndex -= 1
            End If
        Catch ex As Exception
            If frmMain1.debugMode Then Throw ex Else Err.Clear()
        End Try
    End Sub
    Public Sub btnNav_First_Click(ByRef frmMain2 As frmMain, ByRef ddlRecord1 As ComboBox)
        Try
            If ddlRecord1.Items.Count > 0 Then
                ddlRecord1.SelectedIndex = 0
            Else
                ddlRecord1.SelectedIndex = -1
            End If
        Catch ex As Exception
            If frmMain1.debugMode Then Throw ex Else Err.Clear()
        End Try
    End Sub
    Dim gbox_Parent As Control
    Dim gbox_Top As Integer = 0
    Dim gbox_Left As Integer = 0
    Dim meWidth As Integer = 0
    Dim meHeight As Integer = 0
    Public Sub addHandlers(ByRef btnRecordNav_First1 As Button, ByRef btnRecordNav_Previous1 As Button, ByRef btnRecordNav_Next1 As Button, ByRef btnRecordNav_Last1 As Button, ByRef ddlRecord1 As ComboBox)
        AddHandler btnRecordNav_First1.Click, AddressOf btnNav_First_Click
        AddHandler btnRecordNav_Previous1.Click, AddressOf btnNav_Previous_Click
        AddHandler btnRecordNav_Next1.Click, AddressOf btnNav_Next_Click
        AddHandler btnRecordNav_Last1.Click, AddressOf btnNav_Last_Click
        AddHandler ddlRecord1.SelectedIndexChanged, AddressOf ddlRecord_SelectedIndexChanged
    End Sub
    Public Sub removeHandlers(ByRef btnRecordNav_First1 As Button, ByRef btnRecordNav_Previous1 As Button, ByRef btnRecordNav_Next1 As Button, ByRef btnRecordNav_Last1 As Button, ByRef ddlRecord1 As ComboBox)
        RemoveHandler btnRecordNav_First1.Click, AddressOf btnNav_First_Click
        RemoveHandler btnRecordNav_Previous1.Click, AddressOf btnNav_Previous_Click
        RemoveHandler btnRecordNav_Next1.Click, AddressOf btnNav_Next_Click
        RemoveHandler btnRecordNav_Last1.Click, AddressOf btnNav_Last_Click
        RemoveHandler ddlRecord1.SelectedIndexChanged, AddressOf ddlRecord_SelectedIndexChanged
    End Sub
    Private Sub btnMinimize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMinimize.Click
        If btnMinimize.Text.ToLower = "Compact View".ToLower Then
            btnMinimize.Text = "Restore"
            gbox_Parent = GroupBoxNavigate.Parent
            gbox_Left = GroupBoxNavigate.Left
            gbox_Top = GroupBoxNavigate.Top
            meHeight = Me.Height
            meWidth = Me.Width
            GroupBoxNavigate.Parent = Me
            GroupBoxNavigate.Top = 2
            GroupBoxNavigate.Left = 2
            Me.Width = GroupBoxNavigate.Width + 23
            Me.Height = GroupBoxNavigate.Height + 35
            TabControl1.Visible = False
            TableLayoutPanel1.Visible = False
        ElseIf btnMinimize.Text.ToLower = "Restore".ToLower Then
            btnMinimize.Text = "Compact View"
            GroupBoxNavigate.Parent = gbox_Parent
            GroupBoxNavigate.Left = gbox_Left
            GroupBoxNavigate.Top = gbox_Top
            Me.Height = meHeight
            Me.Width = meWidth
            TabControl1.Visible = True
            TableLayoutPanel1.Visible = True
        End If
    End Sub
    Public Sub RefreshPDF(ByVal ddlRecord_SelectedIndex As Integer, Optional ByVal forceRefresh As Boolean = False, Optional ByVal loadPDF As Boolean = True)
        Dim strNames As String = ""
        RemoveHandler ddlRecord.SelectedIndexChanged, AddressOf ddlRecord_SelectedIndexChanged
        ddlRecord.SelectedIndex = ddlRecord_SelectedIndex
        AddHandler ddlRecord.SelectedIndexChanged, AddressOf ddlRecord_SelectedIndexChanged
        Dim autoIncrementField() As String = {}
        For Each pk As System.Data.DataColumn In dsx.Tables(0).Columns
            If pk.AutoIncrement Then
                ReDim autoIncrementField(autoIncrementField.Length)
                autoIncrementField(autoIncrementField.Length - 1) = pk.ColumnName
                Exit For
            End If
        Next
        cfdf = cpdf.PDFOpenFromBuf(frmMain1.Session("savedSource"))
        cfdf.XDPSetValuesFromDataRow(dsx.Tables(0).Rows(ddlRecord.SelectedIndex), autoIncrementField)
        frmMain1.Session("output") = cfdf.PDFMergeFDF2Buf(frmMain1.Session("savedSource"), False, frmMain1.pdfOwnerPassword)
        frmMain1.LoadPDFReaderDoc(frmMain1.pdfOwnerPassword, True)
        If loadPDF Then
            frmMain1.A0_LoadPDF()
        End If
    End Sub
    Public Sub btnUpdateDatabase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateDatabase.Click
        Try
            If dsx Is Nothing Then
                Return
            ElseIf dsx.Tables.Count > 0 Then
                If Database_Connection.ToString.ToLower.EndsWith(".xml") Then
                    cfdf = cpdf.PDFOpenFromBuf(frmMain1.Session, True, True, frmMain.pdfOwnerPassword)
                    Dim autoIncrementField As String = ""
                    For Each c As DataColumn In dsx.Tables(0).Columns
                        If c.AutoIncrement Then
                            autoIncrementField = c.ColumnName
                            Exit For
                        End If
                    Next
                    If Not String.IsNullOrEmpty(autoIncrementField) Then
                        cfdf.XDPSetDataRowFromValues(dsx.Tables(0).Rows(ddlRecord.SelectedIndex), autoIncrementField)
                    Else
                        cfdf.XDPSetDataRowFromValues(dsx.Tables(0).Rows(ddlRecord.SelectedIndex))
                    End If
                    dsx.WriteXml(Database_Connection.ToString, XmlWriteMode.WriteSchema)
                ElseIf Database_Connection.ToString.ToLower.EndsWith(".json") Then
                    cfdf = cpdf.PDFOpenFromBuf(frmMain1.Session, True, True, frmMain.pdfOwnerPassword)
                    Dim autoIncrementField As String = ""
                    For Each c As DataColumn In dsx.Tables(0).Columns
                        If c.AutoIncrement Then
                            autoIncrementField = c.ColumnName
                            Exit For
                        End If
                    Next
                    If Not String.IsNullOrEmpty(autoIncrementField) Then
                        cfdf.XDPSetDataRowFromValues(dsx.Tables(0).Rows(ddlRecord.SelectedIndex), autoIncrementField)
                    Else
                        cfdf.XDPSetDataRowFromValues(dsx.Tables(0).Rows(ddlRecord.SelectedIndex))
                    End If
                    Dim xmlStream As New System.IO.MemoryStream
                    dsx.WriteXml(xmlStream, XmlWriteMode.IgnoreSchema)
                    Dim doc As New System.Xml.XmlDocument
                    doc.LoadXml(System.Text.Encoding.UTF8.GetString(xmlStream.ToArray()))
                    System.IO.File.WriteAllText(Database_Connection.ToString, Newtonsoft.Json.JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.None))
                Else
                    cfdf = cpdf.PDFOpenFromBuf(frmMain1.Session, True, True, frmMain.pdfOwnerPassword)
                    Dim autoIncrementField As String = ""
                    For Each c As DataColumn In dsx.Tables(0).Columns
                        If c.AutoIncrement Then
                            autoIncrementField = c.ColumnName
                            Exit For
                        End If
                    Next
                    If Not String.IsNullOrEmpty(autoIncrementField) Then
                        cfdf.FDFSetDataRowFromValues(dsx.Tables(0).Rows(ddlRecord.SelectedIndex), autoIncrementField)
                    Else
                        cfdf.FDFSetDataRowFromValues(dsx.Tables(0).Rows(ddlRecord.SelectedIndex))
                    End If
                    UpdateDataSource(frmMain1, ddlRecord.SelectedIndex)
                End If
            End If
            UpdateMessage("Updated Record #:" & CInt(ddlRecord.SelectedIndex + 1).ToString() & " @ " & DateTime.Now.ToString())
        Catch ex As Exception
            UpdateMessage(ex.Message, "Error: ")
            If frmMain1.debugMode Then Throw ex Else Err.Clear()
        End Try
    End Sub
    Public Sub New()
        InitializeComponent()
    End Sub
    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        OK_Button_Click(Me, New EventArgs())
    End Sub
    Private Sub dialogDataSource_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged
    End Sub
    Private Sub btnRecordNav_Next_Click(sender As Object, e As EventArgs) Handles btnRecordNav_Next.Click
    End Sub
End Class
