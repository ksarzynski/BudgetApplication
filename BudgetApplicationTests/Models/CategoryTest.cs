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
    public class CategoryTest
    {
        [Fact]
        public void GivenModelCategoryNameShouldValidate()
        {

            var model = new Category
            {
                CategoryName = "Category"
            };

            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(model, context, result, true);


            Assert.True(valid);
        }

        [Fact]
        public void GivenModelCategoryWithNullNameShouldNotValidate()
        {

            var model = new Category
            {
                CategoryName = ""
            };

            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(model, context, result, true);


            Assert.False(valid);
        }

        [Fact]
        public void GivenModelCategoryWithTooLongNameShouldNotValidate()
        {

            var model = new Category
            {
                CategoryName = "Testtesttesttesttesttesttesttest"
            };

            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(model, context, result, true);


            Assert.False(valid);
        }

        [Fact]
        public void GivenModelCategoryWithTooShortNameShouldNotValidate()
        {

            var model = new Category
            {
                CategoryName = "Te"
            };

            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(model, context, result, true);


            Assert.False(valid);
        }


    }
}
