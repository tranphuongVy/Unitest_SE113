﻿CREATE DATABASE airplanedb
USE airplanedb


CREATE TABLE PERMISSION
(
	PermissionID INT PRIMARY KEY ,
	PermissionName VARCHAR(40),
	isDeleted int
)
CREATE TABLE ACCOUNT
(
	UserID VARCHAR(20) ,
	UserName NVARCHAR(40) NOT NULL,
	Phone VARCHAR(20) NULL,
	Email VARCHAR(60),
	Birth SMALLDATETIME NOT NULL,
	PasswordUser VARCHAR(60) NOT NULL,
	PermissionID INT FOREIGN KEY REFERENCES PERMISSION(PermissionID),
	isDeleted int,
	Image VARBINARY(MAX) NULL,

	PRIMARY KEY(UserID, Email)
)


CREATE TABLE AIRPORT
(
	AirportID VARCHAR(20) PRIMARY KEY,
	AirportName NVARCHAR(40) NOT NULL,
	isDeleted int
)

CREATE TABLE TICKET_CLASS
(
	TicketClassID VARCHAR(20) PRIMARY KEY,
	TicketClassName NVARCHAR(40) NOT NULL,
	BaseMultiplier FLOAT NOT NULL,
	isDeleted int,
)

CREATE TABLE FLIGHT
(
	FlightID VARCHAR(20) PRIMARY KEY,
	SourceAirportID VARCHAR(20) NOT NULL,
	DestinationAirportID VARCHAR(20) NOT NULL,
	FlightDay SMALLDATETIME NOT NULL, -- Ngay khoi hanh
	FlightTime TIME NOT NULL, -- Thoi gian bay
	Price MONEY NOT NULL,
	isDeleted int,
	FOREIGN KEY (SourceAirportID) REFERENCES AIRPORT(AirportID),
	FOREIGN KEY (DestinationAirportID) REFERENCES AIRPORT(AirportID)
)

CREATE TABLE CUSTOMER
(
	ID VARCHAR(20) PRIMARY KEY,
	CustomerName NVARCHAR(40) NOT NULL,
	Phone VARCHAR(20) NULL,
	Email VARCHAR(60) NULL,
	Birth SMALLDATETIME NOT NULL,
	isDeleted int,
)

CREATE TABLE BOOKING_TICKET
(
	TicketID VARCHAR(20) PRIMARY KEY NOT NULL,
    FlightID VARCHAR(20) NOT NULL,
    ID VARCHAR(20) NOT NULL,
    TicketClassID VARCHAR(20) NOT NULL,
    TicketStatus INT NOT NULL,
    BookingDate SMALLDATETIME NOT NULL,
	isDeleted int,
    FOREIGN KEY (FlightID) REFERENCES Flight(FlightID),
    FOREIGN KEY (ID) REFERENCES CUSTOMER(ID),
    FOREIGN KEY (TicketClassID) REFERENCES TICKET_CLASS(TicketClassID)
)

CREATE TABLE INTERMEDIATE_AIRPORT
(
	AirportID VARCHAR(20) FOREIGN KEY REFERENCES AIRPORT(AirportID),
	FlightID VARCHAR(20) FOREIGN KEY REFERENCES FLIGHT(FlightID),
	layoverTime TIME NOT NULL, --loi chinh ta
	Note NVARCHAR(100) NULL,
	isDeleted int,
	PRIMARY KEY(AirportID, FlightID)
)

CREATE TABLE TICKETCLASS_FLIGHT
(
	TicketClassID VARCHAR(20) FOREIGN KEY REFERENCES TICKET_CLASS(TicketClassID),
	FlightID VARCHAR(20) FOREIGN KEY REFERENCES FLIGHT(FlightID),
	Quantity INT,
	Multiplier FLOAT NOT NULL,
	isDeleted int,
	PRIMARY KEY (TicketClassID, FlightID)
)

CREATE TABLE PARAMETER
(
    AirportCount            int,            -- Number of airports
    MinFlightTime			time,
	IntermediateAirportCount int,           -- Number of intermediate airports
    MinStopTime             time,            -- Minimum stop time
    MaxStopTime             time,            -- Maximum stop time
    TicketClassCount        int,            -- Number of ticket class
    SlowestBookingTime      time,           -- Slowest booking time
    CancelTime              time,            -- Cancellation time
	isDeleted int,
);
drop table ACCOUNT
delete from ACCOUNT

