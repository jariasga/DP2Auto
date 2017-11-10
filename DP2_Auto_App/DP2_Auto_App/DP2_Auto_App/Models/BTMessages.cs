using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP2_Auto_App.Models
{
    public class BTMessages
    {
        List<string> received{ get; set; }
        List<string> send { get; set; }
        public BTMessages()
        {
            received = new List<string>();
            send = new List<string>();
        }
    }
}
