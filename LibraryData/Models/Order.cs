using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibraryData.Models
{
    //public enum Status
    //{
    //    Isporuceno =1,
    //    Prihvaceno = 2,
    //    Odbijeno = 3, 
    //    Obrada = 4
    //}
    public class Order
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public DateTime DateOfMaking { get; set; }
        [Required]
        public DateTime Deadine { get; set; }
        [Required]
        public double SumPrice { get; set; }
        [Required]
        public User User { get; set; }

        public string Status { get; set; }

        public IEnumerable<OrderItem> OrderItems { get; set; } 
    }
}
