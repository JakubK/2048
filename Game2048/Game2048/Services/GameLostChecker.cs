using Game2048.ViewModels;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2048.Services
{
    public class GameLostChecker : IGameLostChecker
    {
        private IBoardContainer boardContainer;

        public GameLostChecker(IBoardContainer board = null)
        {
            boardContainer = board ?? Locator.Current.GetService<IBoardContainer>();
        }

        public bool IsLost()
        {
            if (boardContainer.Squares.Count != boardContainer.Width * boardContainer.Height)
                return false;

            List<SquareViewModel> list;

            var rows = boardContainer.Squares.GroupBy(r => r.Y).ToList();

            foreach (var row in rows)
            {
                list = row.OrderBy(x => x.X).ToList();
                for (int i = 1; i < row.Count(); i++)
                {
                    if (list[i].Value == list[i - 1].Value)
                    {
                        return false;
                    }
                }
            }

            var cols = boardContainer.Squares.GroupBy(r => r.X).ToList();

            foreach (var col in cols)
            {
                list = col.OrderBy(x => x.Y).ToList();
                for (int i = 1; i < col.Count(); i++)
                {
                    if (list[i].Value == list[i - 1].Value)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
