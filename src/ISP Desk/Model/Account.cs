namespace ISP_Desk.Model
{
    public class Account
    {
        public int AccountID { get; set; }
        public int? DispatcherID { get; set; }
        public int? InstallatorID { get; set; }
        public int? LeadID { get; set; }
        public string Password { get; set; }
    }
}
