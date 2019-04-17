using Game2048.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Game2048.Services
{
    public interface IBoardContainer
    {
        ObservableCollection<SquareViewModel> Squares { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        int Score { get; set; }
    }
}
