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
    public class RepQuotationDetailController : Controller
    {
        // database - LinQ to SQL class
        private Data.InnosoftSolutionsDatabaseDataContext db = new Data.InnosoftSolutionsDatabaseDataContext();

        // quotation detail
        public ActionResult quotationDetail(String quotationId)
        {
            if (quotationId != null)
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
                Font fontArial17Bold = FontFactory.GetFont("Arial", 17, Font.BOLD);
                Font fontArial16Bold = FontFactory.GetFont("Arial", 16, Font.BOLD);
                Font fontArial12Bold = FontFactory.GetFont("Arial", 12, Font.BOLD);
                Font fontArial12 = FontFactory.GetFont("Arial", 12);
                Font fontArial10Bold = FontFactory.GetFont("Arial", 10, Font.BOLD);
                Font fontArial10 = FontFactory.GetFont("Arial", 10);

                // line
                Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));

                // image
                string imagepath = Server.MapPath("~/Images/innosoft.png");
                Image logo = Image.GetInstance(imagepath);
                logo.ScalePercent(18f);
                PdfPCell imageCell = new PdfPCell(logo);

                // header
                PdfPTable quotationDetailIntroHeader = new PdfPTable(2);
                float[] quotationDetailIntroHeaderWithCells = new float[] { 14f, 100f };
                quotationDetailIntroHeader.SetWidths(quotationDetailIntroHeaderWithCells);
                quotationDetailIntroHeader.WidthPercentage = 100;
                quotationDetailIntroHeader.AddCell(new PdfPCell(imageCell) { HorizontalAlignment = 0, Rowspan = 4, Border = 0 });
                quotationDetailIntroHeader.AddCell(new PdfPCell(new Phrase("Cebu Innosoft Solutions Services Inc.", fontArial17Bold)) { HorizontalAlignment = 0, Border = 0, PaddingBottom = 2f });
                quotationDetailIntroHeader.AddCell(new PdfPCell(new Phrase("Innosoft Bldg. Corner V. Rama Avenue & R. Duterte St. Guadalupe, Cebu City", fontArial12)) { HorizontalAlignment = 0, Border = 0 });
                quotationDetailIntroHeader.AddCell(new PdfPCell(new Phrase("Tel No: (032) 520-7245 / (032) 415-1507 / (032) 263-2912", fontArial12)) { HorizontalAlignment = 0, Border = 0 });
                quotationDetailIntroHeader.AddCell(new PdfPCell(new Phrase("Email: info@innosoft.ph Website: http://www.innosoft.ph/", fontArial12)) { HorizontalAlignment = 0, Border = 0, PaddingBottom = 2f });
                document.Add(quotationDetailIntroHeader);

                // Document close
                document.Close();

                byte[] byteInfo = workStream.ToArray();
                workStream.Write(byteInfo, 0, byteInfo.Length);
                workStream.Position = 0;

                return new FileStreamResult(workStream, "application/pdf");
            }
            else
            {
                return RedirectToAction("NotFound", "Software");
            }
        }
    }
}