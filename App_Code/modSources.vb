Imports Newtonsoft.Json
Imports System.Data
Imports System.Data.OleDb
Public Module modSources
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 Nicholas Kowalewicz All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>

    Structure Database
        Dim Connection As String
    End Structure
    Structure PDF
        Dim Source As String
    End Structure
    Structure Table
        Dim Name As String
    End Structure
    Structure DBField
        Dim Name As String
        Dim TableName As String
        Dim FieldType As Integer
    End Structure
    Structure PDFField
        Dim Name As String
    End Structure
    Structure FieldMap
        Dim PDFField As String
        Dim DBFields As String
        Dim DBTable As String
        Dim DBMapRaw As String
    End Structure
    Structure FilterField
        Dim DBField As String
        Dim DBValue As String
        Dim DBTable As String
        Dim DBSql As String
        Dim FieldType As Integer
    End Structure
    Private _MappedFields As New DataSet
    Public _Databases As Database
    Private _PDFs As PDF
    Public Tables As New System.Collections.Generic.List(Of Table)
    Public DBFields As New System.Collections.Generic.List(Of DBField)
    Public PDFFields As New System.Collections.Generic.List(Of PDFField)
    Public FieldsMaps As New System.Collections.Generic.List(Of FieldMap)
    Public FilterFields As New System.Collections.Generic.List(Of FilterField)
    Private cFDFDoc As New FDFApp.FDFDoc_Class
    Public frmMerge1 As frmMerge
    Public Property myFDFDoc() As FDFApp.FDFDoc_Class
        Get
            Return cFDFDoc
        End Get
        Set(ByVal value As FDFApp.FDFDoc_Class)
            cFDFDoc = value
        End Set
    End Property
    Public Property FieldMapping(ByVal DBField As String) As FieldMap
        Get
            For Each fld As FieldMap In FieldsMaps
                If fld.DBFields = DBField Then
                    Return fld
                End If
            Next
            Return Nothing
        End Get
        Set(ByVal Value As FieldMap)
            For Each fld As FieldMap In FieldsMaps
                If fld.DBFields = DBField Then
                    fld = Value
                End If
            Next
        End Set
    End Property
    Public Property FieldMapping(ByVal idx As Integer) As FieldMap
        Get
            Return FieldsMaps(idx)
        End Get
        Set(ByVal Value As FieldMap)
            FieldsMaps(idx) = Value
        End Set
    End Property
    Public Property DBRawFieldMapping(ByVal idx As Integer) As String
        Get
            Return FieldsMaps(idx).DBMapRaw
        End Get
        Set(ByVal Value As String)
            Dim fld As FieldMap = FieldMapping(idx)
            fld.DBMapRaw = Value & ""
            FieldsMaps(idx) = fld
        End Set
    End Property
    Private Function New_Dataset() As DataSet
        Dim xTable As DataTable
        xTable = New DataTable
        xTable.Columns.Add(New DataColumn("DBTable", GetType(String)))
        xTable.Columns.Add(New DataColumn("DBField", GetType(String)))
        xTable.Columns.Add(New DataColumn("PDFField", GetType(String)))
        xTable.Columns.Add(New DataColumn("DBMapRaw", GetType(String)))
        _MappedFields = New DataSet
        xTable.DataSet.WriteXml(Application.StartupPath & "\fields.xml", XmlWriteMode.WriteSchema)
        _MappedFields.Clear()
        _MappedFields.ReadXml(Application.StartupPath & "\fields.xml", XmlReadMode.ReadSchema)
        Return _MappedFields
    End Function
    Public Property DBFieldName(ByVal intRecNum As Integer) As String
        Get
            Return modSources.DBFields(intRecNum).Name & ""
        End Get
        Set(ByVal Value As String)
            Dim fld As DBField = modSources.DBFields(intRecNum)
            fld.Name = Value
            modSources.DBFields(intRecNum) = fld
        End Set
    End Property
    Public Property cDBFieldType(ByVal intRecNum As Integer) As Integer
        Get
            Return modSources.DBFields(intRecNum).FieldType
        End Get
        Set(ByVal Value As Integer)
            Dim fld As DBField = modSources.DBFields(intRecNum)
            fld.FieldType = Value
            modSources.DBFields(intRecNum) = fld
        End Set
    End Property
    Public Property FieldFilters() As FilterField()
        Get
            Return modSources.FilterFields.ToArray
        End Get
        Set(ByVal Value As FilterField())
            modSources.FilterFields.Clear()
            For Each fld As FilterField In Value
                modSources.FilterFields.Add(fld)
            Next
        End Set
    End Property
    Public Property xFilterFieldSQL(ByVal intRecNum As Integer) As String
        Get
            Return modSources.FilterFields(intRecNum).DBSql
        End Get
        Set(ByVal Value As String)
            Dim fld As FilterField = modSources.FilterFields(intRecNum)
            fld.DBSql = Value
            modSources.FilterFields(intRecNum) = fld
        End Set
    End Property
    Public Function UpdateDataSource(ByRef frm As frmMain, Optional ByVal rowIndex As Integer = -1) As Boolean
        Try
            If Database_Connection.ToString().ToLower().EndsWith(".xml") Then
                Dim cFDF As FDFApp.FDFDoc_Class = frm.cFDFApp.PDFOpenFromBuf(frm.Session.ToArray(), True, True, CStr(frm.pdfOwnerPassword & ""))
                Dim keyName As String = ""
                If frm.dsBrowser.dsx.Tables(0).PrimaryKey.Count = 1 Then
                    If frm.dsBrowser.dsx.Tables(0).PrimaryKey(0).AutoIncrement Then
                        keyName = frm.dsBrowser.dsx.Tables(0).PrimaryKey(0).ColumnName & ""
                    End If
                End If
                cFDF.FDFSetDataRowFromValues(frm.dsBrowser.dsx.Tables(0).Rows(rowIndex), keyName)
                frm.dsBrowser.dsx.WriteXml(Database_Connection, XmlWriteMode.WriteSchema)
                frm.TimeStampAdd("UpdateDataSource(" + Database_Connection & ")")
                Return True
            ElseIf Database_Connection.ToString().ToLower().EndsWith(".json") Then
                Dim cFDF As FDFApp.FDFDoc_Class = frm.cFDFApp.PDFOpenFromBuf(frm.Session.ToArray(), True, True, CStr(frm.pdfOwnerPassword & ""))
                Dim keyName As String = ""
                If frm.dsBrowser.dsx.Tables(0).PrimaryKey.Count = 1 Then
                    If frm.dsBrowser.dsx.Tables(0).PrimaryKey(0).AutoIncrement Then
                        keyName = frm.dsBrowser.dsx.Tables(0).PrimaryKey(0).ColumnName & ""
                    End If
                End If
                Try
                    cFDF.FDFSetDataRowFromValues(frm.dsBrowser.dsx.Tables(0).Rows(rowIndex), keyName)
                    Dim xmlStream As New System.IO.MemoryStream
                    frm.dsBrowser.dsx.WriteXml(xmlStream, XmlWriteMode.IgnoreSchema)
                    Dim doc As New System.Xml.XmlDocument

                    doc.LoadXml(System.Text.Encoding.UTF8.GetString(xmlStream.ToArray()))
                    System.IO.File.WriteAllText(Database_Connection, Newtonsoft.Json.JsonConvert.SerializeXmlNode(doc), System.Text.Encoding.UTF8)
                Catch ex As Exception
                    frm.TimeStampAdd("UpdateDataSource(" + Database_Connection & ")")
                    Err.Clear()
                End Try
                Return True
            Else
                If Not rowIndex = Nothing Then
                    If rowIndex < 0 Then
                        Return False
                    End If
                ElseIf Not rowIndex = 0 Then
                    Return False
                End If
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
                    SQL = SQL & " FROM " & xTable(2)
                    SQL = SQL & Tmp_SQL
                Else
                    SQL = SQL & " FROM " & xTable(2)
                End If
                SQL = SQL & ";"
                Dim da As New OleDb.OleDbDataAdapter(SQL, Database_Connection)
                Dim oledbb As New OleDb.OleDbCommandBuilder(da)
                Dim ds As New DataSet
                da.Fill(ds)
                Dim cFDF As FDFApp.FDFDoc_Class = frm.cFDFApp.PDFOpenFromBuf(frm.Session.ToArray(), True, True, CStr(frm.pdfOwnerPassword & ""))
                Dim keyName As String = ""
                If ds.Tables(0).PrimaryKey.Count = 1 Then
                    If ds.Tables(0).PrimaryKey(0).AutoIncrement Then
                        keyName = ds.Tables(0).PrimaryKey(0).ColumnName & ""
                    End If
                End If
                cFDF.FDFSetDataRowFromValues(ds.Tables(0).Rows(rowIndex), keyName)
                da.Update(ds, ds.Tables(0).TableName.ToString)
                da.Dispose()
                ds.Dispose()
                Return True
            End If
        Catch ex As Exception
            frm.TimeStampAdd(ex, frm.debugMode)
            Err.Clear()
        End Try
        Return False

    End Function
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
            SQL = SQL & " FROM " & xTable(2)
            SQL = SQL & Tmp_SQL
        Else
            SQL = SQL & " FROM " & xTable(2)
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
                            If Not xTable(2) = "root" Then
                                xTable(2) = xTable(2) & ", " & xTable(0)
                                xTable(1) = xTable(0)
                            Else
                                xTable(2) = xTable(0)
                                xTable(1) = xTable(0)
                            End If
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
        ds.ReadXml(_Databases.Connection)
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
                            If Not xTable(2) = "root" Then
                                xTable(2) = xTable(2) & ", " & xTable(0)
                                xTable(1) = xTable(0)
                            Else
                                xTable(2) = xTable(0)
                                xTable(1) = xTable(0)
                            End If
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
        Try
            Dim doc As System.Xml.XmlDocument = JsonConvert.DeserializeXmlNode(System.IO.File.ReadAllText(_Databases.Connection))






            Dim cfdf As New FDFApp.FDFDoc_Class

            Dim strXML As String = doc.OuterXml
            If Not cfdf.Determine_Type(System.Text.Encoding.UTF8.GetBytes(strXML)) = FDFApp.FDFDoc_Class.FDFType.XML Then
                strXML = "<?xml version=""1.0"" standalone=""yes""?>" & Environment.NewLine & strXML
            End If

            Dim xmlSR As New System.IO.StringReader(strXML)
            'ds.ReadXml(cfdf.FDFSavetoStream(FDFApp.FDFDoc_Class.FDFType.XML, True), XmlReadMode.IgnoreSchema)
            ds.ReadXml(xmlSR, XmlReadMode.Auto)
            Dim dv As New DataView(ds.Tables(xTable(2)), Tmp_SQL, "", DataViewRowState.CurrentRows)
            Dim dsTbl As New DataSet
            dsTbl.Tables.Add(dv.ToTable())
            Return dsTbl
        Catch ex As Exception
            Err.Clear()
        End Try

    End Function
    Public Property xPDFField(ByVal intRecNum As Integer) As String
        Get
            Return modSources.PDFFields(intRecNum).Name
        End Get
        Set(ByVal Value As String)
            Dim fld As PDFField = modSources.PDFFields(intRecNum)
            fld.Name = Value
            modSources.PDFFields(intRecNum) = fld
        End Set
    End Property
    Public Function AddMap(ByVal PDF_Field As String, ByVal DB_Fields As String, ByVal DB_Table As String, ByVal Map_Raw As String) As Boolean
        Try
            Dim FieldMapTemp As New FieldMap()
            FieldMapTemp.PDFField = PDF_Field
            FieldMapTemp.DBFields = DB_Fields
            FieldMapTemp.DBTable = DB_Table
            FieldMapTemp.DBMapRaw = Map_Raw
            FieldsMaps.Add(FieldMapTemp)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function RemoveMap(ByVal PDF_Field As String) As Boolean
        Try
            For Each FieldMapTemp As FieldMap In FieldsMaps
                If PDF_Field = FieldMapTemp.PDFField Then
                    FieldsMaps.Remove(FieldMapTemp)
                    Return True
                End If
            Next
            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function RemoveFilter(ByVal Index As Integer) As Boolean
        Try
            modSources.FilterFields.Remove(modSources.FilterFields(Index))
            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function RemoveFilter(ByVal SelectedItem As String) As Boolean
        Try
            Dim idx As Integer = 0
            For Each iFilter As FilterField In modSources.FilterFields
                If SelectedItem.ToString.ToLower = iFilter.DBSql.ToLower Then
                    modSources.FilterFields.Remove(modSources.FilterFields(idx))
                    Return True
                End If
                idx = idx + 1
            Next
            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function Test_Connection() As Boolean
        If _Databases.Connection.ToString.EndsWith(".xml") Then
            Dim ds As New DataSet
            ds.ReadXml(_Databases.Connection)
            If Not ds Is Nothing Then
                ds.Dispose()
                ds = Nothing
                Return True
            End If
        ElseIf _Databases.Connection.ToString.EndsWith(".json") Then
            Dim ds As New DataSet
            Dim doc As System.Xml.XmlDocument = DirectCast(Newtonsoft.Json.JsonConvert.DeserializeXmlNode(System.IO.File.ReadAllText(_Databases.Connection)), System.Xml.XmlDocument)
            If Not doc Is Nothing Then
                doc = Nothing
                Return True
            End If
        End If
        Dim xConn As New OleDb.OleDbConnection(_Databases.Connection)
        On Error Resume Next
        xConn.Open()
        If xConn.State = ConnectionState.Open Then
            xConn.Close()
            Return True
        Else
            xConn.Close()
            Return False
        End If
    End Function
    Public Property Database_Connection() As String
        Get
            Return _Databases.Connection
        End Get
        Set(ByVal Value As String)
            _Databases.Connection = Value
        End Set
    End Property
    Public Property PDF_Source() As String
        Get
            Return _PDFs.Source
        End Get
        Set(ByVal Value As String)
            _PDFs.Source = Value
        End Set
    End Property
    Public Sub Add_Table(ByVal TableName As String)
        Dim _Tbl As New modSources.Table
        _Tbl.Name = TableName
        Tables.Add(_Tbl)
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
                    If Comparison.ToLower = "Like" Then
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
    Public Sub Add_DBField(ByVal FieldName As String, ByVal TableName As String, ByVal FieldType As Integer)
        If Not FieldName = "" Then
            Dim _DBFld As New DBField()
            _DBFld.Name = FieldName
            _DBFld.TableName = TableName
            _DBFld.FieldType = FieldType
            modSources.DBFields.Add(_DBFld)
        End If
    End Sub
    Public Sub Add_PDFField(ByVal FieldName As String)
        If Not FieldName = "" Then
            Dim _PDFFld As New PDFField
            _PDFFld.Name = FieldName
            modSources.PDFFields.Add(_PDFFld)
        End If
    End Sub
    Public Function PopulateTables() As Boolean
        Dim Catx As New ADOX.Catalog
        Dim Connx As New ADODB.Connection
        Dim sConnString As String = _Databases.Connection
        If Not Test_Connection() Then Exit Function
        Tables.Clear()
        If _Databases.Connection.ToString.EndsWith(".xml") Then
            Dim ds As New DataSet
            ds.ReadXml(_Databases.Connection)
            If Not ds Is Nothing Then
                For Each tbl As DataTable In ds.Tables
                    If tbl.Columns.Count > 0 Then
                        modSources.Add_Table(tbl.TableName)
                    End If
                Next
                ds.Dispose()
                ds = Nothing
                PopulateTables = True
                Return True
            End If
        ElseIf _Databases.Connection.ToString.EndsWith(".json") Then
            Dim ds As New DataSet
            Dim doc As System.Xml.XmlDocument = DirectCast(Newtonsoft.Json.JsonConvert.DeserializeXmlNode(System.IO.File.ReadAllText(_Databases.Connection)), System.Xml.XmlDocument)
            ds.ReadXml(New System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(doc.OuterXml)), XmlReadMode.Auto)
            If Not ds Is Nothing Then
                For Each tbl As DataTable In ds.Tables
                    If tbl.Columns.Count > 0 Then
                        modSources.Add_Table(tbl.TableName)
                    End If
                Next
                ds.Dispose()
                ds = Nothing
                PopulateTables = True
                Return True
            End If
        Else
            Connx.Open(sConnString)
            Catx.let_ActiveConnection(sConnString)
            Dim Tablex As ADOX.Table
            For Each Tablex In Catx.Tables
                If Tablex.Type = "TABLE" Then
                    modSources.Add_Table(Tablex.Name)
                End If
            Next Tablex
        End If
        PopulateTables = True
        Return True
    End Function
    Public Function Populate_TableCombo(ByVal cTables As ComboBox, ByRef frm As frmMerge) As Boolean
        Dim xTables As Table
        cTables.Items.Clear()
        For Each xTables In Tables.ToArray
            If Not xTables.Name Is Nothing Then
                cTables.Items.Add(xTables.Name & "")
            End If
        Next
        If cTables.Items.Count > 0 Then
            If frm.cmbDBTables.Items.Count = cTables.Items.Count Then
                If frm.cmbDBTables.SelectedIndex >= 0 Then
                    cTables.SelectedIndex = frm.cmbDBTables.SelectedIndex
                Else
                    cTables.SelectedIndex = 0
                End If
            Else
                cTables.SelectedIndex = frm.cmbDBTables.SelectedIndex
            End If
        End If
        Populate_TableCombo = True
    End Function
    Public Function Populate_TableCombo(ByVal cTables As ComboBox, ByRef frm As dialogDataSource) As Boolean
        Dim xTables As Table
        cTables.Items.Clear()
        For Each xTables In Tables.ToArray
            If Not xTables.Name Is Nothing Then
                cTables.Items.Add(xTables.Name & "")
            End If
        Next
        If cTables.Items.Count > 0 Then
            If frm.cmbDBTables.Items.Count = cTables.Items.Count Then
                If frm.cmbDBTables.SelectedIndex >= 0 Then
                    cTables.SelectedIndex = frm.cmbDBTables.SelectedIndex
                Else
                    cTables.SelectedIndex = 0
                End If
            Else
                cTables.SelectedIndex = frm.cmbDBTables.SelectedIndex
            End If
        End If
        Populate_TableCombo = True
    End Function
    Public Sub Populate_DBFieldsList(ByVal cFields As ListBox, ByVal sTableName As String)
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
    Public Sub Populate_PDFFields_Combo(ByRef cFields As ComboBox, ByRef lstMappedFields As ListBox, ByRef frm As frmMerge)
        Dim cField As PDFField
        cFields.Items.Clear()
        lstMappedFields.Items.Clear()
        If modSources.PDFFields.Count <= 0 Then Exit Sub
        Dim cDBFLD As DBField
        lstMappedFields.Items.Clear()
        For Each cField In modSources.PDFFields
            If Not cField.Name Is Nothing Then
                Try
                    cFields.Items.Add(cField.Name & "")
                    For Each cDBFLD In modSources.DBFields
                        If cDBFLD.Name.ToLower = cField.Name.ToLower Then
                            If Not lstMappedFields.Items.Contains(cField.Name & "=" & "{" & cDBFLD.Name & "}") Then
                                If Not frm Is Nothing Then
                                    AddMap(cField.Name, cDBFLD.Name, CStr(IIf(cDBFLD.TableName.ToLower = "table", frm.cmbDBTables.Items(frm.cmbDBTables.SelectedIndex), cDBFLD.TableName)), cField.Name & "=" & "{" & cDBFLD.Name & "}")
                                End If
                                lstMappedFields.Items.Add(cField.Name & "=" & "{" & cDBFLD.Name & "}")
                                Exit For
                            End If
                        End If
                    Next
                Catch ex As Exception
                    Err.Clear()
                End Try
            End If
        Next
    End Sub
    Public Sub Populate_PDFFields_Combo(ByRef cFields As ComboBox, ByRef lstMappedFields As ListBox, ByRef frm As dialogDataSource)
        Dim cField As PDFField
        cFields.Items.Clear()
        lstMappedFields.Items.Clear()
        If modSources.PDFFields.Count <= 0 Then Exit Sub
        Dim cDBFLD As DBField
        lstMappedFields.Items.Clear()
        For Each cField In modSources.PDFFields
            If Not cField.Name Is Nothing Then
                Try
                    cFields.Items.Add(cField.Name & "")
                    For Each cDBFLD In modSources.DBFields
                        If Not lstMappedFields.Items.Contains(cField.Name & "=" & "{" & cDBFLD.Name & "}") Then
                            If cDBFLD.Name.ToLower = cField.Name.ToLower Then
                                If Not frm Is Nothing Then
                                    AddMap(cField.Name, cDBFLD.Name, CStr(IIf(cDBFLD.TableName.ToLower = "table", frm.cmbDBTables.Items(frm.cmbDBTables.SelectedIndex), cDBFLD.TableName)), cField.Name & "=" & "{" & cDBFLD.Name & "}")
                                End If
                                lstMappedFields.Items.Add(cField.Name & "=" & "{" & cDBFLD.Name & "}")
                                Exit For
                            End If
                        End If
                    Next
                Catch ex As Exception
                    Err.Clear()
                End Try
            End If
        Next
    End Sub
    Public Function DelTables() As Boolean
        Tables.Clear()
    End Function
    Public Function Del_DBFields() As Boolean
        modSources.DBFields.Clear()
    End Function
    Public Function Del_PDFField() As Boolean
        modSources.PDFFields.Clear()
    End Function
    Public Function DelFieldsMapss() As Boolean
        FieldsMaps.Clear()
    End Function
    Public Function PopulateFields(ByVal sTableName As String) As Boolean
        Dim sConnString As String = _Databases.Connection
        Del_DBFields()
        Dim cn As System.Data.OleDb.OleDbConnection = Nothing
        Dim _da As System.Data.OleDb.OleDbDataAdapter
        Dim strTbl As String
        Dim strCol As String
        Dim _ds As DataSet = New DataSet()
        If _Databases.Connection.ToString.ToLower.EndsWith(".xml") Then
            _ds.ReadXml(_Databases.Connection, XmlReadMode.Auto)
        ElseIf _Databases.Connection.ToString.ToLower.EndsWith(".json") Then
            Dim doc As System.Xml.XmlDocument = DirectCast(Newtonsoft.Json.JsonConvert.DeserializeXmlNode(System.IO.File.ReadAllText(_Databases.Connection)), System.Xml.XmlDocument)
            _ds.ReadXml(New System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(doc.OuterXml)), XmlReadMode.Auto)
        Else
            cn = New System.Data.OleDb.OleDbConnection(_Databases.Connection)
            With cn
                .Open()
            End With
            Dim selectCommand As New System.Data.OleDb.OleDbCommand("Select * FROM [" & CStr(IIf(sTableName.Contains("$"), sTableName, sTableName & "")) & "];")
            selectCommand.Connection = cn
            _da = New System.Data.OleDb.OleDbDataAdapter(selectCommand)
            _da.Fill(_ds, CStr(IIf(sTableName.Contains("$"), sTableName, sTableName & "")))
        End If
        For Each _tbl As System.Data.DataTable In _ds.Tables
            strTbl = _tbl.TableName
            If sTableName = strTbl Then
                For Each _col As System.Data.DataColumn In _tbl.Columns
                    strCol = _col.ColumnName & ""
                    modSources.Add_DBField(_col.ColumnName, _tbl.TableName, ADODB.DataTypeEnum.adVarWChar)
                Next
                If Not _Databases.Connection.ToString.ToLower.EndsWith(".xml") And Not _Databases.Connection.ToString.ToLower.EndsWith(".json") Then
                    If Not cn.State = ConnectionState.Closed Then
                        Try
                            cn.Close()
                        Catch ex4 As Exception
                            Err.Clear()
                        End Try
                    End If
                End If
                Exit For
            End If
        Next
        Try
            If Not _Databases.Connection.ToString.ToLower.EndsWith(".xml") And Not _Databases.Connection.ToString.ToLower.EndsWith(".json") Then
                cn.Close()
            End If
        Catch ex As Exception
        End Try
        Exit Function
