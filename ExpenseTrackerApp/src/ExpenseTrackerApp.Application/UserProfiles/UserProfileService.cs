using AutoMapper;
using ErrorOr;
using ExpenseTrackerApp.Application.UserProfiles.Data;
using ExpenseTrackerApp.Application.UserProfiles.Interfaces.Application;
using ExpenseTrackerApp.Application.UserProfiles.Interfaces.Infrastructure;
using ExpenseTrackerApp.Application.Users.Interfaces.Infrastructure;
using ExpenseTrackerApp.Domain.Enums;
using ExpenseTrackerApp.Domain.Errors;
using ExpenseTrackerApp.Domain.Entities;

namespace ExpenseTrackerApp.Application.UserProfiles;

public class UserProfileService : IUserProfileService
{
    private readonly IUserProfileRepository _repository;
    private readonly ICurrentUser _currentUser;
    private readonly IMapper _mapper;

    public UserProfileService(
        IUserProfileRepository repository,
        ICurrentUser currentUser,
        IMapper mapper)
    {
        _repository = repository;
        _currentUser = currentUser;
        _mapper = mapper;
    }

    public async Task<ErrorOr<UserProfileResult>> GetProfileAsync(CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;

        var profileResult = await _repository.GetProfileByUserIdAsync(userId, cancellationToken);

        if (profileResult.IsError)
            return profileResult.Errors;

        var profile = _mapper.Map<UserProfileResult>(profileResult.Value);

        return new UserProfileResult
        {
            Username = profile.Username,
            Email = profile.Email,
            Language = profile.Language,
            Currency = profile.Currency,
            AvatarUrl = profile.AvatarUrl
        };
    }

    public async Task<ErrorOr<UserProfileResult>> UpdateProfileAsync(
        Language language,
        Currency currency,
        string? avatarUrl,
        CancellationToken cancellationToken)
    {
        var validationResult = UserProfileValidator.ValidateUserProfile(language, currency, avatarUrl);
        
        if (validationResult.IsError)
            return validationResult.Errors;
        
        var userId = _currentUser.UserId;
        

        var profileResult = await _repository.GetProfileByUserIdAsync(userId, cancellationToken);

        if (profileResult.IsError)
            return profileResult.Errors;

        var profile = profileResult.Value;

      
        profile.Language = language;
        profile.Currency = currency;


        if (avatarUrl is not null)
        {
            profile.AvatarUrl = avatarUrl;
        }
      

        var updatedResult = await _repository.UpdateProfileAsync(profile, cancellationToken);

        if (updatedResult.IsError)
            return updatedResult.Errors;

        var updated = _mapper.Map<UserProfileResult>(updatedResult.Value);

        return new UserProfileResult
        {
            Username = updated.Username,
            Email = updated.Email,
            Language = updated.Language,
            Currency = updated.Currency,
            AvatarUrl = updated.AvatarUrl
        };
    }
}
