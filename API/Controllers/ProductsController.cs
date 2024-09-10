using System;
using API.DTO;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interface;
using Core.Specification;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;


public class ProductsController : BaseApiController
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
   public async Task<ActionResult<IReadOnlyList<ProductToReturnDTO>>> GetProducts(
    string sort = null, int? brandId =null, int? typeId=null){
     var spec = new ProductsWithTypesAndBrandsSpecification(sort, brandId, typeId);
     var products = await productsRepo.ListAsync(spec);
     return Ok(mapper.Map<IReadOnlyList<Product>,
      IReadOnlyList<ProductToReturnDTO>>(products));
   }


   [HttpGet("{id}")]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int id){
    var spec = new ProductsWithTypesAndBrandsSpecification(id);
    var product = await productsRepo.GetEntityWithSpec(spec);
    if (product == null) return NotFound(new ApiResponse(404));
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
