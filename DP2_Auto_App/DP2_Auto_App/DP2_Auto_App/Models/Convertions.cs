using DP2_Auto_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using encription_SHA256;
using DP2_Auto_App.Models.RestServices;
using System.Diagnostics;

[assembly: Xamarin.Forms.Dependency(typeof(Convertions))]
namespace DP2_Auto_App.Models
{
    public class Convertions : IConvertionsIT
    {
        public double[] sensors = new double[20];
        
        public void ConReceived(string value)
        {
            string cadena = BTMessages.addMessage(value);
            string checksum;
            
            SHA_2 sha = new SHA_2();
            for(int i = 0; i < 20; i++)
            {
                sensors[i] = 0;
            }
            
            if (cadena.Contains("7EAB")){
                int pos = cadena.IndexOf("7EAB");
                string tempCadena = cadena.Remove(0, pos + 4);
                int cantSensores = Convert.ToInt32(tempCadena.Substring(0, 2));  // TryParse.Int32 (intentar)              
                tempCadena = tempCadena.Remove(0, 2); // Quitamos la longitud de la cadena
                string datosBasicos = tempCadena.Substring(0, 4);   // Leemos los datos del vehiculo (cinturon)
                tempCadena = tempCadena.Remove(0, 4);   // Llegamos a los sensores
                tempCadena = tempCadena.Substring(0, 6 * cantSensores + 6);
                   
                string datosSensores = tempCadena.Substring(0, tempCadena.Length - 6);
                checksum = tempCadena.Substring(datosSensores.Length, 6);

                string hash = sha.encrypt(datosSensores).Substring(0, 6).ToUpper();
                if (hash.Equals(checksum))
                {
                    while (datosSensores.Length > 0)
                    {
                        int sensorID = Readings.returnSensorID(datosSensores.Substring(0, 3));
                        datosSensores = datosSensores.Remove(0, 3);
                        int primerValor = Convert.ToInt32(datosSensores.Substring(0, 2), 16);
                        datosSensores = datosSensores.Remove(0, 2);
                        int segundoValor = Convert.ToInt32(datosSensores.Substring(0, 1));
                        datosSensores = datosSensores.Remove(0, 1);
                        sensors[sensorID] = 1.0 * primerValor + 1.0 * segundoValor / 10;
                    }
                    BTMessages.deleteMessage(4 + 2 + 6 * cantSensores + checksum.Length + 4 + 2);
                    //BTMessages.print();
                    saveDatatoWeb(sensors);
                    Debug.WriteLine("$$$$$$$$$$$$$$$$ Datos enviados");
                }
            }
        }

        public async void saveDatatoWeb(double [] sensors)
        {
            for (int i = 0; i < 20; i++)
            {
                if (sensors[i] > 0)
                {
                    await webService.rest.storeReadings(i, sensors[i]);
                    Debug.WriteLine("Sensor " + i + " enviado: " + sensors[i]);
                }
            }
        }

        public void ConSend(double[] sValues)
        {
            string initMessage = "7EAB";
            string basicVehicleVerification = "ACE9";
            string sensorData = "";
            string finalMessage = "";
            string checksum = "";
            int countSensor = 0;

            for (int i = 0; i < 6; i++)
                if (sValues[i] > 0)       //Contamos la cantidad de datos a enviar
                {
                    sensorData += Readings.returnCode(i + 1) + convertToHex(sValues[i]);    //Devuelve ya la cadena con el codigo F0X y el valor en HEX
                    countSensor++;
                }
            SHA_2 sha = new SHA_2();
            checksum = sha.encrypt(sensorData).Substring(0, 6).ToUpper();
            finalMessage = initMessage + countSensor.ToString("D2") + basicVehicleVerification + sensorData + checksum;
            Debug.WriteLine(finalMessage);
            ConReceived(finalMessage);
        }

        private string convertToHex(double initialValue)
        {
            int number = Convert.ToInt32(initialValue);
            int decimal_value = Convert.ToInt32(initialValue % 10);
            return number.ToString("X2") + decimal_value.ToString("X1");    // X4 representa a HEXA con 2 digitos de longitud
        }
    }
}
