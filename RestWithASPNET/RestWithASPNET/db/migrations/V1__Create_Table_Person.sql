CREATE TABLE dbo.Person (
    id INT PRIMARY KEY IDENTITY(1,1),
    first_name VARCHAR(80),
    last_name VARCHAR(80),
    address VARCHAR(100),
    gender VARCHAR(6) 
);
