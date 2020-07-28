using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Repository.Common;
using TMDb.Model;
using TMDb.Service.Common;

namespace TMDb.Service
{
    public class FileStorageService : IFileStorageService
    {
        protected IFileStorageRepository fileStorageRepository
        { get; private set; }

        public FileStorageService(IFileStorageRepository fileStorageRepository)
        {
            this.fileStorageRepository = fileStorageRepository;
        }
        public async Task RemoveFileAsync(Guid fileID)
        {
            await fileStorageRepository.RemoveFileAsync(fileID);
        }
        public async Task InsertFileAsync(string imageName, string imagePath)
        {
            await fileStorageRepository.InsertFileAsync(imageName, imagePath);
        }
        public async Task<FileStorage> ReturnFileByIdAsync(Guid fileID)
        {
            return await fileStorageRepository.ReturnFileByIdAsync(fileID);
        }

    }
}
