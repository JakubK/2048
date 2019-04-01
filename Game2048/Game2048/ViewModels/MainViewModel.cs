using DynamicData;
using Game2048.ViewModels.Base;
using ReactiveUI;
using ReactiveUI.Legacy;

namespace Game2048.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ReactiveList<SquareViewModel> Squares;

        public MainViewModel(IScreen hostScreen = null) : base(hostScreen)
        {
            Squares = new ReactiveList<SquareViewModel>();
            Squares.Add(new SquareViewModel() { X = 0, Y = 0, Value = 2 });
            Squares.Add(new SquareViewModel() { X = 1, Y = 0, Value = 2 });
            Squares.Add(new SquareViewModel() { X = 2, Y = 1, Value = 2 });
            Squares.Add(new SquareViewModel() { X = 0, Y = 2, Value = 2 });
            Squares.Add(new SquareViewModel() { X = 3, Y = 2, Value = 4 });
        }
    }
}