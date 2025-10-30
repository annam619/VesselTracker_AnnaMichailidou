IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Vessels') AND type in (N'U'))
BEGIN
    CREATE TABLE Vessels (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(100),
        IMO NVARCHAR(20) UNIQUE,
        Flag NVARCHAR(50),
        BuildYear INT
    );
    PRINT 'Table Vessels created successfully.';
END
ELSE
BEGIN
    PRINT 'Table Vessels already exists.';
END