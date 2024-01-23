using Sistema.Models;
namespace Sistema.Repositories.Interfaces

{
    public interface ITransactionRepository
    {
        Task<List<TransactionModel>> GetAll();
        Task<TransactionModel> GetTransactionById(int id);
        Task<List<TransactionModel>> AddTransactionList(List<TransactionModel> list);
        Task<TransactionModel> AddTransaction(TransactionModel transaction);
        Task<TransactionModel> UpdateTransaction(TransactionModel transactionModel, int id);
        Task<bool> DeleteTransaction(int id);
    }
}
