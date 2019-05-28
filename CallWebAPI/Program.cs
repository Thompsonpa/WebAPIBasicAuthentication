using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CallWebAPI
{
    //Description: simple console app to show how to call a .net WEB API with Basic Authentication
    //Auther: Patrick Thompson (https://github.com/Thermalnuke)
    //Date: 5/20/2019

    class Program
    {
        static void Main(string[] args)
        {
            Task t = new Task(HTTP_GET);
            t.Start();
            Console.ReadLine();
        }

        static async void HTTP_GET()
        {
            var TARGETURL = ""; // ... API URL

            HttpClientHandler handler = new HttpClientHandler();

            Console.WriteLine(" ");
            Console.WriteLine("GET: " + TARGETURL);
            Console.WriteLine("  - Basic Authentication (Username and Password)");

            // ... Use HttpClient.            
            HttpClient client = new HttpClient(handler);

            var byteArray = Encoding.ASCII.GetBytes("USERNAME" + ":" + "PASSWORD");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            HttpResponseMessage response = await client.GetAsync(TARGETURL);
            HttpContent content = response.Content;

            // ... Check Status Code 
            Console.WriteLine(" ");
            Console.WriteLine("Response StatusCode: " + (int)response.StatusCode);

            // ... Read the string.
            string result = await content.ReadAsStringAsync();
            dynamic JSON = JsonConvert.DeserializeObject(result);
            Console.WriteLine(" ");
            Console.WriteLine("JSON RESULT: " + JSON.ToString());
            Console.WriteLine(" ");
            Console.WriteLine("Press enter to exit");
            Console.ReadLine();

        }
    }
}
