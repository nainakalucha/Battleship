namespace BattleShip.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class GameState
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is first player1.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is first player1; otherwise, <c>false</c>.
        /// </value>
        public bool IsFirstPlayer1 { get; set; }
        /// <summary>
        /// Gets or sets the player1.
        /// </summary>
        /// <value>
        /// The player1.
        /// </value>
        public Player Player1 { get; set; }
        /// <summary>
        /// Gets or sets the player2.
        /// </summary>
        /// <value>
        /// The player2.
        /// </value>
        public Player Player2 { get; set; }
    }
}
