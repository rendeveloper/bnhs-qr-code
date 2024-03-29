USE [bnhs-qr-code-Database]
GO

/****** Object:  Table [dbo].[UserProfile]    Script Date: 9/30/2020 2:33:33 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserProfile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TeacherId] [varchar](50) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[MiddleName] [varchar](50) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[Address] [varchar](max) NOT NULL,
	[HealthStatus] [varchar](50) NOT NULL,
	[Department] [varchar](50) NOT NULL,
	[Role] [varchar](50) NOT NULL,
	[Image] [varchar](max) NOT NULL,
 CONSTRAINT [PK_UserProfile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

