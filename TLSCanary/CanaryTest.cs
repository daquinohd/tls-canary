using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace TLSCanary
{
    class CanaryTest
    {
        static void Main(string[] args)
        {
            // Get the HTTP response object for the URL
            string url = ConfigurationManager.AppSettings["UrlToCheck"].ToString();
            HttpWebResponse response = GetResponse(url);

            // Get the temp path and filename
            string tempFilePath = Path.GetTempPath() + ConfigurationManager.AppSettings["TempFile"].ToString();

            // Write to a temp file and display file's contents 
            WriteStream(response, tempFilePath, url);
            ReadStream(tempFilePath);

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        /// <summary>Get HTTP the repsonse for a given URL.</summary>
        /// <param name="url"></param>
        /// <returns>HTTP response object</returns>
        private static HttpWebResponse GetResponse(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "HEAD";

            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                return response;
            }
            catch (WebException ew)
            {
                // Handle 4xx and 5xx statuses
                return (HttpWebResponse)ew.Response;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>Write HTTP response info to a file.</summary>
        /// <param name="resp"></param>
        /// <param name="filePath"></param>
        /// <param name="url"></param>
        private static void WriteStream(HttpWebResponse resp, string filePath, string url)
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                if (resp != null)
                {
                    sw.WriteLine("=== Site status for " + url + " ===");
                    sw.WriteLine("Status code: " + (int)resp.StatusCode + " " + resp.StatusDescription);
                    sw.WriteLine("Content length: " + resp.ContentLength);
                    sw.WriteLine("Content type: " + resp.ContentType);
                }
                else
                {
                    sw.WriteLine("Please verify that the url '" + url + "' is correct.");
                }
            }
        } 

        /// <summary>Read and show each line from a file.</summary>
        /// <param name="dir"></param>
        /// <param name="file"></param>
        private static void ReadStream(string filePath)
        {
            string line = "";
            using (StreamReader sr = new StreamReader(filePath))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        } // End ReadStream()

    } // End CanaryTest() class
}
