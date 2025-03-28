using System.Collections.Generic;
using System.Text.Json;
using BudgetTracker.API.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BudgetTracker.API.Data
{
    public class TransactionDataService
    {
        private readonly string _filePath = "transactions.json";

        public void SaveTransactions(List<Transaction> transactions)
        {
            var json = JsonSerializer.Serialize(transactions); 
            System.IO.File.WriteAllText(_filePath, json);
        }

        public List<Transaction> LoadTransactions()
        {
            
            if (!File.Exists(_filePath))
            {
                return new List<Transaction>();
            }

            var json = File.ReadAllText(_filePath);

            if (!string.IsNullOrWhiteSpace(json))
            {
                var transactions = JsonSerializer.Deserialize<List<Transaction>>(json) ?? new List<Transaction>();
                return transactions;
            }
            return new List<Transaction>();
        }
    }
}
