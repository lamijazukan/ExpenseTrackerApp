namespace ExpenseTrackerApp.Contracts.Categories;

public class UpdateCategoryRequest
{
    public string? Name { get; set; }
    public int? ParentCategoryId { get; set; }
}