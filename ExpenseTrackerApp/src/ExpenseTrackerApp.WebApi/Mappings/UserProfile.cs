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
        
        //
        CreateMap<User, UserResult>();

        CreateMap<UserResult, UserResponse>();

        // Application result → API response
        CreateMap<GetUsersResult<UserResult>, GetUsersResponse>();
    } 
}

