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
        protected IFileStorageRepository FileStorageRepository
        { get; private set; }

        public FileStorageService(IFileStorageRepository fileStorageRepository)
        {
            this.FileStorageRepository = fileStorageRepository;
        }
        public async Task RemoveFileAsync(Guid fileID)
        {
            await FileStorageRepository.RemoveFileAsync(fileID);
        }
        public async Task InsertFileAsync(string imageName, string imagePath)
        {
            await FileStorageRepository.InsertFileAsync(imageName, imagePath);
        }
        public async Task<FileStorage> ReturnFileByIdAsync(Guid fileID)
        {
            return await FileStorageRepository.ReturnFileByIdAsync(fileID);
        }
        public async Task UpdateFileStorageAsync(FileStorage file)
        {
            await FileStorageRepository.UpdateFileStorageAsync(file);
        }

    }
}
