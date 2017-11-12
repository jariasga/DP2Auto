using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP2_Auto_App.Models
{
    public interface IBth
    {
        void Send(String message);
        void Disconnect();
        void Connect(string name);
        List<String> PairedDevices();
    }
}
