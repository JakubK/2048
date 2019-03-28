using ReactiveUI.XamForms;
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Text;

namespace Game2048.Views
{
    public class ContentPageBase<TViewModel> : ReactiveContentPage<TViewModel> where TViewModel : class
    {
        protected readonly CompositeDisposable SubscriptionDisposables = new CompositeDisposable();

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            SubscriptionDisposables.Clear();
        }
    }
}
