using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Android.Bluetooth;
using BuetoothToArduinoTest.Droid.BlueTooth;
using Java.Util;
using Application = Xamarin.Forms.Application;
using DP2_Auto_App.Models;
using Xamarin.Forms;
using System;
using Java.IO;

[assembly: Xamarin.Forms.Dependency(typeof(Bth))]
namespace BuetoothToArduinoTest.Droid.BlueTooth
{
    class Bth : IBth
    {
        private CancellationTokenSource _ct { get; set; }

        public string MessageToSend { get; set; }
        
        public Bth()
        {
        }

        public void Connect(string name)
        {
            Task.Run(async () => ConnectDevice(name));
        }
        public void Disconnect()
        {
            if (_ct != null)
            {
                System.Diagnostics.Debug.WriteLine("Send a cancel to task!");
                BTMessages.macBT = ""; //eliminado mac 
                _ct.Cancel();
            }
        }
        public void Send(string message)
        {
            if (MessageToSend == null)
                MessageToSend = message;
        }
        private async Task ConnectDevice(string name)
        {
            BluetoothDevice device = null;
            BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;
            BluetoothSocket bthSocket = null;

            _ct = new CancellationTokenSource();
            while (_ct.IsCancellationRequested == false)
            {
                try
                {
                    Thread.Sleep(250);

                    adapter = BluetoothAdapter.DefaultAdapter;

                    if (adapter == null)
                        System.Diagnostics.Debug.WriteLine("No Bluetooth adapter found.");
                    else
                        System.Diagnostics.Debug.WriteLine("Adapter found!!");

                    if (!adapter.IsEnabled)
                        System.Diagnostics.Debug.WriteLine("Bluetooth adapter is not enabled.");
                    else
                        System.Diagnostics.Debug.WriteLine("Adapter enabled!");

                    System.Diagnostics.Debug.WriteLine("Try to connect to " + name);

                    foreach (var bd in adapter.BondedDevices)
                    {
                        System.Diagnostics.Debug.WriteLine("Paired devices found: " + bd.Name.ToUpper());
                        if (bd.Name.ToUpper().IndexOf(name.ToUpper()) >= 0)
                        {
                            System.Diagnostics.Debug.WriteLine("Found " + bd.Name + ". Try to connect with it!");
                            device = bd;
                            break;
                        }
                    }

                    if (device == null)
                        System.Diagnostics.Debug.WriteLine("Named device not found.");
                    else
                    {
                        UUID uuid = UUID.FromString("00001101-0000-1000-8000-00805f9b34fb");
                        bthSocket = device.CreateInsecureRfcommSocketToServiceRecord(uuid);
                        if (bthSocket != null)
                        {
                            await bthSocket.ConnectAsync();
                            
                            if (bthSocket.IsConnected)
                            {
                                System.Diagnostics.Debug.WriteLine("Connected!");
                                BTMessages.macBT = device.Address; //Estrayendo la vac del vehiculo;

                                var mReader = new InputStreamReader(bthSocket.InputStream);
                                var buffer = new BufferedReader(mReader);

                                while (_ct.IsCancellationRequested == false)
                                {
                                    if (buffer.Ready())
                                    {
                                        char[] chr = new char[100];
                                        string barcode = "";
                                        await buffer.ReadAsync(chr);
                                        foreach (char c in chr)
                                        {
                                            if (c == '\0')
                                                break;
                                            barcode += c;
                                        }
                                        if (barcode.Length > 0)
                                        {
                                            System.Diagnostics.Debug.WriteLine("Letto: " + barcode);
                                        }
                                        else
                                            System.Diagnostics.Debug.WriteLine("No data ...");
                                    }
                                    else
                                    {
                                        //System.Diagnostics.Debug.WriteLine("No data to read ...");
                                        if (MessageToSend != null)
                                        {
                                            var chars = MessageToSend.ToCharArray();
                                            var bytes = new List<byte>();
                                            foreach (var character in chars)
                                            {
                                                bytes.Add((byte)character);
                                            }

                                            await bthSocket.OutputStream.WriteAsync(bytes.ToArray(), 0, bytes.Count);
                                            MessageToSend = null;
                                        }
                                    }
                                        /*
                                        await bthSocket.InputStream.ReadAsync(buffer, 0, buffer.Length);
                                        for (int i = 0; i < buffer.Length; i++)
                                        {
                                            if (buffer[i] == 0) buffer[i] = 90;
                                        }
                                        valor = System.Text.Encoding.ASCII.GetString(buffer);
                                        //System.Diagnostics.Debug.WriteLine(valor);
                                        DependencyService.Get<IConvertionsIT>().ConReceived(valor);
                                        */
                                }
                                System.Diagnostics.Debug.WriteLine("Exit the inner loop");
                            }
                        }
                        else
                            System.Diagnostics.Debug.WriteLine("bthSocket = null");
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("EXCEPTION: " + ex.Message);
                }
                finally
                {
                    if (bthSocket != null)
                        bthSocket.Close();
                    device = null;
                    adapter = null;
                }
            }
        }
        public List<string> PairedDevices()
        {
            BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;
            List<string> devices = new List<string>();

            if (adapter != null)
            {
                foreach (var bd in adapter.BondedDevices)
                    devices.Add(bd.Name);
            }
                

            return devices;
        }
        /*
        public List<string> MessagesData()
        {
            throw new NotImplementedException();
        }*/
        
    }
}
