using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Text;
using ReactiveUI.XamForms;
namespace Game2048.Views
{
    public class ContentViewBase<TViewModel> : ReactiveContentView<TViewModel> where TViewModel : class
    {
        protected readonly CompositeDisposable SubscriptionDisposables = new CompositeDisposable();
    }
}
