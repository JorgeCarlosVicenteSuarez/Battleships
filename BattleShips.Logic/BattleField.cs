using System.Linq;

namespace BattleShips.Logic
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class that represents a user's or computer's battle field
    ///
    /// A table of 10x10 cells containing the ships/water or game discovered/hidden cells
    ///
    /// The cell type is passed as a parameter to the generic class
    /// </summary>
    public class BattleField
    {
        private readonly int size;

        private readonly Grid grid;

        public Cell [,]Grid => this.grid.Table;

        public IList<Ship> Ships { get; }

        /// <summary>
        /// Initializes a battlefield class with a grid size
        /// </summary>
        /// <param name="size"></param>
        public BattleField(int size = 10)
        {
            this.size = size;
            this.grid = new Grid(size);
            this.Ships = new List<Ship>();
        }

        /// <summary>
        /// Adds a ship to the battlefield returning true if the ship could be added
        /// or false if there was not space in the battlefield
        /// </summary>
        /// <param name="ship">The ship to be added to the battlefield</param>
        /// <returns>True if the ship was added, false if there was not space</returns>
        public bool AddShip(Ship ship)
        {
            if (ship == null) throw new ArgumentNullException(nameof(ship));
            if (ship.Size > this.size)
            {
                return false;
            }

            var count = 100000;
            do
            {
                if (this.SetShipValidPosition(ship))
                {
                    this.Ships.Add(ship);
                    return true;
                }
            } while (--count > 0);

            return false;
        }

        /// <summary>
        /// Shoots one cell changing the status of the battlefield
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public CellContent Shoot(int x, int y)
        {
            if (x < 0 || x >= this.size) throw new ArgumentOutOfRangeException(nameof(x));
            if (y < 0 || y >= this.size) throw new ArgumentOutOfRangeException(nameof(y));

            this.Grid[x, y].Discovered = true;
            var content = this.Grid[x, y].Content;
            if (content != CellContent.WATER && content != CellContent.SHIP_BORDER)
            {
                for (var i = 0; i < this.Ships.Count; i++)
                {
                    var shot = this.Ships[i].Shoot(x, y);
                    if (shot)
                    {
                        if (this.Ships[i].Sunk)
                        {
                            this.DiscoverShip(this.Ships[i]);
                        }
                        return content;
                    }
                }

                if (this.Ships.Any(ship => ship.Shoot(x, y)))
                {
                    return content;
                }
            }

            return content;
        }

        public bool AllShipsSunk => this.Ships.All(s => s.Sunk);

        /// <summary>
        /// Calculates a valid initial position for horizontal or vertical ships
        ///
        /// The orientation and initial position is obtained randomly
        /// </summary>
        /// <param name="ship">The ship to calculate the position</param>
        /// <returns>True if the position has been correctly obtained and the ship location updated</returns>
        private bool SetShipValidPosition(Ship ship)
        {
            var random = new Random((int)DateTime.Now.Ticks);
            var orientation = random.Next(2);
            var pos1 = random.Next(0, this.size - (orientation == 0 ? ship.Size + 1 : 0));
            var pos2 = random.Next(0, this.size - (orientation == 1 ? ship.Size + 1: 0));

            if (this.grid.EmptyCells(pos1, pos2, ship.Size, orientation > 0))
            {
                ship.Locate(pos1, pos2, orientation >0);
                this.grid.AddShip(ship);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Discovers all the ship positions and the surrounding ones
        /// </summary>
        /// <param name="ship"></param>
        private void DiscoverShip(Ship ship)
        {
            if (!ship.Vertical)
            {
                for (var i = ship.PosX - 1; i <= ship.PosX + ship.Size; i++)
                {
                    this.grid.DiscoverCell(i, ship.PosY - 1);
                    this.grid.DiscoverCell(i, ship.PosY);
                    this.grid.DiscoverCell(i, ship.PosY + 1);
                }
            }
            else
            {
                for (var i = ship.PosY - 1; i <= ship.PosY + ship.Size; i++)
                {
                    this.grid.DiscoverCell(ship.PosX - 1, i);
                    this.grid.DiscoverCell(ship.PosX, i);
                    this.grid.DiscoverCell(ship.PosX + 1, i);
                }
            }
        }
    }
}
