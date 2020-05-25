using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models.PackingNotes
{
    public class PackingNoteIndexListingModel
    {
        public int PackingNoteId { get; set; }
        public int PackingNoteNumber { get; set; }
        public string Status { get; set; }
        public DateTime CreationDate { get; set; }
        public int UserId { get; set; }
        public DateTime CompletenessDate { get; set; }
    }
}
