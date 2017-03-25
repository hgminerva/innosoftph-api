using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InnosoftSolutionsWebsiteApi.Reports
{
    // product list
    public class PrintDeliveryProductList
    {
        public Int32 Id { get; set; }
        public String ProductDescription { get; set; }
    }

    // checklist
    public class PrintDeliveryCheckList
    {
        public Int32 Id { get; set; }
        public String CheckListDescription { get; set; }
    }

    // obj list
    public class PrintDeliveryObjectList
    {
        public String ISFormNumber { get; set; }
        public String Customer { get; set; }
        public String CustomerPhoneNumber { get; set; }
        public String CustomerAddress { get; set; }
        public String DocumentNumber { get; set; }
        public String ContactPerson { get; set; }
        public String ContactPersonPhoneNumber { get; set; }
        public String ContactPersonAddress { get; set; }
        public List<PrintDeliveryProductList> ProductLists { get; set; }
        public String Particulars { get; set; }
        public List<PrintDeliveryCheckList> CheckLists { get; set; }
        public String PreparedByUser { get; set; }
        public String SalesUser { get; set; }
        public String TechnicalUser { get; set; }
        public String FunctionalUser { get; set; }
        public String CustomerUser { get; set; }
    }

    // PDF
    public class RepKickOffProductDeliveryDetailController : Controller
    {
        // database - LinQ to SQL class
        private Data.InnosoftSolutionsDatabaseDataContext db = new Data.InnosoftSolutionsDatabaseDataContext();

        // quotation detail
        public ActionResult deliveryDetail(String deliveryId, List<PrintDeliveryObjectList> deliveryObjectList)
        {
            if (deliveryId != null)
            {
                // PDF
                MemoryStream workStream = new MemoryStream();
                Rectangle rectangle = new Rectangle(PageSize.A3);
                Document document = new Document(rectangle, 72, 72, 72, 72);
                document.SetMargins(50f, 50f, 50f, 50f);
                PdfWriter.GetInstance(document, workStream).CloseStream = false;

                // Document open
                document.Open();

                // Fonts
                Font fontArial19Bold = FontFactory.GetFont("Arial", 20, Font.BOLD);
                Font fontArial17Bold = FontFactory.GetFont("Arial", 17, Font.BOLD);
                Font fontArial16Bold = FontFactory.GetFont("Arial", 16, Font.BOLD);
                Font fontArial12Bold = FontFactory.GetFont("Arial", 12, Font.BOLD);
                Font fontArial12 = FontFactory.GetFont("Arial", 12);
                Font fontArial11Bold = FontFactory.GetFont("Arial", 11, Font.BOLD);
                Font fontArial11 = FontFactory.GetFont("Arial", 11);
                Font fontArial11ITALIC = FontFactory.GetFont("Arial", 12, Font.ITALIC);
                Font fontArial10Bold = FontFactory.GetFont("Arial", 10, Font.BOLD);
                Font fontArial10 = FontFactory.GetFont("Arial", 10);
                Font fontArial10ITALIC = FontFactory.GetFont("Arial", 10, Font.ITALIC);

                // line
                Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));

                // image
                string imagepath = Server.MapPath("~/Images/innosoft.png");
                Image logo = Image.GetInstance(imagepath);
                logo.ScalePercent(18f);
                PdfPCell imageCell = new PdfPCell(logo);

                // header
                PdfPTable quotationDetailIntroHeader = new PdfPTable(2);
                float[] quotationDetailIntroHeaderWithCells = new float[] { 30f, 100f };
                quotationDetailIntroHeader.SetWidths(quotationDetailIntroHeaderWithCells);
                quotationDetailIntroHeader.WidthPercentage = 100;
                quotationDetailIntroHeader.AddCell(new PdfPCell(imageCell) { HorizontalAlignment = 2, Rowspan = 4, Border = 0, PaddingRight = 10f });
                quotationDetailIntroHeader.AddCell(new PdfPCell(new Phrase("CEBU INNOSOFT SOLUTIONS SERVICES INC.", fontArial19Bold)) { HorizontalAlignment = 0, Border = 0, PaddingBottom = 2f });
                quotationDetailIntroHeader.AddCell(new PdfPCell(new Phrase("YnS Building, Corner V. Rama Avenue & R. Duterte Street, Guadalupe, Cebu City 6000", fontArial11)) { HorizontalAlignment = 0, Border = 0 });
                quotationDetailIntroHeader.AddCell(new PdfPCell(new Phrase("Tel No: (032) 520-7245 / (032) 415-1507 / (032) 263-2912 ", fontArial11)) { HorizontalAlignment = 0, Border = 0 });
                quotationDetailIntroHeader.AddCell(new PdfPCell(new Phrase("Email: info@innosoft.ph Website: http://www.innosoft.ph/", fontArial11)) { HorizontalAlignment = 0, Border = 0, PaddingBottom = 10f });
                document.Add(quotationDetailIntroHeader);

                // line header
                const string quote = "\"";
                PdfPTable quotationDetailLineHeader = new PdfPTable(1);
                float[] quotationDetailLineHeaderWithCells = new float[] { 100f };
                quotationDetailLineHeader.SetWidths(quotationDetailLineHeaderWithCells);
                quotationDetailLineHeader.WidthPercentage = 100;
                quotationDetailLineHeader.AddCell(new PdfPCell(new Phrase("", fontArial17Bold)) { Border = 1 });
                quotationDetailLineHeader.AddCell(new PdfPCell(new Phrase(quote + "Innovative and Practical Technology Solutions that fits your business and budget needs." + quote, fontArial11ITALIC)) { Border = 0, PaddingBottom = 6f, HorizontalAlignment = 1 });
                quotationDetailLineHeader.AddCell(new PdfPCell(new Phrase("", fontArial17Bold)) { Border = 1 });
                document.Add(quotationDetailLineHeader);


                // Document close
                document.Close();

                byte[] byteInfo = workStream.ToArray();
                workStream.Write(byteInfo, 0, byteInfo.Length);
                workStream.Position = 0;

                return new FileStreamResult(workStream, "application/pdf");
            }
            else
            {
                return null;
            }
        }
    }
}