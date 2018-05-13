using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BudgetApplication.Models
{
    public class Category
    {   
        [Key]
        public int CategoryID { get; set; }
       
        [Display(Name = "Category name")]
        public string CategoryName { get; set; }

        public string UserID { get; set; }

        public ICollection<Subcategory> Subcategories { get; set; }

    }
}