CREATE TABLE [dbo].[Vehicle]
(
	[VehicleId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[VehicleBrand] Varchar(255) not null,
	[VehicleModel] varchar(255) not null,
	[VehicleType] varchar(50) not null,
	[VehicleImage] varchar(255) not null, 
    [VehiclePrice] Decimal not null,
	[VehicleDescription] varchar(MAX) not null,
	[VehicleEngine] decimal not null,
	[VehicleColour] varchar(255) not null,
	[VehicleQuantity] int not null
)
