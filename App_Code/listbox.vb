Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.IO
Public Class listboxTest
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 Nicholas Kowalewicz All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>

    Private Sub RunTest()
        Dim cList As New listboxTest()
        Dim fn As String = Application.StartupPath.ToString.TrimEnd("\"c) & "\listbox-error.pdf"
        Dim b() As Byte = cList.addListBox(System.IO.File.ReadAllBytes(fn), New iTextSharp.text.Rectangle(231.67, 108.0, 395.67, 197.0), "ListBox1", "ListBox1", 1)
        File.WriteAllBytes(fn, b)
        Process.Start(fn)
    End Sub
    Public Function addListBox(ByVal pdfBytes() As Byte, ByVal newRect As Rectangle, ByVal newFldName As String, ByVal oldfldname As String, ByVal pg As Integer) As Byte()
        Dim pdfReaderDoc As New PdfReader(pdfBytes)
        Dim m As New System.IO.MemoryStream
        Dim b() As Byte = Nothing
        Try
            With New PdfStamper(pdfReaderDoc, m)
                Dim txtField As iTextSharp.text.pdf.TextField
                txtField = New iTextSharp.text.pdf.TextField(.Writer, newRect, newFldName)
                txtField.TextColor = BaseColor.BLACK
                txtField.BackgroundColor = BaseColor.WHITE
                txtField.BorderColor = BaseColor.BLACK
                txtField.FieldName = newFldName
                txtField.Alignment = 0
                txtField.BorderStyle = 0
                txtField.BorderWidth = 1.0F
                txtField.Visibility = TextField.VISIBLE
                txtField.Rotation = 0
                txtField.Box = newRect
                Dim opt As New PdfArray
                Dim ListBox_ItemDisplay As New List(Of String)
                ListBox_ItemDisplay.Add("One")
                ListBox_ItemDisplay.Add("Two")
                ListBox_ItemDisplay.Add("Three")
                ListBox_ItemDisplay.Add("Four")
                ListBox_ItemDisplay.Add("Five")
                Dim ListBox_ItemValue As New List(Of String)
                ListBox_ItemValue.Add("1X")
                ListBox_ItemValue.Add("2X")
                ListBox_ItemValue.Add("3X")
                ListBox_ItemValue.Add("4X")
                ListBox_ItemValue.Add("5X")
                txtField.Options += iTextSharp.text.pdf.TextField.MULTISELECT
                Dim selIndex As New List(Of Integer)
                Dim selValues As New List(Of String)
                selIndex.Add(CInt(1))
                selIndex.Add(CInt(3))
                txtField.Choices = ListBox_ItemDisplay.ToArray
                txtField.ChoiceExports = ListBox_ItemValue.ToArray
                txtField.ChoiceSelections = selIndex
                Dim listField As PdfFormField = txtField.GetListField
                If Not String.IsNullOrEmpty(oldfldname & "") Then
                    .AcroFields.RemoveField(oldfldname, pg)
                End If
                .AddAnnotation(listField, pg)
                .Writer.CloseStream = False
                .Close()
                If m.CanSeek Then
                    m.Position = 0
                End If
                b = m.ToArray
                m.Close()
                m.Dispose()
                pdfReaderDoc.Close()
            End With
            Return b.ToArray
        Catch ex As Exception
            Err.Clear()
        Finally
            b = Nothing
        End Try
        Return Nothing
    End Function
End Class
