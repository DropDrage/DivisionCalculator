using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class Equation
    {
        private static readonly Regex ValidationRegex = new(@"^[\d/]+$");

        public string Expression { get; }

        public ExpressionValidity Validity { get; }


        public Equation(string expression)
        {
            Expression = expression;

            Validity = Validate();
        }

        private ExpressionValidity Validate()
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
        /// Check <see cref="Validity"/> before using this method.
        /// </summary>
        /// <returns><see cref="Expression"/> evaluation result</returns>
        /// <exception cref="InvalidOperationException">
        ///     If <see cref="Validity"/> isn't <see cref="ExpressionValidity.Valid"/>
        /// </exception>
        public float Evaluate()
        {
            if (Validity != ExpressionValidity.Valid)
            {
                throw new InvalidOperationException($"{nameof(Validity)} must be {nameof(ExpressionValidity.Valid)}");
            }

            return Expression.Split('/')
                .Select(Convert.ToSingle)
                .Aggregate((acc, value) => acc / value);
        }
    }
}
