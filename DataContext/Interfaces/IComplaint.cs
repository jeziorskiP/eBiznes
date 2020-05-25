using DataContext.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataContext.Interfaces
{
    public interface IComplaint
    {
        void AddComplaint(Complaint complaint);
        void DeleteAddress();
        void UpdateComplaint(Complaint complaint);
        IEnumerable<Complaint> GetAll();
        Complaint GetComplaint(int ComplaintId);
        bool CheckOrder(int OrderNumber, string email);
        int GetOrderId(int OrderNumber);
        void ChangeStatus(int ComplaintId, string Status);
        Order GetOrder(int OrderNumber);
        Complaint GetComplaintByOrder(int OrderNumber);
        IEnumerable<Complaint> GetAllByOrder(int OrderNumber);


    }
}
