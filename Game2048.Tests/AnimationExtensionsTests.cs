using Game2048.Extensions;
using Game2048.ViewModels;
using Game2048.Views;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game2048.Tests
{
    public class AnimationExtensionsTests
    {
        [Test]
        public void AnimateHorizontally_WhenXEqualsXRequest_DoesNothing()
        {
            //arrange
            SquareView squareView = new SquareView();
            double startLeftMargin = squareView.Margin.Left;
            SquareViewModel squareViewModel = new SquareViewModel(0,0,2);
            //act
            AnimationExtensions.AnimateHorizontally(squareView, squareViewModel);
            //assert
            Assert.That(squareView.Margin.Left == startLeftMargin);
        }

        [Test]
        public void AnimateVertically_WhenYEqualsYRequest_DoesNothing()
        {
            //arrange
            SquareView squareView = new SquareView();
            double startTopMargin = squareView.Margin.Top;
            SquareViewModel squareViewModel = new SquareViewModel(0, 0, 2);
            //act
            AnimationExtensions.AnimateVertically(squareView, squareViewModel);
            //assert
            Assert.That(squareView.Margin.Top == startTopMargin);
        }      
    }
}
