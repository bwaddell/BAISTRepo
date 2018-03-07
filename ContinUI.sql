
--===================    Drop and replace table    ===================--


-------------DO NOT RUN this ON DATABAIST--------------------


USE master
GO
DECLARE @dbname sysname
SET @dbname = 'ContinUI'
DECLARE @spid int
SELECT @spid = min(spid) from master.dbo.sysprocesses where dbid = db_id(@dbname)
WHILE @spid IS NOT NULL
BEGIN
EXECUTE ('KILL ' + @spid)
SELECT @spid = min(spid) from master.dbo.sysprocesses where dbid = db_id(@dbname) AND spid > @spid
END
GO
SET NOCOUNT ON
GO
USE master
GO
--sp_help 
IF exists (select * from sysdatabases where name='ContinUI')
	drop database ContinUI											--ContinUI
GO


create database ContinUI
go
use ContinUI
GO



DataBase: ContinUIDB
adminName: cody
password: Jacob$17




create table Facilitator
(
	FacilitatorID int unique identity(1,1) not null,
	FirstName nvarchar(20) not null,
	LastName nvarchar(20) not null,
	Title nvarchar(20) null,
	Organization nvarchar(20) null,
	City nvarchar(20) null	
)
alter table Facilitator
	add constraint PK_Facilitator primary key (FacilitatorID)
Go


create table EventDetails
(
	EventKey nvarchar(5) not null,
	FacilitatorID int not null,
	Location nvarchar(30) not null,
	Performer nvarchar(20) not null,
	NatureOfEvent nvarchar(20) not null,
	EventDate date not null,
	EventBegin datetime null,
	EventEnd datetime null
)
alter table EventDetails
	add constraint PK_EventDetails primary key (EventKey),
		constraint FK_EventDetails foreign key (FacilitatorID) references Facilitator(FacilitatorID),
		constraint CK_EventDetails check (EventEnd > EventBegin)
Go



create table Evaluator
(
	EvaluatorID int unique identity(1,1) not null,
	Name nvarchar(60) not null,
	DateOfBirth date null,
	Sex varchar(1) null,
	SchoolOrOrganization nvarchar(40) null,
	City nvarchar(20) null,
	VotingCriteria nvarchar(40) not null 
)
alter table Evaluator
	add constraint PK_Evaluator primary key (EvaluatorID) 
Go

--drop table EvaluativeData
create table EvaluativeData
(
	EventKey NVarchar(5) not null,
	EvaluatorID int not null,
	TimeOfData datetime not null,
	Rating int not null
)
alter table EvaluativeData
	add constraint PK_EvaluativeData primary key (TimeOfData, EventKey, EvaluatorID),
		constraint FK_EvaluativeData_Event foreign key (EventKey) references EventDetails(EventKey),
		constraint FK_EvaluativeData_Evaluator foreign key (EvaluatorID) references Evaluator(EvaluatorID),
		constraint CK_EvaluativeData check (Rating > 0)
GO


