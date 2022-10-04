using eDoc_APP.Services;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json;
using Scrypt;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using System.Web.Services.Description;

namespace eDoc_APP.Utilities
{
    /// <summary>
    /// Extension Method
    /// </summary>
    public static class Extension
    {

        /// <summary>
        /// Property DI
        /// </summary>
        public static IServiceProvider ServiceProvider { get; set; }

        /// <summary>
        /// Hàm tạo ra QrCode
        /// </summary>
        /// <param name="textToQrCode"></param>
        /// <param name="pathToSave">Nếu không truyền vào sẽ lưu vào thư mục Bin của Project</param>

        private static void CreateQrCode(string textToQrCode, string pathToSave = "")
        {
            var url = string.Format("http://chart.apis.google.com/chart?cht=qr&chs={1}x{2}&chl={0}", textToQrCode, 250, 250);
            WebResponse response = default(WebResponse);
            Stream remoteStream = default(Stream);
            StreamReader readStream = default(StreamReader);
            WebRequest request = WebRequest.Create(url);
            response = request.GetResponse();
            remoteStream = response.GetResponseStream();
            readStream = new StreamReader(remoteStream);
            System.Drawing.Image img = System.Drawing.Image.FromStream(remoteStream);
            //Nếu không có Path sẽ lưu vào thư mục Bin
            if (pathToSave == "")
                img.Save(Path.GetFileNameWithoutExtension(textToQrCode) + ".png");
            else
            {
                string fileName = Path.Combine(pathToSave, Path.GetFileNameWithoutExtension(textToQrCode) + ".png");
                img.Save(fileName);
            }
            response.Close();
            remoteStream.Close();
            readStream.Close();
        }
        /// <summary>
        /// Get Services
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static T GetServices<T>(this T input)
        {
            T result = (T)ServiceProvider.GetService(typeof(T));
            return result;
        }
        /// <summary>
        /// Thuật toán Hash an toàn của thư viện Scrypt.NET
        /// </summary>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string ToScryptEncode(this string encode)
        {
            ScryptEncoder encoder = new ScryptEncoder();
            string hashsedPassword = encoder.Encode(encode);
            return hashsedPassword;
        }
        /// <summary>
        /// So sánh password đã hash lưu trong db và hash mới. Không thể so sánh == hoặc Equals
        /// </summary>
        /// <param name="hashedPassword"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool IsEqualPassword(this string hashedPassword, string password)
        {
            ScryptEncoder encoder = new ScryptEncoder();
            bool areEquals = encoder.Compare(password, hashedPassword);
            return areEquals;
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789~!@#$%&*()_+=-?<>abcdefghijklmnopqrsutwxyz";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        /// <summary>
        /// Convert Object/List To Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToJson<T>(this T input) 
        {
            if (input == null)
                return "{}";
            return JsonConvert.SerializeObject(input);
        }
        public static bool SendMail(List<string> lstEmail, string subject, string body, Dictionary<string, Stream> fileStreams = null)
        {
            try
            {
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.EnableSsl = true; smtpClient.Host = ConfigurationManager.AppSettings["SmtpHost"]; smtpClient.Port = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]); smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SmtpUserName"], ConfigurationManager.AppSettings["SmtpPassword"]);
                    var msg = new MailMessage
                    {
                        IsBodyHtml = true,
                        BodyEncoding = Encoding.UTF8,
                        From = new MailAddress(ConfigurationManager.AppSettings["SmtpUserName"]),
                        Subject = subject,
                        Body = body,
                        Priority = MailPriority.Normal,
                    };
                    foreach (string item in lstEmail)
                    {
                        msg.To.Add(item);
                    }
                    //foreach (var item in fileStreams)
                    //{
                    //    msg.Attachments.Add(new Attachment(item.Value, item.Key));
                    //}

                    smtpClient.Send(msg); return true;
                }
            }
            catch (Exception ex) { LogHelper.LogExceptionMessage(ex); return false; }
        }
        public static Image CreateQRCode(string link)
        {
            var url = string.Format("http://chart.apis.google.com/chart?cht=qr&chs={1}x{2}&chl={0}", link, 250, 250);
            WebResponse response = default(WebResponse);
            Stream remoteStream = default(Stream);
            StreamReader readStream = default(StreamReader);
            WebRequest request = WebRequest.Create(url);
            response = request.GetResponse();
            remoteStream = response.GetResponseStream();
            readStream = new StreamReader(remoteStream);
            Image img = Image.FromStream(remoteStream);
            response.Close();
            remoteStream.Close();
            readStream.Close();
            return img;
        }

    }
}