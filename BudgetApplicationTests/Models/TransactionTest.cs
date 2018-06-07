using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using BudgetApplication.Models;
using System.Threading.Tasks;
using Moq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BudgetApplicationTests.Models
{
    public class TransactionTest
    {
        [Fact]
        public void GivenModelTransactionPriceShouldValidate()
        {

            var model = new Transaction
            {
                Price = 23
            };

            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(model, context, result, true);


            Assert.True(valid);
        }
    }
}
