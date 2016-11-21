namespace BattleShips.Logic
{
    /// <summary>
    /// Class of ship with 5 squares size
    /// </summary>
    public class Battleship : Ship
    {
        public Battleship() : base(5, CellContent.BATTLESHIP) { }
    }
}
