using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace HtmlCombine
{
    class RegressionReport
    {
        public void GetBodyReport(string[] reportList)
        {
            string htmlText = null;
            List<string> htmlBody = new List<string>();

            //Get HTML body from each regression report
            foreach (var report in reportList)
            {
                using (StreamReader readStream = new StreamReader(report+@"\Report.html"))
                {
                    Console.WriteLine("Report:" +report + @"\Report.html");
                    htmlText = readStream.ReadToEnd();
                }

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(htmlText);

                var body = doc.DocumentNode.SelectSingleNode("(//div[@class='row'])[3]");
                if (body != null)
                {
                    //get only body
                    htmlBody.Add(body.OuterHtml);
                }
            }
            CombineReport(htmlBody, reportList[0]);
        }

        public void CombineReport(List<string> report, string finalFolder)
        {
            string fullReport = null;

            //Get Templete
            string ReportHeader = GetReportPath(@"\ReportTemplete\header.html");
            string ReportFooter = GetReportPath(@"\ReportTemplete\footer.html");

            //Add header
            using (StreamReader readStream = new StreamReader(ReportHeader + @"\ReportTemplete\header.html"))
            {
                fullReport = readStream.ReadToEnd();
            }
            //Add Body
            foreach (var item in report)
            {
                fullReport += item;
            }
            //Add Footer
            using (StreamReader readStream = new StreamReader(ReportFooter + @"\ReportTemplete\footer.html"))
            {
                fullReport += readStream.ReadToEnd();
            }

            //Creat new report
            using (FileStream fs = new FileStream(finalFolder + @"\ReportFinal.html", FileMode.Create))
            {
                using (StreamWriter w = new StreamWriter(fs))
                {
                    w.WriteLine(fullReport);
                }
            }
        }

        public string GetReportPath(string templete)
        {
            string _path = null;
            var file = new FileInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            //var parentDirectory = file.Directory?.Parent;

            //if (parentDirectory != null)
            //{
            //    _path = Path.Combine(parentDirectory.FullName, templete);
            //}

            return file.ToString();
        }
    }
}
