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

            this.WhenAny(x => x.ViewModel.Squares, x => x.Value)
                .Subscribe(x => BindableLayout.SetItemsSource(Board, x));

            PanGesture.Events().PanUpdated.InvokeCommand(ViewModel.SwitchMove);
        }

    }
}