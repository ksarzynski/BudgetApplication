using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetApplication.Models
{
    public class Subcategory
    {
        public int SubcategoryID { get; set; }
        [Display(Name = "Subcategory name")]
        public string SubcategoryName { get; set; }

        public ICollection<Category> Categories { get; set; }

    }
}
