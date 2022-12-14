CREATE PROCEDURE [dbo].[GS_GetProcessDocument]
    @DocumentId AS INT = 0
AS
BEGIN
/*
	--Author   : longnguyen2@genesolutions.vn
	--Create On: 26/09/2022
	--Version  : 1.0.0 Init Stored Procedure
*/
------########################DEBUGGER############################-------
--DECLARE @DocumentId AS INT = 6
------########################DEBUGGER############################-------
    SELECT s.StepIndex,
		   AprDoc.StateApprove,
		   AprDoc.Email,
		   AprDoc.Description,
		   Doc.Step
	FROM dbo.Step s 
	JOIN dbo.ApproveDocument AprDoc ON AprDoc.StepId = s.StepId
	JOIN dbo.Document Doc ON Doc.DocumentId = AprDoc.DocumentId
	WHERE AprDoc.DocumentId=@DocumentId;
END
