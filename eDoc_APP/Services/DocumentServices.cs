using AutoMapper;
using eDoc_APP.Utilities;
using eDoc_Core.Commons.Const;
using eDoc_Core.Core;
using eDoc_Core.Models.ApproveProcessModels;
using eDoc_Core.Models.CustomModels;
using eDoc_Core.Models.DocumentModels;
using eDoc_Core.Models.Entities;
using eDoc_Core.Models.Entities.StoredProcedures;
using eDoc_Core.Models.Entities.Views;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace eDoc_APP.Services
{
    public class DocumentServices : BaseServices, IDocumentServices
    {
        private readonly IOfficeServices _officeServices;
        public DocumentServices(IMapper mapper, eDocumentContext db, IOfficeServices officeServices) : base(mapper, db)
        {
            _officeServices = officeServices;
            //_officeServices.Insert(null, @"D:\Data\Documentation\hop-dong-mua-ban-xe\hop-dong-mua-ban-xe.doc", null);
        }
        public async Task<bool> Create(CreateDocumentModel create)
        {
            TypeDocument typeDocument = await _db.TypeDocuments.FindAsync(create.TypeDocumentId);
            if (typeDocument == null)
            {
                return false;
            }
            string rootPath = create.Server.MapPath("~/Content/Document");
            Document doc = new Document();
            doc.ApproveProcessId = typeDocument.ApproveProcessId;
            doc.TypeDocumentId = create.TypeDocumentId;
            doc.Email = create.Email;
            doc.Description = create.Description;
            doc.Step = 1;
            doc.IsApprove = false;
            doc.IsCompleted = false;
            _db.Documents.Add(doc);
            List<Step> lstSteps = await _db.Steps.Where(x => x.ApproveProcessId == typeDocument.ApproveProcessId).AsNoTracking().ToListAsync();
            var listStepIds = lstSteps.Select(y => y.StepId);
            List<Approve> lstApproves = await _db.Approves
                .Where(x => listStepIds.Contains(x.StepId))
                .AsNoTracking().ToListAsync();
            lstApproves.ForEach(x =>
            {
                ApproveDocument approveDocument = new ApproveDocument();
                approveDocument.StateApprove = 0;//chờ duyệt
                approveDocument.Document = doc;
                approveDocument.StepId = x.StepId;
                approveDocument.Email = x.Email;
                _db.ApproveDocuments.Add(approveDocument);
            });
            bool isHavePrimaryFile = false;
            for (int i = 0; i < create.Files.Count; i++)
            {
                //Đổi tên file để lưu không trùng tên.
                string fileName = Guid.NewGuid().ToString().ToLower() + create.Files[i].FileName;
                string fullPathFile = Path.Combine(rootPath, fileName);
                DocumentFile documentFile = new DocumentFile();
                documentFile.FileNameInServer = fullPathFile;
                documentFile.FileName = fileName;
                documentFile.CreateOn = DateTime.Now;
                if (!isHavePrimaryFile && create.Files.Keys[i] == "filePrimary")
                {
                    create.Files[i].SaveAs(fullPathFile);
                    isHavePrimaryFile = true;
                    documentFile.IsPrimaryFile = true;
                }
                else
                    documentFile.IsPrimaryFile = false;
                documentFile.Document = doc;
                _db.DocumentFiles.Add(documentFile);
            }
            int save = await _db.SaveChangesAsync();
            return save > 0;
        }

        public async Task<List<GS_GetListDocumentByEmail>> List(string email, string filter)
        {
            int stateApprove = -1;
            switch (filter)
            {
                case "all":
                    stateApprove = -1;
                    break;
                case "approve":
                    stateApprove = 1;
                    break;
                case "reject":
                    stateApprove = 2;
                    break;
                case "wait":
                    stateApprove = 0;
                    break;
                default:
                    stateApprove = -1;
                    break;
            }
            var data = await _db.ToListAsync<GS_GetListDocumentByEmail>(email, stateApprove);
            return data;
        }

        public async Task<List<SelectListItem>> SelectListApproveProcess(int approveProcessId = 0)
        {
            var data = await _db.ApproveProcesss.Select(x => new SelectListItem
            {
                Text = x.Name,
                Selected = x.ApproveProcessId == approveProcessId,
                Value = x.ApproveProcessId.ToString()
            }).ToListAsync();
            return data;
        }
        public async Task<List<SelectListItem>> SelectListTypeDocument(int typeDocumentId = 0)
        {
            var data = await _db.TypeDocuments.Select(x => new SelectListItem
            {
                Text = x.Name,
                Selected = x.TypeDocumentId == typeDocumentId,
                Value = x.TypeDocumentId.ToString()
            }).ToListAsync();
            return data;
        }
        public async Task<vDocument> Detail(int documentId, string email)
        {
            var data = await _db.Views<vDocument>($"SELECT * FROM vDocument WHERE DocumentId={documentId} AND EmailRererence='{email}'").FirstOrDefaultAsync();
            return data;
        }

        public async Task<ResultCustomModel<bool>> ActionApprove(ApproveActionModel approveModel)
        {
            #region Validate Input

            Document doc = await _db.Documents.FirstOrDefaultAsync(x => x.DocumentId == approveModel.DocumentId);
            if (doc == null)
                return new ResultCustomModel<bool>
                {
                    Code = 400,
                    Success = false,
                    Message = "Không tìm thấy tài liệu"
                };
            if (doc.IsCompleted)
                return new ResultCustomModel<bool>
                {
                    Code = 400,
                    Success = false,
                    Message = "Tài liệu này đã hoàn thành"
                };
            Step step = await _db.Steps.AsNoTracking().FirstOrDefaultAsync(x => x.ApproveProcessId == doc.ApproveProcessId && x.StepIndex == doc.Step);
            if (step == null)
                return new ResultCustomModel<bool>
                {
                    Code = 400,
                    Success = false,
                    Message = "Không tìm thấy công đoạn!"
                };
            Approve approve = await _db.Approves.AsNoTracking().FirstOrDefaultAsync(x => x.StepId == step.StepId && x.Email == approveModel.Email);
            if (approve == null)
                return new ResultCustomModel<bool>
                {
                    Code = 400,
                    Success = false,
                    Message = "Không tìm thấy quyền sở hữu của bạn trong tài liệu tại bước " + doc.Step
                };
            #endregion
            ApproveDocument apvDoc = await _db.ApproveDocuments.FirstOrDefaultAsync(x => x.StepId == approve.StepId && x.DocumentId == doc.DocumentId && x.Email == approveModel.Email);
            apvDoc.StateApprove = approveModel.Type == "approve" ? (short)1 : (short)2;
            apvDoc.Description = approveModel.Description;
            _db.Entry(apvDoc).State = EntityState.Modified;
            //Tổng số người phê duyệt
            int numberApproved = await _db.ApproveDocuments.Where(x => x.StepId == approve.StepId && x.DocumentId == doc.DocumentId && x.StateApprove == 1).CountAsync();
            if (approveModel.Type == "approve")
                numberApproved++;
            //Tổng số người bao gồm phê duyệt, chưa phê duyệt và từ chối
            int numberApprove = await _db.ApproveDocuments.Where(x => x.StepId == approve.StepId && x.DocumentId == doc.DocumentId).CountAsync();
            DocumentFile file = await _db.DocumentFiles.FirstOrDefaultAsync(x => x.DocumentId == doc.DocumentId);
            TypeDocument typeDocument = await _db.TypeDocuments.FirstOrDefaultAsync(x => x.TypeDocumentId == doc.TypeDocumentId);
            //Nếu số lượng Approve = tổng người approve thì tài liệu này tất cả những người khác đều approve
            //Hoặc không nhất thiết phải tất cả (IsAllAccept)
            if (numberApproved == numberApprove || !step.IsAllAccept)
            {
                //Nếu đang Step cuối thì Document IsCompleted ngược lại thì tăng Step lên
                int countStep = await _db.Steps.CountAsync(x => x.ApproveProcessId == doc.ApproveProcessId);
                if (doc.Step == countStep)
                {
                    doc.IsApprove = true;
                    doc.IsCompleted = true;
                    if (Path.GetExtension(file.FileName).ToLower() == ".docx")
                    {
                        _officeServices.InsertQrCodeAndLinkToDocx(Extension.CreateQRCode(Path.Combine(MessageConst.Host, "Content/Document/", file.FileName)), file.FileName, Path.Combine(MessageConst.Host, "Content/Document/", file.FileName));
                    }
                }
                else
                {
                    doc.Step++;
                    Step s = await _db.Steps.FirstOrDefaultAsync(x => x.StepIndex == doc.Step && x.ApproveProcessId == doc.ApproveProcessId);
                    if (s != null)
                    {
                        List<string> lstEmail = await _db.ApproveDocuments.Where(x => x.StepId == s.StepId).Select(x => x.Email).ToListAsync();
                        string content = await _db.EmailTemplates.Select(x => x.Content).FirstOrDefaultAsync();
                        if (file != null)
                        {
                            content.Replace("{Email}", approveModel.Email);
                            content.Replace("{Link}", Path.Combine(MessageConst.Host, "Content/Document/", file.FileName));
                            Extension.SendMail(lstEmail, $"[QUAN TRỌNG] - [Cần phê duyệt tài liệu {typeDocument.Name}]", content);
                        }

                    }
                }
                _db.Entry(doc).State = EntityState.Modified;
            }

            int save = await _db.SaveChangesAsync();
            return new ResultCustomModel<bool>
            {
                Code = 200,
                Success = true,
                Message = "Đã " + approveModel.Type + "!"
            };
        }
        public async Task<GS_SummaryDocument> SummaryDocument(string email)
        {
            var data = await _db.FirstOrDefaultAsync<GS_SummaryDocument>(email);
            return data;
        }
        public async Task<List<GS_GetProcessDocument>> GetProcessDocument(int documentId)
        {
            var data = await _db.ToListAsync<GS_GetProcessDocument>(documentId);
            return data;
        }
    }
    public interface IDocumentServices
    {
        Task<ResultCustomModel<bool>> ActionApprove(ApproveActionModel approveModel);
        Task<bool> Create(CreateDocumentModel create);
        Task<vDocument> Detail(int documentId, string email);
        Task<List<GS_GetProcessDocument>> GetProcessDocument(int documentId);
        Task<List<GS_GetListDocumentByEmail>> List(string email, string filter);
        Task<List<SelectListItem>> SelectListApproveProcess(int approveProcessId = 0);
        Task<List<SelectListItem>> SelectListTypeDocument(int typeDocumentId = 0);
        Task<GS_SummaryDocument> SummaryDocument(string email);
    }
}