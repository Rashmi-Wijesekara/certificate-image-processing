namespace CertificateImageProcessing.Helpers
{
    public class FilesHelper
    {
        private IWebHostEnvironment _hostingEnvironment;

        public FilesHelper(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task<string> UploadFileAsync(IFormFile file)
        {
            string extension = "";

            if(file.FileName.EndsWith(".png"))
            {
                extension = ".png";
            }
            else if (file.FileName.EndsWith(".jpg"))
            {
                extension = ".jpg";
            }
            else if (file.FileName.EndsWith(".jpeg"))
            {
                extension = ".jpeg";
            }
            else if (file.FileName.EndsWith(".svg"))
            {
                extension = ".svg";
            }

            //generate unique filename
            string fileName = Guid.NewGuid().ToString() + extension;

            //upload file to server
            string folderPath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads");
            string filePath = Path.Combine(folderPath, fileName);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return filePath;
        }
    }
}