create procedure AddEvaluationDataPoint
(
	@Event int = null,
	@Evaluator int = null,
	@Rating int = null,
	@TimeOfData datetime = null
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1

	if(@Event is null)
		raiserror('AddEvaluationDataPoint - Required Parameter: @Event',16,1)
	else
	if(@Evaluator is null)
		raiserror('AddEvaluationDataPoint - Required Parameter: @Evaluator',16,1)
	else
	if(@Rating is null)
		raiserror('AddEvaluationDataPoint - Required Parameter: @Rating',16,1)
	else
	if(@TimeOfData is null)
		raiserror('AddEvaluationDataPoint - Required Parameter: @TimeOfData',16,1)
	else
		begin
			insert into EvaluativeData 
			values (@Event, @Evaluator, @TimeOfData, @Rating)

			if @@ERROR = 0
				set @ReturnCode = 0
			else
				raiserror('AddEvaluationDataPoint - Insert Error: Query Failed',16,1)
		end
	return @ReturnCode
GO


create procedure AddEvaluator
(
	@Name nvarchar(60) = null,
	@DateOfBirth date = null,
	@Sex varchar(1) = null,
	@SchoolOrOrganization nvarchar(40) = null,
	@City nvarchar(20) = null,
	@VotingCriteria nvarchar(40) = null 
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1

	if(@Name is null)
		raiserror('AddEvaluator - Required Parameter: @Name',16,1)
	else
	if(@VotingCriteria is null)
		raiserror('AddEvaluator - Required Parameter: @VotingCriteria',16,1)
	else
		begin
			insert into Evaluator 
			values (@Name, @DateOfBirth, @Sex, @SchoolOrOrganization, @City, @VotingCriteria)

			if @@ERROR = 0
				set @ReturnCode = 0
			else
				raiserror('AddEvaluator- Insert Error: Query Failed',16,1)
		end
	return @ReturnCode
GO
--select 


create procedure CreateFacilitator
(
	@FirstName nvarchar(20) = null,
	@LastName nvarchar(20) = null,
	@Title nvarchar(20) = null,
	@Organization nvarchar(20) = null,
	@City nvarchar(20) = null
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1

	if(@FirstName is null)
		raiserror('CreateFacilitator - Required Parameter: @FirstName',16,1)
	else
	if(@LastName is null)
		raiserror('CreateFacilitator - Required Parameter: @LastName',16,1)
	else
		begin
			insert into Facilitator
			values (@FirstName, @LastName, @Title, @Organization, @City)

			if @@ERROR = 0
				set @ReturnCode = 0
			else
				raiserror('CreateFacilitator - Insert Error: Query Failed',16,1)
		end
	return @ReturnCode
GO


execute CreateFacilitator 'David','Elyk','BOS','NAIT','Edmonton'
GO


create procedure CreateEvent
(
	@EventKey nvarchar(5) = null,
	@Facilitator int = null,
	@Location nvarchar(30) = null,
	@Performer nvarchar(20) = null,
	@NatureOfEvent nvarchar(20) = null,
	@EventDate date = null
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1

	if(@EventKey is null)
		raiserror('CreateEvent - Required Parameter: @EventKey',16,1)
	else
	if(@Facilitator is null)
		raiserror('CreateEvent - Required Parameter: @Facilitator',16,1)
	else
	if(@Location is null)
		raiserror('CreateEvent - Required Parameter: @Location',16,1)
	else
	if(@Performer is null)
		raiserror('CreateEvent - Required Parameter: @Performer',16,1)
	else	
	if(@NatureOfEvent is null)
		raiserror('CreateEvent - Required Parameter: @NatureOfEvent',16,1)
	else
	if(@EventDate is null)
		raiserror('CreateEvent - Required Parameter: @EventDate',16,1)
	else
		begin
			insert into EventDetails
			values (@EventKey, @Facilitator, @Location, @Performer, @NatureOfEvent, @EventDate,null,null)

			if @@ERROR = 0
				set @ReturnCode = 0
			else
				raiserror('CreateEvent - Insert Error: Query Failed',16,1)
			end
		return @ReturnCode				
GO


create procedure UpdateEventStatus
(
	@EventStart datetime = null,
	@EventFinish datetime = null
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1

	update EventDetails
	set EventBegin = @EventStart, EventEnd = @EventFinish


	if @@ERROR = 0
		set @ReturnCode = 0
	else
		raiserror('UpdateEventStatus - Update Error: Query Failed',16,1)

	return @ReturnCode
GO


create procedure GetHistoricalEvaluationData
(
	@Event int = null
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1
	
	if(@Event is null)
		raiserror('GetHistoricalEvaluationData - Select ErrorRequired Parameter: @Event',16,1)
	else
		begin
			select EvaluatorID, Rating, TimeOfData 
			from EvaluativeData
			where @Event = EventKey

			if @@ERROR = 0
				set @ReturnCode = 0
			else
				raiserror('GetHistoricalEvaluationData - Select Error: Query Failed',16,1)
		end
	return @ReturnCode				
GO


create procedure GetMostRecentEvaluativeData
(
	@Event int = null
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1
	
	if(@Event is null)
		raiserror('GetMostRecentEvaluativeData - Select Error: Query Failed',16,1)
	else
		begin
			select ed.EvaluatorID, ed.Rating
			from EvaluativeData ed
			inner join (select max(TimeOfData) as LatestData
						from EvaluativeData) evda  
			on @Event = EventKey and ed.TimeOfData = evda.LatestData


			if @@ERROR = 0
				set @ReturnCode = 0
			else
				raiserror('GetMostRecentEvaluativeData - Select Error: Query Failed',16,1)
		end
	return @ReturnCode				
GO

execute GetMostRecentEvaluativeData 1

select * from EventDetails
select * from Facilitator

create procedure GetEvent
(
	@EventKey nvarchar(5) = null
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1

	if(@EventKey is null)
		raiserror('GetEvent - Required Parameter: @EventKey',16,1)
	else
		begin
			select * from EventDetails
			where EventKey = @EventKey

			if @@ERROR = 0
				set @ReturnCode = 0
			else
				raiserror('GetEvent - Select Error: Query Failed',16,1)
			end
		return @ReturnCode				
GO