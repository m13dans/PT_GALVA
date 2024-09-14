using SalaryManagement.Domain.GajiDomain;

namespace SalaryManagement.Application.GajiFeature;

public interface IGajiRepository
{
    public Task<List<Lembur>> GetLembursAsync(int nomerPegawai);
}