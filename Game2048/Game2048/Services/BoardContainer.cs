using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Game2048.ViewModels;
using ReactiveUI;
using Splat;

namespace Game2048.Services
{
    public class BoardContainer : ReactiveObject, IBoardContainer
    {
        private ObservableCollection<SquareViewModel> squares;
        public ObservableCollection<SquareViewModel> Squares
        {
            get => squares;
            set
            {
                this.RaiseAndSetIfChanged(ref squares, value);
            }
        }

        public int Width { get; set; }
        public int Height { get; set; }
    }
}

