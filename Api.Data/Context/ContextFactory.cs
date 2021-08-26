using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            var connectionString = "server=localhost;port=3306;database=DbApi;user=root;password=123456";
            var opitionBuilder = new DbContextOptionsBuilder<MyContext>();
            opitionBuilder.UseMySql(connectionString);
            return new MyContext(opitionBuilder.Options);
        }
    }
}
