using System.ComponentModel.DataAnnotations;

namespace ImageProcessingAPI.Models
{
    public class CourseCompletion
    {
        [Required]
        public string StudentName {  get; set; }
        public string CourseName { get; set; }
        //public IFormFile? Signature { get; set; }

        public CourseCompletion()
        {
            StudentName ??= "";
            CourseName ??= "";
        }
    }
}
