using Game2048.Services;
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
            Locator.CurrentMutable.Register(() => new MenuView(), typeof(IViewFor<MenuViewModel>));
            Locator.CurrentMutable.RegisterConstant(new BoardContainer(), typeof(IBoardContainer));

            Locator.CurrentMutable.Register(() => new GameLostChecker(), typeof(IGameLostChecker));
            Locator.CurrentMutable.Register(() => new SquareSpawner(), typeof(ISquareSpawner));
            Locator.CurrentMutable.Register(() => new SquareTranslator(), typeof(ISquareTranslator));

            Locator.CurrentMutable.Register(() => new DragReader(), typeof(IDragReader));

            Locator.CurrentMutable.Register(() => new ScoreReader(), typeof(IScoreReader));
            Locator.CurrentMutable.Register(() => new ScoreSaver(), typeof(IScoreSaver));


            this
                .Router
                .NavigateAndReset
                .Execute(new MenuViewModel())
                .Subscribe();
        }

        public Page CreateMainPage()
        {
            return new RoutedViewHost();
        }
    }
}