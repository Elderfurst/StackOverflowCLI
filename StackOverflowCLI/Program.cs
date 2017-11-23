using RestSharp;
using System.Configuration;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using Colorful;
using System.Drawing;
using System.Web;

namespace StackOverflowCLI
{
    public class Program
    {
        private static string apiKey = ConfigurationManager.AppSettings["APIKey"];

        public static void Main(string[] args)
        {
            var answer = new Question();

            var soSearch = string.Join(" ", args);

            var client = new RestClient("https://api.stackexchange.com");
            var request = new RestRequest("2.2/search/advanced");
            
            //Get the single most voted on question that has an accepted answer
            request.AddParameter("pagesize", "1");
            request.AddParameter("order", "desc");
            request.AddParameter("sort", "relevance");
            request.AddParameter("site", "stackoverflow");
            request.AddParameter("filter", "withbody");
            request.AddParameter("accepted", "True");
            request.AddParameter("q", soSearch);

            var response = JObject.Parse(client.Execute(request).Content);

            if(!response["items"].HasValues)
            {
                Console.WriteLine("soflow: No questions match your criteria", Color.White);
                return;
            }

            answer.Title = HttpUtility.HtmlDecode(RemoveHtml(response["items"][0]["title"].ToString()));
            answer.Body = HttpUtility.HtmlDecode(RemoveHtml(response["items"][0]["body"].ToString()));

            var answerId = (int)response["items"][0]["accepted_answer_id"];

            var answerRequest = new RestRequest("2.2/answers/" + answerId);

            //Get the accepted answer for the previously retrieved question
            answerRequest.AddParameter("pagesize", "1");
            answerRequest.AddParameter("order", "desc");
            answerRequest.AddParameter("sort", "votes");
            answerRequest.AddParameter("site", "stackoverflow");
            answerRequest.AddParameter("filter", "withbody");

            var answerResponse = JObject.Parse(client.Execute(answerRequest).Content);

            answer.AcceptedAnswer = HttpUtility.HtmlDecode(RemoveHtml(answerResponse["items"][0]["body"].ToString()));

            answer.Print();
        }

        private static string RemoveHtml(string raw)
        {
            return Regex.Replace(raw, @"<[^>]+>|&nbsp;", "").Trim();
        }
    }
}
