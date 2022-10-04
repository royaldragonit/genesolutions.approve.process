CREATE VIEW [dbo].[vDocument]
AS
	/*
		--Author   : longnguyen2@genesolutions.vn
		--Create On: 21/09/2022
		--Version  : 1.0.0 Init View
	*/
	SELECT Doc.DocumentId,
           Doc.TypeDocumentId,
           Doc.ApproveProcessId,
           Doc.Description,
           Doc.IsActive,
           Doc.CreateOn,
           Doc.Email,
		   DocType.Name AS DocumentTypeName,
		   App.Name AS ApproveProcessName,
		   COUNT(DocFile.DocumentFileId) AS FileQuantity,
		   Doc.Step,
		   Doc.IsCompleted,
		   ArvDoc.Email AS EmailRererence,
		   ArvDoc.StateApprove
	FROM dbo.Document Doc
	JOIN dbo.TypeDocument DocType ON DocType.TypeDocumentId = Doc.TypeDocumentId
	JOIN dbo.ApproveProcess App ON App.ApproveProcessId = Doc.ApproveProcessId
	JOIN dbo.DocumentFile DocFile ON DocFile.DocumentId = Doc.DocumentId
	JOIN dbo.ApproveDocument ArvDoc ON ArvDoc.DocumentId = Doc.DocumentId
	GROUP BY Doc.DocumentId,
             Doc.TypeDocumentId,
             Doc.ApproveProcessId,
			 Doc.Description,
             Doc.IsActive,
             Doc.CreateOn,
             Doc.Email,
             DocType.Name,
             App.Name,
             Doc.Step,
             Doc.IsCompleted,
			 ArvDoc.Email,
			 ArvDoc.StateApprove
GO
