using AutoMapper;
using Similar_products.Domain.Entities;
using Similar_products.Application.Dtos;

namespace Similar_products.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
		CreateMap<Product, ProductDto>();
		CreateMap<ProductForCreationDto, Product>();
		CreateMap<ProductForUpdateDto, Product>();

		CreateMap<Enterprise, EnterpriseDto>();
		CreateMap<EnterpriseForCreationDto, Enterprise>();
		CreateMap<EnterpriseForUpdateDto, Enterprise>();

		CreateMap<ProductionPlan, ProductionPlanDto>();
		CreateMap<ProductionPlanForCreationDto, ProductionPlan>();
		CreateMap<ProductionPlanForUpdateDto, ProductionPlan>();

		CreateMap<ProductType, ProductTypeDto>();
		CreateMap<ProductTypeForCreationDto, ProductType>();
		CreateMap<ProductTypeForUpdateDto, ProductType>();

		CreateMap<SalesPlan, SalesPlanDto>();
		CreateMap<SalesPlanForCreationDto, SalesPlan>();
		CreateMap<SalesPlanForUpdateDto, SalesPlan>();

        CreateMap<User, UserDto>();
        CreateMap<UserForCreationDto, User>();
        CreateMap<UserForUpdateDto, User>();
    }
}

