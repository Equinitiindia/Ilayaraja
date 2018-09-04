using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Configuration;
using System.Net.Http.Headers;

namespace EquinitiSampleProject
{
    public class Apiclient : HttpClient
    {
        public Apiclient()
        {
            string baseUri = ConfigurationManager.AppSettings["apiUri"];
           
            this.BaseAddress = new Uri(baseUri);
            this.DefaultRequestHeaders.Clear();
            this.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // To do .,Here we need add Bearer Token at the headed for security purpose
            // But i forgot the syntax.  I have worked this part.
        }
    }
}