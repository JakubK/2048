using Game2048.ViewModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            sut.SelectedDimension = sut.Dimensions[sut.Dimensions.Length - 1];
            //act
            sut.IncrementDimension.Execute().Subscribe();
            //assert
            Assert.That(sut.SelectedDimension == sut.Dimensions[sut.Dimensions.Length-1]);
        }

        [Test]
        public void IncremenetDimension_WhenNotReachedLimit_IncrementsSelectedDimension()
        {
            //arrange
            MenuViewModel sut = new MenuViewModel();
            //act
            sut.IncrementDimension.Execute().Subscribe();
            //assert
            Assert.That(sut.SelectedDimension != sut.Dimensions[0]);
        }

        [Test]
        public void DecrementDimension_WhenReachedLimit_DoesNothing()
        {
            //arrange
            MenuViewModel sut = new MenuViewModel();
            sut.SelectedDimension = sut.Dimensions[0];
            //act
            sut.DecrementDimension.Execute().Subscribe();
            //assert
            Assert.That(sut.SelectedDimension == sut.Dimensions[0]);
        }

        [Test]
        public void DecremenetDimension_WhenNotReachedLimit_DecrementsSelectedDimension()
        {
            //arrange
            MenuViewModel sut = new MenuViewModel();
            sut.SelectedDimension = sut.Dimensions[sut.Dimensions.Length - 1];
            //act
            sut.DecrementDimension.Execute().Subscribe();
            //assert
            Assert.That(sut.SelectedDimension != sut.Dimensions[sut.Dimensions.Length-1]);
        }
    }
}