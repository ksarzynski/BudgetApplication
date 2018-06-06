using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BudgetApplication.Models;
using BudgetApplication.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

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

        private double GetUserTransactions(string userID, int month)
        {
            double transaction = _context.Transactions
               .Where(n => n.UserID == userID)
               .Where(o => o.TransactionDate.Month == month)
               .Sum(p => p.Price);

            return transaction;
        }

        private double[] GetMonthsResult(string currentLoggedInUser)
        {
            double[] listOfValues = new double[12];
            for (int i=0; i<12; i++)
            {
                listOfValues[i] = GetUserTransactions(currentLoggedInUser, i+1);
            }
            return listOfValues;
        }

        private string GetUserID()
        {
            return this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public JsonResult LineChartData()
        {
            Chart _chart = new Chart();
            _chart.labels = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "Novemeber", "December" };
            _chart.datasets = new List<Datasets>();

            string currentLoggedInUser = GetUserID();
            double[] yearsResult = GetMonthsResult(currentLoggedInUser);

            List<Datasets> _dataSet = new List<Datasets>();
            _dataSet.Add(new Datasets()
            {
                label = "Current Year: "+DateTime.Now.Year,
                data = yearsResult,
                borderColor = new string[] { "#800080" },
                borderWidth = "1"
            });
            _chart.datasets = _dataSet;
            return Json(_chart);
        }
    }
}
