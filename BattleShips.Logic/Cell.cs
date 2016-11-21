namespace BattleShips.Logic
{
    /// <summary>
    /// Represents a battlefield cell
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// Gets or Sets a value indicating whether this cell has been discovered or not
        /// </summary>
        public bool Discovered { get; set; }

        /// <summary>
        /// Gets or sets a value with the content of the cell: Water, ship, etc...
        /// </summary>
        public CellContent Content { get; set; }
    }
}
