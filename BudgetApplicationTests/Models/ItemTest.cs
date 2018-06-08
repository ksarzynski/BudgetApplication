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
    public class ItemTest
    {
        [Fact]
        public void GivenModelItemNameShouldValidate()
        {

            var model = new Item
            {
                ItemName = "Item"
            };

            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(model, context, result, true);


            Assert.True(valid);
        }


        [Fact]
        public void GivenModelItemWithNullNameShouldNotValidate()
        {

            var model = new Item
            {
                ItemName = ""
            };

            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(model, context, result, true);


            Assert.False(valid);
        }

        [Fact]
        public void GivenModelItemWithTooLongNameShouldNotValidate()
        {

            var model = new Item
            {
                ItemName = "Testtesttesttesttesttesttesttest"
            };

            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(model, context, result, true);


            Assert.False(valid);
        }

        [Fact]
        public void GivenModelItemWithTooShortNameShouldNotValidate()
        {

            var model = new Item
            {
                ItemName = "Te"
            };

            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(model, context, result, true);


            Assert.False(valid);
        }
    }
}
