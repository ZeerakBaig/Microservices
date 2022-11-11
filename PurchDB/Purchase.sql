CREATE TABLE [dbo].[Purchase]
(
	[BridgeId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Id] INT not null,
	[VehicleId] INT not null,
	[Rating] INT Check(Rating >=1 And Rating <=5) not null,
	[DeliveryDate] DateTime not null
)