errorhandler:
        PopulateFields = False
        Exit Function
    End Function
    Function Load_PDF() As Boolean
        Dim OpenFile As New OpenFileDialog
        Dim FName As String
        OpenFile.Filter = "PDF|*.PDF"
        OpenFile.DefaultExt = "PDF"
        OpenFile.FileName = ""
        OpenFile.Title = "Load Adobe Acrobat PDF"
        Dim dlg As DialogResult = OpenFile.ShowDialog()
        If Not dlg = DialogResult.OK Or dlg = DialogResult.Yes Then
            Return False
        End If
        FName = OpenFile.FileName
        If FName = "" Then
            Load_PDF = False
            Exit Function
        End If
        PDF_Source = FName
        Dim fds() As FDFApp.FDFDoc_Class.FDFField
        Dim FLD As FDFApp.FDFDoc_Class.FDFField
        Dim formApp As New FDFApp.FDFApp_Class
        myFDFDoc = New FDFApp.FDFDoc_Class
        myFDFDoc = formApp.PDFOpenFromFile(FName, True, True, "")
        fds = myFDFDoc.FDFFields
        For Each FLD In fds
            Add_PDFField(FLD.FieldName.ToString & "")
        Next
        Load_PDF = True
        myFDFDoc.FDFClose()
        myFDFDoc.Dispose()
        formApp.Dispose()
        Exit Function
    End Function
    Function Load_PDF(ByVal session() As Byte, ByVal pdfownerPw As String) As Boolean
        Dim FName As String
        FName = Application.StartupPath.ToString.TrimEnd("\"c) & "\temp\pdfMerge-Temp.pdf"
        If Not session Is Nothing Then
            If session.Length > 0 Then
                System.IO.File.WriteAllBytes(FName, session)
            Else
                Return False
            End If
        Else
            Return False
        End If
        PDF_Source = FName
        Dim fds() As FDFApp.FDFDoc_Class.FDFField
        Dim FLD As FDFApp.FDFDoc_Class.FDFField
        Dim formApp As New FDFApp.FDFApp_Class
        myFDFDoc = New FDFApp.FDFDoc_Class
        myFDFDoc = formApp.PDFOpenFromBuf(session, True, True, pdfownerPw & "")
        fds = myFDFDoc.FDFFields
        For Each FLD In fds
            Add_PDFField(FLD.FieldName.ToString & "")
        Next
        Load_PDF = True
        myFDFDoc.FDFClose()
        myFDFDoc.Dispose()
        formApp.Dispose()
        Exit Function
    End Function
    Function Load_PDF(ByVal FName As String) As Boolean
        Try
            PDF_Source = FName
            Dim fds() As FDFApp.FDFDoc_Class.FDFField
            Dim FLD As FDFApp.FDFDoc_Class.FDFField
            Dim formApp As New FDFApp.FDFApp_Class
            myFDFDoc = New FDFApp.FDFDoc_Class
            myFDFDoc = formApp.PDFOpenFromFile(FName, True, True, "")
            fds = myFDFDoc.FDFFields
            For Each FLD In fds
                Add_PDFField(FLD.FieldName.ToString & "")
            Next
            Load_PDF = True
            myFDFDoc.FDFClose()
            myFDFDoc.Dispose()
            formApp.Dispose()
            Return True
        Catch ex As Exception
            Err.Clear()
            Return False
        End Try
    End Function
End Module
