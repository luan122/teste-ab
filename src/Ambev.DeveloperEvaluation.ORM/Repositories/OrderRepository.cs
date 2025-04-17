using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Ambev.DeveloperEvaluation.Domain.Entities.Aggregates;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class OrderRepository : BaseRepository<Order, DefaultContext>, IOrderRepository
    {
        public OrderRepository(DefaultContext context) : base(context)
        { }

    }
}
