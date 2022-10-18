using UnityEngine;
using Zenject;

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


        [Inject]
        private void Construct(ISorryDialogPresenter presenter)
        {
            _presenter = presenter;
        }


        public void OnNewEquationClick()
        {
            _presenter.NewEquation();
        }

        public void OnQuitClick()
        {
            _presenter.Quit();
        }


        void ISorryDialogView.Show()
        {
            gameObject.SetActive(true);
        }

        void ISorryDialogView.Close()
        {
            gameObject.SetActive(false);
        }
    }
}
