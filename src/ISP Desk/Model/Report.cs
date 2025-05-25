namespace ISP_Desk.Model
{
    public class Report
    {
        public int ReportID { get; set; }
        public int RequestID { get; set; }
        public string Status { get; set; }
        public string Commentary { get; set; }
        public Request Request { get; set; }
    }
}
