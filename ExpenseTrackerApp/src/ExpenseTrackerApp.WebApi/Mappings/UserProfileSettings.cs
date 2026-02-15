using AutoMapper;
using ExpenseTrackerApp.Domain.Entities;
using ExpenseTrackerApp.Application.UserProfiles.Data;
using ExpenseTrackerApp.Contracts.UserProfiles;

namespace ExpenseTrackerApp.WebApi.Mappings;

public class UserProfileSettings: Profile
{
    public UserProfileSettings()
    {
        CreateMap<Domain.Enums.Language, Language>();
        
        CreateMap<Domain.Enums.Currency, Currency>();
        
        CreateMap<Domain.Entities.UserProfile, UserProfileResult>()
            .ForMember(dest => dest.Username,
                opt => opt.MapFrom(src => src.User.Username))
            .ForMember(dest => dest.Email,
                opt => opt.MapFrom(src => src.User.Email));

        
        CreateMap<UserProfileResult, UserProfileResponse>();
        
    }
}