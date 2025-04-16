using ISP_Desk.Data;
using ISP_Desk.Model;
using ISP_Desk.Service;
using Microsoft.EntityFrameworkCore;

namespace ISP_Desk.ViewModel
{
    public class Unit_VM
    {
        private readonly AppDbContext _context;
        public Installator inst = new Installator();
        public List<Request> requests = new List<Request>();
        public List<Request> filteredRequests = new List<Request>();
        public List<Abonent> abonents = new List<Abonent>();
        public Dictionary<int, Abonent> abonentsDict => abonents.ToDictionary(a => a.AbonentID);

        public DateTime date = DateTime.Now;
        public string phone;

        public Unit_VM(AppDbContext context)
        {
            _context = context;
        }

        public async Task InitializeAsync(int id)
        {
            inst = _context.Installator.First(i => i.InstallatorID == id);
            requests = await _context.Request.Where(r => r.InstallatorID == id).ToListAsync();
            abonents = await _context.Abonent.ToListAsync();

            phone = $"+7 ({inst.PhoneNumber.Substring(2, 3)}) {inst.PhoneNumber.Substring(5, 3)} {inst.PhoneNumber.Substring(8, 2)} {inst.PhoneNumber.Substring(10, 2)}";
            FilterRequestsByDay();
        }

        public void FilterRequestsByDay() => filteredRequests = requests.Where(r => r.Scheduled.Day == date.Day).ToList();

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

        public async Task EditInst(Installator i)
        {
            var installator = _context.Installator.First(i => i.InstallatorID == inst.InstallatorID);
            installator.FirstName = i.FirstName;
            installator.LastName = i.LastName;
            installator.MiddleName = i.MiddleName;
            installator.Email = i.Email;
            installator.PhoneNumber = i.PhoneNumber;
            inst = i;
        }

    }
}
