using System.Net.Http.Headers;
using SalaryManagement.Domain.GajiDomain;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.Pdf.Tables;

namespace SalaryManagement.Blazor.Service;

public class PDFGenerator(IWebHostEnvironment webHost)
{
    public MemoryStream CreatePDF(ReportGaji reportGaji)
    {
        PdfDocument document = new();
        PdfPage currentPage = document.Pages.Add();
        SizeF clientSize = currentPage.GetClientSize();
        FileStream imageStream = new FileStream(webHost.WebRootPath + "//images//pdf//monitor.jpg", FileMode.Open, FileAccess.Read);
        PdfImage icon = new PdfBitmap(imageStream);
        SizeF iconSize = new SizeF(60, 60);
        PointF iconLocation = new(14, 13);
        PdfGraphics graphics = currentPage.Graphics;
        graphics.DrawImage(icon, iconLocation, iconSize);
        PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20, PdfFontStyle.Bold);
        var headerText = new PdfTextElement("PT MULTI GUNA", font, new PdfSolidBrush(Color.Cyan));
        headerText.StringFormat = new PdfStringFormat(PdfTextAlignment.Right);
        PdfLayoutResult result = headerText.Draw(currentPage, new PointF(clientSize.Width - 25, iconLocation.Y + 10));

        font = new PdfStandardFont(PdfFontFamily.Helvetica, 10, PdfFontStyle.Bold);
        headerText = new PdfTextElement("Slip Gaji Untuk", font);
        result = headerText.Draw(currentPage, new PointF(14, result.Bounds.Bottom + 30));
        font = new PdfStandardFont(PdfFontFamily.Helvetica, 14, PdfFontStyle.Bold);
        headerText = new PdfTextElement(reportGaji.NamaPegawai.ToUpper(), font);
        result = headerText.Draw(currentPage, new PointF(14, result.Bounds.Bottom + 3));
        font = new PdfStandardFont(PdfFontFamily.Helvetica, 10);
        headerText = new PdfTextElement($"Tanggal : {reportGaji.Tanggal.ToShortDateString()}", font);
        result = headerText.Draw(currentPage, new PointF(14, result.Bounds.Bottom + 3));

        font = new PdfStandardFont(PdfFontFamily.Helvetica, 10, PdfFontStyle.Bold);
        headerText = new PdfTextElement($"Laporan Gaji No. {reportGaji.NomerPegawai}", font);
        headerText.StringFormat = new PdfStringFormat(PdfTextAlignment.Right);
        headerText.Draw(currentPage, new PointF(clientSize.Width - 25, result.Bounds.Y - 20));

        PdfGrid grid = new PdfGrid();
        font = new PdfStandardFont(PdfFontFamily.Helvetica, 10, PdfFontStyle.Regular);
        grid.Style.Font = font;
        grid.Columns.Add(4);
        grid.Columns[1].Width = 70;
        grid.Columns[2].Width = 70;
        grid.Columns[3].Width = 70;

        grid.Headers.Add(1);
        PdfStringFormat stringFormat = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);
        PdfGridRow headerRow = grid.Headers[0];

        headerRow.Cells[0].Value = "Penghasilan";
        headerRow.Cells[0].StringFormat.LineAlignment = PdfVerticalAlignment.Middle;
        headerRow.Cells[3].Value = "Jumlah";
        headerRow.Cells[3].StringFormat = stringFormat;

        PdfGridCellStyle cellStyle = new();
        cellStyle.Borders.All = PdfPens.Transparent;
        cellStyle.TextBrush = PdfBrushes.White;
        cellStyle.BackgroundBrush = new PdfSolidBrush(Color.Gray);

        for (int i = 0; i < headerRow.Cells.Count; i++)
        {
            PdfGridCell cell = headerRow.Cells[i];
            cell.Style = cellStyle;
        }

        PdfGridRow row = grid.Rows.Add();
        row.Cells[0].Value = "Gaji Pokok";
        row.Cells[0].StringFormat.LineAlignment = PdfVerticalAlignment.Middle;

        decimal gajiPokok = reportGaji.GajiPokok;
        row.Cells[3].Value = gajiPokok.ToString("0.00");
        row.Cells[3].StringFormat = stringFormat;

        row = grid.Rows.Add();
        row.Cells[0].Value = "Uang Makan";
        row.Cells[0].StringFormat.LineAlignment = PdfVerticalAlignment.Middle;

        decimal uangMakan = reportGaji.UangMakan;
        row.Cells[3].Value = uangMakan.ToString("0.00");
        row.Cells[3].StringFormat = stringFormat;

        row = grid.Rows.Add();
        row.Cells[0].Value = "Uang Transport";
        row.Cells[0].StringFormat.LineAlignment = PdfVerticalAlignment.Middle;

        decimal uangTransport = reportGaji.UangTransport;
        row.Cells[3].Value = uangTransport.ToString("0.00");
        row.Cells[3].StringFormat = stringFormat;

        row = grid.Rows.Add();
        row.Cells[0].Value = "Uang Lembur";
        row.Cells[0].StringFormat.LineAlignment = PdfVerticalAlignment.Middle;

        decimal uangLembur = reportGaji.UangLembur;
        row.Cells[3].Value = uangLembur.ToString("0.00");
        row.Cells[3].StringFormat = stringFormat;

        row = grid.Rows.Add();
        row.Cells[0].Value = "Nilai Tunjangan";
        row.Cells[0].StringFormat.LineAlignment = PdfVerticalAlignment.Middle;

        decimal nilaiTunjangan = reportGaji.NilaiTunjangan;
        row.Cells[3].Value = nilaiTunjangan.ToString("0.00");
        row.Cells[3].StringFormat = stringFormat;

        grid.ApplyBuiltinStyle(PdfGridBuiltinStyle.PlainTable3);
        PdfGridStyle gridStyle = new PdfGridStyle();
        gridStyle.CellPadding = new PdfPaddings(5, 5, 5, 5);

        PdfGridLayoutFormat layoutFormat = new();
        layoutFormat.Layout = PdfLayoutType.Paginate;
        result = grid.Draw(currentPage, 14, result.Bounds.Bottom + 30, clientSize.Width - 35, layoutFormat);

        currentPage.Graphics.DrawRectangle(new PdfSolidBrush(Color.FromArgb(255, 239, 242, 255)),
            new RectangleF(result.Bounds.Right - 100, result.Bounds.Bottom + 20, 100, 20));

        PdfTextElement element = new("Total", font);
        element.Draw(currentPage,
            new RectangleF(result.Bounds.Right - 100, result.Bounds.Bottom + 22, result.Bounds.Width, result.Bounds.Height));

        var totalGaji = reportGaji.TotalGaji.ToString("0.00");
        element = new PdfTextElement(totalGaji, font);
        element.StringFormat = new PdfStringFormat(PdfTextAlignment.Right);
        element.Draw(currentPage, new RectangleF(15, result.Bounds.Bottom + 22, result.Bounds.Width, result.Bounds.Height));



        MemoryStream stream = new MemoryStream();
        document.Save(stream);
        document.Clone();
        stream.Position = 0;

        return stream;
    }
}
