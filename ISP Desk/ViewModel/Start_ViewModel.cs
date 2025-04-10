using ISP_Desk.Data;
using ISP_Desk.Model;
using Microsoft.EntityFrameworkCore;

namespace ISP_Desk.ViewModel
{
    public class Start_ViewModel
    {

        private readonly AppDbContext _context;
        
        public Start_ViewModel(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task InitializeAsync() 
        {

        }
    }
}
