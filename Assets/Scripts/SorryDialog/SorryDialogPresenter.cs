using System;
using UnityEngine;

namespace SorryDialog
{
    public interface ISorryDialogPresenter
    {
        event Action CreateNewEquation;

        void Show();

        void NewEquation();

        void Quit();
    }

    public class SorryDialogPresenter : ISorryDialogPresenter
    {
        private readonly ISorryDialogView _view;

        public event Action CreateNewEquation;


        public SorryDialogPresenter(ISorryDialogView view)
        {
            _view = view;
        }


        public void Show()
        {
            _view.Show();
        }

        public void NewEquation()
        {
            CreateNewEquation?.Invoke();
            _view.Close();
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
