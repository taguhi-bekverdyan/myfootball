using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFootballAdmin.Data.Services.ImageService
{
    public interface IImageService
    {
        Task<string> UploadImageAsync(Stream image, string fileName);
    }
}
