using Game2048.ViewModels;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2048.Services
{
    public class SquareSpawner : ISquareSpawner
    {
        private IBoardContainer boardContainer;
        private Random random;

        public SquareSpawner()
        {
            boardContainer = Locator.Current.GetService<IBoardContainer>();
            random = new Random();
        }

        public void SpawnSquares(int count)
        {
            int i = 0;
            while (i != count)
            {
                SquareViewModel squareViewModel = new SquareViewModel(random.Next(0, boardContainer.Width), random.Next(0, boardContainer.Height), 2);
                if (boardContainer.Squares.Count == boardContainer.Width * boardContainer.Height)
                {
                    return;
                }

                if (!boardContainer.Squares.Any(x => x.XRequest == squareViewModel.XRequest && x.YRequest == squareViewModel.YRequest))
                {
                    boardContainer.Squares.Add(squareViewModel);
                    i++;
                }
            }
        }
    }
}
