
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

