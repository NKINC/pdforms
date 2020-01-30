Imports System.Windows.Forms
Imports System
Imports System.Reflection
Namespace FolderSelect
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 NK-INC.COM All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary> 
    Public Class FolderSelectDialog
        Private ofd As System.Windows.Forms.OpenFileDialog = Nothing
        Public Class Reflector
#Region "variables"
            Private m_ns As String
            Private m_asmb As System.Reflection.Assembly
#End Region
#Region "Constructors"
            ''' <summary>
            ''' Constructor
            ''' </summary>
            ''' <param name="ns">The namespace containing types to be used</param>
            Public Sub New(ByVal ns As String)
                Me.New(ns, ns)
            End Sub
            ''' <summary>
            ''' Constructor
            ''' </summary>
            ''' <param name="an__1">A specific assembly name (used if the assembly name does not tie exactly with the namespace)</param>
            ''' <param name="ns">The namespace containing types to be used</param>
            Public Sub New(ByVal an__1 As String, ByVal ns As String)
                m_ns = ns
                m_asmb = Nothing
                For Each aN__2 As AssemblyName In System.Reflection.Assembly.GetExecutingAssembly().GetReferencedAssemblies()
                    If aN__2.FullName.StartsWith(an__1) Then
                        m_asmb = System.Reflection.Assembly.Load(aN__2)
                        Exit For
                    End If
                Next
            End Sub
#End Region
#Region "Methods"
            ''' <summary>
            ''' 
            ''' </summary>
            ''' <param name="typeName"></param>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Overloads Function [GetType](ByVal typeName As String) As System.Type
                Dim type As Type = Nothing
                Dim names As String() = typeName.Split("."c)
                If names.Length > 0 Then
                    type = m_asmb.[GetType]((m_ns & Convert.ToString("."c)) + names(0))
                End If
                For i As Integer = 1 To names.Length - 1
                    type = type.GetNestedType(names(i), BindingFlags.NonPublic)
                Next
                Return type
            End Function
            ''' <summary>
            ''' Create a new object of a named type passing along any params
            ''' </summary>
            ''' <param name="name">The name of the type to create</param>
            ''' <param name="parameters"></param>
            ''' <returns>An instantiated type</returns>
            Public Function [New](ByVal name As String, ByVal ParamArray parameters As Object()) As Object
                Dim type As Type = [GetType](name)
                Dim ctorInfos As ConstructorInfo() = type.GetConstructors()
                For Each ci As ConstructorInfo In ctorInfos
                    Try
                        Return ci.Invoke(parameters)
                    Catch
                    End Try
                Next
                Return Nothing
            End Function
            ''' <summary>
            ''' Calls method 'func' on object 'obj' passing parameters 'parameters'
            ''' </summary>
            ''' <param name="obj">The object on which to excute function 'func'</param>
            ''' <param name="func">The function to execute</param>
            ''' <param name="parameters">The parameters to pass to function 'func'</param>
            ''' <returns>The result of the function invocation</returns>
            Public Function [Call](ByVal obj As Object, ByVal func As String, ByVal ParamArray parameters As Object()) As Object
                Return Call2(obj, func, parameters)
            End Function
            ''' <summary>
            ''' Calls method 'func' on object 'obj' passing parameters 'parameters'
            ''' </summary>
            ''' <param name="obj">The object on which to excute function 'func'</param>
            ''' <param name="func">The function to execute</param>
            ''' <param name="parameters">The parameters to pass to function 'func'</param>
            ''' <returns>The result of the function invocation</returns>
            Public Function Call2(ByVal obj As Object, ByVal func As String, ByVal parameters As Object()) As Object
                Return CallAs2(obj.[GetType](), obj, func, parameters)
            End Function
            ''' <summary>
            ''' Calls method 'func' on object 'obj' which is of type 'type' passing parameters 'parameters'
            ''' </summary>
            ''' <param name="type">The type of 'obj'</param>
            ''' <param name="obj">The object on which to excute function 'func'</param>
            ''' <param name="func">The function to execute</param>
            ''' <param name="parameters">The parameters to pass to function 'func'</param>
            ''' <returns>The result of the function invocation</returns>
            Public Function CallAs(ByVal type As Type, ByVal obj As Object, ByVal func As String, ByVal ParamArray parameters As Object()) As Object
                Return CallAs2(type, obj, func, parameters)
            End Function
            ''' <summary>
            ''' Calls method 'func' on object 'obj' which is of type 'type' passing parameters 'parameters'
            ''' </summary>
            ''' <param name="type">The type of 'obj'</param>
            ''' <param name="obj">The object on which to excute function 'func'</param>
            ''' <param name="func">The function to execute</param>
            ''' <param name="parameters">The parameters to pass to function 'func'</param>
            ''' <returns>The result of the function invocation</returns>
            Public Function CallAs2(ByVal type As Type, ByVal obj As Object, ByVal func As String, ByVal parameters As Object()) As Object
                Dim methInfo As MethodInfo = type.GetMethod(func, BindingFlags.Instance Or BindingFlags.[Public] Or BindingFlags.NonPublic)
                Return methInfo.Invoke(obj, parameters)
            End Function
            ''' <summary>
            ''' Returns the value of property 'prop' of object 'obj'
            ''' </summary>
            ''' <param name="obj">The object containing 'prop'</param>
            ''' <param name="prop">The property name</param>
            ''' <returns>The property value</returns>
            Public Function [Get](ByVal obj As Object, ByVal prop As String) As Object
                Return GetAs(obj.[GetType](), obj, prop)
            End Function
            ''' <summary>
            ''' Returns the value of property 'prop' of object 'obj' which has type 'type'
            ''' </summary>
            ''' <param name="type">The type of 'obj'</param>
            ''' <param name="obj">The object containing 'prop'</param>
            ''' <param name="prop">The property name</param>
            ''' <returns>The property value</returns>
            Public Function GetAs(ByVal type As Type, ByVal obj As Object, ByVal prop As String) As Object
                Dim propInfo As PropertyInfo = type.GetProperty(prop, BindingFlags.Instance Or BindingFlags.[Public] Or BindingFlags.NonPublic)
                Return propInfo.GetValue(obj, Nothing)
            End Function
            ''' <summary>
            ''' Returns an enum value
            ''' </summary>
            ''' <param name="typeName">The name of enum type</param>
            ''' <param name="name">The name of the value</param>
            ''' <returns>The enum value</returns>
            Public Function GetEnum(ByVal typeName As String, ByVal name As String) As Object
                Dim type As Type = [GetType](typeName)
                Dim fieldInfo As FieldInfo = type.GetField(name)
                Return fieldInfo.GetValue(Nothing)
            End Function
