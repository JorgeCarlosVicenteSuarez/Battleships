namespace BattleShips.Logic.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class ShipTests
    {
        [Test]
        public void ShipNotSunkWhenCreated()
        {
            var destroyer = new Destroyer();
            var battleship = new Battleship();

            Assert.IsFalse(destroyer.Sunk);
            Assert.IsFalse(battleship.Sunk);
        }

        [Test]
        public void ShipLocationInvalidWhenCreated()
        {
            var destroyer = new Destroyer();
            var battleship = new Battleship();

            Assert.AreEqual(-1, destroyer.PosX);
            Assert.AreEqual(-1, destroyer.PosY);
            Assert.AreEqual(-1, battleship.PosX);
            Assert.AreEqual(-1, battleship.PosY);
        }

        [Test]
        public void ShipSizeAssignedWhenCreated()
        {
            var destroyer = new Destroyer();
            var battleship = new Battleship();

            Assert.AreEqual(4, destroyer.Size);
            Assert.AreEqual(5, battleship.Size);
        }

        [Test]
        public void ShipLocationAssignedCorrectly()
        {
            var destroyer = new Destroyer();

            destroyer.Locate(5, 2, true);

            Assert.AreEqual(5, destroyer.PosX);
            Assert.AreEqual(2, destroyer.PosY);
        }

        [Test]
        public void DestroyerSunkInFourShoots()
        {
            var destroyer = new Destroyer();

            destroyer.Locate(0, 0, false);
            var touched1 = destroyer.Shoot(0, 0);
            var touched2 = destroyer.Shoot(1, 0);
            var touched3 = destroyer.Shoot(2, 0);
            var touched4 = destroyer.Shoot(3, 0);

            Assert.IsTrue(touched1);
            Assert.IsTrue(touched2);
            Assert.IsTrue(touched3);
            Assert.IsTrue(touched4);
            Assert.IsTrue(destroyer.Sunk);
        }

        [Test]
        public void BattleshipSunkInFiveShoots()
        {
            var battleship = new Battleship();

            battleship.Locate(0, 0, true);
            var touched1 = battleship.Shoot(0, 0);
            var touched2 = battleship.Shoot(0, 1);
            var touched3 = battleship.Shoot(0, 2);
            var touched4 = battleship.Shoot(0, 3);
            var touched5 = battleship.Shoot(0, 4);

            Assert.IsTrue(touched1);
            Assert.IsTrue(touched2);
            Assert.IsTrue(touched3);
            Assert.IsTrue(touched4);
            Assert.IsTrue(touched5);
            Assert.IsTrue(battleship.Sunk);
        }


        [Test]
        public void BattleshipNotSunkFailingTwoShoots()
        {
            var battleship = new Battleship();

            battleship.Locate(0, 0, true);
            var touched1 = battleship.Shoot(0, 0);
            var notTouched1 = battleship.Shoot(1, 1);
            var touched2 = battleship.Shoot(0, 2);
            var touched3 = battleship.Shoot(0, 3);
            var notTouched2 = battleship.Shoot(0, 5);

            Assert.IsTrue(touched1);
            Assert.IsTrue(touched2);
            Assert.IsTrue(touched3);
            Assert.IsFalse(notTouched1);
            Assert.IsFalse(notTouched2);
            Assert.IsFalse(battleship.Sunk);
        }
    }
}
