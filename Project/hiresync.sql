IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Departments] (
    [DepartmentId] int NOT NULL IDENTITY,
    [DepartmentName] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Departments] PRIMARY KEY ([DepartmentId])
);

CREATE TABLE [Exams] (
    [ExamId] int NOT NULL IDENTITY,
    [ExamName] nvarchar(max) NOT NULL,
    [ExamDate] datetime2 NOT NULL,
    [Score] int NOT NULL,
    CONSTRAINT [PK_Exams] PRIMARY KEY ([ExamId])
);

CREATE TABLE [JobLists] (
    [JobListId] int NOT NULL IDENTITY,
    [DepartmentId] int NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [CreateDate] datetime2 NOT NULL,
    CONSTRAINT [PK_JobLists] PRIMARY KEY ([JobListId]),
    CONSTRAINT [FK_JobLists_Departments_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Departments] ([DepartmentId]) ON DELETE CASCADE
);

CREATE TABLE [Jobs] (
    [JobId] int NOT NULL IDENTITY,
    [DepartmentId] int NOT NULL,
    [JobName] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Jobs] PRIMARY KEY ([JobId]),
    CONSTRAINT [FK_Jobs_Departments_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Departments] ([DepartmentId]) ON DELETE CASCADE
);

CREATE TABLE [SatisfactionSurveys] (
    [SatisfactionSurveyId] int NOT NULL IDENTITY,
    [SurveyTitle] nvarchar(max) NOT NULL,
    [SurveyType] nvarchar(max) NOT NULL,
    [DepartmentId] int NOT NULL,
    CONSTRAINT [PK_SatisfactionSurveys] PRIMARY KEY ([SatisfactionSurveyId]),
    CONSTRAINT [FK_SatisfactionSurveys_Departments_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Departments] ([DepartmentId]) ON DELETE CASCADE
);

CREATE TABLE [Users] (
    [UserId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [Gender] nvarchar(max) NOT NULL,
    [Phone] nvarchar(max) NOT NULL,
    [Address] nvarchar(max) NOT NULL,
    [Birthday] datetime2 NOT NULL,
    [Salary] decimal(18,2) NOT NULL,
    [JobId] int NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([UserId]),
    CONSTRAINT [FK_Users_Jobs_JobId] FOREIGN KEY ([JobId]) REFERENCES [Jobs] ([JobId]) ON DELETE CASCADE
);

CREATE TABLE [JobApplications] (
    [JobApplicationId] int NOT NULL IDENTITY,
    [JobId] int NOT NULL,
    [UserId] int NOT NULL,
    [AppMail] nvarchar(max) NOT NULL,
    [Location] nvarchar(max) NOT NULL,
    [AppDate] datetime2 NOT NULL,
    [ResumePath] nvarchar(max) NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_JobApplications] PRIMARY KEY ([JobApplicationId]),
    CONSTRAINT [FK_JobApplications_Jobs_JobId] FOREIGN KEY ([JobId]) REFERENCES [Jobs] ([JobId]) ON DELETE CASCADE,
    CONSTRAINT [FK_JobApplications_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId]) ON DELETE NO ACTION
);

CREATE TABLE [LeaveRequests] (
    [LeaveRequestId] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NOT NULL,
    [LeaveType] nvarchar(max) NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    [RequestDate] datetime2 NOT NULL,
    CONSTRAINT [PK_LeaveRequests] PRIMARY KEY ([LeaveRequestId]),
    CONSTRAINT [FK_LeaveRequests_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId]) ON DELETE CASCADE
);

CREATE TABLE [Logins] (
    [LoginId] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [UserName] nvarchar(max) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [LastLogin] datetime2 NULL,
    CONSTRAINT [PK_Logins] PRIMARY KEY ([LoginId]),
    CONSTRAINT [FK_Logins_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId]) ON DELETE CASCADE
);

CREATE TABLE [PerformanceReviews] (
    [PerformanceReviewId] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [PerformanceRate] tinyint NOT NULL,
    [ReviewText] nvarchar(max) NOT NULL,
    [ExamId] int NOT NULL,
    [ReviewDate] datetime2 NOT NULL,
    CONSTRAINT [PK_PerformanceReviews] PRIMARY KEY ([PerformanceReviewId]),
    CONSTRAINT [FK_PerformanceReviews_Exams_ExamId] FOREIGN KEY ([ExamId]) REFERENCES [Exams] ([ExamId]) ON DELETE CASCADE,
    CONSTRAINT [FK_PerformanceReviews_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId]) ON DELETE CASCADE
);

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'DepartmentId', N'DepartmentName') AND [object_id] = OBJECT_ID(N'[Departments]'))
    SET IDENTITY_INSERT [Departments] ON;
