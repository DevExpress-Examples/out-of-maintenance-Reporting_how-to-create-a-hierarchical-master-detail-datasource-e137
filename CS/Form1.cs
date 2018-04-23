using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Configuration;

namespace docITypedList {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            XtraReport report = CreateReport();

            report.ShowPreview();
        }

        private XtraReport CreateReport() {
            XtraReport report = new XtraReport();

            DetailBand detail = new DetailBand();
            detail.Height = 30;
            report.Bands.Add(detail);

            DetailReportBand detailReport1 = new DetailReportBand();
            report.Bands.Add(detailReport1);

            DetailBand detail1 = new DetailBand();
            detail1.Height = 30;
            detailReport1.Bands.Add(detail1);

            DetailReportBand detailReport2 = new DetailReportBand();
            detailReport1.Bands.Add(detailReport2);

            DetailBand detail2 = new DetailBand();
            detail2.Height = 30;
            detailReport2.Bands.Add(detail2);

            report.DataSource = CreateData();
            detailReport1.DataMember = "Products";
            detailReport2.DataMember = "Products.OrderDetails";

            detail.Controls.Add(CreateBoundLabel("CompanyName", Color.Gold, 0));
            detail1.Controls.Add(CreateBoundLabel("Products.ProductName", Color.Aqua, 100));
            detail2.Controls.Add(CreateBoundLabel("Products.OrderDetails.Quantity", Color.Pink, 200));

            return report;
        }

        private XRLabel CreateBoundLabel(string dataMember, Color backColor, int offset) {
            XRLabel label = new XRLabel();
            // Specify the label's binding depending on the data binding mode.
            if (Settings.Default.UserDesignerOptions.DataBindingMode == DataBindingMode.Bindings)
                label.DataBindings.Add(new XRBinding("Text", null, dataMember));
            else label.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", dataMember));
            label.BackColor = backColor;
            label.Location = new Point(offset, 0);

            return label;
        }

        private SupplierCollection CreateData() {
            SupplierCollection suppliers = new SupplierCollection();

            Supplier supplier = new Supplier("Exotic Liquids");
            suppliers.Add(supplier);
            supplier.Add(CreateProduct(supplier.SupplierID, "Chai"));
            supplier.Add(CreateProduct(supplier.SupplierID, "Chang"));
            supplier.Add(CreateProduct(supplier.SupplierID, "Aniseed Syrup"));

            supplier = new Supplier("New Orleans Cajun Delights");
            suppliers.Add(supplier);
            supplier.Add(CreateProduct(supplier.SupplierID, "Chef Anton's Cajun Seasoning"));
            supplier.Add(CreateProduct(supplier.SupplierID, "Chef Anton's Gumbo Mix"));

            supplier = new Supplier("Grandma Kelly's Homestead");
            suppliers.Add(supplier);
            supplier.Add(CreateProduct(supplier.SupplierID, "Grandma's Boysenberry Spread"));
            supplier.Add(CreateProduct(supplier.SupplierID, "Uncle Bob's Organic Dried Pears"));
            supplier.Add(CreateProduct(supplier.SupplierID, "Northwoods Cranberry Sauce"));

            return suppliers;
        }

        static Random random = new Random(5);

        private Product CreateProduct(int supplierID, string productName) {
            Product product = new Product(supplierID, productName);

            product.OrderDetails.AddRange(new OrderDetail[] { 
                new OrderDetail(product.ProductID, random.Next(0, 100)), 
                new OrderDetail(product.ProductID, random.Next(0, 100)),
                new OrderDetail(product.ProductID, random.Next(0, 100)) });

            return product;
        }
    }
}