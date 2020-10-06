using BattleShip.BLL.Requests;
using BattleShip.BLL.Ships;

namespace BattleShip.BLL.GameLogic
{
    /// <summary>
    /// 
    /// </summary>
    public class ShipCreator
    {
        /// <summary>
        /// Creates the ship.
        /// </summary>
        /// <param name="ship">The ship.</param>
        /// <returns></returns>
        public static Ship CreateShip(PlaceShipRequest ship)
        {
            switch (ship.ShipType)
            {
                case ShipType.Ship_P:
                    return new Ship(ShipType.Ship_P, ship.Width, ship.Height);
                case ShipType.Ship_Q:
                    return new Ship(ShipType.Ship_Q, ship.Width, ship.Height);
                default: return null;
            }
        }
    }
}
