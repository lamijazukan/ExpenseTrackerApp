using AutoMapper;
using ExpenseTrackerApp.Application.Users.Data;
using ExpenseTrackerApp.Application.Users.Interfaces.Application;
using ExpenseTrackerApp.Application.Users.Interfaces.Infrastructure;
using ExpenseTrackerApp.Domain.Entities;
using ExpenseTrackerApp.Domain.Errors;
using ErrorOr;


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
    
    public async Task<ErrorOr<UserResult>> UpdateUserAsync(Guid userId, string? username, string? password, CancellationToken cancellationToken)
    {
        var validationResult =
            UserValidator.ValidateUpdateUserRequest(username, password);

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

        user.UpdatedAt = DateTime.UtcNow;
        
        var updateResult = await _userRepository.UpdateUserAsync(user, cancellationToken);
        
        if (updateResult.IsError)
            return updateResult.Errors;

        return _mapper.Map<UserResult>(updateResult.Value);
    }
}