using SalaryManagement.Domain.PegawaiDomain;
using SalaryManagement.Domain.StatusValueObject;
using System.ComponentModel.DataAnnotations;

namespace SalaryManagement.Application.PegawaiFeature;

public class PegawaiRequest
{
  public int NomerPegawai { get; set; }

  [Required(ErrorMessage = "Nama harus di isi")]
  [StringLength(255, ErrorMessage = "Batas maksimum karakter sudah tercapai.")]
  public string NamaPegawai { get; set; } = string.Empty;
  public DateTime TanggalMasuk { get; set; } = DateTime.Now;
  [Required(ErrorMessage = "Pilih jenis kelamin")]
  public JenisKelamin JenisKelamin { get; set; }
  public JenisStatusEnum JenisStatus { get; set; }
  [Range(1.0, double.MaxValue, ErrorMessage = "Gaji pokok harus lebih dari 0")]
  [Required(ErrorMessage = "Gaji Pokok harus diisi.")]
  public decimal GajiPokok { get; set; }
  [Range(0.0, double.MaxValue, ErrorMessage = "Uang makan tidak kurang dari 0")]
  [Required(ErrorMessage = "Uang makan harus diisi.")]
  public decimal UangMakan { get; set; }
  [Range(0.0, double.MaxValue, ErrorMessage = "Uang transport tidak kurang dari 0")]
  [Required(ErrorMessage = "Uang transport harus diisi.")]
  public decimal UangTransport { get; set; }
  public decimal UangLembur { get; set; }
  public decimal NilaiTunjangan => Status.GetNilaiTunjangan(JenisStatus);
  public decimal TotalGaji { get; set; }
}
