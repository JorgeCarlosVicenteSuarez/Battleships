namespace BattleShips.Logic
{
    using System.Linq;

    /// <summary>
    /// Base class of a ship that contains the common methods implementation
    /// </summary>
    public abstract class Ship
    {
        /// <summary>
        /// Origin horizontal position of the ship
        /// </summary>
        public int PosX { get; protected set; }

        /// <summary>
        /// Origin vertical position of the ship
        /// </summary>
        public int PosY { get; protected set; }

        /// <summary>
        /// Indicates if the ship is positioned horizontal or vertical
        /// </summary>
        public bool Vertical { get; protected set; }

        /// <summary>
        /// Status of the individual cells of the ship
        /// </summary>
        public bool[] Cells { get; }

        /// <summary>
        /// Gets the ship size
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// Gets the ship cell type
        /// </summary>
        public CellContent Content { get; }

        /// <summary>
        /// Gets a value indicating if the ship was sunk
        /// 
        /// A ship is Sunk when all it's cells are true (touched)
        /// </summary>
        public bool Sunk { get { return this.Cells.All(c => c); } }

        /// <summary>
        /// Initializes a ship setting his position default to an invalid -1,-1 cell
        /// but setting it's size and initializing the ship cells
        /// </summary>
        /// <param name="size">The ship's size</param>
        /// <param name="content">The ship's cell content</param>
        protected Ship(int size, CellContent content)
        {
            this.PosX = -1;
            this.PosY = -1;
            this.Size = size;
            this.Content = content;
            this.Cells = new bool[this.Size];
            for(var i = 0; i < size; i++) this.Cells[i] = false;
        }

        /// <summary>
        /// Sets the coordinates and orientation of the ship
        /// </summary>
        /// <param name="x">horizontal coordinate of the ship</param>
        /// <param name="y">vertical coordinate of the ship</param>
        /// <param name="vertical">orientation of the ship</param>
        public void Locate(int x, int y, bool vertical)
        {
            this.PosX = x;
            this.PosY = y;
            this.Vertical = vertical;
        }

        /// <summary>
        /// Tries a shot on the ship returning true if it was touched
        /// and false if it didn't
        /// </summary>
        /// <param name="x">horizontal coordinate of the shoot</param>
        /// <param name="y">vertical coordinate of the shoot</param>
        /// <returns>true if it was touched or false if it wasn't</returns>
        public bool Shoot(int x, int y)
        {
            if (x < this.PosX) return false;
            if (y < this.PosY) return false;

            if (x == this.PosX)
            {
                if (y >= this.PosY + this.Size) return false;
                this.Cells[y - this.PosY] = true;
                return true;
            }

            if (y == this.PosY)
            {
                if (x >= this.PosX + this.Size) return false;
                this.Cells[x - this.PosX] = true;
                return true;
            }

            return false;
        }
    }
}
