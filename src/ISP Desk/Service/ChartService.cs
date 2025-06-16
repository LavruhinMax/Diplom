using ChartJs.Blazor.PieChart;
using ChartJs.Blazor.Util;
using ISP_Desk.Model;

namespace ISP_Desk.Service
{
    public static class ChartService
    {
        public static List<Request> userRequests = new List<Request>();
        public static List<Report> userReports = new List<Report>();
        public static PieConfig InstData = new PieConfig();
        public static PieConfig GeneralData = new PieConfig();
        public static PieDataset<int> instset;
        public static PieDataset<int> genset;

        public static void DrawTopChart()
        {
            if (InstData.Data.Datasets.Contains(instset))
            {
                InstData.Data.Datasets.Remove(instset);
            }
            instset = new PieDataset<int>(new[] {
                userReports.Where(r => r.Status == "Завершен").Count(),
                userRequests.Where(u => u.Closed == null).Count(),
                userReports.Where(r => r.Status == "Передан").Count()
            })
            {
                BackgroundColor = new[]
                    {
                    ColorUtil.ColorHexString(0, 128, 0),
                    ColorUtil.ColorHexString(255, 0, 0),
                    ColorUtil.ColorHexString(255, 255, 0),
                    }
            };
            InstData.Data.Datasets.Add(instset);
        }

        public static void DrawBottomChart()
        {
            if (GeneralData.Data.Datasets.Contains(genset))
            {
                GeneralData.Data.Datasets.Remove(genset);
            }
            genset = new PieDataset<int>(new[] {
                userRequests.Where(u => u.Type == "Нестабильное соединение").Count(),
                userRequests.Where(u => u.Type == "Нет сигнала ТВ").Count(),
                userRequests.Where(u => u.Type == "Неполадки с мобильной связью").Count(),
                userRequests.Where(u => u.Type == "Аппаратные проблемы").Count()
            })
            {
                BackgroundColor = new[]
                    {
                    ColorUtil.ColorHexString(0, 0, 255),
                    ColorUtil.ColorHexString(165, 42, 42),
                    ColorUtil.ColorHexString(128, 128, 0),
                    ColorUtil.ColorHexString(255, 165, 0),
                    }
            };
            GeneralData.Data.Datasets.Add(genset);
        }
    }
}
