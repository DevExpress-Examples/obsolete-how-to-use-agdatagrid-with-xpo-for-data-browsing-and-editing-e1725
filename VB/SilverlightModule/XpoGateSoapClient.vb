Option Strict On
Option Explicit On

Imports DevExpress.Xpo.DB

<System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"), _
 System.ServiceModel.ServiceContractAttribute(ConfigurationName:="XpoGateSoap")> _
Public Interface IDataStoreContract
    <System.ServiceModel.OperationContractAttribute(AsyncPattern:=True, Action:="http://tempuri.org/SelectData", ReplyAction:="*"), _
     System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults:=True), _
     System.ServiceModel.ServiceKnownTypeAttribute(GetType(DBTableMultiColumnGadget))> _
    Function BeginSelectData(ByVal selects() As SelectStatement, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult

    Function EndSelectData(<System.Runtime.InteropServices.OutAttribute()> ByRef e As ServiceException, ByVal result As System.IAsyncResult) As SelectedData

    <System.ServiceModel.OperationContractAttribute(AsyncPattern:=True, Action:="http://tempuri.org/ModifyData", ReplyAction:="*"), _
     System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults:=True), _
     System.ServiceModel.ServiceKnownTypeAttribute(GetType(DBTableMultiColumnGadget))> _
    Function BeginModifyData(ByVal statements() As ModificationStatement, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult

    Function EndModifyData(ByVal result As System.IAsyncResult) As ModificationResult
End Interface

<System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")> _
Partial Public Class XpoGateSoapClient
    Inherits System.ServiceModel.ClientBase(Of IDataStoreContract)
    Implements IDataStore

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal endpointConfigurationName As String)
        MyBase.New(endpointConfigurationName)
    End Sub

    Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As String)
        MyBase.New(endpointConfigurationName, remoteAddress)
    End Sub

    Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
        MyBase.New(endpointConfigurationName, remoteAddress)
    End Sub

    Public Sub New(ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
        MyBase.New(binding, remoteAddress)
    End Sub

    Protected Overrides Function CreateChannel() As IDataStoreContract
        Return New XpoGateSoapClientChannel(Me)
    End Function

    Private Class XpoGateSoapClientChannel
        Inherits ChannelBase(Of IDataStoreContract)
        Implements IDataStoreContract

        Public Sub New(ByVal client As System.ServiceModel.ClientBase(Of IDataStoreContract))
            MyBase.New(client)
        End Sub

        Public Function BeginSelectData(ByVal selects() As SelectStatement, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult Implements IDataStoreContract.BeginSelectData
            Dim _args((1) - 1) As Object
            _args(0) = selects
            Dim _result As System.IAsyncResult = MyBase.BeginInvoke("SelectData", _args, callback, asyncState)
            Return _result
        End Function

        Public Function EndSelectData(<System.Runtime.InteropServices.OutAttribute()> ByRef e As ServiceException, ByVal result As System.IAsyncResult) As SelectedData Implements IDataStoreContract.EndSelectData
            Dim _args((1) - 1) As Object
            Dim _result As SelectedData = CType(MyBase.EndInvoke("SelectData", _args, result), SelectedData)
            e = CType(_args(0), ServiceException)
            Return _result
        End Function

        Public Function BeginModifyData(ByVal statements() As ModificationStatement, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult Implements IDataStoreContract.BeginModifyData
            Dim _args((1) - 1) As Object
            _args(0) = statements
            Dim _result As System.IAsyncResult = MyBase.BeginInvoke("ModifyData", _args, callback, asyncState)
            Return _result
        End Function

        Public Function EndModifyData(ByVal result As System.IAsyncResult) As ModificationResult Implements IDataStoreContract.EndModifyData
            Dim _args((0) - 1) As Object
            Dim _result As ModificationResult = CType(MyBase.EndInvoke("ModifyData", _args, result), ModificationResult)
            Return _result
        End Function
    End Class

    Public ReadOnly Property AutoCreateOption As DevExpress.Xpo.DB.AutoCreateOption Implements DevExpress.Xpo.DB.IDataStore.AutoCreateOption
        Get
            Return AutoCreateOption.SchemaAlreadyExists
        End Get
    End Property

    Public Function ModifyData(ByVal ParamArray dmlStatements() As DevExpress.Xpo.DB.ModificationStatement) As DevExpress.Xpo.DB.ModificationResult Implements DevExpress.Xpo.DB.IDataStore.ModifyData
        Dim res As IAsyncResult = Channel.BeginModifyData(dmlStatements, Nothing, Nothing)
        Return Channel.EndModifyData(res)
    End Function

    Public Function SelectData(ByVal ParamArray selects() As DevExpress.Xpo.DB.SelectStatement) As DevExpress.Xpo.DB.SelectedData Implements DevExpress.Xpo.DB.IDataStore.SelectData
        Dim res As IAsyncResult = Channel.BeginSelectData(selects, Nothing, Nothing)
        Dim ex As ServiceException
        Dim data As SelectedData = Channel.EndSelectData(ex, res)
        HandleError(ex)
        Return data
    End Function

    Public Function UpdateSchema(ByVal dontCreateIfFirstTableNotExist As Boolean, ByVal ParamArray tables() As DevExpress.Xpo.DB.DBTable) As DevExpress.Xpo.DB.UpdateSchemaResult Implements DevExpress.Xpo.DB.IDataStore.UpdateSchema
        Return UpdateSchemaResult.SchemaExists
    End Function

    Shared Sub HandleError(ByVal res As Object)
        Dim ex As ServiceException = CType(res, ServiceException)
        Select Case ex
            Case ServiceException.Schema
                Throw New Exceptions.SchemaCorrectionNeededException(String.Empty)
        End Select
    End Sub
End Class

<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30128.1"), _
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://tempuri.org/")> _
Public Enum ServiceException
    None
    Schema
End Enum

<System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")> _
Public Interface XpoGateSoapChannel
    Inherits IDataStoreContract, System.ServiceModel.IClientChannel
End Interface
