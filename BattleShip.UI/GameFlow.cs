using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using System;
using System.IO;

namespace BattleShip.UI
{
    /// <summary>
    /// 
    /// </summary>
    class GameFlow
    {
        /// <summary>
        /// The gm
        /// </summary>
        GameState gm;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameFlow"/> class.
        /// </summary>
        public GameFlow()
        {
            gm = new GameState() { IsFirstPlayer1 = true, Player1 = new Player(), Player2 = new Player() };
        }

        private string[] ReadTextFile(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            return lines;
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            GameSetup GameSetup = new GameSetup(gm);
            string[] lines = ReadTextFile(@"BattleText.txt");           
            GameSetup.Setup();
            GameSetup.SetBoard(lines);
            StartShooting(gm, lines);            
        }

        private void StartShooting(GameState gm, string[] lines)
        {
            int shotCountPlayer1 = 0, shotCountPlayer2 = 0;
            FireShotResponse shotresponse;
            do
            {
                string shotLine = gm.IsFirstPlayer1 ? lines[4] : lines[5];               
                shotresponse = Shot(gm.IsFirstPlayer1 ? gm.Player2 : gm.Player1, gm.IsFirstPlayer1 ? gm.Player1 : gm.Player2, ReadCoordinate(shotLine, gm.IsFirstPlayer1 ? shotCountPlayer1 : shotCountPlayer2));
                ControlOutput.ShowShotResult(shotresponse, ReadCoordinate(shotLine, gm.IsFirstPlayer1 ? shotCountPlayer1 : shotCountPlayer2), gm.IsFirstPlayer1 ? gm.Player1.Name : gm.Player2.Name);        
                if (gm.IsFirstPlayer1)
                {
                    shotCountPlayer1++;
                }
                else
                {
                    shotCountPlayer2++;
                }
                if (shotresponse.ShotStatus == ShotStatus.Miss || shotresponse.ShotStatus == ShotStatus.Invalid || shotresponse.ShotStatus == ShotStatus.Duplicate)
                {
                    gm.IsFirstPlayer1 = !gm.IsFirstPlayer1;
                }

            } while (shotresponse.ShotStatus != ShotStatus.Victory);
        }

        private Coordinate ReadCoordinate(string shotLine, int shotCount)
        {
            if(shotLine.Split(' ').Length <= shotCount)
            {
                return null;
            }
            int y = ControlInput.GetNumberFromLetter(shotLine.Split(' ')[shotCount].Substring(0, 1));
            int x = Convert.ToInt32(shotLine.Split(' ')[shotCount].Substring(1));
            Coordinate shotPoint = new Coordinate(x, y);
            return shotPoint;
        }
 
        /// <summary>
        /// Shots the specified victim.
        /// </summary>
        /// <param name="victim">The victim.</param>
        /// <param name="Shoter">The shoter.</param>
        /// <param name="ShotPoint">The shot point.</param>
        /// <returns></returns>
        private FireShotResponse Shot(Player victim, Player Shoter, Coordinate ShotPoint)
        {
            FireShotResponse fire; 
            fire = victim.PlayerBoard.FireShot(ShotPoint);
            return fire;
        }
    }
}
