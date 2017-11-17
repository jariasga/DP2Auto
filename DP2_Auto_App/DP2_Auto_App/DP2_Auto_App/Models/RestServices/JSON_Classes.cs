using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP2_Auto_App.Models.RestServices
{
    public class Users
    {
        public string email { get; set; }
        public string password { get; set; }
    }

    public class Client
    {
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int organization_id { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public string phone { get; set; }
        public int rating { get; set; }
        public object deleted_at { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string profile_img_url { get; set; }
        public string gender { get; set; }
        public int height { get; set; }
        public string heart_illness { get; set; }
        public int heart_frecuency { get; set; }
        public string tools { get; set; }
        public int blocked { get; set; }
        public string token { get; set; }
    }

    public class clientMAC
    {
        public string vehicle_mac { get; set; }
    }

    public class StartedAt
    {
        public string date { get; set; }
        public int timezone_type { get; set; }
        public string timezone { get; set; }
    }

    public class startTravel
    {
        public StartedAt started_at { get; set; }
        public int client_id { get; set; }
        public int vehicle_id { get; set; }
        public string updated_at { get; set; }
        public string created_at { get; set; }
        public int id { get; set; }
    }

    public class EndedAt
    {
        public string date { get; set; }
        public int timezone_type { get; set; }
        public string timezone { get; set; }
    }

    public class endTravel
    {
        public int id { get; set; }
        public string started_at { get; set; }
        public EndedAt ended_at { get; set; }
        public int client_id { get; set; }
        public int vehicle_id { get; set; }
        public object deleted_at { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }

    public class Travel
    {
        public startTravel started { get; set; }
        public endTravel ended { get; set; }
    }

    public class Readings
    {
        public static readonly int WEIGHT = 1;
        public static readonly int PULSE = 2;
        public static readonly int PROXIMITY = 3;
        public static readonly int TEMPERATURE = 4;
        public static readonly int SPEED = 5;
        public static readonly int BATTERY = 6;
        public static readonly int HUMIDITY = 7;
        public static readonly int POSITION = 8;

        public double value { get; set; }
        public int travel_id { get; set; }
        public int sensor_id { get; set; }
        public string updated_at { get; set; }
        public string created_at { get; set; }
        public int id { get; set; }
    }

    public class Objective
    {
        public int id { get; set; }
        public string starts_date { get; set; }
        public string ends_date { get; set; }
        public int goalNumber { get; set; }
        public string description { get; set; }
        public int sensor_id { get; set; }
        public int client_id { get; set; }
        public object deleted_at { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }
}
