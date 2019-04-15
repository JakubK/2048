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
using Xamarin.Forms;

namespace Game2048.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private IBoard board;

        public ObservableCollection<SquareViewModel> Squares
        {
            get => board.Squares;
        }

        public MainViewModel(IScreen hostScreen = null) : base(hostScreen)
        {
            board = Locator.Current.GetService<IBoard>();

            BoardWidth = 4;
            BoardHeight = 4;

            board.Init(BoardWidth, BoardHeight);

            board.SpawnSquare(2);

            MoveLeft = ReactiveCommand.Create(() => 
            {
                board.MoveHorizontally(-1);
                board.SpawnSquare(1);
                if(board.IsLost())
                {
                    System.Diagnostics.Debug.WriteLine("Game is lost");
                }
            });

            MoveRight = ReactiveCommand.Create(() =>
            {
                board.MoveHorizontally(1);
                board.SpawnSquare(1);
                if (board.IsLost())
                {
                    System.Diagnostics.Debug.WriteLine("Game is lost");
                }
            });

            MoveUp = ReactiveCommand.Create(() =>
            {
                board.MoveVertically(-1);
                board.SpawnSquare(1);
                if (board.IsLost())
                {
                    System.Diagnostics.Debug.WriteLine("Game is lost");
                }
            });

            MoveDown = ReactiveCommand.Create(() =>
            {
                board.MoveVertically(1);
                board.SpawnSquare(1);
                if (board.IsLost())
                {
                    System.Diagnostics.Debug.WriteLine("Game is lost");
                }
            });

            SwitchMove = ReactiveCommand.Create<PanUpdatedEventArgs>((args) =>
            {
                if (args.StatusType == GestureStatus.Completed)
                {
                    if (Math.Sqrt(PanX * PanX + PanY * PanY) > 100)
                    {
                        if (PanX > 0)
                        {
                            if (Math.Abs(PanX) > Math.Abs(PanY))
                            {
                                System.Diagnostics.Debug.WriteLine("Drag went right");
                                MoveRight.Execute().Subscribe();
                            }
                        }
                        else
                        {
                            if (Math.Abs(PanX) > Math.Abs(PanY))
                            {
                                System.Diagnostics.Debug.WriteLine("Drag went left");
                                MoveLeft.Execute().Subscribe();
                            }
                        }
                        if (PanY > 0)
                        {
                            if (Math.Abs(PanX) < Math.Abs(PanY))
                            {
                                System.Diagnostics.Debug.WriteLine("Drag went down");
                                MoveDown.Execute().Subscribe();
                            }
                        }
                        else
                        {
                            if (Math.Abs(PanX) < Math.Abs(PanY))
                            {
                                System.Diagnostics.Debug.WriteLine("Drag went up");
                                MoveUp.Execute().Subscribe();
                            }
                        }
                    }
                }
                else
                {
                    PanX = args.TotalX;
                    PanY = args.TotalY;
                }
            });
        }

        private double PanX;
        private double PanY;

        public ReactiveCommand<Unit, Unit> MoveLeft { get; }
        public ReactiveCommand<Unit, Unit> MoveRight { get; }
        public ReactiveCommand<Unit, Unit> MoveUp { get; }
        public ReactiveCommand<Unit, Unit> MoveDown { get; }

        public ReactiveCommand<PanUpdatedEventArgs, Unit> SwitchMove { get; }

        public int BoardWidth { get; set; }
        public int BoardHeight { get; set; }       
    }
}