using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibraryData.Models
{
    public class OrderItem
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double OrderItemPrice { get; set; }
        public Product Product { get; set; }
    }
}
