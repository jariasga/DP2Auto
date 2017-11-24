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
        Task<Readings> getReadingInfo(int readingID);
        Task<Objective> storeGoals(int sId, int goal, string dateIni, string dateEnd, string desc);
        Task<string> getGoalInfo(int goalID);
        Task<List<Objective>> listGoals();
        Task<List<Objective>> listAchievedGoals();
        Task<Reminder> storeReminder(string desc, string date, string time);
        Task<string> getReminderInfo(int reminderID);
        Task<List<Reminder>> listReminders();

    }
}
