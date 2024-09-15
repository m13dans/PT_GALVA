namespace SalaryManagement.Domain.GajiDomain;

public class GajiCalculator
{
    public static decimal HitungUangLembur(
        int jam,
        decimal gajiPokok,
        decimal uangMakan,
        decimal uangTransport) => jam switch
        {
            1 => (gajiPokok + uangMakan + uangTransport) / 173 * 1.5m,
            > 1 => (gajiPokok + uangMakan + uangTransport) / 173 * 2m,
            _ => 0
        };
}
