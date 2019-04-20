using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Foundation;
using Game2048.Dependencies;
using Game2048.iOS.Dependencies;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(CloseApplication))]
namespace Game2048.iOS.Dependencies
{
    public class CloseApplication : ICloseApplication
    {
        public void Close()
        {
            Thread.CurrentThread.Abort();
        }
    }
}