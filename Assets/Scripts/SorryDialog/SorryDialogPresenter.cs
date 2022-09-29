using System;
using Data;
using UnityEngine;

namespace SorryDialog
{
    public interface ISorryDialogPresenter
    {
        event Action ClearEquation;
        
        void Show();
        
        void NewEquation();

        void Quit();
    }

    public class SorryDialogPresenter : ISorryDialogPresenter
    {
        private readonly ISorryDialogView _view;

        private readonly EquationStorage _equationStorage = new();

        public event Action ClearEquation;


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
            _equationStorage.Clear();
            Application.Quit();
        }
    }
}
