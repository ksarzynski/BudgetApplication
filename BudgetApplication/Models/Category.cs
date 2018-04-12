using BudgetApplication.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetApplication.Models
{
    public class Category : BaseEntity
    {
        [Display(Name = "Category name")]
        public string CategoryName { get; set; }

        public string UserID { get; set; }

        public ICollection<Subcategory> Subcategories { get; set; }

    }
}