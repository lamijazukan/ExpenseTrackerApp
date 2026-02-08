using ErrorOr;
using ExpenseTrackerApp.Application.Authentication.Data;
using ExpenseTrackerApp.Application.Authentication.Interfaces.Application;
using ExpenseTrackerApp.Application.Authentication.Interfaces.Infrastructure;
using ExpenseTrackerApp.Application.Users;
using ExpenseTrackerApp.Application.Users.Interfaces.Infrastructure;
using ExpenseTrackerApp.Domain.Entities;
using ExpenseTrackerApp.Domain.Errors;


namespace ExpenseTrackerApp.Application.Authentication;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtGenerator;
   

    public AuthService(
        IUserRepository userRepository,
        IJwtTokenGenerator jwtGenerator)
    {
        _userRepository = userRepository;
        _jwtGenerator = jwtGenerator;
       
    }

    public async Task<ErrorOr<RegisterResult>> RegisterAsync(
        string username,
        string email,
        string password,
        CancellationToken cancellationToken)
    {
        var validate = UserValidator.ValidateCreateUserRequest(username, email, password);
        var existsResult = await _userRepository.EmailExistsAsync(email, cancellationToken);
        if (existsResult.IsError)
            return existsResult.Errors;

        if (existsResult.Value)
            return UserErrors.DuplicateEmail;

        var user = new User
        {
            UserId = Guid.NewGuid(),
            Username = username,
            Email = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var createResult = await _userRepository.CreateUserAsync(user, cancellationToken);
        if (createResult.IsError)
            return createResult.Errors;

        var token = _jwtGenerator.GenerateToken(user);

        return new  RegisterResult
            {
               UserId = user.UserId,
               Email = user.Email, 
               Username = user.Username, 
               Token = token
                
            };
    }

    public async Task<ErrorOr<LoginResult>> LoginAsync(
        string email,
        string password,
        CancellationToken cancellationToken)
    {
        var userResult = await _userRepository.GetUserByEmailAsync(email, cancellationToken);
        if (userResult.IsError)
            return Error.Unauthorized("Auth.InvalidCredentials", "Invalid credentials");

        var user = userResult.Value;

        var validPassword = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        if (!validPassword)
            return Error.Unauthorized("Auth.InvalidCredentials", "Invalid credentials");

        var token = _jwtGenerator.GenerateToken(user);

        return new LoginResult
            {
               UserId = user.UserId, 
               Email = user.Email, 
               Username = user.Username, 
               Token = token
                
            };
    }
}