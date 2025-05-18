using ISP_Desk.Data;
using ISP_Desk.Model;
using ISP_Desk.Service;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ISP_Desk.ViewModel
{
    public class LeadPage_VM
    {
        private readonly AppDbContext _context;
        public List<Installator> installators = new List<Installator>();
        public List<Installator> filteredInstallators = new List<Installator>();
        public List<Request> requests = new List<Request>();
        public List<Request> filteredRequests = new List<Request>();
        public DateTime date = DateTime.Now;
        
        public LeadPage_VM(AppDbContext context)
        {
            _context = context;
        }

        public async Task InitializeAsync()
        {
            installators = await _context.Installator.Where(i => i.LeadID == UserContext.ID && i.Archived == 0).ToListAsync();
            filteredInstallators = installators;
            var installatorIds = installators.Select(i => i.InstallatorID).ToList();
            requests = await _context.Request.ToListAsync();
            filteredRequests = requests.Where(r => installatorIds.Contains(r.InstallatorID) && r.Scheduled.Day == date.Day).ToList();
        }

        public async Task AddInst(Installator inst)
        {
            installators.Add(inst);
            _context.Installator.Add(inst);
            await _context.SaveChangesAsync();
        }

        public void Search(string searchOption)
        {
            if (string.IsNullOrEmpty(searchOption))
            {
                filteredInstallators = installators;
            }
            else
            {
                filteredInstallators = installators.Where(i => i.FirstName.Contains(searchOption, StringComparison.OrdinalIgnoreCase) ||
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
            return _context.Installator.Where(i => i.LeadID == UserContext.ID && i.Archived == 1).ToList();
        }

        public async Task Recover(Installator inst)
        {
            inst.Archived = 0;
            inst.RemovalDate = null;
            installators.Add(inst);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Installator inst)
        {
            installators.Remove(inst);
            _context.Installator.Remove(inst);
            await _context.SaveChangesAsync();
        }
    }
}
