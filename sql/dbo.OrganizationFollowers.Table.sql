USE [CnmPro]
GO
/****** Object:  Table [dbo].[OrganizationFollowers]    Script Date: 8/1/2022 4:32:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrganizationFollowers](
	[OrganizationId] [int] NOT NULL,
	[FollowerId] [int] NOT NULL,
 CONSTRAINT [PK_OrganizationFollowers] PRIMARY KEY CLUSTERED 
(
	[OrganizationId] ASC,
	[FollowerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrganizationFollowers]  WITH CHECK ADD  CONSTRAINT [FK_OrganizationFollowers_Organizations] FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organizations] ([Id])
GO
ALTER TABLE [dbo].[OrganizationFollowers] CHECK CONSTRAINT [FK_OrganizationFollowers_Organizations]
GO
ALTER TABLE [dbo].[OrganizationFollowers]  WITH CHECK ADD  CONSTRAINT [FK_OrganizationFollowers_Users] FOREIGN KEY([FollowerId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[OrganizationFollowers] CHECK CONSTRAINT [FK_OrganizationFollowers_Users]
GO
