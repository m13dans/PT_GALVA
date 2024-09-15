using ErrorOr;
using SalaryManagement.Domain.GajiDomain;

namespace SalaryManagement.Application.GajiFeature;

public interface IGajiRepository
{
    Task<ErrorOr<List<Lembur>>> GetLastLemburListFor(int nomerPegawai);
    Task<ErrorOr<Gaji>> GetLastGajiFor(int nomerPegawai);
    Task<ErrorOr<Created>> CreateGaji(Gaji gaji);
    Task<ErrorOr<Created>> CreateLembur(Lembur lembur);

}