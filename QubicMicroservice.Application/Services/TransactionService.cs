using QubicMicroservice.Application.DTOs;
using QubicMicroservice.Application.Interfaces;
using QubicMicroservice.Domain.Entities;
using QubicMicroservice.Domain.Interfaces;

namespace QubicMicroservice.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync()
    {
        var transactions = await _transactionRepository.GetAllAsync();
        return transactions.Select(t => new TransactionDto
        {
            Id = t.Id,
            TransactionType = t.TransactionType,
            Amount = t.Amount,
            Date = t.Date,
            Description = t.Description
        });
    }

    public async Task<TransactionDto> GetTransactionByIdAsync(string id)
    {
        var transaction = await _transactionRepository.GetByIdAsync(id);
        return new TransactionDto
        {
            Id = transaction.Id,
            TransactionType = transaction.TransactionType,
            Amount = transaction.Amount,
            Date = transaction.Date,
            Description = transaction.Description
        };
    }

    public async Task<TransactionDto> CreateTransactionAsync(CreateTransactionDto createTransactionDto)
    {
        var transaction = new Transaction
        {
            TransactionType = createTransactionDto.TransactionType,
            Amount = createTransactionDto.Amount,
            Date = createTransactionDto.Date,
            Description = createTransactionDto.Description
        };
        await _transactionRepository.CreateAsync(transaction);
        return new TransactionDto
        {
            Id = transaction.Id,
            TransactionType = transaction.TransactionType,
            Amount = transaction.Amount,
            Date = transaction.Date,
            Description = transaction.Description
        };

    }

    public async Task UpdateTransactionAsync(string id, TransactionDto transactionDto)
    {
        var transaction = new Transaction
        {
            TransactionType = transactionDto.TransactionType,
            Amount = transactionDto.Amount,
            Date = transactionDto.Date,
            Description = transactionDto.Description
        };
        await _transactionRepository.UpdateAsync(id, transaction);
    }

    public async Task DeleteTransactionAsync(string id) => await _transactionRepository.DeleteAsync(id);
}
