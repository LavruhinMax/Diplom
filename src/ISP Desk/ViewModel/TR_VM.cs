using ISP_Desk.Model;
using ISP_Desk.Model.Navigation;

namespace ISP_Desk.ViewModel
{
    public class TR_VM
    {
        public List<NavItem> NavItems = new List<NavItem>();

        public async Task InitializeAsync()
        {
            SetNavItems();
        }

        private void SetNavItems()
        {
            NavItems.Add(new NavItem() { linkName = "Главная", Url = "disp" });
            NavItems.Add(new NavItem() { linkName = "Заявка на устранение неполадок", Url = "tr" });
        }
    }
}
