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
        private IBoardContainer board;

        private ISquareTranslator squareTranslator;
        private ISquareSpawner squareSpawner;
        private IGameLostChecker gameLostChecker;


        private ObservableCollection<SquareViewModel> squares;
        public ObservableCollection<SquareViewModel> Squares
        {
            get => board.Squares;
            set => this.RaiseAndSetIfChanged(ref squares, value);
        }

        public MainViewModel(IScreen hostScreen = null) : base(hostScreen)
        {
            squareTranslator = Locator.Current.GetService<ISquareTranslator>();
            squareSpawner = Locator.Current.GetService<ISquareSpawner>();
            gameLostChecker = Locator.Current.GetService<IGameLostChecker>();

            board = Locator.Current.GetService<IBoardContainer>();
            board.Width = 4;
            board.Height = 4;
            board.Squares = new ObservableCollection<SquareViewModel>();

            squareSpawner.SpawnSquares(2);
            MoveLeft = ReactiveCommand.Create(() => 
            {
                squareTranslator.TranslateHorizontally(-1);
                squareSpawner.SpawnSquares(1);

                if (gameLostChecker.IsLost())
                {
                    System.Diagnostics.Debug.WriteLine("Game is lost");
                }
            });

            MoveRight = ReactiveCommand.Create(() =>
            {
                squareTranslator.TranslateHorizontally(1);
                squareSpawner.SpawnSquares(1);
                if (gameLostChecker.IsLost())
                {
                    System.Diagnostics.Debug.WriteLine("Game is lost");
                }
            });

            MoveUp = ReactiveCommand.Create(() =>
            {
                squareTranslator.TranslateVertically(-1);
                squareSpawner.SpawnSquares(1);
                if (gameLostChecker.IsLost())
                {
                    System.Diagnostics.Debug.WriteLine("Game is lost");
                }
            });

            MoveDown = ReactiveCommand.Create(() =>
            {
                squareTranslator.TranslateVertically(1);
                squareSpawner.SpawnSquares(1);
                if (gameLostChecker.IsLost())
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
    }
}