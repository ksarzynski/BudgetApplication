using BudgetApplication.Extensions;
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
        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Capitalize]
        [StringLength(maximumLength: 30, MinimumLength = 3, ErrorMessage = "Please use name of length in range 3..30")]
        public string SubcategoryName { get; set; }

        public string UserID { get; set; }

        public Category Category { get; set; }

        public virtual ICollection<Item> Items { get; set; }

    }
}
