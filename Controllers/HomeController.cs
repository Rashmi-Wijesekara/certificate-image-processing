using CertificateImageProcessing.Helpers;
using CertificateImageProcessing.Models;
using ImageMagick;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CertificateImageProcessing.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        readonly IWebHostEnvironment _hostingEnvironment;
        readonly FilesHelper _filesHelper;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
            _filesHelper = new FilesHelper(_hostingEnvironment);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] InputForm data)
        {
            //Debug.WriteLine(data.StudentName + "," + data.CourseName + "," + data.Signature.FileName);

            if(data.Signature == null)
            {
                ViewData["fileTypeError"] = "Please upload the digital signature";
                return View(data);
            }
            else
            {
                if (!data.Signature.FileName.EndsWith(".png") && !data.Signature.FileName.EndsWith(".jpg") && !data.Signature.FileName.EndsWith(".jpeg") && !data.Signature.FileName.EndsWith(".svg"))
                {
                    ViewData["fileTypeError"] = "Allowed File Types: .png, .jpg, .jpeg, .svg ONLY";
                    return View(data);
                }
            }
            string signaturePath = await _filesHelper.UploadFileAsync(data.Signature);

            string certificateFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/certificate.jpg");

            //string signature = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/signature.png");

            using (var image = new MagickImage(certificateFilePath))
            {
                // adding student name
                using(var nameAdded = new MagickImage("xc:none", 1300, 900))
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
                using (var signAdded = new MagickImage(signaturePath, 1300, 900))
                {
                    signAdded.Resize(200, 0);
                    image.Composite(signAdded, 475, 740, CompositeOperator.Over);
                }


                string editedCertificate = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/EditedCertificate.jpg");
                image.Write(editedCertificate);
            }

            return RedirectToAction("Certificate");
        }

        public IActionResult Certificate()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}