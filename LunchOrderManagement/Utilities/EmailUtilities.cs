using LunchOrderManagement.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LunchOrderManagement.Utilities
{
    public class EmailUtilities
    {
        public static bool SendEmail(Email email, bool isBodyHtml = false)
        {
            using (MailMessage newEmail = new MailMessage(from: email.EmailSender, to: email.EmailTo, subject: email.EmailSubject, body: email.EmailContent))
            {
                if (email.EmailAttachment != null)
                {
                    if (email.EmailAttachment.Length > 0)
                    {
                        string fileName = Path.GetFileName(email.EmailAttachment.FileName);
                        Attachment newAttachMent = new Attachment(email.EmailAttachment.OpenReadStream(), fileName);
                        newEmail.Attachments.Add(newAttachMent);
                    }
                }
                newEmail.IsBodyHtml = isBodyHtml;
                using (SmtpClient smtp = new SmtpClient())
                {
                    try
                    {
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential networdCre = new NetworkCredential(userName: email.EmailSender, password: email.Password);
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = networdCre;
                        smtp.Port = 587;
                        smtp.Send(newEmail);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }
        }

        public static bool SendConfirmEmail(AppIdentityUser user, string linkConfirm)
        {
            Email confirmEmail = new Email()
            {
                EmailSender = "smtp.test.codegym@gmail.com",
                Password = "Lovelykid1@",
                EmailSubject = "Verify email from LunchOrderCodeGym",
                EmailTo = user.Email,
                EmailContent = $"<span>Welcome {user.FirstName} {user.LastName} was registed in LunchOrderCodeGym,</span><br><br>" +
                                $"<span style=\"margin-left:20px;\">Please click this <strong><a href=\"{linkConfirm}\">link</a></strong> to active your account</span><br><br>" +
                                $"<span>Thank you!<span>",
                IsConfirmEmail = true
            };
            SendEmail(email: confirmEmail, isBodyHtml: true);
            return true;
        }

        public static bool SendConfirmChangeEmail(AppIdentityUser user, string linkConfirm)
        {
            Email confirmEmail = new Email()
            {
                EmailSender = "smtp.test.codegym@gmail.com",
                Password = "Lovelykid1@",
                EmailSubject = "Verify change email from LunchOrderCodeGym",
                EmailTo = user.Email,
                EmailContent = $"<span>Dear {user.FirstName} {user.LastName} was change your registered email in LunchOrderCodeGym,</span><br><br>" +
                                $"<span style=\"margin-left:20px;\">Please click this <strong><a href=\"{linkConfirm}\">link</a></strong> to confirm this action</span><br><br>" +
                                $"<span>Thank you!<span>",
                IsConfirmEmail = true
            };
            SendEmail(email: confirmEmail, isBodyHtml: true);
            return true;
        }



        public static bool SendConfirmChangePhoneNumber(AppIdentityUser user, string linkConfirm)
        {
            Email confirmEmail = new Email()
            {
                EmailSender = "smtp.test.codegym@gmail.com",
                Password = "Lovelykid1@",
                EmailSubject = "Verify change phone number from LunchOrderCodeGym",
                EmailTo = user.Email,
                EmailContent = $"<span>Welcome {user.FirstName} {user.LastName} was change your registered phone number in LunchOrderCodeGym,</span><br><br>" +
                                $"<span style=\"margin-left:20px;\">Please click this <strong><a href=\"{linkConfirm}\">link</a></strong> to confirm this action</span><br><br>" +
                                $"<span>Thank you!<span>",
                IsConfirmEmail = true
            };
            SendEmail(email: confirmEmail, isBodyHtml: true);
            return true;
        }
    }
}
