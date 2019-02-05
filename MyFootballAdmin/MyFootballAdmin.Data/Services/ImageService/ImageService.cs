using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyFootballAdmin.Data.Services.ImageService
{
    public class ImageService : IImageService
    {
        private string url = "https://localhost:44350/api/ImageUpload/";
        public async  Task<string> UploadImageAsync(Stream image, string fileName)
        {

            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent())
            {
                HttpContent fileStreamContent = new StreamContent(image);
                fileStreamContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data") { Name = "file", FileName = fileName };
                fileStreamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                formData.Add(fileStreamContent);
                using (var response = await client.PostAsync(url, formData))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception(response.Content.ToString());
                    }
                    else
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                }


            }
        }
    }
}
