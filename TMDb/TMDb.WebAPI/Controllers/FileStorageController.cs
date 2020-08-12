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
using System.Web.Http.Cors;

namespace TMDb.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FileStorageController : ApiController
    {
        protected IFileStorageService FileStorageService
        { get; private set; }

        static MapperConfiguration Mapper = new MapperConfiguration(cfg => cfg.CreateMap<FileStorage, RestFileStorage>().ReverseMap());

        public FileStorageController(IFileStorageService fileStorageService)
        {
            this.FileStorageService = fileStorageService;
        }

        [HttpDelete]
        [Route("api/FileStorage/{fileID}")]
        public async Task<HttpResponseMessage> RemoveFileAsync(Guid fileID)
        {
            await FileStorageService.RemoveFileAsync(fileID);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("api/FileStorage")]
        public async Task<HttpResponseMessage> InsertFileAsync(string imageName, string imagePath)
        {
            await FileStorageService.InsertFileAsync(imageName, imagePath);
            return Request.CreateResponse(HttpStatusCode.OK, String.Format("{0} {1} inserted in the database", imageName, imagePath));
        }

        [HttpGet]
        [Route("api/FileStorage")]
        public async Task<HttpResponseMessage> ReturnFileByIdAsync(Guid fileID)
        {
            var mapper = Mapper.CreateMapper();
            RestFileStorage file = mapper.Map<RestFileStorage>(await FileStorageService.ReturnFileByIdAsync(fileID));
            if (file == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, String.Format("There is no file with Id: {0}", fileID));
            }
            //var Path = System.Web.Hosting.HostingEnvironment.MapPath("~/" + file.ImagePath + file.ImageName);
            var Path = file.ImagePath + file.ImageName;
            return Request.CreateResponse(HttpStatusCode.OK, Path);
        }

        [HttpPut]
        [Route("api/FileStorage/{FileID}")]
        public async Task<HttpResponseMessage> UpdateFileStorageAsync(Guid fileID, RestFileStorage restFileStorage)
        {
            var mapper = Mapper.CreateMapper();
            FileStorage fileStorage = mapper.Map<FileStorage>(restFileStorage);
            fileStorage.FileID = fileID;
            await FileStorageService.UpdateFileStorageAsync(fileStorage);
            return Request.CreateResponse(HttpStatusCode.OK);
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
