using Microsoft.EntityFrameworkCore;
using Sistema.Data;
using Sistema.Models;
using Sistema.Models.Enums;
using Sistema.Repositories.Interfaces;
using System.Runtime.CompilerServices;

namespace Sistema.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly TransactionSystemDbContext _dbContextTransaction;

        private readonly IUserRepository _userRepository;
        public TransactionRepository(TransactionSystemDbContext dbContextTransaction, IUserRepository userRepository)
        {
            _dbContextTransaction = dbContextTransaction;
            _userRepository = userRepository;
        }

        public async Task<List<TransactionModel>> AddTransactionList(List<TransactionModel> list)
        {
            List<TransactionModel> transactionList = new List<TransactionModel>();

            if (Equals(list.Count, 0)) throw new Exception("Dados vazios");

            foreach (TransactionModel transactionModel in list)
            {
                TransactionModel transaction = new TransactionModel();

                transaction.Price = transactionModel.Price;
                transaction.Quantity = transactionModel.Quantity;
                transaction.Date = DateTime.Now;
                transaction.Coin = transactionModel.Coin;
                transaction.Profit = transactionModel.Profit;
                transaction.UserId = 1;

                UserModel user = await _userRepository.GetById(1);

                transaction.User = user;

                if (!Enum.TryParse(transactionModel.Situation, out SituationEnum situation)) throw new Exception("The situation is invalid.");

                transaction.Situation = situation.ToString();

                transactionList.Add(transaction);
            }

            await _dbContextTransaction.Transactions.AddRangeAsync(transactionList);
            await _dbContextTransaction.SaveChangesAsync();

            return transactionList;
        }
        public async Task<List<TransactionModel>> GetAll()
        {
            List<TransactionModel>? result = await _dbContextTransaction.Transactions.Include(x => x.User).ToListAsync();
            return result;
        }

        public async Task<TransactionModel> GetTransactionById(int id)
        {
            TransactionModel? result = await _dbContextTransaction.Transactions.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }
        public async Task<TransactionModel> AddTransaction(TransactionModel transactionModel)
        {
            try
            {
                TransactionModel transaction = new TransactionModel();

                if (Equals(transactionModel, null)) throw new Exception("Dados vazios");

                transaction.Price = transactionModel.Price;
                transaction.Quantity = transactionModel.Quantity;
                transaction.Date = DateTime.Now;
                transaction.Coin = transactionModel.Coin;
                transaction.Profit = transactionModel.Profit;
                transaction.UserId = 1;

                UserModel user = await _userRepository.GetById(1);

                transaction.User = user;

                if (!Enum.TryParse(transactionModel.Situation, out SituationEnum situation)) throw new Exception("The situation is invalid.");

                transaction.Situation = situation.ToString();

                await _dbContextTransaction.Transactions.AddAsync(transaction);

                await _dbContextTransaction.SaveChangesAsync();

                return transactionModel;

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while saving the entity changes: " + ex.Message);
            }
        }

        public async Task<bool> DeleteTransaction(int id)
        {
            TransactionModel transaction = await GetTransactionById(id);

            if (transaction.Equals(null)) throw new Exception($"Transaction not found by id: {id}");

            _dbContextTransaction.Transactions.Remove(transaction);

            await _dbContextTransaction.SaveChangesAsync();

            return true;
        }


        public async Task<TransactionModel> UpdateTransaction(TransactionModel transactionModel, int id)
        {
            try
            {
                TransactionModel transaction = await GetTransactionById(id);

                if (transaction.Equals(null)) throw new Exception($"Transaction not found by id: {id}");

                transaction.Price = transactionModel.Price;
                transaction.Quantity = transactionModel.Quantity;
                transaction.Date = transaction.Date;
                transaction.Coin = transactionModel.Coin;
                transaction.Profit = transactionModel.Profit;
                transaction.Situation = transactionModel.Situation;

                await _dbContextTransaction.SaveChangesAsync();

                return transactionModel;

            }
            catch (Exception ex)
            {
                throw new Exception("An error ocurred updating the entity: " + ex.Message);
            }
        }
    }
}
