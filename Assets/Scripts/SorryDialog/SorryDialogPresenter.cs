using System;
using Data;
using UnityEngine;
using Zenject;

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
        [Inject] private readonly ISorryDialogView _view;
        private readonly EquationService _equationService;

        public event Action CreateNewEquation;


        public SorryDialogPresenter(EquationService equationService)
        {
            _equationService = equationService;
        }


        void ISorryDialogPresenter.Show()
        {
            _view.Show();
        }

        void ISorryDialogPresenter.NewEquation()
        {
            CreateNewEquation?.Invoke();
            _view.Close();
        }

        void ISorryDialogPresenter.Quit()
        {
            _equationService.IsSavingEnabled = false;
            Application.Quit();
        }
    }
}
