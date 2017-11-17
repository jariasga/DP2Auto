using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Android.Bluetooth;
using DP2_Auto_App.Droid.BlueTooth;
using Java.Util;
using Application = Xamarin.Forms.Application;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DP2_Auto_App.Models;

[assembly: Xamarin.Forms.Dependency(typeof(Bth))]
namespace DP2_Auto_App.Droid.BlueTooth
{
    class Bth : IBth
    {

        private CancellationTokenSource _ct { get; set; }
        
        public string MessageToSend { get; set; }
        List<string> messages;

        public Bth()
        {
            _ct = new CancellationTokenSource();
            messages = new List<string>();
        }
        
        public void Connect(string name)
        {
            Task.Run(async () => await ConnectDevice(name));
        }

        public void Disconnect()
        {
            if (_ct != null)
            {
                System.Diagnostics.Debug.WriteLine("Send a cancel to task!");
                _ct.Cancel();
                messages.Add("Desconectando");
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

            Stream inStream = null;     // var entrada de data
            Stream outStream = null;    // var salida de data

            BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;
            BluetoothSocket bthSocket = null;
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
                                System.Diagnostics.Debug.WriteLine("Connected!"); //Enviar estado de conectado
                                messages.Add("Conectado!");
                                try
                                {
                                    inStream = bthSocket.InputStream;
                                    outStream = bthSocket.OutputStream;
                                }
                                catch (IOException ex)
                                {
                                    System.Diagnostics.Debug.WriteLine(ex.Message);
                                }

                                byte[] buffer = new byte[1024];
                                int bytess;

                                while (_ct.IsCancellationRequested == false) {

                                    //enviando data
                                    if(MessageToSend != null)
                                    {
                                        var chars = MessageToSend.ToCharArray();
                                        var bytes = new List<byte>();
                                        foreach (var character in chars)
                                        {
                                            bytes.Add((byte)character);
                                        }
                                        await outStream.WriteAsync(bytes.ToArray(), 0, bytes.Count);
                                        MessageToSend = null;
                                    }

                                    //recibiendo data
                                    bytess = inStream.Read(buffer, 0, buffer.Length);
                                    if(bytess != 0)
                                    {
                                        string valor = System.Text.Encoding.ASCII.GetString(buffer);
                                        System.Diagnostics.Debug.WriteLine(valor);
                                        bytess = 0;
                                    }
                                }
                                

                            }
                        }
                    }
                }
                catch
                {
                }
                finally
                {
                    if (bthSocket != null)
                    {
                        bthSocket.Close();
                    }
                    device = null;
                    adapter = null;
                }
            }
        }
        public List<string> PairedDevices()
        {
            BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;
            List<string> devices = new List<string>();

            foreach (var bd in adapter.BondedDevices)
            {
                devices.Add(bd.Name);
            }
            return devices;
        }

        public void Connect()
        {
            throw new NotImplementedException();
        }

        public List<string> MessagesData()
        {
            messages.Add("Esperando DATOS");
            return messages;
        }
    }
}