USE [bnhs-qr-code-Database]
GO

/****** Object:  Table [dbo].[UserProfile]    Script Date: 10/04/2019 6:34:06 PM ******/

ALTER TABLE [UserProfile] ADD [Admin] BIT NOT NULL DEFAULT 0,
							  [Active] BIT NOT NULL DEFAULT 1;
