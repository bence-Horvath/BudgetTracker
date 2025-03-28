using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetTracker.API.Models;


namespace BudgetTracker.API.Services


{
    public class BudgetManager
    {
        private List<Transaction> Transactions = new List<Transaction> ();

        public Transaction AddTransaction(Transaction transaction)
        {
            Transactions.Add (transaction);

            return transaction;
        }

        public List<Transaction> GetAllTransactions() => Transactions;


        public decimal GetTotalIncome()
        {
            decimal total = 0;
            foreach (var transaction in Transactions)
            {

                if (transaction.Type == TransactionType.Income)
                {
                    total += transaction.Amount;
                }
            }
            return total;
        }

        public decimal GetTotalExpense()
        {
            decimal total = 0;
            foreach (var transaction in Transactions)
            {

                if (transaction.Type == TransactionType.Expense)
                {
                    total += transaction.Amount;
                }
            }
            return total;
        }

        public decimal GetBalance() => GetTotalIncome() - GetTotalExpense();






    }
}
