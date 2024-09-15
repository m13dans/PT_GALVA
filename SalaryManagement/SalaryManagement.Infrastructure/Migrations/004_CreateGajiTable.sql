IF NOT EXISTS (
    SELECT * FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].Gaji') AND type in (N'U'))

BEGIN
	create table Gaji (
		Id int primary key clustered identity(1,1),
		TanggalTerakhirGaji DATE,
		Tanggal DATE,
		NomerPegawai int not null 
			CONSTRAINT FK_Gaji_NomerPegawai 
			REFERENCES Pegawai(NomerPegawai)
		)
END