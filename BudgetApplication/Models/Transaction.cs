﻿using BudgetApplication.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetApplication.Models
{
    public class Transaction :BaseEntity
    {

        [Display(Name = "Item name")]
        public int ItemID { get; set; }
               
        public string UserID { get; set; }
               
        public double Price { get; set; }
        
        [Display(Name = "Transaction date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd H:mm}", ApplyFormatInEditMode = true)]
        public DateTime TransactionDate { get; set; }

        [Display(Name = "Transaction place")]
        public string TransactionPlace { get; set; }

        [Display(Name = "Exchange rate")]
        public double ExchangeRate { get; set; }

        public Item Item { get; set; }
       
    }
}
