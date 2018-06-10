using BudgetApplication.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BudgetApplication.Models
{
    public class Category
    {   
        [Key]
        public int CategoryID { get; set; }
       
        [Display(Name = "Category name")]
        [Capitalize]
        [StringLength(maximumLength: 30, MinimumLength = 3, ErrorMessage = "Please use name of length in range 3..30")]
        public string CategoryName { get; set; }

        public string UserID { get; set; }

        public ICollection<Subcategory> Subcategories { get; set; }

    }
}