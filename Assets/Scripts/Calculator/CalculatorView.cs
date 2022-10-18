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

        public string Expression
        {
            set => expressionInput.text = value;
        }


        [Inject]
        private void Construct(ICalculatorPresenter presenter)
        {
            _presenter = presenter;
        }


        private void OnDestroy()
        {
            _presenter.Dispose();
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
        }

        void ICalculatorView.ClearExpression()
        {
            Expression = string.Empty;
        }
    }
}
