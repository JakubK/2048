using Game2048.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game2048.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainView : ContentPageBase<MainViewModel>
	{
		public MainView ()
		{
			InitializeComponent ();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.OneWayBind(ViewModel, vm => vm.Squares, v => v.Board.ItemsSource);

            this.BindCommand(ViewModel, vm => vm.MoveLeft, v => v.LeftBtn);
            this.BindCommand(ViewModel, vm => vm.MoveRight, v => v.RightBtn);
            this.BindCommand(ViewModel, vm => vm.MoveDown, v => v.DownBtn);
            this.BindCommand(ViewModel, vm => vm.MoveUp, v => v.UpBtn);

            ViewModel.Squares.CollectionChanged += Squares_CollectionChanged;

            PanGesture.Events().PanUpdated.InvokeCommand(ViewModel.SwitchMove);
        }

        private void Squares_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Board.OnItemsSourcePropertyChanged();
        }
    }
}