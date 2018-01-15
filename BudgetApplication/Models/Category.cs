using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetApplication.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        [Display(Name = "Subcategory name")]
        public int SubcategoryID { get; set; }
        [Display(Name = "Category name")]
        public string CategoryName { get; set; }

        public Subcategory Subcategory { get; set; }

        public ICollection<Item> Items { get; set; }

    }
}
