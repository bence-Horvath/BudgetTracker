using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracker.API.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }  
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public TransactionType Type { get; set; }
    }
}
