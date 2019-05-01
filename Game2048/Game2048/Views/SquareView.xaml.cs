using Game2048.Extensions;
using Game2048.Services;
using Game2048.ViewModels;
using ReactiveUI;
using Splat;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game2048.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SquareView : ContentViewBase<SquareViewModel>
    {
        IBoardContainer boardContainer;

        Animation appearAnimation;

        public SquareView ()
		{
			InitializeComponent ();
            boardContainer = Locator.Current.GetService<IBoardContainer>();

            this.OneWayBind(ViewModel, vm => vm.Value, v => v.SquareButton.Text);

            appearAnimation = new Animation(v => this.Scale = v, 0, 1);
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            ViewModel.WhenAnyValue(x => x.Appeared).Subscribe(a =>
            {
                if(a == true)
                {
                    appearAnimation.Commit(this, "Appear", 16, 250, Easing.Linear, (v, c) =>
                    {
                        this.Scale = 1;
                        ViewModel.Appeared = false;
                    }, () => false);
                }
            });

            ViewModel.WhenAnyValue(x => x.XRequest).Subscribe(a =>
            {
                if (a >= 0 && ViewModel.X != a)
                {
                    if (ViewModel.ToBeRemoved)
                    {
                        this.FadeTo(0, 250, Easing.Linear);
                    }

                    this.AnimateHorizontally(ViewModel, (v, c) =>
                     {
                         ViewModel.X = a;
                         this.Margin = new Thickness(0, 0, 0, 0);

                         if (ViewModel.ToBeRemoved)
                         {
                             boardContainer.Squares.Remove(ViewModel);
                         }
                     });
                }
            });

            ViewModel.WhenAnyValue(x => x.YRequest).Subscribe(a =>
            {
                if (a >= 0 && ViewModel.Y != a)
                {
                    if (ViewModel.ToBeRemoved)
                    {
                        this.FadeTo(0, 250, Easing.Linear);
                    }

                    this.AnimateVertically(ViewModel, (v, c) =>
                    {
                        ViewModel.Y = a;
                        this.Margin = new Thickness(0, 0, 0, 0);

                        if (ViewModel.ToBeRemoved)
                        {
                            boardContainer.Squares.Remove(ViewModel);
                        }
                    });
                }
            });
        }
    }
}