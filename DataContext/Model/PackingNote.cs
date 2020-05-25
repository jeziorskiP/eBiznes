using System;
using System.Collections.Generic;
using System.Text;

namespace DataContext.Model
{
    public class PackingNote
    {
        public int Id { get; set; }
        public int PackingNoteNumber { get; set; }

        public string Status { get; set; }
        public DateTime CreationDate { get; set; }
        public int UserId { get; set; }
        public DateTime CompletenessDate { get; set; }
        //public int CompletenessDateUserId { get; set; }



        public IList<PackingNoteLine> PackingNoteLines { get; set; }


    }
}
