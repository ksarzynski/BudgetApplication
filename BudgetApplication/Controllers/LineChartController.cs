using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BudgetApplication.Models;
using BudgetApplication.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace BudgetApplication.Controllers
{
    [Authorize(Roles = "Administrator, User")]
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

        private double GetUserTransactionsAsync(string userID, int month)
        {
            double transaction = _context.Transactions
                                .Where(x => x.UserID == userID)
                                .Where(x => x.TransactionDate.Month == month)
                                .Sum(x => x.Price);

            return transaction;
        }

        private double[] GetMonthsResult(string currentLoggedInUser)
        {
            double[] listOfValues = new double[12];

            for (int i=0; i<12; i++)
            {
                listOfValues[i] = GetUserTransactionsAsync(currentLoggedInUser, i+1);
            }
            return listOfValues;
        }

        private string GetUserID()
        {
            return this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public JsonResult LineChartData()
        {
            Chart _chart = new Chart
            {
                Labels = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "Novemeber", "December" },
                Datasets = new List<Datasets>()
            };

            string currentLoggedInUser = GetUserID();
            double[] yearsResult = GetMonthsResult(currentLoggedInUser);

            List<Datasets> _dataSet = new List<Datasets>();
            _dataSet.Add(new Datasets()
            {
                Label = $"Current Year: {DateTime.Now.Year}",
                Data = yearsResult,
                BorderColor = new string[] { "#800080" },
                BorderWidth = "1"
            });
            _chart.Datasets = _dataSet;
            return Json(_chart);
        }
    }
}
