using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TicTacToeServer
{
    public class Bot
    {
        List<string> ValuesCheck = new List<string>();
        int counter;
        bool sent;
        string temp;
        int LevelBot;
        Player BotPlayer;
        string info;
        int Turns = 0;
        public bool TurnFirst;
        public string OtherPosistion = "";
        public string OtherPosistionLast = "";
        private void MyTurn()
        {
            Thread.Sleep(2500);
            if (LevelBot == 1)
            {
                EasyMode();
            } else if (LevelBot == 2)
            {
                MediumMode();
            } else if (LevelBot == 3)
            {
                HardMode();
            }
        }
        private void HardMode()
        {
            Turns += 1;
            if (Turns == 1)
            {
                FirstTurn();
            }
            else if (Turns == 2)
            {
                SecoundTurn();
            }
            else if (Turns == 3)
            {
                ThirdTurn();
            }
            else if (Turns == 4)
            {
                FourthTurn();
            }
            else if (Turns == 5)
            {
                FifthTurn();
            }
        }
        private void MediumMode()
        {
            int NCounter;
            int CCounter;
            for (int first = 0; first < 3; first++) //rows
            {
                NCounter = 0;
                CCounter = 0;
                for (int secound = 0; secound < 3; secound++)
                {
                    if (BotPlayer.GameIn.GameBoard.Board[first, secound] == TTTBoard.TTTPosition.Nought)
                    {
                        NCounter++;
                    }
                    if (BotPlayer.GameIn.GameBoard.Board[first, secound] == TTTBoard.TTTPosition.Cross)
                    {
                        CCounter++;
                    }
                }
                if (NCounter == 2 && CCounter == 0)//check to win
                {
                    if (BotPlayer.NoughtOrCross == 'N')
                    {
                       // Console.WriteLine("Win at Row " + first);
                        temp = FindEmpty("ROW", first);
                        temp = ConvertToPosistions(temp);
                        send("POS:"+temp);
                        return;
                    }
                }
                else if (NCounter == 0 && CCounter == 2)
                {
                    if (BotPlayer.NoughtOrCross == 'C')
                    {
                        //Console.WriteLine("Win at Row " + first);
                        temp = FindEmpty("ROW", first);
                        temp = ConvertToPosistions(temp);
                        send("POS:" + temp);
                        return;
                    }
                }
            }
            for (int secound = 0; secound < 3; secound++) //col
            {
                NCounter = 0;
                CCounter = 0;
                for (int first = 0; first < 3; first++)
                {
                    if (BotPlayer.GameIn.GameBoard.Board[first, secound] == TTTBoard.TTTPosition.Nought)
                    {
                        NCounter++;
                    }
                    if (BotPlayer.GameIn.GameBoard.Board[first, secound] == TTTBoard.TTTPosition.Cross)
                    {
                        CCounter++;
                    }
                }
                if (NCounter == 2 && CCounter == 0) //check to win
                {
                    if(BotPlayer.NoughtOrCross=='N')
                    {
                        //Console.WriteLine("Win at Col " + secound);
                        temp = FindEmpty("COL", secound);
                        temp = ConvertToPosistions(temp);
                        send("POS:" + temp);
                        return;
                    }
                }else if (CCounter == 2 && NCounter == 0)
                {
                    if (BotPlayer.NoughtOrCross == 'C')
                    {
                       // Console.WriteLine("Win at Col " + secound);
                        temp = FindEmpty("COL", secound);
                        temp = ConvertToPosistions(temp);
                        send("POS:" + temp);
                        return;
                    }
                }
            }
            NCounter = 0;
            CCounter = 0;
            for (int FirstDiagonal = 0; FirstDiagonal < 3; FirstDiagonal++)
            {
                if (BotPlayer.GameIn.GameBoard.Board[FirstDiagonal, FirstDiagonal] == TTTBoard.TTTPosition.Nought)
                {
                    NCounter++;
                }
                if (BotPlayer.GameIn.GameBoard.Board[FirstDiagonal, FirstDiagonal] == TTTBoard.TTTPosition.Cross)
                {
                    CCounter++;
                }
            }
            if (NCounter == 0 && CCounter == 2) // check win
            {
                if (BotPlayer.NoughtOrCross == 'C')
                {
                    //Console.WriteLine("Win at Diag1");
                    temp = FindEmpty("DIAG1", 0);
                    temp = ConvertToPosistions(temp);
                    send("POS:" + temp);
                    return;
                }
            }
            else if (NCounter == 2 && CCounter == 0)
            {
                if (BotPlayer.NoughtOrCross == 'N')
                {
                    //Console.WriteLine("Win at Diag1");
                    temp = FindEmpty("DIAG1", 0);
                    temp = ConvertToPosistions(temp);
                    send("POS:" + temp);
                    return;
                }
            }
            NCounter = 0; // secound diagonal
            CCounter = 0;
            int secoundd = 3;
            for (int first = 0; first < 3;first++)
            {
                secoundd--;
                if (BotPlayer.GameIn.GameBoard.Board[first, secoundd] == TTTBoard.TTTPosition.Nought)
                {
                    NCounter++;
                }
                if (BotPlayer.GameIn.GameBoard.Board[first ,secoundd] == TTTBoard.TTTPosition.Cross)
                {
                    CCounter++;
                }
            }
            if (NCounter == 2 && CCounter == 0)//look to win
            {
                if (BotPlayer.NoughtOrCross == 'N')
                {
                    //Console.WriteLine("Win at Diag2");
                    temp = FindEmpty("DIAG2", 0);
                    temp = ConvertToPosistions(temp);
                    send("POS:" + temp);
                    return;
                }
            }
            else if (NCounter == 0 && CCounter == 2)
            {
                if (BotPlayer.NoughtOrCross == 'C')
                {
                    //Console.WriteLine("Win at Diag1");
                    temp = FindEmpty("DIAG2", 0);
                    temp = ConvertToPosistions(temp);
                    send("POS:" + temp);
                    return;
                }
            }
            for (int first = 0; first < 3; first++) //rows
            {
                NCounter = 0;
                CCounter = 0;
                for (int secound = 0; secound < 3; secound++)
                {
                    if (BotPlayer.GameIn.GameBoard.Board[first, secound] == TTTBoard.TTTPosition.Nought)
                    {
                        NCounter++;
                    }
                    if (BotPlayer.GameIn.GameBoard.Board[first, secound] == TTTBoard.TTTPosition.Cross)
                    {
                        CCounter++;
                    }
                }
                if (NCounter == 2 && CCounter == 0 || CCounter == 2 && NCounter == 0) // check to block
                {
                    //Console.WriteLine("Block at Row "+first);
                    temp = FindEmpty("Row", first);
                    temp = ConvertToPosistions(temp);
                    send("POS:" + temp);
                    return;
                }
            }
            for (int secound = 0; secound < 3; secound++) //col
            {
                NCounter = 0;
                CCounter = 0;
                for (int first = 0; first < 3; first++)
                {
                    if (BotPlayer.GameIn.GameBoard.Board[first, secound] == TTTBoard.TTTPosition.Nought)
                    {
                        NCounter++;
                    }
                    if (BotPlayer.GameIn.GameBoard.Board[first, secound] == TTTBoard.TTTPosition.Cross)
                    {
                        CCounter++;
                    }
                }
                if (NCounter == 2 && CCounter == 0 || CCounter == 2 && NCounter == 0)//check to block
                {
                    //Console.WriteLine("Block at COL " + secound);
                    temp = FindEmpty("COL", secound);
                    temp = ConvertToPosistions(temp);
                    send("POS:" + temp);
                    return;
                }
            }
            NCounter = 0;
            CCounter = 0;
            for (int FirstDiagonal = 0; FirstDiagonal < 3; FirstDiagonal++)
            {
                if (BotPlayer.GameIn.GameBoard.Board[FirstDiagonal, FirstDiagonal] == TTTBoard.TTTPosition.Nought)
                {
                    NCounter++;
                }
                if (BotPlayer.GameIn.GameBoard.Board[FirstDiagonal, FirstDiagonal] == TTTBoard.TTTPosition.Cross)
                {
                    CCounter++;
                }
            }
            if (NCounter == 0 && CCounter == 2 || NCounter ==2 && CCounter == 0)//check block
            {
                //Console.WriteLine("Block at Diag1");
                temp = FindEmpty("DIAG1", 0);
                temp = ConvertToPosistions(temp);
                send("POS:" + temp);
                return;
            }
            NCounter = 0; // secound diagonal
            CCounter = 0;
            int secounddi = 3;
            for (int first = 0; first < 3; first++)
            {
                secounddi--;
                if (BotPlayer.GameIn.GameBoard.Board[first, secounddi] == TTTBoard.TTTPosition.Nought)
                {
                    NCounter++;
                }
                if (BotPlayer.GameIn.GameBoard.Board[first, secounddi] == TTTBoard.TTTPosition.Cross)
                {
                    CCounter++;
                }
            }
            if (NCounter == 2 && CCounter == 0 || NCounter == 0 && CCounter == 2)//look to block
            {
               // Console.WriteLine("Block at Diag2");
                temp = FindEmpty("DIAG2", 0);
                temp = ConvertToPosistions(temp);
                send("POS:" + temp);
                return;
            }
            EasyMode();
        }
        private void EasyMode()
        {
            ValuesCheck.Clear();
           // Console.WriteLine("Random");
            int RandomPostionY = Program.RandomNumber(0, 2);//random
            int RandomPostionX = Program.RandomNumber(0, 2);
            counter = 0;
            sent = false;
            while (counter <9 && !sent)
            {
                while(ValuesCheck.Contains(RandomPostionY.ToString() + RandomPostionX.ToString()))
                {
                    RandomPostionX = Program.RandomNumber(0, 2);
                    RandomPostionY = Program.RandomNumber(0, 2);
                }
                if (BotPlayer.GameIn.GameBoard.Board[RandomPostionY, RandomPostionX] == TTTBoard.TTTPosition.Empty)
                {
                    send("POS:"+ConvertToPosistions(RandomPostionY.ToString() + RandomPostionX.ToString()));
                    sent = true;
                    break;
                }
                ValuesCheck.Add(RandomPostionY.ToString() + RandomPostionX.ToString());
                counter++;
            }
        }
        private bool CheckDraw()
        {
            int counter =0;
            for (int first = 0; first < 3; first++)
            {
                for (int secound = 0; secound < 3; secound++)
                {
                    if (BotPlayer.GameIn.GameBoard.Board[first, secound] != TTTBoard.TTTPosition.Empty)
                    {
                        counter++;
                    }
                }
            }
            if (counter == 9)
            {
                //Console.WriteLine("True:"+counter.ToString());
                return true;
            }
            else
            {
                //Console.WriteLine("False:"+counter.ToString());
                return false;
            }
        }
        private string FindEmpty(string info ,int Pos)
        {
            //Console.WriteLine("Info: " + info);
           // Console.WriteLine("Pos: " + Pos.ToString());
            info = info.ToUpper();
            if (info == "ROW")
            {
                for (int Secound = 0; Secound < 3; Secound++)
                {
                    if (BotPlayer.GameIn.GameBoard.Board[Pos, Secound] == TTTBoard.TTTPosition.Empty)
                    {
                       // Console.WriteLine("Empty At-"+Pos.ToString() + Secound.ToString());
                        return Pos.ToString() + Secound.ToString();
                    }
                }
            }
            else if (info == "COL")
            {
                for (int first = 0; first < 3; first++)
                {
                    if (BotPlayer.GameIn.GameBoard.Board[first,Pos] == TTTBoard.TTTPosition.Empty)
                    {
                       // Console.WriteLine("Empty At-"+first.ToString() + Pos.ToString());
                        return first.ToString()+Pos.ToString();
                    }
                }
            }
            else if (info == "DIAG1")
            {
                for (int pos = 0; pos < 3; pos++)
                {
                    if (BotPlayer.GameIn.GameBoard.Board[pos, pos] == TTTBoard.TTTPosition.Empty)
                    {
                        //Console.WriteLine("Empty At-" + Pos.ToString() + Pos.ToString());
                        return pos.ToString() + pos.ToString();
                    }
                }
            }
            else if(info == "DIAG2")
            {
                int secoundd = 3;
                for (int first = 0; first < 3; first++)
                {
                    secoundd--;
                    if (BotPlayer.GameIn.GameBoard.Board[first, secoundd] == TTTBoard.TTTPosition.Empty)
                    {
                       // Console.WriteLine("Empty At-" + first.ToString() + secoundd.ToString());
                        return first.ToString() + secoundd.ToString();
                    }
                }
            }
            //Console.WriteLine("Empty At- [Return Nothing]");
            return "";
        }
        private string ConvertToPosistions(string BP)
        {
           // Console.WriteLine("Convert: "+BP);
            string Posistion ="";
            var Letters = BP.ToCharArray();
            if(Letters[0] == '0')
            {
                Posistion += "A";
            }
            else if (Letters[0] == '1')
            {
                Posistion += "B";
            }
            else if (Letters[0] == '2')
            {
                Posistion += "C";
            }
            if (Letters[1] == '0')
            {
                Posistion += "1";
            }
            else if (Letters[1] == '1')
            {
                Posistion += "2";
            }
            else if (Letters[1] == '2')
            {
                Posistion += "3";
            }
            //Console.WriteLine("To: " + Posistion);
            return Posistion;
        }
        private void FirstTurn()
        {
            //Console.WriteLine("FirstTURN");
            if (TurnFirst == true)
            {
                send("POS:C3");
            }
            else
            {
                OtherPosistionLast = OtherPosistion;
                if (OtherPosistion == "B2")
                {
                    send("POS:"+"C3");//
                    info = "FirstC3";
                }
                else if(OtherPosistion == "A1")
                {
                    send("POS:"+"B2");//
                    info = "1";
                }
                else if (OtherPosistion == "A2")
                {
                    send("POS:"+"B2");//
                    info = "2";
                }
                else if (OtherPosistion == "A3")
                {
                    send("POS:"+"B2");//
                    info = "3";
                }
                else if (OtherPosistion == "B1")
                {
                    send("POS:"+"B2");//
                    info = "4";
                }
                else if (OtherPosistion == "B3")
                {
                    send("POS:"+"B2");//
                    info = "5";
                }
                else if (OtherPosistion == "C1")
                {
                    send("POS:"+"B2");//
                    info = "6";
                }
                else if (OtherPosistion == "C2")
                {
                    send("POS:"+"B2");//
                    info = "7";
                }
                else if (OtherPosistion == "C3")
                {
                    send("POS:"+"B2");//
                    info = "8";
                }
            }
        }
        private void SecoundTurn()
        {
           // Console.WriteLine("SecoundTURN");
            if (TurnFirst == true)
            {
                if (OtherPosistion == "B2")
                {
                    send("POS:"+"A1");//
                    info = "FirstCenter";
                }
                else
                {
                    if(OtherPosistion == "C2"|| OtherPosistion == "C1")
                    {
                        send("POS:"+"A3");//
                        info = "FirstA3";
                        OtherPosistionLast = OtherPosistion;
                    }
                    else //Could be B1 A1 A2 A3 or B3
                    {
                        send("POS:"+"C1");//
                        info = "FirstC1";
                        OtherPosistionLast = OtherPosistion;
                    }
                }
            }
            else
            {
                if(info == "FirstC3")
                {
                    if (OtherPosistion == "A1")
                    {
                        send("POS:"+"A3");//
                        info = "2SecoundA3";
                    }
                    else if (OtherPosistion == "A2")
                    {
                        send("POS:"+"C2");//
                        info = "SecoundC2";
                    }
                    else if (OtherPosistion == "A3")
                    {
                        send("POS:"+"C1");//
                        info = "SecoundC1";
                    }
                    else if (OtherPosistion == "B1")
                    {
                        send("POS:"+"B3");//
                        info = "2SecoundB3";
                    }
                    else if (OtherPosistion == "B3")
                    {
                        send("POS:"+"B1");//
                        info = "SecoundB1";
                    }
                    else if (OtherPosistion == "C1")
                    {
                        send("POS:"+"A3");//
                        info = "SecoundA3";
                    }
                    else if (OtherPosistion == "C2")
                    {
                        send("POS:"+"A2");//
                        info = "SecoundA2";
                    }
                }else if(info == "1")
                {
                    if(OtherPosistion == "A2")
                    {
                        send("POS:"+"A3");//
                        info = "1";
                    }else if(OtherPosistion == "A3")
                    {
                        send("POS:"+"A2");//
                        info = "2";
                    }else if(OtherPosistion == "B3")
                    {
                        send("POS:"+"A3");//
                        info = "3";
                    }
                    else if (OtherPosistion == "C3")
                    {
                        send("POS:"+"B1");//
                        info = "4";
                    }
                    else if(OtherPosistion == "C2")
                    {
                        send("POS:"+"B1");//
                        info = "5";
                    }
                    else if(OtherPosistion == "C1")
                    {
                        send("POS:"+"B1");//
                        info = "6";
                    }
                    else if(OtherPosistion == "B1")
                    {
                        send("POS:"+"C1");//
                        info = "7";
                    }
                }else if(info == "2")
                {
                    if (OtherPosistion == "A1")
                    {
                        send("POS:"+"A3");//
                        info = "56";
                    }
                    else if (OtherPosistion == "A3")
                    {
                        send("POS:"+"A1");//
                        info = "8";
                    }
                    else if (OtherPosistion == "B3")
                    {
                        send("POS:"+"A3");//
                        info = "9";
                    }
                    else if (OtherPosistion == "C3")
                    {
                        send("POS:"+"A3");//
                        info = "10";
                    }
                    else if (OtherPosistion == "C2")
                    {
                        send("POS:"+"C3");//
                        info = "11";
                    }
                    else if (OtherPosistion == "C1")
                    {
                        send("POS:"+"A1");//
                        info = "12";
                    }
                    else if (OtherPosistion == "B1")
                    {
                        send("POS:"+"A1");//
                        info = "13";
                    }
                }else if(info == "3")
                {
                    if (OtherPosistion == "A1")
                    {
                        send("POS:"+"A2");//
                        info = "14";
                    }
                    else if (OtherPosistion == "A2")
                    {
                        send("POS:"+"A1");//
                        info = "15";
                    }
                    else if (OtherPosistion == "B3")
                    {
                        send("POS:"+"C3");//
                        info = "16";
                    }
                    else if (OtherPosistion == "C3")
                    {
                        send("POS:"+"B3");//
                        info = "17";
                    }
                    else if (OtherPosistion == "C2")
                    {
                        send("POS:"+"C3");//
                        info = "18";
                    }
                    else if (OtherPosistion == "C1")
                    {
                        send("POS:"+"C2");//
                        info = "19";
                    }
                    else if (OtherPosistion == "B1")
                    {
                        send("POS:"+"A1");//
                        info = "20";
                    }
                }else if(info == "4")
                {
                    if (OtherPosistion == "A1")
                    {
                        send("POS:"+"C1");//
                        info = "21";
                    }
                    else if (OtherPosistion == "A2")
                    {
                        send("POS:"+"A1");//
                        info = "22";
                    }
                    else if (OtherPosistion == "A3")
                    {
                        send("POS:"+"A1");//
                        info = "23";
                    }
                    else if (OtherPosistion == "B3")
                    {
                        send("POS:"+"C2");//
                        info = "24";
                    }
                    else if (OtherPosistion == "C3")
                    {
                        send("POS:"+"C1");//
                        info = "25";
                    }
                    else if (OtherPosistion == "C2")
                    {
                        send("POS:"+"C1");//
                        info = "26";
                    }
                    else if (OtherPosistion == "C1")
                    {
                        send("POS:"+"A1");//
                        info = "27";
                    }
                }else if(info == "5")
                {
                    if (OtherPosistion == "A1")
                    {
                        send("POS:"+"A3");//
                        info = "28";
                    }
                    else if (OtherPosistion == "A2")
                    {
                        send("POS:"+"A3");//
                        info = "29";
                    }
                    else if (OtherPosistion == "A3")
                    {
                        send("POS:"+"C3");//
                        info = "30";
                    }
                    else if (OtherPosistion == "C3")
                    {
                        send("POS:"+"A3");//
                        info = "31";
                    }
                    else if (OtherPosistion == "C2")
                    {
                        send("POS:"+"C3");//
                        info = "32";
                    }
                    else if (OtherPosistion == "C1")
                    {
                        send("POS:"+"C3");//
                        info = "33";
                    }
                    else if (OtherPosistion == "B1")
                    {
                        send("POS:"+"A2");//
                        info = "34";
                    }
                }else if(info == "6")
                {
                    if (OtherPosistion == "A1")
                    {
                        send("POS:"+"B1");//
                        info = "35";
                    }
                    else if (OtherPosistion == "A2")
                    {
                        send("POS:"+"A1");//
                        info = "36";
                    }
                    else if (OtherPosistion == "A3")
                    {
                        send("POS:"+"C3");//
                        info = "37";
                    }
                    else if (OtherPosistion == "B3")
                    {
                        send("POS:"+"B1");//
                        info = "38";
                    }
                    else if (OtherPosistion == "C3")
                    {
                        send("POS:"+"C2");//
                        info = "39";
                    }
                    else if (OtherPosistion == "C2")
                    {
                        send("POS:"+"C3");//
                        info = "40";
                    }
                    else if (OtherPosistion == "C1")
                    {
                        send("POS:"+"A1");//
                        info = "41";
                    }
                }else if(info == "7")
                {
                    if (OtherPosistion == "A1")
                    {
                        send("POS:"+"A3");//
                        info = "42";
                    }
                    else if (OtherPosistion == "A2")
                    {
                        send("POS:"+"B1");//
                        info = "43";
                    }
                    else if (OtherPosistion == "A3")
                    {
                        send("POS:"+"C3");//
                        info = "44";
                    }
                    else if (OtherPosistion == "B3")
                    {
                        send("POS:"+"C3");//
                        info = "45";
                    }
                    else if (OtherPosistion == "C3")
                    {
                        send("POS:"+"C1");//
                        info = "46";
                    }
                    else if (OtherPosistion == "C1")
                    {
                        send("POS:"+"C3");//
                        info = "47";
                    }
                    else if (OtherPosistion == "B1")
                    {
                        send("POS:"+"C1");//
                        info = "48";
                    }
                }else if(info == "8")
                {
                    if (OtherPosistion == "A1")
                    {
                        send("POS:"+"C2");//
                        info = "49";
                    }
                    else if (OtherPosistion == "A2")
                    {
                        send("POS:"+"A3");//
                        info = "50";
                    }
                    else if (OtherPosistion == "A3")
                    {
                        send("POS:"+"B3");//
                        info = "51";
                    }else if (OtherPosistion == "B3")
                    {
                        send("POS:"+"A3");//
                        info = "52";
                    }
                    else if (OtherPosistion == "C2")
                    {
                        send("POS:"+"C1");//
                        info = "53";
                    }
                    else if (OtherPosistion == "C1")
                    {
                        send("POS:"+"C2");//
                        info = "54";
                    }
                    
                    else if (OtherPosistion == "B1")
                    {
                        send("POS:"+"C1");//
                        info = "55";
                    }
                }
            }
        }
        private void ThirdTurn()
        {
            //Console.WriteLine("ThirdTURN");
            if (TurnFirst == true)
            {
                if (info == "FirstCenter")
                {
                    if(OtherPosistion == "A3")
                    {
                        send("POS:"+"C1");//
                        info = "SecoundC1";
                    }else if(OtherPosistion=="C1")
                    {
                        send("POS:"+"A3");//
                        info = "2SecoundA3";
                    }
                    else //picks a side
                    {
                        if(OtherPosistion == "A2")
                        {
                            send("POS:"+"C2");//
                            info = "SecoundC2";
                        }else if (OtherPosistion == "B1")
                        {
                            send("POS:"+"B3");//
                            info = "SecoundB3";
                        }
                        else if(OtherPosistion == "B3")
                        {
                            send("POS:"+"B1");//
                            info = "SecoundB1";
                        }
                        else //otherpositions == C2
                        {
                            send("POS:"+"A2");//
                            info = "SecoundA2";
                        }
                    }
                }else if(info == "FirstA3")
                {
                    if (OtherPosistion != "B3")
                    {
                        send("POS:"+"B3");//and win
                    }
                    else
                    {
                        if(OtherPosistionLast == "C2")
                        {
                            send("POS:"+"B2");//
                            info = "SecoundB2";
                        }
                        else
                        {
                            send("POS:"+"A1");//
                            info = "SecoundA1";
                        }
                    }
                }
                else if (info == "FirstC1")
                {
                    if(OtherPosistion != "C2")
                    {
                        send("POS:"+"C2");//and win
                    }
                    else
                    {
                        if(OtherPosistionLast == "B1"|| OtherPosistionLast == "A2" || OtherPosistionLast == "C3")
                        {
                            send("POS:"+"B2");//
                            info = "SecoundMiddle";
                        }
                        else if(OtherPosistionLast == "A1")
                        {
                            send("POS:"+"A3");//
                            info = "SecoundA3";
                        }
                        else //OtherPosistionLast = A3
                        {
                            send("POS:"+"A1");//
                            info = "2SecoundA1";
                        }
                    }
                }
            }
            else
            {
                if (info == "2SecoundA3")
                {
                    if (OtherPosistion != "B3")
                    {
                        send("POS:"+"B3");//and win
                    }
                    else
                    {
                        send("POS:"+"A1");//
                        info = "1";
                    }
                }else if(info == "SecoundC2")
                {
                    if (OtherPosistion != "C1")
                    {
                        send("POS:"+"C1");//and win
                    }
                    else
                    {
                        send("POS:"+"A3");//
                        info = "2";
                    }
                }else if (info == "SecoundC1")
                {
                    if(OtherPosistion != "C2")
                    {
                        send("POS:"+"C2");//and win
                    }
                    else
                    {
                        send("POS:"+"A2");//
                        info = "3";
                    }
                }else if (info == "2SecoundB3")
                {
                    if(OtherPosistion != "A3")
                    {
                        send("POS:"+"A3");//and win
                    }
                    else
                    {
                        send("POS:"+"C1");//
                        info = "4";
                    }
                }else if(info == "SecoundB1")
                {
                    if(OtherPosistion == "A3")
                    {
                        send("POS:"+"C1");//
                        info = "5";
                    }
                    else if (OtherPosistion == "A2")
                    {
                        send("POS:"+"C2");//
                        info = "6";
                    }
                    else if(OtherPosistion == "C1")
                    {
                        send("POS:"+"A3");//
                        info = "7";
                    }
                    else if (OtherPosistion == "C2")
                    {
                        send("POS:"+"A2");//
                        info = "8";
                    }
                    else if(OtherPosistion == "A1")
                    {
                        send("POS:"+"C2");//
                        info = "9";
                    }
                }else if(info == "SecoundA3")
                {
                    if(OtherPosistion != "B3")
                    {
                        send("POS:"+"B3");//amd win
                    }
                    else
                    {
                        send("POS:"+"B1");//
                        info = "10";
                    }
                }else if(info == "SecoundA2")
                {
                    if(OtherPosistion == "B1")
                    {
                        send("POS:"+"B3");//
                        info = "11";
                    }
                    else if (OtherPosistion == "C1")
                    {
                        send("POS:"+"A3");//
                        info = "12";
                    }
                    else if (OtherPosistion == "A3")
                    {
                        send("POS:"+"C1");//
                        info = "13";
                    }
                    else if (OtherPosistion == "B3")
                    {
                        send("POS:"+"B1");//
                        info = "14";
                    }
                    else if (OtherPosistion == "A1")
                    {
                        send("POS:"+"B3");//
                        info = "15";
                    }
                }
                else if(info == "1")
                {

                }
            }
        }
        private void FourthTurn()
        {
           // Console.WriteLine("FourthTURN");
            if (TurnFirst == true)
            {
                if (info == "SecoundB2")
                {
                    if (OtherPosistion != "A1")
                    {
                        send("POS:"+"A1");//and win
                    }
                    else
                    {
                        send("POS:"+"C1");//and win
                    }
                } else if (info == "SecoundA1")
                {
                    if (OtherPosistion != "A2")
                    {
                        send("POS:"+"A2");//and win
                    }
                    else
                    {
                        send("POS:"+"B2");//and win
                    }
                } else if (info == "SecoundMiddle")
                {
                    if (OtherPosistion != "A1")
                    {
                        send("POS:"+"A1");//and win
                    }
                    else
                    {
                        send("POS:"+"A3");//and win
                    }
                } else if (info == "SecoundA3")
                {
                    if (OtherPosistion != "B2")
                    {
                        send("POS:"+"B2");//and win
                    }
                    else
                    {
                        send("POS:"+"B3");//and win
                    }
                } else if (info == "2SecoundA1")
                {
                    if (OtherPosistion != "B2")
                    {
                        send("POS:"+"B2");//and win
                    }
                    else
                    {
                        send("POS:"+"B1");//and win
                    }
                } else if (info == "SecoundC1")
                {
                    if (OtherPosistion != "B1")
                    {
                        send("POS:"+"B1");//and win
                    }
                    else
                    {
                        send("POS:"+"C2");//and win
                    }
                }
                else if (info == "2SecoundA3")
                {
                    if (OtherPosistion != "A2")
                    {
                        send("POS:"+"A2");//and win
                    }
                    else
                    {
                        send("POS:"+"B3");//and win
                    }
                } else if (info == "SecoundC2")
                {
                    if(OtherPosistion != "C1")
                    {
                        send("POS:"+"C1");//and win
                    }
                    else
                    {
                        send("POS:"+"A3");//
                        info = "ThirdA3";
                    }
                }else if (info == "SecoundB3")
                {
                    if(OtherPosistion != "A3")
                    {
                        send("POS:"+"A3");//and win
                    }
                    else
                    {
                        send("POS:"+"C1");//
                        info = "ThirdC1";
                    }
                }else if (info == "SecoundB1")
                {
                    if (OtherPosistion != "C1")
                    {
                        send("POS:"+"C1");//and win
                    }
                    else
                    {
                        send("POS:"+"A3");//
                        info = "2ThirdA3";
                    }
                }else if (info  == "SecoundA2")
                {
                    if (OtherPosistion != "A3")
                    {
                        send("POS:"+"A3");//and win
                    }
                    else
                    {
                        send("POS:"+"C1");//
                        info = "2ThirdC1";
                    }
                }
            }
            else
            {
                if(info == "1")
                {
                    if(OtherPosistion == "A2")
                    {
                        send("POS:"+"C2");//
                    }
                    else if(OtherPosistion == "C2")
                    {
                        send("POS:"+"A2");//
                    }
                    else if(OtherPosistion == "C1")
                    {
                        send("POS:"+"A2");//
                    }
                }else if(info == "2")
                {
                    if (OtherPosistion != "B3")
                    {
                        send("POS:"+"B3");//and win
                    }
                    else
                    {
                        send("POS:"+"A2");//
                    }
                }else if(info == "3")
                {
                    if(OtherPosistion == "B1")
                    {
                        send("POS:"+"B3");//
                    }
                    else if(OtherPosistion == "B3")
                    {
                        send("POS:"+"B1");//
                    }
                    else if(OtherPosistion == "A1")
                    {
                        send("POS:"+"B2");//
                    }
                }else if(info == "4")
                {
                    if (OtherPosistion != "B2")
                    {
                        send("POS:"+"B2");//and win
                    }
                    else
                    {
                        send("POS:"+"A2");//
                    }
                }else if(info == "5")
                {
                    if(OtherPosistion != "C2")
                    {
                        send("POS:"+"C2");//and win
                    }
                    else
                    {
                        send("POS:"+"A1");//and win
                    }
                }else if (info == "6")
                {
                    if (OtherPosistion != "C1")
                    {
                        send("POS:"+"C1");//and win
                    }
                    else
                    {
                        send("POS:"+"A3");//
                    }
                }else if(info == "7")
                {
                    if(OtherPosistion == "A2")
                    {
                        send("POS:"+"C2");//
                    }
                    else if(OtherPosistion == "C2")
                    {
                        send("POS:"+"A2");//
                    }
                    else if(OtherPosistion == "A1")
                    {
                        send("POS:"+"C2");//
                    }
                }else if(info == "8")
                {
                    if(OtherPosistion == "A3")
                    {
                        send("POS:"+"C1");//
                    }
                    else if(OtherPosistion == "C1")
                    {
                        send("POS:"+"A3");//
                    }
                    else if(OtherPosistion == "A1")
                    {
                        send("POS:"+"A3");//
                    }
                }else if(info == "9")
                {
                    if (OtherPosistion != "C1")
                    {
                        send("POS:"+"C1");//and win
                    }
                    else
                    {
                        send("POS:"+"A3");//
                    }
                }else if(info == "10")
                {
                    if(OtherPosistion == "A2")
                    {
                        send("POS:"+"C2");//
                    }
                    else if(OtherPosistion == "C2")
                    {
                        send("POS:"+"A2");//
                    }
                    else if(OtherPosistion == "A1")
                    {
                        send("POS:"+"A2");//
                    }
                }else if(info == "11")
                {
                    if(OtherPosistion != "A3")
                    {
                        send("POS:"+"A3");//and win
                    }
                    else
                    {
                        send("POS:"+"C1");//
                    }
                }else if(info == "12")
                {
                    if (OtherPosistion != "B3")
                    {
                        send("POS:"+"B3");//and win
                    }
                    else
                    {
                        send("POS:"+"A1");//and win
                    }
                }else if(info == "13")
                {
                    if(OtherPosistion == "B3")
                    {
                        send("POS:"+"B1");//
                    }
                    else if(OtherPosistion == "B1")
                    {
                        send("POS:"+"B3");//
                    }
                    else if(OtherPosistion == "A1")
                    {
                        send("POS:"+"B1");//
                    }
                }else if(info == "14")
                {
                    if (OtherPosistion == "A1")
                    {
                        send("POS:"+"C1");//
                    }
                    else if (OtherPosistion == "A3")
                    {
                        send("POS:"+"C1");//
                    }
                    else if (OtherPosistion == "C1")
                    {
                        send("POS:"+"A3");//
                    }
                }else if(info == "15")
                {
                    if(OtherPosistion != "A3")
                    {
                        send("POS:"+"A3");//and win
                    }
                    else
                    {
                        send("POS:"+"C1");//
                    }
                }
            }
        }
        private void FifthTurn()
        {
            //Console.WriteLine("FIFTHTURN");
            if(info == "ThirdA3")
            {
                if(OtherPosistion != "B3")
                {
                    send("POS:"+"B3");//and win
                }
                else
                {
                    send("POS:"+"B1");//and Draw
                }
            }else if(info == "ThirdC1")
            {
                if(OtherPosistion != "C2")
                {
                    send("POS:"+"C2");//and win
                }
                else
                {
                    send("POS:"+"A2");//and Draw
                }
            }else if (info == "2ThirdA3")
            {
                if (OtherPosistion != "A2")
                {
                    send("POS:"+"A2");//and win
                }
                else
                {
                    send("POS:"+"C2");//and Draw
                }
            }else if (info == "2ThirdC1")
            {
                if (OtherPosistion != "B1")
                {
                    send("POS:"+"B1");//and win
                }
                else
                {
                    send("POS:"+"B3");//and Draw
                }
            }
        }
        public Player SetPlayer(Form1 _f1 , int BotLevel)
        {
            LevelBot = BotLevel;
            Player newBot = new Player();
            newBot._Bot = this;
            newBot.f1 = _f1;
            newBot.Name = "BOT";
            BotPlayer = newBot;
            return newBot;
        }
        private void send(string message)
        {
            //Console.WriteLine("[BotSent]"+message);
            if (BotPlayer.NoughtOrCross == 'N')
            {
                BotPlayer.GameIn.NBotPlayer(message);
            }
            else
            {
                BotPlayer.GameIn.CBotPlayer(message);
            }
        }
        string tempdata;
        public void ReciveData(string data)
        {
           // Console.WriteLine("[BotRecived]" + data);
            tempdata = data.ToUpper();
            if(tempdata == "YOU WIN" || tempdata == "OPPONENT LEFT" || tempdata == "YOU WIN DRAW")
            {
                send("READY");
            }
            else if (tempdata == "draw".ToUpper())
            {
                Turns = 0;
                OtherPosistion = "";
                OtherPosistionLast = "";
            }
            else if (tempdata == "True".ToUpper())
            {
                TurnFirst = true;
                MyTurn();
            }
            else if (tempdata == "False".ToUpper())
            {
                TurnFirst = false;
            }
            else if (tempdata == "CrossTrue".ToUpper())
            {
                BotPlayer.NoughtOrCross = 'C';
                TurnFirst = true;
                MyTurn();
            }
            else if (tempdata == "NoughtTrue".ToUpper())
            {
                BotPlayer.NoughtOrCross = 'N';
                TurnFirst = true;
                MyTurn();
            }
            else if (tempdata == "Nought".ToUpper())
            {
                BotPlayer.NoughtOrCross = 'N';
                TurnFirst = false;
            }
            else if (tempdata == "Cross".ToUpper())
            {
                BotPlayer.NoughtOrCross = 'C';
                TurnFirst = false;
            }
            else if (tempdata == "TIE")
            {
                TieBreaker();
            }
            else if(tempdata.StartsWith("POS:"))
            {
                string[] s = data.ToLower().Split(':');
                OtherPosistion = s[1].ToUpper(); ;
               // Console.WriteLine("POS:" + s[1]);
                if(BotPlayer.GameIn.won == null && CheckDraw() == false)
                {
                    Thread MT = new Thread(MyTurn);
                    MT.Start();
                }
            }
        }
        private void TieBreaker()
        {
            send("NUM:" +Program.RandomNumber(1,100).ToString());
        }
    }
}
