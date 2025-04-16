using ISP_Desk.Data;
using ISP_Desk.Model;
using ISP_Desk.Service;
using Microsoft.EntityFrameworkCore;

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
            installators = await _context.Installator.Where(i => i.LeadID == UserContext.ID).ToListAsync();
        }

        public async Task AddInst(Installator inst)
        {
            installators.Add(inst);
            _context.Installator.Add(inst);
            await _context.SaveChangesAsync();
        }

        
    }
}
