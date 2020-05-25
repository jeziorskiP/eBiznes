using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Models.Invoice;

using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

namespace WebApplication3.Models.Helper
{
    public static class InvoicePdfTemplete
    {
      
        public static string DO(InvoiceIndexListingModel model)
        {

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[7] {
                            new DataColumn("L.P.", typeof(string)),
                            new DataColumn("Przedmiot", typeof(string)),
                            new DataColumn("Ilosc", typeof(string)),
                            new DataColumn("Cena brutto", typeof(string)),
                            new DataColumn("Cena netto", typeof(string)),
                            new DataColumn("Vat", typeof(string)),
                            new DataColumn("Razem brutto", typeof(string))});


            for (int i = 0; i < model.Lines.InvoiceLines.Count(); i++)
            {
                var item = model.Lines.InvoiceLines.ToList()[i];
                int lp = i + 1;
                dt.Rows.Add(lp,item.ProductName.ToString(),item.Amount.ToString() ,item.Price.ToString() , item.PriceNetto.ToString(), item.Tax.ToString(), item.TotalPrice.ToString());
            }



            using (StringWriter sw = new StringWriter())
            {
                //using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    StringBuilder sb = new StringBuilder();

                    //Generate Invoice (Bill) Header.
                    sb.Append("<table width='100%' cellspacing='5' cellpadding='2'>");
                    sb.Append("<tr><td align='center' style='background-color: #18B5F0' colspan = '2'><b>Faktura VAT " + model.InvoiceNumber +"</b></td></tr>");
                    sb.Append("<tr><td colspan = '2'></td></tr>");
                    sb.Append("<tr><td><b>Zamówienie nr: </b>");
                    sb.Append(model.OrderNumber);
                    sb.Append("</td><td align = 'right'><b>Data faktury: </b>");
                    sb.Append(model.InvoiceTime.ToString("dd/MM/yyyy"));
                    sb.Append("<b></b>");
                    sb.Append("<p>Data wygenerowanie: "+ DateTime.Now.ToString("dd/MM/yyyy")+" </p>");

                    sb.Append(" </td></tr>");
                    sb.Append("<tr><td colspan = '3'><b>Sprzedawca: </b>");
                    sb.Append("Baloon");
                    sb.Append("<p>K.Kłys, O.Kalinowska, P.Jeziorski</p>");
                    sb.Append("<p>ul. Nowa 23</p>");
                    sb.Append("<p>95-258 Lodz</p>");
                    sb.Append("</td></tr>");

                    sb.Append("<tr><td colspan = '2' align = 'right'><b>Kupujacy: </b>");
                    sb.Append("df");
                    sb.Append("<p>"+model.ClientName+" " +model.ClientSurname+"</p>");
                    sb.Append("<p>" +model.AddressStreet+" "+model.AddressStreetNumber+" " + model.AddressAppartmentNumber+"</p>");
                    sb.Append("<p>"+ model.AddressPostCode+" " + model.AddressCity +"</p>");
                    sb.Append("<p>"+ model.AddressCountry +"</p>");
                    sb.Append("</td></tr>");



                    sb.Append("</table>");
                    sb.Append("<br />");

                    //Generate Invoice (Bill) Items Grid.
                    sb.Append("<table border = '1'>");
                    sb.Append("<tr >");
                    sb.Append("<span style=\" background-color: yellow  \">");

                    foreach (DataColumn column in dt.Columns)
                    {
                        Console.WriteLine("JESTEM: " + column.ColumnName);
                        sb.Append("<th bgcolor=\"#d9d9d9\" >");
                       // sb.Append("<span style=\"color: red;background-color: yellow;font-weight:bold;\">");
                        sb.Append(column.ColumnName.ToString());
                       // sb.Append("</span>");
                        sb.Append("</th>");
                    }

                    sb.Append("</span>");
                    sb.Append("</tr>");
                    foreach (DataRow row in dt.Rows)
                    {
                        sb.Append("<tr>");
                        foreach (DataColumn column in dt.Columns)
                        {
                            sb.Append("<td>");
                            sb.Append(row[column]);
                            sb.Append("</td>");
                        }
                        sb.Append("</tr>");
                    }
                    sb.Append("<tr><td align = 'right' colspan = '4");
                   // sb.Append(dt.Columns.Count - 3);
                    sb.Append("'>Total</td>");
                    sb.Append("<td colspan = '1'>");
                    sb.Append(model.TotalPriceNetto);
                    sb.Append("</td>");
                    sb.Append("<td colspan = '1'>");
                    sb.Append(model.TotalTax);
                    sb.Append("</td>");
                    sb.Append("<td colspan = '1'>");
                    sb.Append(model.TotalPriceBrutto);
                    sb.Append("</td>");
                    sb.Append("</tr></table>");
                    return sb.ToString();
                    /*
                    
                    //Export HTML String as PDF.
                    StringReader sr = new StringReader(sb.ToString());
                    MemoryStream memoryStream = new MemoryStream();
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();
                    /*


                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=Invoice_" + "125" + ".pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                    */
                }
            }



            






        }
            
    }   
}
