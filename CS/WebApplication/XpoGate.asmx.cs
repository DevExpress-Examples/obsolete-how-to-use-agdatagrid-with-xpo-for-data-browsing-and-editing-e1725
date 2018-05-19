using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.DB.Exceptions;

namespace XpoWebService {
	public enum ServiceException { None, Schema }
    [WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
	[System.Web.Script.Services.ScriptService]
	public class XpoGate : System.Web.Services.WebService {
		static IDataStore store;
		static XpoGate() {
            string connStr = MSSqlConnectionProvider.GetConnectionString("(local)", "NorthwindXpo");
			store = XpoDefault.GetConnectionProvider(connStr, AutoCreateOption.SchemaAlreadyExists);
		}
		[WebMethod]
		public SelectedData SelectData(SelectStatement[] selects, out ServiceException e) {
			try {
				e = ServiceException.None;
                return store.SelectData(selects);
			} catch(SchemaCorrectionNeededException) {
				e = ServiceException.Schema;
				return null;
			}
		}
        //[WebMethod]
        //public AutoCreateOption GetAutoCreateOption() {
        //    return AutoCreateOption.SchemaAlreadyExists;
        //}
        //[WebMethod]
        //public UpdateSchemaResult UpdateSchema(bool dontCreateIfFirstTableNotExist, DBTable[] tables) {
        //    return store.UpdateSchema(dontCreateIfFirstTableNotExist, tables);
        //}
		[WebMethod]
		public ModificationResult ModifyData(ModificationStatement[] statements) {
			return store.ModifyData(statements);
		}
	}
}