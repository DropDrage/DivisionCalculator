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

            RestoreEquation();
        }

        private async void RestoreEquation()
        {
            var savedEquation = await _equationStorage.Get();
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
            EnableSave();
        }

        private void EnableSave()
        {
            _isSaveEnabled = true;
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
                    ShowEvaluationResult(equation);
                    break;
                case ExpressionValidity.Invalid:
                    ShowError();
                    DisableSave();
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
