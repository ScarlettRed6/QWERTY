using CIRCUIT.Model;
using CIRCUIT.Model.DataRepositories;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace CIRCUIT.Utilities
{
    public static class PdfGenerator
    {
        public static void GenerateSalesPdf(List<SaleModel> transactions, string exportPath, SalesRepository saleConn)
        {
            using (var writer = new PdfWriter(exportPath))
            using (var pdf = new PdfDocument(writer))
            using (var document = new Document(pdf))
            {
                PdfFont boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

                // Add title
                document.Add(new Paragraph("Sales Transaction Export")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFont(boldFont)
                    .SetFontSize(16));
                document.Add(new Paragraph("\n"));

                //Table for sale initial details
                Table saleTable = new Table(UnitValue.CreatePercentArray(new float[] { 1.5f, 3, 3 }));
                saleTable.SetWidth(UnitValue.CreatePercentValue(100));


                //add the initial sale detail rows
                saleTable.AddHeaderCell(new Cell().Add(new Paragraph("Transaction ID").SetFont(boldFont)));
                saleTable.AddHeaderCell(new Cell().Add(new Paragraph("Cashier").SetFont(boldFont)));
                saleTable.AddHeaderCell(new Cell().Add(new Paragraph("Total Amount").SetFont(boldFont)));

                foreach (var transac in transactions)
                {
                    
                    saleTable.AddCell(new Paragraph(transac.SaleId.ToString()));
                    saleTable.AddCell(new Paragraph(transac.CashierName));
                    saleTable.AddCell(new Paragraph(transac.TotalAmount.ToString("C")));
                    
                }
                document.Add(saleTable);
                // Add spacing between transactions, with also a horizontal line
                LineSeparator separator = new LineSeparator(new SolidLine());
                separator.SetWidth(520);
                separator.SetMarginTop(10);
                separator.SetMarginBottom(5);
                document.Add(separator);


                //Table for sale items
                Table saleItemsTable = new Table(UnitValue.CreatePercentArray(new float[] { 1, 1, 4, 1, 2 }));
                saleItemsTable.SetWidth(UnitValue.CreatePercentValue(100));

                //Table header
                saleItemsTable.AddHeaderCell(new Cell().Add(new Paragraph("Item ID").SetFont(boldFont)));
                saleItemsTable.AddHeaderCell(new Cell().Add(new Paragraph("Sale ID").SetFont(boldFont)));
                saleItemsTable.AddHeaderCell(new Cell().Add(new Paragraph("Product Name").SetFont(boldFont)));
                saleItemsTable.AddHeaderCell(new Cell().Add(new Paragraph("Quantity").SetFont(boldFont)));
                saleItemsTable.AddHeaderCell(new Cell().Add(new Paragraph("Total Price").SetFont(boldFont)));

                foreach (var transac in transactions)
                {

                    // Fetch sale items for this transaction
                    var saleItems = saleConn.FetchSaleItems(transac.SaleId);

                    //Table rows
                    foreach (var item in saleItems)
                    {
                        saleItemsTable.AddCell(new Paragraph(item.SaleItemId.ToString()));
                        saleItemsTable.AddCell(new Paragraph(transac.SaleId.ToString()));
                        saleItemsTable.AddCell(new Paragraph(item.ProductName));
                        saleItemsTable.AddCell(new Paragraph(item.Quantity.ToString()));
                        saleItemsTable.AddCell(new Paragraph(item.ItemTotalPrice.ToString("C")));
                    }

                }

                document.Add(saleItemsTable);
            }
        }

        public static void GenerateReceiptPdf(SaleModel sale, int saleId, List<CartItem> cartItems, string exportPath)
        {
            using (var writer = new PdfWriter(exportPath))
            using (var pdf = new PdfDocument(writer))
            using (var document = new Document(pdf))
            {
                // Fonts
                PdfFont regularFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
                PdfFont boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

                // Header
                document.Add(new Paragraph("QWERTY")
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFont(boldFont)
                        .SetFontSize(18)
                        .SetMarginBottom(10));
                document.Add(new Paragraph("12345 Main Street, City, Country")
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFont(regularFont)
                        .SetFontSize(10));
                document.Add(new Paragraph("Phone: (123) 456-7890 | Email: support@qwerty.com")
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFont(regularFont)
                        .SetFontSize(10));
                document.Add(new Paragraph("------------------------------------------------------------------------------------------------------------------------")
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFont(regularFont));

                // Sale Information
                document.Add(new Paragraph($"Receipt #: {saleId}")
                        .SetFontSize(12)
                        .SetFont(boldFont));
                document.Add(new Paragraph($"Date: {sale.DateTime}")
                        .SetFontSize(12)
                        .SetFont(regularFont));
                document.Add(new Paragraph($"Cashier ID: {sale.CashierId}")
                        .SetFontSize(12)
                        .SetFont(regularFont));
                document.Add(new Paragraph("------------------------------------------------------------------------------------------------------------------------")
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFont(regularFont));

                // Table for sale items
                Table saleItemsTable = new Table(UnitValue.CreatePercentArray(new float[] { 4, 1, 2 }));
                saleItemsTable.SetWidth(UnitValue.CreatePercentValue(100));

                // Table headers
                saleItemsTable.AddHeaderCell(new Cell().Add(new Paragraph("Product").SetFont(boldFont).SetFontSize(10).SetTextAlignment(TextAlignment.LEFT)));
                saleItemsTable.AddHeaderCell(new Cell().Add(new Paragraph("Qty").SetFont(boldFont).SetFontSize(10).SetTextAlignment(TextAlignment.CENTER)));
                saleItemsTable.AddHeaderCell(new Cell().Add(new Paragraph("Price").SetFont(boldFont).SetFontSize(10).SetTextAlignment(TextAlignment.RIGHT)));

                // Table rows
                foreach (var item in cartItems)
                {
                    saleItemsTable.AddCell(new Paragraph(item.ProductName).SetFont(regularFont).SetFontSize(10).SetTextAlignment(TextAlignment.LEFT));
                    saleItemsTable.AddCell(new Paragraph(item.Quantity.ToString()).SetFont(regularFont).SetFontSize(10).SetTextAlignment(TextAlignment.CENTER));
                    saleItemsTable.AddCell(new Paragraph(item.TotalPrice.ToString("C")).SetFont(regularFont).SetFontSize(10).SetTextAlignment(TextAlignment.RIGHT));
                }

                document.Add(saleItemsTable);

                // Sale Summary
                document.Add(new Paragraph("------------------------------------------------------------------------------------------------------------------------")
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFont(regularFont));
                document.Add(new Paragraph($"Total Amount: {sale.TotalAmount:C}")
                        .SetFont(boldFont)
                        .SetFontSize(12)
                        .SetTextAlignment(TextAlignment.RIGHT));
                document.Add(new Paragraph($"Customer Paid: {sale.CustomerPaid:C}")
                        .SetFont(boldFont)
                        .SetFontSize(12)
                        .SetTextAlignment(TextAlignment.RIGHT));
                document.Add(new Paragraph($"Change Given: {sale.ChangeGiven:C}")
                        .SetFont(boldFont)
                        .SetFontSize(12)
                        .SetTextAlignment(TextAlignment.RIGHT));

                // Footer
                document.Add(new Paragraph("\n------------------------------------------------------------------------------------------------------------------------")
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFont(regularFont));
                document.Add(new Paragraph("Thank you for your purchase!")
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFont(boldFont)
                        .SetFontSize(14));
                document.Add(new Paragraph("Visit us again!")
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFont(regularFont)
                        .SetFontSize(12));
            }
        }

        public static void PrintSupplierReceipt(SuppliersModel supplier, int orderId,
        List<ProductModel> orderedProducts, decimal subTotal, decimal shippingFee, decimal totalAmount, string exportPath)
        {
            using (var writer = new PdfWriter(exportPath))
            using (var pdf = new PdfDocument(writer))
            using (var document = new Document(pdf))
            {
                PdfFont boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

                // Header
                document.Add(new Paragraph("Purchase Order Receipt")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFont(boldFont)
                    .SetFontSize(16));

                document.Add(new Paragraph($"\nOrder ID: {orderId}")
                    .SetFontSize(12));
                document.Add(new Paragraph($"Date: {DateTime.Now:dd/MM/yyyy}")
                    .SetFontSize(12));

                // Supplier Details
                document.Add(new Paragraph("\nSupplier Details")
                    .SetFont(boldFont)
                    .SetFontSize(14));
                document.Add(new Paragraph($"Name: {supplier.SupplierName}"));
                document.Add(new Paragraph($"Contact: {supplier.ContactName}"));
                document.Add(new Paragraph($"Phone: {supplier.Phone}"));
                document.Add(new Paragraph($"Email: {supplier.Email}"));
                document.Add(new Paragraph($"Address: {supplier.Address}"));

                // Products Table
                document.Add(new Paragraph("\nOrdered Products")
                    .SetFont(boldFont)
                    .SetFontSize(14));

                Table productTable = new Table(UnitValue.CreatePercentArray(new float[] { 4, 1, 1, 2 }))
                    .SetWidth(UnitValue.CreatePercentValue(100));

                // Table Headers
                productTable.AddHeaderCell(new Cell().Add(new Paragraph("Product Name").SetFont(boldFont)));
                productTable.AddHeaderCell(new Cell().Add(new Paragraph("Quantity").SetFont(boldFont)));
                productTable.AddHeaderCell(new Cell().Add(new Paragraph("Unit Price").SetFont(boldFont)));
                productTable.AddHeaderCell(new Cell().Add(new Paragraph("Total Cost").SetFont(boldFont)));

                // Table Rows
                foreach (var product in orderedProducts)
                {
                    productTable.AddCell(new Paragraph(product.ProductName));
                    productTable.AddCell(new Paragraph(product.OrderQuantity.ToString()));
                    productTable.AddCell(new Paragraph(product.UnitCost.ToString("C")));
                    productTable.AddCell(new Paragraph(product.TotalCost.ToString("C")));
                }

                document.Add(productTable);

                // Totals
                document.Add(new Paragraph("\nOrder Summary")
                    .SetFont(boldFont)
                    .SetFontSize(14));
                document.Add(new Paragraph($"Subtotal: {subTotal:C}"));
                document.Add(new Paragraph($"Shipping Fee: {shippingFee:C}"));
                document.Add(new Paragraph($"Total Amount: {totalAmount:C}"));

                // Footer
                document.Add(new Paragraph("\nThank you for your order!")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(12));
            }
        }

        public static void GenerateReport(
        string filePath,
        decimal grossProfit,
        decimal totalSalesRevenue,
        byte[] barChartImageBytes,
        byte[] pieChartImageBytes)
        {
            using (var writer = new PdfWriter(filePath))
            using (var pdf = new PdfDocument(writer))
            using (var document = new Document(pdf))
            {
                PdfFont boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

                document.Add(new Paragraph("Sales and Performance Report")
                    .SetFont(boldFont)
                    .SetFontSize(16)
                    .SetTextAlignment(TextAlignment.CENTER));

                document.Add(new Paragraph("\nGross Profit and Total Sales Revenue")
                    .SetFont(boldFont)
                    .SetFontSize(14));
                document.Add(new Paragraph($"Gross Profit: {grossProfit:C}"));
                document.Add(new Paragraph($"Total Sales Revenue: {totalSalesRevenue:C}"));

                // Add Cartesian Chart
                document.Add(new Paragraph("\nRevenue for the Last 7 Days").SetFont(boldFont).SetFontSize(14));
                var barChartImage = ImageDataFactory.Create(barChartImageBytes);
                var barChartPdfImage = new Image(barChartImage);
                document.Add(barChartPdfImage.SetHorizontalAlignment(HorizontalAlignment.CENTER));

                // Add Pie Chart
                document.Add(new Paragraph("\nRevenue Breakdown by Category").SetFont(boldFont).SetFontSize(14));
                var pieChartImage = ImageDataFactory.Create(pieChartImageBytes);
                var pieChartPdfImage = new Image(pieChartImage);
                document.Add(pieChartPdfImage.SetHorizontalAlignment(HorizontalAlignment.CENTER));
            }
        }

    }
}
