CREATE PROCEDURE [dbo].[GS_SummaryDocument]
    @Email AS VARCHAR(300) = ''
AS
BEGIN
/*
	--Author   : longnguyen2@genesolutions.vn
	--Create On: 22/09/2022
	--Version  : 1.0.0 Init Stored Procedure
*/
------########################DEBUGGER############################-------
--DECLARE @Email AS VARCHAR(300)='longnguyen2@genesolutions.vn'
------########################DEBUGGER############################-------
DECLARE @Total AS INT,@Wait AS INT, @Approve AS INT, @Reject AS INT;
 ;WITH cteDocument AS
     (
		SELECT Doc.DocumentId,
               Doc.TypeDocumentId,
               Doc.ApproveProcessId,
               Doc.Description,
               Doc.IsActive,
               Doc.CreateOn,
               Doc.Email,
               Doc.Step,
               Doc.IsCompleted,
               MAX(Apr.StateApprove) AS StateApprove
		FROM dbo.Document Doc
		JOIN dbo.Step s ON s.ApproveProcessId = Doc.ApproveProcessId
		JOIN dbo.ApproveDocument Apr ON Apr.StepId = s.StepId
		WHERE Apr.Email=@Email
		GROUP BY Doc.DocumentId,
                 Doc.TypeDocumentId,
                 Doc.ApproveProcessId,
                 Doc.Description,
                 Doc.IsActive,
                 Doc.CreateOn,
                 Doc.Email,
                 Doc.Step,
                 Doc.IsCompleted
     )
     SELECT
		 @Wait = SUM(
				 CASE WHEN cteDocument.StateApprove=0 THEN
				 1
				 ELSE
				 0
				 END
			) ,
		@Approve= SUM(
				 CASE WHEN cteDocument.StateApprove=1 THEN
				 1
				 ELSE
				 0
				 END
			),
		@Reject= SUM(
				 CASE WHEN cteDocument.StateApprove=2 THEN
				 1
				 ELSE
				 0
				 END
			),
		@Total= COUNT(1)
	 FROM cteDocument;
	 SELECT @Total AS TotalDocument, @Wait AS WaitForProcessDocument,@Approve AS ApproveDocument,@Reject AS RejectDocument;
END
