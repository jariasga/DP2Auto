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
        }

        public async void retrieveData()
        {
            user = new Users();
            uri = new Uri(baseAddress, "login");
            try
            {
                var response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject <Users>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }            
        }

        public async void createUserData(Users user, bool isNewUser)
        {
            uri = new Uri(baseAddress, "login");

            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            if (isNewUser)
            {
                response = await client.PostAsync(uri, content);
            }
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine(@"             TodoItem successfully saved.");

            }
        }
    }
}
