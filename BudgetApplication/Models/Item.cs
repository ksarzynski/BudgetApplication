using BudgetApplication.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetApplication.Models
{
    public class Item
    {
        [Key]
        public int ItemID { get; set; }

        [Display(Name = "Subcategory name")]
        public int SubcategoryID { get; set; }

        [Display(Name = "Item name")]
        public string ItemName { get; set; }

        public string UserID { get; set; }

        public Subcategory Subcategory { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
        
    }
}
