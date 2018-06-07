using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using BudgetApplication.Controllers;
using BudgetApplication.Repository;
using BudgetApplication.Models;
using System.Threading.Tasks;
using Moq;
using System.Collections.Generic;

namespace BudgetApplicationTests.Controllers
{
    public class CategoriesControllerTest
    {


        [Fact]
        [Trait("Categories Controller", "returns correct")]
        public async Task Model_After_GivingExistingCategoryIdAsync()
        {

            // Arrange
            var item1 = new Category { CategoryName = "Item1" };
            var item2 = new Category { CategoryName = "Item2" };
            var item3 = new Category { CategoryName = "Item3" };
            var expectedName = item2.CategoryName;

            var serviceMock = new Mock<ICategoriesRepository>();
            serviceMock.Setup(x => x.Get(1)).ReturnsAsync(item1);
            serviceMock.Setup(x => x.Get(2)).ReturnsAsync(item2);
            serviceMock.Setup(x => x.Get(3)).ReturnsAsync(item3);

            var controller = new CategoriesController(serviceMock.Object);

            // Act
            var result = await controller.Details(2);

            // Assert
            var viewResult = (ViewResult)result;
            var model = (Category)viewResult.Model;

            Assert.Equal(expectedName, model.CategoryName);
        }

        [Fact]
        public void CreateCategoryWithoutArguments()
        {
            var categoriesRepository = new FakeCategoriesRepository();
            var controller = new CategoriesController(categoriesRepository);
            var result = controller.Create();
            var expected = typeof(ViewResult);

            Assert.IsType(expected, result);
        }

        
        //[Fact]
        //public void Create_ValidUser_ReturnsRedirectToActionResult()
        //{          

        //    var category = new Category();            
        //    var categoriesRepository = new FakeCategoriesRepository();
        //    var controller = new CategoriesController(categoriesRepository);

        //    var result = controller.Create(category);
        //    var expected = typeof(RedirectToActionResult);

        //    Assert.IsType(expected, result);
        //}


    }
}
