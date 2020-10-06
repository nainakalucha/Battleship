using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using System;
namespace BattleShip.UI
{
    /// <summary>
    /// 
    /// </summary>
    class ControlOutput
    {
        /// <summary>
        /// Shows the header.
        /// </summary>
        public static void ShowHeader()
        {
            Console.WriteLine("----------THE BATTLESHIP GAME!!!----------"); 
        }

        /// <summary>
        /// Shows the whose turn.
        /// </summary>
        /// <param name="player">The player.</param>
        public static void ShowWhoseTurn(Player player)
        {
            Console.WriteLine(player.Name + " turn... ");
        }

        /// <summary>
        /// Gets the letter from number.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        static string GetLetterFromNumber(int number)
        {
            string result = string.Empty;
            switch (number)
            {
                case 1:
                    result = "A";
                    break;
                case 2:
                    result = "B";
                    break;
                case 3:
                    result = "C";
                    break;
                case 4:
                    result = "D";
                    break;
                case 5:
                    result = "E";
                    break;
                case 6:
                    result = "F";
                    break;
                case 7:
                    result = "G";
                    break;
                case 8:
                    result = "H";
                    break;
                case 9:
                    result = "I";
                    break;
                case 10:
                    result = "J";
                    break;
                default:
                    break;
            }
            return result;
        }

        /// <summary>
        /// Shows the shot result.
        /// </summary>
        /// <param name="shotresponse">The shotresponse.</param>
        /// <param name="c">The c.</param>
        /// <param name="playername">The playername.</param>
        public static void ShowShotResult(FireShotResponse shotresponse, Coordinate c, string playername)
        {
            String str = "";
            switch (shotresponse.ShotStatus)
            {
                case ShotStatus.Hit:
                    Console.ForegroundColor = ConsoleColor.Green;
                    str = playername + " fires a missile with target: " + GetLetterFromNumber(c.YCoordinate) + (c.XCoordinate) + " which got Hit";
                    break;
                case ShotStatus.HitAndSunk:
                    Console.ForegroundColor = ConsoleColor.Green;
                    str = playername + " fires a missile with target: " + GetLetterFromNumber(c.YCoordinate)+ (c.XCoordinate) + " which got Hit";
                    break;
                case ShotStatus.Invalid:
                    Console.ForegroundColor = ConsoleColor.Red;
                    str = playername + " has no more missiles left to launch";
                    break;
                case ShotStatus.Miss:
                case ShotStatus.Duplicate:
                    Console.ForegroundColor = ConsoleColor.White;
                    str = playername + " fires a missile with target: " + GetLetterFromNumber(c.YCoordinate) + (c.XCoordinate) + " which got Miss";
                    break;
                case ShotStatus.Victory:
                    Console.ForegroundColor = ConsoleColor.Green;
                    str = playername + " fires a missile with target: " + GetLetterFromNumber(c.YCoordinate) + (c.XCoordinate) + " which got Hit";
                    str +="\n" + playername + " won the battle!";                    
                    break;
            }
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
        }

        /// <summary>
        /// Resets the screen.
        /// </summary>
        public static void ResetScreen()
        {
            Console.Clear();
            ShowHeader();
        }
    }
}
