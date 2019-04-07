using Game2048.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Game2048.Services
{
    public interface IBoardSeeder
    {
        ObservableCollection<SquareViewModel> FillWithSquares(MainViewModel board, int count);
    }
}