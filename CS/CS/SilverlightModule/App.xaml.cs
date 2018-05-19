using System;
using System.Windows;
using System.ServiceModel;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;

namespace SilverlightModule {
    public partial class App : Application {

        public App() {
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;

            InitXpoDataLayer();

            InitializeComponent();
        }

        void InitXpoDataLayer() {
            string XpoWebServiceUri = new Uri(Application.Current.Host.Source, "/XpoGate.asmx").AbsoluteUri;
            EndpointAddress address = new EndpointAddress(XpoWebServiceUri);
            BasicHttpBinding binding = new BasicHttpBinding();
            binding.MaxReceivedMessageSize = Int32.MaxValue;
            IDataStore dataStore = new XpoGateSoapClient(binding, address);
            XpoDefault.DataLayer = new SimpleDataLayer(dataStore);
            XpoDefault.Session = null;
        }


        private void Application_Startup(object sender, StartupEventArgs e) {
            // Load the main control
            this.RootVisual = new Page();
        }

        private void Application_Exit(object sender, EventArgs e) {

        }
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e) {

        }
    }
}