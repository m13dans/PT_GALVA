using System.Dynamic;
using Dapper;
using ErrorOr;
using SalaryManagement.Application.PegawaiFeature;
using SalaryManagement.Domain.GajiDomain;
using SalaryManagement.Domain.PegawaiDomain;
using SalaryManagement.Domain.StatusValueObject;
using SalaryManagement.Infrastructure.Data;

namespace SalaryManagement.Infrastructure.PegawaiFeature;

public class PegawaiRepository(DbConnectionFactory dbConnection) : IPegawaiRepository
{
    public async Task<List<Pegawai>> GetAllPegawai()
    {
        string sql = """
            SELECT 
                p.NomerPegawai, 
                p.NamaPegawai, 
                p.TanggalMasuk,
                p.JenisKelamin,
                p.Status AS JenisStatus, 
                p.GajiPokok, 
                p.UangMakan,
                p.UangTransport, 
                p.UangLembur,
                p.NilaiTunjangan,
                l.Id,
                l.DokumenLembur,
                l.Tanggal,
                l.NomerPegawai,
                l.JumlahLembur
            FROM Pegawai p
            LEFT JOIN Lembur l ON p.NomerPegawai = l.NomerPegawai
         """;

        using var connection = dbConnection.CreateConnection();

        var result = await connection.QueryAsync<
            Pegawai,
            Lembur,
            Pegawai>
            (
            sql,
            map: (pegawai, lembur) =>
            {
                pegawai.Lembur ??= [];
                pegawai.Lembur.Add(lembur);
                return pegawai;
            },
            splitOn: "Id");

        var pegawai = result.GroupBy(x => x.NomerPegawai).Select(g =>
        {
            Pegawai pegawai = g.First();
            var lemburList = g.SelectMany(x => x.Lembur ?? []);
            if (lemburList.Any(x => x != null))
                pegawai.Lembur = lemburList.DistinctBy(x => x.Id).ToList();

            return pegawai;
        });


        return pegawai.ToList();
    }

    public async Task<ErrorOr<Pegawai>> GetPegawaiById(int nomerPegawai)
    {
        string sql = """
            SELECT 
                p.NomerPegawai, 
                p.NamaPegawai, 
                p.TanggalMasuk,
                p.JenisKelamin,
                p.Status, 
                p.GajiPokok, 
                p.UangMakan,
                p.UangTransport, 
                p.UangLembur,
                p.NilaiTunjangan,
                l.Id,
                l.DokumenLembur,
                l.Tanggal,
                l.NomerPegawai,
                l.JumlahLembur
            FROM Pegawai p
            LEFT JOIN Lembur l ON p.NomerPegawai = l.NomerPegawai
            WHERE p.NomerPegawai = @NomerPegawai
         """;

        using var connection = dbConnection.CreateConnection();

        var result = await connection.QueryAsync<
            Pegawai,
            Lembur,
            Pegawai>
            (
            sql,
            map: (pegawai, lembur) =>
            {
                pegawai.Lembur ??= [];
                pegawai.Lembur.Add(lembur);
                return pegawai;
            },
            splitOn: "Id",
            param: new { NomerPegawai = nomerPegawai });

        if (result is null)
            return ErrorPegawai.NotFound();

        var pegawai = result.GroupBy(x => x.NomerPegawai).Select(g =>
        {
            Pegawai pegawai = g.First();
            var lemburList = g.SelectMany(x => x.Lembur ?? []);
            if (lemburList.Any(x => x != null))
                pegawai.Lembur = lemburList.DistinctBy(x => x.Id).ToList();

            return pegawai;
        }).FirstOrDefault();

        if (pegawai is null)
            return ErrorPegawai.NotFound();

        return pegawai;
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
                UangTransport,
                NilaiTunjangan
            )
            OUTPUT INSERTED.NomerPegawai
            VALUES (
                @NamaPegawai,
                @TanggalMasuk,
                @JenisKelamin,
                @Status,
                @GajiPokok,
                @UangMakan,
                @UangTransport,
                @NilaiTunjangan
            )
        """;



        var param = new { NamaPegawai = pegawai.NamaPegawai, TanggalMasuk = pegawai.TanggalMasuk, JenisKelamin = pegawai.JenisKelamin, Status = pegawai.JenisStatus, GajiPokok = pegawai.GajiPokok, UangMakan = pegawai.UangMakan, UangTransport = pegawai.UangTransport, NilaiTunjangan = pegawai.NilaiTunjangan };
        using var conn = dbConnection.CreateConnection();
        var result = await conn.QuerySingleOrDefaultAsync<int>(sql, param);
        if (result <= 0)
            return ErrorPegawai.Validation();

        var pegawaiResult = await GetPegawaiById(result);

        return pegawaiResult;
    }

    public async Task<ErrorOr<Pegawai>> UpdatePegawai(int nomerPegawai, Pegawai pegawai)
    {
        string sql = """
                UPDATE Pegawai SET 
                    NamaPegawai = @NamaPegawai,
                    TanggalMasuk = @TanggalMasuk,
                    JenisKelamin = @JenisKelamin,
                    Status = @JenisStatus,
                    GajiPokok = @GajiPokok,
                    UangMakan = @UangMakan,
                    UangTransport = @UangTransport
                WHERE NomerPegawai = @NomerPegawai
            """;

        using var conn = dbConnection.CreateConnection();
        var param = new { NomerPegawai = nomerPegawai, NamaPegawai = pegawai.NamaPegawai, TanggalMasuk = pegawai.TanggalMasuk, JenisKelamin = pegawai.JenisKelamin, JenisStatus = pegawai.JenisStatus, GajiPokok = pegawai.GajiPokok, UangMakan = pegawai.UangMakan, UangTransport = pegawai.UangTransport, NilaiTunjangan = pegawai.NilaiTunjangan };

        var result = await conn.ExecuteAsync(sql, param);
        if (result <= 0)
            return ErrorPegawai.Validation();

        var pegawaiResult = await GetPegawaiById(pegawai.NomerPegawai);

        return pegawaiResult;
    }

    public async Task<ErrorOr<Deleted>> DeletePegawai(int nomerPegawai)
    {
        string sql = """
                DELETE FROM Pegawai Where NomerPegawai = @NomerPegawai
        """;

        using var conn = dbConnection.CreateConnection();
        var result = await conn.ExecuteAsync(sql, new { NomerPegawai = nomerPegawai });
        if (result <= 0)
            return ErrorPegawai.Validation();

        return Result.Deleted;
    }
}
