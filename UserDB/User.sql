CREATE TABLE [dbo].[User]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [UserName] VARCHAR(MAX) NOT NULL, 
    [UserSurname] VARCHAR(MAX) NOT NULL, 
    [UserEmail] VARCHAR(MAX) NOT NULL, 
    [UserPassword] VARCHAR(MAX) NOT NULL, 
    [UserType] VARCHAR(20) NOT NULL, 
    [Status] VARCHAR(10) NOT NULL, 
    [SpiceCode] VARCHAR(MAX) NOT NULL
)
