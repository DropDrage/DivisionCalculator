using SorryDialog;
using TMPro;
using UnityEngine;

namespace Calculator
{
    public interface ICalculatorView
    {
        string Expression { set; }

        void ShowResult(string result);

        void ShowError(string error);
        
        void ClearExpressionWithoutChangeEvent();
    }

    public class CalculatorView : MonoBehaviour, ICalculatorView
    {
        [SerializeField] private TMP_InputField expressionInput;

        private ICalculatorPresenter _presenter;


        public string Expression
        {
            set => expressionInput.text = value;
        }


        private void Awake()
        {
            _presenter = new CalculatorPresenter(this);
            expressionInput.onValueChanged.AddListener(OnExpressionChanged);
        }

        private void OnDestroy()
        {
            _presenter.Dispose();
        }
        
        
        public void OnSorryDialogInitialized(ISorryDialogPresenter sorryDialog)
        {
            _presenter.SorryDialog = sorryDialog;
        }


        private void OnExpressionChanged(string expression)
        {
            _presenter.ExpressionChanged(expression);
        }

        public void CalculateResult()
        {
            _presenter.CalculateResult();
        }


        public void ShowResult(string result)
        {
            Expression = result;
        }

        public void ShowError(string error)
        {
            expressionInput.onValueChanged.RemoveListener(OnExpressionChanged);
            Expression = error;
            expressionInput.onValueChanged.AddListener(OnExpressionChanged);
        }

        public void ClearExpressionWithoutChangeEvent()
        {
            expressionInput.onValueChanged.RemoveListener(OnExpressionChanged);
            Expression = string.Empty;
            expressionInput.onValueChanged.AddListener(OnExpressionChanged);
        }
    }
}
