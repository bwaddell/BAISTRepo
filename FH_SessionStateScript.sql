--db User
--db user2: Continui02 Continui02

--sp_helpuser Continui
--sp_helpuser Continui02


PRINT '------------------------------------------'
PRINT 'ASP.NET Session State database creation'
PRINT 'Please replace all instances of Continui with your database name'
PRINT 'Please replace all instances of [Web User] with your database user (standard user)' 
PRINT '------------------------------------------'

USE Continui
GO

IF OBJECT_ID(N'dbo.ASPStateTempSessions','U') IS NOT NULL BEGIN
    DROP TABLE dbo.ASPStateTempSessions
END

IF OBJECT_ID(N'dbo.ASPStateTempApplications','U') IS NOT NULL BEGIN
    DROP TABLE dbo.ASPStateTempApplications
END

USE Continui
GO

IF (EXISTS (SELECT name FROM sysobjects WHERE (name = N'GetMajorVersion') AND (type = 'P')))
    DROP PROCEDURE [dbo].GetMajorVersion
GO

IF (EXISTS (SELECT name FROM sysobjects WHERE (name = N'CreateTempTables') AND (type = 'P')))
    DROP PROCEDURE [dbo].CreateTempTables
GO

IF (EXISTS (SELECT name FROM sysobjects WHERE (name = N'TempGetVersion') AND (type = 'P')))
    DROP PROCEDURE [dbo].TempGetVersion
GO

IF (EXISTS (SELECT name FROM sysobjects WHERE (name = N'GetHashCode') AND (type = 'P')))
    DROP PROCEDURE [dbo].GetHashCode
GO

IF (EXISTS (SELECT name FROM sysobjects WHERE (name = N'TempGetAppID') AND (type = 'P')))
    DROP PROCEDURE [dbo].TempGetAppID
GO

IF (EXISTS (SELECT name FROM sysobjects WHERE (name = N'TempGetStateItem') AND (type = 'P')))
    DROP PROCEDURE [dbo].TempGetStateItem
GO

IF (EXISTS (SELECT name FROM sysobjects WHERE (name = N'TempGetStateItem2') AND (type = 'P')))
    DROP PROCEDURE [dbo].TempGetStateItem2
GO

IF (EXISTS (SELECT name FROM sysobjects WHERE (name = N'TempGetStateItem3') AND (type = 'P')))
    DROP PROCEDURE [dbo].TempGetStateItem3
GO

IF (EXISTS (SELECT name FROM sysobjects WHERE (name = N'TempGetStateItemExclusive') AND (type = 'P')))
    DROP PROCEDURE [dbo].TempGetStateItemExclusive
GO

IF (EXISTS (SELECT name FROM sysobjects WHERE (name = N'TempGetStateItemExclusive2') AND (type = 'P')))
    DROP PROCEDURE [dbo].TempGetStateItemExclusive2
GO

IF (EXISTS (SELECT name FROM sysobjects WHERE (name = N'TempGetStateItemExclusive3') AND (type = 'P')))
    DROP PROCEDURE [dbo].TempGetStateItemExclusive3
GO

IF (EXISTS (SELECT name FROM sysobjects WHERE (name = N'TempReleaseStateItemExclusive') AND (type = 'P')))
    DROP PROCEDURE [dbo].TempReleaseStateItemExclusive
GO

IF (EXISTS (SELECT name FROM sysobjects WHERE (name = N'TempInsertUninitializedItem') AND (type = 'P')))
    DROP PROCEDURE [dbo].TempInsertUninitializedItem
GO

IF (EXISTS (SELECT name FROM sysobjects WHERE (name = N'TempInsertStateItemShort') AND (type = 'P')))
    DROP PROCEDURE [dbo].TempInsertStateItemShort
GO

IF (EXISTS (SELECT name FROM sysobjects WHERE (name = N'TempInsertStateItemLong') AND (type = 'P')))
    DROP PROCEDURE [dbo].TempInsertStateItemLong
GO

IF (EXISTS (SELECT name FROM sysobjects WHERE (name = N'TempUpdateStateItemShort') AND (type = 'P')))
    DROP PROCEDURE [dbo].TempUpdateStateItemShort
GO

IF (EXISTS (SELECT name FROM sysobjects WHERE (name = N'TempUpdateStateItemShortNullLong') AND (type = 'P')))
    DROP PROCEDURE [dbo].TempUpdateStateItemShortNullLong
GO

