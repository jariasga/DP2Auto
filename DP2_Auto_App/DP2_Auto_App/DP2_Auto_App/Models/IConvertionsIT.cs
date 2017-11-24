using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DP2_Auto_App.Models;
using DP2_Auto_App.Models.RestServices;

namespace DP2_Auto_App.Models
{
    public interface IConvertionsIT
    {
        void ConReceived(string value);

        void ConSend(double []sValues);
    }
}
