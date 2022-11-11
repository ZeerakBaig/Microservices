CREATE TABLE [dbo].[Service]
(
	[ServiceId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[MechanicId] INT NOT NULL,
	[Id] INT NOT NULL,
	[RequestDate] DATETIME NOT NULL,
	[RequestStatus] VARCHAR(10) NOT NULL,
	[CustomerReview] VARCHAR(MAX),
	[MechanicRating] INT
)
