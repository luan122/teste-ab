using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.Common.Regexes
{
    public static partial class SanitizeRegexes
    {
        [GeneratedRegex(@"[\/\<\>\:\""\/\\\|\?\*\.]")]
        public static partial Regex CharactersNotPermitedOnFileName();
        [GeneratedRegex(@"[\s]{2,}")]
        public static partial Regex MultipleSpaces();
        [GeneratedRegex(@"\s")]
        public static partial Regex Space();
    }
}
