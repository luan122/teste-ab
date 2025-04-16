using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Common
{
    public interface IBaseWithSoftDelete
    {
        public bool IsDeleted { get; set; }
    }
}
