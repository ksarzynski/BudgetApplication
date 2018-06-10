using BudgetApplication.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BudgetApplication.Models
{
    public class Item
    {
        [Key]
        public int ItemID { get; set; }

        [Display(Name = "Subcategory name")]
        public int SubcategoryID { get; set; }

        [Display(Name = "Item name")]
        [Capitalize]
        [StringLength(maximumLength: 30, MinimumLength = 3, ErrorMessage = "Please use name of length in range 3..30")]
        public string ItemName { get; set; }

        public string UserID { get; set; }

        public Subcategory Subcategory { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
        
    }
}
