using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Common.Consts
{
    public static class FilterAttributesConsts
    {
        public const string PAGE = "_page";
        public const string SIZE = "_size";
        public const string ORDER = "_order";
        public const string MIN = "_min";
        public const string MAX = "_max";
        public static readonly ReadOnlyCollection<string> FILTERS = new string[] { PAGE, SIZE, ORDER, MIN, MAX }.AsReadOnly();
        public static readonly ReadOnlyCollection<string> SANITIZED_FILTERS = new string[] { PAGE, SIZE, ORDER, MIN, MAX }.Select(s => s.Replace("_",string.Empty)).ToArray().AsReadOnly();
    }
}
