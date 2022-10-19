using SorryDialog;
using TMPro;
using UnityEngine;
using Zenject;

namespace Calculator
{
    public interface ICalculatorView
    {
        string Expression { set; }

        void ShowResult(string result);

        void ShowError(string error);

        void ClearExpression();
    }

    public class CalculatorView : MonoBehaviour, ICalculatorView
    {
        [SerializeField] private TMP_InputField expressionInput;

        private ICalculatorPresenter _presenter;
        private ISorryDialogView _sorryDialog;

        public string Expression
        {
            set => expressionInput.text = value;
        }


        [Inject]
        private void Construct(ICalculatorPresenter presenter, ISorryDialogView sorryDialog)
        {
            _presenter = presenter;
            _sorryDialog = sorryDialog;
        }


        private void OnDestroy()
        {
            _sorryDialog?.RemoveOnNewEquationListener(_presenter.OnCreateNewEquation);
        }


        public void OnExpressionChanged(string expression)
        {
            _presenter.OnExpressionChanged(expression);
        }

        public void CalculateResult()
        {
            _presenter.CalculateResult();
        }


        void ICalculatorView.ShowResult(string result)
        {
            Expression = result;
        }

        void ICalculatorView.ShowError(string error)
        {
            Expression = error;

            _sorryDialog.Show();
            _sorryDialog.AddOnNewEquationListener(_presenter.OnCreateNewEquation);
        }

        void ICalculatorView.ClearExpression()
        {
            Expression = string.Empty;
        }
    }
}
