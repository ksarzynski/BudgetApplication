using BudgetApplication.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetApplication.Models
{
    public class Item : BaseEntity
    {

        [Display(Name = "Category name")]
        public int CategoryID { get; set; }

        [Display(Name = "Item name")]
        public string ItemName { get; set; }

        public string UserID { get; set; }

        public Category Category { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
        
    }
}
