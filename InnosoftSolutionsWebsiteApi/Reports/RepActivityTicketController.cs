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
    public class RepActivityTicketController : Controller
    {
        // database - LinQ to SQL class
        private Data.InnosoftSolutionsDatabaseDataContext db = new Data.InnosoftSolutionsDatabaseDataContext();

        // lead activity
        public ActionResult activityTicket(String activityId)
        {
            if (activityId != null)
            {
                // PDF
                MemoryStream workStream = new MemoryStream();
                Rectangle rectangle = new Rectangle(PageSize.A3);
                Document document = new Document(rectangle, 72, 72, 72, 72);
                document.SetMargins(10f, 10f, 10f, 10f);
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
                logo.ScalePercent(16f);
                PdfPCell imageCell = new PdfPCell(logo);

                // header
                PdfPTable leadActivityDocumentHeader = new PdfPTable(2);
                float[] leadActivityDocumentHeaderWithCells = new float[] { 25f, 100f };
                leadActivityDocumentHeader.SetWidths(leadActivityDocumentHeaderWithCells);
                leadActivityDocumentHeader.WidthPercentage = 100;
                leadActivityDocumentHeader.AddCell(new PdfPCell(imageCell) { HorizontalAlignment = 0, Rowspan = 4, Border = 0 });
                leadActivityDocumentHeader.AddCell(new PdfPCell(new Phrase("Cebu Innosoft Solutions Services Inc.", fontArial16Bold)) { HorizontalAlignment = 0, Border = 0, PaddingBottom = 2f });
                leadActivityDocumentHeader.AddCell(new PdfPCell(new Phrase("Innosoft Bldg. Corner V. Rama Avenue & R. Duterte St.", fontArial10)) { HorizontalAlignment = 0, Border = 0 });
                leadActivityDocumentHeader.AddCell(new PdfPCell(new Phrase("Guadalupe, Cebu City", fontArial10)) { HorizontalAlignment = 0, Border = 0 });
                leadActivityDocumentHeader.AddCell(new PdfPCell(new Phrase("Contact No: (032) 263 2912 / 520 7245", fontArial10)) { HorizontalAlignment = 0, Border = 0, PaddingBottom = 2f });
                PdfPTable tableDevideLeadDocumentHeader = new PdfPTable(3);
                float[] tableDevideLeadDocumentHeaderWithCells = new float[] { 50f, 3f, 50f };
                tableDevideLeadDocumentHeader.SetWidths(tableDevideLeadDocumentHeaderWithCells);
                tableDevideLeadDocumentHeader.WidthPercentage = 100;
                tableDevideLeadDocumentHeader.AddCell(new PdfPCell(leadActivityDocumentHeader) { PaddingTop = 5f, PaddingBottom = 5f, PaddingLeft = 10f, PaddingRight = 10f });
                tableDevideLeadDocumentHeader.AddCell(new PdfPCell() { Border = 0, PaddingTop = 5f, PaddingBottom = 5f, PaddingLeft = 10f, PaddingRight = 10f });
                tableDevideLeadDocumentHeader.AddCell(new PdfPCell(leadActivityDocumentHeader) { PaddingTop = 5f, PaddingBottom = 5f, PaddingLeft = 10f, PaddingRight = 10f });
                document.Add(tableDevideLeadDocumentHeader);

                // activity query
                var activity = from d in db.IS_TrnActivities
                               where d.Id == Convert.ToInt32(activityId)
                               select d;

                if (activity.Any())
                {
                    // activty data
                    PdfPTable activityData = new PdfPTable(4);
                    float[] activityDataWithCells = new float[] { 25f, 45f, 25f, 30f };
                    activityData.SetWidths(activityDataWithCells);
                    activityData.WidthPercentage = 100;
                    activityData.AddCell(new PdfPCell(new Phrase("Act No:", fontArial10Bold)) { Border = 0, PaddingTop = 1f, PaddingBottom = 2f });
                    activityData.AddCell(new PdfPCell(new Phrase(activity.FirstOrDefault().ActivityNumber, fontArial10)) { Border = 0, PaddingTop = 1f, PaddingBottom = 2f });
                    activityData.AddCell(new PdfPCell(new Phrase("No of Hrs:", fontArial10Bold)) { Border = 0, PaddingTop = 1f, PaddingBottom = 2f });
                    activityData.AddCell(new PdfPCell(new Phrase(activity.FirstOrDefault().NumberOfHours.ToString("#,##0"), fontArial10)) { Border = 0, HorizontalAlignment = 2, PaddingTop = 1f, PaddingBottom = 2f });
                    activityData.AddCell(new PdfPCell(new Phrase("Act Date:", fontArial10Bold)) { Border = 0, PaddingTop = 1f, PaddingBottom = 2f });
                    activityData.AddCell(new PdfPCell(new Phrase(activity.FirstOrDefault().ActivityDate.ToShortDateString(), fontArial10)) { Border = 0, PaddingTop = 1f, PaddingBottom = 2f });
                    activityData.AddCell(new PdfPCell(new Phrase("Amount:", fontArial10Bold)) { Border = 0, PaddingTop = 1f, PaddingBottom = 2f });
                    activityData.AddCell(new PdfPCell(new Phrase(activity.FirstOrDefault().ActivityAmount.ToString("#,##0.00"), fontArial10)) { Border = 0, HorizontalAlignment = 2, PaddingTop = 1f, PaddingBottom = 2f });
                    PdfPTable tableDevideActivityData = new PdfPTable(3);
                    float[] tableDevideActivityDataWithCells = new float[] { 50f, 3f, 50f };
                    tableDevideActivityData.SetWidths(tableDevideActivityDataWithCells);
                    tableDevideActivityData.WidthPercentage = 100;
                    tableDevideActivityData.AddCell(new PdfPCell(activityData) { PaddingTop = 5f, PaddingBottom = 5f, PaddingLeft = 10f, PaddingRight = 10f });
                    tableDevideActivityData.AddCell(new PdfPCell() { Border = 0, PaddingTop = 5f, PaddingBottom = 5f, PaddingLeft = 10f, PaddingRight = 10f });
                    tableDevideActivityData.AddCell(new PdfPCell(activityData) { PaddingTop = 5f, PaddingBottom = 5f, PaddingLeft = 10f, PaddingRight = 10f });
                    document.Add(tableDevideActivityData);

                    var documentReference = "";
                    var documentDate = "";
                    var documentParticulars = "";
                    var address = "";
                    var contactNo = "";
                    if (activity.FirstOrDefault().LeadId != null)
                    {
                        documentReference = "LN-" + activity.FirstOrDefault().IS_TrnLead.LeadNumber;
                        documentDate = activity.FirstOrDefault().IS_TrnLead.LeadDate.ToShortDateString();
                        documentParticulars = activity.FirstOrDefault().IS_TrnLead.Remarks;
                        address = activity.FirstOrDefault().IS_TrnLead.Address;
                        contactNo = activity.FirstOrDefault().IS_TrnLead.ContactPhoneNo;
                    }
                    else
                    {
                        if (activity.FirstOrDefault().QuotationId != null)
                        {
                            documentReference = "QN-" + activity.FirstOrDefault().IS_TrnQuotation.QuotationNumber;
                            documentDate = activity.FirstOrDefault().IS_TrnQuotation.QuotationDate.ToShortDateString();
                            documentParticulars = activity.FirstOrDefault().IS_TrnQuotation.Remarks;
                        }
                        else
                        {
                            if (activity.FirstOrDefault().DeliveryId != null)
                            {
                                documentReference = "DN-" + activity.FirstOrDefault().IS_TrnDelivery.DeliveryNumber;
                                documentDate = activity.FirstOrDefault().IS_TrnDelivery.DeliveryDate.ToShortDateString();
                                documentParticulars = activity.FirstOrDefault().IS_TrnDelivery.Remarks;
                            }
                            else
                            {
                                if (activity.FirstOrDefault().SupportId != null)
                                {
                                    documentReference = "SN-" + activity.FirstOrDefault().IS_TrnSupport.SupportNumber;
                                    documentDate = activity.FirstOrDefault().IS_TrnSupport.SupportDate.ToShortDateString();
                                    documentParticulars = activity.FirstOrDefault().IS_TrnSupport.Remarks;
                                }
                                else
                                {
                                    if (activity.FirstOrDefault().SoftwareDevelopmentId != null)
                                    {
                                        documentReference = "SD-" + activity.FirstOrDefault().IS_TrnSoftwareDevelopment.SoftDevNumber;
                                        documentDate = activity.FirstOrDefault().IS_TrnSoftwareDevelopment.SoftDevDate.ToShortDateString();
                                        documentParticulars = activity.FirstOrDefault().IS_TrnSoftwareDevelopment.Remarks;
                                    }
                                }
                            }
                        }
                    }

                    var customer = "";
                    if (activity.FirstOrDefault().CustomerId != null)
                    {
                        customer = activity.FirstOrDefault().MstArticle.Article;
                    }
                    else
                    {
                        if (activity.FirstOrDefault().LeadId != null)
                        {
                            customer = activity.FirstOrDefault().IS_TrnLead.LeadName;
                        }
                    }

                    var product = "";
                    if (activity.FirstOrDefault().ProductId != null)
                    {
                        product = activity.FirstOrDefault().MstArticle1.Article;
                    }

                    // header data
                    PdfPTable headerActivityData = new PdfPTable(2);
                    float[] headerActivityDataWithCells = new float[] { 25f, 100f };
                    headerActivityData.SetWidths(headerActivityDataWithCells);
                    headerActivityData.WidthPercentage = 100;
                    headerActivityData.AddCell(new PdfPCell(new Phrase("Doc Ref:", fontArial10Bold)) { Border = 0, PaddingTop = 1f, PaddingBottom = 2f });
                    headerActivityData.AddCell(new PdfPCell(new Phrase(documentReference, fontArial10)) { Border = 0, PaddingTop = 1f, PaddingBottom = 2f });
                    headerActivityData.AddCell(new PdfPCell(new Phrase("Doc Date:", fontArial10Bold)) { Border = 0, PaddingTop = 1f, PaddingBottom = 2f });
                    headerActivityData.AddCell(new PdfPCell(new Phrase(documentDate, fontArial10)) { Border = 0, PaddingTop = 1f, PaddingBottom = 2f });
                    headerActivityData.AddCell(new PdfPCell(new Phrase("Customer:", fontArial10Bold)) { Border = 0, PaddingTop = 1f, PaddingBottom = 2f });
                    headerActivityData.AddCell(new PdfPCell(new Phrase(customer, fontArial10)) { Border = 0, PaddingTop = 1f, PaddingBottom = 2f });
                    headerActivityData.AddCell(new PdfPCell(new Phrase("Product:", fontArial10Bold)) { Border = 0, PaddingTop = 1f, PaddingBottom = 2f });
                    headerActivityData.AddCell(new PdfPCell(new Phrase(product, fontArial10)) { Border = 0, PaddingTop = 1f, PaddingBottom = 2f });
                    headerActivityData.AddCell(new PdfPCell(new Phrase("Address:", fontArial10Bold)) { Border = 0, PaddingTop = 1f, PaddingBottom = 2f });
                    headerActivityData.AddCell(new PdfPCell(new Phrase(address, fontArial10)) { Border = 0, PaddingTop = 1f, PaddingBottom = 2f });
                    headerActivityData.AddCell(new PdfPCell(new Phrase("Contact No:", fontArial10Bold)) { Border = 0, PaddingTop = 1f, PaddingBottom = 2f });
                    headerActivityData.AddCell(new PdfPCell(new Phrase(contactNo, fontArial10)) { Border = 0, PaddingTop = 1f, PaddingBottom = 2f });
                    PdfPTable tableDevideHeaderActivityData = new PdfPTable(3);
                    float[] tableDevideHeaderActivityDataWithCells = new float[] { 50f, 3f, 50f };
                    tableDevideHeaderActivityData.SetWidths(tableDevideHeaderActivityDataWithCells);
                    tableDevideHeaderActivityData.WidthPercentage = 100;
                    tableDevideHeaderActivityData.AddCell(new PdfPCell(headerActivityData) { PaddingTop = 5f, PaddingBottom = 5f, PaddingLeft = 10f, PaddingRight = 10f });
                    tableDevideHeaderActivityData.AddCell(new PdfPCell() { Border = 0, PaddingTop = 5f, PaddingBottom = 5f, PaddingLeft = 10f, PaddingRight = 10f });
                    tableDevideHeaderActivityData.AddCell(new PdfPCell(headerActivityData) { PaddingTop = 5f, PaddingBottom = 5f, PaddingLeft = 10f, PaddingRight = 10f });
                    document.Add(tableDevideHeaderActivityData);

                    // remarks
                    PdfPTable headerActivityRemarksData = new PdfPTable(1);
                    float[] headerActivityRemarksDataWithCells = new float[] { 100f };
                    headerActivityRemarksData.SetWidths(headerActivityRemarksDataWithCells);
                    headerActivityRemarksData.WidthPercentage = 100;
                    headerActivityRemarksData.AddCell(new PdfPCell(new Phrase(documentParticulars, fontArial10)) { Border = 0, PaddingTop = 1f, PaddingBottom = 2f });
                    PdfPTable tableDevideHeaderActivityRemakrsData = new PdfPTable(3);
                    float[] tableDevideHeaderActivityRemakrsDataWithCells = new float[] { 50f, 3f, 50f };
                    tableDevideHeaderActivityRemakrsData.SetWidths(tableDevideHeaderActivityRemakrsDataWithCells);
                    tableDevideHeaderActivityRemakrsData.WidthPercentage = 100;
                    tableDevideHeaderActivityRemakrsData.AddCell(new PdfPCell(headerActivityRemarksData) { PaddingTop = 5f, PaddingBottom = 5f, PaddingLeft = 10f, PaddingRight = 10f, FixedHeight = 110 });
                    tableDevideHeaderActivityRemakrsData.AddCell(new PdfPCell() { Border = 0, PaddingTop = 5f, PaddingBottom = 5f, PaddingLeft = 10f, PaddingRight = 10f, FixedHeight = 110 });
                    tableDevideHeaderActivityRemakrsData.AddCell(new PdfPCell(headerActivityRemarksData) { PaddingTop = 5f, PaddingBottom = 5f, PaddingLeft = 10f, PaddingRight = 10f, FixedHeight = 110 });
                    document.Add(tableDevideHeaderActivityRemakrsData);

                    // particulars
                    PdfPTable activityParticularsData = new PdfPTable(1);
                    float[] activityParticularsDataWithCells = new float[] { 100f };
                    activityParticularsData.SetWidths(activityParticularsDataWithCells);
                    activityParticularsData.WidthPercentage = 100;
                    activityParticularsData.AddCell(new PdfPCell(new Phrase(activity.FirstOrDefault().Particulars, fontArial10)) { Border = 0, PaddingTop = 1f, PaddingBottom = 2f });
                    PdfPTable tableDevideActivityParticularsData = new PdfPTable(3);
                    float[] tableDevideActivityParticularsDataWithCells = new float[] { 50f, 3f, 50f };
                    tableDevideActivityParticularsData.SetWidths(tableDevideActivityParticularsDataWithCells);
                    tableDevideActivityParticularsData.WidthPercentage = 100;
                    tableDevideActivityParticularsData.AddCell(new PdfPCell(activityParticularsData) { PaddingTop = 5f, PaddingBottom = 5f, PaddingLeft = 10f, PaddingRight = 10f, FixedHeight = 110 });
                    tableDevideActivityParticularsData.AddCell(new PdfPCell() { Border = 0, PaddingTop = 5f, PaddingBottom = 5f, PaddingLeft = 10f, PaddingRight = 10f, FixedHeight = 110 });
                    tableDevideActivityParticularsData.AddCell(new PdfPCell(activityParticularsData) { PaddingTop = 5f, PaddingBottom = 5f, PaddingLeft = 10f, PaddingRight = 10f, FixedHeight = 110 });
                    document.Add(tableDevideActivityParticularsData);

                    // punch time
                    PdfPTable inOutTable = new PdfPTable(4);
                    float[] inOutTableWithCells = new float[] { 25f, 25f, 25f, 25f };
                    inOutTable.SetWidths(inOutTableWithCells);
                    inOutTable.WidthPercentage = 100;
                    inOutTable.AddCell(new PdfPCell(new Phrase("Time In: ", fontArial10Bold)) { Border = 0, PaddingTop = 1f, PaddingBottom = 2f });
                    inOutTable.AddCell(new PdfPCell(new Phrase(" ", fontArial10)) { Border = 0, PaddingTop = 1f, PaddingBottom = 2f });
                    inOutTable.AddCell(new PdfPCell(new Phrase("Time Out: ", fontArial10Bold)) { Border = 0, PaddingTop = 1f, PaddingBottom = 2f });
                    inOutTable.AddCell(new PdfPCell(new Phrase(" ", fontArial10)) { Border = 0, PaddingTop = 1f, PaddingBottom = 2f });
                    PdfPTable tableDevideInOutTableData = new PdfPTable(3);
                    float[] tableDevideInOutTableDataWithCells = new float[] { 50f, 3f, 50f };
                    tableDevideInOutTableData.SetWidths(tableDevideInOutTableDataWithCells);
                    tableDevideInOutTableData.WidthPercentage = 100;
                    tableDevideInOutTableData.AddCell(new PdfPCell(inOutTable) { PaddingTop = 5f, PaddingBottom = 5f, PaddingLeft = 10f, PaddingRight = 10f });
                    tableDevideInOutTableData.AddCell(new PdfPCell() { Border = 0, PaddingTop = 5f, PaddingBottom = 5f, PaddingLeft = 10f, PaddingRight = 10f });
                    tableDevideInOutTableData.AddCell(new PdfPCell(inOutTable) { PaddingTop = 5f, PaddingBottom = 5f, PaddingLeft = 10f, PaddingRight = 10f });
                    document.Add(tableDevideInOutTableData);

                    // Table for Footer
                    PdfPTable tableFooter = new PdfPTable(3);
                    tableFooter.WidthPercentage = 100;
                    float[] widthsCells2 = new float[] { 100f, 20f, 100f };
                    tableFooter.SetWidths(widthsCells2);
                    tableFooter.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0, PaddingTop = 25f, PaddingBottom = 10f });
                    tableFooter.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0, PaddingTop = 25f, PaddingBottom = 10f });
                    tableFooter.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0, PaddingTop = 25f, PaddingBottom = 10f });
                    tableFooter.AddCell(new PdfPCell(new Phrase(activity.FirstOrDefault().MstUser.FullName, fontArial10)) { HorizontalAlignment = 1, Border = 0, PaddingTop = 5f, PaddingBottom = 5f });
                    tableFooter.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0, PaddingTop = 5f, PaddingBottom = 5f });
                    tableFooter.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0, PaddingTop = 5f, PaddingBottom = 5f });
                    tableFooter.AddCell(new PdfPCell(new Phrase("Prepared by", fontArial10Bold)) { Border = 1, HorizontalAlignment = 1, PaddingBottom = 5f });
                    tableFooter.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0, PaddingBottom = 5f });
                    tableFooter.AddCell(new PdfPCell(new Phrase("Signature over printed name", fontArial10Bold)) { Border = 1, HorizontalAlignment = 1, PaddingBottom = 5f });
                    PdfPTable tableDevideTableFooter = new PdfPTable(3);
                    float[] tableDevideTableFooterWithCells = new float[] { 50f, 3f, 50f };
                    tableDevideTableFooter.SetWidths(tableDevideTableFooterWithCells);
                    tableDevideTableFooter.WidthPercentage = 100;
                    tableDevideTableFooter.AddCell(new PdfPCell(tableFooter) { PaddingTop = 10f, PaddingBottom = 10f, PaddingLeft = 10f, PaddingRight = 10f });
                    tableDevideTableFooter.AddCell(new PdfPCell() { Border = 0, PaddingTop = 10f, PaddingBottom = 10f, PaddingLeft = 10f, PaddingRight = 10f });
                    tableDevideTableFooter.AddCell(new PdfPCell(tableFooter) { PaddingTop = 10f, PaddingBottom = 10f, PaddingLeft = 10f, PaddingRight = 10f });
                    document.Add(tableDevideTableFooter);
                }

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