using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP2_Auto_App.Models
{
    public class BTMessages
    {
        public static string message;
        public static string macBT;
        public BTMessages()
        {
            message = "";
        }
        public static string addMessage(string value)
        {
            string tempMessage = value.Trim('Z');
            message = string.Concat(message, tempMessage);
            return message;
        }
        public static void deleteMessage(int charCount)
        {
            message = message.Remove(0, charCount);
        }
        public static void print()
        {
            System.Diagnostics.Debug.WriteLine("Cola de mensaje: " + message);
        }
    }
}
