using System.Text.RegularExpressions;

namespace System.CommandLine.Facilitator.Infra
{
    internal static class StringExtensions
    {
        public static string PascalToKebabCase(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            return Regex.Replace(
                value,
                "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])",
                "-$1",
                RegexOptions.Compiled)
                .Trim()
                .ToLower();
        }

        public static string ToOptionName(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            return "--" + value.PascalToKebabCase();
        }
    }
}
