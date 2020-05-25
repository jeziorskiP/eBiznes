using System;
using System.Collections.Generic;
using System.Text;

namespace DataContext.Model
{
    public class Complaint
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public string Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ChangeDate { get; set; }
        //public int UserId { get; set; }
        public string Description { get; set; }
        public string Resolution { get; set; }


        public Order Order { get; set; }  // one -> many


    }
}
