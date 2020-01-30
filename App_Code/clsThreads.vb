Imports System
Imports System.Reflection
Imports System.IO
Public Class clsImageThreads
    Implements IDisposable
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 NK-INC.COM All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>

    Public clsAlarm As classThreads = Nothing
    Public imgBytes() As Byte
    Public WithEvents frm As frmMain
    Public blnEventDone As Boolean = True
    Public pageCurrent As Integer = -1, pageFrom As Integer = -1, pageTo As Integer = -1
    Public cmbPercent1SelectedIndex As Integer = 0, cmbPercent1SelectedText As String = "", cmbPercent1Text As String = ""
    Public Sub New(ByRef frmMain1 As frmMain, ByRef pageFrom1 As Integer, ByRef pageTo1 As Integer, ByVal cmbPercent As ComboBox)
        Try
            frm = frmMain1
            Me.pageFrom = pageFrom1
            Me.pageTo = pageTo1
            blnEventDone = False
            cmbPercent1SelectedIndex = cmbPercent.SelectedIndex
            cmbPercent1SelectedText = cmbPercent.SelectedItem.ToString
            cmbPercent1Text = cmbPercent.Text
            clsAlarm = New classThreads(frmMain1, AddressOf doneEventHandler, pageCurrent, pageFrom, pageTo, cmbPercent, imgBytes)
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public Sub New_WaitForDone(ByRef frmMain1 As frmMain, ByRef pageFrom1 As Integer, ByRef pageTo1 As Integer, ByVal cmbPercent As ComboBox)
        Try



            frm = frmMain1
            Me.pageFrom = pageFrom1
            Me.pageCurrent = pageFrom1
            Me.pageTo = pageTo1
            cmbPercent1SelectedIndex = cmbPercent.SelectedIndex
            cmbPercent1SelectedText = cmbPercent.SelectedItem.ToString
            cmbPercent1Text = cmbPercent.Text
            clsAlarm = New classThreads(frmMain1, AddressOf doneEventHandler, pageCurrent, pageFrom, pageTo, cmbPercent, imgBytes)
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public Sub StopTimer()
        Try
            clsAlarm.cThreads.StopTimer()
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public Sub eventDone(ByVal blnDone As Boolean)
        blnEventDone = True
        If blnDone Then
            StopTimer()
        End If
    End Sub
    Private Sub THREAD_TimerStart(ByVal blnStart As Boolean)
        If Not blnStart Then Return
        If Not clsAlarm Is Nothing Then
            clsAlarm.cThreads.StopTimer()
        End If
        Dim dtTimeNow As DateTime = DateTime.Now.ToLocalTime
        Dim targetTime As DateTime = dtTimeNow.AddSeconds(5)
    End Sub
    Private Sub THREAD_MOD(ByVal textLabel As String)
    End Sub
    Private Sub THREAD_MOD_LABEL(ByVal textLabel As String)
    End Sub
    Public Sub Threads_Alarm(ByVal sender As Object, ByVal e As EventArgs)
        Console.WriteLine("Alarm")
        If Not sender Is Nothing Then
            If sender.GetType Is GetType(classThreads.clsThreads) Then
                Try
                Catch ex As Exception
                    Err.Clear()
                    Try
                    Catch ex2 As Exception
                        Err.Clear()
                    End Try
                End Try
            End If
        End If
    End Sub
    Public Sub THREAD_Ching(ByVal bln As Boolean)
        Try
            Dim pdfBytes() As Byte = frm.Session
            Dim m As MemoryStream
            If Not frm.Session("image_cache_history_" & pageCurrent, cmbPercent1SelectedIndex, cmbPercent1SelectedText, cmbPercent1Text) Is Nothing Then
                m = New MemoryStream(frm.Session("image_cache_history_" & pageCurrent, cmbPercent1SelectedIndex, cmbPercent1SelectedText, cmbPercent1Text).ToArray())
            Else
                m = New MemoryStream(frm.A0_LoadImage(pdfBytes, pageCurrent, CInt(frm.pdfReaderDoc.GetPageSizeWithRotation(pageCurrent).Width * frm.getPercent()), CInt(frm.pdfReaderDoc.GetPageSizeWithRotation(pageCurrent).Height * frm.getPercent())))
            End If
            If m.CanSeek Then
                m.Seek(0, SeekOrigin.Begin)
            End If
            If Not frm.bytesMatch(frm.Session("image_cache_history_" & pageCurrent, cmbPercent1SelectedIndex, cmbPercent1SelectedText, cmbPercent1Text), m.ToArray()) Then
                frm.Session("image_cache_history_" & pageCurrent, cmbPercent1SelectedIndex, cmbPercent1SelectedText, cmbPercent1Text) = m.ToArray
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public Sub doneEventHandler(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Try
                Dim Delegate_THREAD_eventDone As Action(Of Boolean) = AddressOf eventDone
                Delegate_THREAD_eventDone.Invoke(True)
            Catch ex As Exception
                Err.Clear()
                THREAD_Ching(True)
            End Try
        Catch ex2 As Exception
            Err.Clear()
        End Try
    End Sub
    Public Sub THREAD_StartTimer(ByVal bln As Boolean)
    End Sub
    Public Sub THREAD_StopTimer(ByVal bln As Boolean)
    End Sub
    Public Sub StartTimerEventHandler(ByVal sender As Object, ByVal e As EventArgs)
    End Sub
    Public Sub StopTimerEventHandler(ByVal sender As Object, ByVal e As EventArgs)
    End Sub
    Public Class classThreads
        Public cThreads As clsThreads = Nothing
        Public imgBytes() As Byte
        Public WithEvents frm As frmMain
        Public pageCurrent As Integer = -1, pageFrom As Integer = -1, pageTo As Integer = -1
        Public cmbPercent1SelectedIndex As Integer = 0, cmbPercent1SelectedText As String = "", cmbPercent1Text As String = ""
        Public Sub New(ByRef frm1 As frmMain, ByRef eventDone1 As EventHandler, ByRef pageCurrent1 As Integer, ByRef pageFrom1 As Integer, ByRef pageTo1 As Integer, ByVal cmbPercent As ComboBox, Optional ByRef imgBytes() As Byte = Nothing)
            Me.frm = frm1
            pageCurrent = pageCurrent1
            pageFrom = pageFrom1
            pageTo = pageTo1
            cmbPercent1SelectedIndex = cmbPercent.SelectedIndex
            cmbPercent1SelectedText = cmbPercent.SelectedItem.ToString
            cmbPercent1Text = cmbPercent.Text
            Me.cThreads = New clsThreads(frm, eventDone1, pageCurrent1, pageFrom1, pageTo1, cmbPercent)
            Me.cThreads.StartTimer()
            Console.ReadLine()
        End Sub
        Public Class clsThreads
            Public TargetTime As DateTime
            Public Const MinSleepMilliseconds As Integer = 1500
            Public threadAlarm As Threading.Thread = Nothing
            Public eventDoneHandler As EventHandler
            Public StartTimerEventHandler As EventHandler
            Public StopTimerEventHandler As EventHandler
            Public objTimer As Windows.Forms.Timer
            Public objTimerChing As Windows.Forms.Timer
            Public StartChingEventHandler As EventHandler
            Public startedTime As DateTime
            Public targeTimeChingLast As DateTime
            Public targetTimeChing As DateTime
            Public imgBytes() As Byte
            Public WithEvents frm As frmMain
            Public pageCurrent As Integer = -1, pageFrom As Integer = -1, pageTo As Integer = -1
            Public cmbPercent1SelectedIndex As Integer = 0, cmbPercent1SelectedText As String = "", cmbPercent1Text As String = ""
            Public Sub New(ByRef frm1 As frmMain, ByRef eventDone As EventHandler, ByRef pageCurrent1 As Integer, ByRef pageFrom1 As Integer, ByRef pageTo1 As Integer, ByVal cmbPercent As ComboBox, Optional ByRef imgBytes1() As Byte = Nothing)
                Me.frm = frm1
                Me.pageFrom = pageFrom1
                Me.pageTo = pageTo1
                Me.pageCurrent = pageCurrent1
                Me.TargetTime = TargetTime
                Me.imgBytes = imgBytes1
                Me.eventDoneHandler = eventDone
                Me.cmbPercent1SelectedIndex = cmbPercent.SelectedIndex
                Me.cmbPercent1SelectedText = cmbPercent.SelectedItem.ToString
                Me.cmbPercent1Text = cmbPercent.Text
                startedTime = DateTime.Now
                targeTimeChingLast = DateTime.Now.AddMinutes(-1)
            End Sub
            Public Sub Close()
                SilenceAlarm()
                StopTimer()
            End Sub
            Public Sub SilenceAlarm()
                Try
                    StopTimerEventHandler(Me, New EventArgs())
                Catch ex As Exception
                    Err.Clear()
                End Try
            End Sub
            Public Sub SoundAlarm(Optional ByVal intervalMilliseconds As Integer = 1500)
                Try
                    StartTimerEventHandler(Me, New EventArgs())
                Catch ex As Exception
                    Err.Clear()
                End Try
            End Sub
            Public Sub StopTimer()
                Try
                    StopTimerEventHandler(Me, New EventArgs())
                    Me.threadAlarm.Abort()
                Catch ex As Exception
                    Err.Clear()
                End Try
            End Sub
            Public Sub StartTimer()
                Try
                    Me.threadAlarm = New Threading.Thread(AddressOf ProcessTimer)
                    Me.threadAlarm.Start()
                Catch ex As Exception
                    Err.Clear()
                End Try
            End Sub
            Public Sub AlarmStart()
            End Sub
            Public Sub ProcessTimer()
                SilenceAlarm()
                Dim NowTime As DateTime = DateTime.Now
                Do While frm.LoadImageGs_InUse
                    frm.DoEvents_Wait(500)
                Loop
                While (pageCurrent <= pageTo)
                    NowTime = DateTime.Now
                    TargetTime = NowTime.AddMilliseconds(MinSleepMilliseconds)
                    Dim SleepMilliseconds As Integer = CInt(Math.Round((TargetTime - NowTime).TotalMilliseconds / 2))
                    If pageCurrent < pageFrom Then
                        pageCurrent = pageFrom
                    End If
                    If pageCurrent > pageTo Then
                        Exit While
                    End If
                    Try
                        frm.LoadPDFReaderDoc(frm.pdfOwnerPassword, True)

                        Dim m As MemoryStream
                        If frm.Session("image_cache_history_" & pageCurrent, cmbPercent1SelectedIndex, cmbPercent1SelectedText, cmbPercent1Text) Is Nothing Then
                            Try
                                m = New MemoryStream(frm.A0_LoadImage(frm.pdfReaderDoc.Clone(), pageCurrent, CInt(frm.pdfReaderDoc.GetPageSizeWithRotation(pageCurrent).Width * frm.getPercentPageNumber(pageCurrent, cmbPercent1SelectedIndex, cmbPercent1SelectedText, cmbPercent1Text)), CInt(frm.pdfReaderDoc.GetPageSizeWithRotation(pageCurrent).Height * frm.getPercentPageNumber(pageCurrent, cmbPercent1SelectedIndex, cmbPercent1SelectedText, cmbPercent1Text))))
                                Try
                                    If Not m Is Nothing Then
                                        If m.Length > 0 Then
                                            If m.CanSeek Then
                                                m.Seek(0, SeekOrigin.Begin)
                                            End If
                                            frm.Session("image_cache_history_" & pageCurrent, cmbPercent1SelectedIndex, cmbPercent1SelectedText, cmbPercent1Text) = m.ToArray
                                        End If
                                    End If
                                Catch exTest As Exception
                                    Throw exTest
                                End Try
                            Catch exTestMain As Exception
                                Throw exTestMain
                            Finally
                                Console.WriteLine(SleepMilliseconds)
                                Threading.Thread.Sleep(CInt(IIf(SleepMilliseconds > MinSleepMilliseconds, SleepMilliseconds, MinSleepMilliseconds)))
                            End Try
                        End If
                    Catch ex As Exception
                        Err.Clear()
                    Finally
                        If pageCurrent <= pageTo Then
                            pageCurrent += 1
                        End If
                    End Try
                End While
                eventDoneHandler(Me, New EventArgs())
                Me.threadAlarm.Abort()
            End Sub
        End Class
    End Class
    Private disposedValue As Boolean = False
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
            End If
            StopTimer()
        End If
        Me.disposedValue = True
    End Sub
#Region " IDisposable Support "
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