select *from FLIGHT
select *from AIRPORT
select *from ACCOUNT
select *from TICKET_CLASS
select *from TICKETCLASS_FLIGHT
select *from BOOKING_TICKET
select *from CUSTOMER
select *from PARAMETER
----------TEST CASE--------
--PERMISSION
INSERT INTO PERMISSION VALUES (1, 'Admin', 0);
INSERT INTO PERMISSION VALUES (2, 'Staff', 0);

--ACCOUNT

-- tk: admin@gmail.com; pass: password1
-- tk: staff1@gmail.com; pass: password2
-- tk: staff2@gmail.com; pass: password3
INSERT INTO ACCOUNT VALUES ('000', 'admin', '0123456789', 'admin@gmail.com', '1980-01-01', '7c6a180b36896a0a8c02787eeafb0e4c', 1, 0, NULL);
INSERT INTO ACCOUNT VALUES ('001', 'staff1', '0123456790', 'staff1@gmail.com', '1985-02-02', '6cb75f652a9b52798eb6cf2201057c73', 2, 0, NULL);
INSERT INTO ACCOUNT VALUES ('002', 'staff2', '0123456791', 'staff2@gmail.com', '1990-03-03', '819b0643d6b89dc9b579fdfc9094f28e', 2, 0, NULL);

--AIRPORT 
INSERT INTO AIRPORT VALUES ('000', N'Nội Bài', 0);
INSERT INTO AIRPORT VALUES ('001', N'Tân Sơn Nhất', 0);
INSERT INTO AIRPORT VALUES ('002', N'Đà Nẵng', 0);
insert into AIRPORT values ('003',N'Phú Quốc', 0)
insert into AIRPORT values ('004',N'Cam Ranh', 0)
insert into AIRPORT values ('005',N'Điện Biên Phủ', 0)
INSERT INTO AIRPORT VALUES ('006', N'Cần Thơ', 0);
INSERT INTO AIRPORT VALUES ('007', N'Vinh', 0);
INSERT INTO AIRPORT VALUES ('008', N'Hải Phòng', 0);
INSERT INTO AIRPORT VALUES ('009', N'Phù Cát', 0);

--TICKET_CLASS
INSERT INTO TICKET_CLASS VALUES ('001', N'Economy', 1.0, 0);
INSERT INTO TICKET_CLASS VALUES ('002', N'Business', 1.5, 0);
INSERT INTO TICKET_CLASS VALUES ('003', N'First Class', 2.0, 0);

