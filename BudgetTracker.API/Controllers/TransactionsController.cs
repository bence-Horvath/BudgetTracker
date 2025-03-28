

using Microsoft.AspNetCore.Mvc;
using BudgetTracker.API.Models;
using BudgetTracker.API.Services;
using System.Transactions;

namespace BudgetTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly BudgetManager _manager;

        public TransactionsController(BudgetManager manager )
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var transactions = _manager.GetAllTransactions();
            return Ok(transactions);
        }

        [HttpPost]
        public IActionResult Post(Models.Transaction transaction)
        {
            transaction.Id = Guid.NewGuid();    
            transaction.Date = DateTime.Now;     

            _manager.AddTransaction(transaction);  

            return Ok(transaction);               
        }

        public class TotalsDto
        {
            public decimal TotalIncome { get; set; }
            public decimal TotalExpense { get; set; }
            public decimal Balance { get; set; }
        }

        [HttpGet("totals")]
        public IActionResult Totals()
        {
            var income = _manager.GetTotalIncome();
            var expense = _manager.GetTotalExpense();
            var balance = _manager.GetBalance();

            var dto = new TotalsDto
            {
                TotalIncome = income,
                TotalExpense = expense,
                Balance = balance
            };

            return Ok(dto);
        }



    }
}

