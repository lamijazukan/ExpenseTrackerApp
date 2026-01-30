using AutoMapper;
using ExpenseTrackerApp.Application.Users.Data;
using ExpenseTrackerApp.Application.Users.Interfaces.Application;
using ExpenseTrackerApp.Application.Users.Interfaces.Infrastructure;
using ExpenseTrackerApp.Domain.Entities;
using ExpenseTrackerApp.Domain.Errors;
using ErrorOr;
using ExpenseTrackerApp.Domain.ValueObjects;

namespace ExpenseTrackerApp.Application.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<ErrorOr<GetUsersResult<UserResult>>> GetUsersAsync(CancellationToken cancellationToken)
    {
        var result = await _userRepository.GetUsersAsync(cancellationToken);
        if (result.IsError)
        {
            return result.Errors;
        }
        
        return new GetUsersResult<UserResult>
        {
            Users = _mapper.Map<List<UserResult>>(result.Value.Users),
            TotalCount = result.Value.TotalCount,
        };
    }
    
    public async Task<ErrorOr<UserResult>> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var result = await _userRepository.GetUserByIdAsync(userId, cancellationToken);
        
        if (result.IsError)
            return result.Errors;
        
        return _mapper.Map<UserResult>(result.Value);
    }
    
    public async Task<ErrorOr<UserResult>> CreateUserAsync(string username, string email, string password, UserPreferences preferences, CancellationToken cancellationToken)
    {
        var validationResult = UserValidator.ValidateCreateUserRequest(username, email, password, preferences);
        if (validationResult.IsError)
        {
            return validationResult.Errors;
        }
        
        // Check if user with email already exists
        var existingUser = await _userRepository.GetUserByEmailAsync(email, cancellationToken);
        if (!existingUser.IsError)
        {
            return UserErrors.DuplicateEmail;
        }
        
    
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
        
        var user = new User
        {
            Username = username,
            Email = email,
            PasswordHash = passwordHash,
            Preferences = preferences,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
      
        var createResult = await _userRepository.CreateUserAsync(user, cancellationToken);
        if (createResult.IsError)
            return createResult.Errors;
        
        return _mapper.Map<UserResult>(createResult.Value);
    }

    public async Task<ErrorOr<UserResult>> UpdateUserAsync(Guid userId, string? username, string? password,
        UserPreferences? preferences, CancellationToken cancellationToken)
    {
        var validationResult =
            UserValidator.ValidateUpdateUserRequest(username, password, preferences);

        if (validationResult.IsError)
        {
            return validationResult.Errors;
        }

     
        var userResult = await _userRepository.GetUserByIdAsync(userId, cancellationToken);

        if (userResult.IsError)
        {
            return UserErrors.NotFound;
        }

        var user = userResult.Value;

        if (username is not null)
        {
            user.Username = username;
        }

        if (password is not null)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        }

        if (preferences is not null)
        {
            user.Preferences = preferences;
        }
        user.UpdatedAt = DateTime.UtcNow;
        
        var updateResult = await _userRepository.UpdateUserAsync(user, cancellationToken);
        
        if (updateResult.IsError)
            return updateResult.Errors;

        return _mapper.Map<UserResult>(updateResult.Value);
    }
}