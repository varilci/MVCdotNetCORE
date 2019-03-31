using Microsoft.AspNetCore.Http;
using System;

namespace Homework1_495.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    public class UploadImageTest
    {
        public string ImageCaption { set; get; }
        public string ImageDescription { set; get; }
        public IFormFile UploadedImage { set; get; }
    }
}