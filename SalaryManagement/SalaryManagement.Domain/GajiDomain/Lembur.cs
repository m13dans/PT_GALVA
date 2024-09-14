using SalaryManagement.Domain.PegawaiDomain;
using SalaryManagement.Domain.StatusValueObject;

namespace SalaryManagement.Domain.GajiDomain;

public class Lembur
{
    public int Id { get; set; }
    public string DokumenLembur { get; set; } = string.Empty;
    public DateTime Tanggal { get; set; }
    public required Pegawai Pegawai { get; set; }
    public int NomerPegawai { get; set; }
    public int JumlahLembur { get; set; }
    public decimal UangLembur => HitungUangLembur(
        JumlahLembur,
        Pegawai.GajiPokok,
        Pegawai.UangMakan,
        Pegawai.UangTransport);

    private static decimal HitungUangLembur(
        int jam,
        decimal gajiPokok,
        decimal uangMakan,
        decimal uangTransport) => jam switch
        {
            1 => (gajiPokok + uangMakan + uangTransport) / 173 * 1.5m,
            > 1 => (gajiPokok + uangMakan + uangTransport) / 173 * 2m,
            _ => 0
        };
}
