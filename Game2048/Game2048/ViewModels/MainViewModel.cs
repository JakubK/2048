using Game2048.Enums;
using Game2048.Exceptions;
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

        private IScoreReader scoreReader;
        private IScoreSaver scoreSaver;

        private bool endGame;
        public bool EndGame
        {
            get => endGame;
            set => this.RaiseAndSetIfChanged(ref endGame, value);
        }

        private int bestScore;
        public int BestScore
        {
            get => bestScore;
            set => this.RaiseAndSetIfChanged(ref bestScore, value);
        }

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

        public MainViewModel(int dimension,IScreen hostScreen = null) : base(hostScreen)
        {
            squareTranslator = Locator.Current.GetService<ISquareTranslator>();
            squareSpawner = Locator.Current.GetService<ISquareSpawner>();
            gameLostChecker = Locator.Current.GetService<IGameLostChecker>();
            dragReader = Locator.Current.GetService<IDragReader>();
            board = Locator.Current.GetService<IBoardContainer>();
            scoreSaver = Locator.Current.GetService<IScoreSaver>();
            scoreReader = Locator.Current.GetService<IScoreReader>();

            board.Init(dimension);

            squareSpawner.SpawnSquares(2);

            this.WhenAnyValue(x => x.board.Score).Subscribe(x =>
            {
                if(x > BestScore)
                {
                    BestScore = x;
                    scoreSaver.Save(board.Width.ToString(), x);
                }
            });


            LastMove = MoveDirection.None;

            try
            {
                BestScore = scoreReader.Read(dimension.ToString());
            }
            catch(Exception)
            {
                BestScore = 0;
            }

            GoToMenu = ReactiveCommand.Create(() =>
            {
                HostScreen.Router.Navigate.Execute(new MenuViewModel()).Subscribe();
            });

            ReplayLevel = ReactiveCommand.Create(() =>
            {
                HostScreen.Router.NavigateAndReset.Execute(new MainViewModel(dimension)).Subscribe();
            });

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

        public ReactiveCommand<Unit,Unit> GoToMenu { get; }
        public ReactiveCommand<Unit,Unit> ReplayLevel { get; }

        public ReactiveCommand<Unit, Unit> MoveLeft { get; }
        public ReactiveCommand<Unit, Unit> MoveRight { get; }
        public ReactiveCommand<Unit, Unit> MoveUp { get; }
        public ReactiveCommand<Unit, Unit> MoveDown { get; }

    
        public ReactiveCommand<PanUpdatedEventArgs, Unit> SwitchMove { get; }

        private ReactiveCommand<Unit, Unit> SpawnSquare { get; }

        private async Task SpawnSquareTask()
        {
            await Task.Delay(300);
            if (squareTranslator.ChangeOccured)
            {
                try
                {
                    squareSpawner.SpawnSquares(1);
                }
                catch (BoardOutOfSpaceException) { }
            }
            if (gameLostChecker.IsLost())
            {
                await Task.Delay(500);
                EndGame = true;
            }
        }
    }
}