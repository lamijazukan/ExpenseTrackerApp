using AutoMapper;
using ExpenseTrackerApp.Application.Statistics.Data;
using ExpenseTrackerApp.Contracts.Statistics;
using ExpenseTrackerApp.Domain.Models;

namespace ExpenseTrackerApp.WebApi.Mappings;

public class StatisticsProfile: Profile
{
    public StatisticsProfile()
    {
        CreateMap<BudgetStatus, BudgetStatusResult>();
        
        CreateMap<BudgetStatusResult, GetBudgetStatusResponse>();
        
        CreateMap<MonthlySavingsResult, GetMonthlySavingsResponse>();
        
        CreateMap<TotalExpenseByCategoryResult, GetTotalExpenseByCategoryResponse>();
        
        
    }
}