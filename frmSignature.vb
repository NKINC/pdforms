Public Class frmSignature
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 Nicholas Kowalewicz All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>
    Public _drawingMode As Boolean = False
    Private _cDraw As New clsPictureBox1_DrawSignature(Me)
    Public WithEvents _pictureBoxSolid As Bitmap
    Public _CloseForm As Boolean = False
    Public _backGroundColor As Color = Color.Transparent
    Public _sigbox_Size As Size
    Public Property backGroundColor As Color
        Get
            If Not PDFField_BackgroundColorPicker.BackColor = Nothing Then
                Return PDFField_BackgroundColorPicker.BackColor
            Else
                Return Color.White
            End If
        End Get
        Set(value As Color)
            PDFField_BackgroundColorPicker.BackColor = value
        End Set
    End Property
    Public Sub CloseForm()
        _CloseForm = True
        Me.Close()
    End Sub
    Public _profiles As New DataSet
    Public _profilePath As String = ""
    Public profileIndex As Integer = -1
    Private Property profiles() As DataSet
        Get
            Try
                _profilePath = frm.ApplicationDataFolder(True, "signatures") & "\signatureProfiles.xml"
                If System.IO.File.Exists(_profilePath) Then
                    _profiles = New DataSet
                    _profiles.ReadXml(_profilePath, XmlReadMode.Auto)
                    If Not _profiles.Tables(0).Columns.Contains("pfxBytesBase64") Then
                        Dim dc As New DataColumn("pfxBytesBase64", GetType(System.String))
                        dc.MaxLength = 99999
                        _profiles.Tables(0).Columns.Add(dc)
                        For _pIdx As Integer = 0 To _profiles.Tables(0).Rows.Count - 1
                            If System.IO.File.Exists(_profiles.Tables(0).Rows(_pIdx)("pfxLocation")) Then
                                _profiles.Tables(0).Rows(_pIdx)("pfxBytesBase64") = System.Convert.ToBase64String(System.IO.File.ReadAllBytes(_profiles.Tables(0).Rows(_pIdx)("pfxLocation"))).ToString().Replace(" "c, "+"c)
                            End If
                        Next
                        _profiles.WriteXml(_profilePath, XmlReadMode.Auto)
                    End If
                Else
                    _profiles = New DataSet("signatureProfiles")
                    Dim dc As New DataColumn
                    Dim pks As New List(Of DataColumn)
                    dc = New DataColumn("id", GetType(System.Int32))
                    dc.AllowDBNull = False
                    dc.AutoIncrement = True
                    dc.AutoIncrementSeed = 0
                    dc.AutoIncrementStep = 1
                    dc.Unique = True
                    Dim dt As New DataTable("signatureProfile")
                    dt.Columns.Add(dc)
                    pks.Add(dc)
                    dc = New DataColumn("profileName", GetType(System.String))
                    dc.MaxLength = 255
                    dt.Columns.Add(dc)
                    dc = New DataColumn("profileCreated", GetType(System.String))
                    dc.MaxLength = 255
                    dt.Columns.Add(dc)
                    dc = New DataColumn("reason", GetType(System.String))
                    dc.MaxLength = 255
                    dt.Columns.Add(dc)
                    dc = New DataColumn("location", GetType(System.String))
                    dc.MaxLength = 255
                    dt.Columns.Add(dc)
                    dc = New DataColumn("creator", GetType(System.String))
                    dc.MaxLength = 255
                    dt.Columns.Add(dc)
                    dc = New DataColumn("contact", GetType(System.String))
                    dc.MaxLength = 255
                    dt.Columns.Add(dc)
                    dc = New DataColumn("pfxLocation", GetType(System.String))
                    dc.MaxLength = 255
                    dt.Columns.Add(dc)
                    dc = New DataColumn("pfxBytesBase64", GetType(System.String))
                    dc.MaxLength = 99999
                    dt.Columns.Add(dc)
                    dc = New DataColumn("password", GetType(System.String))
                    dc.MaxLength = 255
                    dt.Columns.Add(dc)
                    dc = New DataColumn("renderingMode", GetType(System.String))
                    dc.MaxLength = 255
                    dt.Columns.Add(dc)
                    dc = New DataColumn("lineWidth", GetType(System.String))
                    dc.MaxLength = 255
                    dt.Columns.Add(dc)
                    dc = New DataColumn("lineColor", GetType(System.String))
                    dc.MaxLength = 255
                    dt.Columns.Add(dc)
                    dc = New DataColumn("signatureImage", GetType(System.String))
                    dc.MaxLength = 99999
                    dt.Columns.Add(dc)
                    dt.PrimaryKey = pks.ToArray
                    _profiles.Tables.Add(dt)
                    _profilePath = frm.ApplicationDataFolder(True, "signatures") & "\signatureProfiles.xml"
                    _profiles.WriteXml(_profilePath, XmlReadMode.Auto)
                End If
            Catch ex As Exception
                Err.Clear()
            End Try
            Return _profiles
        End Get
        Set(ByVal value As DataSet)
            Try
                _profilePath = frm.ApplicationDataFolder(True, "signatures") & "\signatureProfiles.xml"
                value.WriteXml(_profilePath, XmlReadMode.Auto)
            Catch ex As Exception
                Err.Clear()
            End Try
        End Set
    End Property
    Public Sub LoadProfilesCombo(ByRef cmb As ComboBox)
        cmb.Items.Clear()
        _profiles = profiles
        cmb.Items.Add("SELECT A PROFILE")
        If _profiles.Tables.Count > 0 Then
            If _profiles.Tables(0).Rows.Count > 0 Then
                For Each dr As DataRow In _profiles.Tables(0).Rows
                    cmb.Items.Add(dr("id") & ": " & dr("profileName") & "")
                Next
            End If
        End If
    End Sub
    Public Sub AddProfile()
        _profiles = profiles
        Dim dr As DataRow = _profiles.Tables(0).NewRow
        dr("profileName") = Me.ComboBoxSignatureProfiles.Items.Count & " - " & Me.TextBoxSignatureContact.Text & " - " & Me.TextBoxSignatureCreator.Text & ""
        dr("profileCreated") = CStr(DateTime.Now.ToString()) & ""
        dr("location") = Me.TextBoxLocation.Text & ""
        dr("creator") = Me.TextBoxSignatureCreator.Text & ""
        dr("contact") = Me.TextBoxSignatureContact.Text & ""
        dr("pfxLocation") = Me.TextBoxPFXPath.Text & ""
        dr("password") = FDFCheckChar(System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Me.TextBoxPFXPassword.Text & ""))) & ""
        dr("renderingMode") = Me.ComboBoxSignatureAppearanceRenderMode.SelectedIndex.ToString & ""
        dr("lineWidth") = Me.ComboBoxSignatureLineWidth.SelectedIndex.ToString & ""
        dr("lineColor") = System.Drawing.ColorTranslator.ToHtml(sign_LineColor.BackColor)
        dr("reason") = TextBoxReason.Text & ""
        If Not PictureBox1.Image Is Nothing Then
            Dim m As New System.IO.MemoryStream
            PictureBox1.Image.Save(m, System.Drawing.Imaging.ImageFormat.Jpeg)
            Dim image() As Byte = m.ToArray
            dr("signatureImage") = FDFCheckChar(System.Convert.ToBase64String(image).ToString.Replace(" ", "+"))
        Else
            dr("signatureImage") = ""
        End If
        _profiles.Tables(0).Rows.Add(dr)
        _profilePath = frm.ApplicationDataFolder(True, "signatures") & "\signatureProfiles.xml"
        _profiles.WriteXml(_profilePath, XmlReadMode.Auto)
        LoadProfilesCombo(Me.ComboBoxSignatureProfiles)
        Me.ComboBoxSignatureProfiles.SelectedIndex = Me.ComboBoxSignatureProfiles.Items.Count - 1
        If profileIndex >= 0 Then
            Button6.Visible = True
        Else
            Button6.Visible = False
        End If
    End Sub
    Public Sub SaveProfile()
        _profiles = profiles
        profileIndex = Me.ComboBoxSignatureProfiles.SelectedIndex - 1
        If ComboBoxSignatureProfiles.Enabled = False Or profileIndex < 0 Then
            AddProfile()
            Return
        End If
        _profiles.Tables(0).Rows(profileIndex)("profileName") = Me.ComboBoxSignatureProfiles.Items.Count & " - " & Me.TextBoxSignatureContact.Text & " - " & Me.TextBoxSignatureCreator.Text & ""
        _profiles.Tables(0).Rows(profileIndex)("profileCreated") = CStr(DateTime.Now.ToString()) & ""
        _profiles.Tables(0).Rows(profileIndex)("location") = Me.TextBoxLocation.Text & ""
        _profiles.Tables(0).Rows(profileIndex)("creator") = Me.TextBoxSignatureCreator.Text & ""
        _profiles.Tables(0).Rows(profileIndex)("contact") = Me.TextBoxSignatureContact.Text & ""
        _profiles.Tables(0).Rows(profileIndex)("pfxLocation") = Me.TextBoxPFXPath.Text & ""
        Try
            If Not _profiles.Tables(0).Columns.Contains("imageBackgroundColor") Then
                _profiles.Tables(0).Columns.Add("imageBackgroundColor", GetType(System.String))
            End If
            _profiles.Tables(0).Rows(profileIndex)("imageBackgroundColor") = backGroundColor.A & "," & backGroundColor.R & "," & backGroundColor.G & "," & backGroundColor.B
        Catch ex As Exception
            Err.Clear()
        End Try
        Try
            If System.IO.File.Exists(Me.TextBoxPFXPath.Text & "") Then
                _profiles.Tables(0).Rows(profileIndex)("pfxBytesBase64") = System.Convert.ToBase64String(System.IO.File.ReadAllBytes(Me.TextBoxPFXPath.Text & "")).ToString.Replace(" "c, "+"c)
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
        _profiles.Tables(0).Rows(profileIndex)("password") = FDFCheckChar(System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Me.TextBoxPFXPassword.Text & ""))) & ""
        _profiles.Tables(0).Rows(profileIndex)("renderingMode") = Me.ComboBoxSignatureAppearanceRenderMode.SelectedIndex.ToString & ""
        _profiles.Tables(0).Rows(profileIndex)("lineWidth") = Me.ComboBoxSignatureLineWidth.SelectedIndex.ToString & ""
        _profiles.Tables(0).Rows(profileIndex)("lineColor") = System.Drawing.ColorTranslator.ToHtml(sign_LineColor.BackColor)
        _profiles.Tables(0).Rows(profileIndex)("reason") = TextBoxReason.Text & ""
        If Not _pictureBoxSolid Is Nothing Then
            Dim m As New System.IO.MemoryStream
            _pictureBoxSolid.Save(m, System.Drawing.Imaging.ImageFormat.Png)
            Dim image() As Byte = m.ToArray
            _profiles.Tables(0).Rows(profileIndex)("signatureImage") = (System.Convert.ToBase64String(image).ToString.Replace(" ", "+"))
        Else
            _profiles.Tables(0).Rows(profileIndex)("signatureImage") = ""
        End If
        _profiles.AcceptChanges()
        _profilePath = frm.ApplicationDataFolder(True, "signatures") & "\signatureProfiles.xml"
        _profiles.WriteXml(_profilePath, XmlReadMode.Auto)
    End Sub
    Public Sub SaveProfileWithPicture(ByVal img As System.Drawing.Image)
        _profiles = profiles
        If ComboBoxSignatureProfiles.Enabled = False Then
            AddProfile()
            Return
        End If
        _profiles.Tables(0).Rows(profileIndex)("profileName") = Me.ComboBoxSignatureProfiles.Items.Count & " - " & Me.TextBoxSignatureContact.Text & " - " & Me.TextBoxSignatureCreator.Text & ""
        _profiles.Tables(0).Rows(profileIndex)("profileCreated") = CStr(DateTime.Now.ToString()) & ""
        _profiles.Tables(0).Rows(profileIndex)("location") = Me.TextBoxLocation.Text & ""
        _profiles.Tables(0).Rows(profileIndex)("creator") = Me.TextBoxSignatureCreator.Text & ""
        _profiles.Tables(0).Rows(profileIndex)("contact") = Me.TextBoxSignatureContact.Text & ""
        _profiles.Tables(0).Rows(profileIndex)("pfxLocation") = Me.TextBoxPFXPath.Text & ""
        Try
            If System.IO.File.Exists(Me.TextBoxPFXPath.Text & "") Then
                _profiles.Tables(0).Rows(profileIndex)("pfxBytesBase64") = System.Convert.ToBase64String(System.IO.File.ReadAllBytes(Me.TextBoxPFXPath.Text & "")).ToString.Replace(" "c, "+"c)
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
        _profiles.Tables(0).Rows(profileIndex)("password") = FDFCheckChar(System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Me.TextBoxPFXPassword.Text & ""))) & ""
        _profiles.Tables(0).Rows(profileIndex)("renderingMode") = Me.ComboBoxSignatureAppearanceRenderMode.SelectedIndex.ToString & ""
        _profiles.Tables(0).Rows(profileIndex)("lineWidth") = Me.ComboBoxSignatureLineWidth.SelectedIndex.ToString & ""
        _profiles.Tables(0).Rows(profileIndex)("lineColor") = System.Drawing.ColorTranslator.ToHtml(sign_LineColor.BackColor)
        _profiles.Tables(0).Rows(profileIndex)("reason") = TextBoxReason.Text & ""
        If Not img Is Nothing Then
            Dim m As New System.IO.MemoryStream
            img.Save(m, System.Drawing.Imaging.ImageFormat.Jpeg)
            Dim image() As Byte = m.ToArray
            _profiles.Tables(0).Rows(profileIndex)("signatureImage") = (System.Convert.ToBase64String(image).ToString.Replace(" ", "+"))
        Else
            _profiles.Tables(0).Rows(profileIndex)("signatureImage") = ""
        End If
        _profiles.AcceptChanges()
        _profilePath = frm.ApplicationDataFolder(True, "signatures") & "\signatureProfiles.xml"
        _profiles.WriteXml(_profilePath, XmlReadMode.Auto)
    End Sub
    Private Function FDFCheckChar(ByVal strINPUT As String) As String
        If strINPUT.Length <= 0 Then
            Return ""
            Exit Function
        End If
        strINPUT = strINPUT.Replace("\".ToString, "\\".ToString)
        For Each chrReplace As Char In "/$#~%*^()+=[]{};""<>?|!'".ToCharArray
            If strINPUT.IndexOf(chrReplace) >= 0 Then
                strINPUT = strINPUT.Replace(chrReplace.ToString, CStr("\" & chrReplace.ToString))
            End If
        Next
        strINPUT = strINPUT.Replace(vbNewLine, "\r")
        strINPUT = strINPUT.Replace(Environment.NewLine, "\r")
        strINPUT = strINPUT.Replace(Chr(13), "\r")
        strINPUT = strINPUT.Replace(Chr(10), "\r")
        Return strINPUT & ""
    End Function
    Private Function FDFCheckCharReverse(ByVal strINPUT As String) As String
        If strINPUT.Length <= 0 Then
            Return ""
            Exit Function
        End If
        strINPUT = strINPUT.Replace("\\".ToString, "\".ToString)
        For Each chrReplace As Char In "/$#~%*^()+=[]{};""<>?|!'".ToCharArray
            If strINPUT.IndexOf("\" & chrReplace) >= 0 Then
                strINPUT = strINPUT.Replace("\" & chrReplace, chrReplace)
            End If
        Next
        strINPUT = strINPUT.Replace("\r", vbNewLine)
        strINPUT = strINPUT.Replace("\" & Environment.NewLine, vbNewLine)
        strINPUT = strINPUT.Replace("\" & Chr(13), vbNewLine)
        strINPUT = strINPUT.Replace("\" & Chr(10), vbNewLine)
        Return strINPUT.ToString
    End Function
    Private cfdf As New FDFApp.FDFApp_Class
    Public Sub LoadProfile()
        clearProfile()
        profileIndex = Me.ComboBoxSignatureProfiles.SelectedIndex - 1
        If profileIndex >= 0 Then
            Button6.Visible = True
        Else
            Button6.Visible = False
        End If
        _profiles = profiles
        Dim dr As DataRow = _profiles.Tables(0).Rows(Me.profileIndex)
        Me.TextBoxLocation.Text = dr("location") & ""
        Me.TextBoxSignatureCreator.Text = dr("creator") & ""
        Me.TextBoxSignatureContact.Text = dr("contact") & ""
        Me.TextBoxPFXPath.Text = dr("pfxLocation") & ""
        Try
            pfxBytes = Nothing
            If System.IO.File.Exists(Me.TextBoxPFXPath.Text) Then
                pfxBytes = System.IO.File.ReadAllBytes(Me.TextBoxPFXPath.Text)
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
        Try
            Me.TextBoxPFXPassword.Text = FDFCheckCharReverse(System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(dr("password"))))
        Catch ex As Exception
            Me.TextBoxPFXPassword.Text = dr("password")
            Err.Clear()
        End Try
        Me.ComboBoxSignatureAppearanceRenderMode.SelectedIndex = CInt(dr("renderingMode")) + 0
        Me.ComboBoxSignatureLineWidth.SelectedIndex = CInt(dr("lineWidth")) + 0
        TextBoxReason.Text = dr("reason") & ""
        Dim c As New System.Drawing.Color
        c = System.Drawing.ColorTranslator.FromHtml(dr("lineColor") & "")
        sign_LineColor.BackColor = c
        Try
            If Not _profiles.Tables(0).Rows(profileIndex)("imageBackgroundColor") Is Nothing Then
                If Not _profiles.Tables(0).Rows(profileIndex)("imageBackgroundColor") = "" Then
                    backGroundColor = Color.FromArgb(_profiles.Tables(0).Rows(profileIndex)("imageBackgroundColor").split(",")(0), _profiles.Tables(0).Rows(profileIndex)("imageBackgroundColor").split(",")(1), _profiles.Tables(0).Rows(profileIndex)("imageBackgroundColor").split(",")(2), _profiles.Tables(0).Rows(profileIndex)("imageBackgroundColor").split(",")(3))
                End If
            End If
        Catch ex As Exception
            backGroundColor = Color.Transparent
        End Try
        Try
            If Not String.IsNullOrEmpty(dr("signatureImage") & "") Then
                Dim image As Byte()
                image = System.Convert.FromBase64String(dr("signatureImage"))
                _pictureBoxSolid = System.Drawing.Image.FromStream(New System.IO.MemoryStream(image.ToArray()))
                PictureBox1.Parent = TabPage2
                PictureBox1.BackColor = backGroundColor
                _cDraw.LoadSignatureImage(PictureBox1, _pictureBoxSolid.Clone())
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub frmSignature_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not Me.DialogResult = Windows.Forms.DialogResult.OK Then
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        End If
    End Sub
    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ClearScreen()
        ComboBoxSignatureLineWidth.Items.Clear()
        backGroundColor = Color.Transparent
        '
        For i As Integer = 1 To 20 Step 1
            ComboBoxSignatureLineWidth.Items.Add(i.ToString)
        Next
        ComboBoxSignatureLineWidth.SelectedIndex = 8
        clearProfile()
        LoadProfilesCombo(Me.ComboBoxSignatureProfiles)
        If Me.ComboBoxSignatureProfiles.SelectedIndex < 0 Then
            Me.ComboBoxSignatureProfiles.SelectedIndex = 0
        End If
        If Not Me.Owner Is Nothing Then
            If Me.Owner.GetType Is GetType(frmMain) Then
                Try
                    frm = Me.Owner
                    If Not frm Is Nothing Then
                        _sigbox_Size.Width = frm.cUserRect.rectPDFReversed.Width
                        _sigbox_Size.Height = frm.cUserRect.rectPDFReversed.Height
                        If DirectCast(Me.Owner, frmMain).ComboBoxSignatureAppearanceRenderMode = 4 Then
                            ComboBoxSignatureAppearanceRenderMode.SelectedIndex = DirectCast(Me.Owner, frmMain).ComboBoxSignatureAppearanceRenderMode
                            Me.TabControl1.SelectedTab = TabPage2
                        End If
                    End If
                Catch ex As Exception
                    Err.Clear()
                End Try
            End If
        End If
    End Sub
    Dim mClick As Boolean = False
    Public Sub ClearScreen()
        Me.PictureBox1.Visible = True
        Me.PictureBox1.Width = TabPage2.ClientRectangle.Width
        Me.PictureBox1.Height = TabPage2.ClientRectangle.Height
        Me.PictureBox1.Top = TabPage2.ClientRectangle.Top
        Me.PictureBox1.Left = TabPage2.ClientRectangle.Left
        Me.PictureBox1.BackColor = Color.Transparent
        Dim img As New Bitmap(PictureBox1.Width, PictureBox1.Height, Imaging.PixelFormat.Format32bppArgb)
        Dim g As Graphics = Graphics.FromImage(img)
        g.FillRectangle(New SolidBrush(Color.Transparent), New Rectangle(0, 0, PictureBox1.Width, PictureBox1.Height))
        g.Dispose()
        PictureBox1.Image = img.Clone
        Dim img2 As Bitmap = img.Clone
        Dim g2 = Graphics.FromImage(img2)
        If Not backGroundColor = Color.Transparent Then
            g2.FillRectangle(New SolidBrush(backGroundColor), New Rectangle(0, 0, PictureBox1.Width, PictureBox1.Height))
        End If
        g2.Dispose()
        _pictureBoxSolid = img2.Clone
        TabPage2.BackgroundImageLayout = ImageLayout.Stretch
        TabPage2.BackgroundImage = Image.FromFile(Application.StartupPath.ToString.TrimEnd("\") & "\signaturebar.jpg")
    End Sub
    Private Sub PictureBox1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDoubleClick
        Try
            Select Case MsgBox("Clear Screen?", MsgBoxStyle.YesNo + MsgBoxStyle.ApplicationModal, "Clear:")
                Case MsgBoxResult.Yes, MsgBoxResult.Ok
                    ClearScreen()
            End Select
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub PictureBox1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown
        _cDraw._Mouse_DrawMode = True
        If _cDraw._Mouse_DrawMode Then
            _cDraw._Mouse_Point_Previous = e.Location
            _cDraw._Mouse_Point = e.Location
        End If
    End Sub
    Private Sub PictureBox1_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.MouseEnter
        TabPage2.BackgroundImage = Image.FromFile(Application.StartupPath.ToString.TrimEnd("\") & "\signaturebar.jpg")
    End Sub
    Private Sub PictureBox1_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.MouseHover
        TabPage2.BackgroundImage = Image.FromFile(Application.StartupPath.ToString.TrimEnd("\") & "\signaturebar.jpg")
    End Sub
    Private Sub PictureBox1_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.MouseLeave
        TabPage2.BackgroundImage = Nothing
    End Sub
    Private Sub PictureBox1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseMove
        If _cDraw._Mouse_DrawMode Then
            _cDraw._Mouse_Point = PictureBox1.PointToClient(Cursor.Position)
            If _cDraw._Mouse_Point_Previous.X > 0 And _cDraw._Mouse_Point_Previous.Y > 0 Then
                If _cDraw._Mouse_DrawMode Then
                    If Not _cDraw._Mouse_Point = _cDraw._Mouse_Point_Previous Then
                        _cDraw.DrawSignature(PictureBox1, _pictureBoxSolid, sign_LineColor.BackColor, False, True, CInt(ComboBoxSignatureLineWidth.SelectedIndex + 1))
                    End If
                End If
            End If
            _cDraw._Mouse_Point_Previous = _cDraw._Mouse_Point
            Application.DoEvents()
        End If
    End Sub
    Private Sub PictureBox1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseUp
        _cDraw._Mouse_DrawMode = False
        _cDraw._Mouse_Point_Previous = e.Location
        _cDraw._Mouse_Point = e.Location
    End Sub
    Public Function getSignatureImageBytes() As Byte()
        Try
            Dim m As New System.IO.MemoryStream
            getSignatureImage.Save(m, System.Drawing.Imaging.ImageFormat.Png)
            Return m.ToArray
        Catch ex As Exception
            Err.Clear()
        End Try
        Return Nothing
    End Function
    Private Function GetControlImage(ByVal ctl As Control) As Bitmap
        Dim bm As New Bitmap(ctl.Width, ctl.Height)
        ctl.DrawToBitmap(bm, New Rectangle(0, 0, ctl.Width, ctl.Height))
        Return bm
    End Function
    Private Function GetFormImageWithoutBorders(ByVal frm As Form) As Bitmap
        Using whole_form As Bitmap = GetControlImage(frm)
            Dim origin As Point = frm.PointToScreen(New Point(0, 0))
            Dim dx As Integer = origin.X - frm.Left
            Dim dy As Integer = origin.Y - frm.Top
            Dim wid As Integer = frm.ClientSize.Width
            Dim hgt As Integer = frm.ClientSize.Height
            Dim bm As New Bitmap(wid, hgt)
            Using gr As Graphics = Graphics.FromImage(bm)
                gr.DrawImage(whole_form, 0, 0, New Rectangle(dx, dy, wid, hgt), GraphicsUnit.Pixel)
            End Using
            Return bm
        End Using
    End Function
    Public Function getSignatureImage() As Image
        Try
            Me.Hide()
            Me.Show()
            If Not backGroundColor = Color.Transparent Then
                Dim w As Integer = _pictureBoxSolid.Width, h As Integer = _pictureBoxSolid.Height
                Dim imgTemp As New System.Drawing.Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
                Using g As Graphics = Graphics.FromImage(imgTemp)
                    g.FillRectangle(New SolidBrush(backGroundColor), 0, 0, imgTemp.Width, imgTemp.Height)
                    g.DrawImage(_pictureBoxSolid, 0, 0)
                    g.Dispose()
                End Using
                Return DirectCast(imgTemp, Image)
            End If
            Return DirectCast(_pictureBoxSolid, Image)
        Catch ex As Exception
            Err.Clear()
        End Try
        Return Nothing
    End Function
    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub PictureBox1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBox1.Paint
    End Sub
    Private Sub PictureBox1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
    End Sub
    Public frm As frmMain = Nothing
    Public Sub SetFrmMain(ByRef frmMain1 As frmMain)
        frm = frmMain1
    End Sub
    Public pfxBytes() As Byte = Nothing
    Public Sub New()
        InitializeComponent()
        backGroundColor = Color.Transparent
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        frm.SignatureImage = getSignatureImage()
        frm.sign_AppearanceRenderModeIndex = ComboBoxSignatureAppearanceRenderMode.SelectedIndex
        frm.sign_contact = TextBoxSignatureContact.Text & ""
        frm.sign_creator = TextBoxSignatureCreator.Text & ""
        frm.sign_datetime = TextBoxSignatureDate.Text & ""
        frm.sign_location = TextBoxLocation.Text & ""
        frm.sign_pfxPath = TextBoxPFXPath.Text & ""
        frm.sign_pfxBytes = Nothing
        If System.IO.File.Exists(frm.sign_pfxPath) Then
            pfxBytes = System.IO.File.ReadAllBytes(frm.sign_pfxPath)
            frm.sign_pfxBytes = pfxBytes
        ElseIf Not pfxBytes Is Nothing Then
            If pfxBytes.Length > 0 Then
                frm.sign_pfxBytes = pfxBytes
            End If
        End If
        frm.sign_pfxPassword = TextBoxPFXPassword.Text & ""
        frm.sign_reason = TextBoxReason.Text & ""
        frm.sign_lineWidthIndex = ComboBoxSignatureLineWidth.SelectedIndex
        frm.sign_lineColor = sign_LineColor.BackColor
        Me.DialogResult = DialogResult.OK
        Me._CloseForm = True
        Me.Close()
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ClearScreen()
    End Sub
    Private Sub ComboBoxSignatureAppearanceRenderMode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxSignatureAppearanceRenderMode.SelectedIndexChanged
        If ComboBoxSignatureAppearanceRenderMode.SelectedIndex >= 2 Then
            PictureBox1.Enabled = True
            Button4.Visible = True
            Button2.Visible = True
            If profileIndex >= 0 Then Button6.Visible = True
        Else
            PictureBox1.Enabled = False
            Button4.Visible = False
            Button2.Visible = False
            Button6.Visible = False
        End If
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            Dim fileSelect As New clsPromptDialog()
            Dim defaultFile As String = ""
            If Not TextBoxPFXPath.Text = "" Then
                If System.IO.File.Exists(TextBoxPFXPath.Text) Then
                    defaultFile = (TextBoxPFXPath.Text)
                End If
            End If
            Dim fpath As String = fileSelect.ShowDialogFileSelection("Select a security cerficate (.pfx,.p12):", defaultFile, "Select a file:", Me, "pfx|*.pfx|p12|*.p12|All Files|*.*")
            If Not fpath = "" Then
                TextBoxPFXPath.Text = fpath
                Try
                    pfxBytes = Nothing
                    If System.IO.File.Exists(fpath) Then
                        pfxBytes = System.IO.File.ReadAllBytes(fpath)
                    End If
                Catch ex As Exception
                    Err.Clear()
                End Try
            End If
        Catch ex As Exception
            frm.TimeStampAdd(ex, frm.debugMode)
        End Try
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        TabControl1.SelectTab(TabPage2)
        If profileIndex >= 0 Then Button6.Visible = True
    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Try
            If Me.Owner.GetType Is frmMain.GetType Then
                DirectCast(Me.Owner, frmMain).Show()
            End If
            Me.Visible = False
            Me.Close()
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub sign_LineColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sign_LineColor.Click
        Try
            frm.cUserRect.pauseDraw = True
            Dim ColorDialog1 As New ColorDialog
            ColorDialog1.FullOpen = True
            ColorDialog1.SolidColorOnly = False
            ColorDialog1.Color = Me.sign_LineColor.BackColor
            ColorDialog1.CustomColors = frm.iMyCustomColors
            Select Case ColorDialog1.ShowDialog(Me)
                Case Windows.Forms.DialogResult.OK, Windows.Forms.DialogResult.Yes
                    sign_LineColor.BackColor = System.Drawing.Color.FromArgb(255, ColorDialog1.Color.R, ColorDialog1.Color.G, ColorDialog1.Color.B)
                Case Else
                    sign_LineColor.BackColor = Color.Black
            End Select
            ColorDialog1.Dispose()
            ColorDialog1 = Nothing
        Catch ex As Exception
            frm.TimeStampAdd(ex, frm.debugMode)
        Finally
            frm.cUserRect.pauseDraw = False
        End Try
    End Sub
    Private Sub linkSignatureProfileNew_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles linkSignatureProfileNew.LinkClicked
        ComboBoxSignatureProfiles.Enabled = False
    End Sub
    Private Sub linkSignatureProfileLoad_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles linkSignatureProfileLoad.LinkClicked
        If ComboBoxSignatureProfiles.SelectedIndex - 1 >= 0 Then
            LoadProfile()
        End If
        If Me.ComboBoxSignatureProfiles.Items.Count > 0 Then Me.ComboBoxSignatureProfiles.Enabled = True
    End Sub
    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        LoadProfilesCombo(Me.ComboBoxSignatureProfiles)
        If Me.ComboBoxSignatureProfiles.Items.Count > 0 Then Me.ComboBoxSignatureProfiles.Enabled = True
        Me.ComboBoxSignatureProfiles.SelectedIndex = 0
        clearProfile()
    End Sub
    Private Sub linkSignatureProfileSave_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles linkSignatureProfileSave.LinkClicked
        If ComboBoxSignatureProfiles.Enabled And ComboBoxSignatureProfiles.SelectedIndex - 1 >= 0 Then
            SaveProfile()
        Else
            AddProfile()
        End If
        If Me.ComboBoxSignatureProfiles.Items.Count > 0 Then Me.ComboBoxSignatureProfiles.Enabled = True
    End Sub
    Private Sub linkSignatureProfileDelete_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles linkSignatureProfileDelete.LinkClicked
        Try
            _profiles = profiles
            If _profiles.Tables(0).Rows.Count > Me.ComboBoxSignatureProfiles.SelectedIndex - 1 Then
                _profiles.Tables(0).Rows(Me.ComboBoxSignatureProfiles.SelectedIndex - 1).Delete()
                profiles = _profiles
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
        LoadProfilesCombo(Me.ComboBoxSignatureProfiles)
        If Me.ComboBoxSignatureProfiles.Items.Count > 0 Then Me.ComboBoxSignatureProfiles.Enabled = True
    End Sub
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        frm.SignatureImage = getSignatureImage()
        If ComboBoxSignatureProfiles.SelectedIndex - 1 >= 0 And profileIndex >= 0 And ComboBoxSignatureProfiles.Enabled Then
            If ComboBoxSignatureAppearanceRenderMode.SelectedIndex >= 2 Then
                If Not frm.SignatureImage Is Nothing Then
                    linkSignatureProfileSave_LinkClicked(Me, Nothing)
                End If
            Else
            End If
        End If
        frm.sign_AppearanceRenderModeIndex = ComboBoxSignatureAppearanceRenderMode.SelectedIndex
        frm.sign_contact = TextBoxSignatureContact.Text & ""
        frm.sign_creator = TextBoxSignatureCreator.Text & ""
        frm.sign_datetime = TextBoxSignatureDate.Text & ""
        frm.sign_location = TextBoxLocation.Text & ""
        frm.sign_pfxPath = TextBoxPFXPath.Text & ""
        frm.sign_pfxBytes = Nothing
        If System.IO.File.Exists(frm.sign_pfxPath) Then
            pfxBytes = System.IO.File.ReadAllBytes(frm.sign_pfxPath)
            frm.sign_pfxBytes = pfxBytes
        ElseIf Not pfxBytes Is Nothing Then
            If pfxBytes.Length > 0 Then
                frm.sign_pfxBytes = pfxBytes
            End If
        End If
        frm.sign_pfxPassword = TextBoxPFXPassword.Text & ""
        frm.sign_reason = TextBoxReason.Text & ""
        frm.sign_lineWidthIndex = ComboBoxSignatureLineWidth.SelectedIndex
        frm.sign_lineColor = sign_LineColor.BackColor
        Me._CloseForm = True
        Me.Close()
    End Sub
    Public Sub clearProfile()
        profileIndex = -1
        _profiles = profiles
        Me.TextBoxLocation.Text = ""
        Me.TextBoxSignatureCreator.Text = ""
        Me.TextBoxSignatureContact.Text = ""
        Me.TextBoxPFXPath.Text = ""
        Me.TextBoxPFXPassword.Text = ""
        Me.ComboBoxSignatureAppearanceRenderMode.SelectedIndex = 0
        Me.ComboBoxSignatureLineWidth.SelectedIndex = 9
        TextBoxReason.Text = ""
        Dim c As New System.Drawing.Color
        c = System.Drawing.ColorTranslator.FromHtml("#0000FF")
        sign_LineColor.BackColor = c
        Try
            _pictureBoxSolid = New System.Drawing.Bitmap(PictureBox1.Width, PictureBox1.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
            _cDraw.LoadSignatureImage(PictureBox1, _pictureBoxSolid.Clone())
        Catch ex As Exception
            Err.Clear()
        End Try
        If profileIndex >= 0 Then
            Button6.Visible = True
        Else
            Button6.Visible = False
        End If
    End Sub
    Private Sub ComboBoxSignatureProfiles_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxSignatureProfiles.SelectedIndexChanged
        Try
            If Me.ComboBoxSignatureProfiles.SelectedIndex <= 0 Or Me.ComboBoxSignatureProfiles.Enabled = False Then
                clearProfile()
            ElseIf Me.ComboBoxSignatureProfiles.Enabled = True Then
                LoadProfile()
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub TabPage2_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabPage2.GotFocus
        If profileIndex >= 0 Then
            Button6.Visible = True
        Else
            Button6.Visible = False
        End If
        If ComboBoxSignatureAppearanceRenderMode.SelectedIndex >= 2 Then
            Button2.Visible = True
        Else
            Button2.Visible = False
        End If
    End Sub
    Private Sub ImportSignatureImageToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Public Sub import_signatureImage()
        Try
            Dim fpath As String = frm.ApplicationDataFolder(True, "signatures") & "\"
            Dim c As New clsPromptDialog
            fpath = c.ShowDialogFileSelection("Select an image.", frm.ApplicationDataFolder(True, "signatures") & "\", "Import Signature", Me, "JPG|*.jpg|JPEG|*.jpeg|PNG|*.png|GIF|*.gif|BMP|*.bmp|TIF|*.tif|TIFF|*.tiff|All Files|*.*")
            If System.IO.File.Exists(fpath) Then
                Dim b As Bitmap = Bitmap.FromFile(fpath)
                If Not b Is Nothing Then
                    Dim w As Integer = _pictureBoxSolid.Width, h As Integer = _pictureBoxSolid.Height
                    Dim rat As Single = w / b.Width
                    If h / b.Height < rat Then
                        rat = h / b.Height
                    End If
                    _pictureBoxSolid = New Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
                    Using g As Graphics = Graphics.FromImage(_pictureBoxSolid)
                        If Not backGroundColor = Color.Transparent Then
                            g.FillRectangle(New System.Drawing.SolidBrush(backGroundColor), 0, 0, w, h)
                        End If
                        g.DrawImage(b, New System.Drawing.RectangleF(CSng(w - b.Width * rat) / 2, CSng(h - b.Height * rat) / 2, b.Width * rat, b.Height * rat))
                    End Using
                    _cDraw.LoadSignatureImage(PictureBox1, _pictureBoxSolid.Clone())
                End If
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        import_signatureImage()
    End Sub
    Private Sub PDFField_BackgroundColorPicker_Click(sender As Object, e As EventArgs) Handles PDFField_BackgroundColorPicker.Click
        Try
            If frm Is Nothing Then Return
            frm.cUserRect.pauseDraw = True
            Dim ColorDialog1 As New ColorDialog
            ColorDialog1.SolidColorOnly = True
            If backGroundColor = Color.Transparent Then
                ColorDialog1.Color = Color.White
            Else
                ColorDialog1.Color = backGroundColor
            End If
            ColorDialog1.FullOpen = False
            Me.BringToFront()
            Select Case ColorDialog1.ShowDialog(Me)
                Case Windows.Forms.DialogResult.OK, Windows.Forms.DialogResult.Yes
                    backGroundColor = System.Drawing.Color.FromArgb(ColorDialog1.Color.A, ColorDialog1.Color.R, ColorDialog1.Color.G, ColorDialog1.Color.B)
                    PDFField_BackgroundColorPicker_R.Text = PDFField_BackgroundColorPicker.BackColor.R
                    PDFField_BackgroundColorPicker_G.Text = PDFField_BackgroundColorPicker.BackColor.G
                    PDFField_BackgroundColorPicker_B.Text = PDFField_BackgroundColorPicker.BackColor.B
                    PDFField_BackgroundColorPicker_A.Text = PDFField_BackgroundColorPicker.BackColor.A
                    If PDFField_BackgroundColorPicker.BackColor.A <= 0 Then
                        PDFField_BackgroundColorPicker_Transparent.Visible = False
                    Else
                        PDFField_BackgroundColorPicker_Transparent.Visible = True
                    End If
                    frm.PDFField_BackgroundColorPicker.BackColor = backGroundColor
                Case Else
            End Select
        Catch ex As Exception
            frm.TimeStampAdd(ex, frm.debugMode)
        End Try
        frm.cUserRect.pauseDraw = False
    End Sub
    Private Sub PDFField_BackgroundColorPicker_Transparent_Click(sender As Object, e As EventArgs) Handles PDFField_BackgroundColorPicker_Transparent.Click
        Try
            backGroundColor = Color.Transparent
            frm.cUserRect.pauseDraw = True
            PDFField_BackgroundColorPicker_A.Text = backGroundColor.A
            PDFField_BackgroundColorPicker_R.Text = backGroundColor.R
            PDFField_BackgroundColorPicker_G.Text = backGroundColor.G
            PDFField_BackgroundColorPicker_B.Text = backGroundColor.B
            PDFField_BackgroundColorPicker_Transparent.Visible = False
            frm.cUserRect.pauseDraw = False
        Catch ex As Exception
            frm.TimeStampAdd(ex, frm.debugMode)
        End Try
    End Sub
    Private Sub PDFField_BackgroundColorPicker_R_TextChanged(sender As Object, e As EventArgs) Handles PDFField_BackgroundColorPicker_R.TextChanged
        Try
            If frm Is Nothing Then Return
            If frm.cUserRect.pauseDraw Then Return
            If IsNumeric(PDFField_BackgroundColorPicker_R.Text & "") And IsNumeric(PDFField_BackgroundColorPicker_G.Text & "") And IsNumeric(PDFField_BackgroundColorPicker_B.Text & "") And IsNumeric(PDFField_BackgroundColorPicker_A.Text & "") Then
                If CInt(PDFField_BackgroundColorPicker_R.Text & "") >= 0 And CInt(PDFField_BackgroundColorPicker_G.Text & "") >= 0 And CInt(PDFField_BackgroundColorPicker_B.Text & "") >= 0 And CInt(PDFField_BackgroundColorPicker_A.Text & "") >= 0 Then
                    If CInt(PDFField_BackgroundColorPicker_R.Text & "") <= 255 And CInt(PDFField_BackgroundColorPicker_G.Text & "") <= 255 And CInt(PDFField_BackgroundColorPicker_B.Text & "") <= 255 And CInt(PDFField_BackgroundColorPicker_A.Text & "") <= 255 Then
                        PDFField_BackgroundColorPicker.BackColor = System.Drawing.Color.FromArgb(CInt(PDFField_BackgroundColorPicker_A.Text & ""), CInt(PDFField_BackgroundColorPicker_R.Text & ""), CInt(PDFField_BackgroundColorPicker_G.Text & ""), CInt(PDFField_BackgroundColorPicker_B.Text & ""))
                        Return
                    End If
                End If
                If CInt(PDFField_BackgroundColorPicker_R.Text & "") < 0 Then
                    PDFField_BackgroundColorPicker_R.Text = "0"
                End If
                If CInt(PDFField_BackgroundColorPicker_G.Text & "") < 0 Then
                    PDFField_BackgroundColorPicker_G.Text = "0"
                End If
                If CInt(PDFField_BackgroundColorPicker_B.Text & "") < 0 Then
                    PDFField_BackgroundColorPicker_B.Text = "0"
                End If
                If CInt(PDFField_BackgroundColorPicker_A.Text & "") < 0 Then
                    PDFField_BackgroundColorPicker_A.Text = "0"
                End If
                If CInt(PDFField_BackgroundColorPicker_R.Text & "") > 255 Then
                    PDFField_BackgroundColorPicker_R.Text = "255"
                End If
                If CInt(PDFField_BackgroundColorPicker_G.Text & "") > 255 Then
                    PDFField_BackgroundColorPicker_G.Text = "255"
                End If
                If CInt(PDFField_BackgroundColorPicker_B.Text & "") > 255 Then
                    PDFField_BackgroundColorPicker_B.Text = "255"
                End If
                If CInt(PDFField_BackgroundColorPicker_A.Text & "") > 255 Then
                    PDFField_BackgroundColorPicker_A.Text = "255"
                End If
            End If
        Catch ex As Exception
            frm.TimeStampAdd(ex, frm.debugMode)
        End Try
    End Sub
    Private Sub PDFField_BackgroundColorPicker_G_TextChanged(sender As Object, e As EventArgs) Handles PDFField_BackgroundColorPicker_G.TextChanged
        Try
            If frm Is Nothing Then Return
            If frm.cUserRect.pauseDraw Then Return
            If IsNumeric(PDFField_BackgroundColorPicker_R.Text & "") And IsNumeric(PDFField_BackgroundColorPicker_G.Text & "") And IsNumeric(PDFField_BackgroundColorPicker_B.Text & "") And IsNumeric(PDFField_BackgroundColorPicker_A.Text & "") Then
                If CInt(PDFField_BackgroundColorPicker_R.Text & "") >= 0 And CInt(PDFField_BackgroundColorPicker_G.Text & "") >= 0 And CInt(PDFField_BackgroundColorPicker_B.Text & "") >= 0 And CInt(PDFField_BackgroundColorPicker_A.Text & "") >= 0 Then
                    If CInt(PDFField_BackgroundColorPicker_R.Text & "") <= 255 And CInt(PDFField_BackgroundColorPicker_G.Text & "") <= 255 And CInt(PDFField_BackgroundColorPicker_B.Text & "") <= 255 And CInt(PDFField_BackgroundColorPicker_A.Text & "") <= 255 Then
                        PDFField_BackgroundColorPicker.BackColor = System.Drawing.Color.FromArgb(CInt(PDFField_BackgroundColorPicker_A.Text & ""), CInt(PDFField_BackgroundColorPicker_R.Text & ""), CInt(PDFField_BackgroundColorPicker_G.Text & ""), CInt(PDFField_BackgroundColorPicker_B.Text & ""))
                        Return
                    End If
                End If
                If CInt(PDFField_BackgroundColorPicker_R.Text & "") < 0 Then
                    PDFField_BackgroundColorPicker_R.Text = "0"
                End If
                If CInt(PDFField_BackgroundColorPicker_G.Text & "") < 0 Then
                    PDFField_BackgroundColorPicker_G.Text = "0"
                End If
                If CInt(PDFField_BackgroundColorPicker_B.Text & "") < 0 Then
                    PDFField_BackgroundColorPicker_B.Text = "0"
                End If
                If CInt(PDFField_BackgroundColorPicker_A.Text & "") < 0 Then
                    PDFField_BackgroundColorPicker_A.Text = "0"
                End If
                If CInt(PDFField_BackgroundColorPicker_R.Text & "") > 255 Then
                    PDFField_BackgroundColorPicker_R.Text = "255"
                End If
                If CInt(PDFField_BackgroundColorPicker_G.Text & "") > 255 Then
                    PDFField_BackgroundColorPicker_G.Text = "255"
                End If
                If CInt(PDFField_BackgroundColorPicker_B.Text & "") > 255 Then
                    PDFField_BackgroundColorPicker_B.Text = "255"
                End If
                If CInt(PDFField_BackgroundColorPicker_A.Text & "") > 255 Then
                    PDFField_BackgroundColorPicker_A.Text = "255"
                End If
            End If
        Catch ex As Exception
            frm.TimeStampAdd(ex, frm.debugMode)
        End Try
    End Sub
    Private Sub PDFField_BackgroundColorPicker_B_TextChanged(sender As Object, e As EventArgs) Handles PDFField_BackgroundColorPicker_B.TextChanged
        Try
            If frm Is Nothing Then Return
            If frm.cUserRect.pauseDraw Then Return
            If IsNumeric(PDFField_BackgroundColorPicker_R.Text & "") And IsNumeric(PDFField_BackgroundColorPicker_G.Text & "") And IsNumeric(PDFField_BackgroundColorPicker_B.Text & "") And IsNumeric(PDFField_BackgroundColorPicker_A.Text & "") Then
                If CInt(PDFField_BackgroundColorPicker_R.Text & "") >= 0 And CInt(PDFField_BackgroundColorPicker_G.Text & "") >= 0 And CInt(PDFField_BackgroundColorPicker_B.Text & "") >= 0 And CInt(PDFField_BackgroundColorPicker_A.Text & "") >= 0 Then
                    If CInt(PDFField_BackgroundColorPicker_R.Text & "") <= 255 And CInt(PDFField_BackgroundColorPicker_G.Text & "") <= 255 And CInt(PDFField_BackgroundColorPicker_B.Text & "") <= 255 And CInt(PDFField_BackgroundColorPicker_A.Text & "") <= 255 Then
                        PDFField_BackgroundColorPicker.BackColor = System.Drawing.Color.FromArgb(CInt(PDFField_BackgroundColorPicker_A.Text & ""), CInt(PDFField_BackgroundColorPicker_R.Text & ""), CInt(PDFField_BackgroundColorPicker_G.Text & ""), CInt(PDFField_BackgroundColorPicker_B.Text & ""))
                        Return
                    End If
                End If
                If CInt(PDFField_BackgroundColorPicker_R.Text & "") < 0 Then
                    PDFField_BackgroundColorPicker_R.Text = "0"
                End If
                If CInt(PDFField_BackgroundColorPicker_G.Text & "") < 0 Then
                    PDFField_BackgroundColorPicker_G.Text = "0"
                End If
                If CInt(PDFField_BackgroundColorPicker_B.Text & "") < 0 Then
                    PDFField_BackgroundColorPicker_B.Text = "0"
                End If
                If CInt(PDFField_BackgroundColorPicker_A.Text & "") < 0 Then
                    PDFField_BackgroundColorPicker_A.Text = "0"
                End If
                If CInt(PDFField_BackgroundColorPicker_R.Text & "") > 255 Then
                    PDFField_BackgroundColorPicker_R.Text = "255"
                End If
                If CInt(PDFField_BackgroundColorPicker_G.Text & "") > 255 Then
                    PDFField_BackgroundColorPicker_G.Text = "255"
                End If
                If CInt(PDFField_BackgroundColorPicker_B.Text & "") > 255 Then
                    PDFField_BackgroundColorPicker_B.Text = "255"
                End If
                If CInt(PDFField_BackgroundColorPicker_A.Text & "") > 255 Then
                    PDFField_BackgroundColorPicker_A.Text = "255"
                End If
            End If
        Catch ex As Exception
            frm.TimeStampAdd(ex, frm.debugMode)
        End Try
    End Sub
    Private Sub PDFField_BackgroundColorPicker_A_TextChanged(sender As Object, e As EventArgs) Handles PDFField_BackgroundColorPicker_A.TextChanged
        Try
            If frm Is Nothing Then Return
            If frm.cUserRect.pauseDraw Then Return
            If IsNumeric(PDFField_BackgroundColorPicker_R.Text & "") And IsNumeric(PDFField_BackgroundColorPicker_G.Text & "") And IsNumeric(PDFField_BackgroundColorPicker_B.Text & "") And IsNumeric(PDFField_BackgroundColorPicker_A.Text & "") Then
                If CInt(PDFField_BackgroundColorPicker_R.Text & "") >= 0 And CInt(PDFField_BackgroundColorPicker_G.Text & "") >= 0 And CInt(PDFField_BackgroundColorPicker_B.Text & "") >= 0 And CInt(PDFField_BackgroundColorPicker_A.Text & "") >= 0 Then
                    If CInt(PDFField_BackgroundColorPicker_R.Text & "") <= 255 And CInt(PDFField_BackgroundColorPicker_G.Text & "") <= 255 And CInt(PDFField_BackgroundColorPicker_B.Text & "") <= 255 And CInt(PDFField_BackgroundColorPicker_A.Text & "") <= 255 Then
                        PDFField_BackgroundColorPicker.BackColor = System.Drawing.Color.FromArgb(CInt(PDFField_BackgroundColorPicker_A.Text & ""), CInt(PDFField_BackgroundColorPicker_R.Text & ""), CInt(PDFField_BackgroundColorPicker_G.Text & ""), CInt(PDFField_BackgroundColorPicker_B.Text & ""))
                        Return
                    End If
                End If
                If CInt(PDFField_BackgroundColorPicker_R.Text & "") < 0 Then
                    PDFField_BackgroundColorPicker_R.Text = "0"
                End If
                If CInt(PDFField_BackgroundColorPicker_G.Text & "") < 0 Then
                    PDFField_BackgroundColorPicker_G.Text = "0"
                End If
                If CInt(PDFField_BackgroundColorPicker_B.Text & "") < 0 Then
                    PDFField_BackgroundColorPicker_B.Text = "0"
                End If
                If CInt(PDFField_BackgroundColorPicker_A.Text & "") < 0 Then
                    PDFField_BackgroundColorPicker_A.Text = "0"
                End If
                If CInt(PDFField_BackgroundColorPicker_R.Text & "") > 255 Then
                    PDFField_BackgroundColorPicker_R.Text = "255"
                End If
                If CInt(PDFField_BackgroundColorPicker_G.Text & "") > 255 Then
                    PDFField_BackgroundColorPicker_G.Text = "255"
                End If
                If CInt(PDFField_BackgroundColorPicker_B.Text & "") > 255 Then
                    PDFField_BackgroundColorPicker_B.Text = "255"
                End If
                If CInt(PDFField_BackgroundColorPicker_A.Text & "") > 255 Then
                    PDFField_BackgroundColorPicker_A.Text = "255"
                End If
            End If
        Catch ex As Exception
            frm.TimeStampAdd(ex, frm.debugMode)
        End Try
    End Sub
    Private Sub PDFField_BackgroundColorPicker_BackColorChanged(sender As Object, e As EventArgs) Handles PDFField_BackgroundColorPicker.BackColorChanged
        Try
            If frm Is Nothing Then Return
            Dim blnPause As Boolean = frm.cUserRect.pauseDraw
            If PDFField_BackgroundColorPicker.BackColor.A > 0 Then
                PDFField_BackgroundColorPicker_Transparent.Visible = True
            Else
                PDFField_BackgroundColorPicker_Transparent.Visible = False
            End If
            frm.cUserRect.pauseDraw = True
            PDFField_BackgroundColorPicker_R.Text = PDFField_BackgroundColorPicker.BackColor.R
            PDFField_BackgroundColorPicker_G.Text = PDFField_BackgroundColorPicker.BackColor.G
            PDFField_BackgroundColorPicker_B.Text = PDFField_BackgroundColorPicker.BackColor.B
            PDFField_BackgroundColorPicker_A.Text = PDFField_BackgroundColorPicker.BackColor.A
            frm.cUserRect.pauseDraw = blnPause
        Catch ex As Exception
            frm.TimeStampAdd(ex, frm.debugMode)
        End Try
    End Sub
End Class
