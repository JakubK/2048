using DynamicData;
using Game2048.Models;
using Game2048.ViewModels.Base;
using ReactiveUI;
using ReactiveUI.Legacy;

namespace Game2048.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ReactiveList<Square> Squares;

        public MainViewModel(IScreen hostScreen = null) : base(hostScreen)
        {
            Squares = new ReactiveList<Square>();
            Squares.Add(new Square() { X = 0, Y = 0, Value = 2 });
            Squares.Add(new Square() { X = 1, Y = 0, Value = 2 });
            Squares.Add(new Square() { X = 2, Y = 1, Value = 2 });
            Squares.Add(new Square() { X = 0, Y = 2, Value = 2 });
            Squares.Add(new Square() { X = 3, Y = 2, Value = 4 });
        }
    }
}