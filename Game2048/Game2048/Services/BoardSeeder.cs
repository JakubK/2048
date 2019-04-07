using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text;
using Game2048.ViewModels;

namespace Game2048.Services
{
    public class BoardSeeder : IBoardSeeder
    {
        Random random; 

        public BoardSeeder()
        {
            random = new Random();
        }

        public ObservableCollection<SquareViewModel> FillWithSquares(MainViewModel board, int count)
        {
            int i = 0;
            while(i != count)
            {
                SquareViewModel squareViewModel = new SquareViewModel(random.Next(0, board.BoardWidth), random.Next(0, board.BoardHeight), 2);
                if (board.Squares.Count == board.BoardWidth * board.BoardHeight)
                {
                    return null;
                }

                if (!board.Squares.Any(x => x.X == squareViewModel.X && x.Y == squareViewModel.Y))
                {
                    board.Squares.Add(squareViewModel);
                    i++;
                }
            }

            return board.Squares;
        }
    }
}
