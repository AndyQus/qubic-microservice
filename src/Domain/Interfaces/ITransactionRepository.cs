using QubicMicroservice.Domain.Entities;

namespace QubicMicroservice.Domain.Interfaces;

public interface ITransactionRepository
{
    Task<IEnumerable<Transaction>> GetAllAsync();
    Task<Transaction> GetByIdAsync(string id);
    Task CreateAsync(Transaction transaction);
    Task UpdateAsync(string id, Transaction transaction);
    Task DeleteAsync(string id);
}
