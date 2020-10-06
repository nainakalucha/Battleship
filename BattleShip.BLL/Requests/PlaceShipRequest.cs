using BattleShip.BLL.Ships;

namespace BattleShip.BLL.Requests
{
    /// <summary>
    /// 
    /// </summary>
    public class PlaceShipRequest
    {
        /// <summary>
        /// Gets or sets the coordinate.
        /// </summary>
        /// <value>
        /// The coordinate.
        /// </value>
        public Coordinate Coordinate { get; set; }
        /// <summary>
        /// Gets or sets the type of the ship.
        /// </summary>
        /// <value>
        /// The type of the ship.
        /// </value>
        public ShipType ShipType { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public int Width { get; set; }
        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public int Height { get; set; }
    }
}
