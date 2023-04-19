using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrostLineGames.Models
{
    public class Discount
    {
        [Key]
        public int DiscountID { get; set; }
        public string Name { get; set; }
        public int DiscountRate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate  { get; set; }
    }
}
