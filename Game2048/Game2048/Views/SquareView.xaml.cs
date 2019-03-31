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
	public partial class SquareView : ContentViewBase<SquareViewModel>
    {
		public SquareView ()
		{
			InitializeComponent ();
            this.OneWayBind(ViewModel, vm => vm.Value, v => v.SquareButton.Text);
		}

    }
}