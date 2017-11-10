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
        HttpClient webClient;
        Uri baseAddress, uri;
        public static Client client { get; private set; }

        public RestService()
        {
            baseAddress = new Uri("http://dp2.iamallama.com/api/");
            webClient = new HttpClient();
            webClient.MaxResponseContentBufferSize = 256000;
            webClient.BaseAddress = baseAddress;

            client = new Client();
        }

        public async Task<string> getLoginToken(Users user)
        {
            uri = new Uri(baseAddress, "login");
            client = new Client();
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            try
            {
                var response = await webClient.PostAsync(uri, content);

                var rString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    client = JsonConvert.DeserializeObject<Client>(rString);
                    return "loginSuccess";
                }
            }catch (Exception ex)
            {
                return "connectionProblem";
            }
            return null;
        }

        public async Task<string> getClientInfo()
        {   
            webClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.token);

            try
            {
                var response = await webClient.GetAsync("clients/" + client.id);
                var rString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    client = JsonConvert.DeserializeObject<Client>(rString);
                    return rString;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return null;
        }
    }
}
