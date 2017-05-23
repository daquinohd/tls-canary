using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TLSCanary
{
    class CanaryTest
    {

        private const String URL = "https://clinicaltrialsapi.cancer.gov";

        static void Main(string[] args)
        {
            Console.WriteLine("Site status:");

            HttpWebResponse resp = GetResponse(URL);

            Console.WriteLine("Status code: " + (int)resp.StatusCode + " " + resp.StatusDescription);
            Console.WriteLine("Content length: " + resp.ContentLength);
            Console.WriteLine("Content type: " + resp.ContentType);
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }


        private static HttpWebResponse GetResponse(string url)
        {

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "HEAD";
            var response = (HttpWebResponse)request.GetResponse();

            // status code...
            return response;
        }

    }
}
