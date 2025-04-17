using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Common
{
    public interface IBaseWithCreatedAtEntity
    {
        public DateTime CreatedAt { get; set; }
    }
}
