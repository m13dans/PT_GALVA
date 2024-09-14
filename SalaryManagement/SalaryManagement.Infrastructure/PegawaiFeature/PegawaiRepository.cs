using System.Dynamic;
using Dapper;
using ErrorOr;
using SalaryManagement.Application.PegawaiFeature;
using SalaryManagement.Domain.PegawaiDomain;
using SalaryManagement.Infrastructure.Data;

namespace SalaryManagement.Infrastructure.PegawaiFeature;

public class PegawaiRepository(DbConnectionFactory dbConnection) : IPegawaiRepository
{
    public async Task<List<Pegawai>> GetAllPegawai()
    {
        string sql = """
            SELECT 
                NomerPegawai, 
                NamaPegawai, 
                TanggalMasuk,
                JenisKelamin,
                Status, 
                GajiPokok, 
                UangMakan,
                UangTransport, 
                UangLembur,
                NilaiTunjangan
            FROM Pegawai
         """;

        var connection = dbConnection.CreateConnection();

        var result = await connection.QueryAsync<Pegawai>(sql);

        return result.ToList();
    }

    public async Task<ErrorOr<Pegawai>> GetPegawaiById(int nomerPegawai)
    {
        string sql = """
            SELECT 
                NomerPegawai, 
                NamaPegawai, 
                TanggalMasuk,
                JenisKelamin,
                Status, 
                GajiPokok, 
                UangMakan,
                UangTransport, 
                UangLembur,
                NilaiTunjangan
            FROM Pegawai
            WHERE NomerPegawai = @NomerPegawai
         """;

        using var connection = dbConnection.CreateConnection();

        var result = await connection.QuerySingleOrDefaultAsync<Pegawai>(sql, new { NomerPegawai = nomerPegawai });

        if (result is null)
            return ErrorPegawai.NotFound();

        return result;
    }

    public async Task<ErrorOr<Pegawai>> CreatePegawai(Pegawai pegawai)
    {
        string sql = """
            INSERT INTO Pegawai (
                NamaPegawai,
                TanggalMasuk,
                JenisKelamin,
                [Status],
                GajiPokok,
                UangMakan,
                UangTransport
            )
            OUTPUT INSERTED.*
            VALUES (
                @NamaPegawai,
                @TanggalMasuk,
                @JenisKelamin,
                @Status,
                @GajiPokok,
                @UangMakan,
                @UangTransport
            )
        """;

        using var conn = dbConnection.CreateConnection();
        var result = await conn.QuerySingleOrDefaultAsync<Pegawai>(sql, pegawai);
        if (result is null)
            return ErrorPegawai.Validation();

        return result;
    }

    public Task<ErrorOr<Pegawai>> UpdatePegawai(UpdatePegawaiRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<ErrorOr<Deleted>> DeletePegawai(int nomerPegawai)
    {
        throw new NotImplementedException();
    }
}
