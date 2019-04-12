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
    public class Board : ReactiveObject, IBoard
    {
        private ObservableCollection<SquareViewModel> squares;
        public ObservableCollection<SquareViewModel> Squares
        {
            get => squares;
            private set
            {
                this.RaiseAndSetIfChanged(ref squares, value);
            }
        }

        int BoardWidth;
        int BoardHeight;

        Random random;

        public void Init(int width, int height)
        {
            Squares = new ObservableCollection<SquareViewModel>();

            this.BoardWidth = width;
            this.BoardHeight = height;

            random = new Random();
        }

        public void MoveHorizontally(int direction)
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
            row[0].X = direction > 0 ? (BoardWidth - 1) : 0;
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
                    row[i].X = direction > 0 ? (BoardWidth - 1) - pos : pos;
                }
            }


            foreach (SquareViewModel item in row)
            {
                item.CreatedInThisTurn = false;
            }
        }

        public void MoveVertically(int direction)
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
            col[0].Y = direction > 0 ? (BoardHeight - 1) : 0;
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
                    col[i].Y = direction > 0 ? (BoardHeight - 1) - pos : pos;
                }
            }

            foreach (SquareViewModel item in col)
            {
                item.CreatedInThisTurn = false;
            }
        }

        public void SpawnSquare(int count)
        {
            int i = 0;
            while (i != count)
            {
                SquareViewModel squareViewModel = new SquareViewModel(random.Next(0,BoardWidth), random.Next(0, BoardHeight), 2);
                if (Squares.Count == BoardWidth * BoardHeight)
                {
                    return;
                }

                if (!Squares.Any(x => x.X == squareViewModel.X && x.Y == squareViewModel.Y))
                {
                    Squares.Add(squareViewModel);
                    i++;
                }
            }
        }

        public bool IsLost()
        {
            if (Squares.Count != BoardWidth * BoardHeight)
                return false;

            List<SquareViewModel> list;

            var rows = Squares.GroupBy(r => r.Y).ToList();

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

            var cols = Squares.GroupBy(r => r.X).ToList();

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

