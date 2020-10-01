USE [bnhs-qr-code-Database]
GO

/****** Object:  Table [dbo].[QRCodeHistory]    Script Date: 10/1/2020 4:07:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[QRCodeHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserProfileId] [int] NOT NULL,
	[BodyTemp] [decimal](10, 2) NOT NULL,
	[TimeStatus] [varchar](50) NOT NULL,
	[CreatedByDateTime] [varchar](50) NOT NULL,
 CONSTRAINT [PK_QRCodeHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

