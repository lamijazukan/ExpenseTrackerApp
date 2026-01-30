using AutoMapper;
using ExpenseTrackerApp.Application.Categories.Data;
using ExpenseTrackerApp.Contracts.Categories;
using ExpenseTrackerApp.Domain.Entities;

namespace ExpenseTrackerApp.WebApi.Mappings;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryResult>();
        
        CreateMap<CategoryResult, CategoryResponse>();

        CreateMap<GetCategoriesResult<CategoryResult>, GetCategoriesResponse>();
        
    }
}