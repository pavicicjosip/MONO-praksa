using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Repository.Common;
using TMDb.Model;

namespace TMDb.Service.Common
{
    public interface IFileStorageService
    {
        Task RemoveFileAsync(Guid fileID);
        Task InsertFileAsync(string imageName, string imagePath);
        Task<FileStorage> ReturnFileByIdAsync(Guid fileID);
    }
}
