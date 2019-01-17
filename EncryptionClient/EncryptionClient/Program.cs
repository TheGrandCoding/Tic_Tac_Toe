using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToeClient
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 f1 = new Form1();
            Application.Run(f1);
            f1.FormClosed += F1_FormClosed;
        }
        private static void F1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private static object _logjb = new object();
        private static string _logName;
        private static string logName {  get
            {
                if (_logName != null)
                    return _logName;
                int num = 0;
                string path = $"logs/{Environment.UserName}{num}.txt";
                bool exists = System.IO.File.Exists(path);
                while (exists) 
                {
                    num++;
                    path = $"logs/{Environment.UserName}{num}.txt";
                    exists = System.IO.File.Exists(path);
                }
                // new log path
                _logName = path;
                return _logName;
            } }
        public static void Log(string message)
        {
            if (!System.IO.Directory.Exists("logs/"))
                System.IO.Directory.CreateDirectory("logs");
            lock(_logjb)
            {
                System.IO.File.AppendAllText(logName, $"{DateTime.Now.ToString("hh:mm:ss.fff")}: {message}\r\n");
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.ExceptionObject.ToString());
            System.IO.File.WriteAllText("err.txt", e.ExceptionObject.ToString());
        }

        private static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var desiredASsembly = new AssemblyName(args.Name);
            if (desiredASsembly.Name == "MasterlistDLL")
            {
                return Assembly.Load(Properties.Resources.MasterlistDLL);
            }
            return null;
        }

        /// <summary>
        /// Holds information for use across all forms
        /// </summary>
        public static class SocketConnection
        {
            public static event EventHandler<string> RecievedMessage;

            public static TcpClient Client;

            public static bool ShouldListen = true;
            private static bool hasStarted = false;
            public static void Listening()
            {
                // Call this function in a single thread when the client connects, then one can simply use a function like the above/below
                RecievedMessage += SocketConnection_RecievedMessage;

                if(hasStarted && Client != null && Client.Connected == true)
                {
                    return;
                }
                hasStarted = true;
                ShouldListen = true;
                int err = 0;
                while(ShouldListen) {
                    try
                    {
                        NetworkStream stream = Client.GetStream();
                        byte[] bytes = new byte[Client.ReceiveBufferSize];
                        string data = "";
                        stream.Read(bytes, 0, Client.ReceiveBufferSize);
                        data = System.Text.Encoding.UTF8.GetString(bytes);
                        foreach(var msg in data.Split('%').Where(x => !string.IsNullOrWhiteSpace(x)))
                        {
                            string message = msg.Substring(0, msg.IndexOf('`'));
                            RecievedMessage?.Invoke(Client, message);
                        }
                    } catch (Exception ex)
                    {
                        err++;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.ToString());
                        if (err > 3)
                            ShouldListen = false;
                    }
                }
            }

            private static void SocketConnection_RecievedMessage(object sender, string e)
            {
                // This is an example function
                // Please f or the love of God dont use this one
                // You should create  a function like this, and add it to the event like the above
                // Per form.
                // Then, when you want to close the from, you can just do:
                RecievedMessage -= SocketConnection_RecievedMessage;
                // Or whatever the form is called.
                throw new NotImplementedException();
            }
        }

    }
}
