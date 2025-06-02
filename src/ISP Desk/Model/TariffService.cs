namespace ISP_Desk.Model
{
    public class TariffService
    {
        public int TariffServiceID { get; set; }
        public Tariff Tariff { get; set; }
        public Service Service { get; set; }
    }
}
