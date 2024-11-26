using CIRCUIT.Model;
using CIRCUIT.Model.DataRepositories;
using iText.IO.Font.Constants;
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
        public static void GenerateSalesPdf(List<SalesModel> transactions, string exportPath, SalesRepository saleConn)
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

                foreach (var transac in transactions)
                {
                    //Table for sale initial details
                    Table saleTable = new Table(UnitValue.CreatePercentArray(new float[] { 1.5f, 3, 3 }));
                    saleTable.SetWidth(UnitValue.CreatePercentValue(100));


                    //add the initial sale detail rows
                    saleTable.AddHeaderCell(new Cell().Add(new Paragraph("Transaction ID").SetFont(boldFont)));
                    saleTable.AddHeaderCell(new Cell().Add(new Paragraph("Cashier").SetFont(boldFont)));
                    saleTable.AddHeaderCell(new Cell().Add(new Paragraph("Total Amount").SetFont(boldFont)));
                    saleTable.AddCell(new Paragraph(transac.SaleId.ToString()));
                    saleTable.AddCell(new Paragraph(transac.CashierName));
                    saleTable.AddCell(new Paragraph(transac.TotalAmount.ToString("C")));

                    // Fetch sale items for this transaction
                    var saleItems = saleConn.FetchSaleItems(transac.SaleId);

                    //Table for sale items
                    Table saleItemsTable = new Table(UnitValue.CreatePercentArray(new float[] { 2, 4, 1, 2 }));
                    saleItemsTable.SetWidth(UnitValue.CreatePercentValue(100));

                    //Table header
                    saleItemsTable.AddHeaderCell(new Cell().Add(new Paragraph("Item ID").SetFont(boldFont)));
                    saleItemsTable.AddHeaderCell(new Cell().Add(new Paragraph("Product Name").SetFont(boldFont)));
                    saleItemsTable.AddHeaderCell(new Cell().Add(new Paragraph("Quantity").SetFont(boldFont)));
                    saleItemsTable.AddHeaderCell(new Cell().Add(new Paragraph("Total Price").SetFont(boldFont)));

                    //Table rows
                    foreach (var item in saleItems)
                    {
                        saleItemsTable.AddCell(new Paragraph(item.SaleItemId.ToString()));
                        saleItemsTable.AddCell(new Paragraph(item.ProductName));
                        saleItemsTable.AddCell(new Paragraph(item.Quantity.ToString()));
                        saleItemsTable.AddCell(new Paragraph(item.ItemTotalPrice.ToString("C")));
                    }

                    // Add labels
                    document.Add(new Paragraph("Transaction")
                    .SetTextAlignment(TextAlignment.LEFT)
                    .SetFont(boldFont)
                    .SetFontSize(11));
                    //Add the first table , the initial details
                    document.Add(saleTable);

                    // Another label
                    document.Add(new Paragraph("Sale Items")
                    .SetTextAlignment(TextAlignment.LEFT)
                    .SetFont(boldFont)
                    .SetFontSize(11));
                    //
                    document.Add(saleItemsTable);

                    // Add spacing between transactions, with also a horizontal line
                    LineSeparator separator = new LineSeparator(new SolidLine());
                    separator.SetWidth(520);  
                    separator.SetMarginTop(10);  
                    separator.SetMarginBottom(5);
                    document.Add(separator);

                }
            }
        }


    }
}
