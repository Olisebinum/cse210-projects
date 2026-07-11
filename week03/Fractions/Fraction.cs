using System;

public class Fraction
{
    private int _top;
    private int _bottom;

    // No-parameter constructor -> defaults to 1/1
    public Fraction()
    {
        _top = 1;
        _bottom = 1;
    }

    // One-parameter constructor -> denominator defaults to 1
    public Fraction(int top)
    {
        _top = top;
        _bottom = 1;
    }

    // Two-parameter constructor -> full control
    public Fraction(int top, int bottom)
    {
        _top = top;
        _bottom = bottom;
    }

    // Getter and setter for top
    public int GetTop()
    {
        return _top;
    }

    public void SetTop(int top)
    {
        _top = top;
    }

    // Getter and setter for bottom
    public int GetBottom()
    {
        return _bottom;
    }

    public void SetBottom(int bottom)
    {
        _bottom = bottom;
    }

    // Returns "3/4" style string
    public string GetFractionString()
    {
        return $"{_top}/{_bottom}";
    }

    // Returns the decimal value, e.g. 0.75
    public double GetDecimalValue()
    {
        return (double)_top / _bottom;
    }
}