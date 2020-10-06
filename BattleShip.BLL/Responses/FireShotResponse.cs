namespace BattleShip.BLL.Responses
{
    /// <summary>
    /// 
    /// </summary>
    public class FireShotResponse
    {
        /// <summary>
        /// Gets or sets the shot status.
        /// </summary>
        /// <value>
        /// The shot status.
        /// </value>
        public ShotStatus ShotStatus { get; set; }
        /// <summary>
        /// Gets or sets the ship impacted.
        /// </summary>
        /// <value>
        /// The ship impacted.
        /// </value>
        public string ShipImpacted { get; set; }
    }
}
