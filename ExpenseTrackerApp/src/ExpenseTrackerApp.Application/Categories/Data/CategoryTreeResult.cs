namespace ExpenseTrackerApp.Application.Categories.Data;

public class CategoryTreeResult
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<CategoryTreeResult> Children { get; set; } = new();
}
