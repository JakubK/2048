using Game2048.ViewModels.Base;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game2048.ViewModels
{
    public class SquareViewModel : ViewModelBase
    {
        private bool appeared;
        public bool Appeared
        {
            get => appeared;
            set => this.RaiseAndSetIfChanged(ref appeared, value);
        }

        private bool createdInThisTurn;
        public bool CreatedInThisTurn
        {
            get => createdInThisTurn;
            set => this.RaiseAndSetIfChanged(ref createdInThisTurn, value);
        }

        private int x;
        public int X
        {
            get => x;
            set => this.RaiseAndSetIfChanged(ref x, value);
        }

        private int y;
        public int Y
        {
            get => y;
            set => this.RaiseAndSetIfChanged(ref y, value);
        }

        private int val;
        public int Value
        {
            get => val;
            set => this.RaiseAndSetIfChanged(ref val, value);
        }

        public SquareViewModel()
        {
            this.Appeared = true;
        }

        public SquareViewModel(int x, int y,int value)
        {
            this.X = x;
            this.Y = y;
            this.Value = value;
            this.Appeared = true;
        }
    }
}
