using ErrorOr;

namespace SalaryManagement.Domain.PegawaiDomain;

public class ErrorPegawai
{
    public static Error NotFound() => Error.NotFound(
        code: "Pegawai.DataTidakDitemukan",
        description: "Pegawai tidak ditemukan");

    public static Error Validation(string description = "Data pegawai tidak valid") => Error.Validation(
        code: "Pegawai.DataTidakValid",
        description: description
    );
}
