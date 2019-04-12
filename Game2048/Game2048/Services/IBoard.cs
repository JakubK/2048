using Game2048.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Game2048.Services
{
    public interface IBoard
    {
        ObservableCollection<SquareViewModel> Squares { get; }
        void Init(int width, int height);
        void MoveVertically(int direction);
        void MoveHorizontally(int direction);
        void SpawnSquare(int count);
        bool IsLost();
    }
}
