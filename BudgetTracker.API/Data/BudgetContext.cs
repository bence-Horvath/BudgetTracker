using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.API.Data
{
    public class BudgetContext: DbContext
    {
        public BudgetContext(DbContextOptions<BudgetContext> options) : base(options)
        {

        }
        public DbSet<Models.Transaction> Transactions { get; set; }
    }
}
