using System.ComponentModel.DataAnnotations;
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
    public required string JenisStatus { get; set; }
    public decimal GajiPokok { get; set; }
    public decimal UangMakan { get; set; }
    public decimal UangTransport { get; set; }
    public List<Lembur>? Lembur { get; set; }
    public decimal UangLembur =>
        Lembur is not null && Lembur.Any(x => x is not null) ?
        Lembur.Sum(x => GajiCalculator.HitungUangLembur(
            x.JumlahLembur,
            GajiPokok,
            UangMakan,
            UangTransport)) : 0;
    public decimal NilaiTunjangan => Status.GetNilaiTunjangan(Status.ToJenisStatusEnum(JenisStatus));

    public decimal TotalGaji => GajiPokok + UangMakan + UangTransport + UangLembur + NilaiTunjangan;

    private Pegawai() { }

    public static ErrorOr<Pegawai> Create(
        string namaPegawai,
        DateTime tanggalMasuk,
        JenisKelamin jenisKelamin,
        string status,
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
            JenisStatus = status,
            GajiPokok = gajiPokok,
            UangMakan = uangMakan,
            UangTransport = uangTransport
        };
    }

    public static string ToJenisKelaminString(JenisKelamin jenisKelamin) =>
    jenisKelamin switch
    {
        JenisKelamin.LakiLaki => "Laki-laki",
        JenisKelamin.Perempuan => "Perempuan",
        _ => ""
    };

}
public enum JenisKelamin
{
    [Display(Name = "Laki - laki")]
    LakiLaki,
    [EnumMember(Value = "Perempuan")]
    Perempuan
}
