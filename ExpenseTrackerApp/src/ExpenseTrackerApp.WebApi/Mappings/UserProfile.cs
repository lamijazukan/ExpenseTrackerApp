using AutoMapper;
using ExpenseTrackerApp.Application.Users.Data;
using ExpenseTrackerApp.Contracts.Users;
using ExpenseTrackerApp.Domain.Entities;
using ExpenseTrackerApp.Domain.ValueObjects;

namespace ExpenseTrackerApp.WebApi.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        // Contracts → Domain (for CreateUser)
        CreateMap<UserPreferencesObject, UserPreferences>().ReverseMap();
        
        // Domain User → API response
        CreateMap<User, UserResponse>();

        // Application result → API response
        CreateMap<GetUsersResult, GetUsersResponse>();






    } 
}

