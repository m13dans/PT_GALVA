IF NOT EXISTS (
    SELECT * FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].Pegawai') AND type in (N'U'))

BEGIN
	create table Pegawai (
		NomerPegawai int primary key clustered identity(1,1),
		NamaPegawai VARCHAR(255), 
		TanggalMasuk DATE,
		JenisKelamin int,
		[Status] varchar(50), 
		GajiPokok money, 
		UangMakan money,
		UangTransport money, 
		UangLembur money,
		NilaiTunjangan money)
END