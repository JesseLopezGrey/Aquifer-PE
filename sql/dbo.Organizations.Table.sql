USE [CnmPro]
GO
/****** Object:  Table [dbo].[Organizations]    Script Date: 8/1/2022 4:32:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Organizations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrganizationTypeId] [int] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Headline] [nvarchar](200) NULL,
	[Description] [nvarchar](max) NULL,
	[Logo] [nvarchar](255) NULL,
	[LocationId] [int] NOT NULL,
	[Phone] [nvarchar](50) NULL,
	[SiteUrl] [nvarchar](255) NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_Organizations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Organizations] ADD  CONSTRAINT [DF_Organizations_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Organizations] ADD  CONSTRAINT [DF_Organizations_DateModified]  DEFAULT (getutcdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[Organizations]  WITH CHECK ADD  CONSTRAINT [FK_Organizations_OrganizationTypes1] FOREIGN KEY([OrganizationTypeId])
REFERENCES [dbo].[OrganizationTypes] ([Id])
GO
ALTER TABLE [dbo].[Organizations] CHECK CONSTRAINT [FK_Organizations_OrganizationTypes1]
GO
