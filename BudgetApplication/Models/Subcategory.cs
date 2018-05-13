using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BudgetApplication.Models
{
    public class Subcategory
    {
        [Key]
        public int SubcategoryID { get; set; }

        [Display(Name = "Category name")]
        public int CategoryID { get; set; }

        [Display(Name = "Subcategory name")]
        public string SubcategoryName { get; set; }

        public string UserID { get; set; }

        public Category Category { get; set; }

        public virtual ICollection<Item> Items { get; set; }

    }
}
