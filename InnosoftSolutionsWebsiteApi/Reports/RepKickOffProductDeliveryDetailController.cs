using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public List<PrintDeliveryProductList> ProductLists { get; set; }
        public String Particulars { get; set; }
        public List<PrintDeliveryCheckList> CheckLists { get; set; }
        public String PreparedByUser { get; set; }
        public String SalesUser { get; set; }
        public String TechnicalUser { get; set; }
        public String FunctionalUser { get; set; }
        public String CustomerUser { get; set; }
        public String KickOffDeliveryDate { get; set; }
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
                PdfWriter writer = PdfWriter.GetInstance(document, workStream);
                writer.CloseStream = false;

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
                Font fontArial11White = FontFactory.GetFont("Arial", 11, BaseColor.WHITE);
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

                // line
                document.Add(line);

                // hex header color
                BaseColor headerTableColorHeader = WebColors.GetRGBColor("#112540");

                // table data
                PdfPTable kickOffDeliveryDataTable = new PdfPTable(4);
                float[] kickOffDeliveryDataTableWithCells = new float[] { 25f, 25f, 25f, 25f };
                kickOffDeliveryDataTable.SetWidths(kickOffDeliveryDataTableWithCells);
                kickOffDeliveryDataTable.WidthPercentage = 100;
                kickOffDeliveryDataTable.AddCell(new PdfPCell(new Phrase("Innosoft Form No.: " + deliveryObjectList.FirstOrDefault().ISFormNumber + " (Kick-off Meeting Document)", fontArial11White)) { Colspan = 4, PaddingTop = 10f, PaddingBottom = 12f, PaddingLeft = 5f, PaddingRight = 5f, HorizontalAlignment = 1, BackgroundColor = headerTableColorHeader });
                kickOffDeliveryDataTable.AddCell(new PdfPCell(new Phrase("Document No.: " + deliveryObjectList.FirstOrDefault().DocumentNumber, fontArial11)) { PaddingTop = 7f, PaddingBottom = 9f, PaddingLeft = 5f, PaddingRight = 5f });
                kickOffDeliveryDataTable.AddCell(new PdfPCell(new Phrase("Date: " + deliveryObjectList.FirstOrDefault().KickOffDeliveryDate, fontArial11)) { PaddingTop = 7f, PaddingBottom = 9f, PaddingLeft = 5f, PaddingRight = 5f });
                kickOffDeliveryDataTable.AddCell(new PdfPCell(new Phrase("Prepared By: " + deliveryObjectList.FirstOrDefault().PreparedByUser, fontArial11)) { Colspan = 2, PaddingTop = 7f, PaddingBottom = 9f, PaddingLeft = 5f, PaddingRight = 5f });
                kickOffDeliveryDataTable.AddCell(new PdfPCell(new Phrase("Customer: " + deliveryObjectList.FirstOrDefault().Customer, fontArial11)) { Colspan = 2, PaddingTop = 7f, PaddingBottom = 9f, PaddingLeft = 5f, PaddingRight = 5f });
                kickOffDeliveryDataTable.AddCell(new PdfPCell(new Phrase("Phone No.: " + deliveryObjectList.FirstOrDefault().CustomerPhoneNumber, fontArial11)) { Colspan = 2, PaddingTop = 7f, PaddingBottom = 9f, PaddingLeft = 5f, PaddingRight = 5f });
                kickOffDeliveryDataTable.AddCell(new PdfPCell(new Phrase("Address: " + deliveryObjectList.FirstOrDefault().CustomerAddress, fontArial11)) { Colspan = 4, PaddingTop = 7f, PaddingBottom = 9f, PaddingLeft = 5f, PaddingRight = 5f });
                kickOffDeliveryDataTable.AddCell(new PdfPCell(new Phrase("Contact Person: " + deliveryObjectList.FirstOrDefault().ContactPerson, fontArial11)) { Colspan = 2, PaddingTop = 7f, PaddingBottom = 9f, PaddingLeft = 5f, PaddingRight = 5f });
                kickOffDeliveryDataTable.AddCell(new PdfPCell(new Phrase("Phone No.: " + deliveryObjectList.FirstOrDefault().ContactPersonPhoneNumber, fontArial11)) { Colspan = 2, PaddingTop = 7f, PaddingBottom = 9f, PaddingLeft = 5f, PaddingRight = 5f });

                List<String> productListArray = new List<String>();
                for (var i = 0; i < deliveryObjectList.FirstOrDefault().ProductLists.Count(); i++)
                {
                    productListArray.Add(deliveryObjectList.FirstOrDefault().ProductLists[i].ProductDescription);
                }

                kickOffDeliveryDataTable.AddCell(new PdfPCell(new Phrase("Product/Service: " + String.Join(",", productListArray), fontArial11)) { Colspan = 4, PaddingTop = 7f, PaddingBottom = 9f, PaddingLeft = 5f, PaddingRight = 5f });
                kickOffDeliveryDataTable.AddCell(new PdfPCell(new Phrase("Particulars: " + deliveryObjectList.FirstOrDefault().Particulars, fontArial11)) { Colspan = 4, PaddingTop = 7f, PaddingBottom = 9f, PaddingLeft = 5f, PaddingRight = 5f, FixedHeight = 70f });

                // table data for checklist
                PdfPTable checkListTableData = new PdfPTable(2);
                float[] kcheckListTableDataWithCells = new float[] { 5f, 95f };
                checkListTableData.SetWidths(kcheckListTableDataWithCells);
                checkListTableData.WidthPercentage = 100;
                checkListTableData.AddCell(new PdfPCell(new Phrase("Checklist: ", fontArial11)) { Colspan = 2, Border = 0, PaddingTop = 4f, PaddingBottom = 6f, PaddingLeft = 5f, PaddingRight = 5f });

                foreach (var checklistvalue in deliveryObjectList.FirstOrDefault().CheckLists)
                {
                    PdfContentByte cb = writer.DirectContent;
                    PdfTemplate checkboxtemplate = cb.CreateTemplate(30, 30);
                    checkboxtemplate.SetLineWidth(0.5f);
                    checkboxtemplate.Rectangle(0, 0, 17f, 17f);
                    checkboxtemplate.Stroke();

                    Image checkboximage = Image.GetInstance(checkboxtemplate);
                    checkboximage.ScalePercent(53f);
                    PdfPCell checkboximagecell = new PdfPCell(checkboximage);

                    checkListTableData.AddCell(new PdfPCell(checkboximagecell) { HorizontalAlignment = 2, Border = 0, PaddingTop = 0f, PaddingBottom = 0f, PaddingLeft = 5f, PaddingRight = 5f });
                    checkListTableData.AddCell(new PdfPCell(new Phrase(checklistvalue.CheckListDescription, fontArial11)) { Border = 0, PaddingTop = 4f, PaddingBottom = 4f, PaddingLeft = 2f, PaddingRight = 5f });
                }

                kickOffDeliveryDataTable.AddCell(new PdfPCell(checkListTableData) { Colspan = 4, PaddingTop = 7f, PaddingBottom = 9f, PaddingLeft = 5f, PaddingRight = 5f });

                // table data for terms
                PdfPTable termsListData = new PdfPTable(2);
                float[] termsListDataWithCells = new float[] { 5f, 95f };
                termsListData.SetWidths(termsListDataWithCells);
                termsListData.WidthPercentage = 100;
                termsListData.AddCell(new PdfPCell(new Phrase("Terms: ", fontArial11)) { Colspan = 2, Border = 0, PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                termsListData.AddCell(new PdfPCell(new Phrase("•", fontArial11)) { HorizontalAlignment = 2, Border = 0, PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                termsListData.AddCell(new PdfPCell(new Phrase("This is to certify that the project has been handed over to Operation for implementation and delivery.  The customer now may contact " + deliveryObjectList.FirstOrDefault().FunctionalUser + " of Operation for any question related to the project.", fontArial11)) { Border = 0, PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                termsListData.AddCell(new PdfPCell(new Phrase("•", fontArial11)) { HorizontalAlignment = 2, Border = 0, PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                termsListData.AddCell(new PdfPCell(new Phrase("The one to sign this document will be the signatory of the Software Acceptance Document as well.", fontArial11)) { Border = 0, PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                
                kickOffDeliveryDataTable.AddCell(new PdfPCell(termsListData) { Colspan = 4, PaddingTop = 7f, PaddingBottom = 9f, PaddingLeft = 5f, PaddingRight = 5f });


                // table data for sales users
                PdfPTable salesUserListDataTable = new PdfPTable(1);
                float[] salesUserListDataTableWithCells = new float[] { 100f };
                salesUserListDataTable.SetWidths(salesUserListDataTableWithCells);
                salesUserListDataTable.WidthPercentage = 100;
                salesUserListDataTable.AddCell(new PdfPCell(new Phrase("Sales: ", fontArial11)) { Colspan = 2, Border = 0, PaddingTop = 4f, PaddingBottom = 50f, PaddingLeft = 5f, PaddingRight = 5f });
                salesUserListDataTable.AddCell(new PdfPCell(new Phrase(deliveryObjectList.FirstOrDefault().SalesUser, fontArial11)) { HorizontalAlignment = 1, Border = 0, PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                kickOffDeliveryDataTable.AddCell(new PdfPCell(salesUserListDataTable) { PaddingTop = 2f, PaddingBottom = 5f, PaddingLeft = 5f, PaddingRight = 5f });

                // table data for functional users
                PdfPTable functionalUserListDataTable = new PdfPTable(1);
                float[] functionalUserListDataTableWithCells = new float[] { 100f };
                functionalUserListDataTable.SetWidths(functionalUserListDataTableWithCells);
                functionalUserListDataTable.WidthPercentage = 100;
                functionalUserListDataTable.AddCell(new PdfPCell(new Phrase("Functional: ", fontArial11)) { Colspan = 2, Border = 0, PaddingTop = 4f, PaddingBottom = 50f, PaddingLeft = 5f, PaddingRight = 5f });
                functionalUserListDataTable.AddCell(new PdfPCell(new Phrase(deliveryObjectList.FirstOrDefault().FunctionalUser, fontArial11)) { HorizontalAlignment = 1, Border = 0, PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                kickOffDeliveryDataTable.AddCell(new PdfPCell(functionalUserListDataTable) { PaddingTop = 2f, PaddingBottom = 5f, PaddingLeft = 5f, PaddingRight = 5f });

                // table data for technical users
                PdfPTable technicalUserListDataTable = new PdfPTable(1);
                float[] technicalUserListDataTableWithCells = new float[] { 100f };
                technicalUserListDataTable.SetWidths(technicalUserListDataTableWithCells);
                technicalUserListDataTable.WidthPercentage = 100;
                technicalUserListDataTable.AddCell(new PdfPCell(new Phrase("Technical: ", fontArial11)) { Colspan = 2, Border = 0, PaddingTop = 4f, PaddingBottom = 50f, PaddingLeft = 5f, PaddingRight = 5f });
                technicalUserListDataTable.AddCell(new PdfPCell(new Phrase(deliveryObjectList.FirstOrDefault().TechnicalUser, fontArial11)) { HorizontalAlignment = 1, Border = 0, PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                kickOffDeliveryDataTable.AddCell(new PdfPCell(technicalUserListDataTable) { PaddingTop = 2f, PaddingBottom = 5f, PaddingLeft = 5f, PaddingRight = 5f });

                // table data for customer
                PdfPTable customerUserListDataTable = new PdfPTable(1);
                float[] customerUserListDataTableWithCells = new float[] { 100f };
                customerUserListDataTable.SetWidths(customerUserListDataTableWithCells);
                customerUserListDataTable.WidthPercentage = 100;
                customerUserListDataTable.AddCell(new PdfPCell(new Phrase("Customer: ", fontArial11)) { Colspan = 2, Border = 0, PaddingTop = 4f, PaddingBottom = 50f, PaddingLeft = 5f, PaddingRight = 5f });
                customerUserListDataTable.AddCell(new PdfPCell(new Phrase(deliveryObjectList.FirstOrDefault().CustomerUser, fontArial11)) { HorizontalAlignment = 1, Border = 0, PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                kickOffDeliveryDataTable.AddCell(new PdfPCell(customerUserListDataTable) { PaddingTop = 2f, PaddingBottom = 5f, PaddingLeft = 5f, PaddingRight = 5f });

                kickOffDeliveryDataTable.AddCell(new PdfPCell(new Phrase("Signed over printed name", fontArial11)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f, PaddingLeft = 5f, PaddingRight = 5f });
                kickOffDeliveryDataTable.AddCell(new PdfPCell(new Phrase("Signed over printed name", fontArial11)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f, PaddingLeft = 5f, PaddingRight = 5f });
                kickOffDeliveryDataTable.AddCell(new PdfPCell(new Phrase("Signed over printed name", fontArial11)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f, PaddingLeft = 5f, PaddingRight = 5f });
                kickOffDeliveryDataTable.AddCell(new PdfPCell(new Phrase("Signed over printed name", fontArial11)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f, PaddingLeft = 5f, PaddingRight = 5f });

                document.Add(kickOffDeliveryDataTable);

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