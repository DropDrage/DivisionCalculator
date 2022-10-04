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


        private void Start()
        {
            Close();
        }

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
