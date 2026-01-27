using AutoMapper;
using ExpenseTrackerApp.Application.Categories.Interfaces.Application;
using ExpenseTrackerApp.Contracts.Categories;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerApp.WebApi.Controllers.Categories;


[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/categories")]
[Produces("application/json")]
public class CategoriesController : ApiControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public CategoriesController(ICategoryService categoryService, IMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }

    // GET /categories
    [HttpGet]
    [ProducesResponseType(typeof(GetCategoriesResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
    {
        

        var result = await _categoryService.GetCategoriesAsync(cancellationToken);

        return result.Match(
            success => Ok(_mapper.Map<GetCategoriesResponse>(success)),
            Problem);
    }

    // GET /categories/{id}
    [HttpGet("{categoryId:int}")]
    [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategoryById(
        [FromRoute] int categoryId,
        CancellationToken cancellationToken)
    {
       

        var result = await _categoryService.GetCategoryByIdAsync(categoryId, cancellationToken);

        return result.Match(
            category => Ok(category),
            Problem);
    }

    // GET /categories/tree
    [HttpGet("tree")]
    [ProducesResponseType(typeof(List<CategoryTreeResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategoryTree(CancellationToken cancellationToken)
    {
       

        var result = await _categoryService.GetCategoryTreeAsync( cancellationToken);

        return result.Match(
            tree => Ok(tree),
            Problem);
    }

    // POST /categories
    [HttpPost]
    [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateCategory(
        [FromBody] CreateCategoryRequest request,
        CancellationToken cancellationToken)
    {
        

        var result = await _categoryService.CreateCategoryAsync(
            request.Name,
            request.ParentCategoryId,
            cancellationToken);

        return result.Match(
            category => CreatedAtAction(
                nameof(GetCategoryById),
                new { categoryId = category.CategoryId },
                _mapper.Map<CategoryResponse>(category)),
            Problem);
    }

    // PATCH /categories/{id}
    [HttpPatch("{categoryId:int}")]
    [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateCategory(
        [FromRoute]int categoryId,
        [FromBody] UpdateCategoryRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _categoryService.UpdateCategoryAsync(
            categoryId,
            request.Name,
            request.ParentCategoryId,
            cancellationToken);

        return result.Match(
            category => Ok(_mapper.Map<CategoryResponse>(category)),
            Problem);
    }

    // DELETE /categories/{id}
    [HttpDelete("{categoryId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteCategory(
        [FromRoute ]int categoryId,
        CancellationToken cancellationToken)
    {
        var result = await _categoryService.DeleteCategoryAsync(categoryId, cancellationToken);

        return result.Match(
            _ => NoContent(),
            Problem);
    }
}
