using Dapper;
using ErrorOr;
using SalaryManagement.Application.GajiFeature;
using SalaryManagement.Domain.GajiDomain;
using SalaryManagement.Infrastructure.Data;

namespace SalaryManagement.Infrastructure.GajiFeature;

public class GajiRepository(DbConnectionFactory dbConnection) : IGajiRepository
{
    public async Task<ErrorOr<Gaji>> GetLastGajiFor(int nomerPegawai)
    {
        string sql = """
            SELECT TOP 1
                NomerPegawai,
                TanggalTerakhirGaji, 
                Tanggal
            FROM Gaji
            WHERE NomerPegawai = @NomerPegawai
            ORDER BY TanggalTerakhirGaji DESC
         """;

        var connection = dbConnection.CreateConnection();

        var result = await connection.QuerySingleOrDefaultAsync<Gaji>(
            sql,
            new { NomerPegawai = nomerPegawai });

        if (result is null)
        {
            return Error.NotFound(
                "Gaji.TidakDitemukan",
                $"Gaji untuk pegawai dengan id {nomerPegawai} tidak ditemukan");
        }

        return result;
    }

    public async Task<ErrorOr<Created>> CreateGaji(Gaji gaji)
    {
        string sql = """
            INSERT INTO Gaji(
                TanggalTerakhirGaji, 
                Tanggal,
                NomerPegawai
            )
            VALUES (
                @TanggalTerakhirGaji, 
                @Tanggal,
                @NomerPegawai
            )
        """;

        using var conn = dbConnection.CreateConnection();
        var result = await conn.ExecuteAsync(sql, gaji);
        if (result <= 0)
            return Error.Failure("Gaji.Failure", "Tidak Bisa input gaji");

        return Result.Created;
    }

    public async Task<ErrorOr<List<Lembur>>> GetLastLemburListFor(int nomerPegawai)
    {
        string sql = """
            SELECT
                l.Id,
                l.DokumenLembur, 
                l.Tanggal,
                l.NomerPegawai,
                l.JumlahLembur
            FROM Lembur l
            JOIN Gaji g ON l.NomerPegawai = g.NomerPegawai
            WHERE l.NomerPegawai = @NomerPegawai
                AND l.Tanggal <= g.Tanggal 
                AND l.Tanggal >= g.TanggalTerakhirGaji
            ORDER BY l.Tanggal DESC
         """;

        var connection = dbConnection.CreateConnection();

        var result = await connection.QueryAsync<Lembur>(
            sql,
            new { NomerPegawai = nomerPegawai });

        if (result is null)
            return Error.NotFound(
                "Lembur.TidakDiTemukan",
                "Data lembur tidak ditemukan");

        return result.ToList();
    }

    public async Task<ErrorOr<Created>> CreateLembur(Lembur lembur)
    {
        string sql = """
            INSERT INTO Lembur(
                DokumenLembur, 
                Tanggal,
                NomerPegawai,
                JumlahLembur
            )
            VALUES (
                @DokumenLembur, 
                @Tanggal,
                @NomerPegawai,
                @JumlahLembur
            )
        """;

        var param = new
        {
            DokumenLembur = lembur.DokumenLembur,
            Tanggal = lembur.Tanggal,
            NomerPegawai = lembur.NomerPegawai,
            JumlahLembur = lembur.JumlahLembur
        };

        using var conn = dbConnection.CreateConnection();
        var result = await conn.ExecuteAsync(sql, param);
        if (result <= 0)
            return Error.Failure("Lembur.Failure", "Tidak Bisa input data lembur");

        return Result.Created;
    }
}
