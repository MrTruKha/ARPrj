CREATE DATABASE ARPrj;
CREATE TABLE Airlines (
    AirlineId int NOT NULL IDENTITY(1,1),
    AirlineName nvarchar(100) NOT NULL,
    NetPrice DECIMAL(19, 4) not null, 
    PRIMARY KEY (AirlineId),
	CreateDate datetime,
	UpdateDate datetime,
);
CREATE TABLE TicketsType (
    TicketsTypeId int NOT NULL IDENTITY(1,1),
    TypeName nvarchar(100) NOT NULL,
    PRIMARY KEY (TicketsTypeId),
	CreateDate datetime,
	UpdateDate datetime,
);
Create table InformationFlights(
	InformationFlightID int not null identity(1,1),
	TicketsTypeId int foreign key references TicketsType(TicketsTypeId),
	CreateDate datetime,
	UpdateDate datetime,
	primary key (InformationFlightID)
);
CREATE TABLE Flights (
    FlightId int NOT NULL IDENTITY(1,1),
    [From] nvarchar(100) NOT NULL,
    [To] nvarchar(100) not null,
	DepartureTime time,
	ArrivalTime time,
	SeatsLeft int,
	InformationFlightID int foreign key references InformationFlights(InformationFlightID),
	AirlineId int FOREIGN KEY REFERENCES Airlines(AirlineId), 
    PRIMARY KEY (FlightId),
	CreateDate datetime,
	UpdateDate datetime,  
);
CREATE TABLE AirlinesTicketsType (
    Id int NOT NULL IDENTITY(1,1),
    PRIMARY KEY (Id),
	NumberOfSeats int,
	AirlineId int FOREIGN KEY REFERENCES Airlines(AirlineId),
	TicketsTypeId int FOREIGN KEY REFERENCES TicketsType(TicketsTypeId),
	Price decimal(19,4),
	CreateDate datetime,
	UpdateDate datetime,
);
Create table Promotion(
	PromotionId int not null Identity(1,1),
	PromotionName nvarchar(100),
	PercentOff int,
	PRIMARY KEY (PromotionId),	
	CreateDate datetime,
	UpdateDate datetime,
);
Create table Roles(
	RoleId int not null identity(1,1),
	Role int not null,
	primary key(RoleId)
);
Create table Users(
	UserId int not null identity(1,1),
	UserName varchar(255),
	Email varchar(255),
	[Password] varchar(200),
	CreateDate datetime,
	UpdateDate datetime,
	IsActive bit,
	primary key(UserId)
);
Create table UsersRoles(
	UsersRolesId int not null identity(1,1),
	UserId int foreign key references [Users](UserId),
	RoleId int foreign key references [Roles](RoleId),
	primary key(UsersRolesId),
	CreateDate datetime,
	UpdateDate datetime,
);
Create table [Order](
	[OrderId] int not null Identity(1,1),
	CustomerId int FOREIGN KEY REFERENCES Users(UserId),
	PRIMARY KEY ([OrderId]),
	TotalPrice Decimal(19,4),
	CreateDate datetime,
	UpdateDate datetime,
);
Create table [OrderDetail](
	OrderDetailId int not null identity(1,1),
	OrderId int foreign key references [Order](OrderId),
	primary key(OrderDetailId),
	FlightId int foreign key references [Flights](FlightId),
	[Count] int not null,
	TicketsTypeId int foreign key references [TicketsType](TicketsTypeId)
);



Create table [Airports](
	AirportId int not null identity(1,1),
	[Name] nvarchar(200),
	primary key(AirportId),
);



ALTER TABLE [Flights]
Drop COLUMN [From];
ALTER TABLE [Flights]
drop column [to];
ALTER TABLE [Flights]
add [To] int,
FOREIGN KEY([To]) REFERENCES dbo.Flights(FlightId);
ALTER TABLE [Flights]
add [From] int,
FOREIGN KEY([From]) REFERENCES dbo.Flights(FlightId);