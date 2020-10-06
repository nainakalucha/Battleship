using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using BattleShip.BLL.Ships;
using System;

namespace BattleShip.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class GameSetup
    {
        /// <summary>
        /// The gm
        /// </summary>
        GameState _gm;
        /// <summary>
        /// Initializes a new instance of the <see cref="GameSetup"/> class.
        /// </summary>
        /// <param name="gm">The gm.</param>
        public GameSetup(GameState gm)
        {
            _gm = gm;
        }

        /// <summary>
        /// Setups this instance.
        /// </summary>
        public void Setup()
        {
            ControlOutput.ShowHeader();
            string[] userSetUp = ControlInput.GetNameFromUser();
            _gm.Player1.Name = userSetUp[0];
            _gm.Player2.Name = userSetUp[1];
        }

        /// <summary>
        /// Sets the board.
        /// </summary>
        /// <param name="lines">The lines.</param>
        public void SetBoard(string[] lines)
        {
            ControlOutput.ResetScreen();
            _gm.Player1.PlayerBoard = new Board(Convert.ToInt32(lines[0].Split(' ')[0]), ControlInput.GetNumberFromLetter(lines[0].Split(' ')[1]), Convert.ToInt32(lines[1].Split(' ')[0]));           
            _gm.Player2.PlayerBoard = new Board(Convert.ToInt32(lines[0].Split(' ')[0]), ControlInput.GetNumberFromLetter(lines[0].Split(' ')[1]), Convert.ToInt32(lines[1].Split(' ')[0]));
            PlaceShipOnBoard(_gm.Player1, _gm.Player2, lines);
            Console.WriteLine("All ship were placed successfull! Press any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Places the ship on board.
        /// </summary>
        /// <param name="player1">The player1.</param>
        /// <param name="player2">The player2.</param>
        /// <param name="lines">The lines.</param>
        public void PlaceShipOnBoard(Player player1, Player player2, string[] lines)
        {
            Console.WriteLine("Reading the width, height and location of the ship. Ex:) 1 1 A2:");
            for (ShipType s = ShipType.Ship_Q; s <= ShipType.Ship_P; s++)
            {
                PlaceShipRequest ShipToPlacePlayer1 = new PlaceShipRequest();
                PlaceShipRequest ShipToPlacePlayer2 = new PlaceShipRequest();
                ShipPlacement result = ShipPlacement.NotEnoughSpace;
                ShipToPlacePlayer1 = ControlInput.GetLocation(Convert.ToInt32(lines[(int)s].Split(' ')[1]), Convert.ToInt32(lines[(int)s].Split(' ')[2]), lines[(int)s].Split(' ')[3]);
                ShipToPlacePlayer2 = ControlInput.GetLocation(Convert.ToInt32(lines[(int)s].Split(' ')[1]), Convert.ToInt32(lines[(int)s].Split(' ')[2]), lines[(int)s].Split(' ')[4]);
                ShipToPlacePlayer1.ShipType = s;
                ShipToPlacePlayer2.ShipType = s;
                result = player1.PlayerBoard.PlaceShip(ShipToPlacePlayer1);
                result = player2.PlayerBoard.PlaceShip(ShipToPlacePlayer2);
                if (result == ShipPlacement.NotEnoughSpace)
                    Console.WriteLine("Not Enough Space!");
                else if (result == ShipPlacement.Overlap)
                    Console.WriteLine("Overlap placement!");
            }
        }
    }
}
