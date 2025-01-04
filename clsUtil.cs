using System;
using System.Text;

public class Util
{
    public enum CharType { SmallLetter = 1, CapitalLetter, Digit, MixChar, SpecialCharacter }

    private static Random random = new Random();

    public static void Swap<T>(ref T a, ref T b)
    {
        T temp = a;
        a = b;
        b = temp;
    }

    public static string EncryptText(string text, int shift = 2)
    {
        StringBuilder encrypted = new StringBuilder();
        foreach (char c in text)
        {
            encrypted.Append((char)(c + shift));
        }
        return encrypted.ToString();
    }

    public static string DecryptText(string text, int shift = 2)
    {
        StringBuilder decrypted = new StringBuilder();
        foreach (char c in text)
        {
            decrypted.Append((char)(c - shift));
        }
        return decrypted.ToString();
    }

    public static string NumberToText(int number)
    {
        if (number < 1) return "Zero";

        string[] units = { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
        string[] tens = { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

        if (number < 20)
        {
            return units[number];
        }

        if (number < 100)
        {
            return tens[number / 10] + (number % 10 > 0 ? " " + units[number % 10] : "");
        }

        if (number < 1000)
        {
            return units[number / 100] + " Hundred" + (number % 100 > 0 ? " " + NumberToText(number % 100) : "");
        }

        if (number < 1000000)
        {
            return NumberToText(number / 1000) + " Thousand" + (number % 1000 > 0 ? " " + NumberToText(number % 1000) : "");
        }

        if (number < 1000000000)
        {
            return NumberToText(number / 1000000) + " Million" + (number % 1000000 > 0 ? " " + NumberToText(number % 1000000) : "");
        }

        return NumberToText(number / 1000000000) + " Billion" + (number % 1000000000 > 0 ? " " + NumberToText(number % 1000000000) : "");
    }
}

