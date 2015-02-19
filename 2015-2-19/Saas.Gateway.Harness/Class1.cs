using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ProductStoreClient
{
    class Program
    {
        static void Main()
        {
            RunAsync().Wait();
            Console.Read();
        }
          
        static async Task RunAsync()
        {
            for (int i = 0; i <= 1000; i++)
            {
                using (var client = new HttpClient())
                {

                    // New code:
                    client.BaseAddress = new Uri("http://localhost"); // or xxx.cloudapp.net

                    int n = (new Random()).Next(1, 1000) % 2;
                    string sHeader = "t" + ((n != 0) ? "1" : "2");
                    client.DefaultRequestHeaders.Add("xHEADER", sHeader);
                    
                    
                    HttpResponseMessage response = await client.GetAsync("saasapp");
                    if (response.IsSuccessStatusCode)
                    {
                        string sResponse = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("{0}\t${1}", sHeader, sResponse);
                    }
                } 
                
            }
        }
    }
}