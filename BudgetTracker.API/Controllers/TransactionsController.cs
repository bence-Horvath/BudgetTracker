

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

        public TransactionsController(BudgetManager manager)
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

        [HttpGet("{id}")]

        public IActionResult GetById(Guid id)
        {
            var transaction = _manager.GetTransactionById(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return Ok(transaction);



        }

        [HttpPut("{id}")]

        public IActionResult Update(Guid id, Models.Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return BadRequest("Transaction ID mismatch.");
            }
            var updatedTransaction = _manager.UpdateTransaction(transaction);
            if (updatedTransaction == null)
            {
                return NotFound();
            }
            return Ok(updatedTransaction);
        }

        [HttpDelete("{id}")]

        public IActionResult Delete(Guid id)
        {
            var transaction = _manager.GetTransactionById(id);
            if (transaction == null)
            {
                return NotFound();
            }
            _manager.DeleteTransaction(id);
            return NoContent();
        }
    }
}

