namespace BattleShips.Logic
{
    public class Grid
    {
        private readonly int size;

        public Cell[,] Table { get; }

        public Grid(int size = 10)
        {
            this.size = size;
            this.Table = new Cell[size, size];
            for(var i = 0; i < size; i++)
                for (var j = 0; j < size; j++) this.Table[i, j] = new Cell();
        }

        /// <summary>
        /// Returns true if a ship has enough space to be set
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="length"></param>
        /// <param name="orientation">the orientation horizontal/vertical</param>
        /// <returns>True if the ship can be set in the position, false if cannot</returns>
        public bool EmptyCells(int x, int y, int length, bool orientation)
        {
            for (var i = 0; i < length; i++)
            {
                if (!orientation)
                {
                    if (this.Table[x + i, y].Content == CellContent.WATER) continue;
                }
                else
                {
                    if (this.Table[x, y + i].Content == CellContent.WATER) continue;
                }
                return false;
            }

            return true;
        }

        /// <summary>
        /// Sets ship cells (horizontal or vertical) as the ship cell types
        /// and the previous and later ships rows/columns cells as SHIP_BORDER
        /// </summary>
        /// <param name="ship">the ship to set</param>
        public void AddShip(Ship ship)
        {
            for (var i = -1; i <= ship.Size; i++)
            {
                if (!ship.Vertical)
                {
                    // Sets previous horizontal cells
                    this.SetCell(ship.PosX + i, ship.PosY - 1, CellContent.SHIP_BORDER);

                    // Sets horizontal ship cells
                    if (i == -1 || i == ship.Size)
                        this.SetCell(ship.PosX + i, ship.PosY, CellContent.SHIP_BORDER);
                    else
                        this.SetCell(ship.PosX + i, ship.PosY, ship.Content);

                    // Sets later horizontal ship cells
                    this.SetCell(ship.PosX + i, ship.PosY + 1, CellContent.SHIP_BORDER);
                }
                else
                {
                    // Sets previous vertical ship cells
                    this.SetCell(ship.PosX - 1, ship.PosY + i, CellContent.SHIP_BORDER);

                    // Sets vertical ship cells
                    if (i == -1 || i == ship.Size)
                        this.SetCell(ship.PosX, ship.PosY + i, CellContent.SHIP_BORDER);
                    else
                        this.SetCell(ship.PosX, ship.PosY + i, ship.Content);

                    // Sets later vertical ship cells
                    this.SetCell(ship.PosX + 1, ship.PosY + i, CellContent.SHIP_BORDER);
                }
            }
        }

        /// <summary>
        /// Sets a grid cell only if inside grid bounds, does nothing if is out of bounds
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="content">content to set</param>
        public void SetCell(int x, int y, CellContent content)
        {
            if (x >= 0 && x < this.size && y >= 0 && y < this.size) this.Table[x, y].Content = content;
        }

        /// <summary>
        /// Sets as Discovered a grid cell only if inside grid bounds, does nothing if is out of bounds
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public void DiscoverCell(int x, int y)
        {
            if (x >= 0 && x < this.size && y >= 0 && y < this.size) this.Table[x, y].Discovered = true;
        }
    }
}
