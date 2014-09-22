using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApplication3
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var i = 0;
            while (true)
            {
                i++;
                var handler = new HttpClientHandler();


                handler.CookieContainer.Add(new Cookie("iHeartDevice", "desktop", "/", "www.967kissfm.com"));
                handler.CookieContainer.Add(new Cookie("__qca", "P0-565734645-1381794680804", "/", "www.967kissfm.com"));
                handler.CookieContainer.Add(new Cookie("s_cc", "true", "/", "www.967kissfm.com"));
                handler.CookieContainer.Add(new Cookie("SC_LINK", "%5B%5BB%5D%5D", "/", "www.967kissfm.com"));
                handler.CookieContainer.Add(new Cookie("s_sq", "%5B%5BB%5D%5D", "/", "www.967kissfm.com"));
                handler.CookieContainer.Add(new Cookie("__atuvc", "4%7C42", "/", "www.967kissfm.com"));
                handler.CookieContainer.Add(new Cookie("TGCACHEKEY", "bypass", "/", "www.967kissfm.com"));


                using (var client = new HttpClient(handler))
                {
                // 
                    client.BaseAddress = new Uri("http://content.clearchannel.com");
                    client.DefaultRequestHeaders.Add(
                        "Referer",
                        "http://www.967kissfm.com/pages/hs-invasion-2014.html");

                    client.DefaultRequestHeaders.Add(
                        "User-Agent",
                        "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/30.0.1599.69 Safari/537.36"
                        );
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                    try
                    {
                        var result = client.GetStringAsync(
                            "/cc-common/polling_tool/insertPoll.php?poll_id=245072&redirect=/pages/hs-invasion-2014.html&optionID=1032379&callback=jQuery110205422429461032152_1411392898505")
                            ;
                        while (!result.IsCompleted)
                        {
                            Thread.Sleep(1000);
                        }
                        var poll = Newtonsoft.Json.JsonConvert.DeserializeObject<Rootobject>(result.Result.Replace("jQuery110205422429461032152_1411392898505(","").Replace(");",""));
                        var votes = poll.pollOptions.options.OrderByDescending(o => o.votecount).Take(5);
                        foreach (var option in votes)
                        {
                            Console.WriteLine(option.votecount + " " + option.desc + " " + option.position);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    Console.WriteLine(i);
                    Thread.Sleep(300 * 1000);//5 mins
                }
            }
        }
    }
}

public class Rootobject
{
    public Polldata pollData { get; set; }
    public Polloptions pollOptions { get; set; }
}

public class Polldata
{
    public string poll_id { get; set; }
    public string title { get; set; }
    public string site_id { get; set; }
    public string optioncount { get; set; }
    public string align { get; set; }
    public string display_style { get; set; }
    public string graph { get; set; }
    public string table_style { get; set; }
    public string table_columns { get; set; }
    public string validation { get; set; }
    public string login_require { get; set; }
    public string email_require { get; set; }
    public string startdate { get; set; }
    public string enddate { get; set; }
    public string enddate_action { get; set; }
    public string button_name { get; set; }
    public string hidetitle { get; set; }
    public string unlimitedvoting { get; set; }
}

public class Polloptions
{
    public string votetotal { get; set; }
    public Option[] options { get; set; }
}

public class Option
{
    public string desc { get; set; }
    public string image { get; set; }
    public string audio { get; set; }
    public string position { get; set; }
    public string option_id { get; set; }
    public int votecount { get; set; }
}