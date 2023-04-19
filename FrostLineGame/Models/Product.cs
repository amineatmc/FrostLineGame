using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrostLineGames.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string Name { get; set; }
        public int UnitInStock { get; set; }
        public decimal UnitPrice { get; set; }
        public int CategoryID { get; set; }
        public bool IsDeleted { get; set; }
    }
}
