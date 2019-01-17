using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using System.Windows.Forms;
using MasterlistDLL;

namespace TicTacToeServer
{
    public class Player
    {
        public bool Active;
        public Bot _Bot = null;
        public Spectator spectator = null;
        public Tournament Tournamentin;
        public Form1 f1;
        public Player user;
        public Player oppopent;
        public TcpClient Client;
        public string Name;
        public TTTGame GameIn =null; // game the player is in.
        public char NoughtOrCross;
        public Thread recicivedataThread;
        public bool Connected;
        public int disconnected = 0;
        public void Send(string message)
        {
            try
            {
                if (_Bot == null)
                {
                    //f1.Add(GameIn.gamenumber+1,$"{Name} sent {message}");
                    message = $"%{message}`";
                    NetworkStream SendDataStream = Client.GetStream();
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(message);
                    SendDataStream.Write(msg, 0, msg.Length);
                    try
                    {
                        //f1.Add(GameIn.totalgamenumber + 2, $"Sent: {message} to {Name}");
                    }
                    catch (Exception) { }
                }
                else
                {
                     _Bot.ReciveData(message);
                    try
                    {
                        //f1.Add(GameIn.totalgamenumber + 2, $"Sent: {message} to {Name}");
                    }
                    catch (Exception) { }
                }
            } catch (Exception ex)
            {
                //Console.WriteLine($"{Name} errored while sending {message}: {ex}");
                DisconnectPlayer(false);
            }
        }
        public void DisconnectPlayer(bool kicked)
        {
            try
            {
                if (!Connected || spectator != null) // prevents spam on player leave.
                    return;
                if (GameIn == null)
                {
                    f1.Add(0, Name + " has left");
                    if (Program.AllPlayers.Contains(this))
                    {
                        Program.AllPlayers.Remove(this);
                    }
                    if (Tournamentin != null)
                    {
                        if (Tournamentin.Winners.Contains(this))
                        {
                            Tournamentin.Winners.Remove(this);
                        }
                    }
                    Program.f1.Invoke((MethodInvoker)delegate
                    {
                        Program.AllPlayersnames.Remove(Name);
                        Program.f1.numberplay.Text = (Program.AllPlayers.Count.ToString());
                    });
                    f1.RemovePlayer(Name, "", "");
                }
                else
                {
                    if (kicked == false)
                    {
                        if (GameIn.gamefinshed == false && GameIn.won == null)
                        {
                            f1.Add(0, Name + " has left");
                            if (Program.AllPlayers.Contains(this))
                            {
                                Program.AllPlayers.Remove(this);
                            }
                            Program.AllPlayersnames.Remove(Name);
                            GameIn.gamefinshed = true;
                            Connected = false;
                            oppopent.Send("Opponent left");
                            f1.Add(GameIn.totalgamenumber + 2, $"{Name} quit, {oppopent.Name} wins");
                            f1.Add(0, $"Round {GameIn.totalgamenumber} Game {GameIn.roundgamenumber}: {oppopent.Name} beat {Name}");
                            Tournamentin.SpectatorMessage($"Round {GameIn.roundnumber} Game {GameIn.roundgamenumber}: {oppopent.Name} beat {Name}");
                            GameIn.won = oppopent;
                            f1.RemovePlayer(Name, GameIn.roundgamenumber.ToString(), GameIn.roundnumber.ToString());
                            f1.FinishedGame(GameIn.totalgamenumber + 2);
                            try
                            {
                                recicivedataThread.Abort();
                            }
                            catch (Exception) { }
                        }
                        else
                        {
                            if (Tournamentin.Winners.Contains(this))
                            {
                                Tournamentin.Winners.Remove(this);
                            }
                            if (Program.AllPlayers.Contains(this))
                            {
                                Program.AllPlayers.Remove(this);
                            }
                            Program.AllPlayersnames.Remove(Name);
                            f1.RemovePlayer(Name, GameIn.roundgamenumber.ToString(), GameIn.roundnumber.ToString());
                        }
                    }
                    else
                    {
                        f1.Add(0, Name + " has been kicked for inactivity");
                        if (Program.AllPlayers.Contains(this))
                        {
                            Program.AllPlayers.Remove(this);
                        }
                        Program.AllPlayersnames.Remove(Name);
                        GameIn.gamefinshed = true;
                        Connected = false;
                        oppopent.Send("Opponent left");
                        f1.Add(GameIn.totalgamenumber + 2, $"{Name} was kicked for inactivity, {oppopent.Name} wins");
                        f1.Add(0, $"Round {GameIn.totalgamenumber} Game {GameIn.roundgamenumber}: {oppopent.Name} beat {Name}");
                        Tournamentin.SpectatorMessage($"Round {GameIn.roundnumber} Game {GameIn.roundgamenumber}: {oppopent.Name} beat {Name}");
                        GameIn.won = oppopent;
                        f1.RemovePlayer(Name, GameIn.roundgamenumber.ToString(), GameIn.roundnumber.ToString());
                        f1.FinishedGame(GameIn.totalgamenumber + 2);
                        try
                        {
                            recicivedataThread.Abort();
                        }
                        catch (Exception) { }
                    }
                }
            }
            catch (Exception) { }
            try
            {
                if(Program.MASTERLIST?.Enabled ?? false)
                    Program.MASTERLIST.Update(Program.Server_Name, Program.AllPlayers.Count, false);
            } catch (Exception)
            {
                //Console.WriteLine("ML error: " + ex.ToString());
            }
        }
    }
}
