--ContinUI web site

--	Instructions: --
-- check that the database name below is correct then execute entire script



IF(db_id(N'ContinUIDB') IS NULL)	-- create a new database
	CREATE DATABASE ContinUIDB
go
use ContinUIDB		--use the database created
go



--******************************************--
--					Tables					--
--******************************************--

--drop table Facilitator
create table Facilitator
(
	FacilitatorID int unique identity(1,1) not null,
	Email nvarchar(40) not null,
	Password nvarchar(64) not null,
	Salt nvarchar(10) not null,
	Roles nvarchar(60) not null,
	FirstName nvarchar(20) not null,
	LastName nvarchar(20) not null,
	Title nvarchar(20) null,
	Organization nvarchar(40) null,
	City nvarchar(40) null
)
alter table Facilitator
	add constraint PK_Facilitator primary key (FacilitatorID)
Go

--drop table EventDetails
create table EventDetails
(
	EventKey nvarchar(5) not null,
	FacilitatorID int not null,
	Location nvarchar(100) not null,											
	Performer nvarchar(100) not null,											
	NatureOfEvent nvarchar(100) not null,										
	EventDate nvarchar(25) not null,
	EventBegin nvarchar(25) default '1/1/1800 12:00:00 PM',				
	EventEnd nvarchar(25) default '1/1/1800 12:00:00 PM'
)
alter table EventDetails
	add constraint PK_EventDetails primary key (EventKey),
		constraint FK_EventDetails foreign key (FacilitatorID) references Facilitator(FacilitatorID)
Go


--alter table EventDetails
--alter column
--	Location nvarchar(100) not null

--alter table EventDetails
--alter column
--	Performer nvarchar(100) not null

--alter table EventDetails
--alter column
--	NatureOfEvent nvarchar(100) not null


--drop table Evaluator
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







--****************************************--
--Stored Procedures---
--****************************************--

IF (OBJECT_ID('AddEvaluationDataPoint') IS NOT NULL)
  DROP PROCEDURE AddEvaluationDataPoint
