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
        public DateTime date = DateTime.Now;
        

        public LeadPage_VM(AppDbContext context)
        {
            _context = context;
        }

        public async Task InitializeAsync()
        {
            installators = await _context.Installator.Where(i => i.LeadID == UserContext.ID && i.Archived == 0).ToListAsync();
        }

        public async Task AddInst(Installator inst)
        {
            installators.Add(inst);
            _context.Installator.Add(inst);
            await _context.SaveChangesAsync();
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