INSERT INTO [Departments] ([DepartmentId], [DepartmentName])
VALUES (1, N'General');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'DepartmentId', N'DepartmentName') AND [object_id] = OBJECT_ID(N'[Departments]'))
    SET IDENTITY_INSERT [Departments] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'JobId', N'DepartmentId', N'JobName') AND [object_id] = OBJECT_ID(N'[Jobs]'))
    SET IDENTITY_INSERT [Jobs] ON;
INSERT INTO [Jobs] ([JobId], [DepartmentId], [JobName])
VALUES (1, 1, N'Software Developer'),
(2, 1, N'HR Specialist');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'JobId', N'DepartmentId', N'JobName') AND [object_id] = OBJECT_ID(N'[Jobs]'))
    SET IDENTITY_INSERT [Jobs] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserId', N'Address', N'Birthday', N'Email', N'Gender', N'JobId', N'LastName', N'Name', N'Phone', N'Salary') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] ON;
INSERT INTO [Users] ([UserId], [Address], [Birthday], [Email], [Gender], [JobId], [LastName], [Name], [Phone], [Salary])
VALUES (1, N'New York', '1990-05-20T00:00:00.0000000', N'john.doe@example.com', N'Male', 1, N'Doe', N'John', N'5553330278', 60000.0),
(2, N'Los Angeles', '1992-03-15T00:00:00.0000000', N'jane.smith@example.com', N'Female', 2, N'Smith', N'User', N'5553330279', 55000.0);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserId', N'Address', N'Birthday', N'Email', N'Gender', N'JobId', N'LastName', N'Name', N'Phone', N'Salary') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] OFF;

CREATE INDEX [IX_JobApplications_JobId] ON [JobApplications] ([JobId]);

CREATE INDEX [IX_JobApplications_UserId] ON [JobApplications] ([UserId]);

CREATE INDEX [IX_JobLists_DepartmentId] ON [JobLists] ([DepartmentId]);

CREATE INDEX [IX_Jobs_DepartmentId] ON [Jobs] ([DepartmentId]);

CREATE INDEX [IX_LeaveRequests_UserId] ON [LeaveRequests] ([UserId]);

CREATE INDEX [IX_Logins_UserId] ON [Logins] ([UserId]);

CREATE INDEX [IX_PerformanceReviews_ExamId] ON [PerformanceReviews] ([ExamId]);

CREATE INDEX [IX_PerformanceReviews_UserId] ON [PerformanceReviews] ([UserId]);

CREATE INDEX [IX_SatisfactionSurveys_DepartmentId] ON [SatisfactionSurveys] ([DepartmentId]);

CREATE INDEX [IX_Users_JobId] ON [Users] ([JobId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250411114849_InitialCreate', N'9.0.4');

ALTER TABLE [Users] ADD [RoleName] nvarchar(max) NOT NULL DEFAULT N'';

UPDATE [Users] SET [RoleName] = N'Admin'
WHERE [UserId] = 1;
SELECT @@ROWCOUNT;


UPDATE [Users] SET [RoleName] = N'Worker'
WHERE [UserId] = 2;
SELECT @@ROWCOUNT;


INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250420120157_LoginTable', N'9.0.4');

ALTER TABLE [JobApplications] DROP CONSTRAINT [FK_JobApplications_Users_UserId];

DROP INDEX [IX_JobApplications_UserId] ON [JobApplications];

DELETE FROM [Users]
WHERE [UserId] = 1;
SELECT @@ROWCOUNT;


DELETE FROM [Users]
WHERE [UserId] = 2;
SELECT @@ROWCOUNT;


DELETE FROM [Jobs]
WHERE [JobId] = 1;
SELECT @@ROWCOUNT;


DELETE FROM [Jobs]
WHERE [JobId] = 2;
SELECT @@ROWCOUNT;


DELETE FROM [Departments]
WHERE [DepartmentId] = 1;
SELECT @@ROWCOUNT;


DECLARE @var sysname;
SELECT @var = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[JobApplications]') AND [c].[name] = N'UserId');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [JobApplications] DROP CONSTRAINT [' + @var + '];');
ALTER TABLE [JobApplications] DROP COLUMN [UserId];

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250422201321_AllSeedData', N'9.0.4');

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250425120602_UserDataInitial', N'9.0.4');

ALTER TABLE [JobApplications] DROP CONSTRAINT [FK_JobApplications_Jobs_JobId];

EXEC sp_rename N'[JobApplications].[JobId]', N'JobListId', 'COLUMN';

EXEC sp_rename N'[JobApplications].[IX_JobApplications_JobId]', N'IX_JobApplications_JobListId', 'INDEX';

ALTER TABLE [Logins] ADD [Mail] nvarchar(max) NOT NULL DEFAULT N'';

ALTER TABLE [JobApplications] ADD [CandidateId] int NOT NULL DEFAULT 0;

ALTER TABLE [JobApplications] ADD CONSTRAINT [FK_JobApplications_JobLists_JobListId] FOREIGN KEY ([JobListId]) REFERENCES [JobLists] ([JobListId]) ON DELETE CASCADE;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250425212809_InitialCandidateData', N'9.0.4');

CREATE TABLE [Candidates] (
    [CandidateId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [SurName] nvarchar(max) NOT NULL,
    [Mail] nvarchar(max) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Candidates] PRIMARY KEY ([CandidateId])
);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250425224311_CandidateInitialCreate', N'9.0.4');

ALTER TABLE [PerformanceReviews] DROP CONSTRAINT [FK_PerformanceReviews_Exams_ExamId];

ALTER TABLE [Users] DROP CONSTRAINT [FK_Users_Jobs_JobId];

DROP INDEX [IX_PerformanceReviews_ExamId] ON [PerformanceReviews];

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'Salary');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Users] DROP COLUMN [Salary];

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PerformanceReviews]') AND [c].[name] = N'ExamId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [PerformanceReviews] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [PerformanceReviews] DROP COLUMN [ExamId];

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[JobApplications]') AND [c].[name] = N'AppMail');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [JobApplications] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [JobApplications] DROP COLUMN [AppMail];

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[JobApplications]') AND [c].[name] = N'Location');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [JobApplications] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [JobApplications] DROP COLUMN [Location];

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[JobApplications]') AND [c].[name] = N'ResumePath');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [JobApplications] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [JobApplications] DROP COLUMN [ResumePath];

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Exams]') AND [c].[name] = N'Score');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Exams] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Exams] DROP COLUMN [Score];

