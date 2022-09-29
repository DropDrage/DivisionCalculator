using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace Data
{
    public class EquationStorage
    {
        private const string FileName = "input.txt";

        private readonly string _saveFilePath = Path.Combine(Application.dataPath, FileName);


        public async Task<EquationDto> Get()
        {
            if (!File.Exists(_saveFilePath))
            {
                return new EquationDto();
            }

            using var reader = new StreamReader(new FileStream(_saveFilePath, FileMode.Open));
            var expression = await reader.ReadToEndAsync();
            return new EquationDto(expression);
        }

        public void Save(EquationDto equation)
        {
            using var writer = new StreamWriter(new FileStream(_saveFilePath, FileMode.Create));
            writer.WriteAsync(equation.Expression);
        }

        public void Clear()
        {
            using var fileStream = new FileStream(_saveFilePath, FileMode.Create);
        }
    }
}
