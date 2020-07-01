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

GO

GO


CREATE VIEW vwMenager
AS

SELECT EmployeeID, FirstName + ' '+ LastName  AS Menager
FROM dbo.tblEmployee

GO

GO
CREATE VIEW vwEmployee
AS

SELECT   dbo.tblLOCATION.Adress + ', '+dbo.tblLOCATION.Place  + ', ' +dbo.tblLOCATION.Country AS Location,
         empl.EmployeeID, dbo.tblLOCATION.LocationID, dbo.tblSector.SectorID,dbo.tblGender.GenderID,
		 empl.MenagerID ,
		 empl.FirstName, empl.LastName,empl.DateOfBirth,menager.FirstName + ' ' + menager.LastName AS MenagerName,
         empl.JMBG, empl.RegistrationNumber,empl.TelefonNumber,dbo.tblSector.SectorName,
		 dbo.tblGender.Gender
FROM            dbo.tblEmployee empl INNER JOIN
            dbo.tblSector ON dbo.tblSector.SectorID = empl.SectorID INNER JOIN
            dbo.tblLOCATION ON empl.LocationID = dbo.tblLOCATION.LocationID INNER JOIN
			dbo.tblEmployee menager ON empl.MenagerID = menager.EmployeeID INNER JOIN
			dbo.tblGender ON dbo.tblGender.GenderID = empl.GenderID
GO





 INSERT INTO  tblGender VALUES ('M');
 INSERT INTO  tblGender VALUES ('F');
 INSERT INTO  tblGender VALUES ('O');
 
  --****************
