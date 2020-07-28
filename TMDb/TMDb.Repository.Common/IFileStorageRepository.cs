using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Model;

namespace TMDb.Repository.Common
{
    public interface IFileStorageRepository
    {
        Task RemoveFileAsync(Guid fileID);
        Task InsertFileAsync(string imageName, string imagePath);
        Task<FileStorage> ReturnFileByIdAsync(Guid fileID);

    }
}
