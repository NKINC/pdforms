Public Class WIA_PROPERTIES
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 NK-INC.COM All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>

    Public Const WIA_RESERVED_FOR_NEW_PROPS As UInteger = 1024
    Public Const WIA_DIP_FIRST As UInteger = 2
    Public Const WIA_DPA_FIRST As UInteger = WIA_DIP_FIRST + WIA_RESERVED_FOR_NEW_PROPS
    Public Const WIA_DPC_FIRST As UInteger = WIA_DPA_FIRST + WIA_RESERVED_FOR_NEW_PROPS



    Public Const WIA_DPS_FIRST As UInteger = WIA_DPC_FIRST + WIA_RESERVED_FOR_NEW_PROPS
    Public Const WIA_DPS_DOCUMENT_HANDLING_STATUS As UInteger = WIA_DPS_FIRST + 13
    Public Const WIA_DPS_DOCUMENT_HANDLING_SELECT As UInteger = WIA_DPS_FIRST + 14
End Class
