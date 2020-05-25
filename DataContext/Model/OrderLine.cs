using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataContext.Model
{
    public class OrderLine
    {
        public int Id { get; set; }     //PozycjaID
        [Required]
        //public int OrderId { get; set; }
        //public int ProductId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public string Note { get; set; }

        public Order Order { get; set; }  // one -> many
        public Product Product { get; set; }  // one -> many

    }
}
