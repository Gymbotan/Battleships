using Battleships.Players;
using NUnit.Framework;
using System;

namespace BattleshipsTests
{
    public class GridsManagerTests
    {
        [Test]
        public void SetShip_WrongShipIndex_ThrowArgumentOutOfRangeException()
        {
            GridsManager manager = new();
            Assert.Throws<ArgumentOutOfRangeException>(() => manager.SetShip(-1,1,1,1,5), 
                message: $"ShipIndex should be between 1 and {manager.Ships.Length}.");
        }

        [Test]
        public void SetShip_WrongShipIndex2_ThrowArgumentOutOfRangeException()
        {
            GridsManager manager = new();
            Assert.Throws<ArgumentOutOfRangeException>(() => manager.SetShip(manager.Ships.Length + 1, 1, 1, 1, 5),
                message: $"ShipIndex should be between 1 and {manager.Ships.Length}.");
        }

        [Test]
        public void SetShip_DifferentRowAndColumn_ThrowArgumentException()
        {
            GridsManager manager = new();
            Assert.Throws<ArgumentException>(() => manager.SetShip(1, 1, 1, 5, 5),
                message: "A ship should have the same row or the same column.");
        }

        [Test]
        public void SetShip_WrongShipSize_ThrowArgumentException()
        {
            GridsManager manager = new();
            Assert.Throws<ArgumentException>(() => manager.SetShip(1, 1, 1, 1, 8),
                message: $"This ship has different size.");
        }

        [TestCase(1, 12, 1, 12, 5)]
        [TestCase(2, -1, 3, 2, 3)]
        [TestCase(3, 2, 8, 2, 11)]
        [TestCase(2, 6, -1, 6, 2)]
        [TestCase(1, 4, -1, 4, -5)]
        [TestCase(2, 3, 13, 6, 13)]
        public void SetShip_SetShipOutOfGrid_ThrowArgumentException(int shipIndex, int row1, int column1, int row2, int column2)
        {
            GridsManager manager = new();
            Assert.Throws<ArgumentOutOfRangeException>(() => manager.SetShip(shipIndex, row1, column1, row2, column2));
        }

        [Test]
        public void SetShip_ShipsCollision_ThrowArgumentException()
        {
            GridsManager manager = new();
            manager.SetShip(1, 1, 1, 1, 5);
            Assert.Throws<ArgumentException>(() => manager.SetShip(2, 1, 1, 4, 1),
                message: $"You can not place your ship here. You collide with another ship. please try again.");
        }

        [Test]
        public void SetShip_ShipsCollision2_ThrowArgumentException()
        {
            GridsManager manager = new();
            manager.SetShip(2, 5, 1, 5, 4);
            Assert.Throws<ArgumentException>(() => manager.SetShip(1, 2, 4, 6, 4),
                message: $"You can not place your ship here. You collide with another ship. please try again.");
        }

        [TestCaseSource(typeof(TestCasesDataSource), nameof(TestCasesDataSource.TestCasesForShipPlacing))]
        public void SetShip_CorrectWork(int shipIndex, int row1, int column1, int row2, int column2, int[,] expected)
        {
            GridsManager manager = new();
            manager.SetShip(shipIndex, row1, column1, row2, column2);
            var actual = manager.GetShipsPlacement();
            Assert.AreEqual(expected, actual);
        }

        [TestCase(1, 1, 1, 1, 5)]
        [TestCase(2, 3, 3, 6, 3)]
        [TestCase(3, 10, 4, 10, 7)]
        public void SetShip_CorrectWorkInOppositeSequence(int shipIndex, int row1, int column1, int row2, int column2)
        {
            GridsManager manager1 = new();
            manager1.SetShip(shipIndex, row1, column1, row2, column2);

            GridsManager manager2 = new();
            manager2.SetShip(shipIndex, row2, column2, row1, column1);

            Assert.AreEqual(manager1.GetShipsPlacement(), manager2.GetShipsPlacement());
        }

