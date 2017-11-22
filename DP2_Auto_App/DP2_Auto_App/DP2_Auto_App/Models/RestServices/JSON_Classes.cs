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
        public Organization organization { get; set; }
        public List<object> sensors { get; set; }
        public string token { get; set; }
    }
    public class Organization
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string slug { get; set; }
        public object deleted_at { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public int max_vel { get; set; }
        public int is_parking { get; set; }
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
        public static readonly int ILUMINITY = 9;
        public static readonly int UV = 10;
        public static readonly int ANGLE = 11;

        public static readonly string WEIGHT_CODE = "F01";
        public static readonly string PULSE_CODE = "F02";
        public static readonly string PROXIMITY_CODE = "F03";
        public static readonly string TEMPERATURE_CODE = "F04";
        public static readonly string SPEED_CODE = "F05";
        public static readonly string BATTERY_CODE= "F06";
        public static readonly string HUMIDITY_CODE = "F07";
        public static readonly string POSITION_CODE = "F08";
        public static readonly string ILUMINITY_CODE = "F09";
        public static readonly string UV_CODE = "F10";
        public static readonly string ANGLE_CODE = "F11";

        public static string returnCode(int sensorID)
        {
            if (sensorID == WEIGHT) return WEIGHT_CODE;
            else if (sensorID == PULSE) return PULSE_CODE;
            else if (sensorID == PROXIMITY) return PROXIMITY_CODE;
            else if (sensorID == TEMPERATURE) return TEMPERATURE_CODE;
            else if (sensorID == SPEED) return SPEED_CODE;
            else if (sensorID == BATTERY) return BATTERY_CODE;
            else if (sensorID == HUMIDITY) return HUMIDITY_CODE;
            else if (sensorID == POSITION) return POSITION_CODE;
            else return "NO_SENSOR_ID";
        }
        public static int returnSensorID(string code)
        {
            if (code.Equals(WEIGHT_CODE)) return WEIGHT;
            else if (code.Equals(PULSE_CODE)) return PULSE;
            else if (code.Equals(PROXIMITY_CODE)) return PROXIMITY;
            else if (code.Equals(TEMPERATURE_CODE)) return TEMPERATURE;
            else if (code.Equals(SPEED_CODE)) return SPEED;
            else if (code.Equals(BATTERY_CODE)) return BATTERY;
            else if (code.Equals(HUMIDITY_CODE)) return HUMIDITY;
            else if (code.Equals(POSITION_CODE)) return POSITION;
            else if (code.Equals(ILUMINITY_CODE)) return ILUMINITY;
            else if (code.Equals(UV_CODE)) return UV;
            else if (code.Equals(ANGLE_CODE)) return ANGLE;
            else return 0;
        }

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
        public string start_date { get; set; }
        public string end_date { get; set; }
        public int goalNumber { get; set; }
        public string description { get; set; }
        public int sensor_id { get; set; }
        public int client_id { get; set; }
        public object deleted_at { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }

    public class Reminder
    {
        public string description { get; set; }
        public string end_date { get; set; }
        public string end_time { get; set; }
    }

    public class Parking
    {
        public string modo { get; set; }
        public string angulo { get; set; }
    }
}
