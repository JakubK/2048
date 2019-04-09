using Game2048.ViewModels;
using ReactiveUI;
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
        Animation appearAnimation;

		public SquareView ()
		{
			InitializeComponent ();
            this.OneWayBind(ViewModel, vm => vm.Value, v => v.SquareButton.Text);

            appearAnimation = new Animation(v => SquareButton.Scale = v, 0, 1);
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();

            ViewModel.WhenAnyValue(x => x.Appeared).Subscribe(a =>
            {
                if(a == true)
                {
                    appearAnimation.Commit(SquareButton, "Appear", 16, 500, Easing.Linear, (v, c) =>
                    {
                        SquareButton.Scale = 1;
                        ViewModel.Appeared = false;
                    }, () => false);
                }
            });  
        }
    }
}