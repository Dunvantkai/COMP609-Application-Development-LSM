using System.Globalization;

namespace Livestock_Management_System;
public class Util
{
    public static readonly string BAD_STRING = string.Empty;
    public static readonly int BAD_INT = int.MinValue;
    public static readonly double BAD_DOUBLE = double.MinValue;

    private static readonly List<string> ValidTypes = new() { "Cow", "Sheep", "All" };
    private static readonly List<string> ValidColours = new() { "Red", "Black", "White", "All" };
    public static int ParseInt(object? o)
    {
        if (o == null) return BAD_INT;
        return int.TryParse(o.ToString(), out int n) ? n : BAD_INT;
    }
    public static double ParseDouble(object? o)
    {
        if (o == null) return BAD_DOUBLE;
        return double.TryParse(o.ToString(), out double n) ? n : BAD_DOUBLE;
    }
    public static string ToTitleCase(string s)
    {
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
    }
    public static string VerifyAnimalType(string? type)
    {
        if (string.IsNullOrWhiteSpace(type)) return BAD_STRING;
        string t = ToTitleCase(type);
        return ValidTypes.Contains(t) ? t : BAD_STRING;
    }
    public static string VerifyColour(string? colour)
    {
        if (string.IsNullOrWhiteSpace(colour)) return BAD_STRING;
        string c = ToTitleCase(colour);
        return ValidColours.Contains(c) ? c : BAD_STRING;
    }
    public static string VerifyCost(string? cost)
    {
        if (string.IsNullOrWhiteSpace(cost))
        {
            return BAD_STRING;
        }
        if (double.TryParse(cost, out double n))
        {
            if (n >= 0)
            {
                return n.ToString();
            }
        }
        return BAD_STRING;
    }
    public static string VerifyWeight(string? weight)
    {
        if (string.IsNullOrWhiteSpace(weight))
        {
            return BAD_STRING;
        }
        if (double.TryParse(weight, out double n))
        {
            if (n >= 0)
            {
                return n.ToString();
            }
        }
        return BAD_STRING;
    }
    public static string VerifyProduct(string? product)
    {
        if (string.IsNullOrWhiteSpace(product))
        {
            return BAD_STRING;
        }
        if (double.TryParse(product, out double n))
        {
            if (n >= 0)
            {
                return n.ToString();
            }
        }
        return BAD_STRING;
    }
}
