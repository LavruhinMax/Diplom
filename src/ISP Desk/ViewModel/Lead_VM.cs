using DocumentFormat.OpenXml.Bibliography;
using ISP_Desk.Context;
using ISP_Desk.Data;
using ISP_Desk.Model;
using ISP_Desk.Model.Navigation;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ISP_Desk.ViewModel
{
    public class Lead_VM
    {
        private readonly AppDbContext _context;
        public List<Installator> filteredInstallators = new List<Installator>();
        public List<Request> requests = new List<Request>();
        public List<Request> filteredRequests = new List<Request>();
        public DateTime date = DateTime.Now;
        public List<NavItem> NavItems = new List<NavItem>();

        public Lead_VM(AppDbContext context)
        {
            _context = context;
        }

        public async Task InitializeAsync()
        {
            await GetRequests();
            SetNavItems();
        }

        private async Task GetRequests()
        {
            UserContext.Lead.Installators = await _context.Installator.Where(i => i.LeadID == UserContext.Lead.LeadID && i.isArchived == false).ToListAsync();
            filteredInstallators = UserContext.Lead.Installators;
            var installatorIds = UserContext.Lead.Installators.Select(i => i.InstallatorID).ToList();
            requests = await _context.Request.Where(r => installatorIds.Contains(r.InstallatorID)).ToListAsync();
            filteredRequests = requests.Where(r => installatorIds.Contains(r.InstallatorID) && r.Scheduled.Day == date.Day).ToList();
        }

        private void SetNavItems()
        {
            NavItems.Add(new NavItem() { linkName = "Сотрудники", Url = "lead" });
        }

        public async Task AddInst(Installator inst)
        {
            UserContext.Lead.Installators.Add(inst);
            _context.Installator.Add(inst);
            await _context.SaveChangesAsync();
        }

        public void Search(string searchOption)
        {
            if (string.IsNullOrEmpty(searchOption))
            {
                filteredInstallators = UserContext.Lead.Installators;
            }
            else
            {
                filteredInstallators = UserContext.Lead.Installators.Where(i => i.FirstName.Contains(searchOption, StringComparison.OrdinalIgnoreCase) ||
                i.LastName.Contains(searchOption, StringComparison.OrdinalIgnoreCase) ||
                i.MiddleName.Contains(searchOption, StringComparison.OrdinalIgnoreCase))
                .OrderBy(i => i.LastName)
                .ThenBy(i => i.FirstName)
                .ThenBy(i => i.MiddleName)
                .ToList();
            }
        }

        public List<Installator> GetArchived()
        {
            return _context.Installator.Where(i => i.LeadID == UserContext.Lead.LeadID && i.isArchived == true).ToList();
        }

        public async Task Recover(Installator inst)
        {
            inst.isArchived = false;
            inst.RemovalDate = null;
            await _context.SaveChangesAsync();
            await GetRequests();
        }

        public async Task Delete(Installator inst)
        {
            UserContext.Lead.Installators.Remove(inst);
            _context.Installator.Remove(inst);
            await _context.SaveChangesAsync();
        }
    }
}
