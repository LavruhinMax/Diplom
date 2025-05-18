using DocumentFormat.OpenXml.Spreadsheet;
using ISP_Desk.Model;
using Microsoft.EntityFrameworkCore;

namespace ISP_Desk.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Account> Account { get; set; }
        public DbSet<Abonent> Abonent { get; set; }
        public DbSet<Dispatcher> Dispatcher { get; set; }
        public DbSet<Installator> Installator { get; set; }
        public DbSet<Lead> Lead { get; set; }
        public DbSet<Report> Report { get; set; }
        public DbSet<Request> Request { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Tariff> Tariff { get; set; }
        public DbSet<Model.Service> Service { get; set; }
        public DbSet<TariffService> TariffService { get; set; }

    }
}
