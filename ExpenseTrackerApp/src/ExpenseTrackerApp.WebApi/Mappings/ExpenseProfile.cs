using AutoMapper;
using ExpenseTrackerApp.Application.Expenses.Data;
using ExpenseTrackerApp.Contracts.Expenses;
using ExpenseTrackerApp.Domain.Entities;

namespace ExpenseTrackerApp.WebApi.Mappings;

public class ExpenseProfile: Profile
{
    public ExpenseProfile()
    {
        CreateMap<Expense, ExpenseResult>();
        
        CreateMap<ExpenseResult, ExpenseResponse>();

        CreateMap<GetExpensesResult<ExpenseResult>, GetExpensesResponse>();
    }
}