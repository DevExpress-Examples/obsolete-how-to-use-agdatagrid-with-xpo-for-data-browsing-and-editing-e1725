Imports System.Linq
Imports System.Collections
Imports System.ServiceModel
Imports DevExpress.Xpo
Imports DevExpress.Xpo.DB
Imports SilverlightModule.NorthwindXpo

Partial Public Class Page
    Inherits UserControl

    Private session As Session

    Public Sub New()
        InitializeComponent()
        AddHandler App.Current.Host.Content.Resized, AddressOf Content_Resized
        Content_Resized(Nothing, Nothing)

        GridCustomers.AllowEditing = False

        CreateSession()
        LoadCustomers()
    End Sub

    Private Sub Content_Resized(ByVal sender As Object, ByVal e As EventArgs)
        Width = App.Current.Host.Content.ActualWidth
        Height = App.Current.Host.Content.ActualHeight
    End Sub

    Private Sub CreateSession()
        If session IsNot Nothing Then
            session.Dispose()
        End If
        session = New UnitOfWork()

        BtnSave.IsEnabled = False
        BtnReload.IsEnabled = False
        AddHandler session.AfterBeginTransaction, Function() AnonymousMethod1()
    End Sub

    Private Function AnonymousMethod1() As Boolean
        Dispatcher.BeginInvoke(Function() AnonymousMethod2())
        Return True
    End Function

    Private Function AnonymousMethod2() As Boolean
        BtnSave.IsEnabled = True
        BtnReload.IsEnabled = True
        Return True
    End Function

    Private Sub LoadCustomers()
        Dim cust As New XPQuery(Of Customer)(session)
        Dim data = _
         From c In cust _
         Where c.ContactName.StartsWith("a") OrElse c.ContactName.StartsWith("b") OrElse c.ContactName.StartsWith("c") OrElse c.ContactName.StartsWith("d") OrElse c.ContactName.StartsWith("e") _
         Select c

        DisableControls()

        ' load async
        Dim callback As XPQueryExtensions.AsyncEnumerateCallback = AddressOf LoadCustomersCompleted
        data.EnumerateAsync(callback)
    End Sub

    Private Function LoadCustomersCompleted(ByVal objs As IEnumerable, ByVal ex As Exception) As Boolean
        GridCustomers.DataSource = objs
        EnableControls()
        Return True
    End Function

    Private Sub LoadOrders(ByVal customer As Customer)
        Dim orders As New XPQuery(Of Order)(session)
        Dim data = orders.Where(Function(o) o.Customer Is customer)

        DisableControls()
        Dim callback As XPQueryExtensions.AsyncEnumerateCallback = AddressOf LoadOrdersCompleted
        data.EnumerateAsync(callback)
    End Sub

    Private Function LoadOrdersCompleted(ByVal objs As IEnumerable, ByVal ex As Exception) As Boolean
        EnableControls()
        GridOrders.DataSource = objs
        Return True
    End Function

    Private Sub BtnReload_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        CreateSession()
        LoadCustomers()
    End Sub

    Private Sub BtnSave_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        If session.InTransaction Then
            BtnSave.IsEnabled = False
            BtnReload.IsEnabled = False
            DisableControls()

            session.CommitTransactionAsync(AddressOf CommitCompleted)
        End If
    End Sub

    Private Function CommitCompleted(ByVal ex As Exception) As Boolean
        EnableControls()
        Return True
    End Function

    Private Sub GridCustomers_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.Data.FocusedRowChangedEventArgs)
        If e.IsNewRowValid AndAlso (Not e.IsNewRowGrouped) Then
            Dim current As Customer = CType(e.NewDataRow, Customer)
            LoadOrders(current)
        End If
    End Sub
    Private Sub DisableControls()
        BtnEdit.IsEnabled = False
        BtnNew.IsEnabled = False
        BtnDelete.IsEnabled = False
        GridCustomers.IsEnabled = False
        GridOrders.IsEnabled = False
        ImgLoading.Visibility = Visibility.Visible
    End Sub
    Private Sub EnableControls()
        BtnEdit.IsEnabled = True
        BtnNew.IsEnabled = True
        BtnDelete.IsEnabled = True
        GridCustomers.IsEnabled = True
        GridOrders.IsEnabled = True
        ImgLoading.Visibility = Visibility.Collapsed
    End Sub

    Private Sub BtnEdit_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        GridOrders.Focus()
        GridOrders.ShowEditor()
    End Sub

    Private Sub BtnNew_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        ' A new row cannot be added via the grid. 
        ' It must be added directly to the database (create a new persisten object and save).
        ' The implementation will be shown in a future version of this demo project.
    End Sub

    Private Sub BtnDelete_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        GridOrders.DeleteFocusedRow()
    End Sub
End Class