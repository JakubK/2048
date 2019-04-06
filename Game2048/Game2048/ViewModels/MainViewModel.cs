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
        private ObservableCollection<SquareViewModel> squares;
        public ObservableCollection<SquareViewModel> Squares
        {
            get => squares;
            set => this.RaiseAndSetIfChanged(ref squares, value);
        }

        public MainViewModel(IScreen hostScreen = null, IBoardPreparer boardPreparer = null) : base(hostScreen)
        {
            boardPreparer = Locator.Current.GetService<IBoardPreparer>();
            BoardWidth = 4;
            BoardHeight = 4;
            Squares = boardPreparer.PrepareSquares(4, 4);

            MoveLeft = ReactiveCommand.Create(() =>  PushHorizontally(-1));
            MoveRight = ReactiveCommand.Create(() => PushHorizontally(1));

            MoveUp = ReactiveCommand.Create(() => PushVertically(-1));
            MoveDown = ReactiveCommand.Create(() => PushVertically(1));
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
                    PushRowHorizontally(row.OrderBy(x => x.X).ToList(),direction);
                else
                    PushRowHorizontally(row.OrderByDescending(x => x.X).ToList(), direction);
            }
        }

        private void PushRowHorizontally(List<SquareViewModel> row, int direction)
        {
            bool substituteCollection = false;
            int origin = direction > 0 ? (BoardWidth-1) : 0;
            int diff = direction > 0 ? 1 : -1;

            foreach (SquareViewModel item in row)
            {
                bool finishedMovingItem = false;
                while (!finishedMovingItem)
                {
                    if (item.X == origin)
                    {
                        finishedMovingItem = true;
                    }
                    else
                    {
                        SquareViewModel neighbourElement = null;
                        for (int i = 0; i < Squares.Count; i++)
                        {
                            if (Squares[i].Y == item.Y && Squares[i].X == item.X + diff)
                            {
                                neighbourElement = Squares[i];
                                break;
                            }
                        }

                        if (neighbourElement != null)
                        {
                            if (neighbourElement.Value == item.Value && !neighbourElement.CreatedInThisTurn)
                            {
                                neighbourElement.Value *= 2;
                                neighbourElement.CreatedInThisTurn = true;
                                Squares.Remove(item);

                                substituteCollection = true;
                                finishedMovingItem = true;
                            }
                            else
                            {
                                finishedMovingItem = true;
                            }
                        }
                        else
                        {
                            item.X += diff;
                        }
                    }
                }
            }

            foreach (SquareViewModel item in row)
            {
                item.CreatedInThisTurn = false;
            }

            if (substituteCollection)
                SubstituteCollection();
        }

        private void PushVertically(int direction)
        {
            var cols = Squares.GroupBy(r => r.X).ToList();
            foreach (var col in cols)
            {
                if (direction < 0)
                    PushColVertically(col.OrderBy(x => x.Y).ToList(), direction);
                else
                    PushColVertically(col.OrderByDescending(x => x.Y).ToList(), direction);
            }
        }

        private void PushColVertically(List<SquareViewModel> col, int direction)
        {
            bool substituteCollection = false;
            int origin = direction > 0 ? (BoardHeight-1) : 0;
            int diff = direction > 0 ? 1 : -1;

            foreach (SquareViewModel item in col)
            {
                bool finishedMovingItem = false;
                while (!finishedMovingItem)
                {
                    if (item.Y == origin)
                    {
                        finishedMovingItem = true;
                    }
                    else
                    {
                        SquareViewModel neighbourElement = null;
                        for (int i = 0; i < Squares.Count; i++)
                        {
                            if (Squares[i].X == item.X && Squares[i].Y == item.Y + diff)
                            {
                                neighbourElement = Squares[i];
                                break;
                            }
                        }

                        if (neighbourElement != null)
                        {
                            if (neighbourElement.Value == item.Value && !neighbourElement.CreatedInThisTurn)
                            {
                                neighbourElement.Value *= 2;
                                Squares.Remove(item);
                                neighbourElement.CreatedInThisTurn = true;

                                substituteCollection = true;
                                finishedMovingItem = true;
                            }
                            else
                            {
                                finishedMovingItem = true;
                            }
                        }
                        else
                        {
                            item.Y += diff;
                        }
                    }
                }
            }

            foreach (SquareViewModel item in col)
            {
                item.CreatedInThisTurn = false;
            }

            if (substituteCollection)
                SubstituteCollection();
        }

        private void SubstituteCollection()
        {
            ObservableCollection<SquareViewModel> squareCopy = new ObservableCollection<SquareViewModel>();
            for (int i = 0; i < Squares.Count; i++)
            {
                squareCopy.Add(Squares[i]);
            }
            Squares = squareCopy;
        }
    }
}