using DynamicData;
using Game2048.ViewModels.Base;
using ReactiveUI;
using ReactiveUI.Legacy;
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

        public MainViewModel(IScreen hostScreen = null) : base(hostScreen)
        {
            Squares = new ObservableCollection<SquareViewModel>();

            Squares.Add(new SquareViewModel() { X = 0, Y = 0, Value = 4 });
            Squares.Add(new SquareViewModel() { X = 1, Y = 0, Value = 2 });
            Squares.Add(new SquareViewModel() { X = 2, Y = 0, Value = 2 });
            Squares.Add(new SquareViewModel() { X = 3, Y = 0, Value = 4 });

            MoveLeft = ReactiveCommand.Create(() =>  MoveToLeft());
            MoveRight = ReactiveCommand.Create(() => MoveToRight());
            MoveUp = ReactiveCommand.Create(() => MoveToTop());
            MoveDown = ReactiveCommand.Create(() => MoveToBottom());
        }

        public ReactiveCommand<Unit, Unit> MoveLeft { get; }
        public ReactiveCommand<Unit, Unit> MoveRight { get; }
        public ReactiveCommand<Unit, Unit> MoveUp { get; }
        public ReactiveCommand<Unit, Unit> MoveDown { get; }

        private void MoveToLeft()
        {
            var rows = Squares.GroupBy(r => r.Y).ToList();
            foreach(var row in rows)
            {
                MoveRowToLeft(row.OrderBy(x => x.X).ToList());
            }
        }

        private void MoveRowToLeft(List<SquareViewModel> row)
        {
            bool substituteCollection = false;
            foreach(SquareViewModel item in row)
            {
                bool finishedMovingItem = false;
                while(!finishedMovingItem)
                {
                    if(item.X == 0)
                    {
                        finishedMovingItem = true;
                    }
                    else
                    {
                        SquareViewModel neighbourElement = null;
                        for(int i = 0;i < Squares.Count;i++)
                        {
                            if (Squares[i].Y == item.Y && Squares[i].X == item.X - 1)
                            {
                                neighbourElement = Squares[i];
                                break;
                            }
                        }

                        if(neighbourElement != null)
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
                            item.X--;
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

        private void MoveToRight()
        {
            var rows = Squares.GroupBy(r => r.Y).ToList();
            foreach (var row in rows)
            {
                MoveRowToRight(row.OrderByDescending(x => x.X).ToList());
            }
        }

        private void MoveRowToRight(List<SquareViewModel> row)
        {
            bool substituteCollection = false;
            foreach (SquareViewModel item in row)
            {
                bool finishedMovingItem = false;
                while (!finishedMovingItem)
                {
                    if (item.X == 3)
                    {
                        finishedMovingItem = true;
                    }
                    else
                    {
                        SquareViewModel neighbourElement = null;
                        for (int i = 0; i < Squares.Count; i++)
                        {
                            if (Squares[i].Y == item.Y && Squares[i].X == item.X + 1)
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
                            item.X++;
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

        private void MoveToBottom()
        {
            var cols = Squares.GroupBy(r => r.X).ToList();
            foreach (var col in cols)
            {
                MoveColToBottom(col.OrderByDescending(x => x.Y).ToList());                
            }
        }

        private void MoveColToBottom(List<SquareViewModel> col)
        {
            bool substituteCollection = false;
            foreach (SquareViewModel item in col)
            {
                bool finishedMovingItem = false;
                while (!finishedMovingItem)
                {
                    if (item.Y == 3)
                    {
                        finishedMovingItem = true;
                    }
                    else
                    {
                        SquareViewModel neighbourElement = null;
                        for (int i = 0; i < Squares.Count; i++)
                        {
                            if (Squares[i].X == item.X && Squares[i].Y == item.Y + 1)
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
                            item.Y++;
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


        private void MoveToTop()
        {
            var cols = Squares.GroupBy(r => r.X).ToList();
            foreach (var col in cols)
            {
                MoveColToTop(col.OrderBy(x => x.Y).ToList());
            }
        }

        private void MoveColToTop(List<SquareViewModel> col)
        {
            bool substituteCollection = false;
            foreach (SquareViewModel item in col)
            {
                bool finishedMovingItem = false;
                while (!finishedMovingItem)
                {
                    if (item.Y == 0)
                    {
                        finishedMovingItem = true;
                    }
                    else
                    {
                        SquareViewModel neighbourElement = null;
                        for (int i = 0; i < Squares.Count; i++)
                        {
                            if (Squares[i].X == item.X && Squares[i].Y == item.Y - 1)
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
                            item.Y--;
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