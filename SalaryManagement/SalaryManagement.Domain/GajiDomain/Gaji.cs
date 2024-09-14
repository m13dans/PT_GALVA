using SalaryManagement.Domain.PegawaiDomain;

namespace SalaryManagement.Domain.GajiDomain;

public class Gaji
{
    public int Id { get; }
    public DateTime TanggalTerakhirGaji { get; }
    public DateTime Tanggal { get; }
    public decimal TotalGaji { get; }
    public decimal UangLembur { get; }
    public Pegawai Pegawai { get; }
    public IReadOnlyList<Lembur> Lemburs { get; }

    public Gaji(
        int id,
        Pegawai pegawai,
        List<Lembur> lemburs,
        DateTime tanggal,
        DateTime? tanggalTerakhirGaji = null)
    {
        Id = id;
        TanggalTerakhirGaji = tanggalTerakhirGaji ?? tanggal.AddDays(-30);
        Tanggal = tanggal;
        TotalGaji = CalculateTotalGaji(pegawai, lemburs, tanggalTerakhirGaji, tanggal);
        UangLembur = CalculateTotalUangLembur(lemburs, tanggalTerakhirGaji, tanggal);
        Pegawai = pegawai;
        Lemburs = lemburs.Where(l => l.Pegawai.NomerPegawai == pegawai.NomerPegawai).ToList();
    }

    private decimal CalculateTotalUangLembur(
        List<Lembur> lemburs,
        DateTime? tanggalTerakhirGaji,
        DateTime tanggal)
    {
        return lemburs.Where(l => l.Tanggal <= tanggal && l.Tanggal >= tanggalTerakhirGaji)
               .Sum(x => x.UangLembur);
    }

    private decimal CalculateTotalGaji(
        Pegawai pegawai,
        List<Lembur> lemburs,
        DateTime? tanggalTerakhirGaji,
        DateTime tanggal)
    {
        return pegawai.GajiPokok
               + pegawai.UangMakan
               + pegawai.UangTransport
               + CalculateTotalUangLembur(lemburs, tanggalTerakhirGaji, tanggal)
               + pegawai.NilaiTunjangan;
    }
}
