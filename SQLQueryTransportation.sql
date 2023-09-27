-- Create database
CREATE DATABASE TransportationProject

-- Use database
USE TransportationProject

-- Create Employess table
CREATE TABLE Employees (
    EmployeeID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
	Sex varchar(10),
	ContactNumber varchar(20),
    Role VARCHAR(50),
    -- Add other relevant columns as needed
);

-- Create User table
CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Username VARCHAR(50) NOT NULL,
    Password VARCHAR(100) NOT NULL,
	EmployeeID INT,
    Role VARCHAR(50),
    -- Add other relevant columns as needed
	FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID)
);

-- Create Vehicles table
CREATE TABLE Vehicles (
    VehicleID INT IDENTITY(1,1) PRIMARY KEY,
    Model VARCHAR(50),
    Type VARCHAR(50),
    Capacity INT,
    LicensePlate VARCHAR(50),
    -- Add other relevant columns as needed
);

-- Create Routes table
CREATE TABLE Routes (
    RouteID INT IDENTITY(1,1) PRIMARY KEY,
    StartLocation VARCHAR(100),
    EndLocation VARCHAR(100),
    Distance DECIMAL(10, 2),
    TravelTime TIME,
    -- Add other relevant columns as needed
);

-- Create Trips table
CREATE TABLE Trips (
    TripID INT IDENTITY(1,1) PRIMARY KEY,
    VehicleID INT,
    Driver INT,
    StartTime DATETIME,
    EndTime DATETIME,
    RouteID INT,
    -- Add other relevant columns as needed
    FOREIGN KEY (VehicleID) REFERENCES Vehicles(VehicleID),
    FOREIGN KEY (RouteID) REFERENCES Routes(RouteID),
	FOREIGN KEY (Driver) REFERENCES Employees(EmployeeID)
);

SELECT * FROM Trips;

-- Create PassengerBookings table
CREATE TABLE PassengerBookings (
    BookingID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerName VARCHAR(100),
    ContactNumber VARCHAR(20),
    TripID INT,
    SeatNumber INT,
    Fare DECIMAL(10, 2),
    -- Add other relevant columns as needed
    FOREIGN KEY (TripID) REFERENCES Trips(TripID)
);

-- Create FreightShipments table
CREATE TABLE FreightShipments (
    ShipmentID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerName VARCHAR(100),
    ContactNumber VARCHAR(20),
    TripID INT,
    Weight DECIMAL(10, 2),
    GoodsType VARCHAR(50),
    PickupLocation VARCHAR(100),
    DeliveryLocation VARCHAR(100),
    -- Add other relevant columns as needed
    FOREIGN KEY (TripID) REFERENCES Trips(TripID)
);
