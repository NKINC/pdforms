Option Strict Off
Option Explicit On
Module modADOConn
    ''' <summary>
    ''' PdForms.net - An open source pdf form editor
    ''' Copyright 2018 NK-INC.COM All Rights reserved.
    ''' PdForms.net utilizes iTextSharp technologies.
    ''' Website: www.pdforms.net (source code), www.pdforms.com (about)
    ''' </summary>

    Private ConnectionStringNet As String
    Private ConnectionStringVB As String
    Private ObjectPrefix As String
    Public Property Object_Prefix() As String
        Get
            Object_Prefix = ObjectPrefix
        End Get
        Set(ByVal Value As String)
            ObjectPrefix = Value
        End Set
    End Property
    Public Property Connection_String_Net() As String
        Get
            Connection_String_Net = ConnectionStringNet
        End Get
        Set(ByVal Value As String)
            ConnectionStringNet = Value
        End Set
    End Property
    Public Property Connection_String_VB() As String
        Get
            Connection_String_VB = ConnectionStringVB
        End Get
        Set(ByVal Value As String)
            ConnectionStringVB = Value
        End Set
    End Property
    Public Sub OLEDB_ActiveDirService(ByVal UserName As String, ByVal Password As String)
        Connection_String_VB = "Provider=ADSDSOObject; User Id=" & UserName & ";Password=" & Password
        Connection_String_Net = "User Id=" & UserName & ";Password=" & Password
    End Sub
    Public Sub OLEDB_ActiveDirService_Net(ByVal UserName As String, ByVal Password As String)
    End Sub
    Public Sub OLEDB_Advantage(ByVal DataSource As String, ByVal Password As String)
        Connection_String_VB = "Provider=Advantage OLE DB Provider; Data source=" & DataSource & ";ServerType=ADS_LOCAL_SERVER;TableType=ADS_CDX"
        Connection_String_Net = "Provider=Advantage OLE DB Provider; Data source=" & DataSource & ";ServerType=ADS_LOCAL_SERVER;TableType=ADS_CDX"
        Object_Prefix = "OleDB"
    End Sub
    Public Sub OLEDB_AS400(ByVal DataSource As String, ByVal UserName As String, ByVal Password As String)
        Connection_String_VB = "Provider=IBMDA400; Data source=" & DataSource & ";User Id=" & UserName & ";Password=" & Password
        Connection_String_Net = "Provider=IBMDA400; Data source=" & DataSource & ";User Id=" & UserName & ";Password=" & Password
        Object_Prefix = "OleDB"
    End Sub
    Public Sub OLEDB_AS400wVSAM(ByVal DataSource As String, ByVal UserName As String, ByVal Password As String)
        Connection_String_VB = "Provider=SNAOLEDB; Data source=" & DataSource & ";User Id=" & UserName & ";Password=" & Password
        Connection_String_Net = "Provider=SNAOLEDB; Data source=" & DataSource & ";User Id=" & UserName & ";Password=" & Password
        Object_Prefix = "OleDB"
    End Sub
    Public Sub OLEDB_Commerce_WHouse1(ByVal ServerName As String, ByVal DatabaseName As String, ByVal UserName As String, ByVal Password As String)
        Connection_String_VB = "Provider=Commerce.DSO.1; DataSource=mscop://InProcConn/Server=" & ServerName & ":Catalog=DWSchema:Database=" & DatabaseName & ":User=" & UserName & ":Password=" & Password & ":FastLoad=True"
        Connection_String_Net = "Provider=Commerce.DSO.1; DataSource=mscop://InProcConn/Server=" & ServerName & ":Catalog=DWSchema:Database=" & DatabaseName & ":User=" & UserName & ":Password=" & Password & ":FastLoad=True"
        Object_Prefix = "OleDB"
    End Sub
    Public Sub OLEDB_Commerce_WHouse2(ByVal URL As String, ByVal ServerName As String, ByVal DatabaseName As String, ByVal UserName As String, ByVal Password As String)
        Connection_String_VB = "URL=" & URL & ":Database=" & DatabaseName & ":Catalog=DWSchema:User=" & UserName & ":Password=" & Password & ":FastLoad=True"
        Connection_String_Net = "URL=" & URL & ":Database=" & DatabaseName & ":Catalog=DWSchema:User=" & UserName & ":Password=" & Password & ":FastLoad=True"
        Object_Prefix = "OleDB"
    End Sub
    Public Sub OLEDB_Commerce_Profile1(ByVal ServerName As String, ByVal DatabaseName As String, ByVal UserName As String, ByVal Password As String)
        Connection_String_VB = "Provider=Commerce.DSO.1; DataSource=mscop://InProcConn/Server=" & ServerName & ":Catalog=Profile Definitions:Database=" & DatabaseName & ":User=" & UserName & ":Password=" & Password & ":FastLoad=True"
        Connection_String_Net = "Provider=Commerce.DSO.1; DataSource=mscop://InProcConn/Server=" & ServerName & ":Catalog=Profile Definitions:Database=" & DatabaseName & ":User=" & UserName & ":Password=" & Password & ":FastLoad=True"
        Object_Prefix = "OleDB"
    End Sub
    Public Sub OLEDB_Commerce_Profile2(ByVal URL As String, ByVal DatabaseName As String, ByVal UserName As String, ByVal Password As String)
        Connection_String_VB = "URL=" & URL & ":Database=" & DatabaseName & ":Catalog=Profile Definitions:User=" & UserName & ":Password=" & Password & ":FastLoad=True"
        Connection_String_Net = "URL=" & URL & ":Database=" & DatabaseName & ":Catalog=Profile Definitions:User=" & UserName & ":Password=" & Password & ":FastLoad=True"
        Object_Prefix = "OleDB"
    End Sub
    Public Sub OLEDB_DB2_TCPIP(ByVal Network_Address As String, ByVal sCatalog As String, ByVal PackageCollection As String, ByVal DefaultSchema As String, ByVal UserName As String, ByVal Password As String)
        Connection_String_VB = "Provider=DB2OLEDB;Network Transport Library=TCPIP;Network Address=" & Network_Address & ";Initial Catalog=" & sCatalog & ";Package Collection=" & PackageCollection & ";Default Schema=" & DefaultSchema & ";User Id=" & UserName & ";Password=" & Password
        Connection_String_Net = "Network Transport Library=TCPIP;Network Address=" & Network_Address & ";Initial Catalog=" & sCatalog & ";Package Collection=" & PackageCollection & ";Default Schema=" & DefaultSchema & ";User Id=" & UserName & ";Password=" & Password
        Object_Prefix = "OleDB"
    End Sub
    Public Sub OLEDB_DB2_APPC(ByVal LocalAlias As String, ByVal RemoteAlias As String, ByVal Catalog As String, ByVal PackageCollection As String, ByVal DefaultSchema As String, ByVal UserName As String, ByVal Password As String)
        Connection_String_VB = "Provider=DB2OLEDB;APPC Local LU Alias=" & LocalAlias & ";APPC Remote LU Alias=" & RemoteAlias & ";Package Collection=" & PackageCollection & ";Default Schema=" & DefaultSchema & ";User Id=" & UserName & ";Password=" & Password
        Connection_String_Net = "APPC Local LU Alias=" & LocalAlias & ";APPC Remote LU Alias=" & RemoteAlias & ";Package Collection=" & PackageCollection & ";Default Schema=" & DefaultSchema & ";User Id=" & UserName & ";Password=" & Password
        Object_Prefix = "OleDB"
    End Sub
    Public Sub OLEDB_DTSPackage(ByVal DataSource As String)
        Connection_String_VB = "Provider=DTSPackageDSO;Data Source=" & DataSource
        Connection_String_Net = "Provider=DTSPackageDSO;Data Source=" & DataSource
        Object_Prefix = "OleDB"
    End Sub
    Public Sub OLEDB_Exchange(ByVal xNothing As String)
        Connection_String_VB = "EXOLEDB.DataSource"
        Connection_String_Net = "EXOLEDB.DataSource"
        Object_Prefix = "OleDB"
    End Sub
    Public Sub OLEDB_IndexServer(ByVal Catalog As String)
        Connection_String_VB = "Provider=MSIDXS;Data Source=" & Catalog
        Connection_String_Net = "Provider=MSIDXS;Data Source=" & Catalog
        Object_Prefix = "OleDB"
    End Sub
    Public Sub OLEDB_InternetPublishing(ByVal DataSource As String, ByVal UserName As String, ByVal Password As String)
        Connection_String_VB = "Provider=MSDAIPP.DSO;User Id=" & UserName & ";Password=" & Password
        Connection_String_Net = "Provider=MSDAIPP.DSO;User Id=" & UserName & ";Password=" & Password
        Object_Prefix = "OleDB"
    End Sub
    Public Sub OLEDB_MSJet(ByVal DataSource As String, ByVal UserName As String, ByVal Password As String)
        Connection_String_VB = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & DataSource & ";User Id=" & UserName & ";Password=" & Password
        Connection_String_Net = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & DataSource & ";User Id=" & UserName & ";Password=" & Password
        Object_Prefix = "OleDB"
    End Sub
    Public Sub OLEDB_MSJetWorkGroup(ByVal DataSource As String, ByVal SysDatabase As String, ByVal UserName As String, ByVal Password As String)
        Connection_String_VB = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & DataSource & ";Jet OLEDB:System Database=" & SysDatabase & "," & UserName & "," & Password
        Connection_String_Net = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & DataSource & ";Jet OLEDB:System Database=" & SysDatabase & "," & UserName & "," & Password
        Object_Prefix = "OleDB"
    End Sub
    Public Sub OLEDB_MSJetDBPassword(ByVal DataSource As String, ByVal DatabasePassword As String, ByVal UserName As String, ByVal Password As String)
        Connection_String_VB = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & DataSource & ";Jet OLEDB:Database Password=" & DatabasePassword & ";User Id=" & UserName & ";Password=" & Password
        Connection_String_Net = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & DataSource & ";Jet OLEDB:Database Password=" & DatabasePassword & ";User Id=" & UserName & ";Password=" & Password
        Object_Prefix = "OleDB"
    End Sub
    Public Sub OLEDB_MSJetNwShare(ByVal DataSource As String)
        Connection_String_VB = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & DataSource
        Connection_String_Net = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & DataSource
        Object_Prefix = "OleDB"
    End Sub
    Public Sub OLEDB_MSJetUnknownPath(ByVal relDataPath As String, ByVal UserName As String, ByVal Password As String, Optional ByVal DataSource As String = "")
        Connection_String_VB = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & DataSource & ";User Id=" & UserName & ";Password=" & Password
        Connection_String_Net = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="" & " & "Server.MapPath(""" & relDataPath & """) & "";User Id=" & UserName & ";Password=" & Password & ""
        Object_Prefix = "OleDB"
    End Sub
    Public Sub OLEDB_MSJetExcell(ByVal DataSource As String, ByVal HeaderRow As Boolean)
        If HeaderRow = True Then
            Connection_String_VB = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & DataSource & ";Extended Properties=""Excel 8.0;HDR=Yes"""
            Connection_String_Net = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & DataSource & ";Extended Properties=""Excel 8.0;HDR=Yes"""
            Object_Prefix = "OleDB"
        Else
            Connection_String_VB = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & DataSource & ";Extended Properties=""Excel 8.0;HDR=Yes"""
            Connection_String_Net = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & DataSource & ";Extended Properties=""Excel 8.0;HDR=No"""
            Object_Prefix = "OleDB"
        End If
    End Sub
    Public Sub OLEDB_TextFile(ByVal DataSource As String)
        Connection_String_VB = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & DataSource & ";text;HDR=Yes;FMT=Delimited"
        Connection_String_Net = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & DataSource & ";text;HDR=Yes;FMT=Delimited"
        Object_Prefix = "OleDB"
    End Sub
    Public Sub OLEDB_MyProject(ByVal ProjectPath As String)
        Connection_String_VB = "Provider=Microsoft.Project.OLEDB.9.0; Project Name=" & ProjectPath
        Connection_String_Net = "Provider=Microsoft.Project.OLEDB.9.0; Project Name=" & ProjectPath
        Object_Prefix = "OleDB"
    End Sub
    Public Sub OLEDB_MySql(ByVal DataSource As String, ByVal UserName As String, ByVal Password As String)
        Connection_String_VB = "Provider=MySQLProv; User Id=" & UserName & ";Password=" & Password
        Connection_String_Net = "Provider=MySQLProv; User Id=" & UserName & ";Password=" & Password
        Object_Prefix = "OleDB"
    End Sub
    Public Sub OLEDB_ODBCJetAccess(ByVal DataSource As String, ByVal UserName As String, ByVal Password As String)
        Connection_String_VB = "Provider=MSDASQL;Driver={Microsoft Access Driver (*.mdb)};Dbq=" & DataSource & ";UId=" & UserName & ";Pwd=" & Password
        Connection_String_Net = "Provider=MSDASQL;Driver={Microsoft Access Driver (*.mdb)};Dbq=" & DataSource & ";UId=" & UserName & ";Pwd=" & Password
        Object_Prefix = "ODBC"
    End Sub
    Public Sub OLEDB_ODBCSQLServer(ByVal ServerName As String, ByVal DatabaseName As String, ByVal UserName As String, ByVal Password As String)
        Connection_String_VB = "Provider=MSDASQL;Driver={SQL Server};Server=" & ServerName & ";Database=" & DatabaseName & ";UId=" & UserName & ";Pwd=" & Password
        Connection_String_Net = "Provider=MSDASQL;Driver={SQL Server};Server=" & ServerName & ";Database=" & DatabaseName & ";UId=" & UserName & ";Pwd=" & Password
        Object_Prefix = "ODBC"
    End Sub
    Public Sub OLEDB_MSOLAP(ByVal ServerName As String, ByVal Catalog As String)
        Connection_String_VB = "Provider=MSOLAP;Data Source=" & ServerName & ";Initial Catalog=" & Catalog
        Connection_String_Net = "Provider=MSOLAP;Data Source=" & ServerName & ";Initial Catalog=" & Catalog
        Object_Prefix = "ODBC"
    End Sub
    Public Sub OLEDB_MSOLAP_URL(ByVal ServerURL As String, ByVal Catalog As String)
        Connection_String_VB = "Provider=MSOLAP;Data Source=" & ServerURL & ";Initial Catalog=" & Catalog
        Connection_String_Net = "Provider=MSOLAP;Data Source=" & ServerURL & ";Initial Catalog=" & Catalog
    End Sub
    Public Sub OLEDB_ORACLE_MS(ByVal DataSource As String, ByVal UserName As String, ByVal Password As String)
        Connection_String_VB = "Provider=msdaora;Data Source=" & DataSource & ";User Id=" & UserName & ";Password=" & Password
        Connection_String_Net = "Data Source=" & DataSource & ";User Id=" & UserName & ";Password=" & Password
        Object_Prefix = "Oracle"
    End Sub
    Public Sub OLEDB_ORACLE_ORC_Stand(ByVal DataSource As String, ByVal UserName As String, ByVal Password As String)
        Connection_String_VB = "Provider=OraOLEDB.Oracle;Data Source=" & DataSource & ";User Id=" & UserName & ";Password=" & Password
        Connection_String_Net = "Data Source=" & DataSource & ";User Id=" & UserName & ";Password=" & Password
        Object_Prefix = "Oracle"
    End Sub
    Public Sub OLEDB_ORACLE_ORC_Trust1(ByVal DataSource As String)
        Connection_String_VB = "Provider=OraOLEDB.Oracle;Data Source=" & DataSource & ";User Id=/;Password="
        Connection_String_Net = "Data Source=" & DataSource & ";User Id=/;Password="
        Object_Prefix = "Oracle"
    End Sub
    Public Sub OLEDB_ORACLE_ORC_Trust2(ByVal DataSource As String)
        Connection_String_VB = "Provider=OraOLEDB.Oracle;Data Source=" & DataSource & ";OSAuthent=1"
        Connection_String_Net = "Data Source=" & DataSource & ";OSAuthent=1"
        Object_Prefix = "Oracle"
    End Sub
    Public Sub OLEDB_Pervasive(ByVal DataSource As String)
        Connection_String_VB = "Provider=PervasiveOLEDB; Data Source=" & DataSource
        Connection_String_Net = "Provider=PervasiveOLEDB; Data Source=" & DataSource
        Object_Prefix = "OleDB"
    End Sub
    Public Sub OLEDB_SIMPLE(ByVal sNothing As String)
        Connection_String_VB = "Provider=MSDAOSP; Data Source=MSXML2.DSOControl.2.6"
        Connection_String_Net = "Provider=MSDAOSP; Data Source=MSXML2.DSOControl.2.6"
        Object_Prefix = "OleDB"
    End Sub
    Public Sub OLEDB_SQLBase(ByVal DataSource As String, ByVal Location As String, ByVal UserName As String, ByVal Password As String)
        Connection_String_VB = "Provider=SQLBaseOLEDB;Data Source=" & DataSource & ";Location=" & Location & "; User Id=" & UserName & ";Password=" & Password
        Connection_String_Net = "Provider=SQLBaseOLEDB;Data Source=" & DataSource & ";Location=" & Location & "; User Id=" & UserName & ";Password=" & Password
        Object_Prefix = "OleDB"
    End Sub
    Public Sub OLEDB_SQLServer_Stand(ByVal DataSource As String, ByVal Catalog As String, ByVal UserName As String, ByVal Password As String)
        Connection_String_VB = "Data Source=(local);" & "Initial Catalog=" & Catalog & ";Integrated Security=SSPI"
        Connection_String_Net = "Data Source=(local);" & "Initial Catalog=" & Catalog & ";Integrated Security=SSPI"
        Object_Prefix = "Sql"
    End Sub
    Public Sub OLEDB_SQLServer_Trust(ByVal DataSource As String, ByVal Catalog As String)
        Connection_String_VB = "Provider=sqloledb;Data Source=" & DataSource & ";Initial Catalog=" & Catalog & ";Integrated Security=SSPI"
        Connection_String_Net = "Data Source=" & DataSource & ";Initial Catalog=" & Catalog & ";Integrated Security=SSPI"
        Object_Prefix = "Sql"
    End Sub
    Public Sub OLEDB_SQLNamedInstance(ByVal NamedInstance As String, ByVal Catalog As String, ByVal UserName As String, ByVal Password As String)
        Connection_String_VB = "Provider=sqloledb;Data Source=" & NamedInstance & ";Initial Catalog=" & Catalog & "; User Id=" & UserName & ";Password=" & Password
        Connection_String_Net = "Data Source=" & NamedInstance & ";Initial Catalog=" & Catalog & "; User Id=" & UserName & ";Password=" & Password
        Object_Prefix = "Sql"
    End Sub
    Public Sub OLEDB_SQLServer_SameComp(ByVal DataSource As String, ByVal Catalog As String, ByVal UserName As String, ByVal Password As String)
        Connection_String_VB = "Provider=sqloledb;Data Source=(local);Initial Catalog=" & Catalog & "; User Id=" & UserName & ";Password=" & Password
        Connection_String_Net = "Data Source=(local);Initial Catalog=" & Catalog & "; User Id=" & UserName & ";Password=" & Password
        Object_Prefix = "Sql"
    End Sub
    Public Sub OLEDB_SQLServer_Remote(ByVal DataSourceIPPort As String, ByVal Catalog As String, ByVal UserName As String, ByVal Password As String)
        Connection_String_VB = "Network Library=DBMSSOCN;" & "Data Source=" & DataSourceIPPort & ";" & "Initial Catalog=" & Catalog & ";User ID=" & UserName & ";Password=" & Password
        Connection_String_Net = "Network Library=DBMSSOCN;" & "Data Source=" & DataSourceIPPort & ";" & "Initial Catalog=" & Catalog & ";User ID=" & UserName & ";Password=" & Password
        Object_Prefix = "Sql"
    End Sub
    Public Sub OLEDB_SQLServer_SQLXML(ByVal DataSource As String, ByVal Catalog As String, ByVal UserName As String, ByVal Password As String)
        Connection_String_VB = "Provider=SQLXMLOLEDB.3.0;Data Provider=sqloledb;Data Source=" & DataSource & ";Initial Catalog=" & Catalog & "; User Id=" & UserName & ";Password=" & Password
        Connection_String_Net = "Data Provider=sqloledb;Data Source=" & DataSource & ";Initial Catalog=" & Catalog & "; User Id=" & UserName & ";Password=" & Password
        Object_Prefix = "Sql"
    End Sub
    Public Sub OLEDB_SQLServer_ASEPort(ByVal sbServer As String, ByVal Port As String, ByVal Catalog As String, ByVal UserName As String, ByVal Password As String)
        Connection_String_VB = "Provider=Sybase.ASEOLEDBProvider;Srvr=" & sbServer & "," & Port & ";Catalog=" & Catalog & ";User Id=" & UserName & ";Password=" & Password
        Connection_String_Net = "Srvr=" & sbServer & "," & Port & ";Catalog=" & Catalog & ";User Id=" & UserName & ";Password=" & Password
        Object_Prefix = "Sql"
    End Sub
    Public Sub OLEDB_Remote_SqlSrvrDSN(ByVal rmServer As String, ByVal rmDSN As String, ByVal UserName As String, ByVal Password As String)
        Connection_String_VB = "Provider=MS Remote;Remote Server=" & rmServer & ";Remote Provider=MSDASQL;DSN=" & rmDSN & ";UId=" & UserName & ";Pwd=" & Password
        Connection_String_Net = "Remote Server=" & rmServer & ";Remote Provider=MSDASQL;DSN=" & rmDSN & ";UId=" & UserName & ";Pwd=" & Password
        Object_Prefix = "Sql"
    End Sub
    Public Sub OLEDB_Remote_SqlSrvrDSNLess(ByVal rmServerPath As String, ByVal rmServerName As String, ByVal rmDatabaseName As String, ByVal UserName As String, ByVal Password As String)
        Connection_String_VB = "Provider=MS Remote;Remote Server=" & rmServerPath & ";Remote Provider=SQLOLEDB;Data Source=" & rmServerName & ";Initial Catalog=" & rmDatabaseName & ";User Id=" & UserName & ";Password=" & Password
        Connection_String_Net = "Remote Server=" & rmServerPath & ";Remote Provider=SQLOLEDB;Data Source=" & rmServerName & ";Initial Catalog=" & rmDatabaseName & ";User Id=" & UserName & ";Password=" & Password
        Object_Prefix = "Sql"
    End Sub
    Public Sub OLEDB_Remote_SqlSrvrNTMap(ByVal rmServer As String, ByVal rmPubConn As String)
        Connection_String_VB = "Provider=MS Remote;Remote Server=" & rmServer & ";Handler=MSDFMAP.Handler;Data Source=" & rmPubConn
        Connection_String_Net = "Remote Server=" & rmServer & ";Handler=MSDFMAP.Handler;Data Source=" & rmPubConn
        Object_Prefix = "Sql"
    End Sub
    Public Sub OLEDB_SyBase_ASA(ByVal sbServer As String)
        Connection_String_VB = "Provider=ASAProv; Data Source=" & sbServer
        Connection_String_Net = "Provider=ASAProv; Data Source=" & sbServer
        Object_Prefix = "OleDB"
    End Sub
    Public Sub OLEDB_SyBase_ASE(ByVal sbServer As String)
        Connection_String_VB = "Provider=Sybase ASE OLE DB Provider;Data Source=" & sbServer
        Connection_String_Net = "Provider=Sybase ASE OLE DB Provider;Data Source=" & sbServer
        Object_Prefix = "OleDB"
    End Sub
    Public Sub OLEDB_UniText(ByVal uniServer As String, ByVal uniDatabase As String, ByVal UserName As String, ByVal Password As String)
        Connection_String_VB = "Provider=Ardent.UniOLEDB; Data Source=" & uniServer & ";Location=" & uniDatabase & ";User Id=" & UserName & ";Password=" & Password
        Connection_String_Net = "Provider=Ardent.UniOLEDB; Data Source=" & uniServer & ";Location=" & uniDatabase & ";User Id=" & UserName & ";Password=" & Password
        Object_Prefix = "OleDB"
    End Sub
    Public Sub OLEDB_VFoxPro8(ByVal DataSource As String)
        Connection_String_VB = "Provider=vfpoledb;Data Source=" & DataSource & ";Mode=ReadWrite|Share Deny None;Collating Sequence=MACHINE;Password=''"
        Connection_String_Net = "Provider=vfpoledb;Data Source=" & DataSource & ";Mode=ReadWrite|Share Deny None;Collating Sequence=MACHINE;Password=''"
        Object_Prefix = "OleDB"
    End Sub
    Public Sub OLEDB_VFoxPro7(ByVal DataSource As String)
        Connection_String_VB = "Provider=vfpoledb;Data Source=" & DataSource & ";Mode=ReadWrite|Share Deny None;Collating Sequence=MACHINE;Password=''"
        Connection_String_Net = "Provider=vfpoledb;Data Source=" & DataSource & ";Mode=ReadWrite|Share Deny None;Collating Sequence=MACHINE;Password=''"
        Object_Prefix = "OleDB"
    End Sub
    Public Sub OLEDB_RemoteAccessDSN(ByVal rmServer As String, ByVal rmDSN As String, ByVal UserName As String, ByVal Password As String)
        Connection_String_VB = "Provider=MS Remote;Remote Server=" & rmServer & ";Remote Provider=MSDASQL;DSN=" & rmDSN & ";UId=" & UserName & ";Pwd=" & Password
        Connection_String_Net = "Provider=MS Remote;Remote Server=" & rmServer & ";Remote Provider=MSDASQL;DSN=" & rmDSN & ";UId=" & UserName & ";Pwd=" & Password
        Object_Prefix = "OleDB"
    End Sub
    Public Sub OLEDB_RemoteAccessDSNLess(ByVal rmServer As String, ByVal rmDataSource As String, ByVal UserName As String, ByVal Password As String)
        Connection_String_VB = "Provider=MS Remote;Remote Server=" & rmServer & ";Remote Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & rmDataSource & "," & UserName & "," & Password
        Connection_String_Net = "Provider=MS Remote;Remote Server=" & rmServer & ";Remote Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & rmDataSource & "," & UserName & "," & Password
        Object_Prefix = "OleDB"
    End Sub
    Public Sub OLEDB_RemoteAccessNTMap(ByVal rmServer As String, ByVal rmPubConn As String)
        Connection_String_VB = "Provider=MS Remote;Remote Server=" & rmServer & ";Handler=MSDFMAP.Handler;Data Source=" & rmPubConn
        Connection_String_Net = "Provider=MS Remote;Remote Server=" & rmServer & ";Handler=MSDFMAP.Handler;Data Source=" & rmPubConn
        Object_Prefix = "OleDB"
    End Sub
    Public Function GetImport_Net() As String
        Select Case UCase(Object_Prefix)
            Case "OLEDB"
                GetImport_Net = "Import NameSpace=""System.Data.OleDB"""
            Case "ORACLE"
                GetImport_Net = "Import NameSpace=""System.Data.Oracle"""
            Case "SQL"
                GetImport_Net = "Import NameSpace=""System.Data.SqlClient"""
            Case "ODBC"
                GetImport_Net = "Import NameSpace=""System.Data.ODBC"""
            Case Else
                GetImport_Net = ""
        End Select
    End Function
