using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models.PackingNotes
{
    public class PNDetailsListingModel 
    {
        public IEnumerable<PNDetailsIndexListingModel>  PackingNoteDetails { get; set; }
    }
}
