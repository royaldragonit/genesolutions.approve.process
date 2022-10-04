CREATE VIEW vApproveProcess
AS
	/*
		--Author   : longnguyen2@genesolutions.vn
		--Create On: 21/09/2022
		--Version  : 1.0.0 Init View
	*/
    SELECT AP.ApproveProcessId,
           AP.Name,
           AP.Description,
           AP.CreateOn,
           AP.Email,
		   COUNT(S.StepId) AS Step
	FROM dbo.ApproveProcess AP
	LEFT JOIN dbo.Step S ON S.ApproveProcessId = AP.ApproveProcessId
	GROUP BY AP.ApproveProcessId,
             AP.Name,
             AP.Description,
             AP.CreateOn,
             AP.Email;
GO
