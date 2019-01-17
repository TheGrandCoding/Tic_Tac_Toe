using System;
using System.Net.Sockets;
using System.Threading;

namespace TicTacToeServer
{
    /// <summary>
    /// Contains the shared functions/items that are shared between the Player and Spectator class.
    /// </summary>
    public abstract class Connection
    {//
        public string Name;
        public bool Connected { get; internal set; }
        public int disconnected = 0;
        public TcpClient Client;
        [Obsolete("Disconnect function has built-in anti-spam protections", true)]
        private bool disconnectedfunctioncalled = false;
        public Thread recicivedataThread;

        /// <summary>
        /// Removes any functions that are listening to messages recieved by this Connection
        /// </summary>
        public void ClearListeners()
        {
            RecievedMessage = null;
        }

        public event EventHandler<string> RecievedMessage;

        private bool logicStarted = false;
        public void StartCheckLogic()
        {
            if (logicStarted) return;
            logicStarted = true;
            Connected = true;
            Thread cicl = new Thread(this.checkifplayerleave);
            cicl.Start();
            Program.leaveThreads.Add(cicl);
            recicivedataThread = new Thread(this.handleRecieveMessage);
            recicivedataThread.Start();
        }

        private void handleRecieveMessage()
        {
            string data;
            NetworkStream RecieveDataStream = Client.GetStream();
            byte[] bytes = new byte[256];
            int i;
            try
            {
                while (Connected)
                {
                    if ((i = RecieveDataStream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        RecievedMessage?.Invoke(this, data);
                    }
                }
            }
            catch (Exception ex)
            {
                this.Disconnect(false);
            }
        }

        public abstract void LogSendMessage(string message);

        public virtual void Send(string message)
        {
            try
            {
                //f1.Add(GameIn.gamenumber+1,$"{Name} sent {message}");
                message = $"%{message}`";
                NetworkStream SendDataStream = Client.GetStream();
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(message);
                SendDataStream.Write(msg, 0, msg.Length);
                try
                {
                    LogSendMessage(message);
                }
                catch (Exception) { }
            }
            catch (Exception)
            {
                //Console.WriteLine($"{Name} errored while sending {message}: {ex}");
                Disconnect(false);
            }
        }

        public void Disconnect(bool kicked)
        {
            if (!Connected)
                return;
            Connected = false;
            recicivedataThread.Abort();
            try
            {
                Client.Client.Disconnect(false);
                Client = null;
            }
            catch { }
        }
        public abstract void HandleDisconnectLogic(bool kicked);

        private void checkifplayerleave()
        {
            Socket socket = Client.Client;
            bool stillConnected = true;
            Connected = true;
            while (stillConnected)
            { // when the while loop ends, thread should close.
                try
                {
                    if (socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0)
                    {
                        stillConnected = false;
                        Disconnect(false);
                    }
                }
                catch (Exception)
                {
                    //Console.WriteLine($"Errored in checkPlayer, for {Name} error: {ex.ToString()}");
                    stillConnected = false;
                    Disconnect(false);
                }
            }
            Connected = false;
        }
    }
}
//Thread delete;
//Socket socket = theclient.Client;
//            while (true)
//            {
//                if (socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0)
//                {
//                    int ii = clientUsernames.IndexOf(nameofleaving);
//// TODO: change this function to use the Player object
//// ..  : so then we can access the game and send notice to both.
//delete = leaveThreads[ii];
//                    leaveThreads.RemoveAt(ii);
//                    Console.WriteLine(nameofleaving + " has left");
//                    delete.Abort();

//                }
//            }
