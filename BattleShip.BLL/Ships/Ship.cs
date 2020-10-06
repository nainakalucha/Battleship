using System.Linq;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;

namespace BattleShip.BLL.Ships
{
    /// <summary>
    /// 
    /// </summary>
    public class Ship
    {
        /// <summary>
        /// Gets the type of the ship.
        /// </summary>
        /// <value>
        /// The type of the ship.
        /// </value>
        public ShipType ShipType { get; private set; }
        /// <summary>
        /// Gets the name of the ship.
        /// </summary>
        /// <value>
        /// The name of the ship.
        /// </value>
        public string ShipName { get { return ShipType.ToString(); } }
        /// <summary>
        /// Gets or sets the board positions.
        /// </summary>
        /// <value>
        /// The board positions.
        /// </value>
        public Coordinate[] BoardPositions { get; set; }

        /// <summary>
        /// The life remaining
        /// </summary>
        private int _lifeRemaining;
        /// <summary>
        /// Gets a value indicating whether this instance is sunk.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is sunk; otherwise, <c>false</c>.
        /// </value>
        public bool IsSunk { get { return _lifeRemaining == 0; } }

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

        /// <summary>
        /// Initializes a new instance of the <see cref="Ship"/> class.
        /// </summary>
        /// <param name="shipType">Type of the ship.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public Ship(ShipType shipType, int width, int height)
        {
            ShipType = shipType;
            Width = width;
            Height = height;
            _lifeRemaining = width * height;
            BoardPositions = new Coordinate[width*height];
        }

        /// <summary>
        /// Fires at ship.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns></returns>
        public ShotStatus FireAtShip(Coordinate position)
        {
            if (BoardPositions.Contains(position))
            {
                _lifeRemaining--;

                if(_lifeRemaining == 0)
                    return ShotStatus.HitAndSunk;

                return ShotStatus.Hit;
            }

            return ShotStatus.Miss;
        }
    }
}
