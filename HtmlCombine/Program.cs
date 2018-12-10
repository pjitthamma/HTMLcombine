using System;

namespace HtmlCombine
{
    class Program
    {
        static void Main(string[] userInput)
        {
            RegressionReport init = new RegressionReport();
            string report = null;

            for (int i =0; i < userInput.Length; i++)
            {
                 report += userInput[i]+ " ";
            }
            string[] splitReport = report.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            init.GetBodyReport(splitReport);
        }
    }
}
