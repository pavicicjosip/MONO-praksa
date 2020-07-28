using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TMDb.Model;
using TMDb.Service.Common;
using AutoMapper;
using System.Threading.Tasks;

namespace TMDb.WebAPI.Controllers
{
    public class FileStorageController : ApiController
    {
        /*
        - Get po id - paziti da vrati onu putanju ++
        - DELETE ++
        - INSERT  ++
        */

        protected IFileStorageService fileStorageService
        { get; private set; }

        static MapperConfiguration Mapper = new MapperConfiguration(cfg => cfg.CreateMap<Review, RestReview>().ReverseMap());

        public FileStorageController(IFileStorageService fileStorageService)
        {
            this.fileStorageService = fileStorageService;
        }

        [HttpDelete]
        [Route("api/FileStorage/{fileID}")]
        public async Task<HttpResponseMessage> RemoveFileAsync(Guid fileID)
        {
            await fileStorageService.RemoveFileAsync(fileID);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("api/FileStorage")]
        public async Task<HttpResponseMessage> InsertFileAsync(string imageName, string imagePath)
        {
            await fileStorageService.InsertFileAsync(imageName, imagePath);
            return Request.CreateResponse(HttpStatusCode.OK, String.Format("{0} {1} inserted in the database", imageName, imagePath));
        }

        [Route("api/FileStorage")]
        public async Task<HttpResponseMessage> ReturnFileByIdAsync(Guid fileID)
        {
            var mapper = Mapper.CreateMapper();
            RestFileStorage file = mapper.Map<RestFileStorage>(await fileStorageService.ReturnFileByIdAsync(fileID));
            var Path = System.Web.Hosting.HostingEnvironment.MapPath("~/" + file.ImagePath + file.ImageName);
            if (file == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, String.Format("There is no file with Id: {0}", fileID));
            }
            return Request.CreateResponse(HttpStatusCode.OK, Path);
        }
        public class RestFileStorage
        {
            public string ImageName { get; set; }
            public string ImagePath { get; set; }
            public RestFileStorage(string imageName, string imagePath)
            {
                this.ImageName = imageName;
                this.ImagePath = imagePath;
            }
        }
    }
}
