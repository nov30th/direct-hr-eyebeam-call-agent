using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;


namespace DHRSoftphone.SoftphoneAgent_QZJ
{
    public class ContactsCSVBuilder
    {
        StringBuilder _csvContactsContent = new StringBuilder();
        StringBuilder _csvContactsContentWithNameStyle = new StringBuilder();

        public StringBuilder CsvContactsContent
        {
            get { return _csvContactsContent; }
            set { _csvContactsContent = value; }
        }

        public StringBuilder CsvContactsContentWithNameStyle
        {
            get { return _csvContactsContentWithNameStyle; }
            set { _csvContactsContentWithNameStyle = value; }
        }


        string _savePath = Path.Combine(System.Environment.GetEnvironmentVariable("TEMP"), "softphoneTempPic.dat");


        public void AddPerson(UserContact detail)
        {
            string photoBase64;
            StringBuilder googlecontacts = new StringBuilder();
            googlecontacts.AppendLine("BEGIN:VCARD");
            googlecontacts.AppendLine("VERSION:3.0");

            googlecontacts.AppendFormat("FN;CHARSET=UTF-8:{0}\r\n", detail.Name + " | Direct HR");
            googlecontacts.AppendFormat("N;CHARSET=UTF-8:{0}\r\n", detail.Name);
            if (detail.Email.Length > 3)
                foreach (var email in detail.Email.Split(','))
                {
                    googlecontacts.AppendFormat("EMAIL;TYPE=PREF,INTERNET:{0}\r\n", email);
                }

            if (detail.Cellphone.Length > 3)
            {
                foreach (var cell in detail.Cellphone.Split(','))
                {
                    googlecontacts.AppendFormat("TEL;TYPE=CELL,VOICE:{0}\r\n", cell).Replace("(86)", "+86");
                }
            }

            try
            {
                //savingPath = ReplaceBadCharOfFileName(savingPath);
                HttpWebRequest web2 = (HttpWebRequest)WebRequest.Create(detail.BigPhoto);
                web2.Timeout = 14000;
                web2.Method = "GET";
                //web.ContentType = "application/x-www-form-urlencoded";
                web2.AllowAutoRedirect = true;
                using (var response2 = web2.GetResponse())
                {
                    //var fileBin = new byte[response2.ContentLength+1];
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (var receiveStream = response2.GetResponseStream())
                        {
                            var buff = new byte[64];
                            int c = 0; //实际读取的字节数
                            int total = 0;//total
                            //receiveStream.Read(buff, 0, Convert.ToInt32(response.ContentLength));
                            File.Delete(_savePath);
                            using (FileStream fs = new FileStream(_savePath, System.IO.FileMode.CreateNew))
                            {
                                using (BinaryWriter w = new BinaryWriter(fs))
                                {
                                    //c = receiveStream.Read(fileBin, 0, fileBin.Length);
                                    //w.Write(fileBin, 0, c);
                                    while ((c = receiveStream.Read(buff, 0, buff.Length)) > 0)
                                    {
                                        w.Write(buff, 0, c);
                                        ms.Write(buff, 0, c);
                                        total += c;
                                    }
                                    w.Close();
                                }
                                fs.Close();
                            }
                            //using (var readStream = new StreamReader(receiveStream, Encoding.UTF8))
                            //    responseText = readStream.ReadToEnd();
                            receiveStream.Close();
                        }
                        photoBase64 = EncodeBase64(ms.ToArray());
                        ms.Close();
                        response2.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                new AgentExceptions.AgentException("Failed fetch photo to CSV data", ex);
                photoBase64 = string.Empty;
            }


            if (photoBase64.Length > 3  && detail.Name != "Vincent Qiu")
                googlecontacts.AppendFormat("PHOTO;ENCODING=BASE64;TYPE=JPEG:{0}\r\n", photoBase64);
            googlecontacts.AppendFormat("NOTE: Export from Direct HR Soft Phone Agent\r\n");
            googlecontacts.AppendLine("END:VCARD\r\n");
            _csvContactsContentWithNameStyle.AppendLine(googlecontacts.ToString());
            _csvContactsContent.AppendLine(googlecontacts.ToString().Replace(" | Direct HR", string.Empty));
        }


        public string EncodeBase64(byte[] bytes)
        {
            string encode = "";
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = string.Empty;
            }
            return encode;
        }

        public string DecodeBase64(string code_type, string code)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(code);
            try
            {
                decode = Encoding.GetEncoding(code_type).GetString(bytes);
            }
            catch
            {
                decode = code;
            }
            return decode;
        }


        /// <summary>
        /// 去掉文件名中的无效字符,如 \ / : * ? " < > | 
        /// </summary>
        /// <param name="fileName">待处理的文件名</param>
        /// <returns>处理后的文件名</returns>
        public static string ReplaceBadCharOfFileName(string fileName)
        {
            string str = fileName;
            str = str.Replace("\\", string.Empty);
            str = str.Replace("/", string.Empty);
            str = str.Replace(":", string.Empty);
            str = str.Replace("*", string.Empty);
            str = str.Replace("?", string.Empty);
            str = str.Replace("\"", string.Empty);
            str = str.Replace("<", string.Empty);
            str = str.Replace(">", string.Empty);
            str = str.Replace("|", string.Empty);
            str = str.Replace(" ", string.Empty);
            str = str.Replace(".", string.Empty);
            str = str.Replace("-", string.Empty);
            return str;
        }
    }
}