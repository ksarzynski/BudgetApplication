 using System;
 using System.Collections.Generic;
 using System.Diagnostics;
 using System.IO;
 using System.Threading.Tasks;

 namespace BudgetApplication.Models
 {
     public class Tesseract
     {
         public string Data { get; private set; }
         public Tesseract(string fileName) { Data = ProcessImage(fileName); }
        
         public string getText()
         {
             return Data;
         }
         
         private string ProcessImage(string filename)
         {
             var solutionPath = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
             var tesseractPath = solutionPath + @"\tesseract-master.1671";
             var image = File.ReadAllBytes(filename);
             string text = ParseText(tesseractPath, image);
             return text;
         }

         private static string ParseText(string tesseractPath, byte[] imageFile)
         {
             string output = string.Empty;
             var tempOutput = Path.GetTempPath() + Guid.NewGuid();
             var tempImage = Path.GetTempFileName();

            try
             {
                 File.WriteAllBytes(tempImage, imageFile);

                 ProcessStartInfo info = new ProcessStartInfo();
                 info.WorkingDirectory = tesseractPath;
                 info.UseShellExecute = false;
                 info.FileName = "cmd.exe";
                 info.Arguments =
                     "/c tesseract.exe " +
                    tempImage + " " +
                     tempOutput +
                     " -l " + string.Join("+", "pol");
                 Process process = Process.Start(info);
                 process.WaitForExit();
                 if (process.ExitCode == 0)
                 {
                    output = File.ReadAllText(tempOutput + ".txt");
                 }
                 else
                 {
                     throw new Exception("Error" + process.ExitCode);
                 }
             }
             finally
             {
                 File.Delete(tempImage);
                 File.Delete(tempOutput + ".txt");
             }
             return output;
         }
     }
 }
