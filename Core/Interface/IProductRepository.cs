using System;
using System.Collections.Generic;
using Core.Entities;

namespace Core.Interface;

public interface IProductRepository
{
  Task<Product> GetProductByIdAsync(int id);
  Task<IReadOnlyList<Product>> GetProductAsync();
  Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
  Task<IReadOnlyList<ProductType>> GetProductTypesAsync();

}
