using DP2_Auto_App.Models.RestServices;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;

namespace DP2_Auto_App.Models.RestServices
{
    public class RestService : IRestService
    {
        HttpClient client;
        Uri baseAddress, uri;
        private Users user;

        public RestService()
        {
            baseAddress = new Uri("http://dp2.iamallama.com/api/");
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            client.BaseAddress = baseAddress;
        }

        public async Task<string> createUserData(Users user, bool isNewUser)
        {
            uri = new Uri(baseAddress, "login");

            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            HttpResponseMessage response = null;
            if (isNewUser)
            {
                response = await client.PostAsync(uri, content);
                return response.ToString();
            }
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine(@"             TodoItem successfully saved.");
                return "Exception";
            }
            return "null";
        }
    }
}
