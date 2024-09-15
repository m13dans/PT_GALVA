using ErrorOr;
using SalaryManagement.Domain.PegawaiDomain;
using SalaryManagement.Domain.StatusValueObject;

namespace SalaryManagement.Application.PegawaiFeature;

public class PegawaiService(IPegawaiRepository repo)
{
    public async Task<List<Pegawai>> GetAllPegawai() =>
        await repo.GetAllPegawai();

    public async Task<ErrorOr<Pegawai>> GetPegawaiById(int nomerPegawai) =>
        await repo.GetPegawaiById(nomerPegawai);

    public async Task<ErrorOr<Pegawai>> CreatePegawai(PegawaiRequest request)
    {
        var pegawai = Pegawai.Create(
            request.NamaPegawai,
            request.TanggalMasuk,
            request.JenisKelamin,
            Status.ToString(request.JenisStatus),
            request.GajiPokok,
            request.UangMakan,
            request.UangTransport);

        if (pegawai.IsError)
            return pegawai.FirstError;

        return await repo.CreatePegawai(pegawai.Value);
    }

    public async Task<ErrorOr<Pegawai>> UpdatePegawai(int nomerPegwai, PegawaiRequest request)
    {
        var pegawai = Pegawai.Create(
            request.NamaPegawai,
            request.TanggalMasuk,
            request.JenisKelamin,
            Status.ToString(request.JenisStatus),
            request.GajiPokok,
            request.UangMakan,
            request.UangTransport);

        if (pegawai.IsError)
            return pegawai.FirstError;

        return await  repo.UpdatePegawai(nomerPegwai, pegawai.Value);
    }

    public async Task<ErrorOr<Deleted>> DeletePegawai(int nomerPegawai) =>
        await repo.DeletePegawai(nomerPegawai);


}
