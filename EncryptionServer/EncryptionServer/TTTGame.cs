using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using System.Net;
using System.Net.Sockets;

using System.Windows.Forms;

namespace TicTacToeServer
{

    public class TTTGame
    {
        int noughtguess= -1;
        int crossguess= -1;
        public System.Timers.Timer t;
        public System.Timers.Timer BT;
        System.Timers.Timer dt;
        int drawcount = 0;
        bool timedout = false;
        public int drawsallowed;
        public int totalgamenumber; // game number from all rounds
        public int  roundgamenumber; // game number this round
        public int roundnumber; // round number
        public List<string> messages = new List<string>();
        public Player NPlayer;
        public Player CPlayer;
        public char CurrentPlayer;
        string data;
        public bool gamefinshed = false;
        public TTTBoard GameBoard = new TTTBoard();
        public Tournament tournamentin = new Tournament();
        public Form1 f1;
        public Player won = null;
        bool finished;
        bool TieBreaker = false;
        public char WhoStarting()
        {
            drawcount = 0;
            gamefinshed = false;
            timedout = false;
            settimer();
            GameBoard.gamenumber = totalgamenumber;
            GameBoard.f1 = f1;
            GameBoard.Game = this;
            CPlayer.Send("OPP:"+NPlayer.Name);
            NPlayer.Send("OPP:"+CPlayer.Name);
            f1.Add(totalgamenumber + 2, "Nought(O)=" + NPlayer.Name);
            f1.Add(totalgamenumber + 2, "Cross(X)=" + CPlayer.Name);
            int turn = Program.RandomNumber(1, 2);
            if (turn == 1)
            {
                CurrentPlayer = 'N';
                f1.Add(totalgamenumber + 2, $"Noughts({NPlayer.Name}) turn");
                NPlayer.Send("NoughtTrue");
                CPlayer.Send("cross");
            }
            else
            {
                CurrentPlayer = 'C';
                f1.Add(totalgamenumber + 2, $"Crosses({CPlayer.Name}) turn");
                NPlayer.Send("Nought");
                CPlayer.Send("CrossTrue");
            }
            CheckLastDraw();
            GameBoard.BoardFinished += GameBoard_BoardFinished;
            tournamentin.AddSpectatorGame(NPlayer.Name, CPlayer.Name, roundnumber, roundgamenumber, this);
            t.Start();
            return CurrentPlayer;
        }
        private void settimer()
        {
            t = new System.Timers.Timer(30000);
            t.Elapsed += TimeUp;
            t.AutoReset = true;
            BT = new System.Timers.Timer(30000);
            BT.Elapsed += MessageBoxTimer;
            BT.AutoReset = true;
            dt = new System.Timers.Timer(60000);
            dt.Elapsed += TimeDraw;
            dt.AutoReset = true;
        }
        private bool CheckLastDraw()
        {
            if(drawcount == drawsallowed - 1)
            {
                NPlayer.Send("Last");
                CPlayer.Send("Last");
                return true;
            }
            else
            {
                return false;
            }
        }
        private void GameBoard_BoardFinished(object sender, TTTBoard.BoardFinishedEventArgs e)
        {
            var board = e.Board;
            if(e.CrossWin)
            {
                f1.Add(totalgamenumber+2,$"Crosses ({CPlayer.Name}) wins!");
                f1.Add(0, $"Round {roundnumber} Game {roundgamenumber}: {CPlayer.Name} beat {NPlayer.Name}");
                tournamentin.SpectatorMessage($"Round {roundnumber} Game {roundgamenumber}: {CPlayer.Name} beat {NPlayer.Name}");
                CPlayer.Send("You win");
                NPlayer.Send("You lose");
                won = CPlayer;
                if (Program.AllPlayers.Contains(NPlayer))
                {
                    Program.AllPlayers.Remove(NPlayer);
                }
                f1.RemovePlayer(NPlayer.Name, roundgamenumber.ToString(), roundnumber.ToString());
                f1.FinishedGame(totalgamenumber + 2);
                SpectatorUpdate();
                BT.Start();
            } else if(e.Draw)
            {
                f1.Add(totalgamenumber + 2, "Draw!");
                GameBoard = new TTTBoard();
                GameBoard.BoardFinished += GameBoard_BoardFinished;
                GameBoard.gamenumber = totalgamenumber;
                GameBoard.f1 = f1;
                GameBoard.Game = this;
                if (Program.Quick == true)
                {
                    drawcount++;
                    if (drawcount == drawsallowed)
                    {
                        TieBreaker = true;
                        f1.Add(totalgamenumber + 2, $"{CPlayer.Name} and {NPlayer.Name} go into tie breaker");
                        CPlayer.Send("Tie");
                        NPlayer.Send("Tie");
                        SpectatorUpdate();
                        dt.Start();
                    }
                    else
                    {
                        CPlayer.Send("draw");
                        NPlayer.Send("draw");
                        DrawnGame(); // this determines who's go it is.
                        CheckLastDraw();
                        SpectatorUpdate();
                    }
                }
                else
                {
                    CPlayer.Send("draw");
                    NPlayer.Send("draw");
                    DrawnGame(); // this determines who's go it is.
                    CheckLastDraw();
                    SpectatorUpdate();
                }
            } else if(e.NoughtWin)
            {
                f1.Add(totalgamenumber + 2, $"Nought ({NPlayer.Name}) win!");
                f1.Add(0, $"Round {roundnumber} Game {roundgamenumber}: {NPlayer.Name} beat {CPlayer.Name}");
                tournamentin.SpectatorMessage($"Round {roundnumber} Game {roundgamenumber}: {NPlayer.Name} beat {CPlayer.Name}");
                NPlayer.Send("You win");
                CPlayer.Send("You lose");
                won = NPlayer;
                if (Program.AllPlayers.Contains(CPlayer))
                {
                    Program.AllPlayers.Remove(CPlayer);
                }
                f1.RemovePlayer(CPlayer.Name, roundgamenumber.ToString(), roundnumber.ToString());
                f1.FinishedGame(totalgamenumber + 2);
                SpectatorUpdate();
                BT.Start();
            }
        }

