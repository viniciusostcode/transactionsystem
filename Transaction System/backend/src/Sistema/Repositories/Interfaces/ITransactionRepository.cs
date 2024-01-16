using Sistema.Models;
namespace Sistema.Repositories.Interfaces

{
    public interface ITransactionRepository
    {
        Task<List<TransactionModel>> GetAll();
        Task<TransactionModel> GetTransactionById(int id);
        Task<TransactionModel> AddTransaction(TransactionModel transaction);
        Task<TransactionModel> UpdateTransaction(TransactionModel transactionModel, int id);
        Task<bool> DeleteTransaction(int id);
    }
}
