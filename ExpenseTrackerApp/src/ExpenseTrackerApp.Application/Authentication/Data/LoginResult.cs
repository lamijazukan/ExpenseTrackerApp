namespace ExpenseTrackerApp.Application.Authentication.Data;

public class LoginResult
{
   public Guid UserId { get; set; }
   public string Email {get;set;}
   public string Username {get;set;}
   public string Token {get;set;}
}