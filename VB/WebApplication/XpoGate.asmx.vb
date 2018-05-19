Imports System.Web.Services
Imports DevExpress.Xpo
Imports DevExpress.Xpo.DB
Imports DevExpress.Xpo.DB.Exceptions

Public Enum ServiceException
    None
    Schema
End Enum
' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
<WebService(Namespace:="http://tempuri.org/"), WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1), System.Web.Script.Services.ScriptService()> _
Public Class XpoGate
    Inherits System.Web.Services.WebService
    Private Shared store As IDataStore
    Shared Sub New()
        Dim connStr As String = MSSqlConnectionProvider.GetConnectionString("(local)", "NorthwindXpo")
        store = XpoDefault.GetConnectionProvider(connStr, AutoCreateOption.SchemaAlreadyExists)
    End Sub
    <WebMethod()> _
    Public Function SelectData(ByVal selects() As SelectStatement, <System.Runtime.InteropServices.Out()> ByRef e As ServiceException) As SelectedData
        Try
            e = ServiceException.None
            Return store.SelectData(selects)
        Catch e1 As SchemaCorrectionNeededException
            e = ServiceException.Schema
            Return Nothing
        End Try
    End Function
    '<WebMethod()> _
    'Public Function GetAutoCreateOption() As AutoCreateOption
    '    Return AutoCreateOption.SchemaAlreadyExists
    'End Function
    '<WebMethod()> _
    'Public Function UpdateSchema(ByVal dontCreateIfFirstTableNotExist As Boolean, ByVal tables() As DBTable) As UpdateSchemaResult
    '    Return store.UpdateSchema(dontCreateIfFirstTableNotExist, tables)
    'End Function
    <WebMethod()> _
    Public Function ModifyData(ByVal statements() As ModificationStatement) As ModificationResult
        Return store.ModifyData(statements)
    End Function
End Class
