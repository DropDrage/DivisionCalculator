using Calculator;
using Data;
using SorryDialog;
using UnityEngine;
using Zenject;

namespace DI
{
    public class UiInstaller : MonoInstaller
    {
        [SerializeField] private CalculatorView calculatorView;
        [SerializeField] private SorryDialogView sorryDialogView;

        [SerializeField] private EquationService equationService;


        public override void InstallBindings()
        {
            BindEquation();
            BindSorryDialog();
            BindCalculator();
        }

        private void BindEquation()
        {
            Container.Bind<EquationStorage>().FromNew().AsSingle();
            Container.BindInstance(equationService);
        }

        private void BindSorryDialog()
        {
            Container.BindInstance<ISorryDialogView>(sorryDialogView);
            Container.Bind<ISorryDialogPresenter>()
                .To<SorryDialogPresenter>()
                .AsSingle();
        }

        private void BindCalculator()
        {
            Container.BindInstance<ICalculatorView>(calculatorView);
            Container.BindInterfacesTo<CalculatorPresenter>().AsSingle();
        }
    }
}
