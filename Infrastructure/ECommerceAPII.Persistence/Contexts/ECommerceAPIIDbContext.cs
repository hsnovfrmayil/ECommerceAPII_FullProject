﻿using System;
using ECommerceAPII.Domain.Entities;
using ECommerceAPII.Domain.Entities.Common;
using ECommerceAPII.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPII.Persistence.Contexts;

public class ECommerceAPIIDbContext : IdentityDbContext<AppUser,AppRole,string>
{
    public ECommerceAPIIDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<ProductImageFile> ProductImageFiles { get; set; }

    public DbSet<Domain.Entities.File> Files  { get; set; }

    public DbSet<InvoiceFile> InvoiceFiles { get; set; }

    public DbSet<Basket> Baskets { get; set; }

    public DbSet<BasketItem> BasketItems { get; set; }

    public DbSet<CompletedOrder> CompletedOrders { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Order>()
            .HasKey(b => b.Id);

        builder.Entity<Order>()
            .HasIndex(o => o.OrderCode)
             .IsUnique();

        builder.Entity<Basket>().
            HasOne(b => b.Order).
            WithOne(b => b.Basket)
            .HasForeignKey<Order>(b=>b.Id);


        builder.Entity<Order>().
            HasOne(p => p.CompletedOrder)
            .WithOne(p => p.Order).
            HasForeignKey<CompletedOrder>(p=>p.OrderId);

        base.OnModelCreating(builder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var datas= ChangeTracker.Entries<BaseEntity>();

        foreach(var data in datas)
        {
            _ = data.State switch
            {
                EntityState.Added => data.Entity.CreatedTime = DateTime.UtcNow,
                EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
                _ => DateTime.UtcNow
            } ;
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}

