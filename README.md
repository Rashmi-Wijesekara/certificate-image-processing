## Certificate Image Processing

This project enables the generation of customized certificate images by adding student names, course names, and digital signatures to a certificate template image. It provides two implementations: MVC and API.

### MVC Implementation:

![1](https://i.imgur.com/CRmNVqq.jpg)
![2](https://imgur.com/z3Yylay.jpg)

- **Web Interface**: Users can upload a digital signature image and enter student and course names on a form.
- **Image Generation**: The project uses MagickImage package to manipulate the certificate template image, overlay text values, and add the digital signature image.
- **Dynamic Rendering**: The generated certificate image is displayed on the web interface.

### API Implementation:
- **RESTful Endpoint**: JSON payloads containing student and course names can be sent via a POST request.
- **Image Generation**: The API uses MagickImage package to process the certificate template image, incorporate the provided text values, and add the digital signature image.
- **Response**: The API returns the generated certificate image.

**Features:**
- Image Manipulation: The project applies text overlay and signature integration techniques to create personalized certificates.
- MagickImage Package: The powerful image processing library used for tasks like resizing, cropping, text overlay, and image composition.
