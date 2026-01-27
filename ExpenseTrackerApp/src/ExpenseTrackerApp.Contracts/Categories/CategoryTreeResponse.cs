namespace ExpenseTrackerApp.Contracts.Categories;

public class CategoryTreeResponse
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = null!;
    public List<CategoryTreeResponse> Children { get; set; } = new();
}