--FLIGHT
INSERT INTO FLIGHT VALUES ('FL001', '000', '001', '2024-06-01', '08:00:00', 100.0, 0);
INSERT INTO FLIGHT VALUES ('FL002', '001', '002', '2024-06-02', '09:00:00', 150.0, 0);
INSERT INTO FLIGHT VALUES ('FL003', '002', '000', '2024-06-03', '10:00:00', 200.0, 0);
INSERT INTO FLIGHT VALUES ('FL004', '006', '009', '2024-06-29', '15:47:05', 381.0, 0);
INSERT INTO FLIGHT VALUES ('FL005', '003', '004', '2024-06-30', '10:16:32', 506.0, 0);
INSERT INTO FLIGHT VALUES ('FL006', '005', '002', '2024-07-01', '16:40:48', 513.0, 0);
INSERT INTO FLIGHT VALUES ('FL007', '008', '004', '2024-07-02', '05:55:09', 890.0, 0);
INSERT INTO FLIGHT VALUES ('FL008', '005', '003', '2024-07-03', '21:44:06', 163.0, 0);
INSERT INTO FLIGHT VALUES ('FL009', '006', '000', '2024-07-04', '19:56:08', 364.0, 0);
INSERT INTO FLIGHT VALUES ('FL010', '001', '000', '2024-07-05', '07:22:26', 373.0, 0);
INSERT INTO FLIGHT VALUES ('FL011', '000', '003', '2024-07-06', '03:33:51', 121.0, 0);
INSERT INTO FLIGHT VALUES ('FL012', '004', '002', '2024-07-07', '13:25:08', 253.0, 0);
INSERT INTO FLIGHT VALUES ('FL013', '003', '002', '2024-07-08', '01:37:53', 994.0, 0);
INSERT INTO FLIGHT VALUES ('FL014', '002', '004', '2024-07-09', '06:29:18', 236.0, 0);
INSERT INTO FLIGHT VALUES ('FL015', '008', '000', '2024-07-10', '01:27:32', 145.0, 0);
INSERT INTO FLIGHT VALUES ('FL016', '008', '003', '2024-07-11', '02:46:57', 917.0, 0);
INSERT INTO FLIGHT VALUES ('FL017', '002', '005', '2024-07-12', '03:26:14', 590.0, 0);
INSERT INTO FLIGHT VALUES ('FL018', '009', '008', '2024-07-13', '17:13:24', 156.0, 0);
INSERT INTO FLIGHT VALUES ('FL019', '003', '005', '2024-07-14', '09:01:07', 337.0, 0);
INSERT INTO FLIGHT VALUES ('FL020', '000', '006', '2024-07-15', '20:07:57', 514.0, 0);
INSERT INTO FLIGHT VALUES ('FL021', '008', '006', '2024-07-16', '15:31:14', 839.0, 0);
INSERT INTO FLIGHT VALUES ('FL022', '004', '000', '2024-07-17', '11:13:06', 103.0, 0);
INSERT INTO FLIGHT VALUES ('FL023', '004', '005', '2024-07-18', '03:49:49', 402.0, 0);
INSERT INTO FLIGHT VALUES ('FL024', '002', '005', '2024-07-19', '07:18:50', 758.0, 0);
INSERT INTO FLIGHT VALUES ('FL025', '009', '002', '2024-07-20', '14:23:58', 432.0, 0);
INSERT INTO FLIGHT VALUES ('FL026', '004', '007', '2024-07-21', '02:28:48', 900.0, 0);
INSERT INTO FLIGHT VALUES ('FL027', '004', '003', '2024-07-22', '18:28:24', 127.0, 0);
INSERT INTO FLIGHT VALUES ('FL028', '005', '000', '2024-07-23', '19:41:00', 596.0, 0);
INSERT INTO FLIGHT VALUES ('FL029', '002', '005', '2024-07-24', '12:21:01', 765.0, 0);
INSERT INTO FLIGHT VALUES ('FL030', '004', '001', '2024-07-25', '03:25:28', 923.0, 0);
INSERT INTO FLIGHT VALUES ('FL031', '008', '004', '2024-07-26', '02:49:27', 698.0, 0);
INSERT INTO FLIGHT VALUES ('FL032', '008', '007', '2024-07-27', '20:12:03', 118.0, 0);
INSERT INTO FLIGHT VALUES ('FL033', '001', '000', '2024-07-28', '00:17:04', 956.0, 0);
INSERT INTO FLIGHT VALUES ('FL034', '009', '000', '2024-07-29', '17:59:33', 670.0, 0);
INSERT INTO FLIGHT VALUES ('FL035', '000', '002', '2024-07-30', '20:03:24', 103.0, 0);
INSERT INTO FLIGHT VALUES ('FL036', '003', '004', '2024-07-31', '22:52:23', 260.0, 0);
INSERT INTO FLIGHT VALUES ('FL037', '009', '005', '2024-08-01', '22:43:25', 485.0, 0);
INSERT INTO FLIGHT VALUES ('FL038', '008', '007', '2024-08-02', '21:47:21', 428.0, 0);
INSERT INTO FLIGHT VALUES ('FL039', '004', '002', '2024-08-03', '06:52:51', 805.0, 0);
INSERT INTO FLIGHT VALUES ('FL040', '001', '008', '2024-08-04', '05:26:34', 871.0, 0);
INSERT INTO FLIGHT VALUES ('FL041', '007', '008', '2024-08-05', '19:15:53', 882.0, 0);
INSERT INTO FLIGHT VALUES ('FL042', '009', '000', '2024-08-06', '05:13:26', 823.0, 0);
INSERT INTO FLIGHT VALUES ('FL043', '006', '008', '2024-08-07', '16:22:34', 532.0, 0);
INSERT INTO FLIGHT VALUES ('FL044', '002', '008', '2024-08-08', '08:34:03', 937.0, 0);
INSERT INTO FLIGHT VALUES ('FL045', '007', '008', '2024-08-09', '09:25:14', 182.0, 0);
INSERT INTO FLIGHT VALUES ('FL046', '009', '005', '2024-08-10', '08:13:15', 484.0, 0);
INSERT INTO FLIGHT VALUES ('FL047', '003', '007', '2024-08-11', '08:56:17', 758.0, 0);
INSERT INTO FLIGHT VALUES ('FL048', '000', '007', '2024-08-12', '11:37:00', 350.0, 0);
INSERT INTO FLIGHT VALUES ('FL049', '004', '009', '2024-08-13', '04:47:51', 387.0, 0);
INSERT INTO FLIGHT VALUES ('FL050', '007', '003', '2024-08-14', '15:10:05', 644.0, 0);
INSERT INTO FLIGHT VALUES ('FL051', '004', '000', '2024-08-15', '11:29:03', 490.0, 0);
INSERT INTO FLIGHT VALUES ('FL052', '005', '008', '2024-08-16', '09:05:02', 424.0, 0);
INSERT INTO FLIGHT VALUES ('FL053', '005', '009', '2024-08-17', '13:14:24', 508.0, 0);
INSERT INTO FLIGHT VALUES ('FL054', '001', '007', '2024-08-18', '11:16:15', 548.0, 0);
INSERT INTO FLIGHT VALUES ('FL055', '009', '003', '2024-08-19', '16:50:34', 947.0, 0);
INSERT INTO FLIGHT VALUES ('FL056', '004', '009', '2024-08-20', '11:52:39', 774.0, 0);
INSERT INTO FLIGHT VALUES ('FL057', '008', '002', '2024-08-21', '07:57:46', 131.0, 0);
INSERT INTO FLIGHT VALUES ('FL058', '006', '007', '2024-08-22', '13:23:53', 396.0, 0);
INSERT INTO FLIGHT VALUES ('FL059', '004', '002', '2024-08-23', '16:44:01', 167.0, 0);
INSERT INTO FLIGHT VALUES ('FL060', '004', '008', '2024-08-24', '19:44:45', 113.0, 0);
INSERT INTO FLIGHT VALUES ('FL061', '002', '007', '2024-08-25', '05:31:40', 545.0, 0);
INSERT INTO FLIGHT VALUES ('FL062', '000', '009', '2024-08-26', '02:20:27', 531.0, 0);
INSERT INTO FLIGHT VALUES ('FL063', '000', '001', '2024-08-27', '02:59:23', 118.0, 0);
INSERT INTO FLIGHT VALUES ('FL064', '002', '000', '2024-08-28', '00:42:56', 674.0, 0);
INSERT INTO FLIGHT VALUES ('FL065', '005', '007', '2024-08-29', '08:54:48', 195.0, 0);
INSERT INTO FLIGHT VALUES ('FL066', '008', '006', '2024-08-30', '13:41:46', 115.0, 0);
INSERT INTO FLIGHT VALUES ('FL067', '005', '006', '2024-08-31', '17:13:58', 158.0, 0);
INSERT INTO FLIGHT VALUES ('FL068', '007', '003', '2024-09-01', '07:45:14', 423.0, 0);


