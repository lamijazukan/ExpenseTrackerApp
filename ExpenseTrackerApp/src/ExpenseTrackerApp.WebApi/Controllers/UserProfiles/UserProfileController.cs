using System.Security.Claims;
using AutoMapper;
using ExpenseTrackerApp.Application.UserProfiles.Interfaces.Application;
using ExpenseTrackerApp.Contracts.UserProfiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ExpenseTrackerApp.WebApi.Controllers.UserProfiles;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[Authorize]
[Produces("application/json")]

public class UserProfileController : ApiControllerBase
{
    private readonly IUserProfileService _profileService;
    private readonly IMapper _mapper;

    public UserProfileController(IUserProfileService profileService, IMapper mapper)
    {
        _profileService = profileService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(UserProfileResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProfile(CancellationToken cancellationToken)
    {

        var result = await _profileService.GetProfileAsync(cancellationToken);
        return result.Match(
            profile => Ok(_mapper.Map<UserProfileResponse>(profile)),
            Problem);
    }

    [HttpPatch]
    [ProducesResponseType(typeof(UserProfileResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateProfile(
        [FromBody ]UpdateUserProfileRequest request, CancellationToken cancellationToken)
    {
        
        var result = await _profileService.UpdateProfileAsync((Domain.Enums.Language) request.Language, (Domain.Enums.Currency) request.Currency, request.AvatarUrl, cancellationToken);
        return result.Match(
            profile => Ok(_mapper.Map<UserProfileResponse>(profile)),
            Problem);
    }
}