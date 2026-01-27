using ExpenseTrackerApp.Domain.Entities;

namespace ExpenseTrackerApp.Application.Categories.Data;
 


public class GetCategoriesResult
{
    public List<CategoryResult> Categories { get; set; } = new();
    
    public int TotalCount { get; set; }

}