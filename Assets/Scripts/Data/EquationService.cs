using System;
using System.Threading.Tasks;
using Calculator;
using UnityEngine;
using Zenject;

namespace Data
{
    public class EquationService : MonoBehaviour
    {
        private EquationStorage _storage;

        public Equation Equation { private get; set; }

        [NonSerialized] public bool IsSavingEnabled = true;


        [Inject]
        public void Construct(EquationStorage storage)
        {
            _storage = storage;
        }

        public async Task<Equation> GetSavedEquation()
        {
            var dto = await _storage.Get();
            return Equation = new Equation(dto);
        }


        private void OnApplicationQuit()
        {
            if (IsSavingEnabled)
            {
                _storage.Save(Equation.ToDto());
            }
            else
            {
                _storage.Clear();
            }
        }
    }
}
