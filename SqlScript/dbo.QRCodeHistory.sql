USE [bnhs-qr-code-Database]
GO

/****** Object:  Table [dbo].[QRCodeHistory]    Script Date: 9/30/2020 2:04:47 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[QRCodeHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserProfileId] [int] NOT NULL,
	[BodyTemp] [int] NOT NULL,
	[TimeStatus] [varchar](50) NOT NULL,
	[CreatedByDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_QRCodeHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

