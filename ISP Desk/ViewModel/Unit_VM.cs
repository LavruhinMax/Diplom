using ISP_Desk.Data;

namespace ISP_Desk.ViewModel
{
    public class Unit_VM
    {
        private readonly AppDbContext _context;

        public Unit_VM(AppDbContext context)
        {
            _context = context;
        }

        public async Task InitializeAsync()
        {

        }
    }
}
