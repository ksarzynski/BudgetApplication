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
             List<string> imageToText = new List<string>();
             var solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
             var tesseractPath = solutionDirectory + @"\tesseract-master.1671";
             var image = File.ReadAllBytes(filename);
             string text = ParseText(tesseractPath, image);
             return text;
         }

         private static string ParseText(string tesseractPath, byte[] imageFile)
         {
             string output = string.Empty;
             var tempOutputFile = Path.GetTempPath() + Guid.NewGuid();
             var tempImageFile = Path.GetTempFileName();

            try
             {
                 File.WriteAllBytes(tempImageFile, imageFile);

            ProcessStartInfo info = new ProcessStartInfo();
                 info.WorkingDirectory = tesseractPath;
                 info.WindowStyle = ProcessWindowStyle.Hidden;
                 info.UseShellExecute = false;
                 info.FileName = "cmd.exe";
                 info.Arguments =
                     "/c tesseract.exe " +
                    tempImageFile + " " +
                     tempOutputFile +
                     " -l " + string.Join("+", "pol");
                 Process process = Process.Start(info);
                 process.WaitForExit();
                 if (process.ExitCode == 0)
                 {
                    output = File.ReadAllText(tempOutputFile + ".txt");
                 }
                 else
                 {
                     throw new Exception("Error" + process.ExitCode);
                 }
             }
             finally
             {
                 File.Delete(tempImageFile);
                 File.Delete(tempOutputFile + ".txt");
             }
             return output;
         }
     }
 }

