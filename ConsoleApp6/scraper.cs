
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using AngleSharp.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Windows;

namespace ConsoleApp6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public  class scraper
    {
     
        private string Title { get; set; }
        private string Url { get; set; }
        private string siteUrl = "https://www.oceannetworks.ca/news/stories";
        public string[] QueryTerms { get; } = { "Ocean", "Nature", "Pollution" };


        internal async void ScrapeWebsite()
        {
            CancellationTokenSource cancellationToken = new CancellationTokenSource();
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage request = await httpClient.GetAsync(siteUrl);
            cancellationToken.Token.ThrowIfCancellationRequested();

            Stream response = await request.Content.ReadAsStreamAsync();
            cancellationToken.Token.ThrowIfCancellationRequested();

            HtmlParser parser = new HtmlParser();
            IHtmlDocument document = parser.ParseDocument(response);

            //Add connection between initial scrape, and parsing of results
            GetScrapeResults(document);
        }

        private void GetScrapeResults(IHtmlDocument document)
        {
            IEnumerable<IElement> articleLink = null;

            foreach (var term in QueryTerms)
            {
                articleLink = document.All.Where(x =>
                    x.ClassName == "views-field views-field-nothing" &&
                    (x.ParentElement.InnerHtml.Contains(term) || x.ParentElement.InnerHtml.Contains(term.ToLower()))).Skip(1);

             
            }
        }

     

        private void CleanUpResults(IElement result)
        {
            string htmlResult = result.InnerHtml.ReplaceFirst("        <span class=\"field-content\"><div><a href=\"", @"https://www.oceannetworks.ca");
            htmlResult = htmlResult.ReplaceFirst("\">", "*");
            htmlResult = htmlResult.ReplaceFirst("</a></div>\n<div class=\"article-title-top\">", "-");
            htmlResult = htmlResult.ReplaceFirst("</div>\n<hr></span>  ", "");

            //Seperate the results into our class fields for use in PrintResults()
            SplitResults(htmlResult);
        }

        private void SplitResults(string htmlResult)
        {
            string[] splitResults = htmlResult.Split('*');
            Url = splitResults[0];
            Title = splitResults[1];
        }
    }
}
