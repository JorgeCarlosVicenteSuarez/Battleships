namespace BattleShips.Logic
{
    /// <summary>
    /// Class of ship with 4 squares size
    /// </summary>
    public class Destroyer : Ship
    {
        public Destroyer() : base(4, CellContent.DESTROYER) {}
    }
}
