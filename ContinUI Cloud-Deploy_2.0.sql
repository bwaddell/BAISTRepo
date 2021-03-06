--ContinUI web site

--	Instructions: --
-- check that the database name below is correct then execute entire script

create database Continui

--IF(db_id(N'Continui') IS NULL)	-- create a new database
--	Select 'yes'--CREATE DATABASE Continui
--go
use Continui		--use the database created
go

SET DATEFORMAT dmy
select * from EvaluativeData
select * from EventDetails
--******************************************--
--					Tables					--
--******************************************--
drop table QuestionResponse
drop table CustomQuestion
drop table EvaluativeData
drop table Evaluator
drop table EventDetails
drop table Facilitator


create table Facilitator
(
	FacilitatorID int unique identity(1,1) not null,
	Email nvarchar(80) not null,
	Password nvarchar(64) not null,
	Salt nvarchar(10) not null,
	Roles nvarchar(60) not null,
	FirstName nvarchar(40) not null,
	LastName nvarchar(40) not null,
	Title nvarchar(40) null,
	Organization nvarchar(80) null,
	City nvarchar(80) null
)
alter table Facilitator
	add constraint PK_Facilitator primary key (FacilitatorID)
Go

create table EventDetails
(
	EventID int unique identity(1,1) not null,
	EventKey nvarchar(5) not null,
	FacilitatorID int not null,
	Location nvarchar(100) not null,											
	Performer nvarchar(100) not null,											
	NatureOfEvent nvarchar(100) not null,										
	EventDate nvarchar(25) not null,
	EventBegin nvarchar(25) default '1800-01-01 12:00:00 PM',				
	EventEnd nvarchar(25) default '1800-01-01 12:00:00 PM',
	OpeningMessage nvarchar(4000) default '',
	ClosingMessage nvarchar(4000) default '',
	VotingCrit nvarchar(256) default 'Overall'
)
alter table EventDetails
	add constraint PK_EventDetails primary key (EventID),
		constraint FK_EventDetails foreign key (FacilitatorID) references Facilitator(FacilitatorID)
Go

create table Evaluator
(
	EvaluatorID int unique identity(1,1) not null,
	Name nvarchar(80) not null,
	VotingCriteria nvarchar(40) not null 
)
alter table Evaluator
	add constraint PK_Evaluator primary key (EvaluatorID) 
Go

create table EvaluativeData
(
	EventID int not null,
	EvaluatorID int not null,
	TimeOfData datetime not null,
	Rating int not null
)
alter table EvaluativeData
	add constraint PK_EvaluativeData primary key (TimeOfData, EventID, EvaluatorID),
		constraint FK_EvaluativeData_Event foreign key (EventID) references EventDetails(EventID),
		constraint FK_EvaluativeData_Evaluator foreign key (EvaluatorID) references Evaluator(EvaluatorID),
		constraint CK_EvaluativeData check (Rating > 0)
GO

CREATE TABLE CustomQuestion
(
	QID INT UNIQUE IDENTITY (1,1) NOT NULL,
	EventID INT NOT NULL,
	Question NVARCHAR(1024) NOT NULL
)
ALTER TABLE CustomQuestion
	ADD CONSTRAINT PK_CustomQuestion PRIMARY KEY (QID),
		CONSTRAINT FK_CustomQuestion_Event FOREIGN KEY (EventID) REFERENCES EventDetails(EventID)

CREATE TABLE QuestionResponse
(
	QID INT NOT NULL,
	EvaluatorID INT NOT NULL,
	Response NVARCHAR(1024) NOT NULL
)
ALTER TABLE QuestionResponse
	ADD CONSTRAINT PK_Response PRIMARY KEY (QID, EvaluatorID),
		CONSTRAINT FK_QID FOREIGN KEY (QID) REFERENCES CustomQuestion(QID),
		CONSTRAINT FK_Response_Evaluator FOREIGN KEY (EvaluatorID) REFERENCES Evaluator(EvaluatorID)




--****************************************--
--Stored Procedures---
--****************************************--

IF (OBJECT_ID('AddEvaluationDataPoint') IS NOT NULL)
  DROP PROCEDURE AddEvaluationDataPoint
