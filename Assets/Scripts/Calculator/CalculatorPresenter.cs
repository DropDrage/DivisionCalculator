using System;
using Data;
using SorryDialog;

namespace Calculator
{
    public interface ICalculatorPresenter : IDisposable
    {
        ISorryDialogPresenter SorryDialog { set; }

        void CalculateResult();

        void OnApplicationQuit();
    }

    public class CalculatorPresenter : ICalculatorPresenter
    {
        private const string ErrorMessage = "Error";

        private readonly ICalculatorView _view;
        private readonly EquationStorage _equationStorage = new();

        private bool _isSaveEnabled = true;

        private ISorryDialogPresenter _sorryDialog;
        public ISorryDialogPresenter SorryDialog
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

        private Equation Equation => new(_view.Expression);


        public CalculatorPresenter(ICalculatorView view)
        {
            _view = view;

            RestoreExpression();
        }

        private async void RestoreExpression()
        {
            var savedEquation = await _equationStorage.Get();
            _view.Expression = savedEquation.Expression;
        }


        private void SubscribeSorryDialogEvents()
        {
            _sorryDialog.ClearEquation += OnClearEquation;
            _sorryDialog.QuitAndClear += DisableSave;
        }

        private void UnsubscribeSorryDialogEvents()
        {
            _sorryDialog.ClearEquation -= OnClearEquation;
            _sorryDialog.QuitAndClear -= DisableSave;
        }

        private void OnClearEquation()
        {
            _view.ClearExpression();
        }

        private void DisableSave()
        {
            _isSaveEnabled = false;
        }


        public void CalculateResult()
        {
            var equation = Equation;
            switch (equation.Validity)
            {
                case ExpressionValidity.Valid:
                    var result = equation.Evaluate();
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

        public void OnApplicationQuit()
        {
            if (_isSaveEnabled)
            {
                _equationStorage.Save(new EquationDto(Equation.Expression));
            }
            else
            {
                _equationStorage.Clear();
            }
        }

        public void Dispose()
        {
            UnsubscribeSorryDialogEvents();
        }
    }
}
