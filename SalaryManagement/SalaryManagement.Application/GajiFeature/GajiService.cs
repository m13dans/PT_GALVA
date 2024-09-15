using ErrorOr;
using SalaryManagement.Application.PegawaiFeature;
using SalaryManagement.Domain.GajiDomain;

namespace SalaryManagement.Application.GajiFeature;

public class GajiService(
    IGajiRepository gajiRepo,
    IPegawaiRepository pegawaiRepo)
{
    public async Task<ErrorOr<ReportGaji>> GetReportGajiFor(int nomerPegawai)
    {
        var pegawaiOrError = await pegawaiRepo.GetPegawaiById(nomerPegawai);
        var listLemburOrError = await gajiRepo.GetLastLemburListFor(nomerPegawai);
        var gajiOrError = await gajiRepo.GetLastGajiFor(nomerPegawai);

        if (pegawaiOrError.IsError)
            return pegawaiOrError.FirstError;

        if (gajiOrError.IsError)
            return gajiOrError.FirstError;

        var pegawai = pegawaiOrError.Value;
        var gaji = gajiOrError.Value;
        var uangLembur = listLemburOrError.Value.Sum(x => GajiCalculator.HitungUangLembur(
                    x.JumlahLembur,
                    pegawai.GajiPokok,
                    pegawai.UangMakan,
                    pegawai.UangTransport));

        var jumlahLembur = listLemburOrError.Value.Sum(x => x.JumlahLembur);
        var totalGaji = pegawai.GajiPokok
            + pegawai.UangMakan
            + pegawai.UangTransport
            + uangLembur
            + pegawai.NilaiTunjangan;

        return new ReportGaji
        {
            NomerPegawai = pegawai.NomerPegawai,
            NamaPegawai = pegawai.NamaPegawai,
            TanggalTerakhirGaji = gaji.TanggalTerakhirGaji,
            Tanggal = gaji.Tanggal,
            GajiPokok = pegawai.GajiPokok,
            UangMakan = pegawai.UangMakan,
            UangTransport = pegawai.UangTransport,
            JumlahLembur = jumlahLembur,
            UangLembur = uangLembur,
            NilaiTunjangan = pegawai.NilaiTunjangan,
            TotalGaji = totalGaji,
        };


    }
    public async Task<ErrorOr<Created>> CreateEventGaji(Gaji gaji) =>
        await gajiRepo.CreateGaji(gaji);
    public async Task<ErrorOr<Created>> CreateLembur(Lembur lembur) =>
        await gajiRepo.CreateLembur(lembur);
    public async Task<ErrorOr<Gaji>> GetLastGajiFor(int nomerPegawai) =>
        await gajiRepo.GetLastGajiFor(nomerPegawai);
    public async Task<ErrorOr<List<Lembur>>> GetLastLemburListFor(int nomerPegawai) =>
        await gajiRepo.GetLastLemburListFor(nomerPegawai);
}
