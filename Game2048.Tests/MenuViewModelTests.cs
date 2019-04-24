using Game2048.ViewModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game2048.Tests
{
    public class MenuViewModelTests
    {
        [Test]
        public void IncrementDimension_WhenReachedLimit_DoesNothing()
        {
            //arrange
            MenuViewModel sut = new MenuViewModel();
            sut.SelectedDimension = 8;
            int startVal = sut.SelectedDimension;
            //act
            sut.IncrementDimension.Execute();
            //assert
            Assert.That(sut.SelectedDimension == 8);
        }

        [Test]
        public void DecrementDimension_WhenReachedLimit_DoesNothing()
        {
            //arrange
            MenuViewModel sut = new MenuViewModel();
            sut.SelectedDimension = 3;
            int startVal = sut.SelectedDimension;
            //act
            sut.DecrementDimension.Execute();
            //assert
            Assert.That(sut.SelectedDimension == 3);
        }
    }
}
