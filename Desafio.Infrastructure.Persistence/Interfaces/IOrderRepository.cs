﻿using Desafio.Core.Domain.Entities;

namespace Desafio.Infrastructure.Persistence.Interfaces;

public interface IOrderRepository
{
    Task<int> CreateAsync(OrderEntity request);    
    Task<OrderEntity> GetByIdAsync(int id);
    Task<List<OrderEntity>> GetAllAsync();
}