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

        private int score;
        public int Score
        {
            get => score;
            set => this.RaiseAndSetIfChanged(ref score, value);
        }


        public int Width { get; private set; }
        public int Height { get; private set; }

        public void Init(int dimension)
        {
            Width = Height = dimension;
            Score = 0;
            Squares = new ObservableCollection<SquareViewModel>();
        }
    }
}

