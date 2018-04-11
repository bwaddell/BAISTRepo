
--Azure database-----
--DataBase: ContinUIDB
--adminName: cody
--password: Jacob$17

--WEBBAIST server
--database: ContinUI-DB
--login is windows auth



-- create login for the web server to access data base server
use ContinUIDB
sp_help

drop table EvaluativeData
drop table Evaluator
drop table EventDetails
drop table Facilitator

drop proc AddEvaluationDataPoint
drop proc AddEvaluator
drop proc CreateEvaluator
drop proc CreateEvent
drop proc CreateFacilitator
drop proc GetAllEventData
drop proc GetEvaluator
drop proc GetEvaluatorEventData
drop proc GetEvent
drop proc GetEventEvaluators
drop proc GetFacilitator
drop proc GetFacilitatorInfo
drop proc GetFacilitatorEvents
drop proc GetHistoricalEvaluationData
drop proc GetMostRecentEvaluativeData
drop proc UpdateEventStatus
drop proc UpdateFacilitatorInfo

select * from EvaluativeData
select * from Evaluator
select * from EventDetails
select * from Facilitator

--delete from EvaluativeData
--delete from Facilitator
--delete from Evaluator
--delete from EventDetails

create user aspnet for login [NAIT\WEBBAIST$] 

grant execute on AddEvaluationDataPoint to aspnet
grant execute on AddEvaluator to aspnet
grant execute on CreateEvaluator to aspnet
grant execute on CreateEvent to aspnet
grant execute on CreateFacilitator to aspnet
grant execute on GetAllEventData to aspnet
grant execute on GetEvaluator to aspnet
grant execute on GetEvaluatorEventData to aspnet
grant execute on GetEvent to aspnet
grant execute on GetEventEvaluators to aspnet
grant execute on GetFacilitator to aspnet
grant execute on GetFacilitatorEvents to aspnet
grant execute on GetHistoricalEvaluationData to aspnet
grant execute on GetMostRecentEvaluativeData to aspnet
grant execute on UpdateEventStatus to aspnet
grant execute on UpdateFacilitatorInfo to aspnet



--****************************************--
--Tables---
--****************************************--

use ContinUIDB
GO



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
	Location nvarchar(30) not null,
	Performer nvarchar(20) not null,
	NatureOfEvent nvarchar(20) not null,
	EventDate nvarchar(20) not null,
	EventBegin nvarchar(20) default '01-01-1800 12:00:00',
	EventEnd nvarchar(20) default '01-01-1800 12:00:00'
)
alter table EventDetails
	add constraint PK_EventDetails primary key (EventKey),
		constraint FK_EventDetails foreign key (FacilitatorID) references Facilitator(FacilitatorID)
Go


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


--drop procedure AddEvaluationDataPoint
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

--drop procedure AddEvaluator
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


--drop procedure CreateFacilitator
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


