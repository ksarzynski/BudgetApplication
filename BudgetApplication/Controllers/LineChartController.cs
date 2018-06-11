using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BudgetApplication.Models;
using BudgetApplication.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace BudgetApplication.Controllers
{
    [Authorize(Roles = "User")]
    public class LineChartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LineChartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        private double getSumOfPricesForGivenMonthForUser(string userID, int month)
        {
            double sumForMonth = _context.Transactions
                                .Where(transaction => transaction.UserID == userID)
                                .Where(transaction => transaction.TransactionDate.Month == month)
                                .Sum(transaction => transaction.Price);

            return sumForMonth;
        }

        private double[] GetYearlySumForUser(string currentLoggedInUser)
        {
            double[] sumForYearResult = new double[12];

            for (int i=0; i<12; i++)
            {
                sumForYearResult[i] = getSumOfPricesForGivenMonthForUser(currentLoggedInUser, i+1);
            }
            return sumForYearResult;
        }

        private string GetUserID()
        {
            return this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public JsonResult LineChartData()
        {
            Chart _chart = new Chart
            {
                Labels = new string[] { "January", "February", "March", "April", "May", "June", "July",
                                        "August", "September", "October", "Novemeber", "December" },
                Datasets = new List<Datasets>()
            };

            string currentLoggedInUser = GetUserID();
            double[] yearsResult = GetYearlySumForUser(currentLoggedInUser);

            List<Datasets> _dataSet = new List<Datasets>
            {
                new Datasets()
                {
                    Label = $"Current Year: {DateTime.Now.Year}",
                    Data = yearsResult,
                    BorderColor = "#800080",
                    BorderWidth = "1"
                }
            };
            _chart.Datasets = _dataSet;
            return Json(_chart);
        }
    }
}
