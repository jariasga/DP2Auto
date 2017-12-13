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
        //public static Objective objetivo { get; private set; }
        public static bool isParking { get; private set; }
        public static List<Reminder> reminders;
        private string temporalTokenSave;
        public static startTravel currentTravel { get; private set; }
        public static Objective currentObjective { get; private set;}
        public static Reminder currentReminder { get; private set; }
        public static endTravel end { get; private set; }
        static List<Viajes> travels;
        public static vehicleMAC vehicle { get; set; }
        
        public static int[] sensor_valid;

        public static string parking_ip;

        public RestService()
        {
            baseAddress = new Uri("http://dp2.iamallama.com/api/");
            webClient = new HttpClient();
            webClient.MaxResponseContentBufferSize = 256000;
            webClient.BaseAddress = baseAddress;

            client = new Client();
            travels = new List<Viajes>();
            currentTravel = new startTravel();
        }


        public  async void initializeTravels()
        {
            //travels = new List<Travel>();
            webClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.token);  //Copy
            uri = new Uri(baseAddress, "travels?");

            var content = new StringContent("", Encoding.UTF8, "application/json");

            try
            {
                var response = await webClient.GetAsync(uri);

                var rString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    //travels
                    travels = JsonConvert.DeserializeObject<List<Viajes>>(rString);
                    int a = 0;
                    //return rString;
                }
            }
            catch (Exception ex)
            {
                //return ex.Message;
            }
            //return null;


        }

        public static int CountTravels()
        {
            if (travels != null)
                return travels.Count();
            else return 0;
        }

        public static Viajes getNTravel(int id)
        {
            Viajes aux = new Viajes();
            for(int i = 0; i < travels.Count(); i++)
            {
                aux = travels.ElementAt(i);
                if(aux.id == id)
                    return aux; 
            }

            return null;
        }

        public static Viajes getTravelAt(int i)
        {
            return travels.ElementAt(i);
        }

        public static Viajes getLastTrip()
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
                    if (client.organization.is_parking == 1) isParking = true;
                    else isParking = false;
                    //Obtencion de cod de sensores para filtrar sensorpage
                    sensor_valid = new int[client.sensors.Count];
                    for (int i = 0; i < client.sensors.Count; i++)
                    {
                        sensor_valid[i] = client.sensors[i].id;
                    }
                    //ip de parking
                    parking_ip = client.parking_ip;

                    initializeTravels();
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
            webClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.token);  //Copy
            uri = new Uri(baseAddress, "travels");  //Cambia travels a lo q quieres

            clientMAC mac = new clientMAC   // Aca creas tu objeto (clase)
            {
                vehicle_mac = macAddress
            };
            //startTravel travel = new startTravel();
            currentTravel = new startTravel();
            var json = JsonConvert.SerializeObject(mac);    // Copy, Serializas (cambiar objeto)
            var content = new StringContent(json, Encoding.UTF8, "application/json");   // Cpoy, Lo pones en el contenido

            try
            {
                var response = await webClient.PostAsync(uri, content); // Copy

                var rString = await response.Content.ReadAsStringAsync(); // Copy

                if (response.IsSuccessStatusCode)
                {
                    currentTravel = JsonConvert.DeserializeObject<startTravel>(rString); //Cambias el objeto a devolver
                    return rString; //Puedes tmb retornar una clase
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
                    Viajes travel = new Viajes
                    {
                        id = start.id,
                        started_at = end.started_at,
                        ended_at = end.ended_at.date,
                        client_id = start.client_id,
                        vehicle_id = start.vehicle_id,
                        created_at = start.created_at,
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

        public async Task<string>storePosition(string mac, double lat, double longi)
        {
            webClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.token);
            uri = new Uri(baseAddress, "positions");

            Position_to_send posi = new Position_to_send()
            {
                vehicle_mac = mac,
                latitude = lat,
                longitude = longi,
            };

            var json = JsonConvert.SerializeObject(posi);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await webClient.PostAsync(uri, content);
                var rString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    Position posit = JsonConvert.DeserializeObject<Position>(rString);
                    //Debug.WriteLine("Dato almacenado coorectamente!");
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

                //var rString = await response.Content.ReadAsStringAsync();

                //if (response.IsSuccessStatusCode)
                //{
                //    read = JsonConvert.DeserializeObject<Readings>(rString);
                //    Debug.WriteLine("Dato almacenado coorectamente!");
                //    return rString;
                //}
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

        public async Task<List<Readings>> getReadingList(int sensorID)
        {
            webClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.token);
            
            try
            {
                var response = await webClient.GetAsync("readings?count=1&sensor_id=" + sensorID);
                var rString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    List<Readings> read = new List<Readings>();
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

        public async Task<List<Objective>> listGoals()
        {
            webClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.token);

            try
            {
                var response = await webClient.GetAsync("objectives?finished=false");
                var rString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    List<Objective> obj = new List<Objective>();
                    obj = JsonConvert.DeserializeObject<List<Objective>>(rString);
                    return obj;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null; ;
            }
            return null;
        }
        /*
        public async Task<List<Objective>> listAchievedGoals()
        {
            webClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.token);

            try
            {
                var response = await webClient.GetAsync("objectives?show_previous=true&finished=true");
                var rString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    List<Objective> obj = new List<Objective>();
                    obj = JsonConvert.DeserializeObject<List<Objective>>(rString);
                    return obj;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null; ;
            }
            return null;
        }
        */
        public async Task<Objective> storeGoals(int sensorId, int goalValue, string dateIni, string dateEnd, string desc)
        {
            webClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.token);
            uri = new Uri(baseAddress, "objectives");

            Objective goal = new Objective
            {
                sensor_id = sensorId,
                goal = goalValue,
                start_date = dateIni,
                end_date = dateEnd,
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
                    return currentObjective = JsonConvert.DeserializeObject<Objective>(rString);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ex.Message");
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

        public async Task<List<Reminder>> listReminders()
        {
            webClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.token);

            try
            {
                var response = await webClient.GetAsync("reminders?");
                var rString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    List<Reminder> rem = new List<Reminder>();
                    rem = JsonConvert.DeserializeObject<List<Reminder>>(rString);
                    return rem;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null; ;
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

        public async Task<Reminder> storeReminder(string desc, string date, string time)
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
                    return currentReminder = JsonConvert.DeserializeObject<Reminder>(rString);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ex.Message");
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

        public async Task<string> getVehicleInfo(int vehicleID)
        {
            webClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.token);

            try
            {
                var response = await webClient.GetAsync("vehicles/" + vehicleID);
                var rString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    vehicle = JsonConvert.DeserializeObject<vehicleMAC>(rString);
                    return vehicle.mac;
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
