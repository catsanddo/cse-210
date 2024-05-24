class Fraction
{
    private int _numerator;
    private int _denominator;

    public Fraction()
    {
        _numerator = 1;
        _denominator = 1;
    }

    public Fraction(int n)
    {
        _numerator = n;
        _denominator = 1;
    }

    public Fraction(int n, int d)
    {
        _numerator = n;
        _denominator = d;
    }

    public int GetNumerator()
    {
        return _numerator;
    }

    public int GetDenominator()
    {
        return _denominator;
    }

    public void SetNumerator(int value)
    {
        _numerator = value;
    }

    public void SetDenominator(int value)
    {
        _denominator = value;
    }

    public string GetFractionString()
    {
        return $"{_numerator}/{_denominator}";
    }

    public double GetDecimalValue()
    {
        return (double) _numerator / (double) _denominator;
    }
}