go
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
	if(@Evaluator is null)
		raiserror('AddEvaluationDataPoint - Required Parameter: @Evaluator',16,1)
	if(@Rating is null)
		raiserror('AddEvaluationDataPoint - Required Parameter: @Rating',16,1)
	if(@TimeOfData is null)
		raiserror('AddEvaluationDataPoint - Required Parameter: @TimeOfData',16,1)
	else
		begin
			insert into EvaluativeData (EventID, EvaluatorID, TimeOfData, Rating)
			values (@Event, @Evaluator, @TimeOfData, @Rating)

			if @@ERROR = 0
				set @ReturnCode = 0
			else
				raiserror('AddEvaluationDataPoint - Insert Error: Query Failed',16,1)
		end
	return @ReturnCode
GO

SELECT * FROM EventDetails
SELECT * FROM Evaluator

EXEC AddEvaluationDataPoint 1013,1010,0,'1800-01-01 12:00:00 PM'

--**************************************--
IF (OBJECT_ID('AddEvaluator') IS NOT NULL)
  DROP PROCEDURE AddEvaluator
go
create procedure AddEvaluator
(
	@EvaluatorID int = null output,
	@Name nvarchar(80) = null,
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
			values (@Name, @VotingCriteria)
			
			
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
	@FirstName nvarchar(40) = null,
	@LastName nvarchar(40) = null,
	@Email nvarchar(80) = null,
	@Role nvarchar(60) = null,
	@Password nvarchar(64) = null,
	@Salt nvarchar(10) = null,
	@Title nvarchar(40) = null,
	@Organization nvarchar(80) = null,
	@City nvarchar(80) = null
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


IF (OBJECT_ID('DeleteFacilitator') IS NOT NULL)
  DROP PROCEDURE DeleteFacilitator
go
create procedure DeleteFacilitator
(
	@FacID int = null
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1

	if(@FacID is null)
		raiserror('DeleteFacilitator - Required Parameter: @FacID',16,1)
	else
		begin
			DELETE FROM Facilitator 
			WHERE FacilitatorID = @FacID

			if @@ERROR = 0
				set @ReturnCode = 0
			else
				raiserror('DeleteFacilitator - Insert Error: Query Failed',16,1)
		end
	return @ReturnCode
GO

--**************************************--

IF (OBJECT_ID('CreateEvent') IS NOT NULL)
  DROP PROCEDURE CreateEvent
go
create procedure CreateEvent
(
	@EventID INT = NULL OUTPUT,
	@EventKey nvarchar(5) = null,
	@Facilitator int = null,
	@Location nvarchar(100) = null,
	@Performer nvarchar(100) = null,
	@NatureOfEvent nvarchar(100) = null,
	@EventDate nvarchar(25) = null,
	@OpenMsg NVARCHAR(4000) = '',
	@CloseMsg NVARCHAR(4000) = '',
	@VotingCrit NVARCHAR(256) = 'Overall'
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
	if(@OpenMsg is null)
		raiserror('CreateEvent - Required Parameter: @OpenMsg',16,1)
	if(@CloseMsg is null)
		raiserror('CreateEvent - Required Parameter: @CloseMsg',16,1)
	if(@VotingCrit is null)
		raiserror('CreateEvent - Required Parameter: @VotingCrit',16,1)
	else
		begin
			insert into EventDetails(EventKey,FacilitatorID,Location,Performer,NatureOfEvent,EventDate,OpeningMessage,ClosingMessage,VotingCrit)
			values (@EventKey, @Facilitator, @Location, @Performer, @NatureOfEvent, @EventDate, @OpenMsg, @CloseMsg, @VotingCrit)

			if @@ERROR = 0
				begin
					set @ReturnCode = 0
					select @EventID = @@IDENTITY
				end
			else
				raiserror('CreateEvent - Insert Error: Query Failed',16,1)
			end
		return @ReturnCode
GO

IF (OBJECT_ID('UpdateEvent') IS NOT NULL)
  DROP PROCEDURE UpdateEvent
go
create procedure UpdateEvent
(
	@EventID INT = null,
	@EventKey nvarchar(5) = null,
	@Facilitator int = null,
	@Location nvarchar(100) = null,
	@Performer nvarchar(100) = null,
	@NatureOfEvent nvarchar(100) = null,
	@EventDate nvarchar(25) = null,
	@OpenMsg NVARCHAR(4000) = '',
	@CloseMsg NVARCHAR(4000) = '',
	@VotingCrit NVARCHAR(256) = 'Overall'
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1

	SET DATEFORMAT mdy;

	if(@EventID is null)
		raiserror('UpdateEvent - Required Parameter: @EventID',16,1)
	if(@EventKey is null)
		raiserror('UpdateEvent - Required Parameter: @EventKey',16,1)
	if(@Facilitator is null)
		raiserror('UpdateEvent - Required Parameter: @Facilitator',16,1)
	if(@Location is null)
		raiserror('UpdateEvent - Required Parameter: @Location',16,1)
	if(@Performer is null)
		raiserror('UpdateEvent - Required Parameter: @Performer',16,1)
	if(@NatureOfEvent is null)
		raiserror('UpdateEvent - Required Parameter: @NatureOfEvent',16,1)
	if(@EventDate is null)
		raiserror('UpdateEvent - Required Parameter: @EventDate',16,1)
	if(@OpenMsg is null)
		raiserror('UpdateEvent - Required Parameter: @OpenMsg',16,1)
	if(@CloseMsg is null)
		raiserror('UpdateEvent - Required Parameter: @CloseMsg',16,1)
	if(@VotingCrit is null)
		raiserror('UpdateEvent - Required Parameter: @VotingCrit',16,1)
	else
		begin
			UPDATE EventDetails
			SET EventKey = @EventKey, FacilitatorID = @Facilitator, Location = @Location,
				Performer = @Performer, NatureOfEvent = @NatureOfEvent, EventDate = @EventDate,
				OpeningMessage = @OpenMsg, ClosingMessage = @CloseMsg, VotingCrit = @VotingCrit
			WHERE EventID = @EventID

			if @@ERROR = 0
				begin
					set @ReturnCode = 0
				end
			else
				raiserror('UpdateEvent - Insert Error: Query Failed',16,1)
			end
		return @ReturnCode
GO


IF (OBJECT_ID('CreateQuestion') IS NOT NULL)
  DROP PROCEDURE CreateQuestion
go
create procedure CreateQuestion
(
	@EventID int = null,
	@Question NVARCHAR(1024) = NULL
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1

	if(@EventID is null)
		raiserror('CreateQuestion - Required Parameter: @EventKey',16,1)
	if(@Question is null)
		raiserror('CreateQuestion - Required Parameter: @Question',16,1)
	else
		begin
			insert into CustomQuestion(EventID,Question)
			values (@EventID, @Question)

			if @@ERROR = 0
				set @ReturnCode = 0
			else
				raiserror('CreateQuestion - Insert Error: Query Failed',16,1)
			end
		return @ReturnCode
GO


IF (OBJECT_ID('GetQuestions') IS NOT NULL)
  DROP PROCEDURE GetQuestions
go
create procedure GetQuestions
(
	@EventID int = null
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1

	if(@EventID is null)
		raiserror('GetQuestions - Required Parameter: @EventKey',16,1)
	else
		begin
			SELECT QID, EventiD, Question FROM CustomQuestion
			WHERE EventID = @EventID

			if @@ERROR = 0
				set @ReturnCode = 0
			else
				raiserror('GetQuestions - SELECT Error: Query Failed',16,1)
			end
		return @ReturnCode
GO


IF (OBJECT_ID('DeleteQuestion') IS NOT NULL)
  DROP PROCEDURE DeleteQuestion
go
create procedure DeleteQuestion
(
	@QID int = null
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1

	if(@QID is null)
		raiserror('DeleteQuestions - Required Parameter: @QID',16,1)
	else
		begin
			DELETE FROM QuestionResponse
			WHERE QID = @QID

			DELETE FROM CustomQuestion
			WHERE QID = @QID

			if @@ERROR = 0
				set @ReturnCode = 0
			else
				raiserror('DeleteQuestion - DELETE Error: Query Failed',16,1)
			end
		return @ReturnCode
GO


IF (OBJECT_ID('AnswerQuestion') IS NOT NULL)
  DROP PROCEDURE AnswerQuestion
go
create procedure AnswerQuestion
(
	@QID int = null,
	@EvaluatorID INT = NULL,
	@Response NVARCHAR(1024) = NULL
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1

	if(@QID is null)
		raiserror('AnswerQuestion - Required Parameter: @QID',16,1)
	if(@EvaluatorID is null)
		raiserror('AnswerQuestion - Required Parameter: @EvaluatorID',16,1)
	if(@Response is null)
		raiserror('AnswerQuestion - Required Parameter: @Response',16,1)
	else
		begin
			insert into QuestionResponse (QID, EvaluatorID, Response)
			values (@QID,@EvaluatorID,@Response)

			if @@ERROR = 0
				set @ReturnCode = 0
			else
				raiserror('AnswerQuestion - Insert Error: Query Failed',16,1)
			end
		return @ReturnCode
GO

IF (OBJECT_ID('GetResponse') IS NOT NULL)
  DROP PROCEDURE GetResponse
go
create procedure GetResponse
(
	@QID int = null,
	@EvaluatorID int = null
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1

	if(@QID is null)
		raiserror('GetResponse - Required Parameter: @QID',16,1)
	if(@EvaluatorID is null)
		raiserror('GetResponse - Required Parameter: @EvaluatorID',16,1)
	else
		begin
			SELECT QID, EvaluatorID, Response FROM  QuestionResponse
			WHERE QID = @QID AND EvaluatorID = @EvaluatorID

			if @@ERROR = 0
				set @ReturnCode = 0
			else
				raiserror('GetResponse - SELECT Error: Query Failed',16,1)
			end
		return @ReturnCode
GO


IF (OBJECT_ID('DeleteEvent') IS NOT NULL)
  DROP PROCEDURE DeleteEvent
go
create procedure DeleteEvent
(
	@EventID int = null
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1

	SET DATEFORMAT mdy;

	if(@EventID is null)
		raiserror('DeleteEvent - Required Parameter: @EventID',16,1)
	else
		begin
			DELETE FROM EventDetails
			WHERE EventID = @EventID

			if @@ERROR = 0
				set @ReturnCode = 0
			else
				raiserror('DeleteEvent - Delete Error: Query Failed',16,1)
			end
		return @ReturnCode
GO


--**************************************--
IF (OBJECT_ID('UpdateEventStatus') IS NOT NULL)
  DROP PROCEDURE UpdateEventStatus
go
create procedure UpdateEventStatus
(
	@EventID int = null,
	@EventKey NVARCHAR(5) = NULL,
	@EventStart nvarchar(25) = '1800-01-01 12:00:00 PM',
	@EventFinish nvarchar(25) = '1800-01-01 12:00:00 PM'
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1
	if(@EventID is null)
		raiserror('UpdateEventStatus - Required Parameter: @EventID',16,1)
	if(@EventKey is null)
		raiserror('UpdateEventStatus - Required Parameter: @EventKey',16,1)
	else
		begin
			update EventDetails
			set EventBegin = @EventStart, EventEnd = @EventFinish, EventKey = @EventKey
			where EventID = @EventID
			if @@ERROR = 0
				set @ReturnCode = 0
			else
				raiserror('UpdateEventStatus - Update Error: Query Failed',16,1)
		end
	return @ReturnCode
GO

--SELECT * FROM EventDetails
--EXEC UpdateEventStatus 12, 'AV9E'

--**************************************--
IF (OBJECT_ID('GetFacilitatorInfo') IS NOT NULL)
  DROP PROCEDURE GetFacilitatorInfo										
go
create procedure GetFacilitatorInfo
(
	@Email nvarchar(80) = null
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
			select EventID,EventKey,FacilitatorID,Location,Performer,NatureOfEvent,EventDate,EventBegin,EventEnd from EventDetails
			where FacilitatorID = @FacilitatorID
			order by EventDate desc, EventBegin desc

			if @@ERROR = 0
				set @ReturnCode = 0
			else
				raiserror('GetFacilitatorEvents - Update Error: Query Failed',16,1)
		end
	return @ReturnCode
GO

--**************************************--
IF (OBJECT_ID('CreateEvaluator') IS NOT NULL)
  DROP PROCEDURE CreateEvaluator
go
create procedure CreateEvaluator
(
	@EvaluatorID INT = null output,
	@Name NVARCHAR(80) = 'Voter',
	@Criteria NVARCHAR(40) = 'Overall Quality'
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1

	begin
		insert into Evaluator(Name,VotingCriteria)
		values(@Name, @Criteria)

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
			select EvaluatorID,Name,VotingCriteria from Evaluator
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
	@EventID INT = null
) 
as
	declare @returnCode as int
	set @ReturnCode = 1
	
	if(@EventID is null)
		 raiserror('GetEventEvaluators - Required Parameter: @EventID',16,1)
	else
		begin
			select distinct Evaluator.EvaluatorID,Name,VotingCriteria from Evaluator
			inner join EvaluativeData ed on 
			Evaluator.EvaluatorID = ed.EvaluatorID
			where ed.EventID = @EventID
			
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
	@EventID INT = null
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1
	
	if(@EventID is null)
		raiserror('GetHistoricalEvaluationData - Required Parameter: @EventID',16,1)
	else
		begin
			select EvaluatorID, Rating, TimeOfData 
			from EvaluativeData
			where EventID = @EventID

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
	@EventID INT = null
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1
	
	if(@EventID is null)
		raiserror('GetMostRecentEvaluativeData - Required Parameter: @EventID',16,1)
	else
		begin
			select ed.EvaluatorID, ed.Rating, ed.TimeOfData
			from EvaluativeData ed
			left join EvaluativeData ev
			on ed.EvaluatorID = ev.EvaluatorID and ed.TimeOfData < ev.TimeOfData
			where ed.EventID = @EventID and ev.EvaluatorID is null


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
	@EventID INT = null,
	@EvaluatorID int = null
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1
	
	if(@EventID is null)
		raiserror('GetEvaluatorEventData - Required Parameter: @@EventID',16,1)
	if(@EvaluatorID is null)
		raiserror('GetEvaluatorEventData - Required Parameter: @EvaluatorID',16,1)
	else
		begin
			select EventID,EvaluatorID,TimeOfData,Rating from EvaluativeData
			where EventID = @EventID and @EvaluatorID = EvaluatorID

			if @@ERROR = 0
				set @ReturnCode = 0
			else
				raiserror('GetEvaluatorEventData - Select Error: Query Failed',16,1)
		end
	return @ReturnCode				
GO


IF (OBJECT_ID('DeleteEvaluatorEventData') IS NOT NULL)
  DROP PROCEDURE DeleteEvaluatorEventData
go
create procedure DeleteEvaluatorEventData
(
	@EventID INT = null,
	@EvaluatorID int = null
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1
	
	if(@EventID is null)
		raiserror('DeleteEvaluatorEventData - Required Parameter: @@EventID',16,1)
	if(@EvaluatorID is null)
		raiserror('DeleteEvaluatorEventData - Required Parameter: @EvaluatorID',16,1)
	else
		begin
			DELETE FROM EvaluativeData
			where EventID = @EventID and @EvaluatorID = EvaluatorID

			if @@ERROR = 0
				set @ReturnCode = 0
			else
				raiserror('DeleteEvaluatorEventData - Delete Error: Query Failed',16,1)
		end
	return @ReturnCode				
GO


IF (OBJECT_ID('DeleteEventData') IS NOT NULL)
  DROP PROCEDURE DeleteEventData
go
create procedure DeleteEventData
(
	@EventID INT = null
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1
	
	if(@EventID is null)
		raiserror('DeleteEventData - Required Parameter: @@EventID',16,1)
	else
		begin
			DELETE FROM EvaluativeData
			where EventID = @EventID

			if @@ERROR = 0
				set @ReturnCode = 0
			ELSE
				RAISERROR('DeleteEventData - Delete Error: Query Failed',16,1)
		end
	return @ReturnCode				
GO

--**************************************--
IF (OBJECT_ID('GetAllEventData') IS NOT NULL)
  DROP PROCEDURE GetAllEventData
go
create procedure GetAllEventData
(
	@EventID INT = null
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1

	if(@EventID is null)
		raiserror('GetAllEventData - Required Parameter: @@EventID',16,1)
	else
		begin
			select EventID,EvaluatorID,TimeOfData,Rating from EvaluativeData
			where EventID = @EventID-- and EvaluatorID = '11'						--temp added

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
	@EventID INT = null
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1

	if(@EventID is null)
		raiserror('GetEvent - Required Parameter: @@EventID',16,1)
	else
		begin
			select EventID,EventKey, FacilitatorID, Location, Performer,NatureOfEvent, EventDate, EventBegin, EventEnd, OpeningMessage, ClosingMessage, VotingCrit from EventDetails
			where EventID = @EventID

			if @@ERROR = 0
				set @ReturnCode = 0
			else
				raiserror('GetEvent - Select Error: Query Failed',16,1)
			end
		return @ReturnCode	
go


IF (OBJECT_ID('GetEventFromKey') IS NOT NULL)
  DROP PROCEDURE GetEventFromKey
go
create procedure GetEventFromKey
(
	@EventKey NVARCHAR(4) = null
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1

	if(@EventKey is null)
		raiserror('GetEventFromKey - Required Parameter: @EventKey',16,1)
	else
		begin
			select EventID,EventKey, FacilitatorID, Location, Performer,NatureOfEvent, EventDate, EventBegin, EventEnd, OpeningMessage, ClosingMessage, VotingCrit from EventDetails
			where EventKey = @EventKey

			if @@ERROR = 0
				set @ReturnCode = 0
			else
				raiserror('GetEventFromKey - Select Error: Query Failed',16,1)
			end
		return @ReturnCode	
go


IF (OBJECT_ID('GetEventKeys') IS NOT NULL)
  DROP PROCEDURE GetEventKeys
go
create procedure GetEventKeys
as
	declare @ReturnCode as int
	set @ReturnCode = 1

	begin
		select EventKey FROM EventDetails
		WHERE EventKey NOT IN ('ZZZZ')

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
	@Email nvarchar(80) = null,
	@Password nvarchar(64) = null,
	@Salt nvarchar(10) = null,
	@Roles nvarchar(60) = null,
	@FirstName nvarchar(40) = null,
	@LastName nvarchar(40) = null,
	@Title nvarchar(40) = null,
	@Organization nvarchar(80) = null,
	@City nvarchar(80) = null
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



--sp_helpdb Continui
--sp_helpuser Continui02
--go

SELECT * FROM EvaluativeData
SELECT * FROM EventDetails

GRANT EXECUTE ON AddEvaluationDataPoint to Continui02
GRANT EXECUTE ON AddEvaluator to Continui02
GRANT EXECUTE ON CreateEvaluator to Continui02
GRANT EXECUTE ON CreateEvent to Continui02
GRANT EXECUTE ON CreateFacilitator to Continui02
GRANT EXECUTE ON GetAllEventData to Continui02
GRANT EXECUTE ON GetEvaluator to Continui02
GRANT EXECUTE ON GetEvaluatorEventData to Continui02
GRANT EXECUTE ON GetEvent to Continui02
GRANT EXECUTE ON GetEventEvaluators to Continui02
GRANT EXECUTE ON GetFacilitator to Continui02
GRANT EXECUTE ON GetFacilitatorEvents to Continui02
GRANT EXECUTE ON GetHistoricalEvaluationData to Continui02
GRANT EXECUTE ON GetMostRecentEvaluativeData to Continui02
GRANT EXECUTE ON UpdateEventStatus to Continui02
GRANT EXECUTE ON UpdateFacilitatorInfo to Continui02
GRANT EXECUTE ON GetFacilitatorInfo TO Continui02
GRANT EXECUTE ON DeleteFacilitator TO Continui02
GRANT EXECUTE ON DeleteEvent TO Continui02
GRANT EXECUTE ON DeleteEvaluatorEventData TO Continui02
GRANT EXECUTE ON DeleteEventData TO Continui02
GRANT EXECUTE ON CreateQuestion TO Continui02
GRANT EXECUTE ON GetQuestions TO Continui02
GRANT EXECUTE ON AnswerQuestion TO Continui02
GRANT EXECUTE ON GetResponse TO Continui02
GRANT EXECUTE ON GetEventKeys TO Continui02
GRANT EXECUTE ON GetEventFromKey TO Continui02
GRANT EXECUTE ON DeleteQuestion TO Continui02
GRANT EXECUTE ON UpdateEvent TO Continui02

--CHECK THIS SHIT!!!!

--sp_help







--**************************************--
--			Select Statements			--
--**************************************--

--select * from EvaluativeData
--select * from Evaluator
--select * from EventDetails
--select * from Facilitator

