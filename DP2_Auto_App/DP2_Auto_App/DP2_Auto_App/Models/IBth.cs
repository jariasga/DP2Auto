using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DP2_Auto_App
{
    public interface IBth
    {
        void Send(string message);
        void Disconnect();
        void Connect(string name);
        List<string> PairedDevices();
    }
}

