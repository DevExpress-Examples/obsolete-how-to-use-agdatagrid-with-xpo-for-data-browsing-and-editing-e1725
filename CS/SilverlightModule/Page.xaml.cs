using System;
using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpo;
using NorthwindXpo;

namespace SilverlightModule {
    public partial class Page : UserControl {
        Session session;

        public Page() {
            InitializeComponent();
            App.Current.Host.Content.Resized += new EventHandler(Content_Resized);
            Content_Resized(null, null);

            GridCustomers.AllowEditing = false;
            
            CreateSession();
            LoadCustomers();
        }

        void Content_Resized(object sender, EventArgs e) {
            Width = App.Current.Host.Content.ActualWidth;
            Height = App.Current.Host.Content.ActualHeight;
        }

        void CreateSession() {
            if(session != null)
                session.Dispose();
            session = new UnitOfWork();

            BtnSave.IsEnabled = false;
            BtnReload.IsEnabled = false;
            session.AfterBeginTransaction += delegate {
                Dispatcher.BeginInvoke(delegate { BtnSave.IsEnabled = true; BtnReload.IsEnabled = true; });
            };
        }

        void LoadCustomers() {
            XPQuery<Customer> cust = new XPQuery<Customer>(session);
            var data = from c in cust
                        where c.ContactName.StartsWith("a") ||
                            c.ContactName.StartsWith("b") ||
                            c.ContactName.StartsWith("c") ||
                            c.ContactName.StartsWith("d") ||
                            c.ContactName.StartsWith("e")
                        select c;

            DisableControls();

            // load async
            data.EnumerateAsync(delegate(IEnumerable objs, Exception ex) {
                GridCustomers.DataSource = objs;
                EnableControls();
            });
        }

        private void LoadOrders(Customer customer) {
            XPQuery<Order> orders = new XPQuery<Order>(session);
            var data = orders.Where(o => o.Customer == customer);

            DisableControls();
            data.EnumerateAsync(delegate(IEnumerable objs, Exception ex) {
                GridOrders.DataSource = objs;
                EnableControls();
            });
        }

        void BtnReload_Click(object sender, RoutedEventArgs e) {
            CreateSession();
            LoadCustomers();
        }

        void BtnSave_Click(object sender, RoutedEventArgs e) {
            if(session.InTransaction) {
                BtnSave.IsEnabled = false;
                BtnReload.IsEnabled = false;
                DisableControls();

                session.CommitTransactionAsync(new AsyncCommitCallback(delegate(Exception ex) {
                    EnableControls();
                }));
            }
        }

        private void GridCustomers_FocusedRowChanged(object sender, DevExpress.Data.FocusedRowChangedEventArgs e) {
            if(e.IsNewRowValid && !e.IsNewRowGrouped) {
                Customer current = (Customer)e.NewDataRow;
                LoadOrders(current);
            }
        }
        private void DisableControls() {
            BtnEdit.IsEnabled = false;
            BtnNew.IsEnabled = false;
            BtnDelete.IsEnabled = false;
            GridCustomers.IsEnabled = false;
            GridOrders.IsEnabled = false;
            ImgLoading.Visibility = Visibility.Visible;
        }
        private void EnableControls() {
            BtnEdit.IsEnabled = true;
            BtnNew.IsEnabled = true;
            BtnDelete.IsEnabled = true;
            GridCustomers.IsEnabled = true;
            GridOrders.IsEnabled = true;
            ImgLoading.Visibility = Visibility.Collapsed;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e) {
            GridOrders.Focus();
            GridOrders.ShowEditor();
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e) {
            // A new row cannot be added via the grid. 
            // It must be added directly to the database (create a new persisten object and save).
            // The implementation will be shown in a future version of this demo project.
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e) {
            GridOrders.DeleteFocusedRow();
        }
    }
}
