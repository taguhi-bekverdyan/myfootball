using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyFootballMvc.Services
{
    public class ImageUploadService
    {
        private string url = "https://localhost:44350/api/ImageUpload/";

        private const string PlayerDefaultAvatar = "https://res.cloudinary.com/myfootball-am/image/upload/v1550074966/default_player.png";
        private const string ManagerDefaultAvatar = "https://res.cloudinary.com/myfootball-am/image/upload/v1550074965/default_manager.png";
        private const string RefereeDefaultAvatar = "https://res.cloudinary.com/myfootball-am/image/upload/v1550074965/default_referee.png";
        private const string StaffDefaultAvatar = "https://res.cloudinary.com/myfootball-am/image/upload/v1550074965/default_staff.png";
        private const string TeamDefaultAvatar = "https://res.cloudinary.com/myfootball-am/image/upload/v1550074965/default_club.png";

        public async Task<string> UploadImageAsync(Stream image, string fileName)
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

        public Task<string> GetPlayerDefaultAvatarAsync()
        {
            return Task.Factory.StartNew(()=> {
                return PlayerDefaultAvatar;
            });
        }

        public Task<string> GetManagerDefaultAvatarAsync()
        {
            return Task.Factory.StartNew(() => {
                return ManagerDefaultAvatar;
            });
        }

        public Task<string> GetRefereeDefaultAvatarAsync()
        {
            return Task.Factory.StartNew(() => {
                return RefereeDefaultAvatar;
            });
        }

        public Task<string> GetStaffDefaultAvatarAsync()
        {
            return Task.Factory.StartNew(() => {
                return StaffDefaultAvatar;
            });
        }

        public Task<string> GetTeamDefaultAvatarAsync() {
            return Task.Factory.StartNew(()=> {
                return TeamDefaultAvatar;
            });
        }

    }
}
