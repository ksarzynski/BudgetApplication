using Xunit;
using BudgetApplication.Models;
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
                Price = 23.00
            };

            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(model, context, result, true);


            Assert.True(valid);
        }

        [Fact]
        public void GivenModelTransactionPriceWithNotValidPriceShouldNotValidate()
        {

            var model = new Transaction
            {
                Price = 23.001
            };

            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(model, context, result, true);


            Assert.False(valid);
        }

        [Fact]
        public void GivenModelTransactionPriceWithNotValidPriceShouldNotValidate2()
        {

            var model = new Transaction
            {
                Price = -1
            };

            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(model, context, result, true);


            Assert.False(valid);
        }

        [Fact]
        public void GivenValidModelTransactionPlaceShouldValidate()
        {

            var model = new Transaction
            {
               TransactionPlace = "Transaction place"
            };

            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(model, context, result, true);


            Assert.True(valid);
        }

        [Fact]
        public void GivenValidModelWithTooLongTransactionPlaceShouldNotValidate()
        {

            var model = new Transaction
            {
                TransactionPlace = "Transaction place transaction place"
            };

            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(model, context, result, true);


            Assert.False(valid);
        }
    }
}
