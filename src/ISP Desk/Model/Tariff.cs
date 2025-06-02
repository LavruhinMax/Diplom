namespace ISP_Desk.Model
{
    public class Tariff
    {
        public int TariffID { get; set; }
        public string TariffName { get; set; }
        public decimal TariffPrice { get; set; }
        public List<Service> Services { get; set; }
    }
}
