using System;
using ECommerceAPII.Application.Abstractions.Services;
using ECommerceAPII.Application.DTOs.Order;
using ECommerceAPII.Application.Repositories;
using ECommerceAPII.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPII.Persistence.Services;

public class OrderService : IOrderService
{
    readonly IOrderWriteRepository _orderWriteRepository;
    readonly IOrderReadRepository _orderReadRepository;
    readonly ICompletedOrderWriteRepository _completedOrderWriteRepository;
    readonly ICompletedOrderReadRepository _completedOrderReadRepository;

    public OrderService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository,
        ICompletedOrderWriteRepository completedOrderWriteRepository, ICompletedOrderReadRepository completedOrderReadRepository)
    {
        _orderWriteRepository = orderWriteRepository;
        _orderReadRepository = orderReadRepository;
        _completedOrderWriteRepository = completedOrderWriteRepository;
        _completedOrderReadRepository = completedOrderReadRepository;
    }

    public async Task CreateOrderAsync(CreateOrder createOrder)
    {
        var orderCode = (new Random().NextDouble() * 1000000).ToString();
        orderCode.Substring(orderCode.IndexOf(".")+1,orderCode.Length-orderCode.IndexOf("")-1);
        await _orderWriteRepository.AddAsync(new()
        {
            Address=createOrder.Address,
            Description=createOrder.Description,
            Id=Guid.Parse(createOrder.BasketId),
            OrderCode= orderCode
        });

        await _orderWriteRepository.SaveAsync();
    }

    public async Task<ListOrder> GetAllOrdersAsync(int page, int size)
    {
        var query = _orderReadRepository.Table.Include(a => a.Basket)
            .ThenInclude(b => b.User)
             .Include(a => a.Basket)
            .ThenInclude(b => b.BasketItems)
            .ThenInclude(c => c.Product);

        var qu = from order in query
                 join completedOrder in _completedOrderReadRepository.Table
                 on order.Id equals completedOrder.Id into co
                 from _co in co.DefaultIfEmpty()
                 select new
                 {
                     order.Id,
                     order.CreatedTime,
                     order.OrderCode,
                     order.Basket,
                     Completed=_co!=null ?true :false
                 };
           
         var data=query.Skip(page * size).Take(size);
        //.Take((page * size)..size);

        var data2= (from order in data
         join completedOrder in _completedOrderReadRepository.Table
         on order.Id equals completedOrder.Id into co
         from _co in co.DefaultIfEmpty()
         select new
         {
             Id = order.Id,
             CreatedTime = order.CreatedTime,
             OrderCode = order.OrderCode,
             Basket = order.Basket,
             Completed = _co != null ? true : false
         });

        return new()
        {
            TotalOrderCount = await query.CountAsync(),
            Orders = await data2.Select(p => new
            {
                Id=p.Id, 
                CreatedDate=p.CreatedTime,
                OrderCode=p.OrderCode,
                TotalPrice=p.Basket.BasketItems.Sum(b=>b.Product.Price*b.Quantity),
                UserName=p.Basket.User.UserName,
                p.Completed
            }).ToListAsync()
        };
    }

    public async Task<SingleOrder> GetOrderByIdAsync(string id)
    {
        var data =await  _orderReadRepository.Table
            .Include(p => p.Basket)
            .ThenInclude(b => b.BasketItems)
            .ThenInclude(b => b.Product)
            .FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));

        return new()
        {
            Id=data.Id.ToString(),
            BasketItems = data.Basket.BasketItems.Select(p => new
            {
                p.Product.Name,
                p.Product.Price,
                p.Quantity
            }),
            Address=data.Address,
            CreatedDate=data.CreatedTime,
            Description=data.Description,
            OrderCode=data.OrderCode
        };
    }

    public async Task CompletedOrderAsync(string id)
    {
        Order order= await _orderReadRepository.GetByIdAsync(id);
        if (order != null)
        {
            await _completedOrderWriteRepository.AddAsync(new()
            {
                OrderId = Guid.Parse(id)
            }) ;
            await _completedOrderWriteRepository.SaveAsync();
        }
    } 
}

