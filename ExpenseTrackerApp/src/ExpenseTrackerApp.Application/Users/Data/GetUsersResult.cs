using ExpenseTrackerApp.Domain.Entities;

namespace ExpenseTrackerApp.Application.Users.Data;
 


public class GetUsersResult<T>
{
    public List<T> Users { get; set; }
    
    public int TotalCount { get; set; }
    
}