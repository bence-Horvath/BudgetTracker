using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetTracker.API.Data;
using BudgetTracker.API.Models;


namespace BudgetTracker.API.Services


{
    public class BudgetManager
    {

        private BudgetContext _context;

        private readonly TransactionDataService _dataService;

        public BudgetManager(BudgetContext context)
        {
            _context = context;
        }


        public Transaction AddTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            return transaction;
        }

        public List<Transaction> GetAllTransactions() => _context.Transactions.ToList();


        public decimal GetTotalIncome() =>
            _context.Transactions
                 .Where(t => t.Type == TransactionType.Income)
                 .Sum(t => t.Amount);


        public decimal GetTotalExpense() =>
            _context.Transactions
                .Where(t => t.Type == TransactionType.Expense)
                .Sum(t => t.Amount);


        public decimal GetBalance() => GetTotalIncome() - GetTotalExpense();

        public Transaction? GetTransactionById(Guid id)
        {
            return _context.Transactions.FirstOrDefault(t => t.Id == id);
        }

        public Transaction? UpdateTransaction(Transaction transaction)
        {
            var existingTransaction = _context.Transactions.FirstOrDefault(t => t.Id == transaction.Id);
            if (existingTransaction != null)
            {
                existingTransaction.Date = transaction.Date;
                existingTransaction.Amount = transaction.Amount;
                existingTransaction.Description = transaction.Description;
                existingTransaction.Type = transaction.Type;
                _context.SaveChanges();
            }
            return existingTransaction;
        }

        public void DeleteTransaction(Guid id)
        {
            var transaction = _context.Transactions.FirstOrDefault(t => t.Id == id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
                _context.SaveChanges();
            }

        }
    }
}
