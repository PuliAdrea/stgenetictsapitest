USE stgenetics;
GO

CREATE PROCEDURE InsertAnimals
AS
BEGIN
    DECLARE @animalName VARCHAR(100);
    DECLARE @animalBreed VARCHAR(100);
    DECLARE @animalBirthDate DATE;
    DECLARE @animalSex VARCHAR(10);
    DECLARE @animalPrice DECIMAL(10,2);
    DECLARE @animalStatus VARCHAR(10);

    DECLARE @counter INT = 1;

    DECLARE animalCursor CURSOR FOR
    SELECT TOP 5000
        'Animal' + CAST(ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS VARCHAR),
        'Breed' + CAST(ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS VARCHAR),
        DATEADD(DAY, -FLOOR(RAND(CHECKSUM(NEWID())) * 365.25 * 10), GETDATE()),
        CASE WHEN ABS(CHECKSUM(NEWID())) % 2 = 0 THEN 'Male' ELSE 'Female' END,
        CAST(1000 + (ABS(CHECKSUM(NEWID())) % 9000) AS DECIMAL(10,2)),
        CASE WHEN ABS(CHECKSUM(NEWID())) % 2 = 0 THEN 'Active' ELSE 'Inactive' END
    FROM sys.all_columns ac1
    CROSS JOIN sys.all_columns ac2;

    OPEN animalCursor;

    FETCH NEXT FROM animalCursor INTO
        @animalName,
        @animalBreed,
        @animalBirthDate,
        @animalSex,
        @animalPrice,
        @animalStatus;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        INSERT INTO animals (name, breed, birth_date, sex, price, status)
        VALUES (@animalName, @animalBreed, @animalBirthDate, @animalSex, @animalPrice, @animalStatus);

        SET @counter = @counter + 1;

        FETCH NEXT FROM animalCursor INTO
            @animalName,
            @animalBreed,
            @animalBirthDate,
            @animalSex,
            @animalPrice,
            @animalStatus;
    END;

    CLOSE animalCursor;
    DEALLOCATE animalCursor;
END;




