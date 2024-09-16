using System.ComponentModel.DataAnnotations;

namespace SalaryManagement.Domain.GajiDomain;

public class Lembur
{
    public int Id { get; set; }
    public string DokumenLembur { get; set; } = string.Empty;
    public DateTime Tanggal { get; set; } = DateTime.Now;
    public int NomerPegawai { get; set; }
    [Required]
    [Range(1, 24, ErrorMessage = "lembur tidak bisa kurang dari 1 atau lebih dari 24")]
    public int JumlahLembur { get; set; }
}