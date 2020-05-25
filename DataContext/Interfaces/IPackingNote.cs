using DataContext.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataContext.Interfaces
{
    public interface IPackingNote
    {
        void AddPackingNote(PackingNote packingNote);
        void AddPackingNoteLine(PackingNoteLine packingNoteLine);
        void CreatePackingNotes(IEnumerable<OrderLine> orderLines);
        void DeletePackingNote();
        IEnumerable<PackingNote> GetAll();
        PackingNote GetPackingNote(int PackingNoteId);
        void ChangeStatus(int PackingNoteId, string Status);
        void ChangeOrderStatusByPN(int PackingNoteId, string Status);
        void ChangeStatus(int OrderId, int PackingNoteId, string Status);
        void ChangeOrderStatus(int OrderId, string Status);
        int NextNumber();
        IEnumerable<PackingNoteLine> GetAllLines(int PackingNoteId);
        bool CheckPackingNote(int OrderId);
        void CheckPackingNote(IEnumerable<OrderLine> orderLines);
        string PrepareList(int PackingNoteId);
        void CorrectPackingNoteStatus(int OrderId);

    }
}