IF (EXISTS (SELECT name FROM sysobjects WHERE (name = N'TempUpdateStateItemLong') AND (type = 'P')))
    DROP PROCEDURE [dbo].TempUpdateStateItemLong
GO

IF (EXISTS (SELECT name FROM sysobjects WHERE (name = N'TempUpdateStateItemLongNullShort') AND (type = 'P')))
    DROP PROCEDURE [dbo].TempUpdateStateItemLongNullShort
GO

IF (EXISTS (SELECT name FROM sysobjects WHERE (name = N'TempRemoveStateItem') AND (type = 'P')))
    DROP PROCEDURE [dbo].TempRemoveStateItem
GO

IF (EXISTS (SELECT name FROM sysobjects WHERE (name = N'TempResetTimeout') AND (type = 'P')))
    DROP PROCEDURE [dbo].TempResetTimeout
GO

IF (EXISTS (SELECT name FROM sysobjects WHERE (name = N'DeleteExpiredSessions') AND (type = 'P')))
    DROP PROCEDURE [dbo].DeleteExpiredSessions
GO

IF EXISTS(SELECT name FROM systypes WHERE name ='tSessionId')
    EXECUTE sp_droptype tSessionId
GO

IF EXISTS(SELECT name FROM systypes WHERE name ='tAppName')
    EXECUTE sp_droptype tAppName
GO

IF EXISTS(SELECT name FROM systypes WHERE name ='tSessionItemShort')
    EXECUTE sp_droptype tSessionItemShort
GO

IF EXISTS(SELECT name FROM systypes WHERE name ='tSessionItemLong')
    EXECUTE sp_droptype tSessionItemLong
GO

IF EXISTS(SELECT name FROM systypes WHERE name ='tTextPtr')
    EXECUTE sp_droptype tTextPtr
GO

/*****************************************************************************

CREATE PROCEDURE dbo.GetMajorVersion
    @@ver int OUTPUT
AS
BEGIN
	DECLARE @version        nchar(100)
	DECLARE @dot            int
	DECLARE @hyphen         int
	DECLARE @SqlToExec      nchar(4000)

	SELECT @@ver = 7
	SELECT @version = @@Version
	SELECT @hyphen  = CHARINDEX(N' - ', @version)
	IF (NOT(@hyphen IS NULL) AND @hyphen > 0)
	BEGIN
		SELECT @hyphen = @hyphen + 3
		SELECT @dot    = CHARINDEX(N'.', @version, @hyphen)
		IF (NOT(@dot IS NULL) AND @dot > @hyphen)
		BEGIN
			SELECT @version = SUBSTRING(@version, @hyphen, @dot - @hyphen)
			SELECT @@ver     = CONVERT(int, @version)
		END
	END
END
GO   

*****************************************************************************/

USE Continui

            CREATE TABLE Continui.dbo.ASPStateTempSessions (
                SessionId           nvarchar(88)    NOT NULL PRIMARY KEY,
                Created             datetime        NOT NULL DEFAULT GETUTCDATE(),
                Expires             datetime        NOT NULL,
                LockDate            datetime        NOT NULL,
                LockDateLocal       datetime        NOT NULL,
                LockCookie          int             NOT NULL,
                Timeout             int             NOT NULL,
                Locked              bit             NOT NULL,
                SessionItemShort    VARBINARY(7000) NULL,
                SessionItemLong     image           NULL,
                Flags               int             NOT NULL DEFAULT 0,
            ) 

            CREATE NONCLUSTERED INDEX Index_Expires ON Continui.dbo.ASPStateTempSessions(Expires)

            CREATE TABLE Continui.dbo.ASPStateTempApplications (
                AppId               int             NOT NULL PRIMARY KEY,
                AppName             char(280)       NOT NULL,
            ) 

            CREATE NONCLUSTERED INDEX Index_AppName ON Continui.dbo.ASPStateTempApplications(AppName)

GO   

/*****************************************************************************/

EXECUTE sp_addtype tSessionId, 'nvarchar(88)',  'NOT NULL'
GO

EXECUTE sp_addtype tAppName, 'varchar(280)', 'NOT NULL'
GO

EXECUTE sp_addtype tSessionItemShort, 'varbinary(7000)'
GO

EXECUTE sp_addtype tSessionItemLong, 'image'
GO

EXECUTE sp_addtype tTextPtr, 'varbinary(16)'
GO

/*****************************************************************************/

CREATE PROCEDURE dbo.TempGetVersion
    @ver      char(10) OUTPUT
