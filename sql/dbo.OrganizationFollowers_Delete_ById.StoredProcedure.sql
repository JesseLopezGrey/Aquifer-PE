USE [CnmPro]
GO
/****** Object:  StoredProcedure [dbo].[OrganizationFollowers_Delete_ById]    Script Date: 8/1/2022 4:32:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE proc [dbo].[OrganizationFollowers_Delete_ById]
	@OrganizationId int
	,@FollowerId int 

-- =============================================
-- Author:		<Jesse Lopez>
-- Create date: <20220723>
-- Description:	<Organization Follower>

-- MODIFIED BY: n/a
-- MODIFIED DATE: n/a
-- Code Reviewer: 
-- Note: 
-- =============================================
AS
/*------TEST CODE------

		Declare  @OrganizationId int = 1
		        ,@FollowerId int = 4
 
		  	Select *
		From dbo.OrganizationFollowers
		Where OrganizationId = @OrganizationId
		AND FollowerId = @FollowerId 

		Execute dbo.OrganizationFollowers_Delete_ById
			@OrganizationId
			,@FollowerId
		
		Select *
		From dbo.OrganizationFollowers
		Where OrganizationId = @OrganizationId
		AND FollowerId = @FollowerId
		
			Select *
		From dbo.OrganizationFollowers
		   
*/
BEGIN

DELETE FROM [dbo].[OrganizationFollowers]
    Where OrganizationId = @OrganizationId
		AND FollowerId = @FollowerId

END


GO
