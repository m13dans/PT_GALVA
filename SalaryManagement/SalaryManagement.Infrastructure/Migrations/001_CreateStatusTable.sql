IF NOT EXISTS (
    SELECT * FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].Status') AND type in (N'U'))

BEGIN
	create table Status (
		Id int primary key clustered identity(1,1),
		JenisStatus varchar(50) not null,
		Deskripsi varchar(255) not null,
		NilaiTunjangan money )
END