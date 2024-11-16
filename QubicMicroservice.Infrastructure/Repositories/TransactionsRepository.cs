using MongoDB.Driver;
using QubicMicroservice.Domain.Entities;
using QubicMicroservice.Domain.Interfaces;
using QubicMicroservice.Infrastructure.Data;

namespace QubicMicroservice.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly MongoDBContext _context;

    public TransactionRepository(MongoDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Transaction>> GetAllAsync() =>
        await _context.Transactions.Find(_ => true).ToListAsync();

    public async Task<Transaction> GetByIdAsync(string id) =>
        await _context.Transactions.Find(t => t.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Transaction transaction) =>
        await _context.Transactions.InsertOneAsync(transaction);

    public async Task UpdateAsync(string id, Transaction transaction) =>
        await _context.Transactions.ReplaceOneAsync(t => t.Id == id, transaction);

    public async Task DeleteAsync(string id) =>
        await _context.Transactions.DeleteOneAsync(t => t.Id == id);
}
