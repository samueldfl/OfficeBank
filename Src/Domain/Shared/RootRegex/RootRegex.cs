using System.Text.RegularExpressions;

namespace Application.Shared.RootRegex;

public static partial class RootRegex
{
    [GeneratedRegex(@"^[\w-.]+@([\w-]+.)+[\w-]{2,4}$")]
    public static partial Regex Email();

    [GeneratedRegex(@"\d")]
    public static partial Regex Number();

    [GeneratedRegex(@"[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]")]
    public static partial Regex SpecialChar();

    [GeneratedRegex(@"(?=.*[A-Z])")]
    public static partial Regex UpperCase();


    [GeneratedRegex(@"^[-A-Za-z0-9+/]*={0,3}$")]
    public static partial Regex Base64();
}