go
create procedure AddEvaluationDataPoint
(
	@Event nvarchar(5) = null,
	@Evaluator int = null,
	@Rating int = null,
	@TimeOfData datetime = null
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1

	if(@Event is null)
		raiserror('AddEvaluationDataPoint - Required Parameter: @Event',16,1)
	if(@Evaluator is null)
		raiserror('AddEvaluationDataPoint - Required Parameter: @Evaluator',16,1)
	if(@Rating is null)
		raiserror('AddEvaluationDataPoint - Required Parameter: @Rating',16,1)
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



--**************************************--
IF (OBJECT_ID('AddEvaluator') IS NOT NULL)
  DROP PROCEDURE AddEvaluator
go
create procedure AddEvaluator
(
	@EvaluatorID int = null output,
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
	if(@VotingCriteria is null)
		raiserror('AddEvaluator - Required Parameter: @VotingCriteria',16,1)
	else
		begin
			insert into Evaluator 
			values (@Name, @DateOfBirth, @Sex, @SchoolOrOrganization, @City, @VotingCriteria)
			
			
			if @@ERROR = 0
				begin
					set @ReturnCode = 0
					select @EvaluatorID = @@IDENTITY
				end
			else
				raiserror('AddEvaluator- Insert Error: Query Failed',16,1)
		end
	return @ReturnCode
GO



--**************************************--
IF (OBJECT_ID('CreateFacilitator') IS NOT NULL)
  DROP PROCEDURE CreateFacilitator
go
create procedure CreateFacilitator
(
	@FirstName nvarchar(20) = null,
	@LastName nvarchar(20) = null,
	@Email nvarchar(40) = null,
	@Role nvarchar(60) = null,
	@Password nvarchar(64) = null,
	@Salt nvarchar(10) = null,
	@Title nvarchar(20) = null,
	@Organization nvarchar(40) = null,
	@City nvarchar(40) = null
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1

	if(@FirstName is null)
		raiserror('CreateFacilitator - Required Parameter: @FirstName',16,1)
	if(@LastName is null)
		raiserror('CreateFacilitator - Required Parameter: @LastName',16,1)
	if(@Email is null)
		raiserror('CreateFacilitator - Required Parameter: @Email',16,1)
	if(@Password is null)
		raiserror('CreateFacilitator - Required Parameter: @Password',16,1)
	if(@Salt is null)
		raiserror('CreateFacilitator - Required Parameter: @Salt',16,1)
	if(@Role is null)
		raiserror('CreateFacilitator - Required Parameter: @Role',16,1)
	else
		begin
			insert into Facilitator (Email, Password, Salt, Roles, FirstName, LastName, Title, Organization, City)
			values (@Email, @Password, @Salt, @Role, @FirstName, @LastName, @Title, @Organization, @City)

			if @@ERROR = 0
				set @ReturnCode = 0
			else
				raiserror('CreateFacilitator - Insert Error: Query Failed',16,1)
		end
	return @ReturnCode
GO



--**************************************--
IF (OBJECT_ID('CreateEvent') IS NOT NULL)
  DROP PROCEDURE CreateEvent
go
create procedure CreateEvent
(
	@EventKey nvarchar(5) = null,
	@Facilitator int = null,
	@Location nvarchar(100) = null,
	@Performer nvarchar(100) = null,
	@NatureOfEvent nvarchar(100) = null,
	@EventDate nvarchar(25) = null
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1

	SET DATEFORMAT mdy;

	if(@EventKey is null)
		raiserror('CreateEvent - Required Parameter: @EventKey',16,1)
	if(@Facilitator is null)
		raiserror('CreateEvent - Required Parameter: @Facilitator',16,1)
	if(@Location is null)
		raiserror('CreateEvent - Required Parameter: @Location',16,1)
	if(@Performer is null)
		raiserror('CreateEvent - Required Parameter: @Performer',16,1)
	if(@NatureOfEvent is null)
		raiserror('CreateEvent - Required Parameter: @NatureOfEvent',16,1)
	if(@EventDate is null)
		raiserror('CreateEvent - Required Parameter: @EventDate',16,1)
	else
		begin
			insert into EventDetails(EventKey,FacilitatorID,Location,Performer,NatureOfEvent,EventDate)
			values (@EventKey, @Facilitator, @Location, @Performer, @NatureOfEvent, @EventDate)

			if @@ERROR = 0
				set @ReturnCode = 0
			else
				raiserror('CreateEvent - Insert Error: Query Failed',16,1)
			end
		return @ReturnCode
GO



--**************************************--
IF (OBJECT_ID('UpdateEventStatus') IS NOT NULL)
  DROP PROCEDURE UpdateEventStatus
go
create procedure UpdateEventStatus
(
	@EventKey nvarchar(5) = null,
	@EventStart nvarchar(25) = null,
	@EventFinish nvarchar(25) = null
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1
	if(@EventKey is null)
		raiserror('UpdateEventStatus - Required Parameter: @EventKey',16,1)
	else
		begin
			update EventDetails
			set EventBegin = @EventStart, EventEnd = @EventFinish
			where @EventKey = EventKey
			if @@ERROR = 0
				set @ReturnCode = 0
			else
				raiserror('UpdateEventStatus - Update Error: Query Failed',16,1)
		end
	return @ReturnCode
GO



--**************************************--
IF (OBJECT_ID('GetFacilitatorInfo') IS NOT NULL)
  DROP PROCEDURE GetFacilitatorInfo										
go
create procedure GetFacilitatorInfo
(
	@Email nvarchar(20) = null
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1
	
	if(@Email is null)
		raiserror('GetHistoricalEvaluationData - Required Parameter: @Email',16,1)
	else
		begin
			select FacilitatorID, Email, Password,Salt, Roles,FirstName,LastName,Title,Organization,City from Facilitator
			where Email = @Email

			if @@ERROR = 0
				set @ReturnCode = 0
			else
				raiserror('UpdateEventStatus - Update Error: Query Failed',16,1)
		end
	return @ReturnCode
GO



--**************************************--
IF (OBJECT_ID('GetFacilitator') IS NOT NULL)
  DROP PROCEDURE GetFacilitator
go
create procedure GetFacilitator
(
	@FacilitatorID INT = NULL
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1
	
	if(@facilitatorID is null)
		raiserror('GetFacilitator - Required Parameter: @FaciltatorID',16,1)
	else
		begin			
		select FacilitatorID, Email, Password,Salt, Roles,FirstName,LastName,Title,Organization,City from Facilitator
			where FacilitatorID = @facilitatorID

			if @@ERROR = 0
				set @ReturnCode = 0
			else
				raiserror('GetFacilitator - Update Error: Query Failed',16,1)
		end
	return @ReturnCode
GO



--**************************************--
IF (OBJECT_ID('GetFacilitatorEvents') IS NOT NULL)
  DROP PROCEDURE GetFacilitatorEvents
go
create procedure GetFacilitatorEvents
(
	@FacilitatorID INT = NULL
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1
	
	if(@FacilitatorID is null)
		raiserror('GetFacilitatorEvents - Required Parameter: @FaciltatorID',16,1)
	else
		begin
			select EventKey,FacilitatorID, Location, Performer, NatureOfEvent,EventDate,EventBegin,EventEnd from EventDetails
			where FacilitatorID = @FacilitatorID
			order by EventDate desc, EventBegin desc

			if @@ERROR = 0
				set @ReturnCode = 0
			else
				raiserror('GetFacilitatorEvents - Update Error: Query Failed',16,1)
		end
	return @ReturnCode
GO

exec GetFacilitatorEvents 1


--**************************************--
IF (OBJECT_ID('CreateEvaluator') IS NOT NULL)
  DROP PROCEDURE CreateEvaluator
go
create procedure CreateEvaluator
(
	@EvaluatorID INT = null output
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1

	begin
		insert into Evaluator(Name,VotingCriteria)
		values('TempName','QualityOfPerformance')

		if @@ERROR = 0
			begin
				set @ReturnCode = 0
				select @EvaluatorID = @@IDENTITY
			end
		else
			raiserror('CreateEvaluator - insert Error: Query Failed',16,1)
		end
	return @ReturnCode	
go



--**************************************--
IF (OBJECT_ID('GetEvaluator') IS NOT NULL)
  DROP PROCEDURE GetEvaluator
go
create procedure GetEvaluator
(
	@EvaluatorID int = null
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1

	if(@EvaluatorID is null)
		 raiserror('GetEvaluator - Required Parameter: @EvaluatorID',16,1)
	else
		begin
			select EvaluatorID,Name,DateOfBirth,Sex,SchoolOrOrganization,City,VotingCriteria from Evaluator
			where EvaluatorID = @EvaluatorID 

			if @@ERROR = 0
				set @ReturnCode = 0
			else
				raiserror('GetEvaluator - Select Error: Query Failed',16,1)
		end
	return @ReturnCode				
GO



--**************************************--
IF (OBJECT_ID('GetEventEvaluators') IS NOT NULL)
  DROP PROCEDURE GetEventEvaluators
go
create procedure GetEventEvaluators --for event
(
	@EventKey nvarchar(5)
)
as
	declare @returnCode as int
	set @ReturnCode = 1
	
	if(@EventKey is null)
		 raiserror('GetEventEvaluators - Required Parameter: @EventKey',16,1)
	else
		begin
			select distinct Evaluator.EvaluatorID,Name, DateOfBirth,Sex,SchoolOrOrganization,City,VotingCriteria from Evaluator
			inner join EvaluativeData ed on 
			Evaluator.EvaluatorID = ed.EvaluatorID
			where ed.EventKey = @EventKey
			
			if @@ERROR = 0
				set @ReturnCode = 0
			else
				raiserror('GetEventEvaluators - Select Error: Query Failed',16,1)
		end
	return @ReturnCode				
GO



--**************************************--
IF (OBJECT_ID('GetHistoricalEvaluationData') IS NOT NULL)
  DROP PROCEDURE GetHistoricalEvaluationData
go
create procedure GetHistoricalEvaluationData
(
	@EventKey nvarchar(5) = null
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1
	
	if(@EventKey is null)
		raiserror('GetHistoricalEvaluationData - Required Parameter: @EventKey',16,1)
	else
		begin
			select EvaluatorID, Rating, TimeOfData 
			from EvaluativeData
			where @EventKey = EventKey

			if @@ERROR = 0
				set @ReturnCode = 0
			else
				raiserror('GetHistoricalEvaluationData - Select Error: Query Failed',16,1)
		end
	return @ReturnCode				
GO



--**************************************--
IF (OBJECT_ID('GetMostRecentEvaluativeData') IS NOT NULL)
  DROP PROCEDURE GetMostRecentEvaluativeData
go
create procedure GetMostRecentEvaluativeData
(
	@EventKey nvarchar(5) = null
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1
	
	if(@EventKey is null)
		raiserror('GetMostRecentEvaluativeData - Required Parameter: @EventKey',16,1)
	else
		begin
			select ed.EvaluatorID, ed.Rating, ed.TimeOfData
			from EvaluativeData ed
			left join EvaluativeData ev
			on ed.EvaluatorID = ev.EvaluatorID and ed.TimeOfData < ev.TimeOfData
			where ed.EventKey = @EventKey and ev.EvaluatorID is null


			if @@ERROR = 0
				set @ReturnCode = 0
			else
				raiserror('GetMostRecentEvaluativeData - Select Error: Query Failed',16,1)
		end
	return @ReturnCode				
GO



--**************************************--
IF (OBJECT_ID('GetEvaluatorEventData') IS NOT NULL)
  DROP PROCEDURE GetEvaluatorEventData
go
create procedure GetEvaluatorEventData
(
	@EventKey nvarchar(5) = null,
	@EvaluatorID int = null
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1
	
	if(@EventKey is null)
		raiserror('GetEvaluatorEventData - Required Parameter: @EventKey',16,1)
	if(@EvaluatorID is null)
		raiserror('GetEvaluatorEventData - Required Parameter: @EvaluatorID',16,1)
	else
		begin
			select EventKey,EvaluatorID,TimeOfData,Rating from EvaluativeData
			where @EventKey = EventKey and @EvaluatorID = EvaluatorID

			if @@ERROR = 0
				set @ReturnCode = 0
			else
				raiserror('GetEvaluatorEventData - Select Error: Query Failed',16,1)
		end
	return @ReturnCode				
GO




--**************************************--
IF (OBJECT_ID('GetAllEventData') IS NOT NULL)
  DROP PROCEDURE GetAllEventData
go
create procedure GetAllEventData
(
	@EventKey nvarchar(5) = null
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1

	if(@EventKey is null)
		raiserror('GetAllEventData - Required Parameter: @EventKey',16,1)
	else
		begin
			select EventKey,EvaluatorID,TimeOfData,Rating from EvaluativeData
			where EventKey = @EventKey-- and EvaluatorID = '11'						--temp added

			if @@ERROR = 0
				set @ReturnCode = 0
			else
				raiserror('GetAllEventData - Select Error: Query Failed',16,1)
		end
	return @ReturnCode				
GO



--**************************************--
IF (OBJECT_ID('GetEvent') IS NOT NULL)
  DROP PROCEDURE GetEvent
go
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
			select EventKey, FacilitatorID, Location, Performer,NatureOfEvent, EventDate, EventBegin, EventEnd from EventDetails
			where EventKey = @EventKey

			if @@ERROR = 0
				set @ReturnCode = 0
			else
				raiserror('GetEvent - Select Error: Query Failed',16,1)
			end
		return @ReturnCode	
go



--**************************************--
IF (OBJECT_ID('UpdateFacilitatorInfo') IS NOT NULL)
  DROP PROCEDURE UpdateFacilitatorInfo
go
create procedure UpdateFacilitatorInfo
(
	@ID int = null,
	@Email nvarchar(40) = null,
	@Password nvarchar(64) = null,
	@Salt nvarchar(10) = null,
	@Roles nvarchar(60) = null,
	@FirstName nvarchar(20) = null,
	@LastName nvarchar(20) = null,
	@Title nvarchar(20) = null,
	@Organization nvarchar(40) = null,
	@City nvarchar(40) = null
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1

	if(@ID is null)
		raiserror('UpdateFacilitatorInfo - Required Parameter: @ID',16,1)
	if(@Email is null)
		raiserror('UpdateFacilitatorInfo - Required Parameter: @Email',16,1)
	if(@Password is null)
		raiserror('UpdateFacilitatorInfo - Required Parameter: @Password',16,1)
	if(@Salt is null)
		raiserror('UpdateFacilitatorInfo - Required Parameter: @Salt',16,1)
	if(@Roles is null)
		raiserror('UpdateFacilitatorInfo - Required Parameter: @Roles',16,1)
	if(@FirstName is null)
		raiserror('UpdateFacilitatorInfo - Required Parameter: @FirstName',16,1)
	if(@LastName is null)
		raiserror('UpdateFacilitatorInfo - Required Parameter: @LastName',16,1)
	else
		begin
			update Facilitator
			set EMail = @Email, Password = @Password, Salt = @Salt, Roles = @Roles,
				FirstName = @FirstName, LastName = @LastName, Title = @Title,
				Organization = @Organization, City = @City
			where FacilitatorID = @ID

			if @@ERROR = 0
				set @ReturnCode = 0
			else
				raiserror('UpdateFacilitatorInfo - Update Error: Query Failed',16,1)
			end
		return @ReturnCode	
go


--**************************************--
--			Creating test Data			--
--**************************************--
execute CreateFacilitator 'admin','account','admin@gmail.com','admin','3dfd5cbdd931df72ff375bf1e7bda19feb2cb8975eac67e654b66d656f8c52c4','D/ydVF8=','Mr','NAIT BAIST','Edmonton, AB'
execute CreateFacilitator 'BAIST','Students','baist@gmail.com','admin','ac81acfd35332d177ace4dfd997236d666b54bff5ba4420c7cae6a5237b9174c','D/ydVF8=','Mr','NAIT BAIST','Edmonton, AB'
go
declare @evalID int
execute AddEvaluator @evalID output, 'Cody Jacob','08-12-2001','M','NAIT','Edmonton','Vote criteria?'
execute AddEvaluator @evalID output, 'Ben Waddell','05-10-2001','M','NAIT','Edmonton','Vote criteria?'
execute AddEvaluator @evalID output, 'Martin Sawicki','04-09-2001','M','NAIT','Edmonton','Vote criteria?'
go
execute CreateEvent 'AAAA',1,'Edmonton, AB, Canada','NAIT Baist Students','Building a website','4/20/2018'
go
--update start time of event to NOW
exec UpdateEventStatus 'AAAA', '4/20/2018 7:00:00 AM', '4/20/2018 8:00:00 AM'
go
--add evaluative data to table
exec AddEvaluationDataPoint 'AAAA',1,10,'4/20/2018 7:00:00 AM'
exec AddEvaluationDataPoint 'AAAA',1,9,'4/20/2018 7:00:30 AM'
exec AddEvaluationDataPoint 'AAAA',1,8,'4/20/2018 7:01:00 AM'
exec AddEvaluationDataPoint 'AAAA',1,7,'4/20/2018 7:01:30 AM'
exec AddEvaluationDataPoint 'AAAA',1,8,'4/20/2018 7:02:00 AM'
exec AddEvaluationDataPoint 'AAAA',1,7,'4/20/2018 7:03:00 AM'
exec AddEvaluationDataPoint 'AAAA',1,6,'4/20/2018 7:05:10 AM'
exec AddEvaluationDataPoint 'AAAA',1,5,'4/20/2018 7:05:20 AM'
exec AddEvaluationDataPoint 'AAAA',1,4,'4/20/2018 7:06:30 AM'
exec AddEvaluationDataPoint 'AAAA',1,3,'4/20/2018 7:06:50 AM'
exec AddEvaluationDataPoint 'AAAA',1,4,'4/20/2018 7:06:55 AM'
exec AddEvaluationDataPoint 'AAAA',1,5,'4/20/2018 7:10:00 AM'
exec AddEvaluationDataPoint 'AAAA',1,6,'4/20/2018 7:10:10 AM'
exec AddEvaluationDataPoint 'AAAA',1,7,'4/20/2018 7:11:00 AM'
exec AddEvaluationDataPoint 'AAAA',1,8,'4/20/2018 7:12:00 AM'
exec AddEvaluationDataPoint 'AAAA',1,7,'4/20/2018 7:13:00 AM'
exec AddEvaluationDataPoint 'AAAA',1,8,'4/20/2018 7:14:00 AM'
exec AddEvaluationDataPoint 'AAAA',1,4,'4/20/2018 7:16:00 AM'
exec AddEvaluationDataPoint 'AAAA',1,3,'4/20/2018 7:15:00 AM'
exec AddEvaluationDataPoint 'AAAA',1,4,'4/20/2018 7:30:00 AM'
exec AddEvaluationDataPoint 'AAAA',1,5,'4/20/2018 7:39:50 AM'

exec AddEvaluationDataPoint 'AAAA',2,9,'4/20/2018 7:10:00 AM'
exec AddEvaluationDataPoint 'AAAA',2,8,'4/20/2018 7:11:00 AM'
exec AddEvaluationDataPoint 'AAAA',2,9,'4/20/2018 7:12:00 AM'
exec AddEvaluationDataPoint 'AAAA',2,8,'4/20/2018 7:13:00 AM'
exec AddEvaluationDataPoint 'AAAA',2,7,'4/20/2018 7:16:00 AM'
exec AddEvaluationDataPoint 'AAAA',2,6,'4/20/2018 7:16:30 AM'
exec AddEvaluationDataPoint 'AAAA',2,5,'4/20/2018 7:18:00 AM'
exec AddEvaluationDataPoint 'AAAA',2,4,'4/20/2018 7:19:00 AM'
exec AddEvaluationDataPoint 'AAAA',2,2,'4/20/2018 7:20:00 AM'
exec AddEvaluationDataPoint 'AAAA',2,1,'4/20/2018 7:25:00 AM'
exec AddEvaluationDataPoint 'AAAA',2,2,'4/20/2018 7:30:00 AM'
exec AddEvaluationDataPoint 'AAAA',2,3,'4/20/2018 7:35:00 AM'
exec AddEvaluationDataPoint 'AAAA',2,4,'4/20/2018 7:36:00 AM'
exec AddEvaluationDataPoint 'AAAA',2,5,'4/20/2018 7:37:00 AM'
exec AddEvaluationDataPoint 'AAAA',2,6,'4/20/2018 7:37:10 AM'
exec AddEvaluationDataPoint 'AAAA',2,7,'4/20/2018 7:37:11 AM'
exec AddEvaluationDataPoint 'AAAA',2,6,'4/20/2018 7:37:12 AM'
exec AddEvaluationDataPoint 'AAAA',2,5,'4/20/2018 7:37:15 AM'
exec AddEvaluationDataPoint 'AAAA',2,5,'4/20/2018 7:37:30 AM'
exec AddEvaluationDataPoint 'AAAA',2,4,'4/20/2018 7:38:00 AM'
exec AddEvaluationDataPoint 'AAAA',2,2,'4/20/2018 7:38:20 AM'
exec AddEvaluationDataPoint 'AAAA',2,1,'4/20/2018 7:39:00 AM'
exec AddEvaluationDataPoint 'AAAA',2,2,'4/20/2018 7:40:00 AM'
exec AddEvaluationDataPoint 'AAAA',2,3,'4/20/2018 7:50:00 AM'

exec AddEvaluationDataPoint 'AAAA',3,5,'4/20/2018 7:5:00 AM'
exec AddEvaluationDataPoint 'AAAA',3,3,'4/20/2018 7:10:00 AM'
exec AddEvaluationDataPoint 'AAAA',3,2,'4/20/2018 7:10:20 AM'
exec AddEvaluationDataPoint 'AAAA',3,1,'4/20/2018 7:15:00 AM'
exec AddEvaluationDataPoint 'AAAA',3,2,'4/20/2018 7:16:00 AM'
exec AddEvaluationDataPoint 'AAAA',3,3,'4/20/2018 7:17:00 AM'
exec AddEvaluationDataPoint 'AAAA',3,4,'4/20/2018 7:18:00 AM'
exec AddEvaluationDataPoint 'AAAA',3,5,'4/20/2018 7:19:00 AM'
exec AddEvaluationDataPoint 'AAAA',3,4,'4/20/2018 7:20:00 AM'
exec AddEvaluationDataPoint 'AAAA',3,5,'4/20/2018 7:22:00 AM'
exec AddEvaluationDataPoint 'AAAA',3,6,'4/20/2018 7:33:00 AM'
exec AddEvaluationDataPoint 'AAAA',3,7,'4/20/2018 7:35:00 AM'
exec AddEvaluationDataPoint 'AAAA',3,6,'4/20/2018 7:36:00 AM'
exec AddEvaluationDataPoint 'AAAA',3,5,'4/20/2018 7:37:00 AM'
exec AddEvaluationDataPoint 'AAAA',3,4,'4/20/2018 7:38:00 AM'
exec AddEvaluationDataPoint 'AAAA',3,3,'4/20/2018 7:39:00 AM'
exec AddEvaluationDataPoint 'AAAA',3,2,'4/20/2018 7:40:00 AM'
exec AddEvaluationDataPoint 'AAAA',3,3,'4/20/2018 7:41:00 AM'
exec AddEvaluationDataPoint 'AAAA',3,4,'4/20/2018 7:45:00 AM'
exec AddEvaluationDataPoint 'AAAA',3,5,'4/20/2018 7:50:00 AM'
exec AddEvaluationDataPoint 'AAAA',3,4,'4/20/2018 7:51:00 AM'
exec AddEvaluationDataPoint 'AAAA',3,5,'4/20/2018 7:52:00 AM'
exec AddEvaluationDataPoint 'AAAA',3,6,'4/20/2018 7:55:00 AM'
exec AddEvaluationDataPoint 'AAAA',3,7,'4/20/2018 7:56:00 AM'
go






--**************************************--
--			Select Statements			--
--**************************************--

select * from EvaluativeData
select * from Evaluator
select * from EventDetails
select * from Facilitator

delete from EventDetails where EventKey = 'C2PK'
