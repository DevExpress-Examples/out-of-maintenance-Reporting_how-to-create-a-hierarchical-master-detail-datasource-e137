Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraReports.Configuration

Namespace docITypedList
    Partial Public Class Form1
        Inherits Form

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button1.Click
            Dim report As XtraReport = CreateReport()

            report.ShowPreview()
        End Sub

        Private Function CreateReport() As XtraReport
            Dim report As New XtraReport()

            Dim detail As New DetailBand()
            detail.Height = 30
            report.Bands.Add(detail)

            Dim detailReport1 As New DetailReportBand()
            report.Bands.Add(detailReport1)

            Dim detail1 As New DetailBand()
            detail1.Height = 30
            detailReport1.Bands.Add(detail1)

            Dim detailReport2 As New DetailReportBand()
            detailReport1.Bands.Add(detailReport2)

            Dim detail2 As New DetailBand()
            detail2.Height = 30
            detailReport2.Bands.Add(detail2)

            report.DataSource = CreateData()
            detailReport1.DataMember = "Products"
            detailReport2.DataMember = "Products.OrderDetails"

            detail.Controls.Add(CreateBoundLabel("CompanyName", Color.Gold, 0))
            detail1.Controls.Add(CreateBoundLabel("Products.ProductName", Color.Aqua, 100))
            detail2.Controls.Add(CreateBoundLabel("Products.OrderDetails.Quantity", Color.Pink, 200))

            Return report
        End Function

        Private Function CreateBoundLabel(ByVal dataMember As String, ByVal backColor As Color, ByVal offset As Integer) As XRLabel
            Dim label As New XRLabel()
            ' Specify the label's binding depending on the data binding mode.
            If Settings.Default.UserDesignerOptions.DataBindingMode = DataBindingMode.Bindings Then
                label.DataBindings.Add(New XRBinding("Text", Nothing, dataMember))
            Else
                label.ExpressionBindings.Add(New ExpressionBinding("BeforePrint", "Text", dataMember))
            End If
            label.BackColor = backColor
            label.Location = New Point(offset, 0)

            Return label
        End Function

        Private Function CreateData() As SupplierCollection
            Dim suppliers As New SupplierCollection()

            Dim supplier As New Supplier("Exotic Liquids")
            suppliers.Add(supplier)
            supplier.Add(CreateProduct(supplier.SupplierID, "Chai"))
            supplier.Add(CreateProduct(supplier.SupplierID, "Chang"))
            supplier.Add(CreateProduct(supplier.SupplierID, "Aniseed Syrup"))

            supplier = New Supplier("New Orleans Cajun Delights")
            suppliers.Add(supplier)
            supplier.Add(CreateProduct(supplier.SupplierID, "Chef Anton's Cajun Seasoning"))
            supplier.Add(CreateProduct(supplier.SupplierID, "Chef Anton's Gumbo Mix"))

            supplier = New Supplier("Grandma Kelly's Homestead")
            suppliers.Add(supplier)
            supplier.Add(CreateProduct(supplier.SupplierID, "Grandma's Boysenberry Spread"))
            supplier.Add(CreateProduct(supplier.SupplierID, "Uncle Bob's Organic Dried Pears"))
            supplier.Add(CreateProduct(supplier.SupplierID, "Northwoods Cranberry Sauce"))

            Return suppliers
        End Function

        Private Shared random As New Random(5)

        Private Function CreateProduct(ByVal supplierID As Integer, ByVal productName As String) As Product
            Dim product As New Product(supplierID, productName)

            product.OrderDetails.AddRange(New OrderDetail() { _
                New OrderDetail(product.ProductID, random.Next(0, 100)), _
                New OrderDetail(product.ProductID, random.Next(0, 100)), _
                New OrderDetail(product.ProductID, random.Next(0, 100)) _
            })

            Return product
        End Function
    End Class
End Namespace