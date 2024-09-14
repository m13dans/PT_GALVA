using ErrorOr;
using SalaryManagement.Domain.PegawaiDomain;

namespace SalaryManagement.Application.PegawaiFeature;

public interface IPegawaiRepository
{
    public Task<List<Pegawai>> GetAllPegawai();
    public Task<ErrorOr<Pegawai>> GetPegawaiById(int nomerPegawai);
    public Task<ErrorOr<Pegawai>> CreatePegawai(Pegawai pegawai);
    public Task<ErrorOr<Pegawai>> UpdatePegawai(UpdatePegawaiRequest request);
    public Task<ErrorOr<Deleted>> DeletePegawai(int nomerPegawai);
}