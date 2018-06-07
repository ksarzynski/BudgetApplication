using System.Collections.Generic;

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
        public double[] Data { get; set; }
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public string BorderWidth { get; set; }
    }
}
