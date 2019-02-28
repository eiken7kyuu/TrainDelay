using RestSharp;
using System;

namespace TrainDelay
{
    public class LineNotify
    {
        public void Send(string message)
        {
            var client = new RestClient("https://notify-api.line.me/api/notify/");
            var request = new RestRequest();
            request.Method = Method.POST;
            request.AddParameter("message", message);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Authorization", $"Bearer {Environment.GetEnvironmentVariable("LINE_NOTIFY_TOKEN")}");
            client.Execute(request);
        }
    }
}
