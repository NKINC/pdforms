Public NotInheritable Class Utilities
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 Nicholas Kowalewicz All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>

    Public Class NativeMethods
        <System.Runtime.InteropServices.ComImport()>
        <System.Runtime.InteropServices.Guid("0000010D-0000-0000-C000-000000000046")>
        <System.Runtime.InteropServices.InterfaceType(System.Runtime.InteropServices.ComInterfaceType.InterfaceIsIUnknown)>
        Private Interface IViewObject
            Sub Draw(<System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U4)> ByVal dwAspect As UInteger, ByVal lindex As Integer, ByVal pvAspect As IntPtr, <System.Runtime.InteropServices.In()> ByVal ptd As IntPtr, ByVal hdcTargetDev As IntPtr, ByVal hdcDraw As IntPtr,
             <System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Struct)> ByRef lprcBounds As RECT, <System.Runtime.InteropServices.In()> ByVal lprcWBounds As IntPtr, ByVal pfnContinue As IntPtr, <System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U4)> ByVal dwContinue As UInteger)
        End Interface
        <System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack:=4)>
        Private Structure RECT
            Public Left As Integer
            Public Top As Integer
            Public Right As Integer
            Public Bottom As Integer
        End Structure
        ''' <summary>
        ''' Usage: Dim bitmap As New Bitmap(wb.Width, wb.Height); Utilities.NativeMethods.GetImage(wb.ActiveXInstance, bitmap, Color.Transparent);
        ''' </summary>
        ''' <param name="obj">webBrowser control</param>
        ''' <param name="destination">bitmap or image</param>
        ''' <param name="backgroundColor">color</param>
        ''' <remarks></remarks>
        Public Shared Sub GetImage(ByRef obj As WebBrowser, ByRef destination As Image, ByVal backgroundColor As System.Drawing.Color)
            Using graphics__1 As Graphics = Graphics.FromImage(destination)
                Dim deviceContextHandle As IntPtr = IntPtr.Zero
                Dim rectangle As New RECT()
                rectangle.Right = destination.Width
                rectangle.Bottom = destination.Height
                graphics__1.Clear(backgroundColor)
                Try
                    deviceContextHandle = graphics__1.GetHdc()

                    Dim viewObject As IViewObject = TryCast(obj.Document.DomDocument, IViewObject)
                    viewObject.Draw(1, -1, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, deviceContextHandle,
                     rectangle, IntPtr.Zero, IntPtr.Zero, 0)
                Finally
                    If deviceContextHandle <> IntPtr.Zero Then
                        graphics__1.ReleaseHdc(deviceContextHandle)
                    End If
                End Try
            End Using
        End Sub
    End Class
    Private Sub New()
    End Sub
    Public Const SRCCOPY As Integer = 13369376
    <System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)>
    Public Structure RECT
        Public left As Integer
        Public top As Integer
        Public right As Integer
        Public bottom As Integer
    End Structure
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="webBrowserControl"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DrawToImage(ByVal webBrowserControl As WebBrowser) As Image
        Return CaptureWindow(webBrowserControl.Handle)
    End Function
    Public Shared Function CaptureScreen() As Image
        Return CaptureWindow(GetDesktopWindow())
    End Function
    Public Shared Function CaptureWindow(ByVal handle As IntPtr) As Image
        Dim hdcSrc As IntPtr = GetWindowDC(handle)
        Dim windowRect As New RECT()
        GetWindowRect(handle, windowRect)
        Dim width As Integer = windowRect.right - windowRect.left
        Dim height As Integer = windowRect.bottom - windowRect.top
        Dim hdcDest As IntPtr = CreateCompatibleDC(hdcSrc)
        Dim hBitmap As IntPtr = CreateCompatibleBitmap(hdcSrc, width, height)
        Dim hOld As IntPtr = SelectObject(hdcDest, hBitmap)
        BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, SRCCOPY)
        SelectObject(hdcDest, hOld)
        DeleteDC(hdcDest)
        ReleaseDC(handle, hdcSrc)
        Dim image__1 As Image = Image.FromHbitmap(hBitmap)
        DeleteObject(hBitmap)
        Return image__1
    End Function
    <System.Runtime.InteropServices.DllImport("gdi32.dll")>
    Public Shared Function BitBlt(ByVal hObject As IntPtr, ByVal nXDest As Integer, ByVal nYDest As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer, ByVal hObjectSource As IntPtr,
         ByVal nXSrc As Integer, ByVal nYSrc As Integer, ByVal dwRop As Integer) As Boolean
    End Function
    <System.Runtime.InteropServices.DllImport("gdi32.dll")>
    Public Shared Function CreateCompatibleBitmap(ByVal hDC As IntPtr, ByVal nWidth As Integer, ByVal nHeight As Integer) As IntPtr
    End Function
    <System.Runtime.InteropServices.DllImport("gdi32.dll")>
    Public Shared Function CreateCompatibleDC(ByVal hDC As IntPtr) As IntPtr
    End Function
    <System.Runtime.InteropServices.DllImport("gdi32.dll")>
    Public Shared Function DeleteDC(ByVal hDC As IntPtr) As Boolean
    End Function
    <System.Runtime.InteropServices.DllImport("gdi32.dll")>
    Public Shared Function DeleteObject(ByVal hObject As IntPtr) As Boolean
    End Function
    <System.Runtime.InteropServices.DllImport("gdi32.dll")>
    Public Shared Function SelectObject(ByVal hDC As IntPtr, ByVal hObject As IntPtr) As IntPtr
    End Function
    <System.Runtime.InteropServices.DllImport("user32.dll")>
    Public Shared Function GetDesktopWindow() As IntPtr
    End Function
    <System.Runtime.InteropServices.DllImport("user32.dll")>
    Public Shared Function GetWindowDC(ByVal hWnd As IntPtr) As IntPtr
    End Function
    <System.Runtime.InteropServices.DllImport("user32.dll")>
    Public Shared Function GetWindowRect(ByVal hWnd As IntPtr, ByRef rect As RECT) As IntPtr
    End Function
    <System.Runtime.InteropServices.DllImport("user32.dll")>
    Public Shared Function ReleaseDC(ByVal hWnd As IntPtr, ByVal hDC As IntPtr) As IntPtr
    End Function

End Class
