using DynamicData;
using Game2048.ViewModels.Base;
using ReactiveUI;
using ReactiveUI.Legacy;
using System;
using System.Collections.ObjectModel;
using System.Reactive;

namespace Game2048.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<SquareViewModel> Squares;

        public MainViewModel(IScreen hostScreen = null) : base(hostScreen)
        {
            Squares = new ObservableCollection<SquareViewModel>();

            Squares.Add(new SquareViewModel() { X = 0, Y = 0, Value = 2 });
            Squares.Add(new SquareViewModel() { X = 1, Y = 0, Value = 2 });
            Squares.Add(new SquareViewModel() { X = 2, Y = 1, Value = 2 });
            Squares.Add(new SquareViewModel() { X = 0, Y = 2, Value = 2 });
            Squares.Add(new SquareViewModel() { X = 3, Y = 2, Value = 4 });

            MoveLeft = ReactiveCommand.Create(() => System.Diagnostics.Debug.WriteLine("LEFT"));
            MoveRight = ReactiveCommand.Create(() => System.Diagnostics.Debug.WriteLine("RIGHT"));
            MoveUp = ReactiveCommand.Create(() => System.Diagnostics.Debug.WriteLine("UP"));
            MoveDown = ReactiveCommand.Create(() => System.Diagnostics.Debug.WriteLine("DOWN"));
        }

        public ReactiveCommand<Unit, Unit> MoveLeft { get; }
        public ReactiveCommand<Unit, Unit> MoveRight { get; }
        public ReactiveCommand<Unit, Unit> MoveUp { get; }
        public ReactiveCommand<Unit, Unit> MoveDown { get; }
    }
}