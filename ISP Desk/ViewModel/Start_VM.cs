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
            UserContext.ID = null;
            UserContext.status = null;
            UserContext.name = null;
            UserContext.phone = null;
            UserContext.email = null;
            accounts = await _context.Account.ToListAsync();
            installators = await _context.Installator.ToListAsync();
            leads = await _context.Lead.ToListAsync();
            dispatchers = await _context.Dispatcher.ToListAsync();
        }

        public bool CheckPassword(string pass)
        {
            var installatorsDict = installators.ToDictionary(x => x.InstallatorID);
            var leadsDict = leads.ToDictionary(x => x.LeadID);
            var dispatchersDict = dispatchers.ToDictionary(x => x.DispatcherID);

            foreach (var account in accounts)
            {
                if (CryptoService.Decrypt(account.Password) != pass)
                    continue;

                if (account.InstallatorID.HasValue && installatorsDict.TryGetValue(account.InstallatorID.Value, out var installator))
                {
                    ContextSetter(installator.InstallatorID, "Инсталлятор", installator.FirstName, installator.PhoneNumber, installator.Email);
                    return true;
                }

                if (account.LeadID.HasValue && leadsDict.TryGetValue(account.LeadID.Value, out var lead))
                {
                    ContextSetter(lead.LeadID, "Руководитель", lead.FirstName, lead.PhoneNumber, lead.Email); 
                    return true;
                }

                if (account.DispatcherID.HasValue && dispatchersDict.TryGetValue(account.DispatcherID.Value, out var dispatcher))
                {
                    ContextSetter(dispatcher.DispatcherID, "Диспетчер", dispatcher.FirstName, dispatcher.PhoneNumber, dispatcher.Email);
                    return true;
                }
            }
            return false;
        }

        private void ContextSetter(int ID, string status, string name, string phone, string email)
        {
            UserContext.ID = ID;
            UserContext.status = status;
            UserContext.name = name;
            UserContext.phone = phone;
            UserContext.email = email;
        }
    }
}
