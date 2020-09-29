USE [bnhs-qr-code-Database]
GO

/****** Object:  Table [dbo].[UserImage]    Script Date: 9/30/2020 2:05:17 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserImage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](max) NOT NULL,
	[OriginalFileName] [nvarchar](max) NOT NULL,
	[FileSize] [int] NOT NULL,
	[UploadDate] [datetime] NOT NULL,
	[UserProfileId] [int] NOT NULL,
 CONSTRAINT [PK_UserImage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[UserImage]  WITH CHECK ADD  CONSTRAINT [FK_UserImage_UserProfile] FOREIGN KEY([UserProfileId])
REFERENCES [dbo].[UserProfile] ([Id])
GO

ALTER TABLE [dbo].[UserImage] CHECK CONSTRAINT [FK_UserImage_UserProfile]
GO