#End Region
        End Class
        ''' <summary>
        ''' Default constructor
        ''' </summary>
        Public Sub New()
            ofd = New System.Windows.Forms.OpenFileDialog()
            ofd.Filter = "Folders|" & vbLf
            ofd.AddExtension = False
            ofd.CheckFileExists = False
            ofd.DereferenceLinks = True
            ofd.Multiselect = False
        End Sub
        Public Sub New(ByRef ofd1 As System.Windows.Forms.OpenFileDialog)
            If ofd1 Is Nothing Then
                ofd1 = New System.Windows.Forms.OpenFileDialog()
            End If
            ofd = ofd1
            ofd.Filter = "Folders|" & vbLf
            ofd.AddExtension = False
            ofd.CheckFileExists = False
            ofd.DereferenceLinks = True
            ofd.Multiselect = False
        End Sub
        <STAThread()> _
        Private Shared Sub Main(ByVal args As String())
            Console.WriteLine(Environment.OSVersion.Platform.ToString())
            Console.WriteLine(Environment.OSVersion.Version.Major)
            If True Then
                Dim fsd As New FolderSelectDialog()
                fsd.Title = "What to select"
                fsd.InitialDirectory = "c:\"
                If fsd.ShowDialog(IntPtr.Zero) Then
                    Console.WriteLine(fsd.FileName)
                End If
            End If
        End Sub
#Region "Properties"
        ''' <summary>
        ''' Gets/Sets the initial folder to be selected. A null value selects the current directory.
        ''' </summary>
        Public Property InitialDirectory() As String
            Get
                Return ofd.InitialDirectory
            End Get
            Set(ByVal value As String)
                ofd.InitialDirectory = If(value Is Nothing OrElse value.Length = 0, Environment.CurrentDirectory, value)
            End Set
        End Property
        ''' <summary>
        ''' Gets/Sets the title to show in the dialog
        ''' </summary>
        Public Property Title() As String
            Get
                Return ofd.Title
            End Get
            Set(ByVal value As String)
                ofd.Title = If(value Is Nothing, "Select a folder", value)
            End Set
        End Property
        ''' <summary>
        ''' Gets the selected folder
        ''' </summary>
        Public ReadOnly Property FileName() As String
            Get
                Return ofd.FileName
            End Get
        End Property
