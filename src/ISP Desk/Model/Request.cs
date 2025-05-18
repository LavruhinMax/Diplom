namespace ISP_Desk.Model
{
    public class Request
    {
        public int RequestID { get; set; }
        public int InstallatorID { get; set; }
        public int AbonentID { get; set; }
        public string Type { get; set; }
        public string? Description { get; set; }
        public int isDelayed { get; set; }
        public DateTime Created { get; set; }
        public DateTime Scheduled { get; set; }
        public DateTime? Closed { get; set; }
    }
}
