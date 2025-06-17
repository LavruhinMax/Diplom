using ChartJs.Blazor.PieChart;
using ChartJs.Blazor.Util;
using DocumentFormat.OpenXml.Bibliography;
using ISP_Desk.Model;

namespace ISP_Desk.Service
{
    public static class ChartService
    {
        public static PieConfig InstData = new();
        public static PieConfig GeneralData = new();
        public static PieDataset<int> instset;
        public static PieDataset<int> genset;

        public static void DrawTopChart(int[] set)
        {          
            InstData.Data.Datasets.Remove(instset);
            instset = new PieDataset<int>(set[..3])
            {
                BackgroundColor = new[]
                {
                    ColorUtil.ColorHexString(0, 128, 0),
                    ColorUtil.ColorHexString(255, 0, 0),
                    ColorUtil.ColorHexString(255, 255, 0)
                }
            };
            InstData.Data.Datasets.Add(instset);
        }

        public static void DrawBottomChart(int[] set)
        {
            GeneralData.Data.Datasets.Remove(genset);
            genset = new PieDataset<int>(set[..4])
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