--CUSTOMER
INSERT INTO CUSTOMER VALUES ('12345678901', 'Nguyen Van A', '0123456789', 'nva@example.com', '1990-01-01', 0);
INSERT INTO CUSTOMER VALUES ('12345678902', 'Tran Thi B', '0123456790', 'ttb@example.com', '1992-02-02', 0);
INSERT INTO CUSTOMER VALUES ('12345678903', 'Le Van C', '0123456791', 'lvc@example.com', '1994-03-03', 0);
INSERT INTO CUSTOMER VALUES ('12345678904', 'Hoang Thi D', '0123456792', 'htd@example.com', '1996-04-04', 0);
INSERT INTO CUSTOMER VALUES ('12345678905', 'Pham Van E', '0123456793', 'pve@example.com', '1998-05-05', 0);
INSERT INTO CUSTOMER VALUES ('12345678906', 'Vu Thi F', '0123456794', 'vtf@example.com', '2000-06-06', 0);
INSERT INTO CUSTOMER VALUES ('12345678907', 'Dang Van G', '0123456795', 'dvg@example.com', '2002-07-07', 0);
INSERT INTO CUSTOMER VALUES ('12345678908', 'Mai Thi H', '0123456796', 'mth@example.com', '1988-08-08', 0);
--BOOKING_TICKET
INSERT INTO BOOKING_TICKET VALUES ('BT001', 'FL001', '12345678901', '001', 1, '2024-05-01', 0);
INSERT INTO BOOKING_TICKET VALUES ('BT002', 'FL002', '12345678902', '002', 1, '2024-05-02', 0);
INSERT INTO BOOKING_TICKET VALUES ('BT003', 'FL003', '12345678903', '003', 1, '2024-05-03', 0);
INSERT INTO BOOKING_TICKET VALUES ('BT004', 'FL003', '12345678904', '003', 1, '2024-05-03', 0);
INSERT INTO BOOKING_TICKET VALUES ('BT005', 'FL002', '12345678905', '002', 1, '2024-05-03', 0);
INSERT INTO BOOKING_TICKET VALUES ('BT006', 'FL001', '12345678906', '002', 1, '2024-05-03', 0);
INSERT INTO BOOKING_TICKET VALUES ('BT007', 'FL002', '12345678907', '001', 1, '2024-05-03', 0);
INSERT INTO BOOKING_TICKET VALUES ('BT008', 'FL002', '12345678908', '001', 1, '2024-05-03', 0);

