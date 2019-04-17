using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Game2048.ViewModels;
using Splat;

namespace Game2048.Services
{
    public class SquareTranslator : ISquareTranslator
    {
        private IBoardContainer boardContainer;

        private ObservableCollection<SquareViewModel> Squares
        {
            get { return boardContainer.Squares; }
        }

        private int Width
        {
            get { return boardContainer.Width; }
        }

        private int Height
        {
            get { return boardContainer.Height; }
        }

        public SquareTranslator()
        {
            this.boardContainer = Locator.Current.GetService<IBoardContainer>();
        }

        public void TranslateHorizontally(int direction)
        {
            var rows = Squares.GroupBy(r => r.Y).ToList();
            foreach (var row in rows)
            {
                if (direction < 0)
                    PushRow(row.OrderBy(x => x.X).ToList(), direction);
                else
                    PushRow(row.OrderByDescending(x => x.X).ToList(), direction);
            }
        }

        private void PushRow(List<SquareViewModel> row, int direction)
        {
            int pos = 0;
            row[0].X = direction > 0 ? (Width - 1) : 0;
            pos = 0;
            for (int i = 1; i < row.Count; i++)
            {
                if (row[i].Value == row[pos].Value && !row[pos].CreatedInThisTurn)
                {
                    row[pos].Value *= 2;
                    row[pos].CreatedInThisTurn = true;

                    Squares.Remove(row[i]);
                    row.RemoveAt(i);

                    i--;
                }
                else
                {
                    pos++;
                    row[i].X = direction > 0 ? (Width - 1) - pos : pos;
                }
            }


            foreach (SquareViewModel item in row)
            {
                item.CreatedInThisTurn = false;
            }
        }

        public void TranslateVertically(int direction)
        {
            var cols = Squares.GroupBy(r => r.X).ToList();
            foreach (var col in cols)
            {
                if (direction < 0)
                    PushCol(col.OrderBy(x => x.Y).ToList(), direction);
                else
                    PushCol(col.OrderByDescending(x => x.Y).ToList(), direction);
            }
        }

        private void PushCol(List<SquareViewModel> col, int direction)
        {
            int pos = 0;
            col[0].Y = direction > 0 ? (Height - 1) : 0;
            pos = 0;

            for (int i = 1; i < col.Count; i++)
            {
                if (col[i].Value == col[pos].Value && !col[pos].CreatedInThisTurn)
                {
                    col[pos].Value *= 2;
                    col[pos].CreatedInThisTurn = true;

                    Squares.Remove(col[i]);
                    col.RemoveAt(i);

                    i--;
                }
                else
                {
                    pos++;
                    col[i].Y = direction > 0 ? (Height - 1) - pos : pos;
                }
            }

            foreach (SquareViewModel item in col)
            {
                item.CreatedInThisTurn = false;
            }
        }
    }
}
