using Game2048.Services;
using Game2048.ViewModels;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
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

        Animation leftAnimation;
        Animation rightAnimation;
        Animation topAnimation;
        Animation bottomAnimation;

        public SquareView ()
		{
			InitializeComponent ();

            boardContainer = Locator.Current.GetService<IBoardContainer>();

            this.OneWayBind(ViewModel, vm => vm.Value, v => v.SquareButton.Text);

            appearAnimation = new Animation(v => this.Scale = v, 0, 1);
            leftAnimation = new Animation(v => this.Margin = new Thickness(-v * 75 * Math.Abs(ViewModel.X - ViewModel.XRequest), 0, v * 75  * Math.Abs(ViewModel.X - ViewModel.XRequest), 0), 0, 1);
            rightAnimation = new Animation(v => this.Margin = new Thickness(v * 75 * Math.Abs(ViewModel.X - ViewModel.XRequest), 0, -v * 75 * Math.Abs(ViewModel.X - ViewModel.XRequest), 0), 0, 1);
            topAnimation = new Animation(v => this.Margin = new Thickness(0, -v * 75 * Math.Abs(ViewModel.Y - ViewModel.YRequest), 0, v * 75 * Math.Abs(ViewModel.Y - ViewModel.YRequest)), 0, 1);
            bottomAnimation = new Animation(v => this.Margin = new Thickness(0, v * 75 * Math.Abs(ViewModel.Y - ViewModel.YRequest), 0, -v * 75 * Math.Abs(ViewModel.Y - ViewModel.YRequest)), 0, 1);

        }

        protected async override void OnParentSet()
        {
            base.OnParentSet();

            ViewModel.WhenAnyValue(x => x.Appeared).Subscribe(a =>
            {
                if(a == true)
                {
                    appearAnimation.Commit(this, "Appear", 16, 500, Easing.Linear, (v, c) =>
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
                    if (a < ViewModel.X)
                    {
                        leftAnimation.Commit(this, "LeftAnimation", 16, 500, Easing.Linear, (v, c) =>
                        {
                            ViewModel.X = a;
                            this.Margin = new Thickness(0, 0, 0, 0);
                            if (ViewModel.ToBeRemoved)
                            {
                                boardContainer.Squares.Remove(ViewModel);
                            }
                        }, () => false);
                    }
                    else if(a > ViewModel.X)
                    {
                        rightAnimation.Commit(this, "RightAnimation", 16, 500, Easing.Linear, (v, c) =>
                        {
                            ViewModel.X = a;
                            this.Margin = new Thickness(0, 0, 0, 0);
                            if(ViewModel.ToBeRemoved)
                            {
                                boardContainer.Squares.Remove(ViewModel);
                            }
                        }, () => false);
                    }
                }
            });

            ViewModel.WhenAnyValue(x => x.YRequest).Subscribe(a =>
            {
                if (a >= 0 && ViewModel.Y != a)
                {
                    if (a < ViewModel.Y)
                    {
                        topAnimation.Commit(this, "TopAnimation", 16, 500, Easing.Linear, (v, c) =>
                        {
                            ViewModel.Y = a;
                            this.Margin = new Thickness(0, 0, 0, 0);
                            if (ViewModel.ToBeRemoved)
                            {
                                boardContainer.Squares.Remove(ViewModel);
                            }
                        }, () => false);
                    }
                    else if (a > ViewModel.Y)
                    {
                        bottomAnimation.Commit(this, "BottomAnimation", 16, 500, Easing.Linear, (v, c) =>
                        {
                            ViewModel.Y = a;
                            this.Margin = new Thickness(0, 0, 0, 0);
                            if (ViewModel.ToBeRemoved)
                            {
                                boardContainer.Squares.Remove(ViewModel);
                            }
                        }, () => false);
                    }
                }
            });
        }
    }
}