using RestSharp;
using System.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

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

            request.AddParameter("pagesize", "1");
            request.AddParameter("order", "desc");
            request.AddParameter("sort", "votes");
            request.AddParameter("site", "stackoverflow");
            request.AddParameter("filter", "withbody");
            request.AddParameter("accepted", "True");
            request.AddParameter("title", soSearch);

            var response = JObject.Parse(client.Execute(request).Content);

            answer.Title = response["items"][0]["title"].ToString();
            answer.Body = response["items"][0]["body"].ToString();

            var answerId = (int)response["items"][0]["accepted_answer_id"];

            var answerRequest = new RestRequest("2.2/answers/" + answerId);

            answerRequest.AddParameter("pagesize", "1");
            answerRequest.AddParameter("order", "desc");
            answerRequest.AddParameter("sort", "votes");
            answerRequest.AddParameter("site", "stackoverflow");
            answerRequest.AddParameter("filter", "withbody");

            var answerResponse = JObject.Parse(client.Execute(answerRequest).Content);

            answer.AcceptedAnswer = answerResponse["items"][0]["body"].ToString();

            Console.WriteLine(answer.FormatAnswer());
        }
    }
}
