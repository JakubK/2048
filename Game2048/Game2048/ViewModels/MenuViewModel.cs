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

            IncrementDimension = ReactiveCommand.Create(() =>
            {
                if (index < dimensions.Length-1)
                    index++;

                SelectedDimension = dimensions[index];
            });

            DecrementDimension = ReactiveCommand.Create(() =>
            {
                if (index > 0)
                    index--;

                SelectedDimension = dimensions[index];
            });

            OpenLevel = ReactiveCommand.Create(() =>
            {
                HostScreen.Router.Navigate.Execute(new MainViewModel(SelectedDimension)).Subscribe();
            });

        }

        private int[] dimensions = new int[] { 3, 4, 6, 8 };
        int index = 0;

        private int selectedDimension = 3;
        public int SelectedDimension
        {
            get => selectedDimension;
            set => this.RaiseAndSetIfChanged(ref selectedDimension, value);
        }

        public ReactiveCommand<Unit,Unit> IncrementDimension { get; }
        public ReactiveCommand<Unit, Unit> DecrementDimension { get; }

        public ReactiveCommand<Unit,Unit> OpenLevel { get; }

        public ReactiveCommand<Unit,Unit> OpenGithub { get; }
        public ReactiveCommand<Unit,Unit> CloseApp { get; }
    }
}
