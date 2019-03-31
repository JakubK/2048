using Game2048.ViewModels.Base;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game2048.ViewModels
{
    public class SquareViewModel : ViewModelBase
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Value { get; set; }

        public SquareViewModel()
        {
        }

        public SquareViewModel(int x, int y,int value)
        {
            this.X = x;
            this.Y = y;
            this.Value = value;
        }
    }
}
