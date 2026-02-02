using AutoMapper;
using ExpenseTrackerApp.Application.Budgets.Data;
using ExpenseTrackerApp.Contracts.Budgets;
using ExpenseTrackerApp.Domain.Entities;

namespace ExpenseTrackerApp.WebApi.Mappings;

public class BudgetProfile: Profile
{
    public BudgetProfile()
    {
        CreateMap<Budget, BudgetResult>();

        CreateMap<BudgetResult, BudgetResponse>();

        CreateMap<GetBudgetsResult<BudgetResult>, GetBudgetsResponse>();

       
    }
}