AS
    SELECT @ver = '2'
    RETURN 0
GO

/*****************************************************************************/

CREATE PROCEDURE dbo.GetHashCode
    @input tAppName,
    @hash int OUTPUT
AS
    /* 
       This sproc is based on this C# hash function:

        int GetHashCode(string s)
        {
            int     hash = 5381;
            int     len = s.Length;

            for (int i = 0; i < len; i++) {
                int     c = Convert.ToInt32(s[i]);
                hash = ((hash << 5) + hash) ^ c;
            }

            return hash;
        }

        However, SQL 7 doesn't provide a 32-bit integer
        type that allows rollover of bits, we have to
        divide our 32bit integer into the upper and lower
        16 bits to do our calculation.
    */
       
    DECLARE @hi_16bit   int
    DECLARE @lo_16bit   int
    DECLARE @hi_t       int
    DECLARE @lo_t       int
    DECLARE @len        int
    DECLARE @i          int
    DECLARE @c          int
    DECLARE @carry      int

    SET @hi_16bit = 0
    SET @lo_16bit = 5381
    
    SET @len = DATALENGTH(@input)
    SET @i = 1
    
    WHILE (@i <= @len)
    BEGIN
        SET @c = ASCII(SUBSTRING(@input, @i, 1))

        /* Formula:                        
           hash = ((hash << 5) + hash) ^ c */

        /* hash << 5 */
        SET @hi_t = @hi_16bit * 32 /* high 16bits << 5 */
        SET @hi_t = @hi_t & 0xFFFF /* zero out overflow */
        
        SET @lo_t = @lo_16bit * 32 /* low 16bits << 5 */
        
        SET @carry = @lo_16bit & 0x1F0000 /* move low 16bits carryover to hi 16bits */
        SET @carry = @carry / 0x10000 /* >> 16 */
        SET @hi_t = @hi_t + @carry
        SET @hi_t = @hi_t & 0xFFFF /* zero out overflow */

        /* + hash */
        SET @lo_16bit = @lo_16bit + @lo_t
        SET @hi_16bit = @hi_16bit + @hi_t + (@lo_16bit / 0x10000)
        /* delay clearing the overflow */

        /* ^c */
        SET @lo_16bit = @lo_16bit ^ @c

        /* Now clear the overflow bits */	
        SET @hi_16bit = @hi_16bit & 0xFFFF
        SET @lo_16bit = @lo_16bit & 0xFFFF

        SET @i = @i + 1
    END

    /* Do a sign extension of the hi-16bit if needed */
    IF (@hi_16bit & 0x8000 <> 0)
        SET @hi_16bit = 0xFFFF0000 | @hi_16bit

    /* Merge hi and lo 16bit back together */
    SET @hi_16bit = @hi_16bit * 0x10000 /* << 16 */
    SET @hash = @hi_16bit | @lo_16bit

    RETURN 0
GO

/*****************************************************************************/

DECLARE @cmd nchar(4000)

SET @cmd = N'
    CREATE PROCEDURE dbo.TempGetAppID
    @appName    tAppName,
    @appId      int OUTPUT
    AS
    SET @appName = LOWER(@appName)
    SET @appId = NULL

    SELECT @appId = AppId
    FROM Continui.dbo.ASPStateTempApplications
    WHERE AppName = @appName

    IF @appId IS NULL BEGIN
        BEGIN TRAN        

        SELECT @appId = AppId
        FROM Continui.dbo.ASPStateTempApplications WITH (TABLOCKX)
        WHERE AppName = @appName
        
        IF @appId IS NULL
        BEGIN
            EXEC GetHashCode @appName, @appId OUTPUT
            
            INSERT Continui.dbo.ASPStateTempApplications
            VALUES
            (@appId, @appName)
            
            IF @@ERROR = 2627 
            BEGIN
                DECLARE @dupApp tAppName
            
                SELECT @dupApp = RTRIM(AppName)
                FROM Continui.dbo.ASPStateTempApplications 
                WHERE AppId = @appId
                
                RAISERROR(''SQL session state fatal error: hash-code collision between applications ''''%s'''' and ''''%s''''. Please rename the 1st application to resolve the problem.'', 
                            18, 1, @appName, @dupApp)
            END
        END

        COMMIT
    END

    RETURN 0'
EXEC(@cmd)    
GO

/*****************************************************************************/

/* Find out the version */

