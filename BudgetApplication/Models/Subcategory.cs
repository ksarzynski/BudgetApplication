using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetApplication.Models
{
    public class Subcategory
    {
        [Display(Name = "Subcategory name")]
        public int SubcategoryID { get; set; }

        [Display(Name = "Category name")]
        public int CategoryID { get; set; }

        [Display(Name = "Subcategory name")]
        public string SubcategoryName { get; set; }

        public string UserID { get; set; }

        public Category Category { get; set; }

        public ICollection<Item> Items { get; set; }

    }
}
