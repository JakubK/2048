using Game2048.Services;
using Game2048.ViewModels.Base;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

        private bool toBeRemoved;
        public bool ToBeRemoved
        {
            get => toBeRemoved;
            set => this.RaiseAndSetIfChanged(ref toBeRemoved, value);
        }

        private int xRequest = -1;
        public int XRequest
        {
            get => xRequest;
            set => this.RaiseAndSetIfChanged(ref xRequest, value);
        }

        private int yRequest = -1;
        public int YRequest
        {
            get => yRequest;
            set => this.RaiseAndSetIfChanged(ref yRequest, value);
        }

        public SquareViewModel()
        {
            this.Appeared = false;
        }

        private IBoardContainer board;

        public SquareViewModel(int x, int y,int value)
        {
            this.X = x;
            this.Y = y;
            this.Value = value;
            this.Appeared = false;

            this.XRequest = x;
            this.yRequest = y;

            board = Locator.Current.GetService<IBoardContainer>();

            this.WhenAnyValue(t => t.ToBeRemoved).Subscribe(async t =>
            {
                if(t == true)
                {
                    await RemoveSelf();
                }
            });
        }

        private async Task RemoveSelf()
        {
            await Task.Delay(250);
            board.Squares.Remove(this);
        }
    }
}
