using System;
using UnityEngine;

namespace SorryDialog
{
    public interface ISorryDialogPresenter
    {
        event Action ClearEquation;
        event Action QuitAndClear;

        void Show();

        void NewEquation();

        void Quit();
    }

    public class SorryDialogPresenter : ISorryDialogPresenter
    {
        private readonly ISorryDialogView _view;

        public event Action ClearEquation;
        public event Action QuitAndClear;


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
            ClearEquation?.Invoke();
            _view.Close();
        }

        public void Quit()
        {
            QuitAndClear?.Invoke();
            Application.Quit();
        }
    }
}
