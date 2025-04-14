using ISP_Desk.Data;

namespace ISP_Desk.ViewModel
{
    public class Stats_VM
    {
        private readonly AppDbContext _context;

        public Stats_VM(AppDbContext context)
        {
            _context = context;
        }

        public async Task InitializeAsync()
        {

        }
    }
}
