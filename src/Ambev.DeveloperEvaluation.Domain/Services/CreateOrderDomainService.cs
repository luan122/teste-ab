using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Entities.Aggregates;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Domain.Services
{
    public class CreateOrderDomainService(IProductRepository productRepository) : ICreateOrderDomainService
    {
        public async Task<bool> ValidateProducts(Order order, IMapper mapper)
        {

            var orderItems = await productRepository.Database.AsNoTracking()
                .Where(w => order.Items.Select(s => s.ProductId).Contains(w.Id))
                .ProjectTo<OrderItem>(mapper.ConfigurationProvider).ToListAsync();
            if (orderItems.Count < 1)
                return false;
            _ = orderItems.All(a =>
            {
                var currentValue = order.Items.FirstOrDefault(fd => fd.ProductId == a.Id);
                a.Quantity = currentValue?.Quantity ?? default;
                a.Description = currentValue?.Description ?? default;
                a.IsDeleted = currentValue?.IsDeleted ?? default;

                return true;
            });
            order.Items = orderItems;
            productRepository.ClearTracked();
            return true;
        }
    }
}
