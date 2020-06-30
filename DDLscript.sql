--we create database  
CREATE DATABASE EmployeeDB;
GO

use EmployeeDB;

GO

 --we delete tables in case they exist
 
DROP TABLE IF EXISTS tblLOCATION;
DROP TABLE IF EXISTS tblEmployee;
DROP TABLE IF EXISTS tblGender;
DROP TABLE IF EXISTS tblSector;


--we create table tblLOCATION
 CREATE TABLE tblLOCATION (
    LocationID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Adress varchar(50),
	Place varchar(50),
	Country varchar(50)   
);

--we create table tblGender
 CREATE TABLE tblGender (
    GenderID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Gender char(1) 
);

--we create table tblSector
 CREATE TABLE tblSector (
    SectorID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    SectorName varchar(50)   
);


--we create table tblEmployee
CREATE TABLE tblEmployee (
    EmployeeID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    FirstName varchar(50),
	LastName varchar(50),
	DateOfBirth date,
	JMBG varchar(13),
	RegistrationNumber varchar(9),
    TelefonNumber varchar(50),   
	LocationID int FOREIGN KEY REFERENCES tblLOCATION(LocationID),
	SectorID int FOREIGN KEY REFERENCES tblSector(SectorID),
	MenagerID int FOREIGN KEY REFERENCES tblEmployee(EmployeeID),
	GenderID int FOREIGN KEY REFERENCES tblGender(GenderID) 
);


GO


CREATE VIEW vwLOCATION
AS

SELECT LocationID, Adress + ', '+ Place + ',' + Country AS Location
FROM dbo.tblLOCATION

select * from tblEmployee;

select * from tblGender;

select * from tblLOCATION;

select * from tblSector;
 
select * from vwLOCATION;
 
 
 


 