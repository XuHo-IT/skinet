using System;
using Core.Entities;
using Core.Interface;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Migrations;

public class ProductRepository : IProductRepository
{
    private readonly StoreContext context;

    public ProductRepository(StoreContext context)
    {
        this.context = context;
    }

    public async Task<IReadOnlyList<Product>> GetProductAsync()
    {
        
        return await context.Products
        .Include(p => p.ProductType)
        .Include(p => p.ProductBrand)
        .ToListAsync();
    }

    public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
    {
       return await context.ProductBrands.ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await context.Products
        .Include(p => p.ProductType)
        .Include(p => p.ProductBrand)
        .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
    {
        return await context.ProductTypes.ToListAsync();
    }
}
