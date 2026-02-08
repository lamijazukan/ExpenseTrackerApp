using AutoMapper;
using ExpenseTrackerApp.Application.Authentication.Interfaces.Application;
using ExpenseTrackerApp.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerApp.WebApi.Controllers.Authentication;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
public class AuthController : ApiControllerBase
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public AuthController(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
 
    public async Task<IActionResult> Register(RegisterRequest request, CancellationToken cancellationToken)
    {
        var result = await _authService.RegisterAsync(
            request.Username,
            request.Email,
            request.Password,
            cancellationToken);

      
        return result.Match(
            r => Ok(new AuthResponse
            {
                UserId = r.UserId,
                Email = r.Email,
                Username = r.Username,
                Token = r.Token
            }),
            Problem);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    
    public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        var result = await _authService.LoginAsync(
            request.Email,
            request.Password,
            cancellationToken);

        return result.Match(
            r => Ok(new AuthResponse
            {
                UserId = r.UserId,
                Email = r.Email,
                Username = r.Username,
                Token = r.Token
            }),
            Problem);
    }
}
