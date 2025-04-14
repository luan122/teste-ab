using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Common.Commands
{
    public record FilterCommandRequest
    {
        public int Page { get; set; } = 1!;
        public int Size { get; set; } = 10!;
        public Dictionary<string, string> SearchParams { get; set; } = []!;
        public Dictionary<string, string> MinMaxParams { get; set; } = []!;
        public Dictionary<string, string> OrderParams { get; set; } = []!;
    }
}
