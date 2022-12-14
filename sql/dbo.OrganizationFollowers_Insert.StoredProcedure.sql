USE [CnmPro]
GO
/****** Object:  StoredProcedure [dbo].[OrganizationFollowers_Insert]    Script Date: 8/1/2022 4:32:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[OrganizationFollowers_Insert]
            @OrganizationId int
           ,@FollowerId int

-- =============================================
-- Author:		<Jesse Lopez>
-- Create date: <20220720>
-- Description:	<Inser Organization Follower >

-- MODIFIED BY: n/a
-- MODIFIED DATE: n/a
-- Code Reviewer: 
-- Note: 
-- =============================================
AS
/*-----TESTCODE-----
Declare  @OrganizationId int =  1
Declare  @FollowerId int = 20
          				
EXECUTE dbo.OrganizationFollowers_Insert 
@OrganizationId
,@FollowerId
 
SELECT * FROM dbo.OrganizationFollowers
Where OrganizationId = @OrganizationId
 
------------*/
BEGIN
	

INSERT INTO [dbo].[OrganizationFollowers]
           ([OrganizationId]
           ,[FollowerId])
     VALUES
           (@OrganizationId
           ,@FollowerId)

END


GO
