using DataContext.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataContext.Interfaces
{
    public interface IInvoice
    {
        void AddInvoice(Invoice invoice);
        void AddInvoiceLine(InvoiceLine invoiceLine );
        void UpdateInvoice(Invoice invoice);
        void UpdateInvoiceLine(InvoiceLine invoiceLine, int InvoiceLineId);
        InvoiceLine GetInvoiceLine(int InvoiceLineId);
        void PostInvoice(int InvoiceId);
        int PrepareInvoice(int OrderId);

        IEnumerable<Invoice> GetAll();
        Invoice GetInvoice(int InvoiceId);
        int NextInvoiceNumber();
        decimal CalculateTotalPrice(IEnumerable<OrderLine> orderLines);
        decimal CalculateTotalPrice(int InvoiceId);
        void UpdateTotalPrice(int InvoiceId, decimal totalPrice);
        Invoice GetInvoiceByOrderId(int OrderId);
        IEnumerable<InvoiceLine> GetInvoiceLines(int InvoiceId);
        void SaveToPdf(int InvoiceId);
        bool CheckInvoice(int OrderNumber, string email);
        int GetOrderIdByInvoiceNumber(int InvoiceNumber);

    }
}
