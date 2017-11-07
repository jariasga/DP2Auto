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
        public string token { get; set; }
    }
}
