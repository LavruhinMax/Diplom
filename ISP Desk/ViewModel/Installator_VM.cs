using ISP_Desk.Data;
using ISP_Desk.Model;
using ISP_Desk.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace ISP_Desk.ViewModel
{
    public class Installator_VM
    {
        private readonly AppDbContext _context;
        public List<Request> requests = new List<Request>();
        public List<Request> filteredRequests = new List<Request>();
        public List<Installator> installators = new List<Installator>();
        public List<Abonent> abonents = new List<Abonent>();
        public Dictionary<int, Abonent> abonentsDict => abonents.ToDictionary(a => a.AbonentID);

        public System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("ru-RU");
        public string[] weekDays = new string[7] { "ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ", "ВС" };
        public int[] days = new int[7];
        public DateTime currentDay = DateTime.Today;
        public DateTime currentWeekStart;
        public DateTime selectedDate = DateTime.Today;
        public bool pastWeek = false;
        public bool futureWeek = false;

        public Installator_VM(AppDbContext context)
        {
            _context = context;
        }

        public async Task InitializeAsync()
        {
            requests = await _context.Request.ToListAsync();
            abonents = await _context.Abonent.ToListAsync();
            installators = await _context.Installator.ToListAsync();
            filteredRequests = requests.Where(r => r.Scheduled.Day == selectedDate.Day).ToList();
            DrawHeadRow();
        }

        public void DrawHeadRow()
        {
            if(currentDay.DayOfWeek == DayOfWeek.Sunday)
            {
                currentWeekStart = currentDay.AddDays(-6);
            }
            else
                currentWeekStart = DateTime.Today.AddDays(-(int)currentDay.DayOfWeek + 1);
            UpdateWeekDays();
        }
        public void SelectPreviousWeek()
        {
            currentWeekStart = currentWeekStart.AddDays(-7);
            SelectDate(currentWeekStart.AddDays(6));
            SetCurrentWeek();
            UpdateWeekDays();
        }

        public void SelectNextWeek()
        {
            currentWeekStart = currentWeekStart.AddDays(7);
            SelectDate(currentWeekStart);
            SetCurrentWeek();
            UpdateWeekDays();
        }

        public void SetCurrentWeek()
        {
            TimeSpan span = currentDay - currentWeekStart;
            if (span.TotalDays >= 7) pastWeek = true;
            else if(span.TotalDays < 0) futureWeek = true;
            else
            {
                futureWeek = false;
                pastWeek = false;
            }
        }

        public void SelectDate(DateTime Date)
        {
            selectedDate = Date;
            filteredRequests = requests.Where(r => r.Scheduled.Day == selectedDate.Day).ToList();
        }

        private void UpdateWeekDays()
        {
            for (int i = 0; i < 7; i++)
            {
                var date = currentWeekStart.AddDays(i);
                days[i] = date.Day;
            }
        }
    }
}
