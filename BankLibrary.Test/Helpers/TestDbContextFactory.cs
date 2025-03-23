using BankLibrary.Data;
using Microsoft.EntityFrameworkCore;

namespace BankLibrary.Test.Helpers
{
    public static class TestDbContextFactory
    {
        public static BankContext Create()
        {
            var options = new DbContextOptionsBuilder<BankContext>()
                .UseInMemoryDatabase(databaseName: $"BankDb_{Guid.NewGuid()}")
                .Options;

            return new BankContext(options);
        }
    }
}
