using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows;

namespace KBJ_MessengerByTCP
{
    class EmailHandler
    {
        public static void sendEmail(string filePath)
        {
            string emailAddress_KBJ = "kbj0109.practice@gmail.com";
            string emailPassword_KBJ = "kbjpractice";
            string emailAddressToReceive = "bighead2000@naver.com";

            MailMessage mail = new MailMessage();

            //메일 내용
            mail.From = new MailAddress(emailAddress_KBJ);
            mail.To.Add(emailAddressToReceive);
            mail.Subject = LanguageResource.language_res.strEmailSubject;
            mail.Body = LanguageResource.language_res.strEmailBody + filePath;

            //첨부 파일
            System.Net.Mail.Attachment attachedFile = new System.Net.Mail.Attachment(filePath);
            mail.Attachments.Add(attachedFile);

            //gmail 포트 설정
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            //SSL 설정 (gmail은 기본적으로 true 이지만, 메일 서버 세팅에 따라 다르다)
            smtp.EnableSsl = true;

            //gmail 인증에 필요한 아이디와 패스워드
            smtp.Credentials = new NetworkCredential(emailAddress_KBJ, emailPassword_KBJ);
            try
            {
                smtp.Send(mail);
            }
            catch (SmtpException ex)
            {
                MessageBox.Show(LanguageResource.language_res.strExceptionMessageSendingEmailFailed);
            }

        }
    }
}