#Region "MSSQL 16"

    Public Sub MSSQL16_Standard(ByVal serverAddress As String, ByVal databaseName As String, ByVal UserName As String, ByVal Password As String)
        Connection_String_VB = "Server=" & serverAddress & ";Database=" & databaseName & ";User Id=" & UserName & ";Password=" & Password & ";"
        Connection_String_Net = "Server=" & serverAddress & ";Database=" & databaseName & ";User Id=" & UserName & ";Password=" & Password & ";"
        Object_Prefix = "SqlConnection"
    End Sub
    Public Sub MSSQL16_Trusted(ByVal serverAddress As String, ByVal databaseName As String, ByVal UserName As String, ByVal Password As String)
        Connection_String_VB = "Server=" & serverAddress & ";Database=" & databaseName & ";Trusted_Connection=True;"
        Connection_String_Net = "Server=" & serverAddress & ";Database=" & databaseName & ";Trusted_Connection=True;"
        Object_Prefix = "SqlConnection"
    End Sub
    Public Sub MSSQL16_Instance(ByVal serverAddress As String, ByVal instanceName As String, ByVal UserName As String, ByVal Password As String)
        Connection_String_VB = "Server=" & serverAddress & "\" & instanceName & ";User Id=" & UserName & ";Password=" & Password & ";"
        Connection_String_Net = "Server=" & serverAddress & "\" & instanceName & ";User Id=" & UserName & ";Password=" & Password & ";"
        Object_Prefix = "SqlConnection"
    End Sub
    Public Sub MSSQL16_CE(ByVal serverAddress As String, ByVal intialCatalog As String, ByVal userDomain As String, ByVal UserName As String, ByVal Password As String)
        Connection_String_VB = "Data Source=" & serverAddress & ";Initial Catalog=" & intialCatalog & ";Integrated Security=SSPI;User Id=" & userDomain & "\" & UserName & ";Password=" & Password & ";"
        Connection_String_Net = "Data Source=" & serverAddress & ";Initial Catalog=" & intialCatalog & ";Integrated Security=SSPI;User Id=" & userDomain & "\" & UserName & ";Password=" & Password & ";"
        Object_Prefix = "SqlConnection"
    End Sub
    Public Sub MSSQL16_IPAddress(ByVal serverAddress As String, ByVal intialCatalog As String, ByVal UserName As String, ByVal Password As String)
        Connection_String_VB = "Data Source=" & serverAddress & ";Network Library=DBMSSOCN;Initial Catalog=" & intialCatalog & ";Integrated Security=SSPI;User Id=" & UserName & ";Password=" & Password & ";"
        Connection_String_Net = "Data Source=" & serverAddress & ";Network Library=DBMSSOCN;Initial Catalog=" & intialCatalog & ";Integrated Security=SSPI;User Id=" & UserName & ";Password=" & Password & ";"
        Object_Prefix = "SqlConnection"
    End Sub
    Public Sub MSSQL16_LocalSQLExpress(ByVal dbFilePath As String, ByVal UserName As String, ByVal Password As String)
        Connection_String_VB = "Data Source=.\SQLExpress;Integrated Security=true;AttachDbFilename=" & dbFilePath & ";User Instance=true;"
        Connection_String_Net = "Data Source=.\SQLExpress;Integrated Security=true;AttachDbFilename=" & dbFilePath & ";User Instance=true;"
        Object_Prefix = "SqlConnection"
    End Sub
#End Region
End Module
