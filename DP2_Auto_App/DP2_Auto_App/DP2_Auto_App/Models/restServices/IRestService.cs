using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP2_Auto_App.Models.RestServices
{
    public interface IRestService
    {
        Task<string> createUserData(Users user);
    }
}
