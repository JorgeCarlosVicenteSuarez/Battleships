namespace BattleShips.Logic.Tests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class GridTests
    {
        [Test]
        public void SetCellInBounds()
        {
            var grid = new Grid();

            grid.SetCell(0, 0, CellContent.SHIP_BORDER);
            grid.SetCell(9, 0, CellContent.SHIP_BORDER);
            grid.SetCell(5, 4, CellContent.SHIP_BORDER);
            grid.SetCell(0, 9, CellContent.SHIP_BORDER);
            grid.SetCell(9, 9, CellContent.SHIP_BORDER);

            Assert.AreEqual(CellContent.SHIP_BORDER, grid.Table[0, 0].Content);
            Assert.AreEqual(CellContent.WATER, grid.Table[0, 1].Content);
            Assert.AreEqual(CellContent.WATER, grid.Table[1, 0].Content);
            Assert.AreEqual(CellContent.WATER, grid.Table[1, 1].Content);
            Assert.AreEqual(CellContent.SHIP_BORDER, grid.Table[0, 9].Content);
            Assert.AreEqual(CellContent.SHIP_BORDER, grid.Table[5, 4].Content);
            Assert.AreEqual(CellContent.SHIP_BORDER, grid.Table[9, 0].Content);
            Assert.AreEqual(CellContent.SHIP_BORDER, grid.Table[9, 9].Content);
        }

        [Test]
        public void SetCellOutOfBoundsNotFails()
        {
            var grid = new Grid();

            Assert.DoesNotThrow(() => grid.SetCell(-1, 0, CellContent.SHIP_BORDER));
            Assert.DoesNotThrow(() => grid.SetCell(10, 0, CellContent.SHIP_BORDER));
            Assert.DoesNotThrow(() => grid.SetCell(0, 10, CellContent.SHIP_BORDER));
            Assert.DoesNotThrow(() => grid.SetCell(10, 10, CellContent.SHIP_BORDER));
        }

        [Test]
        public void EmptyCellsOutOfBoundsFails()
        {
            var grid = new Grid();

            Assert.Throws<IndexOutOfRangeException>(() => grid.EmptyCells(-1, 0, 4, false));
            Assert.Throws<IndexOutOfRangeException>(() => grid.EmptyCells(7, 0, 4, false));
            Assert.Throws<IndexOutOfRangeException>(() => grid.EmptyCells(0, 10, 4, true));
            Assert.Throws<IndexOutOfRangeException>(() => grid.EmptyCells(6, 10, 4, true));
        }

        [Test]
        public void ShipCanBeSetInEmptyGrid_InAnyCorner_Horizontal()
        {
            var grid = new Grid();
            var destroyer1 = new Destroyer();
            var destroyer2 = new Destroyer();
            var destroyer3 = new Destroyer();
            var destroyer4 = new Destroyer();

            var empty1 = grid.EmptyCells(0, 0, destroyer1.Size, false);
            Assert.IsTrue(empty1);
            destroyer1.Locate(0, 0, false);
            Assert.DoesNotThrow(() => grid.AddShip(destroyer1));

            var empty2 = grid.EmptyCells(6, 0, destroyer2.Size, false);
            Assert.IsTrue(empty2);
            destroyer2.Locate(0, 6, false);
            Assert.DoesNotThrow(() => grid.AddShip(destroyer2));

            var empty3 = grid.EmptyCells(0, 9, destroyer3.Size, false);
            Assert.IsTrue(empty3);
            destroyer3.Locate(0, 9, false);
            Assert.DoesNotThrow(() => grid.AddShip(destroyer3));

            var empty4 = grid.EmptyCells(6, 9, destroyer4.Size, false);
            Assert.IsTrue(empty4);
            destroyer4.Locate(6, 9, false);
            Assert.DoesNotThrow(() => grid.AddShip(destroyer4));
        }

        [Test]
        public void ShipCanBeSetInEmptyGrid_InAnyCorner_Vertical()
        {
            var grid = new Grid();
            var destroyer1 = new Destroyer();
            var destroyer2 = new Destroyer();
            var destroyer3 = new Destroyer();
            var destroyer4 = new Destroyer();

            var empty1 = grid.EmptyCells(0, 0, destroyer1.Size, true);
            Assert.IsTrue(empty1);
            destroyer1.Locate(0, 0, true);
            Assert.DoesNotThrow(() => grid.AddShip(destroyer1));

            var empty2 = grid.EmptyCells(0, 6, destroyer2.Size, true);
            Assert.IsTrue(empty2);
            destroyer2.Locate(0, 6, true);
            Assert.DoesNotThrow(() => grid.AddShip(destroyer2));

            var empty3 = grid.EmptyCells(9, 0, destroyer3.Size, true);
            Assert.IsTrue(empty3);
            destroyer3.Locate(9, 0, true);
            Assert.DoesNotThrow(() => grid.AddShip(destroyer3));

            var empty4 = grid.EmptyCells(9, 6, destroyer4.Size, true);
            Assert.IsTrue(empty4);
            destroyer4.Locate(9, 6, true);
            Assert.DoesNotThrow(() => grid.AddShip(destroyer4));
        }

        [Test]
        public void AllCellsCorrectlySetAroundShip()
        {
            var grid = new Grid();

            var destroyer = new Destroyer();
            var empty1 = grid.EmptyCells(4, 4, destroyer.Size, false);
            Assert.IsTrue(empty1);
            destroyer.Locate(4, 4, false);
            Assert.DoesNotThrow(() => grid.AddShip(destroyer));

            Assert.AreEqual(CellContent.SHIP_BORDER, grid.Table[3, 3].Content);
            Assert.AreEqual(CellContent.SHIP_BORDER, grid.Table[4, 3].Content);
            Assert.AreEqual(CellContent.SHIP_BORDER, grid.Table[5, 3].Content);
            Assert.AreEqual(CellContent.SHIP_BORDER, grid.Table[6, 3].Content);
            Assert.AreEqual(CellContent.SHIP_BORDER, grid.Table[7, 3].Content);
            Assert.AreEqual(CellContent.SHIP_BORDER, grid.Table[8, 3].Content);

            Assert.AreEqual(CellContent.SHIP_BORDER, grid.Table[3, 4].Content);
            Assert.AreEqual(destroyer.Content, grid.Table[4, 4].Content);
            Assert.AreEqual(destroyer.Content, grid.Table[5, 4].Content);
            Assert.AreEqual(destroyer.Content, grid.Table[6, 4].Content);
            Assert.AreEqual(destroyer.Content, grid.Table[7, 4].Content);
            Assert.AreEqual(CellContent.SHIP_BORDER, grid.Table[8, 4].Content);

            Assert.AreEqual(CellContent.SHIP_BORDER, grid.Table[3, 5].Content);
            Assert.AreEqual(CellContent.SHIP_BORDER, grid.Table[4, 5].Content);
            Assert.AreEqual(CellContent.SHIP_BORDER, grid.Table[5, 5].Content);
            Assert.AreEqual(CellContent.SHIP_BORDER, grid.Table[6, 5].Content);
            Assert.AreEqual(CellContent.SHIP_BORDER, grid.Table[7, 5].Content);
            Assert.AreEqual(CellContent.SHIP_BORDER, grid.Table[8, 5].Content);
        }

        [Test]
        public void TwoShipsOverlapDetected()
        {
            var grid = new Grid();

            var destroyer = new Destroyer();
            var battleship = new Battleship();

            var empty1 = grid.EmptyCells(4, 4, destroyer.Size, false);
            Assert.IsTrue(empty1);
            destroyer.Locate(4, 4, false);
            Assert.DoesNotThrow(() => grid.AddShip(destroyer));

            var notEmpty1 = grid.EmptyCells(5, 0, battleship.Size, true);
            Assert.IsFalse(notEmpty1);

            var notEmpty2 = grid.EmptyCells(3, 0, battleship.Size, true);
            Assert.IsFalse(notEmpty2);
        }

        [Test]
        public void ShipCanBeSetInEmptyGrid_InTheMiddle()
        {
            var grid = new Grid();

            Assert.DoesNotThrow(() => grid.SetCell(-1, 0, CellContent.SHIP_BORDER));
            Assert.DoesNotThrow(() => grid.SetCell(10, 0, CellContent.SHIP_BORDER));
            Assert.DoesNotThrow(() => grid.SetCell(0, 10, CellContent.SHIP_BORDER));
            Assert.DoesNotThrow(() => grid.SetCell(10, 10, CellContent.SHIP_BORDER));
        }
    }
}
