IF NOT EXISTS (
    SELECT * FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].Lembur') AND type in (N'U'))

BEGIN
	create table Lembur (
		Id int primary key clustered identity(1,1),
		DokumenLembur VARCHAR,
		Tanggal DATE,
		NomerPegawai int not null 
			CONSTRAINT FK_Lembur_NomerPegawai 
			REFERENCES Pegawai(NomerPegawai),
		JumlahLembur float, 
		UangLembur money)
END