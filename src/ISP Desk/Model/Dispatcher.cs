namespace ISP_Desk.Model
{
    public class Dispatcher
    {
        public int DispatcherID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender {  get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Region { get; set; }
        public Account Account { get; set; }
    }
}
