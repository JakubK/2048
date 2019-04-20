using Game2048.Helpers;
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

        bool init = false;

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.OneWayBind(ViewModel, vm => vm.EndGame, v => v.EndScreen.IsVisible);
            this.WhenAnyValue(x => x.ViewModel.EndGame).Subscribe(x =>
            {
                Board.IsVisible = !x;
            });


            this.WhenAny(x => x.Board.Width, x => x.Value)
                .Subscribe(x =>
                {
                    if (!init && x > 0)
                    {
                        Board.HeightRequest = x;
                        Board.WidthRequest = x;

                        EndScreen.WidthRequest = x;
                        EndScreen.HeightRequest = x;

                        init = true;
                    }
                });

            this.WhenAny(x => x.ViewModel.Squares, x => x.Value)
                .Subscribe(x => BindableLayout.SetItemsSource(Board, x));

            this.WhenAny(x => x.ViewModel.BestScore, x => x.Value)
                .Subscribe(x => bestScoreLabel.Text = "BEST SCORE: " + x);

            this.WhenAny(x => x.ViewModel.board.Score, x => x.Value)
                .Subscribe(x =>
                {
                    if (x > ViewModel.BestScore)
                    {
                        ViewModel.BestScore = x;
                        Application.Current.Properties[ViewModel.board.Width.ToString()] = x;
                    }
                    pointsLabel.Text = "CURRENT SCORE: " + x;
                });

            this.BindCommand(ViewModel, x => x.GoToMenu, v => v.backMenuBtn);
            this.BindCommand(ViewModel, x => x.ReplayLevel, v => v.replayBtn);

            PanGesture.Events().PanUpdated.InvokeCommand(ViewModel.SwitchMove);
        }

    }
}