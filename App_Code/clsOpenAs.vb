Imports System.Runtime.InteropServices
Imports System.ComponentModel
Imports System.Drawing

Public Class clsOpenAs

    Private btnBrowseImg As System.Windows.Forms.Button

    Private openFileDialog1 As System.Windows.Forms.OpenFileDialog

    Public pictureBox1 As System.Windows.Forms.PictureBox

    <Serializable>
    Public Structure ShellExecuteInfo

        Public Size As Integer

        Public Mask As UInteger

        Public hwnd As IntPtr

        Public Verb As String

        Public File As String

        Public Parameters As String

        Public Directory As String

        Public Show As UInteger

        Public InstApp As IntPtr

        Public IDList As IntPtr

        Public [Class] As String

        Public hkeyClass As IntPtr

        Public HotKey As UInteger

        Public Icon As IntPtr

        Public Monitor As IntPtr
    End Structure

    <DllImport("shell32.dll", SetLastError:=True)>
    Public Shared Function ShellExecuteEx(ByRef lpExecInfo As ShellExecuteInfo) As Boolean
    End Function

    Public Const SW_NORMAL As UInteger = 1

    Public Shared Function OpenAs(ByVal file As String) As Boolean
        Try
            Dim sei As ShellExecuteInfo = New ShellExecuteInfo()
            sei.Size = Marshal.SizeOf(sei)
            sei.Verb = "openas"
            sei.File = file
            sei.Show = SW_NORMAL
            If Not ShellExecuteEx(sei) Then Throw New System.ComponentModel.Win32Exception()
            Return True
        Catch ex As Exception
            Err.Clear()
        End Try
        Return False
    End Function
End Class