--drop procedure CreateEvent
go
create procedure CreateEvent
(
	@EventKey nvarchar(5) = null,
	@Facilitator int = null,
	@Location nvarchar(30) = null,
	@Performer nvarchar(20) = null,
	@NatureOfEvent nvarchar(20) = null,
	@EventDate nvarchar(20) = null
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




--drop procedure UpdateEventStatus
go
create procedure UpdateEventStatus
(
	@EventKey nvarchar(5) = null,
	@EventStart datetime = null,
	@EventFinish datetime = null
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


--drop procedure GetFacilitatorInfo												dropped as unnessisary
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

--drop proc GetFacilitator
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


--drop proc GetFacilitatorEvents
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

			if @@ERROR = 0
				set @ReturnCode = 0
			else
				raiserror('GetFacilitatorEvents - Update Error: Query Failed',16,1)
		end
	return @ReturnCode
GO
--exec GetFacilitatorEvents 1


--drop procedure CreateEvaluator
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
		values('TemporaryName','QualityOfPerformance')

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


--drop procedure GetEvaluator
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

--drop procedure GetEventEvaluators
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
--exec GetEventEvaluators 'aaaa'

--drop procedure GetHistoricalEvaluationData
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

--drop procedure GetMostRecentEvaluativeData
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

--drop procedure GetEvaluatorEventData
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

--exec GetEvaluatorEventData 'abcd',8
go

--drop procedure GetAllEventData
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

--drop procedure GetEvent
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
--exec GetEvent 'aaaa'

--drop procedure UpdateFacilitatorInfo
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

--execute GetAllEventData 'aaaa'



--declare @evalID INT
--execute CreateEvaluator @evalID output
--select @evalID
--go





--****************************************--
--Creating test Data---
--****************************************--

--***********************************************************************************

declare @evalID int
execute AddEvaluator @evalID output, 'Cody Jacob','08-12-2001','M','NAIT','Edmonton','Vote criteria?'
execute AddEvaluator @evalID output, 'Ben Waddell','05-10-2001','M','NAIT','Edmonton','Vote criteria?'
execute AddEvaluator @evalID output, 'Martin Sawicki','04-09-2001','M','NAIT','Edmonton','Vote criteria?'
--select * from Evaluator
--select @evalID
go
execute CreateFacilitator 'admin','User','admin@gmail.com','admin','3dfd5cbdd931df72ff375bf1e7bda19feb2cb8975eac67e654b66d656f8c52c4','D/ydVF8=','SA','NAIT BAIST','Edmonton'
go

execute UpdateFacilitatorInfo 1, 'admin@gmail.com','3dfd5cbdd931df72ff375bf1e7bda19feb2cb8975eac67e654b66d656f8c52c4','D/ydVF8=','admin','FName','LName','SysAdmin','NAIT BAIST','Edmonton'
go



insert into EventDetails values( 'aaaa',1,'Edmonton NAIT','Bruce Wayne','Singing in the Rain', '2018-04-09', null,null)
select * from EventDetails
--declare @date date
--set @date = GETDATE()
--select @date
--execute CreateEvent 'aaaa',1,'Edmonton NAIT','Bruce Wayne','Singing in the Rain', @date
--go


--update start time of event to NOW
declare @date datetime
set @date = GETDATE()
exec UpdateEventStatus 'aaaa', @date, null
go

declare @date datetime
set @date = GETDATE()
set @date = DATEADD(hour,-6,@date)

exec AddEvaluationDataPoint 'aaaa',1,10,@date
set @date = DATEADD(second,10,@date)
exec AddEvaluationDataPoint 'aaaa',1,9,@date
set @date = DATEADD(second,15,@date)
exec AddEvaluationDataPoint 'aaaa',1,8,@date
set @date = DATEADD(second,20,@date)
exec AddEvaluationDataPoint 'aaaa',1,7,@date
set @date = DATEADD(second,30,@date)
exec AddEvaluationDataPoint 'aaaa',1,8,@date
set @date = DATEADD(second,50,@date)
exec AddEvaluationDataPoint 'aaaa',1,7,@date
set @date = DATEADD(second,100,@date)
exec AddEvaluationDataPoint 'aaaa',1,6,@date
set @date = DATEADD(second,130,@date)
exec AddEvaluationDataPoint 'aaaa',1,5,@date
set @date = DATEADD(second,150,@date)
exec AddEvaluationDataPoint 'aaaa',1,4,@date
set @date = DATEADD(second,170,@date)
exec AddEvaluationDataPoint 'aaaa',1,3,@date
set @date = DATEADD(second,200,@date)
exec AddEvaluationDataPoint 'aaaa',1,4,@date
set @date = DATEADD(second,250,@date)
exec AddEvaluationDataPoint 'aaaa',1,5,@date
set @date = DATEADD(second,260,@date)
exec AddEvaluationDataPoint 'aaaa',1,6,@date
set @date = DATEADD(second,270,@date)
exec AddEvaluationDataPoint 'aaaa',1,7,@date
set @date = DATEADD(second,300,@date)
exec AddEvaluationDataPoint 'aaaa',1,8,@date
set @date = DATEADD(second,400,@date)
exec AddEvaluationDataPoint 'aaaa',1,7,@date
set @date = DATEADD(second,450,@date)
exec AddEvaluationDataPoint 'aaaa',1,8,@date
set @date = DATEADD(second,500,@date)
exec AddEvaluationDataPoint 'aaaa',1,7,@date
set @date = DATEADD(second,520,@date)
exec AddEvaluationDataPoint 'aaaa',1,6,@date
set @date = DATEADD(second,530,@date)
exec AddEvaluationDataPoint 'aaaa',1,5,@date
set @date = DATEADD(second,550,@date)
exec AddEvaluationDataPoint 'aaaa',1,4,@date
set @date = DATEADD(second,600,@date)
exec AddEvaluationDataPoint 'aaaa',1,3,@date
set @date = DATEADD(second,700,@date)
exec AddEvaluationDataPoint 'aaaa',1,4,@date
set @date = DATEADD(second,720,@date)
exec AddEvaluationDataPoint 'aaaa',1,5,@date

set @date = GETDATE()
set @date = DATEADD(hour,-6,@date)

exec AddEvaluationDataPoint 'aaaa',2,9,@date
set @date = DATEADD(second,12,@date)
exec AddEvaluationDataPoint 'aaaa',2,8,@date
set @date = DATEADD(second,16,@date)
exec AddEvaluationDataPoint 'aaaa',2,9,@date
set @date = DATEADD(second,22,@date)
exec AddEvaluationDataPoint 'aaaa',2,8,@date
set @date = DATEADD(second,32,@date)
exec AddEvaluationDataPoint 'aaaa',2,7,@date
set @date = DATEADD(second,52,@date)
exec AddEvaluationDataPoint 'aaaa',2,6,@date
set @date = DATEADD(second,102,@date)
exec AddEvaluationDataPoint 'aaaa',2,5,@date
set @date = DATEADD(second,132,@date)
exec AddEvaluationDataPoint 'aaaa',2,4,@date
set @date = DATEADD(second,152,@date)
exec AddEvaluationDataPoint 'aaaa',2,2,@date
set @date = DATEADD(second,172,@date)
exec AddEvaluationDataPoint 'aaaa',2,1,@date
set @date = DATEADD(second,202,@date)
exec AddEvaluationDataPoint 'aaaa',2,2,@date
set @date = DATEADD(second,252,@date)
exec AddEvaluationDataPoint 'aaaa',2,3,@date
set @date = DATEADD(second,250,@date)
exec AddEvaluationDataPoint 'aaaa',2,4,@date
set @date = DATEADD(second,12,@date)
exec AddEvaluationDataPoint 'aaaa',2,5,@date
set @date = DATEADD(second,16,@date)
exec AddEvaluationDataPoint 'aaaa',2,6,@date
set @date = DATEADD(second,22,@date)
exec AddEvaluationDataPoint 'aaaa',2,7,@date
set @date = DATEADD(second,32,@date)
exec AddEvaluationDataPoint 'aaaa',2,6,@date
set @date = DATEADD(second,52,@date)
exec AddEvaluationDataPoint 'aaaa',2,5,@date
set @date = DATEADD(second,102,@date)
exec AddEvaluationDataPoint 'aaaa',2,5,@date
set @date = DATEADD(second,132,@date)
exec AddEvaluationDataPoint 'aaaa',2,4,@date
set @date = DATEADD(second,152,@date)
exec AddEvaluationDataPoint 'aaaa',2,2,@date
set @date = DATEADD(second,172,@date)
exec AddEvaluationDataPoint 'aaaa',2,1,@date
set @date = DATEADD(second,202,@date)
exec AddEvaluationDataPoint 'aaaa',2,2,@date
set @date = DATEADD(second,252,@date)
exec AddEvaluationDataPoint 'aaaa',2,3,@date

set @date = GETDATE()
set @date = DATEADD(hour,-6,@date)

exec AddEvaluationDataPoint 'aaaa',3,5,@date
set @date = DATEADD(second,15,@date)
exec AddEvaluationDataPoint 'aaaa',3,3,@date
set @date = DATEADD(second,17,@date)
exec AddEvaluationDataPoint 'aaaa',3,2,@date
set @date = DATEADD(second,25,@date)
exec AddEvaluationDataPoint 'aaaa',3,1,@date
set @date = DATEADD(second,36,@date)
exec AddEvaluationDataPoint 'aaaa',3,2,@date
set @date = DATEADD(second,58,@date)
exec AddEvaluationDataPoint 'aaaa',3,3,@date
set @date = DATEADD(second,106,@date)
exec AddEvaluationDataPoint 'aaaa',3,4,@date
set @date = DATEADD(second,136,@date)
exec AddEvaluationDataPoint 'aaaa',3,5,@date
set @date = DATEADD(second,156,@date)
exec AddEvaluationDataPoint 'aaaa',3,4,@date
set @date = DATEADD(second,176,@date)
exec AddEvaluationDataPoint 'aaaa',3,5,@date
set @date = DATEADD(second,206,@date)
exec AddEvaluationDataPoint 'aaaa',3,6,@date
set @date = DATEADD(second,256,@date)
exec AddEvaluationDataPoint 'aaaa',3,7,@date
set @date = DATEADD(second,256,@date)
exec AddEvaluationDataPoint 'aaaa',3,6,@date
set @date = DATEADD(second,15,@date)
exec AddEvaluationDataPoint 'aaaa',3,5,@date
set @date = DATEADD(second,17,@date)
exec AddEvaluationDataPoint 'aaaa',3,4,@date
set @date = DATEADD(second,25,@date)
exec AddEvaluationDataPoint 'aaaa',3,3,@date
set @date = DATEADD(second,36,@date)
exec AddEvaluationDataPoint 'aaaa',3,2,@date
set @date = DATEADD(second,58,@date)
exec AddEvaluationDataPoint 'aaaa',3,3,@date
set @date = DATEADD(second,106,@date)
exec AddEvaluationDataPoint 'aaaa',3,4,@date
set @date = DATEADD(second,136,@date)
exec AddEvaluationDataPoint 'aaaa',3,5,@date
set @date = DATEADD(second,156,@date)
exec AddEvaluationDataPoint 'aaaa',3,4,@date
set @date = DATEADD(second,176,@date)
exec AddEvaluationDataPoint 'aaaa',3,5,@date
set @date = DATEADD(second,206,@date)
exec AddEvaluationDataPoint 'aaaa',3,6,@date
set @date = DATEADD(second,256,@date)
exec AddEvaluationDataPoint 'aaaa',3,7,@date

go


delete from EvaluativeData
where EvaluatorID = 4

select * from EvaluativeData
select * from Facilitator


