using DataContext;
using DataContext.Interfaces;
using DataContext.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppService
{
    public class ComplaintService : IComplaint
    {

        private AppDbContext _dbContext;
        public ComplaintService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddComplaint(Complaint complaint)
        {
            _dbContext.Complaints.Add(complaint);
            _dbContext.SaveChanges();
        }

        public void ChangeStatus(int ComplaintId, string Status)
        {
            GetComplaint(ComplaintId).Status = Status;
            _dbContext.SaveChanges();
        }

        public bool CheckOrder(int OrderNumber, string email)
        {
            var client = _dbContext.Clients.FirstOrDefault(c => c.email == email);
            if(client != null || email=="admin")
            {
               // Console.WriteLine("CLIENTID " + client.Id);
                Order obj = _dbContext.Orders.FirstOrDefault(o => o.OrderNumber == OrderNumber);
                Console.WriteLine("ORDER " + obj.OrderNumber);
                if (obj != null)
                    return true;
                else
                    return false;
            }
            else
                return false;

        }

        public void DeleteAddress()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Complaint> GetAll()
        {
            return _dbContext.Complaints
                .Include(o => o.Order);
        }

        public IEnumerable<Complaint> GetAllByOrder(int OrderNumber)
        {
            return GetAll().Where(c => c.OrderNumber == OrderNumber);
        }

        public Complaint GetComplaint(int ComplaintId)
        {
            return GetAll().FirstOrDefault(c => c.Id == ComplaintId);
        }

        public Complaint GetComplaintByOrder(int OrderNumber)
        {
            return GetAll().FirstOrDefault(o => o.OrderNumber == OrderNumber);
        }

        public Order GetOrder(int OrderNumber)
        {
            return _dbContext.Orders.FirstOrDefault(o => o.OrderNumber == OrderNumber);
        }

        public int GetOrderId(int OrderNumber)
        {
            return GetOrder(OrderNumber).Id;
        }

        public void UpdateComplaint(Complaint complaint)
        {
            var complaintOld = GetComplaint(complaint.Id);
            if(complaintOld != null)
            {
                complaintOld.Order = complaint.Order;
                complaintOld.OrderNumber = complaint.OrderNumber;
                complaintOld.Resolution = complaint.Resolution;
                complaintOld.Status = complaint.Status;
                complaintOld.Description = complaint.Description;
                complaintOld.CreateDate = complaint.CreateDate;
                complaintOld.ChangeDate = complaint.ChangeDate;
                _dbContext.SaveChanges();
            }
        }
    }
}
