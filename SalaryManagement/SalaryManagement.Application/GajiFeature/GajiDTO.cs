namespace SalaryManagement.Application.GajiFeature;

public class GajiDTO
{
    public int Id { get; set; }
    public int NomerPegawai { get; set; }
    public DateTime TanggalTerakhirGaji { get; set; }
    public DateTime Tanggal { get; set; }
    public decimal GajiPokok { get; set; }
    public decimal UangMakan { get; set; }
    public decimal UangTransport { get; set; }
    public decimal UangLembur { get; set; }
    public decimal NilaiTunjangan { get; set; }
    public decimal TotalGaji { get; set; }
}