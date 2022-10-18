using System;
using Data;
using SorryDialog;
using Zenject;

namespace Calculator
{
    public interface ICalculatorPresenter : IDisposable
    {
        void OnExpressionChanged(string expression);

        void CalculateResult();
    }

    public class CalculatorPresenter : ICalculatorPresenter, IInitializable
    {
        private const string ErrorMessage = "Error";

        [Inject] private readonly ICalculatorView _view;
        private readonly EquationService _service;

        private ISorryDialogPresenter _sorryDialog;
        private ISorryDialogPresenter SorryDialog
        {
            set
            {
                if (_sorryDialog != null)
                {
                    UnsubscribeSorryDialogEvents();
                }

                _sorryDialog = value;
                SubscribeSorryDialogEvents();
            }
        }

        private Equation _equation;


        public CalculatorPresenter(ISorryDialogPresenter sorryDialog, EquationService service)
        {
            SorryDialog = sorryDialog;
            _service = service;
        }


        void IInitializable.Initialize()
        {
            RestoreEquation();
        }

        private async void RestoreEquation()
        {
            var savedEquation = _equation = await _service.GetSavedEquation();
            _view.Expression = savedEquation.Expression;
        }


        private void SubscribeSorryDialogEvents()
        {
            _sorryDialog.CreateNewEquation += OnCreateNewEquation;
        }

        private void UnsubscribeSorryDialogEvents()
        {
            _sorryDialog.CreateNewEquation -= OnCreateNewEquation;
        }

        private void OnCreateNewEquation()
        {
            _view.ClearExpression();
        }


        void ICalculatorPresenter.OnExpressionChanged(string expression)
        {
            _equation = _service.Equation = new Equation(expression);
        }

        void ICalculatorPresenter.CalculateResult()
        {
            switch (_equation.Validity)
            {
                case ExpressionValidity.Valid:
                    ShowEvaluationResult(_equation);
                    break;
                case ExpressionValidity.Invalid:
                    ShowError();
                    break;
                case ExpressionValidity.Empty: break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private void ShowEvaluationResult(Equation equation)
        {
            var result = equation.Evaluate();
            _view.ShowResult(result.ToString());
        }

        private void ShowError()
        {
            _view.ShowError(ErrorMessage);
            _sorryDialog.Show();
        }


        public void Dispose()
        {
            UnsubscribeSorryDialogEvents();
        }
    }
}
