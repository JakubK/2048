using Game2048.Services;
using Game2048.ViewModels;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Game2048.Tests
{
    public class GameLostCheckerTests
    {
        [Test]
        public void IsLost_WhenBoardNotFilled_ReturnsFalse()
        {
            //arrange
            var container = Substitute.For<IBoardContainer>();
            var sut = new GameLostChecker(container);
            container.Height.Returns(3);
            container.Width.Returns(3);
            container.Squares.Returns(new ObservableCollection<SquareViewModel>()
            {
                new SquareViewModel{X = 0, Y = 0},
                new SquareViewModel{X = 1, Y = 0},
                new SquareViewModel{X = 2, Y = 0},
                new SquareViewModel{X = 0, Y = 1},
                new SquareViewModel{X = 1, Y = 1},
                new SquareViewModel{X = 2, Y = 1},
                new SquareViewModel{X = 0, Y = 2},
            });

            //act
            bool isGameLost = sut.IsLost();
            //assert
            Assert.That(!isGameLost);
        }

        [Test]
        public void IsLost_WhenBoardFilledButOptionToMergeHorizontally_ReturnsFalse()
        {
            //arrange
            var container = Substitute.For<IBoardContainer>();
            var sut = new GameLostChecker(container);
            container.Height.Returns(3);
            container.Width.Returns(3);
            container.Squares.Returns(new ObservableCollection<SquareViewModel>()
            {
                new SquareViewModel{X = 0, Y = 0, Value = 2},
                new SquareViewModel{X = 1, Y = 0, Value = 2},
                new SquareViewModel{X = 2, Y = 0 , Value = 3},
                new SquareViewModel{X = 0, Y = 1 , Value = 4},
                new SquareViewModel{X = 1, Y = 1 , Value = 5},
                new SquareViewModel{X = 2, Y = 1 , Value = 6},
                new SquareViewModel{X = 0, Y = 2 , Value = 7},
                new SquareViewModel{X = 1, Y = 2 , Value = 8},
                new SquareViewModel{X = 2, Y = 2 , Value = 9},
            });

            //act
            bool isGameLost = sut.IsLost();
            //assert
            Assert.That(!isGameLost);
        }

        [Test]
        public void IsLost_WhenBoardFilledButOptionToMergeVertically_ReturnsFalse()
        {
            //arrange
            var container = Substitute.For<IBoardContainer>();
            var sut = new GameLostChecker(container);
            container.Height.Returns(3);
            container.Width.Returns(3);
            container.Squares.Returns(new ObservableCollection<SquareViewModel>()
            {
                new SquareViewModel{X = 0, Y = 0, Value = 1},
                new SquareViewModel{X = 1, Y = 0, Value = 2},
                new SquareViewModel{X = 2, Y = 0 , Value = 3},
                new SquareViewModel{X = 0, Y = 1 , Value = 4},
                new SquareViewModel{X = 1, Y = 1 , Value = 5},
                new SquareViewModel{X = 2, Y = 1 , Value = 6},
                new SquareViewModel{X = 0, Y = 2 , Value = 7},
                new SquareViewModel{X = 1, Y = 2 , Value = 8},
                new SquareViewModel{X = 2, Y = 2 , Value = 8},
            });

            //act
            bool isGameLost = sut.IsLost();
            //assert
            Assert.That(!isGameLost);
        }

        [Test]
        public void IsLost_WhenBoardFilledAndNoOptionToMerge_ReturnsTrue()
        {
            //arrange
            var container = Substitute.For<IBoardContainer>();
            var sut = new GameLostChecker(container);
            container.Height.Returns(3);
            container.Width.Returns(3);
            container.Squares.Returns(new ObservableCollection<SquareViewModel>()
            {
                new SquareViewModel{X = 0, Y = 0, Value = 1},
                new SquareViewModel{X = 1, Y = 0, Value = 2},
                new SquareViewModel{X = 2, Y = 0 , Value = 3},
                new SquareViewModel{X = 0, Y = 1 , Value = 4},
                new SquareViewModel{X = 1, Y = 1 , Value = 5},
                new SquareViewModel{X = 2, Y = 1 , Value = 6},
                new SquareViewModel{X = 0, Y = 2 , Value = 7},
                new SquareViewModel{X = 1, Y = 2 , Value = 8},
                new SquareViewModel{X = 2, Y = 2 , Value = 9},
            });

            //act
            bool isGameLost = sut.IsLost();
            //assert
            Assert.That(isGameLost);
        }
    }
}
