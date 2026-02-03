using AutoMapper;
using ExpenseTrackerApp.Application.Transactions.Data;
using ExpenseTrackerApp.Contracts.Transactions;
using ExpenseTrackerApp.Domain.Entities;

namespace ExpenseTrackerApp.WebApi.Mappings;

public class TransactionProfile: Profile
{
    public TransactionProfile()
    {
        CreateMap<Transaction, TransactionResult>();

        CreateMap<TransactionResult, TransactionResponse>();

        CreateMap<GetTransactionsResult<TransactionResult>, GetTransactionsResponse>();

    }
}