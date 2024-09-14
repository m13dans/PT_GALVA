using SalaryManagement.Domain.PegawaiDomain;

namespace SalaryManagement.Application.PegawaiFeature;

public record UpdatePegawaiRequest
{
    public required string NamaPegawai { get; init; }
    public DateTime TanggalMasuk { get; init; }
    public JenisKelamin JenisKelamin { get; init; }
    public required string Status { get; init; }
    public decimal GajiPokok { get; init; }
    public decimal UangMakan { get; init; }
    public decimal UangTransport { get; init; }
    public decimal UangLembur { get; init; }
}
