namespace ISP_Desk.Model
{
    public class Message
    {
        public int MessageID { get; set; }
        public int LeadID { get; set; }
        public int InstallatorID { get; set; }
        public string Text { get; set; }
        public bool isRead { get; set; }
        public DateTime SendingTime { get; set; }
    }
}