DECLARE @cmd nchar(4000)

    SET @cmd = N'
        CREATE PROCEDURE dbo.TempGetStateItem
            @id         tSessionId,
            @itemShort  tSessionItemShort OUTPUT,
            @locked     bit OUTPUT,
            @lockDate   datetime OUTPUT,
            @lockCookie int OUTPUT
        AS
            DECLARE @textptr AS tTextPtr
            DECLARE @length AS int
            DECLARE @now AS datetime
            SET @now = GETUTCDATE()

            UPDATE Continui.dbo.ASPStateTempSessions
            SET Expires = DATEADD(n, Timeout, @now), 
                @locked = Locked,
                @lockDate = LockDateLocal,
                @lockCookie = LockCookie,
                @itemShort = CASE @locked
                    WHEN 0 THEN SessionItemShort
                    ELSE NULL
                    END,
                @textptr = CASE @locked
                    WHEN 0 THEN TEXTPTR(SessionItemLong)
                    ELSE NULL
                    END,
                @length = CASE @locked
                    WHEN 0 THEN DATALENGTH(SessionItemLong)
                    ELSE NULL
                    END
            WHERE SessionId = @id
            IF @length IS NOT NULL BEGIN
                READTEXT Continui.dbo.ASPStateTempSessions.SessionItemLong @textptr 0 @length
            END

            RETURN 0'    
EXEC (@cmd)    
GO

/*****************************************************************************/

DECLARE @cmd nchar(4000)

    SET @cmd = N'
        CREATE PROCEDURE dbo.TempGetStateItem2
            @id         tSessionId,
            @itemShort  tSessionItemShort OUTPUT,
            @locked     bit OUTPUT,
            @lockAge    int OUTPUT,
            @lockCookie int OUTPUT
        AS
            DECLARE @textptr AS tTextPtr
            DECLARE @length AS int
            DECLARE @now AS datetime
            SET @now = GETUTCDATE()

            UPDATE Continui.dbo.ASPStateTempSessions
            SET Expires = DATEADD(n, Timeout, @now), 
                @locked = Locked,
                @lockAge = DATEDIFF(second, LockDate, @now),
                @lockCookie = LockCookie,
                @itemShort = CASE @locked
                    WHEN 0 THEN SessionItemShort
                    ELSE NULL
                    END,
                @textptr = CASE @locked
                    WHEN 0 THEN TEXTPTR(SessionItemLong)
                    ELSE NULL
                    END,
                @length = CASE @locked
                    WHEN 0 THEN DATALENGTH(SessionItemLong)
                    ELSE NULL
                    END
            WHERE SessionId = @id
            IF @length IS NOT NULL BEGIN
                READTEXT Continui.dbo.ASPStateTempSessions.SessionItemLong @textptr 0 @length
            END

            RETURN 0'

EXEC (@cmd)    
GO
            

/*****************************************************************************/

DECLARE @cmd nchar(4000)

    SET @cmd = N'
        CREATE PROCEDURE dbo.TempGetStateItem3
            @id         tSessionId,
            @itemShort  tSessionItemShort OUTPUT,
            @locked     bit OUTPUT,
            @lockAge    int OUTPUT,
            @lockCookie int OUTPUT,
            @actionFlags int OUTPUT
        AS
            DECLARE @textptr AS tTextPtr
            DECLARE @length AS int
            DECLARE @now AS datetime
            SET @now = GETUTCDATE()

            UPDATE Continui.dbo.ASPStateTempSessions
            SET Expires = DATEADD(n, Timeout, @now), 
                @locked = Locked,
                @lockAge = DATEDIFF(second, LockDate, @now),
                @lockCookie = LockCookie,
                @itemShort = CASE @locked
                    WHEN 0 THEN SessionItemShort
                    ELSE NULL
                    END,
                @textptr = CASE @locked
                    WHEN 0 THEN TEXTPTR(SessionItemLong)
                    ELSE NULL
                    END,
                @length = CASE @locked
                    WHEN 0 THEN DATALENGTH(SessionItemLong)
                    ELSE NULL
                    END,

                /* If the Uninitialized flag (0x1) if it is set,
                   remove it and return InitializeItem (0x1) in actionFlags */
                Flags = CASE
                    WHEN (Flags & 1) <> 0 THEN (Flags & ~1)
                    ELSE Flags
                    END,
                @actionFlags = CASE
                    WHEN (Flags & 1) <> 0 THEN 1
                    ELSE 0
                    END
            WHERE SessionId = @id
            IF @length IS NOT NULL BEGIN
                READTEXT Continui.dbo.ASPStateTempSessions.SessionItemLong @textptr 0 @length
            END

            RETURN 0'

