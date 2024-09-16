using System.ComponentModel.DataAnnotations;

namespace SalaryManagement.Domain.GajiDomain;

public class Gaji : IValidatableObject
{
    public int Id { get; set; }
    public int NomerPegawai { get; set; }
    public DateTime TanggalTerakhirGaji { get; set; }
    public DateTime Tanggal { get; set; } = DateTime.Now;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Tanggal < TanggalTerakhirGaji)
            yield return new ValidationResult("Tanggal Gaji tidak bisa sebelum Tanggal Terakhir Gaji", [nameof(Tanggal)]);
    }
}
