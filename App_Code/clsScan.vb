Imports System
Imports System.Collections.Generic
Imports System.Collections
Imports System.IO
Imports System.Drawing
Imports WIA
Public Class clsScan
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 NK-INC.COM All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>
    Public Enum colorMode
        BW = 0
        Color = 1
        Grayscale = 2
    End Enum
    Public Enum DPI
        LOW = 75
        MID = 100
        HIGH = 200
        MAX = 300
    End Enum
    Class ScanSettings
        Public dpi As Integer = 150
        Public color As Integer = colorMode.Grayscale
        Public adf As Boolean = False
        Public tryFlatbed As Boolean = True
        Public widthInches As Single = 8.5F
        Public heightInches As Single = 11.0F
        Public WithEvents frmMain1 As frmMain = Nothing
        Public Sub New(ByVal dpi1 As Integer, ByVal colorMode1 As colorMode, ByVal adf1 As Boolean, ByVal flatbed As Boolean)
            dpi = dpi1
            color = colorMode1
            adf = adf1
            tryFlatbed = flatbed
        End Sub
        Public Sub New(ByVal dpi1 As Integer, ByVal colorMode1 As colorMode, ByVal adf1 As Boolean, ByVal flatbed As Boolean, ByVal widthInch As Single, ByVal heightInch As Single)
            dpi = dpi1
            color = colorMode1
            adf = adf1
            tryFlatbed = flatbed
            widthInches = widthInch
            heightInches = heightInch
        End Sub
        Public Sub New(ByRef frm As frmMain, ByVal dpi1 As Integer, ByVal colorMode1 As colorMode, ByVal adf1 As Boolean, ByVal flatbed As Boolean, ByVal widthInch As Single, ByVal heightInch As Single)
            frmMain1 = frm
            dpi = dpi1
            color = colorMode1
            adf = adf1
            tryFlatbed = flatbed
            widthInches = widthInch
            heightInches = heightInch
        End Sub
    End Class
    Public frm As frmMain
    Public Sub New(frm1 As frmMain)
        frm = frm1
    End Sub
    Public hasMorePages As Boolean = True
    Public horizontal_exent As Single = 8.5
    Public vertical_exent As Single = 11
    Public Shared Function Scan(ByRef frmMain1 As frmMain, Optional ByVal useAutoDocumentFeeder As Boolean = False, Optional ByVal tryFlatBed As Boolean = True, Optional ByVal colorMode1 As colorMode = colorMode.Grayscale, Optional ByVal dpi As Integer = 300, Optional ByVal widthInches As Single = 8.5F, Optional ByVal heightInches As Single = 11.0F) As List(Of Image)
        Dim ss As New ScanSettings(frmMain1, dpi, colorMode1, useAutoDocumentFeeder, tryFlatBed, widthInches, heightInches)
        Return WIAScanner.Scan(ss)







    End Function













    Class WIAScanner

        Const wiaFormatBMP As String = "{B96B3CAB-0728-11D3-9D7B-0000F81EF32E}"
        Const wiaFormatJPG As String = "{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}"

        Private Class WIA_DPS_DOCUMENT_HANDLING_SELECT
            Public Const FEEDER As Integer = &H1
            Public Const FLATBED As Integer = &H2
            Public Const DUPLEX As Integer = &H4
            Public Const FRONT_FIRST As Integer = &H8
            Public Const BACK_FIRST As Integer = &H10
            Public Const FRONT_ONLY As Integer = &H20
            Public Const BACK_ONLY As Integer = &H40
            Public Const NEXT_PAGE As Integer = &H80
            Public Const PREFEED As Integer = &H100
            Public Const AUTO_ADVANCE As Integer = &H200
        End Class

        Private Class WIA_DPS_DOCUMENT_HANDLING_STATUS
            Public Const FEED_READY As Integer = &H1
            Public Const FLAT_READY As Integer = &H2
            Public Const DUP_READY As Integer = &H4
            Public Const FLAT_COVER_UP As Integer = &H8
            Public Const PATH_COVER_UP As Integer = &H10
            Public Const PAPER_JAM As Integer = &H20
        End Class

        Private Class WIA_ERRORS
            Public Const BASE_VAL_WIA_ERROR As UInteger = &H80210000UI
            Public Const WIA_ERROR_GENERAL_ERROR As UInteger = BASE_VAL_WIA_ERROR + 1
            Public Const WIA_ERROR_PAPER_JAM As UInteger = BASE_VAL_WIA_ERROR + 2
            Public Const WIA_ERROR_PAPER_EMPTY As UInteger = BASE_VAL_WIA_ERROR + 3
            Public Const WIA_ERROR_BUSY As UInteger = BASE_VAL_WIA_ERROR + 6
        End Class

        Private Class WIA_PROPERTIES
            Public Const WIA_RESERVED_FOR_NEW_PROPS As Integer = 1024
            Public Const WIA_DIP_FIRST As Integer = 2
            Public Const WIA_DPA_FIRST As Integer = WIA_DIP_FIRST + WIA_RESERVED_FOR_NEW_PROPS
            Public Const WIA_DPC_FIRST As Integer = WIA_DPA_FIRST + WIA_RESERVED_FOR_NEW_PROPS
            Public Const WIA_DPS_FIRST As Integer = WIA_DPC_FIRST + WIA_RESERVED_FOR_NEW_PROPS
            Public Const WIA_DPS_DOCUMENT_HANDLING_STATUS As Integer = WIA_DPS_FIRST + 13
            Public Const WIA_DPS_DOCUMENT_HANDLING_SELECT As Integer = WIA_DPS_FIRST + 14
            Public Const WIA_DPS_PAGES As Integer = WIA_DPS_FIRST + 22
        End Class


        Public Shared deviceCurrentShared As Device = Nothing
        Public Shared Function Scan(settings As ScanSettings) As List(Of Image)
            Dim dialog As ICommonDialog = New CommonDialog()
            Try
                If deviceCurrentShared Is Nothing Then
                    deviceCurrentShared = dialog.ShowSelectDevice(WiaDeviceType.ScannerDeviceType, True, False)
                End If
            Catch generatedExceptionName As Exception
                Throw New Exception("Cannot initialize scanner selection window. No WIA scanner installed?")
            End Try

            If deviceCurrentShared IsNot Nothing Then
                Return Acquire(deviceCurrentShared, settings)
            Else
                Throw New Exception("You must first select a WIA scanner.")
            End If
        End Function

        Public Shared Function Scan(settings As ScanSettings, alwaysShowSelectDevices As Boolean) As List(Of Image)
            Dim dialog As ICommonDialog = New CommonDialog()

            Try
                If alwaysShowSelectDevices = Nothing Then
                    alwaysShowSelectDevices = False
                End If
                If alwaysShowSelectDevices = True Or deviceCurrentShared Is Nothing Then
                    deviceCurrentShared = dialog.ShowSelectDevice(WiaDeviceType.ScannerDeviceType, alwaysShowSelectDevices, False)
                End If
            Catch generatedExceptionName As Exception
                Throw New Exception("Cannot initialize scanner selection window. No WIA scanner installed?")
            End Try

            If deviceCurrentShared IsNot Nothing Then
                Return Acquire(deviceCurrentShared, settings)
            Else
                Throw New Exception("You must first select a WIA scanner.")
            End If
        End Function

        Public Shared Function Scan(scannerId As [String], settings As ScanSettings) As List(Of Image)
            Dim manager As New DeviceManager()
            For Each info As DeviceInfo In manager.DeviceInfos
                If info.DeviceID = scannerId Then
                    Try
                        deviceCurrentShared = info.Connect()
                    Catch generatedExceptionName As Exception
                        Throw New Exception("Cannot connect to scanner, please check your device and try again.")
                    End Try

                    Exit For
                End If
            Next

            If deviceCurrentShared Is Nothing Then
                Throw New Exception("The provided scanner device could not be found.")
            End If

            Return Acquire(deviceCurrentShared, settings)
        End Function

        Public Shared Function GetDevices() As Hashtable
            Dim devices As New Hashtable()
            Dim manager As DeviceManager = Nothing
            Try
                manager = New DeviceManager()
            Catch generatedExceptionName As Exception
                Throw New Exception("Cannot initialize WIA device manager." & vbLf & "Make sure wiaaut.dll is present in your system32 directory and that it is registered (run 'regsvr32 wiaaut.dll').")
            End Try

            For Each info As DeviceInfo In manager.DeviceInfos
                Dim name As [String] = info.Properties("Name").Value.ToString()
                devices.Add(info.DeviceID, name)
            Next

            Return devices
        End Function






        Private Shared Sub SetDeviceHandling(ByRef device As Device, settings As ScanSettings)
            Try
                If settings.adf Then
                    SetDeviceIntProperty(device, WIA_PROPERTIES.WIA_DPS_DOCUMENT_HANDLING_SELECT, WIA_DPS_DOCUMENT_HANDLING_SELECT.FEEDER)
                Else
                    SetDeviceIntProperty(device, WIA_PROPERTIES.WIA_DPS_DOCUMENT_HANDLING_SELECT, WIA_DPS_DOCUMENT_HANDLING_SELECT.FLATBED)
                End If
            Catch ex As Exception

            End Try
        End Sub

        Private Shared Sub SetDeviceProperties(ByRef device As Device, settings As ScanSettings)
            Dim scan As Item = TryCast(device.Items(1), Item)
            For Each prop As [Property] In scan.Properties
                Select Case prop.PropertyID
                    Case 6146
                        SetProperty(prop, settings.color)
                        Exit Select
                    Case 6147
                        SetProperty(prop, settings.dpi)
                        Exit Select
                    Case 6148
                        SetProperty(prop, settings.dpi)
                        Exit Select
                    Case 6149
                        SetProperty(prop, 0)
                        Exit Select
                    Case 6150
                        SetProperty(prop, 0)
                        Exit Select
                    Case 6151
                        Try
                            SetProperty(prop, CInt(settings.widthInches * settings.dpi))
                        Catch ex As Exception
                            Err.Clear()
                        End Try

                        Exit Select
                    Case 6152
                        Try
                            SetProperty(prop, settings.heightInches * settings.dpi)
                        Catch ex As Exception
                            Err.Clear()
                        End Try

                        Exit Select
                End Select
            Next
        End Sub



        Public Shared Function Acquire(device As Device, settings As ScanSettings) As List(Of Image)
            Dim description As [String] = device.Properties("Name").Value.ToString()

            If description.ToLower().Contains("brother") OrElse description.Contains("Canon MF4500") Then

                Return AcquireBrother(device, settings)
            Else

                Return AcquireNormal(device, settings)
            End If
        End Function

        Public Shared Function AcquireBrother(device As Device, settings As ScanSettings) As List(Of Image)
            Dim images As New List(Of Image)()
            Dim hasMorePages As Boolean = True
            Dim scan As Item = Nothing

            SetDeviceHandling(device, settings)
            SetDeviceProperties(device, settings)

            Try
                scan = TryCast(device.Items(1), Item)
            Catch generatedExceptionName As Exception
                Throw New Exception("Cannot connect to scanner, please check your device and try again.")
            End Try

            Dim wiaCommonDialog As ICommonDialog = New CommonDialog()
            While hasMorePages


                Try
                    SetDeviceIntProperty(device, WIA_PROPERTIES.WIA_DPS_PAGES, 1)

                    Dim image__1 As ImageFile = DirectCast(wiaCommonDialog.ShowTransfer(scan, wiaFormatBMP, False), ImageFile)

                    If image__1 IsNot Nothing Then
                        Dim imageBytes As [Byte]() = DirectCast(image__1.FileData.get_BinaryData(), Byte())

                        images.Add(Image.FromStream(New MemoryStream(imageBytes)))

                        image__1 = Nothing
                        imageBytes = Nothing
                    Else
                        Exit Try
                    End If

                    hasMorePages = False
                    If settings.adf Then
                        Try
                            Dim status As Integer = GetDeviceIntProperty(device, WIA_PROPERTIES.WIA_DPS_DOCUMENT_HANDLING_STATUS)
                            hasMorePages = (status And WIA_DPS_DOCUMENT_HANDLING_STATUS.FEED_READY) <> 0
                        Catch
                        End Try
                    End If
                Catch ex As System.Runtime.InteropServices.COMException
                    Select Case CUInt(ex.ErrorCode)
                        Case WIA_ERRORS.WIA_ERROR_PAPER_EMPTY
                            If images.Count = 0 AndAlso settings.adf AndAlso settings.tryFlatbed Then
                                SetDeviceIntProperty(device, WIA_PROPERTIES.WIA_DPS_DOCUMENT_HANDLING_SELECT, WIA_DPS_DOCUMENT_HANDLING_SELECT.FLATBED)
                                settings.adf = False
                            Else
                                hasMorePages = False
                            End If
                            Exit Select

                        Case WIA_ERRORS.WIA_ERROR_PAPER_JAM
                            Exit Select

                        Case WIA_ERRORS.WIA_ERROR_BUSY
                            System.Threading.Thread.Sleep(2000)
                            Exit Select
                        Case Else

                            Throw ex
                    End Select
                End Try
            End While

            deviceCurrentShared = Nothing
            Return images
        End Function

        Public Shared Function AcquireNormal(device As Device, settings As ScanSettings) As List(Of Image)
            Dim manager As New DeviceManager()
            Dim images As New List(Of Image)()
            Dim hasMorePages As Boolean = True
            Dim scan As Item = Nothing
            Dim scanBacksideStartIndex As Integer = -1
            Dim wiaCommonDialog As ICommonDialog = New CommonDialog()
            Try
                While hasMorePages
                    Try
                        SetDeviceHandling(device, settings)
                        scan = TryCast(device.Items(1), Item)
                        SetDeviceProperties(device, settings)
                    Catch generatedExceptionName As Exception
                        Throw New Exception("Cannot connect to scanner, please check your device and try again.")
                    End Try




                    Try
                        SetDeviceIntProperty(device, WIA_PROPERTIES.WIA_DPS_PAGES, 1)
                        Dim image__1 As ImageFile = DirectCast(wiaCommonDialog.ShowTransfer(scan, wiaFormatBMP, False), ImageFile)

                        If image__1 IsNot Nothing Then
                            Dim imageBytes As [Byte]() = DirectCast(image__1.FileData.BinaryData(), Byte())


                            images.Add(Image.FromStream(New MemoryStream(imageBytes)))
                            image__1 = Nothing
                            imageBytes = Nothing
                        Else
                            Exit Try
                        End If

                        hasMorePages = False
                        If settings.adf Then
                            Try
                                Dim status As Integer = GetDeviceIntProperty(device, WIA_PROPERTIES.WIA_DPS_DOCUMENT_HANDLING_STATUS)
                                hasMorePages = (status And WIA_DPS_DOCUMENT_HANDLING_STATUS.FEED_READY) <> 0

                            Catch
                            End Try
                        End If
                    Catch ex As System.Runtime.InteropServices.COMException
                        If (Math.Abs(ex.ErrorCode) = 2145320957) Then

                            Dim cDialogMultiChoice As New dialogMultiChoice
                            Dim cButtons As New List(Of dialogMultiChoice.clsButton)
                            cButtons.Add(New dialogMultiChoice.clsButton("FINISHED", True, DialogResult.OK))
                            cButtons.Add(New dialogMultiChoice.clsButton("CONTINUE", True, DialogResult.Retry))
                            If scanBacksideStartIndex < 0 Then
                                cButtons.Add(New dialogMultiChoice.clsButton("FLIP", True, DialogResult.Abort))
                            Else
                                cButtons.Add(New dialogMultiChoice.clsButton("ABORT", False, DialogResult.Abort))
                            End If
                            cButtons.Add(New dialogMultiChoice.clsButton("CANCEL", True, DialogResult.Cancel))
                            cDialogMultiChoice = New dialogMultiChoice
                            Select Case cDialogMultiChoice.ShowDialog(settings.frmMain1, "FINISHED SCANNING?", "ERROR: OUT OF PAPER", cButtons.ToArray())
                                Case DialogResult.OK
                                    hasMorePages = False
                                    Err.Clear()
                                    Exit Try
                                Case DialogResult.Retry
                                    hasMorePages = True
                                    Err.Clear()
                                    Exit Try
                                Case DialogResult.Abort
                                    If scanBacksideStartIndex < 0 Then
                                        Select Case MsgBox("Flip the paper, and click ok when ready...", MsgBoxStyle.OkCancel + MsgBoxStyle.Information + MsgBoxStyle.ApplicationModal, "READY?")
                                            Case MsgBoxResult.Ok
                                                hasMorePages = True
                                                scanBacksideStartIndex = images.Count
                                                Err.Clear()
                                                Exit Try
                                        End Select
                                    End If
                                    hasMorePages = False
                                    scanBacksideStartIndex = -1

                                Case Else
                                    hasMorePages = False
                                    Err.Clear()
                                    Exit Try
                            End Select
                        ElseIf (Math.Abs(ex.ErrorCode) = 2145320954) Then
                            Err.Clear()
                            Dim status As Integer = GetDeviceIntProperty(device, WIA_PROPERTIES.WIA_DPS_DOCUMENT_HANDLING_STATUS)
                            Dim cntr As Integer = 12
                            Do While (status And WIA_DPS_DOCUMENT_HANDLING_STATUS.FEED_READY) <> 0
                                cntr -= 1
                                Threading.Thread.Sleep(5000)
                                If cntr <= 0 Then
                                    Dim cDialogMultiChoice As New dialogMultiChoice
                                    Dim cButtons As New List(Of dialogMultiChoice.clsButton)
                                    cButtons.Add(New dialogMultiChoice.clsButton("FINISHED", True, DialogResult.OK))
                                    cButtons.Add(New dialogMultiChoice.clsButton("CONTINUE", True, DialogResult.Retry))
                                    If scanBacksideStartIndex < 0 Then
                                        cButtons.Add(New dialogMultiChoice.clsButton("FLIP", True, DialogResult.Abort))
                                    Else
                                        cButtons.Add(New dialogMultiChoice.clsButton("ABORT", False, DialogResult.Abort))
                                    End If

                                    cButtons.Add(New dialogMultiChoice.clsButton("CANCEL", True, DialogResult.Cancel))
                                    cDialogMultiChoice = New dialogMultiChoice
                                    Select Case cDialogMultiChoice.ShowDialog(settings.frmMain1, "FINISHED SCANNING?", "ERROR: SCANNER BUSY", cButtons.ToArray())
                                        Case DialogResult.OK
                                            hasMorePages = False
                                            Err.Clear()
                                            Exit Try
                                        Case DialogResult.Retry
                                            hasMorePages = True
                                            Err.Clear()
                                            Exit Try
                                        Case DialogResult.Abort
                                            If scanBacksideStartIndex < 0 Then
                                                Select Case MsgBox("Flip the paper, and click ok when ready...", MsgBoxStyle.OkCancel + MsgBoxStyle.Information + MsgBoxStyle.ApplicationModal, "READY?")
                                                    Case MsgBoxResult.Ok
                                                        hasMorePages = True
                                                        scanBacksideStartIndex = images.Count
                                                        Err.Clear()
                                                        Exit Try
                                                End Select
                                            End If
                                            hasMorePages = False
                                            scanBacksideStartIndex = -1
                                        Case Else
                                            hasMorePages = False
                                            Err.Clear()
                                            Exit Try
                                    End Select
                                    Exit Try
                                End If
                            Loop
                            hasMorePages = True
                            Exit Try
                        ElseIf (Math.Abs(ex.ErrorCode) = 2145320957) Then
                            Dim cDialogMultiChoice As New dialogMultiChoice
                            Dim cButtons As New List(Of dialogMultiChoice.clsButton)
                            cButtons.Add(New dialogMultiChoice.clsButton("FINISHED", True, DialogResult.OK))
                            cButtons.Add(New dialogMultiChoice.clsButton("CONTINUE", True, DialogResult.Retry))
                            If scanBacksideStartIndex < 0 Then
                                cButtons.Add(New dialogMultiChoice.clsButton("FLIP", True, DialogResult.Abort))
                            Else
                                cButtons.Add(New dialogMultiChoice.clsButton("ABORT", False, DialogResult.Abort))
                            End If

                            cButtons.Add(New dialogMultiChoice.clsButton("CANCEL", True, DialogResult.Cancel))
                            cDialogMultiChoice = New dialogMultiChoice
                            Select Case cDialogMultiChoice.ShowDialog(settings.frmMain1, "FINISHED SCANNING?", "ERROR: OUT OF PAPER", cButtons.ToArray())
                                Case DialogResult.OK
                                    hasMorePages = False
                                    Err.Clear()
                                    Exit Try
                                Case DialogResult.Retry
                                    hasMorePages = True
                                    Err.Clear()
                                    Exit Try
                                Case DialogResult.Abort
                                    If scanBacksideStartIndex < 0 Then
                                        Select Case MsgBox("Flip the paper, and click ok when ready...", MsgBoxStyle.OkCancel + MsgBoxStyle.Information + MsgBoxStyle.ApplicationModal, "READY?")
                                            Case MsgBoxResult.Ok
                                                hasMorePages = True
                                                scanBacksideStartIndex = images.Count
                                                Err.Clear()
                                                Exit Try
                                        End Select
                                    End If
                                    hasMorePages = False
                                    scanBacksideStartIndex = -1
                                Case Else
                                    hasMorePages = False
                                    Err.Clear()
                                    Exit Try
                            End Select
                        ElseIf (Math.Abs(ex.ErrorCode) = 2145320954) Then
                            Err.Clear()
                            Dim status As Integer = GetDeviceIntProperty(device, WIA_PROPERTIES.WIA_DPS_DOCUMENT_HANDLING_STATUS)
                            Dim cntr As Integer = 12
                            Do While (status And WIA_DPS_DOCUMENT_HANDLING_STATUS.FEED_READY) <> 0
                                cntr -= 1
                                Threading.Thread.Sleep(5000)
                                If cntr <= 0 Then
                                    Dim cDialogMultiChoice As New dialogMultiChoice
                                    Dim cButtons As New List(Of dialogMultiChoice.clsButton)
                                    cButtons.Add(New dialogMultiChoice.clsButton("FINISHED", True, DialogResult.OK))
                                    cButtons.Add(New dialogMultiChoice.clsButton("CONTINUE", True, DialogResult.Retry))
                                    If scanBacksideStartIndex < 0 Then
                                        cButtons.Add(New dialogMultiChoice.clsButton("FLIP", True, DialogResult.Abort))
                                    Else
                                        cButtons.Add(New dialogMultiChoice.clsButton("ABORT", False, DialogResult.Abort))
                                    End If

                                    cButtons.Add(New dialogMultiChoice.clsButton("CANCEL", True, DialogResult.Cancel))
                                    cDialogMultiChoice = New dialogMultiChoice
                                    Select Case cDialogMultiChoice.ShowDialog(settings.frmMain1, "FINISHED SCANNING?", "ERROR: SCANNER BUSY", cButtons.ToArray())
                                        Case DialogResult.OK
                                            hasMorePages = False
                                            Err.Clear()
                                            Exit Try
                                        Case DialogResult.Retry
                                            hasMorePages = True
                                            Err.Clear()
                                            Exit Try
                                        Case DialogResult.Abort
                                            If scanBacksideStartIndex < 0 Then
                                                Select Case MsgBox("Flip the paper, and click ok when ready...", MsgBoxStyle.OkCancel + MsgBoxStyle.Information + MsgBoxStyle.ApplicationModal, "READY?")
                                                    Case MsgBoxResult.Ok
                                                        hasMorePages = True
                                                        scanBacksideStartIndex = images.Count
                                                        Err.Clear()
                                                        Exit Try
                                                End Select
                                            End If
                                            hasMorePages = False
                                            scanBacksideStartIndex = -1
                                        Case Else
                                            hasMorePages = False
                                            Err.Clear()
                                            Exit Try
                                    End Select
                                    Exit Try
                                End If
                            Loop
                            hasMorePages = True

                        Else
                            Select Case Math.Abs(CULng(ex.ErrorCode))
                                Case Math.Abs(CULng(WIA_ERRORS.WIA_ERROR_PAPER_EMPTY))
                                    If images.Count = 0 AndAlso settings.adf AndAlso settings.tryFlatbed Then
                                        settings.adf = False
                                    Else
                                        hasMorePages = False
                                    End If
                                    Exit Select

                                Case Math.Abs(CULng(WIA_ERRORS.WIA_ERROR_PAPER_JAM))
                                    Exit Select

                                Case Math.Abs(CULng(WIA_ERRORS.WIA_ERROR_BUSY))
                                    System.Threading.Thread.Sleep(2000)
                                    Exit Select
                                Case Else
                                    Throw ex
                            End Select
                        End If
                    End Try
                End While

                If scanBacksideStartIndex >= 0 And images.Count > 0 Then
                    Dim cntr As Integer = -1
                    For i As Integer = 0 To scanBacksideStartIndex
                        Try
                            If i < scanBacksideStartIndex Then
                                cntr += 1
                                Dim img As System.Drawing.Image
                                If Not settings.frmMain1 Is Nothing Then
                                    If settings.frmMain1.GetType Is (frmMain.GetType) Then
                                        img = images(i)
                                        settings.frmMain1.ImportImage(img, False, True)
                                    End If
                                End If
                                If Not settings.frmMain1 Is Nothing Then
                                    If settings.frmMain1.GetType Is (frmMain.GetType) Then
                                        If i < images.Count Then
                                            img = images(images.Count - (i) - 1)
                                            settings.frmMain1.ImportImage(img, False, True)
                                        End If
                                    End If
                                End If
                            Else
                                Exit For
                            End If
                        Catch ex As Exception
                            Err.Clear()
                        End Try
                    Next
                ElseIf scanBacksideStartIndex < images.Count And images.Count > 0 Then
                    For Each img As System.Drawing.Image In images.ToArray
                        Try
                            If Not settings.frmMain1 Is Nothing Then
                                If settings.frmMain1.GetType Is (frmMain.GetType) Then
                                    settings.frmMain1.ImportImage(img, False, True)
                                End If
                            End If
                        Catch ex As Exception
                            Err.Clear()
                        End Try
                    Next
                End If
                Return images
            Catch exMain As Exception
                settings.frmMain1.TimeStampAdd(exMain, True)
                Err.Clear()
            Finally
                deviceCurrentShared = Nothing
                If Not settings.frmMain1 Is Nothing Then
                    If settings.frmMain1.GetType Is (frmMain.GetType) Then
                        settings.frmMain1.cUserRect.rect = Nothing
                        settings.frmMain1.cUserRect._highLightFieldName = ""
                        settings.frmMain1.fldNameHighlighted = ""
                        settings.frmMain1.LoadPageList(settings.frmMain1.btnPage)
                        If settings.frmMain1.pnlFields.Visible Then settings.frmMain1.pnlFields.Visible = False
                        settings.frmMain1.ComboBox1_SelectedIndexChanged(settings.frmMain1, New EventArgs())
                        settings.frmMain1.A0_PictureBox1.Enabled = True
                        settings.frmMain1.A0_PictureBox2.Enabled = True
                        settings.frmMain1.btnPage.SelectedIndex = settings.frmMain1.btnPage.Items.Count - 1
                        settings.frmMain1.btnPage_SelectedIndexChanged(settings.frmMain1, New EventArgs())
                    End If
                End If
            End Try
            Return images










        End Function


        Private Shared Sub SetProperty([property] As [Property], value As Integer)
            Dim x As IProperty = DirectCast([property], IProperty)
            If Not value = x.Value Then
                Dim val As [Object] = value
                x.Value = (val)
            End If
        End Sub

        Private Shared Sub SetDeviceIntProperty(ByRef device As Device, propertyID As Integer, propertyValue As Integer)
            For Each p As [Property] In device.Properties
                If p.PropertyID = propertyID Then
                    Dim value As Object = propertyValue
                    If Not propertyValue = p.Value Then
                        p.Value = (value)
                    End If
                    Exit For

                End If
            Next
        End Sub

        Private Shared Function GetDeviceIntProperty(ByRef device As Device, propertyID As Integer) As Integer
            Dim ret As Integer = -1

            For Each p As [Property] In device.Properties
                If p.PropertyID = propertyID Then
                    ret = CInt(p.Value)
                    Exit For
                End If
            Next

            Return ret
        End Function
    End Class


End Class
