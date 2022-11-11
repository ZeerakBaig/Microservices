CREATE TABLE [dbo].[Mechanic]
(
	[MechanicId] INT NOT NULL PRIMARY KEY,
	[MechanicRegNumber] VARCHAR(255) NOT NULL,
	[MechanicSpeciality] VARCHAR(255) NOT NULL,
	[YearsOfExperience] INT NOT NULL,
	[Address] VARCHAR(MAX) NOT NULL,
)
