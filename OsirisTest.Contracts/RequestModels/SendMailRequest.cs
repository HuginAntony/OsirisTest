using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace OsirisTest.Contracts.RequestModels
{
    [DataContract]
    public class SendMailRequest
    {
        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string MailSubjectLine { get; set; }

        [Required]
        public string MailBody { get; set; }
    }
}
