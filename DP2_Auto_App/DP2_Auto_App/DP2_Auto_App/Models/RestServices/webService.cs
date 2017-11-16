using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP2_Auto_App.Models.RestServices
{
    public class webService
    {
        public static RestService rest;
        public webService()
        {
            rest = new RestService();
        }
    }
}
