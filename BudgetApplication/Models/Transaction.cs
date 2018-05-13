﻿using System;
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
               
        public double Price { get; set; }
        
        [Display(Name = "Transaction date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd H:mm}", ApplyFormatInEditMode = true)]
        public DateTime TransactionDate { get; set; }

        [Display(Name = "Transaction place")]
        public string TransactionPlace { get; set; }

        public Item Item { get; set; }
       
    }
}
