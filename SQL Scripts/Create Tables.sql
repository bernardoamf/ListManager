﻿/*
Deployment script for ListManager

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "ListManager"
:setvar DefaultFilePrefix "ListManager"
:setvar DefaultDataPath ""
:setvar DefaultLogPath ""

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO

IF (SELECT OBJECT_ID('tempdb..#tmpErrors')) IS NOT NULL DROP TABLE #tmpErrors
GO
CREATE TABLE #tmpErrors (Error int)
GO
SET XACT_ABORT ON
GO
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
GO
BEGIN TRANSACTION
GO
PRINT N'Creating [dbo].[Account]...';


GO
CREATE TABLE [dbo].[Account] (
    [AccountId]         INT           NOT NULL,
    [CreatedSemesterId] NVARCHAR (50) NULL,
    [ContactId]         INT           NULL,
    [EnrollmentId]      INT           NULL,
    PRIMARY KEY CLUSTERED ([AccountId] ASC)
);


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO
PRINT N'Creating [dbo].[Class]...';


GO
CREATE TABLE [dbo].[Class] (
    [ClassId]    INT            IDENTITY (1, 1) NOT NULL,
    [SemesterId] NVARCHAR (255) NULL,
    [LocationId] INT            NULL,
    [DayOfWeek]  NVARCHAR (9)   NULL,
    [Time]       TIME (7)       NULL,
    PRIMARY KEY CLUSTERED ([ClassId] ASC)
);


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO
PRINT N'Creating [dbo].[Contact]...';


GO
CREATE TABLE [dbo].[Contact] (
    [ContactId] INT            IDENTITY (1, 1) NOT NULL,
    [EmailId]   INT            NOT NULL,
    [FirstName] NVARCHAR (255) NULL,
    [LastName]  NVARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([ContactId] ASC)
);


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO
PRINT N'Creating [dbo].[Email]...';


GO
CREATE TABLE [dbo].[Email] (
    [EmailId] INT            IDENTITY (1, 1) NOT NULL,
    [Email]   NVARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([EmailId] ASC)
);


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO
PRINT N'Creating [dbo].[Enrollment]...';


GO
CREATE TABLE [dbo].[Enrollment] (
    [EnrollmentId] INT           NOT NULL,
    [CreateDate]   DATETIME      NULL,
    [StudentId]    INT           NULL,
    [SemesterId]   NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([EnrollmentId] ASC)
);


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO
PRINT N'Creating [dbo].[Location]...';


GO
CREATE TABLE [dbo].[Location] (
    [LocationId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([LocationId] ASC)
);


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO
PRINT N'Creating [dbo].[Semester]...';


GO
CREATE TABLE [dbo].[Semester] (
    [SemesterId]     NVARCHAR (50) NOT NULL,
    [SemesterNameId] NVARCHAR (50) NULL,
    [SemesterYearId] INT           NULL,
    PRIMARY KEY CLUSTERED ([SemesterId] ASC)
);


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO
PRINT N'Creating [dbo].[SemesterName]...';


GO
CREATE TABLE [dbo].[SemesterName] (
    [SemesterNameId] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([SemesterNameId] ASC)
);


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO
PRINT N'Creating [dbo].[SemesterYear]...';


GO
CREATE TABLE [dbo].[SemesterYear] (
    [SemesterYearId] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([SemesterYearId] ASC)
);


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO
PRINT N'Creating [dbo].[Student]...';


GO
CREATE TABLE [dbo].[Student] (
    [StudentId] INT            IDENTITY (1, 1) NOT NULL,
    [FirstName] NVARCHAR (255) NULL,
    [LastName]  NVARCHAR (255) NULL,
    [BirthDate] DATETIME       NULL,
    [ContactId] INT            NULL,
    PRIMARY KEY CLUSTERED ([StudentId] ASC)
);


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO
PRINT N'Creating [dbo].[Teacher]...';


GO
CREATE TABLE [dbo].[Teacher] (
    [TeacherId] INT           IDENTITY (1, 1) NOT NULL,
    [FirstName] NVARCHAR (50) NULL,
    [LastName]  NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([TeacherId] ASC)
);


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO

IF EXISTS (SELECT * FROM #tmpErrors) ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT>0 BEGIN
PRINT N'The transacted portion of the database update succeeded.'
COMMIT TRANSACTION
END
ELSE PRINT N'The transacted portion of the database update failed.'
GO
DROP TABLE #tmpErrors
GO
PRINT N'Update complete.';


GO
