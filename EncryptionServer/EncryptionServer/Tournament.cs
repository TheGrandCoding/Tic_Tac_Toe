using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TicTacToeServer
{
    public class Tournament
    {
        int BotLevel;
        public Dictionary<string, TTTGame> SpectatorsGames = new Dictionary<string, TTTGame>();
        public List<string> SpectatorMessages = new List<string>();
        int gamesofround;
        List<Player> PlayersWaitingForGame = new List<Player>();
        List<TTTGame> GamesStillPlaying = new List<TTTGame>();
        List<TTTGame> AllGamesDone = new List<TTTGame>();
        public List<Player> Winners = new List<Player>();
        Form1 f1 = Program.f1;
        public int TotalRoundNumber = 1;
        public int GameRoundNumber = 1;
        int GameCount = 0;
        int drawsallowed;
        public bool startedtournament = false;

        public int TotalNumberOfUniquePlayers
        {
            get
            {
                List<Player> possiblyDuplicatePlayers = new List<Player>();
                possiblyDuplicatePlayers.AddRange(PlayersWaitingForGame);
                possiblyDuplicatePlayers.AddRange(GamesStillPlaying.Select(x => x.CPlayer));
                possiblyDuplicatePlayers.AddRange(GamesStillPlaying.Select(x => x.NPlayer));
                possiblyDuplicatePlayers.AddRange(AllGamesDone.Select(x => x.CPlayer));
                possiblyDuplicatePlayers.AddRange(AllGamesDone.Select(x => x.NPlayer));
                possiblyDuplicatePlayers.AddRange(Winners); // not sure if this is covered in the above
                return possiblyDuplicatePlayers.Distinct()/*Gets unique items*/.Count();
            }
        }

        public void StartTournament(int da , int bl)
        {
            foreach(Player p in Program.AllPlayers)
            {
                p.Active = false;
            }
            foreach(Spectator spectator in Program.AllSpectators)
            {
                spectator.SpectatingTournament = this;
            }
            BotLevel = bl;
            SpectatorMessage("Tournament started");
            startedtournament = true;
            drawsallowed = da;
            f1.starttournamet();
            PlayersWaitingForGame.AddRange(Program.AllPlayers);
            pairup();
            foreach (Spectator spectator in Program.AllSpectators)
            {
                spectator.AddGames();
            }
            f1.SpectatorChanged();  
        }
        public void pairup()
        {
            List<Player> playersRemaining = new List<Player>(); // allows us to remove them
            playersRemaining.AddRange(PlayersWaitingForGame); // not directly =, since that might not work? 
            List<Player> shuffledPlayersRemaining = new List<Player>();
            for (int i = 0; i < playersRemaining.Count; i++)
            {
                shuffledPlayersRemaining.Add(playersRemaining[i]);
            }
            int n = shuffledPlayersRemaining.Count;
            while (n > 1)
            {
                n--;
                int k = Program.RandomNumber(0, n);
                var value = shuffledPlayersRemaining[k];
                shuffledPlayersRemaining[k] = shuffledPlayersRemaining[n];
                shuffledPlayersRemaining[n] = value;
            }
            if (shuffledPlayersRemaining.Count % 2 != 0)
            {
                if(BotLevel == 0)
                {
                    var player = shuffledPlayersRemaining[0];
                    shuffledPlayersRemaining.Remove(player);
                    player.Send("Wait");
                    Winners.Add(player);
                    player.Tournamentin = this;
                    Thread Ws = new Thread(() => WaitingSpectator(player));
                    Ws.Start();
                }
                else
                {
                    Bot bot = new Bot();
                    shuffledPlayersRemaining.Add(bot.SetPlayer(f1,BotLevel));
                }
            }

            while (shuffledPlayersRemaining.Count > 0)
            {
                Player nought = shuffledPlayersRemaining[0];
                Player cross = shuffledPlayersRemaining[1];
                nought.Tournamentin = this;
                cross.Tournamentin = this;
                shuffledPlayersRemaining.Remove(nought);
                shuffledPlayersRemaining.Remove(cross);
                TTTGame newgame = new TTTGame();
                newgame.tournamentin = this;
                newgame.drawsallowed = drawsallowed;
                newgame.roundnumber = TotalRoundNumber;
                GameCount += 1;
                newgame.CPlayer = cross;
                newgame.f1 = f1;
                newgame.NPlayer = nought;
                nought.GameIn = newgame;
                nought.NoughtOrCross = 'N';
                nought.oppopent = cross;
                cross.GameIn = newgame;
                cross.NoughtOrCross = 'C';
                cross.oppopent = nought;
                newgame.roundgamenumber = GameRoundNumber;
                f1.NewLB(cross.Name, nought.Name, GameRoundNumber);
                newgame.totalgamenumber = GameCount;
                newgame.WhoStarting();
                GamesStillPlaying.Add(newgame);
                newgame.GameOver += CheckFinshedGames;
                if(nought._Bot == null)
                {
                    Thread NoughtMessages = new Thread(newgame.HandleNoughtPlayer);
                    NoughtMessages.Start();
                    nought.recicivedataThread = NoughtMessages;
                }
                if (cross._Bot == null)
                {
                    Thread CrossMessages = new Thread(newgame.HandleCrossPlayer);
                    CrossMessages.Start();
                    cross.recicivedataThread = CrossMessages;
                }
                GameRoundNumber += 1;
            }
            gamesofround = GamesStillPlaying.Count();
        }
        public void CheckFinshedGames(object source, GameOverEventArgs e)
        {
            e.Game.GameOver -= CheckFinshedGames; // so it wont be raised again.
            if (e.Winner != null && e.Winner._Bot == null)
            {
                e.Winner.Active = true;
                Program.InitTimer(e.Winner);
                Winners.Add(e.Winner);
            }
            AllGamesDone.Add(e.Game);
            e.Game.gamefinshed = true;
            if (AllGamesDone.Count == gamesofround)
            {
                lastplayer();
            }
        }
        public void lastplayer()
        {
            if (Winners.Count == 1)
            {
                Winners[0].Send("You won the tournament");
                f1.Add(0, $"{Winners[0].Name} won the tournament");
                SpectatorMessage($"{Winners[0].Name} won the tournament");
                for(int i = 0; i < AllGamesDone.Count();i++)
                {
                    AllGamesDone[i].gamefinshed = true;
                }
                return;
            }
            else if (Winners.Count == 0)
            {
                f1.Add(0, "Tournament has ended (No players left)");
                SpectatorMessage($"Tournament has ended (No players left)");
                return;
            }
            else
            {
                nextround();
            }
        }
        public void nextround()
        {
            GameRoundNumber = 1;
            TotalRoundNumber += 1;
            f1.roundnumber = TotalRoundNumber;
            f1.Add(0, $"Round {TotalRoundNumber} Started");
            SpectatorMessage($"Round {TotalRoundNumber} Started");
            PlayersWaitingForGame.Clear();
            PlayersWaitingForGame.AddRange(Winners);
            foreach (Player p in PlayersWaitingForGame)
            {
                p.Active = false;
                if (p.spectator != null)
                {
                    if (Program.AllSpectators.Contains(p.spectator))
                    {
                        p.spectator.NotPlaying = false;
                        Program.AllSpectators.Remove(p.spectator);
                        p.spectator.RD.Abort();
                    }
                }
            }
            Thread.Sleep(1000);
            Winners.Clear();
            GamesStillPlaying.Clear();
            AllGamesDone.Clear();
            pairup();
            foreach(Spectator spectator in Program.AllSpectators)
            {
                spectator.AddGames();
            }
            f1.SpectatorChanged();
        }
        public void SpectatorMessage(string message)
        {
            foreach(Spectator spectator in Program.AllSpectators)
            {
                spectator.Send("Message#"+message);
            }
            SpectatorMessages.Add("Message#" + message);
        }
        public void AddSpectatorGame(string noughtplayer, string crossplayer, int roundnumber, int gamenumber, TTTGame Game)
        {
            SpectatorsGames.Add($"{noughtplayer}~{crossplayer}~{Convert.ToString(roundnumber)}~{Convert.ToString(gamenumber)}", Game);
        }
        public void WaitingSpectator(Player player)
        {
            try
            {
                Spectator s = new Spectator();
                player.spectator = s;
                s.NewSpectator(player.Client, player.Name, this,f1);
                Program.AllSpectators.Add(s);
                f1.SpectatorChanged();
            }
            catch (Exception) { }
        }
    }
}
