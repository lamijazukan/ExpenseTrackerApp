using AutoMapper;
using ExpenseTrackerApp.Application.Categories.Data;
using ExpenseTrackerApp.Application.Categories.Interfaces.Application;
using ExpenseTrackerApp.Application.Categories.Interfaces.Infrastructure;
using ErrorOr;
using ExpenseTrackerApp.Application.Users.Interfaces.Infrastructure;
using ExpenseTrackerApp.Domain.Entities;
using ExpenseTrackerApp.Domain.Errors;


namespace ExpenseTrackerApp.Application.Categories;

public class CategoryService : ICategoryService
{
   private readonly ICategoryRepository _categoryRepository;
   private readonly IMapper _mapper;
   private readonly ICurrentUser _currentUser;
   
   public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, ICurrentUser currentUser)
   {
      _categoryRepository = categoryRepository;
      _mapper = mapper;
      _currentUser = currentUser;
   }
   
   public async Task<ErrorOr<GetCategoriesResult<CategoryResult>>> GetCategoriesAsync(CancellationToken cancellationToken)
   {
      var userId = _currentUser.UserId;
      var result = await _categoryRepository.GetCategoriesByUserIdAsync(userId, cancellationToken);

      if (result.IsError)
      {
         return result.Errors;
      }
      
     

      return new GetCategoriesResult<CategoryResult>
      {
         Categories = _mapper.Map<List<CategoryResult>>(result.Value.Categories),
         TotalCount = result.Value.TotalCount,
      };
   }
   
   public async Task<ErrorOr<List<CategoryTreeResult>>> GetCategoryTreeAsync(CancellationToken cancellationToken)
   {
      var userId = _currentUser.UserId;
      var result = await _categoryRepository.GetCategoriesByUserIdAsync(userId, cancellationToken);

      if (result.IsError)
         return result.Errors;

      var categories = result.Value.Categories;

      // Build tree
      var lookup = categories.ToLookup(c => c.ParentCategoryId);
      List<CategoryTreeResult> BuildTree(int? parentId)
      {
         return lookup[parentId]
            .Select(c => new CategoryTreeResult
            {
               CategoryId = c.CategoryId,
               Name = c.Name,
               Children = BuildTree(c.CategoryId)
            })
            .ToList();
      }

      var tree = BuildTree(null);
      return tree;
   }
   public async Task<ErrorOr<CategoryResult>> GetCategoryByIdAsync(int categoryId, CancellationToken cancellationToken)
   {
      var userId = _currentUser.UserId;
      var result = await _categoryRepository.GetCategoryByIdAsync(categoryId, userId, cancellationToken);

      if (result.IsError)
         return result.Errors;

      var category = _mapper.Map<CategoryResult>(result.Value);
      if (category == null)
         return CategoryErrors.NotFound;

      
      return category;
   }
   
   public async Task<ErrorOr<CategoryResult>> CreateCategoryAsync(string name, int? parentCategoryId, CancellationToken cancellationToken)
   {
      var userId = _currentUser.UserId;
      // Load parent if exists
      Category? parent = null;
      if (parentCategoryId.HasValue)
      {
         var parentResult = await _categoryRepository.GetCategoryByIdAsync(parentCategoryId.Value, userId, cancellationToken);
         if (parentResult.IsError)
            return parentResult.Errors;

         parent = parentResult.Value;
         if (parent == null!)
            return CategoryErrors.ParentNotFound;
      }

      // Load siblings
      var siblingsResult = await _categoryRepository.GetCategoriesByUserIdAsync(userId, cancellationToken);
      if (siblingsResult.IsError)
         return siblingsResult.Errors;

      var siblings = siblingsResult.Value.Categories
         .Where(c => c.ParentCategoryId == parentCategoryId)
         .ToList();

      // Validate
      var validation = CategoryValidator.ValidateCreateCategoryRequest(name, parent, siblings);
      if (validation.IsError)
         return validation.Errors;

      // Create category
      var category = new Category
      {
         Name = name,
         UserId = userId,
         ParentCategoryId = parentCategoryId
      };

      var createResult = await _categoryRepository.CreateCategoryAsync(category, cancellationToken);
      
      if (createResult.IsError)
         return createResult.Errors;
      
      var result = _mapper.Map<CategoryResult>(createResult.Value);
      return result;
   }
   
   public async Task<ErrorOr<CategoryResult>> UpdateCategoryAsync(int categoryId, string? name, int? parentCategoryId, CancellationToken cancellationToken)
   {
      var userId = _currentUser.UserId;
      // Load category
      var categoryResult = await _categoryRepository.GetCategoryByIdAsync(categoryId, userId, cancellationToken); // you may pass userId if needed
      if (categoryResult.IsError)
         return categoryResult.Errors;

      var category = categoryResult.Value;
      if (category == null!)
         return CategoryErrors.NotFound;

      // Load new parent if provided
      Category? parent = null;
      if (parentCategoryId.HasValue)
      {
         var parentResult = await _categoryRepository.GetCategoryByIdAsync(parentCategoryId.Value, category.UserId, cancellationToken);
         if (parentResult.IsError)
            return parentResult.Errors;

         parent = parentResult.Value;
         if (parent == null!)
            return CategoryErrors.ParentNotFound;
      }

      // Load siblings
      var siblingsResult = await _categoryRepository.GetCategoriesByUserIdAsync(category.UserId, cancellationToken);
      if (siblingsResult.IsError)
         return siblingsResult.Errors;

      var siblings = siblingsResult.Value.Categories
         .Where(c => c.ParentCategoryId == parentCategoryId && c.CategoryId != categoryId)
         .ToList();

      // Validate
      var validation = CategoryValidator.ValidateUpdateCategoryRequest(category, name, parent, siblings);
      if (validation.IsError)
         return validation.Errors;

      // Apply changes
      if (!string.IsNullOrWhiteSpace(name))
         category.Name = name;
      if (parentCategoryId.HasValue)
         category.ParentCategoryId = parentCategoryId;

      var updateResult = await _categoryRepository.UpdateCategoryAsync(category, cancellationToken);
      
      if (updateResult.IsError)
         return updateResult.Errors;
      
      var result = _mapper.Map<CategoryResult>(updateResult.Value);
      return result;
   }
   public async Task<ErrorOr<Success>> DeleteCategoryAsync(int categoryId, CancellationToken cancellationToken)
   {
      var userId = _currentUser.UserId;
      var categoryResult = await _categoryRepository.GetCategoryByIdAsync(categoryId, userId, cancellationToken);
      if (categoryResult.IsError)
         return categoryResult.Errors;

      var category = categoryResult.Value;
      if (category == null!)
         return CategoryErrors.NotFound;

      var validation = CategoryValidator.ValidateDeleteCategoryRequest(category);
      if (validation.IsError)
         return validation.Errors;

      var deleteResult = await _categoryRepository.DeleteCategoryAsync(category, cancellationToken);
      return deleteResult;
   }
}