
--Azure database-----
--DataBase: ContinUIDB
--adminName: cody
--password: Jacob$17

--WEBBAIST server
--database: ContinUI-DB
--login is windows auth



-- create login for the web server to access data base server
create user aspnet for login [NAIT\WEBBAIST$] 

grant execute on AddEvaluationDataPoint to aspnet
grant execute on AddEvaluator to aspnet
grant execute on CreateEvent to aspnet
grant execute on CreateEvaluator to aspnet
grant execute on CreateFacilitator to aspnet
grant execute on GetEvent to aspnet
grant execute on GetHistoricalEvaluationData to aspnet
grant execute on GetMostRecentEvaluativeData to aspnet
grant execute on UpdateEventStatus to aspnet


select * from EvaluativeData
select * from Evaluator
select * from EventDetails
select * from Facilitator




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
declare @evalID int
execute AddEvaluator @evalID output, 'Cody Jacob','08-12-2001','M','NAIT','Edmonton','Vote criteria?'
execute AddEvaluator @evalID output, 'Ben Waddell','05-10-2001','M','NAIT','Edmonton','Vote criteria?'
execute AddEvaluator @evalID output, 'Martin Sawicki','04-09-2001','M','NAIT','Edmonton','Vote criteria?'
select * from Evaluator
select @evalID
go



--drop procedure CreateFacilitator
go
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

--drop procedure CreateEvent
go
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

execute CreateEvent 'ABCD',1,'Edmonton NAIT','Bruce Wayne','Sing', '03-03-2018'
go


--drop procedure UpdateEventStatus
go
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
			select ed.EvaluatorID, ed.Rating
			from EvaluativeData ed
			inner join (select max(TimeOfData) as LatestData
						from EvaluativeData) evda  
			on @EventKey = EventKey and ed.TimeOfData = evda.LatestData


			if @@ERROR = 0
				set @ReturnCode = 0
			else
				raiserror('GetMostRecentEvaluativeData - Select Error: Query Failed',16,1)
		end
	return @ReturnCode				
GO

execute GetMostRecentEvaluativeData 'C7UT'


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

declare @evalID INT
execute CreateEvaluator @evalID output
select @evalID
go


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
			select * from EventDetails
			where EventKey = @EventKey

			if @@ERROR = 0
				set @ReturnCode = 0
			else
				raiserror('GetEvent - Select Error: Query Failed',16,1)
			end
		return @ReturnCode	
go