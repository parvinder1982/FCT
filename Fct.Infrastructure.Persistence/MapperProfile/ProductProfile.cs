using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fct.Infrastructure.Persistence.MapperProfile
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Domain.Models.Product, Entities.Product>()
                .ForMember(d => d.Id,
                    opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Name,
                    opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Price,
                    opt => opt.MapFrom(s => s.Price))
                 .ForMember(d=> d.Purchase , option => option.Ignore()); 
        }
    }
}
