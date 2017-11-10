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

[assembly: Xamarin.Forms.Dependency(typeof(Bth))]
namespace DP2_Auto_App.Droid.BlueTooth
{
    class Bth : IBth
    {
        private CancellationTokenSource _ct { get; set; }

        

        public string MessageToSend { get; set; }

        DP2_Auto_App.Models.BTMessages mensajes;

        public Bth()
        {
            _ct = new CancellationTokenSource();
            mensajes = new Models.BTMessages();
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
            }
        }
        public void Send(string message)
        {
            if (MessageToSend == null)
                MessageToSend = message;
        }
        public async Task ConnectDevice(string name)
        {
            BluetoothDevice device = null;
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
                                System.Diagnostics.Debug.WriteLine("Connected!");
                                

                                //FINISH
                                while (_ct.IsCancellationRequested == false)
                                {
                                    if (MessageToSend != null)
                                    {
                                        var chars = MessageToSend.ToCharArray();
                                        var bytess = new List<byte>();
                                        foreach (var character in chars)
                                        {
                                            bytess.Add((byte)character);
                                        }

                                        await bthSocket.OutputStream.WriteAsync(bytess.ToArray(), 0, bytess.Count);
                                        System.Diagnostics.Debug.WriteLine(MessageToSend);
                                        MessageToSend = null;
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

            foreach (var bd in adapter.BondedDevices)
                devices.Add(bd.Name);

            return devices;
        }
        /*
        public void BeginListenForData()
        {
            //Busqueda de los mensajes de bluetooth
            try
            {
                inStream = bthSocket.InputStream;
            }
            catch (IOException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            Task.Factory.StartNew(() =>
            {
                byte[] buffer = new byte[1024];
                int bytes;
                while (true)
                {
                    try
                    {
                        bytes = inStream.Read(buffer, 0, buffer.Length);
                        if (bytes > 0)
                        {
                            string valor = System.Text.Encoding.ASCII.GetString(buffer);
                            System.Diagnostics.Debug.WriteLine("Valor Encontrado" + valor); //Imprimir valores que envian del bluetooth

                            //mensajes.received.Add(valor);

                            //Para andrioid interfaz xml

                            //aquí recibirías la información enviada por el serial Bluetooth
                            //RunOnUiThread(() => {
                            //    string valor = System.Text.Encoding.ASCII.GetString(buffer);
                            //    Result.Text = Result.Text + "\n" + valor;
                            //});
                        }
                    }
                    catch (Java.IO.IOException)
                    {
                        System.Diagnostics.Debug.WriteLine("Error");

                        //Para andrioid interfaz xml

                        //RunOnUiThread(() => {
                        //    Result.Text = string.Empty;
                        //});
                        //break;
                    }
                }
            });
        }
        */
    }
}