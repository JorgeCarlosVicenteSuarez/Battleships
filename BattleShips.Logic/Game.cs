namespace BattleShips.Logic
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Manages the game logic
    ///
    /// Player/Computer turnover
    /// End of game
    /// Computer's logic (very primitive, as everything is random)
    /// </summary>
    public class Game
    {
        /// <summary>
        /// The player's battlefield (computer will play over this)
        /// </summary>
        public BattleField Player { get; set; }

        /// <summary>
        /// The computer's battlefield (player will play over this)
        /// </summary>
        public BattleField Computer { get; set; }

        /// <summary>
        /// Gets a value indicating if the game is over (computer or player won)
        /// </summary>
        public bool GameFinished => this.Computer.AllShipsSunk || this.Player.AllShipsSunk;

        /// <summary>
        /// Initializes the game with the list of ships provided
        /// </summary>
        /// <param name="playerShips">The list of ships to initialize the battlefield</param>
        /// <param name="computerShips">The list of ships to initialize the battlefield</param>
        /// <param name="size">The size of the grid</param>
        public Game(IEnumerable<Ship> playerShips, IEnumerable<Ship> computerShips, int size = 10)
        {
            this.Player = new BattleField(size);
            foreach (var ship in playerShips)
            {
                this.Player.AddShip(ship);
            }

            this.Computer = new BattleField(size);
            foreach (var ship in computerShips)
            {
                this.Computer.AddShip(ship);
            }
        }

        /// <summary>
        /// Makes a players shoot against the computer's battlefield
        ///
        /// If the cell was already discovered simple returns
        /// </summary>
        /// <exception cref="InvalidOperationException">The game is over</exception>
        public void UserShoot(int x, int y)
        {
            if (this.GameFinished)
            {
                throw new InvalidOperationException("The game is over");
            }

            if (!this.Computer.Grid[x, y].Discovered)
            {
                this.Computer.Shoot(x, y);
            }
        }

        /// <summary>
        /// Makes a computer shoot against the player's battlefield
        /// </summary>
        /// <exception cref="InvalidOperationException">The game is over</exception>
        public void ComputerShoot()
        {
            if (this.GameFinished)
            {
                throw new InvalidOperationException("The game is over");
            }

            var maxCells = this.Player.Grid.GetLength(0)*this.Player.Grid.GetLength(1);
            var random = new Random((int)DateTime.Now.Ticks);
            var x = random.Next(0, this.Player.Grid.GetLength(0));
            var y = random.Next(0, this.Player.Grid.GetLength(1));

            var iteration = 0;
            while (iteration < maxCells)
            {
                var posX = (x + iteration) % this.Player.Grid.GetLength(0);
                var posY = (y * this.Player.Grid.GetLength(1) + iteration) / this.Player.Grid.GetLength(1);
                if (!this.Player.Grid[posX, posY].Discovered)
                {
                    this.Player.Shoot(posX, posY);
                    return;
                }
                iteration++;
            }
        }
    }
}
