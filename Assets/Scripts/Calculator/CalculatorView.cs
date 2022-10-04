using TMPro;
using UnityEngine;
using Zenject;

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


        [Inject]
        private void Construct(ICalculatorPresenter presenter)
        {
            _presenter = presenter;
        }

        private void OnApplicationQuit()
        {
            _presenter.OnApplicationQuit();
        }

        private void OnDestroy()
        {
            _presenter.Dispose();
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