        [TestCase(1, 1, 1, 1, 5)]
        [TestCase(2, 3, 3, 6, 3)]
        public void CorrectDeleting(int shipIndex, int row1, int column1, int row2, int column2)
        {
            GridsManager manager = new();
            manager.SetShip(shipIndex, row1, column1, row2, column2);
            manager.DeleteShips();
            var actual = manager.GetShipsPlacement();
            var expected = new int[,] { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
            Assert.AreEqual(expected, actual);
        }

        [TestCaseSource(typeof(TestCasesDataSource), nameof(TestCasesDataSource.TestCasesForShipPlacingAfterDeleting))]
        public void CorrectSetAfterDeleting(int shipIndex, int row1, int column1, int row2, int column2, int newShipIndex, int newRow1, int newColumn1, int newRow2, int newColumn2, int[,] expected)
        {
            GridsManager manager = new();
            manager.SetShip(shipIndex, row1, column1, row2, column2);
            manager.DeleteShips();
            manager.SetShip(newShipIndex, newRow1, newColumn1, newRow2, newColumn2);
            var actual = manager.GetShipsPlacement();
            Assert.AreEqual(expected, actual);
        }

        [TestCase(12, 1, 12, 5)]
        [TestCase(-1, 3, 2, 3)]
        [TestCase(2, 8, 2, 11)]
        [TestCase(6, -1, 6, 2)]
        [TestCase(4, -1, 4, -5)]
        [TestCase(3, 13, 6, 13)]
        public void IsPossibleToSetShip_SetShipOutOfGrid_ThrowArgumentException(int row1, int column1, int row2, int column2)
        {
            GridsManager manager = new();
            Assert.Throws<ArgumentOutOfRangeException>(() => manager.IsPossibleToSetShip(row1, column1, row2, column2));
        }

        [TestCase(1, 1, 1, 4, ExpectedResult = true)]
        [TestCase(2, 5, 2, 8, ExpectedResult = false)]
        [TestCase(4, 3, 4, 6, ExpectedResult = false)]
        [TestCase(8, 1, 8, 4, ExpectedResult = false)]
        [TestCase(4, 4, 7, 4, ExpectedResult = true)]
        [TestCase(3, 5, 6, 5, ExpectedResult = true)]
        [TestCase(2, 7, 5, 7, ExpectedResult = true)]
        [TestCase(6, 4, 6, 7, ExpectedResult = true)]
        [TestCase(9, 7, 9, 10, ExpectedResult = true)]
        public bool IsPossibleToSetShip_CorrectWork(int row1, int column1, int row2, int column2)
        {
            GridsManager manager = new();
            manager.SetShip(1, 4, 3, 8, 3);
            manager.SetShip(2, 2, 6, 5, 6);
            return manager.IsPossibleToSetShip(row1, column1, row2, column2);
        }

        [Test]
        public void IsReadyToPlay_OneShip()
        {
            GridsManager manager = new();
            var expected = false;
            manager.SetShip(2, 1, 1, 1, 4);
            Assert.AreEqual(expected, manager.IsReadyToPlay());
        }

        [Test]
        public void IsReadyToPlay_TwoShips()
        {
            GridsManager manager = new();
            var expected = false;
            manager.SetShip(2, 1, 1, 1, 4);
            manager.SetShip(1, 5, 5, 5, 9);
            Assert.AreEqual(expected, manager.IsReadyToPlay());
        }

        [Test]
        public void IsReadyToPlay_threeShips()
        {
            GridsManager manager = new();
            var expected = true;
            manager.SetShip(2, 1, 1, 1, 4);
            manager.SetShip(1, 5, 5, 5, 9);
            manager.SetShip(3, 9, 2, 6, 2);
            Assert.AreEqual(expected, manager.IsReadyToPlay());
        }

        [Test]
        public void IsReadyToPlay_AfterDeleting()
        {
            GridsManager manager = new();
            var expected = false;
            manager.SetShip(2, 1, 1, 1, 4);
            manager.SetShip(1, 5, 5, 5, 9);
            manager.SetShip(3, 9, 2, 6, 2);
            manager.DeleteShips();
            Assert.AreEqual(expected, manager.IsReadyToPlay());
        }

        [TestCaseSource(typeof(TestCasesDataSource), nameof(TestCasesDataSource.TestCasesForGetShotting))]
        public void GetShot_CorrectResultReturn(int row, int column, (bool, bool, bool) expected)
        {
            GridsManager manager = new();            
            manager.SetShip(1, 1, 1, 1, 5);
            manager.SetShip(2, 5, 5, 5, 8);
            manager.SetShip(3, 9, 2, 6, 2);
            manager.GetShot(1, 1);
            manager.GetShot(1, 2);
            manager.GetShot(1, 3);
            manager.GetShot(1, 4);
            Assert.AreEqual(expected, manager.GetShot(row, column));
        }

        [Test]
        public void GetShot_CorrectResultWin()
        {
            GridsManager manager = new();
            manager.SetShip(1, 1, 1, 1, 5);
            manager.SetShip(2, 5, 5, 5, 8);
            manager.SetShip(3, 9, 2, 6, 2);
            manager.GetShot(1, 1);
            manager.GetShot(1, 2);
            manager.GetShot(1, 3);
            manager.GetShot(1, 4);
            manager.GetShot(1, 5);
            manager.GetShot(5, 5);
            manager.GetShot(5, 6);
            manager.GetShot(5, 7);
            manager.GetShot(5, 8);
            manager.GetShot(6, 2);
            manager.GetShot(7, 2);
            manager.GetShot(8, 2);
            Assert.AreEqual((true, true, true), manager.GetShot(9, 2));
        }

        [TestCaseSource(typeof(TestCasesDataSource), nameof(TestCasesDataSource.TestCasesForGetShotGridChanging))]
        public void GetShot_CorrectGridChanging(int row1, int column1, int row2, int column2, int row3, int column3, char[,] expected)
        {
            GridsManager manager = new();
            manager.SetShip(1, 1, 1, 1, 5);
            manager.SetShip(2, 5, 5, 5, 8);
            manager.SetShip(3, 9, 2, 6, 2);
            manager.GetShot(row1, column1);
            manager.GetShot(row2, column2);
            manager.GetShot(row3, column3);
            Assert.AreEqual(expected, manager.GetOwnGrid());
        }
    }
}