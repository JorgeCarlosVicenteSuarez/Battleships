namespace BattleShips.Logic.Tests
{
    using NUnit.Framework;
    using System.Linq;

    [TestFixture]
    public class BattleFieldTests
    {
        [Test]
        public void BattleFieldAddDestroyer()
        {
            var battlefield = new BattleField();
            var battleship = new Destroyer();

            var added1 = battlefield.AddShip(battleship);
            Assert.IsTrue(added1);

            Assert.AreEqual(1, battlefield.Ships.Count);

            var firstShip = battlefield.Ships.First();
            Assert.IsNotNull(firstShip);
            Assert.IsInstanceOf(typeof(Ship), firstShip);
            Assert.IsInstanceOf(typeof(Destroyer), firstShip);

            Assert.GreaterOrEqual(firstShip.PosX, 0);
            Assert.Less(firstShip.PosX, 10);
            Assert.GreaterOrEqual(firstShip.PosY, 0);
            Assert.Less(firstShip.PosY, 10);
        }

        [Test]
        public void BattleFieldAddThreeShips()
        {
            var battlefield = new BattleField();

            var addShip1 = battlefield.AddShip(new Battleship());
            var addShip2 = battlefield.AddShip(new Destroyer());
            var addShip3 = battlefield.AddShip(new Destroyer());

            Assert.IsTrue(addShip1);
            Assert.IsTrue(addShip2);
            Assert.IsTrue(addShip3);
        }

        [Test]
        public void ShootInWater()
        {
            var battlefield = new BattleField();

            var result = battlefield.Shoot(2, 2);
            Assert.AreEqual(CellContent.WATER, result);
            Assert.AreEqual(CellContent.WATER, battlefield.Grid[2, 2].Content);
            Assert.IsTrue(battlefield.Grid[2, 2].Discovered);
        }

        [Test]
        public void SingleShootInShip()
        {
            var battlefield = new BattleField();
            var destroyer = new Destroyer();

            var added = battlefield.AddShip(destroyer);
            Assert.IsTrue(added);

            var result = battlefield.Shoot(destroyer.PosX, destroyer.PosY);
            Assert.AreEqual(destroyer.Content, result);
            Assert.AreEqual(destroyer.Content, battlefield.Grid[destroyer.PosX, destroyer.PosY].Content);
            Assert.IsTrue(battlefield.Grid[destroyer.PosX, destroyer.PosY].Discovered);
            Assert.IsFalse(destroyer.Sunk);
        }

        [Test]
        public void SunkShip()
        {
            var battlefield = new BattleField();
            var destroyer = new Destroyer();

            var added = battlefield.AddShip(destroyer);
            Assert.IsTrue(added);

            if (!destroyer.Vertical)
            {
                for (var i = 0; i < destroyer.Size; i++)
                {
                    var result = battlefield.Shoot(destroyer.PosX + i, destroyer.PosY);
                    Assert.AreEqual(destroyer.Content, result);
                }

                Assert.AreEqual(destroyer.Content, battlefield.Grid[destroyer.PosX, destroyer.PosY].Content);
                Assert.IsTrue(battlefield.Grid[destroyer.PosX, destroyer.PosY].Discovered);
                Assert.AreEqual(destroyer.Content, battlefield.Grid[destroyer.PosX + 1, destroyer.PosY].Content);
                Assert.IsTrue(battlefield.Grid[destroyer.PosX + 1, destroyer.PosY].Discovered);
                Assert.AreEqual(destroyer.Content, battlefield.Grid[destroyer.PosX + 2, destroyer.PosY].Content);
                Assert.IsTrue(battlefield.Grid[destroyer.PosX + 2, destroyer.PosY].Discovered);
                Assert.AreEqual(destroyer.Content, battlefield.Grid[destroyer.PosX + 3, destroyer.PosY].Content);
                Assert.IsTrue(battlefield.Grid[destroyer.PosX + 3, destroyer.PosY].Discovered);
            }
            else
            {
                for (var i = 0; i < destroyer.Size; i++)
                {
                    var result = battlefield.Shoot(destroyer.PosX, destroyer.PosY + i);
                    Assert.AreEqual(destroyer.Content, result);
                }

                Assert.AreEqual(destroyer.Content, battlefield.Grid[destroyer.PosX, destroyer.PosY].Content);
                Assert.IsTrue(battlefield.Grid[destroyer.PosX, destroyer.PosY].Discovered);
                Assert.AreEqual(destroyer.Content, battlefield.Grid[destroyer.PosX, destroyer.PosY + 1].Content);
                Assert.IsTrue(battlefield.Grid[destroyer.PosX, destroyer.PosY + 1].Discovered);
                Assert.AreEqual(destroyer.Content, battlefield.Grid[destroyer.PosX, destroyer.PosY + 2].Content);
                Assert.IsTrue(battlefield.Grid[destroyer.PosX, destroyer.PosY + 2].Discovered);
                Assert.AreEqual(destroyer.Content, battlefield.Grid[destroyer.PosX, destroyer.PosY + 3].Content);
                Assert.IsTrue(battlefield.Grid[destroyer.PosX, destroyer.PosY + 3].Discovered);
            }

            Assert.IsTrue(destroyer.Sunk);
        }
    }
}