EXEC sp_rename N'[PerformanceReviews].[ReviewText]', N'ReviewSummary', 'COLUMN';

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'RoleName');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [Users] ALTER COLUMN [RoleName] nvarchar(max) NULL;

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'JobId');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [Users] ALTER COLUMN [JobId] int NULL;

ALTER TABLE [PerformanceReviews] ADD [AverageScore] float NOT NULL DEFAULT 0.0E0;

ALTER TABLE [Candidates] ADD [Address] nvarchar(max) NOT NULL DEFAULT N'';

ALTER TABLE [Candidates] ADD [Birthday] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

ALTER TABLE [Candidates] ADD [Gender] nvarchar(max) NOT NULL DEFAULT N'';

ALTER TABLE [Candidates] ADD [Phone] nvarchar(max) NOT NULL DEFAULT N'';

ALTER TABLE [Candidates] ADD [ResumePath] nvarchar(max) NOT NULL DEFAULT N'';

ALTER TABLE [Candidates] ADD [UserName] nvarchar(max) NOT NULL DEFAULT N'';

CREATE TABLE [Questions] (
    [QuestionId] int NOT NULL IDENTITY,
    [ExamId] int NOT NULL,
    [QuestionText] nvarchar(max) NOT NULL,
    [AnswerOptions] nvarchar(max) NOT NULL,
    [CorrectAnswer] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Questions] PRIMARY KEY ([QuestionId]),
    CONSTRAINT [FK_Questions_Exams_ExamId] FOREIGN KEY ([ExamId]) REFERENCES [Exams] ([ExamId]) ON DELETE CASCADE
);

CREATE TABLE [UserExams] (
    [UserExamId] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [ExamId] int NOT NULL,
    [Score] int NOT NULL,
    CONSTRAINT [PK_UserExams] PRIMARY KEY ([UserExamId]),
    CONSTRAINT [FK_UserExams_Exams_ExamId] FOREIGN KEY ([ExamId]) REFERENCES [Exams] ([ExamId]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserExams_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId]) ON DELETE CASCADE
);

CREATE INDEX [IX_Questions_ExamId] ON [Questions] ([ExamId]);

CREATE INDEX [IX_UserExams_ExamId] ON [UserExams] ([ExamId]);

CREATE INDEX [IX_UserExams_UserId] ON [UserExams] ([UserId]);

ALTER TABLE [Users] ADD CONSTRAINT [FK_Users_Jobs_JobId] FOREIGN KEY ([JobId]) REFERENCES [Jobs] ([JobId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250429115726_RestoreUser', N'9.0.4');

EXEC sp_rename N'[JobApplications].[CandidateId]', N'UserId', 'COLUMN';

ALTER TABLE [JobLists] ADD [DepartmentName] nvarchar(max) NOT NULL DEFAULT N'';

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250430075902_JobListData', N'9.0.4');

COMMIT;
GO

