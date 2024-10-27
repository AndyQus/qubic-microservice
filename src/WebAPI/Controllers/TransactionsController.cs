using Microsoft.AspNetCore.Mvc;
using QubicMicroservice.Application.Interfaces;
using QubicMicroservice.Application.DTOs;

namespace QubicMicroservice.WebAPI.Controllers;

[ApiController]
[Route("api/transactions")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionsController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var transactions = await _transactionService.GetAllTransactionsAsync();
        return Ok(transactions);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var transaction = await _transactionService.GetTransactionByIdAsync(id);
        if (transaction == null) return NotFound();
        return Ok(transaction);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTransactionDto createTransactionDto)
    {
        var transactionDto = await _transactionService.CreateTransactionAsync(createTransactionDto);
        return CreatedAtAction(nameof(GetById), new { id = transactionDto.Id }, transactionDto);
    }
}