EXEC (@cmd)    
GO

/*****************************************************************************/

DECLARE @cmd nchar(4000)

    SET @cmd = N'
        CREATE PROCEDURE dbo.TempGetStateItemExclusive
            @id         tSessionId,
            @itemShort  tSessionItemShort OUTPUT,
            @locked     bit OUTPUT,
            @lockDate   datetime OUTPUT,
            @lockCookie int OUTPUT
        AS
            DECLARE @textptr AS tTextPtr
            DECLARE @length AS int
            DECLARE @now AS datetime
            DECLARE @nowLocal AS datetime

            SET @now = GETUTCDATE()
            SET @nowLocal = GETDATE()
            
            UPDATE Continui.dbo.ASPStateTempSessions
            SET Expires = DATEADD(n, Timeout, @now), 
                LockDate = CASE Locked
                    WHEN 0 THEN @now
                    ELSE LockDate
                    END,
                @lockDate = LockDateLocal = CASE Locked
                    WHEN 0 THEN @nowLocal
                    ELSE LockDateLocal
                    END,
                @lockCookie = LockCookie = CASE Locked
                    WHEN 0 THEN LockCookie + 1
                    ELSE LockCookie
                    END,
                @itemShort = CASE Locked
                    WHEN 0 THEN SessionItemShort
                    ELSE NULL
                    END,
                @textptr = CASE Locked
                    WHEN 0 THEN TEXTPTR(SessionItemLong)
                    ELSE NULL
                    END,
                @length = CASE Locked
                    WHEN 0 THEN DATALENGTH(SessionItemLong)
                    ELSE NULL
                    END,
                @locked = Locked,
                Locked = 1
            WHERE SessionId = @id
            IF @length IS NOT NULL BEGIN
                READTEXT Continui.dbo.ASPStateTempSessions.SessionItemLong @textptr 0 @length
            END

            RETURN 0'

EXEC (@cmd)    
GO


/*****************************************************************************/

DECLARE @cmd nchar(4000)

    SET @cmd = N'
        CREATE PROCEDURE dbo.TempGetStateItemExclusive2
            @id         tSessionId,
            @itemShort  tSessionItemShort OUTPUT,
            @locked     bit OUTPUT,
            @lockAge    int OUTPUT,
            @lockCookie int OUTPUT
        AS
            DECLARE @textptr AS tTextPtr
            DECLARE @length AS int
            DECLARE @now AS datetime
            DECLARE @nowLocal AS datetime

            SET @now = GETUTCDATE()
            SET @nowLocal = GETDATE()
            
            UPDATE Continui.dbo.ASPStateTempSessions
            SET Expires = DATEADD(n, Timeout, @now), 
                LockDate = CASE Locked
                    WHEN 0 THEN @now
                    ELSE LockDate
                    END,
                LockDateLocal = CASE Locked
                    WHEN 0 THEN @nowLocal
                    ELSE LockDateLocal
                    END,
                @lockAge = CASE Locked
                    WHEN 0 THEN 0
                    ELSE DATEDIFF(second, LockDate, @now)
                    END,
                @lockCookie = LockCookie = CASE Locked
                    WHEN 0 THEN LockCookie + 1
                    ELSE LockCookie
                    END,
                @itemShort = CASE Locked
                    WHEN 0 THEN SessionItemShort
                    ELSE NULL
                    END,
                @textptr = CASE Locked
                    WHEN 0 THEN TEXTPTR(SessionItemLong)
                    ELSE NULL
                    END,
                @length = CASE Locked
                    WHEN 0 THEN DATALENGTH(SessionItemLong)
                    ELSE NULL
                    END,
                @locked = Locked,
                Locked = 1
            WHERE SessionId = @id
            IF @length IS NOT NULL BEGIN
                READTEXT Continui.dbo.ASPStateTempSessions.SessionItemLong @textptr 0 @length
            END

            RETURN 0'

EXEC (@cmd)    
GO


/*****************************************************************************/


