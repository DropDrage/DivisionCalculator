namespace Data
{
    public struct EquationDto
    {
        public string Expression { get; }


        public EquationDto(string expression = "")
        {
            Expression = expression;
        }
    }
}
