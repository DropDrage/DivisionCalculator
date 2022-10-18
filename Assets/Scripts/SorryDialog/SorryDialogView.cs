using System;
using UnityEngine;
using Zenject;

namespace SorryDialog
{
    public interface ISorryDialogView
    {
        void Show();

        void AddOnNewEquationListener(Action onNewEquation);

        void RemoveOnNewEquationListener(Action onNewEquation);

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


        private void OnDestroy()
        {
            _presenter.Dispose();
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

        void ISorryDialogView.AddOnNewEquationListener(Action onNewEquation)
        {
            _presenter.CreateNewEquation += onNewEquation;
        }

        void ISorryDialogView.RemoveOnNewEquationListener(Action onNewEquation)
        {
            _presenter.CreateNewEquation -= onNewEquation;
        }

        void ISorryDialogView.Close()
        {
            gameObject.SetActive(false);
        }
    }
}
