using System;
using Data;
using Zenject;

namespace Calculator
{
    public interface ICalculatorPresenter
    {
        void OnExpressionChanged(string expression);

        void CalculateResult();

        void OnCreateNewEquation();
    }

    public class CalculatorPresenter : ICalculatorPresenter, IInitializable
    {
        private const string ErrorMessage = "Error";

        [Inject] private readonly ICalculatorView _view;
        private readonly EquationService _service;

        private Equation _equation;


        public CalculatorPresenter(EquationService service)
        {
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
        }

        void ICalculatorPresenter.OnCreateNewEquation()
        {
            _view.ClearExpression();
        }
    }
}
