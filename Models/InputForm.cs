using System.ComponentModel.DataAnnotations;

namespace CertificateImageProcessing.Models
{
    public class InputForm
    {
        [Required]
        public string StudentName {  get; set; }
        public string CourseName { get; set; }
        public IFormFile? Signature { get; set; }

        public InputForm()
        {
            StudentName ??= "";
            CourseName ??= "";
        }
    }
}
