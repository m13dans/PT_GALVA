using System.Runtime.Serialization;
using ErrorOr;

namespace SalaryManagement.Domain.StatusValueObject;

public class Status
{
    public JenisStatusEnum JenisStatus { get; }
    public string Deskripsi => GetDescription(JenisStatus);
    public decimal NilaiTunjangan => GetNilaiTunjangan(JenisStatus);

    public Status(JenisStatusEnum jenisStatus)
    {
        JenisStatus = jenisStatus;
    }
    private string GetDescription(JenisStatusEnum jenisStatus) =>
        jenisStatus switch
        {
            JenisStatusEnum.T0 => "Tidak kawin tanggungan 0",
            JenisStatusEnum.T1 => "Tidak kawin tanggungan 1",
            JenisStatusEnum.T2 => "Tidak kawin tanggungan 2",
            JenisStatusEnum.T3 => "Tidak kawin tanggungan 3",
            JenisStatusEnum.K0 => "Kawin tanggungan 0",
            JenisStatusEnum.K1 => "Kawin tanggungan 1",
            JenisStatusEnum.K2 => "Kawin tanggungan 2",
            JenisStatusEnum.K3 => "Kawin tanggungan 3",
            _ => "Invalid"
        };
    private decimal GetNilaiTunjangan(JenisStatusEnum jenisStatus) =>
        jenisStatus switch
        {
            JenisStatusEnum.T0 => 300_000m,
            JenisStatusEnum.T1 => 400_000m,
            JenisStatusEnum.T2 => 500_000m,
            JenisStatusEnum.T3 => 600_000m,
            JenisStatusEnum.K0 => 400_000m,
            JenisStatusEnum.K1 => 500_000m,
            JenisStatusEnum.K2 => 600_000m,
            JenisStatusEnum.K3 => 700_000m,
            _ => 0m
        };
}

public enum JenisStatusEnum
{
    [EnumMember(Value = "T0")] T0,
    [EnumMember(Value = "T1")] T1,
    [EnumMember(Value = "T2")] T2,
    [EnumMember(Value = "T3")] T3,
    [EnumMember(Value = "K0")] K0,
    [EnumMember(Value = "K1")] K1,
    [EnumMember(Value = "K2")] K2,
    [EnumMember(Value = "K3")] K3
}
public class JenisStatus
{
    private readonly string _value;
    private JenisStatus(string value)
    {
        _value = value;
    }
    public string Value => _value;

    public static ErrorOr<JenisStatus> Create(string value) =>
         IsValid(value) ? new JenisStatus(value) : StatusError.Validation();

    private static bool IsValid(string jenisStatus) =>
        jenisStatus switch
        {
            "T0" => true,
            "T1" => true,
            "T2" => true,
            "T3" => true,
            "K0" => true,
            "K1" => true,
            "K2" => true,
            "K3" => true,
            _ => false,
        };
}
