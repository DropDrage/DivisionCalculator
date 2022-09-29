using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class Equation
    {
        private static readonly Regex ValidationRegex = new(@"^[\d/]+$");

        public string Expression { get; set; } = string.Empty;


        public ExpressionValidity Validate()
        {
            if (string.IsNullOrEmpty(Expression))
            {
                return ExpressionValidity.Empty;
            }
            if (ValidationRegex.IsMatch(Expression))
            {
                return ExpressionValidity.Valid;
            }

            return ExpressionValidity.Invalid;
        }

        /// <summary>
        /// Check validity with <see cref="Validate"/> before using this method.
        /// It should be <see cref="ExpressionValidity.Valid"/>.
        /// </summary>
        /// <returns><see cref="Expression"/> evaluation result</returns>
        public float Evaluate()
        {
            return Expression.Split('/')
                .Select(Convert.ToSingle)
                .Aggregate((acc, value) => acc / value);
        }

        public void Clear()
        {
            Expression = string.Empty;
        }
    }
}
