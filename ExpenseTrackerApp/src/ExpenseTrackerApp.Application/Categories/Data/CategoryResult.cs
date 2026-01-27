namespace ExpenseTrackerApp.Application.Categories.Data;

public class CategoryResult
{
    public int CategoryId { get; set; }            
    public string Name { get; set; } = string.Empty;  
    public int? ParentCategoryId { get; set; }
    
}