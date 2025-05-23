﻿namespace ISP_Desk.Model
{
    public class Installator
    {
        public int InstallatorID { get; set; }
        public int LeadID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int Archived { get; set; }
        public DateOnly? RemovalDate { get; set; }
    }
}
