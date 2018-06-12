using System;
using System.Diagnostics;
using System.IO;

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
            var text = string.Empty;
            var solutionPath = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            var tesseractPath = solutionPath + @"\tesseract-master.1671";
            if (!string.IsNullOrEmpty(filename))
            {
                var image = File.ReadAllBytes(filename);
                text = ParseText(tesseractPath, image);
            }

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
                ProcessStartInfo info = new ProcessStartInfo
                {
                    WorkingDirectory = tesseractPath,
                    UseShellExecute = false,
                    FileName = "cmd.exe",
                    Arguments =
                    "/c tesseract.exe " +
                   tempImage + " " +
                    tempOutput +
                    " -l " + string.Join("+", "pol")
                };
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