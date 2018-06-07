using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetApplication.Models
{
    public class Chart
    {
        public string[] Labels { get; set; }
        public List<Datasets> Datasets { get; set; }
    }
    public class Datasets
    {
        public string Label { get; set; }
        public string[] BackgroundColor { get; set; }
        public string[] BorderColor { get; set; }
        public string BorderWidth { get; set; }
        public double[] Data { get; set; }
    }
}
