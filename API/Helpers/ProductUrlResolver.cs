using System;
using API.DTO;
using AutoMapper;
using Core.Entities;


namespace API.Helpers;

public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDTO, string>
{
    private readonly IConfiguration config;

    public ProductUrlResolver(IConfiguration config)
    {
        this.config = config;
    }

    public string Resolve(Product source, ProductToReturnDTO destination, string destMember, ResolutionContext context)
    {
        if(!string.IsNullOrEmpty(source.PictureUrl))
        {
            return config["ApiUrl"] + source.PictureUrl;
        }   
        return null;
    }
}
