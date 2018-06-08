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
    public class SubcategoryTest
    {
        [Fact]
        public void GivenModelSubcategoryNameShouldValidate()
        {

            var model = new Subcategory
            {
               SubcategoryName = "Subcategory"
            };

            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(model, context, result, true);


            Assert.True(valid);
        }


        [Fact]
        public void GivenModelSubcategoryWithNullNameShouldNotValidate()
        {

            var model = new Subcategory
            {
                SubcategoryName = ""
            };

            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(model, context, result, true);


            Assert.False(valid);
        }

        [Fact]
        public void GivenModelSubcategoryWithTooLongNameShouldNotValidate()
        {

            var model = new Subcategory
            {
                SubcategoryName = "Testtesttesttesttesttesttesttest"
            };

            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(model, context, result, true);


            Assert.False(valid);
        }

        [Fact]
        public void GivenModelSubcategoryWithTooShortNameShouldNotValidate()
        {

            var model = new Subcategory
            {
                SubcategoryName = "Te"
            };

            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(model, context, result, true);


            Assert.False(valid);
        }
    }
}
