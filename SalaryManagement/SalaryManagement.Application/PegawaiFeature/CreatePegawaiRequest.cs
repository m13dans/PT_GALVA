using SalaryManagement.Domain.PegawaiDomain;
using SalaryManagement.Domain.StatusValueObject;

namespace SalaryManagement.Application.PegawaiFeature;

public record CreatePegawaiRequest(
    string NamaPegawai,
    DateTime TanggalMasuk,
    JenisKelamin JenisKelamin,
    JenisStatusEnum Status,
    decimal GajiPokok,
    decimal UangMakan,
    decimal UangTransport
);