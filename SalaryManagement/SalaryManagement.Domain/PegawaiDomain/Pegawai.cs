using System.Runtime.Serialization;
using ErrorOr;
using SalaryManagement.Domain.GajiDomain;
using SalaryManagement.Domain.StatusValueObject;

namespace SalaryManagement.Domain.PegawaiDomain;

public class Pegawai
{
    public int NomerPegawai { get; set; }
    public required string NamaPegawai { get; set; }
    public DateTime TanggalMasuk { get; set; }
    public JenisKelamin JenisKelamin { get; set; }
    public required Status Status { get; set; }
    public decimal GajiPokok { get; set; }
    public decimal UangMakan { get; set; }
    public decimal UangTransport { get; set; }
    public List<Lembur>? Lembur { get; set; }
    public decimal UangLembur => GetUangLembur(Lembur);
    public decimal NilaiTunjangan => Status.NilaiTunjangan;

    public decimal TotalGaji => GajiPokok + UangMakan + UangTransport + UangLembur + NilaiTunjangan;

    private decimal GetUangLembur(List<Lembur>? lemburs) =>
        lemburs is null ? 0 : lemburs.Where(l => l.Pegawai.NomerPegawai == NomerPegawai).Sum(x => x.UangLembur);

    private Pegawai() { }

    public static ErrorOr<Pegawai> Create(
        string namaPegawai,
        DateTime tanggalMasuk,
        JenisKelamin jenisKelamin,
        JenisStatusEnum status,
        decimal gajiPokok,
        decimal uangMakan,
        decimal uangTransport)
    {
        if (string.IsNullOrWhiteSpace(namaPegawai))
            return Error.Validation(description: "Nama tidak boleh kosong");

        return new Pegawai()
        {
            NamaPegawai = namaPegawai,
            TanggalMasuk = tanggalMasuk,
            JenisKelamin = jenisKelamin,
            Status = new Status(status),
            GajiPokok = gajiPokok,
            UangMakan = uangMakan,
            UangTransport = uangTransport
        };
    }
}
public enum JenisKelamin
{
    [EnumMember(Value = "Laki-Laki")]
    LakiLaki,
    [EnumMember(Value = "Perempuan")]
    Perempuan
}
