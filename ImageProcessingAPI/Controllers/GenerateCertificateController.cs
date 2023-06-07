using ImageMagick;
using ImageProcessingAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ImageProcessingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenerateCertificateController : ControllerBase
    {
        readonly IWebHostEnvironment _hostingEnvironment;

        public GenerateCertificateController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("")]
        public void GenerateCertificate(CourseCompletion data)
        {
            using (var image = new MagickImage("C:/Users/Dell/Desktop/certificate.jpg"))
            {
                // adding student name
                using (var nameAdded = new MagickImage("xc:none", 1300, 900))
                {
                    nameAdded.Draw(new Drawables()
                        .FillColor(new MagickColor("Black"))
                        .Gravity(Gravity.Center)
                        .FontPointSize(40)
                        .Text(0, 0, data.StudentName));

                    image.Composite(nameAdded, CompositeOperator.Over);
                }

                // adding course title
                using (var courseAdded = new MagickImage("xc:none", 1300, 900))
                {
                    courseAdded.Draw(new Drawables()
                        .FillColor(new MagickColor("Black"))
                        .Gravity(Gravity.South)
                        .FontPointSize(40)
                        .Text(0, 250, data.CourseName));

                    image.Composite(courseAdded, CompositeOperator.Over);
                }

                // uploaded signature added
                using (var signAdded = new MagickImage("C:/Users/Dell/Desktop/signatures/signature.png", 1300, 900))
                {
                    signAdded.Resize(200, 0);
                    image.Composite(signAdded, 475, 740, CompositeOperator.Over);
                }


                string editedCertificate = Path.Combine(_hostingEnvironment.WebRootPath, "Certificates/EditedCertificate.jpg");
                image.Write(editedCertificate);
            }
        }
    }
}