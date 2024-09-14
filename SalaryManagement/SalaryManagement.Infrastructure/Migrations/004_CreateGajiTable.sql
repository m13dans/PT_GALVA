IF NOT EXISTS (
    SELECT * FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].Gaji') AND type in (N'U'))

BEGIN
	create table Gaji (
		Id int primary key clustered identity(1,1),
		TanggalTerakhirGaji DATE,
		Tanggal DATE,
		TotalGaji money,
		UangLembur money,
		NomerPegawai int not null 
			CONSTRAINT FK_Gaji_NomerPegawai 
			REFERENCES Pegawai(NomerPegawai),
		IdLembur int not null 
			CONSTRAINT FK_Gaji_IdLembur 
			REFERENCES Lembur(Id),
		JumlahLembur float)
END