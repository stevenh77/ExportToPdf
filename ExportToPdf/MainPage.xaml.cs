using System.Collections.Generic;
using System.IO;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.FormatProviders;
using Telerik.Windows.Documents.FormatProviders.Html;
using Telerik.Windows.Documents.FormatProviders.Pdf;
using Telerik.Windows.Documents.Model;

namespace ExportToPdf
{
    using System;
    using System.Reflection;
    using System.Windows.Controls;

    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            var data = Factory.GetDummyOrders();

            SaveFileDialog d = new SaveFileDialog();
            d.Filter = "PDF file format|*.pdf";

            // Save the document...
            if (d.ShowDialog() == true)
            {
                var grid = CreateGrid(data);
                var document = CreateDocument(grid);
                
                if (document != null)
                {
                    document.LayoutMode = DocumentLayoutMode.Paged;
                    document.Measure(RadDocument.MAX_DOCUMENT_SIZE);
                    document.Arrange(new RectangleF(PointF.Empty, document.DesiredSize));

                    IDocumentFormatProvider provider = new PdfFormatProvider();
                    using (var output = d.OpenFile())
                    {
                        provider.Export(document, output);
                    }
                }
            }
        }

        private RadGridView CreateGrid(IList<Order> data)
        {
            var grid = new RadGridView {ItemsSource = data};
            return grid;
        }

        private static RadDocument CreateDocument(RadGridView grid)
        {
            RadDocument document = null;

            using (var stream = new MemoryStream())
            {
                grid.Export(stream, new GridViewExportOptions()
                {
                    Format = ExportFormat.Html,
                    ShowColumnFooters = grid.ShowColumnFooters,
                    ShowColumnHeaders = grid.ShowColumnHeaders,
                    ShowGroupFooters = grid.ShowGroupFooters
                });

                stream.Position = 0;
                document = new HtmlFormatProvider().Import(stream);
            }

            return document;
        }
        
        private string GetPropertyValue(object o, string propertyName)
        {
            Type type = o.GetType();
            PropertyInfo info = type.GetProperty(propertyName);
            object value = info.GetValue(o, null);
            return value.ToString();
        }
    }
}