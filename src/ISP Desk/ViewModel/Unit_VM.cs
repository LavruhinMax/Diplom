using DocumentFormat.OpenXml.Office2010.Excel;
using ISP_Desk.Context;
using ISP_Desk.Data;
using ISP_Desk.Model;
using ISP_Desk.Model.Navigation;
using Microsoft.EntityFrameworkCore;

namespace ISP_Desk.ViewModel
{
    public class Unit_VM
    {
        private readonly AppDbContext _context;
        public Installator inst = new();
        public List<Request> filteredRequests = new();
        public List<Abonent> abonents = new();
        public List<Report> reports = new();
        public Dictionary<int, Abonent> abonentsDict => abonents.ToDictionary(a => a.AbonentID);

        public DateTime date = DateTime.Now;
        public string phone;

        public List<NavItem> NavItems = new();
 
        public Unit_VM(AppDbContext context)
        {
            _context = context;
        }

        public async Task InitializeAsync(int id)
        {
            abonents = await _context.Abonent.ToListAsync();
            reports = await _context.Report.ToListAsync();
            inst = _context.Installator.First(i => i.InstallatorID == id);
            inst.Requests = await _context.Request.Where(r => r.InstallatorID == id).ToListAsync();
            foreach (var r in inst.Requests)
                r.Reports = reports.Where(rp => rp.RequestID == r.RequestID).ToList();
            UserContext.Lead.Messages = await _context.Message.Where(m => m.LeadID == UserContext.Lead.LeadID && m.InstallatorID == inst.InstallatorID).ToListAsync();
            phone = $"+7 ({inst.PhoneNumber.Substring(2, 3)}) {inst.PhoneNumber.Substring(5, 3)} {inst.PhoneNumber.Substring(8, 2)} {inst.PhoneNumber.Substring(10, 2)}";
            FilterRequestsByDay();
            SetNavItems();
        }

        private void SetNavItems()
        {
            NavItems.Add(new NavItem() { linkName = "Сотрудники", Url = "lead" });
            NavItems.Add(new NavItem() { linkName = $"{inst.LastName} {inst.FirstName[0]}.{inst.MiddleName[0]}.", Url = $"unit/{inst.InstallatorID}" });
        }

        public void FilterRequestsByDay() => filteredRequests = inst.Requests.Where(r => r.Scheduled.Date == date.Date).ToList();

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

        public async Task Send(Message message)
        {
            await _context.Message.AddAsync(message);
            await _context.SaveChangesAsync();
        }

        public async Task EditInst(Installator i)
        {
            var installator = _context.Installator.First(i => i.InstallatorID == inst.InstallatorID);
            installator.FirstName = i.FirstName;
            installator.LastName = i.LastName;
            installator.MiddleName = i.MiddleName;
            installator.Email = i.Email;
            installator.PhoneNumber = i.PhoneNumber;
            inst = i;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInst()
        {
            inst.isArchived = true;
            inst.RemovalDate = DateOnly.FromDateTime(DateTime.Now);
            await _context.SaveChangesAsync();
        }

        public void DeleteMes(Message m)
        {
            _context.Message.Remove(m);
            _context.SaveChanges();
        }
    }
}
