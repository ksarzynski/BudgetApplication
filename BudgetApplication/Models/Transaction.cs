using System;
using System.ComponentModel.DataAnnotations;

namespace BudgetApplication.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionID { get; set; }
        
        [Display(Name = "Item name")]
        public int ItemID { get; set; }
               
        public string UserID { get; set; }

        [RegularExpression(@"[0-9]?[0-9]*(\.[0-9][0-9]?)?", ErrorMessage = "Please enter valid price format")]
        public double Price { get; set; }
        
        [Display(Name = "Transaction date")]
        // [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime TransactionDate { get; set; }

        [Display(Name = "Transaction place")]
        [StringLength(maximumLength: 30, ErrorMessage = "Please use maximum 30 characters")]
        public string TransactionPlace { get; set; }

        public Item Item { get; set; }
       
    }
}
