using AutoMapper;
using eDoc_Core.Models.Entities;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.qrcode;
using Microsoft.Office.Interop.Word;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Pdf.Exporting.XPS.Schema;
using Spire.Pdf.Security;
using Spire.Pdf.Tables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using Image = System.Drawing.Image;
using Path = System.IO.Path;

namespace eDoc_APP.Services
{
    public class OfficeServices : BaseServices, IOfficeServices
    {
        public OfficeServices(IMapper mapper, eDocumentContext db) : base(mapper, db)
        {

        }
        public async Task<bool> GeneralImage(HttpServerUtilityBase Server)
        {
            // BarcodeQRCode qrcode = new BarcodeQRCode("https://royaldragonit.com", 50, 50, null);
            //Image img1 = qrcode.GetImage();
            //iTextSharp.text.Document doc = new iTextSharp.text.Document();
            //try
            //{
            //    PdfWriter.GetInstance(doc, new FileStream(@"D:\Data\Documentation\test-qrcodepdf.pdf", FileMode.Open)); 
            //    doc.Open();
            //    doc.Add(new Paragraph("QrCode"));
            //    doc.Add(img1);
            //    MemoryStream ms = new MemoryStream(img1.OriginalData);
            //    System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
            //    doc.Close();
            //    //img = returnImage.Save(new Stream(), System.Drawing.Imaging.ImageFormat.Jpeg);
            //}
            //catch (Exception ez)
            //{

            //}
            return true;
        }

        public string InsertQrCodeAndLinkToDocx(Image image, string filePathDocument,string link)
        {
            Spire.Doc.Document doc = new Spire.Doc.Document();
            doc.LoadFromFile(filePathDocument);
            // Find Text
            TextSelection[] textSelectionQrCodes = doc.FindAllString("{QrCode}", true, true);
            doc.Replace("{Link}",link, true, true);
            //TextSelection[] textReg = doc.FindAllPattern(new Regex("Sinh?.."));
            TextRange range = null;
            int index = 0;
            //Highlight Text
            foreach (TextSelection selection in textSelectionQrCodes)
            {
                DocPicture pic = new DocPicture(doc);
                pic.LoadImage(image);
                range = selection.GetAsOneRange();
                index = range.OwnerParagraph.ChildObjects.IndexOf(range);
                range.OwnerParagraph.ChildObjects.Insert(index, pic);
                range.OwnerParagraph.ChildObjects.Remove(range);
            }
            string path = Path.GetDirectoryName(filePathDocument);
            //Convert Word To Pdf
            string fileName = Path.Combine(path,  Path.GetFileNameWithoutExtension(filePathDocument) + ".pdf");
            //Save Document
            ToPdfParameterList toPdf = new ToPdfParameterList();
            toPdf.PdfSecurity.Encrypt("", "haha", PdfPermissionsFlags.Print, PdfEncryptionKeySize.Key128Bit);
            doc.SaveToFile(fileName, toPdf);
            // System.Diagnostics.Process.Start("FindText.docx");
            return fileName;
        }
        public string Insert(Image image, string filePathDocument, string link)
        {
            Application wordApp = new Application();
            Microsoft.Office.Interop.Word.Document wordDoc = wordApp.Documents.Open(filePathDocument);
            Range docRange = wordDoc.Range();

            string imagePath = @"c:\temp\win10.jpg";

            // Create an InlineShape in the InlineShapes collection where the picture should be added later
            // It is used to get automatically scaled sizes.
            InlineShape autoScaledInlineShape = docRange.InlineShapes.AddPicture(filePathDocument);
            float scaledWidth = autoScaledInlineShape.Width;
            float scaledHeight = autoScaledInlineShape.Height;
            autoScaledInlineShape.Delete();

            // Create a new Shape and fill it with the picture
            Shape newShape = wordDoc.Shapes.AddShape(1, 0, 0, scaledWidth, scaledHeight);
            newShape.Fill.UserPicture(imagePath);

            // Convert the Shape to an InlineShape and optional disable Border
            InlineShape finalInlineShape = newShape.ConvertToInlineShape();
            //finalInlineShape.Line.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;

            // Cut the range of the InlineShape to clipboard
            finalInlineShape.Range.Cut();

            // And paste it to the target Range
            docRange.Paste();

            wordDoc.SaveAs2(@"c:\temp\test.docx");
            wordApp.Quit();
            return null;
        }
        //private Image CreateBarcode(float xScale, float yScale, PdfDocument pdfDoc)
        //{
        //    BarcodePDF417 barcode = new BarcodePDF417();


        //    Image barcodeImage = new Image();
        //    return barcodeImage;
        //}
        public string InsertQrcodeToPdf(string pdfPath)
        {
            pdfPath = @"C:\Users\Admin\Downloads\New folder\Sample.pdf";
            string pdfFileOut = @"C:\Users\Admin\Downloads\New folder\Sample_barcode.pdf";
            PdfReader pdfReader = new PdfReader(pdfPath);                    
            PdfStamper pdfStamper = new PdfStamper(pdfReader, new System.IO.FileStream(pdfFileOut, System.IO.FileMode.Create, System.IO.FileAccess.Write));
            // PdfContentByte pdfContentByte = pdfStamper.GetOverContent(1);
            // float pageHeight = pdfReader.GetPageSize(1).Height;
            // float pageWidth = pdfReader.GetPageSize(1).Width;
            
          

            return "";
        }

    }
    public interface IOfficeServices
    {
        Task<bool> GeneralImage(HttpServerUtilityBase Server);
        string InsertQrCodeAndLinkToDocx(Image image, string filePathDocument, string link);
        string Insert(Image image, string filePathDocument, string link);
        string InsertQrcodeToPdf(string pdfPath);
    }
}