using System;
using Data;
using UnityEngine;
using Zenject;

namespace SorryDialog
{
    public interface ISorryDialogPresenter : IDisposable
    {
        event Action CreateNewEquation;

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


        void ISorryDialogPresenter.NewEquation()
        {
            CreateNewEquation?.Invoke();
            ClearEvents();
            _view.Close();
        }

        void ISorryDialogPresenter.Quit()
        {
            _equationService.IsSavingEnabled = false;
            ClearEvents();
            Application.Quit();
        }


        public void Dispose()
        {
            ClearEvents();
        }


        private void ClearEvents()
        {
            CreateNewEquation = null;
        }
    }
}
