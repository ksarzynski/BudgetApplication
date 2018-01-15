using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetApplication.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        [Display(Name = "Item name")]
        public int ItemID { get; set; }
        public double Price { get; set; }
        [Display(Name = "Transaction date")]
        public DateTime TransactionDate { get; set; }
        [Display(Name = "Transaction place")]
        public string TransactionPlace { get; set; }
        [Display(Name = "Exchange rate")]
        public double ExchangeRate { get; set; }

        public Item Item { get; set; }
       
    }
}
