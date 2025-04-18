using Ambev.DeveloperEvaluation.Domain.Entities.Aggregates;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class OrderItemRepository : BaseRepository<OrderItem, DefaultContext>, IOrderItemRepository
    {
        public OrderItemRepository(DefaultContext context) : base(context)
        {
        }
    }
}
