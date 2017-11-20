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
        public static List<Objective> objectives;
        public static List<Reminder> reminders;
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

        public static int CountTravels()
        {
            return travels.Count();
        }

        public static Travel getNTravel(int id)
        {
            Travel aux = new Travel();
            for(int i = 0; i < travels.Count(); i++)
            {
                aux = travels.ElementAt(i);
                if(aux.started.id == id)
                    return aux; 
            }

            return null;
        }

        public static Travel getTravelAt(int i)
        {
            return travels.ElementAt(i);
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
            return "Unauthorized";
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
            currentTravel = new startTravel();
            var json = JsonConvert.SerializeObject(mac);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
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

        public async Task<string> storeReadings(int sId, double value)
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

        public async Task<Readings> getReadingInfo(int readingID)
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
                    return r;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
            return null;
        }

        public async Task<List<Readings>> getReadingList(int readingID)
        {
            webClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.token);
            List<Readings> read = new List<Readings>();
            try
            {
                var response = await webClient.GetAsync("readings?count=1&page=1&sensor_id=" + readingID);
                var rString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    read = JsonConvert.DeserializeObject<List<Readings>>(rString);
                    return read;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
            return null;
        }

        public async Task<String> listGoals()
        {
            webClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.token);
            uri = new Uri(baseAddress, "objectives");

            

            var json = JsonConvert.SerializeObject(objectives);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await webClient.GetAsync("objectives?");
                var rString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    objectives = new List<Objective>();
                    objectives = JsonConvert.DeserializeObject<List<Objective>>(rString);
                    return rString;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return null;
        }

        public async Task<string> storeGoals(int sensorId, int goalValue, string dateIni, string dateEnd, string desc)
        {
            webClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.token);
            uri = new Uri(baseAddress, "objectives");

            Objective goal = new Objective
            {
                sensor_id = sensorId,
                goalNumber = goalValue,
                starts_date = dateIni,
                ends_date = dateEnd,
                description = desc
            };

            var json = JsonConvert.SerializeObject(goal);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await webClient.PostAsync(uri, content);

                var rString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    goal = JsonConvert.DeserializeObject<Objective>(rString);
                    return rString;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return null;
        }

        public async Task<string> getGoalInfo(int goalID)
        {
            webClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.token);

            try
            {
                var response = await webClient.GetAsync("objectives/" + goalID);
                var rString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    Objective o = new Objective();
                    o = JsonConvert.DeserializeObject<Objective>(rString);
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

        public async Task<String> listReminders()
        {
            webClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.token);
            uri = new Uri(baseAddress, "reminders");



            var json = JsonConvert.SerializeObject(reminders);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await webClient.GetAsync("reminders");
                var rString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    reminders = new List<Reminder>();
                    reminders = JsonConvert.DeserializeObject<List<Reminder>>(rString);
                    return rString;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return null;
        }

        public async Task<string> getReminderInfo(int reminderID)
        {
            webClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.token);

            try
            {
                var response = await webClient.GetAsync("reminders/" + reminderID);
                var rString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    Reminder r = new Reminder();
                    r = JsonConvert.DeserializeObject<Reminder>(rString);
                    return rString;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return null;
        }

        public async Task<string> storeReminder(string desc, string date, string time)
        {
            webClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.token);
            uri = new Uri(baseAddress, "reminders");

            Reminder reminder = new Reminder
            {
                description = desc,
                end_date = date,
                end_time = time
            };

            var json = JsonConvert.SerializeObject(reminder);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await webClient.PostAsync(uri, content);

                var rString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    reminder = JsonConvert.DeserializeObject<Reminder>(rString);
                    return rString;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return null;
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
