Imports System.Collections.Generic
Imports iTextSharp.text.pdf
Public Class clsLinks
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 Nicholas Kowalewicz All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
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
        Sub New(ByVal LinkRect As System.Drawing.RectangleF, ByVal DestPage As Integer)
            Link_Rect = LinkRect
            Link_Destination_PageIndex = DestPage
            Link_Destination_URI = ""
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
    Public Sub New(ByVal pdfReader1 As PdfReader, ByRef parent1 As frmMain) 'ByVal pageIndex As Integer, 
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
        Dim pageHeight As Single = parent.getPDFHeight()
        Links = New List(Of Link)
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
            Dim l As List(Of Link)
            l = LoadInternalLinksOnPage(pageIndex)
            If l.Count > 0 Then
                Links.AddRange(l.ToArray())
            End If
            l.Clear()
            l = New List(Of Link)
            l = LoadInternalLinksOnPageNamedDestinations(pageIndex)
            If l.Count > 0 Then
                Links.AddRange(l.ToArray())
            End If
            l.Clear()
            l = New List(Of Link)
            l = LoadImageCoordinatesOnPage(pageIndex)
            If l.Count > 0 Then
                Links.AddRange(l.ToArray())
            End If
            l.Clear()
            l = New List(Of Link)
            l = LoadTextCoordinatesOnPage(pageIndex)
            If l.Count > 0 Then
                Links.AddRange(l.ToArray())
            End If
            l.Clear()
        Catch ex As Exception
            Err.Clear()
        End Try
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
            Dim parser As iTextSharp.text.pdf.parser.PdfReaderContentParser = New iTextSharp.text.pdf.parser.PdfReaderContentParser(pdfReaderInstance)
            Dim listener As clsImageCoordinateListener = New clsImageCoordinateListener(parent, lnks)
            Dim i As Integer = 1
            parser.ProcessContent(pageIndex + 1, listener)
        Catch ex As Exception
            Err.Clear()
        End Try
        Return lnks
    End Function
    Public Function LoadTextCoordinatesOnPage(ByVal pageIndex As Integer) As List(Of Link)
        Dim lnks As New List(Of Link)
        Try
            Dim parser As iTextSharp.text.pdf.parser.PdfReaderContentParser = New iTextSharp.text.pdf.parser.PdfReaderContentParser(pdfReaderInstance)
            Dim listener As clsTextRenderListener = New clsTextRenderListener(parent, lnks)
            Dim i As Integer = 1
            parser.ProcessContent(pageIndex + 1, listener)
        Catch ex As Exception
            Err.Clear()
        End Try
        Return lnks
    End Function
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
