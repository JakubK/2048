using DynamicData;
using Game2048.Services;
using Game2048.ViewModels.Base;
using ReactiveUI;
using ReactiveUI.Legacy;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;

namespace Game2048.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private IBoardSeeder boardSeeder;

        private ObservableCollection<SquareViewModel> squares;
        public ObservableCollection<SquareViewModel> Squares
        {
            get => squares;
            set => this.RaiseAndSetIfChanged(ref squares, value);
        }

        public MainViewModel(IScreen hostScreen = null) : base(hostScreen)
        {
            boardSeeder = Locator.Current.GetService<IBoardSeeder>();
            
            BoardWidth = 4;
            BoardHeight = 4;

            Squares = new ObservableCollection<SquareViewModel>();
            Squares = boardSeeder.FillWithSquares(this, 2);

            MoveLeft = ReactiveCommand.Create(() => 
            {
                PushHorizontally(-1);
                Squares = boardSeeder.FillWithSquares(this, 1);
            });

            MoveRight = ReactiveCommand.Create(() =>
            {
                PushHorizontally(1);
                Squares = boardSeeder.FillWithSquares(this, 1);
            });
            MoveUp = ReactiveCommand.Create(() =>
            {
                PushVertically(-1);
                Squares = boardSeeder.FillWithSquares(this, 1);
            });

            MoveDown = ReactiveCommand.Create(() =>
            {
                PushVertically(1);
                Squares = boardSeeder.FillWithSquares(this, 1);
            });
        }

        public ReactiveCommand<Unit, Unit> MoveLeft { get; }
        public ReactiveCommand<Unit, Unit> MoveRight { get; }
        public ReactiveCommand<Unit, Unit> MoveUp { get; }
        public ReactiveCommand<Unit, Unit> MoveDown { get; }

        public int BoardWidth { get; set; }
        public int BoardHeight { get; set; }

        private void PushHorizontally(int direction) 
        {
            var rows = Squares.GroupBy(r => r.Y).ToList();
            foreach(var row in rows)
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
            row[0].X = direction > 0 ? 3 : 0;
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
                    row[i].X = direction > 0 ? 3 - pos : pos;
                }
            }

       
            foreach (SquareViewModel item in row)
            {
                item.CreatedInThisTurn = false;
            }
        }

        private void PushVertically(int direction)
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
            col[0].Y = direction > 0 ? 3 : 0;
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
                    col[i].Y = direction > 0 ? 3 - pos : pos;
                }
            }

            foreach (SquareViewModel item in col)
            {
                item.CreatedInThisTurn = false;
            }
        }
    }
}