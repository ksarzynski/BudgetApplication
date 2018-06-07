using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using BudgetApplication.Controllers;
using BudgetApplication.Repository;
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
                CategoryName = "Test"
            };

            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(model, context, result, true);


            Assert.True(valid);
        }


    }
}
