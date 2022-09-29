using SorryDialog;
using TMPro;
using UnityEngine;

namespace Calculator
{
    public interface ICalculatorView
    {
        string Expression { get; set; }

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
            get => expressionInput.text;
            set => expressionInput.text = value;
        }


        private void Awake()
        {
            _presenter = new CalculatorPresenter(this);
        }

        private void OnApplicationQuit()
        {
            _presenter.OnApplicationQuit();
        }

        private void OnDestroy()
        {
            _presenter.Dispose();
        }


        public void OnSorryDialogInitialized(ISorryDialogPresenter sorryDialog)
        {
            _presenter.SorryDialog = sorryDialog;
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
            Expression = error;
        }

        public void ClearExpression()
        {
            Expression = string.Empty;
        }
    }
}
