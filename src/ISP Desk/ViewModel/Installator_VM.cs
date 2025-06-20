﻿using ISP_Desk.Constants;
using ISP_Desk.Context;
using ISP_Desk.Data;
using ISP_Desk.Model;
using ISP_Desk.Model.Navigation;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Threading.Tasks;

namespace ISP_Desk.ViewModel
{
    public class Installator_VM
    {
        private readonly AppDbContext _context;
        public List<Request> currentRequests = new();
        public List<Abonent> abonents = new();
        public Lead lead = new();
        public int count = 0;
        public Dictionary<int, Abonent> abonentsDict => abonents.ToDictionary(a => a.AbonentID);

        public string[] weekDays = new string[7] { "ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ", "ВС" };
        public int[] days = new int[7];
        public DateTime currentDay = DateTime.Today, selectedDate = DateTime.Now;
        public DateTime currentWeekStart;
        public bool pastWeek = false, futureWeek = false;
        public List<NavItem> NavItems = new();

        public Installator_VM(AppDbContext context)
        {
            _context = context;
        }

        public async Task InitializeAsync()
        {
            abonents = await _context.Abonent.ToListAsync();
            UserContext.Installator.Requests = await _context.Request.Where(r => r.InstallatorID == UserContext.Installator.InstallatorID).ToListAsync();
            UserContext.Installator.Messages = await _context.Message.Where(m => m.InstallatorID == UserContext.Installator.InstallatorID).ToListAsync();
            count = UserContext.Installator.Messages.Where(m => m.isRead == false).Count();
            currentRequests = UserContext.Installator.Requests.Where(r => r.Scheduled.Date == selectedDate.Date).ToList();
            lead = _context.Lead.First(l => l.LeadID == UserContext.Installator.LeadID);
            SetNavItems();
            DrawHeadRow();
        }

        private void SetNavItems()
        {
            NavItems.Add(new NavItem() { linkName = "Расписание", Url = "inst" });
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
            currentRequests = UserContext.Installator.Requests.Where(r => r.Scheduled.Date == selectedDate.Date).ToList();
        }

        private void UpdateWeekDays()
        {
            for (int i = 0; i < 7; i++)
            {
                var date = currentWeekStart.AddDays(i);
                days[i] = date.Day;
            }
        }

        public bool CheckIfDayoff(DateOnly date)
        {
            foreach(var day in Dayoffs.dayoffs)
                if(date.Month == day.Month && date.Day == day.Day) return true;
            return false;
        }
        
        public void SetZero()
        {
            count = 0;
            foreach(var m in UserContext.Installator.Messages)
                m.isRead = true;
            _context.SaveChanges();
        }
    }
}
