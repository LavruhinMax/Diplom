using ChartJs.Blazor.PieChart;
using ClosedXML.Excel;
using System.Drawing;

namespace ISP_Desk.Service
{
    public static class ExcelService
    {
        public static void ExportToExcel(int[] instset, int[] genset, string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Data");

                ExportDataset(worksheet, instset, "Сет 1", 1);

                ExportDataset(worksheet, genset, "Сет 2", instset.Length + 4);

                workbook.SaveAs(filePath);
            }
        }

        private static void ExportDataset(IXLWorksheet worksheet, int[] dataset, string setName, int startRow)
        {
            var headerCell = worksheet.Cell(startRow, 1);
            headerCell.Value = setName;
            headerCell.Style.Fill.BackgroundColor = XLColor.Gray;
            headerCell.Style.Font.Bold = true;

            for (int i = 0; i < dataset.Length; i++)
            {
                worksheet.Cell(startRow + 1 + i, 1).Value = $"Строка {i + 1}";
                worksheet.Cell(startRow + 1 + i, 2).Value = dataset[i];
            }
        }
    }
}
