using System;
using System.Collections.ObjectModel;
using System.Text;
using Game2048.ViewModels;

namespace Game2048.Services
{
    public class BoardPreparer : IBoardPreparer
    {
        Random random; 
        public BoardPreparer()
        {
            random = new Random();
        }

        public ObservableCollection<SquareViewModel> PrepareSquares(int boardWidth, int boardHeight)
        {
            ObservableCollection<SquareViewModel> preparedBoard = new ObservableCollection<SquareViewModel>();
            SquareViewModel square1 = new SquareViewModel(random.Next(0, boardWidth), random.Next(0, boardHeight), 2);
            SquareViewModel square2;
            while(true)
            {
                square2 = new SquareViewModel(random.Next(0, boardWidth), random.Next(0, boardHeight), 2);
                if (square1.X != square2.X && square1.Y != square2.Y)
                    break;
            }

            preparedBoard.Add(square1);
            preparedBoard.Add(square2);

            return preparedBoard;
        }
    }
}
