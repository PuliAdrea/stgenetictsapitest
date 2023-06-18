USE stgenetics;

CREATE TABLE animals
(
    animal_id INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    breed VARCHAR(100) NOT NULL,
    birth_date DATE NOT NULL,
    sex BIT NOT NULL,
    price DECIMAL(10,2) NOT NULL,
    status BIT NOT NULL
);

INSERT INTO animals (name, breed, birth_date, sex, price, status)
VALUES
    ('Horse', 'Thoroughbred', '2019-05-10', 1, 500.00, 1),
    ('Bird', 'Parakeet', '2021-01-08', 0, 900.00, 0);

SELECT * FROM animals;

DROP TABLE animals;