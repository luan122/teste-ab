using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Common.Regexes;

namespace Ambev.DeveloperEvaluation.Common.Extensions
{
    public static class StringSanitizeExtensions
    {
        public static string Sanitize(string input)
        {
            var sanitized = SanitizeRegexes.NonAlphaNumeric().Replace(input, string.Empty);
            sanitized = SanitizeRegexes.MultipleSpaces().Replace(sanitized, string.Empty).Trim();
            return SanitizeRegexes.Space().Replace(sanitized, "_");
        }

    }
}
