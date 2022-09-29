using UnityEngine;
using UnityEngine.Events;

namespace SorryDialog
{
    public interface ISorryDialogView
    {
        void Show();

        void Close();
    }

    public class SorryDialogView : MonoBehaviour, ISorryDialogView
    {
        private ISorryDialogPresenter _presenter;

        public UnityEvent<ISorryDialogPresenter> initialized;


        private void Start()
        {
            Close();

            _presenter = new SorryDialogPresenter(this);

            InvokeInitialized();
        }

        private void InvokeInitialized()
        {
            initialized.Invoke(_presenter);
            initialized.RemoveAllListeners();
            initialized = null;
        }


        public void OnNewEquationClick()
        {
            _presenter.NewEquation();
        }

        public void OnQuitClick()
        {
            _presenter.Quit();
        }


        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
