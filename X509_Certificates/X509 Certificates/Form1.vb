Imports System.Security.Cryptography.X509Certificates
Imports System.IO
Imports System.Text
Public Class Form1
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(48, 64)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(136, 40)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "&X.509  Properties"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.Location = New System.Drawing.Point(40, 8)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(168, 24)
        Me.LinkLabel1.TabIndex = 1
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "adahmed911@hotmail.com"
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(256, 142)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Button1)
        Me.Name = "Form1"
        Me.Text = "X509 Demo"
        Me.ResumeLayout(False)

    End Sub

#End Region
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'define certificate to access
        Dim Cert As X509Certificate = X509Certificate.CreateFromCertFile(Directory.GetCurrentDirectory & "\Adnan.cer")
        'Now reterive its properties in output window
        ' Get the value.
        Dim resultsTrue As String = Cert.ToString(True)
        ' Display the value 
        Debug.WriteLine(resultsTrue)
        Dim SerialNumber() As Byte = Cert.GetSerialNumber()
        Dim Sr As Byte
        Debug.Write("Serial Number in Bytes: ")
        For Each Sr In SerialNumber
            Debug.Write(Sr)
        Next
    End Sub
End Class
