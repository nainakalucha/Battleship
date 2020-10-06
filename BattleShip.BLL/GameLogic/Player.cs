using BattleShip.BLL.GameLogic;

namespace BattleShip.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name{ get; set;}

        /// <summary>
        /// The player board
        /// </summary>
        public Board PlayerBoard;
    }
}
