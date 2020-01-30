Imports System.IO
Imports System.Text.Encoding
Imports System.Data
Imports System.ComponentModel
Partial Public Class frmScanPDFs
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 NK-INC.COM All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>
    Private xmlDataSourceFilename As String = "scanPDFs.xml"
    'Private AccessDataSourceFilename As String = "scanPDFs.mdb"
    Public maxFileSizeMB As Integer = 100
    Private blnStop As Boolean = False
    Public frmMain1 As frmMain = Nothing
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        FolderBrowserDialog1.SelectedPath = Application.StartupPath
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Me.TextBox1.Text = FolderBrowserDialog1.SelectedPath.ToString.TrimEnd("\".ToCharArray()) & "\"
        End If
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            ProgressBar1.Value = 0
            blnStop = False
            If Directory.Exists(Me.TextBox1.Text) Then
                ReadAllSubdirectories(Me.TextBox1.Text)
                'For Each f As String In Directory.GetFiles(Me.TextBox1.Text)
                'If f.ToLower.EndsWith(".pdf") Then
                'File.WriteAllText(f & ".txt", ReadPdfFile(f))
                'End If
                'Next
            End If
            ProgressBar1.Value = 100
            lblStatus.Text = "Status: " & "Loading Database Complete."
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public Sub IncrementStatus(Optional ByVal increment As Integer = 1)
        Try
            If ProgressBar1.Value < 100 Then
                ProgressBar1.Value += increment
            Else
                ProgressBar1.Value = 0
                ProgressBar1.Value += increment
            End If
        Catch ex As Exception
            ProgressBar1.Value = 0
            ProgressBar1.Value += increment
            Err.Clear()
        End Try
        Application.DoEvents()
    End Sub
    Public Sub ReadAllSubdirectories(ByVal sDirectory As String)
        Try
            If blnStop = True Then Return
            If Directory.Exists(sDirectory) Then
                ReadAllFiles(sDirectory)
                If chkSubfolders.Checked Then
                    For Each d As String In Directory.GetDirectories(sDirectory)
                        If blnStop = True Then Return
                        ReadAllSubdirectories(d)
                    Next
                End If
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public ReadOnly Property StartupPath() As String
        Get
            Return Application.StartupPath.ToString.TrimEnd("\".ToCharArray()) & "\"
        End Get
    End Property
    Public Sub ReadAllFiles(ByVal sDirectory As String)
        Try
            If blnStop = True Then Return
            If Directory.Exists(sDirectory) Then
                Dim fs() As String = Directory.GetFiles(sDirectory), intF As Integer = 0
                For Each f As String In fs.ToArray
                    Try
                        intF += 1
                        If blnStop = True Then Return
                        If (f.ToLower.EndsWith(".pdf") Or f.ToLower.EndsWith(".xfdf") Or f.ToLower.EndsWith(".fdf") Or f.ToLower.EndsWith(".xdp")) And File.Exists(f) Then
                            lblStatus.Text = "Status: " & Path.GetFileName(f.ToString).ToString & " - " & CInt(fs.Length - intF).ToString & " files remaining..."
                            Application.DoEvents()
                            'SaveEntryToAccessDatabase(f, ReadPdfFile(f), StartupPath)
                            If chkOverwrite.Checked = False Then
                                If Not XMLDatabaseRecordExists(StartupPath & xmlDataSourceFilename, f) Then
                                    Dim fi As New FileInfo(f)
                                    If fi.Length < (maxFileSizeMB * 1024 * 1024) Then
                                        'SaveEntryToAccessDatabase(f, ReadPdfFile(f), StartupPath & AccessDataSourceFilename)
                                        SaveEntryToXMLDatabase(f, ReadPdfFile(f), StartupPath & xmlDataSourceFilename)
                                    End If
                                    IncrementStatus(1)
                                Else
                                    IncrementStatus(1)
                                End If
                            Else
                                Dim fi As New FileInfo(f)
                                If fi.Length < (100 * 1024 * 1024) Then
                                    'SaveEntryToAccessDatabase(f, ReadPdfFile(f), StartupPath & AccessDataSourceFilename)
                                    SaveEntryToXMLDatabase(f, ReadPdfFile(f), StartupPath & xmlDataSourceFilename)
                                End If
                                IncrementStatus(1)
                            End If
                            'File.WriteAllText(f & ".txt", ReadPdfFile(f))
                        End If
                    Catch exFile As Exception
                        Err.Clear()
                    End Try
                Next
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    'Public Function ReadPdfFilePDFBox(ByVal strfilename As String) As String
    '    Dim strText As New System.Text.StringBuilder
    '    Try
    '        If String.IsNullOrEmpty(strText.ToString().Trim & "") Then
    '            Dim doc As New PDDocument
    '            doc = PDDocument.load(strfilename)
    '            Dim stripper As PDFTextStripper = New PDFTextStripper()
    '            strText.AppendLine(stripper.getText(doc))
    '            'stripper.getText(doc)
    '        End If
    '    Catch ex As Exception
    '        Err.Clear()
    '    End Try
    '    Return strText.ToString
    'End Function
    Public Function ReadPdfFile(ByVal strfilename As String) As String
        Dim strText As New System.Text.StringBuilder
        Try
            strText.Append("")
            If Not File.Exists(strfilename) Then Return ""
            Dim fi As New FileInfo(strfilename)
            If fi.Length > (maxFileSizeMB * 1024 * 1024) Then
                Return ""
            End If
            If strfilename.ToLower.EndsWith(".pdf") Then
                Dim reader As New iTextSharp.text.pdf.PdfReader(DirectCast(strfilename, String))
                For page As Integer = 1 To reader.NumberOfPages
                    Dim its As iTextSharp.text.pdf.parser.ITextExtractionStrategy = New iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy
                    Try
                        Dim s As [String] = iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, page, its)
                        s = System.Text.Encoding.UTF8.GetString(System.Text.ASCIIEncoding.Convert(System.Text.Encoding.[Default], System.Text.Encoding.UTF8, System.Text.Encoding.[Default].GetBytes(s)))
                        strText.AppendLine(s)
                    Catch exGetText As Exception
                        Err.Clear()
                    End Try
                    Application.DoEvents()
                Next
                Try
                    If reader.AcroFields.Fields.Count > 0 Then
                        For Each fld As String In reader.AcroFields.Fields.Keys
                            strText.AppendLine(fld & " " & reader.AcroFields.GetField(fld).ToString())
                        Next
                    End If
                Catch ex As Exception
                    Err.Clear()
                End Try
                reader.Close()
                reader.Dispose()
            ElseIf strfilename.ToLower.EndsWith(".fdf") Or strfilename.ToLower.EndsWith(".xfdf") Or strfilename.ToLower.EndsWith(".xdp") Then
                Dim cfdfDoc As New FDFApp.FDFDoc_Class
                Dim cfdfApp As New FDFApp.FDFApp_Class
                cfdfDoc = cfdfApp.FDFOpenFromFile(DirectCast(strfilename, String), True, True)
                For Each fld As FDFApp.FDFDoc_Class.FDFField In cfdfDoc.XDPGetAllFields(True)
                    strText.AppendLine(fld.FieldName)
                    If Not fld.FieldValue Is Nothing Then
                        For Each strVal As String In fld.FieldValue.ToArray()
                            strText.Append(" " & strVal)
                        Next
                    End If
                Next
            End If
            Application.DoEvents()
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
            Err.Clear()
        End Try
        'If String.IsNullOrEmpty(strText.ToString().Trim & "") Then
        '    Dim doc As New PDDocument
        '    Application.DoEvents()
        '    doc = PDDocument.load(strfilename)
        '    Application.DoEvents()
        '    Dim stripper As PDFTextStripper = New PDFTextStripper()
        '    Application.DoEvents()
        '    strText.AppendLine(stripper.getText(doc))
        '    Application.DoEvents()
        '    'stripper.getText(doc)
        'End If
        Return strText.ToString
    End Function
    Public Function AccessDatabaseRecordExists(ByVal dataSourcePath As String, ByVal strstrfilename As String)
        Dim ds As New DataSet, tblName As String = "pdftext", conn As String = "PROVIDER=Microsoft.JET.OLEDB.4.0; Data Source=" & dataSourcePath & ";"
        Dim da As New System.Data.OleDb.OleDbDataAdapter("SELECT * FROM [" & tblName & "] WHERE [strfilename] LIKE '" & strstrfilename & "';", conn)
        Dim cmd As New System.Data.OleDb.OleDbCommandBuilder(da)
        da.Fill(ds, tblName)
        If ds Is Nothing Then
            Return False
        Else
            If ds.Tables(tblName).Rows.Count > 0 Then
                Return True
            End If
        End If
        Return False
    End Function
    Public Function XMLDatabaseRecordExists(ByVal dataSourcePath As String, ByVal strstrfilename As String)
        If Not File.Exists(dataSourcePath) Then Return False
        Try
            Dim ds As New DataSet, tblName As String = "pdftext", conn As String = "PROVIDER=Microsoft.JET.OLEDB.4.0; Data Source=" & dataSourcePath & ";"
            'Dim da As New System.Data.OleDb.OleDbDataAdapter("SELECT * FROM [" & tblName & "] WHERE [strfilename] LIKE '" & strstrfilename & "';", conn)
            ds.ReadXml(dataSourcePath, XmlReadMode.Auto)
            Dim drv As New DataView(ds.Tables(0))
            drv.RowFilter = "[strfilename] LIKE '" & strstrfilename & "'"
            'Dim cmd As New System.Data.OleDb.OleDbCommandBuilder(da)
            'da.Fill(ds, tblName)
            If ds Is Nothing Then
                Return False
            Else
                If drv.ToTable.Rows.Count > 0 Then
                    Return True
                End If
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
        Return False
    End Function
    Public Sub SaveEntryToAccessDatabase(ByVal strstrfilename As String, ByVal strFileContents As String, ByVal dataSourcePath As String)
        Dim ds As New DataSet, tblName As String = "pdftext", conn As String = "PROVIDER=Microsoft.JET.OLEDB.4.0; Data Source=" & dataSourcePath & ";"
        Dim da As New System.Data.OleDb.OleDbDataAdapter("SELECT * FROM [" & tblName & "] WHERE [strfilename] LIKE '" & strstrfilename & "';", conn)
        Dim cmd As New System.Data.OleDb.OleDbCommandBuilder(da)
        da.Fill(ds, tblName)
        Dim lastModified As New DateTime
        Try
            If Not ds Is Nothing Then
                If ds.Tables(tblName).Rows.Count = 1 And chkOverwrite.Checked Then
                    ds.Tables(tblName).Rows(0)("strfilename") = strstrfilename & ""
                    ds.Tables(tblName).Rows(0)("strcontents") = strFileContents & ""
                    Try
                        Dim fi As New FileInfo(strstrfilename)
                        lastModified = fi.LastWriteTimeUtc
                        ds.Tables(tblName).Rows(0)("dtcreated") = fi.CreationTimeUtc
                        ds.Tables(tblName).Rows(0)("dtmodified") = lastModified
                    Catch ex As Exception
                        ds.Tables(tblName).Rows(0)("dtcreated") = DateTime.Now.ToUniversalTime
                        ds.Tables(tblName).Rows(0)("dtmodified") = DateTime.Now.ToUniversalTime
                        Err.Clear()
                    End Try
                    If IsDBNull(ds.Tables(tblName).Rows(0)("dtentrycreated")) Then
                        ds.Tables(tblName).Rows(0)("dtentrycreated") = DateTime.Now.ToUniversalTime
                    End If
                    da.Update(ds, tblName)
                ElseIf ds.Tables(tblName).Rows.Count <= 0 Then
                    Dim dr As DataRow = ds.Tables(tblName).NewRow
                    dr("strfilename") = strstrfilename & ""
                    dr("strcontents") = strFileContents & ""
                    Try
                        Dim fi As New FileInfo(strstrfilename)
                        lastModified = fi.LastWriteTimeUtc
                        dr("dtcreated") = fi.CreationTimeUtc
                        dr("dtmodified") = lastModified
                    Catch ex As Exception
                        dr("dtcreated") = DateTime.Now.ToUniversalTime
                        dr("dtmodified") = DateTime.Now.ToUniversalTime
                        Err.Clear()
                    End Try
                    dr("dtentrycreated") = DateTime.Now.ToUniversalTime
                    ds.Tables(tblName).Rows.Add(dr)
                    da.Update(ds, tblName)
                End If
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public Sub SaveEntryToXMLDatabase(ByVal strstrfilename As String, ByVal strFileContents As String, ByVal dataSourcePath As String)
        Dim ds As New DataSet, tblName As String = "pdftext"
        'dataSourcePath &= xmlDataSourceFilename
        If File.Exists(dataSourcePath) Then
            ds.ReadXml(dataSourcePath, XmlReadMode.Auto)
        Else
            ds = New DataSet
            Dim dt As New DataTable(tblName)
            Dim dc As DataColumn, pk As New System.Collections.Generic.List(Of DataColumn)
            dc = New DataColumn("strfilename", GetType(System.String))
            dc.AllowDBNull = False
            dc.MaxLength = 500
            dc.Unique = True
            pk.Add(dc)
            dt.Columns.Add(dc)
            dc = New DataColumn("strcontents", GetType(System.String))
            dc.MaxLength = 30000
            dt.Columns.Add(dc)
            dc = New DataColumn("dtcreated", GetType(System.DateTime))
            dt.Columns.Add(dc)
            dc = New DataColumn("dtmodified", GetType(System.DateTime))
            dt.Columns.Add(dc)
            dc = New DataColumn("dtentrycreated", GetType(System.DateTime))
            dt.Columns.Add(dc)
            dt.PrimaryKey = pk.ToArray
            ds.Tables.Add(dt)
        End If
        'Dim da As New System.Data.OleDb.OleDbDataAdapter("SELECT * FROM [" & tblName & "] WHERE [strstrfilename] LIKE '%" & strstrfilename & "%';", conn)
        'Dim cmd As New System.Data.OleDb.OleDbCommandBuilder(da)
        'da.Fill(ds, tblName)
        Dim lastModified As New DateTime
        Try
            If Not ds Is Nothing Then
                Dim drv As New DataView(ds.Tables(tblName))
                drv.RowFilter = "strfilename LIKE '%" & strstrfilename & "%'"
                If drv.ToTable.Rows.Count = 1 And chkOverwrite.Checked Then
                    For i As Integer = 0 To ds.Tables(tblName).Rows.Count - 1
                        If ds.Tables(tblName).Rows(i)("strfilename") = strstrfilename & "" Then
                            ds.Tables(tblName).Rows(i)("strfilename") = strstrfilename & ""
                            ds.Tables(tblName).Rows(i)("strcontents") = strFileContents & ""
                            Try
                                Dim fi As New FileInfo(strstrfilename)
                                lastModified = fi.LastWriteTimeUtc
                                ds.Tables(tblName).Rows(i)("dtcreated") = fi.CreationTimeUtc
                                ds.Tables(tblName).Rows(i)("dtmodified") = lastModified
                            Catch ex As Exception
                                ds.Tables(tblName).Rows(i)("dtcreated") = DateTime.Now.ToUniversalTime
                                ds.Tables(tblName).Rows(i)("dtmodified") = DateTime.Now.ToUniversalTime
                                Err.Clear()
                            End Try
                            If IsDBNull(ds.Tables(tblName).Rows(i)("dtentrycreated")) Then
                                ds.Tables(tblName).Rows(i)("dtentrycreated") = DateTime.Now.ToUniversalTime
                            End If
                            ds.WriteXml(dataSourcePath, XmlWriteMode.WriteSchema)
                            Exit Try
                        End If
                    Next
                Else 'If ds.Tables(tblName).Rows.Count <= 0 Then
                    Dim dr As DataRow = ds.Tables(tblName).NewRow
                    dr("strfilename") = strstrfilename & ""
                    dr("strcontents") = strFileContents & ""
                    Try
                        Dim fi As New FileInfo(strstrfilename)
                        lastModified = fi.LastWriteTimeUtc
                        dr("dtcreated") = fi.CreationTimeUtc
                        dr("dtmodified") = lastModified
                    Catch ex As Exception
                        dr("dtcreated") = DateTime.Now.ToUniversalTime
                        dr("dtmodified") = DateTime.Now.ToUniversalTime
                        Err.Clear()
                    End Try
                    dr("dtentrycreated") = DateTime.Now.ToUniversalTime
                    ds.Tables(tblName).Rows.Add(dr)
                    ds.WriteXml(dataSourcePath, XmlWriteMode.WriteSchema)
                End If
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private searchds As New DataSet
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        'Try
        '    If String.IsNullOrEmpty(TextBox2.Text) Then
        '        ProgressBar1.Value = 100
        '        Return
        '    End If
        '    Dim tblName As String = "pdftext", conn As String = "PROVIDER=Microsoft.JET.OLEDB.4.0; Data Source=" & StartupPath & AccessDataSourceFilename & ";"
        '    Dim da As New System.Data.OleDb.OleDbDataAdapter("SELECT [strfilename] FROM [" & tblName & "] WHERE [strfilename] LIKE '%" & TextBox2.Text.Replace("  ", " ").Replace(" ", "%") & "%' OR [strcontents] LIKE '%" & TextBox2.Text.Replace("  ", " ").Replace(" ", "%") & "%';", conn)
        '    searchds = New DataSet
        '    'Dim cmd As New System.Data.OleDb.OleDbCommandBuilder(da)
        '    da.Fill(searchds, tblName)
        '    If searchds Is Nothing Then
        '        Return
        '    Else
        '        'MsgBox("Records found: " & searchds.Tables(tblName).Rows.Count)
        '        lblStatus.Text = "Status: " & "Records found: " & searchds.Tables(tblName).Rows.Count
        '        Me.DataGridView1.AutoGenerateColumns = True
        '        'Me.DataGridView1.DataMember = "strfilename"
        '        Me.DataGridView1.DataSource = searchds.Tables(tblName)
        '        Me.DataGridView1.AutoResizeColumns()
        '        Me.DataGridView1.AutoResizeRows()
        '        'DataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader)
        '    End If
        'Catch ex As Exception
        '    Err.Clear()
        'End Try
        Try
            If String.IsNullOrEmpty(TextBox2.Text) Then
                ProgressBar1.Value = 100
                Return
            End If
            Dim tblName As String = "pdftext" ', conn As String = "PROVIDER=Microsoft.JET.OLEDB.4.0; Data Source=" & StartupPath & AccessDataSourceFilename & ";"
            'Dim da As New System.Data.OleDb.OleDbDataAdapter("SELECT [strfilename] FROM [" & tblName & "] WHERE [strfilename] LIKE '%" & TextBox2.Text.Replace("  ", " ").Replace(" ", "%") & "%' OR [strcontents] LIKE '%" & TextBox2.Text.Replace("  ", " ").Replace(" ", "%") & "%';", conn)
            searchds = New DataSet
            searchds.ReadXml(Application.StartupPath.ToString.TrimEnd("\"c) & "\" & xmlDataSourceFilename, XmlReadMode.Auto)
            Dim drv As New System.Data.DataView(searchds.Tables(0))
            drv.RowFilter = "[strfilename] LIKE '%" & TextBox2.Text.Replace("  ", " ").Replace(" ", "%") & "%' OR [strcontents] LIKE '%" & TextBox2.Text.Replace("  ", " ").Replace(" ", "%") & "%'"
            'Dim cmd As New System.Data.OleDb.OleDbCommandBuilder(da)
            'da.Fill(searchds, tblName)
            Dim dt As System.Data.DataTable = drv.ToTable
            If dt Is Nothing Then
                Return
            Else
                'MsgBox("Records found: " & searchds.Tables(tblName).Rows.Count)
                lblStatus.Text = "Status: " & "Records found: " & dt.Rows.Count
                Me.DataGridView1.AutoGenerateColumns = True
                'Me.DataGridView1.DataMember = "strfilename"
                Me.DataGridView1.DataSource = dt 'searchds.Tables(tblName)
                Me.DataGridView1.AutoResizeColumns()
                Me.DataGridView1.AutoResizeRows()
                'DataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader)
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If Not Me.ParentForm Is Nothing Then
                If Me.ParentForm.GetType Is frmMain.GetType Then
                    frmMain1 = Me.ParentForm
                End If
            ElseIf Not Me.Owner Is Nothing Then
                If Me.Owner.GetType Is frmMain.GetType Then
                    frmMain1 = Me.Owner
                End If
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
    End Sub
    Private Sub DataGridView1_CellContentDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentDoubleClick
        Try
            If e.RowIndex >= 0 Then
                Dim fp As String = Me.DataGridView1.Item(e.ColumnIndex, e.RowIndex).Value
                'fp = fp.ToLower.Replace("d:\proposals\", "\\eap001\proposals\")
                If Not frmMain1 Is Nothing Then
                    If frmMain1.GetType Is frmMain.GetType Then
                        frmMain1.OpenFile(fp)
                        Me.Close()
                    End If
                End If
                'Process.Start(fp)
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        blnStop = True
    End Sub
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub frmScanPDFs_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If Not frmMain1 Is Nothing Then
            If frmMain1.GetType Is frmMain.GetType Then
                frmMain1.Show()
                Me.Hide()
            End If
        End If
    End Sub
End Class
