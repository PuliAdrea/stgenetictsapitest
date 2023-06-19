-- 1)	Create the "STGenetics" database.
-- 2)	Create a script to create Animal table.

		USE stgenetics;

		CREATE TABLE animals
		(
			animal_id INT IDENTITY(1,1) PRIMARY KEY,
			name VARCHAR(100) NOT NULL,
			breed VARCHAR(100) NOT NULL,
			birth_date DATE NOT NULL,
			sex VARCHAR(10) NOT NULL,
			price DECIMAL(10,2) NOT NULL,
			status VARCHAR(10) NOT NULL
		);

-- 3)	Create a script (Insert / Update / Delete) for record in Animal table.

		-- Insert
		INSERT INTO animals (name, breed, birth_date, sex, price, status)
		VALUES ('Ginger', 'Labrador Retriever', '2019-03-15', 'Female', 1500.00, 'Active');

		-- Update
		UPDATE animals
		SET price = 1200.00, status = 'Inactive' WHERE animal_id = 1;

		-- Delete
		DELETE FROM animals WHERE animal_id = 3;


-- 4)	Create a script to insert 5,000 animals in the Animal table.

		-- Before running the build procedure, run the file createprocedureinsertanimals.sql
		USE stgenetics;
		EXEC InsertAnimals;

-- 5)	Create a script to list animals older than 2 years and female, sorted by name.

		SELECT *
			FROM animals
			WHERE birth_date <= DATEADD(YEAR, -2, GETDATE()) 
				AND sex = 'Female'
			ORDER BY name;
-- 6)	Create a script to list the quantity of animals by sex.

		SELECT sex, COUNT(*) AS quantity
			FROM animals
			GROUP BY sex;

-- Script to create the orders table

		CREATE TABLE orders
		(
			id INT IDENTITY(1,1) PRIMARY KEY,
			total DECIMAL(10, 2) NOT NULL,
			charge_freight BIT NOT NULL
		);
