using Calculator;
using SorryDialog;
using UnityEngine;
using Zenject;

namespace DI
{
    public class UiInstaller : MonoInstaller
    {
        [SerializeField] private CalculatorView calculatorView;
        [SerializeField] private SorryDialogView sorryDialogView;


        public override void InstallBindings()
        {
            Container.BindInstance<ISorryDialogView>(sorryDialogView);
            Container.Bind<ISorryDialogPresenter>()
                .To<SorryDialogPresenter>()
                .FromMethod(() => new SorryDialogPresenter(sorryDialogView))
                .AsSingle();

            Container.BindInstance<ICalculatorView>(calculatorView);
            Container.Bind<ICalculatorPresenter>()
                .To<CalculatorPresenter>()
                .AsSingle();
        }
    }
}
