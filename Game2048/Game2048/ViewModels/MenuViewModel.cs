using Game2048.Dependencies;
using Game2048.ViewModels.Base;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using Xamarin.Forms;

namespace Game2048.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        public MenuViewModel(IScreen hostScreen = null) : base(hostScreen)
        {
            OpenGithub = ReactiveCommand.Create(() =>
            {
                Device.OpenUri(new Uri("https://github.com/JakubK/2048"));
            });

            CloseApp = ReactiveCommand.Create(() =>
            {
                DependencyService.Get<ICloseApplication>()?.Close();
            });
            
        }

        public ReactiveCommand<Unit,Unit> OpenGithub { get; }
        public ReactiveCommand<Unit,Unit> CloseApp { get; }
    }
}
