using QubicMicroservice.Application.DTOs;

namespace QubicMicroservice.Application.Interfaces;

public interface ITransactionService
{
    Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync();
    Task<TransactionDto> GetTransactionByIdAsync(string id);
    Task<TransactionDto> CreateTransactionAsync(CreateTransactionDto createTransactionDto);
    Task UpdateTransactionAsync(string id, TransactionDto transactionDto);
    Task DeleteTransactionAsync(string id);
}
