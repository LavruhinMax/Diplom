using DocumentFormat.OpenXml.Office2010.Excel;
using ISP_Desk.Data;
using ISP_Desk.Model;
using ISP_Desk.Model.Navigation;
using Microsoft.EntityFrameworkCore;

namespace ISP_Desk.ViewModel
{
    public class Dispatcher_VM
    {
        private readonly AppDbContext _context;
        public List<Request> requests = new List<Request>();
        public List<Request> filteredRequests = new List<Request>();
        public List<Abonent> abonents = new List<Abonent>();
        public Dictionary<int, Abonent> abonentsDict => abonents.ToDictionary(a => a.AbonentID);

        public DateTime date = DateTime.Now;
        public List<NavItem> NavItems = new List<NavItem>();

        public Dispatcher_VM(AppDbContext context)
        {
            _context = context;
        }

        public async Task InitializeAsync()
        {
            requests = await _context.Request.ToListAsync();
            abonents = await _context.Abonent.ToListAsync();
            FilterRequestsByDay();
            SetNavItems();
        }

        private void SetNavItems()
        {
            NavItems.Add(new NavItem() { linkName = "Сотрудники", Url = "lead" });
        }

        public void FilterRequestsByDay() => filteredRequests = requests.Where(r => r.Scheduled.Date == date.Date).ToList();

        public void AddDay()
        {
            date = date.AddDays(1);
            FilterRequestsByDay();
        }

        public void RemoveDay()
        {
            date = date.AddDays(-1);
            FilterRequestsByDay();
        }
    }
}
