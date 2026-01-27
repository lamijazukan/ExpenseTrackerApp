namespace ExpenseTrackerApp.Contracts.Categories;

public class CreateCategoryRequest
{
    public string Name { get; set; } = null!;
    public int? ParentCategoryId { get; set; }
}