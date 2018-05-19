using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.DB.Exceptions;

namespace SilverlightModule {
    [ServiceContract]
    public interface IDataStoreContract {
        [XmlSerializerFormat,
        OperationContract(AsyncPattern = true, Action = "http://tempuri.org/SelectData", ReplyAction = "*")]
        IAsyncResult BeginSelectData(SelectStatement[] selects, AsyncCallback callback, object asyncState);
        SelectedData EndSelectData(out ServiceException e, IAsyncResult result);

        [XmlSerializerFormat,
        OperationContract(AsyncPattern = true, Action = "http://tempuri.org/ModifyData", ReplyAction = "*")]
        IAsyncResult BeginModifyData(ModificationStatement[] statements, AsyncCallback callback, object asyncState);
        ModificationResult EndModifyData(IAsyncResult result);
    }
    public class XpoGateSoapClient : ClientBase<IDataStoreContract>, IDataStore {
        public XpoGateSoapClient(Binding binding, EndpointAddress remoteAddress)
            : base(binding, remoteAddress) {
        }
        protected override IDataStoreContract CreateChannel() {
            return new DataStoreClientChannel(this);
        }
        new DataStoreClientChannel Channel {
            get {
                return (DataStoreClientChannel)base.Channel;
            }
        }

        AutoCreateOption IDataStore.AutoCreateOption {
            get {
                return AutoCreateOption.SchemaAlreadyExists;
            }
        }
        ModificationResult IDataStore.ModifyData(ModificationStatement[] dmlStatements) {
            IAsyncResult res = Channel.BeginModifyData(dmlStatements, null, null);
            return Channel.EndModifyData(res);
        }
        SelectedData IDataStore.SelectData(SelectStatement[] selects) {
            IAsyncResult res = Channel.BeginSelectData(selects, null, null);
            ServiceException e;
            SelectedData data = Channel.EndSelectData(out e, res);
            HandleError(e);
            return data;
        }
        UpdateSchemaResult IDataStore.UpdateSchema(bool dontCreateIfFirstTableNotExist, DBTable[] tables) {
            return UpdateSchemaResult.SchemaExists;
        }
        static void HandleError(object res) {
            ServiceException error = (ServiceException)res;
            switch(error) {
                case ServiceException.Schema:
                    throw new SchemaCorrectionNeededException(string.Empty);
            }
        }

        class DataStoreClientChannel : ClientBase<IDataStoreContract>.ChannelBase<IDataStoreContract>, IDataStoreContract {
            public DataStoreClientChannel(ClientBase<IDataStoreContract> client)
                : base(client) {
            }
            public IAsyncResult BeginModifyData(ModificationStatement[] statements, AsyncCallback callback, object asyncState) {
                return BeginInvoke("ModifyData", new object[] { statements }, callback, asyncState);
            }
            public IAsyncResult BeginSelectData(SelectStatement[] selects, AsyncCallback callback, object asyncState) {
                return BeginInvoke("SelectData", new object[] { selects }, callback, asyncState);
            }
            static object[] emptyArray = new object[0];
            public ModificationResult EndModifyData(IAsyncResult result) {
                return (ModificationResult)EndInvoke("ModifyData", emptyArray, result);
            }
            public SelectedData EndSelectData(out ServiceException e, IAsyncResult result) {
                object[] res = new object[1];
                SelectedData data = (SelectedData)EndInvoke("SelectData", res, result);
                e = (ServiceException)res[0];
                return data;
            }
        }
    }
    public enum ServiceException { None, Schema }

}