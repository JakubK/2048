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
    public class SquareTranslatorTests
    {
       
        [Test]
        public void TranslateHorizontally_WhenNothingChanged_SetsChangeOccuredToFalse()
        {
            //arrange
            var container = Substitute.For<IBoardContainer>();
            var sut = new SquareTranslator(container);
            container.Height.Returns(3);
            container.Width.Returns(3);
            container.Squares.Returns(new ObservableCollection<SquareViewModel>()
            {
                new SquareViewModel(0,0,1),
                new SquareViewModel(1,0,2),
                new SquareViewModel(2,0,3),
                new SquareViewModel(0,1,4),
                new SquareViewModel(1,1,5),
                new SquareViewModel(2,1,6),
                new SquareViewModel(0,2,7),
                new SquareViewModel(1,2,8),
                new SquareViewModel(2,2,9)
            });
            //act
            sut.TranslateHorizontally(1);
            bool rightChangeOccured = sut.ChangeOccured;

            sut.TranslateHorizontally(-1);
            bool leftChangeOccured = sut.ChangeOccured;
            //assert
            Assert.That(!leftChangeOccured && !rightChangeOccured);
        }

        [Test]
        public void TranslateHorizontally_WhenHorizontalMergePossible_MergesProperly()
        {
            //arrange
            var container = Substitute.For<IBoardContainer>();
            var sut = new SquareTranslator(container);
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
                new SquareViewModel{X = 2, Y = 2 , Value = 8}
            });

            //act
            sut.TranslateHorizontally(1);
            //assert
            
            Assert.That(container.Squares[container.Squares.Count-1].Value == 16);
        }

        [Test]
        public void TranslateHorizontally_WhenMergedSquares_SetsChangeOccuredToTrue()
        {
            //arrange
            var container = Substitute.For<IBoardContainer>();
            var sut = new SquareTranslator(container);
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
                new SquareViewModel{X = 2, Y = 2 , Value = 8}
            });
            //act
            sut.TranslateHorizontally(1);
            //assert
            Assert.That(sut.ChangeOccured);
        }

        [Test]
        public void TranslateHorizontally_WhenMovedSquares_SetsChangeOccuredToTrue()
        {
            //arrange
            var container = Substitute.For<IBoardContainer>();
            var sut = new SquareTranslator(container);
            container.Height.Returns(3);
            container.Width.Returns(3);
            container.Squares.Returns(new ObservableCollection<SquareViewModel>()
            {
                new SquareViewModel{X = 1, Y = 0, Value = 1},
                new SquareViewModel{X = 1, Y = 1, Value = 2}
            });
            //act
            sut.TranslateHorizontally(1);
            //assert
            Assert.That(sut.ChangeOccured);
        }

        [Test]
        public void TranslateVertically_WhenNothingChanged_SetsChangeOccuredToFalse()
        {
            //arrange
            var container = Substitute.For<IBoardContainer>();
            var sut = new SquareTranslator(container);
            container.Height.Returns(3);
            container.Width.Returns(3);
            container.Squares.Returns(new ObservableCollection<SquareViewModel>()
            {
                new SquareViewModel(0,0,1),
                new SquareViewModel(1,0,2),
                new SquareViewModel(2,0,3),
                new SquareViewModel(0,1,4),
                new SquareViewModel(1,1,5),
                new SquareViewModel(2,1,6),
                new SquareViewModel(0,2,7),
                new SquareViewModel(1,2,8),
                new SquareViewModel(2,2,9)
            });
            //act
            sut.TranslateVertically(1);
            bool bottomChangeOccured = sut.ChangeOccured;

            sut.TranslateVertically(-1);
            bool topChangeOccured = sut.ChangeOccured;
            //assert
            Assert.That(!bottomChangeOccured && !topChangeOccured);
        }

        [Test]
        public void TranslateVertically_WhenMergedSquares_SetsChangeOccuredToTrue()
        {
            //arrange
            var container = Substitute.For<IBoardContainer>();
            var sut = new SquareTranslator(container);
            container.Height.Returns(3);
            container.Width.Returns(3);
            container.Squares.Returns(new ObservableCollection<SquareViewModel>()
            {
                new SquareViewModel{X = 0, Y = 0, Value = 1},
                new SquareViewModel{X = 1, Y = 0, Value = 2},
                new SquareViewModel{X = 2, Y = 0 , Value = 3},
                new SquareViewModel{X = 0, Y = 1 , Value = 1},
                new SquareViewModel{X = 1, Y = 1 , Value = 5},
                new SquareViewModel{X = 2, Y = 1 , Value = 6},
                new SquareViewModel{X = 0, Y = 2 , Value = 7},
                new SquareViewModel{X = 1, Y = 2 , Value = 8},
                new SquareViewModel{X = 2, Y = 2 , Value = 9}
            });
            //act
            sut.TranslateVertically(1);
            //assert
            Assert.That(sut.ChangeOccured);
        }

        [Test]
        public void TranslateVertically_WhenVerticalMergePossible_MergesProperly()
        {
            //arrange
            var container = Substitute.For<IBoardContainer>();
            var sut = new SquareTranslator(container);
            container.Height.Returns(3);
            container.Width.Returns(3);
            container.Squares.Returns(new ObservableCollection<SquareViewModel>()
            {
                new SquareViewModel{X = 0, Y = 0, Value = 1},
                new SquareViewModel{X = 1, Y = 0, Value = 2},
                new SquareViewModel{X = 2, Y = 0 , Value = 3},
                new SquareViewModel{X = 0, Y = 1 , Value = 1},
                new SquareViewModel{X = 1, Y = 1 , Value = 5},
                new SquareViewModel{X = 2, Y = 1 , Value = 6},
                new SquareViewModel{X = 0, Y = 2 , Value = 7},
                new SquareViewModel{X = 1, Y = 2 , Value = 8},
                new SquareViewModel{X = 2, Y = 2 , Value = 16}
            });
            //act
            sut.TranslateVertically(1);
            //assert
            Assert.That(container.Squares[container.Squares.Count-1].Value == 16);
        }

        [Test]
        public void TranslateVertically_WhenMovedSquares_SetsChangeOccuredToTrue()
        {
            //arrange
            var container = Substitute.For<IBoardContainer>();
            var sut = new SquareTranslator(container);
            container.Height.Returns(3);
            container.Width.Returns(3);
            container.Squares.Returns(new ObservableCollection<SquareViewModel>()
            {
                new SquareViewModel{X = 0, Y = 1, Value = 1},
                new SquareViewModel{X = 1, Y = 1, Value = 2}
            });
            //act
            sut.TranslateVertically(1);
            //assert
            Assert.That(sut.ChangeOccured);
        }
    }
}