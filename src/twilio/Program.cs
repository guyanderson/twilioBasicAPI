using System;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using twilio.Models;

namespace twilio
{
    class Program
    {// Post req sends a text to "To"
        //        static void Main(string[] args)
        //       {
        //
        //            var client = new RestClient("https://api.twilio.com/2010-04-01");

        //            var request = new RestRequest("Accounts/AC8f1ff03903ea3aaca2b2ec0c2e2e1a11/Messages", Method.POST);

        //            request.AddParameter("To", "+15032601463");
        //            request.AddParameter("From", "+19714074164");
        //            request.AddParameter("Body", "Hello world!");

        //            client.Authenticator = new HttpBasicAuthenticator("AC8f1ff03903ea3aaca2b2ec0c2e2e1a11", "fc91c3dcc8e1bc5faf658339bf7d9fa0");

        //            client.ExecuteAsync(request, response =>
        //            {
        //                Console.WriteLine(response);
        //            });
        //            Console.ReadLine();
        //        }

            //get request pulls all messages (not sure if its sent or received messages, assume sent from user twilio account)
        static void Main(string[] args)
        {
            var client = new RestClient("https://api.twilio.com/2010-04-01");
            //1
            var request = new RestRequest("Accounts/AC8f1ff03903ea3aaca2b2ec0c2e2e1a11/Messages.json", Method.GET);
            client.Authenticator = new HttpBasicAuthenticator("AC8f1ff03903ea3aaca2b2ec0c2e2e1a11", "fc91c3dcc8e1bc5faf658339bf7d9fa0");
            //2
            var response = new RestResponse();
            //3a
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
            //4
            JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
            var messageList = JsonConvert.DeserializeObject<List<Message>>(jsonResponse["messages"].ToString());
            foreach (var message in messageList)
            {
                Console.WriteLine("To: {0}", message.To);
                Console.WriteLine("From: {0}", message.From);
                Console.WriteLine("Body: {0}", message.Body);
                Console.WriteLine("Status: {0}", message.Status);
            }
            Console.ReadLine();
        }

        //3b
        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response =>
            {
                tcs.SetResult(response);
            });
            return tcs.Task;




        }
    }
}