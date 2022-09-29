using System;
using Data;
using SorryDialog;

namespace Calculator
{
    public interface ICalculatorPresenter : IDisposable
    {
        ISorryDialogPresenter SorryDialog { set; }

        void ExpressionChanged(string expression);

        void CalculateResult();
    }

    public class CalculatorPresenter : ICalculatorPresenter
    {
        private const string ErrorMessage = "Error";

        private readonly ICalculatorView _view;
        private readonly Equation _equation = new();

        private readonly EquationStorage _equationStorage = new();

        private ISorryDialogPresenter _sorryDialog;
        public ISorryDialogPresenter SorryDialog
        {
            set
            {
                if (_sorryDialog != null)
                {
                    _sorryDialog.ClearEquation -= OnClearEquation;
                }

                _sorryDialog = value;
                _sorryDialog.ClearEquation += OnClearEquation;
            }
        }


        public CalculatorPresenter(ICalculatorView view)
        {
            _view = view;

            InitializeView();
        }

        private async void InitializeView()
        {
            var savedEquation = await _equationStorage.Get();
            _view.Expression = savedEquation.Expression;
            _equation.Expression = savedEquation.Expression;
        }


        private void OnClearEquation()
        {
            _equation.Clear();
            _view.ClearExpressionWithoutChangeEvent();
        }


        public void ExpressionChanged(string expression)
        {
            _equation.Expression = expression;
            _equationStorage.Save(new EquationDto(expression));
        }

        public void CalculateResult()
        {
            var equationValidity = _equation.Validate();
            switch (equationValidity)
            {
                case ExpressionValidity.Valid:
                    var result = _equation.Evaluate();
                    _view.ShowResult(result.ToString());
                    break;
                case ExpressionValidity.Invalid:
                    _view.ShowError(ErrorMessage);
                    _sorryDialog.Show();
                    break;
                case ExpressionValidity.Empty: break;
                default: throw new ArgumentOutOfRangeException();
            }
        }


        public void Dispose()
        {
            _sorryDialog.ClearEquation -= OnClearEquation;
        }
    }
}