--INTERMEDIATE_AIRPORT
INSERT INTO INTERMEDIATE_AIRPORT VALUES ('001', 'FL001', '01:00:00', 'Brief stop', 0);
INSERT INTO INTERMEDIATE_AIRPORT VALUES ('002', 'FL002', '01:30:00', 'Refuel', 0);
INSERT INTO INTERMEDIATE_AIRPORT VALUES ('000', 'FL003', '02:00:00', 'Maintenance', 0);

--TICKETCLASS_FLIGHT
INSERT INTO TICKETCLASS_FLIGHT VALUES ('001', 'FL001', 30, 1.0, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('002', 'FL001', 20, 1.5, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('001', 'FL002', 30, 1.0, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('002', 'FL002', 30, 1.5, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL003', 20, 2.0, 0)
INSERT INTO TICKETCLASS_FLIGHT VALUES ('001', 'FL004', 42, 2.6, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('002', 'FL004', 48, 2.7, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('000', 'FL005', 33, 1.8, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('002', 'FL005', 38, 2.5, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('002', 'FL006', 48, 2.6, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL006', 42, 2.8, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('000', 'FL007', 47, 1.8, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL007', 40, 2.5, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('002', 'FL008', 31, 2.4, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL008', 50, 2.8, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('000', 'FL009', 46, 2.4, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('002', 'FL009', 39, 2.6, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('000', 'FL010', 32, 2.8, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL010', 42, 2.9, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('000', 'FL011', 48, 3.0, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL011', 36, 3.0, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('002', 'FL012', 31, 2.4, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL012', 34, 2.6, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('000', 'FL013', 37, 1.8, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('002', 'FL013', 32, 2.0, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('000', 'FL014', 31, 3.0, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('001', 'FL014', 40, 3.0, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('000', 'FL015', 35, 2.8, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('002', 'FL015', 40, 2.9, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('002', 'FL016', 41, 2.4, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL016', 34, 2.5, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('002', 'FL017', 30, 2.6, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL017', 41, 2.8, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('002', 'FL018', 47, 2.0, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL018', 46, 2.9, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('001', 'FL019', 32, 2.0, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL019', 35, 2.9, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('000', 'FL020', 34, 2.7, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL020', 41, 2.8, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('000', 'FL021', 35, 2.1, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('001', 'FL021', 33, 2.9, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('000', 'FL022', 32, 1.6, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('002', 'FL022', 46, 2.4, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('001', 'FL023', 32, 2.7, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL023', 39, 2.9, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('001', 'FL024', 41, 1.8, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('002', 'FL024', 50, 2.4, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('001', 'FL025', 45, 2.1, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL025', 34, 2.5, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('002', 'FL026', 45, 2.4, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL026', 41, 3.0, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('002', 'FL027', 39, 1.7, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL027', 49, 2.3, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('000', 'FL028', 45, 1.9, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL028', 30, 2.3, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('001', 'FL029', 35, 1.9, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('002', 'FL029', 37, 2.0, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('000', 'FL030', 44, 2.4, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('002', 'FL030', 32, 3.0, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('000', 'FL031', 44, 1.6, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL031', 40, 2.8, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('000', 'FL032', 46, 2.2, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL032', 35, 2.4, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('000', 'FL033', 31, 1.9, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('002', 'FL033', 31, 2.9, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('000', 'FL034', 49, 1.9, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('001', 'FL034', 41, 2.3, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('000', 'FL035', 40, 1.7, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL035', 48, 2.7, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('000', 'FL036', 31, 1.6, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('001', 'FL036', 48, 2.3, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('002', 'FL037', 43, 2.7, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL037', 34, 2.8, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('000', 'FL038', 42, 1.9, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL038', 32, 2.0, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('001', 'FL039', 38, 1.5, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL039', 40, 1.7, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('000', 'FL040', 35, 2.7, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('001', 'FL040', 33, 2.7, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('000', 'FL041', 50, 2.7, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('002', 'FL041', 40, 3.0, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('001', 'FL042', 36, 2.7, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('002', 'FL042', 41, 2.9, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('000', 'FL043', 48, 1.8, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL043', 38, 2.8, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('002', 'FL044', 33, 1.8, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL044', 46, 2.3, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('001', 'FL045', 37, 2.9, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('002', 'FL045', 40, 3.0, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('001', 'FL046', 38, 1.6, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL046', 33, 1.7, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('000', 'FL047', 47, 2.9, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('001', 'FL047', 40, 2.9, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('000', 'FL048', 41, 2.4, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL048', 30, 2.7, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('001', 'FL049', 33, 1.9, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('002', 'FL049', 44, 2.3, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('001', 'FL050', 48, 2.8, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('002', 'FL050', 38, 2.9, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('000', 'FL051', 34, 2.5, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('001', 'FL051', 37, 2.7, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('001', 'FL052', 43, 2.0, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL052', 48, 2.2, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('000', 'FL053', 37, 2.7, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL053', 44, 2.9, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('001', 'FL054', 49, 2.0, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('002', 'FL054', 41, 2.3, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('000', 'FL055', 36, 1.5, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL055', 50, 1.6, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('000', 'FL056', 50, 2.4, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL056', 34, 2.6, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('000', 'FL057', 36, 2.4, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL057', 45, 2.7, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('000', 'FL058', 41, 1.7, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('001', 'FL058', 42, 2.3, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('001', 'FL059', 38, 1.9, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('002', 'FL059', 34, 2.5, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('002', 'FL060', 33, 2.4, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL060', 33, 2.7, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('002', 'FL061', 30, 2.8, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL061', 39, 2.9, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('001', 'FL062', 50, 1.6, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL062', 36, 2.1, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('001', 'FL063', 32, 1.9, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL063', 48, 2.1, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('000', 'FL064', 48, 2.0, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('001', 'FL064', 38, 2.6, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('001', 'FL065', 42, 2.7, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL065', 40, 2.9, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('000', 'FL066', 47, 2.7, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL066', 34, 2.9, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('000', 'FL067', 50, 1.9, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL067', 47, 2.1, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('002', 'FL068', 37, 1.8, 0);
INSERT INTO TICKETCLASS_FLIGHT VALUES ('003', 'FL068', 50, 2.0, 0);


--PARAMETER

INSERT INTO PARAMETER VALUES (10, '08:00:00', 3, '00:10:00', '00:20:00', 3, '07:00:00', '06:00:00', 0)