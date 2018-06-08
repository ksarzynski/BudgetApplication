using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BudgetApplication.Models;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;

namespace BudgetApplication.Controllers
{

    public class ScannerController : Controller
    {
        private readonly IHostingEnvironment _environment;

        public ScannerController(IHostingEnvironment IHostingEnvironment)
        {
            _environment = IHostingEnvironment;
        }

        [HttpPost]
        public IActionResult Index(string name)
        {
            var newFileName = string.Empty;
            var fileName = string.Empty;

            if (HttpContext.Request.Form.Files != null)
            {
                var files = HttpContext.Request.Form.Files;

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                        var FileExtension = Path.GetExtension(fileName);
                        newFileName = myUniqueFileName + FileExtension;
                        fileName = Path.Combine(_environment.WebRootPath, "scans") + $@"\{newFileName}";
                        using (FileStream fs = System.IO.File.Create(fileName))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }

                    }
                }
            }
            Tesseract tess = new Tesseract(fileName);
            string text = tess.getText();
            string pattern = @"SUMA.*? (\d+,\d+)";
            Regex r = new Regex(pattern);
            Match match = r.Match(text);
            string sum = match.Groups[1].ToString();
            string output = sum.Replace(",",".");
            ViewBag.Sum = output;
            System.IO.File.Delete(fileName);
            return View("~/Views/Transactions/Create.cshtml");

        }
            
    }
}
