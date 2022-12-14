CREATE PROCEDURE [dbo].[GS_GetListDocumentByEmail]
    @Email AS VARCHAR(300) = 'longnguyen2@genesolutions.vn',
	@StateApprove AS INT = -1
AS
BEGIN
/*
	--Author   : longnguyen2@genesolutions.vn
	--Create On: 22/09/2022
	--Version  : 1.0.0 Init Stored Procedure
	--Version  : 1.0.1 Add Param @StateApprove
*/
------########################DEBUGGER############################-------
--DECLARE @Email AS VARCHAR(300)='longnguyen2@genesolutions.vn'
--DECLARE @StateApprove AS INT =-1
------########################DEBUGGER############################-------
	SELECT Doc.DocumentId,
			   Doc.TypeDocumentId,
			   Doc.ApproveProcessId,
			   Doc.Description,
			   CONCAT(
						CAST(Doc.Step AS VARCHAR(2))
						,'/',
						CAST(
								(
									SELECT COUNT(1)
									FROM dbo.Step
									WHERE ApproveProcessId = Doc.ApproveProcessId
								) AS  VARCHAR(2))
					) AS Process,
			   AppDoc.Description AS DescriptionApprove,
			   Doc.IsActive,
			   Doc.CreateOn,
			   Doc.Email,
			   DocType.Name AS DocumentTypeName,
			   App.Name AS ApproveProcessName,
			   (
									SELECT COUNT(1)
									FROM dbo.DocumentFile df
									WHERE Doc.DocumentId = df.DocumentId
			  ) AS FileQuantity,
			   Doc.Step,
			   Doc.IsCompleted,
			   Doc.IsApprove,
			   AppDoc.StateApprove
		FROM dbo.Document Doc
		JOIN dbo.TypeDocument DocType ON DocType.TypeDocumentId = Doc.TypeDocumentId
		JOIN dbo.ApproveProcess App ON App.ApproveProcessId = Doc.ApproveProcessId
		--Chỉ lấy Step hiện hành
		JOIN dbo.Step s ON s.ApproveProcessId = App.ApproveProcessId --AND s.StepIndex=Doc.Step
		JOIN dbo.DocumentFile DocFile ON DocFile.DocumentId = Doc.DocumentId
		JOIN dbo.ApproveDocument AppDoc ON AppDoc.DocumentId = Doc.DocumentId AND AppDoc.Email=@Email
		WHERE @Email IN (SELECT AppSub.Email FROM dbo.Approve AppSub WHERE AppSub.StepId=s.StepId) 
		AND (@StateApprove=-1 OR AppDoc.StateApprove=@StateApprove)
		GROUP BY AppDoc.StateApprove,
                 Doc.DocumentId,
                 Doc.TypeDocumentId,
                 Doc.ApproveProcessId,
                 Doc.Description,
				 AppDoc.Description,
                 Doc.IsActive,
                 Doc.CreateOn,
                 Doc.Email,
                 DocType.Name,
                 App.Name,
                 Doc.Step,
				 Doc.IsApprove,
                 Doc.IsCompleted;
END
