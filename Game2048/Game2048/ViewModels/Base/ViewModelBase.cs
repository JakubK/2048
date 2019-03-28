using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Text;

namespace Game2048.ViewModels.Base
{
    public class ViewModelBase : ReactiveObject, IRoutableViewModel
    {
        public string UrlPathSegment { get; protected set; }
        public IScreen HostScreen { get; protected set; }

        protected readonly CompositeDisposable subscriptionDisposables = new CompositeDisposable();

        public ViewModelBase(IScreen hostScreen = null)
        {
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
        }
    }
}
