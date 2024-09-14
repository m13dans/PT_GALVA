using ErrorOr;
using SalaryManagement.Domain.PegawaiDomain;

namespace SalaryManagement.Application.PegawaiFeature;

public class PegawaiService(IPegawaiRepository repo)
{
    public async Task<List<Pegawai>> GetAllPegawai() =>
        await repo.GetAllPegawai();

    public async Task<ErrorOr<Pegawai>> GetPegawaiById(int nomerPegawai) =>
        await repo.GetPegawaiById(nomerPegawai);

    public async Task<ErrorOr<Pegawai>> CreatePegawai(CreatePegawaiRequest request)
    {
        var pegawai = Pegawai.Create(
            request.NamaPegawai,
            request.TanggalMasuk,
            request.JenisKelamin,
            request.Status,
            request.GajiPokok,
            request.UangMakan,
            request.UangTransport);

        if (pegawai.IsError)
            return pegawai.FirstError;

        return await repo.CreatePegawai(pegawai.Value);
    }

    public async Task<ErrorOr<Pegawai>> UpdatePegawai(UpdatePegawaiRequest request) =>
        await repo.UpdatePegawai(request);

    public async Task<ErrorOr<Deleted>> DeletePegawai(int nomerPegawai) =>
        await repo.DeletePegawai(nomerPegawai);


}
