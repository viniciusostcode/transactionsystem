using Microsoft.AspNetCore.Mvc;
using Sistema.Models;
using Sistema.Repositories.Interfaces;

namespace Sistema.Controllers
{
    [Route("transaction")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<TransactionModel>>> GetAllTransactions()
        {
            try
            {
                List<TransactionModel> transactions = await _transactionRepository.GetAll();

                if (transactions != null && transactions.Count > 0) return Ok(transactions);

                return NotFound("Result not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionModel>> GetTransactionById(int id)
        {
            try
            {
                TransactionModel transactions = await _transactionRepository.GetTransactionById(id);

                if (transactions != null) return Ok(transactions);

                return NotFound("Result not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TransactionModel>> AddTransaction([FromBody] TransactionModel transactionModel)
        {
            TransactionModel transaction = await _transactionRepository.AddTransaction(transactionModel);

            return Ok(transaction);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TransactionModel>> UpdateTransaction(int id, [FromBody] TransactionModel transactionModel)
        {
            TransactionModel transaction = await _transactionRepository.UpdateTransaction(transactionModel, id);

            return Ok(transaction);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TransactionModel>> DeleteTransaction(int id)
        {
            bool deleted = await _transactionRepository.DeleteTransaction(id);

            return Ok(deleted);
        }

    }
}
