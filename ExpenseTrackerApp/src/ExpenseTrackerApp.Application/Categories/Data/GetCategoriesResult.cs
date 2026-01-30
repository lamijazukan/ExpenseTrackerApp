using ExpenseTrackerApp.Domain.Entities;

namespace ExpenseTrackerApp.Application.Categories.Data;
 


public class GetCategoriesResult<T>
{
    public List<T> Categories { get; set; } = new();
    
    public int TotalCount { get; set; }

}