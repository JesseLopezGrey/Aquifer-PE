USE [CnmPro]
GO
/****** Object:  StoredProcedure [dbo].[OrganizationFollowers_SelectById]    Script Date: 8/1/2022 4:32:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Jesse Lopez>
-- Create date: <20220720>
-- Description:	<SelectByIdOrg Organization Follower >

-- MODIFIED BY: n/a
-- MODIFIED DATE: n/a
-- Code Reviewer: 
-- Note: 
-- =============================================

CREATE PROC [dbo].[OrganizationFollowers_SelectById]
				@OrganizationId int

AS
/*------ TEST CODE--------

DECLARE     @OrganizationId int = 1

EXECUTE dbo.OrganizationFollowers_SelectById
                      @OrganizationId

---------------------------------------
DECLARE     @FollowerId int = 19

EXECUTE dbo.OrganizationFollowers_SelectByIdUser
                      @FollowerId
-----------------------------------------------
		select* from dbo.Organizations
		select* from dbo.Users
------END TEST CODE -----*/
BEGIN

SELECT  orgf.[OrganizationId]
       ,orgf.[FollowerId]
       ,up.FirstName
	   ,up.LastName
	   ,up.Mi
	   ,up.AvatarUrl
	   
	   
	   
	   

	   ,TotalCount = COUNT(1) OVER()
  FROM [dbo].[OrganizationFollowers] as orgf 
  inner join [dbo].[Organizations] as O
  ON orgf.OrganizationId = O.Id
  LEFT  join dbo.UserProfiles as up
  ON  up.UserId = orgf.FollowerId 

  
  Where OrganizationId = @OrganizationId
  

END




GO