        public void HandleNoughtPlayer()
        {
            try
            {
                NetworkStream RecieveDataStream = NPlayer.Client.GetStream();
                byte[] bytes = new byte[256];
                int i;
                while (gamefinshed == false)
                {
                    if ((i = RecieveDataStream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        if (data.ToUpper() == "READY")
                        {
                            BT.Stop();
                            t.Stop();
                            finished = true;
                            gamefinshed = true;
                            OnGameOver(won, this);
                            return;
                        }
                        else if (data == "TSpectator")
                        {
                            gamefinshed = true;
                            LostSpectater(NPlayer);
                        }
                        else if (data == "FSpectator")
                        {
                            gamefinshed = true;
                        }
                        else if (data.ToUpper().StartsWith("NUM"))
                        {
                            string[] s = data.ToLower().Split(':');
                            noughtguess = Convert.ToInt32(s[1]);
                            f1.Add(totalgamenumber + 2, $"Received Nought({NPlayer.Name}) guess");
                            tiebreaker();
                        }
                        else if(data.ToUpper().StartsWith("POS"))
                        {
                            if (won == null && TieBreaker == false)
                            {
                                t.Stop();
                                string[] s = data.ToUpper().Split(':');
                                //f1.Add(totalgamenumber + 2, $"from {NPlayer.Name}: {s[1]}");
                                if (timedout == false)
                                {
                                    NPlayer.disconnected = 0;
                                }
                                timedout = false;
                                CurrentPlayer = 'C';
                                GameBoard.SetPosition(s[1], TTTBoard.TTTPosition.Nought, data, CPlayer);
                                if (won == null && TieBreaker == false)
                                {
                                    f1.Add(totalgamenumber + 2, $"Crosses({CPlayer.Name}) turn");
                                }
                                t.Start();
                            }
                        }else if (data.ToUpper() == "LEFT")
                        {
                            NPlayer.DisconnectPlayer(false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               // Console.WriteLine($"{NPlayer.Name} ex: " + ex.Message);
                NPlayer.DisconnectPlayer(false);
            }
        }

        public void HandleCrossPlayer()
        {
            try
            {
                NetworkStream RecieveDataStream = CPlayer.Client.GetStream();
                byte[] bytes = new byte[256];
                int i;
                while (gamefinshed == false)
                {
                    if ((i = RecieveDataStream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        if (data.ToUpper() == "READY")
                        {
                            t.Stop();
                            BT.Stop();
                            gamefinshed = true;
                            finished = true;
                            OnGameOver(won, this);
                            return;
                        }
                        else if (data == "TSpectator")
                        {
                            gamefinshed = true;
                            LostSpectater(CPlayer);
                        }
                        else if (data == "FSpectator")
                        {
                            gamefinshed = true;
                        }
                        else if (data.ToUpper().StartsWith("NUM"))
                        {
                            string[] s = data.ToLower().Split(':');
                            crossguess = Convert.ToInt32(s[1]);
                            f1.Add(totalgamenumber + 2, $"Received Cross({CPlayer.Name}) guess");
                            tiebreaker();
                        }
                        else if(data.ToUpper().StartsWith("POS"))
                        {
                            if (won == null && TieBreaker == false)
                            {
                                t.Stop();
                                string[] s = data.ToUpper().Split(':');
                                //f1.Add(totalgamenumber + 2, $"from {CPlayer.Name}: {s[1]}");
                                if (timedout == false)
                                {
                                    CPlayer.disconnected = 0;
                                }
                                timedout = false;
                                CurrentPlayer = 'N';
                                GameBoard.SetPosition(s[1], TTTBoard.TTTPosition.Cross, data, NPlayer);
                                if (won == null && TieBreaker == false)
                                {
                                    f1.Add(totalgamenumber + 2, $"Noughts({NPlayer.Name}) turn");
                                }
                                t.Start();
                            }
                        }
                        else if (data.ToUpper() == "LEFT")
                        {
                            CPlayer.DisconnectPlayer(false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Unknown error in {CPlayer.Name}: {ex.ToString()}");
                CPlayer.DisconnectPlayer(false);
            }
        }
        public void NBotPlayer(string data)
        {
            if (data.ToUpper() == "READY")
            {
                t.Stop();
                BT.Stop();
                gamefinshed = true;
                finished = true;
                OnGameOver(won, this);
                return;
            }
            else if (data.ToUpper().StartsWith("NUM"))
            {
                string[] s = data.ToLower().Split(':');
                noughtguess = Convert.ToInt32(s[1]);
                f1.Add(totalgamenumber + 2, $"Received Nought({NPlayer.Name}) guess");
                tiebreaker();
            }
            else if (data.ToUpper().StartsWith("POS"))
            {
                if (won == null && TieBreaker == false)
                {
                    t.Stop();
                    string[] s = data.ToUpper().Split(':');
                    // f1.Add(totalgamenumber + 2, $"from {NPlayer.Name}: {s[1]}");
                    if (timedout == false)
                    {
                        NPlayer.disconnected = 0;
                    }
                    timedout = false;
                    CurrentPlayer = 'C';
                    GameBoard.SetPosition(s[1], TTTBoard.TTTPosition.Nought, data, CPlayer);
                    if (won == null && TieBreaker == false)
                    {
                        f1.Add(totalgamenumber + 2, $"Cross({NPlayer.Name}) turn");
                    }
                    t.Start();
                }
            }
        }
        public void CBotPlayer(string data)
        {
            if (data.ToUpper() == "READY")
            {
                t.Stop();
                BT.Stop();
                gamefinshed = true;
                finished = true;
                OnGameOver(won, this);
                return;
            }
            else if (data.ToUpper().StartsWith("NUM"))
            {
                string[] s = data.ToLower().Split(':');
                crossguess = Convert.ToInt32(s[1]);
                f1.Add(totalgamenumber + 2, $"Received Cross({CPlayer.Name}) guess");
                tiebreaker();
            }
            else if(data.ToUpper().StartsWith("POS"))
            {
                if (won == null && TieBreaker == false)
                {
                    t.Stop();
                    string[] s = data.ToUpper().Split(':');
                    //f1.Add(totalgamenumber + 2, $"from {CPlayer.Name}: {s[1]}");
                    if (timedout == false)
                    {
                        CPlayer.disconnected = 0;
                    }
                    timedout = false;
                    CurrentPlayer = 'N';
                    GameBoard.SetPosition(s[1], TTTBoard.TTTPosition.Cross, data, NPlayer);
                    if (won == null && TieBreaker == false)
                    {
                        f1.Add(totalgamenumber + 2, $"Noughts({NPlayer.Name}) turn");
                    }
                    t.Start();
                }
            }
        }
        public char DrawnGame()
        {
            if (CurrentPlayer == 'C')
            {
                CurrentPlayer = 'C';
                NPlayer.Send("False");
                CPlayer.Send("True");
            }
            else
            {
                CurrentPlayer = 'N';
                NPlayer.Send("True");
                CPlayer.Send("False");
            }
            return CurrentPlayer;
        }
        private void tiebreaker()
        {
            int myguess = Program.RandomNumber(1, 100);
            string rtnvalue = checkwinnertie(myguess);
            if (rtnvalue == "")
                return;
            dt.Stop();
            if (rtnvalue == "ReTie")
            {
                int winnervalue = Program.RandomNumber(1, 2);
                if (winnervalue == 1)
                {
                    f1.Add(totalgamenumber + 2, $"Crosses ({CPlayer.Name}) gets a winning draw!(RND)");
                    f1.Add(0, $"Round {roundnumber} Game {roundgamenumber}: {CPlayer.Name} beat {NPlayer.Name}");
                    tournamentin.SpectatorMessage($"Round {roundnumber} Game {roundgamenumber}: {CPlayer.Name} beat {NPlayer.Name}");
                    CPlayer.Send($"You win draw:RND:{crossguess}:{noughtguess}:{myguess}");
                    NPlayer.Send($"You lose draw:RND:{noughtguess}:{crossguess}:{myguess}");
                    won = CPlayer;
                    SpectatorUpdate();
                    if (Program.AllPlayers.Contains(NPlayer))
                    {
                        Program.AllPlayers.Remove(NPlayer);
                    }
                    f1.RemovePlayer(NPlayer.Name, roundgamenumber.ToString(), roundnumber.ToString());
                    f1.FinishedGame(totalgamenumber + 2);
                    BT.Start();
                }
                else if (winnervalue == 2)
                {
                    f1.Add(totalgamenumber + 2, $"Nought ({NPlayer.Name}) gets a winning draw!(RND)");
                    f1.Add(0, $"Round {roundnumber} Game {roundgamenumber}: {NPlayer.Name} beat {CPlayer.Name}");
                    tournamentin.SpectatorMessage($"Round {roundnumber} Game {roundgamenumber}: {NPlayer.Name} beat {CPlayer.Name}");
                    NPlayer.Send($"You win draw:RND:{noughtguess}:{crossguess}:{myguess}");
                    CPlayer.Send($"You lose draw:RND:{crossguess}:{noughtguess}:{myguess}");
                    won = NPlayer;
                    SpectatorUpdate();
                    if (Program.AllPlayers.Contains(CPlayer))
                    {
                        Program.AllPlayers.Remove(CPlayer);
                    }
                    f1.RemovePlayer(CPlayer.Name, roundgamenumber.ToString(), roundnumber.ToString());
                    f1.FinishedGame(totalgamenumber + 2);
                    BT.Start();
                }
            }else if (rtnvalue == "Cross")
            {
                f1.Add(totalgamenumber + 2, $"Crosses ({CPlayer.Name}) gets a winning draw!(TB {crossguess}:{noughtguess} - {myguess})");
                f1.Add(0, $"Round {roundnumber} Game {roundgamenumber}: {CPlayer.Name} beat {NPlayer.Name}");
                tournamentin.SpectatorMessage($"Round {roundnumber} Game {roundgamenumber}: {CPlayer.Name} beat {NPlayer.Name}");
                CPlayer.Send($"You win draw:TIE:{crossguess}:{noughtguess}:{myguess}");
                NPlayer.Send($"You lose draw:TIE:{noughtguess}:{crossguess}:{myguess}");
                won = CPlayer;
                SpectatorUpdate();
                if (Program.AllPlayers.Contains(NPlayer))
                {
                    Program.AllPlayers.Remove(NPlayer);
                }
                f1.RemovePlayer(NPlayer.Name, roundgamenumber.ToString(), roundnumber.ToString());
                f1.FinishedGame(totalgamenumber + 2);
                BT.Start();
            }
            else
            {
                f1.Add(totalgamenumber + 2, $"Nought ({NPlayer.Name}) gets a winning draw!(TB {noughtguess}:{crossguess} - {myguess})");
                f1.Add(0, $"Round {roundnumber} Game {roundgamenumber}: {NPlayer.Name} beat {CPlayer.Name}");
                tournamentin.SpectatorMessage($"Round {roundnumber} Game {roundgamenumber}: {NPlayer.Name} beat {CPlayer.Name}");
                NPlayer.Send($"You win draw:TIE:{noughtguess}:{crossguess}:{myguess}");
                CPlayer.Send($"You lose draw:TIE:{crossguess}:{noughtguess}:{myguess}");
                won = NPlayer;
                SpectatorUpdate();
                if (Program.AllPlayers.Contains(CPlayer))
                {
                    Program.AllPlayers.Remove(CPlayer);
                }
                f1.RemovePlayer(CPlayer.Name, roundgamenumber.ToString(), roundnumber.ToString());
                f1.FinishedGame(totalgamenumber + 2);
                BT.Start();
            }
        }
        private string checkwinnertie(int num)
        {
            if (noughtguess !=-1 && crossguess != -1)
            {
                int noughtdiffernece = Math.Abs(num - noughtguess);
                int crossdifference = Math.Abs(num - crossguess);
                if(crossdifference == noughtdiffernece)
                {
                    return "ReTie";
                }else if (crossdifference < noughtdiffernece)
                {
                    return "Cross";
                }
                else
                {
                    return "Nought";
                }
            }else
            {
                return "";
            }
        }
        private void TimeDraw(object source, EventArgs e)
        {
            if (noughtguess == -1 && crossguess == -1)
            {
                return;
            }else if (noughtguess == -1)
            {
                NPlayer.disconnected = 3;
                kick(NPlayer , CPlayer, $"{NPlayer.Name} has been kicked(msgbox)");
            }
            else if (crossguess == -1)
            {
                CPlayer.disconnected = 3;
                kick(CPlayer , NPlayer,$"{CPlayer.Name} has been kicked(msgbox)");
            }
        }
        public delegate void GameoverEventHandler(object sender, GameOverEventArgs e);
        public event GameoverEventHandler GameOver;

        public virtual void OnGameOver(Player winner ,TTTGame donegame )
        {
            GameOver?.Invoke(this, new GameOverEventArgs(winner, donegame));
        }

        public override string ToString()
        {
            return $"{CPlayer.Name} vs {NPlayer.Name}{(gamefinshed ? " - Finished" : "")}";
        }
        private void TimeUp(object source, EventArgs e)
        {
            if(gamefinshed == false && won == null && TieBreaker==false)
            {
                timedout = true;
                if (CurrentPlayer == 'C')
                {
                    CPlayer.Send("Times Up");
                    CPlayer.disconnected++;
                    kick(CPlayer , NPlayer, $"{CPlayer.Name} has been kicked(msgbox)");
                }
                else
                {
                    NPlayer.Send("Times Up");
                    NPlayer.disconnected++;
                    kick(NPlayer , CPlayer, $"{NPlayer.Name} has been kicked(msgbox)");
                }
            }
        }
        private void MessageBoxTimer(object source, EventArgs e)
        {
            if (finished == false && won != null && gamefinshed == false)
            {
                Player p = won;
                p.disconnected = 3;
                kick(p,null,$"{p.Name} has been kicked(msgbox)");
            }
        }
        private void kick(Player p , Player playerleft , string msg)
        {
            if (p.disconnected == 3)
            {
                Thread.Sleep(1000);
                finished = true;
                p.Send("Kicked");
                p.Client.Close();
                p.recicivedataThread.Abort();
                f1.Add(totalgamenumber + 2, msg);
                if (Program.AllPlayers.Contains(p))
                {
                    Program.AllPlayers.Remove(p);
                }
                Program.AllPlayersnames.Remove(p.Name);
                Program.f1.Invoke((MethodInvoker)delegate
                {
                    f1.numberplay.Text = Program.AllPlayers.Count.ToString();
                });
                f1.RemovePlayer(p.Name, p.GameIn.roundgamenumber.ToString(), p.GameIn.roundnumber.ToString());
                OnGameOver(playerleft, this);
            }
        }
        public string GetInfo()
        {
            string strBoard = GameBoard.InfoAsString();
            if (CurrentPlayer == 'N')
            {
                strBoard += $"/N:{CPlayer.Name}:{NPlayer.Name}";
            }
            else
            {
                strBoard += $"/C:{CPlayer.Name}:{NPlayer.Name}";
            }
            if(won == null)
            {
                if (TieBreaker == true)
                {
                    strBoard += ":D";
                }
                else
                {
                    strBoard += ":F";
                }
            }
            else
            {
                if (won == NPlayer && TieBreaker ==false)
                {
                    strBoard += ":N";
                }
                else if(won == CPlayer && TieBreaker == false)
                {
                    strBoard += ":C";
                }
                else if (won == NPlayer && TieBreaker == true)
                {
                    strBoard += ":NT";
                }
                else if (won == CPlayer && TieBreaker == true)
                {
                    strBoard += ":C";
                }
            }
            strBoard = "Info/" + strBoard;
            return strBoard;
        }
        public List<Spectator> Spectators = new List<Spectator>();
        public void SpectatorUpdate()
        {
            foreach (Spectator spectator in Spectators)
            {
                spectator.GameUpdate();
            }
        }
        private void LostSpectater(Player loser)
        {
            Program.NewSpectator(loser);
        }
    }
    public class GameOverEventArgs : EventArgs
    {
        public Player Winner { get; set; }
        public TTTGame Game { get; set; }
        public GameOverEventArgs(Player winner, TTTGame game)
        {
            Winner = winner;
            Game = game;
        }
    }
}
