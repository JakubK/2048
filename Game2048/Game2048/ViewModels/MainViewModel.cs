using Game2048.Enums;
using Game2048.Services;
using Game2048.ViewModels.Base;
using ReactiveUI;
using Splat;
using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Game2048.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public IBoardContainer board { get; private set; }

        private ISquareTranslator squareTranslator;
        private ISquareSpawner squareSpawner;
        private IGameLostChecker gameLostChecker;

        private IDragReader dragReader;

        public ObservableCollection<SquareViewModel> Squares
        {
            get => board.Squares;
        }

        private MoveDirection lastMove;
        public MoveDirection LastMove
        {
            get => lastMove;
            set => this.RaiseAndSetIfChanged(ref lastMove, value);
        }

        public MainViewModel(IScreen hostScreen = null) : base(hostScreen)
        {
            squareTranslator = Locator.Current.GetService<ISquareTranslator>();
            squareSpawner = Locator.Current.GetService<ISquareSpawner>();
            gameLostChecker = Locator.Current.GetService<IGameLostChecker>();

            dragReader = Locator.Current.GetService<IDragReader>();

            board = Locator.Current.GetService<IBoardContainer>();
            board.Width = 4;
            board.Height = 4;
            board.Score = 0;
            board.Squares = new ObservableCollection<SquareViewModel>();

            squareSpawner.SpawnSquares(2);

            LastMove = MoveDirection.None;

            MoveLeft = ReactiveCommand.Create(() => 
            {
                squareTranslator.TranslateHorizontally(-1);
            });

            MoveRight = ReactiveCommand.Create(() =>
            {
                squareTranslator.TranslateHorizontally(1);
            });

            MoveUp = ReactiveCommand.Create(() =>
            {
                squareTranslator.TranslateVertically(-1);
            });

            MoveDown = ReactiveCommand.Create(() =>
            {
                squareTranslator.TranslateVertically(1);
            });

            SpawnSquare = ReactiveCommand.CreateFromTask(SpawnSquareTask);

            SwitchMove = ReactiveCommand.Create<PanUpdatedEventArgs>((args) =>
            {
                LastMove = dragReader.GetDirection(args);
                switch(LastMove)
                {
                    case MoveDirection.Left:
                        MoveLeft.Execute().Subscribe();
                        break;
                    case MoveDirection.Right:
                        MoveRight.Execute().Subscribe();
                        break;
                    case MoveDirection.Top:
                        MoveUp.Execute().Subscribe();
                        break;
                    case MoveDirection.Bottom:
                        MoveDown.Execute().Subscribe();
                        break;
                    case MoveDirection.None:
                        return;
                }

                SpawnSquare.Execute().Subscribe();
            });
        }


        public ReactiveCommand<Unit, Unit> MoveLeft { get; }
        public ReactiveCommand<Unit, Unit> MoveRight { get; }
        public ReactiveCommand<Unit, Unit> MoveUp { get; }
        public ReactiveCommand<Unit, Unit> MoveDown { get; }

    
        public ReactiveCommand<PanUpdatedEventArgs, Unit> SwitchMove { get; }

        private ReactiveCommand<Unit, Unit> SpawnSquare { get; }

        private async Task SpawnSquareTask()
        {
            await Task.Delay(500);
            if (squareTranslator.ChangeOccured)
                squareSpawner.SpawnSquares(1);
            if (gameLostChecker.IsLost())
            {
                System.Diagnostics.Debug.WriteLine("Game is lost");
            }
        }

    }
}