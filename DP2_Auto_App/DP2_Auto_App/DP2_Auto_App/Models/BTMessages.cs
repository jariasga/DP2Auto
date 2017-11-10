using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP2_Auto_App.Models
{
    public class BTMessages
    {
        public List<string> received{ get; set; }   //  Recibidos por el sensor
        public List<string> send { get; set; }      //  Para enviar al sensor
        public BTMessages()
        {
            received = new List<string>();
            send = new List<string>();
        }
    }
}
