namespace BattleShip.UI
{
    /// <summary>
    /// 
    /// </summary>
    class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            GameFlow flow = new GameFlow();
            flow.Start();
        }
    }
}
