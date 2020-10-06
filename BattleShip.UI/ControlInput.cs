using BattleShip.BLL.Requests;
using System;

namespace BattleShip.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class ControlInput
    {
        /// <summary>
        /// Gets the name from user.
        /// </summary>
        /// <returns></returns>
        public static string[] GetNameFromUser()
        {
            string player1 = string.Empty, player2 = string.Empty;
            do
            {
                Console.Write("Input player 1 name: ");
                player1 = Console.ReadLine();
            } while (string.IsNullOrEmpty(player1.Trim()));

            do
            {
                Console.Write("Input player 2 name: ");
                player2 = Console.ReadLine();
            } while (string.IsNullOrEmpty(player2.Trim()));
            return new string[] { player1, player2 };
        }

        /// <summary>
        /// Gets the location.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="location">The location.</param>
        /// <returns></returns>
        public static PlaceShipRequest GetLocation(int width, int height, string location)
        {
            string strX, strY;
            int y;

            if (!string.IsNullOrEmpty(location))
            {
                strY = location.Substring(0, 1); ;
                strX = location.Substring(1);
                y = GetNumberFromLetter(strY);
                PlaceShipRequest ShipToPlace = new PlaceShipRequest();
                ShipToPlace.Coordinate = new Coordinate(Convert.ToInt32(strX), y);
                ShipToPlace.Width = width;
                ShipToPlace.Height = height;
                return ShipToPlace;
            }
            return null;
        }

        /// <summary>
        /// Gets the number from letter.
        /// </summary>
        /// <param name="letter">The letter.</param>
        /// <returns></returns>
        public static int GetNumberFromLetter(string letter)
        {
            int result = -1;
            switch (letter.ToLower())
            {
                case "a":
                    result = 1;
                    break;
                case "b":
                    result = 2;
                    break;
                case "c":
                    result = 3;
                    break;
                case "d":
                    result = 4;
                    break;
                case "e":
                    result = 5;
                    break;
                case "f":
                    result = 6;
                    break;
                case "g":
                    result = 7;
                    break;
                case "h":
                    result = 8;
                    break;
                case "i":
                    result = 9;
                    break;
                case "j":
                    result = 10;
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}
