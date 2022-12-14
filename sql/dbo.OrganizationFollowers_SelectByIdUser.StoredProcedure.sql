USE [CnmPro]
GO
/****** Object:  StoredProcedure [dbo].[OrganizationFollowers_SelectByIdUser]    Script Date: 8/1/2022 4:32:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Jesse Lopez>
-- Create date: <20220720>
-- Description:	<SelectByIdUser an Organization Follower >

-- MODIFIED BY: n/a
-- MODIFIED DATE: n/a
-- Code Reviewer: 
-- Note: 
-- =============================================

CREATE PROC [dbo].[OrganizationFollowers_SelectByIdUser]
				@FollowerId int

AS
/*------ TEST CODE--------

DECLARE     @FollowerId int = 19

EXECUTE dbo.OrganizationFollowers_SelectByIdUser
                      @FollowerId

----------------------------------------
		select* from dbo.Organizations
		select* from dbo.Users
------END TEST CODE -----*/
BEGIN

SELECT  orgf.[FollowerId]
       ,orgf.[OrganizationId]
       ,O.Name
	   ,O.Headline
	   ,O.Description
	   ,O.Logo
	   ,O.Phone
	   ,O.SiteUrl

	   ,TotalCount = COUNT(1) OVER()
  FROM [dbo].[OrganizationFollowers] as orgf 
  inner join [dbo].[Organizations] as O
  ON orgf.OrganizationId = O.Id
  inner join dbo.Users as u
  ON  u.Id = orgf.FollowerId 
  
  Where FollowerId = @FollowerId

END




GO
