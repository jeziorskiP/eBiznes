using DataContext.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataContext.Interfaces
{
    public interface IDeliveryNote
    {
        void AddDeliveryNote(DeliveryNote deliveryNote);
        void AddDeliveryNoteLine(DeliveryNoteLine deliveryNoteLine);
        void CreateDeliveryNote(int PackingNoteId, int OrderId, string Note);
        IEnumerable<Order> GetAllOrders();
        Order GetOrder(int DeliveryNoteId);
        void DeleteDeliveryNote();
        IEnumerable<DeliveryNote> GetAll();
        DeliveryNote GetDeliveryNote(int DeliveryNoteId);
        IEnumerable<DeliveryNoteLine> GetDeliveryNoteLines(int DeliveryNoteId);
        void ChangeStatus(int DeliveryNoteId, string Status);
        void FinishOrder(int DeliveryNoteId);
        int NextNumber();
        int GetDeliveryID(int DeliveryNumber);
    }
}
