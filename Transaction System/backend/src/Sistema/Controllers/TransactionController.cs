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
            List<TransactionModel> transactions = await _transactionRepository.GetAll();

            return Ok(transactions);
        } 
        
        [HttpGet("{id}")]
        public async Task<ActionResult<List<TransactionModel>>> GetTransactionById(int id)
        {
            TransactionModel transaction = await _transactionRepository.GetTransactionById(id);
            return Ok(transaction);
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
