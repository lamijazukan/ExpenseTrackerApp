
using Microsoft.AspNetCore.Mvc;
using ExpenseTrackerApp.Application.Users.Interfaces.Application;
using ExpenseTrackerApp.Contracts.Users;
using AutoMapper;
using ExpenseTrackerApp.Domain.ValueObjects;


namespace ExpenseTrackerApp.WebApi.Controllers.Users;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
public class UsersController : ApiControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetUsersResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
    {
        var result = await _userService.GetUsersAsync(cancellationToken);

        return result.Match(
            users => Ok(_mapper.Map<GetUsersResponse>(users)),
            Problem);
    }

    [HttpGet("{userId}")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserById(
        [FromRoute] Guid userId,
        CancellationToken cancellationToken)
    {
        var result = await _userService.GetUserByIdAsync(userId, cancellationToken);

        return result.Match(
            user => Ok(_mapper.Map<GetUsersResponse>(user)),
            Problem);
    }

    [HttpPost]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateUser(
        [FromBody] CreateUserRequest request,
        CancellationToken cancellationToken)
    {
        var domainPreferences = _mapper.Map<UserPreferences>(request.Preferences);
        var result = await _userService.CreateUserAsync(
            request.Username,
            request.Email,
            request.Password,
            domainPreferences,
            cancellationToken);

        return result.Match(
            user => CreatedAtAction(
                nameof(GetUserById),
                new { userId = user.UserId },
                _mapper.Map<UserResponse>(user)),
            Problem);
    }

    [HttpPatch("{userId}")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> UpdateUser(
        [FromRoute] Guid userId,  
        [FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var domainPreferences = _mapper.Map<UserPreferences>(request.Preferences);
        var result = await _userService.UpdateUserAsync(
            userId,
            request.Username,
            request.Password,
            domainPreferences,
            cancellationToken);
        
        return result.Match(
            user => Ok(_mapper.Map<UserResponse>(user)),
            Problem);
    }
}
