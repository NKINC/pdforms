<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class dialogBarCodesZXING
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.picBarcode = New System.Windows.Forms.PictureBox()
        Me.txtTextToEncode = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chkPurBarCodeOnly = New System.Windows.Forms.CheckBox()
        Me.cmbSymbology = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtMargin = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtBarcodeWidth = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtBarcodeHeight = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.picBarcode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(465, 401)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'picBarcode
        '
        Me.picBarcode.Location = New System.Drawing.Point(20, 201)
        Me.picBarcode.Name = "picBarcode"
        Me.picBarcode.Size = New System.Drawing.Size(593, 197)
        Me.picBarcode.TabIndex = 3
        Me.picBarcode.TabStop = False
        '
        'txtTextToEncode
        '
        Me.txtTextToEncode.Location = New System.Drawing.Point(20, 101)
        Me.txtTextToEncode.Multiline = True
        Me.txtTextToEncode.Name = "txtTextToEncode"
        Me.txtTextToEncode.Size = New System.Drawing.Size(593, 94)
        Me.txtTextToEncode.TabIndex = 17
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 13)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Symbology:"
        '
        'chkPurBarCodeOnly
        '
        Me.chkPurBarCodeOnly.AutoSize = True
        Me.chkPurBarCodeOnly.Checked = True
        Me.chkPurBarCodeOnly.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPurBarCodeOnly.Location = New System.Drawing.Point(268, 27)
        Me.chkPurBarCodeOnly.Name = "chkPurBarCodeOnly"
        Me.chkPurBarCodeOnly.Size = New System.Drawing.Size(155, 17)
        Me.chkPurBarCodeOnly.TabIndex = 16
        Me.chkPurBarCodeOnly.Text = "PureBar Code only (no text)"
        Me.chkPurBarCodeOnly.UseVisualStyleBackColor = True
        '
        'cmbSymbology
        '
        Me.cmbSymbology.FormattingEnabled = True
        Me.cmbSymbology.Items.AddRange(New Object() {"PDF 417", "QR Code", "Data Matrix"})
        Me.cmbSymbology.Location = New System.Drawing.Point(18, 23)
        Me.cmbSymbology.Name = "cmbSymbology"
        Me.cmbSymbology.Size = New System.Drawing.Size(231, 21)
        Me.cmbSymbology.TabIndex = 15
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(17, 85)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 13)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "Text to encode:"
        '
        'txtMargin
        '
        Me.txtMargin.Location = New System.Drawing.Point(148, 62)
        Me.txtMargin.Name = "txtMargin"
        Me.txtMargin.Size = New System.Drawing.Size(58, 20)
        Me.txtMargin.TabIndex = 19
        Me.txtMargin.Text = "0"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(145, 47)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(42, 13)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "Margin:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(17, 47)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(38, 13)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "Width:"
        '
        'txtBarcodeWidth
        '
        Me.txtBarcodeWidth.Location = New System.Drawing.Point(20, 62)
        Me.txtBarcodeWidth.Name = "txtBarcodeWidth"
        Me.txtBarcodeWidth.Size = New System.Drawing.Size(58, 20)
        Me.txtBarcodeWidth.TabIndex = 21
        Me.txtBarcodeWidth.Text = "-1"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(81, 47)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(41, 13)
        Me.Label5.TabIndex = 24
        Me.Label5.Text = "Height:"
        '
        'txtBarcodeHeight
        '
        Me.txtBarcodeHeight.Location = New System.Drawing.Point(84, 62)
        Me.txtBarcodeHeight.Name = "txtBarcodeHeight"
        Me.txtBarcodeHeight.Size = New System.Drawing.Size(58, 20)
        Me.txtBarcodeHeight.TabIndex = 23
        Me.txtBarcodeHeight.Text = "-1"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(17, 414)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(39, 13)
        Me.Label6.TabIndex = 25
        Me.Label6.Text = "Label6"
        '
        'dialogBarCodesZXING
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(623, 442)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtBarcodeHeight)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtBarcodeWidth)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtMargin)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtTextToEncode)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.chkPurBarCodeOnly)
        Me.Controls.Add(Me.cmbSymbology)
        Me.Controls.Add(Me.picBarcode)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dialogBarCodesZXING"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Barcodes:"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.picBarcode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents picBarcode As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Public WithEvents txtTextToEncode As TextBox
    Public WithEvents chkPurBarCodeOnly As CheckBox
    Public WithEvents cmbSymbology As ComboBox
    Public WithEvents txtMargin As TextBox
    Public WithEvents txtBarcodeWidth As TextBox
    Public WithEvents txtBarcodeHeight As TextBox
    Friend WithEvents Label6 As Label
End Class
