using ExpenseTrackerApp.Domain.Entities;

namespace ExpenseTrackerApp.Application.Users.Data;
 


public class GetUsersResult
{
    public List<User> Users { get; set; }
    
    public int TotalCount { get; set; }
    
}