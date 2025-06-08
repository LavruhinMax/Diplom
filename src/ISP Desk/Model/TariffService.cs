namespace ISP_Desk.Model
{
    public class TariffService
    {
        public int TariffServiceID { get; set; }
        public int TariffID { get; set; }
        public int ServiceID { get; set; }
        public Tariff Tariff { get; set; }
        public Service Service { get; set; }
    }
}
