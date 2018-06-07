﻿using System;
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
    [Authorize(Roles = "Administrator, User")]
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
                //   var fileName = string.Empty;
                string PathDB = string.Empty;

                var files = HttpContext.Request.Form.Files;

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        //Getting FileName
                        fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                        //Assigning Unique Filename (Guid)
                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                        //Getting file Extension
                        var FileExtension = Path.GetExtension(fileName);

                        // concating  FileName + FileExtension
                        newFileName = myUniqueFileName + FileExtension;

                        // Combines two strings into a path.
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
            Regex word = new Regex(@"(SUMA|SUMA PLN|Suma PLN).*?([0-9,]+)");
            Match m = word.Match(text);
            string sum = m.Value;
            ViewBag.Sum = sum;
            ViewBag.Message = text;
            System.IO.File.Delete(fileName);
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
     
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
