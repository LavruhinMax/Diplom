using ISP_Desk.Data;
using ISP_Desk.Model;
using ISP_Desk.Model.Navigation;
using Microsoft.EntityFrameworkCore;

namespace ISP_Desk.ViewModel
{
    public class CR_VM
    {
        private readonly AppDbContext _context;
        public List<Tariff> tariffs = new List<Tariff>();

        public List<NavItem> NavItems = new List<NavItem>();

        public CR_VM(AppDbContext context)
        {
            _context = context;
        }

        public async Task InitializeAsync()
        {
            tariffs = await _context.Tariff.ToListAsync();
            SetNavItems();
        }

        private void SetNavItems()
        {
            NavItems.Add(new NavItem() { linkName = "Главная", Url = "disp" });
            NavItems.Add(new NavItem() { linkName = "Заявка на подключение", Url = "cr" });
        }
    }
}