#End Region
#Region "Methods"
        ''' <summary>
        ''' Shows the dialog
        ''' </summary>
        ''' <returns>True if the user presses OK else false</returns>
        Public Function ShowDialog() As Boolean
            Return ShowDialog(IntPtr.Zero)
        End Function
        ''' <summary>
        ''' Shows the dialog
        ''' </summary>
        ''' <param name="hWndOwner">Handle of the control to be parent</param>
        ''' <returns>True if the user presses OK else false</returns>
        Public Function ShowDialog(ByVal hWndOwner As IntPtr) As Boolean
            Dim flag As Boolean = False
            If Environment.OSVersion.Version.Major >= 6 Then
                Dim r As New Reflector("System.Windows.Forms")
                Dim num As UInteger = 0
                Dim typeIFileDialog As Type = r.[GetType]("FileDialogNative.IFileDialog")
                Dim dialog As Object = r.[Call](ofd, "CreateVistaDialog")
                r.[Call](ofd, "OnBeforeVistaDialog", dialog)
                Dim options As UInteger = CUInt(r.CallAs(GetType(System.Windows.Forms.FileDialog), ofd, "GetOptions"))
                options = options Or CUInt(r.GetEnum("FileDialogNative.FOS", "FOS_PICKFOLDERS"))
                r.CallAs(typeIFileDialog, dialog, "SetOptions", options)
                Dim pfde As Object = r.[New]("FileDialog.VistaDialogEvents", ofd)
                Dim parameters As Object() = New Object() {pfde, num}
                r.CallAs2(typeIFileDialog, dialog, "Advise", parameters)
                num = CUInt(parameters(1))
                Try
                    Dim num2 As Integer = CInt(r.CallAs(typeIFileDialog, dialog, "Show", hWndOwner))
                    flag = 0 = num2
                Finally
                    r.CallAs(typeIFileDialog, dialog, "Unadvise", num)
                    GC.KeepAlive(pfde)
                End Try
            Else
                Dim fbd As New FolderBrowserDialog()
                fbd.Description = Me.Title
                fbd.SelectedPath = Me.InitialDirectory
                fbd.ShowNewFolderButton = False
                If fbd.ShowDialog(New WindowWrapper(hWndOwner)) <> DialogResult.OK Then
                    Return False
                End If
                ofd.FileName = fbd.SelectedPath
                flag = True
            End If
            Return flag
        End Function
        ''' <summary>
        ''' Creates IWin32Window around an IntPtr
        ''' </summary>
        Public Class WindowWrapper
            Implements System.Windows.Forms.IWin32Window
            ''' <summary>
            ''' Constructor
            ''' </summary>
            ''' <param name="handle">Handle to wrap</param>
            Public Sub New(ByVal handle As IntPtr)
                _hwnd = handle
            End Sub
            ''' <summary>
            ''' Original ptr
            ''' </summary>
            Public ReadOnly Property Handle() As IntPtr Implements IWin32Window.Handle
                Get
                    Return _hWnd
                End Get
            End Property
            Private _hwnd As IntPtr
        End Class
#End Region
    End Class
End Namespace
