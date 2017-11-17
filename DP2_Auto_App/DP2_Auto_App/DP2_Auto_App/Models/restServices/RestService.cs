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
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DP2_Auto_App.Models.RestServices
{
    public class RestService : IRestService
    {
        HttpClient webClient;
        Uri baseAddress, uri;
        public static Client client { get; private set; }
        private string temporalTokenSave;
        public static startTravel currentTravel { get; private set; }
        public static endTravel end { get; private set; }
        static List<Travel> travels;

        public RestService()
        {
            baseAddress = new Uri("http://dp2.iamallama.com/api/");
            webClient = new HttpClient();
            webClient.MaxResponseContentBufferSize = 256000;
            webClient.BaseAddress = baseAddress;

            client = new Client();
            travels = new List<Travel>();
            currentTravel = new startTravel();
        }

        public static Travel getLastTrip()
        {
            return travels.ElementAt(travels.Count-1);
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
                return "connectionProblem: " + ex.Message;
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
                    temporalTokenSave = client.token;
                    client = JsonConvert.DeserializeObject<Client>(rString);
                    client.token = temporalTokenSave;
                    return rString;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return null;
        }

        public async Task<string> updateClientInfo()
        {
            uri = new Uri(baseAddress, "clients/" + client.id);

            webClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.token);

            var json = JsonConvert.SerializeObject(client);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await webClient.PutAsync(uri, content);
                var rString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return rString;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return null;
        }

        public async Task<string> startTravel(string macAddress)
        {
            webClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.token);
            uri = new Uri(baseAddress, "travels");

            clientMAC mac = new clientMAC
            {
                vehicle_mac = macAddress
            };
            //startTravel travel = new startTravel();
            //aqui esta el problema (poner breakpoint
            currentTravel = new startTravel();

            var json = JsonConvert.SerializeObject(mac);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                //no hay response :C
                var response = await webClient.PostAsync(uri, content);

                var rString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    currentTravel = JsonConvert.DeserializeObject<startTravel>(rString);
                    return rString;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return null;
        }

        public async Task<string> endTravel(startTravel start)
        {
            webClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.token);
            uri = new Uri(baseAddress, "travels/" + start.id);

            //endTravel
            end = new endTravel();
            var content = new StringContent("", Encoding.UTF8, "application/json");

            try
            {
                var response = await webClient.PutAsync(uri, content);

                var rString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    end = JsonConvert.DeserializeObject<endTravel>(rString);
                    Travel travel = new Travel
                    {
                        started = start,
                        ended = end
                    };
                    travels.Add(travel);
                    return rString;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return null;
        }

        public async Task<string> storeReadings(int sId, float value)
        {
            webClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.token);
            uri = new Uri(baseAddress, "readings");

            Readings read = new Readings()
            {
                sensor_id = sId,
                value = value
            };

            var json = JsonConvert.SerializeObject(read);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await webClient.PostAsync(uri, content);

                var rString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    read = JsonConvert.DeserializeObject<Readings>(rString);
                    return rString;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return null;
        }

        public async Task<string> getReadingInfo(int readingID)
        {
            webClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.token);

            try
            {
                var response = await webClient.GetAsync("readings/" + readingID);
                var rString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    Readings r = new Readings();
                    r = JsonConvert.DeserializeObject<Readings>(rString);
                    // ToDo: Save readings
                    return rString;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return null;
        }

        public static void logout()
        {
            client = null;
        }

        public async void updateClient(string name, string last, string phone, string email)
        {
            client.name = name;
            client.lastname = last;
            client.phone = phone;
            client.email = email;
            string result = await updateClientInfo();
        }
    }
}
