Imports System.Collections.Generic
Imports iTextSharp.text.pdf
Public Class clsLinks
    ''' <summary>
    ''' PdForms.net- Created by Nicholas Kowalewicz (www.PdForms.net)
    ''' Copyright 2017 NK-INC.COM All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Email Contact: support@nk-inc.ccom
    ''' Website: www.pdforms.net
    ''' </summary>

    Public Structure Link
        Public Link_Rect As System.Drawing.RectangleF
        Public Link_Destination_PageIndex As Integer
        Public Link_Destination_URI As String
        Public Link_IsImage As Boolean
        Public Link_ImageBytes() As Byte
        Public Link_ImageMaskBytes() As Byte
        Public Link_RefNum As Long
        Public Link_RefName As PdfName
        Public Link_ImageFormat As System.Drawing.Imaging.ImageFormat
        Public Link_ImageSize_Pdf As System.Drawing.SizeF
        Public Link_Text As String
        Public Link_StickNote As Boolean
        Public Link_StickNote_Open As Boolean
        Public Link_StickNote_XHTML As String
        Public Link_StickNote_PopupRect As System.Drawing.RectangleF
        Public Link_StickNote_PopupLinkRect As System.Drawing.RectangleF
        Public Link_StickyNoteDict As PdfDictionary
        Public Link_StickyNoteLinkDict As PdfDictionary
        Public Link_StickyNotePopupIndirectRef As PdfIndirectReference
        Public Link_AnnotationIndex As Integer
        Function StripXHTMLTags(ByVal xhtml As String) As String
            ' Remove HTML tags.
            Return System.Text.Encoding.UTF8.GetString(System.Text.Encoding.Convert(System.Text.Encoding.ASCII, System.Text.Encoding.UTF8, System.Text.Encoding.ASCII.GetBytes(System.Text.RegularExpressions.Regex.Replace(xhtml, "<.*?>", ""))))
            'Return System.Text.Encoding.UTF8.GetString(System.Text.Encoding.ASCII.GetBytes(System.Text.RegularExpressions.Regex.Replace(xhtml, "<.*?>", "")))
            '.Replace("&#13;", Environment.NewLine())
        End Function
        Sub New(ByVal LinkRect As System.Drawing.RectangleF, ByVal DestPage As Integer)
            Link_Rect = LinkRect
            Link_Destination_PageIndex = DestPage
            Link_Destination_URI = ""
        End Sub
        Sub New(ByVal LinkRect As System.Drawing.RectangleF, ByVal DestPage As Integer, ByVal PopupRect As System.Drawing.RectangleF, PopupXHTML As String, StickNote_Open As Boolean, ByRef StickyNote_Dict As PdfDictionary, ByRef StickyNotePopupIndirectRef As PdfIndirectReference, annotationIndex As Integer)
            Link_StickNote = True
            Link_Rect = LinkRect
            Link_StickNote_PopupLinkRect = LinkRect
            Link_StickNote_PopupRect = PopupRect
            Link_StickyNoteDict = StickyNote_Dict
            'Link_StickyNoteLinkDict = StickyNoteLinkDict
            Link_StickyNotePopupIndirectRef = StickyNotePopupIndirectRef
            Link_Destination_PageIndex = DestPage
            Link_StickNote_Open = StickNote_Open
            Link_StickNote_XHTML = PopupXHTML
            Link_Text = StripXHTMLTags(PopupXHTML.ToString()).ToString().Replace("&#13;", Environment.NewLine())
            Link_Destination_URI = ""
            Link_AnnotationIndex = annotationIndex
        End Sub
        Sub New(ByVal LinkRect As System.Drawing.RectangleF, ByVal DestPage As Integer, ByVal LinkText As String)
            Link_Rect = LinkRect
            Link_Destination_PageIndex = DestPage
            Link_Text = LinkText
            Link_IsImage = False
            Link_Destination_URI = ""
        End Sub
        Sub New(ByVal LinkRect As System.Drawing.RectangleF, ByVal DestLink As String)
            Link_Rect = LinkRect
            Link_Destination_URI = DestLink
            Link_Destination_PageIndex = -1
        End Sub
        Sub New(ByVal LinkRect As System.Drawing.RectangleF, ByVal IsImage As Boolean, ByVal ImageBytes() As Byte)
            Link_Rect = LinkRect
            Link_IsImage = IsImage
            Link_ImageBytes = ImageBytes
        End Sub
        Sub New(ByVal LinkRect As System.Drawing.RectangleF, ByVal IsImage As Boolean, ByVal ImageBytes() As Byte, ByVal refNum As Long)
            Link_Rect = LinkRect
            Link_IsImage = IsImage
            Link_ImageBytes = ImageBytes
            Link_RefNum = refNum
        End Sub
        Sub New(ByVal LinkRect As System.Drawing.RectangleF, ByVal LinkImageSizePdf As System.Drawing.SizeF, ByVal IsImage As Boolean, ByVal ImageBytes() As Byte, ByVal refNum As Long)
            Link_Rect = LinkRect
            Link_IsImage = IsImage
            Link_ImageBytes = ImageBytes
            Link_RefNum = refNum
            Link_ImageSize_Pdf = LinkImageSizePdf
        End Sub
        Sub New(ByVal LinkRect As System.Drawing.RectangleF, ByVal LinkImageSizePdf As System.Drawing.SizeF, ByVal IsImage As Boolean, ByVal ImageBytes() As Byte, ByVal imageFormat As System.Drawing.Imaging.ImageFormat, ByVal refNum As Long)
            Link_Rect = LinkRect
            Link_IsImage = IsImage
            Link_ImageBytes = ImageBytes
            Link_RefNum = refNum
            Link_ImageSize_Pdf = LinkImageSizePdf
            If imageFormat.GetHashCode() = System.Drawing.Imaging.ImageFormat.MemoryBmp.GetHashCode() Then
                imageFormat = System.Drawing.Imaging.ImageFormat.Jpeg
            End If
            Link_ImageFormat = imageFormat
        End Sub
        Sub New(ByVal LinkRect As System.Drawing.RectangleF, ByVal LinkImageSizePdf As System.Drawing.SizeF, ByVal IsImage As Boolean, ByVal ImageBytes() As Byte, ByVal ImageMaskBytes() As Byte, ByVal imageFormat As System.Drawing.Imaging.ImageFormat, ByVal refNum As Long)
            Link_Rect = LinkRect
            Link_IsImage = IsImage
            Link_ImageBytes = ImageBytes
            Link_RefNum = refNum
            Link_ImageSize_Pdf = LinkImageSizePdf
            Link_ImageMaskBytes = ImageMaskBytes
            If imageFormat.GetHashCode() = System.Drawing.Imaging.ImageFormat.MemoryBmp.GetHashCode() Then
                imageFormat = System.Drawing.Imaging.ImageFormat.Jpeg
            End If
            Link_ImageFormat = imageFormat
        End Sub
        Sub New(ByVal LinkRect As System.Drawing.RectangleF, ByVal LinkImageSizePdf As System.Drawing.SizeF, ByVal IsImage As Boolean, ByVal ImageBytes() As Byte, ByVal ImageMaskBytes() As Byte, ByVal imageFormat As System.Drawing.Imaging.ImageFormat, ByVal refNum As Long, refPdfName As PdfName)
            Link_Rect = LinkRect
            Link_IsImage = IsImage
            Link_ImageBytes = ImageBytes
            Link_RefNum = refNum
            Link_ImageSize_Pdf = LinkImageSizePdf
            Link_ImageMaskBytes = ImageMaskBytes
            If imageFormat.GetHashCode() = System.Drawing.Imaging.ImageFormat.MemoryBmp.GetHashCode() Then
                imageFormat = System.Drawing.Imaging.ImageFormat.Jpeg
            End If
            Link_ImageFormat = imageFormat
            Link_RefName = refPdfName
        End Sub

    End Structure
    Dim pages As PdfDictionary
    Public Links As New List(Of Link)
    Public parent As frmMain = Nothing
    Public currentPageIndex As Integer = -1
    Public mouseCursor As Cursor = Cursors.Default
    Public pdfReaderInstance As PdfReader
    'Public frmMain1 As frmMain = Nothing
    Public Sub New(ByRef pdfReader1 As PdfReader, ByRef parent1 As frmMain) 'ByVal pageIndex As Integer, 
        If pdfReader1 Is Nothing Then Return
        pdfReaderInstance = pdfReader1
        pages = DirectCast(PdfReader.GetPdfObject(pdfReader1.Catalog.GetAsDict(PdfName.PAGES)), PdfDictionary)
        parent = parent1
    End Sub
    Public Function findPageIndex(ByVal indirectObjNum As Integer) As Integer
        Dim pgIndex As Integer = -1
        For Each pg As PdfIndirectReference In pages.GetAsArray(PdfName.KIDS).ArrayList.ToArray
            pgIndex += 1
            If pg.Number = indirectObjNum Then
                Return pgIndex
            End If
        Next
        Return -1
    End Function
    Public Shared Function findPageIndex(ByVal indirectObjNum As Integer, ByVal pages As PdfDictionary) As Integer
        Dim pgIndex As Integer = -1
        For Each pg As PdfIndirectReference In pages.GetAsArray(PdfName.KIDS).ArrayList.ToArray
            pgIndex += 1
            If pg.Number = indirectObjNum Then
                Return pgIndex
            End If
        Next
        Return -1
    End Function
    Public Function LinkClickedDestinationPage(ByVal pageIndex As Integer, ByVal ptScreen As PointF) As Integer
        If Not currentPageIndex = pageIndex Then
            currentPageIndex = pageIndex
            LoadLinksOnPage(currentPageIndex)
        End If
        For Each l As Link In Links.ToArray
            If l.Link_Rect.Contains(ptScreen) Then
                Return l.Link_Destination_PageIndex + 0
            End If
        Next
        Return -1
    End Function
    Public Function LinkClickedDestinationUriString(ByVal pageIndex As Integer, ByVal ptScreen As PointF) As String
        If Not currentPageIndex = pageIndex Then
            currentPageIndex = pageIndex
            LoadLinksOnPage(currentPageIndex)
        End If
        For Each l As Link In Links.ToArray
            If l.Link_Rect.Contains(ptScreen) Then
                Return l.Link_Destination_URI & ""
            End If
        Next
        Return ""
    End Function
    Public Function LinkClickedPopupText(ByVal pageIndex As Integer, ByVal ptScreen As PointF) As String
        If Not currentPageIndex = pageIndex Then
            currentPageIndex = pageIndex
            LoadLinksOnPage(currentPageIndex)
        End If
        For Each l As Link In Links.ToArray
            If l.Link_StickNote_PopupLinkRect.Contains(ptScreen) Then
                Return l.Link_Text & ""
            ElseIf l.Link_StickNote_PopupRect.Contains(ptScreen) Then
                Return l.Link_Text & ""
            End If
        Next
        Return ""
    End Function
    Public Function LinkClickedPopupLink(ByVal pageIndex As Integer, ByVal ptScreen As PointF) As clsLinks.Link
        If Not currentPageIndex = pageIndex Then
            currentPageIndex = pageIndex
            LoadLinksOnPage(currentPageIndex)
        End If
        For Each l As Link In Links.ToArray
            If l.Link_StickNote_PopupLinkRect.Contains(ptScreen) Then
                Return l
            ElseIf l.Link_StickNote_PopupRect.Contains(ptScreen) Then
                Return l
            End If
        Next
        Return Nothing
    End Function
    Public Function LinkClickedPopupLinkRect(ByVal pageIndex As Integer, ByVal ptScreen As PointF) As System.Drawing.RectangleF
        If Not currentPageIndex = pageIndex Then
            currentPageIndex = pageIndex
            LoadLinksOnPage(currentPageIndex)
        End If
        For Each l As Link In Links.ToArray
            If l.Link_StickNote_PopupLinkRect.Contains(ptScreen) Then
                Return l.Link_StickNote_PopupLinkRect
            ElseIf l.Link_StickNote_PopupRect.Contains(ptScreen) Then
                Return l.Link_StickNote_PopupLinkRect
            End If
        Next
        Return Nothing
    End Function
    Public Function ImageClicked(ByVal pageIndex As Integer, ByVal ptScreen As PointF) As Byte()
        If Not currentPageIndex = pageIndex Then
            currentPageIndex = pageIndex
            LoadImageCoordinatesOnPage(currentPageIndex)
        End If
        Dim bImg() As Byte = Nothing
        For Each l As Link In Links.ToArray
            If l.Link_Rect.Contains(ptScreen) Then
                If l.Link_IsImage Then
                    bImg = l.Link_ImageBytes
                End If
            End If
        Next
        Return bImg
    End Function
    Public Function TextClicked(ByVal pageIndex As Integer, ByVal ptScreen As PointF) As String
        If Not currentPageIndex = pageIndex Then
            currentPageIndex = pageIndex
            LoadTextCoordinatesOnPage(currentPageIndex)
        End If
        Dim txt As String = ""
        For Each l As Link In Links.ToArray
            If l.Link_Rect.Contains(ptScreen) Then
                If Not l.Link_IsImage Then
                    txt = l.Link_Text
                End If
            End If
        Next
        Return txt
    End Function
    Public Function ImageClickedLink(ByVal pageIndex As Integer, ByVal ptScreen As PointF) As Link
        If Not currentPageIndex = pageIndex Then
            currentPageIndex = pageIndex
            LoadImageCoordinatesOnPage(currentPageIndex)
        End If
        Dim lnkTop As Link = Nothing
        For Each l As Link In Links.ToArray
            If l.Link_Rect.Contains(ptScreen) Then
                If l.Link_IsImage Then
                    'Return l
                    lnkTop = l
                End If
            End If
        Next
        Return lnkTop
    End Function
    Public Function ImageClickedLinks(ByVal pageIndex As Integer, ByVal ptScreen As PointF) As List(Of Link)
        If Not currentPageIndex = pageIndex Then
            currentPageIndex = pageIndex
            LoadImageCoordinatesOnPage(currentPageIndex)
        End If
        Dim lnkArray As New List(Of Link)
        For Each l As Link In Links.ToArray
            If l.Link_Rect.Contains(ptScreen) Then
                If l.Link_IsImage Then
                    lnkArray.Add(l)
                End If
            End If
        Next
        Return lnkArray
    End Function
    Dim toolstripTextDefault As String = ""
    Public Function LinksMouseCursorChange(ByVal pageIndex As Integer, ByVal ptScreen As PointF) As Boolean
        If Not currentPageIndex = pageIndex Then
            currentPageIndex = pageIndex
            LoadLinksOnPage(currentPageIndex)
        End If
        parent.ToolStripStatusLabel2.IsLink = True
        If Links Is Nothing Then
            If Not parent Is Nothing Then
                If parent.GetType Is GetType(frmMain) Then
                    If Not String.IsNullOrEmpty(parent.ToolStripStatusLabel5.Text & "") Then
                        If Not parent.ToolStripStatusLabel2.Text.Contains("Link: ") Then
                            toolstripTextDefault = parent.ToolStripStatusLabel2.Text & ""
                        End If
                    End If
                    If Not parent.ToolStripStatusLabel5.Text = toolstripTextDefault Then
                        parent.ToolStripStatusLabel2.Text = toolstripTextDefault
                    End If
                End If
            End If
        ElseIf Links.Count <= 0 Then
            If Not parent Is Nothing Then
                If parent.GetType Is GetType(frmMain) Then
                    If Not String.IsNullOrEmpty(parent.ToolStripStatusLabel2.Text & "") Then
                        If Not parent.ToolStripStatusLabel2.Text.Contains("Link: ") Then
                            toolstripTextDefault = parent.ToolStripStatusLabel2.Text & ""
                        End If
                    End If
                    If Not parent.ToolStripStatusLabel2.Text = toolstripTextDefault Then
                        parent.ToolStripStatusLabel2.Text = toolstripTextDefault
                    End If
                End If
            End If
        Else
            If Not parent Is Nothing Then
                If parent.GetType Is GetType(frmMain) Then
                    If Not String.IsNullOrEmpty(parent.ToolStripStatusLabel2.Text & "") Then
                        If Not parent.ToolStripStatusLabel2.Text.Contains("Link: ") Then
                            toolstripTextDefault = parent.ToolStripStatusLabel2.Text & ""
                        End If
                    End If
                    If Not parent.ToolStripStatusLabel2.Text = toolstripTextDefault Then
                        parent.ToolStripStatusLabel2.Text = toolstripTextDefault
                    End If
                End If
            End If
            For Each l As Link In Links.ToArray
                If l.Link_Rect.Contains(ptScreen) Then
                    mouseCursor = Cursors.Hand
                    If Not l.Link_Destination_URI = Nothing Then
                        If Not String.IsNullOrEmpty(l.Link_Destination_URI & "") Then
                            If Not parent Is Nothing Then
                                If parent.GetType Is GetType(frmMain) Then
                                    parent.ToolStripStatusLabel2.Text = "Double Click Link: " & l.Link_Destination_URI.ToString
                                End If
                            End If
                        End If
                        'ElseIf Not l.Link_Destination_PageIndex = Nothing Then
                        '    If Not l.Link_Destination_PageIndex >= 0 Then
                        '        If Not parent Is Nothing Then
                        '            If parent.GetType Is GetType(frmMain) Then
                        '                parent.ToolStripStatusLabel2.Text = "Page Link: " & l.Link_Destination_PageIndex.ToString
                        '            End If
                        '        End If
                        '    End If
                    ElseIf Not l.Link_IsImage = Nothing Then
                        If Not l.Link_ImageBytes.Length >= 0 Then
                            If Not parent Is Nothing Then
                                If parent.GetType Is GetType(frmMain) Then
                                    If Not String.IsNullOrEmpty(parent.ToolStripStatusLabel2.Text & "") Then
                                        If Not parent.ToolStripStatusLabel2.Text.Contains("Double Click Link: ") Then
                                            toolstripTextDefault = parent.ToolStripStatusLabel2.Text & ""
                                        End If
                                    End If
                                    If Not parent.ToolStripStatusLabel2.Text = toolstripTextDefault Then
                                        parent.ToolStripStatusLabel2.Text = "Double Click Link: " & toolstripTextDefault
                                        parent.ToolStripStatusLabel2.IsLink = False
                                    End If
                                End If
                            End If
                        End If
                    End If
                    Return True
                End If
            Next
        End If
        mouseCursor = Cursors.Default
        Return False
    End Function
    Public Function LoadLinksOnPage(ByVal pageIndex As Integer) As List(Of Link)
        ' parent.TimeStampAdd("OpenFile(cLinks.LoadLinksOnPage(" & pageIndex & ")-start)")
        Dim pageHeight As Single = parent.getPDFHeight()
        Links = New List(Of Link)
        ' parent.TimeStampAdd("OpenFile(cLinks.LoadLinksOnPage(" & pageIndex & ")-ANNOTS-start)")
        Dim cntrX As Integer = -1
        Try

            If Not pages.GetAsArray(PdfName.KIDS).GetAsDict(pageIndex).Get(PdfName.ANNOTS) Is Nothing Then
                If pages.GetAsArray(PdfName.KIDS).GetAsDict(pageIndex).Get(PdfName.ANNOTS).IsArray Then
                    For annotIndex As Integer = 0 To pages.GetAsArray(PdfName.KIDS).GetAsDict(pageIndex).GetAsArray(PdfName.ANNOTS).ArrayList.Count - 1
                        cntrX += 1
                        If cntrX = 18 Then
                            cntrX = cntrX
                        End If
                        If pages.GetAsArray(PdfName.KIDS).GetAsDict(pageIndex).GetAsArray(PdfName.ANNOTS)(annotIndex).IsDictionary Then
                            Dim annot As PdfDictionary = DirectCast(pages.GetAsArray(PdfName.KIDS).GetAsDict(pageIndex).GetAsArray(PdfName.ANNOTS).GetAsDict(annotIndex), PdfDictionary) '
                            If Not annot Is Nothing Then
                                If Not annot.Get(PdfName.SUBTYPE) Is Nothing Then
                                    If Not annot.Get(PdfName.SUBTYPE) Is Nothing Then
                                        If annot.Get(PdfName.SUBTYPE).IsName Then
                                            If annot.GetAsName(PdfName.SUBTYPE) Is PdfName.LINK Then
                                                If Not annot.Get(PdfName.RECT) Is Nothing Then
                                                    If Not annot.Get(PdfName.A) Is Nothing Then
                                                        If annot.Get(PdfName.A).IsDictionary Then
                                                            Dim a As PdfDictionary = annot.GetAsDict(PdfName.A)
                                                            If Not a Is Nothing Then
                                                                If Not a.Get(PdfName.S) Is Nothing Then
                                                                    If a.Get(PdfName.S).IsName Then
                                                                        If a.GetAsName(PdfName.S) Is PdfName.URI Then
                                                                            Links.Add(New Link(parent.getRectangleScreen(annot.GetAsArray(PdfName.RECT), pageIndex + 1), a.GetAsString(PdfName.URI).ToUnicodeString()))
                                                                        ElseIf a.GetAsName(PdfName.S) Is PdfName.GOTO Then 'GoTo Destination

                                                                        End If
                                                                    End If
                                                                End If
                                                            End If
                                                        End If
                                                    End If
                                                End If
                                            ElseIf annot.GetAsName(PdfName.SUBTYPE) Is PdfName.TEXT Then
                                                'If annot.GetAsString(New PdfName("Subj")) Is Nothing Then
                                                '    If annot.GetAsString(New PdfName("Subj")).ToUnicodeString = "Sticky Note" Then
                                                '        'Links.Add(New Link(parent.getRectangleScreen(annot.GetAsArray(PdfName.RECT), pageIndex + 1), a.GetAsString(PdfName.URI).ToUnicodeString()))
                                                '    End If
                                                'Else

                                                If Not annot.Get(PdfName.RECT) Is Nothing Then
                                                    If Not annot.Get(PdfName.A) Is Nothing Then
                                                        If annot.Get(PdfName.A).IsDictionary Then
                                                            Dim a As PdfDictionary = annot.GetAsDict(PdfName.A)
                                                            If Not a Is Nothing Then
                                                                If Not a.Get(PdfName.S) Is Nothing Then
                                                                    If a.Get(PdfName.S).IsName Then
                                                                        If a.GetAsName(PdfName.S) Is PdfName.URI Then
                                                                            Links.Add(New Link(parent.getRectangleScreen(annot.GetAsArray(PdfName.RECT), pageIndex + 1), a.GetAsString(PdfName.URI).ToUnicodeString()))
                                                                        ElseIf a.GetAsName(PdfName.S) Is PdfName.GOTO Then 'GoTo Destination

                                                                        End If
                                                                    End If
                                                                End If
                                                            End If
                                                        End If
                                                    End If
                                                End If
                                            ElseIf annot.GetAsName(PdfName.SUBTYPE) Is PdfName.POPUP Then
                                                'Dim stickyNote As PdfDictionary = DirectCast(annot.GetDirectObject(PdfName.PARENT), PdfDictionary)
                                                Dim stickyNote As PdfDictionary = DirectCast(pdfReaderInstance.GetPdfObject(annot.GetAsIndirectObject(PdfName.PARENT).Number), PdfDictionary)
                                                'Links.Add(New Link(parent.getRectangleScreen(annot.GetAsArray(PdfName.RECT), pageIndex + 1), pageIndex + 1, parent.getRectangleScreen(stickyNote.GetAsArray(PdfName.RECT), pageIndex + 1), stickyNote.GetAsString(PdfName.RC).ToUnicodeString(), annot.GetAsBoolean(PdfName.OPEN).BooleanValue, stickyNote, annot, annotIndex))
                                                Dim stickyNoteRef As PdfIndirectReference = annot.GetAsIndirectObject(PdfName.PARENT)
                                                'Dim stickyNote As PdfDictionary = DirectCast(pdfReaderInstance.GetPdfObject(stickyNoteRef.Number), PdfDictionary)
                                                Links.Add(New Link(parent.getRectangleScreen(annot.GetAsArray(PdfName.RECT), pageIndex + 1), pageIndex + 1, parent.getRectangleScreen(stickyNote.GetAsArray(PdfName.RECT), pageIndex + 1), CStr(stickyNote.GetAsString(PdfName.RC).ToUnicodeString()), CBool(annot.GetAsBoolean(PdfName.OPEN).BooleanValue), annot, stickyNoteRef, annotIndex))
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        ElseIf pages.GetAsArray(PdfName.KIDS).GetAsDict(pageIndex).GetAsArray(PdfName.ANNOTS)(annotIndex).IsIndirect Then
                            If Not pages.GetAsArray(PdfName.KIDS).GetAsDict(pageIndex).GetAsArray(PdfName.ANNOTS).GetAsDict(annotIndex) Is Nothing Then
                                Dim annot As PdfDictionary = DirectCast(pages.GetAsArray(PdfName.KIDS).GetAsDict(pageIndex).GetAsArray(PdfName.ANNOTS).GetAsDict(annotIndex), PdfDictionary) '
                                If Not annot Is Nothing Then
                                    If Not annot.Get(PdfName.SUBTYPE) Is Nothing Then
                                        If Not annot.Get(PdfName.SUBTYPE) Is Nothing Then
                                            If annot.Get(PdfName.SUBTYPE).IsName Then
                                                If annot.GetAsName(PdfName.SUBTYPE) Is PdfName.LINK Then
                                                    If Not annot.Get(PdfName.RECT) Is Nothing Then
                                                        If Not annot.Get(PdfName.A) Is Nothing Then
                                                            If annot.Get(PdfName.A).IsDictionary Then
                                                                Dim a As PdfDictionary = annot.GetAsDict(PdfName.A)
                                                                If Not a Is Nothing Then
                                                                    If Not a.Get(PdfName.S) Is Nothing Then
                                                                        If a.Get(PdfName.S).IsName Then
                                                                            If a.GetAsName(PdfName.S) Is PdfName.URI Then
                                                                                Links.Add(New Link(parent.getRectangleScreen(annot.GetAsArray(PdfName.RECT), pageIndex + 1), a.GetAsString(PdfName.URI).ToUnicodeString()))
                                                                            ElseIf a.GetAsName(PdfName.S) Is PdfName.GOTO Then 'GoTo Destination

                                                                            End If
                                                                        End If
                                                                    End If
                                                                End If
                                                            End If
                                                        End If
                                                    End If
                                                ElseIf annot.GetAsName(PdfName.SUBTYPE) Is PdfName.POPUP Then
                                                    'Dim stickyNote As PdfDictionary = DirectCast(annot.GetDirectObject(PdfName.PARENT), PdfDictionary)
                                                    Dim stickyNote As PdfDictionary = DirectCast(pdfReaderInstance.GetPdfObject(annot.GetAsIndirectObject(PdfName.PARENT).Number), PdfDictionary)
                                                    'Links.Add(New Link(parent.getRectangleScreen(annot.GetAsArray(PdfName.RECT), pageIndex + 1), pageIndex + 1, parent.getRectangleScreen(stickyNote.GetAsArray(PdfName.RECT), pageIndex + 1), stickyNote.GetAsString(PdfName.RC).ToUnicodeString(), annot.GetAsBoolean(PdfName.OPEN).BooleanValue, stickyNote, annot, annotIndex))
                                                    Dim stickyNoteRef As PdfIndirectReference = annot.GetAsIndirectObject(PdfName.PARENT)
                                                    'Dim stickyNote As PdfDictionary = DirectCast(pdfReaderInstance.GetPdfObject(stickyNoteRef.Number), PdfDictionary)
                                                    Links.Add(New Link(parent.getRectangleScreen(annot.GetAsArray(PdfName.RECT), pageIndex + 1), pageIndex + 1, parent.getRectangleScreen(stickyNote.GetAsArray(PdfName.RECT), pageIndex + 1), CStr(stickyNote.GetAsString(PdfName.RC).ToUnicodeString()), CBool(annot.GetAsBoolean(PdfName.OPEN).BooleanValue), annot, stickyNoteRef, annotIndex))
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If

                        End If
                    Next
                ElseIf pages.GetAsArray(PdfName.KIDS).GetAsDict(pageIndex).Get(PdfName.ANNOTS).IsIndirect Then
                    For annotIndex As Integer = 0 To pages.GetAsArray(PdfName.KIDS).GetAsDict(pageIndex).GetAsArray(PdfName.ANNOTS).ArrayList.Count - 1
                        cntrX += 1
                        If cntrX = 18 Then
                            cntrX = cntrX
                        End If
                        If pages.GetAsArray(PdfName.KIDS).GetAsDict(pageIndex).GetAsArray(PdfName.ANNOTS)(annotIndex).IsDictionary Then
                            Dim annot As PdfDictionary = DirectCast(pages.GetAsArray(PdfName.KIDS).GetAsDict(pageIndex).GetAsArray(PdfName.ANNOTS).GetAsDict(annotIndex), PdfDictionary) '
                            If Not annot Is Nothing Then
                                If Not annot.Get(PdfName.SUBTYPE) Is Nothing Then
                                    If Not annot.Get(PdfName.SUBTYPE) Is Nothing Then
                                        If annot.Get(PdfName.SUBTYPE).IsName Then
                                            If annot.GetAsName(PdfName.SUBTYPE) Is PdfName.LINK Then
                                                If Not annot.Get(PdfName.RECT) Is Nothing Then
                                                    If Not annot.Get(PdfName.A) Is Nothing Then
                                                        If annot.Get(PdfName.A).IsDictionary Then
                                                            Dim a As PdfDictionary = annot.GetAsDict(PdfName.A)
                                                            If Not a Is Nothing Then
                                                                If Not a.Get(PdfName.S) Is Nothing Then
                                                                    If a.Get(PdfName.S).IsName Then
                                                                        If a.GetAsName(PdfName.S) Is PdfName.URI Then
                                                                            Links.Add(New Link(parent.getRectangleScreen(annot.GetAsArray(PdfName.RECT), pageIndex + 1), a.GetAsString(PdfName.URI).ToUnicodeString()))
                                                                        ElseIf a.GetAsName(PdfName.S) Is PdfName.GOTO Then 'GoTo Destination

                                                                        End If
                                                                    End If
                                                                End If
                                                            End If
                                                        End If
                                                    End If
                                                End If
                                            ElseIf annot.GetAsName(PdfName.SUBTYPE) Is PdfName.TEXT Then
                                                'If annot.GetAsString(New PdfName("Subj")) Is Nothing Then
                                                '    If annot.GetAsString(New PdfName("Subj")).ToUnicodeString = "Sticky Note" Then
                                                '        'Links.Add(New Link(parent.getRectangleScreen(annot.GetAsArray(PdfName.RECT), pageIndex + 1), a.GetAsString(PdfName.URI).ToUnicodeString()))
                                                '    End If
                                                'Else

                                                If Not annot.Get(PdfName.RECT) Is Nothing Then
                                                    If Not annot.Get(PdfName.A) Is Nothing Then
                                                        If annot.Get(PdfName.A).IsDictionary Then
                                                            Dim a As PdfDictionary = annot.GetAsDict(PdfName.A)
                                                            If Not a Is Nothing Then
                                                                If Not a.Get(PdfName.S) Is Nothing Then
                                                                    If a.Get(PdfName.S).IsName Then
                                                                        If a.GetAsName(PdfName.S) Is PdfName.URI Then
                                                                            Links.Add(New Link(parent.getRectangleScreen(annot.GetAsArray(PdfName.RECT), pageIndex + 1), a.GetAsString(PdfName.URI).ToUnicodeString()))
                                                                        ElseIf a.GetAsName(PdfName.S) Is PdfName.GOTO Then 'GoTo Destination

                                                                        End If
                                                                    End If
                                                                End If
                                                            End If
                                                        End If
                                                    End If
                                                End If
                                            ElseIf annot.GetAsName(PdfName.SUBTYPE) Is PdfName.POPUP Then
                                                'Dim stickyNote As PdfDictionary = DirectCast(annot.GetDirectObject(PdfName.PARENT), PdfDictionary)
                                                Dim stickyNote As PdfDictionary = DirectCast(pdfReaderInstance.GetPdfObject(annot.GetAsIndirectObject(PdfName.PARENT).Number), PdfDictionary)
                                                'Links.Add(New Link(parent.getRectangleScreen(annot.GetAsArray(PdfName.RECT), pageIndex + 1), pageIndex + 1, parent.getRectangleScreen(stickyNote.GetAsArray(PdfName.RECT), pageIndex + 1), stickyNote.GetAsString(PdfName.RC).ToUnicodeString(), annot.GetAsBoolean(PdfName.OPEN).BooleanValue, stickyNote, annot, annotIndex))
                                                Dim stickyNoteRef As PdfIndirectReference = annot.GetAsIndirectObject(PdfName.PARENT)
                                                'Dim stickyNote As PdfDictionary = DirectCast(pdfReaderInstance.GetPdfObject(stickyNoteRef.Number), PdfDictionary)
                                                Links.Add(New Link(parent.getRectangleScreen(annot.GetAsArray(PdfName.RECT), pageIndex + 1), pageIndex + 1, parent.getRectangleScreen(stickyNote.GetAsArray(PdfName.RECT), pageIndex + 1), CStr(stickyNote.GetAsString(PdfName.RC).ToUnicodeString()), CBool(annot.GetAsBoolean(PdfName.OPEN).BooleanValue), annot, stickyNoteRef, annotIndex))
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        ElseIf pages.GetAsArray(PdfName.KIDS).GetAsDict(pageIndex).GetAsArray(PdfName.ANNOTS)(annotIndex).IsIndirect Then
                            If Not pages.GetAsArray(PdfName.KIDS).GetAsDict(pageIndex).GetAsArray(PdfName.ANNOTS).GetAsDict(annotIndex) Is Nothing Then
                                Dim annot As PdfDictionary = DirectCast(pages.GetAsArray(PdfName.KIDS).GetAsDict(pageIndex).GetAsArray(PdfName.ANNOTS).GetAsDict(annotIndex), PdfDictionary) '
                                If Not annot Is Nothing Then
                                    If Not annot.Get(PdfName.SUBTYPE) Is Nothing Then
                                        If Not annot.Get(PdfName.SUBTYPE) Is Nothing Then
                                            If annot.Get(PdfName.SUBTYPE).IsName Then
                                                If annot.GetAsName(PdfName.SUBTYPE) Is PdfName.LINK Then
                                                    If Not annot.Get(PdfName.RECT) Is Nothing Then
                                                        If Not annot.Get(PdfName.A) Is Nothing Then
                                                            If annot.Get(PdfName.A).IsDictionary Then
                                                                Dim a As PdfDictionary = annot.GetAsDict(PdfName.A)
                                                                If Not a Is Nothing Then
                                                                    If Not a.Get(PdfName.S) Is Nothing Then
                                                                        If a.Get(PdfName.S).IsName Then
                                                                            If a.GetAsName(PdfName.S) Is PdfName.URI Then
                                                                                Links.Add(New Link(parent.getRectangleScreen(annot.GetAsArray(PdfName.RECT), pageIndex + 1), a.GetAsString(PdfName.URI).ToUnicodeString()))
                                                                            ElseIf a.GetAsName(PdfName.S) Is PdfName.GOTO Then 'GoTo Destination

                                                                            End If
                                                                        End If
                                                                    End If
                                                                End If
                                                            End If
                                                        End If
                                                    End If
                                                ElseIf annot.GetAsName(PdfName.SUBTYPE) Is PdfName.POPUP Then
                                                    'Dim stickyNote As PdfDictionary = DirectCast(annot.GetDirectObject(PdfName.PARENT), PdfDictionary)
                                                    Dim stickyNote As PdfDictionary = DirectCast(pdfReaderInstance.GetPdfObject(annot.GetAsIndirectObject(PdfName.PARENT).Number), PdfDictionary)
                                                    'Links.Add(New Link(parent.getRectangleScreen(annot.GetAsArray(PdfName.RECT), pageIndex + 1), pageIndex + 1, parent.getRectangleScreen(stickyNote.GetAsArray(PdfName.RECT), pageIndex + 1), stickyNote.GetAsString(PdfName.RC).ToUnicodeString(), annot.GetAsBoolean(PdfName.OPEN).BooleanValue, stickyNote, annot, annotIndex))
                                                    Dim stickyNoteRef As PdfIndirectReference = annot.GetAsIndirectObject(PdfName.PARENT)
                                                    'Dim stickyNote As PdfDictionary = DirectCast(pdfReaderInstance.GetPdfObject(stickyNoteRef.Number), PdfDictionary)
                                                    Links.Add(New Link(parent.getRectangleScreen(annot.GetAsArray(PdfName.RECT), pageIndex + 1), pageIndex + 1, parent.getRectangleScreen(stickyNote.GetAsArray(PdfName.RECT), pageIndex + 1), CStr(stickyNote.GetAsString(PdfName.RC).ToUnicodeString()), CBool(annot.GetAsBoolean(PdfName.OPEN).BooleanValue), annot, stickyNoteRef, annotIndex))
                                                End If

                                            End If
                                        End If
                                    End If
                                End If
                            End If

                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            cntrX = cntrX
            Err.Clear()
        End Try
        Try
            ' parent.TimeStampAdd("OpenFile(cLinks.LoadLinksOnPage(" & pageIndex & "))-LoadInternalLinksOnPage-start")
            Dim l As List(Of Link)
            l = LoadInternalLinksOnPage(pageIndex)
            If l.Count > 0 Then
                Links.AddRange(l.ToArray())
            End If
            ' parent.TimeStampAdd("OpenFile(cLinks.LoadLinksOnPage(" & pageIndex & "))-LoadInternalLinksOnPage-end")
            l.Clear()
            l = New List(Of Link)
            ' parent.TimeStampAdd("OpenFile(cLinks.LoadLinksOnPage(" & pageIndex & "))-LoadInternalLinksOnPageNamedDestinations-start")
            l = LoadInternalLinksOnPageNamedDestinations(pageIndex)
            If l.Count > 0 Then
                Links.AddRange(l.ToArray())
            End If
            ' parent.TimeStampAdd("OpenFile(cLinks.LoadLinksOnPage(" & pageIndex & "))-LoadInternalLinksOnPageNamedDestinations-end")
            l.Clear()
            ' parent.TimeStampAdd("OpenFile(cLinks.LoadLinksOnPage(" & pageIndex & "))-LoadImageCoordinatesOnPage-start")
            l = New List(Of Link)
            l = LoadImageCoordinatesOnPage(pageIndex)
            If l.Count > 0 Then
                Links.AddRange(l.ToArray())
            End If
            ' parent.TimeStampAdd("OpenFile(cLinks.LoadLinksOnPage(" & pageIndex & "))-LoadImageCoordinatesOnPage-end")
            l.Clear()
            ' parent.TimeStampAdd("OpenFile(cLinks.LoadLinksOnPage(" & pageIndex & "))-LoadImageCoordinatesOnPage-start")
            l = New List(Of Link)
            l = LoadTextCoordinatesOnPage(pageIndex)
            If l.Count > 0 Then
                Links.AddRange(l.ToArray())
            End If
            l.Clear()
        Catch ex As Exception
            Err.Clear()
        End Try
        ' parent.TimeStampAdd("OpenFile(cLinks.LoadLinksOnPage(" & pageIndex & ")-end)")
        Return Links
    End Function
    Public Function LoadInternalLinksOnPage(ByVal pageIndex As Integer) As List(Of Link)
        Dim lnks As New List(Of Link)
        Try
            For Each l As iTextSharp.text.pdf.PdfAnnotation.PdfImportedLink In pdfReaderInstance.GetLinks(pageIndex + 1).ToArray
                Try
                    If l.IsInternal Then
                        Dim lnk As New Link(parent.getRectangleScreen(l.GetRect(), pageIndex + 1), l.GetDestinationPage() - 1)
                        If Not lnks.Contains(lnk) Then
                            lnks.Add(lnk)
                        End If
                    End If
                Catch ex As Exception
                    Err.Clear()
                End Try
            Next
        Catch ex As Exception
            Err.Clear()
        End Try
        Return lnks
    End Function
    Public Function LoadImageCoordinatesOnPage(ByVal pageIndex As Integer) As List(Of Link)
        Dim lnks As New List(Of Link)
        Try
            'Dim clsImageCoordinates As New clsImageCoordinateListener(parent, lnks)
            Dim parser As iTextSharp.text.pdf.parser.PdfReaderContentParser = New iTextSharp.text.pdf.parser.PdfReaderContentParser(pdfReaderInstance)
            Dim listener As clsImageCoordinateListener = New clsImageCoordinateListener(parent, lnks)
            Dim i As Integer = 1
            'Do While (i <= pdfReaderInstance.NumberOfPages)
            parser.ProcessContent(pageIndex + 1, listener)
            'i = (i + 1)
            'Loop
        Catch ex As Exception
            Err.Clear()
        End Try
        Return lnks
    End Function
    Public Function LoadTextCoordinatesOnPage(ByVal pageIndex As Integer) As List(Of Link)
        Dim lnks As New List(Of Link)
        Try
            'Dim clsImageCoordinates As New clsImageCoordinateListener(parent, lnks)
            Dim parser As iTextSharp.text.pdf.parser.PdfReaderContentParser = New iTextSharp.text.pdf.parser.PdfReaderContentParser(pdfReaderInstance)
            Dim listener As clsTextRenderListener = New clsTextRenderListener(parent, lnks)
            Dim i As Integer = 1
            'Do While (i <= pdfReaderInstance.NumberOfPages)
            parser.ProcessContent(pageIndex + 1, listener)
            'i = (i + 1)
            'Loop
        Catch ex As Exception
            Err.Clear()
        End Try
        Return lnks
    End Function
    'Public Function LoadTextCoordinatesOnPage(ByVal pageIndex As Integer) As List(Of Link)
    '    Dim lnks As New List(Of Link)
    '    Try
    '        Dim parser As iTextSharp.text.pdf.parser.PdfReaderContentParser = New iTextSharp.text.pdf.parser.PdfReaderContentParser(pdfReaderInstance)
    '        Dim strategy As iTextSharp.text.pdf.parser.ITextExtractionStrategy
    '        Dim finder As iTextSharp.text.pdf.parser.TextMarginFinder
    '        'For i As Integer = 1 To reader.NumberOfPages
    '        finder = parser.ProcessContent(pageIndex + 1, New iTextSharp.text.pdf.parser.TextMarginFinder)
    '        Dim area As New iTextSharp.text.Rectangle(finder.GetLlx(), finder.GetLly(), finder.GetWidth() / 2, finder.GetHeight() / 2)
    '        Dim filter As iTextSharp.text.pdf.parser.RenderFilter = New iTextSharp.text.pdf.parser.RegionTextRenderFilter(area)
    '        strategy = New iTextSharp.text.pdf.parser.FilteredTextRenderListener(New iTextSharp.text.pdf.parser.LocationTextExtractionStrategy(), filter)
    '        'sw.WriteLine(PdfTextExtractor.GetTextFromPage(reader, i, strategy))
    '        'Next

    '    Catch ex As Exception
    '        Err.Clear()
    '    End Try
    '    Return lnks
    'End Function
    Public Function LoadInternalLinksOnPageNamedDestinations(ByVal pageIndex As Integer) As List(Of Link)
        Dim pageHeight As Single = parent.getPDFHeight()
        Dim lnks As New List(Of Link)
        Try
            If Not pages.GetAsArray(PdfName.KIDS).GetAsDict(pageIndex).Get(PdfName.ANNOTS) Is Nothing Then
                For annotIndex As Integer = 0 To pages.GetAsArray(PdfName.KIDS).GetAsDict(pageIndex).GetAsArray(PdfName.ANNOTS).ArrayList.Count - 1
                    Dim annot As PdfDictionary = DirectCast(pages.GetAsArray(PdfName.KIDS).GetAsDict(pageIndex).GetAsArray(PdfName.ANNOTS).GetAsDict(annotIndex), PdfDictionary) '
                    If Not annot.Get(PdfName.SUBTYPE) Is Nothing Then
                        If Not annot.Get(PdfName.SUBTYPE) Is Nothing Then
                            If annot.Get(PdfName.SUBTYPE).IsName Then
                                If annot.GetAsName(PdfName.SUBTYPE) Is PdfName.LINK Then
                                    If Not annot.Get(PdfName.RECT) Is Nothing Then
                                        If Not annot.Get(PdfName.A) Is Nothing Then
                                            If annot.Get(PdfName.A).IsDictionary Then
                                                Dim a As PdfDictionary = annot.GetAsDict(PdfName.A)
                                                If Not a Is Nothing Then
                                                    If Not a.Get(PdfName.S) Is Nothing Then
                                                        If a.GetAsName(PdfName.S) Is PdfName.GOTO Then 'GoTo Destination
                                                            lnks.Add(New Link(parent.getRectangleScreen(annot.GetAsArray(PdfName.RECT), pageIndex + 1), GetNamedDestinationPageIndex(a.GetAsString(PdfName.D).ToUnicodeString())))
                                                        End If
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
        Return lnks
    End Function
    Public Class named_destinations
    End Class
    Public Function GetNamedDestinationPageIndex(ByVal destinationName As String) As Integer
        Dim namesDict As PdfDictionary = DirectCast(pdfReaderInstance.Catalog.GetAsDict(PdfName.NAMES), PdfDictionary)
        Dim arrNames As PdfArray = namesDict.GetAsDict(PdfName.DESTS).GetAsArray(PdfName.KIDS).GetAsDict(0).GetAsArray(PdfName.NAMES)
        For i As Integer = 0 To arrNames.Size - 1 Step 2
            Try
                If arrNames(i).IsString Then
                    Dim destName As String = arrNames.GetAsString(i).ToUnicodeString()
                    If destinationName = destName Then
                        If i < arrNames.Size Then
                            If Not arrNames(CInt(i + 1)) Is Nothing Then
                                If Not arrNames.GetAsDict(CInt(i + 1)) Is Nothing Then
                                    Dim pageIndirectReferenceNumber As Integer = arrNames.GetAsDict(CInt(i + 1)).GetAsArray(PdfName.D).GetAsIndirectObject(0).Number
                                    Dim pageIndex As Integer = findPageIndex(pageIndirectReferenceNumber)
                                    Return pageIndex
                                End If
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                Err.Clear()
            End Try
        Next
        Return -1
    End Function
    Public Function LoadBookmarkLinksOnPage(ByVal pageIndex As Integer) As List(Of Link)
        Dim lnks As New List(Of Link)
        Try
        Catch ex As Exception
            Err.Clear()
        End Try
        Return lnks
    End Function
End Class
