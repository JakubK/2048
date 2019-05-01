using Game2048.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Game2048.Extensions
{
    public static class AnimationExtensions
    {
        public static void AnimateHorizontally(this View self, SquareViewModel square, Action<double,bool> finished)
        {
            if(square.X > square.XRequest)
            {
                //left
                new Animation(v => self.Margin = new Thickness(-v * self.Width * Math.Abs(square.X - square.XRequest), 0, v * self.Width * Math.Abs(square.X - square.XRequest), 0), 0, 1)
                    .Commit(self, "LeftAnimation",16,250,Easing.Linear,finished, () => false);
            }
            else if(square.X < square.XRequest)
            {
                //right
                new Animation(v => self.Margin = new Thickness(v * self.Width * Math.Abs(square.X - square.XRequest), 0, -v * self.Width * Math.Abs(square.X - square.XRequest), 0), 0, 1)
                    .Commit(self, "RightAnimation", 16, 250, Easing.Linear, finished, () => false);
            }
        }

        public static void AnimateVertically(this View self, SquareViewModel square, Action<double,bool> finished)
        {
            if (square.Y > square.YRequest)
            {
                //top
                new Animation(v => self.Margin = new Thickness(0, -v * self.Height * Math.Abs(square.Y - square.YRequest), 0, v * self.Height * Math.Abs(square.Y - square.YRequest)), 0, 1)
                    .Commit(self, "TopAnimation", 16, 250, Easing.Linear, finished, () => false);
            }
            else if (square.Y < square.YRequest)
            {
                //bottom
                new Animation(v => self.Margin = new Thickness(0, v * self.Height * Math.Abs(square.Y - square.YRequest), 0, -v * self.Height * Math.Abs(square.Y - square.YRequest)), 0, 1)
                    .Commit(self, "BottomAnimation", 16, 250, Easing.Linear, finished, () => false);
            }
        }
    }
}
