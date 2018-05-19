Imports System
Imports System.Collections.Generic
Imports DevExpress.Xpo

Namespace NorthwindXpo
    Public Class Employee
        Inherits PersistentBase
        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        <Key(True)> _
        Public OID As Integer

        Private fLastName As String
        Public Property LastName() As String
            Get
                Return fLastName
            End Get
            Set(ByVal value As String)
                SetPropertyValue("LastName", fLastName, value)
            End Set
        End Property
        Private fFirstName As String
        Public Property FirstName() As String
            Get
                Return fFirstName
            End Get
            Set(ByVal value As String)
                SetPropertyValue("FirstName", fFirstName, value)
            End Set
        End Property
        Private fTitle As String
        Public Property Title() As String
            Get
                Return fTitle
            End Get
            Set(ByVal value As String)
                SetPropertyValue("Title", fTitle, value)
            End Set
        End Property
        Private fBirthDate As DateTime
        Public Property BirthDate() As DateTime
            Get
                Return fBirthDate
            End Get
            Set(ByVal value As DateTime)
                SetPropertyValue("BirthDate", fBirthDate, value)
            End Set
        End Property
        Private fHireDate As DateTime
        Public Property HireDate() As DateTime
            Get
                Return fHireDate
            End Get
            Set(ByVal value As DateTime)
                SetPropertyValue("HireDate", fHireDate, value)
            End Set
        End Property
        Private fAddress As String
        Public Property Address() As String
            Get
                Return fAddress
            End Get
            Set(ByVal value As String)
                SetPropertyValue("Address", fAddress, value)
            End Set
        End Property
        Private fCity As String
        Public Property City() As String
            Get
                Return fCity
            End Get
            Set(ByVal value As String)
                SetPropertyValue("City", fCity, value)
            End Set
        End Property
        Private fPostalCode As String
        Public Property PostalCode() As String
            Get
                Return fPostalCode
            End Get
            Set(ByVal value As String)
                SetPropertyValue("PostalCode", fPostalCode, value)
            End Set
        End Property
        Private fCountry As String
        Public Property Country() As String
            Get
                Return fCountry
            End Get
            Set(ByVal value As String)
                SetPropertyValue("Country", fCountry, value)
            End Set
        End Property
        Private fHomePhone As String
        Public Property HomePhone() As String
            Get
                Return fHomePhone
            End Get
            Set(ByVal value As String)
                SetPropertyValue("HomePhone", fHomePhone, value)
            End Set
        End Property
        Private fExtension As String
        Public Property Extension() As String
            Get
                Return fExtension
            End Get
            Set(ByVal value As String)
                SetPropertyValue("Extension", fExtension, value)
            End Set
        End Property
        <Delayed()> _
        Public Property Photo() As Byte()
            Get
                Return GetDelayedPropertyValue(Of Byte())("Photo")
            End Get
            Set(ByVal value As Byte())
                SetDelayedPropertyValue("Photo", value)
            End Set
        End Property
        Private fNotes As String
        <Size(SizeAttribute.Unlimited)> _
        Public Property Notes() As String
            Get
                Return fNotes
            End Get
            Set(ByVal value As String)
                SetPropertyValue("Notes", fNotes, value)
            End Set
        End Property
        Private fReportsTo As Employee
        <Association("Employee-Subordinates")> _
        Public Property ReportsTo() As Employee
            Get
                Return fReportsTo
            End Get
            Set(ByVal value As Employee)
                SetPropertyValue(Of Employee)("ReportsTo", fReportsTo, value)
            End Set
        End Property

        <Association("Employee-Orders")> _
        Public ReadOnly Property Orders() As IList(Of Order)
            Get
                Return GetList(Of Order)("Orders")
            End Get
        End Property

        <Association("Employee-Subordinates")> _
        Public ReadOnly Property Subordinates() As IList(Of Employee)
            Get
                Return GetList(Of Employee)("Subordinates")
            End Get
        End Property
    End Class

    Public Class Order
        Inherits PersistentBase
        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        <Key(True)> _
        Public OID As Integer

        Private fCustomer As Customer
        <Association("Customer-Orders")> _
        Public Property Customer() As Customer
            Get
                Return fCustomer
            End Get
            Set(ByVal value As Customer)
                SetPropertyValue("Customer", fCustomer, value)
            End Set
        End Property
        Private fEmployee As Employee
        <Association("Employee-Orders")> _
        Public Property Employee() As Employee
            Get
                Return fEmployee
            End Get
            Set(ByVal value As Employee)
                SetPropertyValue(Of Employee)("Employee", fEmployee, value)
            End Set
        End Property
        Private fOrderDate As DateTime
        Public Property OrderDate() As DateTime
            Get
                Return fOrderDate
            End Get
            Set(ByVal value As DateTime)
                SetPropertyValue("OrderDate", fOrderDate, value)
            End Set
        End Property
        Private fShippedDate As DateTime
        Public Property ShippedDate() As DateTime
            Get
                Return fShippedDate
            End Get
            Set(ByVal value As DateTime)
                SetPropertyValue("ShippedDate", fShippedDate, value)
            End Set
        End Property
        Private fFreight As Decimal
        Public Property Freight() As Decimal
            Get
                Return fFreight
            End Get
            Set(ByVal value As Decimal)
                SetPropertyValue("Freight", fFreight, value)
            End Set
        End Property
        Private fShipName As String
        Public Property ShipName() As String
            Get
                Return fShipName
            End Get
            Set(ByVal value As String)
                SetPropertyValue("ShipName", fShipName, value)
            End Set
        End Property
        Private fShipAddress As String
        Public Property ShipAddress() As String
            Get
                Return fShipAddress
            End Get
            Set(ByVal value As String)
                SetPropertyValue("ShipAddress", fShipAddress, value)
            End Set
        End Property
        Private fShipCity As String
        Public Property ShipCity() As String
            Get
                Return fShipCity
            End Get
            Set(ByVal value As String)
                SetPropertyValue("ShipCity", fShipCity, value)
            End Set
        End Property
        Private fShipRegion As String
        Public Property ShipRegion() As String
            Get
                Return fShipRegion
            End Get
            Set(ByVal value As String)
                SetPropertyValue("ShipRegion", fShipRegion, value)
            End Set
        End Property
        Private fShipPostalCode As String
        Public Property ShipPostalCode() As String
            Get
                Return fShipPostalCode
            End Get
            Set(ByVal value As String)
                SetPropertyValue("ShipPostalCode", fShipPostalCode, value)
            End Set
        End Property
        Private fShipCountry As String
        Public Property ShipCountry() As String
            Get
                Return fShipCountry
            End Get
            Set(ByVal value As String)
                SetPropertyValue("ShipCountry", fShipCountry, value)
            End Set
        End Property
    End Class

    Public Class Customer
        Inherits PersistentBase
        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        <Key(True)> _
        Public OID As Integer

        Private fCompanyName As String
        Public Property CompanyName() As String
            Get
                Return fCompanyName
            End Get
            Set(ByVal value As String)
                SetPropertyValue("CompanyName", fCompanyName, value)
            End Set
        End Property
        Private fContactName As String
        Public Property ContactName() As String
            Get
                Return fContactName
            End Get
            Set(ByVal value As String)
                SetPropertyValue("ContactName", fContactName, value)
            End Set
        End Property
        Private fContactTitle As String
        Public Property ContactTitle() As String
            Get
                Return fContactTitle
            End Get
            Set(ByVal value As String)
                SetPropertyValue("ContactTitle", fContactTitle, value)
            End Set
        End Property
        Private fAddress As String
        Public Property Address() As String
            Get
                Return fAddress
            End Get
            Set(ByVal value As String)
                SetPropertyValue("Address", fAddress, value)
            End Set
        End Property
        Private fCity As String
        Public Property City() As String
            Get
                Return fCity
            End Get
            Set(ByVal value As String)
                SetPropertyValue("City", fCity, value)
            End Set
        End Property
        Private fRegion As String
        Public Property Region() As String
            Get
                Return fRegion
            End Get
            Set(ByVal value As String)
                SetPropertyValue("Region", fRegion, value)
            End Set
        End Property
        Private fPostalCode As String
        Public Property PostalCode() As String
            Get
                Return fPostalCode
            End Get
            Set(ByVal value As String)
                SetPropertyValue("PostalCode", fPostalCode, value)
            End Set
        End Property
        Private fCountry As String
        Public Property Country() As String
            Get
                Return fCountry
            End Get
            Set(ByVal value As String)
                SetPropertyValue("Country", fCountry, value)
            End Set
        End Property
        Private fPhone As String
        Public Property Phone() As String
            Get
                Return fPhone
            End Get
            Set(ByVal value As String)
                SetPropertyValue("Phone", fPhone, value)
            End Set
        End Property

        <Association("Customer-Orders")> _
        Public ReadOnly Property Orders() As IList(Of Order)
            Get
                Return GetList(Of Order)("Orders")
            End Get
        End Property
    End Class

    Public Class Category
        Inherits PersistentBase
        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        Private fCategoryName As String
        Public Property CategoryName() As String
            Get
                Return fCategoryName
            End Get
            Set(ByVal value As String)
                SetPropertyValue("CategoryName", fCategoryName, value)
            End Set
        End Property
        Private fDescription As String
        <Size(SizeAttribute.Unlimited)> _
        Public Property Description() As String
            Get
                Return fDescription
            End Get
            Set(ByVal value As String)
                SetPropertyValue("Description", fDescription, value)
            End Set
        End Property

        <Delayed()> _
        Public Property Picture() As Byte()
            Get
                Return GetDelayedPropertyValue(Of Byte())("Picture")
            End Get
            Set(ByVal value As Byte())
                SetDelayedPropertyValue("Picture", value)
            End Set
        End Property

        <Association("Category-Products")> _
        Public ReadOnly Property Products() As IList(Of Product)
            Get
                Return GetList(Of Product)("Products")
            End Get
        End Property
    End Class

    Public Class Product
        Inherits PersistentBase
        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        Private fProductName As String
        Public Property ProductName() As String
            Get
                Return fProductName
            End Get
            Set(ByVal value As String)
                SetPropertyValue("ProductName", fProductName, value)
            End Set
        End Property
        Private fCategory As Category
        <Association("Category-Products")> _
        Public Property Category() As Category
            Get
                Return fCategory
            End Get
            Set(ByVal value As Category)
                SetPropertyValue(Of Category)("Category", fCategory, value)
            End Set
        End Property
        Private fQuantityPerUnit As String
        Public Property QuantityPerUnit() As String
            Get
                Return fQuantityPerUnit
            End Get
            Set(ByVal value As String)
                SetPropertyValue("QuantityPerUnit", fQuantityPerUnit, value)
            End Set
        End Property
        Private fUnitPrice As Decimal
        Public Property UnitPrice() As Decimal
            Get
                Return fUnitPrice
            End Get
            Set(ByVal value As Decimal)
                SetPropertyValue("UnitPrice", fUnitPrice, value)
            End Set
        End Property
        Private fDiscontinued As Boolean
        Public Property Discontinued() As Boolean
            Get
                Return fDiscontinued
            End Get
            Set(ByVal value As Boolean)
                SetPropertyValue("Discontinued", fDiscontinued, value)
            End Set
        End Property
    End Class
End Namespace

