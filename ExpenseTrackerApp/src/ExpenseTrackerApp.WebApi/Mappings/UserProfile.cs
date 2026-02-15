using AutoMapper;
using ExpenseTrackerApp.Application.Users.Data;
using ExpenseTrackerApp.Contracts.Users;
using ExpenseTrackerApp.Domain.Entities;


namespace ExpenseTrackerApp.WebApi.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        
        CreateMap<User, UserResult>();

        CreateMap<UserResult, UserResponse>();

        // Application result → API response
        CreateMap<GetUsersResult<UserResult>, GetUsersResponse>();
    } 
}

