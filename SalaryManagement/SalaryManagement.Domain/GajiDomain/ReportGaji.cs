using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalaryManagement.Domain.GajiDomain;

public class ReportGaji
{
    public int NomerPegawai { get; set; }
    public string NamaPegawai { get; set; } = string.Empty;

    public DateTime TanggalTerakhirGaji { get; set; }
    public DateTime Tanggal { get; set; }
    public decimal GajiPokok { get; set; }
    public decimal UangMakan { get; set; }
    public decimal UangTransport { get; set; }
    public int JumlahLembur { get; set; }
    public decimal UangLembur { get; set; }
    public decimal NilaiTunjangan { get; set; }
    public decimal TotalGaji { get; set; }

}
