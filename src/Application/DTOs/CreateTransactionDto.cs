namespace QubicMicroservice.Application.DTOs;

public class CreateTransactionDto
{
    public required string TransactionType { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public required string Description { get; set; }
}
