using ISP_Desk.Data;

namespace ISP_Desk.ViewModel
{
    public class Dispatcher_VM
    {
        private readonly AppDbContext _context;

        public Dispatcher_VM(AppDbContext context)
        {
            _context = context;
        }

        public async Task InitializeAsync()
        {

        }
    }
}
