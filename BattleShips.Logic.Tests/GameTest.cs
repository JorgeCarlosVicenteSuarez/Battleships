namespace BattleShips.Logic.Tests
 {
    using System.Collections.Generic;

    using NUnit.Framework;

    [TestFixture]
    public class GameTest
    {
        [Test]
        public void GameInit()
        {
            var playerShips = new List<Ship> { new Battleship(), new Destroyer(), new Destroyer() };
            var computerShips = new List<Ship> { new Battleship(), new Destroyer(), new Destroyer() };

            var game = new Game(playerShips, computerShips);

            Assert.AreEqual(3, game.Player.Ships.Count);
            Assert.AreEqual(3, game.Computer.Ships.Count);
        }
    }
}
