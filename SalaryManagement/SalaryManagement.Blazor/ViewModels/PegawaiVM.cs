using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using SalaryManagement.Domain.PegawaiDomain;
using SalaryManagement.Domain.StatusValueObject;

namespace SalaryManagement.Blazor.ViewModels
{
    public class PegawaiVM
    {
        public int NomerPegawai { get; set; }

        [Required(ErrorMessage = "Nama harus di isi")]
        [StringLength(5, ErrorMessage = "Jangan lebih dari 5 karakter")]
        public string NamaPegawai { get; set; } = string.Empty;
        public DateTime TanggalMasuk { get; set; } = DateTime.Now;
        public JenisKelamin JenisKelamin { get; set; }
        public JenisStatusEnum Status { get; set; }
        public decimal GajiPokok { get; set; }
        public decimal UangMakan { get; set; }
        public decimal UangTransport { get; set; }
        public decimal UangLembur { get; set; }
        public decimal NilaiTunjangan { get; set; }
        public decimal TotalGaji { get; set; }
    }
}
