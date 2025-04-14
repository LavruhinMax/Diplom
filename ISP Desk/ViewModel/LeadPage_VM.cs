using ISP_Desk.Data;

namespace ISP_Desk.ViewModel
{
    public class LeadPage_VM
    {
        private readonly AppDbContext _context;

        public LeadPage_VM(AppDbContext context)
        {
            _context = context;
        }

        public async Task InitializeAsync()
        {

        }
    }
}
