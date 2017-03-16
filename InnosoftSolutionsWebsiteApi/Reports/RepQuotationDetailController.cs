using iTextSharp.text;
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
    public class RepQuotationDetailController : Controller
    {
        // database - LinQ to SQL class
        private Data.InnosoftSolutionsDatabaseDataContext db = new Data.InnosoftSolutionsDatabaseDataContext();

        // quotation detail
        public ActionResult quotationDetail(String quotationId, List<Entities.PrintQuotationObjectLists> quotationObjectLists)
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

                // quotation title
                PdfPTable quotationDetailQuotationTitleHeader = new PdfPTable(1);
                float[] quotationDetailQuotationTitleHeaderWithCells = new float[] { 100f };
                quotationDetailQuotationTitleHeader.SetWidths(quotationDetailQuotationTitleHeaderWithCells);
                quotationDetailQuotationTitleHeader.WidthPercentage = 100;
                quotationDetailQuotationTitleHeader.AddCell(new PdfPCell(new Phrase("SALES QUOTATION", fontArial12Bold)) { Border = 0, PaddingBottom = 20f, PaddingTop = 20f, HorizontalAlignment = 1 });
                document.Add(quotationDetailQuotationTitleHeader);

                // quotation customer
                foreach (var customerDetail in quotationObjectLists)
                {
                    PdfPTable quotationDetailCustomerDetail = new PdfPTable(4);
                    float[] quotationDetailCustomerDetaileHeaderWithCells = new float[] { 15f, 50f, 15f, 20f };
                    quotationDetailCustomerDetail.SetWidths(quotationDetailCustomerDetaileHeaderWithCells);
                    quotationDetailCustomerDetail.WidthPercentage = 100;
                    quotationDetailCustomerDetail.AddCell(new PdfPCell(new Phrase("Customer", fontArial11Bold)) { PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                    quotationDetailCustomerDetail.AddCell(new PdfPCell(new Phrase(customerDetail.CustomerName, fontArial11)) { PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                    quotationDetailCustomerDetail.AddCell(new PdfPCell(new Phrase("QRef. No", fontArial11Bold)) { PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                    quotationDetailCustomerDetail.AddCell(new PdfPCell(new Phrase(customerDetail.QRefNumber, fontArial11)) { PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                    quotationDetailCustomerDetail.AddCell(new PdfPCell(new Phrase("Address", fontArial11Bold)) { PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                    quotationDetailCustomerDetail.AddCell(new PdfPCell(new Phrase(customerDetail.CustomerAddress, fontArial11)) { PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                    quotationDetailCustomerDetail.AddCell(new PdfPCell(new Phrase("Date", fontArial11Bold)) { PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                    quotationDetailCustomerDetail.AddCell(new PdfPCell(new Phrase(customerDetail.QDate, fontArial11)) { PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                    quotationDetailCustomerDetail.AddCell(new PdfPCell(new Phrase("Contact Person", fontArial11Bold)) { PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                    quotationDetailCustomerDetail.AddCell(new PdfPCell(new Phrase(customerDetail.CustomerContactPerson, fontArial11)) { PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                    quotationDetailCustomerDetail.AddCell(new PdfPCell(new Phrase("Client PO No", fontArial11Bold)) { PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                    quotationDetailCustomerDetail.AddCell(new PdfPCell(new Phrase(customerDetail.ClientPONo, fontArial11)) { PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                    quotationDetailCustomerDetail.AddCell(new PdfPCell(new Phrase("Contact Number", fontArial11Bold)) { PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                    quotationDetailCustomerDetail.AddCell(new PdfPCell(new Phrase(customerDetail.CustomerContactPerson, fontArial11)) { PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                    quotationDetailCustomerDetail.AddCell(new PdfPCell(new Phrase("Client PO Date", fontArial11Bold)) { PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                    quotationDetailCustomerDetail.AddCell(new PdfPCell(new Phrase(customerDetail.ClientPONo, fontArial11)) { PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                    quotationDetailCustomerDetail.AddCell(new PdfPCell(new Phrase("Contact Email", fontArial11Bold)) { PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                    quotationDetailCustomerDetail.AddCell(new PdfPCell(new Phrase(customerDetail.CustomerContactPerson, fontArial11)) { PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                    quotationDetailCustomerDetail.AddCell(new PdfPCell(new Phrase("Leads Ref No", fontArial11Bold)) { PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                    quotationDetailCustomerDetail.AddCell(new PdfPCell(new Phrase(customerDetail.ClientPONo, fontArial11)) { PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                    document.Add(quotationDetailCustomerDetail);
                }

                // quotation product label and product data
                PdfPTable quotationDetailQuotationProductLabel = new PdfPTable(1);
                float[] quotationDetailQuotationProductLabelWithCells = new float[] { 100f };
                quotationDetailQuotationProductLabel.SetWidths(quotationDetailQuotationProductLabelWithCells);
                quotationDetailQuotationProductLabel.WidthPercentage = 100;
                quotationDetailQuotationProductLabel.AddCell(new PdfPCell(new Phrase("I. \t\t\t\t\t\t\t\t Product / Service ", fontArial11)) { Border = 0, PaddingTop = 20f, PaddingBottom = 10f, HorizontalAlignment = 0 });
                document.Add(quotationDetailQuotationProductLabel);
                PdfPTable quotationDetailQuotationProduct = new PdfPTable(5);
                float[] quotationDetailQuotationProductWithCells = new float[] { 20f, 35f, 15f, 15f, 15f };
                quotationDetailQuotationProduct.SetWidths(quotationDetailQuotationProductWithCells);
                quotationDetailQuotationProduct.WidthPercentage = 100;
                quotationDetailQuotationProduct.AddCell(new PdfPCell(new Phrase("Product Code", fontArial11Bold)) { HorizontalAlignment = 1, PaddingTop = 5f, PaddingBottom = 7f, PaddingLeft = 5f, PaddingRight = 5f });
                quotationDetailQuotationProduct.AddCell(new PdfPCell(new Phrase("Description (Product / Modules)", fontArial11Bold)) { HorizontalAlignment = 1, PaddingTop = 5f, PaddingBottom = 7f, PaddingLeft = 5f, PaddingRight = 5f });
                quotationDetailQuotationProduct.AddCell(new PdfPCell(new Phrase("Unit Price", fontArial11Bold)) { HorizontalAlignment = 1, PaddingTop = 5f, PaddingBottom = 7f, PaddingLeft = 5f, PaddingRight = 5f });
                quotationDetailQuotationProduct.AddCell(new PdfPCell(new Phrase("Quantity License", fontArial11Bold)) { HorizontalAlignment = 1, PaddingTop = 5f, PaddingBottom = 7f, PaddingLeft = 5f, PaddingRight = 5f });
                quotationDetailQuotationProduct.AddCell(new PdfPCell(new Phrase("Amount", fontArial11Bold)) { HorizontalAlignment = 1, PaddingTop = 5f, PaddingBottom = 7f, PaddingLeft = 5f, PaddingRight = 5f });
                Decimal totalAmount = 0;
                foreach (var productList in quotationObjectLists.FirstOrDefault().ProdcutLists)
                {
                    totalAmount += productList.Amount;
                    quotationDetailQuotationProduct.AddCell(new PdfPCell(new Phrase(productList.ProductCode, fontArial11)) { PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                    quotationDetailQuotationProduct.AddCell(new PdfPCell(new Phrase(productList.ProductDescription, fontArial11)) { PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                    quotationDetailQuotationProduct.AddCell(new PdfPCell(new Phrase(productList.Price.ToString("#,##0.00"), fontArial11)) { HorizontalAlignment = 2, PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                    quotationDetailQuotationProduct.AddCell(new PdfPCell(new Phrase(productList.Quantity.ToString("#,##0.00"), fontArial11)) { HorizontalAlignment = 2, PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                    quotationDetailQuotationProduct.AddCell(new PdfPCell(new Phrase(productList.Amount.ToString("#,##0.00"), fontArial11)) { HorizontalAlignment = 2, PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                }
                quotationDetailQuotationProduct.AddCell(new PdfPCell(new Phrase("", fontArial11Bold)) { });
                quotationDetailQuotationProduct.AddCell(new PdfPCell(new Phrase("Total", fontArial11Bold)) { HorizontalAlignment = 0, PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                quotationDetailQuotationProduct.AddCell(new PdfPCell(new Phrase("", fontArial11Bold)) { });
                quotationDetailQuotationProduct.AddCell(new PdfPCell(new Phrase("", fontArial11Bold)) { });
                quotationDetailQuotationProduct.AddCell(new PdfPCell(new Phrase(totalAmount.ToString("#,##0.00"), fontArial11Bold)) { HorizontalAlignment = 2, PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                document.Add(quotationDetailQuotationProduct);

                // quotation payment label and payment data
                PdfPTable quotationDetailQuotationpaymentLabel = new PdfPTable(1);
                float[] quotationDetailQuotationpaymentLabelWithCells = new float[] { 100f };
                quotationDetailQuotationpaymentLabel.SetWidths(quotationDetailQuotationpaymentLabelWithCells);
                quotationDetailQuotationpaymentLabel.WidthPercentage = 100;
                quotationDetailQuotationpaymentLabel.AddCell(new PdfPCell(new Phrase("II. \t\t\t\t\t\t\t\t Payment Schedule", fontArial11)) { Border = 0, PaddingTop = 20f, PaddingBottom = 10f, HorizontalAlignment = 0 });
                document.Add(quotationDetailQuotationpaymentLabel);
                PdfPTable quotationDetailQuotationpayment = new PdfPTable(3);
                float[] quotationDetailQuotationpaymentWithCells = new float[] { 40f, 25f, 35f };
                quotationDetailQuotationpayment.SetWidths(quotationDetailQuotationpaymentWithCells);
                quotationDetailQuotationpayment.WidthPercentage = 100;
                quotationDetailQuotationpayment.AddCell(new PdfPCell(new Phrase("Description", fontArial11Bold)) { HorizontalAlignment = 1, PaddingTop = 5f, PaddingBottom = 7f, PaddingLeft = 5f, PaddingRight = 5f });
                quotationDetailQuotationpayment.AddCell(new PdfPCell(new Phrase("Amount", fontArial11Bold)) { HorizontalAlignment = 1, PaddingTop = 5f, PaddingBottom = 7f, PaddingLeft = 5f, PaddingRight = 5f });
                quotationDetailQuotationpayment.AddCell(new PdfPCell(new Phrase("Remarks", fontArial11Bold)) { HorizontalAlignment = 1, PaddingTop = 5f, PaddingBottom = 7f, PaddingLeft = 5f, PaddingRight = 5f });
                Decimal totalPaymentAmount = 0;
                foreach (var paymentList in quotationObjectLists.FirstOrDefault().PaymentLists)
                {
                    totalPaymentAmount += paymentList.Amount;
                    quotationDetailQuotationpayment.AddCell(new PdfPCell(new Phrase(paymentList.Description, fontArial11)) { PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                    quotationDetailQuotationpayment.AddCell(new PdfPCell(new Phrase(paymentList.Amount.ToString("#,##0.00"), fontArial11)) { HorizontalAlignment = 2, PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                    quotationDetailQuotationpayment.AddCell(new PdfPCell(new Phrase(paymentList.Remarks, fontArial11)) { PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                }
                quotationDetailQuotationpayment.AddCell(new PdfPCell(new Phrase("Total", fontArial11Bold)) { HorizontalAlignment = 0, PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                quotationDetailQuotationpayment.AddCell(new PdfPCell(new Phrase(totalPaymentAmount.ToString("#,##0.00"), fontArial11Bold)) { HorizontalAlignment = 2, PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                quotationDetailQuotationpayment.AddCell(new PdfPCell(new Phrase("", fontArial11Bold)) { });
                document.Add(quotationDetailQuotationpayment);

                // quotation model of payment
                PdfPTable quotationDetailQuotationModeofPaymentLabel = new PdfPTable(1);
                float[] quotationDetailQuotationModeofPaymentLabelWithCells = new float[] { 100f };
                quotationDetailQuotationModeofPaymentLabel.SetWidths(quotationDetailQuotationModeofPaymentLabelWithCells);
                quotationDetailQuotationModeofPaymentLabel.WidthPercentage = 100;
                quotationDetailQuotationModeofPaymentLabel.AddCell(new PdfPCell(new Phrase("III. \t\t\t\t\t\t\t\t Mode of Payment", fontArial11)) { Border = 0, PaddingTop = 20f, PaddingBottom = 2f, HorizontalAlignment = 0 });
                quotationDetailQuotationModeofPaymentLabel.AddCell(new PdfPCell(new Phrase("Payments can be done on the following:", fontArial11)) { Border = 0, PaddingTop = 2f, PaddingBottom = 2f, PaddingLeft = 20f, HorizontalAlignment = 0 });
                quotationDetailQuotationModeofPaymentLabel.AddCell(new PdfPCell(new Phrase("a. \t\t\t\t\t\t\t\t Check Issuance payable to CEBU INNOSOFT SOLUTIONS SERVICES INC. subject to clearing", fontArial11)) { Border = 0, PaddingTop = 2f, PaddingBottom = 2f, PaddingLeft = 30f, HorizontalAlignment = 0 });
                quotationDetailQuotationModeofPaymentLabel.AddCell(new PdfPCell(new Phrase("b. \t\t\t\t\t\t\t\t On-line Deposit via Metrobank Account # or Chinabank Account #", fontArial11)) { Border = 0, PaddingTop = 2f, PaddingBottom = 2f, PaddingLeft = 30f, HorizontalAlignment = 0 });
                document.Add(quotationDetailQuotationModeofPaymentLabel);

                // quotation timeline label and timeline data
                PdfPTable quotationDetailQuotationtimelineLabel = new PdfPTable(1);
                float[] quotationDetailQuotationtimelineLabelWithCells = new float[] { 100f };
                quotationDetailQuotationtimelineLabel.SetWidths(quotationDetailQuotationtimelineLabelWithCells);
                quotationDetailQuotationtimelineLabel.WidthPercentage = 100;
                quotationDetailQuotationtimelineLabel.AddCell(new PdfPCell(new Phrase("IV. \t\t\t\t\t\t\t\t Estimated Project Timeline and Delivery", fontArial11)) { Border = 0, PaddingTop = 20f, PaddingBottom = 10f, HorizontalAlignment = 0 });
                document.Add(quotationDetailQuotationtimelineLabel);
                PdfPTable quotationDetailQuotationtimeline = new PdfPTable(3);
                float[] quotationDetailQuotationtimelineWithCells = new float[] { 35f, 30f, 35f };
                quotationDetailQuotationtimeline.SetWidths(quotationDetailQuotationtimelineWithCells);
                quotationDetailQuotationtimeline.WidthPercentage = 100;
                quotationDetailQuotationtimeline.AddCell(new PdfPCell(new Phrase("Product", fontArial11Bold)) { HorizontalAlignment = 1, PaddingTop = 5f, PaddingBottom = 7f, PaddingLeft = 5f, PaddingRight = 5f });
                quotationDetailQuotationtimeline.AddCell(new PdfPCell(new Phrase("Estimated Timeline", fontArial11Bold)) { HorizontalAlignment = 1, PaddingTop = 5f, PaddingBottom = 7f, PaddingLeft = 5f, PaddingRight = 5f });
                quotationDetailQuotationtimeline.AddCell(new PdfPCell(new Phrase("Remarks", fontArial11Bold)) { HorizontalAlignment = 1, PaddingTop = 5f, PaddingBottom = 7f, PaddingLeft = 5f, PaddingRight = 5f });
                foreach (var timelineList in quotationObjectLists.FirstOrDefault().TimelineLists)
                {
                    quotationDetailQuotationtimeline.AddCell(new PdfPCell(new Phrase(timelineList.Product, fontArial11)) { PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                    quotationDetailQuotationtimeline.AddCell(new PdfPCell(new Phrase(timelineList.Timeline, fontArial11)) { PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                    quotationDetailQuotationtimeline.AddCell(new PdfPCell(new Phrase(timelineList.Remarks, fontArial11)) { PaddingTop = 2f, PaddingBottom = 4f, PaddingLeft = 5f, PaddingRight = 5f });
                }
                document.Add(quotationDetailQuotationtimeline);

                // quotation validity
                PdfPTable quotationDetailQuotationValidityLabel = new PdfPTable(1);
                float[] quotationDetailQuotationValidityLabelWithCells = new float[] { 100f };
                quotationDetailQuotationValidityLabel.SetWidths(quotationDetailQuotationValidityLabelWithCells);
                quotationDetailQuotationValidityLabel.WidthPercentage = 100;
                quotationDetailQuotationValidityLabel.AddCell(new PdfPCell(new Phrase("V. \t\t\t\t\t\t\t\t Quotation Validity", fontArial11)) { Border = 0, PaddingTop = 20f, PaddingBottom = 2f, HorizontalAlignment = 0 });
                quotationDetailQuotationValidityLabel.AddCell(new PdfPCell(new Phrase("a. \t\t\t\t\t\t\t\t This quotation is valid only for one (1) month from the quotation date.", fontArial11)) { Border = 0, PaddingTop = 2f, PaddingBottom = 2f, PaddingLeft = 30f, HorizontalAlignment = 0 });
                document.Add(quotationDetailQuotationValidityLabel);

                // quotation bcs
                PdfPTable quotationDetailQuotationbcsLabel = new PdfPTable(1);
                float[] quotationDetailQuotationbcsLabelWithCells = new float[] { 100f };
                quotationDetailQuotationbcsLabel.SetWidths(quotationDetailQuotationbcsLabelWithCells);
                quotationDetailQuotationbcsLabel.WidthPercentage = 100;
                quotationDetailQuotationbcsLabel.AddCell(new PdfPCell(new Phrase("VI. \t\t\t\t\t\t\t\t Business Continuity Service (BCS)", fontArial11)) { Border = 0, PaddingTop = 20f, PaddingBottom = 2f, HorizontalAlignment = 0 });
                quotationDetailQuotationbcsLabel.AddCell(new PdfPCell(new Phrase("a. \t\t\t\t\t\t\t\t Automatic Ten Percent (10%) Annual Business Continuity Fee after Installation Date. The Fee is computed based on the Software Cost.", fontArial11)) { Border = 0, PaddingTop = 2f, PaddingBottom = 2f, PaddingLeft = 30f, HorizontalAlignment = 0 });
                quotationDetailQuotationbcsLabel.AddCell(new PdfPCell(new Phrase("b. \t\t\t\t\t\t\t\t Free One (1) Year BCS from the start of the Installation Date.", fontArial11)) { Border = 0, PaddingTop = 2f, PaddingBottom = 2f, PaddingLeft = 30f, HorizontalAlignment = 0 });
                document.Add(quotationDetailQuotationbcsLabel);

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