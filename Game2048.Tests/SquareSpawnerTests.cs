using Game2048.Exceptions;
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
    public class SquareSpawnerTests
    {
        [Test]
        public void SpawnSquares_When2PlacesRemainingAnd2SquaresAreToBeSpawned_FillsWholeBoard()
        {
            //arrange
            var container = Substitute.For<IBoardContainer>();
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

            var sut = new SquareSpawner(container);
            //act
            sut.SpawnSquares(2);
            //assert
            Assert.That(container.Squares.Count == container.Height * container.Width);
        }

        [Test]
        public void SpawnSquares_WhenNoEmptySpace_Throws()
        {
            //arrange
            var container = Substitute.For<IBoardContainer>();
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
                new SquareViewModel{X = 1, Y = 2},
                new SquareViewModel{X = 2, Y = 2}
            });

            var sut = new SquareSpawner(container);
            //act
            //assert
            Assert.Throws<BoardOutOfSpaceException>(() =>
            {
                sut.SpawnSquares(1);
            });
        }

        [Test]
        public void SpawnSquares_WhenAttemptingToSpawnMoreSquaresThanItsPossible_Throws()
        {
            //arrange
            var container = Substitute.For<IBoardContainer>();
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
                new SquareViewModel{X = 1, Y = 2}
            });

            var sut = new SquareSpawner(container);
            //act
            //assert
            Assert.Throws<BoardOutOfSpaceException>(() =>
            {
                sut.SpawnSquares(2);
            });
        }
    }
}