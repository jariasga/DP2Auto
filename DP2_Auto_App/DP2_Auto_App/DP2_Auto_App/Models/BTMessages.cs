using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP2_Auto_App.Models
{
    public class BTMessages
    {
        public static bool isSimulation;
        public static string message;
        public static string macBT;
        public static int statusBT = 0;
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
            message = message.Remove(0, charCount);   // To remove carriage return and new line
        }
        public static void print()
        {
            System.Diagnostics.Debug.WriteLine("Cola de mensaje: " + message);
        }

        public class Messages
        {
            public static readonly int START = 0;
            public static readonly int CONNECT = 1;
            public static readonly int DISCONNECT = 2;
            public static readonly int CONNECTING = 3;
            public static readonly int RECEIVED = 4;

            public static readonly string START_MSG = "";
            public static readonly string CONNECT_MSG = "Conectado";
            public static readonly string DISCONNECT_MSG = "Desconectado";
            public static readonly string CONNECTING_MSG = "Conectando";
            public static readonly string RECEIVED_MSG = "Recibiendo y Guardando Datos";


            public static string returnMessage(int MessageID)
            {
                if (MessageID == START) return START_MSG;
                else if (MessageID == CONNECT) return CONNECT_MSG;
                else if (MessageID == DISCONNECT) return DISCONNECT_MSG;
                else if (MessageID == CONNECTING) return CONNECTING_MSG;
                else if (MessageID == RECEIVED) return RECEIVED_MSG;
                else return "NO_MESSAGE_FOUND";
            }
        }
    }
}
