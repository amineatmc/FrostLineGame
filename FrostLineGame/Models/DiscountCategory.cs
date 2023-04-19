using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrostLineGames.Models
{
    public class DiscountCategory
    {
        [Key]
        public int DiscountCategoryID { get; set; }
        public int DiscountID { get; set; }
      //  public Discount Discount { get; set; }
        public int CategoryID { get; set; }
        //public Category Category { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
