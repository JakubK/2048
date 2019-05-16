using Game2048.Enums;
using Game2048.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Game2048.Tests
{
    public class DragReaderTests
    {
        [Test]
        public void GetDirection_WhenInCompletedGestureStatusGiven_ReturnsNoneMoveDirectionType()
        {
            //arrange
            var sut = new DragReader();
            List<MoveDirection> responses = new List<MoveDirection>();
            //act
            responses.Add(sut.GetDirection(new PanUpdatedEventArgs(GestureStatus.Canceled,0)));
            responses.Add(sut.GetDirection(new PanUpdatedEventArgs(GestureStatus.Canceled,0)));
            //assert
            Assert.That(responses.Where(x => x == MoveDirection.None).Count() == responses.Count);
        }

        [Test]
        public void GetDirection_WhenTooShortPanPerformed_ReturnsNoneMoveDirectionType()
        {
            //arrange
            var sut = new DragReader();
            MoveDirection response;
            //act
            sut.GetDirection(new PanUpdatedEventArgs(GestureStatus.Started, 0,12,16));
            response = sut.GetDirection(new PanUpdatedEventArgs(GestureStatus.Completed, 0,0,0));
            //assert
            Assert.That(response == MoveDirection.None);
        }

        [Test]
        public void GetDirection_WhenPannedRight_ReturnsRightMoveDirectionType()
        {
            //arrange
            var sut = new DragReader();
            MoveDirection response;
            //act
            sut.GetDirection(new PanUpdatedEventArgs(GestureStatus.Started, 0, 500, 0));
            response = sut.GetDirection(new PanUpdatedEventArgs(GestureStatus.Completed, 0, 0, 0));
            //assert
            Assert.That(response == MoveDirection.Right);
        }

        [Test]
        public void GetDirection_WhenPannedLeft_ReturnsLeftMoveDirectionType()
        {
            //arrange
            var sut = new DragReader();
            MoveDirection response;
            //act
            sut.GetDirection(new PanUpdatedEventArgs(GestureStatus.Started, 0, -500, 0));
            response = sut.GetDirection(new PanUpdatedEventArgs(GestureStatus.Completed, 0, 0, 0));
            //assert
            Assert.That(response == MoveDirection.Left);
        }

        [Test]
        public void GetDirection_WhenPannedTop_ReturnsTopMoveDirectionType()
        {
            //arrange
            var sut = new DragReader();
            MoveDirection response;
            //act
            sut.GetDirection(new PanUpdatedEventArgs(GestureStatus.Started, 0, 0, -500));
            response = sut.GetDirection(new PanUpdatedEventArgs(GestureStatus.Completed, 0, 0, 0));
            //assert
            Assert.That(response == MoveDirection.Top);
        }

        [Test]
        public void GetDirection_WhenPannedBottom_ReturnsBottomMoveDirectionType()
        {
            //arrange
            var sut = new DragReader();
            MoveDirection response;
            //act
            sut.GetDirection(new PanUpdatedEventArgs(GestureStatus.Started, 0, 0, 500));
            response = sut.GetDirection(new PanUpdatedEventArgs(GestureStatus.Completed, 0, 0, 0));
            //assert
            Assert.That(response == MoveDirection.Bottom);
        }
    }
}