DECLARE @cmd nchar(4000)

    SET @cmd = N'
        CREATE PROCEDURE dbo.TempGetStateItemExclusive3
            @id         tSessionId,
            @itemShort  tSessionItemShort OUTPUT,
            @locked     bit OUTPUT,
            @lockAge    int OUTPUT,
            @lockCookie int OUTPUT,
            @actionFlags int OUTPUT
        AS
            DECLARE @textptr AS tTextPtr
            DECLARE @length AS int
            DECLARE @now AS datetime
            DECLARE @nowLocal AS datetime

            SET @now = GETUTCDATE()
            SET @nowLocal = GETDATE()
            
            UPDATE Continui.dbo.ASPStateTempSessions
            SET Expires = DATEADD(n, Timeout, @now), 
                LockDate = CASE Locked
                    WHEN 0 THEN @now
                    ELSE LockDate
                    END,
                LockDateLocal = CASE Locked
                    WHEN 0 THEN @nowLocal
                    ELSE LockDateLocal
                    END,
                @lockAge = CASE Locked
                    WHEN 0 THEN 0
                    ELSE DATEDIFF(second, LockDate, @now)
                    END,
                @lockCookie = LockCookie = CASE Locked
                    WHEN 0 THEN LockCookie + 1
                    ELSE LockCookie
                    END,
                @itemShort = CASE Locked
                    WHEN 0 THEN SessionItemShort
                    ELSE NULL
                    END,
                @textptr = CASE Locked
                    WHEN 0 THEN TEXTPTR(SessionItemLong)
                    ELSE NULL
                    END,
                @length = CASE Locked
                    WHEN 0 THEN DATALENGTH(SessionItemLong)
                    ELSE NULL
                    END,
                @locked = Locked,
                Locked = 1,

                /* If the Uninitialized flag (0x1) if it is set,
                   remove it and return InitializeItem (0x1) in actionFlags */
                Flags = CASE
                    WHEN (Flags & 1) <> 0 THEN (Flags & ~1)
                    ELSE Flags
                    END,
                @actionFlags = CASE
                    WHEN (Flags & 1) <> 0 THEN 1
                    ELSE 0
                    END
            WHERE SessionId = @id
            IF @length IS NOT NULL BEGIN
                READTEXT Continui.dbo.ASPStateTempSessions.SessionItemLong @textptr 0 @length
            END

            RETURN 0'

EXEC (@cmd)    
GO


/*****************************************************************************/

DECLARE @cmd nchar(4000)

    SET @cmd = N'
        CREATE PROCEDURE dbo.TempReleaseStateItemExclusive
            @id         tSessionId,
            @lockCookie int
        AS
            UPDATE Continui.dbo.ASPStateTempSessions
            SET Expires = DATEADD(n, Timeout, GETUTCDATE()), 
                Locked = 0
            WHERE SessionId = @id AND LockCookie = @lockCookie

            RETURN 0'

EXEC (@cmd)    
GO


/*****************************************************************************/

DECLARE @cmd nchar(4000)

    SET @cmd = N'
        CREATE PROCEDURE dbo.TempInsertUninitializedItem
            @id         tSessionId,
            @itemShort  tSessionItemShort,
            @timeout    int
        AS    

            DECLARE @now AS datetime
            DECLARE @nowLocal AS datetime
            
            SET @now = GETUTCDATE()
            SET @nowLocal = GETDATE()

            INSERT Continui.dbo.ASPStateTempSessions 
                (SessionId, 
                 SessionItemShort, 
                 Timeout, 
                 Expires, 
                 Locked, 
                 LockDate,
                 LockDateLocal,
                 LockCookie,
                 Flags) 
            VALUES 
                (@id, 
                 @itemShort, 
                 @timeout, 
                 DATEADD(n, @timeout, @now), 
                 0, 
                 @now,
                 @nowLocal,
                 1,
                 1)

            RETURN 0'

EXEC (@cmd)    
GO


/*****************************************************************************/

DECLARE @cmd nchar(4000)

    SET @cmd = N'
        CREATE PROCEDURE dbo.TempInsertStateItemShort
            @id         tSessionId,
            @itemShort  tSessionItemShort,
            @timeout    int
        AS    

            DECLARE @now AS datetime
            DECLARE @nowLocal AS datetime
            
            SET @now = GETUTCDATE()
            SET @nowLocal = GETDATE()

            INSERT Continui.dbo.ASPStateTempSessions 
                (SessionId, 
                 SessionItemShort, 
                 Timeout, 
                 Expires, 
                 Locked, 
                 LockDate,
                 LockDateLocal,
                 LockCookie) 
            VALUES 
                (@id, 
                 @itemShort, 
                 @timeout, 
                 DATEADD(n, @timeout, @now), 
                 0, 
                 @now,
                 @nowLocal,
                 1)

            RETURN 0'

