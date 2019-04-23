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
	public partial class MenuView : ContentPageBase<MenuViewModel>
	{
		public MenuView ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.BindCommand(ViewModel, vm => vm.DecrementDimension, v => v.leftListBtn);
            this.BindCommand(ViewModel, vm => vm.IncrementDimension, v => v.rightListBtn);

            this.BindCommand(ViewModel, vm => vm.OpenLevel, v => v.openLevelBtn);

            this.BindCommand(ViewModel, vm => vm.OpenGithub, v => v.githubBtn);
            this.BindCommand(ViewModel, vm => vm.CloseApp, v => v.closeBtn);

            this.WhenAnyValue(x => x.ViewModel.SelectedDimension).Subscribe(x =>
            {
                dimensionLabel.Text = x + " X " + x;
                mapImg.Source = ImageSource.FromFile("P" + x + ".png");
            });
        }
    }
}