namespace ISP_Desk.Model
{
    public class Service
    {
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public decimal AdditionalPrice { get; set; }
        public List<TariffService> TariffServices { get; set; }
    }
}