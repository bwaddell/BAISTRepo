
--===================    Drop and replace table    ===================--
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
go

create table EventDetails
(
	EventID int unique identity(1,1) not null,
	FacilitatorID int not null,
	Location nvarchar(30) not null,
	Performer nvarchar(20) not null,
	NatureOfEvent nvarchar(20) not null,
	EventDate date not null,
	EventBegin datetime not null,
	EventEnd datetime not null
)
go
alter table EventDetails
	add constraint PK_EventDetails primary key (EventID),
		constraint FK_EventDetails foreign key (FacilitatorID) references Facilitator(FacilitatorID),
		constraint CK_EventDetails check (EventEnd > EventBegin)
go




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
go
alter table Evaluator
	add constraint PK_Evaluator primary key (EvaluatorID) 
go


create table EvaluativeData
(
	EventID int not null,
	EvaluatorID int not null,
	TimeOfData time not null,
	Rating int not null
)
alter table EvaluativeData
	add constraint PK_EvaluativeData primary key (TimeOfData, EventID, EvaluatorID),
		constraint FK_EvaluativeData_Event foreign key (EventID) references EventDetails(EventID),
		constraint FK_EvaluativeData_Evaluator foreign key (EvaluatorID) references Evaluator(EvaluatorID),
		constraint CK_EvaluativeData check (Rating > 0)
go



insert into Facilitator values ('Code', 'Man', 'Teacher', 'NAIT', 'Edmonton') 
go
insert into EventDetails values (1, 'Winspear Center', 'Rush', 'Concert', getdate(), '19:00:00', '23:00:00')
go



create procedure AddEvaluationDataPoint
(
	@Event int = null,
	@Evaluator int = null,
	@DataTime time = null,
	@Rating int = null
)
as
	declare @ReturnCode as int
	set @ReturnCode = 1

	if(@Event is null)
		raiserror('AddEvaluationDataPoint - Required Parameter: @Event', 16, 1)
	else
		if(@Evaluator is null)
			raiserror('AddEvaluationDataPoint - Required Parameter: @Evaluator', 16, 1)
		else
			if(@DataTime is null)
				raiserror('AddEvaluationDataPoint - Required Parameter: @DataTime', 16, 1)
			else
				if(@Rating is null)
					raiserror('AddEvaluationDataPoint - Required Parameter: @Rating', 16, 1)
				else
					begin
						insert into EvaluativeData 
						values (@Event, @Evaluator, @DataTime, @Rating)

						if @@ERROR = 0
							set @ReturnCode = 0
						else
							raiserror('AddEvaluationDataPoint - Insert Error: Query Failed',16,1)
					end
				return @ReturnCode
go


select * from Evaluator
select * from EventDetails
select * from Facilitator
select * from EvaluativeData

