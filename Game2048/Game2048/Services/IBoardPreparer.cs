using Game2048.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Game2048.Services
{
    public interface IBoardPreparer
    {
        ObservableCollection<SquareViewModel> PrepareSquares(int boardWidth, int boardHeight);
    }
}