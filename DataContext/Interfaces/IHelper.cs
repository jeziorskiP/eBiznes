using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;

namespace DataContext.Interfaces
{
    public interface IHelper
    {
        void WarehouseEmailSender(string email, string title, string content);
        void InvoiceEmailSender(string email, string title, MemoryStream stream);
    
    }
}