EXEC (@cmd)    
GO


/*****************************************************************************/

DECLARE @cmd nchar(4000)

    SET @cmd = N'
        CREATE PROCEDURE dbo.TempInsertStateItemLong
            @id         tSessionId,
            @itemLong   tSessionItemLong,
            @timeout    int
        AS    
            DECLARE @now AS datetime
            DECLARE @nowLocal AS datetime
            
            SET @now = GETUTCDATE()
            SET @nowLocal = GETDATE()

            INSERT Continui.dbo.ASPStateTempSessions 
                (SessionId, 
                 SessionItemLong, 
                 Timeout, 
                 Expires, 
                 Locked, 
                 LockDate,
                 LockDateLocal,
                 LockCookie) 
            VALUES 
                (@id, 
                 @itemLong, 
                 @timeout, 
                 DATEADD(n, @timeout, @now), 
                 0, 
                 @now,
                 @nowLocal,
                 1)

            RETURN 0'

EXEC (@cmd)    
GO


/*****************************************************************************/


DECLARE @cmd nchar(4000)

    SET @cmd = N'
        CREATE PROCEDURE dbo.TempUpdateStateItemShort
            @id         tSessionId,
            @itemShort  tSessionItemShort,
            @timeout    int,
            @lockCookie int
        AS    
            UPDATE Continui.dbo.ASPStateTempSessions
            SET Expires = DATEADD(n, @timeout, GETUTCDATE()), 
                SessionItemShort = @itemShort, 
                Timeout = @timeout,
                Locked = 0
            WHERE SessionId = @id AND LockCookie = @lockCookie

            RETURN 0'

EXEC (@cmd)    
GO


/*****************************************************************************/

DECLARE @cmd nchar(4000)

    SET @cmd = N'
        CREATE PROCEDURE dbo.TempUpdateStateItemShortNullLong
            @id         tSessionId,
            @itemShort  tSessionItemShort,
            @timeout    int,
            @lockCookie int
        AS    
            UPDATE Continui.dbo.ASPStateTempSessions
            SET Expires = DATEADD(n, @timeout, GETUTCDATE()), 
                SessionItemShort = @itemShort, 
                SessionItemLong = NULL, 
                Timeout = @timeout,
                Locked = 0
            WHERE SessionId = @id AND LockCookie = @lockCookie

            RETURN 0'

EXEC (@cmd)    
GO


/*****************************************************************************/

DECLARE @cmd nchar(4000)

    SET @cmd = N'
        CREATE PROCEDURE dbo.TempUpdateStateItemLong
            @id         tSessionId,
            @itemLong   tSessionItemLong,
            @timeout    int,
            @lockCookie int
        AS    
            UPDATE Continui.dbo.ASPStateTempSessions
            SET Expires = DATEADD(n, @timeout, GETUTCDATE()), 
                SessionItemLong = @itemLong,
                Timeout = @timeout,
                Locked = 0
            WHERE SessionId = @id AND LockCookie = @lockCookie

            RETURN 0'

EXEC (@cmd)            
GO


/*****************************************************************************/

DECLARE @cmd nchar(4000)

    SET @cmd = N'
        CREATE PROCEDURE dbo.TempUpdateStateItemLongNullShort
            @id         tSessionId,
            @itemLong   tSessionItemLong,
            @timeout    int,
            @lockCookie int
        AS    
            UPDATE Continui.dbo.ASPStateTempSessions
            SET Expires = DATEADD(n, @timeout, GETUTCDATE()), 
                SessionItemLong = @itemLong, 
                SessionItemShort = NULL,
                Timeout = @timeout,
                Locked = 0
            WHERE SessionId = @id AND LockCookie = @lockCookie

            RETURN 0'

EXEC (@cmd)            
GO

/*****************************************************************************/

DECLARE @cmd nchar(4000)
SET @cmd = N'
    CREATE PROCEDURE dbo.TempRemoveStateItem
        @id     tSessionId,
        @lockCookie int
    AS
        DELETE Continui.dbo.ASPStateTempSessions
        WHERE SessionId = @id AND LockCookie = @lockCookie
        RETURN 0'
EXEC(@cmd)    
GO
            
/*****************************************************************************/

