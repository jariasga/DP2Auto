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

        public RestService()
        {
            baseAddress = new Uri("http://dp2.iamallama.com/api/");
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            client.BaseAddress = baseAddress;
        }

        public async Task<string> createUserData(Users user)
        {
            uri = new Uri(baseAddress, "login");

            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync(uri, content);

                var rString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"                      Login successfull !!.");
                    return rString;
                }
            }catch (Exception ex)
            {
                return "connectionProblem";
            }
            return null;
        }
    }
}
