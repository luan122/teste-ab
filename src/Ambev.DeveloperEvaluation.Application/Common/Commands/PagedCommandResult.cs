using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Common.Commands
{
    public class PagedCommandResult<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public PagedCommandResult() { }
        public PagedCommandResult(IEnumerable<T> items)
        {
            AddRange(items);
        }

    }
}
