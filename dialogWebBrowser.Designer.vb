<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dialogWebBrowser
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Print_Button = New System.Windows.Forms.Button()
        Me.Import_Button = New System.Windows.Forms.Button()
        Me.SaveAs_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.textboxURL = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 5
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Print_Button, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Import_Button, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.SaveAs_Button, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 4, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(624, 496)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(376, 29)
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
        'Print_Button
        '
        Me.Print_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Print_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Print_Button.Location = New System.Drawing.Point(222, 3)
        Me.Print_Button.Name = "Print_Button"
        Me.Print_Button.Size = New System.Drawing.Size(67, 23)
        Me.Print_Button.TabIndex = 3
        Me.Print_Button.Text = "Print"
        '
        'Import_Button
        '
        Me.Import_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Import_Button.Location = New System.Drawing.Point(76, 3)
        Me.Import_Button.Name = "Import_Button"
        Me.Import_Button.Size = New System.Drawing.Size(67, 23)
        Me.Import_Button.TabIndex = 4
        Me.Import_Button.Text = "Import"
        '
        'SaveAs_Button
        '
        Me.SaveAs_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.SaveAs_Button.Location = New System.Drawing.Point(149, 3)
        Me.SaveAs_Button.Name = "SaveAs_Button"
        Me.SaveAs_Button.Size = New System.Drawing.Size(67, 23)
        Me.SaveAs_Button.TabIndex = 2
        Me.SaveAs_Button.Text = "Save As"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(300, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.WebBrowser1.Location = New System.Drawing.Point(0, 0)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.Size = New System.Drawing.Size(1008, 488)
        Me.WebBrowser1.TabIndex = 1
        '
        'textboxURL
        '
        Me.textboxURL.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.textboxURL.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.textboxURL.Location = New System.Drawing.Point(8, 496)
        Me.textboxURL.Name = "textboxURL"
        Me.textboxURL.Size = New System.Drawing.Size(590, 29)
        Me.textboxURL.TabIndex = 2
        '
        'dialogWebBrowser
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(1006, 533)
        Me.Controls.Add(Me.textboxURL)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.WebBrowser1)
        Me.Name = "dialogWebBrowser"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Built-in Web Browser:"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents WebBrowser1 As System.Windows.Forms.WebBrowser
    Friend WithEvents textboxURL As System.Windows.Forms.TextBox
    Friend WithEvents SaveAs_Button As System.Windows.Forms.Button
    Friend WithEvents Print_Button As System.Windows.Forms.Button
    Friend WithEvents Import_Button As System.Windows.Forms.Button

End Class
