using System;

namespace TicTacToeServer
{
    public class TTTBoard
    {
        public TTTGame Game;
        public Form1 f1;
        public int gamenumber;

        public enum TTTPosition
        {
            Empty,
            Nought,
            Cross
        }
        public TTTPosition[,] Board = new TTTPosition[3, 3]
        {
            { TTTPosition.Empty, TTTPosition.Empty, TTTPosition.Empty },
            { TTTPosition.Empty, TTTPosition.Empty, TTTPosition.Empty },
            { TTTPosition.Empty, TTTPosition.Empty, TTTPosition.Empty },
        };
        public TTTPosition GetPosition(string value)
        {
            var pos = convertString(value);
            return Board[pos.Item1, pos.Item2];
        }
        private Tuple<int, int> convertString(string value)
        {
            if (value.Length != 2)
            {
                throw new ArgumentException("Must be two-charactaers, eg A1, B2, C3", "value");
            }
            string letterStr = value.Substring(0, 1).ToLower();
            string numberStr = value.Substring(1);
            int number = int.Parse(numberStr) - 1; // expecting it to be 1-based, we need index based.
            if (number > 2 || number < 0)
            {
                throw new ArgumentException("Must be 1, 2 or 3.", "value");
            }
            int letter = letterStr == "a" ? 0 : letterStr == "b" ? 1 : 2;
            return new Tuple<int, int>(letter, number);
        }
        private string convertToLetter(TTTPosition position)
        {
            if(position == TTTPosition.Cross)
            {
                return "X";
            } else if (position == TTTPosition.Nought)
            {
                return "O";
            } else
            {
                return "  ";
            }
        }
        public void SetPosition(string position, TTTPosition value , string  data , Player To)
        {
            var pos = convertString(position);
            Board[pos.Item1, pos.Item2] = value;
            PrettyPrint();
            Game.SpectatorUpdate();
            To.Send(data);
            CheckWinner();
        }
        private void PrettyPrint()
        {
            string value = "";
            for(int letter = 0; letter < 3; letter++)
            {
                value = "";
                for (int number = 0; number < 3; number++)
                {
                    if(number == 0)
                    {
                        value += $" {convertToLetter(Board[letter, number])} | ";
                    } else if(number == 1)
                    {
                        value += $" {convertToLetter(Board[letter, number])} ";
                    } else
                    {
                        value += $" | {convertToLetter(Board[letter, number])} ";
                    }
                }
                f1.Add(gamenumber + 2, value);
            }
        }

        private void CheckWinner()
        {
            int crosses = 0;
            int noughtes = 0;
            int number = 0;
            // { { A1, A2, A3 }, { B1, B2, B3 }, { C1, C2, C3 }, { A1, B1, C1 }, { A2, B2, C2 }, { A3, B3, C3 }, { A1, B2, C3 }, { A3, B2, C1 } };
            string[,] wins = new string[8, 3]
            {
                { "A1", "A2", "A3"}, // row a
                { "B1", "B2", "B3"}, // row b
                { "C1", "C2", "C3"}, // row c
                { "A1", "B1", "C1"}, // col 1
                { "A2", "B2", "C2"}, // col 2
                { "A3", "B3", "C3"}, // col 3
                { "A1", "B2", "C3"}, // diag topL -> botR
                { "A3", "B2", "C1"}, // diag botL -> topR
            };

            for (int i = 0; i < 8; i++)
            {
                for (int ii = 0; ii < 3; ii++)
                {
                    var position = wins[i, ii];
                    var ttPos = GetPosition(position);
                    if (ttPos == TTTPosition.Cross)
                    {
                        crosses += 1;
                    }
                    if (ttPos == TTTPosition.Nought)
                    {
                        noughtes += 1;
                    }
                }
                if (crosses == 3)
                {
                    BoardFinished?.Invoke(this, new BoardFinishedEventArgs(this, Board, "Cross"));
                    return;
                }
                if (noughtes == 3)
                {
                    BoardFinished?.Invoke(this, new BoardFinishedEventArgs(this, Board, "Nought"));
                    return;
                }
                crosses = 0;
                noughtes = 0;
            }
            for(int let = 0; let < 3; let++)
            {
                for(int num  = 0; num < 3; num++)
                {
                    if(Board[let, num] != TTTPosition.Empty)
                    {
                        number += 1;
                    }
                }
            }
            if (number == 9)
            {
                BoardFinished?.Invoke(this, new BoardFinishedEventArgs(this, Board, "Draw"));
            }
        }
        
        public string InfoAsString()
        {
            string Info = "";
            for(int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    Info += convertToLetter(Board[row, col]).Replace("  ", "-");
                }
                Info += "_";
            }
            return Info;
        }

        public event BoardFinishedEventHandler BoardFinished;
        public delegate void BoardFinishedEventHandler(object sender, BoardFinishedEventArgs e);
        public class BoardFinishedEventArgs : EventArgs
        {
            public readonly TTTBoard Board;
            public readonly TTTPosition[,] DirectBoard;
            private readonly string _Winner;
            public bool Draw => _Winner == "Draw";
            public bool NoughtWin => _Winner == "Nought";
            public bool CrossWin => _Winner == "Cross";
            public BoardFinishedEventArgs(TTTBoard board, TTTPosition[,] direct, string winner)
            {
                Board = board; DirectBoard = direct; _Winner = winner;
            }
        }
    }
}