DECLARE @cmd nchar(4000)

    SET @cmd = N'
        CREATE PROCEDURE dbo.TempResetTimeout
            @id     tSessionId
        AS
            UPDATE Continui.dbo.ASPStateTempSessions
            SET Expires = DATEADD(n, Timeout, GETUTCDATE())
            WHERE SessionId = @id
            RETURN 0'

EXEC (@cmd)            
GO

            
/*****************************************************************************/


DECLARE @cmd nchar(4000)

    SET @cmd = N'
        CREATE PROCEDURE dbo.DeleteExpiredSessions
        AS
            SET NOCOUNT ON
            SET DEADLOCK_PRIORITY LOW 

            DECLARE @now datetime
            SET @now = GETUTCDATE() 

            CREATE TABLE #tblExpiredSessions 
            ( 
                SessionId nvarchar(88) NOT NULL PRIMARY KEY
            )

            INSERT #tblExpiredSessions (SessionId)
                SELECT SessionId
                FROM Continui.dbo.ASPStateTempSessions WITH (READUNCOMMITTED)
                WHERE Expires < @now

            IF @@ROWCOUNT <> 0 
            BEGIN 
                DECLARE ExpiredSessionCursor CURSOR LOCAL FORWARD_ONLY READ_ONLY
                FOR SELECT SessionId FROM #tblExpiredSessions 

                DECLARE @SessionId nvarchar(88)

                OPEN ExpiredSessionCursor

                FETCH NEXT FROM ExpiredSessionCursor INTO @SessionId

                WHILE @@FETCH_STATUS = 0 
                    BEGIN
                        DELETE FROM Continui.dbo.ASPStateTempSessions WHERE SessionId = @SessionId AND Expires < @now
                        FETCH NEXT FROM ExpiredSessionCursor INTO @SessionId
                    END

                CLOSE ExpiredSessionCursor

                DEALLOCATE ExpiredSessionCursor

            END 

            DROP TABLE #tblExpiredSessions

        RETURN 0'

EXEC (@cmd)            
GO

/*****************************************************************************/

CREATE PROCEDURE dbo.GetMajorVersion
    @@ver int OUTPUT
AS
BEGIN
	DECLARE @version        nchar(100)
	DECLARE @dot            int
	DECLARE @hyphen         int
	DECLARE @SqlToExec      nchar(4000)

	SELECT @@ver = 7
	SELECT @version = @@Version
	SELECT @hyphen  = CHARINDEX(N' - ', @version)
	IF (NOT(@hyphen IS NULL) AND @hyphen > 0)
	BEGIN
		SELECT @hyphen = @hyphen + 3
		SELECT @dot    = CHARINDEX(N'.', @version, @hyphen)
		IF (NOT(@dot IS NULL) AND @dot > @hyphen)
		BEGIN
			SELECT @version = SUBSTRING(@version, @hyphen, @dot - @hyphen)
			SELECT @@ver     = CONVERT(int, @version)
		END
	END
END
GO 

/*****************************************************************************/
            
GRANT EXEC ON TempGetVersion TO Continui02;
GRANT EXEC ON GetHashCode TO Continui02;
GRANT EXEC ON TempGetAppID TO Continui02;
GRANT EXEC ON TempGetStateItem TO Continui02;
GRANT EXEC ON TempGetStateItem2 TO Continui02;
GRANT EXEC ON TempGetStateItem3 TO Continui02;
GRANT EXEC ON TempGetStateItemExclusive TO Continui02;
GRANT EXEC ON TempGetStateItemExclusive2 TO Continui02;
GRANT EXEC ON TempGetStateItemExclusive3 TO Continui02;
GRANT EXEC ON TempReleaseStateItemExclusive TO Continui02;
GRANT EXEC ON TempInsertUninitializedItem TO Continui02;
GRANT EXEC ON TempInsertStateItemShort TO Continui02;
GRANT EXEC ON TempUpdateStateItemShort TO Continui02;
GRANT EXEC ON TempUpdateStateItemShortNullLong TO Continui02;
GRANT EXEC ON TempUpdateStateItemLong TO Continui02;
GRANT EXEC ON TempUpdateStateItemLongNullShort TO Continui02;
GRANT EXEC ON TempRemoveStateItem TO Continui02;
GRANT EXEC ON TempResetTimeout TO Continui02;
GRANT EXEC ON DeleteExpiredSessions TO Continui02;
GRANT EXEC ON GetMajorVersion TO Continui02;


PRINT '------------------------------------------'
PRINT 'Completed execution of InstallSqlState.SQL'
PRINT '------------------------------------------'