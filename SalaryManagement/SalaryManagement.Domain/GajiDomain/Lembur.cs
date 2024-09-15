namespace SalaryManagement.Domain.GajiDomain;

public class Lembur
{
    public int Id { get; set; }
    public string DokumenLembur { get; set; } = string.Empty;
    public DateTime Tanggal { get; set; } = DateTime.Now;
    public int NomerPegawai { get; set; }
    public int JumlahLembur { get; set; }
}