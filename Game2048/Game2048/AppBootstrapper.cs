using Game2048.ViewModels;
using Game2048.Views;
using ReactiveUI;
using ReactiveUI.XamForms;
using Splat;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Game2048
{
    public class AppBootstrapper : ReactiveObject, IScreen
    {
        public RoutingState Router { get; protected set; }

        public AppBootstrapper()
        {
            Router = new RoutingState();
            Locator.CurrentMutable.RegisterConstant(this, typeof(IScreen));
            Locator.CurrentMutable.Register(() => new MainView(), typeof(IViewFor<MainViewModel>));

            this
                .Router
                .NavigateAndReset
                .Execute(new MainViewModel())
                .Subscribe();
        }

        public Page CreateMainPage()
        {
            return new RoutedViewHost();
        }
    }
}
