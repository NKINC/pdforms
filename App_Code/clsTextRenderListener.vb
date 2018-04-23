Imports iTextSharp.text.pdf
Public Class clsTextRenderListener
    Implements iTextSharp.text.pdf.parser.ITextExtractionStrategy
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 Nicholas Kowalewicz All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>


    Private result As New System.Text.StringBuilder()



    Private lastBaseLine As iTextSharp.text.pdf.parser.Vector

    Public strings As List(Of String) = New List(Of String)()
    Public baselines As New List(Of Single)()
    Public parent As frmMain
    Public page As Integer
    Public Links As New List(Of clsLinks.Link)
    Private counter As Integer = 100000
    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(ByRef frm1 As frmMain, ByRef Links1 As List(Of clsLinks.Link))
        MyBase.New()
        Me.parent = frm1
        Me.Links = Links1
    End Sub
    Public Shared Sub setImageCoordinatesFormMain(ByVal reader As PdfReader, ByVal page1 As Integer, ByRef frm1 As frmMain, ByRef Links1 As List(Of clsLinks.Link))
        Dim parser As iTextSharp.text.pdf.parser.PdfReaderContentParser = New iTextSharp.text.pdf.parser.PdfReaderContentParser(reader)
        Dim listener As clsTextRenderListener = New clsTextRenderListener(frm1, Links1)
        parser.ProcessContent(page1, listener)
    End Sub

    Public Sub RenderText(renderInfo As iTextSharp.text.pdf.parser.TextRenderInfo) Implements iTextSharp.text.pdf.parser.IRenderListener.RenderText

        Dim curBaseline As iTextSharp.text.pdf.parser.Vector = renderInfo.GetBaseline().GetStartPoint()
        '

        If (Me.lastBaseLine IsNot Nothing) AndAlso (curBaseline(iTextSharp.text.pdf.parser.Vector.I2) <> lastBaseLine(iTextSharp.text.pdf.parser.Vector.I2)) Then

            If (Not [String].IsNullOrWhiteSpace(Me.result.ToString())) Then

                Dim x As Single = renderInfo.GetBaseline().GetBoundingRectange().X
                Dim y As Single = renderInfo.GetBaseline().GetBoundingRectange().Y
                Dim w As Single = renderInfo.GetBaseline().GetBoundingRectange().Width

                Dim h As Single = renderInfo.GetBaseline().GetBoundingRectange().Height
                Me.baselines.Add(Me.lastBaseLine(iTextSharp.text.pdf.parser.Vector.I2))
                Me.strings.Add(Me.result.ToString())

                Dim lnk As New clsLinks.Link(parent.getRectangleScreen(New System.Drawing.RectangleF(x, y, w, h)), page, Me.result.ToString())
                If Not Links.Contains(lnk) Then
                    Links.Add(lnk)
                End If
            End If

            Me.result.Clear()
        End If


        Me.result.Append(renderInfo.GetText())


        Me.lastBaseLine = curBaseline
    End Sub

    Public Function GetResultantText() As String Implements iTextSharp.text.pdf.parser.ITextExtractionStrategy.GetResultantText

        If (Not [String].IsNullOrWhiteSpace(Me.result.ToString())) Then
            Me.baselines.Add(Me.lastBaseLine(iTextSharp.text.pdf.parser.Vector.I2))
            Me.strings.Add(Me.result.ToString())




        End If

        Return Nothing
    End Function


    Public Sub BeginTextBlock() Implements iTextSharp.text.pdf.parser.IRenderListener.BeginTextBlock
    End Sub
    Public Sub EndTextBlock() Implements iTextSharp.text.pdf.parser.IRenderListener.EndTextBlock
    End Sub
    Public Sub RenderImage(renderInfo As iTextSharp.text.pdf.parser.ImageRenderInfo) Implements iTextSharp.text.pdf.parser.IRenderListener.RenderImage

    End Sub



End Class
