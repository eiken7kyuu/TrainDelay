using AngleSharp.Html.Parser;
using AngleSharp.Dom;
using RestSharp;
using System.Threading.Tasks;

namespace TrainDelay
{
    public class TrainDelay
    {
        private readonly string routeNumber;

        public TrainDelay(string routeNumber)
        {
            this.routeNumber = routeNumber;
        }

        private IRestResponse GetHtml()
        {
            var client = new RestClient("https://transit.yahoo.co.jp/");
            return client.Execute(new RestRequest($"traininfo/detail/{routeNumber}/"));
        }

        private bool IsDelayed(IElement element) =>
            element.GetElementsByClassName("icnNormalLarge").Length == 0;

        public async Task Run()
        {
            var response = GetHtml();
            var parser = new HtmlParser();
            var document = parser.ParseDocument(response.Content);
            var statusElement = document.GetElementById("mdServiceStatus");

            if (!IsDelayed(statusElement))
                return;

            var header = statusElement.QuerySelector("dt").LastChild.TextContent;
            var message = statusElement.QuerySelector("p").TextContent;
            var routeName = document.QuerySelector("h1").TextContent;
            LineNotify.Send($"\r\n【{routeName}】\r\n【{header}】\r\n{message}");
        }
    }
}