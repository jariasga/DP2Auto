using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP2_Auto_App.Models.RestServices
{
    public interface IRestService
    {
        Task<string> getLoginToken(Users user);
        Task<string> getClientInfo();
        Task<string> updateClientInfo();
        Task<string> startTravel(string macAddress);
        Task<string> endTravel(startTravel start);
        Task<string> storeReadings(int sId, double value);
        Task<string> getReadingInfo(int readingID);
        Task<string> storeGoals(Objective goal);
        Task<string> getGoalInfo(int goalID);
        Task<string> listGoals();

        Task<string> storeReminder(string desc, string date, string time);
        Task<string> getReminderInfo(int reminderID);
        Task<string> listReminders();

    }
}
