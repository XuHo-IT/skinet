using System;
using API.DTO;
using AutoMapper;
using Core.Entities;
using Core.Interface;
using Core.Specification;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IGenericRepository<Product> productsRepo;
    private readonly IGenericRepository<ProductBrand> productsBrandRepo;
    private readonly IGenericRepository<ProductType> productsTypeRepo;
    private readonly IMapper mapper;

    public ProductsController(IGenericRepository<Product> productsRepo,
    IGenericRepository<ProductBrand> productsBrandRepo,
     IGenericRepository<ProductType> productsTypeRepo,
     IMapper mapper)
    {
        this.productsRepo = productsRepo;
        this.productsBrandRepo = productsBrandRepo;
        this.productsTypeRepo = productsTypeRepo;
        this.mapper = mapper;
    }

    [HttpGet]
   public async Task<ActionResult<IReadOnlyList<ProductToReturnDTO>>> GetProducts(){
     var spec = new ProductsWithTypesAndBrandsSpecification();
     var products = await productsRepo.ListAsync(spec);
     return Ok(mapper.Map<IReadOnlyList<Product>,
      IReadOnlyList<ProductToReturnDTO>>(products));
   }


   [HttpGet("{id}")]
   public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int id){
    var spec = new ProductsWithTypesAndBrandsSpecification(id);
    var product = await productsRepo.GetEntityWithSpec(spec);
    return mapper.Map<Product, ProductToReturnDTO>(product);
   }

   [HttpGet("brands")]
   public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductsBrand()
   {
    return Ok( await productsBrandRepo.ListAllAsync());
   }

    [HttpGet("types")]
   public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductsTypes()
   {
    return Ok( await productsTypeRepo.ListAllAsync());
   }

}
