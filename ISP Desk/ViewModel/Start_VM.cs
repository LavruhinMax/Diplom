using DocumentFormat.OpenXml.Spreadsheet;
using ISP_Desk.Data;
using ISP_Desk.Model;
using ISP_Desk.Service;
using Microsoft.EntityFrameworkCore;

namespace ISP_Desk.ViewModel
{
    public class Start_VM
    {
        private readonly AppDbContext _context;
        public List<Account> accounts = new List<Account>();
        public List<Installator> installators = new List<Installator>();
        public List<Lead> leads = new List<Lead>();
        public List<Dispatcher> dispatchers = new List<Dispatcher>();

        public Start_VM(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task InitializeAsync() 
        {
            ContextSetter();
            accounts = await _context.Account.ToListAsync();
            installators = await _context.Installator.ToListAsync();
            leads = await _context.Lead.ToListAsync();
            dispatchers = await _context.Dispatcher.ToListAsync();
        }

        public bool CheckPassword(string mail, string pass)
        {

            foreach (var account in accounts)
            {
                foreach(var i in installators)
                {
                    if (mail == i.Email && CryptoService.Decrypt(account.Password) == pass)
                    {
                        ContextSetter(i.InstallatorID, "Инсталлятор", i.FirstName, i.PhoneNumber, i.Email);
                        return true;
                    }
                }
                foreach(var l in leads)
                {
                    if(mail == l.Email && CryptoService.Decrypt(account.Password) == pass)
                    {
                        ContextSetter(l.LeadID, "Руководитель", l.FirstName, l.PhoneNumber, l.Email);
                        return true;
                    }
                }
                foreach(var d in dispatchers)
                {
                    if (mail == d.Email && CryptoService.Decrypt(account.Password) == pass)
                    {
                        ContextSetter(d.DispatcherID, "Диспетчер", d.FirstName, d.PhoneNumber, d.Email);
                        return true;
                    }
                }
            }
            return false;
        }

        private void ContextSetter(int? ID = null, string? status = null, string? name = null, string? phone = null, string? email = null)
        {
            UserContext.ID = ID;
            UserContext.status = status;
            UserContext.name = name;
            UserContext.phone = phone;
            UserContext.email = email;
        }
    }
}
