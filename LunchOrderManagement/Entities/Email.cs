using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchOrderManagement.Entities
{
    public class Email
    {
        private string _emailTo;
        private string _emailSubject;
        private string _emailContent;
        private IFormFile _emailAttachment;
        private string _emailSender;
        private string _password;
        private bool _isConfirmEmail;

        public string EmailTo { get => _emailTo; set => _emailTo = value; }
        public string EmailSubject { get => _emailSubject; set => _emailSubject = value; }
        public string EmailContent { get => _emailContent; set => _emailContent = value; }
        public IFormFile EmailAttachment { get => _emailAttachment; set => _emailAttachment = value; }
        public string EmailSender { get => _emailSender; set => _emailSender = value; }
        public string Password { get => _password; set => _password = value; }
        public bool IsConfirmEmail { get => _isConfirmEmail; set => _isConfirmEmail = value; }
    }
}
